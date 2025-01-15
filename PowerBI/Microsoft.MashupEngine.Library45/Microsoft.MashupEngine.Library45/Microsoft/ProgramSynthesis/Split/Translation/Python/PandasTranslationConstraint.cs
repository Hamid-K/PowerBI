using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Translation.Python
{
	// Token: 0x02001407 RID: 5127
	public class PandasTranslationConstraint : TranslationConstraint, IRenderingOptions
	{
		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06009E53 RID: 40531 RVA: 0x00219008 File Offset: 0x00217208
		// (set) Token: 0x06009E54 RID: 40532 RVA: 0x00219010 File Offset: 0x00217210
		public string DataFrameName { get; set; } = "df";

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06009E55 RID: 40533 RVA: 0x00219019 File Offset: 0x00217219
		// (set) Token: 0x06009E56 RID: 40534 RVA: 0x00219021 File Offset: 0x00217221
		public uint IndentLevel { get; set; }

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06009E57 RID: 40535 RVA: 0x0021902A File Offset: 0x0021722A
		// (set) Token: 0x06009E58 RID: 40536 RVA: 0x00219032 File Offset: 0x00217232
		public uint IndentSize { get; set; } = 4U;

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06009E59 RID: 40537 RVA: 0x0021903B File Offset: 0x0021723B
		// (set) Token: 0x06009E5A RID: 40538 RVA: 0x00219043 File Offset: 0x00217243
		public string InputColumnName { get; set; } = "input";

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06009E5B RID: 40539 RVA: 0x0021904C File Offset: 0x0021724C
		// (set) Token: 0x06009E5C RID: 40540 RVA: 0x00219054 File Offset: 0x00217254
		public int? NewColumnsIndex { get; set; }

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06009E5D RID: 40541 RVA: 0x0021905D File Offset: 0x0021725D
		// (set) Token: 0x06009E5E RID: 40542 RVA: 0x00219065 File Offset: 0x00217265
		public int MaximumExamplesInComments { get; set; } = 10;

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x06009E5F RID: 40543 RVA: 0x0021906E File Offset: 0x0021726E
		// (set) Token: 0x06009E60 RID: 40544 RVA: 0x00219076 File Offset: 0x00217276
		public string PandasModuleAlias { get; set; } = "pd";

		// Token: 0x06009E61 RID: 40545 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			return true;
		}

		// Token: 0x06009E62 RID: 40546 RVA: 0x00219080 File Offset: 0x00217280
		public override int GetHashCode()
		{
			return (((((((17 * 17 + this.DataFrameName.GetHashCode()) * 17 + this.InputColumnName.GetHashCode()) * 17 + this.OutputColumnPrefix.GetHashCode()) * 17 + this.NewColumnsIndex.GetHashCode()) * 17 + this.MaximumExamplesInComments.GetHashCode()) * 17 + this.IndentLevel.GetHashCode()) * 17 + this.IndentSize.GetHashCode()) * 17 + this.PandasModuleAlias.GetHashCode();
		}

		// Token: 0x06009E63 RID: 40547 RVA: 0x0021911C File Offset: 0x0021731C
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			if (other != null)
			{
				PandasTranslationConstraint pandasTranslationConstraint = other as PandasTranslationConstraint;
				if (pandasTranslationConstraint != null && this.DataFrameName == pandasTranslationConstraint.DataFrameName && this.InputColumnName == pandasTranslationConstraint.InputColumnName && this.OutputColumnPrefix == pandasTranslationConstraint.OutputColumnPrefix)
				{
					int? newColumnsIndex = this.NewColumnsIndex;
					int? newColumnsIndex2 = pandasTranslationConstraint.NewColumnsIndex;
					if (((newColumnsIndex.GetValueOrDefault() == newColumnsIndex2.GetValueOrDefault()) & (newColumnsIndex != null == (newColumnsIndex2 != null))) && this.MaximumExamplesInComments == pandasTranslationConstraint.MaximumExamplesInComments && this.IndentLevel == pandasTranslationConstraint.IndentLevel && this.IndentSize == pandasTranslationConstraint.IndentSize)
					{
						return this.PandasModuleAlias == pandasTranslationConstraint.PandasModuleAlias;
					}
				}
			}
			return false;
		}

		// Token: 0x06009E64 RID: 40548 RVA: 0x002191F0 File Offset: 0x002173F0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataFrameName=",
				this.DataFrameName,
				" InputColumnName=",
				this.InputColumnName,
				" OutputColumnPrefix=",
				this.OutputColumnPrefix,
				string.Format(" NewColumnIndex={0}", this.NewColumnsIndex),
				string.Format(" IndentLevel={0}", this.IndentLevel),
				string.Format(" IndentSize={0}", this.IndentSize),
				" PandasModuleAlias=",
				this.PandasModuleAlias
			});
		}

		// Token: 0x04004017 RID: 16407
		public string OutputColumnPrefix = "col";
	}
}
