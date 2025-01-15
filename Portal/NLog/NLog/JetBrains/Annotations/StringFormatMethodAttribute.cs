using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001C3 RID: 451
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
	internal sealed class StringFormatMethodAttribute : Attribute
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x00036659 File Offset: 0x00034859
		public StringFormatMethodAttribute([NotNull] string formatParameterName)
		{
			this.FormatParameterName = formatParameterName;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00036668 File Offset: 0x00034868
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00036670 File Offset: 0x00034870
		[NotNull]
		public string FormatParameterName { get; private set; }
	}
}
