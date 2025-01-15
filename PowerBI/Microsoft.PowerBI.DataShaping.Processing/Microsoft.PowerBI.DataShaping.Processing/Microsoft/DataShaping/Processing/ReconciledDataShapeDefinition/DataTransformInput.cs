using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200003A RID: 58
	internal sealed class DataTransformInput
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00005ACC File Offset: 0x00003CCC
		internal DataTransformInput(ResultTable table, int tableIndex, DataTransformTableSchema schema, IList<DataTransformParameter> parameters)
		{
			this._table = table;
			this._schema = schema;
			this._parameters = parameters;
			this._tableIndex = tableIndex;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005AF1 File Offset: 0x00003CF1
		internal ResultTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005AF9 File Offset: 0x00003CF9
		internal DataTransformTableSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005B01 File Offset: 0x00003D01
		internal IList<DataTransformParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005B09 File Offset: 0x00003D09
		public int TableIndex
		{
			get
			{
				return this._tableIndex;
			}
		}

		// Token: 0x04000108 RID: 264
		private readonly ResultTable _table;

		// Token: 0x04000109 RID: 265
		private readonly int _tableIndex;

		// Token: 0x0400010A RID: 266
		private readonly DataTransformTableSchema _schema;

		// Token: 0x0400010B RID: 267
		private readonly IList<DataTransformParameter> _parameters;
	}
}
