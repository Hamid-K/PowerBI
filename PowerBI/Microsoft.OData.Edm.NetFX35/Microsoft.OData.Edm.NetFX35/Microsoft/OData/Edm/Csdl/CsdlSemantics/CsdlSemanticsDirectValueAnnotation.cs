using System;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000093 RID: 147
	internal class CsdlSemanticsDirectValueAnnotation : CsdlSemanticsElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00006391 File Offset: 0x00004591
		public CsdlSemanticsDirectValueAnnotation(CsdlDirectValueAnnotation annotation, CsdlSemanticsModel model)
			: base(annotation)
		{
			this.annotation = annotation;
			this.model = model;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000063B3 File Offset: 0x000045B3
		public override CsdlElement Element
		{
			get
			{
				return this.annotation;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000063BB File Offset: 0x000045BB
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000063C3 File Offset: 0x000045C3
		public string NamespaceUri
		{
			get
			{
				return this.annotation.NamespaceName;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000063D0 File Offset: 0x000045D0
		public string Name
		{
			get
			{
				return this.annotation.Name;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000063DD File Offset: 0x000045DD
		public object Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDirectValueAnnotation.ComputeValueFunc, null);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000063F4 File Offset: 0x000045F4
		private IEdmValue ComputeValue()
		{
			IEdmStringValue edmStringValue = new EdmStringConstant(new EdmStringTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), false), this.annotation.Value);
			edmStringValue.SetIsSerializedAsElement(this.model, !this.annotation.IsAttribute);
			return edmStringValue;
		}

		// Token: 0x040000F9 RID: 249
		private readonly CsdlDirectValueAnnotation annotation;

		// Token: 0x040000FA RID: 250
		private readonly CsdlSemanticsModel model;

		// Token: 0x040000FB RID: 251
		private readonly Cache<CsdlSemanticsDirectValueAnnotation, IEdmValue> valueCache = new Cache<CsdlSemanticsDirectValueAnnotation, IEdmValue>();

		// Token: 0x040000FC RID: 252
		private static readonly Func<CsdlSemanticsDirectValueAnnotation, IEdmValue> ComputeValueFunc = (CsdlSemanticsDirectValueAnnotation me) => me.ComputeValue();
	}
}
