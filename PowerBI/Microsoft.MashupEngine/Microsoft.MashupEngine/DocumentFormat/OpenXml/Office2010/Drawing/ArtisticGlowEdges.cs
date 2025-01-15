using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002364 RID: 9060
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticGlowEdges : OpenXmlLeafElement
	{
		// Token: 0x17004A6E RID: 19054
		// (get) Token: 0x0601048D RID: 66701 RVA: 0x002E1C77 File Offset: 0x002DFE77
		public override string LocalName
		{
			get
			{
				return "artisticGlowEdges";
			}
		}

		// Token: 0x17004A6F RID: 19055
		// (get) Token: 0x0601048E RID: 66702 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A70 RID: 19056
		// (get) Token: 0x0601048F RID: 66703 RVA: 0x002E1C7E File Offset: 0x002DFE7E
		internal override int ElementTypeId
		{
			get
			{
				return 12743;
			}
		}

		// Token: 0x06010490 RID: 66704 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A71 RID: 19057
		// (get) Token: 0x06010491 RID: 66705 RVA: 0x002E1C85 File Offset: 0x002DFE85
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticGlowEdges.attributeTagNames;
			}
		}

		// Token: 0x17004A72 RID: 19058
		// (get) Token: 0x06010492 RID: 66706 RVA: 0x002E1C8C File Offset: 0x002DFE8C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticGlowEdges.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A73 RID: 19059
		// (get) Token: 0x06010493 RID: 66707 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010494 RID: 66708 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A74 RID: 19060
		// (get) Token: 0x06010495 RID: 66709 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010496 RID: 66710 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "smoothness")]
		public Int32Value Smoothness
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

		// Token: 0x06010498 RID: 66712 RVA: 0x002E1C93 File Offset: 0x002DFE93
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "smoothness" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010499 RID: 66713 RVA: 0x002E1CC9 File Offset: 0x002DFEC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticGlowEdges>(deep);
		}

		// Token: 0x0601049A RID: 66714 RVA: 0x002E1CD4 File Offset: 0x002DFED4
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticGlowEdges()
		{
			byte[] array = new byte[2];
			ArtisticGlowEdges.attributeNamespaceIds = array;
		}

		// Token: 0x040073E7 RID: 29671
		private const string tagName = "artisticGlowEdges";

		// Token: 0x040073E8 RID: 29672
		private const byte tagNsId = 48;

		// Token: 0x040073E9 RID: 29673
		internal const int ElementTypeIdConst = 12743;

		// Token: 0x040073EA RID: 29674
		private static string[] attributeTagNames = new string[] { "trans", "smoothness" };

		// Token: 0x040073EB RID: 29675
		private static byte[] attributeNamespaceIds;
	}
}
