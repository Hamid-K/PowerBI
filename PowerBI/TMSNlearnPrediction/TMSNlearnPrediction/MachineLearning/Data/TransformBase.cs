using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000015 RID: 21
	public abstract class TransformBase : IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003A9F File Offset: 0x00001C9F
		protected TransformBase(IHostEnvironment env, string name, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonWhiteSpace(env, name, "name");
			Contracts.CheckValue<IDataView>(env, input, "input");
			this._host = env.Register(name);
			this._input = input;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003ADF File Offset: 0x00001CDF
		protected TransformBase(IHost host, IDataView input)
		{
			Contracts.CheckValue<IHost>(host, "host");
			Contracts.CheckValue<IDataView>(host, input, "input");
			this._host = host;
			this._input = input;
		}

		// Token: 0x06000060 RID: 96
		public abstract void Save(ModelSaveContext ctx);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003B0C File Offset: 0x00001D0C
		public IDataView Source
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x06000062 RID: 98
		public abstract long? GetRowCount(bool lazy = true);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003B14 File Offset: 0x00001D14
		public virtual bool CanShuffle
		{
			get
			{
				return this._input.CanShuffle;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000064 RID: 100
		public abstract ISchema Schema { get; }

		// Token: 0x06000065 RID: 101 RVA: 0x00003B24 File Offset: 0x00001D24
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			IRandom random = (this.CanShuffle ? rand : null);
			IRowCursor rowCursor;
			if (this.ShouldUseParallelCursors(predicate) != false && DataViewUtils.TryCreateConsolidatingCursor(out rowCursor, this, predicate, this._host, random))
			{
				return rowCursor;
			}
			return this.GetRowCursorCore(predicate, random);
		}

		// Token: 0x06000066 RID: 102
		protected abstract bool? ShouldUseParallelCursors(Func<int, bool> predicate);

		// Token: 0x06000067 RID: 103
		protected abstract IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null);

		// Token: 0x06000068 RID: 104
		public abstract IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null);

		// Token: 0x0400002F RID: 47
		protected readonly IHost _host;

		// Token: 0x04000030 RID: 48
		protected readonly IDataView _input;
	}
}
