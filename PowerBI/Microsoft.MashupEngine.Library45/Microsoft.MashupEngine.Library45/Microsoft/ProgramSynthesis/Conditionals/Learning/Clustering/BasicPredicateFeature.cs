using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Conditionals.Build;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A72 RID: 2674
	[DebuggerDisplay("{Negated ? \"! \": \"\"}{Type}, {K}, {Score}")]
	internal class BasicPredicateFeature : PredicateFeature, IEquatable<BasicPredicateFeature>
	{
		// Token: 0x06004253 RID: 16979 RVA: 0x000CF988 File Offset: 0x000CDB88
		public BasicPredicateFeature(PredicateType type, bool negated = false, int k = 1)
			: base(type, negated, k)
		{
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x000CF993 File Offset: 0x000CDB93
		public bool Equals(BasicPredicateFeature other)
		{
			return other != null && (this == other || (base.Type == other.Type && base.Negated == other.Negated && base.K == other.K));
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x000CF9CC File Offset: 0x000CDBCC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BasicPredicateFeature);
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000CF9DC File Offset: 0x000CDBDC
		public override pred GenerateProgramNode(GrammarBuilders build)
		{
			match match;
			switch (base.Type)
			{
			case PredicateType.IsNullOrWhiteSpace:
				match = build.Node.Rule.IsNullOrWhiteSpace(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.IsNull:
				match = build.Node.Rule.IsNull(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.IsWhiteSpace:
				match = build.Node.Rule.IsWhiteSpace(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.StartsWithDigit:
				match = build.Node.Rule.StartsWithDigit(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.StartsWithLetter:
				match = build.Node.Rule.StartsWithLetter(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.EndsWithDigit:
				match = build.Node.Rule.EndsWithDigit(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.EndsWithLetter:
				match = build.Node.Rule.StartsWithLetter(build.Node.Variable.s);
				goto IL_018E;
			case PredicateType.True:
				match = build.Node.Rule.True();
				goto IL_018E;
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported feature {0}!", new object[] { base.Type })));
			IL_018E:
			match match2 = match;
			if (!base.Negated)
			{
				return build.Node.UnnamedConversion.pred_match(match2);
			}
			return build.Node.Rule.Not(match2);
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000CFBA4 File Offset: 0x000CDDA4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000CFBAC File Offset: 0x000CDDAC
		public override PredicateFeature Negate()
		{
			return new BasicPredicateFeature(base.Type, !base.Negated, base.K);
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000CFBC8 File Offset: 0x000CDDC8
		protected override double CalculateScore()
		{
			int num;
			switch (base.Type)
			{
			case PredicateType.IsNullOrWhiteSpace:
				num = -1;
				goto IL_008B;
			case PredicateType.IsNull:
				num = -2;
				goto IL_008B;
			case PredicateType.IsWhiteSpace:
				num = -3;
				goto IL_008B;
			case PredicateType.StartsWithDigit:
				num = -6;
				goto IL_008B;
			case PredicateType.StartsWithLetter:
				num = -6;
				goto IL_008B;
			case PredicateType.EndsWithDigit:
				num = -7;
				goto IL_008B;
			case PredicateType.EndsWithLetter:
				num = -7;
				goto IL_008B;
			case PredicateType.True:
				num = 0;
				goto IL_008B;
			}
			throw new NotImplementedException(base.Type.ToString());
			IL_008B:
			return (double)num;
		}
	}
}
