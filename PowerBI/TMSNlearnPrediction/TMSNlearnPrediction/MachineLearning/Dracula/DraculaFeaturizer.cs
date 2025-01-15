using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000424 RID: 1060
	public sealed class DraculaFeaturizer : ICountFeaturizer, ICanSaveInBinaryFormat
	{
		// Token: 0x06001605 RID: 5637 RVA: 0x000803A0 File Offset: 0x0007E5A0
		public DraculaFeaturizer(DraculaFeaturizer.Arguments args, IHostEnvironment env, long labelBinCount, ICountTable countTable)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("DraculaFeaturizer");
			Contracts.CheckUserArg(this._host, labelBinCount > 1L, "labelBinCount", "Label bin count must be greater than 1");
			this._labelBinCount = (int)labelBinCount;
			this._logOddsCount = ((this._labelBinCount == 2) ? 1 : this._labelBinCount);
			this._numFeatures = this._labelBinCount + this._logOddsCount + 1;
			Contracts.CheckUserArg(this._host, args.priorCoefficient > 0f, "priorCoefficient", "Prior coefficient must be greater than zero");
			this._priorCoef = args.priorCoefficient;
			Contracts.CheckUserArg(this._host, args.laplaceScale >= 0f, "laplace", "Must be greater than or equal to zero.");
			this._laplaceScale = args.laplaceScale;
			this._seed = args.seed;
			this._garbageCounts = new float[this._labelBinCount];
			this._priorFrequencies = new double[this._labelBinCount];
			this._countsBuffers = new MadeObjectPool<float[]>(() => new float[this._labelBinCount]);
			this._countTable = countTable;
			this._garbageThreshold = this._countTable.GarbageThreshold;
			this.InitializePriors();
			this._rnd = new ThreadLocal<SysRandom>();
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00080500 File Offset: 0x0007E700
		public DraculaFeaturizer(ModelLoadContext ctx, IHostEnvironment env, ICountTable countTable)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("DraculaFeaturizer");
			Contracts.CheckValue<ModelLoadContext>(this._host, ctx, "ctx");
			if (ctx.Header.ModelVerReadable < 65538U)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num == 4);
			}
			this._labelBinCount = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._labelBinCount > 1, "labelBinCount");
			this._logOddsCount = ((this._labelBinCount == 2) ? 1 : this._labelBinCount);
			this._numFeatures = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._numFeatures == this._labelBinCount + this._logOddsCount + 1, "numFeatures");
			this._priorCoef = ctx.Reader.ReadSingle();
			Contracts.CheckDecode(this._host, this._priorCoef > 0f, "priorCoef");
			this._laplaceScale = ctx.Reader.ReadSingle();
			Contracts.CheckDecode(this._host, this._laplaceScale >= 0f, "laplaceScale");
			this._seed = ctx.Reader.ReadInt32();
			this._garbageCounts = new float[this._labelBinCount];
			this._priorFrequencies = new double[this._labelBinCount];
			this._countsBuffers = new MadeObjectPool<float[]>(() => new float[this._labelBinCount]);
			this._countTable = countTable;
			this._garbageThreshold = this._countTable.GarbageThreshold;
			this.InitializePriors();
			this._rnd = new ThreadLocal<SysRandom>();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000806C0 File Offset: 0x0007E8C0
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("DRCTFEAT", 65538U, 65538U, 65537U, "DraculaFeaturizer", null);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000806E1 File Offset: 0x0007E8E1
		public void SaveAsBinary(BinaryWriter writer)
		{
			ModelSaveContext.Save(writer, new Action<ModelSaveContext>(this.SaveCore));
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000806F8 File Offset: 0x0007E8F8
		private void SaveCore(ModelSaveContext ctx)
		{
			ctx.SetVersionInfo(DraculaFeaturizer.GetVersionInfo());
			ctx.Writer.Write(this._labelBinCount);
			ctx.Writer.Write(this._numFeatures);
			ctx.Writer.Write(this._priorCoef);
			ctx.Writer.Write(this._laplaceScale);
			ctx.Writer.Write(this._seed);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00080765 File Offset: 0x0007E965
		public int NumFeatures
		{
			get
			{
				return this._numFeatures;
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00080978 File Offset: 0x0007EB78
		public IEnumerable<string> GetFeatureNames(string[] classNames)
		{
			if (classNames == null)
			{
				classNames = new string[this._labelBinCount];
				for (int k = 0; k < this._labelBinCount; k++)
				{
					classNames[k] = string.Format("Class{0:000}", k);
				}
			}
			Contracts.Check(Utils.Size<string>(classNames) == this._labelBinCount, "incorrect class names");
			for (int i = 0; i < this._labelBinCount; i++)
			{
				yield return string.Format("{0}_Count", classNames[i]);
			}
			for (int j = 0; j < this._logOddsCount; j++)
			{
				yield return string.Format("{0}_LogOdds", classNames[j]);
			}
			yield return "IsBackoff";
			yield break;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000809AC File Offset: 0x0007EBAC
		private void InitializePriors()
		{
			float[] array = new float[this._labelBinCount];
			this._countTable.GetPriors(array, this._garbageCounts);
			Contracts.Check(array.All((float x) => x >= 0f));
			float num = array.Sum();
			if (num > 0f)
			{
				for (int i = 0; i < this._labelBinCount; i++)
				{
					this._priorFrequencies[i] = (double)(array[i] / num);
				}
				return;
			}
			double num2 = 1.0 / (double)this._labelBinCount;
			for (int j = 0; j < this._labelBinCount; j++)
			{
				this._priorFrequencies[j] = num2;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00080A60 File Offset: 0x0007EC60
		public void GetFeatures(long key, float[] features, int startIdx)
		{
			float[] array = this._countsBuffers.Get();
			this._countTable.GetCounts(key, array);
			float num = array.Sum();
			bool flag = num <= this._garbageThreshold;
			if (flag)
			{
				Array.Copy(this._garbageCounts, array, this._labelBinCount);
			}
			num = this.AddLaplacianNoisePerLabel(array);
			Array.Copy(array, 0, features, startIdx, this._labelBinCount);
			features[this._numFeatures - 1] = (float)(flag ? 1 : 0);
			this.GenerateLogOdds(array, features, startIdx, num);
			this._countsBuffers.Return(array);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00080AF0 File Offset: 0x0007ECF0
		private float AddLaplacianNoisePerLabel(float[] counts)
		{
			if (this._rnd.Value == null)
			{
				this._rnd.Value = new SysRandom(this._seed);
			}
			float num = 0f;
			for (int i = 0; i < this._labelBinCount; i++)
			{
				if (this._laplaceScale > 0f)
				{
					counts[i] += Stats.SampleFromLaplacian(this._rnd.Value, 0f, this._laplaceScale);
				}
				if (counts[i] < 0f)
				{
					counts[i] = 0f;
				}
				num += counts[i];
			}
			return num;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00080B8C File Offset: 0x0007ED8C
		private void GenerateLogOdds(float[] counts, float[] features, int ifeatMin, float sum)
		{
			int i = 0;
			int num = ifeatMin + this._labelBinCount;
			while (i < this._logOddsCount)
			{
				if ((counts[i] <= 0f && this._priorFrequencies[i] <= 0.0) || this._priorFrequencies[i] >= 1.0)
				{
					features[num] = 0f;
				}
				else
				{
					features[num] = (float)Math.Log(((double)counts[i] + (double)this._priorCoef * this._priorFrequencies[i]) / ((double)(sum - counts[i]) + (double)this._priorCoef * (1.0 - this._priorFrequencies[i])));
				}
				i++;
				num++;
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00080C38 File Offset: 0x0007EE38
		[Conditional("DEBUG")]
		private void AssertValidOutput(float[] features, int ifeatmin)
		{
			int num = ifeatmin + this.NumFeatures;
			for (int i = ifeatmin; i < num; i++)
			{
			}
		}

		// Token: 0x04000D7F RID: 3455
		public const string LoaderSignature = "DraculaFeaturizer";

		// Token: 0x04000D80 RID: 3456
		private const int VersionSingleOnly = 65538;

		// Token: 0x04000D81 RID: 3457
		private readonly IHost _host;

		// Token: 0x04000D82 RID: 3458
		private readonly int _numFeatures;

		// Token: 0x04000D83 RID: 3459
		private readonly int _labelBinCount;

		// Token: 0x04000D84 RID: 3460
		private readonly int _logOddsCount;

		// Token: 0x04000D85 RID: 3461
		private readonly float _priorCoef;

		// Token: 0x04000D86 RID: 3462
		private readonly int _seed;

		// Token: 0x04000D87 RID: 3463
		private readonly float _laplaceScale;

		// Token: 0x04000D88 RID: 3464
		private readonly ICountTable _countTable;

		// Token: 0x04000D89 RID: 3465
		private readonly float[] _garbageCounts;

		// Token: 0x04000D8A RID: 3466
		private readonly double[] _priorFrequencies;

		// Token: 0x04000D8B RID: 3467
		private readonly float _garbageThreshold;

		// Token: 0x04000D8C RID: 3468
		private ThreadLocal<SysRandom> _rnd;

		// Token: 0x04000D8D RID: 3469
		private readonly MadeObjectPool<float[]> _countsBuffers;

		// Token: 0x02000425 RID: 1061
		public class Arguments
		{
			// Token: 0x04000D8F RID: 3471
			[Argument(0, HelpText = "The coefficient with which to apply the prior smoothing to the features", ShortName = "prior")]
			public float priorCoefficient = 1f;

			// Token: 0x04000D90 RID: 3472
			[Argument(0, HelpText = "Laplacian noise diversity/scale-parameter. Suggest keeping it less than 1.", ShortName = "laplace")]
			public float laplaceScale;

			// Token: 0x04000D91 RID: 3473
			[Argument(0, HelpText = "Seed for the random generator for the laplacian noise.", ShortName = "seed")]
			public int seed = 314489979;
		}
	}
}
