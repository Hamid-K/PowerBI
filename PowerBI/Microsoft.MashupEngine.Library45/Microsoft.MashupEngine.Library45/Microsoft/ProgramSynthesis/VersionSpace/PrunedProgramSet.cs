using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x020002BD RID: 701
	public class PrunedProgramSet : DirectProgramSet
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0002C450 File Offset: 0x0002A650
		public IReadOnlyList<ProgramNode> TopPrograms { get; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002C458 File Offset: 0x0002A658
		public IReadOnlyList<ProgramNode> RandomlySampledPrograms { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0002C460 File Offset: 0x0002A660
		public PruningRequest PruningRequest { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002C468 File Offset: 0x0002A668
		public FeatureCalculationContext FeatureCalculationContext { get; }

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002C470 File Offset: 0x0002A670
		public PrunedProgramSet(Symbol symbol, PruningRequest pruningRequest, FeatureCalculationContext featureCalculationContext, IEnumerable<ProgramNode> topPrograms, IEnumerable<ProgramNode> randomlySampledPrograms)
			: this(symbol, topPrograms, randomlySampledPrograms)
		{
			if (pruningRequest == null)
			{
				throw new ArgumentException("pruningRequest", FormattableString.Invariant(FormattableStringFactory.Create("Must be non-null.", Array.Empty<object>())));
			}
			this.PruningRequest = pruningRequest;
			this.FeatureCalculationContext = featureCalculationContext;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0002C4AE File Offset: 0x0002A6AE
		public new static PrunedProgramSet Empty(Symbol symbol)
		{
			return new PrunedProgramSet(symbol, null, null);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002C4B8 File Offset: 0x0002A6B8
		private PrunedProgramSet(Symbol symbol, IEnumerable<ProgramNode> topPrograms, IEnumerable<ProgramNode> randomPrograms)
			: base(symbol)
		{
			IReadOnlyList<ProgramNode> readOnlyList;
			if ((readOnlyList = topPrograms as IReadOnlyList<ProgramNode>) == null)
			{
				IReadOnlyList<ProgramNode> readOnlyList2 = ((topPrograms != null) ? topPrograms.ToList<ProgramNode>() : null);
				readOnlyList = readOnlyList2 ?? PrunedProgramSet.EmptyProgramList;
			}
			this.TopPrograms = readOnlyList;
			IReadOnlyList<ProgramNode> readOnlyList3;
			if ((readOnlyList3 = randomPrograms as IReadOnlyList<ProgramNode>) == null)
			{
				IReadOnlyList<ProgramNode> readOnlyList2 = ((randomPrograms != null) ? randomPrograms.ToList<ProgramNode>() : null);
				readOnlyList3 = readOnlyList2 ?? PrunedProgramSet.EmptyProgramList;
			}
			this.RandomlySampledPrograms = readOnlyList3;
			IReadOnlyList<ProgramNode> readOnlyList4;
			if (this.RandomlySampledPrograms.Count != 0)
			{
				IReadOnlyList<ProgramNode> readOnlyList2 = this.TopPrograms.Concat(this.RandomlySampledPrograms).ToList<ProgramNode>();
				readOnlyList4 = readOnlyList2;
			}
			else
			{
				readOnlyList4 = this.TopPrograms;
			}
			base.Programs = readOnlyList4;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0002C54D File Offset: 0x0002A74D
		private void ThrowIfReconstructed(string methodName)
		{
			if (this.PruningRequest == null && !this.IsEmpty)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Method: {0} cannot be called on a deserialized PrunedProgramSet.", new object[] { methodName })));
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002C580 File Offset: 0x0002A780
		protected override IEnumerable<ProgramNode> CalculateTopK(IFeature feature, int k, FeatureCalculationContext context, LogListener logListener)
		{
			if (this.IsEmpty)
			{
				return Enumerable.Empty<ProgramNode>();
			}
			this.ThrowIfReconstructed("CalculateTopK");
			if ((context == null || context.Equals(this.FeatureCalculationContext)) && feature == this.PruningRequest.TopProgramsFeature)
			{
				int? k2 = this.PruningRequest.K;
				if ((k >= k2.GetValueOrDefault()) & (k2 != null))
				{
					return this.TopPrograms;
				}
			}
			return base.TopK(this.TopPrograms, feature, k, context, logListener);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0002C603 File Offset: 0x0002A803
		public override ProgramNode Sample(Random random, ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformRandom)
		{
			return this.RandomlySampledPrograms[random.Next(this.RandomlySampledPrograms.Count)];
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002C624 File Offset: 0x0002A824
		public override PrunedProgramSet Prune(PruningRequest pruningRequest, FeatureCalculationContext fcc, Random random, LogListener logListener)
		{
			if (this.IsEmpty)
			{
				return this;
			}
			this.ThrowIfReconstructed("Prune");
			int? k = pruningRequest.K;
			int? randomK = pruningRequest.RandomK;
			int? k2 = this.PruningRequest.K;
			int? randomK2 = this.PruningRequest.RandomK;
			IEnumerable<ProgramNode> enumerable2;
			if (k == null || this.PruningRequest.TopProgramsFeature == pruningRequest.TopProgramsFeature)
			{
				if (k == null || k2 == null || k.Value >= k2.Value)
				{
					IEnumerable<ProgramNode> enumerable = this.TopPrograms;
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = this.TopPrograms.TakeKDistinctOn((ProgramNode p) => p.GetFeatureValue(pruningRequest.TopProgramsFeature, fcc), k.Value, null);
				}
			}
			else
			{
				enumerable2 = base.TopK(this.TopPrograms, pruningRequest.TopProgramsFeature, k.Value, fcc, logListener);
			}
			IEnumerable<ProgramNode> enumerable3 = enumerable2;
			IEnumerable<ProgramNode> enumerable4;
			if (randomK == null || randomK2 == null || randomK.Value >= randomK2.Value)
			{
				IEnumerable<ProgramNode> enumerable = this.RandomlySampledPrograms;
				enumerable4 = enumerable;
			}
			else
			{
				enumerable4 = this.RandomlySampledPrograms.RandomlySample(random, randomK.Value);
			}
			IEnumerable<ProgramNode> enumerable5 = enumerable4;
			if (enumerable3 == this.TopPrograms && this.RandomlySampledPrograms == enumerable5)
			{
				return this;
			}
			return new PrunedProgramSet(base.Symbol, pruningRequest, this.FeatureCalculationContext, enumerable3, enumerable5);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0002C798 File Offset: 0x0002A998
		public override XElement ToXML(Dictionary<object, int> identityCache = null, params IFeature[] featureCalculators)
		{
			Func<ProgramNode, XAttribute>[] array = featureCalculators.GetAttributeCalculators(identityCache).ToArray<Func<ProgramNode, XAttribute>>();
			return new XElement("Pruned", new object[]
			{
				this.TopPrograms.CollectionToXML("TopPrograms", "Program", ObjectFormatting.ToString, null, array),
				this.RandomlySampledPrograms.CollectionToXML("RandomlySampledPrograms", "Program", ObjectFormatting.ToString, null, array)
			}).WithAttribute("symbol", base.Symbol).WithAttribute("size", base.Size);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002C824 File Offset: 0x0002AA24
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache)
		{
			return new XElement("Pruned", new object[]
			{
				new XElement("TopPrograms", this.TopPrograms.Select((ProgramNode p) => p.ToInternedXML(identityCache))),
				new XElement("RandomPrograms", this.RandomlySampledPrograms.Select((ProgramNode p) => p.ToInternedXML(identityCache)))
			});
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		internal new static DirectProgramSet InternedDeserialize(XElement node, Symbol resolvedSymbol, Grammar grammar, Dictionary<int, object> identityCache)
		{
			if (node.Name.LocalName != "Pruned")
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			XElement xelement = node.Element("TopPrograms");
			IEnumerable<ProgramNode> enumerable = ((xelement != null) ? (from p in xelement.Elements()
				select ProgramNode.FromInternedXML(p, grammar, identityCache)) : null);
			XElement xelement2 = node.Element("RandomPrograms");
			IEnumerable<ProgramNode> enumerable2 = ((xelement2 != null) ? (from p in xelement2.Elements()
				select ProgramNode.FromInternedXML(p, grammar, identityCache)) : null);
			if (enumerable == null || enumerable2 == null)
			{
				throw new ArgumentException("Invalid XML encountered during DeserializeFromXML().");
			}
			return new PrunedProgramSet(resolvedSymbol, enumerable, enumerable2);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002C960 File Offset: 0x0002AB60
		protected override bool StructuralEqualsImpl(ProgramSet other)
		{
			PrunedProgramSet prunedProgramSet = (PrunedProgramSet)other;
			return this.TopPrograms.ConvertToHashSet<ProgramNode>().SetEquals(prunedProgramSet.TopPrograms) && this.RandomlySampledPrograms.ConvertToHashSet<ProgramNode>().SetEquals(prunedProgramSet.RandomlySampledPrograms);
		}

		// Token: 0x0400076B RID: 1899
		private static readonly IReadOnlyList<ProgramNode> EmptyProgramList = new ProgramNode[0];

		// Token: 0x0400076C RID: 1900
		internal new const string XMLKey = "Pruned";
	}
}
