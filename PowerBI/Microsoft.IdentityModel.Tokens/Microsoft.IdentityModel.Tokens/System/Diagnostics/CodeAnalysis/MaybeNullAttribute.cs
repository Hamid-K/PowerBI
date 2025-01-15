using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class MaybeNullAttribute : Attribute
	{
	}
}
