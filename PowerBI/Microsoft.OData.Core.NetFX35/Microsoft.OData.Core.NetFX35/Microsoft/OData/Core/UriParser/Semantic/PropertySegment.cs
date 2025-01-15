using System;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000259 RID: 601
	public sealed class PropertySegment : ODataPathSegment
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x0004AFB0 File Offset: 0x000491B0
		public PropertySegment(IEdmStructuralProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuralProperty>(property, "property");
			this.property = property;
			base.Identifier = property.Name;
			base.TargetEdmType = property.Type.Definition;
			base.SingleResult = !property.Type.IsCollection();
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0004B006 File Offset: 0x00049206
		public IEdmStructuralProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0004B00E File Offset: 0x0004920E
		public override IEdmType EdmType
		{
			get
			{
				return this.Property.Type.Definition;
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004B020 File Offset: 0x00049220
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0004B034 File Offset: 0x00049234
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0004B048 File Offset: 0x00049248
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			PropertySegment propertySegment = other as PropertySegment;
			return propertySegment != null && propertySegment.Property == this.Property;
		}

		// Token: 0x040008DB RID: 2267
		private readonly IEdmStructuralProperty property;
	}
}
