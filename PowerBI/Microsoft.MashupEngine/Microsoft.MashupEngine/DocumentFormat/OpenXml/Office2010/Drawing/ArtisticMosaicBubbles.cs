using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002368 RID: 9064
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticMosaicBubbles : OpenXmlLeafElement
	{
		// Token: 0x17004A8A RID: 19082
		// (get) Token: 0x060104C5 RID: 66757 RVA: 0x002E1EC7 File Offset: 0x002E00C7
		public override string LocalName
		{
			get
			{
				return "artisticMosiaicBubbles";
			}
		}

		// Token: 0x17004A8B RID: 19083
		// (get) Token: 0x060104C6 RID: 66758 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A8C RID: 19084
		// (get) Token: 0x060104C7 RID: 66759 RVA: 0x002E1ECE File Offset: 0x002E00CE
		internal override int ElementTypeId
		{
			get
			{
				return 12747;
			}
		}

		// Token: 0x060104C8 RID: 66760 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A8D RID: 19085
		// (get) Token: 0x060104C9 RID: 66761 RVA: 0x002E1ED5 File Offset: 0x002E00D5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticMosaicBubbles.attributeTagNames;
			}
		}

		// Token: 0x17004A8E RID: 19086
		// (get) Token: 0x060104CA RID: 66762 RVA: 0x002E1EDC File Offset: 0x002E00DC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticMosaicBubbles.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A8F RID: 19087
		// (get) Token: 0x060104CB RID: 66763 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104CC RID: 66764 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "trans")]
		public Int32Value Transparancy
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004A90 RID: 19088
		// (get) Token: 0x060104CD RID: 66765 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104CE RID: 66766 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pressure")]
		public Int32Value Pressure
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060104D0 RID: 66768 RVA: 0x002E1953 File Offset: 0x002DFB53
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "pressure" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060104D1 RID: 66769 RVA: 0x002E1EE3 File Offset: 0x002E00E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticMosaicBubbles>(deep);
		}

		// Token: 0x060104D2 RID: 66770 RVA: 0x002E1EEC File Offset: 0x002E00EC
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticMosaicBubbles()
		{
			byte[] array = new byte[2];
			ArtisticMosaicBubbles.attributeNamespaceIds = array;
		}

		// Token: 0x040073FB RID: 29691
		private const string tagName = "artisticMosiaicBubbles";

		// Token: 0x040073FC RID: 29692
		private const byte tagNsId = 48;

		// Token: 0x040073FD RID: 29693
		internal const int ElementTypeIdConst = 12747;

		// Token: 0x040073FE RID: 29694
		private static string[] attributeTagNames = new string[] { "trans", "pressure" };

		// Token: 0x040073FF RID: 29695
		private static byte[] attributeNamespaceIds;
	}
}
