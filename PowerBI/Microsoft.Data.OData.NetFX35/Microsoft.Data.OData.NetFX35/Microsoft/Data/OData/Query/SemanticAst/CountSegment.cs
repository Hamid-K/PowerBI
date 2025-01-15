using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000068 RID: 104
	public sealed class CountSegment : ODataPathSegment
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000A153 File Offset: 0x00008353
		private CountSegment()
		{
			base.Identifier = "$count";
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.PrimitiveValue;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000A174 File Offset: 0x00008374
		public override IEdmType EdmType
		{
			get
			{
				return EdmCoreModel.Instance.GetInt32(false).Definition;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A186 File Offset: 0x00008386
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A19A File Offset: 0x0000839A
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A1AE File Offset: 0x000083AE
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is CountSegment;
		}

		// Token: 0x040000A9 RID: 169
		public static readonly CountSegment Instance = new CountSegment();
	}
}
