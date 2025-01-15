using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000067 RID: 103
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000B353 File Offset: 0x00009553
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B362 File Offset: 0x00009562
		public bool ReturnValue { get; }
	}
}
