using System;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FDC RID: 4060
	public class ProgramProperties : IProgramProperties
	{
		// Token: 0x06007002 RID: 28674 RVA: 0x0016E01E File Offset: 0x0016C21E
		public ProgramProperties(string[] columnSelectors, string rowSelector, int numOperators, string title, TableKind? kind, int rowCount, string[] attributes = null)
		{
			this.ColumnSelectors = columnSelectors;
			this.RowSelector = rowSelector;
			this.NumOperators = numOperators;
			this.Title = title;
			this.TableKind = kind;
			this.RowCount = rowCount;
			this.Attributes = attributes;
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06007003 RID: 28675 RVA: 0x0016E05B File Offset: 0x0016C25B
		public string[] ColumnSelectors { get; }

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06007004 RID: 28676 RVA: 0x0016E063 File Offset: 0x0016C263
		public string RowSelector { get; }

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06007005 RID: 28677 RVA: 0x0016E06B File Offset: 0x0016C26B
		public int NumOperators { get; }

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06007006 RID: 28678 RVA: 0x0016E073 File Offset: 0x0016C273
		public string Title { get; }

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06007007 RID: 28679 RVA: 0x0016E07B File Offset: 0x0016C27B
		public int RowCount { get; }

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06007008 RID: 28680 RVA: 0x0016E083 File Offset: 0x0016C283
		public TableKind? TableKind { get; }

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x0016E08B File Offset: 0x0016C28B
		public string[] Attributes { get; }
	}
}
