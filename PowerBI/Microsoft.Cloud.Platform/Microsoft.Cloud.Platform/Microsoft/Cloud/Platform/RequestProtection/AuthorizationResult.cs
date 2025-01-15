using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200045F RID: 1119
	public sealed class AuthorizationResult
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0007F00D File Offset: 0x0007D20D
		public bool IsAuthorized
		{
			get
			{
				return this.AuthorizationResultType == AuthorizationResultType.Success;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0007F018 File Offset: 0x0007D218
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x0007F020 File Offset: 0x0007D220
		public object AuthorizationContext { get; private set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0007F029 File Offset: 0x0007D229
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x0007F031 File Offset: 0x0007D231
		public AuthorizationResultType AuthorizationResultType { get; private set; }

		// Token: 0x06002307 RID: 8967 RVA: 0x0007F03A File Offset: 0x0007D23A
		public AuthorizationResult(bool isAuthorized)
			: this(isAuthorized, null)
		{
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x0007F044 File Offset: 0x0007D244
		public AuthorizationResult(bool isAuthorized, object context)
			: this(isAuthorized ? AuthorizationResultType.Success : AuthorizationResultType.FailAndContinue, context)
		{
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0007F054 File Offset: 0x0007D254
		public AuthorizationResult(AuthorizationResultType authorizationResultType, object context)
		{
			this.AuthorizationResultType = authorizationResultType;
			this.AuthorizationContext = context;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0007F06A File Offset: 0x0007D26A
		public override string ToString()
		{
			return "IsAuthorized={0}, AuthorizationContext={1}".FormatWithInvariantCulture(new object[] { this.IsAuthorized, this.AuthorizationContext });
		}

		// Token: 0x04000C3B RID: 3131
		public static readonly AuthorizationResult Failed = new AuthorizationResult(false, null);

		// Token: 0x04000C3C RID: 3132
		public static readonly AuthorizationResult Succeeded = new AuthorizationResult(true, null);

		// Token: 0x04000C3D RID: 3133
		public static readonly AuthorizationResult FailedAndStop = new AuthorizationResult(AuthorizationResultType.FailAndStop, null);
	}
}
