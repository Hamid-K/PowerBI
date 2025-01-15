using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017C RID: 380
	internal class CsdlSemanticsDirectValueAnnotation : CsdlSemanticsElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x0001C858 File Offset: 0x0001AA58
		public CsdlSemanticsDirectValueAnnotation(CsdlDirectValueAnnotation annotation, CsdlSemanticsModel model)
			: base(annotation)
		{
			this.annotation = annotation;
			this.model = model;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001C87A File Offset: 0x0001AA7A
		public override CsdlElement Element
		{
			get
			{
				return this.annotation;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001C882 File Offset: 0x0001AA82
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001C88A File Offset: 0x0001AA8A
		public string NamespaceUri
		{
			get
			{
				return this.annotation.NamespaceName;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001C897 File Offset: 0x0001AA97
		public string Name
		{
			get
			{
				return this.annotation.Name;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		public object Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDirectValueAnnotation.ComputeValueFunc, null);
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		private IEdmValue ComputeValue()
		{
			IEdmStringValue edmStringValue = new EdmStringConstant(new EdmStringTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), false), this.annotation.Value);
			edmStringValue.SetIsSerializedAsElement(this.model, !this.annotation.IsAttribute);
			return edmStringValue;
		}

		// Token: 0x0400063C RID: 1596
		private readonly CsdlDirectValueAnnotation annotation;

		// Token: 0x0400063D RID: 1597
		private readonly CsdlSemanticsModel model;

		// Token: 0x0400063E RID: 1598
		private readonly Cache<CsdlSemanticsDirectValueAnnotation, IEdmValue> valueCache = new Cache<CsdlSemanticsDirectValueAnnotation, IEdmValue>();

		// Token: 0x0400063F RID: 1599
		private static readonly Func<CsdlSemanticsDirectValueAnnotation, IEdmValue> ComputeValueFunc = (CsdlSemanticsDirectValueAnnotation me) => me.ComputeValue();
	}
}
