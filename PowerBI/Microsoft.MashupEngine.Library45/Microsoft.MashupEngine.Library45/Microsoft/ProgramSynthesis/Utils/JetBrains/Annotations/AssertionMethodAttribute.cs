using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000579 RID: 1401
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AssertionMethodAttribute : Attribute
	{
	}
}
