using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep
{
	// Token: 0x020000F8 RID: 248
	public class KthLabelStep : TreePathStep, IEquatable<KthLabelStep>
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00012E1A File Offset: 0x0001101A
		public KthLabelStep(string label, int k)
		{
			if (k == 0)
			{
				throw new InvalidOperationException("Expect non-zero index!");
			}
			this.Label = label;
			this.K = k;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00012E3E File Offset: 0x0001103E
		public string Label { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00012E46 File Offset: 0x00011046
		public int K { get; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00012E4E File Offset: 0x0001104E
		public override double Score
		{
			get
			{
				return (double)(-(double)this.K);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012E58 File Offset: 0x00011058
		public bool Equals(KthLabelStep other)
		{
			return other != null && (this == other || (this.Label == other.Label && this.K == other.K));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00012E88 File Offset: 0x00011088
		internal override string Serialize()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0},{1}]", new object[] { this.Label, this.K }));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00012EB8 File Offset: 0x000110B8
		public override Node Find(Node node)
		{
			int num = 0;
			Node[] children = node.Children;
			int num2 = children.Length;
			if (num2 == 0)
			{
				return null;
			}
			if (this.K > 0)
			{
				for (int i = 0; i < num2; i++)
				{
					if (children[i].Label == this.Label)
					{
						num++;
						if (num == this.K)
						{
							return children[i];
						}
					}
				}
			}
			if (this.K < 0)
			{
				int num3 = this.K * -1;
				for (int j = num2 - 1; j >= 0; j--)
				{
					if (children[j].Label == this.Label)
					{
						num++;
						if (num == num3)
						{
							return children[j];
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00012E88 File Offset: 0x00011088
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("[{0},{1}]", new object[] { this.Label, this.K }));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00012F5C File Offset: 0x0001115C
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((KthLabelStep)obj)));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00012F8A File Offset: 0x0001118A
		public override int GetHashCode()
		{
			return this.Label.GetHashCode() * this.K;
		}
	}
}
