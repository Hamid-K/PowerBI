using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200001A RID: 26
	public sealed class SchemaTransformContext
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002219 File Offset: 0x00000419
		public SchemaTransformContext(ISchemaRow schema, ISchemaRowFactory schemaFactory, IReadOnlyList<DataTransformParameter> parameters)
		{
			this._schema = schema;
			this._schemaFactory = schemaFactory;
			this._parameters = parameters;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002236 File Offset: 0x00000436
		public ISchemaRow Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000223E File Offset: 0x0000043E
		public ISchemaRowFactory SchemaFactory
		{
			get
			{
				return this._schemaFactory;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002246 File Offset: 0x00000446
		public IReadOnlyList<DataTransformParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly ISchemaRow _schema;

		// Token: 0x04000044 RID: 68
		private readonly ISchemaRowFactory _schemaFactory;

		// Token: 0x04000045 RID: 69
		private readonly IReadOnlyList<DataTransformParameter> _parameters;
	}
}
