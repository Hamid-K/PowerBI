using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016D RID: 365
	internal class CsdlSemanticsDirectValueAnnotation : CsdlSemanticsElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x0001A750 File Offset: 0x00018950
		public CsdlSemanticsDirectValueAnnotation(CsdlDirectValueAnnotation annotation, CsdlSemanticsModel model)
			: base(annotation)
		{
			this.annotation = annotation;
			this.model = model;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001A772 File Offset: 0x00018972
		public override CsdlElement Element
		{
			get
			{
				return this.annotation;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001A77A File Offset: 0x0001897A
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001A782 File Offset: 0x00018982
		public string NamespaceUri
		{
			get
			{
				return this.annotation.NamespaceName;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001A78F File Offset: 0x0001898F
		public string Name
		{
			get
			{
				return this.annotation.Name;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001A79C File Offset: 0x0001899C
		public object Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDirectValueAnnotation.ComputeValueFunc, null);
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001A7B0 File Offset: 0x000189B0
		private IEdmValue ComputeValue()
		{
			IEdmStringValue edmStringValue = new EdmStringConstant(new EdmStringTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), false), this.annotation.Value);
			edmStringValue.SetIsSerializedAsElement(this.model, !this.annotation.IsAttribute);
			return edmStringValue;
		}

		// Token: 0x040005C1 RID: 1473
		private readonly CsdlDirectValueAnnotation annotation;

		// Token: 0x040005C2 RID: 1474
		private readonly CsdlSemanticsModel model;

		// Token: 0x040005C3 RID: 1475
		private readonly Cache<CsdlSemanticsDirectValueAnnotation, IEdmValue> valueCache = new Cache<CsdlSemanticsDirectValueAnnotation, IEdmValue>();

		// Token: 0x040005C4 RID: 1476
		private static readonly Func<CsdlSemanticsDirectValueAnnotation, IEdmValue> ComputeValueFunc = (CsdlSemanticsDirectValueAnnotation me) => me.ComputeValue();
	}
}
