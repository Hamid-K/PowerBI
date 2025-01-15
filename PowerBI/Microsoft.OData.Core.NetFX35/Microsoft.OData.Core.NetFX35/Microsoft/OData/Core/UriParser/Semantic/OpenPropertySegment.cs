using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000250 RID: 592
	public sealed class OpenPropertySegment : ODataPathSegment
	{
		// Token: 0x06001500 RID: 5376 RVA: 0x0004A6B7 File Offset: 0x000488B7
		public OpenPropertySegment(string propertyName)
		{
			this.propertyName = propertyName;
			base.Identifier = propertyName;
			base.TargetEdmType = null;
			base.TargetKind = RequestTargetKind.OpenProperty;
			base.SingleResult = true;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0004A6E3 File Offset: 0x000488E3
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0004A6EB File Offset: 0x000488EB
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0004A6EE File Offset: 0x000488EE
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0004A702 File Offset: 0x00048902
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0004A718 File Offset: 0x00048918
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OpenPropertySegment openPropertySegment = other as OpenPropertySegment;
			return openPropertySegment != null && openPropertySegment.PropertyName == this.PropertyName;
		}

		// Token: 0x040008C2 RID: 2242
		private readonly string propertyName;
	}
}
