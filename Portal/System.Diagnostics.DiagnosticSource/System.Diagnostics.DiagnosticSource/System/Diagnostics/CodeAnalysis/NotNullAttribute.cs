using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000066 RID: 102
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class NotNullAttribute : Attribute
	{
	}
}
