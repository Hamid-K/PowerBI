using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B25 RID: 11045
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SingleXmlCell))]
	internal class SingleXmlCells : OpenXmlPartRootElement
	{
		// Token: 0x170076E1 RID: 30433
		// (get) Token: 0x060168C7 RID: 92359 RVA: 0x0032C40C File Offset: 0x0032A60C
		public override string LocalName
		{
			get
			{
				return "singleXmlCells";
			}
		}

		// Token: 0x170076E2 RID: 30434
		// (get) Token: 0x060168C8 RID: 92360 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076E3 RID: 30435
		// (get) Token: 0x060168C9 RID: 92361 RVA: 0x0032C413 File Offset: 0x0032A613
		internal override int ElementTypeId
		{
			get
			{
				return 11043;
			}
		}

		// Token: 0x060168CA RID: 92362 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060168CB RID: 92363 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal SingleXmlCells(SingleCellTablePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060168CC RID: 92364 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SingleCellTablePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076E4 RID: 30436
		// (get) Token: 0x060168CD RID: 92365 RVA: 0x0032C41A File Offset: 0x0032A61A
		// (set) Token: 0x060168CE RID: 92366 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SingleCellTablePart SingleCellTablePart
		{
			get
			{
				return base.OpenXmlPart as SingleCellTablePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060168CF RID: 92367 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public SingleXmlCells(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060168D0 RID: 92368 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public SingleXmlCells(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060168D1 RID: 92369 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public SingleXmlCells(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060168D2 RID: 92370 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public SingleXmlCells()
		{
		}

		// Token: 0x060168D3 RID: 92371 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SingleCellTablePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060168D4 RID: 92372 RVA: 0x0032C427 File Offset: 0x0032A627
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "singleXmlCell" == name)
			{
				return new SingleXmlCell();
			}
			return null;
		}

		// Token: 0x060168D5 RID: 92373 RVA: 0x0032C442 File Offset: 0x0032A642
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SingleXmlCells>(deep);
		}

		// Token: 0x0400991A RID: 39194
		private const string tagName = "singleXmlCells";

		// Token: 0x0400991B RID: 39195
		private const byte tagNsId = 22;

		// Token: 0x0400991C RID: 39196
		internal const int ElementTypeIdConst = 11043;
	}
}
