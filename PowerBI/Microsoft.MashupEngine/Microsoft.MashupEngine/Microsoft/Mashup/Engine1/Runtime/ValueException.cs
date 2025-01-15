using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200169F RID: 5791
	[Serializable]
	public sealed class ValueException : ValueException2
	{
		// Token: 0x06009317 RID: 37655 RVA: 0x001E5ED4 File Offset: 0x001E40D4
		private ValueException(RecordValue value, Exception innerException = null)
			: base(string.Empty, innerException)
		{
			this.value = value;
		}

		// Token: 0x06009318 RID: 37656 RVA: 0x001E5EEC File Offset: 0x001E40EC
		private ValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			List<string> list = new List<string>();
			List<Value> list2 = new List<Value>();
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					if (enumerator.Name.StartsWith("$", StringComparison.Ordinal))
					{
						list2.Add(ValueMarshaller.MarshalFromClr(enumerator.Value));
						list.Add(enumerator.Name.Substring("$".Length));
					}
				}
				catch (InvalidOperationException)
				{
				}
			}
			if (list.Count == 0)
			{
				this.value = RecordValue.Empty;
				return;
			}
			this.value = RecordValue.New(Keys.New(list.ToArray()), list2.ToArray());
		}

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x06009319 RID: 37657 RVA: 0x001E5FA4 File Offset: 0x001E41A4
		public RecordValue Value
		{
			get
			{
				RecordValue recordValue = this.value;
				string text = this.Data["Microsoft.Data.Mashup.Error.Context"] as string;
				if (text != null)
				{
					recordValue = recordValue.NewMeta(recordValue.MetaValue.Concatenate(RecordValue.New(ValueException.ContextKeys, new Value[] { TextValue.New(text) })).AsRecord).AsRecord;
				}
				if (this.stack != null)
				{
					recordValue = recordValue.NewMeta(recordValue.MetaValue.Concatenate(ValueException.Stack.GetTrace(this.stack)).AsRecord).AsRecord;
				}
				return recordValue;
			}
		}

		// Token: 0x0600931A RID: 37658 RVA: 0x001E6036 File Offset: 0x001E4236
		public void AddFrame(SourceLocation location)
		{
			if (this.stack == null)
			{
				this.stack = new List<SourceLocation>();
			}
			this.stack.Add(location);
		}

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x0600931B RID: 37659 RVA: 0x001E6057 File Offset: 0x001E4257
		public override IRecordValue Value2
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x0600931C RID: 37660 RVA: 0x001E6060 File Offset: 0x001E4260
		public override string Message
		{
			get
			{
				object reasonString = base.ReasonString;
				string messageString = base.MessageString;
				return Strings.ValueException_ReasonMessageFormat(reasonString, messageString);
			}
		}

		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x0600931D RID: 37661 RVA: 0x001E6088 File Offset: 0x001E4288
		public string MessageFormat
		{
			get
			{
				object reasonString = base.ReasonString;
				string messageFormatString = base.MessageFormatString;
				return Strings.ValueException_ReasonMessageFormat(reasonString, messageFormatString);
			}
		}

		// Token: 0x0600931E RID: 37662 RVA: 0x001E60B0 File Offset: 0x001E42B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			foreach (NamedValue namedValue in this.value.GetFields())
			{
				try
				{
					info.AddValue("$" + namedValue.Key, ValueMarshaller.MarshalToClr(namedValue.Value));
				}
				catch (ValueException)
				{
				}
			}
		}

		// Token: 0x0600931F RID: 37663 RVA: 0x001E6140 File Offset: 0x001E4340
		public override string ToString()
		{
			string text = base.ToString();
			string newLine = Environment.NewLine;
			RecordValue recordValue = this.value;
			return text + newLine + ((recordValue != null) ? recordValue.ToString() : null);
		}

		// Token: 0x06009320 RID: 37664 RVA: 0x001E6164 File Offset: 0x001E4364
		public static ValueException New(RecordValue value, Exception innerException = null)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			UserCredentialError.Check(value);
			return new ValueException(value, innerException);
		}

		// Token: 0x06009321 RID: 37665 RVA: 0x001E6181 File Offset: 0x001E4381
		public static ValueException NewDataSourceError(string message, Value detail, Exception innerException = null)
		{
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceError, TextValue.New(message), detail), innerException);
		}

		// Token: 0x06009322 RID: 37666 RVA: 0x001E619A File Offset: 0x001E439A
		public static ValueException NewDataSourceError<T>(T message, Value detail, Exception innerException = null) where T : IUserMessage
		{
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceError, message, detail), innerException);
		}

		// Token: 0x06009323 RID: 37667 RVA: 0x001E61B3 File Offset: 0x001E43B3
		public static ValueException NewDataSourceNotFound(string message, Value detail, Exception innerException = null)
		{
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceNotFound, TextValue.New(message), detail), innerException);
		}

		// Token: 0x06009324 RID: 37668 RVA: 0x001E61CC File Offset: 0x001E43CC
		public static ValueException NewDataSourceNotFound<T>(T message, Value detail, Exception innerException = null) where T : IUserMessage
		{
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceNotFound, message, detail), innerException);
		}

		// Token: 0x06009325 RID: 37669 RVA: 0x001E61E5 File Offset: 0x001E43E5
		public static ValueException NewExpressionError(string message, Value detail = null, Exception innerException = null)
		{
			return new ValueException(Library.Error.ExpressionError.Invoke(TextValue.New(message), detail ?? Microsoft.Mashup.Engine1.Runtime.Value.Null).AsRecord, innerException);
		}

		// Token: 0x06009326 RID: 37670 RVA: 0x001E620C File Offset: 0x001E440C
		public static ValueException NewExpressionError<T>(T message, Value detail = null, Exception innerException = null) where T : IUserMessage
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, message, detail ?? Microsoft.Mashup.Engine1.Runtime.Value.Null), innerException);
		}

		// Token: 0x06009327 RID: 37671 RVA: 0x001E622E File Offset: 0x001E442E
		public static ValueException NewDataFormatError(string message, Value detail, Exception innerException = null)
		{
			return ValueException.New(Library.Error.DataFormatError.Invoke(TextValue.New(message), detail).AsRecord, innerException);
		}

		// Token: 0x06009328 RID: 37672 RVA: 0x001E624C File Offset: 0x001E444C
		public static ValueException NewDataFormatError<T>(T message, Value detail, Exception innerException = null) where T : IUserMessage
		{
			return new ValueException(ErrorRecord.New(ValueException.DataFormatError, message, detail), innerException);
		}

		// Token: 0x06009329 RID: 37673 RVA: 0x001E6265 File Offset: 0x001E4465
		public static ValueException NotImplemented(string message)
		{
			return new ValueException(Library.Expression.NotImplemented.Invoke(TextValue.New(message)).AsRecord, null);
		}

		// Token: 0x0600932A RID: 37674 RVA: 0x001E6282 File Offset: 0x001E4482
		public static ValueException NotImplemented<T>(T message) where T : IUserMessage
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, message, Microsoft.Mashup.Engine1.Runtime.Value.Null), null);
		}

		// Token: 0x0600932B RID: 37675 RVA: 0x001E629F File Offset: 0x001E449F
		public static ValueException NumberOutOfRange(string message, Value detail, Exception innerException = null)
		{
			return new ValueException(Library.Error.ExpressionError.Invoke(TextValue.New(message), detail).AsRecord, innerException);
		}

		// Token: 0x0600932C RID: 37676 RVA: 0x001E62BD File Offset: 0x001E44BD
		public static ValueException NumberOutOfRange<T>(T message, Value detail, Exception innerException = null) where T : IUserMessage
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, message, detail), innerException);
		}

		// Token: 0x0600932D RID: 37677 RVA: 0x001E62D6 File Offset: 0x001E44D6
		public static ValueException ArgumentOutOfRange(string argumentName, Value detail)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ArgumentOutOfRange(PiiFree.New(argumentName)), detail), null);
		}

		// Token: 0x0600932E RID: 37678 RVA: 0x001E62FC File Offset: 0x001E44FC
		public static ValueException UnknownImport(string import, Value value)
		{
			if (import == Identifier.Underscore)
			{
				return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_UnderscoreOutsideEach, value), null);
			}
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_UnknownImport(import), value), null);
		}

		// Token: 0x0600932F RID: 37679 RVA: 0x001E6353 File Offset: 0x001E4553
		public static ValueException DuplicateExport(string export, Value value)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_DuplicateExport(export), value), null);
		}

		// Token: 0x06009330 RID: 37680 RVA: 0x001E6371 File Offset: 0x001E4571
		public static ValueException MissingField(string field, Value value)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_MissingField(field), value), null);
		}

		// Token: 0x06009331 RID: 37681 RVA: 0x001E6390 File Offset: 0x001E4590
		public static ValueException DuplicateField(string field)
		{
			Keys keys = Keys.New("Name", "Value");
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_DuplicateField(field), RecordValue.New(keys, new Value[]
			{
				TextValue.New(field),
				Microsoft.Mashup.Engine1.Runtime.Value.Null
			})), null);
		}

		// Token: 0x06009332 RID: 37682 RVA: 0x001E63E5 File Offset: 0x001E45E5
		public static ValueException KeyNotFound(Value key, TableValue table)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_KeyNotFound, RecordValue.New(Keys.New("Key", "Table"), new Value[] { key, table })), null);
		}

		// Token: 0x06009333 RID: 37683 RVA: 0x001E6423 File Offset: 0x001E4623
		public static ValueException KeyNotUnique(Value key, TableValue table)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_KeyNotUnique, RecordValue.New(Keys.New("Key", "Table"), new Value[] { key, table })), null);
		}

		// Token: 0x06009334 RID: 37684 RVA: 0x001E6461 File Offset: 0x001E4661
		public static ValueException KeyNotUnique(Value key)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_KeyNotUnique, RecordValue.New(Keys.New("Key"), new Value[] { key })), null);
		}

		// Token: 0x06009335 RID: 37685 RVA: 0x001E6498 File Offset: 0x001E4698
		public static ValueException StructureIndexCannotBeNegative(int index, Value list)
		{
			Keys keys = Keys.New("Value", "Index");
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.List_IndexCannotBeNegative, RecordValue.New(keys, new Value[]
			{
				list,
				NumberValue.New(index)
			})), null);
		}

		// Token: 0x06009336 RID: 37686 RVA: 0x001E64E8 File Offset: 0x001E46E8
		public static ValueException TextIndexOutOfRange(int index, TextValue text)
		{
			Keys keys = Keys.New("Text", "Index");
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_TextIndexOutOfRange, RecordValue.New(keys, new Value[]
			{
				text,
				NumberValue.New(index)
			})), null);
		}

		// Token: 0x06009337 RID: 37687 RVA: 0x001E6538 File Offset: 0x001E4738
		public static ValueException RecordIndexOutOfRange(int index, Value record)
		{
			Keys keys = Keys.New("Record", "Index");
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_RecordIndexOutOfRange, RecordValue.New(keys, new Value[]
			{
				record,
				NumberValue.New(index)
			})), null);
		}

		// Token: 0x06009338 RID: 37688 RVA: 0x001E6588 File Offset: 0x001E4788
		public static ValueException ListIndexOutOfRange(int index, ListValue list)
		{
			if (index >= 0)
			{
				return ValueException.InsufficientElements(list);
			}
			return ValueException.StructureIndexCannotBeNegative(index, list);
		}

		// Token: 0x06009339 RID: 37689 RVA: 0x001E659C File Offset: 0x001E479C
		public static ValueException InsufficientElements(Value list)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_InsufficientElements, list), null);
		}

		// Token: 0x0600933A RID: 37690 RVA: 0x001E65B9 File Offset: 0x001E47B9
		public static ValueException TooManyElements(Value value)
		{
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, Strings.ValueException_TooManyElements, value), null);
		}

		// Token: 0x0600933B RID: 37691 RVA: 0x001E65D6 File Offset: 0x001E47D6
		public static ValueException TableColumnNotFound(string columnName)
		{
			return ValueException.NewExpressionError<Message1>(Strings.ValueException_MissingColumn(columnName), TextValue.New(columnName), null);
		}

		// Token: 0x0600933C RID: 37692 RVA: 0x001E65EC File Offset: 0x001E47EC
		public static ValueException BinaryOperatorTypeMismatch(string op, Value left, Value right)
		{
			Keys keys = Keys.New("Operator", "Left", "Right");
			Func<object, object, object, Message3> func = new Func<object, object, object, Message3>(Strings.ValueException_BinaryOperatorTypeMismatch);
			if (op == "+" && ((left.IsList && right.IsList) || (left.IsRecord && right.IsRecord) || (left.IsTable && right.IsTable)))
			{
				func = new Func<object, object, object, Message3>(Strings.ValueException_BinaryOperatorAddTypeMismatch);
			}
			return ValueException.NewExpressionError<Message3>(func(PiiFree.New(op), left.Kind, right.Kind), RecordValue.New(keys, new Value[]
			{
				TextValue.New(op),
				left,
				right
			}), null);
		}

		// Token: 0x0600933D RID: 37693 RVA: 0x001E66AC File Offset: 0x001E48AC
		public static ValueException CastTypeMismatch(Value value, TypeValue type)
		{
			IUserMessage userMessage;
			if (value.Kind != type.TypeKind)
			{
				object obj;
				IPiiFree piiFree;
				switch (value.Kind)
				{
				case ValueKind.Null:
				case ValueKind.Time:
				case ValueKind.Date:
				case ValueKind.DateTime:
				case ValueKind.DateTimeZone:
				case ValueKind.Duration:
				case ValueKind.Number:
				case ValueKind.Logical:
					obj = value.ToSource();
					piiFree = PiiFree.New(Enum.GetName(typeof(ValueKind), type.TypeKind));
					userMessage = Strings.ValueException_CastTypeMismatch_Simple(obj, piiFree);
					goto IL_01F2;
				case ValueKind.Text:
				{
					string asString = value.AsString;
					obj = ((asString.Length > 20) ? (asString.Substring(0, 20) + "...") : asString);
					piiFree = PiiFree.New(Enum.GetName(typeof(ValueKind), type.TypeKind));
					userMessage = Strings.ValueException_CastTypeMismatch_Simple("\"" + ((obj != null) ? obj.ToString() : null) + "\"", piiFree);
					goto IL_01F2;
				}
				case ValueKind.Type:
					if (type.IsType)
					{
						piiFree = PiiFree.New(Enum.GetName(typeof(ValueKind), type.TypeKind));
						userMessage = Strings.ValueException_CastTypeMismatch_Types(piiFree);
						goto IL_01F2;
					}
					obj = PiiFree.New(Enum.GetName(typeof(ValueKind), value.Kind));
					piiFree = PiiFree.New(Enum.GetName(typeof(ValueKind), type.TypeKind));
					userMessage = Strings.ValueException_CastTypeMismatch_Complex(obj, piiFree);
					goto IL_01F2;
				}
				obj = PiiFree.New(Enum.GetName(typeof(ValueKind), value.Kind));
				piiFree = PiiFree.New(Enum.GetName(typeof(ValueKind), type.TypeKind));
				userMessage = Strings.ValueException_CastTypeMismatch_Complex(obj, piiFree);
			}
			else
			{
				userMessage = Strings.ValueException_CastTypeMismatch;
			}
			IL_01F2:
			return ValueException.NewExpressionError<IUserMessage>(userMessage, RecordValue.New(Keys.New("Value", "Type"), new Value[] { value, type }), null);
		}

		// Token: 0x0600933E RID: 37694 RVA: 0x001E68D4 File Offset: 0x001E4AD4
		public static ValueException UnaryOperatorTypeMismatch(string op, Value value)
		{
			Keys keys = Keys.New("Operator", "Value");
			return ValueException.NewExpressionError<Message2>(Strings.ValueException_UnaryOperatorTypeMismatch(PiiFree.New(op), value.Kind), RecordValue.New(keys, new Value[]
			{
				TextValue.New(op),
				value
			}), null);
		}

		// Token: 0x0600933F RID: 37695 RVA: 0x001E6928 File Offset: 0x001E4B28
		public static ValueException ElementAccessByKeyTypeMismatch(Value value, Value field)
		{
			Keys keys = Keys.New("Value", "Key");
			return ValueException.NewExpressionError<Message1>(Strings.ValueException_ElementAccessByKeyTypeMismatch(value.Kind), RecordValue.New(keys, new Value[] { value, field }), null);
		}

		// Token: 0x06009340 RID: 37696 RVA: 0x001E6970 File Offset: 0x001E4B70
		public static ValueException ElementAccessTypeMismatch(Value value, Value index)
		{
			Keys keys = Keys.New("Value", "Index");
			return ValueException.NewExpressionError<Message1>(Strings.ValueException_ElementAccessTypeMismatch(value.Kind), RecordValue.New(keys, new Value[] { value, index }), null);
		}

		// Token: 0x06009341 RID: 37697 RVA: 0x001E69B8 File Offset: 0x001E4BB8
		public static ValueException ElementAccessIndexTypeMismatch(Value value, Value index)
		{
			Keys keys = Keys.New("Value", "Index");
			return ValueException.NewExpressionError<Message1>(Strings.ValueException_ElementAccessIndexTypeMismatch(PiiFree.New(index.Kind)), RecordValue.New(keys, new Value[] { value, index }), null);
		}

		// Token: 0x06009342 RID: 37698 RVA: 0x001E6A04 File Offset: 0x001E4C04
		public static ValueException InvalidArguments(string message, Value function, Value[] arguments)
		{
			Keys keys = Keys.New("Pattern", "Arguments");
			return new ValueException(Library.Error.ExpressionError.Invoke(TextValue.New(message), RecordValue.New(keys, new Value[]
			{
				Microsoft.Mashup.Engine1.Runtime.Value.Null,
				ListValue.New(arguments)
			})).AsRecord, null);
		}

		// Token: 0x06009343 RID: 37699 RVA: 0x001E6A5C File Offset: 0x001E4C5C
		public static ValueException InvalidArguments<T>(T message, Value function, Value[] arguments) where T : IUserMessage
		{
			Keys keys = Keys.New("Pattern", "Arguments");
			return new ValueException(ErrorRecord.New(ValueException.ExpressionError, message, RecordValue.New(keys, new Value[]
			{
				Microsoft.Mashup.Engine1.Runtime.Value.Null,
				ListValue.New(arguments)
			})), null);
		}

		// Token: 0x06009344 RID: 37700 RVA: 0x001E6AAC File Offset: 0x001E4CAC
		public static ValueException InvalidArguments(Value function, params Value[] args)
		{
			FunctionTypeValue asFunctionType = function.Type.AsFunctionType;
			if (asFunctionType.Min == asFunctionType.Max)
			{
				return ValueException.InvalidArguments<Message2>(Strings.ValueException_InvalidArguments_WrongNumber_Exact(PiiFree.New(args.Length), PiiFree.New(asFunctionType.Min)), function, args);
			}
			if (asFunctionType.Max != 2147483647)
			{
				return ValueException.InvalidArguments<Message3>(Strings.ValueException_InvalidArguments_WrongNumber_TooFewOrTooMany(PiiFree.New(args.Length), PiiFree.New(asFunctionType.Min), PiiFree.New(asFunctionType.Max)), function, args);
			}
			return ValueException.InvalidArguments<Message2>(Strings.ValueException_InvalidArguments_WrongNumber_TooFew(PiiFree.New(args.Length), PiiFree.New(asFunctionType.Min)), function, args);
		}

		// Token: 0x06009345 RID: 37701 RVA: 0x001E6B4A File Offset: 0x001E4D4A
		public static ValueException ListCountTooLarge(long count)
		{
			return ValueException.NewDataFormatError<Message2>(Strings.List_CountTooLarge(PiiFree.New(int.MaxValue), PiiFree.New(ListValue.MaxCount)), NumberValue.New((double)count), null);
		}

		// Token: 0x06009346 RID: 37702 RVA: 0x001E6B72 File Offset: 0x001E4D72
		public static ValueException SkipCountTooLarge(double count)
		{
			return ValueException.NewDataFormatError<Message1>(Strings.Skip_CountTooLarge(PiiFree.New(ListValue.MaxCount)), NumberValue.New(count), null);
		}

		// Token: 0x06009347 RID: 37703 RVA: 0x001E6B8F File Offset: 0x001E4D8F
		public static ValueException NewParameterError(string message, Value details)
		{
			return new ValueException(ErrorRecord.New(ValueException.ParameterError, TextValue.New(message), details), null);
		}

		// Token: 0x06009348 RID: 37704 RVA: 0x001E6BA8 File Offset: 0x001E4DA8
		public static ValueException NewParameterError<T>(T message, Value details) where T : IUserMessage
		{
			return new ValueException(ErrorRecord.New(ValueException.ParameterError, message, details), null);
		}

		// Token: 0x04004E91 RID: 20113
		private const string SerializationPrefix = "$";

		// Token: 0x04004E92 RID: 20114
		private readonly RecordValue value;

		// Token: 0x04004E93 RID: 20115
		private List<SourceLocation> stack;

		// Token: 0x04004E94 RID: 20116
		public const string ContextKey = "Microsoft.Data.Mashup.Error.Context";

		// Token: 0x04004E95 RID: 20117
		private static readonly Keys ContextKeys = Keys.New("Microsoft.Data.Mashup.Error.Context");

		// Token: 0x04004E96 RID: 20118
		private static readonly Keys IsPiiKeys = Keys.New("Is.Pii");

		// Token: 0x04004E97 RID: 20119
		public static readonly RecordValue NonPii = RecordValue.New(ValueException.IsPiiKeys, new Value[] { LogicalValue.False });

		// Token: 0x04004E98 RID: 20120
		public static readonly TextValue DataFormatError = TextValue.New("DataFormat.Error").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E99 RID: 20121
		public static readonly TextValue ExpressionError = TextValue.New("Expression.Error").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E9A RID: 20122
		public static readonly TextValue DataSourceError = TextValue.New("DataSource.Error").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E9B RID: 20123
		public static readonly TextValue DataSourceNotFound = TextValue.New("DataSource.NotFound").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E9C RID: 20124
		public static readonly TextValue DataSourceMissingClientLibrary = TextValue.New("DataSource.MissingClientLibrary").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E9D RID: 20125
		public static readonly TextValue DataSourceChanged = TextValue.New("DataSource.Changed").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E9E RID: 20126
		public static readonly TextValue DataSourceCapacityExceeded = TextValue.New("DataSource.CapacityExceeded").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x04004E9F RID: 20127
		public static readonly TextValue ParameterError = TextValue.New("Parameter.Error").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x020016A0 RID: 5792
		public static class Stack
		{
			// Token: 0x0600934A RID: 37706 RVA: 0x001E6CFC File Offset: 0x001E4EFC
			public static Value GetTrace(IList<SourceLocation> stack)
			{
				Value[] array = new Value[stack.Count];
				for (int i = 0; i < stack.Count; i++)
				{
					array[i] = ValueException.Stack.GetFrame(stack[i]);
				}
				return RecordValue.New(ValueException.Stack.stackFields, new Value[] { ListValue.New(array) });
			}

			// Token: 0x0600934B RID: 37707 RVA: 0x001E6D50 File Offset: 0x001E4F50
			private static Value GetFrame(SourceLocation location)
			{
				Value[] array = new Value[]
				{
					Microsoft.Mashup.Engine1.Runtime.Value.Null,
					ValueException.Stack.GetLocation(location)
				};
				return RecordValue.New(ValueException.Stack.frameFields, array);
			}

			// Token: 0x0600934C RID: 37708 RVA: 0x001E6D80 File Offset: 0x001E4F80
			private static Value GetLocation(SourceLocation location)
			{
				ITranslateSourceLocation translateSourceLocation = location.Document as ITranslateSourceLocation;
				if (translateSourceLocation != null)
				{
					location = translateSourceLocation.TranslateSourceLocation(location);
				}
				TextPosition start = location.Range.Start;
				TextPosition end = location.Range.End;
				bool flag = start != end;
				Value[] array = new Value[]
				{
					ValueException.Stack.GetDocumentID(location.Document),
					flag ? ValueException.Stack.GetPosition(start) : Microsoft.Mashup.Engine1.Runtime.Value.Null,
					flag ? ValueException.Stack.GetPosition(end) : Microsoft.Mashup.Engine1.Runtime.Value.Null
				};
				return RecordValue.New(ValueException.Stack.locationFields, array);
			}

			// Token: 0x0600934D RID: 37709 RVA: 0x001E6E18 File Offset: 0x001E5018
			private static Value GetDocumentID(IDocumentHost document)
			{
				string uniqueID = document.UniqueID;
				if (uniqueID == null)
				{
					return Microsoft.Mashup.Engine1.Runtime.Value.Null;
				}
				return TextValue.New(uniqueID);
			}

			// Token: 0x0600934E RID: 37710 RVA: 0x001E6E3C File Offset: 0x001E503C
			private static Value GetPosition(TextPosition position)
			{
				Value[] array = new Value[]
				{
					NumberValue.New(position.Row),
					NumberValue.New(position.Column)
				};
				return RecordValue.New(ValueException.Stack.positionFields, array);
			}

			// Token: 0x04004EA0 RID: 20128
			public const string traceKey = "Expression.Stack";

			// Token: 0x04004EA1 RID: 20129
			private static readonly Keys frameFields = Keys.New("Function", "Location");

			// Token: 0x04004EA2 RID: 20130
			private static readonly Keys locationFields = Keys.New("Section", "Start", "End");

			// Token: 0x04004EA3 RID: 20131
			private static readonly Keys positionFields = Keys.New("Row", "Column");

			// Token: 0x04004EA4 RID: 20132
			private static readonly Keys stackFields = Keys.New("Expression.Stack");
		}
	}
}
