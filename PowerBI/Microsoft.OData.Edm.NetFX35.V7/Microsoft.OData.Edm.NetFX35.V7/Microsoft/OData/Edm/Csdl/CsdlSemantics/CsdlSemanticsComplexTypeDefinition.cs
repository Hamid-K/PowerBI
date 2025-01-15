using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000187 RID: 391
	internal class CsdlSemanticsComplexTypeDefinition : CsdlSemanticsStructuredTypeDefinition, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0001C116 File Offset: 0x0001A316
		public CsdlSemanticsComplexTypeDefinition(CsdlSemanticsSchema context, CsdlComplexType complex)
			: base(context, complex)
		{
			this.complex = complex;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001C132 File Offset: 0x0001A332
		public override IEdmStructuredType BaseType
		{
			get
			{
				return this.baseTypeCache.GetValue(this, CsdlSemanticsComplexTypeDefinition.ComputeBaseTypeFunc, CsdlSemanticsComplexTypeDefinition.OnCycleBaseTypeFunc);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001C14A File Offset: 0x0001A34A
		public override bool IsAbstract
		{
			get
			{
				return this.complex.IsAbstract;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001C157 File Offset: 0x0001A357
		public override bool IsOpen
		{
			get
			{
				return this.complex.IsOpen;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0001C164 File Offset: 0x0001A364
		public string Name
		{
			get
			{
				return this.complex.Name;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001C171 File Offset: 0x0001A371
		protected override CsdlStructuredType MyStructured
		{
			get
			{
				return this.complex;
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001C17C File Offset: 0x0001A37C
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

		// Token: 0x0400061E RID: 1566
		private readonly CsdlComplexType complex;

		// Token: 0x0400061F RID: 1567
		private readonly Cache<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> baseTypeCache = new Cache<CsdlSemanticsComplexTypeDefinition, IEdmComplexType>();

		// Token: 0x04000620 RID: 1568
		private static readonly Func<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> ComputeBaseTypeFunc = (CsdlSemanticsComplexTypeDefinition me) => me.ComputeBaseType();

		// Token: 0x04000621 RID: 1569
		private static readonly Func<CsdlSemanticsComplexTypeDefinition, IEdmComplexType> OnCycleBaseTypeFunc = (CsdlSemanticsComplexTypeDefinition me) => new CyclicComplexType(me.GetCyclicBaseTypeName(me.complex.BaseTypeName), me.Location);
	}
}
