using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A61 RID: 10849
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700725D RID: 29277
		// (get) Token: 0x06015E7E RID: 89726 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x1700725E RID: 29278
		// (get) Token: 0x06015E7F RID: 89727 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700725F RID: 29279
		// (get) Token: 0x06015E80 RID: 89728 RVA: 0x00324504 File Offset: 0x00322704
		internal override int ElementTypeId
		{
			get
			{
				return 12267;
			}
		}

		// Token: 0x06015E81 RID: 89729 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007260 RID: 29280
		// (get) Token: 0x06015E82 RID: 89730 RVA: 0x0032450B File Offset: 0x0032270B
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x17007261 RID: 29281
		// (get) Token: 0x06015E83 RID: 89731 RVA: 0x00324512 File Offset: 0x00322712
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007262 RID: 29282
		// (get) Token: 0x06015E84 RID: 89732 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06015E85 RID: 89733 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015E86 RID: 89734 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x06015E87 RID: 89735 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E88 RID: 89736 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E89 RID: 89737 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E8A RID: 89738 RVA: 0x0032451C File Offset: 0x0032271C
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

		// Token: 0x17007263 RID: 29283
		// (get) Token: 0x06015E8B RID: 89739 RVA: 0x00324692 File Offset: 0x00322892
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17007264 RID: 29284
		// (get) Token: 0x06015E8C RID: 89740 RVA: 0x00324699 File Offset: 0x00322899
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007265 RID: 29285
		// (get) Token: 0x06015E8D RID: 89741 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007266 RID: 29286
		// (get) Token: 0x06015E8E RID: 89742 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06015E8F RID: 89743 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x06015E90 RID: 89744 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015E91 RID: 89745 RVA: 0x003246A0 File Offset: 0x003228A0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06015E92 RID: 89746 RVA: 0x003246AC File Offset: 0x003228AC
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

		// Token: 0x0400955B RID: 38235
		private const string tagName = "spPr";

		// Token: 0x0400955C RID: 38236
		private const byte tagNsId = 24;

		// Token: 0x0400955D RID: 38237
		internal const int ElementTypeIdConst = 12267;

		// Token: 0x0400955E RID: 38238
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x0400955F RID: 38239
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009560 RID: 38240
		private static readonly string[] eleTagNames;

		// Token: 0x04009561 RID: 38241
		private static readonly byte[] eleNamespaceIds;
	}
}
