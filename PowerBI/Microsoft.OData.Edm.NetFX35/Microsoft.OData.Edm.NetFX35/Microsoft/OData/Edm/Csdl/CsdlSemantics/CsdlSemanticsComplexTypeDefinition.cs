using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A5 RID: 421
	internal class CsdlSemanticsComplexTypeDefinition : CsdlSemanticsStructuredTypeDefinition, IEdmComplexType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00016269 File Offset: 0x00014469
		public CsdlSemanticsComplexTypeDefinition(CsdlSemanticsSchema context, CsdlComplexType complex)
			: base(context, complex)
		{
			this.complex = complex;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00016285 File Offset: 0x00014485
		public override IEdmStructuredType BaseType
		{
			get
			{
				return this.baseTypeCache.GetValue(this, CsdlSemanticsComplexTypeDefinition.ComputeBaseTypeFunc, CsdlSemanticsComplexTypeDefinition.OnCycleBaseTypeFunc);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001629D File Offset: 0x0001449D
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x000162A0 File Offset: 0x000144A0
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x000162A3 File Offset: 0x000144A3
		public override bool IsAbstract
		{
			get
			{
				return this.complex.IsAbstract;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x000162B0 File Offset: 0x000144B0
		public override bool IsOpen
		{
			get
			{
				return this.complex.IsOpen;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x000162BD File Offset: 0x000144BD
		public string Name
		{
			get
			{
				return this.complex.Name;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x000162CA File Offset: 0x000144CA
		protected override CsdlStructuredType MyStructured
		{
			get
			{
				return this.complex;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000162D4 File Offset: 0x000144D4
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

		// Token: 0x04000434 RID: 1076
		private readonly CsdlComplexType complex;

		// Token: 0x04000435 RID: 1077
		private readonly Cache<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> baseTypeCache = new Cache<CsdlSemanticsComplexTypeDefinition, IEdmComplexType>();

		// Token: 0x04000436 RID: 1078
		private static readonly Func<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> ComputeBaseTypeFunc = (CsdlSemanticsComplexTypeDefinition me) => me.ComputeBaseType();

		// Token: 0x04000437 RID: 1079
		private static readonly Func<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> OnCycleBaseTypeFunc = (CsdlSemanticsComplexTypeDefinition me) => new CyclicComplexType(me.GetCyclicBaseTypeName(me.complex.BaseTypeName), me.Location);
	}
}
