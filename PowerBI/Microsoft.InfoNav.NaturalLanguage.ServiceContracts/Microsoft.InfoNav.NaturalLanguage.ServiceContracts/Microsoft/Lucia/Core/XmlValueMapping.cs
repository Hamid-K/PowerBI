using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000079 RID: 121
	internal static class XmlValueMapping
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00005148 File Offset: 0x00003348
		internal static object GetXmlValueFromSemanticValue(DataValue value)
		{
			if (value == null)
			{
				return XmlNullValue.DefaultInstance;
			}
			DataType type = value.Type;
			if (type == DataType.Year)
			{
				return XmlYearValue.FromSemanticValue(value);
			}
			if (type != DataType.DateTime)
			{
				return value.GetValueAsObject();
			}
			return XmlDateTimeValue.FromSemanticValue(value);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000518B File Offset: 0x0000338B
		internal static DataValue GetSemanticValueFromXmlValue(object value)
		{
			if (value is XmlNullValue)
			{
				return null;
			}
			if (value is XmlYearValue)
			{
				return ((XmlYearValue)value).ToSemanticValue();
			}
			if (value is XmlDateTimeValue)
			{
				return ((XmlDateTimeValue)value).ToSemanticValue();
			}
			return DataValue.FromObject(value);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000051C8 File Offset: 0x000033C8
		internal static XmlValueType GetTypeFromSemanticValue(DataValue value)
		{
			if (value == null)
			{
				return XmlValueType.Null;
			}
			DataType type = value.Type;
			if (type <= DataType.Number)
			{
				if (type == DataType.Text)
				{
					return XmlValueType.StringValue;
				}
				if (type == DataType.Boolean)
				{
					return XmlValueType.BooleanValue;
				}
				if (type == DataType.Number)
				{
					return XmlValueType.NumberValue;
				}
			}
			else
			{
				if (type == DataType.Integer)
				{
					return XmlValueType.IntegerValue;
				}
				if (type == DataType.Year)
				{
					return XmlValueType.YearValue;
				}
				if (type == DataType.DateTime)
				{
					return XmlValueType.DateTimeValue;
				}
			}
			throw new ArgumentException();
		}
	}
}
