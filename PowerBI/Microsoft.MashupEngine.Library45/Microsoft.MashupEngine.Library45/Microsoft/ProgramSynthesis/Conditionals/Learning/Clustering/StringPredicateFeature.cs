using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Conditionals.Build;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A71 RID: 2673
	[DebuggerDisplay("{Negated ? \"! \": \"\"}{Type}, {ContainedString}, {K}, {Score}")]
	internal class StringPredicateFeature : PredicateFeature, IEquatable<StringPredicateFeature>
	{
		// Token: 0x0600424B RID: 16971 RVA: 0x000CF6D5 File Offset: 0x000CD8D5
		public StringPredicateFeature(string containedString, PredicateType type, bool negated = false, int k = 1)
			: base(type, negated, k)
		{
			if (containedString == null)
			{
				throw new ArgumentNullException("containedString");
			}
			this.ContainedString = containedString;
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x000CF6F7 File Offset: 0x000CD8F7
		public string ContainedString { get; }

		// Token: 0x0600424D RID: 16973 RVA: 0x000CF700 File Offset: 0x000CD900
		public bool Equals(StringPredicateFeature other)
		{
			return other != null && (this == other || (base.Type == other.Type && base.Negated == other.Negated && base.K == other.K && object.Equals(this.ContainedString, other.ContainedString)));
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000CF755 File Offset: 0x000CD955
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StringPredicateFeature);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x000CF764 File Offset: 0x000CD964
		public override pred GenerateProgramNode(GrammarBuilders build)
		{
			PredicateType type = base.Type;
			match match;
			if (type != PredicateType.StartsWithString)
			{
				if (type != PredicateType.EndsWithString)
				{
					if (type != PredicateType.ContainsString)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported feature {0}!", new object[] { base.Type })));
					}
					match = build.Node.Rule.ContainsString(build.Node.Variable.s, build.Node.Rule.str(this.ContainedString), build.Node.Rule.k(base.K));
				}
				else
				{
					match = build.Node.Rule.EndsWithString(build.Node.Variable.s, build.Node.Rule.str(this.ContainedString));
				}
			}
			else
			{
				match = build.Node.Rule.StartsWithString(build.Node.Variable.s, build.Node.Rule.str(this.ContainedString));
			}
			match match2 = match;
			if (!base.Negated)
			{
				return build.Node.UnnamedConversion.pred_match(match2);
			}
			return build.Node.Rule.Not(match2);
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x000CF8A4 File Offset: 0x000CDAA4
		public override int GetHashCode()
		{
			return (base.GetHashCode() * 4451) ^ this.ContainedString.GetHashCode();
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x000CF8BE File Offset: 0x000CDABE
		public override PredicateFeature Negate()
		{
			return new StringPredicateFeature(this.ContainedString, base.Type, !base.Negated, base.K);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x000CF8E0 File Offset: 0x000CDAE0
		protected override double CalculateScore()
		{
			PredicateType type = base.Type;
			double num;
			if (type != PredicateType.StartsWithString)
			{
				if (type != PredicateType.EndsWithString)
				{
					if (type != PredicateType.ContainsString)
					{
						throw new NotImplementedException(base.Type.ToString());
					}
					num = (double)(-5 - base.K) - 1.0 / (double)this.ContainedString.Length;
				}
				else
				{
					num = -4.0 - 1.0 / (double)this.ContainedString.Length;
				}
			}
			else
			{
				num = -4.0 - 1.0 / (double)this.ContainedString.Length;
			}
			return num;
		}
	}
}
