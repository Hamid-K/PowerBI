using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000714 RID: 1812
	[Serializable]
	internal abstract class InfoBase
	{
		// Token: 0x06006521 RID: 25889 RVA: 0x0018F0C0 File Offset: 0x0018D2C0
		internal static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.None, memberInfoList);
		}
	}
}
