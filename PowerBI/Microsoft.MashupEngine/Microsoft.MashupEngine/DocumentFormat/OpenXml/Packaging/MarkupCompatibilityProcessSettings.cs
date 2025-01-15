using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200211F RID: 8479
	internal class MarkupCompatibilityProcessSettings
	{
		// Token: 0x170032BC RID: 12988
		// (get) Token: 0x0600D1D7 RID: 53719 RVA: 0x0029BC55 File Offset: 0x00299E55
		// (set) Token: 0x0600D1D8 RID: 53720 RVA: 0x0029BC5D File Offset: 0x00299E5D
		public MarkupCompatibilityProcessMode ProcessMode { get; internal set; }

		// Token: 0x170032BD RID: 12989
		// (get) Token: 0x0600D1D9 RID: 53721 RVA: 0x0029BC66 File Offset: 0x00299E66
		// (set) Token: 0x0600D1DA RID: 53722 RVA: 0x0029BC6E File Offset: 0x00299E6E
		public FileFormatVersions TargetFileFormatVersions { get; internal set; }

		// Token: 0x0600D1DB RID: 53723 RVA: 0x0029BC77 File Offset: 0x00299E77
		public MarkupCompatibilityProcessSettings(MarkupCompatibilityProcessMode processMode, FileFormatVersions targetFileFormatVersions)
		{
			this.ProcessMode = processMode;
			this.TargetFileFormatVersions = targetFileFormatVersions;
		}

		// Token: 0x0600D1DC RID: 53724 RVA: 0x0029BC8D File Offset: 0x00299E8D
		private MarkupCompatibilityProcessSettings()
		{
			this.ProcessMode = MarkupCompatibilityProcessMode.NoProcess;
			this.TargetFileFormatVersions = FileFormatVersions.Office2007;
		}
	}
}
