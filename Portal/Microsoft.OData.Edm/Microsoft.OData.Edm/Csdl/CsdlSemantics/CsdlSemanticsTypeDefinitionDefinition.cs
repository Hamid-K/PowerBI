using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000169 RID: 361
	internal class CsdlSemanticsTypeDefinitionDefinition : CsdlSemanticsTypeDefinition, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0001B578 File Offset: 0x00019778
		public CsdlSemanticsTypeDefinitionDefinition(CsdlSemanticsSchema context, CsdlTypeDefinition typeDefinition)
			: base(typeDefinition)
		{
			this.context = context;
			this.typeDefinition = typeDefinition;
			CsdlSemanticsSchema csdlSemanticsSchema = this.context;
			string text = ((csdlSemanticsSchema != null) ? csdlSemanticsSchema.Namespace : null);
			CsdlTypeDefinition csdlTypeDefinition = this.typeDefinition;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlTypeDefinition != null) ? csdlTypeDefinition.Name : null);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0001B5D4 File Offset: 0x000197D4
		IEdmPrimitiveType IEdmTypeDefinition.UnderlyingType
		{
			get
			{
				return this.underlyingTypeCache.GetValue(this, CsdlSemanticsTypeDefinitionDefinition.ComputeUnderlyingTypeFunc, null);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0000268E File Offset: 0x0000088E
		EdmSchemaElementKind IEdmSchemaElement.SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001B5E8 File Offset: 0x000197E8
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0001B5F5 File Offset: 0x000197F5
		string IEdmNamedElement.Name
		{
			get
			{
				return this.typeDefinition.Name;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001B602 File Offset: 0x00019802
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00003AFB File Offset: 0x00001CFB
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.TypeDefinition;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001B60A File Offset: 0x0001980A
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001B617 File Offset: 0x00019817
		public override CsdlElement Element
		{
			get
			{
				return this.typeDefinition;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001B61F File Offset: 0x0001981F
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001B634 File Offset: 0x00019834
		private IEdmPrimitiveType ComputeUnderlyingType()
		{
			if (this.typeDefinition.UnderlyingTypeName == null)
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
			EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(this.typeDefinition.UnderlyingTypeName);
			if (primitiveTypeKind == EdmPrimitiveTypeKind.None)
			{
				return new UnresolvedPrimitiveType(this.typeDefinition.UnderlyingTypeName, base.Location);
			}
			return EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x040005EB RID: 1515
		private readonly CsdlSemanticsSchema context;

		// Token: 0x040005EC RID: 1516
		private readonly CsdlTypeDefinition typeDefinition;

		// Token: 0x040005ED RID: 1517
		private readonly string fullName;

		// Token: 0x040005EE RID: 1518
		private readonly Cache<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType> underlyingTypeCache = new Cache<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType>();

		// Token: 0x040005EF RID: 1519
		private static readonly Func<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType> ComputeUnderlyingTypeFunc = (CsdlSemanticsTypeDefinitionDefinition me) => me.ComputeUnderlyingType();
	}
}
