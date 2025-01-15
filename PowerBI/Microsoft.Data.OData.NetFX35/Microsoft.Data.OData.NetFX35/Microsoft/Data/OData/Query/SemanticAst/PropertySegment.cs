using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000076 RID: 118
	public sealed class PropertySegment : ODataPathSegment
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A80C File Offset: 0x00008A0C
		public PropertySegment(IEdmStructuralProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuralProperty>(property, "property");
			this.property = property;
			base.Identifier = property.Name;
			base.TargetEdmType = property.Type.Definition;
			base.SingleResult = !property.Type.IsCollection();
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A862 File Offset: 0x00008A62
		public IEdmStructuralProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000A86A File Offset: 0x00008A6A
		public override IEdmType EdmType
		{
			get
			{
				return this.Property.Type.Definition;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A87C File Offset: 0x00008A7C
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A890 File Offset: 0x00008A90
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			PropertySegment propertySegment = other as PropertySegment;
			return propertySegment != null && propertySegment.Property == this.Property;
		}

		// Token: 0x040000C0 RID: 192
		private readonly IEdmStructuralProperty property;
	}
}
