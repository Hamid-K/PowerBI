using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000026D8 File Offset: 0x000008D8
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026F0 File Offset: 0x000008F0
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026FF File Offset: 0x000008FF
		public string[] Members { get; }
	}
}
