using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200054C RID: 1356
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x00059832 File Offset: 0x00057A32
		public StringFormatMethodAttribute(string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00059841 File Offset: 0x00057A41
		public string FormatParameterName { get; }
	}
}
