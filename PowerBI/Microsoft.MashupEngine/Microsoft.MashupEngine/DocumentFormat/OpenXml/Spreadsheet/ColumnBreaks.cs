using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD9 RID: 11225
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnBreaks : PageBreakType
	{
		// Token: 0x17007D64 RID: 32100
		// (get) Token: 0x0601770A RID: 96010 RVA: 0x00336CC9 File Offset: 0x00334EC9
		public override string LocalName
		{
			get
			{
				return "colBreaks";
			}
		}

		// Token: 0x17007D65 RID: 32101
		// (get) Token: 0x0601770B RID: 96011 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D66 RID: 32102
		// (get) Token: 0x0601770C RID: 96012 RVA: 0x00336CD0 File Offset: 0x00334ED0
		internal override int ElementTypeId
		{
			get
			{
				return 11197;
			}
		}

		// Token: 0x0601770D RID: 96013 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601770E RID: 96014 RVA: 0x00336C9D File Offset: 0x00334E9D
		public ColumnBreaks()
		{
		}

		// Token: 0x0601770F RID: 96015 RVA: 0x00336CA5 File Offset: 0x00334EA5
		public ColumnBreaks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017710 RID: 96016 RVA: 0x00336CAE File Offset: 0x00334EAE
		public ColumnBreaks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017711 RID: 96017 RVA: 0x00336CB7 File Offset: 0x00334EB7
		public ColumnBreaks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017712 RID: 96018 RVA: 0x00336CD7 File Offset: 0x00334ED7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnBreaks>(deep);
		}

		// Token: 0x04009C5B RID: 40027
		private const string tagName = "colBreaks";

		// Token: 0x04009C5C RID: 40028
		private const byte tagNsId = 22;

		// Token: 0x04009C5D RID: 40029
		internal const int ElementTypeIdConst = 11197;
	}
}
