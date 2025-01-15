using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000B381 File Offset: 0x00009581
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B390 File Offset: 0x00009590
		public string ParameterName { get; }
	}
}
