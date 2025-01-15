using System;
using Microsoft.OData.Buffers;
using Microsoft.OData.Evaluation;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x0200009A RID: 154
	public sealed class ODataMessageWriterSettings
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x0000FC7D File Offset: 0x0000DE7D
		public ODataMessageWriterSettings()
		{
			this.EnableMessageStreamDisposal = true;
			this.EnableCharactersCheck = false;
			this.Validations = ValidationKinds.All;
			this.Validator = new WriterValidator(this);
			this.LibraryCompatibility = ODataLibraryCompatibility.Latest;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000FCB1 File Offset: 0x0000DEB1
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000FCB9 File Offset: 0x0000DEB9
		public ODataLibraryCompatibility LibraryCompatibility { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000FCC2 File Offset: 0x0000DEC2
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000FCCA File Offset: 0x0000DECA
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000FD06 File Offset: 0x0000DF06
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0000FD0E File Offset: 0x0000DF0E
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

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0000FD24 File Offset: 0x0000DF24
		public bool EnableMessageStreamDisposal { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000FD2D File Offset: 0x0000DF2D
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0000FD35 File Offset: 0x0000DF35
		public bool EnableCharactersCheck { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0000FD3E File Offset: 0x0000DF3E
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0000FD46 File Offset: 0x0000DF46
		public string JsonPCallback { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0000FD4F File Offset: 0x0000DF4F
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0000FD57 File Offset: 0x0000DF57
		public ICharArrayPool ArrayPool { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0000FD60 File Offset: 0x0000DF60
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0000FD7B File Offset: 0x0000DF7B
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0000FD84 File Offset: 0x0000DF84
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000FDA9 File Offset: 0x0000DFA9
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000FDBA File Offset: 0x0000DFBA
		public ODataVersion? Version { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000FDC3 File Offset: 0x0000DFC3
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0000FDCB File Offset: 0x0000DFCB
		public ODataMetadataSelector MetadataSelector { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		internal IWriterValidator Validator { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000FDE5 File Offset: 0x0000DFE5
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0000FDED File Offset: 0x0000DFED
		internal bool ThrowIfTypeConflictsWithMetadata { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000FDF6 File Offset: 0x0000DFF6
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0000FDFE File Offset: 0x0000DFFE
		internal bool ThrowOnDuplicatePropertyNames { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000FE07 File Offset: 0x0000E007
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0000FE0F File Offset: 0x0000E00F
		internal bool ThrowOnUndeclaredPropertyForNonOpenType { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000FE18 File Offset: 0x0000E018
		internal string AcceptableMediaTypes
		{
			get
			{
				return this.acceptMediaTypes;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000FE20 File Offset: 0x0000E020
		internal string AcceptableCharsets
		{
			get
			{
				return this.acceptCharSets;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0000FE28 File Offset: 0x0000E028
		internal ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0000FE30 File Offset: 0x0000E030
		internal bool IsIndividualProperty
		{
			get
			{
				return this.ODataUri.Path != null && this.ODataUri.Path.IsIndividualProperty();
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000FE51 File Offset: 0x0000E051
		internal Uri MetadataDocumentUri
		{
			get
			{
				return this.ODataUri.MetadataDocumentUri;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000FE5E File Offset: 0x0000E05E
		internal bool? UseFormat
		{
			get
			{
				return this.useFormat;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000FE66 File Offset: 0x0000E066
		internal SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.ODataUri.SelectAndExpand;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0000FE73 File Offset: 0x0000E073
		internal SelectedPropertiesNode SelectedProperties
		{
			get
			{
				if (this.SelectExpandClause == null)
				{
					return new SelectedPropertiesNode(SelectedPropertiesNode.SelectionType.EntireSubtree);
				}
				return SelectedPropertiesNode.Create(this.SelectExpandClause);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0000FE8F File Offset: 0x0000E08F
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0000FE97 File Offset: 0x0000E097
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

		// Token: 0x06000677 RID: 1655 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public ODataMessageWriterSettings Clone()
		{
			ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings();
			odataMessageWriterSettings.CopyFrom(this);
			return odataMessageWriterSettings;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000FEBB File Offset: 0x0000E0BB
		public void SetContentType(string acceptableMediaTypes, string acceptableCharSets)
		{
			this.acceptMediaTypes = (string.Equals(acceptableMediaTypes, "json", StringComparison.OrdinalIgnoreCase) ? "application/json" : acceptableMediaTypes);
			this.acceptCharSets = acceptableCharSets;
			this.format = null;
			this.useFormat = new bool?(false);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
		public void SetContentType(ODataFormat payloadFormat)
		{
			this.acceptCharSets = null;
			this.acceptMediaTypes = null;
			this.format = payloadFormat;
			this.useFormat = new bool?(true);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000FF18 File Offset: 0x0000E118
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

		// Token: 0x0600067B RID: 1659 RVA: 0x0000FF42 File Offset: 0x0000E142
		internal void SetServiceDocumentUri(Uri serviceDocumentUri)
		{
			this.ODataUri.ServiceRoot = serviceDocumentUri;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000FF50 File Offset: 0x0000E150
		internal bool HasJsonPaddingFunction()
		{
			return !string.IsNullOrEmpty(this.JsonPCallback);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000FF60 File Offset: 0x0000E160
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation(annotationName);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000FF7C File Offset: 0x0000E17C
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
			this.LibraryCompatibility = other.LibraryCompatibility;
			this.MetadataSelector = other.MetadataSelector;
			this.validations = other.validations;
			this.ThrowIfTypeConflictsWithMetadata = other.ThrowIfTypeConflictsWithMetadata;
			this.ThrowOnDuplicatePropertyNames = other.ThrowOnDuplicatePropertyNames;
			this.ThrowOnUndeclaredPropertyForNonOpenType = other.ThrowOnUndeclaredPropertyForNonOpenType;
		}

		// Token: 0x0400027D RID: 637
		private string acceptCharSets;

		// Token: 0x0400027E RID: 638
		private string acceptMediaTypes;

		// Token: 0x0400027F RID: 639
		private Uri baseUri;

		// Token: 0x04000280 RID: 640
		private ODataFormat format;

		// Token: 0x04000281 RID: 641
		private ODataMessageQuotas messageQuotas;

		// Token: 0x04000282 RID: 642
		private ODataUri odataUri;

		// Token: 0x04000283 RID: 643
		private Func<string, bool> shouldIncludeAnnotation;

		// Token: 0x04000284 RID: 644
		private bool? useFormat;

		// Token: 0x04000285 RID: 645
		private ValidationKinds validations;
	}
}
