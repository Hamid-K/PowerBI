using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000552 RID: 1362
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class CannotApplyEqualityOperatorAttribute : Attribute
	{
	}
}
