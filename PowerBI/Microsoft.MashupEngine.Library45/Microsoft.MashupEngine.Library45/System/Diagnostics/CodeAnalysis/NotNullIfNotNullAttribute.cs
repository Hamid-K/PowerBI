using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200205C RID: 8284
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x060113B7 RID: 70583 RVA: 0x003B5422 File Offset: 0x003B3622
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17002E0B RID: 11787
		// (get) Token: 0x060113B8 RID: 70584 RVA: 0x003B5431 File Offset: 0x003B3631
		public string ParameterName { get; }
	}
}
