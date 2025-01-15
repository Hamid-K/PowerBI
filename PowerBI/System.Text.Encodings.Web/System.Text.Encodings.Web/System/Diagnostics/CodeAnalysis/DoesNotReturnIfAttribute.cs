using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000026C1 File Offset: 0x000008C1
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026D0 File Offset: 0x000008D0
		public bool ParameterValue { get; }
	}
}
