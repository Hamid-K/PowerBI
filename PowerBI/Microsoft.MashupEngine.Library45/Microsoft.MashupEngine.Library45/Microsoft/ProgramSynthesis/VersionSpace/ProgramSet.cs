using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000281 RID: 641
	public abstract class ProgramSet : ILanguage
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002882E File Offset: 0x00026A2E
		protected ProgramSet(Symbol symbol)
		{
			this.Symbol = symbol;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00028848 File Offset: 0x00026A48
		public Symbol Symbol { get; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000DE3 RID: 3555
		public abstract IEnumerable<ProgramNode> RealizedPrograms { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00028850 File Offset: 0x00026A50
		public BigInteger Size
		{
			get
			{
				if (this._size != null)
				{
					return this._size.Value;
				}
				this._size = new BigInteger?(this.CalculateSize());
				return this._size.Value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00028887 File Offset: 0x00026A87
		public int Volume
		{
			get
			{
				if (this._volume != null)
				{
					return this._volume.Value;
				}
				this._volume = new int?(this.CalculateVolume(null));
				return this._volume.Value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000DE6 RID: 3558
		protected abstract ProgramSet[] ChildProgramSets { get; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x000288BF File Offset: 0x00026ABF
		public virtual bool IsEmpty
		{
			get
			{
				return this.Size == 0L;
			}
		}

		// Token: 0x06000DE8 RID: 3560
		public abstract ProgramSet Intersect(ProgramSet other);

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x000288CE File Offset: 0x00026ACE
		public Symbol LanguageSymbol
		{
			get
			{
				return this.Symbol;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000288D6 File Offset: 0x00026AD6
		public IEnumerable<ProgramNode> AllElements
		{
			get
			{
				return this.RealizedPrograms;
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x000288DE File Offset: 0x00026ADE
		public override string ToString()
		{
			return this.FormatAST();
		}

		// Token: 0x06000DEC RID: 3564
		internal abstract string FormatAST();

		// Token: 0x06000DED RID: 3565 RVA: 0x000288E8 File Offset: 0x00026AE8
		public ILanguage Intersect(ILanguage other)
		{
			ProgramSet programSet = other as ProgramSet;
			if (programSet != null)
			{
				return this.Intersect(programSet);
			}
			if (!(other is Symbol) && !(other is GrammarRule))
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not handle language of type \"{0}\" in call to ProgramSet.Intersect()", new object[] { other.GetType() })));
			}
			if (other.LanguageSymbol == this.Symbol)
			{
				return this;
			}
			return ProgramSet.Empty(this.Symbol);
		}

		// Token: 0x06000DEE RID: 3566
		protected abstract BigInteger CalculateSize();

		// Token: 0x06000DEF RID: 3567 RVA: 0x0002895C File Offset: 0x00026B5C
		private int CalculateVolume(HashSet<ProgramSet> alreadySeen = null)
		{
			alreadySeen = alreadySeen ?? new HashSet<ProgramSet>(IdentityEquality.Comparer);
			if (alreadySeen.Contains(this))
			{
				return 0;
			}
			alreadySeen.Add(this);
			return 1 + this.ChildProgramSets.Sum((ProgramSet child) => child.CalculateVolume(alreadySeen));
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000289C8 File Offset: 0x00026BC8
		internal void ClearTopKCache()
		{
			lock (this)
			{
				this._topKCache.Clear();
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00028A08 File Offset: 0x00026C08
		public IEnumerable<ProgramNode> TopK(IFeature feature, int k = 1, FeatureCalculationContext context = null, LogListener logListener = null)
		{
			Dictionary<FeatureInfo, Dictionary<FeatureCalculationContext, ProgramSet.TopKCacheEntry>> topKCache = this._topKCache;
			IEnumerable<ProgramNode> enumerable;
			lock (topKCache)
			{
				Dictionary<FeatureCalculationContext, ProgramSet.TopKCacheEntry> dictionary;
				if (!this._topKCache.TryGetValue(feature.Info, out dictionary))
				{
					dictionary = (this._topKCache[feature.Info] = new Dictionary<FeatureCalculationContext, ProgramSet.TopKCacheEntry>(feature.GetFccEqualityComparer()));
				}
				FeatureCalculationContext featureCalculationContext = context ?? FeatureCalculationContext.Empty;
				ProgramSet.TopKCacheEntry topKCacheEntry;
				if (dictionary.TryGetValue(featureCalculationContext, out topKCacheEntry) && topKCacheEntry.K >= k)
				{
					enumerable = topKCacheEntry.Programs.TakeKDistinctOn((ProgramNode p) => p.GetFeatureValue(feature, context), k, null);
				}
				else
				{
					IEnumerable<ProgramNode> enumerable2 = this.CalculateTopK(feature, k, context, logListener);
					dictionary[featureCalculationContext] = new ProgramSet.TopKCacheEntry
					{
						K = k,
						Programs = enumerable2
					};
					enumerable = enumerable2;
				}
			}
			return enumerable;
		}

		// Token: 0x06000DF2 RID: 3570
		protected abstract IEnumerable<ProgramNode> CalculateTopK(IFeature feature, int k, FeatureCalculationContext context, LogListener logListener);

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00028B1C File Offset: 0x00026D1C
		protected IEnumerable<ProgramNode> TopK(IEnumerable<ProgramNode> programs, IFeature feature, int k, FeatureCalculationContext context = null, LogListener logListener = null)
		{
			Func<ProgramNode, object> func = ((context == null) ? ((ProgramNode p) => p.GetFeatureValue(feature, null)) : ((ProgramNode p) => p.GetFeatureValue(feature, context)));
			return programs.TopK(func, k, null, null);
		}

		// Token: 0x06000DF4 RID: 3572
		internal abstract ProgramSet FindSetDepthFirst(Predicate<ProgramSet> predicate, Predicate<ProgramSet> traverseChildren = null);

		// Token: 0x06000DF5 RID: 3573
		public abstract ProgramNode Sample(Random random, ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformRandom);

		// Token: 0x06000DF6 RID: 3574
		public abstract Dictionary<int, ProgramSet> Shatter(ProgramSetUtils.ShatteringContext context);

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00028B6B File Offset: 0x00026D6B
		public static ProgramSet Empty(Symbol symbol)
		{
			return new DirectProgramSet(symbol, Array.Empty<ProgramNode>());
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00028B78 File Offset: 0x00026D78
		public static ProgramSet List(Symbol symbol, params ProgramNode[] programs)
		{
			return ProgramSet.List(symbol, programs.AsEnumerable<ProgramNode>());
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00028B86 File Offset: 0x00026D86
		public static ProgramSet List(Symbol symbol, IEnumerable<ProgramNode> programs)
		{
			return new DirectProgramSet(symbol, programs);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00028B90 File Offset: 0x00026D90
		public static ProgramSet Join(NonterminalRule rule, params ProgramSet[] parameterSpaces)
		{
			Func<ProgramSet, bool> func;
			if ((func = ProgramSet.<>O.<0>__IsNullOrEmpty) == null)
			{
				func = (ProgramSet.<>O.<0>__IsNullOrEmpty = new Func<ProgramSet, bool>(ProgramSet.IsNullOrEmpty));
			}
			if (parameterSpaces.Any(func))
			{
				return ProgramSet.Empty(rule.Head);
			}
			BigInteger bigInteger = parameterSpaces.Aggregate(BigInteger.One, (BigInteger x, ProgramSet y) => x * y.Size);
			JoinProgramSet joinProgramSet = new JoinProgramSet(rule, parameterSpaces);
			if (!(bigInteger >= 20L))
			{
				return ProgramSet.List(rule.Head, joinProgramSet.RealizedPrograms.ToArray<ProgramNode>());
			}
			return joinProgramSet;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00028C20 File Offset: 0x00026E20
		public static ProgramSet Join(NonterminalRule rule, IEnumerable<ProgramSet> parameterSpaces)
		{
			return ProgramSet.Join(rule, parameterSpaces.ToArray<ProgramSet>());
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00028C2E File Offset: 0x00026E2E
		public static bool IsNullOrEmpty(ProgramSet set)
		{
			return set == null || set.IsEmpty;
		}

		// Token: 0x06000DFD RID: 3581
		public abstract XElement ToXML(Dictionary<object, int> identityCache = null, params IFeature[] featureCalculators);

		// Token: 0x06000DFE RID: 3582 RVA: 0x00028C3C File Offset: 0x00026E3C
		public ProgramSet Filter(Spec spec)
		{
			if (spec is TopSpec)
			{
				return this;
			}
			Dictionary<object[], ProgramSet> dictionary = this.ClusterOnInputTuple(spec.ProvidedInputs);
			ExampleSpec exampleSpec = spec as ExampleSpec;
			if (!(exampleSpec != null))
			{
				List<ProgramSet> list = new List<ProgramSet>(dictionary.Count);
				foreach (KeyValuePair<object[], ProgramSet> keyValuePair in dictionary)
				{
					if (spec.CorrectOnAllProvided(keyValuePair.Key))
					{
						list.Add(keyValuePair.Value);
					}
				}
				return list.NormalizedUnion();
			}
			object[] array = exampleSpec.ProvidedInputs.Select((State i) => exampleSpec.Examples[i]).ToArray<object>();
			ProgramSet programSet;
			if (!dictionary.TryGetValue(array, out programSet))
			{
				return ProgramSet.Empty(this.Symbol);
			}
			return programSet;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00028D28 File Offset: 0x00026F28
		public void PartitionByValidity(Spec spec, out ProgramSet validSet, out ProgramSet invalidSet)
		{
			if (spec is TopSpec)
			{
				validSet = this;
				invalidSet = ProgramSet.Empty(this.Symbol);
				return;
			}
			Dictionary<object[], ProgramSet> dictionary = this.ClusterOnInputTuple(spec.ProvidedInputs);
			List<ProgramSet> list = new List<ProgramSet>(dictionary.Count);
			List<ProgramSet> list2 = new List<ProgramSet>(dictionary.Count);
			foreach (KeyValuePair<object[], ProgramSet> keyValuePair in dictionary)
			{
				if (spec.CorrectOnAllProvided(keyValuePair.Key))
				{
					list.Add(keyValuePair.Value);
				}
				else
				{
					list2.Add(keyValuePair.Value);
				}
			}
			validSet = list.NormalizedUnion();
			invalidSet = list2.NormalizedUnion();
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00028DE8 File Offset: 0x00026FE8
		public PrunedProgramSet Prune(int? topProgramsK, int? randomProgramsK, IFeature feature, IFeatureOptions featureOptions, FeatureCalculationContext fcc, ProgramSamplingStrategy samplingStrategy, Random random, LogListener logListener)
		{
			if (randomProgramsK == null && topProgramsK == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("{0} and {1} cannot both be null in call to {2}.", new object[] { "randomProgramsK", "topProgramsK", "Prune" })));
			}
			if (topProgramsK != null && feature == null)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("{0} cannot be null if {1} is non-null, in call to {2}", new object[] { "feature", "topProgramsK", "Prune" })));
			}
			return this.Prune(PruningRequest.Create(topProgramsK, randomProgramsK, feature, featureOptions, samplingStrategy), fcc, random, logListener);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00028E94 File Offset: 0x00027094
		public virtual PrunedProgramSet Prune(PruningRequest pruningRequest, FeatureCalculationContext fcc, Random random, LogListener logListener)
		{
			if (pruningRequest.IsEmpty)
			{
				throw new ArgumentException("pruningRequest", "Pruning request cannot be empty.");
			}
			if (this.IsEmpty)
			{
				return PrunedProgramSet.Empty(this.Symbol);
			}
			List<ProgramNode> list = null;
			if (pruningRequest.HasTopKRequest)
			{
				list = this.TopK(pruningRequest.TopProgramsFeature, pruningRequest.K.Value, fcc, logListener).ToList<ProgramNode>();
			}
			List<ProgramNode> list2 = null;
			if (pruningRequest.HasRandomKRequest)
			{
				list2 = this.SamplePrograms(pruningRequest.RandomK.Value, random, pruningRequest.ProgramSamplingStrategy).ToList<ProgramNode>();
			}
			return new PrunedProgramSet(this.Symbol, pruningRequest, fcc, list, list2);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00028F34 File Offset: 0x00027134
		protected ProgramSet Prune(int k, IFeature feature, FeatureCalculationContext context, LogListener logListener)
		{
			return ProgramSet.List(this.Symbol, this.TopK(feature, k, context, logListener));
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00028F4C File Offset: 0x0002714C
		protected virtual IEnumerable<ProgramNode> SamplePrograms(int numProgramsToSample, Random random, ProgramSamplingStrategy samplingStrategy = ProgramSamplingStrategy.UniformRandom)
		{
			if (this.Size <= (long)numProgramsToSample)
			{
				foreach (ProgramNode programNode in this.RealizedPrograms)
				{
					yield return programNode;
				}
				IEnumerator<ProgramNode> enumerator = null;
				yield break;
			}
			HashSet<ProgramNode> randomlyChosenPrograms = new HashSet<ProgramNode>();
			int sampleCount = 0;
			while (randomlyChosenPrograms.Count < numProgramsToSample && sampleCount < numProgramsToSample * 3)
			{
				ProgramNode programNode2 = this.Sample(random, samplingStrategy);
				if (!randomlyChosenPrograms.Contains(programNode2))
				{
					randomlyChosenPrograms.Add(programNode2);
					yield return programNode2;
				}
				int num = sampleCount + 1;
				sampleCount = num;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000E04 RID: 3588
		internal abstract ProgramSet FindChildSet(ProgramNode node, int indexInParent);

		// Token: 0x06000E05 RID: 3589
		public abstract bool Contains(ProgramNode program);

		// Token: 0x06000E06 RID: 3590
		public abstract T AcceptVisitor<T>(ProgramSetVisitor<T> visitor);

		// Token: 0x06000E07 RID: 3591
		protected abstract Dictionary<object, ProgramSet> ClusterOnInputImpl(State inputState);

		// Token: 0x06000E08 RID: 3592 RVA: 0x00028F71 File Offset: 0x00027171
		public Dictionary<object, ProgramSet> ClusterOnInput(State inputState)
		{
			return this.ClusterOnInputImpl(inputState);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00028F7C File Offset: 0x0002717C
		public Dictionary<object[], ProgramSet> ClusterOnInputTuple(IEnumerable<State> inputStates)
		{
			State[] array = (inputStates as State[]) ?? inputStates.ToArray<State>();
			if (array.Length == 0)
			{
				Dictionary<object[], ProgramSet> dictionary = new Dictionary<object[], ProgramSet>(ObjectArrayEquality.Comparer);
				object[] array2 = new object[0];
				dictionary[array2] = this;
				return dictionary;
			}
			if (array.Length == 1)
			{
				return this.ClusterOnInput(array[0]).ToDictionary((KeyValuePair<object, ProgramSet> kvp) => new object[] { kvp.Key }, (KeyValuePair<object, ProgramSet> kvp) => kvp.Value, ObjectArrayEquality.Comparer);
			}
			List<ProgramSet.Cluster> list = new List<ProgramSet.Cluster>
			{
				new ProgramSet.Cluster(null, this)
			};
			List<ProgramSet.Cluster> list2 = new List<ProgramSet.Cluster>();
			for (int i = array.Length - 1; i >= 0; i--)
			{
				list2.Clear();
				foreach (ProgramSet.Cluster cluster in list)
				{
					foreach (KeyValuePair<object, ProgramSet> keyValuePair in cluster.Space.ClusterOnInput(array[i]))
					{
						if (!ProgramSet.IsNullOrEmpty(keyValuePair.Value))
						{
							list2.Add(new ProgramSet.Cluster(new ProgramSet.HashedRecord(keyValuePair.Key, cluster.ValueTail), keyValuePair.Value));
						}
					}
				}
				List<ProgramSet.Cluster> list3 = list;
				list = list2;
				list2 = list3;
			}
			Dictionary<object[], ProgramSet> dictionary2 = new Dictionary<object[], ProgramSet>(ObjectArrayEquality.Comparer);
			foreach (ProgramSet.Cluster cluster2 in list)
			{
				object[] array3 = cluster2.ValueTail.ToArray<object>();
				ProgramSet programSet;
				if (dictionary2.TryGetValue(array3, out programSet))
				{
					UnionProgramSet unionProgramSet = programSet as UnionProgramSet;
					dictionary2[array3] = ((unionProgramSet == null) ? new UnionProgramSet(programSet.Symbol, new ProgramSet[] { programSet, cluster2.Space }) : unionProgramSet.UnionSpaces.AppendItem(cluster2.Space).NormalizedUnion());
				}
				else
				{
					dictionary2[array3] = cluster2.Space;
				}
			}
			return dictionary2;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000291CC File Offset: 0x000273CC
		public bool StructuralEquals(ProgramSet other)
		{
			return other == this || (other != null && !(base.GetType() != other.GetType()) && !(this.Size != other.Size) && this.Volume == other.Volume && this.StructuralEqualsImpl(other));
		}

		// Token: 0x06000E0B RID: 3595
		protected abstract bool StructuralEqualsImpl(ProgramSet other);

		// Token: 0x06000E0C RID: 3596 RVA: 0x00029228 File Offset: 0x00027428
		public string SerializeToXMLString(bool indent = false)
		{
			XElement xelement = this.SerializeToXML();
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = indent
			};
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					xelement.WriteTo(xmlWriter);
					xmlWriter.Flush();
					text = stringWriter.ToString();
				}
			}
			return text;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000292AC File Offset: 0x000274AC
		internal XElement SerializeToXML()
		{
			Dictionary<object, int> dictionary = new Dictionary<object, int>();
			return this.InternedSerialize(dictionary);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000292C8 File Offset: 0x000274C8
		internal XElement InternedSerialize(Dictionary<object, int> identityCache)
		{
			int num;
			if (identityCache.TryGetValue(this, out num))
			{
				return new XElement("Reference", num);
			}
			XElement xelement = this.SerializeImpl(identityCache).WithAttribute("ObjectID", identityCache.Count);
			identityCache[this] = identityCache.Count;
			return xelement.WithAttribute("symbol", this.Symbol);
		}

		// Token: 0x06000E0F RID: 3599
		protected abstract XElement SerializeImpl(Dictionary<object, int> identityCache);

		// Token: 0x06000E10 RID: 3600 RVA: 0x00029330 File Offset: 0x00027530
		public static ProgramSet DeserializeFromXMLString(string xmlString, Grammar grammar)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				CheckCharacters = false,
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
			ProgramSet programSet;
			using (StringReader stringReader = new StringReader(xmlString))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					programSet = ProgramSet.DeserializeFromXML(XElement.Load(xmlReader), grammar);
				}
			}
			return programSet;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000293A8 File Offset: 0x000275A8
		internal static ProgramSet DeserializeFromXML(XElement node, Grammar grammar)
		{
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			return ProgramSet.InternedDeserialize(node, grammar, dictionary);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000293C4 File Offset: 0x000275C4
		internal static ProgramSet InternedDeserialize(XElement node, Grammar grammar, Dictionary<int, object> identityCache)
		{
			if (node.Name.LocalName == "Reference")
			{
				int num;
				object obj;
				if (int.TryParse(node.Value, out num) && identityCache.TryGetValue(num, out obj))
				{
					ProgramSet programSet = obj as ProgramSet;
					if (programSet != null)
					{
						return programSet;
					}
				}
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			XAttribute xattribute = node.Attribute("symbol");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			Symbol symbol = grammar.Symbol(text);
			if (symbol == null)
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			XAttribute xattribute2 = node.Attribute("ObjectID");
			string text2 = ((xattribute2 != null) ? xattribute2.Value : null);
			int num2;
			if (text2 == null || !int.TryParse(text2, out num2))
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			string localName = node.Name.LocalName;
			ProgramSet programSet2;
			if (!(localName == "Direct"))
			{
				if (!(localName == "Pruned"))
				{
					if (!(localName == "Union"))
					{
						if (!(localName == "Join"))
						{
							throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
						}
						programSet2 = JoinProgramSet.InternedDeserialize(node, grammar, identityCache);
					}
					else
					{
						programSet2 = UnionProgramSet.InternedDeserialize(node, symbol, grammar, identityCache);
					}
				}
				else
				{
					programSet2 = PrunedProgramSet.InternedDeserialize(node, symbol, grammar, identityCache);
				}
			}
			else
			{
				programSet2 = DirectProgramSet.InternedDeserialize(node, symbol, grammar, identityCache);
			}
			if (identityCache.ContainsKey(num2))
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			identityCache[num2] = programSet2;
			return programSet2;
		}

		// Token: 0x040006BA RID: 1722
		internal const int LeafSetSize = 20;

		// Token: 0x040006BB RID: 1723
		private readonly Dictionary<FeatureInfo, Dictionary<FeatureCalculationContext, ProgramSet.TopKCacheEntry>> _topKCache = new Dictionary<FeatureInfo, Dictionary<FeatureCalculationContext, ProgramSet.TopKCacheEntry>>();

		// Token: 0x040006BC RID: 1724
		private BigInteger? _size;

		// Token: 0x040006BD RID: 1725
		private int? _volume;

		// Token: 0x040006BF RID: 1727
		private const string XMLReferenceKey = "Reference";

		// Token: 0x040006C0 RID: 1728
		private const string XMLObjectIdAttributeName = "ObjectID";

		// Token: 0x040006C1 RID: 1729
		internal const string XMLSymbolAttributeName = "symbol";

		// Token: 0x02000282 RID: 642
		private class TopKCacheEntry
		{
			// Token: 0x040006C2 RID: 1730
			public int K;

			// Token: 0x040006C3 RID: 1731
			public IEnumerable<ProgramNode> Programs;
		}

		// Token: 0x02000283 RID: 643
		private class HashedRecord : IEquatable<ProgramSet.HashedRecord>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x06000E14 RID: 3604 RVA: 0x0002953B File Offset: 0x0002773B
			public HashedRecord(object value, ProgramSet.HashedRecord rest = null)
			{
				this.Top = value;
				this.Rest = rest;
			}

			// Token: 0x06000E15 RID: 3605 RVA: 0x00029551 File Offset: 0x00027751
			public IEnumerator<object> GetEnumerator()
			{
				return new ProgramSet.HashedRecord.RecordEnumerator(this);
			}

			// Token: 0x06000E16 RID: 3606 RVA: 0x00029559 File Offset: 0x00027759
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000E17 RID: 3607 RVA: 0x00029564 File Offset: 0x00027764
			public bool Equals(ProgramSet.HashedRecord that)
			{
				if (that == null)
				{
					return false;
				}
				if (this.Rest == null)
				{
					return that.Rest == null && ValueEquality.Comparer.Equals(this.Top, that.Top);
				}
				return ValueEquality.Comparer.Equals(this.Top, that.Top) && this.Rest.Equals(that.Rest);
			}

			// Token: 0x06000E18 RID: 3608 RVA: 0x000295CA File Offset: 0x000277CA
			public override bool Equals(object obj)
			{
				return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ProgramSet.HashedRecord)obj)));
			}

			// Token: 0x06000E19 RID: 3609 RVA: 0x000295F8 File Offset: 0x000277F8
			public override int GetHashCode()
			{
				if (this._hash != null)
				{
					return this._hash.Value;
				}
				ProgramSet.HashedRecord rest = this.Rest;
				this._hash = new int?(((rest != null) ? (rest.GetHashCode() * 15273809) : 0) ^ ValueEquality.Comparer.GetHashCode(this.Top));
				return this._hash.Value;
			}

			// Token: 0x040006C4 RID: 1732
			public readonly ProgramSet.HashedRecord Rest;

			// Token: 0x040006C5 RID: 1733
			public readonly object Top;

			// Token: 0x040006C6 RID: 1734
			private int? _hash;

			// Token: 0x02000284 RID: 644
			private class RecordEnumerator : IEnumerator<object>, IDisposable, IEnumerator
			{
				// Token: 0x06000E1A RID: 3610 RVA: 0x0002965D File Offset: 0x0002785D
				public RecordEnumerator(ProgramSet.HashedRecord record)
				{
					this._record = record;
					this._current = null;
				}

				// Token: 0x06000E1B RID: 3611 RVA: 0x0000CC37 File Offset: 0x0000AE37
				public void Dispose()
				{
				}

				// Token: 0x06000E1C RID: 3612 RVA: 0x00029673 File Offset: 0x00027873
				public bool MoveNext()
				{
					this._current = ((this._current == null) ? this._record : this._current.Rest);
					return this._current != null;
				}

				// Token: 0x06000E1D RID: 3613 RVA: 0x0002969F File Offset: 0x0002789F
				public void Reset()
				{
					this._current = null;
				}

				// Token: 0x17000342 RID: 834
				// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000296A8 File Offset: 0x000278A8
				public object Current
				{
					get
					{
						return this._current.Top;
					}
				}

				// Token: 0x17000343 RID: 835
				// (get) Token: 0x06000E1F RID: 3615 RVA: 0x000296B5 File Offset: 0x000278B5
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x040006C7 RID: 1735
				private readonly ProgramSet.HashedRecord _record;

				// Token: 0x040006C8 RID: 1736
				private ProgramSet.HashedRecord _current;
			}
		}

		// Token: 0x02000285 RID: 645
		private class Cluster
		{
			// Token: 0x06000E20 RID: 3616 RVA: 0x000296BD File Offset: 0x000278BD
			public Cluster(ProgramSet.HashedRecord valueTail, ProgramSet space)
			{
				this.ValueTail = valueTail;
				this.Space = space;
			}

			// Token: 0x040006C9 RID: 1737
			public readonly ProgramSet Space;

			// Token: 0x040006CA RID: 1738
			public readonly ProgramSet.HashedRecord ValueTail;
		}

		// Token: 0x02000286 RID: 646
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040006CB RID: 1739
			public static Func<ProgramSet, bool> <0>__IsNullOrEmpty;
		}
	}
}
