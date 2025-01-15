using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02002057 RID: 8279
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	internal sealed class DisallowNullAttribute : Attribute
	{
	}
}
