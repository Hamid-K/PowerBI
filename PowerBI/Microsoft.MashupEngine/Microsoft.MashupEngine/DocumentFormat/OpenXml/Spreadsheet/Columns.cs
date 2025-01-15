using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C92 RID: 11410
	[ChildElementInfo(typeof(Column))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Columns : OpenXmlCompositeElement
	{
		// Token: 0x170083D8 RID: 33752
		// (get) Token: 0x06018574 RID: 99700 RVA: 0x00340B40 File Offset: 0x0033ED40
		public override string LocalName
		{
			get
			{
				return "cols";
			}
		}

		// Token: 0x170083D9 RID: 33753
		// (get) Token: 0x06018575 RID: 99701 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083DA RID: 33754
		// (get) Token: 0x06018576 RID: 99702 RVA: 0x00340B47 File Offset: 0x0033ED47
		internal override int ElementTypeId
		{
			get
			{
				return 11390;
			}
		}

		// Token: 0x06018577 RID: 99703 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018578 RID: 99704 RVA: 0x00293ECF File Offset: 0x002920CF
		public Columns()
		{
		}

		// Token: 0x06018579 RID: 99705 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Columns(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601857A RID: 99706 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Columns(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601857B RID: 99707 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Columns(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601857C RID: 99708 RVA: 0x00340B4E File Offset: 0x0033ED4E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "col" == name)
			{
				return new Column();
			}
			return null;
		}

		// Token: 0x0601857D RID: 99709 RVA: 0x00340B69 File Offset: 0x0033ED69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Columns>(deep);
		}

		// Token: 0x04009FDB RID: 40923
		private const string tagName = "cols";

		// Token: 0x04009FDC RID: 40924
		private const byte tagNsId = 22;

		// Token: 0x04009FDD RID: 40925
		internal const int ElementTypeIdConst = 11390;
	}
}
