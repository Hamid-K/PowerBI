using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018C0 RID: 6336
	internal static class DataSourceLocationOperations
	{
		// Token: 0x0600A194 RID: 41364 RVA: 0x00218794 File Offset: 0x00216994
		public static string ComparableRepresentation(this IDataSourceLocation location)
		{
			if (location == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (location.Protocol != null)
			{
				stringBuilder.Append(location.Protocol);
			}
			if (location.Authentication != null)
			{
				stringBuilder.Append(location.Authentication);
			}
			foreach (KeyValuePair<string, object> keyValuePair in location.Address.OrderBy((KeyValuePair<string, object> e) => e.Key))
			{
				if (keyValuePair.Value != null)
				{
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append(keyValuePair.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600A195 RID: 41365 RVA: 0x00218864 File Offset: 0x00216A64
		public static int ComputeHashCode(this IDataSourceLocation location)
		{
			int hashCode = location.Protocol.GetHashCode();
			DataSourceLocationOperations.AddToHashCode(location.Authentication, ref hashCode);
			DataSourceLocationOperations.AddToHashCode(location.Query, ref hashCode);
			DataSourceLocationOperations.AddToHashCode(location.Address, ref hashCode);
			return hashCode;
		}

		// Token: 0x0600A196 RID: 41366 RVA: 0x002188A8 File Offset: 0x00216AA8
		public static bool AreEqual(this IDataSourceLocation location, IDataSourceLocation other)
		{
			return other != null && location.Protocol == other.Protocol && location.Authentication == other.Authentication && location.Query == other.Query && DataSourceLocationOperations.AddressEquals(location.Address, other.Address);
		}

		// Token: 0x0600A197 RID: 41367 RVA: 0x00218904 File Offset: 0x00216B04
		public static string GetAddressFieldLabel(string addressField)
		{
			return Strings.ResourceManager.GetString("DataSourceLocation_Address_" + addressField) ?? addressField;
		}

		// Token: 0x0600A198 RID: 41368 RVA: 0x00218920 File Offset: 0x00216B20
		private static void AddToHashCode(object value, ref int hashCode)
		{
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			if (dictionary != null)
			{
				using (IEnumerator<KeyValuePair<string, object>> enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						DataSourceLocationOperations.AddToHashCode(keyValuePair.Value, ref hashCode);
					}
					return;
				}
			}
			if (value != null)
			{
				hashCode ^= value.GetHashCode();
			}
		}

		// Token: 0x0600A199 RID: 41369 RVA: 0x00218988 File Offset: 0x00216B88
		private static bool AddressEquals(IDictionary<string, object> address1, IDictionary<string, object> address2)
		{
			if (address1 == null || address2 == null)
			{
				return address1 == address2;
			}
			if (address1.Count != address2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in address1)
			{
				object obj;
				if (!address2.TryGetValue(keyValuePair.Key, out obj))
				{
					return false;
				}
				if (keyValuePair.Value == null)
				{
					if (obj != null)
					{
						return false;
					}
				}
				else
				{
					IDictionary<string, object> dictionary = keyValuePair.Value as IDictionary<string, object>;
					IDictionary<string, object> dictionary2 = obj as IDictionary<string, object>;
					if (dictionary != null && dictionary2 != null)
					{
						if (!DataSourceLocationOperations.AddressEquals(dictionary, dictionary2))
						{
							return false;
						}
					}
					else if (!keyValuePair.Value.Equals(obj))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400548E RID: 21646
		private const string addressLocalizationPrefix = "DataSourceLocation_Address_";
	}
}
