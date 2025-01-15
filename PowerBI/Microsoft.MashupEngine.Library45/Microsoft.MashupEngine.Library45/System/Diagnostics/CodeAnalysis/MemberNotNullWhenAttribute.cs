using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02002060 RID: 8288
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x060113BF RID: 70591 RVA: 0x003B547F File Offset: 0x003B367F
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x060113C0 RID: 70592 RVA: 0x003B549E File Offset: 0x003B369E
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x17002E0E RID: 11790
		// (get) Token: 0x060113C1 RID: 70593 RVA: 0x003B54B4 File Offset: 0x003B36B4
		public bool ReturnValue { get; }

		// Token: 0x17002E0F RID: 11791
		// (get) Token: 0x060113C2 RID: 70594 RVA: 0x003B54BC File Offset: 0x003B36BC
		public string[] Members { get; }
	}
}
