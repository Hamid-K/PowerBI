using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000149 RID: 329
	public sealed class DynamicPathSegment : ODataPathSegment
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x0002A2D7 File Offset: 0x000284D7
		public DynamicPathSegment(string identifier)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			base.Identifier = identifier;
			base.TargetEdmType = null;
			base.TargetKind = RequestTargetKind.Dynamic;
			base.SingleResult = true;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002A308 File Offset: 0x00028508
		public DynamicPathSegment(string identifier, IEdmType edmType, IEdmNavigationSource navigationSource, bool singleResult)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			base.Identifier = identifier;
			base.TargetEdmType = edmType;
			base.SingleResult = singleResult;
			base.TargetKind = ((edmType == null) ? RequestTargetKind.Dynamic : edmType.GetTargetKindFromType());
			base.TargetEdmNavigationSource = navigationSource;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002A357 File Offset: 0x00028557
		public override IEdmType EdmType
		{
			get
			{
				return base.TargetEdmType;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0002A35F File Offset: 0x0002855F
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0002A374 File Offset: 0x00028574
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002A38C File Offset: 0x0002858C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			DynamicPathSegment dynamicPathSegment = other as DynamicPathSegment;
			return dynamicPathSegment != null && dynamicPathSegment.Identifier == base.Identifier && dynamicPathSegment.EdmType == this.EdmType && dynamicPathSegment.TargetEdmNavigationSource == base.TargetEdmNavigationSource && dynamicPathSegment.SingleResult == base.SingleResult;
		}
	}
}
