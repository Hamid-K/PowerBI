using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010FA RID: 4346
	public static class PageExceptionSerializer
	{
		// Token: 0x060071C1 RID: 29121 RVA: 0x00187118 File Offset: 0x00185318
		public static Exception GetExceptionFromProperties(ISerializedException properties)
		{
			string text = properties["Reason"];
			string text2 = properties["Message"];
			string text3 = null;
			bool flag = properties.TryGetValue("PiiFlags", out text3);
			int num = 0;
			TextValue textValue = TextValue.New(text);
			if (flag && text3[num++] == '0')
			{
				textValue = textValue.NewMeta(ValueException.NonPii).AsText;
			}
			Value value = ((text2 != null) ? TextValue.New(text2) : Value.Null);
			if (flag && text3[num++] == '0')
			{
				value = value.NewMeta(ValueException.NonPii);
			}
			Value value2 = Value.Null;
			string text4;
			if (properties.TryGetValue("Detail", out text4))
			{
				value2 = PageExceptionSerializer.ParseDetail(text4);
				if (flag && text3[num++] == '0')
				{
					value2 = value2.NewMeta(ValueException.NonPii);
				}
			}
			Value value3 = Value.Null;
			string text5;
			if (properties.TryGetValue("Message.Format", out text5) && text5 != null)
			{
				value3 = TextValue.New(text5);
				if (flag && text3[num++] == '0')
				{
					value3 = value3.NewMeta(ValueException.NonPii);
				}
			}
			Value[] array = null;
			string text6;
			if (properties.TryGetValue("MessageParametersLength", out text6))
			{
				int num2 = int.Parse(text6, CultureInfo.InvariantCulture);
				array = new Value[num2];
				for (int i = 0; i < num2; i++)
				{
					string text7;
					Value value4 = (properties.TryGetValue("Message.Parameters" + i.ToString(CultureInfo.InvariantCulture), out text7) ? PageExceptionSerializer.ParseDetail(text7) : Value.Null);
					if (flag && text3[num++] == '0')
					{
						value4 = value4.NewMeta(ValueException.NonPii);
					}
					array[i] = value4;
				}
			}
			Exception ex = ValueException.New(ErrorRecord.New(textValue, value, value2, value3, (array == null) ? Value.Null : ListValue.New(array)), null);
			string text8;
			if (properties.TryGetValue("Data", out text8) && text8 != null)
			{
				RecordValue asRecord = PageExceptionSerializer.ParseDetail(text8).AsRecord;
				foreach (string text9 in asRecord.Keys)
				{
					ex.Data[text9] = asRecord[text9].AsString;
				}
			}
			return ex;
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x00187378 File Offset: 0x00185578
		public static bool TryGetPropertiesFromException(Exception exception, out ISerializedException properties)
		{
			ValueException ex = exception as ValueException;
			if (ex != null)
			{
				properties = new SerializedException(6);
				StringBuilder stringBuilder = new StringBuilder(5);
				properties["Reason"] = ex.ReasonString;
				stringBuilder.Append(ex.ReasonIsPii ? "1" : "0");
				properties["Message"] = ex.MessageString;
				stringBuilder.Append(ex.MessageIsPii ? "1" : "0");
				Value value = ex.Detail as Value;
				if (value != null)
				{
					properties["Detail"] = value.PrimitiveAndRecordToString(2);
					stringBuilder.Append(ex.DetailIsPii ? "1" : "0");
				}
				properties["Message.Format"] = ex.MessageFormatString;
				stringBuilder.Append(ex.MessageFormatIsPii ? "1" : "0");
				ListValue listValue = ex.MessageParameters as ListValue;
				if (listValue != null)
				{
					properties["MessageParametersLength"] = listValue.Count.ToString(CultureInfo.InvariantCulture);
					for (int i = 0; i < listValue.Count; i++)
					{
						properties["Message.Parameters" + i.ToString(CultureInfo.InvariantCulture)] = listValue[i].PrimitiveAndRecordToString(2);
						if (ValueException2.IsPii(listValue[i]))
						{
							stringBuilder.Append("1");
						}
						else
						{
							stringBuilder.Append("0");
						}
					}
				}
				properties["PiiFlags"] = stringBuilder.ToString();
				if (ex.Data.Count > 0)
				{
					RecordBuilder recordBuilder = new RecordBuilder(ex.Data.Count);
					foreach (object obj in ex.Data)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						string text = dictionaryEntry.Key as string;
						if (text != null && dictionaryEntry.Value is string)
						{
							recordBuilder.Add(text, TextValue.New((string)dictionaryEntry.Value), TypeValue.Text);
						}
					}
					RecordValue recordValue = recordBuilder.ToRecord();
					properties["Data"] = recordValue.PrimitiveAndRecordToString(1);
				}
				return true;
			}
			properties = null;
			return false;
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x001875E4 File Offset: 0x001857E4
		private static Value ParseDetail(string detail)
		{
			Value value;
			try
			{
				IExpressionDocument expressionDocument = (IExpressionDocument)Engine.Instance.Parse(detail, delegate(IError e)
				{
					throw new InvalidOperationException(e.Message);
				});
				value = ValueCreator.CreateValue(RecordValue.Empty, expressionDocument.Expression);
			}
			catch (Exception ex)
			{
				if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				value = Value.Null;
			}
			return value;
		}
	}
}
