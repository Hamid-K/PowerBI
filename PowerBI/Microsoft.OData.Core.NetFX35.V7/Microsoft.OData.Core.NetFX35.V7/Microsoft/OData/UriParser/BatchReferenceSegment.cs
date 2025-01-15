using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000128 RID: 296
	public sealed class BatchReferenceSegment : ODataPathSegment
	{
		// Token: 0x06000D8B RID: 3467 RVA: 0x0002891C File Offset: 0x00026B1C
		public BatchReferenceSegment(string contentId, IEdmType edmType, IEdmEntitySetBase entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(edmType, "resultingType");
			ExceptionUtils.CheckArgumentNotNull<string>(contentId, "contentId");
			if (!ODataPathParser.ContentIdRegex.IsMatch(contentId))
			{
				throw new ODataException(Strings.BatchReferenceSegment_InvalidContentID(contentId));
			}
			this.edmType = edmType;
			this.entitySet = entitySet;
			this.contentId = contentId;
			base.Identifier = this.ContentId;
			base.TargetEdmType = edmType;
			base.TargetEdmNavigationSource = this.EntitySet;
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.Resource;
			if (entitySet != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(edmType, entitySet.EntityType(), "BatchReferenceSegments");
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x000289B6 File Offset: 0x00026BB6
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x000289BE File Offset: 0x00026BBE
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000289C6 File Offset: 0x00026BC6
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000289CE File Offset: 0x00026BCE
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000289E3 File Offset: 0x00026BE3
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000289F8 File Offset: 0x00026BF8
		internal override bool Equals(ODataPathSegment other)
		{
			BatchReferenceSegment batchReferenceSegment = other as BatchReferenceSegment;
			return batchReferenceSegment != null && batchReferenceSegment.EdmType == this.edmType && batchReferenceSegment.EntitySet == this.entitySet && batchReferenceSegment.ContentId == this.contentId;
		}

		// Token: 0x0400072C RID: 1836
		private readonly IEdmType edmType;

		// Token: 0x0400072D RID: 1837
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x0400072E RID: 1838
		private readonly string contentId;
	}
}
