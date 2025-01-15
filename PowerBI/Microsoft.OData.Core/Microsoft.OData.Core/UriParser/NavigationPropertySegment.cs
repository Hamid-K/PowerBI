using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018C RID: 396
	public sealed class NavigationPropertySegment : ODataPathSegment
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x00039624 File Offset: 0x00037824
		public NavigationPropertySegment(IEdmNavigationProperty navigationProperty, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.navigationProperty = navigationProperty;
			base.TargetEdmNavigationSource = navigationSource;
			base.Identifier = navigationProperty.Name;
			base.TargetEdmType = navigationProperty.Type.Definition;
			base.SingleResult = !navigationProperty.Type.IsCollection();
			base.TargetKind = RequestTargetKind.Resource;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x00039689 File Offset: 0x00037889
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x000395A9 File Offset: 0x000377A9
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return base.TargetEdmNavigationSource;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00039691 File Offset: 0x00037891
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000396A3 File Offset: 0x000378A3
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000396B8 File Offset: 0x000378B8
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000396D0 File Offset: 0x000378D0
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertySegment navigationPropertySegment = other as NavigationPropertySegment;
			return navigationPropertySegment != null && navigationPropertySegment.NavigationProperty == this.NavigationProperty;
		}

		// Token: 0x040008A5 RID: 2213
		private readonly IEdmNavigationProperty navigationProperty;
	}
}
