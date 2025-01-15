using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000247 RID: 583
	public sealed class NavigationPropertyLinkSegment : ODataPathSegment
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x00049DD8 File Offset: 0x00047FD8
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00049E3C File Offset: 0x0004803C
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00049E44 File Offset: 0x00048044
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return base.TargetEdmNavigationSource;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00049E4C File Offset: 0x0004804C
		public override IEdmType EdmType
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00049E5E File Offset: 0x0004805E
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00049E72 File Offset: 0x00048072
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00049E88 File Offset: 0x00048088
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			NavigationPropertyLinkSegment navigationPropertyLinkSegment = other as NavigationPropertyLinkSegment;
			return navigationPropertyLinkSegment != null && navigationPropertyLinkSegment.NavigationProperty == this.navigationProperty;
		}

		// Token: 0x040008B6 RID: 2230
		private readonly IEdmNavigationProperty navigationProperty;
	}
}
