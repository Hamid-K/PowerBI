using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018D RID: 397
	internal class CsdlSemanticsEntityTypeDefinition : CsdlSemanticsStructuredTypeDefinition, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		public CsdlSemanticsEntityTypeDefinition(CsdlSemanticsSchema context, CsdlEntityType entity)
			: base(context, entity)
		{
			this.entity = entity;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0001CA0F File Offset: 0x0001AC0F
		public override IEdmStructuredType BaseType
		{
			get
			{
				return this.baseTypeCache.GetValue(this, CsdlSemanticsEntityTypeDefinition.ComputeBaseTypeFunc, CsdlSemanticsEntityTypeDefinition.OnCycleBaseTypeFunc);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0001CA27 File Offset: 0x0001AC27
		public string Name
		{
			get
			{
				return this.entity.Name;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001CA34 File Offset: 0x0001AC34
		public override bool IsAbstract
		{
			get
			{
				return this.entity.IsAbstract;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0001CA41 File Offset: 0x0001AC41
		public override bool IsOpen
		{
			get
			{
				return this.entity.IsOpen;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001CA4E File Offset: 0x0001AC4E
		public bool HasStream
		{
			get
			{
				return this.entity.HasStream;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001CA5B File Offset: 0x0001AC5B
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKeyCache.GetValue(this, CsdlSemanticsEntityTypeDefinition.ComputeDeclaredKeyFunc, null);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0001CA6F File Offset: 0x0001AC6F
		protected override CsdlStructuredType MyStructured
		{
			get
			{
				return this.entity;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001CA78 File Offset: 0x0001AC78
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

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
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

		// Token: 0x04000637 RID: 1591
		private readonly CsdlEntityType entity;

		// Token: 0x04000638 RID: 1592
		private readonly Cache<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> baseTypeCache = new Cache<CsdlSemanticsEntityTypeDefinition, IEdmEntityType>();

		// Token: 0x04000639 RID: 1593
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> ComputeBaseTypeFunc = (CsdlSemanticsEntityTypeDefinition me) => me.ComputeBaseType();

		// Token: 0x0400063A RID: 1594
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEdmEntityType> OnCycleBaseTypeFunc = (CsdlSemanticsEntityTypeDefinition me) => new CyclicEntityType(me.GetCyclicBaseTypeName(me.entity.BaseTypeName), me.Location);

		// Token: 0x0400063B RID: 1595
		private readonly Cache<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>> declaredKeyCache = new Cache<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>>();

		// Token: 0x0400063C RID: 1596
		private static readonly Func<CsdlSemanticsEntityTypeDefinition, IEnumerable<IEdmStructuralProperty>> ComputeDeclaredKeyFunc = (CsdlSemanticsEntityTypeDefinition me) => me.ComputeDeclaredKey();
	}
}
