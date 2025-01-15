using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200205A RID: 8282
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x060113B3 RID: 70579 RVA: 0x003B53F4 File Offset: 0x003B35F4
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17002E09 RID: 11785
		// (get) Token: 0x060113B4 RID: 70580 RVA: 0x003B5403 File Offset: 0x003B3603
		public bool ReturnValue { get; }
	}
}
