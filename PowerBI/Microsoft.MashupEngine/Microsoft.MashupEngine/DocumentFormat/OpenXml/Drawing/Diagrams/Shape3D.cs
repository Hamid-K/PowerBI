using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200268B RID: 9867
	[ChildElementInfo(typeof(BevelTop))]
	[ChildElementInfo(typeof(ContourColor))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ExtrusionColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BevelBottom))]
	internal class Shape3D : OpenXmlCompositeElement
	{
		// Token: 0x17005CE8 RID: 23784
		// (get) Token: 0x06012DDD RID: 77277 RVA: 0x003002F0 File Offset: 0x002FE4F0
		public override string LocalName
		{
			get
			{
				return "sp3d";
			}
		}

		// Token: 0x17005CE9 RID: 23785
		// (get) Token: 0x06012DDE RID: 77278 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CEA RID: 23786
		// (get) Token: 0x06012DDF RID: 77279 RVA: 0x003002F7 File Offset: 0x002FE4F7
		internal override int ElementTypeId
		{
			get
			{
				return 10682;
			}
		}

		// Token: 0x06012DE0 RID: 77280 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CEB RID: 23787
		// (get) Token: 0x06012DE1 RID: 77281 RVA: 0x003002FE File Offset: 0x002FE4FE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape3D.attributeTagNames;
			}
		}

		// Token: 0x17005CEC RID: 23788
		// (get) Token: 0x06012DE2 RID: 77282 RVA: 0x00300305 File Offset: 0x002FE505
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape3D.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CED RID: 23789
		// (get) Token: 0x06012DE3 RID: 77283 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06012DE4 RID: 77284 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005CEE RID: 23790
		// (get) Token: 0x06012DE5 RID: 77285 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06012DE6 RID: 77286 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005CEF RID: 23791
		// (get) Token: 0x06012DE7 RID: 77287 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x06012DE8 RID: 77288 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17005CF0 RID: 23792
		// (get) Token: 0x06012DE9 RID: 77289 RVA: 0x002E0CE1 File Offset: 0x002DEEE1
		// (set) Token: 0x06012DEA RID: 77290 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x06012DEB RID: 77291 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape3D()
		{
		}

		// Token: 0x06012DEC RID: 77292 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012DED RID: 77293 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012DEE RID: 77294 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012DEF RID: 77295 RVA: 0x0030030C File Offset: 0x002FE50C
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

		// Token: 0x17005CF1 RID: 23793
		// (get) Token: 0x06012DF0 RID: 77296 RVA: 0x00300392 File Offset: 0x002FE592
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape3D.eleTagNames;
			}
		}

		// Token: 0x17005CF2 RID: 23794
		// (get) Token: 0x06012DF1 RID: 77297 RVA: 0x00300399 File Offset: 0x002FE599
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape3D.eleNamespaceIds;
			}
		}

		// Token: 0x17005CF3 RID: 23795
		// (get) Token: 0x06012DF2 RID: 77298 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005CF4 RID: 23796
		// (get) Token: 0x06012DF3 RID: 77299 RVA: 0x002E0D84 File Offset: 0x002DEF84
		// (set) Token: 0x06012DF4 RID: 77300 RVA: 0x002E0D8D File Offset: 0x002DEF8D
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

		// Token: 0x17005CF5 RID: 23797
		// (get) Token: 0x06012DF5 RID: 77301 RVA: 0x002E0D97 File Offset: 0x002DEF97
		// (set) Token: 0x06012DF6 RID: 77302 RVA: 0x002E0DA0 File Offset: 0x002DEFA0
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

		// Token: 0x17005CF6 RID: 23798
		// (get) Token: 0x06012DF7 RID: 77303 RVA: 0x002E0DAA File Offset: 0x002DEFAA
		// (set) Token: 0x06012DF8 RID: 77304 RVA: 0x002E0DB3 File Offset: 0x002DEFB3
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

		// Token: 0x17005CF7 RID: 23799
		// (get) Token: 0x06012DF9 RID: 77305 RVA: 0x002E0DBD File Offset: 0x002DEFBD
		// (set) Token: 0x06012DFA RID: 77306 RVA: 0x002E0DC6 File Offset: 0x002DEFC6
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

		// Token: 0x17005CF8 RID: 23800
		// (get) Token: 0x06012DFB RID: 77307 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x06012DFC RID: 77308 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
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

		// Token: 0x06012DFD RID: 77309 RVA: 0x003003A0 File Offset: 0x002FE5A0
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

		// Token: 0x06012DFE RID: 77310 RVA: 0x0030040D File Offset: 0x002FE60D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape3D>(deep);
		}

		// Token: 0x06012DFF RID: 77311 RVA: 0x00300418 File Offset: 0x002FE618
		// Note: this type is marked as 'beforefieldinit'.
		static Shape3D()
		{
			byte[] array = new byte[4];
			Shape3D.attributeNamespaceIds = array;
			Shape3D.eleTagNames = new string[] { "bevelT", "bevelB", "extrusionClr", "contourClr", "extLst" };
			Shape3D.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
		}

		// Token: 0x040081FD RID: 33277
		private const string tagName = "sp3d";

		// Token: 0x040081FE RID: 33278
		private const byte tagNsId = 14;

		// Token: 0x040081FF RID: 33279
		internal const int ElementTypeIdConst = 10682;

		// Token: 0x04008200 RID: 33280
		private static string[] attributeTagNames = new string[] { "z", "extrusionH", "contourW", "prstMaterial" };

		// Token: 0x04008201 RID: 33281
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008202 RID: 33282
		private static readonly string[] eleTagNames;

		// Token: 0x04008203 RID: 33283
		private static readonly byte[] eleNamespaceIds;
	}
}
