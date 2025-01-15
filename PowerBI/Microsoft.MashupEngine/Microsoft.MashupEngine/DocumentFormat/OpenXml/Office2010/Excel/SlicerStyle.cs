using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200242F RID: 9263
	[ChildElementInfo(typeof(SlicerStyleElements), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlicerStyle : OpenXmlCompositeElement
	{
		// Token: 0x17004FD1 RID: 20433
		// (get) Token: 0x06011083 RID: 69763 RVA: 0x002E9DE7 File Offset: 0x002E7FE7
		public override string LocalName
		{
			get
			{
				return "slicerStyle";
			}
		}

		// Token: 0x17004FD2 RID: 20434
		// (get) Token: 0x06011084 RID: 69764 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FD3 RID: 20435
		// (get) Token: 0x06011085 RID: 69765 RVA: 0x002E9DEE File Offset: 0x002E7FEE
		internal override int ElementTypeId
		{
			get
			{
				return 12987;
			}
		}

		// Token: 0x06011086 RID: 69766 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FD4 RID: 20436
		// (get) Token: 0x06011087 RID: 69767 RVA: 0x002E9DF5 File Offset: 0x002E7FF5
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerStyle.attributeTagNames;
			}
		}

		// Token: 0x17004FD5 RID: 20437
		// (get) Token: 0x06011088 RID: 69768 RVA: 0x002E9DFC File Offset: 0x002E7FFC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FD6 RID: 20438
		// (get) Token: 0x06011089 RID: 69769 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601108A RID: 69770 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601108B RID: 69771 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerStyle()
		{
		}

		// Token: 0x0601108C RID: 69772 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601108D RID: 69773 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601108E RID: 69774 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601108F RID: 69775 RVA: 0x002E9E03 File Offset: 0x002E8003
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "slicerStyleElements" == name)
			{
				return new SlicerStyleElements();
			}
			return null;
		}

		// Token: 0x17004FD7 RID: 20439
		// (get) Token: 0x06011090 RID: 69776 RVA: 0x002E9E1E File Offset: 0x002E801E
		internal override string[] ElementTagNames
		{
			get
			{
				return SlicerStyle.eleTagNames;
			}
		}

		// Token: 0x17004FD8 RID: 20440
		// (get) Token: 0x06011091 RID: 69777 RVA: 0x002E9E25 File Offset: 0x002E8025
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlicerStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17004FD9 RID: 20441
		// (get) Token: 0x06011092 RID: 69778 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004FDA RID: 20442
		// (get) Token: 0x06011093 RID: 69779 RVA: 0x002E9E2C File Offset: 0x002E802C
		// (set) Token: 0x06011094 RID: 69780 RVA: 0x002E9E35 File Offset: 0x002E8035
		public SlicerStyleElements SlicerStyleElements
		{
			get
			{
				return base.GetElement<SlicerStyleElements>(0);
			}
			set
			{
				base.SetElement<SlicerStyleElements>(0, value);
			}
		}

		// Token: 0x06011095 RID: 69781 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011096 RID: 69782 RVA: 0x002E9E3F File Offset: 0x002E803F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerStyle>(deep);
		}

		// Token: 0x06011097 RID: 69783 RVA: 0x002E9E48 File Offset: 0x002E8048
		// Note: this type is marked as 'beforefieldinit'.
		static SlicerStyle()
		{
			byte[] array = new byte[1];
			SlicerStyle.attributeNamespaceIds = array;
			SlicerStyle.eleTagNames = new string[] { "slicerStyleElements" };
			SlicerStyle.eleNamespaceIds = new byte[] { 53 };
		}

		// Token: 0x0400775C RID: 30556
		private const string tagName = "slicerStyle";

		// Token: 0x0400775D RID: 30557
		private const byte tagNsId = 53;

		// Token: 0x0400775E RID: 30558
		internal const int ElementTypeIdConst = 12987;

		// Token: 0x0400775F RID: 30559
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04007760 RID: 30560
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007761 RID: 30561
		private static readonly string[] eleTagNames;

		// Token: 0x04007762 RID: 30562
		private static readonly byte[] eleNamespaceIds;
	}
}
