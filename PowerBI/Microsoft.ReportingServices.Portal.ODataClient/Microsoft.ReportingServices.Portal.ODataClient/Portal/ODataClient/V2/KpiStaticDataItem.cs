using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000009 RID: 9
	[OriginalName("KpiStaticDataItem")]
	public class KpiStaticDataItem : KpiDataItem
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002929 File Offset: 0x00000B29
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static KpiStaticDataItem CreateKpiStaticDataItem(KpiDataItemType type)
		{
			return new KpiStaticDataItem
			{
				Type = type
			};
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002937 File Offset: 0x00000B37
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000293F File Offset: 0x00000B3F
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

		// Token: 0x0400005C RID: 92
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
