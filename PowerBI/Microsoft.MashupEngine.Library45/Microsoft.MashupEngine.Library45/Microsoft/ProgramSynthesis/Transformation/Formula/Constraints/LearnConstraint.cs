using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019D3 RID: 6611
	public class LearnConstraint : OperatorLearnConstraint, IOptionConstraint<LearnOptions>, IUniqueConstraint<LearnConstraint>
	{
		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x0600D7B7 RID: 55223 RVA: 0x002DD5C2 File Offset: 0x002DB7C2
		// (set) Token: 0x0600D7B8 RID: 55224 RVA: 0x002DD5CA File Offset: 0x002DB7CA
		public IReadOnlyList<string> ColumnNamePriority { get; set; }

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x0600D7B9 RID: 55225 RVA: 0x002DD5D3 File Offset: 0x002DB7D3
		// (set) Token: 0x0600D7BA RID: 55226 RVA: 0x002DD5DB File Offset: 0x002DB7DB
		public ConditionalLearnConstraint Conditional { get; set; }

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x0600D7BB RID: 55227 RVA: 0x002DD5E4 File Offset: 0x002DB7E4
		// (set) Token: 0x0600D7BC RID: 55228 RVA: 0x002DD5EC File Offset: 0x002DB7EC
		public int ConditionalBranchMinExampleCount { get; set; } = 1;

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x0600D7BD RID: 55229 RVA: 0x002DD5F5 File Offset: 0x002DB7F5
		// (set) Token: 0x0600D7BE RID: 55230 RVA: 0x002DD5FD File Offset: 0x002DB7FD
		public int ConditionalMaxBranches { get; set; } = 5;

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x0600D7BF RID: 55231 RVA: 0x002DD606 File Offset: 0x002DB806
		// (set) Token: 0x0600D7C0 RID: 55232 RVA: 0x002DD60E File Offset: 0x002DB80E
		public IReadOnlyList<CultureInfo> DataCultures { get; set; } = new CultureInfo[]
		{
			new CultureInfo("en-US"),
			new CultureInfo("en-GB")
		};

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x0600D7C1 RID: 55233 RVA: 0x002DD617 File Offset: 0x002DB817
		// (set) Token: 0x0600D7C2 RID: 55234 RVA: 0x002DD61F File Offset: 0x002DB81F
		public bool EnableConditional { get; set; } = true;

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x0600D7C3 RID: 55235 RVA: 0x002DD628 File Offset: 0x002DB828
		// (set) Token: 0x0600D7C4 RID: 55236 RVA: 0x002DD630 File Offset: 0x002DB830
		public int FromNumbersColumnLimit { get; set; } = 8;

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x0600D7C5 RID: 55237 RVA: 0x002DD639 File Offset: 0x002DB839
		// (set) Token: 0x0600D7C6 RID: 55238 RVA: 0x002DD641 File Offset: 0x002DB841
		public LearnConfidenceBehavior LearnConfidence { get; set; }

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x0600D7C7 RID: 55239 RVA: 0x002DD64A File Offset: 0x002DB84A
		// (set) Token: 0x0600D7C8 RID: 55240 RVA: 0x002DD652 File Offset: 0x002DB852
		public double MinLearnConfidence { get; set; } = 0.7;

		// Token: 0x0600D7C9 RID: 55241 RVA: 0x002DD65B File Offset: 0x002DB85B
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return ((other != null) ? other.GetType() : null) == base.GetType();
		}

		// Token: 0x0600D7CA RID: 55242 RVA: 0x002DD674 File Offset: 0x002DB874
		public override void SetOptions(LearnOptions options)
		{
			base.SetOptions(options);
			options.ColumnNamePriority = this.ColumnNamePriority;
			options.DataCultures = this.DataCultures;
			options.FromNumbersColumnLimit = this.FromNumbersColumnLimit;
			options.EnableConditional = this.EnableConditional;
			options.ConditionalBranchMinExampleCount = this.ConditionalBranchMinExampleCount;
			options.ConditionalMaxBranches = this.ConditionalMaxBranches;
			options.LearnConfidence = this.LearnConfidence;
			options.MinLearnConfidence = this.MinLearnConfidence;
		}

		// Token: 0x0600D7CB RID: 55243 RVA: 0x002DD6E8 File Offset: 0x002DB8E8
		internal override string ToEqualString()
		{
			string[] array = new string[12];
			array[0] = base.ToEqualString();
			array[1] = " ColumnNamePriority=";
			array[2] = this.ColumnNamePriority.ToJoinString(",");
			array[3] = "; DataCultures=";
			int num = 4;
			IReadOnlyList<CultureInfo> dataCultures = this.DataCultures;
			IEnumerable<string> enumerable;
			if (dataCultures == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = dataCultures.Select((CultureInfo c) => c.Name);
			}
			array[num] = enumerable.ToJoinString(",");
			array[5] = ";";
			array[6] = string.Format(" {0}={1};", "FromNumbersColumnLimit", this.FromNumbersColumnLimit);
			array[7] = string.Format(" {0}={1};", "EnableConditional", this.EnableConditional);
			array[8] = string.Format(" {0}={1};", "ConditionalMaxBranches", this.ConditionalMaxBranches);
			array[9] = string.Format(" {0}={1};", "ConditionalBranchMinExampleCount", this.ConditionalBranchMinExampleCount);
			array[10] = string.Format(" {0}={1};", "LearnConfidence", this.LearnConfidence);
			array[11] = string.Format(" {0}={1};", "MinLearnConfidence", this.MinLearnConfidence);
			return string.Concat(array);
		}
	}
}
