using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002433 File Offset: 0x00000633
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002442 File Offset: 0x00000642
		public bool ReturnValue { get; }
	}
}
