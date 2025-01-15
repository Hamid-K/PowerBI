using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000340 RID: 832
	public class ActiveState
	{
		// Token: 0x06001BE8 RID: 7144 RVA: 0x000025F4 File Offset: 0x000007F4
		public ActiveState()
		{
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00070F88 File Offset: 0x0006F188
		internal ActiveState(ActiveState state)
		{
			this.DeliveryExtensionRemoved = state.DeliveryExtensionRemoved;
			this.DeliveryExtensionRemovedSpecified = state.DeliveryExtensionRemovedSpecified;
			this.SharedDataSourceRemoved = state.SharedDataSourceRemoved;
			this.SharedDataSourceRemovedSpecified = state.SharedDataSourceRemovedSpecified;
			this.MissingParameterValue = state.MissingParameterValue;
			this.MissingParameterValueSpecified = state.MissingParameterValueSpecified;
			this.InvalidParameterValue = state.InvalidParameterValue;
			this.InvalidParameterValueSpecified = state.InvalidParameterValueSpecified;
			this.UnknownReportParameter = state.UnknownItemParameter;
			this.UnknownReportParameterSpecified = state.UnknownItemParameterSpecified;
			this.DisabledByUser = state.DisabledByUser;
			this.DisabledByUserSpecified = state.DisabledByUserSpecified;
		}

		// Token: 0x04000B44 RID: 2884
		public bool DeliveryExtensionRemoved;

		// Token: 0x04000B45 RID: 2885
		[XmlIgnore]
		public bool DeliveryExtensionRemovedSpecified;

		// Token: 0x04000B46 RID: 2886
		public bool SharedDataSourceRemoved;

		// Token: 0x04000B47 RID: 2887
		[XmlIgnore]
		public bool SharedDataSourceRemovedSpecified;

		// Token: 0x04000B48 RID: 2888
		public bool MissingParameterValue;

		// Token: 0x04000B49 RID: 2889
		[XmlIgnore]
		public bool MissingParameterValueSpecified;

		// Token: 0x04000B4A RID: 2890
		public bool InvalidParameterValue;

		// Token: 0x04000B4B RID: 2891
		[XmlIgnore]
		public bool InvalidParameterValueSpecified;

		// Token: 0x04000B4C RID: 2892
		public bool UnknownReportParameter;

		// Token: 0x04000B4D RID: 2893
		[XmlIgnore]
		public bool UnknownReportParameterSpecified;

		// Token: 0x04000B4E RID: 2894
		public bool DisabledByUser;

		// Token: 0x04000B4F RID: 2895
		[XmlIgnore]
		public bool DisabledByUserSpecified;
	}
}
