using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016D RID: 365
	public sealed class BatchReferenceSegment : ODataPathSegment
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x00038250 File Offset: 0x00036450
		public BatchReferenceSegment(string contentId, IEdmType edmType, IEdmEntitySetBase entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(edmType, "edmType");
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

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x000382EA File Offset: 0x000364EA
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x000382F2 File Offset: 0x000364F2
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x000382FA File Offset: 0x000364FA
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00038302 File Offset: 0x00036502
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00038317 File Offset: 0x00036517
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0003832C File Offset: 0x0003652C
		internal override bool Equals(ODataPathSegment other)
		{
			BatchReferenceSegment batchReferenceSegment = other as BatchReferenceSegment;
			return batchReferenceSegment != null && batchReferenceSegment.EdmType == this.edmType && batchReferenceSegment.EntitySet == this.entitySet && batchReferenceSegment.ContentId == this.contentId;
		}

		// Token: 0x0400084D RID: 2125
		private readonly IEdmType edmType;

		// Token: 0x0400084E RID: 2126
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x0400084F RID: 2127
		private readonly string contentId;
	}
}
