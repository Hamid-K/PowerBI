using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000724 RID: 1828
	[Serializable]
	internal sealed class LineInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x060065F2 RID: 26098 RVA: 0x001909E6 File Offset: 0x0018EBE6
		internal LineInstanceInfo(ReportProcessing.ProcessingContext pc, Line reportItemDef, ReportItemInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x001909F3 File Offset: 0x0018EBF3
		internal LineInstanceInfo(Line reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x001909FC File Offset: 0x0018EBFC
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstanceInfo, memberInfoList);
		}
	}
}
