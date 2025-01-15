using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x020012CD RID: 4813
	public class Options : DSLOptions
	{
		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06009122 RID: 37154 RVA: 0x001E9377 File Offset: 0x001E7577
		// (set) Token: 0x06009123 RID: 37155 RVA: 0x001E937F File Offset: 0x001E757F
		public bool LearnCsv { get; set; } = true;

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06009124 RID: 37156 RVA: 0x001E9388 File Offset: 0x001E7588
		// (set) Token: 0x06009125 RID: 37157 RVA: 0x001E9390 File Offset: 0x001E7590
		public bool LearnFw { get; set; } = true;

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06009126 RID: 37158 RVA: 0x001E9399 File Offset: 0x001E7599
		// (set) Token: 0x06009127 RID: 37159 RVA: 0x001E93A1 File Offset: 0x001E75A1
		public bool LearnExtractionText { get; set; }

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06009128 RID: 37160 RVA: 0x001E93AA File Offset: 0x001E75AA
		// (set) Token: 0x06009129 RID: 37161 RVA: 0x001E93B2 File Offset: 0x001E75B2
		public string FwSchema { get; set; }

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x0600912A RID: 37162 RVA: 0x001E93BB File Offset: 0x001E75BB
		// (set) Token: 0x0600912B RID: 37163 RVA: 0x001E93C3 File Offset: 0x001E75C3
		public IReadOnlyList<Record<int, int?>> FieldPositions { get; set; }

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x0600912C RID: 37164 RVA: 0x001E93CC File Offset: 0x001E75CC
		// (set) Token: 0x0600912D RID: 37165 RVA: 0x001E93D4 File Offset: 0x001E75D4
		public string Delimiter { get; set; }

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x0600912E RID: 37166 RVA: 0x001E93DD File Offset: 0x001E75DD
		// (set) Token: 0x0600912F RID: 37167 RVA: 0x001E93E5 File Offset: 0x001E75E5
		public int? Skip { get; set; }

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06009130 RID: 37168 RVA: 0x001E93EE File Offset: 0x001E75EE
		// (set) Token: 0x06009131 RID: 37169 RVA: 0x001E93F6 File Offset: 0x001E75F6
		public int? SkipFooter { get; set; }

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06009132 RID: 37170 RVA: 0x001E93FF File Offset: 0x001E75FF
		// (set) Token: 0x06009133 RID: 37171 RVA: 0x001E9407 File Offset: 0x001E7607
		public ColumnNameCleaningType ColumnNameCleaning { get; set; }

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06009134 RID: 37172 RVA: 0x001E9410 File Offset: 0x001E7610
		// (set) Token: 0x06009135 RID: 37173 RVA: 0x001E9418 File Offset: 0x001E7618
		public int? LearnLineLimit { get; set; }
	}
}
