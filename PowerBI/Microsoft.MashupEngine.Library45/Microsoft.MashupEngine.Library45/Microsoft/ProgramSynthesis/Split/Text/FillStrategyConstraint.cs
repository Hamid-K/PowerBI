using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F0 RID: 4848
	public class FillStrategyConstraint : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06009218 RID: 37400 RVA: 0x001EBFC8 File Offset: 0x001EA1C8
		public FillStrategyConstraint(FillStrategy strategy)
		{
			this.Strategy = strategy;
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06009219 RID: 37401 RVA: 0x001EBFD7 File Offset: 0x001EA1D7
		public FillStrategy Strategy { get; }

		// Token: 0x0600921A RID: 37402 RVA: 0x001EBFDF File Offset: 0x001EA1DF
		public void SetOptions(Witnesses.Options options)
		{
			options.FillStrategy = new FillStrategy?(this.Strategy);
		}

		// Token: 0x0600921B RID: 37403 RVA: 0x001EBFF4 File Offset: 0x001EA1F4
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			SplitRegion splitRegion;
			return Language.Build.Node.IsRule.SplitRegion(program.ProgramNode, out splitRegion) && splitRegion.fillStrategy.Value == this.Strategy;
		}

		// Token: 0x0600921C RID: 37404 RVA: 0x001EC038 File Offset: 0x001EA238
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			FillStrategyConstraint fillStrategyConstraint = other as FillStrategyConstraint;
			return fillStrategyConstraint != null && fillStrategyConstraint.Strategy != this.Strategy;
		}

		// Token: 0x0600921D RID: 37405 RVA: 0x001EC068 File Offset: 0x001EA268
		public bool Equals(FillStrategyConstraint other)
		{
			return other != null && (this == other || this.Strategy == other.Strategy);
		}

		// Token: 0x0600921E RID: 37406 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600921F RID: 37407 RVA: 0x001EC083 File Offset: 0x001EA283
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FillStrategyConstraint)obj)));
		}

		// Token: 0x06009220 RID: 37408 RVA: 0x001EC0B4 File Offset: 0x001EA2B4
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int num = (int)(391 + this.Strategy);
			this._hashCode = new int?(num);
			return num;
		}

		// Token: 0x06009221 RID: 37409 RVA: 0x001EC0F4 File Offset: 0x001EA2F4
		public override string ToString()
		{
			return string.Format("{0}({1})", "FillStrategyConstraint", this.Strategy);
		}

		// Token: 0x04003BF9 RID: 15353
		private int? _hashCode;
	}
}
