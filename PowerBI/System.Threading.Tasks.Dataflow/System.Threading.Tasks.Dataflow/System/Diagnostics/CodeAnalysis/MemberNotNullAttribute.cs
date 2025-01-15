using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000245A File Offset: 0x0000065A
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002472 File Offset: 0x00000672
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002481 File Offset: 0x00000681
		public string[] Members { get; }
	}
}
