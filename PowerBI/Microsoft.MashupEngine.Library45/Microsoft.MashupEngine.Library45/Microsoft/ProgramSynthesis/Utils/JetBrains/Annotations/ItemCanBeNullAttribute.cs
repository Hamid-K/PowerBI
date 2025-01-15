using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200054B RID: 1355
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class ItemCanBeNullAttribute : Attribute
	{
	}
}
