using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B95 RID: 11157
	[GeneratedCode("DomGen", "2.0")]
	internal class InlineString : RstType
	{
		// Token: 0x17007B1B RID: 31515
		// (get) Token: 0x0601722D RID: 94765 RVA: 0x0033332E File Offset: 0x0033152E
		public override string LocalName
		{
			get
			{
				return "is";
			}
		}

		// Token: 0x17007B1C RID: 31516
		// (get) Token: 0x0601722E RID: 94766 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B1D RID: 31517
		// (get) Token: 0x0601722F RID: 94767 RVA: 0x00333335 File Offset: 0x00331535
		internal override int ElementTypeId
		{
			get
			{
				return 11180;
			}
		}

		// Token: 0x06017230 RID: 94768 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017231 RID: 94769 RVA: 0x00333302 File Offset: 0x00331502
		public InlineString()
		{
		}

		// Token: 0x06017232 RID: 94770 RVA: 0x0033330A File Offset: 0x0033150A
		public InlineString(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017233 RID: 94771 RVA: 0x00333313 File Offset: 0x00331513
		public InlineString(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017234 RID: 94772 RVA: 0x0033331C File Offset: 0x0033151C
		public InlineString(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017235 RID: 94773 RVA: 0x0033333C File Offset: 0x0033153C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InlineString>(deep);
		}

		// Token: 0x04009B2F RID: 39727
		private const string tagName = "is";

		// Token: 0x04009B30 RID: 39728
		private const byte tagNsId = 22;

		// Token: 0x04009B31 RID: 39729
		internal const int ElementTypeIdConst = 11180;
	}
}
