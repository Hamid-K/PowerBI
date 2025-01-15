using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000105 RID: 261
	public sealed class AnnotationSegment : ODataPathSegment
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x00025D04 File Offset: 0x00023F04
		public AnnotationSegment(IEdmTerm term)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTerm>(term, "term");
			this.term = term;
			base.Identifier = term.Name;
			base.TargetEdmType = term.Type.Definition;
			base.SingleResult = !term.Type.IsCollection();
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00025D5B File Offset: 0x00023F5B
		public IEdmTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00025D63 File Offset: 0x00023F63
		public override IEdmType EdmType
		{
			get
			{
				return this.term.Type.Definition;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00025D75 File Offset: 0x00023F75
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00025D8A File Offset: 0x00023F8A
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00025DA0 File Offset: 0x00023FA0
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			AnnotationSegment annotationSegment = other as AnnotationSegment;
			return annotationSegment != null && annotationSegment.term == this.term;
		}

		// Token: 0x0400076D RID: 1901
		private readonly IEdmTerm term;
	}
}
