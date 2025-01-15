using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029F RID: 671
	internal class MapiTypeConverterMapEntry
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x000408C0 File Offset: 0x0003F8C0
		internal MapiTypeConverterMapEntry(Type type)
		{
			EwsUtilities.Assert(MapiTypeConverterMapEntry.defaultValueMap.Member.ContainsKey(type), "MapiTypeConverterMapEntry ctor", string.Format("No default value entry for type {0}", type.Name));
			this.Type = type;
			this.ConvertToString = (object o) => (string)Convert.ChangeType(o, typeof(string), CultureInfo.InvariantCulture);
			this.Parse = (string s) => Convert.ChangeType(s, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0004095C File Offset: 0x0003F95C
		internal object ChangeType(object value)
		{
			if (this.IsArray)
			{
				this.ValidateValueAsArray(value);
				return value;
			}
			if (value.GetType() == this.Type)
			{
				return value;
			}
			object obj;
			try
			{
				obj = Convert.ChangeType(value, this.Type, CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException ex)
			{
				throw new ArgumentException(string.Format(Strings.ValueOfTypeCannotBeConverted, value, value.GetType(), this.Type), ex);
			}
			return obj;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000409D4 File Offset: 0x0003F9D4
		internal object ConvertToValue(string stringValue)
		{
			object obj;
			try
			{
				obj = this.Parse.Invoke(stringValue);
			}
			catch (FormatException ex)
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.ValueCannotBeConverted, stringValue, this.Type), ex);
			}
			catch (InvalidCastException ex2)
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.ValueCannotBeConverted, stringValue, this.Type), ex2);
			}
			catch (OverflowException ex3)
			{
				throw new ServiceXmlDeserializationException(string.Format(Strings.ValueCannotBeConverted, stringValue, this.Type), ex3);
			}
			return obj;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00040A74 File Offset: 0x0003FA74
		internal object ConvertToValueOrDefault(string stringValue)
		{
			if (!string.IsNullOrEmpty(stringValue))
			{
				return this.ConvertToValue(stringValue);
			}
			return this.DefaultValue;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00040A8C File Offset: 0x0003FA8C
		private void ValidateValueAsArray(object value)
		{
			Array array = value as Array;
			if (array == null)
			{
				throw new ArgumentException(string.Format(Strings.IncompatibleTypeForArray, value.GetType(), this.Type));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Strings.ArrayMustHaveSingleDimension);
			}
			if (array.Length == 0)
			{
				throw new ArgumentException(Strings.ArrayMustHaveAtLeastOneElement);
			}
			if (array.GetType().GetElementType() != this.Type)
			{
				throw new ArgumentException(string.Format(Strings.IncompatibleTypeForArray, value.GetType(), this.Type));
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00040B29 File Offset: 0x0003FB29
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x00040B31 File Offset: 0x0003FB31
		internal Func<string, object> Parse { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00040B3A File Offset: 0x0003FB3A
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x00040B42 File Offset: 0x0003FB42
		internal Func<object, string> ConvertToString { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00040B4B File Offset: 0x0003FB4B
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00040B53 File Offset: 0x0003FB53
		internal Type Type { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00040B5C File Offset: 0x0003FB5C
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x00040B64 File Offset: 0x0003FB64
		internal bool IsArray { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00040B6D File Offset: 0x0003FB6D
		internal object DefaultValue
		{
			get
			{
				return MapiTypeConverterMapEntry.defaultValueMap.Member[this.Type];
			}
		}

		// Token: 0x04001373 RID: 4979
		private static LazyMember<Dictionary<Type, object>> defaultValueMap = new LazyMember<Dictionary<Type, object>>(delegate
		{
			Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
			dictionary.Add(typeof(bool), false);
			dictionary.Add(typeof(byte[]), null);
			dictionary.Add(typeof(short), 0);
			dictionary.Add(typeof(int), 0);
			dictionary.Add(typeof(long), 0L);
			dictionary.Add(typeof(float), 0f);
			dictionary.Add(typeof(double), 0.0);
			dictionary.Add(typeof(DateTime), DateTime.MinValue);
			dictionary.Add(typeof(Guid), Guid.Empty);
			dictionary.Add(typeof(string), null);
			return dictionary;
		});
	}
}
