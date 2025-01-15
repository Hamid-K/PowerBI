using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB8 RID: 12216
	[GeneratedCode("DomGen", "2.0")]
	internal class EmbedBoldFont : FontRelationshipType
	{
		// Token: 0x170093AE RID: 37806
		// (get) Token: 0x0601A7C1 RID: 108481 RVA: 0x00362F0B File Offset: 0x0036110B
		public override string LocalName
		{
			get
			{
				return "embedBold";
			}
		}

		// Token: 0x170093AF RID: 37807
		// (get) Token: 0x0601A7C2 RID: 108482 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093B0 RID: 37808
		// (get) Token: 0x0601A7C3 RID: 108483 RVA: 0x00362F12 File Offset: 0x00361112
		internal override int ElementTypeId
		{
			get
			{
				return 11923;
			}
		}

		// Token: 0x0601A7C4 RID: 108484 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A7C6 RID: 108486 RVA: 0x00362F19 File Offset: 0x00361119
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbedBoldFont>(deep);
		}

		// Token: 0x0400AD28 RID: 44328
		private const string tagName = "embedBold";

		// Token: 0x0400AD29 RID: 44329
		private const byte tagNsId = 23;

		// Token: 0x0400AD2A RID: 44330
		internal const int ElementTypeIdConst = 11923;
	}
}
