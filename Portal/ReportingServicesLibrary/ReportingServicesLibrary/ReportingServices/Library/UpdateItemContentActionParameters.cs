using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200004A RID: 74
	internal class UpdateItemContentActionParameters : UpdateItemActionParameters
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000FC84 File Offset: 0x0000DE84
		public CatalogItemContent CatalogItemContent { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000FC8D File Offset: 0x0000DE8D
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", base.ItemPath);
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
			if (this.CatalogItemContent == null)
			{
				throw new MissingParameterException("CatalogItemContent");
			}
		}
	}
}
