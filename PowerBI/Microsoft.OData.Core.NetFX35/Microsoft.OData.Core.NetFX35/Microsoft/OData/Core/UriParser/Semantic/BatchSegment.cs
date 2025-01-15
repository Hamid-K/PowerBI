using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000225 RID: 549
	public sealed class BatchSegment : ODataPathSegment
	{
		// Token: 0x060013DD RID: 5085 RVA: 0x00048CFA File Offset: 0x00046EFA
		private BatchSegment()
		{
			base.Identifier = "$batch";
			base.TargetKind = RequestTargetKind.Batch;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00048D15 File Offset: 0x00046F15
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00048D18 File Offset: 0x00046F18
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00048D2C File Offset: 0x00046F2C
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00048D40 File Offset: 0x00046F40
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is BatchSegment;
		}

		// Token: 0x04000864 RID: 2148
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BatchSegment is immutable")]
		public static readonly BatchSegment Instance = new BatchSegment();
	}
}
