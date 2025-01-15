using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BF0 RID: 3056
	internal class ExchangeSearchResultSerializer
	{
		// Token: 0x06005333 RID: 21299 RVA: 0x00119C70 File Offset: 0x00117E70
		public static void SerializeCount(IPersistentCache cache, string key, int count)
		{
			using (Stream stream = new PersistentCacheExtensions.WriteOnlyCachingStream(cache, key, cache.MaxEntryLength, null))
			{
				new BinaryWriter(stream).Write(count);
			}
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x00119CBC File Offset: 0x00117EBC
		public static void Serialize(IPersistentCache cache, string key, IEnumerable<ExchangeSearchResult> searchResults, bool moreAvailable, int? nextPageOffset, ExchangeColumnInfo[] columnInfos, PropertyDefinitionBase[] propertiesLoaded)
		{
			using (Stream stream = new PersistentCacheExtensions.WriteOnlyCachingStream(cache, key, cache.MaxEntryLength, null))
			{
				ExchangeSearchResultSerializer.ExchangeDataWriter exchangeDataWriter = new ExchangeSearchResultSerializer.ExchangeDataWriter(stream, columnInfos, propertiesLoaded);
				exchangeDataWriter.Write(moreAvailable, nextPageOffset);
				foreach (ExchangeSearchResult exchangeSearchResult in searchResults)
				{
					exchangeDataWriter.Write(exchangeSearchResult);
				}
				exchangeDataWriter.WriteResultEnd();
			}
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00119D50 File Offset: 0x00117F50
		public static void Serialize(IPersistentCache cache, string key, ExchangeSearchResult searchResult, ExchangeColumnInfo[] columnInfos, PropertyDefinitionBase[] propertiesLoaded)
		{
			using (Stream stream = new PersistentCacheExtensions.WriteOnlyCachingStream(cache, key, cache.MaxEntryLength, null))
			{
				ExchangeSearchResultSerializer.ExchangeDataWriter exchangeDataWriter = new ExchangeSearchResultSerializer.ExchangeDataWriter(stream, columnInfos, propertiesLoaded);
				exchangeDataWriter.Write(searchResult);
				exchangeDataWriter.WriteResultEnd();
			}
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00119DA8 File Offset: 0x00117FA8
		public static void SerializeString(Stream stream, string value)
		{
			new BinaryWriter(stream).Write(value);
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00119DB8 File Offset: 0x00117FB8
		public static IEnumerable<ExchangeSearchResult> Deserialize(Stream stream, out bool moreAvailable, out int? nextPageOffset)
		{
			ExchangeSearchResultSerializer.ExchangeDataReader exchangeDataReader = new ExchangeSearchResultSerializer.ExchangeDataReader(stream);
			exchangeDataReader.ReadPageResult(out moreAvailable, out nextPageOffset);
			return exchangeDataReader.ReadResults();
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x00119DDD File Offset: 0x00117FDD
		public static int DeserializeCount(Stream stream)
		{
			return new BinaryReader(stream).ReadInt32();
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x00119DEC File Offset: 0x00117FEC
		public static ExchangeSearchResult Deserialize(Stream stream)
		{
			ExchangeSearchResultSerializer.ExchangeDataReader exchangeDataReader = new ExchangeSearchResultSerializer.ExchangeDataReader(stream);
			return exchangeDataReader.ReadResults().Single<ExchangeSearchResult>();
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x00119E0D File Offset: 0x0011800D
		public static string DeserializeString(Stream stream)
		{
			return new BinaryReader(stream).ReadString();
		}

		// Token: 0x02000BF1 RID: 3057
		private struct ExchangeDataWriter
		{
			// Token: 0x0600533C RID: 21308 RVA: 0x00119E1A File Offset: 0x0011801A
			public ExchangeDataWriter(Stream stream, ExchangeColumnInfo[] columnInfos, PropertyDefinitionBase[] propertiesLoaded)
			{
				this.writer = new BinaryWriter(stream);
				this.columnInfos = columnInfos ?? new ExchangeColumnInfo[0];
				this.propertiesLoaded = propertiesLoaded ?? new PropertyDefinitionBase[0];
			}

			// Token: 0x0600533D RID: 21309 RVA: 0x00119E4A File Offset: 0x0011804A
			public void Write(bool moreAvailable, int? nextPageOffset)
			{
				this.writer.Write(moreAvailable);
				this.writer.Write(nextPageOffset.GetValueOrDefault(-1));
			}

			// Token: 0x0600533E RID: 21310 RVA: 0x00119E6C File Offset: 0x0011806C
			public void Write(ExchangeSearchResult result)
			{
				this.writer.Write(true);
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				Dictionary<string, object[]> dictionary2 = new Dictionary<string, object[]>();
				foreach (ExchangeColumnInfo exchangeColumnInfo in this.columnInfos)
				{
					if (exchangeColumnInfo.ColumnCategory == ColumnCategory.PrimitiveColumn && exchangeColumnInfo.FieldSelector != null)
					{
						ExchangeSearchResultSerializer.ExchangeDataWriter.AddNestedColumnInfos(result, dictionary, exchangeColumnInfo.Property);
					}
				}
				foreach (PropertyDefinitionBase propertyDefinitionBase in this.propertiesLoaded)
				{
					if (propertyDefinitionBase != ItemSchema.Id)
					{
						ExchangeColumnInfo loadedExchangeColumnInfo = ExchangeCatalog.GetLoadedExchangeColumnInfo(propertyDefinitionBase);
						if (loadedExchangeColumnInfo.ColumnCategory == ColumnCategory.RecordColumn)
						{
							ExchangeSearchResultSerializer.ExchangeDataWriter.AddNestedColumnInfos(result, dictionary, propertyDefinitionBase);
						}
						object columnValue = result.GetColumnValue(loadedExchangeColumnInfo);
						if (columnValue is object[])
						{
							object[] array3 = (object[])columnValue;
							dictionary2.Add(loadedExchangeColumnInfo.UniqueName, array3);
						}
						else
						{
							dictionary.Add(loadedExchangeColumnInfo.UniqueName, columnValue);
						}
					}
				}
				this.writer.Write(dictionary.Count);
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					this.writer.Write(keyValuePair.Key);
					object value = keyValuePair.Value;
					if (value != null && value.GetType().BaseType == typeof(Enum))
					{
						this.writer.WriteObject(value.ToString());
					}
					else
					{
						this.writer.WriteObject(value);
					}
				}
				this.writer.Write(dictionary2.Count);
				foreach (KeyValuePair<string, object[]> keyValuePair2 in dictionary2)
				{
					this.writer.Write(keyValuePair2.Key);
					object[] value2 = keyValuePair2.Value;
					if (value2 != null)
					{
						this.writer.Write(value2.Length);
						foreach (object[] array5 in value2)
						{
							this.writer.Write(array5.Length);
							foreach (object obj in array5)
							{
								this.writer.WriteObject(obj);
							}
						}
					}
					else
					{
						this.writer.Write(0);
					}
				}
				this.writer.Write(result.Id);
				this.writer.Write(result.FolderPath);
			}

			// Token: 0x0600533F RID: 21311 RVA: 0x0011A0F8 File Offset: 0x001182F8
			private static void AddNestedColumnInfos(ExchangeSearchResult result, Dictionary<string, object> primitiveColumns, PropertyDefinitionBase property)
			{
				ExchangeColumnInfo[] subColumns = ExchangeCatalog.GetLoadedExchangeColumnInfo(property).SubColumns;
				if (subColumns != null)
				{
					foreach (ExchangeColumnInfo exchangeColumnInfo in subColumns)
					{
						if (!primitiveColumns.ContainsKey(exchangeColumnInfo.UniqueName))
						{
							primitiveColumns.Add(exchangeColumnInfo.UniqueName, result.GetColumnValue(exchangeColumnInfo));
						}
					}
				}
			}

			// Token: 0x06005340 RID: 21312 RVA: 0x0011A149 File Offset: 0x00118349
			public void WriteResultEnd()
			{
				this.writer.Write(false);
			}

			// Token: 0x04002DF8 RID: 11768
			private readonly BinaryWriter writer;

			// Token: 0x04002DF9 RID: 11769
			private ExchangeColumnInfo[] columnInfos;

			// Token: 0x04002DFA RID: 11770
			private PropertyDefinitionBase[] propertiesLoaded;
		}

		// Token: 0x02000BF2 RID: 3058
		private struct ExchangeDataReader
		{
			// Token: 0x06005341 RID: 21313 RVA: 0x0011A157 File Offset: 0x00118357
			public ExchangeDataReader(Stream stream)
			{
				this.reader = new BinaryReader(stream);
			}

			// Token: 0x06005342 RID: 21314 RVA: 0x0011A168 File Offset: 0x00118368
			public void ReadPageResult(out bool moreAvailable, out int? nextPageOffset)
			{
				moreAvailable = this.reader.ReadBoolean();
				nextPageOffset = new int?(this.reader.ReadInt32());
				int? num = nextPageOffset;
				int num2 = -1;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					nextPageOffset = null;
				}
			}

			// Token: 0x06005343 RID: 21315 RVA: 0x0011A1BC File Offset: 0x001183BC
			public IEnumerable<ExchangeSearchResult> ReadResults()
			{
				for (;;)
				{
					try
					{
						if (!this.reader.ReadBoolean())
						{
							yield break;
						}
					}
					catch (Exception ex)
					{
						if (ex is IOException || (ex.InnerException != null && ex.InnerException is IOException))
						{
							throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex);
						}
						throw;
					}
					yield return this.ReadResult();
				}
				yield break;
			}

			// Token: 0x06005344 RID: 21316 RVA: 0x0011A1D4 File Offset: 0x001183D4
			private ExchangeSearchResult ReadResult()
			{
				int num = this.reader.ReadInt32();
				Dictionary<string, object> dictionary = new Dictionary<string, object>(num);
				for (int i = 0; i < num; i++)
				{
					string text = this.reader.ReadString();
					object obj = this.reader.ReadObject(this.reader.ReadObjectTag());
					dictionary.Add(text, obj);
				}
				num = this.reader.ReadInt32();
				for (int j = 0; j < num; j++)
				{
					string text2 = this.reader.ReadString();
					int num2 = this.reader.ReadInt32();
					object[] array = new object[num2];
					for (int k = 0; k < num2; k++)
					{
						int num3 = this.reader.ReadInt32();
						object[] array2 = new object[num3];
						for (int l = 0; l < num3; l++)
						{
							array2[l] = this.reader.ReadObject(this.reader.ReadObjectTag());
						}
						array[k] = array2;
					}
					dictionary.Add(text2, array);
				}
				string text3 = this.reader.ReadString();
				string text4 = this.reader.ReadString();
				return new ExchangeCachingSearchResult(dictionary, text3, text4);
			}

			// Token: 0x04002DFB RID: 11771
			private readonly BinaryReader reader;
		}
	}
}
