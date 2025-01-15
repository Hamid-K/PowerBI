using System;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200026A RID: 618
	public sealed class ValueSegment : ODataPathSegment
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x0004BEE4 File Offset: 0x0004A0E4
		public ValueSegment(IEdmType previousType)
		{
			base.Identifier = "$value";
			base.SingleResult = true;
			if (previousType is IEdmCollectionType)
			{
				throw new ODataException(Strings.PathParser_CannotUseValueOnCollection);
			}
			if (previousType is IEdmEntityType)
			{
				this.edmType = EdmCoreModel.Instance.GetStream(false).Definition;
				return;
			}
			this.edmType = previousType;
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0004BF42 File Offset: 0x0004A142
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0004BF4A File Offset: 0x0004A14A
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0004BF5E File Offset: 0x0004A15E
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004BF74 File Offset: 0x0004A174
		internal override bool Equals(ODataPathSegment other)
		{
			ValueSegment valueSegment = other as ValueSegment;
			return valueSegment != null && valueSegment.EdmType == this.edmType;
		}

		// Token: 0x04000907 RID: 2311
		private readonly IEdmType edmType;
	}
}
