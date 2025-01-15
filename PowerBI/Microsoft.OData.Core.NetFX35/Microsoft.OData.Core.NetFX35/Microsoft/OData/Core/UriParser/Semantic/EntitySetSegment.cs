using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200023C RID: 572
	public sealed class EntitySetSegment : ODataPathSegment
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x00049898 File Offset: 0x00047A98
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x000498FA File Offset: 0x00047AFA
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00049902 File Offset: 0x00047B02
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		public override IEdmType EdmType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0004990A File Offset: 0x00047B0A
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0004991E File Offset: 0x00047B1E
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00049934 File Offset: 0x00047B34
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			EntitySetSegment entitySetSegment = other as EntitySetSegment;
			return entitySetSegment != null && entitySetSegment.EntitySet == this.EntitySet;
		}

		// Token: 0x0400089B RID: 2203
		private readonly IEdmEntitySet entitySet;

		// Token: 0x0400089C RID: 2204
		private readonly IEdmType type;
	}
}
