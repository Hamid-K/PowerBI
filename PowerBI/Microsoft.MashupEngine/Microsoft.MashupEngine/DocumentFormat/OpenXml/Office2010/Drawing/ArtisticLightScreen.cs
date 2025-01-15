using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002365 RID: 9061
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticLightScreen : OpenXmlLeafElement
	{
		// Token: 0x17004A75 RID: 19061
		// (get) Token: 0x0601049B RID: 66715 RVA: 0x002E1D0B File Offset: 0x002DFF0B
		public override string LocalName
		{
			get
			{
				return "artisticLightScreen";
			}
		}

		// Token: 0x17004A76 RID: 19062
		// (get) Token: 0x0601049C RID: 66716 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A77 RID: 19063
		// (get) Token: 0x0601049D RID: 66717 RVA: 0x002E1D12 File Offset: 0x002DFF12
		internal override int ElementTypeId
		{
			get
			{
				return 12744;
			}
		}

		// Token: 0x0601049E RID: 66718 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A78 RID: 19064
		// (get) Token: 0x0601049F RID: 66719 RVA: 0x002E1D19 File Offset: 0x002DFF19
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticLightScreen.attributeTagNames;
			}
		}

		// Token: 0x17004A79 RID: 19065
		// (get) Token: 0x060104A0 RID: 66720 RVA: 0x002E1D20 File Offset: 0x002DFF20
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticLightScreen.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A7A RID: 19066
		// (get) Token: 0x060104A1 RID: 66721 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104A2 RID: 66722 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A7B RID: 19067
		// (get) Token: 0x060104A3 RID: 66723 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104A4 RID: 66724 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "gridSize")]
		public Int32Value GridSize
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

		// Token: 0x060104A6 RID: 66726 RVA: 0x002E1D27 File Offset: 0x002DFF27
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "gridSize" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060104A7 RID: 66727 RVA: 0x002E1D5D File Offset: 0x002DFF5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticLightScreen>(deep);
		}

		// Token: 0x060104A8 RID: 66728 RVA: 0x002E1D68 File Offset: 0x002DFF68
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticLightScreen()
		{
			byte[] array = new byte[2];
			ArtisticLightScreen.attributeNamespaceIds = array;
		}

		// Token: 0x040073EC RID: 29676
		private const string tagName = "artisticLightScreen";

		// Token: 0x040073ED RID: 29677
		private const byte tagNsId = 48;

		// Token: 0x040073EE RID: 29678
		internal const int ElementTypeIdConst = 12744;

		// Token: 0x040073EF RID: 29679
		private static string[] attributeTagNames = new string[] { "trans", "gridSize" };

		// Token: 0x040073F0 RID: 29680
		private static byte[] attributeNamespaceIds;
	}
}
