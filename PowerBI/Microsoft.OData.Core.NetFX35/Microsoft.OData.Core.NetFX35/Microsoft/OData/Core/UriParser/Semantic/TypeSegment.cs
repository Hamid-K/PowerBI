using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000267 RID: 615
	public sealed class TypeSegment : ODataPathSegment
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x0004BD90 File Offset: 0x00049F90
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		public TypeSegment(IEdmType edmType, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(edmType, "type");
			this.edmType = edmType;
			this.navigationSource = navigationSource;
			base.TargetEdmType = edmType;
			base.TargetEdmNavigationSource = navigationSource;
			if (navigationSource != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(edmType, navigationSource.EntityType(), "TypeSegments");
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0004BDDE File Offset: 0x00049FDE
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0004BDE6 File Offset: 0x00049FE6
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004BDEE File Offset: 0x00049FEE
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004BE02 File Offset: 0x0004A002
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004BE18 File Offset: 0x0004A018
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			TypeSegment typeSegment = other as TypeSegment;
			return typeSegment != null && typeSegment.EdmType == this.EdmType;
		}

		// Token: 0x04000900 RID: 2304
		private readonly IEdmType edmType;

		// Token: 0x04000901 RID: 2305
		private readonly IEdmNavigationSource navigationSource;
	}
}
