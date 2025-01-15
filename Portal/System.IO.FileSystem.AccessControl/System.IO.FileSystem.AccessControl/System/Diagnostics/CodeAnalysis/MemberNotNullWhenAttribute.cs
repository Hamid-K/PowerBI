using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000024AF File Offset: 0x000006AF
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024CE File Offset: 0x000006CE
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024E4 File Offset: 0x000006E4
		public bool ReturnValue { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000024EC File Offset: 0x000006EC
		public string[] Members { get; }
	}
}
