using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C93 RID: 11411
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Row))]
	internal class SheetData : OpenXmlCompositeElement
	{
		// Token: 0x170083DB RID: 33755
		// (get) Token: 0x0601857E RID: 99710 RVA: 0x0033BA9B File Offset: 0x00339C9B
		public override string LocalName
		{
			get
			{
				return "sheetData";
			}
		}

		// Token: 0x170083DC RID: 33756
		// (get) Token: 0x0601857F RID: 99711 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083DD RID: 33757
		// (get) Token: 0x06018580 RID: 99712 RVA: 0x00340B72 File Offset: 0x0033ED72
		internal override int ElementTypeId
		{
			get
			{
				return 11391;
			}
		}

		// Token: 0x06018581 RID: 99713 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018582 RID: 99714 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetData()
		{
		}

		// Token: 0x06018583 RID: 99715 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018584 RID: 99716 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018585 RID: 99717 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018586 RID: 99718 RVA: 0x00340B79 File Offset: 0x0033ED79
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "row" == name)
			{
				return new Row();
			}
			return null;
		}

		// Token: 0x06018587 RID: 99719 RVA: 0x00340B94 File Offset: 0x0033ED94
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetData>(deep);
		}

		// Token: 0x04009FDE RID: 40926
		private const string tagName = "sheetData";

		// Token: 0x04009FDF RID: 40927
		private const byte tagNsId = 22;

		// Token: 0x04009FE0 RID: 40928
		internal const int ElementTypeIdConst = 11391;
	}
}
