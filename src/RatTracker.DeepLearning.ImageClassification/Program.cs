using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Vision;
using static Microsoft.ML.DataOperationsCatalog;

namespace RatTracker.DeepLearning.ImageClassification
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            var workspaceRelativePath = Path.Combine(projectDirectory, "workspace");
            var assetsRelativePath = Path.Combine(projectDirectory, "assets");

            Console.WriteLine($"Project dir: {projectDirectory}");
            Console.WriteLine($"workspaceRelative dir: {workspaceRelativePath}");
            Console.WriteLine($"assetsRelative dir: {assetsRelativePath}");

            var mlContext = new MLContext();

            // You must unzip assets.zip before training
            Console.WriteLine("Loading images ..");
            var images = LoadImagesFromDirectory(folder: assetsRelativePath, useFolderNameAsLabel: true);
            Console.WriteLine($"Found {images.Count()} images.");

            var imageData = mlContext.Data.LoadFromEnumerable(images);

            var shuffledData = mlContext.Data.ShuffleRows(imageData);

            var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "Label",
                    outputColumnName: "LabelAsKey")
                .Append(mlContext.Transforms.LoadRawImageBytes(
                    outputColumnName: "Image",
                    imageFolder: assetsRelativePath,
                    inputColumnName: "ImagePath"));

            var preProcessedData = preprocessingPipeline
                                .Fit(shuffledData)
                                .Transform(shuffledData);

            var trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
            var validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);

            var trainSet = trainSplit.TrainSet;
            var validationSet = validationTestSplit.TrainSet;
            var testSet = validationTestSplit.TestSet;

            var classifierOptions = new ImageClassificationTrainer.Options()
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                ValidationSet = validationSet,
                Arch = ImageClassificationTrainer.Architecture.ResnetV2101,
                MetricsCallback = (metrics) => Console.WriteLine(metrics),
                TestOnTrainSet = false,
                ReuseTrainSetBottleneckCachedValues = true,
                ReuseValidationSetBottleneckCachedValues = true
            };

            var trainingPipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(classifierOptions)
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            ITransformer trainedModel = trainingPipeline.Fit(trainSet);
            Console.WriteLine("Model produced.");

            ClassifySingleImage(mlContext, testSet, trainedModel);

            ClassifyImages(mlContext, testSet, trainedModel);

            Console.ReadKey();
        }

        public static void ClassifySingleImage(MLContext mlContext, IDataView data, ITransformer trainedModel)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);

            var image = mlContext.Data.CreateEnumerable<ModelInput>(data, reuseRowObject: true);
            if (image != null && image.Any() && image.FirstOrDefault() != null)
            {
                var prediction = predictionEngine.Predict(image.FirstOrDefault());

                Console.WriteLine("Classifying single image");
                OutputPrediction(prediction);
            }
            else
            {
                Console.WriteLine("Couldn't find a single image?");
            }
        }

        public static void ClassifyImages(MLContext mlContext, IDataView data, ITransformer trainedModel)
        {
            var predictionData = trainedModel.Transform(data);

            var predictions = mlContext.Data.CreateEnumerable<ModelOutput>(predictionData, reuseRowObject: true).Take(10);

            if (predictions != null && predictions.Any())
            {
                Console.WriteLine("Classifying multiple images");
                foreach (var prediction in predictions)
                {
                    OutputPrediction(prediction);
                }
            }
            else
            {
                Console.WriteLine("No predictions?");
            }

        }

        private static void OutputPrediction(ModelOutput prediction)
        {
            var imageName = Path.GetFileName(prediction.ImagePath);
            Console.WriteLine($"Image: {imageName} | Actual Value: {prediction.Label} | Predicted Value: {prediction.PredictedLabel}");
        }

        public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder, bool useFolderNameAsLabel = true)
        {
            var files = Directory.GetFiles(folder, "*",
                searchOption: SearchOption.AllDirectories);

            List<ImageData> retVal = new List<ImageData>();

            foreach (var file in files)
            {
                if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png"))
                    continue;

                var label = Path.GetFileName(file);

                if (useFolderNameAsLabel)
                    label = Directory.GetParent(file).Name;
                else
                {
                    for (var index = 0; index < label.Length; index++)
                    {
                        if (!char.IsLetter(label[index]))
                        {
                            label = label.Substring(0, index);
                            break;
                        }
                    }
                }

                retVal.Add(new ImageData()
                {
                    ImagePath = file,
                    Label = label
                });
            }

            return retVal.AsEnumerable();
        }
    }

    internal class ImageData
    {
        public string ImagePath { get; set; }

        public string Label { get; set; }
    }

    internal class ModelInput
    {
        public byte[] Image { get; set; }

        public uint LabelAsKey { get; set; }

        public string ImagePath { get; set; }

        public string Label { get; set; }
    }

    internal class ModelOutput
    {
        public string ImagePath { get; set; }

        public string Label { get; set; }

        public string PredictedLabel { get; set; }
    }
}
