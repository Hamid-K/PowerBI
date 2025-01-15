using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x020012C9 RID: 4809
	public abstract class LearnResult
	{
		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x060090FD RID: 37117 RVA: 0x001E91EF File Offset: 0x001E73EF
		// (set) Token: 0x060090FE RID: 37118 RVA: 0x001E91F7 File Offset: 0x001E73F7
		public IReadOnlyList<string> RawColumnNames { get; set; }

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x060090FF RID: 37119 RVA: 0x001E9200 File Offset: 0x001E7400
		// (set) Token: 0x06009100 RID: 37120 RVA: 0x001E9208 File Offset: 0x001E7408
		public IReadOnlyList<string> ColumnNames { get; set; }

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06009101 RID: 37121 RVA: 0x001E9211 File Offset: 0x001E7411
		// (set) Token: 0x06009102 RID: 37122 RVA: 0x001E9219 File Offset: 0x001E7419
		public int Skip { get; set; }

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06009103 RID: 37123 RVA: 0x001E9222 File Offset: 0x001E7422
		// (set) Token: 0x06009104 RID: 37124 RVA: 0x001E922A File Offset: 0x001E742A
		public int SkipFooter { get; set; }

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06009105 RID: 37125 RVA: 0x001E9233 File Offset: 0x001E7433
		// (set) Token: 0x06009106 RID: 37126 RVA: 0x001E923B File Offset: 0x001E743B
		public bool FilterEmptyLines { get; set; }

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06009107 RID: 37127 RVA: 0x001E9244 File Offset: 0x001E7444
		// (set) Token: 0x06009108 RID: 37128 RVA: 0x001E924C File Offset: 0x001E744C
		public Optional<string> CommentStr { get; set; }

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06009109 RID: 37129 RVA: 0x001E9255 File Offset: 0x001E7455
		// (set) Token: 0x0600910A RID: 37130 RVA: 0x001E925D File Offset: 0x001E745D
		public int SkipEmptyAndCommentCount { get; set; }

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x0600910B RID: 37131 RVA: 0x001E9266 File Offset: 0x001E7466
		// (set) Token: 0x0600910C RID: 37132 RVA: 0x001E926E File Offset: 0x001E746E
		public bool HasEmptyLines { get; set; }

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x0600910D RID: 37133 RVA: 0x001E9277 File Offset: 0x001E7477
		// (set) Token: 0x0600910E RID: 37134 RVA: 0x001E927F File Offset: 0x001E747F
		public bool HasMultiLineRows { get; set; }

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x0600910F RID: 37135 RVA: 0x001E9288 File Offset: 0x001E7488
		// (set) Token: 0x06009110 RID: 37136 RVA: 0x001E9290 File Offset: 0x001E7490
		public IReadOnlyList<string> NewLineStrings { get; set; }

		// Token: 0x06009111 RID: 37137 RVA: 0x001E929C File Offset: 0x001E749C
		public T Switch<T>(Func<LearnCsvResult, T> csvCase, Func<LearnFwResult, T> fwCase, Func<LearnETextResult, T> etextCase)
		{
			LearnCsvResult learnCsvResult = this as LearnCsvResult;
			T t;
			if (learnCsvResult == null)
			{
				LearnFwResult learnFwResult = this as LearnFwResult;
				if (learnFwResult == null)
				{
					LearnETextResult learnETextResult = this as LearnETextResult;
					if (learnETextResult == null)
					{
						throw new Exception(string.Format("Unknown ${0}: ${1}", "LearnResult", base.GetType()));
					}
					t = etextCase(learnETextResult);
				}
				else
				{
					t = fwCase(learnFwResult);
				}
			}
			else
			{
				t = csvCase(learnCsvResult);
			}
			return t;
		}
	}
}
