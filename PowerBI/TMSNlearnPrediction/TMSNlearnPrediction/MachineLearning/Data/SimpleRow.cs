using System;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000140 RID: 320
	public sealed class SimpleRow : IRow, ISchematized, ICounted
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00022B10 File Offset: 0x00020D10
		public ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00022B18 File Offset: 0x00020D18
		public long Position
		{
			get
			{
				return this._input.Position;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00022B25 File Offset: 0x00020D25
		public long Batch
		{
			get
			{
				return this._input.Batch;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00022B34 File Offset: 0x00020D34
		public SimpleRow(ISchema schema, IRow input, Delegate[] getters)
		{
			Contracts.CheckValue<ISchema>(schema, "schema");
			Contracts.CheckValue<IRow>(input, "input");
			Contracts.Check(Utils.Size<Delegate>(getters) == schema.ColumnCount);
			this._schema = schema;
			this._input = input;
			this._getters = getters ?? new Delegate[0];
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00022B8F File Offset: 0x00020D8F
		public ValueGetter<UInt128> GetIdGetter()
		{
			return this._input.GetIdGetter();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00022B9C File Offset: 0x00020D9C
		public ValueGetter<T> GetGetter<T>(int col)
		{
			Contracts.CheckParam(0 <= col && col < this._getters.Length, "col", "Invalid col value in GetGetter");
			Contracts.Check(this.IsColumnActive(col));
			ValueGetter<T> valueGetter = this._getters[col] as ValueGetter<T>;
			if (valueGetter == null)
			{
				throw Contracts.Except("Unexpected TValue in GetGetter");
			}
			return valueGetter;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00022BF3 File Offset: 0x00020DF3
		public bool IsColumnActive(int col)
		{
			Contracts.Check(0 <= col && col < this._getters.Length);
			return this._getters[col] != null;
		}

		// Token: 0x0400034B RID: 843
		private readonly ISchema _schema;

		// Token: 0x0400034C RID: 844
		private readonly IRow _input;

		// Token: 0x0400034D RID: 845
		private readonly Delegate[] _getters;
	}
}
