using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200052B RID: 1323
	internal sealed class VisibilityToggleInfo
	{
		// Token: 0x04001FDD RID: 8157
		internal ObjectType ObjectType;

		// Token: 0x04001FDE RID: 8158
		internal string ObjectName;

		// Token: 0x04001FDF RID: 8159
		internal Visibility Visibility;

		// Token: 0x04001FE0 RID: 8160
		internal string GroupName;

		// Token: 0x04001FE1 RID: 8161
		internal Hashtable GroupingSet;

		// Token: 0x04001FE2 RID: 8162
		internal bool IsTablixMember;
	}
}
