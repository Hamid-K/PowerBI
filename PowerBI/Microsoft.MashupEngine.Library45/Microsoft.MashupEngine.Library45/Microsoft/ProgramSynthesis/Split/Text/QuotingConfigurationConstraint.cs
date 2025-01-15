using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F8 RID: 4856
	public class QuotingConfigurationConstraint : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06009257 RID: 37463 RVA: 0x001EC782 File Offset: 0x001EA982
		public QuotingConfigurationConstraint(QuotingConfiguration conf)
		{
			this.Configuration = conf;
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x001EC791 File Offset: 0x001EA991
		public void SetOptions(Witnesses.Options options)
		{
			if (options.ProvidedQuotingConfigurations == null)
			{
				options.ProvidedQuotingConfigurations = new List<QuotingConfiguration>();
			}
			options.ProvidedQuotingConfigurations.Add(this.Configuration);
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06009259 RID: 37465 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool IsSoft
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x0600925A RID: 37466 RVA: 0x001EC7B7 File Offset: 0x001EA9B7
		public QuotingConfiguration Configuration { get; }

		// Token: 0x0600925B RID: 37467 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, SplitCell[]> _)
		{
			return true;
		}

		// Token: 0x0600925C RID: 37468 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> _)
		{
			return false;
		}

		// Token: 0x0600925D RID: 37469 RVA: 0x001EC7C0 File Offset: 0x001EA9C0
		public override int GetHashCode()
		{
			return this.Configuration.GetHashCode();
		}

		// Token: 0x0600925E RID: 37470 RVA: 0x001EC7E4 File Offset: 0x001EA9E4
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			QuotingConfigurationConstraint quotingConfigurationConstraint = other as QuotingConfigurationConstraint;
			return quotingConfigurationConstraint != null && this.Configuration.Equals(quotingConfigurationConstraint.Configuration);
		}

		// Token: 0x0600925F RID: 37471 RVA: 0x001EC824 File Offset: 0x001EAA24
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			Constraint<StringRegion, SplitCell[]> constraint = other as Constraint<StringRegion, SplitCell[]>;
			return constraint != null && this.Equals(constraint);
		}
	}
}
