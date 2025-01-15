using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000194 RID: 404
	internal static class EdmPrimitiveHelpers
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x00034360 File Offset: 0x00032560
		public static object ConvertPrimitiveValue(object value, Type type)
		{
			if (value.GetType() == type || value.GetType() == Nullable.GetUnderlyingType(type))
			{
				return value;
			}
			if (type.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (type == typeof(char))
			{
				if (text == null || text.Length != 1)
				{
					throw new ValidationException(Error.Format(SRResources.PropertyMustBeStringLengthOne, new object[0]));
				}
				return text[0];
			}
			else if (type == typeof(char?))
			{
				if (text == null || text.Length > 1)
				{
					throw new ValidationException(Error.Format(SRResources.PropertyMustBeStringMaxLengthOne, new object[0]));
				}
				return (text.Length > 0) ? new char?(text[0]) : null;
			}
			else if (type == typeof(char[]))
			{
				if (text == null)
				{
					throw new ValidationException(Error.Format(SRResources.PropertyMustBeString, new object[0]));
				}
				return text.ToCharArray();
			}
			else
			{
				if (type == typeof(Binary))
				{
					return new Binary((byte[])value);
				}
				if (type == typeof(XElement))
				{
					if (text == null)
					{
						throw new ValidationException(Error.Format(SRResources.PropertyMustBeString, new object[0]));
					}
					return XElement.Parse(text);
				}
				else
				{
					type = Nullable.GetUnderlyingType(type) ?? type;
					if (TypeHelper.IsEnum(type))
					{
						if (text == null)
						{
							throw new ValidationException(Error.Format(SRResources.PropertyMustBeString, new object[0]));
						}
						return Enum.Parse(type, text);
					}
					else if (type == typeof(DateTime))
					{
						if (value is DateTimeOffset)
						{
							DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
							TimeZoneInfo timeZone = TimeZoneInfoHelper.TimeZone;
							return TimeZoneInfo.ConvertTime(dateTimeOffset, timeZone).DateTime;
						}
						if (value is Date)
						{
							return (Date)value;
						}
						throw new ValidationException(Error.Format(SRResources.PropertyMustBeDateTimeOffsetOrDate, new object[0]));
					}
					else if (type == typeof(TimeSpan))
					{
						if (value is TimeOfDay)
						{
							return (TimeOfDay)value;
						}
						throw new ValidationException(Error.Format(SRResources.PropertyMustBeTimeOfDay, new object[0]));
					}
					else
					{
						if (!(type == typeof(bool)))
						{
							return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
						}
						bool flag;
						if (text != null && bool.TryParse(text, out flag))
						{
							return flag;
						}
						throw new ValidationException(Error.Format(SRResources.PropertyMustBeBoolean, new object[0]));
					}
				}
			}
		}
	}
}
