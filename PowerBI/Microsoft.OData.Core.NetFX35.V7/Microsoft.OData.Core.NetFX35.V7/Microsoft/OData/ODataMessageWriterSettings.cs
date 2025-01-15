using System;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x02000074 RID: 116
	public sealed class ODataMessageWriterSettings
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		public ODataMessageWriterSettings()
		{
			this.EnableMessageStreamDisposal = true;
			this.EnableCharactersCheck = false;
			this.Validations = ValidationKinds.All;
			this.Validator = new WriterValidator(this);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000C9E1 File Offset: 0x0000ABE1
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000C9E9 File Offset: 0x0000ABE9
		public ValidationKinds Validations
		{
			get
			{
				return this.validations;
			}
			set
			{
				this.validations = value;
				this.ThrowIfTypeConflictsWithMetadata = (this.validations & ValidationKinds.ThrowIfTypeConflictsWithMetadata) > ValidationKinds.None;
				this.ThrowOnDuplicatePropertyNames = (this.validations & ValidationKinds.ThrowOnDuplicatePropertyNames) > ValidationKinds.None;
				this.ThrowOnUndeclaredPropertyForNonOpenType = (this.validations & ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType) > ValidationKinds.None;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000CA25 File Offset: 0x0000AC25
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000CA2D File Offset: 0x0000AC2D
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = UriUtils.EnsureTaillingSlash(value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000CA3B File Offset: 0x0000AC3B
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000CA43 File Offset: 0x0000AC43
		public bool EnableMessageStreamDisposal { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000CA54 File Offset: 0x0000AC54
		public bool EnableCharactersCheck { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000CA5D File Offset: 0x0000AC5D
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000CA65 File Offset: 0x0000AC65
		public string JsonPCallback { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000CA6E File Offset: 0x0000AC6E
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000CA89 File Offset: 0x0000AC89
		public ODataMessageQuotas MessageQuotas
		{
			get
			{
				if (this.messageQuotas == null)
				{
					this.messageQuotas = new ODataMessageQuotas();
				}
				return this.messageQuotas;
			}
			set
			{
				this.messageQuotas = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000CA94 File Offset: 0x0000AC94
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000CAB9 File Offset: 0x0000ACB9
		public ODataUri ODataUri
		{
			get
			{
				ODataUri odataUri;
				if ((odataUri = this.odataUri) == null)
				{
					odataUri = (this.odataUri = new ODataUri());
				}
				return odataUri;
			}
			set
			{
				this.odataUri = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000CAC2 File Offset: 0x0000ACC2
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000CACA File Offset: 0x0000ACCA
		public ODataVersion? Version { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000CAD3 File Offset: 0x0000ACD3
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000CADB File Offset: 0x0000ACDB
		internal IWriterValidator Validator { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000CAEC File Offset: 0x0000ACEC
		internal bool ThrowIfTypeConflictsWithMetadata { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000CAF5 File Offset: 0x0000ACF5
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000CAFD File Offset: 0x0000ACFD
		internal bool ThrowOnDuplicatePropertyNames { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000CB06 File Offset: 0x0000AD06
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000CB0E File Offset: 0x0000AD0E
		internal bool ThrowOnUndeclaredPropertyForNonOpenType { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000CB17 File Offset: 0x0000AD17
		internal string AcceptableMediaTypes
		{
			get
			{
				return this.acceptMediaTypes;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		internal string AcceptableCharsets
		{
			get
			{
				return this.acceptCharSets;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000CB27 File Offset: 0x0000AD27
		internal ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000CB2F File Offset: 0x0000AD2F
		internal bool IsIndividualProperty
		{
			get
			{
				return this.ODataUri.Path != null && this.ODataUri.Path.IsIndividualProperty();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000CB50 File Offset: 0x0000AD50
		internal Uri MetadataDocumentUri
		{
			get
			{
				return this.ODataUri.MetadataDocumentUri;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		internal bool? UseFormat
		{
			get
			{
				return this.useFormat;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000CB65 File Offset: 0x0000AD65
		internal SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.ODataUri.SelectAndExpand;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000CB72 File Offset: 0x0000AD72
		internal SelectedPropertiesNode SelectedProperties
		{
			get
			{
				if (this.SelectExpandClause == null)
				{
					return SelectedPropertiesNode.EntireSubtree;
				}
				return SelectedPropertiesNode.Create(this.SelectExpandClause);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000CB95 File Offset: 0x0000AD95
		internal Func<string, bool> ShouldIncludeAnnotation
		{
			get
			{
				return this.shouldIncludeAnnotation;
			}
			set
			{
				this.shouldIncludeAnnotation = value;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public ODataMessageWriterSettings Clone()
		{
			ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings();
			odataMessageWriterSettings.CopyFrom(this);
			return odataMessageWriterSettings;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000CBBB File Offset: 0x0000ADBB
		public void SetContentType(string acceptableMediaTypes, string acceptableCharSets)
		{
			this.acceptMediaTypes = (string.Equals(acceptableMediaTypes, "json", 5) ? "application/json" : acceptableMediaTypes);
			this.acceptCharSets = acceptableCharSets;
			this.format = null;
			this.useFormat = new bool?(false);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		public void SetContentType(ODataFormat payloadFormat)
		{
			this.acceptCharSets = null;
			this.acceptMediaTypes = null;
			this.format = payloadFormat;
			this.useFormat = new bool?(true);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000CC18 File Offset: 0x0000AE18
		internal static ODataMessageWriterSettings CreateWriterSettings(IServiceProvider container, ODataMessageWriterSettings other)
		{
			ODataMessageWriterSettings odataMessageWriterSettings;
			if (container == null)
			{
				odataMessageWriterSettings = new ODataMessageWriterSettings();
			}
			else
			{
				odataMessageWriterSettings = container.GetRequiredService<ODataMessageWriterSettings>();
			}
			if (other != null)
			{
				odataMessageWriterSettings.CopyFrom(other);
			}
			return odataMessageWriterSettings;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000CC42 File Offset: 0x0000AE42
		internal void SetServiceDocumentUri(Uri serviceDocumentUri)
		{
			this.ODataUri.ServiceRoot = serviceDocumentUri;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000CC50 File Offset: 0x0000AE50
		internal bool HasJsonPaddingFunction()
		{
			return !string.IsNullOrEmpty(this.JsonPCallback);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000CC60 File Offset: 0x0000AE60
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation.Invoke(annotationName);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		private void CopyFrom(ODataMessageWriterSettings other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(other, "other");
			this.acceptCharSets = other.acceptCharSets;
			this.acceptMediaTypes = other.acceptMediaTypes;
			this.BaseUri = other.BaseUri;
			this.EnableMessageStreamDisposal = other.EnableMessageStreamDisposal;
			this.EnableCharactersCheck = other.EnableCharactersCheck;
			this.format = other.format;
			this.JsonPCallback = other.JsonPCallback;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
			this.ODataUri = other.ODataUri.Clone();
			this.shouldIncludeAnnotation = other.shouldIncludeAnnotation;
			this.useFormat = other.useFormat;
			this.Version = other.Version;
			this.validations = other.validations;
			this.ThrowIfTypeConflictsWithMetadata = other.ThrowIfTypeConflictsWithMetadata;
			this.ThrowOnDuplicatePropertyNames = other.ThrowOnDuplicatePropertyNames;
			this.ThrowOnUndeclaredPropertyForNonOpenType = other.ThrowOnUndeclaredPropertyForNonOpenType;
		}

		// Token: 0x0400021A RID: 538
		private string acceptCharSets;

		// Token: 0x0400021B RID: 539
		private string acceptMediaTypes;

		// Token: 0x0400021C RID: 540
		private Uri baseUri;

		// Token: 0x0400021D RID: 541
		private ODataFormat format;

		// Token: 0x0400021E RID: 542
		private ODataMessageQuotas messageQuotas;

		// Token: 0x0400021F RID: 543
		private ODataUri odataUri;

		// Token: 0x04000220 RID: 544
		private Func<string, bool> shouldIncludeAnnotation;

		// Token: 0x04000221 RID: 545
		private bool? useFormat;

		// Token: 0x04000222 RID: 546
		private ValidationKinds validations;
	}
}
