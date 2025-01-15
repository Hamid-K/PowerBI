using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core
{
	// Token: 0x02000185 RID: 389
	public sealed class ODataMessageWriterSettings : ODataMessageWriterSettingsBase
	{
		// Token: 0x06000EAE RID: 3758 RVA: 0x00033DDC File Offset: 0x00031FDC
		public ODataMessageWriterSettings()
		{
			this.writerBehavior = ODataWriterBehavior.DefaultBehavior;
			this.EnableFullValidation = true;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00033DF8 File Offset: 0x00031FF8
		public ODataMessageWriterSettings(ODataMessageWriterSettings other)
			: base(other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(other, "other");
			this.acceptCharSets = other.acceptCharSets;
			this.acceptMediaTypes = other.acceptMediaTypes;
			this.PayloadBaseUri = other.PayloadBaseUri;
			this.DisableMessageStreamDisposal = other.DisableMessageStreamDisposal;
			this.format = other.format;
			this.useFormat = other.useFormat;
			this.Version = other.Version;
			this.JsonPCallback = other.JsonPCallback;
			this.shouldIncludeAnnotation = other.shouldIncludeAnnotation;
			this.AutoComputePayloadMetadataInJson = other.AutoComputePayloadMetadataInJson;
			this.UseKeyAsSegment = other.UseKeyAsSegment;
			this.alwaysUseDefaultXmlNamespaceForRootElement = other.alwaysUseDefaultXmlNamespaceForRootElement;
			this.ODataUri = other.ODataUri;
			this.writerBehavior = other.writerBehavior;
			this.EnableAtom = other.EnableAtom;
			this.EnableFullValidation = other.EnableFullValidation;
			this.mediaTypeResolver = other.mediaTypeResolver;
			this.ODataSimplified = other.ODataSimplified;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00033EEF File Offset: 0x000320EF
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00033EF7 File Offset: 0x000320F7
		public ODataVersion? Version { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00033F00 File Offset: 0x00032100
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00033F08 File Offset: 0x00032108
		public Uri PayloadBaseUri
		{
			get
			{
				return this.payloadBaseUri;
			}
			set
			{
				this.payloadBaseUri = UriUtils.EnsureTaillingSlash(value);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00033F18 File Offset: 0x00032118
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x00033F3D File Offset: 0x0003213D
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00033F46 File Offset: 0x00032146
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x00033F4E File Offset: 0x0003214E
		public bool DisableMessageStreamDisposal { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00033F57 File Offset: 0x00032157
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00033F5F File Offset: 0x0003215F
		public string JsonPCallback { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00033F68 File Offset: 0x00032168
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00033F70 File Offset: 0x00032170
		public bool AutoComputePayloadMetadataInJson { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x00033F79 File Offset: 0x00032179
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x00033F81 File Offset: 0x00032181
		public bool? UseKeyAsSegment { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00033F8A File Offset: 0x0003218A
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00033F92 File Offset: 0x00032192
		public bool EnableFullValidation { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00033F9B File Offset: 0x0003219B
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00033FBC File Offset: 0x000321BC
		public ODataMediaTypeResolver MediaTypeResolver
		{
			get
			{
				if (this.mediaTypeResolver == null)
				{
					this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.EnableAtom);
				}
				return this.mediaTypeResolver;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<ODataMediaTypeResolver>(value, "MediaTypeResolver");
				this.mediaTypeResolver = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00033FD0 File Offset: 0x000321D0
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00033FD8 File Offset: 0x000321D8
		public bool ODataSimplified { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00033FE1 File Offset: 0x000321E1
		internal bool AlwaysUseDefaultXmlNamespaceForRootElement
		{
			get
			{
				return this.alwaysUseDefaultXmlNamespaceForRootElement;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00033FE9 File Offset: 0x000321E9
		internal string AcceptableMediaTypes
		{
			get
			{
				return this.acceptMediaTypes;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00033FF1 File Offset: 0x000321F1
		internal string AcceptableCharsets
		{
			get
			{
				return this.acceptCharSets;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00033FF9 File Offset: 0x000321F9
		internal ODataWriterBehavior WriterBehavior
		{
			get
			{
				return this.writerBehavior;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00034001 File Offset: 0x00032201
		internal ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00034009 File Offset: 0x00032209
		internal bool? UseFormat
		{
			get
			{
				return this.useFormat;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00034011 File Offset: 0x00032211
		internal Uri MetadataDocumentUri
		{
			get
			{
				return this.ODataUri.MetadataDocumentUri;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003401E File Offset: 0x0003221E
		internal SelectExpandClause SelectExpandClause
		{
			get
			{
				return this.ODataUri.SelectAndExpand;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003402B File Offset: 0x0003222B
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

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00034046 File Offset: 0x00032246
		internal bool IsIndividualProperty
		{
			get
			{
				return this.ODataUri.Path != null && this.ODataUri.Path.IsIndividualProperty();
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00034067 File Offset: 0x00032267
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0003406F File Offset: 0x0003226F
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00034078 File Offset: 0x00032278
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00034080 File Offset: 0x00032280
		internal bool EnableAtom { get; set; }

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00034089 File Offset: 0x00032289
		public void SetContentType(string acceptableMediaTypes, string acceptableCharSets)
		{
			this.acceptMediaTypes = (string.Equals(acceptableMediaTypes, "json", 5) ? "application/json" : acceptableMediaTypes);
			this.acceptCharSets = acceptableCharSets;
			this.format = null;
			this.useFormat = new bool?(false);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000340C1 File Offset: 0x000322C1
		public void SetContentType(ODataFormat payloadFormat)
		{
			this.acceptCharSets = null;
			this.acceptMediaTypes = null;
			this.format = payloadFormat;
			this.useFormat = new bool?(true);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000340E4 File Offset: 0x000322E4
		public void EnableDefaultBehavior()
		{
			this.writerBehavior = ODataWriterBehavior.DefaultBehavior;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000340F1 File Offset: 0x000322F1
		public void EnableODataServerBehavior()
		{
			this.writerBehavior = ODataWriterBehavior.CreateODataServerBehavior();
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000340FE File Offset: 0x000322FE
		public void EnableODataServerBehavior(bool alwaysUseDefaultXmlNamespaceForRootElement)
		{
			this.EnableODataServerBehavior();
			this.alwaysUseDefaultXmlNamespaceForRootElement = alwaysUseDefaultXmlNamespaceForRootElement;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003410D File Offset: 0x0003230D
		public void EnableWcfDataServicesClientBehavior()
		{
			this.writerBehavior = ODataWriterBehavior.CreateWcfDataServicesClientBehavior();
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003411A File Offset: 0x0003231A
		internal void SetServiceDocumentUri(Uri serviceDocumentUri)
		{
			this.ODataUri.ServiceRoot = serviceDocumentUri;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00034128 File Offset: 0x00032328
		internal bool HasJsonPaddingFunction()
		{
			return !string.IsNullOrEmpty(this.JsonPCallback);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00034138 File Offset: 0x00032338
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation.Invoke(annotationName);
		}

		// Token: 0x0400064C RID: 1612
		private string acceptCharSets;

		// Token: 0x0400064D RID: 1613
		private string acceptMediaTypes;

		// Token: 0x0400064E RID: 1614
		private ODataFormat format;

		// Token: 0x0400064F RID: 1615
		private bool? useFormat;

		// Token: 0x04000650 RID: 1616
		private ODataWriterBehavior writerBehavior;

		// Token: 0x04000651 RID: 1617
		private Func<string, bool> shouldIncludeAnnotation;

		// Token: 0x04000652 RID: 1618
		private bool alwaysUseDefaultXmlNamespaceForRootElement;

		// Token: 0x04000653 RID: 1619
		private ODataUri odataUri;

		// Token: 0x04000654 RID: 1620
		private Uri payloadBaseUri;

		// Token: 0x04000655 RID: 1621
		private ODataMediaTypeResolver mediaTypeResolver;
	}
}
