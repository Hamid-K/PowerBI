using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016BE RID: 5822
	internal abstract class ValueSerializer
	{
		// Token: 0x06009419 RID: 37913 RVA: 0x001E8EF8 File Offset: 0x001E70F8
		protected ValueSerializer(IValueWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x0600941A RID: 37914 RVA: 0x001E8F07 File Offset: 0x001E7107
		protected IValueWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x0600941B RID: 37915 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool ShouldSkip(Value value)
		{
			return false;
		}

		// Token: 0x0600941C RID: 37916 RVA: 0x001E8F0F File Offset: 0x001E710F
		public virtual void WriteStartValue(ValueKind kind, ValueFlags flags)
		{
			this.Writer.WriteStartValue(kind, flags);
		}

		// Token: 0x0600941D RID: 37917 RVA: 0x001E8F1E File Offset: 0x001E711E
		public virtual void WriteEndValue()
		{
			this.Writer.WriteEndValue();
		}

		// Token: 0x0600941E RID: 37918 RVA: 0x001E8F2B File Offset: 0x001E712B
		public virtual void WriteNull()
		{
			this.Writer.WriteNull();
		}

		// Token: 0x0600941F RID: 37919 RVA: 0x001E8F38 File Offset: 0x001E7138
		public virtual void WriteTime(TimeValue time)
		{
			this.Writer.WriteTime(time.AsClrTimeSpan);
		}

		// Token: 0x06009420 RID: 37920 RVA: 0x001E8F4B File Offset: 0x001E714B
		public virtual void WriteDate(DateValue date)
		{
			this.Writer.WriteDate(date.AsClrDateTime);
		}

		// Token: 0x06009421 RID: 37921 RVA: 0x001E8F5E File Offset: 0x001E715E
		public virtual void WriteDateTime(DateTimeValue dateTime)
		{
			this.Writer.WriteDateTime(dateTime.AsClrDateTime);
		}

		// Token: 0x06009422 RID: 37922 RVA: 0x001E8F71 File Offset: 0x001E7171
		public virtual void WriteDateTimeZone(DateTimeZoneValue dateTimeZone)
		{
			this.Writer.WriteDateTimeZone(dateTimeZone.AsClrDateTimeOffset);
		}

		// Token: 0x06009423 RID: 37923 RVA: 0x001E8F84 File Offset: 0x001E7184
		public virtual void WriteDuration(DurationValue duration)
		{
			this.Writer.WriteDuration(duration.AsClrTimeSpan);
		}

		// Token: 0x06009424 RID: 37924 RVA: 0x001E8F98 File Offset: 0x001E7198
		public virtual void WriteNumber(NumberValue number)
		{
			NumberKind numberKind = ValueHelper.NumberKind(number);
			this.Writer.WriteNumberKind(numberKind);
			switch (numberKind)
			{
			case NumberKind.Int32:
				this.Writer.WriteNumber(number.AsInteger32);
				return;
			case NumberKind.Double:
				this.Writer.WriteNumber(number.AsDouble);
				return;
			case NumberKind.Decimal:
				this.Writer.WriteNumber(number.AsDecimal);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009425 RID: 37925 RVA: 0x001E9007 File Offset: 0x001E7207
		public virtual void WriteLogical(LogicalValue logical)
		{
			this.Writer.WriteLogical(logical.AsBoolean);
		}

		// Token: 0x06009426 RID: 37926 RVA: 0x001E901A File Offset: 0x001E721A
		public virtual void WriteText(TextValue text)
		{
			this.Writer.WriteText(text.AsString);
		}

		// Token: 0x06009427 RID: 37927 RVA: 0x001E9030 File Offset: 0x001E7230
		public virtual void WriteBinary(BinaryValue binary)
		{
			this.Writer.WriteStartBinary();
			byte[] array = new byte[16384];
			Stream stream = binary.Open();
			int num;
			while ((num = stream.Read(array, 0, 1024)) > 0)
			{
				this.Writer.WriteBinary(array, 0, num);
			}
			this.Writer.WriteEndBinary();
		}

		// Token: 0x06009428 RID: 37928 RVA: 0x001E9088 File Offset: 0x001E7288
		public virtual void WriteList(ListValue list)
		{
			this.Writer.WriteStartList();
			foreach (IValueReference valueReference in list)
			{
				this.Write(valueReference.Value);
			}
			this.Writer.WriteEndList();
		}

		// Token: 0x06009429 RID: 37929 RVA: 0x001E90EC File Offset: 0x001E72EC
		public virtual void WriteRecord(RecordValue record)
		{
			Keys keys = record.Keys;
			this.Writer.WriteStartRecord(keys);
			for (int i = 0; i < keys.Length; i++)
			{
				this.Write(record[i]);
			}
			this.Writer.WriteEndRecord();
		}

		// Token: 0x0600942A RID: 37930 RVA: 0x001E9138 File Offset: 0x001E7338
		public virtual void WriteTable(TableValue table)
		{
			Keys columns = table.Columns;
			this.Writer.WriteStartTable(columns);
			foreach (IValueReference valueReference in table)
			{
				this.Writer.WriteStartRow();
				for (int i = 0; i < columns.Length; i++)
				{
					this.Write(valueReference.Value[i]);
				}
				this.Writer.WriteEndRow();
			}
			this.Writer.WriteEndTable();
		}

		// Token: 0x0600942B RID: 37931 RVA: 0x001E91D0 File Offset: 0x001E73D0
		public virtual void WriteFunction(FunctionValue function)
		{
			FunctionTypeValue asFunctionType = function.Type.AsFunctionType;
			this.Writer.WriteFunction(asFunctionType.Parameters.Keys, asFunctionType.Min);
		}

		// Token: 0x0600942C RID: 37932 RVA: 0x001E9205 File Offset: 0x001E7405
		public virtual void WriteAction()
		{
			this.Writer.WriteAction();
		}

		// Token: 0x0600942D RID: 37933 RVA: 0x001E9212 File Offset: 0x001E7412
		public virtual void WriteStartType(ValueKind kind, bool nullable)
		{
			this.Writer.WriteStartType(kind, nullable);
		}

		// Token: 0x0600942E RID: 37934 RVA: 0x001E9221 File Offset: 0x001E7421
		public virtual void WriteEndType()
		{
			this.Writer.WriteEndType();
		}

		// Token: 0x0600942F RID: 37935 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WritePrimitiveType(ValueKind kind)
		{
		}

		// Token: 0x06009430 RID: 37936 RVA: 0x001E922E File Offset: 0x001E742E
		public virtual void WriteListType(ListTypeValue listType)
		{
			this.Write(listType.ItemType);
		}

		// Token: 0x06009431 RID: 37937 RVA: 0x001E923C File Offset: 0x001E743C
		public virtual void WriteRecordType(RecordTypeValue recordType)
		{
			RecordValue fields = recordType.Fields;
			this.Writer.WriteStartRecordType(fields.Keys, recordType.Open);
			for (int i = 0; i < fields.Keys.Length; i++)
			{
				RecordValue asRecord = fields[i].AsRecord;
				this.Writer.WriteFieldType(asRecord["Optional"].AsBoolean);
				this.Write(asRecord["Type"].AsType);
			}
			this.Writer.WriteEndRecordType();
		}

		// Token: 0x06009432 RID: 37938 RVA: 0x001E92C8 File Offset: 0x001E74C8
		public virtual void WriteTableType(TableTypeValue tableType)
		{
			TableKey[] array = new TableKey[tableType.TableKeys.Count];
			tableType.TableKeys.CopyTo(array, 0);
			this.Writer.WriteTableType(array);
			this.Write(tableType.ItemType);
		}

		// Token: 0x06009433 RID: 37939 RVA: 0x001E930C File Offset: 0x001E750C
		public virtual void WriteFunctionType(FunctionTypeValue functionType)
		{
			RecordValue parameters = functionType.Parameters;
			this.Writer.WriteStartFunctionType(parameters.Keys, functionType.Min);
			for (int i = 0; i < parameters.Keys.Length; i++)
			{
				this.Write(parameters[i].AsType);
			}
			this.Writer.WriteEndFunctionType();
		}

		// Token: 0x06009434 RID: 37940 RVA: 0x001E936C File Offset: 0x001E756C
		public virtual void WriteType(TypeValue type)
		{
			if (this.ShouldSkip(type))
			{
				this.WriteSkipped();
				return;
			}
			this.WriteStartType(type.TypeKind, type.IsNullable);
			switch (type.TypeKind)
			{
			case ValueKind.None:
			case ValueKind.Any:
			case ValueKind.Null:
			case ValueKind.Time:
			case ValueKind.Date:
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
			case ValueKind.Duration:
			case ValueKind.Number:
			case ValueKind.Logical:
			case ValueKind.Text:
			case ValueKind.Binary:
			case ValueKind.Type:
			case ValueKind.Action:
				this.WritePrimitiveType(type.TypeKind);
				break;
			case ValueKind.List:
				this.WriteListType(type.AsListType);
				break;
			case ValueKind.Record:
				this.WriteRecordType(type.AsRecordType);
				break;
			case ValueKind.Table:
				this.WriteTableType(type.AsTableType);
				break;
			case ValueKind.Function:
				this.WriteFunctionType(type.AsFunctionType);
				break;
			default:
				throw new InvalidOperationException();
			}
			this.WriteEndType();
		}

		// Token: 0x06009435 RID: 37941 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void WriteSkipped()
		{
		}

		// Token: 0x06009436 RID: 37942 RVA: 0x001E9448 File Offset: 0x001E7648
		public virtual void Write(Value value)
		{
			if (this.ShouldSkip(value))
			{
				this.Writer.WriteStartValue(ValueKind.Skipped, ValueFlags.None);
				this.WriteSkipped();
				this.Writer.WriteEndValue();
				return;
			}
			ValueFlags valueFlags = ValueHelper.Flags(value);
			this.WriteStartValue(value.Kind, valueFlags);
			if ((valueFlags & ValueFlags.HasMeta) == ValueFlags.HasMeta)
			{
				this.Write(value.MetaValue);
			}
			if ((valueFlags & ValueFlags.HasType) == ValueFlags.HasType)
			{
				this.Write(value.Type);
			}
			switch (value.Kind)
			{
			case ValueKind.Null:
				this.WriteNull();
				break;
			case ValueKind.Time:
				this.WriteTime(value.AsTime);
				break;
			case ValueKind.Date:
				this.WriteDate(value.AsDate);
				break;
			case ValueKind.DateTime:
				this.WriteDateTime(value.AsDateTime);
				break;
			case ValueKind.DateTimeZone:
				this.WriteDateTimeZone(value.AsDateTimeZone);
				break;
			case ValueKind.Duration:
				this.WriteDuration(value.AsDuration);
				break;
			case ValueKind.Number:
				this.WriteNumber(value.AsNumber);
				break;
			case ValueKind.Logical:
				this.WriteLogical(value.AsLogical);
				break;
			case ValueKind.Text:
				this.WriteText(value.AsText);
				break;
			case ValueKind.Binary:
				this.WriteBinary(value.AsBinary);
				break;
			case ValueKind.List:
				this.WriteList(value.AsList);
				break;
			case ValueKind.Record:
				this.WriteRecord(value.AsRecord);
				break;
			case ValueKind.Table:
				this.WriteTable(value.AsTable);
				break;
			case ValueKind.Function:
				this.WriteFunction(value.AsFunction);
				break;
			case ValueKind.Type:
				this.WriteType(value.AsType);
				break;
			case ValueKind.Action:
				this.WriteAction();
				break;
			default:
				throw new InvalidOperationException();
			}
			this.WriteEndValue();
		}

		// Token: 0x04004EE6 RID: 20198
		private readonly IValueWriter writer;
	}
}
