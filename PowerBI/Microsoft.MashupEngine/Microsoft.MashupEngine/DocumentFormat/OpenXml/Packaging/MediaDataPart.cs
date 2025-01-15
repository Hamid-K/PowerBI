using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020020FE RID: 8446
	internal class MediaDataPart : DataPart
	{
		// Token: 0x0600D029 RID: 53289 RVA: 0x00295DC3 File Offset: 0x00293FC3
		internal MediaDataPart()
		{
		}

		// Token: 0x1700324B RID: 12875
		// (get) Token: 0x0600D02A RID: 53290 RVA: 0x00295DCB File Offset: 0x00293FCB
		internal override string TargetPath
		{
			get
			{
				return "media";
			}
		}

		// Token: 0x1700324C RID: 12876
		// (get) Token: 0x0600D02B RID: 53291 RVA: 0x00295DD2 File Offset: 0x00293FD2
		internal override string TargetName
		{
			get
			{
				return "mediadata";
			}
		}

		// Token: 0x1700324D RID: 12877
		// (get) Token: 0x0600D02C RID: 53292 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x040068CD RID: 26829
		private const string DefaultTargetPart = "media";

		// Token: 0x040068CE RID: 26830
		private const string DefaultTargetName = "mediadata";

		// Token: 0x040068CF RID: 26831
		private const string DefaultTargetExt = ".bin";
	}
}
