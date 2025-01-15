using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000064 RID: 100
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DisallowNullAttribute : Attribute
	{
	}
}
