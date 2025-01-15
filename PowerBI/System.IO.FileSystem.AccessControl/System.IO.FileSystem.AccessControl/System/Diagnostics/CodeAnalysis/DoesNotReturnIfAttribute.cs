using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002469 File Offset: 0x00000669
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002478 File Offset: 0x00000678
		public bool ParameterValue { get; }
	}
}
