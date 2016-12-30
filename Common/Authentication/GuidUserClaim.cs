using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Common.Authentication
{
    public class GuidUserClaim : IdentityUserClaim<Guid>
    {
    }
}
