using System;
using System.Security.Principal;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200045D RID: 1117
	public sealed class AuthenticationResult
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x0007EDFA File Offset: 0x0007CFFA
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x0007EE02 File Offset: 0x0007D002
		public AuthenticationResultType AuthenticationResultType { get; private set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0007EE0B File Offset: 0x0007D00B
		public bool IsAuthenticated
		{
			get
			{
				return this.AuthenticationResultType == AuthenticationResultType.Success;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0007EE16 File Offset: 0x0007D016
		public bool IsFailure
		{
			get
			{
				return this.AuthenticationResultType != AuthenticationResultType.Success && this.AuthenticationResultType != AuthenticationResultType.StopAndRedirect;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0007EE2F File Offset: 0x0007D02F
		public bool IsRedirect
		{
			get
			{
				return this.AuthenticationResultType == AuthenticationResultType.StopAndRedirect;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0007EE3A File Offset: 0x0007D03A
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x0007EE42 File Offset: 0x0007D042
		public bool IsAnonymous { get; private set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0007EE4B File Offset: 0x0007D04B
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x0007EE53 File Offset: 0x0007D053
		public IIdentity UserIdentity { get; private set; }

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0007EE5C File Offset: 0x0007D05C
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x0007EE64 File Offset: 0x0007D064
		public IIdentity ServiceIdentity { get; private set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x0007EE6D File Offset: 0x0007D06D
		// (set) Token: 0x060022F8 RID: 8952 RVA: 0x0007EE75 File Offset: 0x0007D075
		public object AuthenticationContext { get; private set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0007EE7E File Offset: 0x0007D07E
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0007EE86 File Offset: 0x0007D086
		public string AuthenticationIdentifier { get; private set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x0007EE8F File Offset: 0x0007D08F
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x0007EE97 File Offset: 0x0007D097
		public bool SkipDospViolationReport { get; private set; }

		// Token: 0x060022FD RID: 8957 RVA: 0x0007EEA0 File Offset: 0x0007D0A0
		public AuthenticationResult(AuthenticationResultType authenticationResultType, IIdentity userIdentity, IIdentity serviceIdentity)
			: this(authenticationResultType, userIdentity, serviceIdentity, null, null)
		{
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0007EEAD File Offset: 0x0007D0AD
		public AuthenticationResult(AuthenticationResultType authenticationResultType, IIdentity userIdentity, IIdentity serviceIdentity, string authenticationIdentifier, object context)
			: this(authenticationResultType, false, userIdentity, serviceIdentity, authenticationIdentifier, context, false)
		{
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0007EEC0 File Offset: 0x0007D0C0
		public AuthenticationResult(AuthenticationResultType authenticationResultType, bool isAnonymous, IIdentity userIdentity, IIdentity serviceIdentity, string authenticationIdentifier, object context, bool skipDospViolationReport = false)
		{
			if (authenticationResultType == AuthenticationResultType.Success)
			{
				if (!isAnonymous)
				{
					ExtendedDiagnostics.EnsureOperation(userIdentity != null || serviceIdentity != null, "You must provide either userIdentity or serviceIdentity for non-anonymous authenticated user/service");
				}
			}
			else
			{
				ExtendedDiagnostics.EnsureOperation(userIdentity == null && serviceIdentity == null, "Please do not provide identity for unauthenticated user/service");
			}
			this.AuthenticationResultType = authenticationResultType;
			this.IsAnonymous = isAnonymous;
			this.UserIdentity = userIdentity;
			this.ServiceIdentity = serviceIdentity;
			this.AuthenticationIdentifier = authenticationIdentifier;
			this.AuthenticationContext = context;
			this.SkipDospViolationReport = skipDospViolationReport;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x0007EF3C File Offset: 0x0007D13C
		public override string ToString()
		{
			return "AuthenticationResultType={0}, UserIdentity={1}, ServiceIdentity={2}".FormatWithInvariantCulture(new object[]
			{
				this.AuthenticationResultType,
				(this.UserIdentity != null) ? this.UserIdentity.Name : "null",
				(this.ServiceIdentity != null) ? this.ServiceIdentity.Name : "null"
			});
		}

		// Token: 0x04000C29 RID: 3113
		public static readonly AuthenticationResult Failed = new AuthenticationResult(AuthenticationResultType.FailAndContinue, null, null);

		// Token: 0x04000C2A RID: 3114
		public static readonly AuthenticationResult FailedAndStop = new AuthenticationResult(AuthenticationResultType.FailAndStop, null, null);

		// Token: 0x04000C2B RID: 3115
		public static readonly AuthenticationResult FailedAndStopSkipDosp = new AuthenticationResult(AuthenticationResultType.FailAndStop, false, null, null, null, null, true);

		// Token: 0x04000C2C RID: 3116
		public static readonly AuthenticationResult StopAndRedirect = new AuthenticationResult(AuthenticationResultType.StopAndRedirect, null, null);

		// Token: 0x04000C2D RID: 3117
		public static readonly IIdentity AnonymousIdentity = new GenericIdentity("Anonymous-8BAB87CC-7A98-4D69-A06B-EB15A25F8EBF");

		// Token: 0x04000C2E RID: 3118
		public static readonly AuthenticationResult Anonymous = new AuthenticationResult(AuthenticationResultType.Success, true, AuthenticationResult.AnonymousIdentity, null, null, null, false);
	}
}
