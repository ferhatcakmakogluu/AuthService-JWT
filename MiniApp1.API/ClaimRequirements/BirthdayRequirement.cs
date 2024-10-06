using Microsoft.AspNetCore.Authorization;

namespace MiniApp1.API.ClaimRequirements
{
    public class BirthdayRequirement : IAuthorizationRequirement
    {
        public int Age { get; set; }

        public BirthdayRequirement(int age)
        {
            Age = age;
        }

        public class BirthdayRequrenmentHandler : AuthorizationHandler<BirthdayRequirement>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                BirthdayRequirement requirement)
            {
                //olusturdugumuz claimlerden birth-date i al
                var birthdate = context.User.FindFirst("Birth-Date");
                
                if(birthdate == null)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                var today = DateTime.Now;
                var age = today.Year - Convert.ToDateTime(birthdate.Value).Year;

                if(age >= requirement.Age)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
                return Task.CompletedTask;
            }
        }
    }
}
