using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000584 RID: 1412
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspDataFieldAttribute : Attribute
	{
	}
}
