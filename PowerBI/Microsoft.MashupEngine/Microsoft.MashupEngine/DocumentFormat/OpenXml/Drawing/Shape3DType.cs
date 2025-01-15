using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F9 RID: 9977
	[ChildElementInfo(typeof(BevelTop))]
	[ChildElementInfo(typeof(BevelBottom))]
	[ChildElementInfo(typeof(ExtrusionColor))]
	[ChildElementInfo(typeof(ContourColor))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Shape3DType : OpenXmlCompositeElement
	{
		// Token: 0x17005E2E RID: 24110
		// (get) Token: 0x060130A4 RID: 77988 RVA: 0x003002F0 File Offset: 0x002FE4F0
		public override string LocalName
		{
			get
			{
				return "sp3d";
			}
		}

		// Token: 0x17005E2F RID: 24111
		// (get) Token: 0x060130A5 RID: 77989 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E30 RID: 24112
		// (get) Token: 0x060130A6 RID: 77990 RVA: 0x00302DCF File Offset: 0x00300FCF
		internal override int ElementTypeId
		{
			get
			{
				return 10041;
			}
		}

		// Token: 0x060130A7 RID: 77991 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E31 RID: 24113
		// (get) Token: 0x060130A8 RID: 77992 RVA: 0x00302DD6 File Offset: 0x00300FD6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape3DType.attributeTagNames;
			}
		}

		// Token: 0x17005E32 RID: 24114
		// (get) Token: 0x060130A9 RID: 77993 RVA: 0x00302DDD File Offset: 0x00300FDD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape3DType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E33 RID: 24115
		// (get) Token: 0x060130AA RID: 77994 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060130AB RID: 77995 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "z")]
		public Int64Value Z
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E34 RID: 24116
		// (get) Token: 0x060130AC RID: 77996 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060130AD RID: 77997 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "extrusionH")]
		public Int64Value ExtrusionHeight
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005E35 RID: 24117
		// (get) Token: 0x060130AE RID: 77998 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x060130AF RID: 77999 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "contourW")]
		public Int64Value ContourWidth
		{
			get
			{
				return (Int64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005E36 RID: 24118
		// (get) Token: 0x060130B0 RID: 78000 RVA: 0x002E0CE1 File Offset: 0x002DEEE1
		// (set) Token: 0x060130B1 RID: 78001 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "prstMaterial")]
		public EnumValue<PresetMaterialTypeValues> PresetMaterial
		{
			get
			{
				return (EnumValue<PresetMaterialTypeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060130B2 RID: 78002 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape3DType()
		{
		}

		// Token: 0x060130B3 RID: 78003 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape3DType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060130B4 RID: 78004 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape3DType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060130B5 RID: 78005 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape3DType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060130B6 RID: 78006 RVA: 0x00302DE4 File Offset: 0x00300FE4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bevelT" == name)
			{
				return new BevelTop();
			}
			if (10 == namespaceId && "bevelB" == name)
			{
				return new BevelBottom();
			}
			if (10 == namespaceId && "extrusionClr" == name)
			{
				return new ExtrusionColor();
			}
			if (10 == namespaceId && "contourClr" == name)
			{
				return new ContourColor();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005E37 RID: 24119
		// (get) Token: 0x060130B7 RID: 78007 RVA: 0x00302E6A File Offset: 0x0030106A
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape3DType.eleTagNames;
			}
		}

		// Token: 0x17005E38 RID: 24120
		// (get) Token: 0x060130B8 RID: 78008 RVA: 0x00302E71 File Offset: 0x00301071
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape3DType.eleNamespaceIds;
			}
		}

		// Token: 0x17005E39 RID: 24121
		// (get) Token: 0x060130B9 RID: 78009 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005E3A RID: 24122
		// (get) Token: 0x060130BA RID: 78010 RVA: 0x002E0D84 File Offset: 0x002DEF84
		// (set) Token: 0x060130BB RID: 78011 RVA: 0x002E0D8D File Offset: 0x002DEF8D
		public BevelTop BevelTop
		{
			get
			{
				return base.GetElement<BevelTop>(0);
			}
			set
			{
				base.SetElement<BevelTop>(0, value);
			}
		}

		// Token: 0x17005E3B RID: 24123
		// (get) Token: 0x060130BC RID: 78012 RVA: 0x002E0D97 File Offset: 0x002DEF97
		// (set) Token: 0x060130BD RID: 78013 RVA: 0x002E0DA0 File Offset: 0x002DEFA0
		public BevelBottom BevelBottom
		{
			get
			{
				return base.GetElement<BevelBottom>(1);
			}
			set
			{
				base.SetElement<BevelBottom>(1, value);
			}
		}

		// Token: 0x17005E3C RID: 24124
		// (get) Token: 0x060130BE RID: 78014 RVA: 0x002E0DAA File Offset: 0x002DEFAA
		// (set) Token: 0x060130BF RID: 78015 RVA: 0x002E0DB3 File Offset: 0x002DEFB3
		public ExtrusionColor ExtrusionColor
		{
			get
			{
				return base.GetElement<ExtrusionColor>(2);
			}
			set
			{
				base.SetElement<ExtrusionColor>(2, value);
			}
		}

		// Token: 0x17005E3D RID: 24125
		// (get) Token: 0x060130C0 RID: 78016 RVA: 0x002E0DBD File Offset: 0x002DEFBD
		// (set) Token: 0x060130C1 RID: 78017 RVA: 0x002E0DC6 File Offset: 0x002DEFC6
		public ContourColor ContourColor
		{
			get
			{
				return base.GetElement<ContourColor>(3);
			}
			set
			{
				base.SetElement<ContourColor>(3, value);
			}
		}

		// Token: 0x17005E3E RID: 24126
		// (get) Token: 0x060130C2 RID: 78018 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x060130C3 RID: 78019 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x060130C4 RID: 78020 RVA: 0x00302E78 File Offset: 0x00301078
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "z" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "extrusionH" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "contourW" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "prstMaterial" == name)
			{
				return new EnumValue<PresetMaterialTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060130C5 RID: 78021 RVA: 0x00302EE5 File Offset: 0x003010E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape3DType>(deep);
		}

		// Token: 0x060130C6 RID: 78022 RVA: 0x00302EF0 File Offset: 0x003010F0
		// Note: this type is marked as 'beforefieldinit'.
		static Shape3DType()
		{
			byte[] array = new byte[4];
			Shape3DType.attributeNamespaceIds = array;
			Shape3DType.eleTagNames = new string[] { "bevelT", "bevelB", "extrusionClr", "contourClr", "extLst" };
			Shape3DType.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008463 RID: 33891
		private const string tagName = "sp3d";

		// Token: 0x04008464 RID: 33892
		private const byte tagNsId = 10;

		// Token: 0x04008465 RID: 33893
		internal const int ElementTypeIdConst = 10041;

		// Token: 0x04008466 RID: 33894
		private static string[] attributeTagNames = new string[] { "z", "extrusionH", "contourW", "prstMaterial" };

		// Token: 0x04008467 RID: 33895
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008468 RID: 33896
		private static readonly string[] eleTagNames;

		// Token: 0x04008469 RID: 33897
		private static readonly byte[] eleNamespaceIds;
	}
}
