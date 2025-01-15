using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016F RID: 367
	internal class CsdlSemanticsEnumTypeDefinition : CsdlSemanticsTypeDefinition, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0001A910 File Offset: 0x00018B10
		public CsdlSemanticsEnumTypeDefinition(CsdlSemanticsSchema context, CsdlEnumType enumeration)
			: base(enumeration)
		{
			this.Context = context;
			this.enumeration = enumeration;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001A93D File Offset: 0x00018B3D
		IEdmPrimitiveType IEdmEnumType.UnderlyingType
		{
			get
			{
				return this.underlyingTypeCache.GetValue(this, CsdlSemanticsEnumTypeDefinition.ComputeUnderlyingTypeFunc, null);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001A951 File Offset: 0x00018B51
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return this.membersCache.GetValue(this, CsdlSemanticsEnumTypeDefinition.ComputeMembersFunc, null);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001A965 File Offset: 0x00018B65
		bool IEdmEnumType.IsFlags
		{
			get
			{
				return this.enumeration.IsFlags;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00008D76 File Offset: 0x00006F76
		EdmSchemaElementKind IEdmSchemaElement.SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0001A972 File Offset: 0x00018B72
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001A97F File Offset: 0x00018B7F
		string IEdmNamedElement.Name
		{
			get
			{
				return this.enumeration.Name;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x000092ED File Offset: 0x000074ED
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001A98C File Offset: 0x00018B8C
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001A999 File Offset: 0x00018B99
		public override CsdlElement Element
		{
			get
			{
				return this.enumeration;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001A9A1 File Offset: 0x00018BA1
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0001A9A9 File Offset: 0x00018BA9
		public CsdlSemanticsSchema Context { get; private set; }

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001A9B2 File Offset: 0x00018BB2
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001A9C8 File Offset: 0x00018BC8
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

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001AA2C File Offset: 0x00018C2C
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

		// Token: 0x040005C9 RID: 1481
		private readonly CsdlEnumType enumeration;

		// Token: 0x040005CA RID: 1482
		private readonly Cache<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType> underlyingTypeCache = new Cache<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType>();

		// Token: 0x040005CB RID: 1483
		private static readonly Func<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType> ComputeUnderlyingTypeFunc = (CsdlSemanticsEnumTypeDefinition me) => me.ComputeUnderlyingType();

		// Token: 0x040005CC RID: 1484
		private readonly Cache<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>> membersCache = new Cache<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>>();

		// Token: 0x040005CD RID: 1485
		private static readonly Func<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>> ComputeMembersFunc = (CsdlSemanticsEnumTypeDefinition me) => me.ComputeMembers();
	}
}
