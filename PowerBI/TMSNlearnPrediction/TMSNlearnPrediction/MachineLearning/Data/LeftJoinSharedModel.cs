using System;
using System.IO;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000110 RID: 272
	internal sealed class LeftJoinSharedModel
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001DC60 File Offset: 0x0001BE60
		public LeftJoinDataViewBase.JoinKeyColumn[] KeyColumns
		{
			get
			{
				return this._keyColumns;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001DC68 File Offset: 0x0001BE68
		public IDataView TransformedRightDv
		{
			get
			{
				return this._transformedRightDv;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001DC70 File Offset: 0x0001BE70
		public LeftJoinSharedModel(IHost host, LeftJoinDataViewBase.JoinKeyColumn[] keyColumns, IDataView transformedRightDv)
		{
			this._host = host;
			this._keyColumns = keyColumns;
			this._transformedRightDv = transformedRightDv;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.Writer.Write(this._keyColumns.Length);
			foreach (LeftJoinDataViewBase.JoinKeyColumn joinKeyColumn in this._keyColumns)
			{
				joinKeyColumn.Save(ctx);
			}
			BinarySaver saver = new BinarySaver(new BinarySaver.Arguments
			{
				silent = true
			}, this._host);
			ctx.SaveBinaryStream("Right.idv", delegate(BinaryWriter w)
			{
				saver.SaveData(w.BaseStream, this._transformedRightDv, Utils.GetIdentityPermutation(this._transformedRightDv.Schema.ColumnCount));
			});
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001DD69 File Offset: 0x0001BF69
		public static LeftJoinSharedModel Create(ModelLoadContext ctx, IHost host)
		{
			return new LeftJoinSharedModel(ctx, host);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		private LeftJoinSharedModel(ModelLoadContext ctx, IHost host)
		{
			LeftJoinSharedModel.<>c__DisplayClass5 CS$<>8__locals1 = new LeftJoinSharedModel.<>c__DisplayClass5();
			CS$<>8__locals1.ctx = ctx;
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			this._host = host;
			int num = CS$<>8__locals1.ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num > 0);
			this._keyColumns = new LeftJoinDataViewBase.JoinKeyColumn[num];
			for (int i = 0; i < num; i++)
			{
				this._keyColumns[i] = new LeftJoinDataViewBase.JoinKeyColumn(CS$<>8__locals1.ctx, this._host);
			}
			BinaryLoader loader = null;
			if (!CS$<>8__locals1.ctx.TryLoadBinaryStream("Right.idv", delegate(BinaryReader r)
			{
				loader = BinaryLoader.Create(CS$<>8__locals1.ctx, CS$<>8__locals1.<>4__this._host, r.BaseStream);
			}))
			{
				throw Contracts.ExceptDecode(this._host, "Missing serialized right data view stream.");
			}
			this._transformedRightDv = loader;
		}

		// Token: 0x040002C0 RID: 704
		private const string SerializedRightDvName = "Right.idv";

		// Token: 0x040002C1 RID: 705
		private readonly IHost _host;

		// Token: 0x040002C2 RID: 706
		private readonly LeftJoinDataViewBase.JoinKeyColumn[] _keyColumns;

		// Token: 0x040002C3 RID: 707
		private readonly IDataView _transformedRightDv;
	}
}
