using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200023A RID: 570
	internal class ActiveState
	{
		// Token: 0x060014B7 RID: 5303 RVA: 0x000507D9 File Offset: 0x0004E9D9
		public ActiveState()
		{
			this.DeliveryExtensionRemovedSpecified = false;
			this.SharedDataSourceRemovedSpecified = false;
			this.MissingParameterValueSpecified = false;
			this.InvalidParameterValueSpecified = false;
			this.UnknownItemParameterSpecified = false;
			this.CachingNotEnabledOnItemSpecified = false;
			this.DisabledByUserSpecified = false;
		}

		// Token: 0x04000761 RID: 1889
		public bool DeliveryExtensionRemoved;

		// Token: 0x04000762 RID: 1890
		public bool DeliveryExtensionRemovedSpecified;

		// Token: 0x04000763 RID: 1891
		public bool SharedDataSourceRemoved;

		// Token: 0x04000764 RID: 1892
		public bool SharedDataSourceRemovedSpecified;

		// Token: 0x04000765 RID: 1893
		public bool MissingParameterValue;

		// Token: 0x04000766 RID: 1894
		public bool MissingParameterValueSpecified;

		// Token: 0x04000767 RID: 1895
		public bool InvalidParameterValue;

		// Token: 0x04000768 RID: 1896
		public bool InvalidParameterValueSpecified;

		// Token: 0x04000769 RID: 1897
		public bool UnknownItemParameter;

		// Token: 0x0400076A RID: 1898
		public bool UnknownItemParameterSpecified;

		// Token: 0x0400076B RID: 1899
		public bool CachingNotEnabledOnItem;

		// Token: 0x0400076C RID: 1900
		public bool CachingNotEnabledOnItemSpecified;

		// Token: 0x0400076D RID: 1901
		public bool DisabledByUser;

		// Token: 0x0400076E RID: 1902
		public bool DisabledByUserSpecified;
	}
}
