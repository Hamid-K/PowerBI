using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000020EF File Offset: 0x000002EF
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020FE File Offset: 0x000002FE
		public bool ParameterValue { get; }
	}
}
