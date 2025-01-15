using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000AC RID: 172
	public sealed class EmptyDataView : IDataView, ISchematized
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00013CD5 File Offset: 0x00011ED5
		public EmptyDataView(IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("EmptyDataView");
			Contracts.CheckValue<ISchema>(this._host, schema, "schema");
			this._schema = schema;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00013D11 File Offset: 0x00011F11
		public bool CanShuffle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00013D14 File Offset: 0x00011F14
		public ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00013D1C File Offset: 0x00011F1C
		public long? GetRowCount(bool lazy = true)
		{
			return new long?(0L);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00013D25 File Offset: 0x00011F25
		public IRowCursor GetRowCursor(Func<int, bool> needCol, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, needCol, "needCol");
			return new EmptyDataView.Cursor(this._host, this._schema, needCol);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00013D4C File Offset: 0x00011F4C
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> needCol, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, needCol, "needCol");
			consolidator = null;
			return new EmptyDataView.Cursor[]
			{
				new EmptyDataView.Cursor(this._host, this._schema, needCol)
			};
		}

		// Token: 0x0400017F RID: 383
		private readonly IHost _host;

		// Token: 0x04000180 RID: 384
		private readonly ISchema _schema;

		// Token: 0x020000AD RID: 173
		private sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x0600032A RID: 810 RVA: 0x00013D8A File Offset: 0x00011F8A
			public Cursor(IChannelProvider provider, ISchema schema, Func<int, bool> needCol)
				: base(provider)
			{
				this._schema = schema;
				this._active = Utils.BuildArray<bool>(schema.ColumnCount, needCol);
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600032B RID: 811 RVA: 0x00013DAC File Offset: 0x00011FAC
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x0600032C RID: 812 RVA: 0x00013DC2 File Offset: 0x00011FC2
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					throw Contracts.Except(this._ch, "Cannot call ID getter in current state");
				};
			}

			// Token: 0x0600032D RID: 813 RVA: 0x00013DD0 File Offset: 0x00011FD0
			protected override bool MoveNextCore()
			{
				return false;
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600032E RID: 814 RVA: 0x00013DD3 File Offset: 0x00011FD3
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x0600032F RID: 815 RVA: 0x00013DDB File Offset: 0x00011FDB
			public bool IsColumnActive(int col)
			{
				return 0 <= col && col < this._active.Length && this._active[col];
			}

			// Token: 0x06000330 RID: 816 RVA: 0x00013E08 File Offset: 0x00012008
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col), "Can't get getter for inactive column");
				return delegate(ref TValue value)
				{
					throw Contracts.Except(this._ch, "Cannot use getter with cursor in this state");
				};
			}

			// Token: 0x04000181 RID: 385
			private readonly ISchema _schema;

			// Token: 0x04000182 RID: 386
			private readonly bool[] _active;
		}
	}
}
