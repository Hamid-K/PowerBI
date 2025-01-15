using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000031CE File Offset: 0x000013CE
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000031DD File Offset: 0x000013DD
		public bool ReturnValue { get; }
	}
}
