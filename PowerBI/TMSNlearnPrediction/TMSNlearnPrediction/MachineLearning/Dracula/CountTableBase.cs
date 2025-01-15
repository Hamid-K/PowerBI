using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200040B RID: 1035
	public abstract class CountTableBase : ICountTable, ICanSaveModel
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x0007E1F0 File Offset: 0x0007C3F0
		protected CountTableBase(IHostEnvironment env, string name, int labelCardinality, float[] priorCounts, float garbageThreshold, float[] garbageCounts)
		{
			this._host = env.Register(name);
			Contracts.Check(this._host, 0 < labelCardinality && labelCardinality < 100, "Label cardinality out of bounds");
			Contracts.CheckValue<float[]>(this._host, priorCounts, "priorCounts");
			Contracts.Check(this._host, priorCounts.All((float x) => x >= 0f));
			Contracts.Check(this._host, priorCounts.Length == labelCardinality);
			Contracts.Check(this._host, garbageThreshold >= 0f, "Garbage threshold must be non-negative");
			if (garbageThreshold > 0f)
			{
				Contracts.CheckValue<float[]>(this._host, garbageCounts, "garbageCounts");
				Contracts.Check(this._host, garbageCounts.Length == labelCardinality);
				Contracts.Check(this._host, garbageCounts.All((float x) => x >= 0f));
			}
			this._labelCardinality = labelCardinality;
			this._priorCounts = priorCounts;
			this._garbageCounts = garbageCounts;
			this._garbageThreshold = garbageThreshold;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0007E330 File Offset: 0x0007C530
		protected CountTableBase(IHostEnvironment env, string name, ModelLoadContext ctx)
		{
			this._host = env.Register(name);
			this._labelCardinality = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, 0 < this._labelCardinality && this._labelCardinality < 100);
			this._priorCounts = Utils.ReadSingleArray(ctx.Reader);
			Contracts.CheckDecode(this._host, Utils.Size<float>(this._priorCounts) == this._labelCardinality);
			Contracts.CheckDecode(this._host, this._priorCounts.All((float x) => x >= 0f));
			this._garbageThreshold = ctx.Reader.ReadSingle();
			Contracts.CheckDecode(this._host, this._garbageThreshold >= 0f);
			this._garbageCounts = Utils.ReadSingleArray(ctx.Reader);
			if (this._garbageThreshold == 0f)
			{
				Contracts.CheckDecode(this._host, Utils.Size<float>(this._garbageCounts) == 0);
				return;
			}
			Contracts.CheckDecode(this._host, Utils.Size<float>(this._garbageCounts) == this._labelCardinality);
			Contracts.CheckDecode(this._host, this._garbageCounts.All((float x) => x >= 0f));
		}

		// Token: 0x060015A8 RID: 5544
		public abstract void GetCounts(long key, float[] counts);

		// Token: 0x060015A9 RID: 5545
		public abstract void GetRawCounts(RawCountKey key, float[] counts);

		// Token: 0x060015AA RID: 5546
		public abstract IEnumerable<RawCountKey> AllRawCountKeys();

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0007E499 File Offset: 0x0007C699
		public float GarbageThreshold
		{
			get
			{
				return this._garbageThreshold;
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0007E4A4 File Offset: 0x0007C6A4
		public void GetPriors(float[] priorCounts, float[] garbageCounts)
		{
			Contracts.CheckValue<float[]>(this._host, priorCounts, "priorCounts");
			Contracts.Check(this._host, priorCounts.Length == this._labelCardinality);
			if (this._garbageThreshold > 0f)
			{
				Contracts.CheckValue<float[]>(this._host, garbageCounts, "garbageCounts");
				Contracts.Check(this._host, garbageCounts.Length == this._labelCardinality);
			}
			Array.Copy(this._priorCounts, priorCounts, this._labelCardinality);
			if (this._garbageThreshold > 0f)
			{
				Array.Copy(this._garbageCounts, garbageCounts, this._labelCardinality);
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0007E540 File Offset: 0x0007C740
		public virtual void Save(ModelSaveContext ctx)
		{
			ctx.Writer.Write(this._labelCardinality);
			Utils.WriteSingleArray(ctx.Writer, this._priorCounts);
			ctx.Writer.Write(this._garbageThreshold);
			float garbageThreshold = this._garbageThreshold;
			Utils.WriteSingleArray(ctx.Writer, this._garbageCounts);
		}

		// Token: 0x04000D41 RID: 3393
		public const int LabelCardinalityLim = 100;

		// Token: 0x04000D42 RID: 3394
		protected readonly IHost _host;

		// Token: 0x04000D43 RID: 3395
		protected readonly int _labelCardinality;

		// Token: 0x04000D44 RID: 3396
		protected readonly float[] _priorCounts;

		// Token: 0x04000D45 RID: 3397
		protected float _garbageThreshold;

		// Token: 0x04000D46 RID: 3398
		protected readonly float[] _garbageCounts;
	}
}
