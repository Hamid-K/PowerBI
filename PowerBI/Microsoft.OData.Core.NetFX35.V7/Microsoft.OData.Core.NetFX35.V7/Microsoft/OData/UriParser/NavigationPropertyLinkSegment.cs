using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000141 RID: 321
	public sealed class NavigationPropertyLinkSegment : ODataPathSegment
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x00029874 File Offset: 0x00027A74
		public NavigationPropertyLinkSegment(IEdmNavigationProperty navigationProperty, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.navigationProperty = navigationProperty;
			base.TargetEdmNavigationSource = navigationSource;
			base.Identifier = navigationProperty.Name;
			base.TargetEdmType = navigationProperty.Type.Definition;
			base.SingleResult = !navigationProperty.Type.IsCollection();
			base.TargetKind = RequestTargetKind.Resource;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x000298D9 File Offset: 0x00027AD9
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x000298E1 File Offset: 0x00027AE1
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return base.TargetEdmNavigationSource;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x000298E9 File Offset: 0x00027AE9
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000298FB File Offset: 0x00027AFB
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00029910 File Offset: 0x00027B10
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00029928 File Offset: 0x00027B28
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertyLinkSegment navigationPropertyLinkSegment = other as NavigationPropertyLinkSegment;
			return navigationPropertyLinkSegment != null && navigationPropertyLinkSegment.NavigationProperty == this.navigationProperty;
		}

		// Token: 0x0400076F RID: 1903
		private readonly IEdmNavigationProperty navigationProperty;
	}
}
