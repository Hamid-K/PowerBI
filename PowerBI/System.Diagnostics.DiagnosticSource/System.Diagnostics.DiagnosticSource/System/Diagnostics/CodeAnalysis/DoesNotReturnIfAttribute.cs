using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200006B RID: 107
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000B3A0 File Offset: 0x000095A0
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B3AF File Offset: 0x000095AF
		public bool ParameterValue { get; }
	}
}
