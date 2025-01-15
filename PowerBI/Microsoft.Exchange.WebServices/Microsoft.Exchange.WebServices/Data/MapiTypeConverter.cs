using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029E RID: 670
	internal class MapiTypeConverter
	{
		// Token: 0x0600178D RID: 6029 RVA: 0x00040028 File Offset: 0x0003F028
		internal static Array ConvertToValue(MapiPropertyType mapiPropType, IEnumerable<string> strings)
		{
			EwsUtilities.ValidateParam(strings, "strings");
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry = MapiTypeConverter.MapiTypeConverterMap[mapiPropType];
			Array array = Array.CreateInstance(mapiTypeConverterMapEntry.Type, Enumerable.Count<string>(strings));
			int num = 0;
			foreach (string text in strings)
			{
				object obj = mapiTypeConverterMapEntry.ConvertToValueOrDefault(text);
				array.SetValue(obj, num++);
			}
			return array;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000400B0 File Offset: 0x0003F0B0
		internal static object ConvertToValue(MapiPropertyType mapiPropType, string stringValue)
		{
			return MapiTypeConverter.MapiTypeConverterMap[mapiPropType].ConvertToValue(stringValue);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000400C3 File Offset: 0x0003F0C3
		internal static string ConvertToString(MapiPropertyType mapiPropType, object value)
		{
			if (value != null)
			{
				return MapiTypeConverter.MapiTypeConverterMap[mapiPropType].ConvertToString.Invoke(value);
			}
			return string.Empty;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000400E4 File Offset: 0x0003F0E4
		internal static object ChangeType(MapiPropertyType mapiType, object value)
		{
			EwsUtilities.ValidateParam(value, "value");
			return MapiTypeConverter.MapiTypeConverterMap[mapiType].ChangeType(value);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00040104 File Offset: 0x0003F104
		internal static object ParseMapiIntegerValue(string s)
		{
			int num;
			if (int.TryParse(s, 7, CultureInfo.InvariantCulture, ref num))
			{
				return num;
			}
			return s;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00040129 File Offset: 0x0003F129
		internal static bool IsArrayType(MapiPropertyType mapiType)
		{
			return MapiTypeConverter.MapiTypeConverterMap[mapiType].IsArray;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0004013B File Offset: 0x0003F13B
		internal static Dictionary<MapiPropertyType, MapiTypeConverterMapEntry> MapiTypeConverterMap
		{
			get
			{
				return MapiTypeConverter.mapiTypeConverterMap.Member;
			}
		}

		// Token: 0x0400135D RID: 4957
		private const DateTimeStyles UtcDataTimeStyles = 80;

		// Token: 0x0400135E RID: 4958
		private static LazyMember<Dictionary<MapiPropertyType, MapiTypeConverterMapEntry>> mapiTypeConverterMap = new LazyMember<Dictionary<MapiPropertyType, MapiTypeConverterMapEntry>>(delegate
		{
			Dictionary<MapiPropertyType, MapiTypeConverterMapEntry> dictionary = new Dictionary<MapiPropertyType, MapiTypeConverterMapEntry>();
			dictionary.Add(MapiPropertyType.ApplicationTime, new MapiTypeConverterMapEntry(typeof(double)));
			dictionary.Add(MapiPropertyType.ApplicationTimeArray, new MapiTypeConverterMapEntry(typeof(double))
			{
				IsArray = true
			});
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry = new MapiTypeConverterMapEntry(typeof(byte[]));
			mapiTypeConverterMapEntry.Parse = delegate(string s)
			{
				if (!string.IsNullOrEmpty(s))
				{
					return Convert.FromBase64String(s);
				}
				return null;
			};
			mapiTypeConverterMapEntry.ConvertToString = (object o) => Convert.ToBase64String((byte[])o);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry2 = mapiTypeConverterMapEntry;
			dictionary.Add(MapiPropertyType.Binary, mapiTypeConverterMapEntry2);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry3 = new MapiTypeConverterMapEntry(typeof(byte[]));
			mapiTypeConverterMapEntry3.Parse = delegate(string s)
			{
				if (!string.IsNullOrEmpty(s))
				{
					return Convert.FromBase64String(s);
				}
				return null;
			};
			mapiTypeConverterMapEntry3.ConvertToString = (object o) => Convert.ToBase64String((byte[])o);
			mapiTypeConverterMapEntry3.IsArray = true;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry4 = mapiTypeConverterMapEntry3;
			dictionary.Add(MapiPropertyType.BinaryArray, mapiTypeConverterMapEntry4);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry5 = new MapiTypeConverterMapEntry(typeof(bool));
			mapiTypeConverterMapEntry5.Parse = (string s) => Convert.ChangeType(s, typeof(bool), CultureInfo.InvariantCulture);
			mapiTypeConverterMapEntry5.ConvertToString = (object o) => ((bool)o).ToString(CultureInfo.InvariantCulture).ToLower();
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry6 = mapiTypeConverterMapEntry5;
			dictionary.Add(MapiPropertyType.Boolean, mapiTypeConverterMapEntry6);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry7 = new MapiTypeConverterMapEntry(typeof(Guid));
			mapiTypeConverterMapEntry7.Parse = (string s) => new Guid(s);
			mapiTypeConverterMapEntry7.ConvertToString = (object o) => ((Guid)o).ToString();
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry8 = mapiTypeConverterMapEntry7;
			dictionary.Add(MapiPropertyType.CLSID, mapiTypeConverterMapEntry8);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry9 = new MapiTypeConverterMapEntry(typeof(Guid));
			mapiTypeConverterMapEntry9.Parse = (string s) => new Guid(s);
			mapiTypeConverterMapEntry9.ConvertToString = (object o) => ((Guid)o).ToString();
			mapiTypeConverterMapEntry9.IsArray = true;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry10 = mapiTypeConverterMapEntry9;
			dictionary.Add(MapiPropertyType.CLSIDArray, mapiTypeConverterMapEntry10);
			dictionary.Add(MapiPropertyType.Currency, new MapiTypeConverterMapEntry(typeof(long)));
			dictionary.Add(MapiPropertyType.CurrencyArray, new MapiTypeConverterMapEntry(typeof(long))
			{
				IsArray = true
			});
			dictionary.Add(MapiPropertyType.Double, new MapiTypeConverterMapEntry(typeof(double)));
			dictionary.Add(MapiPropertyType.DoubleArray, new MapiTypeConverterMapEntry(typeof(double))
			{
				IsArray = true
			});
			dictionary.Add(MapiPropertyType.Error, new MapiTypeConverterMapEntry(typeof(int)));
			dictionary.Add(MapiPropertyType.Float, new MapiTypeConverterMapEntry(typeof(float)));
			dictionary.Add(MapiPropertyType.FloatArray, new MapiTypeConverterMapEntry(typeof(float))
			{
				IsArray = true
			});
			Dictionary<MapiPropertyType, MapiTypeConverterMapEntry> dictionary2 = dictionary;
			MapiPropertyType mapiPropertyType = MapiPropertyType.Integer;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry11 = new MapiTypeConverterMapEntry(typeof(int));
			mapiTypeConverterMapEntry11.Parse = (string s) => MapiTypeConverter.ParseMapiIntegerValue(s);
			dictionary2.Add(mapiPropertyType, mapiTypeConverterMapEntry11);
			dictionary.Add(MapiPropertyType.IntegerArray, new MapiTypeConverterMapEntry(typeof(int))
			{
				IsArray = true
			});
			dictionary.Add(MapiPropertyType.Long, new MapiTypeConverterMapEntry(typeof(long)));
			dictionary.Add(MapiPropertyType.LongArray, new MapiTypeConverterMapEntry(typeof(long))
			{
				IsArray = true
			});
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry12 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry12.Parse = (string s) => s;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry13 = mapiTypeConverterMapEntry12;
			dictionary.Add(MapiPropertyType.Object, mapiTypeConverterMapEntry13);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry14 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry14.Parse = (string s) => s;
			mapiTypeConverterMapEntry14.IsArray = true;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry15 = mapiTypeConverterMapEntry14;
			dictionary.Add(MapiPropertyType.ObjectArray, mapiTypeConverterMapEntry15);
			dictionary.Add(MapiPropertyType.Short, new MapiTypeConverterMapEntry(typeof(short)));
			dictionary.Add(MapiPropertyType.ShortArray, new MapiTypeConverterMapEntry(typeof(short))
			{
				IsArray = true
			});
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry16 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry16.Parse = (string s) => s;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry17 = mapiTypeConverterMapEntry16;
			dictionary.Add(MapiPropertyType.String, mapiTypeConverterMapEntry17);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry18 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry18.Parse = (string s) => s;
			mapiTypeConverterMapEntry18.IsArray = true;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry19 = mapiTypeConverterMapEntry18;
			dictionary.Add(MapiPropertyType.StringArray, mapiTypeConverterMapEntry19);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry20 = new MapiTypeConverterMapEntry(typeof(DateTime));
			mapiTypeConverterMapEntry20.Parse = (string s) => DateTime.Parse(s, CultureInfo.InvariantCulture, 80);
			mapiTypeConverterMapEntry20.ConvertToString = (object o) => EwsUtilities.DateTimeToXSDateTime((DateTime)o);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry21 = mapiTypeConverterMapEntry20;
			dictionary.Add(MapiPropertyType.SystemTime, mapiTypeConverterMapEntry21);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry22 = new MapiTypeConverterMapEntry(typeof(DateTime));
			mapiTypeConverterMapEntry22.IsArray = true;
			mapiTypeConverterMapEntry22.Parse = (string s) => DateTime.Parse(s, CultureInfo.InvariantCulture, 80);
			mapiTypeConverterMapEntry22.ConvertToString = (object o) => EwsUtilities.DateTimeToXSDateTime((DateTime)o);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry23 = mapiTypeConverterMapEntry22;
			dictionary.Add(MapiPropertyType.SystemTimeArray, mapiTypeConverterMapEntry23);
			return dictionary;
		});
	}
}
