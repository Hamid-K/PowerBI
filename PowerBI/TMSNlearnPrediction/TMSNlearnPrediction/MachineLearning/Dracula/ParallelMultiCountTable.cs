using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000429 RID: 1065
	public sealed class ParallelMultiCountTable : IMultiCountTable, ICanSaveModel
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x00081104 File Offset: 0x0007F304
		public ParallelMultiCountTable(IHostEnvironment env, ICountTable[][] countTables)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("ParallelMultiCountTable");
			this._countTables = countTables;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0008112F File Offset: 0x0007F32F
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PAR  MCT", 65537U, 65537U, 65537U, "ParallelMultiCountTable", null);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00081150 File Offset: 0x0007F350
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ParallelMultiCountTable.GetVersionInfo());
			ctx.Writer.Write(this._countTables.Length);
			for (int i = 0; i < this._countTables.Length; i++)
			{
				int num = this._countTables[i].Length;
				ctx.Writer.Write(num);
				for (int j = 0; j < num; j++)
				{
					string text = string.Format("Table_{0:000}_{1:000}", i, j);
					ctx.SaveModel<ICountTable>(this._countTables[i][j], text);
				}
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000811F0 File Offset: 0x0007F3F0
		public ParallelMultiCountTable(ModelLoadContext ctx, IHostEnvironment env)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			this._host = env.Register("ParallelMultiCountTable");
			ctx.CheckAtModel(ParallelMultiCountTable.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num > 0);
			this._countTables = new ICountTable[num][];
			for (int i = 0; i < num; i++)
			{
				int num2 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this._host, num2 > 0);
				this._countTables[i] = new ICountTable[num2];
				for (int j = 0; j < num2; j++)
				{
					string text = string.Format("Table_{0:000}_{1:000}", i, j);
					ctx.LoadModel<ICountTable, SignatureLoadModel>(out this._countTables[i][j], text, new object[] { this._host });
				}
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000812E0 File Offset: 0x0007F4E0
		public ICountTable GetCountTable(int iCol, int iSlot)
		{
			return this._countTables[iCol][iSlot];
		}

		// Token: 0x04000D96 RID: 3478
		public const string LoaderSignature = "ParallelMultiCountTable";

		// Token: 0x04000D97 RID: 3479
		private readonly IHost _host;

		// Token: 0x04000D98 RID: 3480
		private readonly ICountTable[][] _countTables;
	}
}
