using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000006 RID: 6
	public sealed class DataTransformExecutionContext
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002129 File Offset: 0x00000329
		public DataTransformExecutionContext(IEnumerable<IDataRow> inputRows, IDataRowFactory rowFactory, IReadOnlyList<DataTransformParameter> parameters, ISchemaRow inputSchema, CancellationToken cancellationToken)
		{
			this._inputRows = inputRows;
			this._rowFactory = rowFactory;
			this._parameters = parameters;
			this._inputSchema = inputSchema;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002156 File Offset: 0x00000356
		public IEnumerable<IDataRow> InputRows
		{
			get
			{
				return this._inputRows;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000215E File Offset: 0x0000035E
		public IDataRowFactory RowFactory
		{
			get
			{
				return this._rowFactory;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002166 File Offset: 0x00000366
		public IReadOnlyList<DataTransformParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000216E File Offset: 0x0000036E
		public ISchemaRow InputSchema
		{
			get
			{
				return this._inputSchema;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002176 File Offset: 0x00000376
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly IEnumerable<IDataRow> _inputRows;

		// Token: 0x0400002B RID: 43
		private readonly IDataRowFactory _rowFactory;

		// Token: 0x0400002C RID: 44
		private readonly IReadOnlyList<DataTransformParameter> _parameters;

		// Token: 0x0400002D RID: 45
		private readonly ISchemaRow _inputSchema;

		// Token: 0x0400002E RID: 46
		private readonly CancellationToken _cancellationToken;
	}
}
