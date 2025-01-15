using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x0200231B RID: 8987
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004846 RID: 18502
		// (get) Token: 0x0600FFDA RID: 65498 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x17004847 RID: 18503
		// (get) Token: 0x0600FFDB RID: 65499 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004848 RID: 18504
		// (get) Token: 0x0600FFDC RID: 65500 RVA: 0x002DE3F6 File Offset: 0x002DC5F6
		internal override int ElementTypeId
		{
			get
			{
				return 12695;
			}
		}

		// Token: 0x0600FFDD RID: 65501 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004849 RID: 18505
		// (get) Token: 0x0600FFDE RID: 65502 RVA: 0x002DE3FD File Offset: 0x002DC5FD
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x1700484A RID: 18506
		// (get) Token: 0x0600FFDF RID: 65503 RVA: 0x002DE404 File Offset: 0x002DC604
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700484B RID: 18507
		// (get) Token: 0x0600FFE0 RID: 65504 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x0600FFE1 RID: 65505 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0600FFE2 RID: 65506 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x0600FFE3 RID: 65507 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FFE4 RID: 65508 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FFE5 RID: 65509 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FFE6 RID: 65510 RVA: 0x002DE41C File Offset: 0x002DC61C
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

		// Token: 0x1700484C RID: 18508
		// (get) Token: 0x0600FFE7 RID: 65511 RVA: 0x002DE592 File Offset: 0x002DC792
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x1700484D RID: 18509
		// (get) Token: 0x0600FFE8 RID: 65512 RVA: 0x002DE599 File Offset: 0x002DC799
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700484E RID: 18510
		// (get) Token: 0x0600FFE9 RID: 65513 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700484F RID: 18511
		// (get) Token: 0x0600FFEA RID: 65514 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x0600FFEB RID: 65515 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x0600FFEC RID: 65516 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FFED RID: 65517 RVA: 0x002DE5D3 File Offset: 0x002DC7D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x0600FFEE RID: 65518 RVA: 0x002DE5DC File Offset: 0x002DC7DC
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

		// Token: 0x04007290 RID: 29328
		private const string tagName = "spPr";

		// Token: 0x04007291 RID: 29329
		private const byte tagNsId = 46;

		// Token: 0x04007292 RID: 29330
		internal const int ElementTypeIdConst = 12695;

		// Token: 0x04007293 RID: 29331
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04007294 RID: 29332
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007295 RID: 29333
		private static readonly string[] eleTagNames;

		// Token: 0x04007296 RID: 29334
		private static readonly byte[] eleNamespaceIds;
	}
}
