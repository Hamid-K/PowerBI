using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000131 RID: 305
	public struct SignificantInput<TInput> : IEquatable<SignificantInput<TInput>>
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001619B File Offset: 0x0001439B
		public SignificantInput(double confidence, TInput input, Optional<IReadOnlyList<TInput>> cluster)
		{
			this.Confidence = confidence;
			this.Input = input;
			this.Cluster = cluster;
			this._hashCode = null;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000161BE File Offset: 0x000143BE
		public readonly double Confidence { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000161C6 File Offset: 0x000143C6
		public readonly TInput Input { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x000161CE File Offset: 0x000143CE
		public readonly Optional<IReadOnlyList<TInput>> Cluster { get; }

		// Token: 0x060006D9 RID: 1753 RVA: 0x000161D8 File Offset: 0x000143D8
		public bool Equals(SignificantInput<TInput> other)
		{
			return this.Confidence.Equals(other.Confidence) && ValueEquality.Comparer.Equals(this.Input, other.Input) && this.Cluster.HasValue == other.Cluster.HasValue && (!this.Cluster.HasValue || this.Cluster.Value.SequenceEqual(other.Cluster.Value));
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00016274 File Offset: 0x00014474
		public override bool Equals(object obj)
		{
			return obj != null && obj is SignificantInput<TInput> && this.Equals((SignificantInput<TInput>)obj);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00016294 File Offset: 0x00014494
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				int num = (this.Cluster.HasValue ? this.Cluster.Value.OrderDependentHashCode(ValueEquality<TInput>.Instance) : 0);
				int hashCode = ValueEquality.Comparer.GetHashCode(this.Input);
				int hashCode2 = this.Confidence.GetHashCode();
				this._hashCode = new int?((hashCode2 * 397) ^ (num * 98807) ^ hashCode);
			}
			return this._hashCode.Value;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00016327 File Offset: 0x00014527
		public static bool operator ==(SignificantInput<TInput> left, SignificantInput<TInput> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00016331 File Offset: 0x00014531
		public static bool operator !=(SignificantInput<TInput> left, SignificantInput<TInput> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400030D RID: 781
		private int? _hashCode;
	}
}
