using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B0 RID: 176
	[Guid("F12A463D-AC10-4a56-8FB7-2F4498EA24F3")]
	public sealed class RestoreFolder
	{
		// Token: 0x0600085A RID: 2138 RVA: 0x00027F07 File Offset: 0x00026107
		public RestoreFolder()
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00027F0F File Offset: 0x0002610F
		public RestoreFolder(string originalFolder, string newFolder)
		{
			this.Original = originalFolder;
			this.New = newFolder;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00027F25 File Offset: 0x00026125
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x00027F2D File Offset: 0x0002612D
		[XmlElement(IsNullable = false)]
		public string Original
		{
			get
			{
				return this.original;
			}
			set
			{
				Utils.CheckValidPath(value);
				this.original = Utils.Trim(value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00027F41 File Offset: 0x00026141
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00027F49 File Offset: 0x00026149
		[XmlElement(IsNullable = false)]
		public string New
		{
			get
			{
				return this.newFolder;
			}
			set
			{
				Utils.CheckValidPath(value);
				this.newFolder = Utils.Trim(value);
			}
		}

		// Token: 0x040004D0 RID: 1232
		private string original;

		// Token: 0x040004D1 RID: 1233
		private string newFolder;
	}
}
