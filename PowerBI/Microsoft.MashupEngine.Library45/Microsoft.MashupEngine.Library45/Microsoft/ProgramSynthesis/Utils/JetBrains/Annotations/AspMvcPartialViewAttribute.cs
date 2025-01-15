using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200056B RID: 1387
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcPartialViewAttribute : Attribute
	{
	}
}
