using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06000123 RID: 291 RVA: 0x000031E5 File Offset: 0x000013E5
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000031F4 File Offset: 0x000013F4
		public bool ReturnValue { get; }
	}
}
