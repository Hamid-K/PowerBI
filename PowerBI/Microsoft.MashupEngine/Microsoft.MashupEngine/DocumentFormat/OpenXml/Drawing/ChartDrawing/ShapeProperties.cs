using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002631 RID: 9777
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A5A RID: 23130
		// (get) Token: 0x060127F5 RID: 75765 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x17005A5B RID: 23131
		// (get) Token: 0x060127F6 RID: 75766 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A5C RID: 23132
		// (get) Token: 0x060127F7 RID: 75767 RVA: 0x002FBDF1 File Offset: 0x002F9FF1
		internal override int ElementTypeId
		{
			get
			{
				return 10596;
			}
		}

		// Token: 0x060127F8 RID: 75768 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A5D RID: 23133
		// (get) Token: 0x060127F9 RID: 75769 RVA: 0x002FBDF8 File Offset: 0x002F9FF8
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x17005A5E RID: 23134
		// (get) Token: 0x060127FA RID: 75770 RVA: 0x002FBDFF File Offset: 0x002F9FFF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A5F RID: 23135
		// (get) Token: 0x060127FB RID: 75771 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x060127FC RID: 75772 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060127FD RID: 75773 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x060127FE RID: 75774 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127FF RID: 75775 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012800 RID: 75776 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012801 RID: 75777 RVA: 0x002FBE08 File Offset: 0x002FA008
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (10 == namespaceId && "custGeom" == name)
			{
				return new CustomGeometry();
			}
			if (10 == namespaceId && "prstGeom" == name)
			{
				return new PresetGeometry();
			}
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			if (10 == namespaceId && "ln" == name)
			{
				return new Outline();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			if (10 == namespaceId && "scene3d" == name)
			{
				return new Scene3DType();
			}
			if (10 == namespaceId && "sp3d" == name)
			{
				return new Shape3DType();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ShapePropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x17005A60 RID: 23136
		// (get) Token: 0x06012802 RID: 75778 RVA: 0x002FBF7E File Offset: 0x002FA17E
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005A61 RID: 23137
		// (get) Token: 0x06012803 RID: 75779 RVA: 0x002FBF85 File Offset: 0x002FA185
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A62 RID: 23138
		// (get) Token: 0x06012804 RID: 75780 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A63 RID: 23139
		// (get) Token: 0x06012805 RID: 75781 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06012806 RID: 75782 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(0);
			}
			set
			{
				base.SetElement<Transform2D>(0, value);
			}
		}

		// Token: 0x06012807 RID: 75783 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012808 RID: 75784 RVA: 0x002FBF8C File Offset: 0x002FA18C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06012809 RID: 75785 RVA: 0x002FBF98 File Offset: 0x002FA198
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeProperties()
		{
			byte[] array = new byte[1];
			ShapeProperties.attributeNamespaceIds = array;
			ShapeProperties.eleTagNames = new string[]
			{
				"xfrm", "custGeom", "prstGeom", "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill", "ln",
				"effectLst", "effectDag", "scene3d", "sp3d", "extLst"
			};
			ShapeProperties.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10, 10, 10
			};
		}

		// Token: 0x04008063 RID: 32867
		private const string tagName = "spPr";

		// Token: 0x04008064 RID: 32868
		private const byte tagNsId = 12;

		// Token: 0x04008065 RID: 32869
		internal const int ElementTypeIdConst = 10596;

		// Token: 0x04008066 RID: 32870
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04008067 RID: 32871
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008068 RID: 32872
		private static readonly string[] eleTagNames;

		// Token: 0x04008069 RID: 32873
		private static readonly byte[] eleNamespaceIds;
	}
}
