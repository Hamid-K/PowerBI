using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B3 RID: 435
	public sealed class ValueSegment : ODataPathSegment
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x0003BB70 File Offset: 0x00039D70
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

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0003BBCE File Offset: 0x00039DCE
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0003BBD6 File Offset: 0x00039DD6
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0003BBEB File Offset: 0x00039DEB
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0003BC00 File Offset: 0x00039E00
		internal override bool Equals(ODataPathSegment other)
		{
			ValueSegment valueSegment = other as ValueSegment;
			return valueSegment != null && valueSegment.EdmType == this.edmType;
		}

		// Token: 0x040008FC RID: 2300
		private readonly IEdmType edmType;
	}
}
