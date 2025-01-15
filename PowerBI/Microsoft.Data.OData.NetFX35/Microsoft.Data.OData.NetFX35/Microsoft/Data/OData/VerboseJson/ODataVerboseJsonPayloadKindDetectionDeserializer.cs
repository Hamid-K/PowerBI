using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001BA RID: 442
	internal sealed class ODataVerboseJsonPayloadKindDetectionDeserializer : ODataVerboseJsonPropertyAndValueDeserializer
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x0002E223 File Offset: 0x0002C423
		internal ODataVerboseJsonPayloadKindDetectionDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002E23C File Offset: 0x0002C43C
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind()
		{
			this.detectedPayloadKinds.Clear();
			base.JsonReader.DisableInStreamErrorDetection = true;
			IEnumerable<ODataPayloadKind> enumerable;
			try
			{
				base.ReadPayloadStart(false);
				JsonNodeType nodeType = base.JsonReader.NodeType;
				if (nodeType == JsonNodeType.StartObject)
				{
					base.JsonReader.ReadStartObject();
					int num = 0;
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text = base.JsonReader.ReadPropertyName();
						num++;
						if (string.CompareOrdinal("__metadata", text) == 0)
						{
							this.ProcessMetadataPropertyValue();
							break;
						}
						if (num == 1)
						{
							this.AddPayloadKinds(new ODataPayloadKind[]
							{
								ODataPayloadKind.Property,
								ODataPayloadKind.Entry,
								ODataPayloadKind.Parameter
							});
							ODataError odataError;
							if (string.CompareOrdinal("uri", text) == 0 && base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
							{
								this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
							}
							else if (string.CompareOrdinal("error", text) == 0 && base.JsonReader.StartBufferingAndTryToReadInStreamErrorPropertyValue(out odataError))
							{
								this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.Error });
							}
						}
						else if (num == 2)
						{
							this.RemovePayloadKinds(new ODataPayloadKind[]
							{
								ODataPayloadKind.Property,
								ODataPayloadKind.EntityReferenceLink,
								ODataPayloadKind.Error
							});
						}
						if (string.CompareOrdinal("results", text) == 0 && base.JsonReader.NodeType == JsonNodeType.StartArray)
						{
							this.DetectStartArrayPayloadKind(false);
						}
						else if (base.ReadingResponse && string.CompareOrdinal("EntitySets", text) == 0 && base.JsonReader.NodeType == JsonNodeType.StartArray)
						{
							this.ProcessEntitySetsArray();
						}
						base.JsonReader.SkipValue();
					}
					if (num == 0)
					{
						this.AddPayloadKinds(new ODataPayloadKind[]
						{
							ODataPayloadKind.Entry,
							ODataPayloadKind.Parameter
						});
					}
				}
				else if (nodeType == JsonNodeType.StartArray)
				{
					this.DetectStartArrayPayloadKind(true);
				}
				enumerable = this.detectedPayloadKinds;
			}
			catch (ODataException)
			{
				enumerable = Enumerable.Empty<ODataPayloadKind>();
			}
			finally
			{
				base.JsonReader.DisableInStreamErrorDetection = false;
			}
			return enumerable;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002E450 File Offset: 0x0002C650
		private void DetectStartArrayPayloadKind(bool isTopLevel)
		{
			if (!isTopLevel)
			{
				this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.Property });
			}
			base.JsonReader.StartBuffering();
			try
			{
				base.JsonReader.ReadStartArray();
				JsonNodeType nodeType = base.JsonReader.NodeType;
				if (nodeType != JsonNodeType.StartObject)
				{
					switch (nodeType)
					{
					case JsonNodeType.EndArray:
						this.AddPayloadKinds(new ODataPayloadKind[]
						{
							ODataPayloadKind.Feed,
							ODataPayloadKind.Collection,
							ODataPayloadKind.EntityReferenceLinks
						});
						break;
					case JsonNodeType.PrimitiveValue:
						this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.Collection });
						break;
					}
				}
				else
				{
					base.JsonReader.ReadStartObject();
					bool flag = false;
					int num = 0;
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text = base.JsonReader.ReadPropertyName();
						num++;
						if (num > 1)
						{
							break;
						}
						if (string.CompareOrdinal("uri", text) == 0 && base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
						{
							flag = true;
						}
						base.JsonReader.SkipValue();
					}
					this.AddPayloadKinds(new ODataPayloadKind[]
					{
						ODataPayloadKind.Feed,
						ODataPayloadKind.Collection
					});
					if (flag && num == 1)
					{
						this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
					}
				}
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002E5A8 File Offset: 0x0002C7A8
		private void ProcessMetadataPropertyValue()
		{
			this.detectedPayloadKinds.Clear();
			string text = base.ReadTypeNameFromMetadataPropertyValue();
			EdmTypeKind edmTypeKind = EdmTypeKind.None;
			if (text != null)
			{
				MetadataUtils.ResolveTypeNameForRead(EdmCoreModel.Instance, null, text, base.MessageReaderSettings.ReaderBehavior, base.Version, out edmTypeKind);
			}
			if (edmTypeKind == EdmTypeKind.Primitive || edmTypeKind == EdmTypeKind.Collection)
			{
				return;
			}
			this.detectedPayloadKinds.Add(ODataPayloadKind.Entry);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002E604 File Offset: 0x0002C804
		private void ProcessEntitySetsArray()
		{
			base.JsonReader.StartBuffering();
			try
			{
				base.JsonReader.ReadStartArray();
				if (base.JsonReader.NodeType == JsonNodeType.EndArray || base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
				{
					this.AddPayloadKinds(new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
				}
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002E674 File Offset: 0x0002C874
		private void AddPayloadKinds(params ODataPayloadKind[] payloadKinds)
		{
			this.AddOrRemovePayloadKinds(new Func<ODataPayloadKind, bool>(this.detectedPayloadKinds.Add), payloadKinds);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002E68E File Offset: 0x0002C88E
		private void RemovePayloadKinds(params ODataPayloadKind[] payloadKinds)
		{
			this.AddOrRemovePayloadKinds(new Func<ODataPayloadKind, bool>(this.detectedPayloadKinds.Remove), payloadKinds);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
		private void AddOrRemovePayloadKinds(Func<ODataPayloadKind, bool> addOrRemoveAction, params ODataPayloadKind[] payloadKinds)
		{
			foreach (ODataPayloadKind odataPayloadKind in payloadKinds)
			{
				if (ODataUtilsInternal.IsPayloadKindSupported(odataPayloadKind, !base.ReadingResponse))
				{
					addOrRemoveAction.Invoke(odataPayloadKind);
				}
			}
		}

		// Token: 0x04000494 RID: 1172
		private readonly HashSet<ODataPayloadKind> detectedPayloadKinds = new HashSet<ODataPayloadKind>(EqualityComparer<ODataPayloadKind>.Default);
	}
}
