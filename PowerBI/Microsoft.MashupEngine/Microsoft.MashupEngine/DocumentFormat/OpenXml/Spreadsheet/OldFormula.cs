using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B38 RID: 11064
	[GeneratedCode("DomGen", "2.0")]
	internal class OldFormula : XstringType
	{
		// Token: 0x1700778F RID: 30607
		// (get) Token: 0x06016A63 RID: 92771 RVA: 0x0032D8F0 File Offset: 0x0032BAF0
		public override string LocalName
		{
			get
			{
				return "oldFormula";
			}
		}

		// Token: 0x17007790 RID: 30608
		// (get) Token: 0x06016A64 RID: 92772 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007791 RID: 30609
		// (get) Token: 0x06016A65 RID: 92773 RVA: 0x0032D8F7 File Offset: 0x0032BAF7
		internal override int ElementTypeId
		{
			get
			{
				return 11182;
			}
		}

		// Token: 0x06016A66 RID: 92774 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A67 RID: 92775 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public OldFormula()
		{
		}

		// Token: 0x06016A68 RID: 92776 RVA: 0x0032D835 File Offset: 0x0032BA35
		public OldFormula(string text)
			: base(text)
		{
		}

		// Token: 0x06016A69 RID: 92777 RVA: 0x0032D900 File Offset: 0x0032BB00
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A6A RID: 92778 RVA: 0x0032D91B File Offset: 0x0032BB1B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OldFormula>(deep);
		}

		// Token: 0x04009968 RID: 39272
		private const string tagName = "oldFormula";

		// Token: 0x04009969 RID: 39273
		private const byte tagNsId = 22;

		// Token: 0x0400996A RID: 39274
		internal const int ElementTypeIdConst = 11182;
	}
}
