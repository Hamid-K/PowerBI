using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200236A RID: 9066
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticPaintBrush : OpenXmlLeafElement
	{
		// Token: 0x17004A98 RID: 19096
		// (get) Token: 0x060104E1 RID: 66785 RVA: 0x002E1F7F File Offset: 0x002E017F
		public override string LocalName
		{
			get
			{
				return "artisticPaintBrush";
			}
		}

		// Token: 0x17004A99 RID: 19097
		// (get) Token: 0x060104E2 RID: 66786 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A9A RID: 19098
		// (get) Token: 0x060104E3 RID: 66787 RVA: 0x002E1F86 File Offset: 0x002E0186
		internal override int ElementTypeId
		{
			get
			{
				return 12749;
			}
		}

		// Token: 0x060104E4 RID: 66788 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A9B RID: 19099
		// (get) Token: 0x060104E5 RID: 66789 RVA: 0x002E1F8D File Offset: 0x002E018D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPaintBrush.attributeTagNames;
			}
		}

		// Token: 0x17004A9C RID: 19100
		// (get) Token: 0x060104E6 RID: 66790 RVA: 0x002E1F94 File Offset: 0x002E0194
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPaintBrush.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A9D RID: 19101
		// (get) Token: 0x060104E7 RID: 66791 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104E8 RID: 66792 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A9E RID: 19102
		// (get) Token: 0x060104E9 RID: 66793 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104EA RID: 66794 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "brushSize")]
		public Int32Value BrushSize
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

		// Token: 0x060104EC RID: 66796 RVA: 0x002E1F9B File Offset: 0x002E019B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "brushSize" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060104ED RID: 66797 RVA: 0x002E1FD1 File Offset: 0x002E01D1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPaintBrush>(deep);
		}

		// Token: 0x060104EE RID: 66798 RVA: 0x002E1FDC File Offset: 0x002E01DC
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPaintBrush()
		{
			byte[] array = new byte[2];
			ArtisticPaintBrush.attributeNamespaceIds = array;
		}

		// Token: 0x04007405 RID: 29701
		private const string tagName = "artisticPaintBrush";

		// Token: 0x04007406 RID: 29702
		private const byte tagNsId = 48;

		// Token: 0x04007407 RID: 29703
		internal const int ElementTypeIdConst = 12749;

		// Token: 0x04007408 RID: 29704
		private static string[] attributeTagNames = new string[] { "trans", "brushSize" };

		// Token: 0x04007409 RID: 29705
		private static byte[] attributeNamespaceIds;
	}
}
