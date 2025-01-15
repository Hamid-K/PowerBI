using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000B3B7 File Offset: 0x000095B7
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B3CF File Offset: 0x000095CF
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B3DE File Offset: 0x000095DE
		public string[] Members { get; }
	}
}
