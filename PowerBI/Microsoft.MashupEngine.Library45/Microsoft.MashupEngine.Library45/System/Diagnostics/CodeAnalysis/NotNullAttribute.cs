using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02002059 RID: 8281
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class NotNullAttribute : Attribute
	{
	}
}
