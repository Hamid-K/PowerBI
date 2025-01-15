using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Features.InputTransformation;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003B9 RID: 953
	internal static class ConceptTopKStreamHelper
	{
		// Token: 0x06001560 RID: 5472 RVA: 0x0003E83C File Offset: 0x0003CA3C
		internal static IEnumerable<ProgramNode> SequenceTransformerTopKStream(JoinProgramSet programSet, IFeature feature, int sequenceGeneratorIndex, int sequenceTransformerIndex, Func<ProgramNode, ProgramNode, ProgramNode> builder, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			if (fcc == null || fcc.ReferenceSpecInputs.Count == 0)
			{
				return NonterminalRule.GenericTopKStream(programSet, feature, k, fcc, logListener);
			}
			int num;
			int num2;
			NonterminalRule.ComputeParamK(k, programSet.ParameterSpaces[sequenceGeneratorIndex].Size, programSet.ParameterSpaces[sequenceTransformerIndex].Size).Deconstruct(out num, out num2);
			int num3 = num;
			int num4 = num2;
			NonterminalRule languageRule = programSet.LanguageRule;
			IEnumerable<Record<ProgramNode, ProgramNode>> enumerable = Enumerable.Empty<Record<ProgramNode, ProgramNode>>();
			using (IEnumerator<ProgramNode> enumerator = programSet.ParameterSpaces[sequenceGeneratorIndex].TopK(feature.GetExternFeature(languageRule, sequenceGeneratorIndex), num3, fcc, logListener).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProgramNode generator = enumerator.Current;
					FeatureCalculationContext featureCalculationContext = fcc.WithAdditionalTransform(new SequenceTransformConceptLambdaInputTransformer(generator, null));
					IEnumerable<ProgramNode> enumerable2 = programSet.ParameterSpaces[sequenceTransformerIndex].TopK(feature.GetExternFeature(languageRule, sequenceTransformerIndex), num4, featureCalculationContext, logListener);
					enumerable = enumerable.Concat(enumerable2.Select((ProgramNode p) => Record.Create<ProgramNode, ProgramNode>(generator, p)));
				}
			}
			return from generatorAndTransformer in enumerable.Distinct<Record<ProgramNode, ProgramNode>>()
				select builder(generatorAndTransformer.Item1, generatorAndTransformer.Item2);
		}
	}
}
