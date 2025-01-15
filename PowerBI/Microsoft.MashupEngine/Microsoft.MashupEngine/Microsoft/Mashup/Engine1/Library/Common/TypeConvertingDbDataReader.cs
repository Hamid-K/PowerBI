using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200115A RID: 4442
	internal class TypeConvertingDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x0600744E RID: 29774 RVA: 0x0018F680 File Offset: 0x0018D880
		private TypeConvertingDbDataReader(DbDataReaderWithTableSchema reader, TableSchema schema, Dictionary<int, ValueConversion> valueConversions)
			: base(reader)
		{
			this.schema = schema;
			this.valueConversions = valueConversions;
		}

		// Token: 0x0600744F RID: 29775 RVA: 0x0018F698 File Offset: 0x0018D898
		public static DbDataReaderWithTableSchema New(DbDataReaderWithTableSchema reader, TableSchema sourceSchema, TableSchema targetSchema, IList<TypeConversion> typeConversions)
		{
			Dictionary<int, ValueConversion> dictionary = TypeConversion.GetTypeConversions(sourceSchema, targetSchema, typeConversions).ToDictionary((KeyValuePair<int, TypeConversion> kvp) => kvp.Key, (KeyValuePair<int, TypeConversion> kvp) => kvp.Value.ValueConversion);
			return new TypeConvertingDbDataReader(reader, targetSchema, dictionary);
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x06007450 RID: 29776 RVA: 0x0018F6F9 File Offset: 0x0018D8F9
		public override TableSchema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x06007451 RID: 29777 RVA: 0x0018F704 File Offset: 0x0018D904
		public override object GetValue(int ordinal)
		{
			object obj = base.GetValue(ordinal);
			ValueConversion valueConversion;
			if (this.valueConversions.TryGetValue(ordinal, out valueConversion))
			{
				obj = valueConversion.GetValue(base.DataReader, ordinal);
			}
			return obj;
		}

		// Token: 0x06007452 RID: 29778 RVA: 0x0018F738 File Offset: 0x0018D938
		public override int GetValues(object[] values)
		{
			int values2 = base.GetValues(values);
			foreach (KeyValuePair<int, ValueConversion> keyValuePair in this.valueConversions)
			{
				if (keyValuePair.Key < values2)
				{
					values[keyValuePair.Key] = keyValuePair.Value.GetValue(values, keyValuePair.Key);
				}
			}
			return values2;
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x0018F7B4 File Offset: 0x0018D9B4
		public override Type GetFieldType(int ordinal)
		{
			ValueConversion valueConversion;
			if (this.valueConversions.TryGetValue(ordinal, out valueConversion))
			{
				return valueConversion.ResultType;
			}
			return base.GetFieldType(ordinal);
		}

		// Token: 0x06007454 RID: 29780 RVA: 0x0018F7E0 File Offset: 0x0018D9E0
		public override string GetDataTypeName(int ordinal)
		{
			ValueConversion valueConversion;
			if (this.valueConversions.TryGetValue(ordinal, out valueConversion))
			{
				return valueConversion.ResultType.Name;
			}
			return base.GetDataTypeName(ordinal);
		}

		// Token: 0x06007455 RID: 29781 RVA: 0x0018F810 File Offset: 0x0018DA10
		public override string GetString(int ordinal)
		{
			ValueConversion valueConversion;
			if (this.valueConversions.TryGetValue(ordinal, out valueConversion))
			{
				return valueConversion.GetString(base.DataReader, ordinal);
			}
			return base.GetString(ordinal);
		}

		// Token: 0x17002058 RID: 8280
		public override object this[string name]
		{
			get
			{
				return this[base.GetOrdinal(name)];
			}
		}

		// Token: 0x17002059 RID: 8281
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x04003FFA RID: 16378
		private readonly TableSchema schema;

		// Token: 0x04003FFB RID: 16379
		private readonly Dictionary<int, ValueConversion> valueConversions;
	}
}
