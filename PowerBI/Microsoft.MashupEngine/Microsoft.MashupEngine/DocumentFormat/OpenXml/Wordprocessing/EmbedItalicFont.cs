using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB9 RID: 12217
	[GeneratedCode("DomGen", "2.0")]
	internal class EmbedItalicFont : FontRelationshipType
	{
		// Token: 0x170093B1 RID: 37809
		// (get) Token: 0x0601A7C7 RID: 108487 RVA: 0x00362F22 File Offset: 0x00361122
		public override string LocalName
		{
			get
			{
				return "embedItalic";
			}
		}

		// Token: 0x170093B2 RID: 37810
		// (get) Token: 0x0601A7C8 RID: 108488 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093B3 RID: 37811
		// (get) Token: 0x0601A7C9 RID: 108489 RVA: 0x00362F29 File Offset: 0x00361129
		internal override int ElementTypeId
		{
			get
			{
				return 11924;
			}
		}

		// Token: 0x0601A7CA RID: 108490 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A7CC RID: 108492 RVA: 0x00362F30 File Offset: 0x00361130
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbedItalicFont>(deep);
		}

		// Token: 0x0400AD2B RID: 44331
		private const string tagName = "embedItalic";

		// Token: 0x0400AD2C RID: 44332
		private const byte tagNsId = 23;

		// Token: 0x0400AD2D RID: 44333
		internal const int ElementTypeIdConst = 11924;
	}
}
