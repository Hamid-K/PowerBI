using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B0 RID: 432
	public sealed class TypeSegment : ODataPathSegment
	{
		// Token: 0x06001464 RID: 5220 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		public TypeSegment(IEdmType actualType, IEdmNavigationSource navigationSource)
		{
			IEdmType edmType2;
			if (navigationSource != null)
			{
				IEdmType edmType = navigationSource.EntityType();
				edmType2 = edmType;
			}
			else
			{
				edmType2 = actualType;
			}
			this..ctor(actualType, edmType2, navigationSource);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003B9FC File Offset: 0x00039BFC
		public TypeSegment(IEdmType actualType, IEdmType expectedType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(actualType, "actualType");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(expectedType, "expectedType");
			this.edmType = actualType;
			this.navigationSource = navigationSource;
			this.expectedType = expectedType;
			base.TargetEdmType = expectedType;
			base.TargetEdmNavigationSource = navigationSource;
			if (navigationSource != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(actualType, expectedType, "TypeSegments");
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0003BA59 File Offset: 0x00039C59
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0003BA61 File Offset: 0x00039C61
		public IEdmType ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0003BA69 File Offset: 0x00039C69
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0003BA71 File Offset: 0x00039C71
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0003BA86 File Offset: 0x00039C86
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0003BA9C File Offset: 0x00039C9C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			TypeSegment typeSegment = other as TypeSegment;
			return typeSegment != null && typeSegment.EdmType == this.EdmType;
		}

		// Token: 0x040008F4 RID: 2292
		private readonly IEdmType edmType;

		// Token: 0x040008F5 RID: 2293
		private readonly IEdmType expectedType;

		// Token: 0x040008F6 RID: 2294
		private readonly IEdmNavigationSource navigationSource;
	}
}
