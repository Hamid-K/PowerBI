using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000AC RID: 172
	[OriginalName("KpiStaticDataItem")]
	public class KpiStaticDataItem : KpiDataItem
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x0000ED65 File Offset: 0x0000CF65
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static KpiStaticDataItem CreateKpiStaticDataItem(KpiDataItemType type)
		{
			return new KpiStaticDataItem
			{
				Type = type
			};
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0000ED73 File Offset: 0x0000CF73
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0000ED7B File Offset: 0x0000CF7B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.OnPropertyChanged("Value");
			}
		}

		// Token: 0x0400037E RID: 894
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
