using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000764 RID: 1892
	internal class ODataUserSettings
	{
		// Token: 0x060037B0 RID: 14256 RVA: 0x000B23E0 File Offset: 0x000B05E0
		public ODataUserSettings()
		{
			this.Headers = Value.Null;
			this.EnableBatch = false;
			this.MaxUriLength = ODataUserSettings.DefaultMaxUriLength;
			this.PageSize = 5000;
			this.UseConcurrentRequests = false;
			this.QueryOptions = Value.Null;
			this.ODataVersion = ODataServerVersion.All;
			this.ExcludedFromCacheKey = Value.Null;
			this.Timeout = ODataUserSettings.DefaultTimeout;
			this.MoreColumns = false;
			this.IncludeAnnotations = null;
			this.IncludeMetadataAnnotations = null;
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000B2460 File Offset: 0x000B0660
		public ODataUserSettings(ODataUserSettings settings)
		{
			this.Headers = settings.Headers;
			this.EnableBatch = settings.EnableBatch;
			this.MaxUriLength = settings.MaxUriLength;
			this.PageSize = settings.PageSize;
			this.UseConcurrentRequests = settings.UseConcurrentRequests;
			this.QueryOptions = settings.QueryOptions;
			this.ODataVersion = settings.ODataVersion;
			this.ExcludedFromCacheKey = settings.ExcludedFromCacheKey;
			this.Timeout = settings.Timeout;
			this.MoreColumns = settings.MoreColumns;
			this.IncludeAnnotations = settings.IncludeAnnotations;
			this.IncludeMetadataAnnotations = settings.IncludeMetadataAnnotations;
			this.ApiKeyName = settings.ApiKeyName;
			this.fingerprint = settings.fingerprint;
			this.FunctionOverloads = settings.FunctionOverloads;
			this.OAuthResource = settings.OAuthResource;
			this.UseODataLib7 = settings.UseODataLib7;
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000B253F File Offset: 0x000B073F
		// (set) Token: 0x060037B3 RID: 14259 RVA: 0x000B2547 File Offset: 0x000B0747
		public int MaxUriLength { get; set; }

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000B2550 File Offset: 0x000B0750
		// (set) Token: 0x060037B5 RID: 14261 RVA: 0x000B2558 File Offset: 0x000B0758
		public string ApiKeyName { get; set; }

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000B2561 File Offset: 0x000B0761
		// (set) Token: 0x060037B7 RID: 14263 RVA: 0x000B2569 File Offset: 0x000B0769
		public bool EnableBatch { get; set; }

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000B2572 File Offset: 0x000B0772
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000B257A File Offset: 0x000B077A
		public bool UseConcurrentRequests { get; set; }

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000B2583 File Offset: 0x000B0783
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000B258B File Offset: 0x000B078B
		public TimeSpan Timeout { get; set; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000B2594 File Offset: 0x000B0794
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x000B259C File Offset: 0x000B079C
		public Value QueryOptions { get; set; }

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000B25A5 File Offset: 0x000B07A5
		// (set) Token: 0x060037BF RID: 14271 RVA: 0x000B25AD File Offset: 0x000B07AD
		public ODataServerVersion ODataVersion { get; private set; }

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000B25B6 File Offset: 0x000B07B6
		// (set) Token: 0x060037C1 RID: 14273 RVA: 0x000B25BE File Offset: 0x000B07BE
		public bool UseODataLib7 { get; set; }

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000B25C7 File Offset: 0x000B07C7
		// (set) Token: 0x060037C3 RID: 14275 RVA: 0x000B25CF File Offset: 0x000B07CF
		public Value Headers { get; private set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000B25D8 File Offset: 0x000B07D8
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x000B25E0 File Offset: 0x000B07E0
		public Value ExcludedFromCacheKey { get; private set; }

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000B25E9 File Offset: 0x000B07E9
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000B25F1 File Offset: 0x000B07F1
		public bool? FunctionOverloads { get; private set; }

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000B25FA File Offset: 0x000B07FA
		// (set) Token: 0x060037C9 RID: 14281 RVA: 0x000B2602 File Offset: 0x000B0802
		public bool MoreColumns { get; private set; }

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000B260B File Offset: 0x000B080B
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000B2613 File Offset: 0x000B0813
		public string IncludeAnnotations { get; private set; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x000B261C File Offset: 0x000B081C
		// (set) Token: 0x060037CD RID: 14285 RVA: 0x000B2624 File Offset: 0x000B0824
		public string IncludeMetadataAnnotations { get; private set; }

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000B262D File Offset: 0x000B082D
		// (set) Token: 0x060037CF RID: 14287 RVA: 0x000B2635 File Offset: 0x000B0835
		public string OmitValues { get; private set; }

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000B263E File Offset: 0x000B083E
		// (set) Token: 0x060037D1 RID: 14289 RVA: 0x000B2646 File Offset: 0x000B0846
		public string OAuthResource { get; private set; }

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x060037D2 RID: 14290 RVA: 0x000B264F File Offset: 0x000B084F
		// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000B2657 File Offset: 0x000B0857
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				if (value == 0)
				{
					this.UseConcurrentRequests = false;
					return;
				}
				this.pageSize = value;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000B266B File Offset: 0x000B086B
		public string Fingerprint
		{
			get
			{
				return this.fingerprint;
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000B2674 File Offset: 0x000B0874
		public static ODataUserSettings BuildUserSettings(Value headers, Value options, bool privilegedMode = false)
		{
			ODataUserSettings odataUserSettings = new ODataUserSettings();
			odataUserSettings.Headers = headers;
			RecordValue recordValue = RecordValue.Empty;
			if (!headers.IsNull && headers.AsRecord.Count > 0)
			{
				recordValue = RecordValue.New(new NamedValue[]
				{
					new NamedValue("Headers", headers)
				});
			}
			if (options.IsRecord)
			{
				RecordValue asRecord = options.AsRecord;
				Options.ValidateOptions(asRecord, privilegedMode ? ODataUserSettings.validPrivilegedOptionKeys : ODataUserSettings.validOptionKeys, "OData.Feed", null);
				Value value;
				if (asRecord.TryGetValue("Headers", out value))
				{
					if (!headers.IsNull && headers.AsRecord.Count > 0 && value.AsRecord.Count > 0)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ODataTwoHeaderOptions, null, null);
					}
					odataUserSettings.Headers = value.AsRecord;
				}
				Value value2;
				if (asRecord.TryGetValue("ExcludedFromCacheKey", out value2))
				{
					odataUserSettings.ExcludedFromCacheKey = value2.AsList;
				}
				Value value3;
				if (asRecord.TryGetValue("ApiKeyName", out value3))
				{
					odataUserSettings.ApiKeyName = value3.AsText.String;
				}
				Value value4;
				if (asRecord.TryGetValue("Timeout", out value4))
				{
					odataUserSettings.Timeout = value4.AsDuration.AsClrTimeSpan;
				}
				Value value5;
				if (asRecord.TryGetValue("EnableBatch", out value5))
				{
					odataUserSettings.EnableBatch = value5.AsBoolean;
				}
				Value value6;
				if (asRecord.TryGetValue("MaxUriLength", out value6))
				{
					odataUserSettings.MaxUriLength = value6.AsInteger32;
				}
				Value value7;
				if (asRecord.TryGetValue("MoreColumns", out value7))
				{
					odataUserSettings.MoreColumns = value7.AsBoolean;
				}
				Value value8;
				if (asRecord.TryGetValue("Query", out value8))
				{
					odataUserSettings.QueryOptions = value8.AsRecord;
				}
				Value value9;
				if (asRecord.TryGetValue("ODataVersion", out value9))
				{
					int asInteger = value9.AsInteger32;
					if (asInteger != 3 && asInteger != 4)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.UnsupportedODataVersion(value9), null, null);
					}
					if (asInteger == 3)
					{
						odataUserSettings.ODataVersion = ODataServerVersion.V3;
					}
					else if (asInteger == 4)
					{
						odataUserSettings.ODataVersion = ODataServerVersion.V4;
					}
				}
				Value value10;
				if (asRecord.TryGetValue("Implementation", out value10) && !value10.IsNull)
				{
					if (!string.Equals(value10.AsString, ODataUserSettings.Implementation20Beta, StringComparison.OrdinalIgnoreCase) && !string.Equals(value10.AsString, ODataUserSettings.Implementation20, StringComparison.OrdinalIgnoreCase))
					{
						throw ValueException.NewExpressionError<Message1>(Strings.UnsupportedODataImplementation(value10.AsString), null, null);
					}
					odataUserSettings.UseODataLib7 = true;
				}
				Value value11;
				if (asRecord.TryGetValue("Concurrent", out value11) && value11.IsLogical)
				{
					odataUserSettings.UseConcurrentRequests = value11.AsBoolean;
				}
				Value value12;
				if (asRecord.TryGetValue("FunctionOverloads", out value12) && value12.IsLogical)
				{
					odataUserSettings.FunctionOverloads = new bool?(value12.AsBoolean);
					if (odataUserSettings.UseODataLib7)
					{
						throw ValueException.NewExpressionError<Message2>(Strings.ODataImplementationUnsupportedOption(value10.AsString, "FunctionOverloads"), null, null);
					}
				}
				Value value13;
				if (asRecord.TryGetValue("IncludeAnnotations", out value13) && value13.IsText)
				{
					odataUserSettings.IncludeAnnotations = value13.AsString;
					odataUserSettings.IncludeMetadataAnnotations = value13.AsString;
				}
				Value value14;
				if (asRecord.TryGetValue("IncludeMetadataAnnotations", out value14) && value14.IsText)
				{
					odataUserSettings.IncludeMetadataAnnotations = value14.AsString;
				}
				Value value15;
				if (asRecord.TryGetValue("OmitValues", out value15) && !value15.IsNull)
				{
					if (!odataUserSettings.UseODataLib7)
					{
						throw ValueException.NewExpressionError<Message2>(Strings.OptionOnlySupportedInImplementation("OmitValues", ODataUserSettings.Implementation20), null, null);
					}
					if (odataUserSettings.ODataVersion != ODataServerVersion.V4)
					{
						throw ValueException.NewExpressionError<Message2>(Strings.ODataOptionOnlySupportedInVersion("OmitValues", 4), null, null);
					}
					if (!value15.IsText || value15.AsString != "nulls")
					{
						throw ValueException.NewExpressionError<Message2>(Strings.InvalidValueForOption(value15, "OmitValues"), null, null);
					}
					odataUserSettings.OmitValues = value15.AsString;
				}
				Value value16;
				if (asRecord.TryGetValue("Resource", out value16) && value16.IsText)
				{
					odataUserSettings.OAuthResource = value16.AsString;
				}
				recordValue = recordValue.Concatenate(asRecord).AsRecord;
			}
			else if (options.IsLogical)
			{
				odataUserSettings.UseConcurrentRequests = options.AsBoolean;
				recordValue = recordValue.Concatenate(RecordValue.New(new NamedValue[]
				{
					new NamedValue("Concurrent", options)
				})).AsRecord;
			}
			else if (!options.IsNull)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.ODataUnknownOptionType, options, null);
			}
			ODataUserSettings.ValidateHeaderValuesAreValidTypes(odataUserSettings.Headers);
			odataUserSettings.fingerprint = recordValue.CreateCacheKey();
			return odataUserSettings;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000B2AB8 File Offset: 0x000B0CB8
		public RecordValue GetOptionsValue()
		{
			RecordBuilder recordBuilder = new RecordBuilder(ODataUserSettings.validOptionKeys.Count + ODataUserSettings.validPrivilegedOptionKeys.Count);
			if (!this.Headers.IsNull && this.Headers.AsRecord.Count > 0)
			{
				recordBuilder.Add("Headers", this.Headers, TypeValue.Any);
			}
			if (!this.ExcludedFromCacheKey.IsNull && this.ExcludedFromCacheKey.AsRecord.Count > 0)
			{
				recordBuilder.Add("ExcludedFromCacheKey", this.ExcludedFromCacheKey, TypeValue.Any);
			}
			if (this.ApiKeyName != null)
			{
				recordBuilder.Add("ApiKeyName", TextValue.New(this.ApiKeyName), TypeValue.Any);
			}
			if (this.Timeout != ODataUserSettings.DefaultTimeout)
			{
				recordBuilder.Add("Timeout", DurationValue.New(this.Timeout), TypeValue.Any);
			}
			if (this.EnableBatch)
			{
				recordBuilder.Add("EnableBatch", LogicalValue.New(this.EnableBatch), TypeValue.Any);
			}
			if (this.MaxUriLength != ODataUserSettings.DefaultMaxUriLength)
			{
				recordBuilder.Add("MaxUriLength", NumberValue.New(this.MaxUriLength), TypeValue.Any);
			}
			if (this.MoreColumns)
			{
				recordBuilder.Add("MoreColumns", LogicalValue.New(this.MoreColumns), TypeValue.Any);
			}
			if (!this.QueryOptions.IsNull && this.QueryOptions.AsRecord.Count > 0)
			{
				recordBuilder.Add("Query", this.QueryOptions, TypeValue.Any);
			}
			if (this.ODataVersion != ODataServerVersion.All)
			{
				ODataServerVersion odataVersion = this.ODataVersion;
				int num;
				if (odataVersion != ODataServerVersion.V3)
				{
					if (odataVersion != ODataServerVersion.V4)
					{
						throw new InvalidOperationException();
					}
					num = 4;
				}
				else
				{
					num = 3;
				}
				recordBuilder.Add("ODataVersion", NumberValue.New(num), TypeValue.Any);
			}
			if (this.UseODataLib7)
			{
				recordBuilder.Add("Implementation", TextValue.New(ODataUserSettings.Implementation20), TypeValue.Any);
			}
			if (this.UseConcurrentRequests)
			{
				recordBuilder.Add("Concurrent", LogicalValue.New(this.UseConcurrentRequests), TypeValue.Any);
			}
			if (this.FunctionOverloads != null)
			{
				recordBuilder.Add("FunctionOverloads", LogicalValue.New(this.FunctionOverloads.Value), TypeValue.Any);
			}
			if (this.IncludeAnnotations != null)
			{
				recordBuilder.Add("IncludeAnnotations", TextValue.New(this.IncludeAnnotations), TypeValue.Any);
			}
			if (this.IncludeMetadataAnnotations != null)
			{
				recordBuilder.Add("IncludeMetadataAnnotations", TextValue.New(this.IncludeMetadataAnnotations), TypeValue.Any);
			}
			if (this.OmitValues != null)
			{
				recordBuilder.Add("OmitValues", TextValue.New(this.OmitValues), TypeValue.Any);
			}
			if (this.OAuthResource != null)
			{
				recordBuilder.Add("Resource", TextValue.New(this.OAuthResource), TypeValue.Any);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000B2D9C File Offset: 0x000B0F9C
		public Uri ApplyQueryOptions(Uri requestUri)
		{
			if (this.QueryOptions.IsNull)
			{
				return requestUri;
			}
			using (Keys.StringKeysEnumerator enumerator = this.QueryOptions.AsRecord.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current[0] == '$')
					{
						throw ValueException.NewExpressionError<Message0>(Strings.ODataInvalidQueryOption, null, null);
					}
				}
			}
			return UriHelper.AddQueryRecord(requestUri, this.QueryOptions.AsRecord);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000B2E28 File Offset: 0x000B1028
		private static void ValidateHeaderValuesAreValidTypes(Value headers)
		{
			if (headers.IsRecord)
			{
				RecordValue asRecord = headers.AsRecord;
				for (int i = 0; i < asRecord.Count; i++)
				{
					ODataUserSettings.ValidateHeaderValueIsValidType(asRecord.Keys[i], asRecord[i]);
				}
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000B2E70 File Offset: 0x000B1070
		private static void ValidateHeaderValueIsValidType(string headerName, Value headerValue)
		{
			bool flag = false;
			ValueKind typeKind = headerValue.Type.TypeKind;
			if (typeKind == ValueKind.DateTime || typeKind - ValueKind.Number <= 2)
			{
				flag = true;
			}
			if (!flag)
			{
				throw ODataCommonErrors.InvalidHeaderValueType(headerName, headerValue.Type.TypeKind);
			}
		}

		// Token: 0x04001CD0 RID: 7376
		public static readonly string Implementation20Beta = "2.0 beta";

		// Token: 0x04001CD1 RID: 7377
		public static readonly string Implementation20 = "2.0";

		// Token: 0x04001CD2 RID: 7378
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(600.0);

		// Token: 0x04001CD3 RID: 7379
		private static readonly int DefaultMaxUriLength = 1400;

		// Token: 0x04001CD4 RID: 7380
		private int pageSize;

		// Token: 0x04001CD5 RID: 7381
		private string fingerprint;

		// Token: 0x04001CD6 RID: 7382
		private static readonly HashSet<string> validOptionKeys = new HashSet<string>
		{
			"ApiKeyName", "Headers", "ExcludedFromCacheKey", "Concurrent", "Timeout", "EnableBatch", "MaxUriLength", "Query", "ODataVersion", "FunctionOverloads",
			"MoreColumns", "IncludeAnnotations", "IncludeMetadataAnnotations", "Implementation", "OmitValues"
		};

		// Token: 0x04001CD7 RID: 7383
		private static readonly HashSet<string> validPrivilegedOptionKeys = new HashSet<string>(ODataUserSettings.validOptionKeys.Concat(new string[] { "Resource" }));
	}
}
