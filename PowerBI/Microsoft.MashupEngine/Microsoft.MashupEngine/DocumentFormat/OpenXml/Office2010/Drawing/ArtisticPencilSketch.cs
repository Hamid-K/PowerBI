using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200236D RID: 9069
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticPencilSketch : OpenXmlLeafElement
	{
		// Token: 0x17004AAD RID: 19117
		// (get) Token: 0x0601050B RID: 66827 RVA: 0x002E20CB File Offset: 0x002E02CB
		public override string LocalName
		{
			get
			{
				return "artisticPencilSketch";
			}
		}

		// Token: 0x17004AAE RID: 19118
		// (get) Token: 0x0601050C RID: 66828 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AAF RID: 19119
		// (get) Token: 0x0601050D RID: 66829 RVA: 0x002E20D2 File Offset: 0x002E02D2
		internal override int ElementTypeId
		{
			get
			{
				return 12752;
			}
		}

		// Token: 0x0601050E RID: 66830 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AB0 RID: 19120
		// (get) Token: 0x0601050F RID: 66831 RVA: 0x002E20D9 File Offset: 0x002E02D9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPencilSketch.attributeTagNames;
			}
		}

		// Token: 0x17004AB1 RID: 19121
		// (get) Token: 0x06010510 RID: 66832 RVA: 0x002E20E0 File Offset: 0x002E02E0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPencilSketch.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AB2 RID: 19122
		// (get) Token: 0x06010511 RID: 66833 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010512 RID: 66834 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004AB3 RID: 19123
		// (get) Token: 0x06010513 RID: 66835 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010514 RID: 66836 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06010516 RID: 66838 RVA: 0x002E1953 File Offset: 0x002DFB53
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

		// Token: 0x06010517 RID: 66839 RVA: 0x002E20E7 File Offset: 0x002E02E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPencilSketch>(deep);
		}

		// Token: 0x06010518 RID: 66840 RVA: 0x002E20F0 File Offset: 0x002E02F0
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPencilSketch()
		{
			byte[] array = new byte[2];
			ArtisticPencilSketch.attributeNamespaceIds = array;
		}

		// Token: 0x04007414 RID: 29716
		private const string tagName = "artisticPencilSketch";

		// Token: 0x04007415 RID: 29717
		private const byte tagNsId = 48;

		// Token: 0x04007416 RID: 29718
		internal const int ElementTypeIdConst = 12752;

		// Token: 0x04007417 RID: 29719
		private static string[] attributeTagNames = new string[] { "trans", "pressure" };

		// Token: 0x04007418 RID: 29720
		private static byte[] attributeNamespaceIds;
	}
}
