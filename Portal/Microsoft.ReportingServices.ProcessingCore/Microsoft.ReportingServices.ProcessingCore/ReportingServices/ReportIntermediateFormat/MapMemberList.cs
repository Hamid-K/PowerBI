using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000418 RID: 1048
	internal sealed class MapMemberList : HierarchyNodeList
	{
		// Token: 0x170015E2 RID: 5602
		internal MapMember this[int index]
		{
			get
			{
				return (MapMember)base[index];
			}
		}
	}
}
