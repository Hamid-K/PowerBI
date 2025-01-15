using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000231 RID: 561
	public sealed class CountSegment : ODataPathSegment
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x00049359 File Offset: 0x00047559
		private CountSegment()
		{
			base.Identifier = "$count";
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.PrimitiveValue;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0004937A File Offset: 0x0004757A
		public override IEdmType EdmType
		{
			get
			{
				return EdmCoreModel.Instance.GetInt32(false).Definition;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004938C File Offset: 0x0004758C
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000493A0 File Offset: 0x000475A0
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x000493B4 File Offset: 0x000475B4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is CountSegment;
		}

		// Token: 0x04000883 RID: 2179
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "CountSegment is immutable")]
		public static readonly CountSegment Instance = new CountSegment();
	}
}
