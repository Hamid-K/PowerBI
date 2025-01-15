using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A7 RID: 10151
	[ChildElementInfo(typeof(GroupFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062B2 RID: 25266
		// (get) Token: 0x06013AB8 RID: 80568 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x170062B3 RID: 25267
		// (get) Token: 0x06013AB9 RID: 80569 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062B4 RID: 25268
		// (get) Token: 0x06013ABA RID: 80570 RVA: 0x0030A7A1 File Offset: 0x003089A1
		internal override int ElementTypeId
		{
			get
			{
				return 10184;
			}
		}

		// Token: 0x06013ABB RID: 80571 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170062B5 RID: 25269
		// (get) Token: 0x06013ABC RID: 80572 RVA: 0x0030A7A8 File Offset: 0x003089A8
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170062B6 RID: 25270
		// (get) Token: 0x06013ABD RID: 80573 RVA: 0x0030A7AF File Offset: 0x003089AF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170062B7 RID: 25271
		// (get) Token: 0x06013ABE RID: 80574 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06013ABF RID: 80575 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06013AC0 RID: 80576 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x06013AC1 RID: 80577 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AC2 RID: 80578 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AC3 RID: 80579 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013AC4 RID: 80580 RVA: 0x0030A7B8 File Offset: 0x003089B8
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

		// Token: 0x170062B8 RID: 25272
		// (get) Token: 0x06013AC5 RID: 80581 RVA: 0x0030A92E File Offset: 0x00308B2E
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170062B9 RID: 25273
		// (get) Token: 0x06013AC6 RID: 80582 RVA: 0x0030A935 File Offset: 0x00308B35
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062BA RID: 25274
		// (get) Token: 0x06013AC7 RID: 80583 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062BB RID: 25275
		// (get) Token: 0x06013AC8 RID: 80584 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x06013AC9 RID: 80585 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x06013ACA RID: 80586 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013ACB RID: 80587 RVA: 0x0030A93C File Offset: 0x00308B3C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x06013ACC RID: 80588 RVA: 0x0030A948 File Offset: 0x00308B48
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

		// Token: 0x04008739 RID: 34617
		private const string tagName = "spPr";

		// Token: 0x0400873A RID: 34618
		private const byte tagNsId = 10;

		// Token: 0x0400873B RID: 34619
		internal const int ElementTypeIdConst = 10184;

		// Token: 0x0400873C RID: 34620
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x0400873D RID: 34621
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400873E RID: 34622
		private static readonly string[] eleTagNames;

		// Token: 0x0400873F RID: 34623
		private static readonly byte[] eleNamespaceIds;
	}
}
