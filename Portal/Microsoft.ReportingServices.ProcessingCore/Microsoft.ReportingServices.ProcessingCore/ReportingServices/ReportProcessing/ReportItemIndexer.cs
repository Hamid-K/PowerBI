using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006CE RID: 1742
	[Serializable]
	internal struct ReportItemIndexer
	{
		// Token: 0x06005DAB RID: 23979 RVA: 0x0017EA4C File Offset: 0x0017CC4C
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.IsComputed, Token.Boolean),
				new MemberInfo(MemberName.Index, Token.Int32)
			});
		}

		// Token: 0x04002FE2 RID: 12258
		internal bool IsComputed;

		// Token: 0x04002FE3 RID: 12259
		internal int Index;
	}
}
