using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD8 RID: 11224
	[GeneratedCode("DomGen", "2.0")]
	internal class RowBreaks : PageBreakType
	{
		// Token: 0x17007D61 RID: 32097
		// (get) Token: 0x06017701 RID: 96001 RVA: 0x00336C8F File Offset: 0x00334E8F
		public override string LocalName
		{
			get
			{
				return "rowBreaks";
			}
		}

		// Token: 0x17007D62 RID: 32098
		// (get) Token: 0x06017702 RID: 96002 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D63 RID: 32099
		// (get) Token: 0x06017703 RID: 96003 RVA: 0x00336C96 File Offset: 0x00334E96
		internal override int ElementTypeId
		{
			get
			{
				return 11196;
			}
		}

		// Token: 0x06017704 RID: 96004 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017705 RID: 96005 RVA: 0x00336C9D File Offset: 0x00334E9D
		public RowBreaks()
		{
		}

		// Token: 0x06017706 RID: 96006 RVA: 0x00336CA5 File Offset: 0x00334EA5
		public RowBreaks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017707 RID: 96007 RVA: 0x00336CAE File Offset: 0x00334EAE
		public RowBreaks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017708 RID: 96008 RVA: 0x00336CB7 File Offset: 0x00334EB7
		public RowBreaks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017709 RID: 96009 RVA: 0x00336CC0 File Offset: 0x00334EC0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowBreaks>(deep);
		}

		// Token: 0x04009C58 RID: 40024
		private const string tagName = "rowBreaks";

		// Token: 0x04009C59 RID: 40025
		private const byte tagNsId = 22;

		// Token: 0x04009C5A RID: 40026
		internal const int ElementTypeIdConst = 11196;
	}
}
