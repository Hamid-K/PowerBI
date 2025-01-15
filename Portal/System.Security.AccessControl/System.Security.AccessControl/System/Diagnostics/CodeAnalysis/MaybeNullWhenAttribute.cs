using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002508 File Offset: 0x00000708
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002517 File Offset: 0x00000717
		public bool ReturnValue { get; }
	}
}
