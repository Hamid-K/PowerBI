using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200005E RID: 94
	internal class ExternalOriginalItemPath : ExternalItemPath
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00009FD5 File Offset: 0x000081D5
		public ExternalOriginalItemPath(string value, string editSessionID)
			: base(value, editSessionID)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009FDF File Offset: 0x000081DF
		private ExternalOriginalItemPath(string value, string editSessionID, bool runChecks)
			: base(value, editSessionID)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009FE9 File Offset: 0x000081E9
		public new static ExternalOriginalItemPath CreateWithoutChecks(string value, string editSessionID)
		{
			return new ExternalOriginalItemPath(value, editSessionID, false);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00009FF3 File Offset: 0x000081F3
		public override string FullEditSessionIdentifier
		{
			get
			{
				return base.Value;
			}
		}
	}
}
