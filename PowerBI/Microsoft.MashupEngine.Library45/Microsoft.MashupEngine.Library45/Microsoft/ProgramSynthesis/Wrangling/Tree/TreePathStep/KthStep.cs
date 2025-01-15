using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep
{
	// Token: 0x020000F9 RID: 249
	public class KthStep : TreePathStep, IEquatable<KthStep>
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x00012F9E File Offset: 0x0001119E
		public KthStep(int k)
		{
			if (k == 0)
			{
				throw new InvalidOperationException("Expect non-zero index!");
			}
			this.K = k;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00012FBB File Offset: 0x000111BB
		public int K { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00012FC3 File Offset: 0x000111C3
		public override double Score
		{
			get
			{
				return (double)(-(double)this.K + 1);
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00012FCF File Offset: 0x000111CF
		public bool Equals(KthStep other)
		{
			return other != null && (this == other || this.K == other.K);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00012FEA File Offset: 0x000111EA
		internal override string Serialize()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { this.K }));
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00013010 File Offset: 0x00011210
		public override Node Find(Node node)
		{
			if (node == null)
			{
				return null;
			}
			if (Math.Abs(this.K) > node.Children.Length)
			{
				return null;
			}
			if (this.K > 0)
			{
				return node.Children[this.K - 1];
			}
			return node.Children[node.Children.Length + this.K];
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00012FEA File Offset: 0x000111EA
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { this.K }));
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00013068 File Offset: 0x00011268
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((KthStep)obj)));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00013096 File Offset: 0x00011296
		public override int GetHashCode()
		{
			return this.K;
		}
	}
}
