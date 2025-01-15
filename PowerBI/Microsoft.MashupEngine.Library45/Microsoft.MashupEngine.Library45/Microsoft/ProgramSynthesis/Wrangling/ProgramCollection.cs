using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B9 RID: 185
	public class ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> : IEnumerable<TProgram>, IEnumerable where TProgram : Program<TInput, TOutput> where TFeatureValue : IComparable
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000E41B File Offset: 0x0000C61B
		public IReadOnlyList<TProgram> TopPrograms { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000E423 File Offset: 0x0000C623
		public IReadOnlyList<TProgram> RandomPrograms { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000E42B File Offset: 0x0000C62B
		public FeatureCalculationContext FeatureCalculationContext { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000E433 File Offset: 0x0000C633
		public Feature<TFeatureValue> Feature { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000E43B File Offset: 0x0000C63B
		private static IReadOnlyList<TProgram> EmptyProgramList { get; } = new TProgram[0];

		// Token: 0x06000429 RID: 1065 RVA: 0x0000E442 File Offset: 0x0000C642
		private ProgramCollection()
		{
			this.TopPrograms = ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>.EmptyProgramList;
			this.RandomPrograms = ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>.EmptyProgramList;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000E460 File Offset: 0x0000C660
		public bool IsEmpty
		{
			get
			{
				return this.TopPrograms.Count == 0 && this.RandomPrograms.Count == 0;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000E47F File Offset: 0x0000C67F
		public static ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> Empty { get; } = new ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>();

		// Token: 0x0600042C RID: 1068 RVA: 0x0000E488 File Offset: 0x0000C688
		public ProgramCollection(IEnumerable<TProgram> topPrograms, IEnumerable<TProgram> randomPrograms, Feature<TFeatureValue> feature, FeatureCalculationContext fcc)
		{
			IReadOnlyList<TProgram> readOnlyList = ((topPrograms != null) ? topPrograms.ToList<TProgram>() : null);
			this.TopPrograms = readOnlyList ?? ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>.EmptyProgramList;
			readOnlyList = ((randomPrograms != null) ? randomPrograms.ToList<TProgram>() : null);
			this.RandomPrograms = readOnlyList ?? ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>.EmptyProgramList;
			this.Feature = feature;
			this.FeatureCalculationContext = fcc;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public IEnumerator<TProgram> GetEnumerator()
		{
			return this.TopPrograms.Concat(this.RandomPrograms).GetEnumerator();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000E504 File Offset: 0x0000C704
		public TFeatureValue GetFeatureValueForProgram(TProgram program)
		{
			if (this.Feature == null)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot call {0} on a {1} object which has a null value for its {2} property.", new object[]
				{
					"GetFeatureValueForProgram",
					base.GetType(),
					"Feature"
				})));
			}
			return program.ProgramNode.GetFeatureValue<TFeatureValue>(this.Feature, this.FeatureCalculationContext);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000E569 File Offset: 0x0000C769
		public static ProgramCollection<TProgram, TInput, TOutput, TFeatureValue> From(PrunedProgramSet prunedProgramSet, Func<ProgramNode, TProgram> programFactory, Feature<TFeatureValue> feature)
		{
			return new ProgramCollection<TProgram, TInput, TOutput, TFeatureValue>(prunedProgramSet.TopPrograms.Select(programFactory), prunedProgramSet.RandomlySampledPrograms.Select(programFactory), feature, prunedProgramSet.FeatureCalculationContext);
		}
	}
}
