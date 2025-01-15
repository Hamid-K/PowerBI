using System;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200040E RID: 1038
	public sealed class CMCountTableBuilder : ICountTableBuilder
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x0007EC60 File Offset: 0x0007CE60
		public CMCountTableBuilder(CMCountTableBuilder.Arguments args, IHostEnvironment env, long labelCardinality)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("CMCountTableBuilder");
			Contracts.CheckValue<CMCountTableBuilder.Arguments>(this._host, args, "args");
			Contracts.Check(this._host, labelCardinality > 0L, "Label cardinality must be positive");
			this._labelCardinality = (int)labelCardinality;
			Contracts.Check(this._host, 0 < args.depth && args.depth < 101, "Depth out of range");
			this._depth = args.depth;
			Contracts.Check(this._host, 0 < args.width, "Width out of range");
			this._width = args.width;
			this._tables = new double[this._labelCardinality][][];
			for (int i = 0; i < this._labelCardinality; i++)
			{
				this._tables[i] = new double[this._depth][];
				for (int j = 0; j < this._depth; j++)
				{
					this._tables[i][j] = new double[this._width];
				}
			}
			this._priorCounts = new double[this._labelCardinality];
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0007ED80 File Offset: 0x0007CF80
		public double Increment(long key, long labelKey, double amount)
		{
			Contracts.Check(this._host, 0L <= labelKey && labelKey < (long)this._labelCardinality);
			this._priorCounts[(int)(checked((IntPtr)labelKey))] += amount;
			uint num = Hashing.MurmurRound((uint)(key >> 32), (uint)key);
			double num2 = double.MaxValue;
			for (int i = 0; i < this._depth; i++)
			{
				int num3 = (int)((ulong)Hashing.MixHash(Hashing.MurmurRound(num, (uint)i)) % (ulong)((long)this._width));
				double num4 = this._tables[(int)(checked((IntPtr)labelKey))][i][num3];
				if (num2 > num4)
				{
					num2 = num4;
				}
				this._tables[(int)(checked((IntPtr)labelKey))][i][num3] += amount;
			}
			return num2;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0007EE3C File Offset: 0x0007D03C
		public void InsertOrUpdateRawCounts(int hashId, long hashValue, float[] counts)
		{
			Contracts.Check(this._host, Utils.Size<float>(counts) == this._labelCardinality);
			Contracts.Check(this._host, hashId >= 0 && hashId < this._depth);
			Contracts.Check(this._host, hashValue >= 0L && hashValue < (long)this._width);
			for (int i = 0; i < this._labelCardinality; i++)
			{
				if (counts[i] >= 0f)
				{
					this._tables[i][hashId][(int)(checked((IntPtr)hashValue))] = (double)counts[i];
				}
			}
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0007EED0 File Offset: 0x0007D0D0
		public ICountTable CreateCountTable()
		{
			float[] array = this._priorCounts.Select((double x) => (float)x).ToArray<float>();
			float[][][] array2 = new float[this._labelCardinality][][];
			for (int i = 0; i < this._labelCardinality; i++)
			{
				array2[i] = new float[this._depth][];
				for (int j = 0; j < this._depth; j++)
				{
					array2[i][j] = this._tables[i][j].Select((double input) => (float)input).ToArray<float>();
				}
			}
			return new CMCountTable(this._host, array2, array);
		}

		// Token: 0x04000D4F RID: 3407
		private const int DepthLim = 101;

		// Token: 0x04000D50 RID: 3408
		public const string LoaderSignature = "CMCountTableBuilder";

		// Token: 0x04000D51 RID: 3409
		private readonly IHost _host;

		// Token: 0x04000D52 RID: 3410
		private readonly double[][][] _tables;

		// Token: 0x04000D53 RID: 3411
		private readonly int _labelCardinality;

		// Token: 0x04000D54 RID: 3412
		private readonly int _depth;

		// Token: 0x04000D55 RID: 3413
		private readonly int _width;

		// Token: 0x04000D56 RID: 3414
		private readonly double[] _priorCounts;

		// Token: 0x0200040F RID: 1039
		public class Arguments
		{
			// Token: 0x04000D59 RID: 3417
			[Argument(0, HelpText = "Count-Min Sketch table depth", ShortName = "d")]
			public int depth = 4;

			// Token: 0x04000D5A RID: 3418
			[Argument(0, HelpText = "Count-Min Sketch width", ShortName = "w")]
			public int width = 8388608;
		}
	}
}
