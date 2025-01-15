using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002424 File Offset: 0x00000624
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002433 File Offset: 0x00000633
		public string ParameterName { get; }
	}
}
