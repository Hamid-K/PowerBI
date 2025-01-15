using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C69 RID: 11369
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class EndBorder : BorderPropertiesType
	{
		// Token: 0x170082B6 RID: 33462
		// (get) Token: 0x060182C5 RID: 99013 RVA: 0x0030761A File Offset: 0x0030581A
		public override string LocalName
		{
			get
			{
				return "end";
			}
		}

		// Token: 0x170082B7 RID: 33463
		// (get) Token: 0x060182C6 RID: 99014 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082B8 RID: 33464
		// (get) Token: 0x060182C7 RID: 99015 RVA: 0x0033F309 File Offset: 0x0033D509
		internal override int ElementTypeId
		{
			get
			{
				return 11349;
			}
		}

		// Token: 0x060182C8 RID: 99016 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060182C9 RID: 99017 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public EndBorder()
		{
		}

		// Token: 0x060182CA RID: 99018 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public EndBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182CB RID: 99019 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public EndBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182CC RID: 99020 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public EndBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182CD RID: 99021 RVA: 0x0033F310 File Offset: 0x0033D510
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndBorder>(deep);
		}

		// Token: 0x04009F29 RID: 40745
		private const string tagName = "end";

		// Token: 0x04009F2A RID: 40746
		private const byte tagNsId = 22;

		// Token: 0x04009F2B RID: 40747
		internal const int ElementTypeIdConst = 11349;
	}
}
