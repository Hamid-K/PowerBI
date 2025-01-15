using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200001B RID: 27
	public sealed class SchemaTransformResult
	{
		// Token: 0x06000046 RID: 70 RVA: 0x0000224E File Offset: 0x0000044E
		public SchemaTransformResult(ISchemaRow schema)
		{
			this._schema = schema;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000225D File Offset: 0x0000045D
		public ISchemaRow Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x04000046 RID: 70
		private readonly ISchemaRow _schema;
	}
}
