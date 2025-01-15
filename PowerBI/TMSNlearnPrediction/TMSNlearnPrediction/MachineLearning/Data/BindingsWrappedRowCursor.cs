using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200038F RID: 911
	public sealed class BindingsWrappedRowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0006FB78 File Offset: 0x0006DD78
		public ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0006FB80 File Offset: 0x0006DD80
		public BindingsWrappedRowCursor(IChannelProvider provider, IRowCursor input, ColumnBindingsBase bindings)
			: base(provider, input)
		{
			Contracts.CheckValue<IRowCursor>(this._ch, input, "input");
			Contracts.CheckValue<ColumnBindingsBase>(this._ch, bindings, "bindings");
			this._bindings = bindings;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0006FBB4 File Offset: 0x0006DDB4
		public bool IsColumnActive(int col)
		{
			Contracts.Check(this._ch, (0 <= col) & (col < this._bindings.ColumnCount), "col");
			bool flag;
			col = this._bindings.MapColumnIndex(out flag, col);
			return flag && base.Input.IsColumnActive(col);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0006FC08 File Offset: 0x0006DE08
		public ValueGetter<TValue> GetGetter<TValue>(int col)
		{
			Contracts.Check(this._ch, this.IsColumnActive(col), "col");
			bool flag;
			col = this._bindings.MapColumnIndex(out flag, col);
			return base.Input.GetGetter<TValue>(col);
		}

		// Token: 0x04000B69 RID: 2921
		private readonly ColumnBindingsBase _bindings;
	}
}
