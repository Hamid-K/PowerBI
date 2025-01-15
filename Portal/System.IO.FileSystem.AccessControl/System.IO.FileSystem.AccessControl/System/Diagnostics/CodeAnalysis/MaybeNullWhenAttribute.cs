using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000241C File Offset: 0x0000061C
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000242B File Offset: 0x0000062B
		public bool ReturnValue { get; }
	}
}
