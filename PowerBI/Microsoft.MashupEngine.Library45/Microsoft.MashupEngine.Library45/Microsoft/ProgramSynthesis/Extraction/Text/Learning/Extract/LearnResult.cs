using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract
{
	// Token: 0x02000F8C RID: 3980
	internal class LearnResult<T> : IComparable<LearnResult<T>> where T : IProgramNodeBuilder
	{
		// Token: 0x06006E21 RID: 28193 RVA: 0x00167BC2 File Offset: 0x00165DC2
		public LearnResult(ProgramSetBuilder<T> programSet, double nullRatio = 0.0, double nullOrWhitespaceRatio = 0.0, bool needTrim = false)
		{
			this.ProgramSet = programSet;
			this.NullRatio = nullRatio;
			this.NullOrWhitespaceRatio = nullOrWhitespaceRatio;
			this.NeedTrim = needTrim;
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06006E22 RID: 28194 RVA: 0x00167BE7 File Offset: 0x00165DE7
		public ProgramSetBuilder<T> ProgramSet { get; }

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06006E23 RID: 28195 RVA: 0x00167BEF File Offset: 0x00165DEF
		public bool NeedTrim { get; }

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06006E24 RID: 28196 RVA: 0x00167BF7 File Offset: 0x00165DF7
		public double NullRatio { get; }

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06006E25 RID: 28197 RVA: 0x00167BFF File Offset: 0x00165DFF
		public double NullOrWhitespaceRatio { get; }

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06006E26 RID: 28198 RVA: 0x00167C07 File Offset: 0x00165E07
		public bool HasNullOrWhiteSpace
		{
			get
			{
				return this.NullRatio > 0.0 || this.NullOrWhitespaceRatio > 0.0;
			}
		}

		// Token: 0x06006E27 RID: 28199 RVA: 0x00167C30 File Offset: 0x00165E30
		internal static LearnResult<T> BuildLearnResult(ProgramSetBuilder<T> programSet, ExtractExample example, Func<StringRegion, StringRegion> transform)
		{
			int num = example.AdditionalInputs.Count + example.Examples.Count;
			int num2 = 0;
			int num3 = 0;
			foreach (StringRegion stringRegion in example.AdditionalInputs)
			{
				StringRegion stringRegion2 = transform(stringRegion);
				if (stringRegion2 == null)
				{
					num2++;
					num3++;
				}
				else if (stringRegion2.IsNullOrWhiteSpace())
				{
					num3++;
				}
			}
			return new LearnResult<T>(programSet, (double)num2 / (double)num, (double)num3 / (double)num, false);
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x00167CD0 File Offset: 0x00165ED0
		public static bool operator >(LearnResult<T> left, LearnResult<T> right)
		{
			return LearnResult<T>.CompareTo(left, right) > 0;
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x00167CDC File Offset: 0x00165EDC
		public static bool operator <(LearnResult<T> left, LearnResult<T> right)
		{
			return LearnResult<T>.CompareTo(left, right) < 0;
		}

		// Token: 0x06006E2A RID: 28202 RVA: 0x00167CE8 File Offset: 0x00165EE8
		public int CompareTo(LearnResult<T> other)
		{
			return LearnResult<T>.CompareTo(this, other);
		}

		// Token: 0x06006E2B RID: 28203 RVA: 0x00167CF4 File Offset: 0x00165EF4
		private static int CompareTo(LearnResult<T> left, LearnResult<T> right)
		{
			if (left == right)
			{
				return 0;
			}
			if (right == null)
			{
				return 1;
			}
			if (left == null)
			{
				return -1;
			}
			int num = LearnResult<T>.<CompareTo>g__Compare|20_0(left.NullRatio, right.NullRatio);
			if (num != 0)
			{
				return num;
			}
			return LearnResult<T>.<CompareTo>g__Compare|20_0(left.NullOrWhitespaceRatio, right.NullOrWhitespaceRatio);
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x00167D3C File Offset: 0x00165F3C
		[CompilerGenerated]
		internal static int <CompareTo>g__Compare|20_0(double l, double r)
		{
			double num = l - r;
			if (num < -0.05)
			{
				return 1;
			}
			if (num > 0.05)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04002FFB RID: 12283
		private const double Epsilon = 0.05;
	}
}
