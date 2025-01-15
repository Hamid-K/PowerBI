using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002504 RID: 9476
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005419 RID: 21529
		// (get) Token: 0x06011A22 RID: 72226 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x1700541A RID: 21530
		// (get) Token: 0x06011A23 RID: 72227 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700541B RID: 21531
		// (get) Token: 0x06011A24 RID: 72228 RVA: 0x002F0DEB File Offset: 0x002EEFEB
		internal override int ElementTypeId
		{
			get
			{
				return 10343;
			}
		}

		// Token: 0x06011A25 RID: 72229 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700541C RID: 21532
		// (get) Token: 0x06011A26 RID: 72230 RVA: 0x002F0DF2 File Offset: 0x002EEFF2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x1700541D RID: 21533
		// (get) Token: 0x06011A27 RID: 72231 RVA: 0x002F0DF9 File Offset: 0x002EEFF9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700541E RID: 21534
		// (get) Token: 0x06011A28 RID: 72232 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06011A29 RID: 72233 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011A2A RID: 72234 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartShapeProperties()
		{
		}

		// Token: 0x06011A2B RID: 72235 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A2C RID: 72236 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A2D RID: 72237 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011A2E RID: 72238 RVA: 0x002F0E00 File Offset: 0x002EF000
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
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700541F RID: 21535
		// (get) Token: 0x06011A2F RID: 72239 RVA: 0x002F0F5E File Offset: 0x002EF15E
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005420 RID: 21536
		// (get) Token: 0x06011A30 RID: 72240 RVA: 0x002F0F65 File Offset: 0x002EF165
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005421 RID: 21537
		// (get) Token: 0x06011A31 RID: 72241 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005422 RID: 21538
		// (get) Token: 0x06011A32 RID: 72242 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06011A33 RID: 72243 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x06011A34 RID: 72244 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011A35 RID: 72245 RVA: 0x002F0F6C File Offset: 0x002EF16C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartShapeProperties>(deep);
		}

		// Token: 0x06011A36 RID: 72246 RVA: 0x002F0F78 File Offset: 0x002EF178
		// Note: this type is marked as 'beforefieldinit'.
		static ChartShapeProperties()
		{
			byte[] array = new byte[1];
			ChartShapeProperties.attributeNamespaceIds = array;
			ChartShapeProperties.eleTagNames = new string[]
			{
				"xfrm", "custGeom", "prstGeom", "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "ln", "effectLst",
				"effectDag", "scene3d", "sp3d", "extLst"
			};
			ChartShapeProperties.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10, 10
			};
		}

		// Token: 0x04007B9C RID: 31644
		private const string tagName = "spPr";

		// Token: 0x04007B9D RID: 31645
		private const byte tagNsId = 11;

		// Token: 0x04007B9E RID: 31646
		internal const int ElementTypeIdConst = 10343;

		// Token: 0x04007B9F RID: 31647
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04007BA0 RID: 31648
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007BA1 RID: 31649
		private static readonly string[] eleTagNames;

		// Token: 0x04007BA2 RID: 31650
		private static readonly byte[] eleNamespaceIds;
	}
}
