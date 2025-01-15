using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000581 RID: 1409
	[AttributeUsage(AttributeTargets.Class)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class XamlItemsControlAttribute : Attribute
	{
	}
}
