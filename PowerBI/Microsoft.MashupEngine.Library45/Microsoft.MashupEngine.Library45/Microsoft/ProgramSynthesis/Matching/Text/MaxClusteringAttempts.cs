using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011AA RID: 4522
	public class MaxClusteringAttempts<TInput, TOutput> : Constraint<TInput, TOutput>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x060086A2 RID: 34466 RVA: 0x001C43B6 File Offset: 0x001C25B6
		public int MaxAttempts { get; }

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x060086A3 RID: 34467 RVA: 0x001C43BE File Offset: 0x001C25BE
		public int MaxAttemptsAfterFailure { get; }

		// Token: 0x060086A4 RID: 34468 RVA: 0x001C43C8 File Offset: 0x001C25C8
		public MaxClusteringAttempts(int maxAttempts, int maxAttemptsAfterFailure)
		{
			if (maxAttempts <= 0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid range for max attempts: 0 <= {0}", new object[] { maxAttempts })));
			}
			if (maxAttemptsAfterFailure <= 0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid range for max attempts after failure: 0 <= {0}", new object[] { maxAttemptsAfterFailure })));
			}
			if (maxAttemptsAfterFailure > maxAttempts)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Max attempts must be greater than Max attempts after failure: {0} > {1}", new object[] { maxAttempts, maxAttemptsAfterFailure })));
			}
			this.MaxAttempts = maxAttempts;
			this.MaxAttemptsAfterFailure = maxAttemptsAfterFailure;
		}

		// Token: 0x060086A5 RID: 34469 RVA: 0x001C446A File Offset: 0x001C266A
		public override bool ConflictsWith(Constraint<TInput, TOutput> other)
		{
			return other is MaxClusteringAttempts<TInput, TOutput> && !this.Equals(other);
		}

		// Token: 0x060086A6 RID: 34470 RVA: 0x001C4480 File Offset: 0x001C2680
		public override bool Equals(Constraint<TInput, TOutput> other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && this.Equals((MaxClusteringAttempts<TInput, TOutput>)other)));
		}

		// Token: 0x060086A7 RID: 34471 RVA: 0x001C44AE File Offset: 0x001C26AE
		public bool Equals(MaxClusteringAttempts<TInput, TOutput> other)
		{
			return other != null && (this == other || (this.MaxAttempts == other.MaxAttempts && this.MaxAttemptsAfterFailure == other.MaxAttemptsAfterFailure));
		}

		// Token: 0x060086A8 RID: 34472 RVA: 0x001C44DC File Offset: 0x001C26DC
		public override int GetHashCode()
		{
			return this.MaxAttempts.GetHashCode();
		}

		// Token: 0x060086A9 RID: 34473 RVA: 0x001C44F7 File Offset: 0x001C26F7
		public void SetOptions(Witnesses.Options options)
		{
			options.MaxAttempts = new int?(this.MaxAttempts);
			options.MaxAttemptsAfterFailure = new int?(this.MaxAttemptsAfterFailure);
		}

		// Token: 0x060086AA RID: 34474 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<TInput, TOutput> program)
		{
			return true;
		}
	}
}
