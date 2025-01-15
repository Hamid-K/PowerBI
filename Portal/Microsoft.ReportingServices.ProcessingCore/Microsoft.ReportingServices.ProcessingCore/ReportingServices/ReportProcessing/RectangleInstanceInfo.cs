using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000729 RID: 1833
	[Serializable]
	internal sealed class RectangleInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006621 RID: 26145 RVA: 0x00190F51 File Offset: 0x0018F151
		internal RectangleInstanceInfo(ReportProcessing.ProcessingContext pc, Rectangle reportItemDef, RectangleInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x00190F5E File Offset: 0x0018F15E
		internal RectangleInstanceInfo(Rectangle reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x00190F68 File Offset: 0x0018F168
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}
