using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000276 RID: 630
	public class JoinProgramSet : ProgramSet, IJoinLanguage, ILanguage
	{
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00027E1B File Offset: 0x0002601B
		public JoinProgramSet(NonterminalRule rule, params ProgramSet[] parameterSpaces)
			: base(rule.Head)
		{
			this.Rule = rule;
			this.ParameterSpaces = parameterSpaces;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00027E37 File Offset: 0x00026037
		public ProgramSet[] ParameterSpaces { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00027E3F File Offset: 0x0002603F
		public NonterminalRule Rule { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x00027E47 File Offset: 0x00026047
		public override IEnumerable<ProgramNode> RealizedPrograms
		{
			get
			{
				IEnumerable<IEnumerable<ProgramNode>> enumerable = this.ParameterSpaces.Select((ProgramSet p) => p.RealizedPrograms).CartesianProduct<ProgramNode>();
				foreach (IEnumerable<ProgramNode> enumerable2 in enumerable)
				{
					yield return this.Rule.BuildASTNode(enumerable2.ToArray<ProgramNode>());
				}
				IEnumerator<IEnumerable<ProgramNode>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00027E57 File Offset: 0x00026057
		public override bool IsEmpty
		{
			get
			{
				return this.ParameterSpaces.Any((ProgramSet p) => p.IsEmpty);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00027E83 File Offset: 0x00026083
		protected override ProgramSet[] ChildProgramSets
		{
			get
			{
				return this.ParameterSpaces;
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00027E8C File Offset: 0x0002608C
		public override ProgramSet Intersect(ProgramSet other)
		{
			ProgramSet programSet = ProgramSet.Empty(base.Symbol);
			if (ProgramSet.IsNullOrEmpty(other))
			{
				return programSet;
			}
			if (base.Symbol != other.Symbol)
			{
				return programSet;
			}
			if (this == other)
			{
				return this;
			}
			if (other is DirectProgramSet)
			{
				return other.Intersect(this);
			}
			UnionProgramSet unionProgramSet = other as UnionProgramSet;
			if (unionProgramSet != null)
			{
				return unionProgramSet.UnionSpaces.Select(new Func<ProgramSet, ProgramSet>(this.Intersect)).NormalizedUnion() ?? programSet;
			}
			JoinProgramSet joinProgramSet = other as JoinProgramSet;
			if (joinProgramSet == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown ProgramSet type: {0}", new object[] { other.GetType() })), "other");
			}
			if (!this.Rule.Equals(joinProgramSet.Rule))
			{
				return programSet;
			}
			ProgramSet[] array = new ProgramSet[this.ParameterSpaces.Length];
			for (int i = 0; i < this.ParameterSpaces.Length; i++)
			{
				ProgramSet programSet2 = this.ParameterSpaces[i].Intersect(joinProgramSet.ParameterSpaces[i]);
				if (programSet2.IsEmpty)
				{
					return programSet;
				}
				array[i] = programSet2;
			}
			return new JoinProgramSet(this.Rule, array);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00027E83 File Offset: 0x00026083
		public IEnumerable<ILanguage> JoinedLanguages
		{
			get
			{
				return this.ParameterSpaces;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00027FAA File Offset: 0x000261AA
		public NonterminalRule LanguageRule
		{
			get
			{
				return this.Rule;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00027FB4 File Offset: 0x000261B4
		protected override IEnumerable<ProgramNode> CalculateTopK(IFeature feature, int k, FeatureCalculationContext context, LogListener logListener)
		{
			IEnumerable<ProgramNode> topKStream = this.Rule.GetTopKStream(this, feature, k, context, logListener);
			return base.TopK(topKStream, feature, k, context, logListener);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00027FE0 File Offset: 0x000261E0
		internal override ProgramSet FindSetDepthFirst(Predicate<ProgramSet> predicate, Predicate<ProgramSet> traverseChildren = null)
		{
			if (predicate(this))
			{
				return this;
			}
			if (traverseChildren != null && !traverseChildren(this))
			{
				return null;
			}
			return this.ParameterSpaces.Collect((ProgramSet c) => c.FindSetDepthFirst(predicate, traverseChildren)).FirstOrDefault<ProgramSet>();
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00028045 File Offset: 0x00026245
		protected override Dictionary<object, ProgramSet> ClusterOnInputImpl(State inputState)
		{
			return this.Rule.Cluster(this, inputState);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00028054 File Offset: 0x00026254
		public override XElement ToXML(Dictionary<object, int> identityCache = null, params IFeature[] featureCalculators)
		{
			return new XElement("Join", this.ParameterSpaces.Select((ProgramSet s, int i) => new XElement("Param", s.ToXML(identityCache, featureCalculators)).WithAttribute("name", this.Rule.Body[i]))).WithAttribute("rule", this.Rule).WithAttribute("size", base.Size);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000280C8 File Offset: 0x000262C8
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache)
		{
			return new XElement("Join", this.ParameterSpaces.Select((ProgramSet s) => s.InternedSerialize(identityCache))).WithAttribute("rule", this.Rule.Id);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00028120 File Offset: 0x00026320
		internal override ProgramSet FindChildSet(ProgramNode node, int indexInParent)
		{
			if (!this.Rule.Equals(node.GrammarRule))
			{
				return null;
			}
			if (this.ParameterSpaces.Length <= indexInParent)
			{
				return null;
			}
			for (int i = 0; i < this.ParameterSpaces.Length; i++)
			{
				if (i != indexInParent && !this.ParameterSpaces[i].Contains(node.Children[i]))
				{
					return null;
				}
			}
			return this.ParameterSpaces[indexInParent];
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00028188 File Offset: 0x00026388
		internal new static JoinProgramSet InternedDeserialize(XElement node, Grammar grammar, Dictionary<int, object> identityCache)
		{
			if (node.Name.LocalName != "Join")
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			ProgramSet[] array = (from c in node.Elements()
				select ProgramSet.InternedDeserialize(c, grammar, identityCache)).ToArray<ProgramSet>();
			XAttribute xattribute = node.Attribute("rule");
			string text = ((xattribute != null) ? xattribute.Value : null);
			NonterminalRule nonterminalRule;
			if (string.IsNullOrEmpty(text))
			{
				XAttribute xattribute2 = node.Attribute("symbol");
				string text2 = ((xattribute2 != null) ? xattribute2.Value : null);
				if (text2 == null)
				{
					throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
				}
				Symbol symbol = grammar.Symbol(text2);
				if (symbol == null)
				{
					throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
				}
				ReadOnlyCollection<GrammarRule> readOnlyCollection = grammar.RulesOfHead(symbol);
				if (readOnlyCollection == null || readOnlyCollection.Count != 1)
				{
					throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
				}
				nonterminalRule = readOnlyCollection.Single<GrammarRule>() as NonterminalRule;
			}
			else
			{
				nonterminalRule = grammar.Rule(text) as NonterminalRule;
			}
			if (nonterminalRule == null)
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			return new JoinProgramSet(nonterminalRule, array);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000282BC File Offset: 0x000264BC
		public override bool Contains(ProgramNode program)
		{
			if (!this.Rule.Equals(program.GrammarRule))
			{
				return false;
			}
			int num = 0;
			foreach (ProgramSet programSet in this.ParameterSpaces)
			{
				if (num >= program.Children.Length || !programSet.Contains(program.Children[num++]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002831B File Offset: 0x0002651B
		protected override BigInteger CalculateSize()
		{
			return this.ParameterSpaces.Aggregate(BigInteger.One, (BigInteger s, ProgramSet p) => s * p.Size);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002834C File Offset: 0x0002654C
		internal override string FormatAST()
		{
			return this.Rule.FormatAST(this.ParameterSpaces.Select((ProgramSet v) => CodeBuilder.Create(v.FormatAST()))).GetCode();
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00028388 File Offset: 0x00026588
		public override ProgramNode Sample(Random random, ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformRandom)
		{
			return this.Rule.BuildASTNode(this.ParameterSpaces.Select((ProgramSet space) => space.Sample(random, programSamplingStrategy)).ToArray<ProgramNode>());
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000283D0 File Offset: 0x000265D0
		public override Dictionary<int, ProgramSet> Shatter(ProgramSetUtils.ShatteringContext context)
		{
			return (from fragment in this.ParameterSpaces.Select(new Func<ProgramSet, Dictionary<int, ProgramSet>>(context.ShatterChild)).CartesianProduct<KeyValuePair<int, ProgramSet>>()
				group ProgramSet.Join(this.Rule, fragment.Select((KeyValuePair<int, ProgramSet> kvp) => kvp.Value).ToArray<ProgramSet>()) by context.UHash(this.Rule.GetHashCode(), fragment.Select((KeyValuePair<int, ProgramSet> kvp) => kvp.Key))).ToDictionary((IGrouping<int, ProgramSet> grp) => grp.Key, (IGrouping<int, ProgramSet> grp) => grp.NormalizedUnion());
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00028472 File Offset: 0x00026672
		public override T AcceptVisitor<T>(ProgramSetVisitor<T> visitor)
		{
			return visitor.VisitJoin(this);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002847C File Offset: 0x0002667C
		protected override bool StructuralEqualsImpl(ProgramSet other)
		{
			JoinProgramSet joinProgramSet = (JoinProgramSet)other;
			if (joinProgramSet.ParameterSpaces.Length != this.ParameterSpaces.Length)
			{
				return false;
			}
			return this.ParameterSpaces.ZipWith(joinProgramSet.ParameterSpaces).All((Record<ProgramSet, ProgramSet> r) => r.Item1.StructuralEquals(r.Item2));
		}

		// Token: 0x0400069E RID: 1694
		internal const string XMLKey = "Join";
	}
}
