using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200017D RID: 381
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class AssemblyCodeTypeAttribute : Attribute
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x00022473 File Offset: 0x00020673
		public AssemblyCodeTypeAttribute(CodeType codeType)
		{
			this.m_codeType = codeType;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00022482 File Offset: 0x00020682
		public CodeType CodeType
		{
			get
			{
				return this.m_codeType;
			}
		}

		// Token: 0x040003DA RID: 986
		private CodeType m_codeType;
	}
}
