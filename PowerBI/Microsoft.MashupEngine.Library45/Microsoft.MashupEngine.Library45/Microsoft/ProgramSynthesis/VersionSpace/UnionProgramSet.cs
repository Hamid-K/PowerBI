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
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x020002C1 RID: 705
	public class UnionProgramSet : ProgramSet, IAlternatingLanguage, ILanguage
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		public UnionProgramSet(Symbol symbol, params ProgramSet[] unionSpaces)
			: base(symbol)
		{
			this.UnionSpaces = unionSpaces;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public ProgramSet[] UnionSpaces { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0002CA04 File Offset: 0x0002AC04
		public override IEnumerable<ProgramNode> RealizedPrograms
		{
			get
			{
				return this.UnionSpaces.SelectMany((ProgramSet space) => space.RealizedPrograms);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0002CA30 File Offset: 0x0002AC30
		public override bool IsEmpty
		{
			get
			{
				return this.UnionSpaces.All((ProgramSet p) => p.IsEmpty);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		protected override ProgramSet[] ChildProgramSets
		{
			get
			{
				return this.UnionSpaces;
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002CA64 File Offset: 0x0002AC64
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
			if (other is DirectProgramSet || other is JoinProgramSet)
			{
				return other.Intersect(this);
			}
			UnionProgramSet unionProgramSet = other as UnionProgramSet;
			if (unionProgramSet == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown ProgramSet type: {0}", new object[] { other.GetType() })), "other");
			}
			List<ProgramSet> list = new List<ProgramSet>();
			foreach (ProgramSet programSet2 in this.UnionSpaces)
			{
				foreach (ProgramSet programSet3 in unionProgramSet.UnionSpaces)
				{
					ProgramSet programSet4 = programSet2.Intersect(programSet3);
					if (!programSet4.IsEmpty)
					{
						list.Add(programSet4);
					}
				}
			}
			return list.NormalizedUnion() ?? programSet;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		public IEnumerable<ILanguage> Alternatives
		{
			get
			{
				return this.UnionSpaces;
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002CB5C File Offset: 0x0002AD5C
		protected override IEnumerable<ProgramNode> CalculateTopK(IFeature feature, int k, FeatureCalculationContext context, LogListener logListener)
		{
			IEnumerable<ProgramNode> enumerable = this.UnionSpaces.SelectMany((ProgramSet s) => s.TopK(feature, k, context, logListener));
			return base.TopK(enumerable, feature, k, context, logListener);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
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
			return this.UnionSpaces.Collect((ProgramSet c) => c.FindSetDepthFirst(predicate, traverseChildren)).FirstOrDefault<ProgramSet>();
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002CC2C File Offset: 0x0002AE2C
		protected override Dictionary<object, ProgramSet> ClusterOnInputImpl(State inputState)
		{
			MultiValueDictionary<object, ProgramSet> multiValueDictionary = new MultiValueDictionary<object, ProgramSet>(ValueEquality.Comparer);
			ProgramSet[] unionSpaces = this.UnionSpaces;
			for (int i = 0; i < unionSpaces.Length; i++)
			{
				foreach (KeyValuePair<object, ProgramSet> keyValuePair in unionSpaces[i].ClusterOnInput(inputState))
				{
					multiValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return multiValueDictionary.ToDictionary((KeyValuePair<object, IReadOnlyCollection<ProgramSet>> kvp) => kvp.Key, (KeyValuePair<object, IReadOnlyCollection<ProgramSet>> kvp) => kvp.Value.NormalizedUnion(), ValueEquality.Comparer);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		public override XElement ToXML(Dictionary<object, int> identityCache = null, params IFeature[] featureCalculators)
		{
			return new XElement("Union", this.UnionSpaces.Select((ProgramSet s) => s.ToXML(identityCache, featureCalculators))).WithAttribute("symbol", base.Symbol).WithAttribute("size", base.Size);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002CD68 File Offset: 0x0002AF68
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache)
		{
			return new XElement("Union", this.UnionSpaces.Select((ProgramSet s) => s.InternedSerialize(identityCache)));
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
		internal static UnionProgramSet InternedDeserialize(XElement node, Symbol resolvedSymbol, Grammar grammar, Dictionary<int, object> identityCache)
		{
			if (node.Name.LocalName != "Union")
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			ProgramSet[] array = (from c in node.Elements()
				select ProgramSet.InternedDeserialize(c, grammar, identityCache)).ToArray<ProgramSet>();
			return new UnionProgramSet(resolvedSymbol, array);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002CE10 File Offset: 0x0002B010
		internal override ProgramSet FindChildSet(ProgramNode node, int indexInParent)
		{
			return (from space in this.UnionSpaces
				select space.FindChildSet(node, indexInParent) into ps
				where !ProgramSet.IsNullOrEmpty(ps)
				select ps).Distinct<ProgramSet>().NormalizedUnion();
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002CE78 File Offset: 0x0002B078
		public override bool Contains(ProgramNode program)
		{
			return this.UnionSpaces.Any((ProgramSet space) => space.Contains(program));
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002CEA9 File Offset: 0x0002B0A9
		protected override BigInteger CalculateSize()
		{
			return this.UnionSpaces.Aggregate(BigInteger.Zero, (BigInteger acc, ProgramSet set) => acc + set.Size);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002CEDC File Offset: 0x0002B0DC
		internal override string FormatAST()
		{
			if (this.UnionSpaces.Length != 1)
			{
				return "∪" + this.UnionSpaces.DumpCollection(ObjectFormatting.Literal, "(", ")", ", ", null);
			}
			return this.UnionSpaces[0].ToString();
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002CF28 File Offset: 0x0002B128
		public override ProgramNode Sample(Random random, ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformRandom)
		{
			if (programSamplingStrategy == ProgramSamplingStrategy.UniformRandom)
			{
				BigInteger bigInteger = random.Next(base.Size);
				BigInteger bigInteger2 = 0;
				ProgramSet programSet = this.UnionSpaces[0];
				int num = 0;
				while (num < this.UnionSpaces.Length && bigInteger2 <= bigInteger)
				{
					bigInteger2 += this.UnionSpaces[num].Size;
					programSet = this.UnionSpaces[num];
					num++;
				}
				return programSet.Sample(random, programSamplingStrategy);
			}
			if (programSamplingStrategy != ProgramSamplingStrategy.UniformAcrossUnions)
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("ProgramSamplingStrategy value: {0} unsupported.", new object[] { programSamplingStrategy })));
			}
			int num2 = random.Next(this.ChildProgramSets.Length);
			return this.ChildProgramSets[num2].Sample(random, programSamplingStrategy);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
		public override Dictionary<int, ProgramSet> Shatter(ProgramSetUtils.ShatteringContext context)
		{
			return (from kvp in this.UnionSpaces.SelectMany(new Func<ProgramSet, IEnumerable<KeyValuePair<int, ProgramSet>>>(context.ShatterChild))
				group kvp.Value by kvp.Key).ToDictionary((IGrouping<int, ProgramSet> group) => group.Key, (IGrouping<int, ProgramSet> group) => group.NormalizedUnion());
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002D08E File Offset: 0x0002B28E
		public override T AcceptVisitor<T>(ProgramSetVisitor<T> visitor)
		{
			return visitor.VisitUnion(this);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002D098 File Offset: 0x0002B298
		protected override bool StructuralEqualsImpl(ProgramSet other)
		{
			UnionProgramSet unionProgramSet = (UnionProgramSet)other;
			if (this.ChildProgramSets.Length != unionProgramSet.ChildProgramSets.Length)
			{
				return false;
			}
			ProgramSet[] childProgramSets = this.ChildProgramSets;
			ProgramSet[] childProgramSets2 = unionProgramSet.ChildProgramSets;
			ProgramSet[] array = childProgramSets;
			for (int i = 0; i < array.Length; i++)
			{
				ProgramSet myChild = array[i];
				if (!childProgramSets2.Any((ProgramSet c) => c.StructuralEquals(myChild)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000773 RID: 1907
		internal const string XMLKey = "Union";
	}
}
