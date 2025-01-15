using System;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001F2 RID: 498
	internal sealed class ODataUriParserConfiguration
	{
		// Token: 0x0600123E RID: 4670 RVA: 0x00042078 File Offset: 0x00040278
		public ODataUriParserConfiguration(IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			this.model = model;
			this.Settings = new ODataUriParserSettings();
			this.EnableUriTemplateParsing = false;
			this.EnableCaseInsensitiveUriFunctionIdentifier = false;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x000420CC File Offset: 0x000402CC
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x000420D4 File Offset: 0x000402D4
		public ODataUriParserSettings Settings { get; private set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000420DD File Offset: 0x000402DD
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000420E5 File Offset: 0x000402E5
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x000420ED File Offset: 0x000402ED
		public ODataUrlConventions UrlConventions
		{
			get
			{
				return this.urlConventions;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<ODataUrlConventions>(value, "UrlConventions");
				this.urlConventions = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00042101 File Offset: 0x00040301
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00042109 File Offset: 0x00040309
		public Func<string, BatchReferenceSegment> BatchReferenceCallback { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00042112 File Offset: 0x00040312
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x0004211F File Offset: 0x0004031F
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

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004212D File Offset: 0x0004032D
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00042135 File Offset: 0x00040335
		internal bool EnableUriTemplateParsing { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004213E File Offset: 0x0004033E
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00042146 File Offset: 0x00040346
		internal ParameterAliasValueAccessor ParameterAliasValueAccessor { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004214F File Offset: 0x0004034F
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00042157 File Offset: 0x00040357
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

		// Token: 0x040007D2 RID: 2002
		private readonly IEdmModel model;

		// Token: 0x040007D3 RID: 2003
		private ODataUrlConventions urlConventions = ODataUrlConventions.Default;

		// Token: 0x040007D4 RID: 2004
		private ODataUriResolver uriResolver = new ODataUriResolver();
	}
}
