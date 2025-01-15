using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Wrangling.SignificantInputs
{
	// Token: 0x02000103 RID: 259
	internal class SignificantInputsEngine<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x000135B8 File Offset: 0x000117B8
		internal SignificantInputsEngine(SignificantInputProgramProfile<TProgram, TInput, TOutput> programProfile, IImmutableList<TInput> inputs, IReadOnlyList<Distinguisher<TInput>> distinguishers, CancellationToken cancellationToken)
		{
			Record<Func<int, bool>, IReadOnlyList<TProgram>> record = programProfile.RankedPrograms();
			this._isTopProgram = record.Item1;
			this._programs = record.Item2;
			this._inputs = inputs;
			this._outputTable = SignificantInputsEngine<TProgram, TInput, TOutput>.ConstructOutputTable(this._programs, inputs, cancellationToken);
			this._distinguishers = distinguishers;
			this._distinguisherTable = SignificantInputsEngine<TProgram, TInput, TOutput>.ConstructDistinguisherTable(distinguishers, inputs, cancellationToken);
			this._programRepresentatives = this.ComputeProgramRepresentatives();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00013628 File Offset: 0x00011828
		private static int[][] ConstructOutputTable(IReadOnlyList<TProgram> programs, IReadOnlyList<TInput> inputs, CancellationToken cancellationToken)
		{
			Dictionary<TOutput, int> dictionary = new Dictionary<TOutput, int>(ValueEquality<TOutput>.Instance);
			int[][] array = new int[programs.Count][];
			foreach (Record<int, TProgram> record in programs.Enumerate<TProgram>())
			{
				int num;
				TProgram tprogram;
				record.Deconstruct(out num, out tprogram);
				int num2 = num;
				TProgram tprogram2 = tprogram;
				array[num2] = new int[inputs.Count];
				foreach (Record<int, TInput> record2 in inputs.Enumerate<TInput>())
				{
					TInput tinput;
					record2.Deconstruct(out num, out tinput);
					int num3 = num;
					TInput tinput2 = tinput;
					cancellationToken.ThrowIfCancellationRequested();
					TOutput toutput = tprogram2.Run(tinput2);
					array[num2][num3] = ((toutput == null) ? 0 : dictionary.GetOrAdd(toutput, dictionary.Count + 1));
				}
			}
			return array;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001372C File Offset: 0x0001192C
		private static int[][] ConstructDistinguisherTable(IReadOnlyList<Distinguisher<TInput>> distinguishers, IReadOnlyList<TInput> inputs, CancellationToken cancellationToken)
		{
			int[][] array = new int[distinguishers.Count][];
			foreach (Record<int, Distinguisher<TInput>> record in distinguishers.Enumerate<Distinguisher<TInput>>())
			{
				int num;
				Distinguisher<TInput> distinguisher;
				record.Deconstruct(out num, out distinguisher);
				int num2 = num;
				Distinguisher<TInput> distinguisher2 = distinguisher;
				array[num2] = new int[inputs.Count];
				foreach (Record<int, TInput> record2 in inputs.Enumerate<TInput>())
				{
					TInput tinput;
					record2.Deconstruct(out num, out tinput);
					int num3 = num;
					TInput tinput2 = tinput;
					cancellationToken.ThrowIfCancellationRequested();
					uint? num4 = distinguisher2.ChoiceFor(tinput2);
					array[num2][num3] = (int)((num4 == null) ? uint.MaxValue : num4.Value);
				}
			}
			return array;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00013818 File Offset: 0x00011A18
		private IReadOnlyList<int> ComputeProgramRepresentatives()
		{
			return (from @group in this._programs.Indices<TProgram>().GroupBy((int i) => this._outputTable[i], new ArrayEquality<int>())
				select @group.First<int>()).ToList<int>();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00013870 File Offset: 0x00011A70
		private int[] InputSignature(int inputId)
		{
			Dictionary<int, int> firstProgramForOutput = new Dictionary<int, int>();
			IEnumerable<int> enumerable = this._programRepresentatives.Select((int programId) => base.<InputSignature>g__FirstProgramForOutput|0(this._outputTable[programId][inputId], programId));
			IEnumerable<int> enumerable2 = from x in this._distinguisherTable.Indices<int[]>()
				select this._distinguisherTable[x][inputId];
			return enumerable.Concat(enumerable2).ToArray<int>();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000138DC File Offset: 0x00011ADC
		private IDictionary<int, IEnumerable<int>> InputClusters()
		{
			return this._inputs.Indices<TInput>().GroupBy(new Func<int, int[]>(this.InputSignature), new ArrayEquality<int>()).ToDictionary((IGrouping<int[], int> group) => group.First<int>(), (IGrouping<int[], int> group) => group.AsEnumerable<int>());
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00013950 File Offset: 0x00011B50
		private IReadOnlyList<int> SignificantInputsCover(IReadOnlyList<int> inputRepresentatives, HashSet<TInput> constrainedInputs)
		{
			SignificantInputsEngine<TProgram, TInput, TOutput>.<>c__DisplayClass13_0 CS$<>8__locals1 = new SignificantInputsEngine<TProgram, TInput, TOutput>.<>c__DisplayClass13_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.constrainedInputs = constrainedInputs;
			CS$<>8__locals1.universeSize = 0;
			List<int> list = this._programRepresentatives.Where((int pi) => CS$<>8__locals1.<>4__this._isTopProgram(pi)).ToList<int>();
			IEnumerable<int> enumerable = this._programRepresentatives.Where((int pi) => !CS$<>8__locals1.<>4__this._isTopProgram(pi));
			IEnumerable<Record<int, int>> enumerable2 = list.UnorderedPairs(false);
			IEnumerable<Record<int, int>> enumerable3 = list.OrderedPairs(enumerable);
			IReadOnlyDictionary<Record<int, int>, int> readOnlyDictionary = enumerable2.Concat(enumerable3).ToDictionary((Record<int, int> pp) => pp, delegate(Record<int, int> _)
			{
				int universeSize = CS$<>8__locals1.universeSize;
				CS$<>8__locals1.universeSize = universeSize + 1;
				return universeSize;
			});
			int i;
			IReadOnlyDictionary<Record<int, int>, int> readOnlyDictionary2 = this._distinguishers.SelectMany((Distinguisher<TInput> distinguisher, int i) => distinguisher.Choices.Select((int c) => Record.Create<int, int>(i, c))).ToList<Record<int, int>>().ToDictionary((Record<int, int> k) => k, delegate(Record<int, int> k)
			{
				int universeSize2 = CS$<>8__locals1.universeSize;
				CS$<>8__locals1.universeSize = universeSize2 + 1;
				return universeSize2;
			});
			BitArray[] array = new BitArray[inputRepresentatives.Count];
			BitArray bitArray = new BitArray(CS$<>8__locals1.universeSize, false);
			for (i = 0; i < inputRepresentatives.Count; i++)
			{
				BitArray bitArray2 = new BitArray(CS$<>8__locals1.universeSize, false);
				int inputRep = inputRepresentatives[i];
				foreach (Record<int, int> record in readOnlyDictionary.Keys.Where((Record<int, int> pp) => CS$<>8__locals1.<>4__this._outputTable[pp.Item1][inputRep] != CS$<>8__locals1.<>4__this._outputTable[pp.Item2][inputRep]))
				{
					bitArray2[readOnlyDictionary[record]] = true;
				}
				foreach (Record<int, int> record2 in this._distinguisherTable.Select((int[] arr, int idx) => Record.Create<int, int>(idx, arr[inputRep])))
				{
					bitArray2[readOnlyDictionary2[record2]] = true;
				}
				array[i] = bitArray2;
				bitArray.Or(bitArray2);
			}
			for (int j = 0; j < inputRepresentatives.Count; j++)
			{
				int num = inputRepresentatives[j];
				if (CS$<>8__locals1.<SignificantInputsCover>g__IsFree|0(num))
				{
					bitArray.And(array[j].Clone().Not());
				}
			}
			List<int> list2 = new List<int>();
			while (bitArray.BitCount() > 0)
			{
				int num2 = -1;
				int num3 = -1;
				foreach (Record<int, BitArray> record3 in array.Enumerate<BitArray>())
				{
					int num4;
					BitArray bitArray3;
					record3.Deconstruct(out num4, out bitArray3);
					int num5 = num4;
					BitArray bitArray4 = bitArray3;
					bitArray4.And(bitArray);
					int num6 = bitArray4.BitCount();
					if (num6 > num2)
					{
						num2 = num6;
						num3 = num5;
					}
				}
				bitArray.And(array[num3].Clone().Not());
				list2.Add(inputRepresentatives[num3]);
			}
			return list2;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00013C80 File Offset: 0x00011E80
		public IReadOnlyList<SignificantInput<TInput>> SignificantInputClustersCover(NonInteractiveSession<TProgram, TInput, TOutput> session, HashSet<TInput> constrainedInputs)
		{
			SignificantInputsEngine<TProgram, TInput, TOutput>.<>c__DisplayClass14_0 CS$<>8__locals1 = new SignificantInputsEngine<TProgram, TInput, TOutput>.<>c__DisplayClass14_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.session = session;
			CS$<>8__locals1.inputClusters = this.InputClusters();
			IReadOnlyList<int> readOnlyList = CS$<>8__locals1.inputClusters.Keys.ToList<int>();
			return this.SignificantInputsCover(readOnlyList, constrainedInputs).Select(new Func<int, SignificantInput<TInput>>(CS$<>8__locals1.<SignificantInputClustersCover>g__ClusterFor|0)).ToList<SignificantInput<TInput>>();
		}

		// Token: 0x0400027A RID: 634
		private readonly int[][] _outputTable;

		// Token: 0x0400027B RID: 635
		private readonly int[][] _distinguisherTable;

		// Token: 0x0400027C RID: 636
		private readonly IReadOnlyList<TProgram> _programs;

		// Token: 0x0400027D RID: 637
		private readonly IReadOnlyList<Distinguisher<TInput>> _distinguishers;

		// Token: 0x0400027E RID: 638
		private readonly IImmutableList<TInput> _inputs;

		// Token: 0x0400027F RID: 639
		private readonly IReadOnlyList<int> _programRepresentatives;

		// Token: 0x04000280 RID: 640
		private readonly Func<int, bool> _isTopProgram;
	}
}
