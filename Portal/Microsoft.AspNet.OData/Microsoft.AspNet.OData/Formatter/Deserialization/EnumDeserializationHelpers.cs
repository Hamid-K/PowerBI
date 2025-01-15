using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B2 RID: 434
	internal static class EnumDeserializationHelpers
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x0003AC70 File Offset: 0x00038E70
		public static object ConvertEnumValue(object value, Type type)
		{
			if (value == null)
			{
				throw Error.ArgumentNull("value");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			Type underlyingTypeOrSelf = TypeHelper.GetUnderlyingTypeOrSelf(type);
			if (value.GetType() == underlyingTypeOrSelf)
			{
				return value;
			}
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue == null)
			{
				throw new ValidationException(Error.Format(SRResources.PropertyMustBeEnum, new object[]
				{
					value.GetType().Name,
					"ODataEnumValue"
				}));
			}
			if (!TypeHelper.IsEnum(underlyingTypeOrSelf))
			{
				throw Error.InvalidOperation(Error.Format(SRResources.TypeMustBeEnumOrNullableEnum, new object[] { type.Name }), new object[0]);
			}
			return Enum.Parse(underlyingTypeOrSelf, odataEnumValue.Value);
		}
	}
}
