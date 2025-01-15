using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000244 RID: 580
	public sealed class MetadataSegment : ODataPathSegment
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x00049CED File Offset: 0x00047EED
		private MetadataSegment()
		{
			base.Identifier = "$metadata";
			base.TargetKind = RequestTargetKind.Metadata;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00049D07 File Offset: 0x00047F07
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00049D0A File Offset: 0x00047F0A
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00049D1E File Offset: 0x00047F1E
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00049D32 File Offset: 0x00047F32
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is MetadataSegment;
		}

		// Token: 0x040008B2 RID: 2226
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "MetadataSegment is immutable")]
		public static readonly MetadataSegment Instance = new MetadataSegment();
	}
}
