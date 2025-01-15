﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Microsoft.IdentityModel.Json.Serialization;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x0200002D RID: 45
	[NullableContext(2)]
	[Nullable(0)]
	internal class JsonSerializerSettings
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005D51 File Offset: 0x00003F51
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005D5E File Offset: 0x00003F5E
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
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005D6C File Offset: 0x00003F6C
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005D79 File Offset: 0x00003F79
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
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005D87 File Offset: 0x00003F87
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005D94 File Offset: 0x00003F94
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
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005DA2 File Offset: 0x00003FA2
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005DAF File Offset: 0x00003FAF
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
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005DBD File Offset: 0x00003FBD
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00005DCA File Offset: 0x00003FCA
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
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005DD8 File Offset: 0x00003FD8
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00005DE0 File Offset: 0x00003FE0
		[Nullable(1)]
		public IList<JsonConverter> Converters
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005DE9 File Offset: 0x00003FE9
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00005DF6 File Offset: 0x00003FF6
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
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005E04 File Offset: 0x00004004
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005E11 File Offset: 0x00004011
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
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005E1F File Offset: 0x0000401F
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005E2C File Offset: 0x0000402C
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
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005E3A File Offset: 0x0000403A
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005E42 File Offset: 0x00004042
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
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005E4B File Offset: 0x0000404B
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005E58 File Offset: 0x00004058
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
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005E66 File Offset: 0x00004066
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005E73 File Offset: 0x00004073
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
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005E81 File Offset: 0x00004081
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005E89 File Offset: 0x00004089
		public IContractResolver ContractResolver { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005E92 File Offset: 0x00004092
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005E9A File Offset: 0x0000409A
		public IEqualityComparer EqualityComparer { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005EA3 File Offset: 0x000040A3
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00005EB8 File Offset: 0x000040B8
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
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005EEF File Offset: 0x000040EF
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00005EF7 File Offset: 0x000040F7
		public Func<IReferenceResolver> ReferenceResolverProvider { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005F00 File Offset: 0x00004100
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00005F08 File Offset: 0x00004108
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00005F14 File Offset: 0x00004114
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00005F4B File Offset: 0x0000414B
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
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005F5F File Offset: 0x0000415F
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00005F67 File Offset: 0x00004167
		public ISerializationBinder SerializationBinder { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005F70 File Offset: 0x00004170
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00005F78 File Offset: 0x00004178
		[Nullable(new byte[] { 2, 1 })]
		public EventHandler<ErrorEventArgs> Error
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005F84 File Offset: 0x00004184
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00005FAE File Offset: 0x000041AE
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
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005FBC File Offset: 0x000041BC
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00005FCD File Offset: 0x000041CD
		[Nullable(1)]
		public string DateFormatString
		{
			[NullableContext(1)]
			get
			{
				return this._dateFormatString ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
			}
			[NullableContext(1)]
			set
			{
				this._dateFormatString = value;
				this._dateFormatStringSet = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005FDD File Offset: 0x000041DD
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00005FF8 File Offset: 0x000041F8
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
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000603E File Offset: 0x0000423E
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000604B File Offset: 0x0000424B
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
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006059 File Offset: 0x00004259
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00006066 File Offset: 0x00004266
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
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006074 File Offset: 0x00004274
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000609A File Offset: 0x0000429A
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				DateTimeZoneHandling? dateTimeZoneHandling = this._dateTimeZoneHandling;
				if (dateTimeZoneHandling == null)
				{
					return DateTimeZoneHandling.RoundtripKind;
				}
				return dateTimeZoneHandling.GetValueOrDefault();
			}
			set
			{
				this._dateTimeZoneHandling = new DateTimeZoneHandling?(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000060A8 File Offset: 0x000042A8
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000060CE File Offset: 0x000042CE
		public DateParseHandling DateParseHandling
		{
			get
			{
				DateParseHandling? dateParseHandling = this._dateParseHandling;
				if (dateParseHandling == null)
				{
					return DateParseHandling.DateTime;
				}
				return dateParseHandling.GetValueOrDefault();
			}
			set
			{
				this._dateParseHandling = new DateParseHandling?(value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000060DC File Offset: 0x000042DC
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000060E9 File Offset: 0x000042E9
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
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000060F7 File Offset: 0x000042F7
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006104 File Offset: 0x00004304
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
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006112 File Offset: 0x00004312
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000611F File Offset: 0x0000431F
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
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000612D File Offset: 0x0000432D
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000613E File Offset: 0x0000433E
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? JsonSerializerSettings.DefaultCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006147 File Offset: 0x00004347
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00006154 File Offset: 0x00004354
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

		// Token: 0x060001D9 RID: 473 RVA: 0x00006179 File Offset: 0x00004379
		[DebuggerStepThrough]
		public JsonSerializerSettings()
		{
			this.Converters = new List<JsonConverter>();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000618C File Offset: 0x0000438C
		[NullableContext(1)]
		public JsonSerializerSettings(JsonSerializerSettings original)
		{
			this._floatParseHandling = original._floatParseHandling;
			this._floatFormatHandling = original._floatFormatHandling;
			this._dateParseHandling = original._dateParseHandling;
			this._dateTimeZoneHandling = original._dateTimeZoneHandling;
			this._dateFormatHandling = original._dateFormatHandling;
			this._formatting = original._formatting;
			this._maxDepth = original._maxDepth;
			this._maxDepthSet = original._maxDepthSet;
			this._dateFormatString = original._dateFormatString;
			this._dateFormatStringSet = original._dateFormatStringSet;
			this._context = original._context;
			this.Error = original.Error;
			this.SerializationBinder = original.SerializationBinder;
			this.TraceWriter = original.TraceWriter;
			this._culture = original._culture;
			this.ReferenceResolverProvider = original.ReferenceResolverProvider;
			this.EqualityComparer = original.EqualityComparer;
			this.ContractResolver = original.ContractResolver;
			this._constructorHandling = original._constructorHandling;
			this._typeNameAssemblyFormatHandling = original._typeNameAssemblyFormatHandling;
			this._metadataPropertyHandling = original._metadataPropertyHandling;
			this._typeNameHandling = original._typeNameHandling;
			this._preserveReferencesHandling = original._preserveReferencesHandling;
			this.Converters = original.Converters.ToList<JsonConverter>();
			this._defaultValueHandling = original._defaultValueHandling;
			this._nullValueHandling = original._nullValueHandling;
			this._objectCreationHandling = original._objectCreationHandling;
			this._missingMemberHandling = original._missingMemberHandling;
			this._referenceLoopHandling = original._referenceLoopHandling;
			this._checkAdditionalContent = original._checkAdditionalContent;
			this._stringEscapeHandling = original._stringEscapeHandling;
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
		[Nullable(1)]
		internal static readonly CultureInfo DefaultCulture = CultureInfo.InvariantCulture;

		// Token: 0x040000A6 RID: 166
		internal const bool DefaultCheckAdditionalContent = false;

		// Token: 0x040000A7 RID: 167
		[Nullable(1)]
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
