using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020A2 File Offset: 0x000002A2
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020B1 File Offset: 0x000002B1
		public bool ReturnValue { get; }
	}
}
