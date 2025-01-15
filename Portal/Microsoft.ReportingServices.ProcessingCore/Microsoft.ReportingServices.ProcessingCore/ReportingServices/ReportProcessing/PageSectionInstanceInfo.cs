using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000755 RID: 1877
	[Serializable]
	internal sealed class PageSectionInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006825 RID: 26661 RVA: 0x00195A68 File Offset: 0x00193C68
		internal PageSectionInstanceInfo(ReportProcessing.ProcessingContext pc, PageSection reportItemDef, PageSectionInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00195A74 File Offset: 0x00193C74
		internal PageSectionInstanceInfo(PageSection reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x00195A80 File Offset: 0x00193C80
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.None, memberInfoList);
		}
	}
}
