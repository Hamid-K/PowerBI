using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014D RID: 333
	internal sealed class ODataUriParserConfiguration
	{
		// Token: 0x06001147 RID: 4423 RVA: 0x00031054 File Offset: 0x0002F254
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
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000310C6 File Offset: 0x0002F2C6
		internal ODataUriParserConfiguration(IEdmModel model)
			: this(model, null)
		{
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x000310D0 File Offset: 0x0002F2D0
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x000310D8 File Offset: 0x0002F2D8
		public ODataUriParserSettings Settings { get; private set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x000310E1 File Offset: 0x0002F2E1
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x000310E9 File Offset: 0x0002F2E9
		public IEdmModel Model { get; private set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000310F2 File Offset: 0x0002F2F2
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x000310FA File Offset: 0x0002F2FA
		public IServiceProvider Container { get; private set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00031103 File Offset: 0x0002F303
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0003110B File Offset: 0x0002F30B
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00031120 File Offset: 0x0002F320
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x00031128 File Offset: 0x0002F328
		public Func<string, BatchReferenceSegment> BatchReferenceCallback { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00031131 File Offset: 0x0002F331
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x00031139 File Offset: 0x0002F339
		public ParseDynamicPathSegment ParseDynamicPathSegmentFunc { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00031142 File Offset: 0x0002F342
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0003114F File Offset: 0x0002F34F
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

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0003115D File Offset: 0x0002F35D
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003116A File Offset: 0x0002F36A
		internal bool EnableNoDollarQueryOptions
		{
			get
			{
				return this.Resolver.EnableNoDollarQueryOptions;
			}
			set
			{
				this.Resolver.EnableNoDollarQueryOptions = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00031178 File Offset: 0x0002F378
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00031180 File Offset: 0x0002F380
		internal bool EnableUriTemplateParsing { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00031189 File Offset: 0x0002F389
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x00031191 File Offset: 0x0002F391
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0003119A File Offset: 0x0002F39A
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x000311A2 File Offset: 0x0002F3A2
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

		// Token: 0x040007F2 RID: 2034
		private ODataUrlKeyDelimiter urlKeyDelimiter;

		// Token: 0x040007F3 RID: 2035
		private ODataUriResolver uriResolver;
	}
}
