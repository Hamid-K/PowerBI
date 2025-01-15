using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core;

namespace Azure.Messaging
{
	// Token: 0x02000035 RID: 53
	[NullableContext(2)]
	[Nullable(0)]
	[JsonConverter(typeof(CloudEventConverter))]
	public class CloudEvent
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00003EEC File Offset: 0x000020EC
		[NullableContext(1)]
		public CloudEvent(string source, string type, [Nullable(2)] object jsonSerializableData, [Nullable(2)] Type dataSerializationType = null)
		{
			if (jsonSerializableData is BinaryData)
			{
				throw new InvalidOperationException("This constructor does not support BinaryData. Use the constructor that takes a BinaryData instance.");
			}
			this.Source = source;
			this.Type = type;
			this.Id = Guid.NewGuid().ToString();
			this.DataFormat = CloudEventDataFormat.Json;
			this.Data = new BinaryData(jsonSerializableData, null, dataSerializationType ?? ((jsonSerializableData != null) ? jsonSerializableData.GetType() : null));
			this.SpecVersion = "1.0";
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003F88 File Offset: 0x00002188
		[NullableContext(1)]
		public CloudEvent(string source, string type, [Nullable(2)] BinaryData data, [Nullable(2)] string dataContentType, CloudEventDataFormat dataFormat = CloudEventDataFormat.Binary)
		{
			this.Source = source;
			this.Type = type;
			this.DataContentType = dataContentType;
			this.Id = Guid.NewGuid().ToString();
			this.DataFormat = dataFormat;
			this.Data = data;
			this.SpecVersion = "1.0";
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003FFF File Offset: 0x000021FF
		internal CloudEvent()
		{
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004022 File Offset: 0x00002222
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000402A File Offset: 0x0000222A
		public BinaryData Data { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004033 File Offset: 0x00002233
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000403B File Offset: 0x0000223B
		[Nullable(1)]
		public string Id
		{
			[NullableContext(1)]
			get
			{
				return this._id;
			}
			[NullableContext(1)]
			set
			{
				Argument.AssertNotNull<string>(value, "value");
				this._id = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000404F File Offset: 0x0000224F
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00004057 File Offset: 0x00002257
		internal CloudEventDataFormat DataFormat { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004060 File Offset: 0x00002260
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004068 File Offset: 0x00002268
		[Nullable(1)]
		public string Source
		{
			[NullableContext(1)]
			get
			{
				return this._source;
			}
			[NullableContext(1)]
			set
			{
				Argument.AssertNotNull<string>(value, "value");
				this._source = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000407C File Offset: 0x0000227C
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00004084 File Offset: 0x00002284
		[Nullable(1)]
		public string Type
		{
			[NullableContext(1)]
			get
			{
				return this._type;
			}
			[NullableContext(1)]
			set
			{
				Argument.AssertNotNull<string>(value, "value");
				this._type = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004098 File Offset: 0x00002298
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000040A0 File Offset: 0x000022A0
		internal string SpecVersion { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000040A9 File Offset: 0x000022A9
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000040B1 File Offset: 0x000022B1
		public DateTimeOffset? Time { get; set; } = new DateTimeOffset?(DateTimeOffset.UtcNow);

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000040BA File Offset: 0x000022BA
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000040C2 File Offset: 0x000022C2
		public string DataSchema { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000040CB File Offset: 0x000022CB
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000040D3 File Offset: 0x000022D3
		public string DataContentType { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000040DC File Offset: 0x000022DC
		internal Type DataSerializationType { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000040E4 File Offset: 0x000022E4
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000040EC File Offset: 0x000022EC
		public string Subject { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000040F5 File Offset: 0x000022F5
		[Nullable(1)]
		public IDictionary<string, object> ExtensionAttributes
		{
			[NullableContext(1)]
			get;
		} = new CloudEventExtensionAttributes<string, object>();

		// Token: 0x06000133 RID: 307 RVA: 0x00004100 File Offset: 0x00002300
		[NullableContext(1)]
		public static CloudEvent[] ParseMany(BinaryData json, bool skipValidation = false)
		{
			Argument.AssertNotNull<BinaryData>(json, "json");
			CloudEvent[] array = null;
			JsonDocument jsonDocument = JsonDocument.Parse(json, default(JsonDocumentOptions));
			if (jsonDocument.RootElement.ValueKind == 1)
			{
				array = new CloudEvent[] { CloudEventConverter.DeserializeCloudEvent(jsonDocument.RootElement, skipValidation) };
			}
			else if (jsonDocument.RootElement.ValueKind == 2)
			{
				array = new CloudEvent[jsonDocument.RootElement.GetArrayLength()];
				int num = 0;
				foreach (JsonElement jsonElement in jsonDocument.RootElement.EnumerateArray())
				{
					array[num++] = CloudEventConverter.DeserializeCloudEvent(jsonElement, skipValidation);
				}
			}
			return array ?? Array.Empty<CloudEvent>();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000041E8 File Offset: 0x000023E8
		[NullableContext(1)]
		[return: Nullable(2)]
		public static CloudEvent Parse(BinaryData json, bool skipValidation = false)
		{
			Argument.AssertNotNull<BinaryData>(json, "json");
			CloudEvent cloudEvent2;
			using (JsonDocument jsonDocument = JsonDocument.Parse(json, default(JsonDocumentOptions)))
			{
				CloudEvent cloudEvent = null;
				if (jsonDocument.RootElement.ValueKind == 1)
				{
					cloudEvent = CloudEventConverter.DeserializeCloudEvent(jsonDocument.RootElement, skipValidation);
				}
				else if (jsonDocument.RootElement.ValueKind == 2)
				{
					if (jsonDocument.RootElement.GetArrayLength() > 1)
					{
						throw new ArgumentException("The BinaryData instance contains JSON from multiple cloud events. This method should only be used with BinaryData containing a single cloud event. " + Environment.NewLine + "To parse multiple events, use the ParseMany overload.");
					}
					using (JsonElement.ArrayEnumerator enumerator = jsonDocument.RootElement.EnumerateArray().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							cloudEvent = CloudEventConverter.DeserializeCloudEvent(enumerator.Current, skipValidation);
						}
					}
				}
				cloudEvent2 = cloudEvent;
			}
			return cloudEvent2;
		}

		// Token: 0x04000066 RID: 102
		private string _id;

		// Token: 0x04000068 RID: 104
		private string _source;

		// Token: 0x04000069 RID: 105
		private string _type;
	}
}
