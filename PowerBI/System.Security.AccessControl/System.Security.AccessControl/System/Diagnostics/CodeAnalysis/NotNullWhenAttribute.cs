using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x0600004A RID: 74 RVA: 0x0000251F File Offset: 0x0000071F
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000252E File Offset: 0x0000072E
		public bool ReturnValue { get; }
	}
}
