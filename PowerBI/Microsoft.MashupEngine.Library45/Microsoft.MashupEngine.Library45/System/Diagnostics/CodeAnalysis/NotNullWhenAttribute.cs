using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200205B RID: 8283
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x060113B5 RID: 70581 RVA: 0x003B540B File Offset: 0x003B360B
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17002E0A RID: 11786
		// (get) Token: 0x060113B6 RID: 70582 RVA: 0x003B541A File Offset: 0x003B361A
		public bool ReturnValue { get; }
	}
}
