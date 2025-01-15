using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000248 RID: 584
	public sealed class NavigationPropertySegment : ODataPathSegment
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x00049EBC File Offset: 0x000480BC
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

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00049F20 File Offset: 0x00048120
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00049F28 File Offset: 0x00048128
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return base.TargetEdmNavigationSource;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00049F30 File Offset: 0x00048130
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00049F42 File Offset: 0x00048142
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00049F56 File Offset: 0x00048156
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00049F6C File Offset: 0x0004816C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertySegment navigationPropertySegment = other as NavigationPropertySegment;
			return navigationPropertySegment != null && navigationPropertySegment.NavigationProperty == this.NavigationProperty;
		}

		// Token: 0x040008B7 RID: 2231
		private readonly IEdmNavigationProperty navigationProperty;
	}
}
