using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000024 RID: 36
	public struct SecurityHelper
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00003078 File Offset: 0x00001278
		public SecurityHelper(IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._context = context;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003090 File Offset: 0x00001290
		public void AddUserIdentity(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsPrincipal newClaimsPrincipal = new ClaimsPrincipal(identity);
			IPrincipal existingPrincipal = this._context.Request.User;
			if (existingPrincipal != null)
			{
				ClaimsPrincipal existingClaimsPrincipal = existingPrincipal as ClaimsPrincipal;
				if (existingClaimsPrincipal == null)
				{
					IIdentity existingIdentity = existingPrincipal.Identity;
					if (existingIdentity.IsAuthenticated)
					{
						newClaimsPrincipal.AddIdentity((existingIdentity as ClaimsIdentity) ?? new ClaimsIdentity(existingIdentity));
					}
				}
				else
				{
					foreach (ClaimsIdentity existingClaimsIdentity in existingClaimsPrincipal.Identities)
					{
						if (existingClaimsIdentity.IsAuthenticated)
						{
							newClaimsPrincipal.AddIdentity(existingClaimsIdentity);
						}
					}
				}
			}
			this._context.Request.User = newClaimsPrincipal;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003158 File Offset: 0x00001358
		public AuthenticationResponseChallenge LookupChallenge(string authenticationType, AuthenticationMode authenticationMode)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			AuthenticationResponseChallenge challenge = this._context.Authentication.AuthenticationResponseChallenge;
			if (challenge != null && challenge.AuthenticationTypes != null && challenge.AuthenticationTypes.Length != 0)
			{
				foreach (string challengeType in challenge.AuthenticationTypes)
				{
					if (string.Equals(challengeType, authenticationType, StringComparison.Ordinal))
					{
						return challenge;
					}
				}
				return null;
			}
			if (authenticationMode != AuthenticationMode.Active)
			{
				return null;
			}
			return challenge ?? new AuthenticationResponseChallenge(null, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000031DC File Offset: 0x000013DC
		public AuthenticationResponseGrant LookupSignIn(string authenticationType)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			AuthenticationResponseGrant grant = this._context.Authentication.AuthenticationResponseGrant;
			if (grant == null)
			{
				return null;
			}
			foreach (ClaimsIdentity claimsIdentity in grant.Principal.Identities)
			{
				if (string.Equals(authenticationType, claimsIdentity.AuthenticationType, StringComparison.Ordinal))
				{
					return new AuthenticationResponseGrant(claimsIdentity, grant.Properties ?? new AuthenticationProperties());
				}
			}
			return null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003278 File Offset: 0x00001478
		public AuthenticationResponseRevoke LookupSignOut(string authenticationType, AuthenticationMode authenticationMode)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			AuthenticationResponseRevoke revoke = this._context.Authentication.AuthenticationResponseRevoke;
			if (revoke == null)
			{
				return null;
			}
			if (revoke.AuthenticationTypes != null && revoke.AuthenticationTypes.Length != 0)
			{
				for (int index = 0; index != revoke.AuthenticationTypes.Length; index++)
				{
					if (string.Equals(authenticationType, revoke.AuthenticationTypes[index], StringComparison.Ordinal))
					{
						return revoke;
					}
				}
				return null;
			}
			if (authenticationMode != AuthenticationMode.Active)
			{
				return null;
			}
			return revoke;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000032E8 File Offset: 0x000014E8
		public bool Equals(SecurityHelper other)
		{
			return object.Equals(this._context, other._context);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000032FB File Offset: 0x000014FB
		public override bool Equals(object obj)
		{
			return obj is SecurityHelper && this.Equals((SecurityHelper)obj);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003313 File Offset: 0x00001513
		public override int GetHashCode()
		{
			if (this._context == null)
			{
				return 0;
			}
			return this._context.GetHashCode();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000332A File Offset: 0x0000152A
		public static bool operator ==(SecurityHelper left, SecurityHelper right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003334 File Offset: 0x00001534
		public static bool operator !=(SecurityHelper left, SecurityHelper right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000046 RID: 70
		private readonly IOwinContext _context;
	}
}
