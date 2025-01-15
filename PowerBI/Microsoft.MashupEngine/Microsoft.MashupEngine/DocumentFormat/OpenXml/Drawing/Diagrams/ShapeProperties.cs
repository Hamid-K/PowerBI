using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002693 RID: 9875
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(GroupFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005D49 RID: 23881
		// (get) Token: 0x06012EAC RID: 77484 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x17005D4A RID: 23882
		// (get) Token: 0x06012EAD RID: 77485 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D4B RID: 23883
		// (get) Token: 0x06012EAE RID: 77486 RVA: 0x00300CF0 File Offset: 0x002FEEF0
		internal override int ElementTypeId
		{
			get
			{
				return 10690;
			}
		}

		// Token: 0x06012EAF RID: 77487 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D4C RID: 23884
		// (get) Token: 0x06012EB0 RID: 77488 RVA: 0x00300CF7 File Offset: 0x002FEEF7
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x17005D4D RID: 23885
		// (get) Token: 0x06012EB1 RID: 77489 RVA: 0x00300CFE File Offset: 0x002FEEFE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D4E RID: 23886
		// (get) Token: 0x06012EB2 RID: 77490 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06012EB3 RID: 77491 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012EB4 RID: 77492 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x06012EB5 RID: 77493 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EB6 RID: 77494 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EB7 RID: 77495 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012EB8 RID: 77496 RVA: 0x00300D08 File Offset: 0x002FEF08
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

		// Token: 0x17005D4F RID: 23887
		// (get) Token: 0x06012EB9 RID: 77497 RVA: 0x00300E7E File Offset: 0x002FF07E
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005D50 RID: 23888
		// (get) Token: 0x06012EBA RID: 77498 RVA: 0x00300E85 File Offset: 0x002FF085
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005D51 RID: 23889
		// (get) Token: 0x06012EBB RID: 77499 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D52 RID: 23890
		// (get) Token: 0x06012EBC RID: 77500 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06012EBD RID: 77501 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x06012EBE RID: 77502 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012EBF RID: 77503 RVA: 0x00300E8C File Offset: 0x002FF08C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06012EC0 RID: 77504 RVA: 0x00300E98 File Offset: 0x002FF098
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

		// Token: 0x04008227 RID: 33319
		private const string tagName = "spPr";

		// Token: 0x04008228 RID: 33320
		private const byte tagNsId = 14;

		// Token: 0x04008229 RID: 33321
		internal const int ElementTypeIdConst = 10690;

		// Token: 0x0400822A RID: 33322
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x0400822B RID: 33323
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400822C RID: 33324
		private static readonly string[] eleTagNames;

		// Token: 0x0400822D RID: 33325
		private static readonly byte[] eleNamespaceIds;
	}
}
