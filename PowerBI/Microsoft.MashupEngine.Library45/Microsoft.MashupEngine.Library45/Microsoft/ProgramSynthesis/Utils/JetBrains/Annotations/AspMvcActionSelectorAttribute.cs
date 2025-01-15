using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000573 RID: 1395
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcActionSelectorAttribute : Attribute
	{
	}
}
