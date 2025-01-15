using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000068 RID: 104
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000B36A File Offset: 0x0000956A
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B379 File Offset: 0x00009579
		public bool ReturnValue { get; }
	}
}
