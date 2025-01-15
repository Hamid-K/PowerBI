using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000096 RID: 150
	internal class CsdlSemanticsEnumTypeDefinition : CsdlSemanticsTypeDefinition, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x06000289 RID: 649 RVA: 0x000065A1 File Offset: 0x000047A1
		public CsdlSemanticsEnumTypeDefinition(CsdlSemanticsSchema context, CsdlEnumType enumeration)
			: base(enumeration)
		{
			this.Context = context;
			this.enumeration = enumeration;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000065CE File Offset: 0x000047CE
		IEdmPrimitiveType IEdmEnumType.UnderlyingType
		{
			get
			{
				return this.underlyingTypeCache.GetValue(this, CsdlSemanticsEnumTypeDefinition.ComputeUnderlyingTypeFunc, null);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000065E2 File Offset: 0x000047E2
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return this.membersCache.GetValue(this, CsdlSemanticsEnumTypeDefinition.ComputeMembersFunc, null);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000065F6 File Offset: 0x000047F6
		bool IEdmEnumType.IsFlags
		{
			get
			{
				return this.enumeration.IsFlags;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00006603 File Offset: 0x00004803
		EdmSchemaElementKind IEdmSchemaElement.SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00006606 File Offset: 0x00004806
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00006613 File Offset: 0x00004813
		string IEdmNamedElement.Name
		{
			get
			{
				return this.enumeration.Name;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00006620 File Offset: 0x00004820
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00006623 File Offset: 0x00004823
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00006630 File Offset: 0x00004830
		public override CsdlElement Element
		{
			get
			{
				return this.enumeration;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00006638 File Offset: 0x00004838
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00006640 File Offset: 0x00004840
		public CsdlSemanticsSchema Context { get; private set; }

		// Token: 0x06000295 RID: 661 RVA: 0x00006649 File Offset: 0x00004849
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00006660 File Offset: 0x00004860
		private IEdmPrimitiveType ComputeUnderlyingType()
		{
			if (this.enumeration.UnderlyingTypeName == null)
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
			EdmPrimitiveTypeKind primitiveTypeKind = EdmCoreModel.Instance.GetPrimitiveTypeKind(this.enumeration.UnderlyingTypeName);
			if (primitiveTypeKind == EdmPrimitiveTypeKind.None)
			{
				return new UnresolvedPrimitiveType(this.enumeration.UnderlyingTypeName, base.Location);
			}
			return EdmCoreModel.Instance.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000066C4 File Offset: 0x000048C4
		private IEnumerable<IEdmEnumMember> ComputeMembers()
		{
			List<IEdmEnumMember> list = new List<IEdmEnumMember>();
			long num = -1L;
			foreach (CsdlEnumMember csdlEnumMember in this.enumeration.Members)
			{
				long? num2 = default(long?);
				IEdmEnumMember edmEnumMember;
				if (csdlEnumMember.Value == null)
				{
					if (num < 9223372036854775807L)
					{
						num2 = new long?(num + 1L);
						num = num2.Value;
						csdlEnumMember.Value = num2;
						edmEnumMember = new CsdlSemanticsEnumMember(this, csdlEnumMember);
					}
					else
					{
						edmEnumMember = new CsdlSemanticsEnumMember(this, csdlEnumMember);
					}
					edmEnumMember.SetIsValueExplicit(this.Model, new bool?(false));
				}
				else
				{
					num = csdlEnumMember.Value.Value;
					edmEnumMember = new CsdlSemanticsEnumMember(this, csdlEnumMember);
					edmEnumMember.SetIsValueExplicit(this.Model, new bool?(true));
				}
				list.Add(edmEnumMember);
			}
			return list;
		}

		// Token: 0x04000103 RID: 259
		private readonly CsdlEnumType enumeration;

		// Token: 0x04000104 RID: 260
		private readonly Cache<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType> underlyingTypeCache = new Cache<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType>();

		// Token: 0x04000105 RID: 261
		private static readonly Func<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType> ComputeUnderlyingTypeFunc = (CsdlSemanticsEnumTypeDefinition me) => me.ComputeUnderlyingType();

		// Token: 0x04000106 RID: 262
		private readonly Cache<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>> membersCache = new Cache<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>>();

		// Token: 0x04000107 RID: 263
		private static readonly Func<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>> ComputeMembersFunc = (CsdlSemanticsEnumTypeDefinition me) => me.ComputeMembers();
	}
}
