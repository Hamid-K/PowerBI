using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x0200223A RID: 8762
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomBorder : BorderType
	{
		// Token: 0x1700396C RID: 14700
		// (get) Token: 0x0600E088 RID: 57480 RVA: 0x002BFE70 File Offset: 0x002BE070
		public override string LocalName
		{
			get
			{
				return "borderbottom";
			}
		}

		// Token: 0x1700396D RID: 14701
		// (get) Token: 0x0600E089 RID: 57481 RVA: 0x002BFE26 File Offset: 0x002BE026
		internal override byte NamespaceId
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x1700396E RID: 14702
		// (get) Token: 0x0600E08A RID: 57482 RVA: 0x002BFE77 File Offset: 0x002BE077
		internal override int ElementTypeId
		{
			get
			{
				return 12433;
			}
		}

		// Token: 0x0600E08B RID: 57483 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E08D RID: 57485 RVA: 0x002BFE7E File Offset: 0x002BE07E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomBorder>(deep);
		}

		// Token: 0x04006E56 RID: 28246
		private const string tagName = "borderbottom";

		// Token: 0x04006E57 RID: 28247
		private const byte tagNsId = 28;

		// Token: 0x04006E58 RID: 28248
		internal const int ElementTypeIdConst = 12433;
	}
}
