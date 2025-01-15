using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Microsoft.Identity.Json.Serialization;

namespace Microsoft.Identity.Json
{
	// Token: 0x0200002D RID: 45
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonSerializerSettings
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005D0D File Offset: 0x00003F0D
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005D1A File Offset: 0x00003F1A
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005D28 File Offset: 0x00003F28
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005D35 File Offset: 0x00003F35
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005D43 File Offset: 0x00003F43
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005D50 File Offset: 0x00003F50
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005D5E File Offset: 0x00003F5E
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005D6B File Offset: 0x00003F6B
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005D79 File Offset: 0x00003F79
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00005D86 File Offset: 0x00003F86
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005D94 File Offset: 0x00003F94
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00005D9C File Offset: 0x00003F9C
		[Nullable(0)]
		public IList<JsonConverter> Converters
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005DA5 File Offset: 0x00003FA5
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00005DB2 File Offset: 0x00003FB2
		public PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._preserveReferencesHandling.GetValueOrDefault();
			}
			set
			{
				this._preserveReferencesHandling = new PreserveReferencesHandling?(value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005DC0 File Offset: 0x00003FC0
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005DCD File Offset: 0x00003FCD
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005DDB File Offset: 0x00003FDB
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._metadataPropertyHandling.GetValueOrDefault();
			}
			set
			{
				this._metadataPropertyHandling = new MetadataPropertyHandling?(value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005DF6 File Offset: 0x00003FF6
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005DFE File Offset: 0x00003FFE
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return (FormatterAssemblyStyle)this.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this.TypeNameAssemblyFormatHandling = (TypeNameAssemblyFormatHandling)value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005E07 File Offset: 0x00004007
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005E14 File Offset: 0x00004014
		public TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._typeNameAssemblyFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameAssemblyFormatHandling = new TypeNameAssemblyFormatHandling?(value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005E22 File Offset: 0x00004022
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005E2F File Offset: 0x0000402F
		public ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._constructorHandling.GetValueOrDefault();
			}
			set
			{
				this._constructorHandling = new ConstructorHandling?(value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005E3D File Offset: 0x0000403D
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005E45 File Offset: 0x00004045
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005E4E File Offset: 0x0000404E
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005E56 File Offset: 0x00004056
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005E5F File Offset: 0x0000405F
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00005E74 File Offset: 0x00004074
		[Obsolete("ReferenceResolver property is obsolete. Use the ReferenceResolverProvider property to set the IReferenceResolver: settings.ReferenceResolverProvider = () => resolver")]
		public IReferenceResolver ReferenceResolver
		{
			get
			{
				Func<IReferenceResolver> referenceResolverProvider = this.ReferenceResolverProvider;
				if (referenceResolverProvider == null)
				{
					return null;
				}
				return referenceResolverProvider();
			}
			set
			{
				this.ReferenceResolverProvider = ((value != null) ? (() => value) : null);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005EAB File Offset: 0x000040AB
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00005EB3 File Offset: 0x000040B3
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005EBC File Offset: 0x000040BC
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00005EC4 File Offset: 0x000040C4
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00005ED0 File Offset: 0x000040D0
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00005F07 File Offset: 0x00004107
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public SerializationBinder Binder
		{
			get
			{
				if (this.SerializationBinder == null)
				{
					return null;
				}
				SerializationBinderAdapter serializationBinderAdapter = this.SerializationBinder as SerializationBinderAdapter;
				if (serializationBinderAdapter != null)
				{
					return serializationBinderAdapter.SerializationBinder;
				}
				throw new InvalidOperationException("Cannot get SerializationBinder because an ISerializationBinder was previously set.");
			}
			set
			{
				this.SerializationBinder = ((value == null) ? null : new SerializationBinderAdapter(value));
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005F1B File Offset: 0x0000411B
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00005F23 File Offset: 0x00004123
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005F2C File Offset: 0x0000412C
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00005F34 File Offset: 0x00004134
		[Nullable(new byte[] { 2, 0 })]
		public EventHandler<ErrorEventArgs> Error
		{
			[return: Nullable(new byte[] { 2, 0 })]
			get;
			[param: Nullable(new byte[] { 2, 0 })]
			set;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005F40 File Offset: 0x00004140
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00005F6A File Offset: 0x0000416A
		public StreamingContext Context
		{
			get
			{
				StreamingContext? context = this._context;
				if (context == null)
				{
					return JsonSerializerSettings.DefaultContext;
				}
				return context.GetValueOrDefault();
			}
			set
			{
				this._context = new StreamingContext?(value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005F78 File Offset: 0x00004178
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00005F89 File Offset: 0x00004189
		[Nullable(0)]
		public string DateFormatString
		{
			[NullableContext(0)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(0)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005F99 File Offset: 0x00004199
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00005FB4 File Offset: 0x000041B4
		public int? MaxDepth
		{
			get
			{
				if (!this._maxDepthSet)
				{
					return new int?(64);
				}
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if ((num.GetValueOrDefault() <= num2) & (num != null))
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
				this._maxDepthSet = true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005FFA File Offset: 0x000041FA
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006007 File Offset: 0x00004207
		public Formatting Formatting
		{
			get
			{
				return this._formatting.GetValueOrDefault();
			}
			set
			{
				this._formatting = new Formatting?(value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006015 File Offset: 0x00004215
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006022 File Offset: 0x00004222
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._dateFormatHandling = new DateFormatHandling?(value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006030 File Offset: 0x00004230
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000603E File Offset: 0x0000423E
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling.GetValueOrDefault(DateTimeZoneHandling.RoundtripKind);
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000604C File Offset: 0x0000424C
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000605A File Offset: 0x0000425A
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling.GetValueOrDefault(DateParseHandling.DateTime);
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006068 File Offset: 0x00004268
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006075 File Offset: 0x00004275
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling.GetValueOrDefault();
			}
			set
			{
				this._floatFormatHandling = new FloatFormatHandling?(value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006083 File Offset: 0x00004283
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006090 File Offset: 0x00004290
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling.GetValueOrDefault();
			}
			set
			{
				this._floatParseHandling = new FloatParseHandling?(value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000609E File Offset: 0x0000429E
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000060AB File Offset: 0x000042AB
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling.GetValueOrDefault();
			}
			set
			{
				this._stringEscapeHandling = new StringEscapeHandling?(value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000060B9 File Offset: 0x000042B9
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000060CA File Offset: 0x000042CA
		[Nullable(0)]
		public CultureInfo Culture
		{
			[NullableContext(0)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(0)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000060D3 File Offset: 0x000042D3
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000060E0 File Offset: 0x000042E0
		public bool CheckAdditionalContent
		{
			get
			{
				return this._checkAdditionalContent.GetValueOrDefault();
			}
			set
			{
				this._checkAdditionalContent = new bool?(value);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006105 File Offset: 0x00004305
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x04000093 RID: 147
		internal const ReferenceLoopHandling DefaultReferenceLoopHandling = ReferenceLoopHandling.Error;

		// Token: 0x04000094 RID: 148
		internal const MissingMemberHandling DefaultMissingMemberHandling = MissingMemberHandling.Ignore;

		// Token: 0x04000095 RID: 149
		internal const NullValueHandling DefaultNullValueHandling = NullValueHandling.Include;

		// Token: 0x04000096 RID: 150
		internal const DefaultValueHandling DefaultDefaultValueHandling = DefaultValueHandling.Include;

		// Token: 0x04000097 RID: 151
		internal const ObjectCreationHandling DefaultObjectCreationHandling = ObjectCreationHandling.Auto;

		// Token: 0x04000098 RID: 152
		internal const PreserveReferencesHandling DefaultPreserveReferencesHandling = PreserveReferencesHandling.None;

		// Token: 0x04000099 RID: 153
		internal const ConstructorHandling DefaultConstructorHandling = ConstructorHandling.Default;

		// Token: 0x0400009A RID: 154
		internal const TypeNameHandling DefaultTypeNameHandling = TypeNameHandling.None;

		// Token: 0x0400009B RID: 155
		internal const MetadataPropertyHandling DefaultMetadataPropertyHandling = MetadataPropertyHandling.Default;

		// Token: 0x0400009C RID: 156
		internal static readonly StreamingContext DefaultContext = default(StreamingContext);

		// Token: 0x0400009D RID: 157
		internal const Formatting DefaultFormatting = Formatting.None;

		// Token: 0x0400009E RID: 158
		internal const DateFormatHandling DefaultDateFormatHandling = DateFormatHandling.IsoDateFormat;

		// Token: 0x0400009F RID: 159
		internal const DateTimeZoneHandling DefaultDateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;

		// Token: 0x040000A0 RID: 160
		internal const DateParseHandling DefaultDateParseHandling = DateParseHandling.DateTime;

		// Token: 0x040000A1 RID: 161
		internal const FloatParseHandling DefaultFloatParseHandling = FloatParseHandling.Double;

		// Token: 0x040000A2 RID: 162
		internal const FloatFormatHandling DefaultFloatFormatHandling = FloatFormatHandling.String;

		// Token: 0x040000A3 RID: 163
		internal const StringEscapeHandling DefaultStringEscapeHandling = StringEscapeHandling.Default;

		// Token: 0x040000A4 RID: 164
		internal const TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		// Token: 0x040000A5 RID: 165
		[Nullable(0)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x040000A6 RID: 166
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x040000A7 RID: 167
		[Nullable(0)]
		internal const string DefaultDateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x040000A8 RID: 168
		internal const int DefaultMaxDepth = 64;

		// Token: 0x040000A9 RID: 169
		internal Formatting? _formatting;

		// Token: 0x040000AA RID: 170
		internal DateFormatHandling? _dateFormatHandling;

		// Token: 0x040000AB RID: 171
		internal DateTimeZoneHandling? _dateTimeZoneHandling;

		// Token: 0x040000AC RID: 172
		internal DateParseHandling? _dateParseHandling;

		// Token: 0x040000AD RID: 173
		internal FloatFormatHandling? _floatFormatHandling;

		// Token: 0x040000AE RID: 174
		internal FloatParseHandling? _floatParseHandling;

		// Token: 0x040000AF RID: 175
		internal StringEscapeHandling? _stringEscapeHandling;

		// Token: 0x040000B0 RID: 176
		internal CultureInfo _culture;

		// Token: 0x040000B1 RID: 177
		internal bool? _checkAdditionalContent;

		// Token: 0x040000B2 RID: 178
		internal int? _maxDepth;

		// Token: 0x040000B3 RID: 179
		internal bool _maxDepthSet;

		// Token: 0x040000B4 RID: 180
		internal string _dateFormatString;

		// Token: 0x040000B5 RID: 181
		internal bool _dateFormatStringSet;

		// Token: 0x040000B6 RID: 182
		internal TypeNameAssemblyFormatHandling? _typeNameAssemblyFormatHandling;

		// Token: 0x040000B7 RID: 183
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040000B8 RID: 184
		internal PreserveReferencesHandling? _preserveReferencesHandling;

		// Token: 0x040000B9 RID: 185
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040000BA RID: 186
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040000BB RID: 187
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x040000BC RID: 188
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040000BD RID: 189
		internal StreamingContext? _context;

		// Token: 0x040000BE RID: 190
		internal ConstructorHandling? _constructorHandling;

		// Token: 0x040000BF RID: 191
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040000C0 RID: 192
		internal MetadataPropertyHandling? _metadataPropertyHandling;
	}
}
