using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200314E RID: 12622
	internal class ValidationSettings
	{
		// Token: 0x0601B5D9 RID: 112089 RVA: 0x00376D0E File Offset: 0x00374F0E
		internal ValidationSettings()
		{
			this.FileFormat = FileFormatVersions.Office2007;
			this.MaxNumberOfErrors = 1000;
		}

		// Token: 0x0601B5DA RID: 112090 RVA: 0x00376D28 File Offset: 0x00374F28
		internal ValidationSettings(FileFormatVersions fileFormat)
		{
			fileFormat.ThrowExceptionIfNot2007Or2010("fileFormat");
			this.FileFormat = fileFormat;
			this.MaxNumberOfErrors = 1000;
		}

		// Token: 0x170099B9 RID: 39353
		// (get) Token: 0x0601B5DB RID: 112091 RVA: 0x00376D4D File Offset: 0x00374F4D
		// (set) Token: 0x0601B5DC RID: 112092 RVA: 0x00376D55 File Offset: 0x00374F55
		internal FileFormatVersions FileFormat { get; set; }

		// Token: 0x170099BA RID: 39354
		// (get) Token: 0x0601B5DD RID: 112093 RVA: 0x00376D5E File Offset: 0x00374F5E
		// (set) Token: 0x0601B5DE RID: 112094 RVA: 0x00376D66 File Offset: 0x00374F66
		internal int MaxNumberOfErrors { get; set; }

		// Token: 0x0400B55A RID: 46426
		private const int _defaultMaxNumberOfErrorsReturned = 1000;
	}
}
