using System;
using Microsoft.MachineLearning.Data;

namespace Microsoft.MachineLearning.Training
{
	// Token: 0x0200048A RID: 1162
	public abstract class TrainingCursorBase : IDisposable
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0008AF85 File Offset: 0x00089185
		public IRow Row
		{
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0008AF8D File Offset: 0x0008918D
		public long SkippedRowCount
		{
			get
			{
				return this._skipCount;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0008AF95 File Offset: 0x00089195
		public long KeptRowCount
		{
			get
			{
				return this._keptCount;
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008AF9D File Offset: 0x0008919D
		protected TrainingCursorBase(IRowCursor input, Action<CursOpt> signal)
		{
			this._cursor = input;
			this._signal = signal;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0008AFB3 File Offset: 0x000891B3
		protected static IRowCursor CreateCursor(RoleMappedData data, CursOpt opt, IRandom rand, params int[] extraCols)
		{
			return data.CreateRowCursor(opt, rand, extraCols);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0008AFBE File Offset: 0x000891BE
		protected virtual CursOpt CursoringCompleteFlags()
		{
			if (this.SkippedRowCount != 0L)
			{
				return (CursOpt)0U;
			}
			return CursOpt.AllowBadEverything;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0008AFD4 File Offset: 0x000891D4
		public bool MoveNext()
		{
			while (this._cursor.MoveNext())
			{
				if (this.Accept())
				{
					this._keptCount += 1L;
					return true;
				}
				this._skipCount += 1L;
			}
			if (this._signal != null)
			{
				this._signal(this.CursoringCompleteFlags());
			}
			return false;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0008B032 File Offset: 0x00089232
		public virtual bool Accept()
		{
			return true;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008B035 File Offset: 0x00089235
		public void Dispose()
		{
			this._cursor.Dispose();
		}

		// Token: 0x04000E9A RID: 3738
		private readonly IRowCursor _cursor;

		// Token: 0x04000E9B RID: 3739
		private readonly Action<CursOpt> _signal;

		// Token: 0x04000E9C RID: 3740
		private long _skipCount;

		// Token: 0x04000E9D RID: 3741
		private long _keptCount;

		// Token: 0x0200048B RID: 1163
		public abstract class FactoryBase<TCurs> where TCurs : TrainingCursorBase
		{
			// Token: 0x17000259 RID: 601
			// (get) Token: 0x0600184C RID: 6220 RVA: 0x0008B042 File Offset: 0x00089242
			public RoleMappedData Data
			{
				get
				{
					return this._data;
				}
			}

			// Token: 0x0600184D RID: 6221 RVA: 0x0008B04C File Offset: 0x0008924C
			protected FactoryBase(RoleMappedData data, CursOpt opt)
			{
				Contracts.CheckValue<RoleMappedData>(data, "data");
				this._data = data;
				this._initOpts = opt;
				this._opts = opt;
				this._lock = new object();
			}

			// Token: 0x0600184E RID: 6222 RVA: 0x0008B08C File Offset: 0x0008928C
			private void SignalCore(CursOpt opt)
			{
				lock (this._lock)
				{
					this._opts |= opt;
				}
			}

			// Token: 0x0600184F RID: 6223 RVA: 0x0008B0D4 File Offset: 0x000892D4
			public TCurs Create(IRandom rand = null, params int[] extraCols)
			{
				CursOpt opts;
				lock (this._lock)
				{
					opts = this._opts;
				}
				IRowCursor rowCursor = this._data.CreateRowCursor(opts, rand, extraCols);
				return this.CreateCursorCore(rowCursor, this._data, opts, new Action<CursOpt>(this.SignalCore));
			}

			// Token: 0x06001850 RID: 6224 RVA: 0x0008B140 File Offset: 0x00089340
			public TCurs[] CreateSet(int n, IRandom rand = null, params int[] extraCols)
			{
				CursOpt opts;
				lock (this._lock)
				{
					opts = this._opts;
				}
				IRowCursorConsolidator rowCursorConsolidator;
				IRowCursor[] array = this._data.CreateRowCursorSet(out rowCursorConsolidator, opts, n, rand, extraCols);
				Action<CursOpt> action;
				if (array.Length > 1)
				{
					action = new Action<CursOpt>(new TrainingCursorBase.FactoryBase<TCurs>.AndAccumulator(new Action<CursOpt>(this, ldftn(SignalCore)), array.Length).Signal);
				}
				else
				{
					action = new Action<CursOpt>(this.SignalCore);
				}
				TCurs[] array2 = new TCurs[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = this.CreateCursorCore(array[i], this._data, opts, action);
				}
				return array2;
			}

			// Token: 0x06001851 RID: 6225
			protected abstract TCurs CreateCursorCore(IRowCursor input, RoleMappedData data, CursOpt opt, Action<CursOpt> signal);

			// Token: 0x04000E9E RID: 3742
			private readonly RoleMappedData _data;

			// Token: 0x04000E9F RID: 3743
			private readonly CursOpt _initOpts;

			// Token: 0x04000EA0 RID: 3744
			private readonly object _lock;

			// Token: 0x04000EA1 RID: 3745
			private CursOpt _opts;

			// Token: 0x0200048C RID: 1164
			private sealed class AndAccumulator
			{
				// Token: 0x06001852 RID: 6226 RVA: 0x0008B208 File Offset: 0x00089408
				public AndAccumulator(Action<CursOpt> signal, int lim)
				{
					this._signal = signal;
					this._lim = lim;
					this._opts = (CursOpt)4294967295U;
				}

				// Token: 0x06001853 RID: 6227 RVA: 0x0008B228 File Offset: 0x00089428
				public void Signal(CursOpt opt)
				{
					lock (this)
					{
						this._opts &= opt;
						if (++this._count == this._lim)
						{
							this._signal(this._opts);
						}
					}
				}

				// Token: 0x04000EA2 RID: 3746
				private readonly Action<CursOpt> _signal;

				// Token: 0x04000EA3 RID: 3747
				private readonly int _lim;

				// Token: 0x04000EA4 RID: 3748
				private int _count;

				// Token: 0x04000EA5 RID: 3749
				private CursOpt _opts;
			}
		}
	}
}
