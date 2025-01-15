using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FBA RID: 12218
	[GeneratedCode("DomGen", "2.0")]
	internal class EmbedBoldItalicFont : FontRelationshipType
	{
		// Token: 0x170093B4 RID: 37812
		// (get) Token: 0x0601A7CD RID: 108493 RVA: 0x00362F39 File Offset: 0x00361139
		public override string LocalName
		{
			get
			{
				return "embedBoldItalic";
			}
		}

		// Token: 0x170093B5 RID: 37813
		// (get) Token: 0x0601A7CE RID: 108494 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093B6 RID: 37814
		// (get) Token: 0x0601A7CF RID: 108495 RVA: 0x00362F40 File Offset: 0x00361140
		internal override int ElementTypeId
		{
			get
			{
				return 11925;
			}
		}

		// Token: 0x0601A7D0 RID: 108496 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A7D2 RID: 108498 RVA: 0x00362F47 File Offset: 0x00361147
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbedBoldItalicFont>(deep);
		}

		// Token: 0x0400AD2E RID: 44334
		private const string tagName = "embedBoldItalic";

		// Token: 0x0400AD2F RID: 44335
		private const byte tagNsId = 23;

		// Token: 0x0400AD30 RID: 44336
		internal const int ElementTypeIdConst = 11925;
	}
}
