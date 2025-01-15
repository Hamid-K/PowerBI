using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00003232 File Offset: 0x00001432
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000324A File Offset: 0x0000144A
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003259 File Offset: 0x00001459
		public string[] Members { get; }
	}
}
