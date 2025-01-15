using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000B3E6 File Offset: 0x000095E6
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B405 File Offset: 0x00009605
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B41B File Offset: 0x0000961B
		public bool ReturnValue { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B423 File Offset: 0x00009623
		public string[] Members { get; }
	}
}
