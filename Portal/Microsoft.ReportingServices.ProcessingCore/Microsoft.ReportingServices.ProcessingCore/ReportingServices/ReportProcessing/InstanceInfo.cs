using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000716 RID: 1814
	[Serializable]
	internal abstract class InstanceInfo : InfoBase
	{
		// Token: 0x06006528 RID: 25896 RVA: 0x0018F140 File Offset: 0x0018D340
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.InfoBase, memberInfoList);
		}
	}
}
