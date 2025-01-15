using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000136 RID: 310
	public sealed class EntitySetSegment : ODataPathSegment
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002923C File Offset: 0x0002743C
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

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002929F File Offset: 0x0002749F
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x000292A7 File Offset: 0x000274A7
		public override IEdmType EdmType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000292AF File Offset: 0x000274AF
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000292C4 File Offset: 0x000274C4
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x000292DC File Offset: 0x000274DC
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			EntitySetSegment entitySetSegment = other as EntitySetSegment;
			return entitySetSegment != null && entitySetSegment.EntitySet == this.EntitySet;
		}

		// Token: 0x0400074F RID: 1871
		private readonly IEdmEntitySet entitySet;

		// Token: 0x04000750 RID: 1872
		private readonly IEdmType type;
	}
}
