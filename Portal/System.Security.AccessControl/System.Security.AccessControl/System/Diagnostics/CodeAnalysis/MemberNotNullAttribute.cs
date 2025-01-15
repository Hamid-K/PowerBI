using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x06000051 RID: 81 RVA: 0x0000256C File Offset: 0x0000076C
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002584 File Offset: 0x00000784
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002593 File Offset: 0x00000793
		public string[] Members { get; }
	}
}
