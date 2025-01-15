using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C2E RID: 11310
	[GeneratedCode("DomGen", "2.0")]
	internal class TotalsRowFormula : TableFormulaType
	{
		// Token: 0x170080DC RID: 32988
		// (get) Token: 0x06017E86 RID: 97926 RVA: 0x0033C764 File Offset: 0x0033A964
		public override string LocalName
		{
			get
			{
				return "totalsRowFormula";
			}
		}

		// Token: 0x170080DD RID: 32989
		// (get) Token: 0x06017E87 RID: 97927 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080DE RID: 32990
		// (get) Token: 0x06017E88 RID: 97928 RVA: 0x0033C76B File Offset: 0x0033A96B
		internal override int ElementTypeId
		{
			get
			{
				return 11291;
			}
		}

		// Token: 0x06017E89 RID: 97929 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017E8A RID: 97930 RVA: 0x0033C72D File Offset: 0x0033A92D
		public TotalsRowFormula()
		{
		}

		// Token: 0x06017E8B RID: 97931 RVA: 0x0033C735 File Offset: 0x0033A935
		public TotalsRowFormula(string text)
			: base(text)
		{
		}

		// Token: 0x06017E8C RID: 97932 RVA: 0x0033C774 File Offset: 0x0033A974
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06017E8D RID: 97933 RVA: 0x0033C78F File Offset: 0x0033A98F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TotalsRowFormula>(deep);
		}

		// Token: 0x04009E16 RID: 40470
		private const string tagName = "totalsRowFormula";

		// Token: 0x04009E17 RID: 40471
		private const byte tagNsId = 22;

		// Token: 0x04009E18 RID: 40472
		internal const int ElementTypeIdConst = 11291;
	}
}
