using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000227 RID: 551
	internal class ODataJsonLightBatchPayloadItemPropertiesCache
	{
		// Token: 0x0600180F RID: 6159 RVA: 0x00044E4F File Offset: 0x0004304F
		internal ODataJsonLightBatchPayloadItemPropertiesCache(ODataJsonLightBatchReader jsonBatchReader)
		{
			this.jsonReader = jsonBatchReader.JsonLightInputContext.JsonReader;
			this.listener = jsonBatchReader;
			this.ScanJsonProperties();
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00044E78 File Offset: 0x00043078
		internal object GetPropertyValue(string propertyName)
		{
			if (this.jsonProperties != null)
			{
				string text = ODataJsonLightBatchPayloadItemPropertiesCache.Normalize(propertyName);
				object obj;
				if (this.jsonProperties.TryGetValue(text, out obj))
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00044EA8 File Offset: 0x000430A8
		private ODataJsonLightBatchBodyContentReaderStream CreateJsonPayloadBodyContentStream(string contentTypeHeader)
		{
			ODataJsonLightBatchBodyContentReaderStream odataJsonLightBatchBodyContentReaderStream = new ODataJsonLightBatchBodyContentReaderStream(this.listener);
			this.isStreamPopulated = odataJsonLightBatchBodyContentReaderStream.PopulateBodyContent(this.jsonReader, contentTypeHeader);
			return odataJsonLightBatchBodyContentReaderStream;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00044ED5 File Offset: 0x000430D5
		private static string Normalize(string propertyName)
		{
			return propertyName.ToUpperInvariant();
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00044EE0 File Offset: 0x000430E0
		private void ScanJsonProperties()
		{
			this.jsonProperties = new Dictionary<string, object>();
			string text = null;
			ODataJsonLightBatchBodyContentReaderStream odataJsonLightBatchBodyContentReaderStream = null;
			try
			{
				this.jsonReader.ReadStartObject();
				while (this.jsonReader.NodeType != JsonNodeType.EndObject)
				{
					string text2 = ODataJsonLightBatchPayloadItemPropertiesCache.Normalize(this.jsonReader.ReadPropertyName());
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
					if (num <= 1969840958U)
					{
						if (num <= 507941321U)
						{
							if (num != 48695600U)
							{
								if (num != 507941321U)
								{
									goto IL_027A;
								}
								if (!(text2 == "HEADERS"))
								{
									goto IL_027A;
								}
								ODataBatchOperationHeaders odataBatchOperationHeaders = new ODataBatchOperationHeaders();
								text = "";
								this.jsonReader.ReadStartObject();
								while (this.jsonReader.NodeType != JsonNodeType.EndObject)
								{
									string text3 = this.jsonReader.ReadPropertyName();
									string text4 = this.jsonReader.ReadPrimitiveValue().ToString();
									if (text3.Equals("Content-Type", StringComparison.CurrentCultureIgnoreCase))
									{
										text = text4;
									}
									odataBatchOperationHeaders.Add(text3, text4);
								}
								this.jsonReader.ReadEndObject();
								this.jsonProperties.Add(text2, odataBatchOperationHeaders);
								if (!this.isStreamPopulated && odataJsonLightBatchBodyContentReaderStream != null)
								{
									odataJsonLightBatchBodyContentReaderStream.PopulateCachedBodyContent(text);
									continue;
								}
								continue;
							}
							else if (!(text2 == "METHOD"))
							{
								goto IL_027A;
							}
						}
						else if (num != 1458105184U)
						{
							if (num != 1969840958U)
							{
								goto IL_027A;
							}
							if (!(text2 == "URL"))
							{
								goto IL_027A;
							}
						}
						else if (!(text2 == "ID"))
						{
							goto IL_027A;
						}
					}
					else if (num <= 2237933493U)
					{
						if (num != 2170542633U)
						{
							if (num != 2237933493U)
							{
								goto IL_027A;
							}
							if (!(text2 == "BODY"))
							{
								goto IL_027A;
							}
							odataJsonLightBatchBodyContentReaderStream = this.CreateJsonPayloadBodyContentStream(text);
							this.jsonProperties.Add(text2, odataJsonLightBatchBodyContentReaderStream);
							continue;
						}
						else
						{
							if (!(text2 == "DEPENDSON"))
							{
								goto IL_027A;
							}
							IList<string> list = new List<string>();
							this.jsonReader.ReadStartArray();
							while (this.jsonReader.NodeType != JsonNodeType.EndArray)
							{
								list.Add(this.jsonReader.ReadStringValue());
							}
							this.jsonReader.ReadEndArray();
							this.jsonProperties.Add(text2, list);
							continue;
						}
					}
					else if (num != 2549462383U)
					{
						if (num != 3317780047U)
						{
							goto IL_027A;
						}
						if (!(text2 == "ATOMICITYGROUP"))
						{
							goto IL_027A;
						}
					}
					else
					{
						if (!(text2 == "STATUS"))
						{
							goto IL_027A;
						}
						this.jsonProperties.Add(text2, this.jsonReader.ReadPrimitiveValue());
						continue;
					}
					this.jsonProperties.Add(text2, this.jsonReader.ReadStringValue());
					continue;
					IL_027A:
					throw new ODataException(string.Format(CultureInfo.InvariantCulture, "Unknown property name '{0}' for message in batch", new object[] { text2 }));
				}
				this.jsonReader.ReadEndObject();
			}
			finally
			{
				this.jsonReader = null;
			}
		}

		// Token: 0x04000ABD RID: 2749
		internal const string PropertyNameId = "ID";

		// Token: 0x04000ABE RID: 2750
		internal const string PropertyNameAtomicityGroup = "ATOMICITYGROUP";

		// Token: 0x04000ABF RID: 2751
		internal const string PropertyNameHeaders = "HEADERS";

		// Token: 0x04000AC0 RID: 2752
		internal const string PropertyNameBody = "BODY";

		// Token: 0x04000AC1 RID: 2753
		internal const string PropertyNameDependsOn = "DEPENDSON";

		// Token: 0x04000AC2 RID: 2754
		internal const string PropertyNameMethod = "METHOD";

		// Token: 0x04000AC3 RID: 2755
		internal const string PropertyNameUrl = "URL";

		// Token: 0x04000AC4 RID: 2756
		internal const string PropertyNameStatus = "STATUS";

		// Token: 0x04000AC5 RID: 2757
		private IJsonReader jsonReader;

		// Token: 0x04000AC6 RID: 2758
		private IODataStreamListener listener;

		// Token: 0x04000AC7 RID: 2759
		private Dictionary<string, object> jsonProperties;

		// Token: 0x04000AC8 RID: 2760
		private bool isStreamPopulated;
	}
}
