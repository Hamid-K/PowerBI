using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200057D RID: 1405
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class LinqTunnelAttribute : Attribute
	{
	}
}
