using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010B RID: 267
	public sealed class EachSegment : ODataPathSegment
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0002604C File Offset: 0x0002424C
		public EachSegment(IEdmNavigationSource navigationSource, IEdmType targetEdmType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationSource>(navigationSource, "navigationSource");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(targetEdmType, "targetEdmType");
			base.Identifier = "$each";
			base.SingleResult = false;
			base.TargetEdmNavigationSource = navigationSource;
			base.TargetEdmType = targetEdmType;
			base.TargetKind = targetEdmType.GetTargetKindFromType();
			this.edmType = navigationSource.Type;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000260AF File Offset: 0x000242AF
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000260B7 File Offset: 0x000242B7
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000260CC File Offset: 0x000242CC
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000260E4 File Offset: 0x000242E4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			EachSegment eachSegment = other as EachSegment;
			return eachSegment != null && eachSegment.TargetEdmNavigationSource == base.TargetEdmNavigationSource;
		}

		// Token: 0x0400077B RID: 1915
		private readonly IEdmType edmType;
	}
}
