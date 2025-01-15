using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000259B File Offset: 0x0000079B
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000025BA File Offset: 0x000007BA
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000025D0 File Offset: 0x000007D0
		public bool ReturnValue { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000025D8 File Offset: 0x000007D8
		public string[] Members { get; }
	}
}
