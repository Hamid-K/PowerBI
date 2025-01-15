using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	internal sealed class NotNullAttribute : Attribute
	{
	}
}
