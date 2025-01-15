using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200205F RID: 8287
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x060113BC RID: 70588 RVA: 0x003B5450 File Offset: 0x003B3650
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[] { member };
		}

		// Token: 0x060113BD RID: 70589 RVA: 0x003B5468 File Offset: 0x003B3668
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x17002E0D RID: 11789
		// (get) Token: 0x060113BE RID: 70590 RVA: 0x003B5477 File Offset: 0x003B3677
		public string[] Members { get; }
	}
}
