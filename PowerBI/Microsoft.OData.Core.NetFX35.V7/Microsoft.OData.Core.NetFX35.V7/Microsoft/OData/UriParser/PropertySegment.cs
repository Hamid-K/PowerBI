using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000152 RID: 338
	public sealed class PropertySegment : ODataPathSegment
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002ACE4 File Offset: 0x00028EE4
		public PropertySegment(IEdmStructuralProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuralProperty>(property, "property");
			this.property = property;
			base.Identifier = property.Name;
			base.TargetEdmType = property.Type.Definition;
			base.SingleResult = !property.Type.IsCollection();
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0002AD3B File Offset: 0x00028F3B
		public IEdmStructuralProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0002AD43 File Offset: 0x00028F43
		public override IEdmType EdmType
		{
			get
			{
				return this.Property.Type.Definition;
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002AD55 File Offset: 0x00028F55
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002AD6A File Offset: 0x00028F6A
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002AD80 File Offset: 0x00028F80
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			PropertySegment propertySegment = other as PropertySegment;
			return propertySegment != null && propertySegment.Property == this.Property;
		}

		// Token: 0x0400078F RID: 1935
		private readonly IEdmStructuralProperty property;
	}
}
