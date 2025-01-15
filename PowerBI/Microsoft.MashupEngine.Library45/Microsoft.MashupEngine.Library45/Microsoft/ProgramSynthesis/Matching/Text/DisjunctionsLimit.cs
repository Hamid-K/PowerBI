using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A9 RID: 4521
	public class DisjunctionsLimit<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06008695 RID: 34453 RVA: 0x001C40B0 File Offset: 0x001C22B0
		public DisjunctionsLimit(uint? minDisjuncts, uint? maxDisjuncts)
		{
			uint? num = minDisjuncts;
			uint num2 = 1U;
			if (!((num.GetValueOrDefault() < num2) & (num != null)))
			{
				num = minDisjuncts;
				uint? num3 = maxDisjuncts;
				if (!((num.GetValueOrDefault() > num3.GetValueOrDefault()) & ((num != null) & (num3 != null))))
				{
					num3 = maxDisjuncts;
					num2 = 1U;
					if (!((num3.GetValueOrDefault() < num2) & (num3 != null)))
					{
						this._hashCode = null;
						this.MinDisjuncts = minDisjuncts;
						this.MaxDisjuncts = maxDisjuncts;
						return;
					}
				}
			}
			throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid range for number of disjuncts: 1 <= min({0}) <= max({1}) must hold.", new object[] { minDisjuncts, maxDisjuncts })));
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06008696 RID: 34454 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool IsSoft
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06008697 RID: 34455 RVA: 0x001C4160 File Offset: 0x001C2360
		public static DisjunctionsLimit<TInput, TOutput> DefaultCategoricalLimit { get; } = new DisjunctionsLimit<TInput, TOutput>(new uint?(1U), new uint?(16U));

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06008698 RID: 34456 RVA: 0x001C4167 File Offset: 0x001C2367
		public static DisjunctionsLimit<TInput, TOutput> DefaultQuantitativeLimit { get; } = new DisjunctionsLimit<TInput, TOutput>(new uint?(1U), new uint?(6U));

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06008699 RID: 34457 RVA: 0x001C416E File Offset: 0x001C236E
		public uint? MinDisjuncts { get; }

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x0600869A RID: 34458 RVA: 0x001C4176 File Offset: 0x001C2376
		public uint? MaxDisjuncts { get; }

		// Token: 0x0600869B RID: 34459 RVA: 0x001C4180 File Offset: 0x001C2380
		public void SetOptions(Witnesses.Options options)
		{
			uint? num = this.MinDisjuncts;
			options.MinDisjuncts = ((num != null) ? num : options.MinDisjuncts);
			num = this.MaxDisjuncts;
			options.MaxDisjuncts = ((num != null) ? num : options.MaxDisjuncts);
		}

		// Token: 0x0600869C RID: 34460 RVA: 0x001C41CB File Offset: 0x001C23CB
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((DisjunctionsLimit<TInput, TOutput>)other)));
		}

		// Token: 0x0600869D RID: 34461 RVA: 0x001C41F9 File Offset: 0x001C23F9
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return other is DisjunctionsLimit<TInput, TOutput> && !this.Equals(other);
		}

		// Token: 0x0600869E RID: 34462 RVA: 0x001C4210 File Offset: 0x001C2410
		public override bool Valid(Program<TInput, TOutput> program)
		{
			uint num = Convert.ToUInt32(program.ProgramNode.GetFeatureValue<IEnumerable<ProgramNode>>(Learner.Instance.DisjunctsFeature, null).Count<ProgramNode>());
			if (this.MinDisjuncts != null)
			{
				uint? num2 = this.MinDisjuncts;
				uint num3 = num;
				if (!((num2.GetValueOrDefault() <= num3) & (num2 != null)))
				{
					return false;
				}
			}
			if (this.MaxDisjuncts != null)
			{
				uint num4 = num;
				uint? num2 = this.MaxDisjuncts;
				return (num4 <= num2.GetValueOrDefault()) & (num2 != null);
			}
			return true;
		}

		// Token: 0x0600869F RID: 34463 RVA: 0x001C42A0 File Offset: 0x001C24A0
		public bool Equals(DisjunctionsLimit<TInput, TOutput> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			uint? num = this.MaxDisjuncts;
			uint? num2 = other.MaxDisjuncts;
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				num2 = this.MinDisjuncts;
				num = other.MinDisjuncts;
				return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
			}
			return false;
		}

		// Token: 0x060086A0 RID: 34464 RVA: 0x001C431C File Offset: 0x001C251C
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int? num = (this._hashCode = new int?((6893533 * this.MinDisjuncts.GetHashCode()) ^ this.MaxDisjuncts.GetHashCode()));
			return num.Value;
		}

		// Token: 0x04003794 RID: 14228
		private int? _hashCode;
	}
}
