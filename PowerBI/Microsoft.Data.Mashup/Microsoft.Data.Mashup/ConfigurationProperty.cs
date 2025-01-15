using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000005 RID: 5
	internal static class ConfigurationProperty
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002656 File Offset: 0x00000856
		public static string Create(IDictionary<string, object> properties)
		{
			if (properties == null || properties.Count == 0)
			{
				return null;
			}
			return new JavaScriptSerializer().Serialize(properties);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002670 File Offset: 0x00000870
		public static Dictionary<string, object> ToDictionary(string value)
		{
			if (value == null)
			{
				return null;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				Dictionary<string, object> dictionary = javaScriptSerializer.Deserialize<Dictionary<string, object>>(value);
				foreach (KeyValuePair<string, object> keyValuePair in dictionary.OrderBy((KeyValuePair<string, object> pair) => pair.Key))
				{
					if (keyValuePair.Value != null)
					{
						TypeCode typeCode = Type.GetTypeCode(keyValuePair.Value.GetType());
						if (typeCode <= TypeCode.Boolean)
						{
							if (typeCode == TypeCode.Empty || typeCode - TypeCode.DBNull <= 1)
							{
								continue;
							}
						}
						else
						{
							switch (typeCode)
							{
							case TypeCode.Int32:
							case TypeCode.Int64:
							case TypeCode.Double:
							case TypeCode.Decimal:
								continue;
							case TypeCode.UInt32:
							case TypeCode.UInt64:
							case TypeCode.Single:
								break;
							default:
								if (typeCode == TypeCode.String)
								{
									continue;
								}
								break;
							}
						}
						throw new MashupException(ProviderErrorStrings.NotSupportedConfigurationValue(keyValuePair.Key));
					}
				}
				return dictionary;
			}
			catch (MissingMethodException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			throw new MashupException(ProviderErrorStrings.NotExpectedSwitchFormat);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002794 File Offset: 0x00000994
		public static void Validate(string key, object value)
		{
			if (value != null)
			{
				TypeCode typeCode = Type.GetTypeCode(value.GetType());
				if (typeCode <= TypeCode.Boolean)
				{
					if (typeCode == TypeCode.Empty || typeCode - TypeCode.DBNull <= 1)
					{
						return;
					}
				}
				else
				{
					switch (typeCode)
					{
					case TypeCode.Int32:
					case TypeCode.Int64:
					case TypeCode.Double:
					case TypeCode.Decimal:
						return;
					case TypeCode.UInt32:
					case TypeCode.UInt64:
					case TypeCode.Single:
						break;
					default:
						if (typeCode == TypeCode.String)
						{
							return;
						}
						break;
					}
				}
				throw new MashupException(ProviderErrorStrings.NotSupportedConfigurationValue(key));
			}
		}
	}
}
