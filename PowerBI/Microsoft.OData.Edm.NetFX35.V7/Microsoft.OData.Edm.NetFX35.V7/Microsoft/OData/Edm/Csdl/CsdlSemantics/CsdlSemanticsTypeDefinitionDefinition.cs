using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015A RID: 346
	internal class CsdlSemanticsTypeDefinitionDefinition : CsdlSemanticsTypeDefinition, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x000194B8 File Offset: 0x000176B8
		public CsdlSemanticsTypeDefinitionDefinition(CsdlSemanticsSchema context, CsdlTypeDefinition typeDefinition)
			: base(typeDefinition)
		{
			this.context = context;
			this.typeDefinition = typeDefinition;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000194DA File Offset: 0x000176DA
		IEdmPrimitiveType IEdmTypeDefinition.UnderlyingType
		{
			get
			{
				return this.underlyingTypeCache.GetValue(this, CsdlSemanticsTypeDefinitionDefinition.ComputeUnderlyingTypeFunc, null);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00008D76 File Offset: 0x00006F76
		EdmSchemaElementKind IEdmSchemaElement.SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000194EE File Offset: 0x000176EE
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000194FB File Offset: 0x000176FB
		string IEdmNamedElement.Name
		{
			get
			{
				return this.typeDefinition.Name;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0000C558 File Offset: 0x0000A758
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.TypeDefinition;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00019508 File Offset: 0x00017708
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00019515 File Offset: 0x00017715
		public override CsdlElement Element
		{
			get
			{
				return this.typeDefinition;
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001951D File Offset: 0x0001771D
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00019534 File Offset: 0x00017734
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

		// Token: 0x04000571 RID: 1393
		private readonly CsdlSemanticsSchema context;

		// Token: 0x04000572 RID: 1394
		private readonly CsdlTypeDefinition typeDefinition;

		// Token: 0x04000573 RID: 1395
		private readonly Cache<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType> underlyingTypeCache = new Cache<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType>();

		// Token: 0x04000574 RID: 1396
		private static readonly Func<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType> ComputeUnderlyingTypeFunc = (CsdlSemanticsTypeDefinitionDefinition me) => me.ComputeUnderlyingType();
	}
}
