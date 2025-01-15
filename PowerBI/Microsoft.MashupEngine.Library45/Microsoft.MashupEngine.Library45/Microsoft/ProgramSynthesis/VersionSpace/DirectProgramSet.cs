using System;
using System.Collections.Generic;
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
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000270 RID: 624
	public class DirectProgramSet : ProgramSet, IDirectSetLanguage, ILanguage
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00027664 File Offset: 0x00025864
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x0002766C File Offset: 0x0002586C
		protected IReadOnlyList<ProgramNode> Programs { get; set; }

		// Token: 0x06000D79 RID: 3449 RVA: 0x00027675 File Offset: 0x00025875
		public DirectProgramSet(Symbol symbol, params ProgramNode[] programs)
			: this(symbol, programs)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002767F File Offset: 0x0002587F
		protected DirectProgramSet(Symbol symbol)
			: base(symbol)
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00027688 File Offset: 0x00025888
		public DirectProgramSet(Symbol symbol, IEnumerable<ProgramNode> programs)
			: base(symbol)
		{
			this.Programs = (programs as IReadOnlyList<ProgramNode>) ?? programs.ToList<ProgramNode>();
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000276A7 File Offset: 0x000258A7
		public override IEnumerable<ProgramNode> RealizedPrograms
		{
			get
			{
				return this.Programs;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000276AF File Offset: 0x000258AF
		public override bool IsEmpty
		{
			get
			{
				return this.Programs.IsEmpty<ProgramNode>();
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000276BC File Offset: 0x000258BC
		protected override ProgramSet[] ChildProgramSets
		{
			get
			{
				return new ProgramSet[0];
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000276C4 File Offset: 0x000258C4
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
				return ProgramSet.List(base.Symbol, this.RealizedPrograms.Intersect(other.RealizedPrograms));
			}
			JoinProgramSet joinProgramSet = other as JoinProgramSet;
			if (joinProgramSet != null)
			{
				return ProgramSet.List(base.Symbol, this.IntersectJoin(joinProgramSet));
			}
			UnionProgramSet unionProgramSet = other as UnionProgramSet;
			if (unionProgramSet == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown ProgramSet type: {0}", new object[] { other.GetType() })), "other");
			}
			return unionProgramSet.UnionSpaces.Select(new Func<ProgramSet, ProgramSet>(this.Intersect)).NormalizedUnion() ?? programSet;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0002779B File Offset: 0x0002599B
		public bool IsVariable
		{
			get
			{
				return base.Symbol.IsVariable;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x000277A8 File Offset: 0x000259A8
		public bool IsInput
		{
			get
			{
				return base.Symbol.IsInput;
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000277B5 File Offset: 0x000259B5
		protected override IEnumerable<ProgramNode> CalculateTopK(IFeature feature, int k, FeatureCalculationContext context, LogListener logListener)
		{
			return base.TopK(this.Programs, feature, k, context, logListener);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000277C8 File Offset: 0x000259C8
		internal override ProgramSet FindSetDepthFirst(Predicate<ProgramSet> predicate, Predicate<ProgramSet> traverseChildren = null)
		{
			if (!predicate(this))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000277D8 File Offset: 0x000259D8
		protected override Dictionary<object, ProgramSet> ClusterOnInputImpl(State inputState)
		{
			Dictionary<object, List<ProgramNode>> dictionary = new Dictionary<object, List<ProgramNode>>(ValueEquality.Comparer);
			foreach (ProgramNode programNode in this.Programs)
			{
				object obj = programNode.Invoke(inputState).NullToBottom();
				dictionary.GetOrCreateValue(obj).Add(programNode);
			}
			Dictionary<object, ProgramSet> dictionary2 = new Dictionary<object, ProgramSet>(ValueEquality.Comparer);
			foreach (KeyValuePair<object, List<ProgramNode>> keyValuePair in dictionary)
			{
				dictionary2[keyValuePair.Key] = ProgramSet.List(base.Symbol, keyValuePair.Value);
			}
			return dictionary2;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000278A8 File Offset: 0x00025AA8
		private IEnumerable<ProgramNode> IntersectJoin(JoinProgramSet other)
		{
			foreach (ProgramNode programNode in this.Programs)
			{
				NonterminalNode nonterminalNode = programNode as NonterminalNode;
				if (!(nonterminalNode == null) && nonterminalNode.Rule.Equals(other.Rule))
				{
					bool flag = true;
					int num = 0;
					while (flag && num < other.ParameterSpaces.Length)
					{
						if (ProgramSet.List(nonterminalNode.Rule.Body[num], new ProgramNode[] { nonterminalNode.Children[num] }).Intersect(other.ParameterSpaces[num]).IsEmpty)
						{
							flag = false;
						}
						num++;
					}
					if (flag)
					{
						yield return programNode;
					}
				}
			}
			IEnumerator<ProgramNode> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000278C0 File Offset: 0x00025AC0
		public override XElement ToXML(Dictionary<object, int> identityCache = null, params IFeature[] featureCalculators)
		{
			Func<ProgramNode, XAttribute>[] array = featureCalculators.GetAttributeCalculators(identityCache).ToArray<Func<ProgramNode, XAttribute>>();
			return this.Programs.CollectionToXML("Direct", "Program", ObjectFormatting.ToString, null, array).WithAttribute("symbol", base.Symbol).WithAttribute("size", base.Size);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00027918 File Offset: 0x00025B18
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache)
		{
			return new XElement("Direct", this.Programs.Select((ProgramNode p) => new XElement("Program", p.ToInternedXML(identityCache))));
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00027958 File Offset: 0x00025B58
		internal static DirectProgramSet InternedDeserialize(XElement node, Symbol resolvedSymbol, Grammar grammar, Dictionary<int, object> identityCache)
		{
			if (node.Name.LocalName != "Direct")
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			List<ProgramNode> list = (from c in node.Elements("Program")
				select ProgramNode.FromInternedXML(c.Elements().Single<XElement>(), grammar, identityCache)).ToList<ProgramNode>();
			return new DirectProgramSet(resolvedSymbol, list);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000279CC File Offset: 0x00025BCC
		internal override ProgramSet FindChildSet(ProgramNode node, int indexInParent)
		{
			LambdaNode lambdaNode = node as LambdaNode;
			if (indexInParent != 0 || lambdaNode == null)
			{
				return null;
			}
			LambdaRule lambdaRule = lambdaNode.Rule as LambdaRule;
			if (lambdaRule == null)
			{
				return null;
			}
			Symbol variable = lambdaRule.Variable;
			if (this.Programs.All(delegate(ProgramNode p)
			{
				LambdaNode lambdaNode2 = p as LambdaNode;
				LambdaRule lambdaRule2 = ((lambdaNode2 != null) ? lambdaNode2.Rule : null) as LambdaRule;
				return lambdaRule2 != null && lambdaRule2.Variable == variable;
			}))
			{
				return new DirectProgramSet(lambdaNode.Symbol, this.Programs.Select(delegate(ProgramNode llnode)
				{
					LambdaNode lambdaNode3 = llnode as LambdaNode;
					if (lambdaNode3 == null)
					{
						return null;
					}
					return lambdaNode3.BodyNode;
				}));
			}
			return null;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00027A61 File Offset: 0x00025C61
		public override bool Contains(ProgramNode program)
		{
			return this.Programs != null && this.Programs.Contains(program);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00027A79 File Offset: 0x00025C79
		protected override BigInteger CalculateSize()
		{
			return this.Programs.LongCount<ProgramNode>();
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00027A8B File Offset: 0x00025C8B
		internal override string FormatAST()
		{
			return this.Programs.DumpCollection(ObjectFormatting.Literal, "{", "}", ", ", null);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00027AA9 File Offset: 0x00025CA9
		public override ProgramNode Sample(Random random, ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformRandom)
		{
			return this.Programs.ElementAt(random.Next((int)base.Size));
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override Dictionary<int, ProgramSet> Shatter(ProgramSetUtils.ShatteringContext context)
		{
			return this.Programs.GroupBy(new Func<ProgramNode, int>(context.HashProgram)).ToDictionary((IGrouping<int, ProgramNode> group) => group.Key, (IGrouping<int, ProgramNode> group) => ProgramSet.List(base.Symbol, group));
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00027B1C File Offset: 0x00025D1C
		public override T AcceptVisitor<T>(ProgramSetVisitor<T> visitor)
		{
			return visitor.VisitDirect(this);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00027B25 File Offset: 0x00025D25
		protected override bool StructuralEqualsImpl(ProgramSet other)
		{
			return ((DirectProgramSet)other).Programs.ConvertToHashSet<ProgramNode>().SetEquals(this.Programs);
		}

		// Token: 0x0400068D RID: 1677
		internal const string XMLKey = "Direct";
	}
}
