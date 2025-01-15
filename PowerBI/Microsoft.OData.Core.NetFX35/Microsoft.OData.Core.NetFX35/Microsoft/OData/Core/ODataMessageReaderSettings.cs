using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000182 RID: 386
	public sealed class ODataMessageReaderSettings : ODataMessageReaderSettingsBase
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x00032E9E File Offset: 0x0003109E
		public ODataMessageReaderSettings()
		{
			this.DisablePrimitiveTypeConversion = false;
			this.DisableMessageStreamDisposal = false;
			this.UndeclaredPropertyBehaviorKinds = ODataUndeclaredPropertyBehaviorKinds.None;
			this.readerBehavior = ODataReaderBehavior.DefaultBehavior;
			this.MaxProtocolVersion = ODataVersion.V4;
			this.EnableFullValidation = true;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00032ED4 File Offset: 0x000310D4
		public ODataMessageReaderSettings(ODataMessageReaderSettings other)
			: base(other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(other, "other");
			this.BaseUri = other.BaseUri;
			this.DisableMessageStreamDisposal = other.DisableMessageStreamDisposal;
			this.DisablePrimitiveTypeConversion = other.DisablePrimitiveTypeConversion;
			this.UndeclaredPropertyBehaviorKinds = other.UndeclaredPropertyBehaviorKinds;
			this.MaxProtocolVersion = other.MaxProtocolVersion;
			this.readerBehavior = other.ReaderBehavior;
			this.EnableAtom = other.EnableAtom;
			this.EnableFullValidation = other.EnableFullValidation;
			this.UseKeyAsSegment = other.UseKeyAsSegment;
			this.mediaTypeResolver = other.mediaTypeResolver;
			this.ODataSimplified = other.ODataSimplified;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00032F77 File Offset: 0x00031177
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00032F7F File Offset: 0x0003117F
		public Uri BaseUri
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00032F8D File Offset: 0x0003118D
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00032F95 File Offset: 0x00031195
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00032FA3 File Offset: 0x000311A3
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x00032FAB File Offset: 0x000311AB
		public bool DisablePrimitiveTypeConversion { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00032FB4 File Offset: 0x000311B4
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x00032FBC File Offset: 0x000311BC
		public ODataUndeclaredPropertyBehaviorKinds UndeclaredPropertyBehaviorKinds { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00032FC5 File Offset: 0x000311C5
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x00032FCD File Offset: 0x000311CD
		public bool DisableMessageStreamDisposal { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00032FD6 File Offset: 0x000311D6
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x00032FDE File Offset: 0x000311DE
		public ODataVersion MaxProtocolVersion { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00032FE7 File Offset: 0x000311E7
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00032FEF File Offset: 0x000311EF
		public bool EnableFullValidation { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00032FF8 File Offset: 0x000311F8
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x00033000 File Offset: 0x00031200
		public bool? UseKeyAsSegment { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00033009 File Offset: 0x00031209
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0003302A File Offset: 0x0003122A
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

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0003303E File Offset: 0x0003123E
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x00033046 File Offset: 0x00031246
		public bool ODataSimplified { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003304F File Offset: 0x0003124F
		internal bool DisableStrictMetadataValidation
		{
			get
			{
				return this.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.ODataServer || this.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003306F File Offset: 0x0003126F
		internal ODataReaderBehavior ReaderBehavior
		{
			get
			{
				return this.readerBehavior;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00033077 File Offset: 0x00031277
		internal bool ReportUndeclaredLinkProperties
		{
			get
			{
				return this.UndeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00033085 File Offset: 0x00031285
		internal bool IgnoreUndeclaredValueProperties
		{
			get
			{
				return this.UndeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00033093 File Offset: 0x00031293
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0003309B File Offset: 0x0003129B
		internal bool EnableAtom { get; set; }

		// Token: 0x06000E6B RID: 3691 RVA: 0x000330A4 File Offset: 0x000312A4
		public void EnableDefaultBehavior()
		{
			this.readerBehavior = ODataReaderBehavior.DefaultBehavior;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000330B1 File Offset: 0x000312B1
		public void EnableODataServerBehavior()
		{
			this.readerBehavior = ODataReaderBehavior.CreateWcfDataServicesServerBehavior();
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000330BE File Offset: 0x000312BE
		public void EnableWcfDataServicesClientBehavior(Func<IEdmType, string, IEdmType> typeResolver)
		{
			this.readerBehavior = ODataReaderBehavior.CreateWcfDataServicesClientBehavior(typeResolver);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000330CC File Offset: 0x000312CC
		internal bool ShouldSkipAnnotation(string annotationName)
		{
			return this.ShouldIncludeAnnotation == null || !this.ShouldIncludeAnnotation.Invoke(annotationName);
		}

		// Token: 0x0400062E RID: 1582
		private ODataReaderBehavior readerBehavior;

		// Token: 0x0400062F RID: 1583
		private Uri payloadBaseUri;

		// Token: 0x04000630 RID: 1584
		private ODataMediaTypeResolver mediaTypeResolver;
	}
}
