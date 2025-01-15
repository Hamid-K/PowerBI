using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200042B RID: 1067
	public sealed class SharedMultiCountTable : IMultiCountTable, ICanSaveModel
	{
		// Token: 0x06001628 RID: 5672 RVA: 0x00081398 File Offset: 0x0007F598
		public SharedMultiCountTable(IHostEnvironment env, ICountTable baseBaseTable)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("SharedMultiCountTable");
			this._baseTable = baseBaseTable;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000813C3 File Offset: 0x0007F5C3
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("SHRD MCT", 65537U, 65537U, 65537U, "SharedMultiCountTable", null);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000813E4 File Offset: 0x0007F5E4
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(SharedMultiCountTable.GetVersionInfo());
			ctx.Writer.Write(0);
			ctx.SaveModel<ICountTable>(this._baseTable, "BaseTable");
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00081420 File Offset: 0x0007F620
		public SharedMultiCountTable(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			this._host = env.Register("SharedMultiCountTable");
			ctx.CheckAtModel(SharedMultiCountTable.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 0);
			ctx.LoadModel<ICountTable, SignatureLoadModel>(out this._baseTable, "BaseTable", new object[] { this._host });
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000814A3 File Offset: 0x0007F6A3
		public ICountTable GetCountTable(int iCol, int iSlot)
		{
			return new SharedMultiCountTable.ProxyCountTable(iCol, iSlot, this._baseTable);
		}

		// Token: 0x04000D9C RID: 3484
		public const string LoaderSignature = "SharedMultiCountTable";

		// Token: 0x04000D9D RID: 3485
		private readonly IHost _host;

		// Token: 0x04000D9E RID: 3486
		private readonly ICountTable _baseTable;

		// Token: 0x0200042C RID: 1068
		private class ProxyCountTable : ICountTable
		{
			// Token: 0x0600162D RID: 5677 RVA: 0x000814B2 File Offset: 0x0007F6B2
			public ProxyCountTable(int iCol, int iSlot, ICountTable baseCountTable)
			{
				Contracts.CheckValue<ICountTable>(baseCountTable, "baseCountTable");
				Contracts.Check(baseCountTable.GarbageThreshold == 0f, "Garbage bin not supported for shared table");
				this._mixin = Hashing.MurmurRound((uint)iCol, (uint)iSlot);
				this._table = baseCountTable;
			}

			// Token: 0x0600162E RID: 5678 RVA: 0x000814F0 File Offset: 0x0007F6F0
			public void GetCounts(long key, float[] counts)
			{
				long num = (long)((ulong)Hashing.MurmurRound(this._mixin, (uint)key));
				this._table.GetCounts(num, counts);
			}

			// Token: 0x0600162F RID: 5679 RVA: 0x00081519 File Offset: 0x0007F719
			public void GetRawCounts(RawCountKey key, float[] counts)
			{
				throw Contracts.Except("Shared table doesn't support raw count export");
			}

			// Token: 0x06001630 RID: 5680 RVA: 0x00081525 File Offset: 0x0007F725
			public IEnumerable<RawCountKey> AllRawCountKeys()
			{
				throw Contracts.Except("Shared table doesn't support raw count export");
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06001631 RID: 5681 RVA: 0x00081531 File Offset: 0x0007F731
			public float GarbageThreshold
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x06001632 RID: 5682 RVA: 0x00081538 File Offset: 0x0007F738
			public void GetPriors(float[] priorCounts, float[] garbageCounts)
			{
				this._table.GetPriors(priorCounts, garbageCounts);
			}

			// Token: 0x04000D9F RID: 3487
			private readonly uint _mixin;

			// Token: 0x04000DA0 RID: 3488
			private readonly ICountTable _table;
		}
	}
}
