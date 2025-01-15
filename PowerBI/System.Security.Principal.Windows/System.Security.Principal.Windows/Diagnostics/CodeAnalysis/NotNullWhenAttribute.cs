using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020B9 File Offset: 0x000002B9
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020C8 File Offset: 0x000002C8
		public bool ReturnValue { get; }
	}
}
