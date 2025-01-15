using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200008A RID: 138
	internal class WeakConcurrentRandom
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00013421 File Offset: 0x00011621
		public WeakConcurrentRandom()
		{
			this.Initialize();
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001342F File Offset: 0x0001162F
		public static WeakConcurrentRandom Instance
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (WeakConcurrentRandom.random != null)
				{
					return WeakConcurrentRandom.random;
				}
				Interlocked.CompareExchange<WeakConcurrentRandom>(ref WeakConcurrentRandom.random, new WeakConcurrentRandom(), null);
				return WeakConcurrentRandom.random;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00013454 File Offset: 0x00011654
		public void Initialize()
		{
			this.Initialize((ulong seed) => new XorshiftRandomBatchGenerator(seed), 3, 10);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013480 File Offset: 0x00011680
		public void Initialize(Func<ulong, IRandomNumberBatchGenerator> randomGeneratorFactory, int segmentIndexBits, int segmentBits)
		{
			int num = segmentIndexBits;
			if (segmentIndexBits < 1 || segmentIndexBits > 4)
			{
				num = 3;
			}
			int num2 = segmentBits;
			if (segmentBits < 7 || segmentBits > 15)
			{
				num2 = 9;
			}
			this.bitsToStoreRandomIndexWithinSegment = num2;
			this.segmentCount = 1 << num;
			this.segmentSize = 1 << num2;
			this.segmentIndexMask = this.segmentCount - 1 << this.bitsToStoreRandomIndexWithinSegment;
			this.randomIndexWithinSegmentMask = this.segmentSize - 1;
			this.randomArrayIndexMask = this.segmentIndexMask | this.randomIndexWithinSegmentMask;
			int num3 = this.segmentCount * this.segmentSize;
			this.randomGemerators = new IRandomNumberBatchGenerator[this.segmentCount];
			XorshiftRandomBatchGenerator xorshiftRandomBatchGenerator = new XorshiftRandomBatchGenerator((ulong)((long)Environment.TickCount));
			ulong[] array = new ulong[this.segmentCount];
			xorshiftRandomBatchGenerator.NextBatch(array, 0, this.segmentCount);
			for (int i = 0; i < this.segmentCount; i++)
			{
				Func<ulong, IRandomNumberBatchGenerator> func = (ulong seed) => new XorshiftRandomBatchGenerator(seed);
				IRandomNumberBatchGenerator randomNumberBatchGenerator = ((randomGeneratorFactory == null) ? func(array[i]) : (randomGeneratorFactory(array[i]) ?? func(array[i])));
				this.randomGemerators[i] = randomNumberBatchGenerator;
			}
			this.randomNumbers = new ulong[num3];
			xorshiftRandomBatchGenerator.NextBatch(this.randomNumbers, 0, num3);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000135D0 File Offset: 0x000117D0
		public ulong Next()
		{
			int num = Interlocked.Increment(ref this.index);
			if ((num & this.randomIndexWithinSegmentMask) == 0)
			{
				this.RegenerateSegment(num);
			}
			return this.randomNumbers[num & this.randomArrayIndexMask];
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001360C File Offset: 0x0001180C
		private void RegenerateSegment(int newIndex)
		{
			int num;
			if ((newIndex & this.segmentIndexMask) == 0)
			{
				num = this.segmentCount - 1;
			}
			else
			{
				num = ((newIndex & this.segmentIndexMask) >> this.bitsToStoreRandomIndexWithinSegment) - 1;
			}
			this.randomGemerators[num].NextBatch(this.randomNumbers, num * this.segmentSize, this.segmentSize);
		}

		// Token: 0x040001AF RID: 431
		private static WeakConcurrentRandom random;

		// Token: 0x040001B0 RID: 432
		private int index;

		// Token: 0x040001B1 RID: 433
		private int segmentCount;

		// Token: 0x040001B2 RID: 434
		private int segmentSize;

		// Token: 0x040001B3 RID: 435
		private int bitsToStoreRandomIndexWithinSegment;

		// Token: 0x040001B4 RID: 436
		private int segmentIndexMask;

		// Token: 0x040001B5 RID: 437
		private int randomIndexWithinSegmentMask;

		// Token: 0x040001B6 RID: 438
		private int randomArrayIndexMask;

		// Token: 0x040001B7 RID: 439
		private IRandomNumberBatchGenerator[] randomGemerators;

		// Token: 0x040001B8 RID: 440
		private ulong[] randomNumbers;
	}
}
