using Shouldly;
using Volo.Abp.Identity;
using Xunit;

namespace RatTracker.Samples
{
    /* This is just an example test class.
     * Normally, you don't test code of the modules you are using
     * (like IdentityUserManager here).
     * Only test your own domain services.
     */

    public class SampleDomainTests : RatTrackerDomainTestBase
    {
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IdentityUserManager _identityUserManager;

        public SampleDomainTests()
        {
            _identityUserRepository = GetRequiredService<IIdentityUserRepository>();
            _identityUserManager = GetRequiredService<IdentityUserManager>();
        }

        [Fact]
        public async Task Should_Set_Email_Of_A_User()
        {
            IdentityUser adminUser;

            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                adminUser = await _identityUserRepository
                    .FindByNormalizedUserNameAsync("ADMIN").ConfigureAwait(false);

                await _identityUserManager.SetEmailAsync(adminUser, "newemail@abp.io").ConfigureAwait(false);
                await _identityUserRepository.UpdateAsync(adminUser).ConfigureAwait(false);
            }).ConfigureAwait(false);

            adminUser = await _identityUserRepository.FindByNormalizedUserNameAsync("ADMIN").ConfigureAwait(false);
            adminUser.Email.ShouldBe("newemail@abp.io");
        }
    }
}
