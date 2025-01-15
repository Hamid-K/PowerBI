using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017E RID: 382
	internal class CsdlSemanticsEnumTypeDefinition : CsdlSemanticsTypeDefinition, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x06000A62 RID: 2658 RVA: 0x0001CA18 File Offset: 0x0001AC18
		public CsdlSemanticsEnumTypeDefinition(CsdlSemanticsSchema context, CsdlEnumType enumeration)
			: base(enumeration)
		{
			this.Context = context;
			this.enumeration = enumeration;
			CsdlSemanticsSchema context2 = this.Context;
			string text = ((context2 != null) ? context2.Namespace : null);
			CsdlEnumType csdlEnumType = this.enumeration;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlEnumType != null) ? csdlEnumType.Name : null);
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001CA7F File Offset: 0x0001AC7F
		IEdmPrimitiveType IEdmEnumType.UnderlyingType
		{
			get
			{
				return this.underlyingTypeCache.GetValue(this, CsdlSemanticsEnumTypeDefinition.ComputeUnderlyingTypeFunc, null);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001CA93 File Offset: 0x0001AC93
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return this.membersCache.GetValue(this, CsdlSemanticsEnumTypeDefinition.ComputeMembersFunc, null);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0001CAA7 File Offset: 0x0001ACA7
		bool IEdmEnumType.IsFlags
		{
			get
			{
				return this.enumeration.IsFlags;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0000268E File Offset: 0x0000088E
		EdmSchemaElementKind IEdmSchemaElement.SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
		public string Namespace
		{
			get
			{
				return this.Context.Namespace;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0001CAC1 File Offset: 0x0001ACC1
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001CAC9 File Offset: 0x0001ACC9
		string IEdmNamedElement.Name
		{
			get
			{
				return this.enumeration.Name;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00003A59 File Offset: 0x00001C59
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001CAD6 File Offset: 0x0001ACD6
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001CAE3 File Offset: 0x0001ACE3
		public override CsdlElement Element
		{
			get
			{
				return this.enumeration;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001CAEB File Offset: 0x0001ACEB
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0001CAF3 File Offset: 0x0001ACF3
		public CsdlSemanticsSchema Context { get; private set; }

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001CAFC File Offset: 0x0001ACFC
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001CB10 File Offset: 0x0001AD10
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

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001CB74 File Offset: 0x0001AD74
		private IEnumerable<IEdmEnumMember> ComputeMembers()
		{
			List<IEdmEnumMember> list = new List<IEdmEnumMember>();
			long num = -1L;
			foreach (CsdlEnumMember csdlEnumMember in this.enumeration.Members)
			{
				long? num2 = null;
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

		// Token: 0x04000644 RID: 1604
		private readonly string fullName;

		// Token: 0x04000645 RID: 1605
		private readonly CsdlEnumType enumeration;

		// Token: 0x04000646 RID: 1606
		private readonly Cache<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType> underlyingTypeCache = new Cache<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType>();

		// Token: 0x04000647 RID: 1607
		private static readonly Func<CsdlSemanticsEnumTypeDefinition, IEdmPrimitiveType> ComputeUnderlyingTypeFunc = (CsdlSemanticsEnumTypeDefinition me) => me.ComputeUnderlyingType();

		// Token: 0x04000648 RID: 1608
		private readonly Cache<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>> membersCache = new Cache<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>>();

		// Token: 0x04000649 RID: 1609
		private static readonly Func<CsdlSemanticsEnumTypeDefinition, IEnumerable<IEdmEnumMember>> ComputeMembersFunc = (CsdlSemanticsEnumTypeDefinition me) => me.ComputeMembers();
	}
}
