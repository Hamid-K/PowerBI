using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000063 RID: 99
	internal sealed class CreateSystemResourceActionParameters : CreateItemActionParameters
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000115C4 File Offset: 0x0000F7C4
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x000115CC File Offset: 0x0000F7CC
		public byte[] Content { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000115D5 File Offset: 0x0000F7D5
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000115DD File Offset: 0x0000F7DD
		public string MimeType { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x000115E6 File Offset: 0x0000F7E6
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ItemName, base.ParentPath, this.MimeType);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001160C File Offset: 0x0000F80C
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("ItemName");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("ParentPath");
			}
			if (this.Content == null)
			{
				throw new MissingParameterException("Content");
			}
			if (this.MimeType == null)
			{
				throw new MissingParameterException("MimeType");
			}
		}
	}
}
