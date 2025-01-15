using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016BA RID: 5818
	public abstract class ValueDeserializer
	{
		// Token: 0x060093F3 RID: 37875 RVA: 0x001E8697 File Offset: 0x001E6897
		protected ValueDeserializer(IValueReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x060093F4 RID: 37876 RVA: 0x001E86A6 File Offset: 0x001E68A6
		protected IValueReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x060093F5 RID: 37877 RVA: 0x001E86AE File Offset: 0x001E68AE
		public virtual void ReadStartValue(out ValueKind kind, out ValueFlags flags)
		{
			this.Reader.ReadStartValue(out kind, out flags);
		}

		// Token: 0x060093F6 RID: 37878 RVA: 0x001E86BD File Offset: 0x001E68BD
		public virtual void ReadEndValue()
		{
			this.Reader.ReadEndValue();
		}

		// Token: 0x060093F7 RID: 37879 RVA: 0x001E86CA File Offset: 0x001E68CA
		public virtual NullValue ReadNull()
		{
			this.Reader.ReadNull();
			return NullValue.Instance;
		}

		// Token: 0x060093F8 RID: 37880 RVA: 0x001E86DC File Offset: 0x001E68DC
		public virtual TimeValue ReadTime()
		{
			return TimeValue.New(this.Reader.ReadTime());
		}

		// Token: 0x060093F9 RID: 37881 RVA: 0x001E86EE File Offset: 0x001E68EE
		public virtual DateValue ReadDate()
		{
			return DateValue.New(this.Reader.ReadDate());
		}

		// Token: 0x060093FA RID: 37882 RVA: 0x001E8700 File Offset: 0x001E6900
		public virtual DateTimeValue ReadDateTime()
		{
			return DateTimeValue.New(this.Reader.ReadDateTime());
		}

		// Token: 0x060093FB RID: 37883 RVA: 0x001E8712 File Offset: 0x001E6912
		public virtual DateTimeZoneValue ReadDateTimeZone()
		{
			return DateTimeZoneValue.New(this.Reader.ReadDateTimeZone());
		}

		// Token: 0x060093FC RID: 37884 RVA: 0x001E8724 File Offset: 0x001E6924
		public virtual DurationValue ReadDuration()
		{
			return DurationValue.New(this.Reader.ReadDuration());
		}

		// Token: 0x060093FD RID: 37885 RVA: 0x001E8738 File Offset: 0x001E6938
		public virtual NumberValue ReadNumber()
		{
			switch (this.Reader.ReadNumberKind())
			{
			case NumberKind.Int32:
				return NumberValue.New(this.Reader.ReadInt32Number());
			case NumberKind.Double:
				return NumberValue.New(this.Reader.ReadDoubleNumber());
			case NumberKind.Decimal:
				return NumberValue.New(this.Reader.ReadDecimalNumber());
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060093FE RID: 37886 RVA: 0x001E879D File Offset: 0x001E699D
		public virtual LogicalValue ReadLogical()
		{
			return LogicalValue.New(this.Reader.ReadLogical());
		}

		// Token: 0x060093FF RID: 37887 RVA: 0x001E87AF File Offset: 0x001E69AF
		public virtual TextValue ReadText()
		{
			return TextValue.New(this.Reader.ReadText());
		}

		// Token: 0x06009400 RID: 37888 RVA: 0x001E87C4 File Offset: 0x001E69C4
		public virtual BinaryValue ReadBinary()
		{
			BinaryValue binaryValue;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				long num = this.Reader.ReadStartBinary() - this.Reader.BaseStream.Position;
				int num2 = 16384;
				byte[] array = new byte[num2];
				while (num > 0L)
				{
					if (num < (long)num2)
					{
						num2 = (int)num;
					}
					int num3 = this.Reader.ReadBinary(array, 0, num2);
					memoryStream.Write(array, 0, num3);
					num -= (long)num3;
				}
				this.Reader.ReadEndBinary();
				binaryValue = BinaryValue.New(memoryStream.ToArray());
			}
			return binaryValue;
		}

		// Token: 0x06009401 RID: 37889 RVA: 0x001E8868 File Offset: 0x001E6A68
		public virtual ListValue ReadList()
		{
			this.Reader.ReadStartList();
			List<Value> list = new List<Value>();
			while (!this.Reader.EndOfList)
			{
				list.Add(this.Read());
			}
			this.Reader.ReadEndList();
			return ListValue.New(list.ToArray());
		}

		// Token: 0x06009402 RID: 37890 RVA: 0x001E88B8 File Offset: 0x001E6AB8
		public virtual RecordValue ReadRecord()
		{
			Keys keys;
			this.Reader.ReadStartRecord(out keys);
			Value[] array = new Value[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this.Read();
			}
			this.Reader.ReadEndRecord();
			return RecordValue.New(keys, array);
		}

		// Token: 0x06009403 RID: 37891 RVA: 0x001E890C File Offset: 0x001E6B0C
		public virtual TableValue ReadTable()
		{
			Keys keys;
			this.Reader.ReadStartTable(out keys);
			int length = keys.Length;
			List<Value> list = new List<Value>();
			while (!this.Reader.EndOfTable)
			{
				this.Reader.ReadStartRow();
				Value[] array = new Value[length];
				for (int i = 0; i < length; i++)
				{
					array[i] = this.Read();
				}
				this.Reader.ReadEndRow();
				list.Add(ListValue.New(array));
			}
			this.Reader.ReadEndTable();
			return TableModule.Table.FromRows.Invoke(ListValue.New(list.ToArray()), ValueDeserializer.ListFromKeys(keys)).AsTable;
		}

		// Token: 0x06009404 RID: 37892 RVA: 0x001E89B4 File Offset: 0x001E6BB4
		public virtual FunctionValue ReadFunction()
		{
			Keys keys;
			int num;
			this.Reader.ReadFunction(out keys, out num);
			return new ValueDeserializer.DummyFunctionValueN(num, keys);
		}

		// Token: 0x06009405 RID: 37893 RVA: 0x001E89D7 File Offset: 0x001E6BD7
		public virtual ActionValue ReadAction()
		{
			this.Reader.ReadAction();
			return new ValueDeserializer.DummyActionValue();
		}

		// Token: 0x06009406 RID: 37894 RVA: 0x001E89E9 File Offset: 0x001E6BE9
		public virtual void ReadStartType(out ValueKind kind, out bool nullable)
		{
			this.Reader.ReadStartType(out kind, out nullable);
		}

		// Token: 0x06009407 RID: 37895 RVA: 0x001E89F8 File Offset: 0x001E6BF8
		public virtual void ReadEndType()
		{
			this.Reader.ReadEndType();
		}

		// Token: 0x06009408 RID: 37896 RVA: 0x001E8A05 File Offset: 0x001E6C05
		public virtual TypeValue ReadPrimitiveType(ValueKind kind, bool nullable)
		{
			return ValueHelper.PrimitiveType(kind, nullable);
		}

		// Token: 0x06009409 RID: 37897 RVA: 0x001E8A0E File Offset: 0x001E6C0E
		public virtual ListTypeValue ReadListType()
		{
			return ListTypeValue.New(this.Read().AsType);
		}

		// Token: 0x0600940A RID: 37898 RVA: 0x001E8A20 File Offset: 0x001E6C20
		public virtual RecordTypeValue ReadRecordType()
		{
			Keys keys;
			bool flag;
			this.Reader.ReadStartRecordType(out keys, out flag);
			Value[] array = new Value[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				bool flag2;
				this.Reader.ReadFieldType(out flag2);
				TypeValue asType = this.Read().AsType;
				array[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					asType,
					LogicalValue.New(flag2)
				});
			}
			RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(keys, array), flag);
			this.Reader.ReadEndRecordType();
			return recordTypeValue;
		}

		// Token: 0x0600940B RID: 37899 RVA: 0x001E8AB0 File Offset: 0x001E6CB0
		public virtual TableTypeValue ReadTableType()
		{
			TableKey[] array;
			this.Reader.ReadTableType(out array);
			return TableTypeValue.New(this.Read().AsType.AsRecordType, array);
		}

		// Token: 0x0600940C RID: 37900 RVA: 0x001E8AE0 File Offset: 0x001E6CE0
		public virtual FunctionTypeValue ReadFunctionType()
		{
			Keys keys;
			int num;
			this.Reader.ReadStartFunctionType(out keys, out num);
			Value[] array = new Value[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = this.Read().AsType;
			}
			FunctionTypeValue functionTypeValue = FunctionTypeValue.New(TypeValue.Any, RecordValue.New(keys, array), num);
			this.Reader.ReadEndFunctionType();
			return functionTypeValue;
		}

		// Token: 0x0600940D RID: 37901 RVA: 0x001E8B44 File Offset: 0x001E6D44
		public virtual TypeValue ReadType()
		{
			ValueKind valueKind;
			bool flag;
			this.ReadStartType(out valueKind, out flag);
			TypeValue typeValue;
			switch (valueKind)
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
				typeValue = this.ReadPrimitiveType(valueKind, flag);
				flag = false;
				break;
			case ValueKind.List:
				typeValue = this.ReadListType();
				break;
			case ValueKind.Record:
				typeValue = this.ReadRecordType();
				break;
			case ValueKind.Table:
				typeValue = this.ReadTableType();
				break;
			case ValueKind.Function:
				typeValue = this.ReadFunctionType();
				break;
			default:
				throw new InvalidOperationException();
			}
			if (flag)
			{
				typeValue = typeValue.Nullable;
			}
			this.ReadEndType();
			return typeValue;
		}

		// Token: 0x0600940E RID: 37902 RVA: 0x00019E42 File Offset: 0x00018042
		public virtual Value ReadSkipped()
		{
			return Value.Null;
		}

		// Token: 0x0600940F RID: 37903 RVA: 0x001E8BF8 File Offset: 0x001E6DF8
		public virtual Value Read()
		{
			ValueKind valueKind;
			ValueFlags valueFlags;
			this.ReadStartValue(out valueKind, out valueFlags);
			RecordValue recordValue = (((valueFlags & ValueFlags.HasMeta) == ValueFlags.HasMeta) ? this.Read().AsRecord : null);
			TypeValue typeValue = (((valueFlags & ValueFlags.HasType) == ValueFlags.HasType) ? this.Read().AsType : null);
			Value value;
			switch (valueKind)
			{
			case ValueKind.Skipped:
				value = this.ReadSkipped();
				goto IL_0152;
			case ValueKind.Null:
				value = this.ReadNull();
				goto IL_0152;
			case ValueKind.Time:
				value = this.ReadTime();
				goto IL_0152;
			case ValueKind.Date:
				value = this.ReadDate();
				goto IL_0152;
			case ValueKind.DateTime:
				value = this.ReadDateTime();
				goto IL_0152;
			case ValueKind.DateTimeZone:
				value = this.ReadDateTimeZone();
				goto IL_0152;
			case ValueKind.Duration:
				value = this.ReadDuration();
				goto IL_0152;
			case ValueKind.Number:
				value = this.ReadNumber();
				goto IL_0152;
			case ValueKind.Logical:
				value = this.ReadLogical();
				goto IL_0152;
			case ValueKind.Text:
				value = this.ReadText();
				goto IL_0152;
			case ValueKind.Binary:
				value = this.ReadBinary();
				goto IL_0152;
			case ValueKind.List:
				value = this.ReadList();
				goto IL_0152;
			case ValueKind.Record:
				value = this.ReadRecord();
				goto IL_0152;
			case ValueKind.Table:
				value = this.ReadTable();
				goto IL_0152;
			case ValueKind.Function:
				value = this.ReadFunction();
				goto IL_0152;
			case ValueKind.Type:
				value = this.ReadType();
				goto IL_0152;
			case ValueKind.Action:
				value = this.ReadAction();
				goto IL_0152;
			}
			throw new InvalidOperationException();
			IL_0152:
			if (recordValue != null)
			{
				value = value.NewMeta(recordValue);
			}
			if (typeValue != null)
			{
				value = value.NewType(typeValue);
			}
			this.ReadEndValue();
			return value;
		}

		// Token: 0x06009410 RID: 37904 RVA: 0x001E8D7C File Offset: 0x001E6F7C
		private static string[] StringsFromKeys(Keys keys)
		{
			string[] array = new string[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = keys[i];
			}
			return array;
		}

		// Token: 0x06009411 RID: 37905 RVA: 0x001E8DB4 File Offset: 0x001E6FB4
		private static ListValue ListFromKeys(Keys keys)
		{
			Value[] array = new TextValue[keys.Length];
			Value[] array2 = array;
			for (int i = 0; i < keys.Length; i++)
			{
				array2[i] = TextValue.New(keys[i]);
			}
			return ListValue.New(array2);
		}

		// Token: 0x06009412 RID: 37906 RVA: 0x001E8DF8 File Offset: 0x001E6FF8
		private static ListValue ListFromStrings(string[] strings)
		{
			Value[] array = new TextValue[strings.Length];
			Value[] array2 = array;
			for (int i = 0; i < strings.Length; i++)
			{
				array2[i] = TextValue.New(strings[i]);
			}
			return ListValue.New(array2);
		}

		// Token: 0x04004EE5 RID: 20197
		private readonly IValueReader reader;

		// Token: 0x020016BB RID: 5819
		private class DummyFunctionValueN : NativeFunctionValueN
		{
			// Token: 0x06009413 RID: 37907 RVA: 0x001E8E2F File Offset: 0x001E702F
			public DummyFunctionValueN(int min, Keys paramNames)
				: base(min, ValueDeserializer.StringsFromKeys(paramNames))
			{
			}

			// Token: 0x06009414 RID: 37908 RVA: 0x001E8E3E File Offset: 0x001E703E
			protected override Value InvokeN(Value[] args)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NotImplementedFunction_NotImplemented, null, null);
			}
		}

		// Token: 0x020016BC RID: 5820
		private class DummyActionValue : ActionValue
		{
			// Token: 0x06009415 RID: 37909 RVA: 0x001E8E3E File Offset: 0x001E703E
			public override Value Execute()
			{
				throw ValueException.NewExpressionError<Message0>(Strings.NotImplementedFunction_NotImplemented, null, null);
			}
		}
	}
}
