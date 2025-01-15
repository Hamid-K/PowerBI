using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200005D RID: 93
	internal class CsdlSemanticsTypeDefinitionDefinition : CsdlSemanticsTypeDefinition, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00003CD6 File Offset: 0x00001ED6
		public CsdlSemanticsTypeDefinitionDefinition(CsdlSemanticsSchema context, CsdlTypeDefinition typeDefinition)
			: base(typeDefinition)
		{
			this.context = context;
			this.typeDefinition = typeDefinition;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003CF8 File Offset: 0x00001EF8
		IEdmPrimitiveType IEdmTypeDefinition.UnderlyingType
		{
			get
			{
				return this.underlyingTypeCache.GetValue(this, CsdlSemanticsTypeDefinitionDefinition.ComputeUnderlyingTypeFunc, null);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003D0C File Offset: 0x00001F0C
		EdmSchemaElementKind IEdmSchemaElement.SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00003D0F File Offset: 0x00001F0F
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003D1C File Offset: 0x00001F1C
		string IEdmNamedElement.Name
		{
			get
			{
				return this.typeDefinition.Name;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003D29 File Offset: 0x00001F29
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.TypeDefinition;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003D2C File Offset: 0x00001F2C
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003D39 File Offset: 0x00001F39
		public override CsdlElement Element
		{
			get
			{
				return this.typeDefinition;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003D41 File Offset: 0x00001F41
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003D58 File Offset: 0x00001F58
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

		// Token: 0x0400006E RID: 110
		private readonly CsdlSemanticsSchema context;

		// Token: 0x0400006F RID: 111
		private readonly CsdlTypeDefinition typeDefinition;

		// Token: 0x04000070 RID: 112
		private readonly Cache<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType> underlyingTypeCache = new Cache<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType>();

		// Token: 0x04000071 RID: 113
		private static readonly Func<CsdlSemanticsTypeDefinitionDefinition, IEdmPrimitiveType> ComputeUnderlyingTypeFunc = (CsdlSemanticsTypeDefinitionDefinition me) => me.ComputeUnderlyingType();
	}
}
