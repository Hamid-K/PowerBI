using System;

namespace Microsoft.OData
{
	// Token: 0x0200001A RID: 26
	public sealed class ODataSimplifiedOptions
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000036B4 File Offset: 0x000018B4
		public ODataSimplifiedOptions()
			: this(null)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000036D0 File Offset: 0x000018D0
		public ODataSimplifiedOptions(ODataVersion? version)
		{
			this.EnableParsingKeyAsSegmentUrl = true;
			this.EnableWritingKeyAsSegment = false;
			this.EnableReadingKeyAsSegment = false;
			if (version == null || version < ODataVersion.V401)
			{
				this.EnableReadingODataAnnotationWithoutPrefix = false;
				this.enableWritingODataAnnotationWithoutPrefix = this.omitODataPrefix40;
				return;
			}
			this.EnableReadingODataAnnotationWithoutPrefix = true;
			this.enableWritingODataAnnotationWithoutPrefix = this.omitODataPrefix;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003749 File Offset: 0x00001949
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00003751 File Offset: 0x00001951
		public bool EnableParsingKeyAsSegmentUrl { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000375A File Offset: 0x0000195A
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00003762 File Offset: 0x00001962
		public bool EnableReadingKeyAsSegment { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000376B File Offset: 0x0000196B
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003773 File Offset: 0x00001973
		public bool EnableReadingODataAnnotationWithoutPrefix { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000377C File Offset: 0x0000197C
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00003784 File Offset: 0x00001984
		public bool EnableWritingKeyAsSegment { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00003798 File Offset: 0x00001998
		[Obsolete("Deprecated. Use Get/SetOmitODataPrefix()")]
		public bool EnableWritingODataAnnotationWithoutPrefix
		{
			get
			{
				return this.enableWritingODataAnnotationWithoutPrefix;
			}
			set
			{
				this.omitODataPrefix40 = value;
				this.omitODataPrefix = value;
				this.enableWritingODataAnnotationWithoutPrefix = value;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000037C0 File Offset: 0x000019C0
		public ODataSimplifiedOptions Clone()
		{
			ODataSimplifiedOptions odataSimplifiedOptions = new ODataSimplifiedOptions();
			odataSimplifiedOptions.CopyFrom(this);
			return odataSimplifiedOptions;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000378D File Offset: 0x0000198D
		public bool GetOmitODataPrefix()
		{
			return this.enableWritingODataAnnotationWithoutPrefix;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000037DB File Offset: 0x000019DB
		public bool GetOmitODataPrefix(ODataVersion version)
		{
			if (version >= ODataVersion.V401)
			{
				return this.omitODataPrefix;
			}
			return this.omitODataPrefix40;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000037F0 File Offset: 0x000019F0
		public void SetOmitODataPrefix(bool enabled)
		{
			this.omitODataPrefix40 = enabled;
			this.omitODataPrefix = enabled;
			this.enableWritingODataAnnotationWithoutPrefix = enabled;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003816 File Offset: 0x00001A16
		public void SetOmitODataPrefix(bool enabled, ODataVersion version)
		{
			if (version == ODataVersion.V4)
			{
				this.omitODataPrefix40 = enabled;
				return;
			}
			this.omitODataPrefix = enabled;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000382A File Offset: 0x00001A2A
		internal static ODataSimplifiedOptions GetODataSimplifiedOptions(IServiceProvider container, ODataVersion? version = null)
		{
			if (container == null)
			{
				return new ODataSimplifiedOptions(version);
			}
			return container.GetRequiredService<ODataSimplifiedOptions>();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000383C File Offset: 0x00001A3C
		private void CopyFrom(ODataSimplifiedOptions other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataSimplifiedOptions>(other, "other");
			this.EnableParsingKeyAsSegmentUrl = other.EnableParsingKeyAsSegmentUrl;
			this.EnableReadingKeyAsSegment = other.EnableReadingKeyAsSegment;
			this.EnableReadingODataAnnotationWithoutPrefix = other.EnableReadingODataAnnotationWithoutPrefix;
			this.EnableWritingKeyAsSegment = other.EnableWritingKeyAsSegment;
			this.enableWritingODataAnnotationWithoutPrefix = other.enableWritingODataAnnotationWithoutPrefix;
			this.omitODataPrefix40 = other.omitODataPrefix40;
			this.omitODataPrefix = other.omitODataPrefix;
		}

		// Token: 0x0400003F RID: 63
		private bool enableWritingODataAnnotationWithoutPrefix;

		// Token: 0x04000040 RID: 64
		private bool omitODataPrefix40;

		// Token: 0x04000041 RID: 65
		private bool omitODataPrefix = true;
	}
}
