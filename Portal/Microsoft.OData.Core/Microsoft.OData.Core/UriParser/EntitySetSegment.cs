using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000180 RID: 384
	public sealed class EntitySetSegment : ODataPathSegment
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x00038E88 File Offset: 0x00037088
		public EntitySetSegment(IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntitySet>(entitySet, "entitySet");
			this.entitySet = entitySet;
			this.type = new EdmCollectionType(new EdmEntityTypeReference(this.entitySet.EntityType(), false));
			base.TargetEdmNavigationSource = entitySet;
			base.TargetEdmType = entitySet.EntityType();
			base.TargetKind = RequestTargetKind.Resource;
			base.SingleResult = false;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00038EEB File Offset: 0x000370EB
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00038EF3 File Offset: 0x000370F3
		public override IEdmType EdmType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00038EFB File Offset: 0x000370FB
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00038F10 File Offset: 0x00037110
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00038F28 File Offset: 0x00037128
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			EntitySetSegment entitySetSegment = other as EntitySetSegment;
			return entitySetSegment != null && entitySetSegment.EntitySet == this.EntitySet;
		}

		// Token: 0x04000883 RID: 2179
		private readonly IEdmEntitySet entitySet;

		// Token: 0x04000884 RID: 2180
		private readonly IEdmType type;
	}
}
