using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000244A File Offset: 0x0000064A
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002459 File Offset: 0x00000659
		public string ParameterName { get; }
	}
}
