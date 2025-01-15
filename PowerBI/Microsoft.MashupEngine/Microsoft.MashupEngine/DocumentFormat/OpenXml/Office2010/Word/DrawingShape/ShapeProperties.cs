using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024FE RID: 9470
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170053CC RID: 21452
		// (get) Token: 0x06011982 RID: 72066 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x170053CD RID: 21453
		// (get) Token: 0x06011983 RID: 72067 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053CE RID: 21454
		// (get) Token: 0x06011984 RID: 72068 RVA: 0x002F038C File Offset: 0x002EE58C
		internal override int ElementTypeId
		{
			get
			{
				return 13136;
			}
		}

		// Token: 0x06011985 RID: 72069 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170053CF RID: 21455
		// (get) Token: 0x06011986 RID: 72070 RVA: 0x002F0393 File Offset: 0x002EE593
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170053D0 RID: 21456
		// (get) Token: 0x06011987 RID: 72071 RVA: 0x002F039A File Offset: 0x002EE59A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170053D1 RID: 21457
		// (get) Token: 0x06011988 RID: 72072 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06011989 RID: 72073 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601198A RID: 72074 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x0601198B RID: 72075 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601198C RID: 72076 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601198D RID: 72077 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601198E RID: 72078 RVA: 0x002F03A4 File Offset: 0x002EE5A4
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

		// Token: 0x170053D2 RID: 21458
		// (get) Token: 0x0601198F RID: 72079 RVA: 0x002F051A File Offset: 0x002EE71A
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170053D3 RID: 21459
		// (get) Token: 0x06011990 RID: 72080 RVA: 0x002F0521 File Offset: 0x002EE721
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170053D4 RID: 21460
		// (get) Token: 0x06011991 RID: 72081 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053D5 RID: 21461
		// (get) Token: 0x06011992 RID: 72082 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06011993 RID: 72083 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x06011994 RID: 72084 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011995 RID: 72085 RVA: 0x002F0528 File Offset: 0x002EE728
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06011996 RID: 72086 RVA: 0x002F0534 File Offset: 0x002EE734
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

		// Token: 0x04007B76 RID: 31606
		private const string tagName = "spPr";

		// Token: 0x04007B77 RID: 31607
		private const byte tagNsId = 61;

		// Token: 0x04007B78 RID: 31608
		internal const int ElementTypeIdConst = 13136;

		// Token: 0x04007B79 RID: 31609
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04007B7A RID: 31610
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B7B RID: 31611
		private static readonly string[] eleTagNames;

		// Token: 0x04007B7C RID: 31612
		private static readonly byte[] eleNamespaceIds;
	}
}
