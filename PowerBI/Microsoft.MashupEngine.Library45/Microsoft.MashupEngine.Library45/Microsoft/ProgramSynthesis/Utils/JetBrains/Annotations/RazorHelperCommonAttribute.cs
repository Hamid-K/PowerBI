using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200058D RID: 1421
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class RazorHelperCommonAttribute : Attribute
	{
	}
}
