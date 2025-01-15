using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010E RID: 270
	public sealed class ReferenceSegment : ODataPathSegment
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x0002635C File Offset: 0x0002455C
		public ReferenceSegment(IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationSource>(navigationSource, "navigationSource");
			base.Identifier = "$ref";
			base.SingleResult = navigationSource.Type.TypeKind != EdmTypeKind.Collection;
			base.TargetEdmNavigationSource = navigationSource;
			base.TargetKind = RequestTargetKind.Resource;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000263AB File Offset: 0x000245AB
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000263C0 File Offset: 0x000245C0
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x000263D8 File Offset: 0x000245D8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			ReferenceSegment referenceSegment = other as ReferenceSegment;
			return referenceSegment != null && referenceSegment.TargetEdmNavigationSource == base.TargetEdmNavigationSource;
		}
	}
}
