using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002369 RID: 9065
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticPaintStrokes : OpenXmlLeafElement
	{
		// Token: 0x17004A91 RID: 19089
		// (get) Token: 0x060104D3 RID: 66771 RVA: 0x002E1F23 File Offset: 0x002E0123
		public override string LocalName
		{
			get
			{
				return "artisticPaintStrokes";
			}
		}

		// Token: 0x17004A92 RID: 19090
		// (get) Token: 0x060104D4 RID: 66772 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A93 RID: 19091
		// (get) Token: 0x060104D5 RID: 66773 RVA: 0x002E1F2A File Offset: 0x002E012A
		internal override int ElementTypeId
		{
			get
			{
				return 12748;
			}
		}

		// Token: 0x060104D6 RID: 66774 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A94 RID: 19092
		// (get) Token: 0x060104D7 RID: 66775 RVA: 0x002E1F31 File Offset: 0x002E0131
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPaintStrokes.attributeTagNames;
			}
		}

		// Token: 0x17004A95 RID: 19093
		// (get) Token: 0x060104D8 RID: 66776 RVA: 0x002E1F38 File Offset: 0x002E0138
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPaintStrokes.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A96 RID: 19094
		// (get) Token: 0x060104D9 RID: 66777 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104DA RID: 66778 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A97 RID: 19095
		// (get) Token: 0x060104DB RID: 66779 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104DC RID: 66780 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "intensity")]
		public Int32Value Intensity
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

		// Token: 0x060104DE RID: 66782 RVA: 0x002E1BFF File Offset: 0x002DFDFF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "intensity" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060104DF RID: 66783 RVA: 0x002E1F3F File Offset: 0x002E013F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPaintStrokes>(deep);
		}

		// Token: 0x060104E0 RID: 66784 RVA: 0x002E1F48 File Offset: 0x002E0148
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPaintStrokes()
		{
			byte[] array = new byte[2];
			ArtisticPaintStrokes.attributeNamespaceIds = array;
		}

		// Token: 0x04007400 RID: 29696
		private const string tagName = "artisticPaintStrokes";

		// Token: 0x04007401 RID: 29697
		private const byte tagNsId = 48;

		// Token: 0x04007402 RID: 29698
		internal const int ElementTypeIdConst = 12748;

		// Token: 0x04007403 RID: 29699
		private static string[] attributeTagNames = new string[] { "trans", "intensity" };

		// Token: 0x04007404 RID: 29700
		private static byte[] attributeNamespaceIds;
	}
}
