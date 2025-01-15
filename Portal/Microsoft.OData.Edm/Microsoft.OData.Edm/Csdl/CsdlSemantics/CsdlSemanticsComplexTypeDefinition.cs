using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000197 RID: 407
	internal class CsdlSemanticsComplexTypeDefinition : CsdlSemanticsStructuredTypeDefinition, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x0001E434 File Offset: 0x0001C634
		public CsdlSemanticsComplexTypeDefinition(CsdlSemanticsSchema context, CsdlComplexType complex)
			: base(context, complex)
		{
			this.complex = complex;
			string text = ((context != null) ? context.Namespace : null);
			CsdlComplexType csdlComplexType = this.complex;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlComplexType != null) ? csdlComplexType.Name : null);
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001E484 File Offset: 0x0001C684
		public override IEdmStructuredType BaseType
		{
			get
			{
				return this.baseTypeCache.GetValue(this, CsdlSemanticsComplexTypeDefinition.ComputeBaseTypeFunc, CsdlSemanticsComplexTypeDefinition.OnCycleBaseTypeFunc);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001E49C File Offset: 0x0001C69C
		public override bool IsAbstract
		{
			get
			{
				return this.complex.IsAbstract;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001E4A9 File Offset: 0x0001C6A9
		public override bool IsOpen
		{
			get
			{
				return this.complex.IsOpen;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001E4B6 File Offset: 0x0001C6B6
		public string Name
		{
			get
			{
				return this.complex.Name;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001E4C3 File Offset: 0x0001C6C3
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0001E4CB File Offset: 0x0001C6CB
		protected override CsdlStructuredType MyStructured
		{
			get
			{
				return this.complex;
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "baseType2", Justification = "Value assignment is required by compiler.")]
		private IEdmComplexType ComputeBaseType()
		{
			if (this.complex.BaseTypeName != null)
			{
				IEdmComplexType edmComplexType = base.Context.FindType(this.complex.BaseTypeName) as IEdmComplexType;
				if (edmComplexType != null)
				{
					IEdmStructuredType baseType = edmComplexType.BaseType;
				}
				return edmComplexType ?? new UnresolvedComplexType(base.Context.UnresolvedName(this.complex.BaseTypeName), base.Location);
			}
			return null;
		}

		// Token: 0x0400069F RID: 1695
		private readonly string fullName;

		// Token: 0x040006A0 RID: 1696
		private readonly CsdlComplexType complex;

		// Token: 0x040006A1 RID: 1697
		private readonly Cache<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> baseTypeCache = new Cache<CsdlSemanticsComplexTypeDefinition, IEdmComplexType>();

		// Token: 0x040006A2 RID: 1698
		private static readonly Func<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> ComputeBaseTypeFunc = (CsdlSemanticsComplexTypeDefinition me) => me.ComputeBaseType();

		// Token: 0x040006A3 RID: 1699
		private static readonly Func<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> OnCycleBaseTypeFunc = (CsdlSemanticsComplexTypeDefinition me) => new CyclicComplexType(me.GetCyclicBaseTypeName(me.complex.BaseTypeName), me.Location);
	}
}
