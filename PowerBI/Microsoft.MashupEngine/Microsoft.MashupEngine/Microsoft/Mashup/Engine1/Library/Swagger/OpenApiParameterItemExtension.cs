using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200038E RID: 910
	internal static class OpenApiParameterItemExtension
	{
		// Token: 0x06001FDD RID: 8157 RVA: 0x00053064 File Offset: 0x00051264
		public static string ConvertValueToString(this OpenApiParameterItem parameterItem, OpenApiDocument document, string parameterName, Value value)
		{
			TypeValue type = parameterItem.Type;
			if (type.IsListType)
			{
				List<string> list = parameterItem.ConvertListValueToStringList(document, parameterName, value.AsList);
				return string.Join(parameterItem.GetSeparator(document), list.ToArray());
			}
			string asString;
			try
			{
				asString = TypeValue.Text.Cast(value, document.EngineHost).AsString;
			}
			catch (Exception ex)
			{
				throw DataSourceException.NewDataSourceError<Message1>(document.EngineHost, Strings.OpenApiParameterTypeNotSupported(type), document.Resource, "Parameter", TextValue.New(parameterName), TypeValue.Text, ex);
			}
			return asString;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000530F8 File Offset: 0x000512F8
		public static List<string> ConvertListValueToStringList(this OpenApiParameterItem parameterItem, OpenApiDocument document, string parameterName, ListValue listValue)
		{
			List<string> list = new List<string>();
			if (listValue != null)
			{
				foreach (IValueReference valueReference in listValue)
				{
					string text = parameterItem.Items.ConvertValueToString(document, parameterName, valueReference.Value);
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x00053160 File Offset: 0x00051360
		public static string GetSeparator(this OpenApiParameterItem parameterItem, OpenApiDocument document)
		{
			string collectionFormat = parameterItem.CollectionFormat;
			if (collectionFormat == "csv")
			{
				return ",";
			}
			if (collectionFormat == "ssv")
			{
				return " ";
			}
			if (collectionFormat == "tsv")
			{
				return "\t";
			}
			if (!(collectionFormat == "pipes"))
			{
				throw DataSourceException.NewDataSourceError<Message1>(document.EngineHost, Strings.OpenApiCollectionFormatNotSupported(collectionFormat), document.Resource, null, null);
			}
			return "|";
		}

		// Token: 0x04000C16 RID: 3094
		private const string ParameterKey = "Parameter";
	}
}
