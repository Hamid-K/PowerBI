using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000165 RID: 357
	public sealed class ValueSegment : ODataPathSegment
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0002BAB4 File Offset: 0x00029CB4
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

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002BB12 File Offset: 0x00029D12
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002BB1A File Offset: 0x00029D1A
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002BB2F File Offset: 0x00029D2F
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002BB44 File Offset: 0x00029D44
		internal override bool Equals(ODataPathSegment other)
		{
			ValueSegment valueSegment = other as ValueSegment;
			return valueSegment != null && valueSegment.EdmType == this.edmType;
		}

		// Token: 0x040007AE RID: 1966
		private readonly IEdmType edmType;
	}
}
