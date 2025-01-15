using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200056C RID: 1388
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspMvcSuppressViewErrorAttribute : Attribute
	{
	}
}
