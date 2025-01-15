using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020D4 File Offset: 0x000002D4
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020E3 File Offset: 0x000002E3
		public bool ReturnValue { get; }
	}
}
