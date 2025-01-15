using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002331 RID: 9009
	[ChildElementInfo(typeof(OfficeArtExtensionList))]
	[ChildElementInfo(typeof(TextBody))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(ShapeNonVisualProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x170048E3 RID: 18659
		// (get) Token: 0x0601012E RID: 65838 RVA: 0x002DF64E File Offset: 0x002DD84E
		public override string LocalName
		{
			get
			{
				return "sp";
			}
		}

		// Token: 0x170048E4 RID: 18660
		// (get) Token: 0x0601012F RID: 65839 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048E5 RID: 18661
		// (get) Token: 0x06010130 RID: 65840 RVA: 0x002DF655 File Offset: 0x002DD855
		internal override int ElementTypeId
		{
			get
			{
				return 13032;
			}
		}

		// Token: 0x06010131 RID: 65841 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170048E6 RID: 18662
		// (get) Token: 0x06010132 RID: 65842 RVA: 0x002DF65C File Offset: 0x002DD85C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x170048E7 RID: 18663
		// (get) Token: 0x06010133 RID: 65843 RVA: 0x002DF663 File Offset: 0x002DD863
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x170048E8 RID: 18664
		// (get) Token: 0x06010134 RID: 65844 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010135 RID: 65845 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "modelId")]
		public StringValue ModelId
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

		// Token: 0x06010136 RID: 65846 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x06010137 RID: 65847 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010138 RID: 65848 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010139 RID: 65849 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601013A RID: 65850 RVA: 0x002DF66C File Offset: 0x002DD86C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (56 == namespaceId && "nvSpPr" == name)
			{
				return new ShapeNonVisualProperties();
			}
			if (56 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (56 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (56 == namespaceId && "txBody" == name)
			{
				return new TextBody();
			}
			if (56 == namespaceId && "txXfrm" == name)
			{
				return new Transform2D();
			}
			if (56 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x170048E9 RID: 18665
		// (get) Token: 0x0601013B RID: 65851 RVA: 0x002DF70A File Offset: 0x002DD90A
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape.eleTagNames;
			}
		}

		// Token: 0x170048EA RID: 18666
		// (get) Token: 0x0601013C RID: 65852 RVA: 0x002DF711 File Offset: 0x002DD911
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape.eleNamespaceIds;
			}
		}

		// Token: 0x170048EB RID: 18667
		// (get) Token: 0x0601013D RID: 65853 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048EC RID: 18668
		// (get) Token: 0x0601013E RID: 65854 RVA: 0x002DF718 File Offset: 0x002DD918
		// (set) Token: 0x0601013F RID: 65855 RVA: 0x002DF721 File Offset: 0x002DD921
		public ShapeNonVisualProperties ShapeNonVisualProperties
		{
			get
			{
				return base.GetElement<ShapeNonVisualProperties>(0);
			}
			set
			{
				base.SetElement<ShapeNonVisualProperties>(0, value);
			}
		}

		// Token: 0x170048ED RID: 18669
		// (get) Token: 0x06010140 RID: 65856 RVA: 0x002DF72B File Offset: 0x002DD92B
		// (set) Token: 0x06010141 RID: 65857 RVA: 0x002DF734 File Offset: 0x002DD934
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x170048EE RID: 18670
		// (get) Token: 0x06010142 RID: 65858 RVA: 0x002DF73E File Offset: 0x002DD93E
		// (set) Token: 0x06010143 RID: 65859 RVA: 0x002DF747 File Offset: 0x002DD947
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(2);
			}
			set
			{
				base.SetElement<ShapeStyle>(2, value);
			}
		}

		// Token: 0x170048EF RID: 18671
		// (get) Token: 0x06010144 RID: 65860 RVA: 0x002DF751 File Offset: 0x002DD951
		// (set) Token: 0x06010145 RID: 65861 RVA: 0x002DF75A File Offset: 0x002DD95A
		public TextBody TextBody
		{
			get
			{
				return base.GetElement<TextBody>(3);
			}
			set
			{
				base.SetElement<TextBody>(3, value);
			}
		}

		// Token: 0x170048F0 RID: 18672
		// (get) Token: 0x06010146 RID: 65862 RVA: 0x002DF764 File Offset: 0x002DD964
		// (set) Token: 0x06010147 RID: 65863 RVA: 0x002DF76D File Offset: 0x002DD96D
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(4);
			}
			set
			{
				base.SetElement<Transform2D>(4, value);
			}
		}

		// Token: 0x170048F1 RID: 18673
		// (get) Token: 0x06010148 RID: 65864 RVA: 0x002DF777 File Offset: 0x002DD977
		// (set) Token: 0x06010149 RID: 65865 RVA: 0x002DF780 File Offset: 0x002DD980
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(5);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(5, value);
			}
		}

		// Token: 0x0601014A RID: 65866 RVA: 0x002DF78A File Offset: 0x002DD98A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "modelId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601014B RID: 65867 RVA: 0x002DF7AA File Offset: 0x002DD9AA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x0601014C RID: 65868 RVA: 0x002DF7B4 File Offset: 0x002DD9B4
		// Note: this type is marked as 'beforefieldinit'.
		static Shape()
		{
			byte[] array = new byte[1];
			Shape.attributeNamespaceIds = array;
			Shape.eleTagNames = new string[] { "nvSpPr", "spPr", "style", "txBody", "txXfrm", "extLst" };
			Shape.eleNamespaceIds = new byte[] { 56, 56, 56, 56, 56, 56 };
		}

		// Token: 0x040072F9 RID: 29433
		private const string tagName = "sp";

		// Token: 0x040072FA RID: 29434
		private const byte tagNsId = 56;

		// Token: 0x040072FB RID: 29435
		internal const int ElementTypeIdConst = 13032;

		// Token: 0x040072FC RID: 29436
		private static string[] attributeTagNames = new string[] { "modelId" };

		// Token: 0x040072FD RID: 29437
		private static byte[] attributeNamespaceIds;

		// Token: 0x040072FE RID: 29438
		private static readonly string[] eleTagNames;

		// Token: 0x040072FF RID: 29439
		private static readonly byte[] eleNamespaceIds;
	}
}
