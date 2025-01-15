using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000109 RID: 265
	internal sealed class ODataUriParserConfiguration
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00022C28 File Offset: 0x00020E28
		public ODataUriParserConfiguration(IEdmModel model, IServiceProvider container)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			this.Model = model;
			this.Container = container;
			this.Resolver = ODataUriResolver.GetUriResolver(container);
			this.urlKeyDelimiter = ODataUrlKeyDelimiter.GetODataUrlKeyDelimiter(container);
			if (this.Container == null)
			{
				this.Settings = new ODataUriParserSettings();
			}
			else
			{
				this.Settings = this.Container.GetRequiredService<ODataUriParserSettings>();
			}
			this.EnableUriTemplateParsing = false;
			this.EnableCaseInsensitiveUriFunctionIdentifier = false;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00022CA1 File Offset: 0x00020EA1
		internal ODataUriParserConfiguration(IEdmModel model)
			: this(model, null)
		{
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00022CAB File Offset: 0x00020EAB
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00022CB3 File Offset: 0x00020EB3
		public ODataUriParserSettings Settings { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00022CBC File Offset: 0x00020EBC
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00022CC4 File Offset: 0x00020EC4
		public IEdmModel Model { get; private set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00022CCD File Offset: 0x00020ECD
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00022CD5 File Offset: 0x00020ED5
		public IServiceProvider Container { get; private set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00022CDE File Offset: 0x00020EDE
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00022CE6 File Offset: 0x00020EE6
		public ODataUrlKeyDelimiter UrlKeyDelimiter
		{
			get
			{
				return this.urlKeyDelimiter;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<ODataUrlKeyDelimiter>(value, "UrlKeyDelimiter");
				this.urlKeyDelimiter = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00022CFB File Offset: 0x00020EFB
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00022D03 File Offset: 0x00020F03
		public Func<string, BatchReferenceSegment> BatchReferenceCallback { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00022D0C File Offset: 0x00020F0C
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00022D14 File Offset: 0x00020F14
		public ParseDynamicPathSegment ParseDynamicPathSegmentFunc { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00022D1D File Offset: 0x00020F1D
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00022D2A File Offset: 0x00020F2A
		internal bool EnableCaseInsensitiveUriFunctionIdentifier
		{
			get
			{
				return this.Resolver.EnableCaseInsensitive;
			}
			set
			{
				this.Resolver.EnableCaseInsensitive = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00022D38 File Offset: 0x00020F38
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00022D40 File Offset: 0x00020F40
		internal bool EnableNoDollarQueryOptions { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00022D49 File Offset: 0x00020F49
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00022D51 File Offset: 0x00020F51
		internal bool EnableUriTemplateParsing { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00022D5A File Offset: 0x00020F5A
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00022D62 File Offset: 0x00020F62
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00022D6B File Offset: 0x00020F6B
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00022D73 File Offset: 0x00020F73
		internal ODataUriResolver Resolver
		{
			get
			{
				return this.uriResolver;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<ODataUriResolver>(value, "Resolver");
				this.uriResolver = value;
			}
		}

		// Token: 0x040006D3 RID: 1747
		private ODataUrlKeyDelimiter urlKeyDelimiter;

		// Token: 0x040006D4 RID: 1748
		private ODataUriResolver uriResolver;
	}
}
