using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class ValueProviderResult
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00003AA7 File Offset: 0x00001CA7
		protected ValueProviderResult()
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000057FC File Offset: 0x000039FC
		public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
		{
			this.RawValue = rawValue;
			this.AttemptedValue = attemptedValue;
			this.Culture = culture;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005819 File Offset: 0x00003A19
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00005821 File Offset: 0x00003A21
		public string AttemptedValue { get; protected set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000582A File Offset: 0x00003A2A
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00005845 File Offset: 0x00003A45
		public CultureInfo Culture
		{
			get
			{
				if (this._instanceCulture == null)
				{
					this._instanceCulture = ValueProviderResult._staticCulture;
				}
				return this._instanceCulture;
			}
			protected set
			{
				this._instanceCulture = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000584E File Offset: 0x00003A4E
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00005856 File Offset: 0x00003A56
		public object RawValue { get; protected set; }

		// Token: 0x060001AD RID: 429 RVA: 0x00005860 File Offset: 0x00003A60
		private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (text != null && string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			TypeConverter typeConverter = TypeDescriptor.GetConverter(destinationType);
			bool flag = typeConverter.CanConvertFrom(value.GetType());
			if (!flag)
			{
				typeConverter = TypeDescriptor.GetConverter(value.GetType());
			}
			if (flag || typeConverter.CanConvertTo(destinationType))
			{
				object obj;
				try
				{
					obj = (flag ? typeConverter.ConvertFrom(null, culture, value) : typeConverter.ConvertTo(null, culture, value, destinationType));
				}
				catch (Exception ex)
				{
					throw Error.InvalidOperation(ex, SRResources.ValueProviderResult_ConversionThrew, new object[]
					{
						value.GetType(),
						destinationType
					});
				}
				return obj;
			}
			if (destinationType.IsEnum && value is int)
			{
				return Enum.ToObject(destinationType, (int)value);
			}
			Type underlyingType = Nullable.GetUnderlyingType(destinationType);
			if (underlyingType != null)
			{
				return ValueProviderResult.ConvertSimpleType(culture, value, underlyingType);
			}
			throw Error.InvalidOperation(SRResources.ValueProviderResult_NoConverterExists, new object[]
			{
				value.GetType(),
				destinationType
			});
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005960 File Offset: 0x00003B60
		public object ConvertTo(Type type)
		{
			return this.ConvertTo(type, null);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000596C File Offset: 0x00003B6C
		public virtual object ConvertTo(Type type, CultureInfo culture)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object rawValue = this.RawValue;
			if (rawValue == null)
			{
				if (!type.IsValueType)
				{
					return null;
				}
				return Activator.CreateInstance(type);
			}
			else
			{
				if (type.IsInstanceOfType(rawValue))
				{
					return rawValue;
				}
				return ValueProviderResult.UnwrapPossibleListType(culture ?? this.Culture, rawValue, type);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000059C8 File Offset: 0x00003BC8
		private static object UnwrapPossibleListType(CultureInfo culture, object value, Type destinationType)
		{
			IList list = value as IList;
			if (destinationType.IsArray)
			{
				Type elementType = destinationType.GetElementType();
				if (list != null)
				{
					IList list2 = Array.CreateInstance(elementType, list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						list2[i] = ValueProviderResult.ConvertSimpleType(culture, list[i], elementType);
					}
					return list2;
				}
				object obj = ValueProviderResult.ConvertSimpleType(culture, value, elementType);
				Array array = Array.CreateInstance(elementType, 1);
				((IList)array)[0] = obj;
				return array;
			}
			else
			{
				if (list == null)
				{
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				if (list.Count > 0)
				{
					value = list[0];
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				return null;
			}
		}

		// Token: 0x04000054 RID: 84
		private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

		// Token: 0x04000055 RID: 85
		private CultureInfo _instanceCulture;
	}
}
