using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000142 RID: 322
	public sealed class NavigationPropertySegment : ODataPathSegment
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x0002995C File Offset: 0x00027B5C
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000299C1 File Offset: 0x00027BC1
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000298E1 File Offset: 0x00027AE1
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return base.TargetEdmNavigationSource;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000299C9 File Offset: 0x00027BC9
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000299DB File Offset: 0x00027BDB
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000299F0 File Offset: 0x00027BF0
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00029A08 File Offset: 0x00027C08
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertySegment navigationPropertySegment = other as NavigationPropertySegment;
			return navigationPropertySegment != null && navigationPropertySegment.NavigationProperty == this.NavigationProperty;
		}

		// Token: 0x04000770 RID: 1904
		private readonly IEdmNavigationProperty navigationProperty;
	}
}
