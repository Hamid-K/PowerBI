using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000128 RID: 296 RVA: 0x0000321B File Offset: 0x0000141B
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000322A File Offset: 0x0000142A
		public bool ParameterValue { get; }
	}
}
