using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019E RID: 414
	public sealed class PropertySegment : ODataPathSegment
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x0003AC2C File Offset: 0x00038E2C
		public PropertySegment(IEdmStructuralProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuralProperty>(property, "property");
			this.property = property;
			base.Identifier = property.Name;
			base.TargetEdmType = property.Type.Definition;
			base.SingleResult = !property.Type.IsCollection();
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0003AC83 File Offset: 0x00038E83
		public IEdmStructuralProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0003AC8B File Offset: 0x00038E8B
		public override IEdmType EdmType
		{
			get
			{
				return this.Property.Type.Definition;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0003AC9D File Offset: 0x00038E9D
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0003ACB2 File Offset: 0x00038EB2
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			PropertySegment propertySegment = other as PropertySegment;
			return propertySegment != null && propertySegment.Property == this.Property;
		}

		// Token: 0x040008D3 RID: 2259
		private readonly IEdmStructuralProperty property;
	}
}
