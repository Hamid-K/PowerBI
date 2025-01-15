using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E0C RID: 3596
	[DataContract]
	internal class CdpaFunnelRequest : CdpaRequestWithTimeConfigurationAndResponseProtocol
	{
		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x060060DF RID: 24799 RVA: 0x0014A425 File Offset: 0x00148625
		[DataMember(Name = "dataModelType", IsRequired = true)]
		public string DataModelType
		{
			get
			{
				return "funnel";
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x060060E0 RID: 24800 RVA: 0x0014A42C File Offset: 0x0014862C
		// (set) Token: 0x060060E1 RID: 24801 RVA: 0x0014A434 File Offset: 0x00148634
		[DataMember(Name = "dataModel", IsRequired = true)]
		public CdpaFunnelDefinition DataModel { get; set; }

		// Token: 0x060060E2 RID: 24802 RVA: 0x0014A440 File Offset: 0x00148640
		public override CdpaRequestWithTimeConfigurationAndResponseProtocol ShallowCopy()
		{
			return new CdpaFunnelRequest
			{
				DataModel = this.DataModel,
				Granularity = base.Granularity,
				TimeRange = base.TimeRange,
				ResponseFormat = base.ResponseFormat,
				ResponseProtocol = base.ResponseProtocol
			};
		}
	}
}
