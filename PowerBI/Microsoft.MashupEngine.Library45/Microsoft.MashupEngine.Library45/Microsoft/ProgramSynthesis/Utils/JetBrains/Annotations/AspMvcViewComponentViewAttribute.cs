using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000572 RID: 1394
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcViewComponentViewAttribute : Attribute
	{
	}
}
