using System;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009F0 RID: 2544
	public class FillStrategy : Constraint<StringRegion, ITable<StringRegion>>, IOptionConstraint<Options>
	{
		// Token: 0x06003D69 RID: 15721 RVA: 0x000C04FB File Offset: 0x000BE6FB
		public FillStrategy(FillStrategy strategy)
		{
			this.Strategy = strategy;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x000C050A File Offset: 0x000BE70A
		public FillStrategy Strategy { get; }

		// Token: 0x06003D6B RID: 15723 RVA: 0x000C0512 File Offset: 0x000BE712
		public void SetOptions(Options options)
		{
			options.TextConstraints.Add(new FillStrategyConstraint(this.Strategy));
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			return true;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000C052C File Offset: 0x000BE72C
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			FillStrategy fillStrategy = other as FillStrategy;
			return fillStrategy != null && fillStrategy.Strategy != this.Strategy;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000C055C File Offset: 0x000BE75C
		public bool Equals(FillStrategy other)
		{
			return other != null && (this == other || this.Strategy == other.Strategy);
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x000C0577 File Offset: 0x000BE777
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((FillStrategy)obj)));
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000C05A5 File Offset: 0x000BE7A5
		public override int GetHashCode()
		{
			return (int)this.Strategy;
		}
	}
}
