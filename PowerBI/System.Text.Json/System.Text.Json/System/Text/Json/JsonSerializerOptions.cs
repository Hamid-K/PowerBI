using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;
using System.Text.Json.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Converters;
using System.Text.Json.Serialization.Metadata;
using System.Threading;

namespace System.Text.Json
{
	// Token: 0x0200004F RID: 79
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public sealed class JsonSerializerOptions
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00013E0C File Offset: 0x0001200C
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal JsonSerializerOptions.CachingContext CacheContext
		{
			get
			{
				JsonSerializerOptions.CachingContext cachingContext;
				if ((cachingContext = this._cachingContext) == null)
				{
					cachingContext = (this._cachingContext = JsonSerializerOptions.TrackedCachingContexts.GetOrCreate(this));
				}
				return cachingContext;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00013E32 File Offset: 0x00012032
		[NullableContext(1)]
		public JsonTypeInfo GetTypeInfo(Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException("type");
			}
			if (JsonTypeInfo.IsInvalidForSerialization(type))
			{
				ThrowHelper.ThrowArgumentException_CannotSerializeInvalidType("type", type, null, null);
			}
			return this.GetTypeInfoInternal(type, true, new bool?(true), true, false);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013E68 File Offset: 0x00012068
		[NullableContext(1)]
		public bool TryGetTypeInfo(Type type, [Nullable(2)] [NotNullWhen(true)] out JsonTypeInfo typeInfo)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException("type");
			}
			if (JsonTypeInfo.IsInvalidForSerialization(type))
			{
				ThrowHelper.ThrowArgumentException_CannotSerializeInvalidType("type", type, null, null);
			}
			typeInfo = this.GetTypeInfoInternal(type, true, null, true, false);
			return typeInfo != null;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013EB4 File Offset: 0x000120B4
		[return: NotNullIfNotNull("ensureNotNull")]
		internal JsonTypeInfo GetTypeInfoInternal(Type type, bool ensureConfigured = true, bool? ensureNotNull = true, bool resolveIfMutable = false, bool fallBackToNearestAncestorType = false)
		{
			JsonTypeInfo jsonTypeInfo = null;
			if (this.IsReadOnly)
			{
				jsonTypeInfo = this.CacheContext.GetOrAddTypeInfo(type, fallBackToNearestAncestorType);
				if (ensureConfigured && jsonTypeInfo != null)
				{
					jsonTypeInfo.EnsureConfigured();
				}
			}
			else if (resolveIfMutable)
			{
				jsonTypeInfo = this.GetTypeInfoNoCaching(type);
			}
			if (jsonTypeInfo == null)
			{
				bool? flag = ensureNotNull;
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					ThrowHelper.ThrowNotSupportedException_NoMetadataForType(type, this.TypeInfoResolver);
				}
			}
			return jsonTypeInfo;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00013F1C File Offset: 0x0001211C
		internal bool TryGetTypeInfoCached(Type type, [NotNullWhen(true)] out JsonTypeInfo typeInfo)
		{
			if (this._cachingContext == null)
			{
				typeInfo = null;
				return false;
			}
			return this._cachingContext.TryGetTypeInfo(type, out typeInfo);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013F38 File Offset: 0x00012138
		internal JsonTypeInfo GetTypeInfoForRootType(Type type, bool fallBackToNearestAncestorType = false)
		{
			JsonTypeInfo jsonTypeInfo = this._lastTypeInfo;
			if (((jsonTypeInfo != null) ? jsonTypeInfo.Type : null) != type)
			{
				jsonTypeInfo = (this._lastTypeInfo = this.GetTypeInfoInternal(type, true, new bool?(true), false, fallBackToNearestAncestorType));
			}
			return jsonTypeInfo;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013F80 File Offset: 0x00012180
		internal bool TryGetPolymorphicTypeInfoForRootType(object rootValue, [NotNullWhen(true)] out JsonTypeInfo polymorphicTypeInfo)
		{
			Type type = rootValue.GetType();
			if (type != JsonTypeInfo.ObjectType)
			{
				polymorphicTypeInfo = this.GetTypeInfoForRootType(type, true);
				JsonTypeInfo ancestorPolymorphicType = polymorphicTypeInfo.AncestorPolymorphicType;
				if (ancestorPolymorphicType != null)
				{
					polymorphicTypeInfo = ancestorPolymorphicType;
				}
				return true;
			}
			polymorphicTypeInfo = null;
			return false;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00013FC0 File Offset: 0x000121C0
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal JsonTypeInfo ObjectTypeInfo
		{
			get
			{
				JsonTypeInfo jsonTypeInfo;
				if ((jsonTypeInfo = this._objectTypeInfo) == null)
				{
					jsonTypeInfo = (this._objectTypeInfo = this.GetTypeInfoInternal(JsonTypeInfo.ObjectType, true, new bool?(true), false, false));
				}
				return jsonTypeInfo;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013FF4 File Offset: 0x000121F4
		internal void ClearCaches()
		{
			JsonSerializerOptions.CachingContext cachingContext = this._cachingContext;
			if (cachingContext != null)
			{
				cachingContext.Clear();
			}
			this._lastTypeInfo = null;
			this._objectTypeInfo = null;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00014018 File Offset: 0x00012218
		[Nullable(1)]
		public IList<JsonConverter> Converters
		{
			[NullableContext(1)]
			get
			{
				JsonSerializerOptions.ConverterList converterList;
				if ((converterList = this._converters) == null)
				{
					converterList = (this._converters = new JsonSerializerOptions.ConverterList(this, null));
				}
				return converterList;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001403F File Offset: 0x0001223F
		[NullableContext(1)]
		[RequiresUnreferencedCode("Getting a converter for a type may require reflection which depends on unreferenced code.")]
		[RequiresDynamicCode("Getting a converter for a type may require reflection which depends on runtime code generation.")]
		public JsonConverter GetConverter(Type typeToConvert)
		{
			if (typeToConvert == null)
			{
				ThrowHelper.ThrowArgumentNullException("typeToConvert");
			}
			if (JsonSerializer.IsReflectionEnabledByDefault && this._typeInfoResolver == null)
			{
				return DefaultJsonTypeInfoResolver.GetConverterForType(typeToConvert, this, true);
			}
			return this.GetConverterInternal(typeToConvert);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00014070 File Offset: 0x00012270
		internal JsonConverter GetConverterInternal(Type typeToConvert)
		{
			JsonTypeInfo typeInfoInternal = this.GetTypeInfoInternal(typeToConvert, false, new bool?(true), true, false);
			return typeInfoInternal.Converter;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00014094 File Offset: 0x00012294
		internal JsonConverter GetConverterFromList(Type typeToConvert)
		{
			JsonSerializerOptions.ConverterList converters = this._converters;
			if (converters != null)
			{
				foreach (JsonConverter jsonConverter in converters)
				{
					if (jsonConverter.CanConvert(typeToConvert))
					{
						return jsonConverter;
					}
				}
			}
			return null;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000140F8 File Offset: 0x000122F8
		[return: NotNullIfNotNull("converter")]
		internal JsonConverter ExpandConverterFactory(JsonConverter converter, Type typeToConvert)
		{
			JsonConverterFactory jsonConverterFactory = converter as JsonConverterFactory;
			if (jsonConverterFactory != null)
			{
				converter = jsonConverterFactory.GetConverterInternal(typeToConvert, this);
			}
			return converter;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001411A File Offset: 0x0001231A
		internal static void CheckConverterNullabilityIsSameAsPropertyType(JsonConverter converter, Type propertyType)
		{
			if (propertyType.IsValueType && converter.IsValueType && (propertyType.IsNullableOfT() ^ converter.Type.IsNullableOfT()))
			{
				ThrowHelper.ThrowInvalidOperationException_ConverterCanConvertMultipleTypes(propertyType, converter);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00014148 File Offset: 0x00012348
		[Nullable(1)]
		public static JsonSerializerOptions Default
		{
			[NullableContext(1)]
			[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
			[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
			get
			{
				JsonSerializerOptions orCreateDefaultOptionsInstance = JsonSerializerOptions.s_defaultOptions;
				if (orCreateDefaultOptionsInstance == null)
				{
					orCreateDefaultOptionsInstance = JsonSerializerOptions.GetOrCreateDefaultOptionsInstance();
				}
				return orCreateDefaultOptionsInstance;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00014165 File Offset: 0x00012365
		public JsonSerializerOptions()
		{
			JsonSerializerOptions.TrackOptionsInstance(this);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014188 File Offset: 0x00012388
		[NullableContext(1)]
		public JsonSerializerOptions(JsonSerializerOptions options)
		{
			if (options == null)
			{
				ThrowHelper.ThrowArgumentNullException("options");
			}
			this._dictionaryKeyPolicy = options._dictionaryKeyPolicy;
			this._jsonPropertyNamingPolicy = options._jsonPropertyNamingPolicy;
			this._readCommentHandling = options._readCommentHandling;
			this._referenceHandler = options._referenceHandler;
			JsonSerializerOptions.ConverterList converters = options._converters;
			this._converters = ((converters != null) ? new JsonSerializerOptions.ConverterList(this, converters) : null);
			this._encoder = options._encoder;
			this._defaultIgnoreCondition = options._defaultIgnoreCondition;
			this._numberHandling = options._numberHandling;
			this._preferredObjectCreationHandling = options._preferredObjectCreationHandling;
			this._unknownTypeHandling = options._unknownTypeHandling;
			this._unmappedMemberHandling = options._unmappedMemberHandling;
			this._defaultBufferSize = options._defaultBufferSize;
			this._maxDepth = options._maxDepth;
			this._allowTrailingCommas = options._allowTrailingCommas;
			this._ignoreNullValues = options._ignoreNullValues;
			this._ignoreReadOnlyProperties = options._ignoreReadOnlyProperties;
			this._ignoreReadonlyFields = options._ignoreReadonlyFields;
			this._includeFields = options._includeFields;
			this._propertyNameCaseInsensitive = options._propertyNameCaseInsensitive;
			this._writeIndented = options._writeIndented;
			this._typeInfoResolver = options._typeInfoResolver;
			this.EffectiveMaxDepth = options.EffectiveMaxDepth;
			this.ReferenceHandlingStrategy = options.ReferenceHandlingStrategy;
			JsonSerializerOptions.TrackOptionsInstance(this);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000142E3 File Offset: 0x000124E3
		public JsonSerializerOptions(JsonSerializerDefaults defaults)
			: this()
		{
			if (defaults == JsonSerializerDefaults.Web)
			{
				this._propertyNameCaseInsensitive = true;
				this._jsonPropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				this._numberHandling = JsonNumberHandling.AllowReadingFromString;
				return;
			}
			if (defaults != JsonSerializerDefaults.General)
			{
				throw new ArgumentOutOfRangeException("defaults");
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00014317 File Offset: 0x00012517
		private static void TrackOptionsInstance(JsonSerializerOptions options)
		{
			JsonSerializerOptions.TrackedOptionsInstances.All.Add(options, null);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00014328 File Offset: 0x00012528
		[NullableContext(0)]
		[global::System.Obsolete("JsonSerializerOptions.AddContext is obsolete. To register a JsonSerializerContext, use either the TypeInfoResolver or TypeInfoResolverChain properties.", DiagnosticId = "SYSLIB0049", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void AddContext<TContext>() where TContext : JsonSerializerContext, new()
		{
			this.VerifyMutable();
			TContext tcontext = new TContext();
			tcontext.AssociateWithOptions(this);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001434D File Offset: 0x0001254D
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00014358 File Offset: 0x00012558
		public IJsonTypeInfoResolver TypeInfoResolver
		{
			get
			{
				return this._typeInfoResolver;
			}
			set
			{
				this.VerifyMutable();
				JsonSerializerOptions.OptionsBoundJsonTypeInfoResolverChain typeInfoResolverChain = this._typeInfoResolverChain;
				if (typeInfoResolverChain != null && typeInfoResolverChain != value)
				{
					typeInfoResolverChain.Clear();
					typeInfoResolverChain.AddFlattened(value);
				}
				this._typeInfoResolver = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00014390 File Offset: 0x00012590
		[Nullable(1)]
		public IList<IJsonTypeInfoResolver> TypeInfoResolverChain
		{
			[NullableContext(1)]
			get
			{
				JsonSerializerOptions.OptionsBoundJsonTypeInfoResolverChain optionsBoundJsonTypeInfoResolverChain;
				if ((optionsBoundJsonTypeInfoResolverChain = this._typeInfoResolverChain) == null)
				{
					optionsBoundJsonTypeInfoResolverChain = (this._typeInfoResolverChain = new JsonSerializerOptions.OptionsBoundJsonTypeInfoResolverChain(this));
				}
				return optionsBoundJsonTypeInfoResolverChain;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000143B6 File Offset: 0x000125B6
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000143BE File Offset: 0x000125BE
		public bool AllowTrailingCommas
		{
			get
			{
				return this._allowTrailingCommas;
			}
			set
			{
				this.VerifyMutable();
				this._allowTrailingCommas = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000143CD File Offset: 0x000125CD
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000143D5 File Offset: 0x000125D5
		public int DefaultBufferSize
		{
			get
			{
				return this._defaultBufferSize;
			}
			set
			{
				this.VerifyMutable();
				if (value < 1)
				{
					throw new ArgumentException(SR.SerializationInvalidBufferSize);
				}
				this._defaultBufferSize = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000143F3 File Offset: 0x000125F3
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x000143FB File Offset: 0x000125FB
		public JavaScriptEncoder Encoder
		{
			get
			{
				return this._encoder;
			}
			set
			{
				this.VerifyMutable();
				this._encoder = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001440A File Offset: 0x0001260A
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00014412 File Offset: 0x00012612
		public JsonNamingPolicy DictionaryKeyPolicy
		{
			get
			{
				return this._dictionaryKeyPolicy;
			}
			set
			{
				this.VerifyMutable();
				this._dictionaryKeyPolicy = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00014421 File Offset: 0x00012621
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00014429 File Offset: 0x00012629
		[global::System.Obsolete("JsonSerializerOptions.IgnoreNullValues is obsolete. To ignore null values when serializing, set DefaultIgnoreCondition to JsonIgnoreCondition.WhenWritingNull.", DiagnosticId = "SYSLIB0020", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IgnoreNullValues
		{
			get
			{
				return this._ignoreNullValues;
			}
			set
			{
				this.VerifyMutable();
				if (value && this._defaultIgnoreCondition != JsonIgnoreCondition.Never)
				{
					throw new InvalidOperationException(SR.DefaultIgnoreConditionAlreadySpecified);
				}
				this._ignoreNullValues = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001444E File Offset: 0x0001264E
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00014456 File Offset: 0x00012656
		public JsonIgnoreCondition DefaultIgnoreCondition
		{
			get
			{
				return this._defaultIgnoreCondition;
			}
			set
			{
				this.VerifyMutable();
				if (value == JsonIgnoreCondition.Always)
				{
					throw new ArgumentException(SR.DefaultIgnoreConditionInvalid);
				}
				if (value != JsonIgnoreCondition.Never && this._ignoreNullValues)
				{
					throw new InvalidOperationException(SR.DefaultIgnoreConditionAlreadySpecified);
				}
				this._defaultIgnoreCondition = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001448A File Offset: 0x0001268A
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00014492 File Offset: 0x00012692
		public JsonNumberHandling NumberHandling
		{
			get
			{
				return this._numberHandling;
			}
			set
			{
				this.VerifyMutable();
				if (!JsonSerializer.IsValidNumberHandlingValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._numberHandling = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000144B4 File Offset: 0x000126B4
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x000144BC File Offset: 0x000126BC
		public JsonObjectCreationHandling PreferredObjectCreationHandling
		{
			get
			{
				return this._preferredObjectCreationHandling;
			}
			set
			{
				this.VerifyMutable();
				if (!JsonSerializer.IsValidCreationHandlingValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._preferredObjectCreationHandling = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000144DE File Offset: 0x000126DE
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x000144E6 File Offset: 0x000126E6
		public bool IgnoreReadOnlyProperties
		{
			get
			{
				return this._ignoreReadOnlyProperties;
			}
			set
			{
				this.VerifyMutable();
				this._ignoreReadOnlyProperties = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000144F5 File Offset: 0x000126F5
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x000144FD File Offset: 0x000126FD
		public bool IgnoreReadOnlyFields
		{
			get
			{
				return this._ignoreReadonlyFields;
			}
			set
			{
				this.VerifyMutable();
				this._ignoreReadonlyFields = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001450C File Offset: 0x0001270C
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00014514 File Offset: 0x00012714
		public bool IncludeFields
		{
			get
			{
				return this._includeFields;
			}
			set
			{
				this.VerifyMutable();
				this._includeFields = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00014523 File Offset: 0x00012723
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001452B File Offset: 0x0001272B
		public int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				this.VerifyMutable();
				if (value < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_MaxDepthMustBePositive("value");
				}
				this._maxDepth = value;
				this.EffectiveMaxDepth = ((value == 0) ? 64 : value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00014556 File Offset: 0x00012756
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001455E File Offset: 0x0001275E
		internal int EffectiveMaxDepth { get; private set; } = 64;

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00014567 File Offset: 0x00012767
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0001456F File Offset: 0x0001276F
		public JsonNamingPolicy PropertyNamingPolicy
		{
			get
			{
				return this._jsonPropertyNamingPolicy;
			}
			set
			{
				this.VerifyMutable();
				this._jsonPropertyNamingPolicy = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001457E File Offset: 0x0001277E
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00014586 File Offset: 0x00012786
		public bool PropertyNameCaseInsensitive
		{
			get
			{
				return this._propertyNameCaseInsensitive;
			}
			set
			{
				this.VerifyMutable();
				this._propertyNameCaseInsensitive = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00014595 File Offset: 0x00012795
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001459D File Offset: 0x0001279D
		public JsonCommentHandling ReadCommentHandling
		{
			get
			{
				return this._readCommentHandling;
			}
			set
			{
				this.VerifyMutable();
				if (value > JsonCommentHandling.Skip)
				{
					throw new ArgumentOutOfRangeException("value", SR.JsonSerializerDoesNotSupportComments);
				}
				this._readCommentHandling = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000145C0 File Offset: 0x000127C0
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x000145C8 File Offset: 0x000127C8
		public JsonUnknownTypeHandling UnknownTypeHandling
		{
			get
			{
				return this._unknownTypeHandling;
			}
			set
			{
				this.VerifyMutable();
				this._unknownTypeHandling = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000145D7 File Offset: 0x000127D7
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x000145DF File Offset: 0x000127DF
		public JsonUnmappedMemberHandling UnmappedMemberHandling
		{
			get
			{
				return this._unmappedMemberHandling;
			}
			set
			{
				this.VerifyMutable();
				this._unmappedMemberHandling = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000145EE File Offset: 0x000127EE
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x000145F6 File Offset: 0x000127F6
		public bool WriteIndented
		{
			get
			{
				return this._writeIndented;
			}
			set
			{
				this.VerifyMutable();
				this._writeIndented = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00014605 File Offset: 0x00012805
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0001460D File Offset: 0x0001280D
		public ReferenceHandler ReferenceHandler
		{
			get
			{
				return this._referenceHandler;
			}
			set
			{
				this.VerifyMutable();
				this._referenceHandler = value;
				this.ReferenceHandlingStrategy = ((value != null) ? value.HandlingStrategy : ReferenceHandlingStrategy.None);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00014630 File Offset: 0x00012830
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal bool CanUseFastPathSerializationLogic
		{
			get
			{
				bool flag = this._canUseFastPathSerializationLogic.GetValueOrDefault();
				if (this._canUseFastPathSerializationLogic == null)
				{
					flag = this.TypeInfoResolver.IsCompatibleWithOptions(this);
					this._canUseFastPathSerializationLogic = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00014672 File Offset: 0x00012872
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001467C File Offset: 0x0001287C
		public void MakeReadOnly()
		{
			if (this._typeInfoResolver == null)
			{
				ThrowHelper.ThrowInvalidOperationException_JsonSerializerOptionsNoTypeInfoResolverSpecified();
			}
			this._isReadOnly = true;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00014694 File Offset: 0x00012894
		[RequiresUnreferencedCode("Populating unconfigured TypeInfoResolver properties with the reflection resolver requires unreferenced code.")]
		[RequiresDynamicCode("Populating unconfigured TypeInfoResolver properties with the reflection resolver requires runtime code generation.")]
		public void MakeReadOnly(bool populateMissingResolver)
		{
			if (populateMissingResolver)
			{
				if (!this._isConfiguredForJsonSerializer)
				{
					this.ConfigureForJsonSerializer();
					return;
				}
			}
			else
			{
				this.MakeReadOnly();
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000146B0 File Offset: 0x000128B0
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private void ConfigureForJsonSerializer()
		{
			if (JsonSerializer.IsReflectionEnabledByDefault)
			{
				DefaultJsonTypeInfoResolver defaultJsonTypeInfoResolver = DefaultJsonTypeInfoResolver.RootDefaultInstance();
				IJsonTypeInfoResolver typeInfoResolver = this._typeInfoResolver;
				if (typeInfoResolver != null)
				{
					JsonSerializerContext jsonSerializerContext = typeInfoResolver as JsonSerializerContext;
					if (jsonSerializerContext != null)
					{
						if (AppContextSwitchHelper.IsSourceGenReflectionFallbackEnabled)
						{
							this._effectiveJsonTypeInfoResolver = JsonTypeInfoResolver.Combine(new IJsonTypeInfoResolver[] { jsonSerializerContext, defaultJsonTypeInfoResolver });
							JsonSerializerOptions.CachingContext cachingContext = this._cachingContext;
							if (cachingContext != null)
							{
								if (cachingContext.Options != this && !cachingContext.Options._isConfiguredForJsonSerializer)
								{
									cachingContext.Options.ConfigureForJsonSerializer();
								}
								else
								{
									cachingContext.Clear();
								}
							}
						}
					}
				}
				else
				{
					this._typeInfoResolver = defaultJsonTypeInfoResolver;
				}
			}
			else
			{
				IJsonTypeInfoResolver typeInfoResolver2 = this._typeInfoResolver;
				bool flag = typeInfoResolver2 == null || typeInfoResolver2 is EmptyJsonTypeInfoResolver;
				if (flag)
				{
					ThrowHelper.ThrowInvalidOperationException_JsonSerializerIsReflectionDisabled();
				}
			}
			this._isReadOnly = true;
			this._isConfiguredForJsonSerializer = true;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001477C File Offset: 0x0001297C
		private JsonTypeInfo GetTypeInfoNoCaching(Type type)
		{
			IJsonTypeInfoResolver jsonTypeInfoResolver = this._effectiveJsonTypeInfoResolver ?? this._typeInfoResolver;
			if (jsonTypeInfoResolver == null)
			{
				return null;
			}
			JsonTypeInfo jsonTypeInfo = jsonTypeInfoResolver.GetTypeInfo(type, this);
			if (jsonTypeInfo != null)
			{
				if (jsonTypeInfo.Type != type)
				{
					ThrowHelper.ThrowInvalidOperationException_ResolverTypeNotCompatible(type, jsonTypeInfo.Type);
				}
				if (jsonTypeInfo.Options != this)
				{
					ThrowHelper.ThrowInvalidOperationException_ResolverTypeInfoOptionsNotCompatible();
				}
			}
			else if (type == JsonTypeInfo.ObjectType)
			{
				SlimObjectConverter slimObjectConverter = new SlimObjectConverter(jsonTypeInfoResolver);
				jsonTypeInfo = new JsonTypeInfo<object>(slimObjectConverter, this);
			}
			return jsonTypeInfo;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000147F4 File Offset: 0x000129F4
		internal JsonDocumentOptions GetDocumentOptions()
		{
			return new JsonDocumentOptions
			{
				AllowTrailingCommas = this.AllowTrailingCommas,
				CommentHandling = this.ReadCommentHandling,
				MaxDepth = this.MaxDepth
			};
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00014834 File Offset: 0x00012A34
		internal JsonNodeOptions GetNodeOptions()
		{
			return new JsonNodeOptions
			{
				PropertyNameCaseInsensitive = this.PropertyNameCaseInsensitive
			};
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014858 File Offset: 0x00012A58
		internal JsonReaderOptions GetReaderOptions()
		{
			return new JsonReaderOptions
			{
				AllowTrailingCommas = this.AllowTrailingCommas,
				CommentHandling = this.ReadCommentHandling,
				MaxDepth = this.EffectiveMaxDepth
			};
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014898 File Offset: 0x00012A98
		internal JsonWriterOptions GetWriterOptions()
		{
			return new JsonWriterOptions
			{
				Encoder = this.Encoder,
				Indented = this.WriteIndented,
				MaxDepth = this.EffectiveMaxDepth,
				SkipValidation = true
			};
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000148DD File Offset: 0x00012ADD
		internal void VerifyMutable()
		{
			if (this._isReadOnly)
			{
				ThrowHelper.ThrowInvalidOperationException_SerializerOptionsReadOnly(this._typeInfoResolver as JsonSerializerContext);
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000148FC File Offset: 0x00012AFC
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonSerializerOptions GetOrCreateDefaultOptionsInstance()
		{
			JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
			IJsonTypeInfoResolver jsonTypeInfoResolver;
			if (!JsonSerializer.IsReflectionEnabledByDefault)
			{
				jsonTypeInfoResolver = JsonTypeInfoResolver.Empty;
			}
			else
			{
				IJsonTypeInfoResolver jsonTypeInfoResolver2 = DefaultJsonTypeInfoResolver.RootDefaultInstance();
				jsonTypeInfoResolver = jsonTypeInfoResolver2;
			}
			jsonSerializerOptions.TypeInfoResolver = jsonTypeInfoResolver;
			jsonSerializerOptions._isReadOnly = true;
			JsonSerializerOptions jsonSerializerOptions2 = jsonSerializerOptions;
			return Interlocked.CompareExchange<JsonSerializerOptions>(ref JsonSerializerOptions.s_defaultOptions, jsonSerializerOptions2, null) ?? jsonSerializerOptions2;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00014944 File Offset: 0x00012B44
		[Nullable(1)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				string text = "TypeInfoResolver = {0}, IsReadOnly = {1}";
				IJsonTypeInfoResolver typeInfoResolver = this.TypeInfoResolver;
				return string.Format(text, ((typeInfoResolver != null) ? typeInfoResolver.ToString() : null) ?? "<null>", this.IsReadOnly);
			}
		}

		// Token: 0x040001AA RID: 426
		private JsonSerializerOptions.CachingContext _cachingContext;

		// Token: 0x040001AB RID: 427
		private volatile JsonTypeInfo _lastTypeInfo;

		// Token: 0x040001AC RID: 428
		private JsonTypeInfo _objectTypeInfo;

		// Token: 0x040001AD RID: 429
		internal const int BufferSizeDefault = 16384;

		// Token: 0x040001AE RID: 430
		internal const int DefaultMaxDepth = 64;

		// Token: 0x040001AF RID: 431
		private static JsonSerializerOptions s_defaultOptions;

		// Token: 0x040001B0 RID: 432
		private IJsonTypeInfoResolver _typeInfoResolver;

		// Token: 0x040001B1 RID: 433
		private JsonNamingPolicy _dictionaryKeyPolicy;

		// Token: 0x040001B2 RID: 434
		private JsonNamingPolicy _jsonPropertyNamingPolicy;

		// Token: 0x040001B3 RID: 435
		private JsonCommentHandling _readCommentHandling;

		// Token: 0x040001B4 RID: 436
		private ReferenceHandler _referenceHandler;

		// Token: 0x040001B5 RID: 437
		private JavaScriptEncoder _encoder;

		// Token: 0x040001B6 RID: 438
		private JsonSerializerOptions.ConverterList _converters;

		// Token: 0x040001B7 RID: 439
		private JsonIgnoreCondition _defaultIgnoreCondition;

		// Token: 0x040001B8 RID: 440
		private JsonNumberHandling _numberHandling;

		// Token: 0x040001B9 RID: 441
		private JsonObjectCreationHandling _preferredObjectCreationHandling;

		// Token: 0x040001BA RID: 442
		private JsonUnknownTypeHandling _unknownTypeHandling;

		// Token: 0x040001BB RID: 443
		private JsonUnmappedMemberHandling _unmappedMemberHandling;

		// Token: 0x040001BC RID: 444
		private int _defaultBufferSize = 16384;

		// Token: 0x040001BD RID: 445
		private int _maxDepth;

		// Token: 0x040001BE RID: 446
		private bool _allowTrailingCommas;

		// Token: 0x040001BF RID: 447
		private bool _ignoreNullValues;

		// Token: 0x040001C0 RID: 448
		private bool _ignoreReadOnlyProperties;

		// Token: 0x040001C1 RID: 449
		private bool _ignoreReadonlyFields;

		// Token: 0x040001C2 RID: 450
		private bool _includeFields;

		// Token: 0x040001C3 RID: 451
		private bool _propertyNameCaseInsensitive;

		// Token: 0x040001C4 RID: 452
		private bool _writeIndented;

		// Token: 0x040001C5 RID: 453
		private JsonSerializerOptions.OptionsBoundJsonTypeInfoResolverChain _typeInfoResolverChain;

		// Token: 0x040001C7 RID: 455
		private bool? _canUseFastPathSerializationLogic;

		// Token: 0x040001C8 RID: 456
		internal ReferenceHandlingStrategy ReferenceHandlingStrategy;

		// Token: 0x040001C9 RID: 457
		private volatile bool _isReadOnly;

		// Token: 0x040001CA RID: 458
		private volatile bool _isConfiguredForJsonSerializer;

		// Token: 0x040001CB RID: 459
		private IJsonTypeInfoResolver _effectiveJsonTypeInfoResolver;

		// Token: 0x02000121 RID: 289
		internal sealed class CachingContext
		{
			// Token: 0x06000D95 RID: 3477 RVA: 0x00034977 File Offset: 0x00032B77
			public CachingContext(JsonSerializerOptions options, int hashCode)
			{
				this.Options = options;
				this.HashCode = hashCode;
				this._cacheEntryFactory = (Type type) => JsonSerializerOptions.CachingContext.CreateCacheEntry(type, this);
			}

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000349AA File Offset: 0x00032BAA
			public JsonSerializerOptions Options { get; }

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000D97 RID: 3479 RVA: 0x000349B2 File Offset: 0x00032BB2
			public int HashCode { get; }

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000349BA File Offset: 0x00032BBA
			public int Count
			{
				get
				{
					return this._cache.Count;
				}
			}

			// Token: 0x06000D99 RID: 3481 RVA: 0x000349C8 File Offset: 0x00032BC8
			public JsonTypeInfo GetOrAddTypeInfo(Type type, bool fallBackToNearestAncestorType = false)
			{
				JsonSerializerOptions.CachingContext.CacheEntry orAddCacheEntry = this.GetOrAddCacheEntry(type);
				if (!fallBackToNearestAncestorType || orAddCacheEntry.HasResult)
				{
					return orAddCacheEntry.GetResult();
				}
				return this.FallBackToNearestAncestor(type, orAddCacheEntry);
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x000349F8 File Offset: 0x00032BF8
			public bool TryGetTypeInfo(Type type, [NotNullWhen(true)] out JsonTypeInfo typeInfo)
			{
				JsonSerializerOptions.CachingContext.CacheEntry cacheEntry;
				this._cache.TryGetValue(type, out cacheEntry);
				typeInfo = ((cacheEntry != null) ? cacheEntry.TypeInfo : null);
				return typeInfo != null;
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x00034A2A File Offset: 0x00032C2A
			public void Clear()
			{
				this._cache.Clear();
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x00034A37 File Offset: 0x00032C37
			private JsonSerializerOptions.CachingContext.CacheEntry GetOrAddCacheEntry(Type type)
			{
				return this._cache.GetOrAdd(type, this._cacheEntryFactory);
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x00034A4C File Offset: 0x00032C4C
			private static JsonSerializerOptions.CachingContext.CacheEntry CreateCacheEntry(Type type, JsonSerializerOptions.CachingContext context)
			{
				JsonSerializerOptions.CachingContext.CacheEntry cacheEntry;
				try
				{
					JsonTypeInfo typeInfoNoCaching = context.Options.GetTypeInfoNoCaching(type);
					cacheEntry = new JsonSerializerOptions.CachingContext.CacheEntry(typeInfoNoCaching);
				}
				catch (Exception ex)
				{
					ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
					cacheEntry = new JsonSerializerOptions.CachingContext.CacheEntry(exceptionDispatchInfo);
				}
				return cacheEntry;
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x00034A94 File Offset: 0x00032C94
			private JsonTypeInfo FallBackToNearestAncestor(Type type, JsonSerializerOptions.CachingContext.CacheEntry entry)
			{
				JsonSerializerOptions.CachingContext.CacheEntry cacheEntry = (entry.IsNearestAncestorResolved ? entry.NearestAncestor : this.DetermineNearestAncestor(type, entry));
				if (cacheEntry == null)
				{
					return null;
				}
				return cacheEntry.GetResult();
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x00034AC8 File Offset: 0x00032CC8
			[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "We only need to examine the interface types that are supported by the underlying resolver.")]
			private JsonSerializerOptions.CachingContext.CacheEntry DetermineNearestAncestor(Type type, JsonSerializerOptions.CachingContext.CacheEntry entry)
			{
				JsonSerializerOptions.CachingContext.CacheEntry cacheEntry = null;
				Type type2 = null;
				Type type3 = type.BaseType;
				while (type3 != null && !(type3 == JsonTypeInfo.ObjectType))
				{
					cacheEntry = this.GetOrAddCacheEntry(type3);
					if (cacheEntry.HasResult)
					{
						type2 = type3;
						break;
					}
					type3 = type3.BaseType;
				}
				foreach (Type type4 in type.GetInterfaces())
				{
					JsonSerializerOptions.CachingContext.CacheEntry orAddCacheEntry = this.GetOrAddCacheEntry(type4);
					if (orAddCacheEntry.HasResult)
					{
						if (type2 != null)
						{
							if (type4.IsAssignableFrom(type2))
							{
								goto IL_00A0;
							}
							if (!type2.IsAssignableFrom(type4))
							{
								NotSupportedException notSupportedException_AmbiguousMetadataForType = ThrowHelper.GetNotSupportedException_AmbiguousMetadataForType(type, type2, type4);
								cacheEntry = new JsonSerializerOptions.CachingContext.CacheEntry(ExceptionDispatchInfo.Capture(notSupportedException_AmbiguousMetadataForType));
								break;
							}
						}
						cacheEntry = orAddCacheEntry;
						type2 = type4;
					}
					IL_00A0:;
				}
				entry.NearestAncestor = cacheEntry;
				entry.IsNearestAncestorResolved = true;
				return cacheEntry;
			}

			// Token: 0x0400048C RID: 1164
			private readonly ConcurrentDictionary<Type, JsonSerializerOptions.CachingContext.CacheEntry> _cache = new ConcurrentDictionary<Type, JsonSerializerOptions.CachingContext.CacheEntry>();

			// Token: 0x0400048D RID: 1165
			private readonly Func<Type, JsonSerializerOptions.CachingContext.CacheEntry> _cacheEntryFactory;

			// Token: 0x02000174 RID: 372
			private sealed class CacheEntry
			{
				// Token: 0x06000E7F RID: 3711 RVA: 0x000377E1 File Offset: 0x000359E1
				public CacheEntry(JsonTypeInfo typeInfo)
				{
					this.TypeInfo = typeInfo;
					this.HasResult = typeInfo != null;
				}

				// Token: 0x06000E80 RID: 3712 RVA: 0x000377FA File Offset: 0x000359FA
				public CacheEntry(ExceptionDispatchInfo exception)
				{
					this.ExceptionDispatchInfo = exception;
					this.HasResult = true;
				}

				// Token: 0x06000E81 RID: 3713 RVA: 0x00037810 File Offset: 0x00035A10
				public JsonTypeInfo GetResult()
				{
					ExceptionDispatchInfo exceptionDispatchInfo = this.ExceptionDispatchInfo;
					if (exceptionDispatchInfo != null)
					{
						exceptionDispatchInfo.Throw();
					}
					return this.TypeInfo;
				}

				// Token: 0x04000567 RID: 1383
				public readonly bool HasResult;

				// Token: 0x04000568 RID: 1384
				public readonly JsonTypeInfo TypeInfo;

				// Token: 0x04000569 RID: 1385
				public readonly ExceptionDispatchInfo ExceptionDispatchInfo;

				// Token: 0x0400056A RID: 1386
				public volatile bool IsNearestAncestorResolved;

				// Token: 0x0400056B RID: 1387
				public JsonSerializerOptions.CachingContext.CacheEntry NearestAncestor;
			}
		}

		// Token: 0x02000122 RID: 290
		internal static class TrackedCachingContexts
		{
			// Token: 0x06000DA1 RID: 3489 RVA: 0x00034B9C File Offset: 0x00032D9C
			public static JsonSerializerOptions.CachingContext GetOrCreate(JsonSerializerOptions options)
			{
				int hashCode = JsonSerializerOptions.TrackedCachingContexts.s_optionsComparer.GetHashCode(options);
				int num;
				JsonSerializerOptions.CachingContext cachingContext;
				if (JsonSerializerOptions.TrackedCachingContexts.TryGetContext(options, hashCode, out num, out cachingContext))
				{
					return cachingContext;
				}
				if (num < 0)
				{
					return new JsonSerializerOptions.CachingContext(options, hashCode);
				}
				WeakReference<JsonSerializerOptions.CachingContext>[] array = JsonSerializerOptions.TrackedCachingContexts.s_trackedContexts;
				JsonSerializerOptions.CachingContext cachingContext2;
				lock (array)
				{
					if (JsonSerializerOptions.TrackedCachingContexts.TryGetContext(options, hashCode, out num, out cachingContext))
					{
						cachingContext2 = cachingContext;
					}
					else
					{
						JsonSerializerOptions.CachingContext cachingContext3 = new JsonSerializerOptions.CachingContext(options, hashCode);
						if (num >= 0)
						{
							ref WeakReference<JsonSerializerOptions.CachingContext> ptr = ref JsonSerializerOptions.TrackedCachingContexts.s_trackedContexts[num];
							if (ptr == null)
							{
								ptr = new WeakReference<JsonSerializerOptions.CachingContext>(cachingContext3);
							}
							else
							{
								ptr.SetTarget(cachingContext3);
							}
						}
						cachingContext2 = cachingContext3;
					}
				}
				return cachingContext2;
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x00034C4C File Offset: 0x00032E4C
			private static bool TryGetContext(JsonSerializerOptions options, int hashCode, out int firstUnpopulatedIndex, [NotNullWhen(true)] out JsonSerializerOptions.CachingContext result)
			{
				WeakReference<JsonSerializerOptions.CachingContext>[] array = JsonSerializerOptions.TrackedCachingContexts.s_trackedContexts;
				firstUnpopulatedIndex = -1;
				for (int i = 0; i < array.Length; i++)
				{
					WeakReference<JsonSerializerOptions.CachingContext> weakReference = array[i];
					JsonSerializerOptions.CachingContext cachingContext;
					if (weakReference == null || !weakReference.TryGetTarget(out cachingContext))
					{
						if (firstUnpopulatedIndex < 0)
						{
							firstUnpopulatedIndex = i;
						}
					}
					else if (hashCode == cachingContext.HashCode && JsonSerializerOptions.TrackedCachingContexts.s_optionsComparer.Equals(options, cachingContext.Options))
					{
						result = cachingContext;
						return true;
					}
				}
				result = null;
				return false;
			}

			// Token: 0x04000490 RID: 1168
			private const int MaxTrackedContexts = 64;

			// Token: 0x04000491 RID: 1169
			private static readonly WeakReference<JsonSerializerOptions.CachingContext>[] s_trackedContexts = new WeakReference<JsonSerializerOptions.CachingContext>[64];

			// Token: 0x04000492 RID: 1170
			private static readonly JsonSerializerOptions.EqualityComparer s_optionsComparer = new JsonSerializerOptions.EqualityComparer();
		}

		// Token: 0x02000123 RID: 291
		private sealed class EqualityComparer : IEqualityComparer<JsonSerializerOptions>
		{
			// Token: 0x06000DA4 RID: 3492 RVA: 0x00034CC8 File Offset: 0x00032EC8
			public bool Equals(JsonSerializerOptions left, JsonSerializerOptions right)
			{
				return left._dictionaryKeyPolicy == right._dictionaryKeyPolicy && left._jsonPropertyNamingPolicy == right._jsonPropertyNamingPolicy && left._readCommentHandling == right._readCommentHandling && left._referenceHandler == right._referenceHandler && left._encoder == right._encoder && left._defaultIgnoreCondition == right._defaultIgnoreCondition && left._numberHandling == right._numberHandling && left._preferredObjectCreationHandling == right._preferredObjectCreationHandling && left._unknownTypeHandling == right._unknownTypeHandling && left._unmappedMemberHandling == right._unmappedMemberHandling && left._defaultBufferSize == right._defaultBufferSize && left._maxDepth == right._maxDepth && left._allowTrailingCommas == right._allowTrailingCommas && left._ignoreNullValues == right._ignoreNullValues && left._ignoreReadOnlyProperties == right._ignoreReadOnlyProperties && left._ignoreReadonlyFields == right._ignoreReadonlyFields && left._includeFields == right._includeFields && left._propertyNameCaseInsensitive == right._propertyNameCaseInsensitive && left._writeIndented == right._writeIndented && left._typeInfoResolver == right._typeInfoResolver && JsonSerializerOptions.EqualityComparer.<Equals>g__CompareLists|0_0<JsonConverter>(left._converters, right._converters);
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x00034E24 File Offset: 0x00033024
			public int GetHashCode(JsonSerializerOptions options)
			{
				JsonSerializerOptions.EqualityComparer.HashCode hashCode = default(JsonSerializerOptions.EqualityComparer.HashCode);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonNamingPolicy>(ref hashCode, options._dictionaryKeyPolicy);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonNamingPolicy>(ref hashCode, options._jsonPropertyNamingPolicy);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonCommentHandling>(ref hashCode, options._readCommentHandling);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<ReferenceHandler>(ref hashCode, options._referenceHandler);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JavaScriptEncoder>(ref hashCode, options._encoder);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonIgnoreCondition>(ref hashCode, options._defaultIgnoreCondition);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonNumberHandling>(ref hashCode, options._numberHandling);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonObjectCreationHandling>(ref hashCode, options._preferredObjectCreationHandling);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonUnknownTypeHandling>(ref hashCode, options._unknownTypeHandling);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<JsonUnmappedMemberHandling>(ref hashCode, options._unmappedMemberHandling);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<int>(ref hashCode, options._defaultBufferSize);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<int>(ref hashCode, options._maxDepth);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._allowTrailingCommas);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._ignoreNullValues);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._ignoreReadOnlyProperties);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._ignoreReadonlyFields);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._includeFields);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._propertyNameCaseInsensitive);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<bool>(ref hashCode, options._writeIndented);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<IJsonTypeInfoResolver>(ref hashCode, options._typeInfoResolver);
				JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddListHashCode|1_0<JsonConverter>(ref hashCode, options._converters);
				return hashCode.ToHashCode();
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x00034F5C File Offset: 0x0003315C
			[CompilerGenerated]
			internal static bool <Equals>g__CompareLists|0_0<TValue>(ConfigurationList<TValue> left, ConfigurationList<TValue> right) where TValue : class
			{
				if (left == null)
				{
					return right == null || right.Count == 0;
				}
				if (right == null)
				{
					return left.Count == 0;
				}
				int count;
				if ((count = left.Count) != right.Count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (left[i] != right[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x00034FC4 File Offset: 0x000331C4
			[CompilerGenerated]
			internal static void <GetHashCode>g__AddListHashCode|1_0<TValue>(ref JsonSerializerOptions.EqualityComparer.HashCode hc, ConfigurationList<TValue> list)
			{
				if (list == null)
				{
					return;
				}
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					JsonSerializerOptions.EqualityComparer.<GetHashCode>g__AddHashCode|1_1<TValue>(ref hc, list[i]);
				}
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x00034FF5 File Offset: 0x000331F5
			[CompilerGenerated]
			internal static void <GetHashCode>g__AddHashCode|1_1<TValue>(ref JsonSerializerOptions.EqualityComparer.HashCode hc, TValue value)
			{
				if (typeof(TValue).IsValueType)
				{
					hc.Add<TValue>(value);
					return;
				}
				hc.Add<int>(RuntimeHelpers.GetHashCode(value));
			}

			// Token: 0x02000175 RID: 373
			private struct HashCode
			{
				// Token: 0x06000E82 RID: 3714 RVA: 0x0003782C File Offset: 0x00035A2C
				public void Add<T>(T value)
				{
					this._hashCode = new global::System.ValueTuple<int, T>(this._hashCode, value).GetHashCode();
				}

				// Token: 0x06000E83 RID: 3715 RVA: 0x00037859 File Offset: 0x00035A59
				public int ToHashCode()
				{
					return this._hashCode;
				}

				// Token: 0x0400056C RID: 1388
				private int _hashCode;
			}
		}

		// Token: 0x02000124 RID: 292
		internal static class TrackedOptionsInstances
		{
			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00035021 File Offset: 0x00033221
			public static ConditionalWeakTable<JsonSerializerOptions, object> All { get; } = new ConditionalWeakTable<JsonSerializerOptions, object>();
		}

		// Token: 0x02000125 RID: 293
		private sealed class ConverterList : ConfigurationList<JsonConverter>
		{
			// Token: 0x06000DAC RID: 3500 RVA: 0x00035034 File Offset: 0x00033234
			public ConverterList(JsonSerializerOptions options, IList<JsonConverter> source = null)
				: base(source)
			{
				this._options = options;
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00035044 File Offset: 0x00033244
			public override bool IsReadOnly
			{
				get
				{
					return this._options.IsReadOnly;
				}
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x00035051 File Offset: 0x00033251
			protected override void OnCollectionModifying()
			{
				this._options.VerifyMutable();
			}

			// Token: 0x04000494 RID: 1172
			private readonly JsonSerializerOptions _options;
		}

		// Token: 0x02000126 RID: 294
		private sealed class OptionsBoundJsonTypeInfoResolverChain : JsonTypeInfoResolverChain
		{
			// Token: 0x06000DAF RID: 3503 RVA: 0x0003505E File Offset: 0x0003325E
			public OptionsBoundJsonTypeInfoResolverChain(JsonSerializerOptions options)
			{
				this._options = options;
				base.AddFlattened(options._typeInfoResolver);
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00035079 File Offset: 0x00033279
			public override bool IsReadOnly
			{
				get
				{
					return this._options.IsReadOnly;
				}
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x00035086 File Offset: 0x00033286
			protected override void ValidateAddedValue(IJsonTypeInfoResolver item)
			{
				if (item == this || item == this._options._typeInfoResolver)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidChainedResolver();
				}
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x0003509F File Offset: 0x0003329F
			protected override void OnCollectionModifying()
			{
				this._options.VerifyMutable();
				this._options._typeInfoResolver = this;
			}

			// Token: 0x04000495 RID: 1173
			private readonly JsonSerializerOptions _options;
		}
	}
}
