using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002555 File Offset: 0x00000755
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002564 File Offset: 0x00000764
		public bool ParameterValue { get; }
	}
}
