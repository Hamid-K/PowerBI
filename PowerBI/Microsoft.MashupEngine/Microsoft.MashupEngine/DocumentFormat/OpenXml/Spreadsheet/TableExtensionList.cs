using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C65 RID: 11365
	[ChildElementInfo(typeof(TableExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170082A0 RID: 33440
		// (get) Token: 0x06018290 RID: 98960 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170082A1 RID: 33441
		// (get) Token: 0x06018291 RID: 98961 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082A2 RID: 33442
		// (get) Token: 0x06018292 RID: 98962 RVA: 0x0033F133 File Offset: 0x0033D333
		internal override int ElementTypeId
		{
			get
			{
				return 11346;
			}
		}

		// Token: 0x06018293 RID: 98963 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018294 RID: 98964 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableExtensionList()
		{
		}

		// Token: 0x06018295 RID: 98965 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018296 RID: 98966 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018297 RID: 98967 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018298 RID: 98968 RVA: 0x0033F13A File Offset: 0x0033D33A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new TableExtension();
			}
			return null;
		}

		// Token: 0x06018299 RID: 98969 RVA: 0x0033F155 File Offset: 0x0033D355
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableExtensionList>(deep);
		}

		// Token: 0x04009F1A RID: 40730
		private const string tagName = "extLst";

		// Token: 0x04009F1B RID: 40731
		private const byte tagNsId = 22;

		// Token: 0x04009F1C RID: 40732
		internal const int ElementTypeIdConst = 11346;
	}
}
