using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000767 RID: 1895
	[Serializable]
	internal sealed class CustomReportItemInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006932 RID: 26930 RVA: 0x001998D8 File Offset: 0x00197AD8
		internal CustomReportItemInstanceInfo(ReportProcessing.ProcessingContext pc, CustomReportItem reportItemDef, CustomReportItemInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x001998E4 File Offset: 0x00197AE4
		internal CustomReportItemInstanceInfo(CustomReportItem reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x001998F0 File Offset: 0x00197AF0
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.None, memberInfoList);
		}
	}
}
