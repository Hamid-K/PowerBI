using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000580 RID: 1408
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class NoReorderAttribute : Attribute
	{
	}
}
