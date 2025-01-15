using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000BA RID: 186
	public abstract class ProgramLearner<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		public bool SupportsProgramSampling { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000E5AE File Offset: 0x0000C7AE
		public bool SupportsArbitraryFeatures { get; }

		// Token: 0x06000434 RID: 1076 RVA: 0x0000E5B6 File Offset: 0x0000C7B6
		protected ProgramLearner(bool supportsProgramSampling, bool supportsArbitraryFeatures)
		{
			this.SupportsProgramSampling = supportsProgramSampling;
			this.SupportsArbitraryFeatures = supportsArbitraryFeatures;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		public TProgram Learn(IEnumerable<Constraint<TInput, TOutput>> constraints, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.Learn<double>(constraints, this.ScoreFeature, additionalInputs, cancel);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public TProgram Learn<TFeatureValue>(IEnumerable<Constraint<TInput, TOutput>> constraints, Feature<TFeatureValue> feature, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken)) where TFeatureValue : IComparable
		{
			return this.LearnTopKImpl<TFeatureValue>(constraints, feature, 1, null, ProgramSamplingStrategy.UniformRandom, additionalInputs, cancel).FirstOrDefault<TProgram>();
		}

		// Token: 0x06000437 RID: 1079
		protected abstract ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<TInput, TOutput>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null) where TFeatureValue : IComparable;

		// Token: 0x06000438 RID: 1080 RVA: 0x0000E608 File Offset: 0x0000C808
		private static IEnumerable<TProgram> CheckSoundness(IEnumerable<TProgram> programs, IReadOnlyCollection<Constraint<TInput, TOutput>> constraints, IReadOnlyCollection<TInput> inputs)
		{
			if (programs == null)
			{
				return null;
			}
			return programs.Where(delegate(TProgram program)
			{
				foreach (Constraint<TInput, TOutput> constraint in constraints)
				{
					if (!constraint.IsSoft && !constraint.Valid(program, inputs, constraints))
					{
						return false;
					}
				}
				return true;
			});
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000E640 File Offset: 0x0000C840
		public ProgramCollection<TProgram, TInput, TOutput, double> LearnTopK(IEnumerable<Constraint<TInput, TOutput>> constraints, int k, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnTopK(constraints, k, null, ProgramSamplingStrategy.UniformRandom, additionalInputs, cancel);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000E664 File Offset: 0x0000C864
		public ProgramCollection<TProgram, TInput, TOutput, double> LearnTopK(IEnumerable<Constraint<TInput, TOutput>> constraints, int k, TimeSpan timeout, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancel }))
			{
				Task<ProgramCollection<TProgram, TInput, TOutput, double>> task = Task.Run<ProgramCollection<TProgram, TInput, TOutput, double>>(() => this.LearnTopK(constraints, k, additionalInputs, cancel));
				if (task.Wait(timeout))
				{
					return task.Result;
				}
				cancellationTokenSource.Cancel();
			}
			return ProgramCollection<TProgram, TInput, TOutput, double>.Empty;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000E704 File Offset: 0x0000C904
		private ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> LearnTopKImpl<TFeatureValue>(IEnumerable<Constraint<TInput, TOutput>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken)) where TFeatureValue : IComparable
		{
			IReadOnlyCollection<Constraint<TInput, TOutput>> readOnlyCollection = (constraints as IReadOnlyCollection<Constraint<TInput, TOutput>>) ?? ((constraints != null) ? constraints.ToList<Constraint<TInput, TOutput>>() : null);
			IReadOnlyCollection<TInput> readOnlyCollection2 = (additionalInputs as IReadOnlyCollection<TInput>) ?? ((additionalInputs != null) ? additionalInputs.ToList<TInput>() : null);
			ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> programCollection = this.LearnTopKUnchecked<TFeatureValue>(readOnlyCollection, feature, k, numRandomProgramsToInclude, samplingStrategy, readOnlyCollection2, cancel, null);
			if (readOnlyCollection == null)
			{
				return programCollection;
			}
			IEnumerable<TInput>[] array = new IEnumerable<TInput>[3];
			array[0] = from c in readOnlyCollection.OfType<ConstraintOnInput<TInput, TOutput>>()
				select c.Input;
			array[1] = readOnlyCollection2;
			array[2] = readOnlyCollection.SelectMany((Constraint<TInput, TOutput> c) => c.PossibleInput.AsEnumerable<TInput>());
			List<TInput> list = Seq.Of<IEnumerable<TInput>>(array).Collect<IEnumerable<TInput>>().SelectMany((IEnumerable<TInput> l) => l)
				.ToList<TInput>();
			IEnumerable<TProgram> enumerable = ProgramLearner<TProgram, TInput, TOutput>.CheckSoundness(programCollection.TopPrograms, readOnlyCollection, list).ToList<TProgram>();
			List<TProgram> list2 = ProgramLearner<TProgram, TInput, TOutput>.CheckSoundness(programCollection.RandomPrograms, readOnlyCollection, list).ToList<TProgram>();
			return new ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>(enumerable, list2, programCollection.Feature, programCollection.FeatureCalculationContext);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000E82E File Offset: 0x0000CA2E
		public ProgramCollection<TProgram, TInput, TOutput, double> LearnTopK(IEnumerable<Constraint<TInput, TOutput>> constraints, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			return this.LearnTopK<double>(constraints, this.ScoreFeature, k, numRandomProgramsToInclude, samplingStrategy, additionalInputs, cancel);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000E845 File Offset: 0x0000CA45
		public ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> LearnTopK<TFeatureValue>(IEnumerable<Constraint<TInput, TOutput>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken)) where TFeatureValue : IComparable
		{
			ProgramLearner<TProgram, TInput, TOutput>.ThrowIfZeroOrNegative(k, "k", "LearnTopK");
			ProgramLearner<TProgram, TInput, TOutput>.ThrowIfZeroOrNegative(numRandomProgramsToInclude, "numRandomProgramsToInclude", "LearnTopK");
			return this.LearnTopKImpl<TFeatureValue>(constraints, feature, k, numRandomProgramsToInclude, samplingStrategy, additionalInputs, cancel);
		}

		// Token: 0x0600043E RID: 1086
		public abstract ProgramSet LearnAll(IEnumerable<Constraint<TInput, TOutput>> constraints, IEnumerable<TInput> additionalInputs = null, CancellationToken cancel = default(CancellationToken));

		// Token: 0x0600043F RID: 1087 RVA: 0x00002188 File Offset: 0x00000388
		public virtual IFeatureOptions GetFeatureOptionsFor(IEnumerable<Constraint<TInput, TOutput>> constraints, IEnumerable<TInput> additionalInputs = null)
		{
			return null;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000E879 File Offset: 0x0000CA79
		public virtual Constraint<TInput, TOutput> BuildPositiveConstraint(TInput input, TOutput output, bool isSoft)
		{
			return new Example<TInput, TOutput>(input, output, isSoft);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000E883 File Offset: 0x0000CA83
		public virtual Constraint<TInput, TOutput> BuildNegativeConstraint(TInput input, TOutput output, bool isSoft)
		{
			return new DoesNotEqual<TInput, TOutput>(input, output, isSoft);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000E88D File Offset: 0x0000CA8D
		protected static void ThrowIfZeroOrNegative(int value, string parameterName, string methodName)
		{
			if (value <= 0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("{0} must be greater than zero in call to {1}.", new object[] { parameterName, methodName })));
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000E8B6 File Offset: 0x0000CAB6
		protected static void ThrowIfZeroOrNegative(int? value, string parameterName, string methodName)
		{
			if (value != null && value.Value <= 0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("{0} must be either null or greater than zero in call to {1}.", new object[] { parameterName, methodName })));
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000444 RID: 1092
		public abstract Feature<double> ScoreFeature { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000E8EE File Offset: 0x0000CAEE
		public virtual SpecSerializationContext SpecSerializationContext
		{
			get
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Serialization context not implemented in learner: {0}.", new object[] { base.GetType() })));
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000E913 File Offset: 0x0000CB13
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000E91B File Offset: 0x0000CB1B
		public IEnumerable<Spec> SpecsGeneratedFromLastLearn { get; protected set; }
	}
}
