using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000031FC File Offset: 0x000013FC
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000320B File Offset: 0x0000140B
		public string ParameterName { get; }
	}
}
