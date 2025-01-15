using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DisallowNullAttribute : Attribute
	{
	}
}
