using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002366 RID: 9062
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticLineDrawing : OpenXmlLeafElement
	{
		// Token: 0x17004A7C RID: 19068
		// (get) Token: 0x060104A9 RID: 66729 RVA: 0x002E1D9F File Offset: 0x002DFF9F
		public override string LocalName
		{
			get
			{
				return "artisticLineDrawing";
			}
		}

		// Token: 0x17004A7D RID: 19069
		// (get) Token: 0x060104AA RID: 66730 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A7E RID: 19070
		// (get) Token: 0x060104AB RID: 66731 RVA: 0x002E1DA6 File Offset: 0x002DFFA6
		internal override int ElementTypeId
		{
			get
			{
				return 12745;
			}
		}

		// Token: 0x060104AC RID: 66732 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A7F RID: 19071
		// (get) Token: 0x060104AD RID: 66733 RVA: 0x002E1DAD File Offset: 0x002DFFAD
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticLineDrawing.attributeTagNames;
			}
		}

		// Token: 0x17004A80 RID: 19072
		// (get) Token: 0x060104AE RID: 66734 RVA: 0x002E1DB4 File Offset: 0x002DFFB4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticLineDrawing.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A81 RID: 19073
		// (get) Token: 0x060104AF RID: 66735 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104B0 RID: 66736 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A82 RID: 19074
		// (get) Token: 0x060104B1 RID: 66737 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104B2 RID: 66738 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pencilSize")]
		public Int32Value PencilSize
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

		// Token: 0x060104B4 RID: 66740 RVA: 0x002E1DBB File Offset: 0x002DFFBB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "pencilSize" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060104B5 RID: 66741 RVA: 0x002E1DF1 File Offset: 0x002DFFF1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticLineDrawing>(deep);
		}

		// Token: 0x060104B6 RID: 66742 RVA: 0x002E1DFC File Offset: 0x002DFFFC
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticLineDrawing()
		{
			byte[] array = new byte[2];
			ArtisticLineDrawing.attributeNamespaceIds = array;
		}

		// Token: 0x040073F1 RID: 29681
		private const string tagName = "artisticLineDrawing";

		// Token: 0x040073F2 RID: 29682
		private const byte tagNsId = 48;

		// Token: 0x040073F3 RID: 29683
		internal const int ElementTypeIdConst = 12745;

		// Token: 0x040073F4 RID: 29684
		private static string[] attributeTagNames = new string[] { "trans", "pencilSize" };

		// Token: 0x040073F5 RID: 29685
		private static byte[] attributeNamespaceIds;
	}
}
