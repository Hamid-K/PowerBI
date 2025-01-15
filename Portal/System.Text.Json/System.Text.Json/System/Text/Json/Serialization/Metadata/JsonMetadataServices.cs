using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Converters;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A8 RID: 168
	[NullableContext(1)]
	[Nullable(0)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class JsonMetadataServices
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x0002904B File Offset: 0x0002724B
		public static JsonTypeInfo<TElement[]> CreateArrayInfo<[Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TElement[]> collectionInfo)
		{
			return JsonMetadataServices.CreateCore<TElement[]>(options, collectionInfo, new ArrayConverter<TElement[], TElement>(), null, null);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002905B File Offset: 0x0002725B
		public static JsonTypeInfo<TCollection> CreateListInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : List<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ListOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002906B File Offset: 0x0002726B
		public static JsonTypeInfo<TCollection> CreateDictionaryInfo<[Nullable(0)] TCollection, TKey, [Nullable(2)] TValue>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : Dictionary<TKey, TValue>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new DictionaryOfTKeyTValueConverter<TCollection, TKey, TValue>(), null, null);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002907B File Offset: 0x0002727B
		public static JsonTypeInfo<TCollection> CreateImmutableDictionaryInfo<[Nullable(0)] TCollection, TKey, [Nullable(2)] TValue>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo, [Nullable(new byte[] { 1, 1, 0, 1, 1, 1 })] Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection> createRangeFunc) where TCollection : IReadOnlyDictionary<TKey, TValue>
		{
			if (createRangeFunc == null)
			{
				ThrowHelper.ThrowArgumentNullException("createRangeFunc");
			}
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ImmutableDictionaryOfTKeyTValueConverter<TCollection, TKey, TValue>(), createRangeFunc, null);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00029098 File Offset: 0x00027298
		public static JsonTypeInfo<TCollection> CreateIDictionaryInfo<[Nullable(0)] TCollection, TKey, [Nullable(2)] TValue>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IDictionary<TKey, TValue>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IDictionaryOfTKeyTValueConverter<TCollection, TKey, TValue>(), null, null);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000290A8 File Offset: 0x000272A8
		public static JsonTypeInfo<TCollection> CreateIReadOnlyDictionaryInfo<[Nullable(0)] TCollection, TKey, [Nullable(2)] TValue>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IReadOnlyDictionary<TKey, TValue>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IReadOnlyDictionaryOfTKeyTValueConverter<TCollection, TKey, TValue>(), null, null);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000290B8 File Offset: 0x000272B8
		public static JsonTypeInfo<TCollection> CreateImmutableEnumerableInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo, Func<IEnumerable<TElement>, TCollection> createRangeFunc) where TCollection : IEnumerable<TElement>
		{
			if (createRangeFunc == null)
			{
				ThrowHelper.ThrowArgumentNullException("createRangeFunc");
			}
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ImmutableEnumerableOfTConverter<TCollection, TElement>(), createRangeFunc, null);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000290D5 File Offset: 0x000272D5
		public static JsonTypeInfo<TCollection> CreateIListInfo<[Nullable(0)] TCollection>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IList
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IListConverter<TCollection>(), null, null);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000290E5 File Offset: 0x000272E5
		public static JsonTypeInfo<TCollection> CreateIListInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IList<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IListOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000290F5 File Offset: 0x000272F5
		public static JsonTypeInfo<TCollection> CreateISetInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : ISet<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ISetOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00029105 File Offset: 0x00027305
		public static JsonTypeInfo<TCollection> CreateICollectionInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : ICollection<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ICollectionOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00029115 File Offset: 0x00027315
		public static JsonTypeInfo<TCollection> CreateStackInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : Stack<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new StackOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00029125 File Offset: 0x00027325
		public static JsonTypeInfo<TCollection> CreateQueueInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : Queue<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new QueueOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00029135 File Offset: 0x00027335
		public static JsonTypeInfo<TCollection> CreateConcurrentStackInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : ConcurrentStack<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ConcurrentStackOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00029145 File Offset: 0x00027345
		public static JsonTypeInfo<TCollection> CreateConcurrentQueueInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : ConcurrentQueue<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new ConcurrentQueueOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00029155 File Offset: 0x00027355
		public static JsonTypeInfo<TCollection> CreateIEnumerableInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IEnumerable<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IEnumerableOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00029165 File Offset: 0x00027365
		public static JsonTypeInfo<TCollection> CreateIAsyncEnumerableInfo<[Nullable(0)] TCollection, [Nullable(2)] TElement>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IAsyncEnumerable<TElement>
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IAsyncEnumerableOfTConverter<TCollection, TElement>(), null, null);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00029175 File Offset: 0x00027375
		public static JsonTypeInfo<TCollection> CreateIDictionaryInfo<[Nullable(0)] TCollection>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IDictionary
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IDictionaryConverter<TCollection>(), null, null);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00029185 File Offset: 0x00027385
		public static JsonTypeInfo<TCollection> CreateStackInfo<[Nullable(0)] TCollection>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo, [Nullable(new byte[] { 1, 1, 2 })] Action<TCollection, object> addFunc) where TCollection : IEnumerable
		{
			return JsonMetadataServices.CreateStackOrQueueInfo<TCollection>(options, collectionInfo, addFunc);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002918F File Offset: 0x0002738F
		public static JsonTypeInfo<TCollection> CreateQueueInfo<[Nullable(0)] TCollection>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo, [Nullable(new byte[] { 1, 1, 2 })] Action<TCollection, object> addFunc) where TCollection : IEnumerable
		{
			return JsonMetadataServices.CreateStackOrQueueInfo<TCollection>(options, collectionInfo, addFunc);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00029199 File Offset: 0x00027399
		private static JsonTypeInfo<TCollection> CreateStackOrQueueInfo<TCollection>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo, Action<TCollection, object> addFunc) where TCollection : IEnumerable
		{
			if (addFunc == null)
			{
				ThrowHelper.ThrowArgumentNullException("addFunc");
			}
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new StackOrQueueConverter<TCollection>(), null, addFunc);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000291B6 File Offset: 0x000273B6
		public static JsonTypeInfo<TCollection> CreateIEnumerableInfo<[Nullable(0)] TCollection>(JsonSerializerOptions options, JsonCollectionInfoValues<TCollection> collectionInfo) where TCollection : IEnumerable
		{
			return JsonMetadataServices.CreateCore<TCollection>(options, collectionInfo, new IEnumerableConverter<TCollection>(), null, null);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000291C6 File Offset: 0x000273C6
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static JsonTypeInfo<Memory<TElement>> CreateMemoryInfo<[Nullable(2)] TElement>(JsonSerializerOptions options, [Nullable(new byte[] { 1, 0, 1 })] JsonCollectionInfoValues<Memory<TElement>> collectionInfo)
		{
			return JsonMetadataServices.CreateCore<Memory<TElement>>(options, collectionInfo, new MemoryConverter<TElement>(), null, null);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000291D6 File Offset: 0x000273D6
		[return: Nullable(new byte[] { 1, 0, 1 })]
		public static JsonTypeInfo<ReadOnlyMemory<TElement>> CreateReadOnlyMemoryInfo<[Nullable(2)] TElement>(JsonSerializerOptions options, [Nullable(new byte[] { 1, 0, 1 })] JsonCollectionInfoValues<ReadOnlyMemory<TElement>> collectionInfo)
		{
			return JsonMetadataServices.CreateCore<ReadOnlyMemory<TElement>>(options, collectionInfo, new ReadOnlyMemoryConverter<TElement>(), null, null);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000291E6 File Offset: 0x000273E6
		public static JsonConverter<bool> BooleanConverter
		{
			get
			{
				JsonConverter<bool> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_booleanConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_booleanConverter = new global::System.Text.Json.Serialization.Converters.BooleanConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000291FC File Offset: 0x000273FC
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<byte[]> ByteArrayConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<byte[]> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_byteArrayConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_byteArrayConverter = new ByteArrayConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00029212 File Offset: 0x00027412
		public static JsonConverter<byte> ByteConverter
		{
			get
			{
				JsonConverter<byte> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_byteConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_byteConverter = new global::System.Text.Json.Serialization.Converters.ByteConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00029228 File Offset: 0x00027428
		public static JsonConverter<char> CharConverter
		{
			get
			{
				JsonConverter<char> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_charConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_charConverter = new global::System.Text.Json.Serialization.Converters.CharConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002923E File Offset: 0x0002743E
		public static JsonConverter<DateTime> DateTimeConverter
		{
			get
			{
				JsonConverter<DateTime> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_dateTimeConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_dateTimeConverter = new global::System.Text.Json.Serialization.Converters.DateTimeConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00029254 File Offset: 0x00027454
		public static JsonConverter<DateTimeOffset> DateTimeOffsetConverter
		{
			get
			{
				JsonConverter<DateTimeOffset> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_dateTimeOffsetConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_dateTimeOffsetConverter = new global::System.Text.Json.Serialization.Converters.DateTimeOffsetConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002926A File Offset: 0x0002746A
		public static JsonConverter<decimal> DecimalConverter
		{
			get
			{
				JsonConverter<decimal> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_decimalConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_decimalConverter = new global::System.Text.Json.Serialization.Converters.DecimalConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00029280 File Offset: 0x00027480
		public static JsonConverter<double> DoubleConverter
		{
			get
			{
				JsonConverter<double> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_doubleConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_doubleConverter = new global::System.Text.Json.Serialization.Converters.DoubleConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00029296 File Offset: 0x00027496
		public static JsonConverter<Guid> GuidConverter
		{
			get
			{
				JsonConverter<Guid> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_guidConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_guidConverter = new global::System.Text.Json.Serialization.Converters.GuidConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000292AC File Offset: 0x000274AC
		public static JsonConverter<short> Int16Converter
		{
			get
			{
				JsonConverter<short> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_int16Converter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_int16Converter = new global::System.Text.Json.Serialization.Converters.Int16Converter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000292C2 File Offset: 0x000274C2
		public static JsonConverter<int> Int32Converter
		{
			get
			{
				JsonConverter<int> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_int32Converter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_int32Converter = new global::System.Text.Json.Serialization.Converters.Int32Converter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000292D8 File Offset: 0x000274D8
		public static JsonConverter<long> Int64Converter
		{
			get
			{
				JsonConverter<long> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_int64Converter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_int64Converter = new global::System.Text.Json.Serialization.Converters.Int64Converter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000292EE File Offset: 0x000274EE
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<JsonArray> JsonArrayConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<JsonArray> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_jsonArrayConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_jsonArrayConverter = new JsonArrayConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00029304 File Offset: 0x00027504
		public static JsonConverter<JsonElement> JsonElementConverter
		{
			get
			{
				JsonConverter<JsonElement> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_jsonElementConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_jsonElementConverter = new JsonElementConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002931A File Offset: 0x0002751A
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<JsonNode> JsonNodeConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<JsonNode> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_jsonNodeConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_jsonNodeConverter = new JsonNodeConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00029330 File Offset: 0x00027530
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<JsonObject> JsonObjectConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<JsonObject> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_jsonObjectConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_jsonObjectConverter = new JsonObjectConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00029346 File Offset: 0x00027546
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<JsonValue> JsonValueConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<JsonValue> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_jsonValueConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_jsonValueConverter = new JsonValueConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0002935C File Offset: 0x0002755C
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<JsonDocument> JsonDocumentConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<JsonDocument> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_jsonDocumentConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_jsonDocumentConverter = new JsonDocumentConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00029372 File Offset: 0x00027572
		[Nullable(new byte[] { 1, 0 })]
		public static JsonConverter<Memory<byte>> MemoryByteConverter
		{
			[return: Nullable(new byte[] { 1, 0 })]
			get
			{
				JsonConverter<Memory<byte>> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_memoryByteConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_memoryByteConverter = new MemoryByteConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00029388 File Offset: 0x00027588
		[Nullable(new byte[] { 1, 0 })]
		public static JsonConverter<ReadOnlyMemory<byte>> ReadOnlyMemoryByteConverter
		{
			[return: Nullable(new byte[] { 1, 0 })]
			get
			{
				JsonConverter<ReadOnlyMemory<byte>> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_readOnlyMemoryByteConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_readOnlyMemoryByteConverter = new ReadOnlyMemoryByteConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002939E File Offset: 0x0002759E
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<object> ObjectConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<object> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_objectConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_objectConverter = new DefaultObjectConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000293B4 File Offset: 0x000275B4
		public static JsonConverter<float> SingleConverter
		{
			get
			{
				JsonConverter<float> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_singleConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_singleConverter = new global::System.Text.Json.Serialization.Converters.SingleConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000293CA File Offset: 0x000275CA
		[CLSCompliant(false)]
		public static JsonConverter<sbyte> SByteConverter
		{
			get
			{
				JsonConverter<sbyte> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_sbyteConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_sbyteConverter = new global::System.Text.Json.Serialization.Converters.SByteConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000293E0 File Offset: 0x000275E0
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<string> StringConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<string> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_stringConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_stringConverter = new global::System.Text.Json.Serialization.Converters.StringConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000293F6 File Offset: 0x000275F6
		public static JsonConverter<TimeSpan> TimeSpanConverter
		{
			get
			{
				JsonConverter<TimeSpan> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_timeSpanConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_timeSpanConverter = new global::System.Text.Json.Serialization.Converters.TimeSpanConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002940C File Offset: 0x0002760C
		[CLSCompliant(false)]
		public static JsonConverter<ushort> UInt16Converter
		{
			get
			{
				JsonConverter<ushort> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_uint16Converter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_uint16Converter = new global::System.Text.Json.Serialization.Converters.UInt16Converter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00029422 File Offset: 0x00027622
		[CLSCompliant(false)]
		public static JsonConverter<uint> UInt32Converter
		{
			get
			{
				JsonConverter<uint> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_uint32Converter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_uint32Converter = new global::System.Text.Json.Serialization.Converters.UInt32Converter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00029438 File Offset: 0x00027638
		[CLSCompliant(false)]
		public static JsonConverter<ulong> UInt64Converter
		{
			get
			{
				JsonConverter<ulong> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_uint64Converter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_uint64Converter = new global::System.Text.Json.Serialization.Converters.UInt64Converter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002944E File Offset: 0x0002764E
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<Uri> UriConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<Uri> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_uriConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_uriConverter = new UriConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00029464 File Offset: 0x00027664
		[Nullable(new byte[] { 1, 2 })]
		public static JsonConverter<Version> VersionConverter
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				JsonConverter<Version> jsonConverter;
				if ((jsonConverter = JsonMetadataServices.s_versionConverter) == null)
				{
					jsonConverter = (JsonMetadataServices.s_versionConverter = new VersionConverter());
				}
				return jsonConverter;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002947A File Offset: 0x0002767A
		public static JsonConverter<T> GetUnsupportedTypeConverter<[Nullable(2)] T>()
		{
			return new UnsupportedTypeConverter<T>(null);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00029482 File Offset: 0x00027682
		[return: Nullable(new byte[] { 1, 0 })]
		public static JsonConverter<T> GetEnumConverter<[Nullable(0)] T>(JsonSerializerOptions options) where T : struct, Enum
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			return new EnumConverter<T>(EnumConverterOptions.AllowNumbers, options);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00029498 File Offset: 0x00027698
		[NullableContext(0)]
		[return: Nullable(new byte[] { 1, 0 })]
		public static JsonConverter<T?> GetNullableConverter<T>([Nullable(new byte[] { 1, 0 })] JsonTypeInfo<T> underlyingTypeInfo) where T : struct
		{
			if (underlyingTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("underlyingTypeInfo");
			}
			JsonConverter<T> typedConverter = JsonMetadataServices.GetTypedConverter<T>(underlyingTypeInfo.Converter);
			return new NullableConverter<T>(typedConverter);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000294C4 File Offset: 0x000276C4
		[NullableContext(0)]
		[return: Nullable(new byte[] { 1, 0 })]
		public static JsonConverter<T?> GetNullableConverter<T>([Nullable(1)] JsonSerializerOptions options) where T : struct
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			JsonConverter<T> typedConverter = JsonMetadataServices.GetTypedConverter<T>(options.GetConverterInternal(typeof(T)));
			return new NullableConverter<T>(typedConverter);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000294FC File Offset: 0x000276FC
		internal static JsonConverter<T> GetTypedConverter<T>(JsonConverter converter)
		{
			JsonConverter<T> jsonConverter = converter as JsonConverter<T>;
			if (jsonConverter == null)
			{
				throw new InvalidOperationException(SR.Format(SR.SerializationConverterNotCompatible, jsonConverter, typeof(T)));
			}
			return jsonConverter;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00029530 File Offset: 0x00027730
		private static JsonTypeInfo<T> CreateCore<T>(JsonConverter converter, JsonSerializerOptions options)
		{
			JsonTypeInfo<T> jsonTypeInfo = new JsonTypeInfo<T>(converter, options);
			jsonTypeInfo.PopulatePolymorphismMetadata();
			jsonTypeInfo.MapInterfaceTypesToCallbacks();
			converter.ConfigureJsonTypeInfo(jsonTypeInfo, options);
			return jsonTypeInfo;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002955C File Offset: 0x0002775C
		private static JsonTypeInfo<T> CreateCore<T>(JsonSerializerOptions options, JsonObjectInfoValues<T> objectInfo)
		{
			JsonConverter<T> converter = JsonMetadataServices.GetConverter<T>(objectInfo);
			JsonTypeInfo<T> jsonTypeInfo = new JsonTypeInfo<T>(converter, options);
			if (objectInfo.ObjectWithParameterizedConstructorCreator != null)
			{
				jsonTypeInfo.CreateObjectWithArgs = objectInfo.ObjectWithParameterizedConstructorCreator;
				JsonMetadataServices.PopulateParameterInfoValues(jsonTypeInfo, objectInfo.ConstructorParameterMetadataInitializer);
			}
			else
			{
				jsonTypeInfo.SetCreateObjectIfCompatible(objectInfo.ObjectCreator);
				jsonTypeInfo.CreateObjectForExtensionDataProperty = jsonTypeInfo.CreateObject;
			}
			if (objectInfo.PropertyMetadataInitializer != null)
			{
				jsonTypeInfo.SourceGenDelayedPropertyInitializer = objectInfo.PropertyMetadataInitializer;
			}
			else
			{
				jsonTypeInfo.PropertyMetadataSerializationNotSupported = true;
			}
			jsonTypeInfo.SerializeHandler = objectInfo.SerializeHandler;
			jsonTypeInfo.NumberHandling = new JsonNumberHandling?(objectInfo.NumberHandling);
			jsonTypeInfo.PopulatePolymorphismMetadata();
			jsonTypeInfo.MapInterfaceTypesToCallbacks();
			converter.ConfigureJsonTypeInfo(jsonTypeInfo, options);
			return jsonTypeInfo;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00029604 File Offset: 0x00027804
		private static JsonTypeInfo<T> CreateCore<T>(JsonSerializerOptions options, JsonCollectionInfoValues<T> collectionInfo, JsonConverter<T> converter, object createObjectWithArgs = null, object addFunc = null)
		{
			if (collectionInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("collectionInfo");
			}
			converter = ((collectionInfo.SerializeHandler != null) ? new JsonMetadataServicesConverter<T>(converter) : converter);
			JsonTypeInfo<T> jsonTypeInfo = new JsonTypeInfo<T>(converter, options);
			jsonTypeInfo.KeyTypeInfo = collectionInfo.KeyInfo;
			jsonTypeInfo.ElementTypeInfo = collectionInfo.ElementInfo;
			jsonTypeInfo.NumberHandling = new JsonNumberHandling?(collectionInfo.NumberHandling);
			jsonTypeInfo.SerializeHandler = collectionInfo.SerializeHandler;
			jsonTypeInfo.CreateObjectWithArgs = createObjectWithArgs;
			jsonTypeInfo.AddMethodDelegate = addFunc;
			jsonTypeInfo.SetCreateObjectIfCompatible(collectionInfo.ObjectCreator);
			jsonTypeInfo.PopulatePolymorphismMetadata();
			jsonTypeInfo.MapInterfaceTypesToCallbacks();
			converter.ConfigureJsonTypeInfo(jsonTypeInfo, options);
			return jsonTypeInfo;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000296A0 File Offset: 0x000278A0
		private static JsonConverter<T> GetConverter<T>(JsonObjectInfoValues<T> objectInfo)
		{
			JsonConverter<T> jsonConverter = ((objectInfo.ObjectWithParameterizedConstructorCreator != null) ? new LargeObjectWithParameterizedConstructorConverter<T>() : new ObjectDefaultConverter<T>());
			if (objectInfo.SerializeHandler == null)
			{
				return jsonConverter;
			}
			return new JsonMetadataServicesConverter<T>(jsonConverter);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000296D4 File Offset: 0x000278D4
		private static void PopulateParameterInfoValues(JsonTypeInfo typeInfo, Func<JsonParameterInfoValues[]> paramFactory)
		{
			JsonParameterInfoValues[] array = ((paramFactory != null) ? paramFactory() : null);
			if (array != null)
			{
				typeInfo.ParameterInfoValues = array;
				return;
			}
			typeInfo.PropertyMetadataSerializationNotSupported = true;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00029700 File Offset: 0x00027900
		internal static void PopulateProperties(JsonTypeInfo typeInfo, JsonTypeInfo.JsonPropertyInfoList propertyList, Func<JsonSerializerContext, JsonPropertyInfo[]> propInitFunc)
		{
			JsonSerializerContext jsonSerializerContext = typeInfo.Options.TypeInfoResolver as JsonSerializerContext;
			JsonPropertyInfo[] array = propInitFunc(jsonSerializerContext);
			JsonTypeInfo.PropertyHierarchyResolutionState propertyHierarchyResolutionState = new JsonTypeInfo.PropertyHierarchyResolutionState(typeInfo.Options);
			foreach (JsonPropertyInfo jsonPropertyInfo in array)
			{
				if (!jsonPropertyInfo.SrcGen_IsPublic)
				{
					if (jsonPropertyInfo.SrcGen_HasJsonInclude)
					{
						ThrowHelper.ThrowInvalidOperationException_JsonIncludeOnInaccessibleProperty(jsonPropertyInfo.MemberName, jsonPropertyInfo.DeclaringType);
					}
				}
				else if (jsonPropertyInfo.MemberType != MemberTypes.Field || jsonPropertyInfo.SrcGen_HasJsonInclude || typeInfo.Options.IncludeFields)
				{
					propertyList.AddPropertyWithConflictResolution(jsonPropertyInfo, ref propertyHierarchyResolutionState);
				}
			}
			if (propertyHierarchyResolutionState.IsPropertyOrderSpecified)
			{
				propertyList.SortProperties();
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000297AC File Offset: 0x000279AC
		private static JsonPropertyInfo<T> CreatePropertyInfoCore<T>(JsonPropertyInfoValues<T> propertyInfoValues, JsonSerializerOptions options)
		{
			JsonPropertyInfo<T> jsonPropertyInfo = new JsonPropertyInfo<T>(propertyInfoValues.DeclaringType, null, options);
			JsonMetadataServices.DeterminePropertyName(jsonPropertyInfo, propertyInfoValues.PropertyName, propertyInfoValues.JsonPropertyName);
			jsonPropertyInfo.MemberName = propertyInfoValues.PropertyName;
			jsonPropertyInfo.MemberType = (propertyInfoValues.IsProperty ? MemberTypes.Property : MemberTypes.Field);
			jsonPropertyInfo.SrcGen_IsPublic = propertyInfoValues.IsPublic;
			jsonPropertyInfo.SrcGen_HasJsonInclude = propertyInfoValues.HasJsonInclude;
			jsonPropertyInfo.IsExtensionData = propertyInfoValues.IsExtensionData;
			jsonPropertyInfo.CustomConverter = propertyInfoValues.Converter;
			JsonIgnoreCondition? ignoreCondition = jsonPropertyInfo.IgnoreCondition;
			JsonIgnoreCondition jsonIgnoreCondition = JsonIgnoreCondition.Always;
			if (!((ignoreCondition.GetValueOrDefault() == jsonIgnoreCondition) & (ignoreCondition != null)))
			{
				jsonPropertyInfo.Get = propertyInfoValues.Getter;
				jsonPropertyInfo.Set = propertyInfoValues.Setter;
			}
			jsonPropertyInfo.IgnoreCondition = propertyInfoValues.IgnoreCondition;
			jsonPropertyInfo.JsonTypeInfo = propertyInfoValues.PropertyTypeInfo;
			jsonPropertyInfo.NumberHandling = propertyInfoValues.NumberHandling;
			return jsonPropertyInfo;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00029884 File Offset: 0x00027A84
		private static void DeterminePropertyName(JsonPropertyInfo propertyInfo, string declaredPropertyName, string declaredJsonPropertyName)
		{
			string text;
			if (declaredJsonPropertyName != null)
			{
				text = declaredJsonPropertyName;
			}
			else if (propertyInfo.Options.PropertyNamingPolicy == null)
			{
				text = declaredPropertyName;
			}
			else
			{
				text = propertyInfo.Options.PropertyNamingPolicy.ConvertName(declaredPropertyName);
			}
			if (text == null)
			{
				ThrowHelper.ThrowInvalidOperationException_SerializerPropertyNameNull(propertyInfo);
			}
			propertyInfo.Name = text;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000298CC File Offset: 0x00027ACC
		public static JsonPropertyInfo CreatePropertyInfo<[Nullable(2)] T>(JsonSerializerOptions options, JsonPropertyInfoValues<T> propertyInfo)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			if (propertyInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("propertyInfo");
			}
			Type declaringType = propertyInfo.DeclaringType;
			if (declaringType == null)
			{
				throw new ArgumentException("DeclaringType");
			}
			if (propertyInfo.PropertyName == null)
			{
				throw new ArgumentException("PropertyName");
			}
			if (!propertyInfo.IsProperty && propertyInfo.IsVirtual)
			{
				throw new InvalidOperationException(SR.Format(SR.FieldCannotBeVirtual, "IsProperty", "IsVirtual"));
			}
			return JsonMetadataServices.CreatePropertyInfoCore<T>(propertyInfo, options);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00029954 File Offset: 0x00027B54
		public static JsonTypeInfo<T> CreateObjectInfo<T>(JsonSerializerOptions options, JsonObjectInfoValues<T> objectInfo)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			if (objectInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("objectInfo");
			}
			return JsonMetadataServices.CreateCore<T>(options, objectInfo);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00029978 File Offset: 0x00027B78
		public static JsonTypeInfo<T> CreateValueInfo<[Nullable(2)] T>(JsonSerializerOptions options, JsonConverter converter)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException("converter");
			}
			return JsonMetadataServices.CreateCore<T>(converter, options);
		}

		// Token: 0x04000340 RID: 832
		private static JsonConverter<bool> s_booleanConverter;

		// Token: 0x04000341 RID: 833
		private static JsonConverter<byte[]> s_byteArrayConverter;

		// Token: 0x04000342 RID: 834
		private static JsonConverter<byte> s_byteConverter;

		// Token: 0x04000343 RID: 835
		private static JsonConverter<char> s_charConverter;

		// Token: 0x04000344 RID: 836
		private static JsonConverter<DateTime> s_dateTimeConverter;

		// Token: 0x04000345 RID: 837
		private static JsonConverter<DateTimeOffset> s_dateTimeOffsetConverter;

		// Token: 0x04000346 RID: 838
		private static JsonConverter<decimal> s_decimalConverter;

		// Token: 0x04000347 RID: 839
		private static JsonConverter<double> s_doubleConverter;

		// Token: 0x04000348 RID: 840
		private static JsonConverter<Guid> s_guidConverter;

		// Token: 0x04000349 RID: 841
		private static JsonConverter<short> s_int16Converter;

		// Token: 0x0400034A RID: 842
		private static JsonConverter<int> s_int32Converter;

		// Token: 0x0400034B RID: 843
		private static JsonConverter<long> s_int64Converter;

		// Token: 0x0400034C RID: 844
		private static JsonConverter<JsonArray> s_jsonArrayConverter;

		// Token: 0x0400034D RID: 845
		private static JsonConverter<JsonElement> s_jsonElementConverter;

		// Token: 0x0400034E RID: 846
		private static JsonConverter<JsonNode> s_jsonNodeConverter;

		// Token: 0x0400034F RID: 847
		private static JsonConverter<JsonObject> s_jsonObjectConverter;

		// Token: 0x04000350 RID: 848
		private static JsonConverter<JsonValue> s_jsonValueConverter;

		// Token: 0x04000351 RID: 849
		private static JsonConverter<JsonDocument> s_jsonDocumentConverter;

		// Token: 0x04000352 RID: 850
		private static JsonConverter<Memory<byte>> s_memoryByteConverter;

		// Token: 0x04000353 RID: 851
		private static JsonConverter<ReadOnlyMemory<byte>> s_readOnlyMemoryByteConverter;

		// Token: 0x04000354 RID: 852
		private static JsonConverter<object> s_objectConverter;

		// Token: 0x04000355 RID: 853
		private static JsonConverter<float> s_singleConverter;

		// Token: 0x04000356 RID: 854
		private static JsonConverter<sbyte> s_sbyteConverter;

		// Token: 0x04000357 RID: 855
		private static JsonConverter<string> s_stringConverter;

		// Token: 0x04000358 RID: 856
		private static JsonConverter<TimeSpan> s_timeSpanConverter;

		// Token: 0x04000359 RID: 857
		private static JsonConverter<ushort> s_uint16Converter;

		// Token: 0x0400035A RID: 858
		private static JsonConverter<uint> s_uint32Converter;

		// Token: 0x0400035B RID: 859
		private static JsonConverter<ulong> s_uint64Converter;

		// Token: 0x0400035C RID: 860
		private static JsonConverter<Uri> s_uriConverter;

		// Token: 0x0400035D RID: 861
		private static JsonConverter<Version> s_versionConverter;
	}
}
