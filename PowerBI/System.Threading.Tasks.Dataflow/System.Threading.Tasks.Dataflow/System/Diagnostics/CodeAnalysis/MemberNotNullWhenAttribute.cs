using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002489 File Offset: 0x00000689
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024A8 File Offset: 0x000006A8
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000024BE File Offset: 0x000006BE
		public bool ReturnValue { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024C6 File Offset: 0x000006C6
		public string[] Members { get; }
	}
}
