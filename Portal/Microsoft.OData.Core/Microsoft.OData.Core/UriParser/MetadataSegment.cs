using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000188 RID: 392
	public sealed class MetadataSegment : ODataPathSegment
	{
		// Token: 0x06001342 RID: 4930 RVA: 0x00039458 File Offset: 0x00037658
		private MetadataSegment()
		{
			base.Identifier = "$metadata";
			base.TargetKind = RequestTargetKind.Metadata;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0000360D File Offset: 0x0000180D
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00039472 File Offset: 0x00037672
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00039487 File Offset: 0x00037687
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0003949C File Offset: 0x0003769C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is MetadataSegment;
		}

		// Token: 0x040008A0 RID: 2208
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "MetadataSegment is immutable")]
		public static readonly MetadataSegment Instance = new MetadataSegment();
	}
}
