using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Json;
using Azure.Core.Serialization;

namespace Azure
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	public static class AzureCoreExtensions
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00003C65 File Offset: 0x00001E65
		[return: Nullable(2)]
		public static T ToObject<[Nullable(2)] T>(this BinaryData data, ObjectSerializer serializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (T)((object)serializer.Deserialize(data.ToStream(), typeof(T), cancellationToken));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003C84 File Offset: 0x00001E84
		[return: Nullable(new byte[] { 0, 2 })]
		public static async ValueTask<T> ToObjectAsync<[Nullable(2)] T>(this BinaryData data, ObjectSerializer serializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ConfiguredValueTaskAwaitable<object>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter = serializer.DeserializeAsync(data.ToStream(), typeof(T), cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredValueTaskAwaiter.IsCompleted)
			{
				await configuredValueTaskAwaiter;
				ConfiguredValueTaskAwaitable<object>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
				configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
				configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<object>.ConfiguredValueTaskAwaiter);
			}
			return (T)((object)configuredValueTaskAwaiter.GetResult());
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003CD8 File Offset: 0x00001ED8
		[return: Nullable(2)]
		public static object ToObjectFromJson(this BinaryData data)
		{
			JsonElement jsonElement = data.ToObjectFromJson<JsonElement>(null);
			return (in jsonElement).GetObject();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003CF4 File Offset: 0x00001EF4
		[return: Dynamic]
		public static dynamic ToDynamicFromJson(this BinaryData utf8Json)
		{
			DynamicDataOptions dynamicDataOptions = new DynamicDataOptions();
			return utf8Json.ToDynamicFromJson(dynamicDataOptions);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003D10 File Offset: 0x00001F10
		[return: Dynamic]
		public static dynamic ToDynamicFromJson(this BinaryData utf8Json, JsonPropertyNames propertyNameFormat, string dateTimeFormat = "o")
		{
			DynamicDataOptions dynamicDataOptions = new DynamicDataOptions
			{
				PropertyNameFormat = propertyNameFormat,
				DateTimeFormat = dateTimeFormat
			};
			return utf8Json.ToDynamicFromJson(dynamicDataOptions);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003D38 File Offset: 0x00001F38
		[return: Dynamic]
		internal static dynamic ToDynamicFromJson(this BinaryData utf8Json, DynamicDataOptions options)
		{
			return new DynamicData(MutableJsonDocument.Parse(utf8Json, DynamicDataOptions.ToSerializerOptions(options)).RootElement, options);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003D54 File Offset: 0x00001F54
		[NullableContext(2)]
		private static object GetObject(this JsonElement element)
		{
			switch (element.ValueKind)
			{
			case 0:
			case 7:
				return null;
			case 1:
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (JsonProperty jsonProperty in element.EnumerateObject())
				{
					Dictionary<string, object> dictionary2 = dictionary;
					string name = jsonProperty.Name;
					JsonElement value = jsonProperty.Value;
					dictionary2.Add(name, (in value).GetObject());
				}
				return dictionary;
			}
			case 2:
			{
				List<object> list = new List<object>();
				foreach (JsonElement jsonElement in element.EnumerateArray())
				{
					list.Add((in jsonElement).GetObject());
				}
				return list.ToArray();
			}
			case 3:
				return element.GetString();
			case 4:
			{
				int num;
				if (element.TryGetInt32(ref num))
				{
					return num;
				}
				long num2;
				if (element.TryGetInt64(ref num2))
				{
					return num2;
				}
				return element.GetDouble();
			}
			case 5:
				return true;
			case 6:
				return false;
			default:
				throw new NotSupportedException("Not supported value kind " + element.ValueKind.ToString());
			}
		}
	}
}
