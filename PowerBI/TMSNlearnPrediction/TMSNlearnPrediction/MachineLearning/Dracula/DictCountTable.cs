using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000412 RID: 1042
	public sealed class DictCountTable : CountTableBase
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x0007EFB4 File Offset: 0x0007D1B4
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("DICT  CT", 65537U, 65537U, 65537U, "DictCountTable", null);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0007EFE0 File Offset: 0x0007D1E0
		public DictCountTable(IHostEnvironment env, Dictionary<long, float>[] counts, int labelCardinality, float[] priorCounts, float garbageThreshold, float[] garbageCounts)
			: base(env, "DictCountTable", labelCardinality, priorCounts, garbageThreshold, garbageCounts)
		{
			Contracts.CheckValue<Dictionary<long, float>[]>(this._host, counts, "counts");
			Contracts.Check(this._host, counts.Length == labelCardinality, "Counts must be parallel to label cardinality");
			Contracts.Check(this._host, counts.All((Dictionary<long, float> x) => x != null), "Count dictionaries must all exist");
			this._tables = counts;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0007F061 File Offset: 0x0007D261
		public static DictCountTable Create(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(DictCountTable.GetVersionInfo());
			return new DictCountTable(ctx, env);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0007F08C File Offset: 0x0007D28C
		private DictCountTable(ModelLoadContext ctx, IHostEnvironment env)
			: base(env, "DictCountTable", ctx)
		{
			this._tables = new Dictionary<long, float>[this._labelCardinality];
			for (int i = 0; i < this._labelCardinality; i++)
			{
				this._tables[i] = new Dictionary<long, float>();
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num >= 0);
				for (int j = 0; j < num; j++)
				{
					long num2 = ctx.Reader.ReadInt64();
					Contracts.CheckDecode(this._host, !this._tables[i].ContainsKey(num2));
					float num3 = ctx.Reader.ReadSingle();
					Contracts.CheckDecode(this._host, num3 >= 0f);
					this._tables[i].Add(num2, num3);
				}
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0007F164 File Offset: 0x0007D364
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.SetVersionInfo(DictCountTable.GetVersionInfo());
			base.Save(ctx);
			foreach (Dictionary<long, float> dictionary in this._tables)
			{
				ctx.Writer.Write(dictionary.Count);
				foreach (KeyValuePair<long, float> keyValuePair in dictionary)
				{
					ctx.Writer.Write(keyValuePair.Key);
					ctx.Writer.Write(keyValuePair.Value);
				}
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0007F220 File Offset: 0x0007D420
		public override void GetCounts(long key, float[] counts)
		{
			Contracts.Check(this._host, Utils.Size<float>(counts) == this._labelCardinality);
			for (int i = 0; i < this._labelCardinality; i++)
			{
				float num;
				if (!this._tables[i].TryGetValue(key, out num))
				{
					num = 0f;
				}
				counts[i] = num;
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0007F274 File Offset: 0x0007D474
		public override void GetRawCounts(RawCountKey key, float[] counts)
		{
			Contracts.Check(this._host, Utils.Size<float>(counts) == this._labelCardinality);
			Contracts.Check(this._host, key.HashId == 0);
			int num = this._tables.Length;
			for (int i = 0; i < num; i++)
			{
				if (!this._tables[i].TryGetValue(key.HashValue, out counts[i]))
				{
					counts[i] = 0f;
				}
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0007F4F0 File Offset: 0x0007D6F0
		public override IEnumerable<RawCountKey> AllRawCountKeys()
		{
			HashSet<long> keys = new HashSet<long>();
			foreach (Dictionary<long, float> dictionary in this._tables)
			{
				foreach (long num in dictionary.Keys)
				{
					keys.Add(num);
				}
			}
			foreach (long key in keys)
			{
				yield return new RawCountKey(0, key);
			}
			yield break;
		}

		// Token: 0x04000D5D RID: 3421
		public const string LoaderSignature = "DictCountTable";

		// Token: 0x04000D5E RID: 3422
		private readonly Dictionary<long, float>[] _tables;
	}
}
