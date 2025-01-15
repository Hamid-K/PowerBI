using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003E5 RID: 997
	internal sealed class GaugeMemberList : HierarchyNodeList
	{
		// Token: 0x1700147D RID: 5245
		internal GaugeMember this[int index]
		{
			get
			{
				return (GaugeMember)base[index];
			}
		}
	}
}
