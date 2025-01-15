using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200236F RID: 9071
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticPlasticWrap : OpenXmlLeafElement
	{
		// Token: 0x17004ABB RID: 19131
		// (get) Token: 0x06010527 RID: 66855 RVA: 0x002E21BB File Offset: 0x002E03BB
		public override string LocalName
		{
			get
			{
				return "artisticPlasticWrap";
			}
		}

		// Token: 0x17004ABC RID: 19132
		// (get) Token: 0x06010528 RID: 66856 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004ABD RID: 19133
		// (get) Token: 0x06010529 RID: 66857 RVA: 0x002E21C2 File Offset: 0x002E03C2
		internal override int ElementTypeId
		{
			get
			{
				return 12754;
			}
		}

		// Token: 0x0601052A RID: 66858 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004ABE RID: 19134
		// (get) Token: 0x0601052B RID: 66859 RVA: 0x002E21C9 File Offset: 0x002E03C9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPlasticWrap.attributeTagNames;
			}
		}

		// Token: 0x17004ABF RID: 19135
		// (get) Token: 0x0601052C RID: 66860 RVA: 0x002E21D0 File Offset: 0x002E03D0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPlasticWrap.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AC0 RID: 19136
		// (get) Token: 0x0601052D RID: 66861 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601052E RID: 66862 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004AC1 RID: 19137
		// (get) Token: 0x0601052F RID: 66863 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010530 RID: 66864 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06010532 RID: 66866 RVA: 0x002E1C93 File Offset: 0x002DFE93
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

		// Token: 0x06010533 RID: 66867 RVA: 0x002E21D7 File Offset: 0x002E03D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPlasticWrap>(deep);
		}

		// Token: 0x06010534 RID: 66868 RVA: 0x002E21E0 File Offset: 0x002E03E0
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPlasticWrap()
		{
			byte[] array = new byte[2];
			ArtisticPlasticWrap.attributeNamespaceIds = array;
		}

		// Token: 0x0400741E RID: 29726
		private const string tagName = "artisticPlasticWrap";

		// Token: 0x0400741F RID: 29727
		private const byte tagNsId = 48;

		// Token: 0x04007420 RID: 29728
		internal const int ElementTypeIdConst = 12754;

		// Token: 0x04007421 RID: 29729
		private static string[] attributeTagNames = new string[] { "trans", "smoothness" };

		// Token: 0x04007422 RID: 29730
		private static byte[] attributeNamespaceIds;
	}
}
