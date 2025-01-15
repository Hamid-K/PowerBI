using System;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000006 RID: 6
	public struct ExpressionValidationResult
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000003C0
		public bool Supported
		{
			get
			{
				return this._supported;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021C8 File Offset: 0x000003C8
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021D0 File Offset: 0x000003D0
		public static ExpressionValidationResult NotSupported
		{
			get
			{
				return new ExpressionValidationResult(false, false);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D9 File Offset: 0x000003D9
		public ExpressionValidationResult(bool supported, bool enabled)
		{
			this._supported = supported;
			this._enabled = enabled;
		}

		// Token: 0x04000002 RID: 2
		private readonly bool _supported;

		// Token: 0x04000003 RID: 3
		private readonly bool _enabled;
	}
}
