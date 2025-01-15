using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00003261 File Offset: 0x00001461
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[] { member };
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003280 File Offset: 0x00001480
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00003296 File Offset: 0x00001496
		public bool ReturnValue { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000329E File Offset: 0x0000149E
		public string[] Members { get; }
	}
}
