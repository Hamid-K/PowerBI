using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002881 RID: 10369
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700673A RID: 26426
		// (get) Token: 0x06014540 RID: 83264 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x1700673B RID: 26427
		// (get) Token: 0x06014541 RID: 83265 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700673C RID: 26428
		// (get) Token: 0x06014542 RID: 83266 RVA: 0x00312131 File Offset: 0x00310331
		internal override int ElementTypeId
		{
			get
			{
				return 10731;
			}
		}

		// Token: 0x06014543 RID: 83267 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700673D RID: 26429
		// (get) Token: 0x06014544 RID: 83268 RVA: 0x00312138 File Offset: 0x00310338
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x1700673E RID: 26430
		// (get) Token: 0x06014545 RID: 83269 RVA: 0x0031213F File Offset: 0x0031033F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700673F RID: 26431
		// (get) Token: 0x06014546 RID: 83270 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06014547 RID: 83271 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06014548 RID: 83272 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x06014549 RID: 83273 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601454A RID: 83274 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601454B RID: 83275 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601454C RID: 83276 RVA: 0x00312148 File Offset: 0x00310348
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

		// Token: 0x17006740 RID: 26432
		// (get) Token: 0x0601454D RID: 83277 RVA: 0x003122BE File Offset: 0x003104BE
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17006741 RID: 26433
		// (get) Token: 0x0601454E RID: 83278 RVA: 0x003122C5 File Offset: 0x003104C5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006742 RID: 26434
		// (get) Token: 0x0601454F RID: 83279 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006743 RID: 26435
		// (get) Token: 0x06014550 RID: 83280 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06014551 RID: 83281 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x06014552 RID: 83282 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014553 RID: 83283 RVA: 0x003122CC File Offset: 0x003104CC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06014554 RID: 83284 RVA: 0x003122D8 File Offset: 0x003104D8
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

		// Token: 0x04008D98 RID: 36248
		private const string tagName = "spPr";

		// Token: 0x04008D99 RID: 36249
		private const byte tagNsId = 18;

		// Token: 0x04008D9A RID: 36250
		internal const int ElementTypeIdConst = 10731;

		// Token: 0x04008D9B RID: 36251
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04008D9C RID: 36252
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D9D RID: 36253
		private static readonly string[] eleTagNames;

		// Token: 0x04008D9E RID: 36254
		private static readonly byte[] eleNamespaceIds;
	}
}
