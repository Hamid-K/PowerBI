using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000024 RID: 36
	public interface IAuthenticationManager
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001AE RID: 430
		// (set) Token: 0x060001AF RID: 431
		ClaimsPrincipal User { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001B0 RID: 432
		// (set) Token: 0x060001B1 RID: 433
		AuthenticationResponseChallenge AuthenticationResponseChallenge { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001B2 RID: 434
		// (set) Token: 0x060001B3 RID: 435
		AuthenticationResponseGrant AuthenticationResponseGrant { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001B4 RID: 436
		// (set) Token: 0x060001B5 RID: 437
		AuthenticationResponseRevoke AuthenticationResponseRevoke { get; set; }

		// Token: 0x060001B6 RID: 438
		IEnumerable<AuthenticationDescription> GetAuthenticationTypes();

		// Token: 0x060001B7 RID: 439
		IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate);

		// Token: 0x060001B8 RID: 440
		Task<AuthenticateResult> AuthenticateAsync(string authenticationType);

		// Token: 0x060001B9 RID: 441
		Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes);

		// Token: 0x060001BA RID: 442
		void Challenge(AuthenticationProperties properties, params string[] authenticationTypes);

		// Token: 0x060001BB RID: 443
		void Challenge(params string[] authenticationTypes);

		// Token: 0x060001BC RID: 444
		void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);

		// Token: 0x060001BD RID: 445
		void SignIn(params ClaimsIdentity[] identities);

		// Token: 0x060001BE RID: 446
		void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);

		// Token: 0x060001BF RID: 447
		void SignOut(params string[] authenticationTypes);
	}
}
