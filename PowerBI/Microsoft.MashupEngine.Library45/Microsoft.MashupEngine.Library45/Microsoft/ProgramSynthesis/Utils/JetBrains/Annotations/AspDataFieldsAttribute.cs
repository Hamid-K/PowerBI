using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000585 RID: 1413
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class AspDataFieldsAttribute : Attribute
	{
	}
}
