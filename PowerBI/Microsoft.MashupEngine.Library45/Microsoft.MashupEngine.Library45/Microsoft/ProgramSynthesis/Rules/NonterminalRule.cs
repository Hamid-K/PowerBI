using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x02000399 RID: 921
	[DataContract]
	public abstract class NonterminalRule : GrammarRule, IJoinLanguage, ILanguage
	{
		// Token: 0x060014BD RID: 5309 RVA: 0x0003CBAF File Offset: 0x0003ADAF
		protected NonterminalRule(Symbol head, IEnumerable<Symbol> body)
			: base(head, body)
		{
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0003CBB9 File Offset: 0x0003ADB9
		protected NonterminalRule(Symbol head, params Symbol[] letBody)
			: this(head, letBody.AsEnumerable<Symbol>())
		{
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0003CBC8 File Offset: 0x0003ADC8
		public IEnumerable<ILanguage> JoinedLanguages
		{
			get
			{
				return base.Body;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00004FAE File Offset: 0x000031AE
		public NonterminalRule LanguageRule
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		public override IEnumerable<ProgramNode> AllElements
		{
			get
			{
				ProgramSet[] array = new ProgramSet[base.Body.Count];
				for (int i = 0; i < base.Body.Count; i++)
				{
					Optional<ProgramSet> optional = base.Body[i].TryGetAllPrograms(true, true);
					if (!optional.HasValue || ProgramSet.IsNullOrEmpty(optional.Value))
					{
						return Enumerable.Empty<ProgramNode>();
					}
					array[i] = optional.Value;
				}
				return ProgramSet.Join(this, array).RealizedPrograms;
			}
		}

		// Token: 0x060014C2 RID: 5314
		internal abstract object Evaluate(object[] args);

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003CC4B File Offset: 0x0003AE4B
		internal CodeBuilder FormatAST(IEnumerable<CodeBuilder> parameters)
		{
			return this.FormatAST(parameters, ASTSerializationSettings.HumanReadable);
		}

		// Token: 0x060014C4 RID: 5316
		internal abstract CodeBuilder FormatAST(IEnumerable<CodeBuilder> parameters, ASTSerializationSettings settings);

		// Token: 0x060014C5 RID: 5317
		internal abstract State ValidStateFromArgumentInvocations(params Record<State, object>[] argumentInvocations);

		// Token: 0x060014C6 RID: 5318
		internal abstract Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState);

		// Token: 0x060014C7 RID: 5319 RVA: 0x0003CC59 File Offset: 0x0003AE59
		public override ProgramNode BuildASTNode(object data, params ProgramNode[] children)
		{
			return new NonterminalNode(this, children);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0003CC64 File Offset: 0x0003AE64
		public override string ToString()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.Append(base.Head.Name);
			codeBuilder.Append(" := ");
			codeBuilder.Append(this.FormatAST(base.Body.Select((Symbol s) => CodeBuilder.Create(s.ToString()))));
			return codeBuilder.GetCode();
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003CCCE File Offset: 0x0003AECE
		internal virtual IEnumerable<Symbol> RequiredChildrenForFeatureCalculator(FeatureCalculator fc)
		{
			if (fc is CustomFeatureCalculator.FromChildrenFeatureValues)
			{
				return base.Body;
			}
			return Enumerable.Empty<Symbol>();
		}

		// Token: 0x060014CA RID: 5322
		internal abstract IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null);

		// Token: 0x060014CB RID: 5323 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
		internal static IReadOnlyList<int> ComputeParamK(int k, int arity, IEnumerable<BigInteger> paramSpaceSizes)
		{
			int[] array = new int[arity];
			var enumerable = from space in paramSpaceSizes.Select((BigInteger size, int index) => new { size, index })
				orderby space.size
				select space;
			double num = Math.Ceiling(Math.Pow(2.0, (double)arity) * (double)k);
			int num2 = arity;
			foreach (var <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType in enumerable)
			{
				int num3 = Math.Min(k, (int)Math.Ceiling(Math.Pow(num, 1.0 / (double)num2)));
				if (<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.size >= (long)num3)
				{
					for (int i = 0; i < arity; i++)
					{
						array[i] = ((array[i] == 0) ? num3 : array[i]);
					}
					break;
				}
				array[<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.index] = (int)<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.size;
				num = Math.Ceiling(num / (double)<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.size);
				num2--;
			}
			return array;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0003CE1C File Offset: 0x0003B01C
		internal static Record<int, int> ComputeParamK(int k, BigInteger sizeLeft, BigInteger sizeRight)
		{
			IReadOnlyList<int> readOnlyList = NonterminalRule.ComputeParamK(k, 2, new BigInteger[] { sizeLeft, sizeRight });
			return Record.Create<int, int>(readOnlyList[0], readOnlyList[1]);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0003CE5C File Offset: 0x0003B05C
		internal static IEnumerable<ProgramNode> GenericTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			NonterminalRule rule = programSet.LanguageRule;
			IReadOnlyList<int> readOnlyList = NonterminalRule.ComputeParamK(k, programSet.ParameterSpaces.Length, programSet.ParameterSpaces.Select((ProgramSet space) => space.Size));
			return from args in programSet.ParameterSpaces.ZipWith(readOnlyList).Select((Record<ProgramSet, int> psWithParamK, int idx) => psWithParamK.Item1.TopK(feature.GetExternFeature(rule, idx), psWithParamK.Item2, fcc, logListener)).CartesianProduct<ProgramNode>()
				select rule.BuildASTNode(args.ToArray<ProgramNode>());
		}
	}
}
