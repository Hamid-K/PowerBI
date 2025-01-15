using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C2D RID: 11309
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculatedColumnFormula : TableFormulaType
	{
		// Token: 0x170080D9 RID: 32985
		// (get) Token: 0x06017E7E RID: 97918 RVA: 0x0033C71F File Offset: 0x0033A91F
		public override string LocalName
		{
			get
			{
				return "calculatedColumnFormula";
			}
		}

		// Token: 0x170080DA RID: 32986
		// (get) Token: 0x06017E7F RID: 97919 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080DB RID: 32987
		// (get) Token: 0x06017E80 RID: 97920 RVA: 0x0033C726 File Offset: 0x0033A926
		internal override int ElementTypeId
		{
			get
			{
				return 11290;
			}
		}

		// Token: 0x06017E81 RID: 97921 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017E82 RID: 97922 RVA: 0x0033C72D File Offset: 0x0033A92D
		public CalculatedColumnFormula()
		{
		}

		// Token: 0x06017E83 RID: 97923 RVA: 0x0033C735 File Offset: 0x0033A935
		public CalculatedColumnFormula(string text)
			: base(text)
		{
		}

		// Token: 0x06017E84 RID: 97924 RVA: 0x0033C740 File Offset: 0x0033A940
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06017E85 RID: 97925 RVA: 0x0033C75B File Offset: 0x0033A95B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedColumnFormula>(deep);
		}

		// Token: 0x04009E13 RID: 40467
		private const string tagName = "calculatedColumnFormula";

		// Token: 0x04009E14 RID: 40468
		private const byte tagNsId = 22;

		// Token: 0x04009E15 RID: 40469
		internal const int ElementTypeIdConst = 11290;
	}
}
