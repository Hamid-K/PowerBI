using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D7 RID: 9687
	[ChildElementInfo(typeof(Shape3DType))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005838 RID: 22584
		// (get) Token: 0x0601234D RID: 74573 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x17005839 RID: 22585
		// (get) Token: 0x0601234E RID: 74574 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700583A RID: 22586
		// (get) Token: 0x0601234F RID: 74575 RVA: 0x002F721B File Offset: 0x002F541B
		internal override int ElementTypeId
		{
			get
			{
				return 10529;
			}
		}

		// Token: 0x06012350 RID: 74576 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700583B RID: 22587
		// (get) Token: 0x06012351 RID: 74577 RVA: 0x002F7222 File Offset: 0x002F5422
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x1700583C RID: 22588
		// (get) Token: 0x06012352 RID: 74578 RVA: 0x002F7229 File Offset: 0x002F5429
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700583D RID: 22589
		// (get) Token: 0x06012353 RID: 74579 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06012354 RID: 74580 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012355 RID: 74581 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x06012356 RID: 74582 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012357 RID: 74583 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012358 RID: 74584 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012359 RID: 74585 RVA: 0x002F7230 File Offset: 0x002F5430
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

		// Token: 0x1700583E RID: 22590
		// (get) Token: 0x0601235A RID: 74586 RVA: 0x002F73A6 File Offset: 0x002F55A6
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x1700583F RID: 22591
		// (get) Token: 0x0601235B RID: 74587 RVA: 0x002F73AD File Offset: 0x002F55AD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005840 RID: 22592
		// (get) Token: 0x0601235C RID: 74588 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005841 RID: 22593
		// (get) Token: 0x0601235D RID: 74589 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x0601235E RID: 74590 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x0601235F RID: 74591 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012360 RID: 74592 RVA: 0x002F73B4 File Offset: 0x002F55B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06012361 RID: 74593 RVA: 0x002F73C0 File Offset: 0x002F55C0
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

		// Token: 0x04007EA0 RID: 32416
		private const string tagName = "spPr";

		// Token: 0x04007EA1 RID: 32417
		private const byte tagNsId = 11;

		// Token: 0x04007EA2 RID: 32418
		internal const int ElementTypeIdConst = 10529;

		// Token: 0x04007EA3 RID: 32419
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04007EA4 RID: 32420
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007EA5 RID: 32421
		private static readonly string[] eleTagNames;

		// Token: 0x04007EA6 RID: 32422
		private static readonly byte[] eleNamespaceIds;
	}
}
