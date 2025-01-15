using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000413 RID: 1043
	public sealed class DictCountTableBuilder : ICountTableBuilder
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x0007F510 File Offset: 0x0007D710
		public DictCountTableBuilder(DictCountTableBuilder.Arguments args, IHostEnvironment env, long labelCardinality)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("DictCountTableBuilder");
			Contracts.CheckValue<DictCountTableBuilder.Arguments>(this._host, args, "args");
			Contracts.Check(this._host, 0L <= labelCardinality && labelCardinality < 100L, "Label cardinality out of bounds");
			Contracts.CheckUserArg(this._host, args.garbageThreshold >= 0f, "garbageThreshold", "Garbage threshold must be non-negative");
			this._labelCardinality = (int)labelCardinality;
			this._tables = new Dictionary<long, double>[this._labelCardinality];
			for (int i = 0; i < this._labelCardinality; i++)
			{
				this._tables[i] = new Dictionary<long, double>();
			}
			this._priorCounts = new double[this._labelCardinality];
			this._garbageThreshold = args.garbageThreshold;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0007F5E8 File Offset: 0x0007D7E8
		public double Increment(long key, long labelKey, double amount)
		{
			Contracts.Check(this._host, 0L <= labelKey && labelKey < (long)this._labelCardinality, "Invalid LabelKey");
			this._priorCounts[(int)(checked((IntPtr)labelKey))] += amount;
			double num;
			if (!this._tables[(int)(checked((IntPtr)labelKey))].TryGetValue(key, out num))
			{
				num = 0.0;
			}
			this._tables[(int)(checked((IntPtr)labelKey))][key] = num + amount;
			return num;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0007F664 File Offset: 0x0007D864
		public void InsertOrUpdateRawCounts(int hashId, long hashValue, float[] counts)
		{
			Contracts.Check(this._host, Utils.Size<float>(counts) == this._labelCardinality);
			Contracts.Check(this._host, hashId == 0, "Dict count table can only have zero as hash id");
			for (int i = 0; i < this._labelCardinality; i++)
			{
				if (counts[i] > 0f)
				{
					this._tables[i][hashValue] = (double)counts[i];
				}
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0007F6D0 File Offset: 0x0007D8D0
		public ICountTable CreateCountTable()
		{
			float[] array = this._priorCounts.Select((double x) => (float)x).ToArray<float>();
			Dictionary<long, float>[] array2 = new Dictionary<long, float>[this._labelCardinality];
			for (int i = 0; i < this._labelCardinality; i++)
			{
				array2[i] = new Dictionary<long, float>();
			}
			float[] array3 = null;
			if (this._garbageThreshold > 0f)
			{
				this.ProcessGarbage(array2, out array3);
			}
			else
			{
				for (int j = 0; j < this._labelCardinality; j++)
				{
					Dictionary<long, float> dictionary = array2[j];
					Dictionary<long, double> dictionary2 = this._tables[j];
					foreach (KeyValuePair<long, double> keyValuePair in dictionary2)
					{
						dictionary[keyValuePair.Key] = (float)keyValuePair.Value;
					}
				}
			}
			return new DictCountTable(this._host, array2, this._labelCardinality, array, this._garbageThreshold, array3);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0007F7E4 File Offset: 0x0007D9E4
		private void ProcessGarbage(Dictionary<long, float>[] outputTables, out float[] outputGarbageCounts)
		{
			HashSet<long> hashSet = new HashSet<long>();
			foreach (Dictionary<long, double> dictionary in this._tables)
			{
				foreach (long num in dictionary.Keys)
				{
					hashSet.Add(num);
				}
			}
			double[] array = new double[this._labelCardinality];
			double[] array2 = new double[this._labelCardinality];
			foreach (long num2 in hashSet)
			{
				double num3 = 0.0;
				for (int j = 0; j < this._labelCardinality; j++)
				{
					if (!this._tables[j].TryGetValue(num2, out array[j]))
					{
						array[j] = 0.0;
					}
					num3 += array[j];
				}
				if (num3 <= (double)this._garbageThreshold)
				{
					for (int k = 0; k < this._labelCardinality; k++)
					{
						array2[k] += array[k];
					}
				}
				else
				{
					for (int l = 0; l < this._labelCardinality; l++)
					{
						if (array[l] > 0.0)
						{
							outputTables[l][num2] = (float)array[l];
						}
					}
				}
			}
			outputGarbageCounts = array2.Select((double x) => (float)x).ToArray<float>();
		}

		// Token: 0x04000D60 RID: 3424
		public const string LoaderSignature = "DictCountTableBuilder";

		// Token: 0x04000D61 RID: 3425
		private readonly IHost _host;

		// Token: 0x04000D62 RID: 3426
		private readonly int _labelCardinality;

		// Token: 0x04000D63 RID: 3427
		private readonly Dictionary<long, double>[] _tables;

		// Token: 0x04000D64 RID: 3428
		private readonly float _garbageThreshold;

		// Token: 0x04000D65 RID: 3429
		private readonly double[] _priorCounts;

		// Token: 0x02000414 RID: 1044
		public class Arguments
		{
			// Token: 0x04000D68 RID: 3432
			[Argument(0, HelpText = "Garbage threshold (counts below or equal to the threshold are assigned to the garbage bin)", ShortName = "gb")]
			public float garbageThreshold;
		}
	}
}
