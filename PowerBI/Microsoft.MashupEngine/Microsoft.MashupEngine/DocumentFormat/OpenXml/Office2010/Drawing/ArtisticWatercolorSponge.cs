using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002371 RID: 9073
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticWatercolorSponge : OpenXmlLeafElement
	{
		// Token: 0x17004AC9 RID: 19145
		// (get) Token: 0x06010543 RID: 66883 RVA: 0x002E2273 File Offset: 0x002E0473
		public override string LocalName
		{
			get
			{
				return "artisticWatercolorSponge";
			}
		}

		// Token: 0x17004ACA RID: 19146
		// (get) Token: 0x06010544 RID: 66884 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004ACB RID: 19147
		// (get) Token: 0x06010545 RID: 66885 RVA: 0x002E227A File Offset: 0x002E047A
		internal override int ElementTypeId
		{
			get
			{
				return 12756;
			}
		}

		// Token: 0x06010546 RID: 66886 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004ACC RID: 19148
		// (get) Token: 0x06010547 RID: 66887 RVA: 0x002E2281 File Offset: 0x002E0481
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticWatercolorSponge.attributeTagNames;
			}
		}

		// Token: 0x17004ACD RID: 19149
		// (get) Token: 0x06010548 RID: 66888 RVA: 0x002E2288 File Offset: 0x002E0488
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticWatercolorSponge.attributeNamespaceIds;
			}
		}

		// Token: 0x17004ACE RID: 19150
		// (get) Token: 0x06010549 RID: 66889 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601054A RID: 66890 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004ACF RID: 19151
		// (get) Token: 0x0601054B RID: 66891 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601054C RID: 66892 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601054E RID: 66894 RVA: 0x002E1F9B File Offset: 0x002E019B
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

		// Token: 0x0601054F RID: 66895 RVA: 0x002E228F File Offset: 0x002E048F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticWatercolorSponge>(deep);
		}

		// Token: 0x06010550 RID: 66896 RVA: 0x002E2298 File Offset: 0x002E0498
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticWatercolorSponge()
		{
			byte[] array = new byte[2];
			ArtisticWatercolorSponge.attributeNamespaceIds = array;
		}

		// Token: 0x04007428 RID: 29736
		private const string tagName = "artisticWatercolorSponge";

		// Token: 0x04007429 RID: 29737
		private const byte tagNsId = 48;

		// Token: 0x0400742A RID: 29738
		internal const int ElementTypeIdConst = 12756;

		// Token: 0x0400742B RID: 29739
		private static string[] attributeTagNames = new string[] { "trans", "brushSize" };

		// Token: 0x0400742C RID: 29740
		private static byte[] attributeNamespaceIds;
	}
}
