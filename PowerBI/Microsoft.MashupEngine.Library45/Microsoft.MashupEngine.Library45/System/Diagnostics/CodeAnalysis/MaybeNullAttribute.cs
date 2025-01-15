using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02002058 RID: 8280
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class MaybeNullAttribute : Attribute
	{
	}
}
