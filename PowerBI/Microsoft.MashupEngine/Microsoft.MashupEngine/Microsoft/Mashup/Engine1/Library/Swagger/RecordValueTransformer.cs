using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x020003A2 RID: 930
	internal class RecordValueTransformer
	{
		// Token: 0x06002064 RID: 8292 RVA: 0x000552E4 File Offset: 0x000534E4
		public static RecordValue Transform(RecordValue record, RecordTypeValue resultingRecordType, Func<TypeValue, Value, Value> transformer)
		{
			if (resultingRecordType.Open)
			{
				throw ValueException.CastTypeMismatch(record, resultingRecordType);
			}
			int moreColumnsIndex;
			Value value;
			if (!resultingRecordType.TryGetMetaField("MoreColumns", out value) || !resultingRecordType.Fields.Keys.TryGetKeyIndex(value.AsString, out moreColumnsIndex))
			{
				moreColumnsIndex = -1;
			}
			RecordValue recordValue = RecordValue.New(resultingRecordType.SubtractMetaValue.AsType.AsRecordType, resultingRecordType.Fields.Keys.Select(delegate(string key, int i)
			{
				TypeValue asType = resultingRecordType.Fields[i]["Type"].AsType;
				if (i == moreColumnsIndex)
				{
					RecordTypeValue recordTypeValue = asType.AsRecordType;
					if (recordTypeValue == RecordTypeValue.Any)
					{
						recordTypeValue = RecordValueTransformer.CreateMoreColumnsColumnRecordType(record, resultingRecordType, moreColumnsIndex);
					}
					return RecordValueTransformer.Transform(record, recordTypeValue, transformer);
				}
				int num;
				if (record.Keys.TryGetKeyIndex(key, out num))
				{
					return new TransformValueReference(record.GetReference(num), new TransfromValue(asType, transformer));
				}
				if (asType.IsNullable)
				{
					return Value.Null;
				}
				return new ExceptionValueReference(ValueException.CastTypeMismatch(Value.Null, asType));
			}).ToArray<IValueReference>());
			if (record.MetaValue != RecordValue.Empty)
			{
				recordValue = recordValue.NewMeta(record.MetaValue).AsRecord;
			}
			return recordValue;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000553D4 File Offset: 0x000535D4
		public static RecordTypeValue CreateMoreColumnsRecordType(List<NamedValue> otherColumns)
		{
			return RecordValueTransformer.CreateMoreColumnsRecordType(otherColumns, RecordTypeValue.Any);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000553E4 File Offset: 0x000535E4
		public static RecordTypeValue CreateMoreColumnsRecordType(List<NamedValue> otherColumns, RecordTypeValue moreColumnsType)
		{
			string text = RecordValueTransformer.ConstructMoreColumnsName(otherColumns);
			otherColumns.Add(new NamedValue(text, RecordTypeValue.NewField(moreColumnsType, null)));
			return TypeServices.ConvertToMoreColumns(RecordTypeValue.New(RecordValue.New(otherColumns.ToArray())), text);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00055424 File Offset: 0x00053624
		private static string ConstructMoreColumnsName(List<NamedValue> otherColumns)
		{
			string text = "MoreColumns";
			IDictionary<string, NamedValue> dictionary = otherColumns.ToDictionary((NamedValue nv) => nv.Key);
			long num = 2L;
			while (dictionary.ContainsKey(text))
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", "MoreColumns", num);
				num += 1L;
			}
			return text;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0005548C File Offset: 0x0005368C
		private static RecordTypeValue CreateMoreColumnsColumnRecordType(RecordValue record, RecordTypeValue resultingRecordType, int moreColumnsIndex)
		{
			List<NamedValue> list = new List<NamedValue>();
			foreach (string text in record.Keys)
			{
				int num;
				if (!resultingRecordType.Fields.Keys.TryGetKeyIndex(text, out num) || moreColumnsIndex == num)
				{
					list.Add(new NamedValue(text, RecordTypeValue.NewField(TypeValue.Any, null)));
				}
			}
			return RecordTypeValue.New(RecordValue.New(list.ToArray()));
		}

		// Token: 0x04000C60 RID: 3168
		private const string MoreColumnsNameBase = "MoreColumns";
	}
}
