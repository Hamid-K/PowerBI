using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000162 RID: 354
	public sealed class TypeSegment : ODataPathSegment
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x0002B92C File Offset: 0x00029B2C
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

		// Token: 0x06000F2E RID: 3886 RVA: 0x0002B950 File Offset: 0x00029B50
		public TypeSegment(IEdmType actualType, IEdmType expectedType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(actualType, "actualType");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(expectedType, "expectedType");
			this.edmType = actualType;
			this.navigationSource = navigationSource;
			base.TargetEdmType = expectedType;
			base.TargetEdmNavigationSource = navigationSource;
			if (navigationSource != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(actualType, expectedType, "TypeSegments");
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002B9A6 File Offset: 0x00029BA6
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0002B9AE File Offset: 0x00029BAE
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0002B9B6 File Offset: 0x00029BB6
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002B9CB File Offset: 0x00029BCB
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			TypeSegment typeSegment = other as TypeSegment;
			return typeSegment != null && typeSegment.EdmType == this.EdmType;
		}

		// Token: 0x040007A7 RID: 1959
		private readonly IEdmType edmType;

		// Token: 0x040007A8 RID: 1960
		private readonly IEdmNavigationSource navigationSource;
	}
}
