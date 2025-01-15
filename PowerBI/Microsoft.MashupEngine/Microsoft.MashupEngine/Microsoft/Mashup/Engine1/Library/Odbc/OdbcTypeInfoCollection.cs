using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200067B RID: 1659
	internal class OdbcTypeInfoCollection
	{
		// Token: 0x06003425 RID: 13349 RVA: 0x000A76B2 File Offset: 0x000A58B2
		public OdbcTypeInfoCollection(OdbcDataSource dataSource, OdbcImplicitTypeConversions implicitConversions)
		{
			this.dataSource = dataSource;
			this.implicitConversions = implicitConversions;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000A76C8 File Offset: 0x000A58C8
		public bool TryGetType(Odbc32.SQL_TYPE sqlType, string name, out OdbcTypeInfo typeInfo)
		{
			this.EnsureInitialized();
			OrderedDictionary orderedDictionary;
			if (this.typeInfosBySqlType.TryGetValue(sqlType, out orderedDictionary) && name != null && orderedDictionary.Contains(name))
			{
				typeInfo = (OdbcTypeInfo)orderedDictionary[name];
				return true;
			}
			typeInfo = null;
			return false;
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000A770C File Offset: 0x000A590C
		public bool TryGetType(Odbc32.SQL_TYPE sqlType, out OdbcTypeInfo typeInfo)
		{
			this.EnsureInitialized();
			typeInfo = null;
			OrderedDictionary orderedDictionary;
			if (this.typeInfosBySqlType.TryGetValue(sqlType, out orderedDictionary))
			{
				typeInfo = (OdbcTypeInfo)orderedDictionary[0];
				return true;
			}
			typeInfo = null;
			return false;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000A7748 File Offset: 0x000A5948
		public bool TryGetType(string name, out OdbcTypeInfo typeInfo)
		{
			this.EnsureInitialized();
			if (this.typeInfosByName.TryGetValue(name, out typeInfo))
			{
				return true;
			}
			if (this.typeInfosByCaseInsensitiveName == null)
			{
				this.typeInfosByCaseInsensitiveName = new Dictionary<string, OdbcTypeInfo>(this.typeInfosByName.Count, StringComparer.OrdinalIgnoreCase);
				foreach (KeyValuePair<string, OdbcTypeInfo> keyValuePair in this.typeInfosByName)
				{
					try
					{
						this.typeInfosByCaseInsensitiveName.Add(keyValuePair.Key, keyValuePair.Value);
					}
					catch (ArgumentException)
					{
						this.typeInfosByCaseInsensitiveName[keyValuePair.Key] = null;
					}
				}
			}
			return this.typeInfosByCaseInsensitiveName.TryGetValue(name, out typeInfo) && typeInfo != null;
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000A7824 File Offset: 0x000A5A24
		public bool TryGetImplicitConversion(OdbcTypeInfo type1, OdbcTypeInfo type2, out OdbcTypeInfo result)
		{
			string text;
			if (this.implicitConversions.TryGetImplicitConversion(type1.Name, type2.Name, out text) && this.TryGetType(text, out result))
			{
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000A785C File Offset: 0x000A5A5C
		public OdbcTypeInfo GetType(string name)
		{
			OdbcTypeInfo odbcTypeInfo;
			if (this.TryGetType(name, out odbcTypeInfo))
			{
				return odbcTypeInfo;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000A787B File Offset: 0x000A5A7B
		private void EnsureInitialized()
		{
			if (this.typeInfosBySqlType == null)
			{
				this.dataSource.ConnectForMetadata(delegate(IOdbcConnection connection)
				{
					Dictionary<Odbc32.SQL_TYPE, OrderedDictionary> dictionary = new Dictionary<Odbc32.SQL_TYPE, OrderedDictionary>();
					Dictionary<string, OdbcTypeInfo> dictionary2 = new Dictionary<string, OdbcTypeInfo>();
					try
					{
						using (IDataReader typeInfo = connection.GetTypeInfo(0))
						{
							while (typeInfo.Read())
							{
								string stringOrNull = typeInfo.GetStringOrNull(0);
								if (stringOrNull != null)
								{
									string text = stringOrNull;
									Odbc32.SQL_TYPE sql_TYPE = (Odbc32.SQL_TYPE)typeInfo.GetNullableInt32(1).GetValueOrDefault();
									Odbc32.SQL_SEARCHABLE valueOrDefault = (Odbc32.SQL_SEARCHABLE)typeInfo.GetNullableInt32(8).GetValueOrDefault();
									int? nullableInt = typeInfo.GetNullableInt32(9);
									int num = 1;
									OdbcTypeInfo odbcTypeInfo = new OdbcTypeInfo(this, text, sql_TYPE, valueOrDefault, (nullableInt.GetValueOrDefault() == num) & (nullableInt != null), typeInfo.GetNullableInt32(2), this.dataSource.Info.IsDriverV3 ? typeInfo.GetNumberPrecisionRadix(17) : null, typeInfo.GetStringOrNull(3), typeInfo.GetStringOrNull(4));
									OrderedDictionary orderedDictionary;
									if (!dictionary.TryGetValue(odbcTypeInfo.SqlType, out orderedDictionary))
									{
										orderedDictionary = new OrderedDictionary();
										dictionary[odbcTypeInfo.SqlType] = orderedDictionary;
									}
									orderedDictionary[odbcTypeInfo.Name] = odbcTypeInfo;
									dictionary2[odbcTypeInfo.Name] = odbcTypeInfo;
								}
							}
						}
					}
					catch (OdbcException ex)
					{
						if (!ex.IsNonTransient)
						{
							throw;
						}
					}
					this.typeInfosByName = dictionary2;
					this.typeInfosBySqlType = dictionary;
				});
			}
		}

		// Token: 0x04001752 RID: 5970
		public const int TypeNameOrdinal = 0;

		// Token: 0x04001753 RID: 5971
		public const int DataTypeOrdinal = 1;

		// Token: 0x04001754 RID: 5972
		public const int ColumnSizeOrdinal = 2;

		// Token: 0x04001755 RID: 5973
		public const int literalPrefixOrdinal = 3;

		// Token: 0x04001756 RID: 5974
		public const int literalSuffixOrdinal = 4;

		// Token: 0x04001757 RID: 5975
		public const int SearchableOrdinal = 8;

		// Token: 0x04001758 RID: 5976
		public const int UnsignedAttributeOrdinal = 9;

		// Token: 0x04001759 RID: 5977
		public const int NumberPrecisionRadixOrdinal = 17;

		// Token: 0x0400175A RID: 5978
		private readonly OdbcDataSource dataSource;

		// Token: 0x0400175B RID: 5979
		private readonly OdbcImplicitTypeConversions implicitConversions;

		// Token: 0x0400175C RID: 5980
		private Dictionary<Odbc32.SQL_TYPE, OrderedDictionary> typeInfosBySqlType;

		// Token: 0x0400175D RID: 5981
		private Dictionary<string, OdbcTypeInfo> typeInfosByName;

		// Token: 0x0400175E RID: 5982
		private Dictionary<string, OdbcTypeInfo> typeInfosByCaseInsensitiveName;
	}
}
