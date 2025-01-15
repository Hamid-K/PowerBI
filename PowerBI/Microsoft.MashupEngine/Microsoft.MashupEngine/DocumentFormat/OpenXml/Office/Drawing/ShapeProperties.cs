using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002329 RID: 9001
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700489D RID: 18589
		// (get) Token: 0x06010093 RID: 65683 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x1700489E RID: 18590
		// (get) Token: 0x06010094 RID: 65684 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x1700489F RID: 18591
		// (get) Token: 0x06010095 RID: 65685 RVA: 0x002DECCD File Offset: 0x002DCECD
		internal override int ElementTypeId
		{
			get
			{
				return 13024;
			}
		}

		// Token: 0x06010096 RID: 65686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170048A0 RID: 18592
		// (get) Token: 0x06010097 RID: 65687 RVA: 0x002DECD4 File Offset: 0x002DCED4
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170048A1 RID: 18593
		// (get) Token: 0x06010098 RID: 65688 RVA: 0x002DECDB File Offset: 0x002DCEDB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170048A2 RID: 18594
		// (get) Token: 0x06010099 RID: 65689 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x0601009A RID: 65690 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601009B RID: 65691 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x0601009C RID: 65692 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601009D RID: 65693 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601009E RID: 65694 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601009F RID: 65695 RVA: 0x002DECE4 File Offset: 0x002DCEE4
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

		// Token: 0x170048A3 RID: 18595
		// (get) Token: 0x060100A0 RID: 65696 RVA: 0x002DEE5A File Offset: 0x002DD05A
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170048A4 RID: 18596
		// (get) Token: 0x060100A1 RID: 65697 RVA: 0x002DEE61 File Offset: 0x002DD061
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170048A5 RID: 18597
		// (get) Token: 0x060100A2 RID: 65698 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048A6 RID: 18598
		// (get) Token: 0x060100A3 RID: 65699 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x060100A4 RID: 65700 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x060100A5 RID: 65701 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060100A6 RID: 65702 RVA: 0x002DEE68 File Offset: 0x002DD068
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x060100A7 RID: 65703 RVA: 0x002DEE74 File Offset: 0x002DD074
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

		// Token: 0x040072CD RID: 29389
		private const string tagName = "spPr";

		// Token: 0x040072CE RID: 29390
		private const byte tagNsId = 56;

		// Token: 0x040072CF RID: 29391
		internal const int ElementTypeIdConst = 13024;

		// Token: 0x040072D0 RID: 29392
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x040072D1 RID: 29393
		private static byte[] attributeNamespaceIds;

		// Token: 0x040072D2 RID: 29394
		private static readonly string[] eleTagNames;

		// Token: 0x040072D3 RID: 29395
		private static readonly byte[] eleNamespaceIds;
	}
}
