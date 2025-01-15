using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C68 RID: 11368
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class StartBorder : BorderPropertiesType
	{
		// Token: 0x170082B3 RID: 33459
		// (get) Token: 0x060182BC RID: 99004 RVA: 0x00313F27 File Offset: 0x00312127
		public override string LocalName
		{
			get
			{
				return "start";
			}
		}

		// Token: 0x170082B4 RID: 33460
		// (get) Token: 0x060182BD RID: 99005 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082B5 RID: 33461
		// (get) Token: 0x060182BE RID: 99006 RVA: 0x0033F2D6 File Offset: 0x0033D4D6
		internal override int ElementTypeId
		{
			get
			{
				return 11348;
			}
		}

		// Token: 0x060182BF RID: 99007 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060182C0 RID: 99008 RVA: 0x0033F2DD File Offset: 0x0033D4DD
		public StartBorder()
		{
		}

		// Token: 0x060182C1 RID: 99009 RVA: 0x0033F2E5 File Offset: 0x0033D4E5
		public StartBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182C2 RID: 99010 RVA: 0x0033F2EE File Offset: 0x0033D4EE
		public StartBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060182C3 RID: 99011 RVA: 0x0033F2F7 File Offset: 0x0033D4F7
		public StartBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060182C4 RID: 99012 RVA: 0x0033F300 File Offset: 0x0033D500
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartBorder>(deep);
		}

		// Token: 0x04009F26 RID: 40742
		private const string tagName = "start";

		// Token: 0x04009F27 RID: 40743
		private const byte tagNsId = 22;

		// Token: 0x04009F28 RID: 40744
		internal const int ElementTypeIdConst = 11348;
	}
}
