using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010C0 RID: 4288
	internal class ErrorColumnMappingDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x0600705D RID: 28765 RVA: 0x00182240 File Offset: 0x00180440
		private ErrorColumnMappingDbDataReader(DbDataReaderWithTableSchema reader, DbEnvironment environment, Dictionary<int, ValueException> udtFieldsMap)
			: base(reader)
		{
			this.environment = environment;
			this.udtFieldsMap = udtFieldsMap;
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x00182258 File Offset: 0x00180458
		public static Func<DbDataReaderWithTableSchema, DbDataReaderWithTableSchema> CreateWrapper(DbEnvironment environment, Dictionary<int, ValueException> errorColumnsMap)
		{
			if (errorColumnsMap == null || errorColumnsMap.Count == 0)
			{
				return null;
			}
			return (DbDataReaderWithTableSchema reader) => new ErrorColumnMappingDbDataReader(reader, environment, errorColumnsMap);
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x0018229C File Offset: 0x0018049C
		public override object GetValue(int ordinal)
		{
			ValueException ex;
			if (this.udtFieldsMap.TryGetValue(ordinal, out ex))
			{
				throw ex;
			}
			return base.GetValue(ordinal);
		}

		// Token: 0x06007060 RID: 28768 RVA: 0x001822C4 File Offset: 0x001804C4
		public override int GetValues(object[] values)
		{
			ValueException ex = this.udtFieldsMap.Values.SingleOrDefault<ValueException>();
			if (ex != null)
			{
				throw ex;
			}
			return base.GetValues(values);
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x001822EE File Offset: 0x001804EE
		public override Type GetFieldType(int ordinal)
		{
			if (this.udtFieldsMap.ContainsKey(ordinal))
			{
				return typeof(object);
			}
			return base.GetFieldType(ordinal);
		}

		// Token: 0x17001F9B RID: 8091
		public override object this[string name]
		{
			get
			{
				return this[base.GetOrdinal(name)];
			}
		}

		// Token: 0x17001F9C RID: 8092
		public override object this[int ordinal]
		{
			get
			{
				ValueException ex;
				if (this.udtFieldsMap.TryGetValue(ordinal, out ex))
				{
					throw ex;
				}
				return base[ordinal];
			}
		}

		// Token: 0x06007064 RID: 28772 RVA: 0x00182348 File Offset: 0x00180548
		public override bool IsDBNull(int ordinal)
		{
			ValueException ex;
			return !this.udtFieldsMap.TryGetValue(ordinal, out ex) && base.IsDBNull(ordinal);
		}

		// Token: 0x04003E0E RID: 15886
		private readonly DbEnvironment environment;

		// Token: 0x04003E0F RID: 15887
		private readonly Dictionary<int, ValueException> udtFieldsMap;
	}
}
