using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200054A RID: 1354
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class ItemNotNullAttribute : Attribute
	{
	}
}
