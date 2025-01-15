using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA9 RID: 12201
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableStyleRowBandSize))]
	[ChildElementInfo(typeof(TableStyleColumnBandSize))]
	[ChildElementInfo(typeof(TableJustification))]
	[ChildElementInfo(typeof(TableCellSpacing))]
	[ChildElementInfo(typeof(TableIndentation))]
	[ChildElementInfo(typeof(TableBorders))]
	[ChildElementInfo(typeof(Shading))]
	[ChildElementInfo(typeof(TableCellMarginDefault))]
	internal class StyleTableProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009318 RID: 37656
		// (get) Token: 0x0601A688 RID: 108168 RVA: 0x0030DFE2 File Offset: 0x0030C1E2
		public override string LocalName
		{
			get
			{
				return "tblPr";
			}
		}

		// Token: 0x17009319 RID: 37657
		// (get) Token: 0x0601A689 RID: 108169 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700931A RID: 37658
		// (get) Token: 0x0601A68A RID: 108170 RVA: 0x00361D4C File Offset: 0x0035FF4C
		internal override int ElementTypeId
		{
			get
			{
				return 11908;
			}
		}

		// Token: 0x0601A68B RID: 108171 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A68C RID: 108172 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleTableProperties()
		{
		}

		// Token: 0x0601A68D RID: 108173 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleTableProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A68E RID: 108174 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleTableProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A68F RID: 108175 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleTableProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A690 RID: 108176 RVA: 0x00361D54 File Offset: 0x0035FF54
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tblStyleRowBandSize" == name)
			{
				return new TableStyleRowBandSize();
			}
			if (23 == namespaceId && "tblStyleColBandSize" == name)
			{
				return new TableStyleColumnBandSize();
			}
			if (23 == namespaceId && "jc" == name)
			{
				return new TableJustification();
			}
			if (23 == namespaceId && "tblCellSpacing" == name)
			{
				return new TableCellSpacing();
			}
			if (23 == namespaceId && "tblInd" == name)
			{
				return new TableIndentation();
			}
			if (23 == namespaceId && "tblBorders" == name)
			{
				return new TableBorders();
			}
			if (23 == namespaceId && "shd" == name)
			{
				return new Shading();
			}
			if (23 == namespaceId && "tblCellMar" == name)
			{
				return new TableCellMarginDefault();
			}
			return null;
		}

		// Token: 0x1700931B RID: 37659
		// (get) Token: 0x0601A691 RID: 108177 RVA: 0x00361E22 File Offset: 0x00360022
		internal override string[] ElementTagNames
		{
			get
			{
				return StyleTableProperties.eleTagNames;
			}
		}

		// Token: 0x1700931C RID: 37660
		// (get) Token: 0x0601A692 RID: 108178 RVA: 0x00361E29 File Offset: 0x00360029
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StyleTableProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700931D RID: 37661
		// (get) Token: 0x0601A693 RID: 108179 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700931E RID: 37662
		// (get) Token: 0x0601A694 RID: 108180 RVA: 0x00361E30 File Offset: 0x00360030
		// (set) Token: 0x0601A695 RID: 108181 RVA: 0x00361E39 File Offset: 0x00360039
		public TableStyleRowBandSize TableStyleRowBandSize
		{
			get
			{
				return base.GetElement<TableStyleRowBandSize>(0);
			}
			set
			{
				base.SetElement<TableStyleRowBandSize>(0, value);
			}
		}

		// Token: 0x1700931F RID: 37663
		// (get) Token: 0x0601A696 RID: 108182 RVA: 0x00361E43 File Offset: 0x00360043
		// (set) Token: 0x0601A697 RID: 108183 RVA: 0x00361E4C File Offset: 0x0036004C
		public TableStyleColumnBandSize TableStyleColumnBandSize
		{
			get
			{
				return base.GetElement<TableStyleColumnBandSize>(1);
			}
			set
			{
				base.SetElement<TableStyleColumnBandSize>(1, value);
			}
		}

		// Token: 0x17009320 RID: 37664
		// (get) Token: 0x0601A698 RID: 108184 RVA: 0x00361E56 File Offset: 0x00360056
		// (set) Token: 0x0601A699 RID: 108185 RVA: 0x00361E5F File Offset: 0x0036005F
		public TableJustification TableJustification
		{
			get
			{
				return base.GetElement<TableJustification>(2);
			}
			set
			{
				base.SetElement<TableJustification>(2, value);
			}
		}

		// Token: 0x17009321 RID: 37665
		// (get) Token: 0x0601A69A RID: 108186 RVA: 0x00361E69 File Offset: 0x00360069
		// (set) Token: 0x0601A69B RID: 108187 RVA: 0x00361E72 File Offset: 0x00360072
		public TableCellSpacing TableCellSpacing
		{
			get
			{
				return base.GetElement<TableCellSpacing>(3);
			}
			set
			{
				base.SetElement<TableCellSpacing>(3, value);
			}
		}

		// Token: 0x17009322 RID: 37666
		// (get) Token: 0x0601A69C RID: 108188 RVA: 0x00361E7C File Offset: 0x0036007C
		// (set) Token: 0x0601A69D RID: 108189 RVA: 0x00361E85 File Offset: 0x00360085
		public TableIndentation TableIndentation
		{
			get
			{
				return base.GetElement<TableIndentation>(4);
			}
			set
			{
				base.SetElement<TableIndentation>(4, value);
			}
		}

		// Token: 0x17009323 RID: 37667
		// (get) Token: 0x0601A69E RID: 108190 RVA: 0x00361E8F File Offset: 0x0036008F
		// (set) Token: 0x0601A69F RID: 108191 RVA: 0x00361E98 File Offset: 0x00360098
		public TableBorders TableBorders
		{
			get
			{
				return base.GetElement<TableBorders>(5);
			}
			set
			{
				base.SetElement<TableBorders>(5, value);
			}
		}

		// Token: 0x17009324 RID: 37668
		// (get) Token: 0x0601A6A0 RID: 108192 RVA: 0x00357042 File Offset: 0x00355242
		// (set) Token: 0x0601A6A1 RID: 108193 RVA: 0x0035704B File Offset: 0x0035524B
		public Shading Shading
		{
			get
			{
				return base.GetElement<Shading>(6);
			}
			set
			{
				base.SetElement<Shading>(6, value);
			}
		}

		// Token: 0x17009325 RID: 37669
		// (get) Token: 0x0601A6A2 RID: 108194 RVA: 0x00356D81 File Offset: 0x00354F81
		// (set) Token: 0x0601A6A3 RID: 108195 RVA: 0x00356D8A File Offset: 0x00354F8A
		public TableCellMarginDefault TableCellMarginDefault
		{
			get
			{
				return base.GetElement<TableCellMarginDefault>(7);
			}
			set
			{
				base.SetElement<TableCellMarginDefault>(7, value);
			}
		}

		// Token: 0x0601A6A4 RID: 108196 RVA: 0x00361EA2 File Offset: 0x003600A2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleTableProperties>(deep);
		}

		// Token: 0x0400ACDE RID: 44254
		private const string tagName = "tblPr";

		// Token: 0x0400ACDF RID: 44255
		private const byte tagNsId = 23;

		// Token: 0x0400ACE0 RID: 44256
		internal const int ElementTypeIdConst = 11908;

		// Token: 0x0400ACE1 RID: 44257
		private static readonly string[] eleTagNames = new string[] { "tblStyleRowBandSize", "tblStyleColBandSize", "jc", "tblCellSpacing", "tblInd", "tblBorders", "shd", "tblCellMar" };

		// Token: 0x0400ACE2 RID: 44258
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
