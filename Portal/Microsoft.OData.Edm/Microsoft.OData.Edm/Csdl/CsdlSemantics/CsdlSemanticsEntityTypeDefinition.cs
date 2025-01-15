using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019C RID: 412
	internal class CsdlSemanticsEntityTypeDefinition : CsdlSemanticsStructuredTypeDefinition, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x0001ED28 File Offset: 0x0001CF28
		public CsdlSemanticsEntityTypeDefinition(CsdlSemanticsSchema context, CsdlEntityType entity)
			: base(context, entity)
		{
			this.entity = entity;
			string text = ((context != null) ? context.Namespace : null);
			CsdlEntityType csdlEntityType = this.entity;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlEntityType != null) ? csdlEntityType.Name : null);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001ED83 File Offset: 0x0001CF83
		public override IEdmStructuredType BaseType
		{
			get
			{
				return this.baseTypeCache.GetValue(this, CsdlSemanticsEntityTypeDefinition.ComputeBaseTypeFunc, CsdlSemanticsEntityTypeDefinition.OnCycleBaseTypeFunc);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0001ED9B File Offset: 0x0001CF9B
		public string Name
		{
			get
			{
				return this.entity.Name;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		public override bool IsAbstract
		{
			get
			{
				return this.entity.IsAbstract;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0001EDBD File Offset: 0x0001CFBD
		public override bool IsOpen
		{
			get
			{
				return this.entity.IsOpen;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0001EDCA File Offset: 0x0001CFCA
		public bool HasStream
		{
			get
			{
				return this.entity.HasStream;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0001EDD7 File Offset: 0x0001CFD7
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKeyCache.GetValue(this, CsdlSemanticsEntityTypeDefinition.ComputeDeclaredKeyFunc, null);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001EDEB File Offset: 0x0001CFEB
		protected override CsdlStructuredType MyStructured
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "baseType2", Justification = "Value assignment is required by compiler.")]
		private IEdmEntityType ComputeBaseType()
		{
			if (this.entity.BaseTypeName != null)
			{
				IEdmEntityType edmEntityType = base.Context.FindType(this.entity.BaseTypeName) as IEdmEntityType;
				if (edmEntityType != null)
				{
					IEdmStructuredType baseType = edmEntityType.BaseType;
				}
				return edmEntityType ?? new UnresolvedEntityType(base.Context.UnresolvedName(this.entity.BaseTypeName), base.Location);
			}
			return null;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001EE5C File Offset: 0x0001D05C
		private IEnumerable<IEdmStructuralProperty> ComputeDeclaredKey()
		{
			if (this.entity.Key != null)
			{
				List<IEdmStructuralProperty> list = new List<IEdmStructuralProperty>();
				using (IEnumerator<CsdlPropertyReference> enumerator = this.entity.Key.Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CsdlPropertyReference keyProperty = enumerator.Current;
						IEdmStructuralProperty edmStructuralProperty = base.FindProperty(keyProperty.PropertyName) as IEdmStructuralProperty;
						if (edmStructuralProperty != null)
						{
							list.Add(edmStructuralProperty);
						}
						else
						{
							edmStructuralProperty = base.DeclaredProperties.FirstOrDefault((IEdmProperty p) => p.Name == keyProperty.PropertyName) as IEdmStructuralProperty;
							if (edmStructuralProperty != null)
							{
								list.Add(edmStructuralProperty);
							}
							else
							{
								list.Add(new UnresolvedProperty(this, keyProperty.PropertyName, base.Location));
							}
						}
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x040006B8 RID: 1720
		private readonly CsdlEntityType entity;

		// Token: 0x040006B9 RID: 1721
		private readonly string fullName;

		// Token: 0x040006BA RID: 1722
		private readonly Cache<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> baseTypeCache = new Cache<CsdlSemanticsEntityTypeDefinition, IEdmEntityType>();

		// Token: 0x040006BB RID: 1723
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> ComputeBaseTypeFunc = (CsdlSemanticsEntityTypeDefinition me) => me.ComputeBaseType();

		// Token: 0x040006BC RID: 1724
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> OnCycleBaseTypeFunc = (CsdlSemanticsEntityTypeDefinition me) => new CyclicEntityType(me.GetCyclicBaseTypeName(me.entity.BaseTypeName), me.Location);

		// Token: 0x040006BD RID: 1725
		private readonly Cache<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>> declaredKeyCache = new Cache<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>>();

		// Token: 0x040006BE RID: 1726
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>> ComputeDeclaredKeyFunc = (CsdlSemanticsEntityTypeDefinition me) => me.ComputeDeclaredKey();
	}
}
