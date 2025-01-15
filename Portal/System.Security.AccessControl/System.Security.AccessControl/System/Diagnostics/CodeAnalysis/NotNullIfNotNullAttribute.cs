using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002536 File Offset: 0x00000736
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002545 File Offset: 0x00000745
		public string ParameterName { get; }
	}
}
