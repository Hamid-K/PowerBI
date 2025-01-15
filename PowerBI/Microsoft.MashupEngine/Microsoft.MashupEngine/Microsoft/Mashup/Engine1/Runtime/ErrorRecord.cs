using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Host;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012EA RID: 4842
	public static class ErrorRecord
	{
		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x0600803A RID: 32826 RVA: 0x001B548C File Offset: 0x001B368C
		public static RecordTypeValue _Type
		{
			get
			{
				if (ErrorRecord.type == null)
				{
					ErrorRecord.type = RecordTypeValue.New(RecordValue.New(ErrorRecord.Fields, new Value[]
					{
						RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Text,
							LogicalValue.False
						}),
						RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Text.Nullable,
							LogicalValue.False
						}),
						RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Any,
							LogicalValue.False
						}),
						RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Text.Nullable,
							LogicalValue.False
						}),
						RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.List.Nullable,
							LogicalValue.False
						})
					}));
				}
				return ErrorRecord.type;
			}
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x001B5580 File Offset: 0x001B3780
		public static RecordValue New(TextValue message)
		{
			return ErrorRecord.New(ErrorRecord.DefaultReason, message, Value.Null, message, Value.Null);
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x001B5598 File Offset: 0x001B3798
		public static RecordValue New(TextValue messageFormat, ListValue messageParameters)
		{
			return ErrorRecord.New(ErrorRecord.DefaultReason, messageFormat, Value.Null, messageParameters);
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x001B55AB File Offset: 0x001B37AB
		public static RecordValue New(TextValue reason, Value message, Value detail)
		{
			return ErrorRecord.New(reason, message, detail, message, Value.Null);
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x001B55BB File Offset: 0x001B37BB
		public static RecordValue New(TextValue reason, IUserMessage message, Value detail)
		{
			return ErrorRecord.New(reason, message.Message.NewMeta(ValueException.NonPii), detail, message.Parameters);
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x001B55DC File Offset: 0x001B37DC
		public static RecordValue New(TextValue reason, Value messageFormat, Value detail, Value messageParameters)
		{
			if (messageFormat.IsNull)
			{
				return ErrorRecord.New(reason, Value.Null, detail, Value.Null, Value.Null);
			}
			Value value = ErrorRecord.textFormatFunction.Invoke(messageFormat, messageParameters.IsNull ? ListValue.Empty : messageParameters.AsList);
			Value value2;
			if (messageFormat.TryGetMetaField("Is.Pii", out value2) && value2.IsLogical && value2.AsBoolean.Equals(false))
			{
				if (!messageParameters.IsNull)
				{
					using (IEnumerator<IValueReference> enumerator = messageParameters.AsList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!((Value)enumerator.Current).TryGetMetaField("Is.Pii", out value2) || !value2.IsLogical || value2.AsBoolean.Equals(true))
							{
								return ErrorRecord.New(reason, value, detail, messageFormat, messageParameters);
							}
						}
					}
				}
				value = value.NewMeta(ValueException.NonPii);
			}
			return ErrorRecord.New(reason, value, detail, messageFormat, messageParameters);
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x001B56EC File Offset: 0x001B38EC
		public static RecordValue New(TextValue reason, Value message, Value detail, Value messageFormat, Value messageParameters)
		{
			return RecordValue.New(ErrorRecord._Type, new Value[]
			{
				reason,
				(!message.IsNull) ? message.AsText : Value.Null,
				detail,
				(!messageFormat.IsNull) ? messageFormat.AsText : Value.Null,
				(!messageParameters.IsNull) ? ((messageParameters.AsList != ListValue.Empty) ? messageParameters.AsList : Value.Null) : Value.Null
			});
		}

		// Token: 0x040045CF RID: 17871
		public static string ReasonKey = "Reason";

		// Token: 0x040045D0 RID: 17872
		public static string MessageKey = "Message";

		// Token: 0x040045D1 RID: 17873
		public static string DetailKey = "Detail";

		// Token: 0x040045D2 RID: 17874
		public static string MessageFormatKey = "Message.Format";

		// Token: 0x040045D3 RID: 17875
		public static string MessageParametersKey = "Message.Parameters";

		// Token: 0x040045D4 RID: 17876
		public static readonly Keys Fields = Keys.New(new string[]
		{
			ErrorRecord.ReasonKey,
			ErrorRecord.MessageKey,
			ErrorRecord.DetailKey,
			ErrorRecord.MessageFormatKey,
			ErrorRecord.MessageParametersKey
		});

		// Token: 0x040045D5 RID: 17877
		public static readonly TextValue DefaultReason = TextValue.New("Expression.Error").NewMeta(ValueException.NonPii).AsText;

		// Token: 0x040045D6 RID: 17878
		public static readonly RecordValue DefaultReasonRecord = RecordValue.New(Keys.New(ErrorRecord.ReasonKey), new Value[] { ErrorRecord.DefaultReason });

		// Token: 0x040045D7 RID: 17879
		private static FunctionValue textFormatFunction = new Library.Text.FormatFunctionValue(EngineHost.Empty);

		// Token: 0x040045D8 RID: 17880
		private static RecordTypeValue type = null;
	}
}
