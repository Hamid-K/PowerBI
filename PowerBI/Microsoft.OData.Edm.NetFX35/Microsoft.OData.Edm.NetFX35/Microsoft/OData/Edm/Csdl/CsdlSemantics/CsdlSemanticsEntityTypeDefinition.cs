using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AC RID: 428
	internal class CsdlSemanticsEntityTypeDefinition : CsdlSemanticsStructuredTypeDefinition, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00016C84 File Offset: 0x00014E84
		public CsdlSemanticsEntityTypeDefinition(CsdlSemanticsSchema context, CsdlEntityType entity)
			: base(context, entity)
		{
			this.entity = entity;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00016CAB File Offset: 0x00014EAB
		public override IEdmStructuredType BaseType
		{
			get
			{
				return this.baseTypeCache.GetValue(this, CsdlSemanticsEntityTypeDefinition.ComputeBaseTypeFunc, CsdlSemanticsEntityTypeDefinition.OnCycleBaseTypeFunc);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00016CC3 File Offset: 0x00014EC3
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00016CC6 File Offset: 0x00014EC6
		public string Name
		{
			get
			{
				return this.entity.Name;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00016CD3 File Offset: 0x00014ED3
		public override bool IsAbstract
		{
			get
			{
				return this.entity.IsAbstract;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00016CE0 File Offset: 0x00014EE0
		public override bool IsOpen
		{
			get
			{
				return this.entity.IsOpen;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00016CED File Offset: 0x00014EED
		public bool HasStream
		{
			get
			{
				return this.entity.HasStream;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00016CFA File Offset: 0x00014EFA
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKeyCache.GetValue(this, CsdlSemanticsEntityTypeDefinition.ComputeDeclaredKeyFunc, null);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00016D0E File Offset: 0x00014F0E
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00016D11 File Offset: 0x00014F11
		protected override CsdlStructuredType MyStructured
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00016D1C File Offset: 0x00014F1C
		protected override List<IEdmProperty> ComputeDeclaredProperties()
		{
			List<IEdmProperty> list = base.ComputeDeclaredProperties();
			foreach (CsdlNavigationProperty csdlNavigationProperty in this.entity.NavigationProperties)
			{
				list.Add(new CsdlSemanticsNavigationProperty(this, csdlNavigationProperty));
			}
			return list;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00016D7C File Offset: 0x00014F7C
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

		// Token: 0x060008D2 RID: 2258 RVA: 0x00016E04 File Offset: 0x00015004
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
							edmStructuralProperty = Enumerable.FirstOrDefault<IEdmProperty>(base.DeclaredProperties, (IEdmProperty p) => p.Name == keyProperty.PropertyName) as IEdmStructuralProperty;
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

		// Token: 0x04000457 RID: 1111
		private readonly CsdlEntityType entity;

		// Token: 0x04000458 RID: 1112
		private readonly Cache<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> baseTypeCache = new Cache<CsdlSemanticsEntityTypeDefinition, IEdmEntityType>();

		// Token: 0x04000459 RID: 1113
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> ComputeBaseTypeFunc = (CsdlSemanticsEntityTypeDefinition me) => me.ComputeBaseType();

		// Token: 0x0400045A RID: 1114
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> OnCycleBaseTypeFunc = (CsdlSemanticsEntityTypeDefinition me) => new CyclicEntityType(me.GetCyclicBaseTypeName(me.entity.BaseTypeName), me.Location);

		// Token: 0x0400045B RID: 1115
		private readonly Cache<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>> declaredKeyCache = new Cache<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>>();

		// Token: 0x0400045C RID: 1116
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>> ComputeDeclaredKeyFunc = (CsdlSemanticsEntityTypeDefinition me) => me.ComputeDeclaredKey();
	}
}
