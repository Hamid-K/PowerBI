using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C55 RID: 11349
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Sheet))]
	internal class Sheets : OpenXmlCompositeElement
	{
		// Token: 0x1700823A RID: 33338
		// (get) Token: 0x0601819C RID: 98716 RVA: 0x0033E85F File Offset: 0x0033CA5F
		public override string LocalName
		{
			get
			{
				return "sheets";
			}
		}

		// Token: 0x1700823B RID: 33339
		// (get) Token: 0x0601819D RID: 98717 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700823C RID: 33340
		// (get) Token: 0x0601819E RID: 98718 RVA: 0x0033E866 File Offset: 0x0033CA66
		internal override int ElementTypeId
		{
			get
			{
				return 11330;
			}
		}

		// Token: 0x0601819F RID: 98719 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060181A0 RID: 98720 RVA: 0x00293ECF File Offset: 0x002920CF
		public Sheets()
		{
		}

		// Token: 0x060181A1 RID: 98721 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Sheets(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181A2 RID: 98722 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Sheets(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181A3 RID: 98723 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Sheets(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060181A4 RID: 98724 RVA: 0x0033E86D File Offset: 0x0033CA6D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheet" == name)
			{
				return new Sheet();
			}
			return null;
		}

		// Token: 0x060181A5 RID: 98725 RVA: 0x0033E888 File Offset: 0x0033CA88
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sheets>(deep);
		}

		// Token: 0x04009ED8 RID: 40664
		private const string tagName = "sheets";

		// Token: 0x04009ED9 RID: 40665
		private const byte tagNsId = 22;

		// Token: 0x04009EDA RID: 40666
		internal const int ElementTypeIdConst = 11330;
	}
}
