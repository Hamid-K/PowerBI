using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200205E RID: 8286
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x060113BA RID: 70586 RVA: 0x003B5439 File Offset: 0x003B3639
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17002E0C RID: 11788
		// (get) Token: 0x060113BB RID: 70587 RVA: 0x003B5448 File Offset: 0x003B3648
		public bool ParameterValue { get; }
	}
}
