using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C1C RID: 11292
	[ChildElementInfo(typeof(ExternalSheetData))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetDataSet : OpenXmlCompositeElement
	{
		// Token: 0x1700804A RID: 32842
		// (get) Token: 0x06017D3C RID: 97596 RVA: 0x0033B9B4 File Offset: 0x00339BB4
		public override string LocalName
		{
			get
			{
				return "sheetDataSet";
			}
		}

		// Token: 0x1700804B RID: 32843
		// (get) Token: 0x06017D3D RID: 97597 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700804C RID: 32844
		// (get) Token: 0x06017D3E RID: 97598 RVA: 0x0033B9BB File Offset: 0x00339BBB
		internal override int ElementTypeId
		{
			get
			{
				return 11273;
			}
		}

		// Token: 0x06017D3F RID: 97599 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017D40 RID: 97600 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetDataSet()
		{
		}

		// Token: 0x06017D41 RID: 97601 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetDataSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D42 RID: 97602 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetDataSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D43 RID: 97603 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetDataSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D44 RID: 97604 RVA: 0x0033B9C2 File Offset: 0x00339BC2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetData" == name)
			{
				return new ExternalSheetData();
			}
			return null;
		}

		// Token: 0x06017D45 RID: 97605 RVA: 0x0033B9DD File Offset: 0x00339BDD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetDataSet>(deep);
		}

		// Token: 0x04009DBB RID: 40379
		private const string tagName = "sheetDataSet";

		// Token: 0x04009DBC RID: 40380
		private const byte tagNsId = 22;

		// Token: 0x04009DBD RID: 40381
		internal const int ElementTypeIdConst = 11273;
	}
}
