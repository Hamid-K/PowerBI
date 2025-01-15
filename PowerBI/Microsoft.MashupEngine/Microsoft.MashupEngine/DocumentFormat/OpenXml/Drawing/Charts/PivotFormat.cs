using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C1 RID: 9665
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(Marker))]
	[ChildElementInfo(typeof(DataLabel))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotFormat : OpenXmlCompositeElement
	{
		// Token: 0x1700577F RID: 22399
		// (get) Token: 0x060121B1 RID: 74161 RVA: 0x002F591F File Offset: 0x002F3B1F
		public override string LocalName
		{
			get
			{
				return "pivotFmt";
			}
		}

		// Token: 0x17005780 RID: 22400
		// (get) Token: 0x060121B2 RID: 74162 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005781 RID: 22401
		// (get) Token: 0x060121B3 RID: 74163 RVA: 0x002F5926 File Offset: 0x002F3B26
		internal override int ElementTypeId
		{
			get
			{
				return 10491;
			}
		}

		// Token: 0x060121B4 RID: 74164 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121B5 RID: 74165 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFormat()
		{
		}

		// Token: 0x060121B6 RID: 74166 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121B7 RID: 74167 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121B8 RID: 74168 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060121B9 RID: 74169 RVA: 0x002F5930 File Offset: 0x002F3B30
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (11 == namespaceId && "marker" == name)
			{
				return new Marker();
			}
			if (11 == namespaceId && "dLbl" == name)
			{
				return new DataLabel();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005782 RID: 22402
		// (get) Token: 0x060121BA RID: 74170 RVA: 0x002F59B6 File Offset: 0x002F3BB6
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotFormat.eleTagNames;
			}
		}

		// Token: 0x17005783 RID: 22403
		// (get) Token: 0x060121BB RID: 74171 RVA: 0x002F59BD File Offset: 0x002F3BBD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotFormat.eleNamespaceIds;
			}
		}

		// Token: 0x17005784 RID: 22404
		// (get) Token: 0x060121BC RID: 74172 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005785 RID: 22405
		// (get) Token: 0x060121BD RID: 74173 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x060121BE RID: 74174 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public Index Index
		{
			get
			{
				return base.GetElement<Index>(0);
			}
			set
			{
				base.SetElement<Index>(0, value);
			}
		}

		// Token: 0x17005786 RID: 22406
		// (get) Token: 0x060121BF RID: 74175 RVA: 0x002F59C4 File Offset: 0x002F3BC4
		// (set) Token: 0x060121C0 RID: 74176 RVA: 0x002F59CD File Offset: 0x002F3BCD
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x17005787 RID: 22407
		// (get) Token: 0x060121C1 RID: 74177 RVA: 0x002F59D7 File Offset: 0x002F3BD7
		// (set) Token: 0x060121C2 RID: 74178 RVA: 0x002F59E0 File Offset: 0x002F3BE0
		public Marker Marker
		{
			get
			{
				return base.GetElement<Marker>(2);
			}
			set
			{
				base.SetElement<Marker>(2, value);
			}
		}

		// Token: 0x17005788 RID: 22408
		// (get) Token: 0x060121C3 RID: 74179 RVA: 0x002F59EA File Offset: 0x002F3BEA
		// (set) Token: 0x060121C4 RID: 74180 RVA: 0x002F59F3 File Offset: 0x002F3BF3
		public DataLabel DataLabel
		{
			get
			{
				return base.GetElement<DataLabel>(3);
			}
			set
			{
				base.SetElement<DataLabel>(3, value);
			}
		}

		// Token: 0x17005789 RID: 22409
		// (get) Token: 0x060121C5 RID: 74181 RVA: 0x002F2A44 File Offset: 0x002F0C44
		// (set) Token: 0x060121C6 RID: 74182 RVA: 0x002F2A4D File Offset: 0x002F0C4D
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

		// Token: 0x060121C7 RID: 74183 RVA: 0x002F59FD File Offset: 0x002F3BFD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFormat>(deep);
		}

		// Token: 0x04007E42 RID: 32322
		private const string tagName = "pivotFmt";

		// Token: 0x04007E43 RID: 32323
		private const byte tagNsId = 11;

		// Token: 0x04007E44 RID: 32324
		internal const int ElementTypeIdConst = 10491;

		// Token: 0x04007E45 RID: 32325
		private static readonly string[] eleTagNames = new string[] { "idx", "spPr", "marker", "dLbl", "extLst" };

		// Token: 0x04007E46 RID: 32326
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11 };
	}
}
