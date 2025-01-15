using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Conditionals.Build;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A6F RID: 2671
	[DebuggerDisplay("{Negated ? \"! \": \"\"}{Type}, {Regex}, {K}, {Score}")]
	internal class RegexPredicateFeature : PredicateFeature, IEquatable<RegexPredicateFeature>
	{
		// Token: 0x0600423F RID: 16959 RVA: 0x000CF321 File Offset: 0x000CD521
		public RegexPredicateFeature(RegularExpression regex, PredicateType type, bool negated = false, int k = 1)
			: base(type, negated, k)
		{
			this.Regex = regex;
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x000CF334 File Offset: 0x000CD534
		public RegularExpression Regex { get; }

		// Token: 0x06004241 RID: 16961 RVA: 0x000CF33C File Offset: 0x000CD53C
		public bool Equals(RegexPredicateFeature other)
		{
			return other != null && (this == other || (base.Type == other.Type && base.Negated == other.Negated && base.K == other.K && object.Equals(this.Regex, other.Regex)));
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000CF391 File Offset: 0x000CD591
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RegexPredicateFeature);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000CF3A0 File Offset: 0x000CD5A0
		public override pred GenerateProgramNode(GrammarBuilders build)
		{
			match match;
			switch (base.Type)
			{
			case PredicateType.Matches:
				match = build.Node.Rule.Matches(build.Node.Variable.s, build.Node.Rule.r(this.Regex));
				break;
			case PredicateType.StartsWith:
				match = build.Node.Rule.StartsWith(build.Node.Variable.s, build.Node.Rule.r(this.Regex));
				break;
			case PredicateType.Contains:
				match = build.Node.Rule.Contains(build.Node.Variable.s, build.Node.Rule.r(this.Regex), build.Node.Rule.k(base.K));
				break;
			case PredicateType.EndsWith:
				match = build.Node.Rule.EndsWith(build.Node.Variable.s, build.Node.Rule.r(this.Regex));
				break;
			default:
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported feature {0}!", new object[] { base.Type })));
			}
			match match2 = match;
			if (!base.Negated)
			{
				return build.Node.UnnamedConversion.pred_match(match2);
			}
			return build.Node.Rule.Not(match2);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000CF528 File Offset: 0x000CD728
		public override int GetHashCode()
		{
			return (base.GetHashCode() * 8329) ^ this.Regex.GetHashCode();
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000CF542 File Offset: 0x000CD742
		public override PredicateFeature Negate()
		{
			return new RegexPredicateFeature(this.Regex, base.Type, !base.Negated, base.K);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000CF564 File Offset: 0x000CD764
		protected override double CalculateScore()
		{
			double num;
			switch (base.Type)
			{
			case PredicateType.Matches:
				num = -5.3 + (RegexPredicateFeature.<CalculateScore>g__isAllConstant|9_0(this.Regex) ? 0.1 : ((double)this.Regex.Score / 10000.0));
				break;
			case PredicateType.StartsWith:
				num = -5.5 + (RegexPredicateFeature.<CalculateScore>g__isAllConstant|9_0(this.Regex) ? 0.1 : (2.0 * (double)this.Regex.Score / 10000.0));
				break;
			case PredicateType.Contains:
				num = (double)(-(double)(base.K + 3) * this.Regex.Score);
				break;
			case PredicateType.EndsWith:
				num = -5.7 + (RegexPredicateFeature.<CalculateScore>g__isAllConstant|9_0(this.Regex) ? 0.1 : (3.0 * (double)this.Regex.Score / 10000.0));
				break;
			default:
				throw new NotImplementedException(base.Type.ToString());
			}
			return num;
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000CF692 File Offset: 0x000CD892
		[CompilerGenerated]
		internal static bool <CalculateScore>g__isAllConstant|9_0(RegularExpression regex)
		{
			return regex.Tokens.All((Token t) => t is StringToken);
		}
	}
}
