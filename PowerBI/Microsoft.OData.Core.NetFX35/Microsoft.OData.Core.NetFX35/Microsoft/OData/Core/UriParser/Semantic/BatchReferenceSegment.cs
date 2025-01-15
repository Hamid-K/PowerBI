using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000224 RID: 548
	public sealed class BatchReferenceSegment : ODataPathSegment
	{
		// Token: 0x060013D6 RID: 5078 RVA: 0x00048BDC File Offset: 0x00046DDC
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00048C74 File Offset: 0x00046E74
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00048C7C File Offset: 0x00046E7C
		public IEdmEntitySetBase EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00048C84 File Offset: 0x00046E84
		public string ContentId
		{
			get
			{
				return this.contentId;
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00048C8C File Offset: 0x00046E8C
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00048CA0 File Offset: 0x00046EA0
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00048CB4 File Offset: 0x00046EB4
		internal override bool Equals(ODataPathSegment other)
		{
			BatchReferenceSegment batchReferenceSegment = other as BatchReferenceSegment;
			return batchReferenceSegment != null && batchReferenceSegment.EdmType == this.edmType && batchReferenceSegment.EntitySet == this.entitySet && batchReferenceSegment.ContentId == this.contentId;
		}

		// Token: 0x04000861 RID: 2145
		private readonly IEdmType edmType;

		// Token: 0x04000862 RID: 2146
		private readonly IEdmEntitySetBase entitySet;

		// Token: 0x04000863 RID: 2147
		private readonly string contentId;
	}
}
