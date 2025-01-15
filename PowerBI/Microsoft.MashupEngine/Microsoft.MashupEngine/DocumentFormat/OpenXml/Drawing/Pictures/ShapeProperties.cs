using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
	// Token: 0x02002875 RID: 10357
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(CustomGeometry))]
	[ChildElementInfo(typeof(PresetGeometry))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ShapePropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170066BA RID: 26298
		// (get) Token: 0x0601442A RID: 82986 RVA: 0x002DE3EF File Offset: 0x002DC5EF
		public override string LocalName
		{
			get
			{
				return "spPr";
			}
		}

		// Token: 0x170066BB RID: 26299
		// (get) Token: 0x0601442B RID: 82987 RVA: 0x000E78AE File Offset: 0x000E5AAE
		internal override byte NamespaceId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x170066BC RID: 26300
		// (get) Token: 0x0601442C RID: 82988 RVA: 0x00311016 File Offset: 0x0030F216
		internal override int ElementTypeId
		{
			get
			{
				return 10719;
			}
		}

		// Token: 0x0601442D RID: 82989 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170066BD RID: 26301
		// (get) Token: 0x0601442E RID: 82990 RVA: 0x0031101D File Offset: 0x0030F21D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170066BE RID: 26302
		// (get) Token: 0x0601442F RID: 82991 RVA: 0x00311024 File Offset: 0x0030F224
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170066BF RID: 26303
		// (get) Token: 0x06014430 RID: 82992 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x06014431 RID: 82993 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06014432 RID: 82994 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeProperties()
		{
		}

		// Token: 0x06014433 RID: 82995 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014434 RID: 82996 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014435 RID: 82997 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014436 RID: 82998 RVA: 0x0031102C File Offset: 0x0030F22C
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

		// Token: 0x170066C0 RID: 26304
		// (get) Token: 0x06014437 RID: 82999 RVA: 0x003111A2 File Offset: 0x0030F3A2
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170066C1 RID: 26305
		// (get) Token: 0x06014438 RID: 83000 RVA: 0x003111A9 File Offset: 0x0030F3A9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170066C2 RID: 26306
		// (get) Token: 0x06014439 RID: 83001 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066C3 RID: 26307
		// (get) Token: 0x0601443A RID: 83002 RVA: 0x002DE5A0 File Offset: 0x002DC7A0
		// (set) Token: 0x0601443B RID: 83003 RVA: 0x002DE5A9 File Offset: 0x002DC7A9
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

		// Token: 0x0601443C RID: 83004 RVA: 0x002DE5B3 File Offset: 0x002DC7B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601443D RID: 83005 RVA: 0x003111B0 File Offset: 0x0030F3B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeProperties>(deep);
		}

		// Token: 0x0601443E RID: 83006 RVA: 0x003111BC File Offset: 0x0030F3BC
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

		// Token: 0x04008D50 RID: 36176
		private const string tagName = "spPr";

		// Token: 0x04008D51 RID: 36177
		private const byte tagNsId = 17;

		// Token: 0x04008D52 RID: 36178
		internal const int ElementTypeIdConst = 10719;

		// Token: 0x04008D53 RID: 36179
		private static string[] attributeTagNames = new string[] { "bwMode" };

		// Token: 0x04008D54 RID: 36180
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D55 RID: 36181
		private static readonly string[] eleTagNames;

		// Token: 0x04008D56 RID: 36182
		private static readonly byte[] eleNamespaceIds;
	}
}
