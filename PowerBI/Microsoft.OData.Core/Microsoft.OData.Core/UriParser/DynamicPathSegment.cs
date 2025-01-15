using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000195 RID: 405
	public sealed class DynamicPathSegment : ODataPathSegment
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x0003A0F3 File Offset: 0x000382F3
		public DynamicPathSegment(string identifier)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			base.Identifier = identifier;
			base.TargetEdmType = null;
			base.TargetKind = RequestTargetKind.Dynamic;
			base.SingleResult = true;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003A124 File Offset: 0x00038324
		public DynamicPathSegment(string identifier, IEdmType edmType, IEdmNavigationSource navigationSource, bool singleResult)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			base.Identifier = identifier;
			base.TargetEdmType = edmType;
			base.SingleResult = singleResult;
			base.TargetKind = ((edmType == null) ? RequestTargetKind.Dynamic : edmType.GetTargetKindFromType());
			base.TargetEdmNavigationSource = navigationSource;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0003A173 File Offset: 0x00038373
		public override IEdmType EdmType
		{
			get
			{
				return base.TargetEdmType;
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0003A17B File Offset: 0x0003837B
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0003A190 File Offset: 0x00038390
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0003A1A8 File Offset: 0x000383A8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			DynamicPathSegment dynamicPathSegment = other as DynamicPathSegment;
			return dynamicPathSegment != null && dynamicPathSegment.Identifier == base.Identifier && dynamicPathSegment.EdmType == this.EdmType && dynamicPathSegment.TargetEdmNavigationSource == base.TargetEdmNavigationSource && dynamicPathSegment.SingleResult == base.SingleResult;
		}
	}
}
