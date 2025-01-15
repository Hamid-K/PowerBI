using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018B RID: 395
	public sealed class NavigationPropertyLinkSegment : ODataPathSegment
	{
		// Token: 0x06001353 RID: 4947 RVA: 0x0003953C File Offset: 0x0003773C
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x000395A1 File Offset: 0x000377A1
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x000395A9 File Offset: 0x000377A9
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return base.TargetEdmNavigationSource;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x000395B1 File Offset: 0x000377B1
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000395C3 File Offset: 0x000377C3
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000395D8 File Offset: 0x000377D8
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000395F0 File Offset: 0x000377F0
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertyLinkSegment navigationPropertyLinkSegment = other as NavigationPropertyLinkSegment;
			return navigationPropertyLinkSegment != null && navigationPropertyLinkSegment.NavigationProperty == this.navigationProperty;
		}

		// Token: 0x040008A4 RID: 2212
		private readonly IEdmNavigationProperty navigationProperty;
	}
}
