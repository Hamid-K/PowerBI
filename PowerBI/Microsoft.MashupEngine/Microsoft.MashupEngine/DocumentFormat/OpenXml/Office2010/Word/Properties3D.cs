using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A9 RID: 9385
	[ChildElementInfo(typeof(ExtrusionColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BevelBottom), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BevelTop), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ContourColor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Properties3D : OpenXmlCompositeElement
	{
		// Token: 0x17005224 RID: 21028
		// (get) Token: 0x060115CB RID: 71115 RVA: 0x002EDB15 File Offset: 0x002EBD15
		public override string LocalName
		{
			get
			{
				return "props3d";
			}
		}

		// Token: 0x17005225 RID: 21029
		// (get) Token: 0x060115CC RID: 71116 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005226 RID: 21030
		// (get) Token: 0x060115CD RID: 71117 RVA: 0x002EDB1C File Offset: 0x002EBD1C
		internal override int ElementTypeId
		{
			get
			{
				return 12859;
			}
		}

		// Token: 0x060115CE RID: 71118 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005227 RID: 21031
		// (get) Token: 0x060115CF RID: 71119 RVA: 0x002EDB23 File Offset: 0x002EBD23
		internal override string[] AttributeTagNames
		{
			get
			{
				return Properties3D.attributeTagNames;
			}
		}

		// Token: 0x17005228 RID: 21032
		// (get) Token: 0x060115D0 RID: 71120 RVA: 0x002EDB2A File Offset: 0x002EBD2A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Properties3D.attributeNamespaceIds;
			}
		}

		// Token: 0x17005229 RID: 21033
		// (get) Token: 0x060115D1 RID: 71121 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060115D2 RID: 71122 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "extrusionH")]
		public Int64Value ExtrusionHeight
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

		// Token: 0x1700522A RID: 21034
		// (get) Token: 0x060115D3 RID: 71123 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060115D4 RID: 71124 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "contourW")]
		public Int64Value ContourWidth
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

		// Token: 0x1700522B RID: 21035
		// (get) Token: 0x060115D5 RID: 71125 RVA: 0x002EDB31 File Offset: 0x002EBD31
		// (set) Token: 0x060115D6 RID: 71126 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "prstMaterial")]
		public EnumValue<PresetMaterialTypeValues> PresetMaterialType
		{
			get
			{
				return (EnumValue<PresetMaterialTypeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060115D7 RID: 71127 RVA: 0x00293ECF File Offset: 0x002920CF
		public Properties3D()
		{
		}

		// Token: 0x060115D8 RID: 71128 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Properties3D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115D9 RID: 71129 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Properties3D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115DA RID: 71130 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Properties3D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060115DB RID: 71131 RVA: 0x002EDB40 File Offset: 0x002EBD40
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "bevelT" == name)
			{
				return new BevelTop();
			}
			if (52 == namespaceId && "bevelB" == name)
			{
				return new BevelBottom();
			}
			if (52 == namespaceId && "extrusionClr" == name)
			{
				return new ExtrusionColor();
			}
			if (52 == namespaceId && "contourClr" == name)
			{
				return new ContourColor();
			}
			return null;
		}

		// Token: 0x1700522C RID: 21036
		// (get) Token: 0x060115DC RID: 71132 RVA: 0x002EDBAE File Offset: 0x002EBDAE
		internal override string[] ElementTagNames
		{
			get
			{
				return Properties3D.eleTagNames;
			}
		}

		// Token: 0x1700522D RID: 21037
		// (get) Token: 0x060115DD RID: 71133 RVA: 0x002EDBB5 File Offset: 0x002EBDB5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Properties3D.eleNamespaceIds;
			}
		}

		// Token: 0x1700522E RID: 21038
		// (get) Token: 0x060115DE RID: 71134 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700522F RID: 21039
		// (get) Token: 0x060115DF RID: 71135 RVA: 0x002EDBBC File Offset: 0x002EBDBC
		// (set) Token: 0x060115E0 RID: 71136 RVA: 0x002EDBC5 File Offset: 0x002EBDC5
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

		// Token: 0x17005230 RID: 21040
		// (get) Token: 0x060115E1 RID: 71137 RVA: 0x002EDBCF File Offset: 0x002EBDCF
		// (set) Token: 0x060115E2 RID: 71138 RVA: 0x002EDBD8 File Offset: 0x002EBDD8
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

		// Token: 0x17005231 RID: 21041
		// (get) Token: 0x060115E3 RID: 71139 RVA: 0x002EDBE2 File Offset: 0x002EBDE2
		// (set) Token: 0x060115E4 RID: 71140 RVA: 0x002EDBEB File Offset: 0x002EBDEB
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

		// Token: 0x17005232 RID: 21042
		// (get) Token: 0x060115E5 RID: 71141 RVA: 0x002EDBF5 File Offset: 0x002EBDF5
		// (set) Token: 0x060115E6 RID: 71142 RVA: 0x002EDBFE File Offset: 0x002EBDFE
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

		// Token: 0x060115E7 RID: 71143 RVA: 0x002EDC08 File Offset: 0x002EBE08
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "extrusionH" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "contourW" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "prstMaterial" == name)
			{
				return new EnumValue<PresetMaterialTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060115E8 RID: 71144 RVA: 0x002EDC65 File Offset: 0x002EBE65
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Properties3D>(deep);
		}

		// Token: 0x04007973 RID: 31091
		private const string tagName = "props3d";

		// Token: 0x04007974 RID: 31092
		private const byte tagNsId = 52;

		// Token: 0x04007975 RID: 31093
		internal const int ElementTypeIdConst = 12859;

		// Token: 0x04007976 RID: 31094
		private static string[] attributeTagNames = new string[] { "extrusionH", "contourW", "prstMaterial" };

		// Token: 0x04007977 RID: 31095
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52, 52 };

		// Token: 0x04007978 RID: 31096
		private static readonly string[] eleTagNames = new string[] { "bevelT", "bevelB", "extrusionClr", "contourClr" };

		// Token: 0x04007979 RID: 31097
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52, 52, 52 };
	}
}
