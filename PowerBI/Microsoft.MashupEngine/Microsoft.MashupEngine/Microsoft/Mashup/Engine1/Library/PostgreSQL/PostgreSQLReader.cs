using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.PostgreSQL
{
	// Token: 0x02000542 RID: 1346
	internal class PostgreSQLReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x06002B4D RID: 11085 RVA: 0x000834A6 File Offset: 0x000816A6
		public PostgreSQLReader(DbDataReaderWithTableSchema reader, PostgreSQLEnvironment environment)
			: base(reader)
		{
			this.environment = environment;
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000834B8 File Offset: 0x000816B8
		public override string GetDataTypeName(int ordinal)
		{
			string dataTypeName = base.GetDataTypeName(ordinal);
			int num;
			if (int.TryParse(dataTypeName, out num))
			{
				return null;
			}
			return dataTypeName;
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000834DA File Offset: 0x000816DA
		public override bool HasRows
		{
			get
			{
				return this.environment.ConvertDbExceptions<bool>(() => base.HasRows);
			}
		}

		// Token: 0x040012CB RID: 4811
		private readonly PostgreSQLEnvironment environment;
	}
}
