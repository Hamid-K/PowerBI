using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D0 RID: 1744
	[Serializable]
	internal struct CodeClass
	{
		// Token: 0x06005DB5 RID: 23989 RVA: 0x0017EB20 File Offset: 0x0017CD20
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ClassName, Token.String),
				new MemberInfo(MemberName.InstanceName, Token.String)
			});
		}

		// Token: 0x04002FE6 RID: 12262
		internal string ClassName;

		// Token: 0x04002FE7 RID: 12263
		internal string InstanceName;
	}
}
