using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019C RID: 412
	internal abstract class CsdlSemanticsStructuredTypeDefinition : CsdlSemanticsTypeDefinition, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0001F484 File Offset: 0x0001D684
		protected CsdlSemanticsStructuredTypeDefinition(CsdlSemanticsSchema context, CsdlStructuredType type)
			: base(type)
		{
			this.context = context;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00008EC3 File Offset: 0x000070C3
		public virtual bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00008EC3 File Offset: 0x000070C3
		public virtual bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B3B RID: 2875
		public abstract IEdmStructuredType BaseType { get; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001F4AA File Offset: 0x0001D6AA
		public override CsdlElement Element
		{
			get
			{
				return this.MyStructured;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001F4B2 File Offset: 0x0001D6B2
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001F4BF File Offset: 0x0001D6BF
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredPropertiesCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputeDeclaredPropertiesFunc, null);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000B41 RID: 2881
		protected abstract CsdlStructuredType MyStructured { get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		private IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionaryCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			this.PropertiesDictionary.TryGetValue(name, ref edmProperty);
			return edmProperty;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001F51C File Offset: 0x0001D71C
		protected List<IEdmProperty> ComputeDeclaredProperties()
		{
			List<IEdmProperty> list = new List<IEdmProperty>();
			foreach (CsdlProperty csdlProperty in this.MyStructured.StructuralProperties)
			{
				list.Add(new CsdlSemanticsProperty(this, csdlProperty));
			}
			foreach (CsdlNavigationProperty csdlNavigationProperty in this.MyStructured.NavigationProperties)
			{
				list.Add(new CsdlSemanticsNavigationProperty(this, csdlNavigationProperty));
			}
			return list;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001F5C4 File Offset: 0x0001D7C4
		protected string GetCyclicBaseTypeName(string baseTypeName)
		{
			IEdmSchemaType edmSchemaType = this.context.FindType(baseTypeName);
			if (edmSchemaType == null)
			{
				return baseTypeName;
			}
			return edmSchemaType.FullName();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001F600 File Offset: 0x0001D800
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0400067C RID: 1660
		private readonly CsdlSemanticsSchema context;

		// Token: 0x0400067D RID: 1661
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> declaredPropertiesCache = new Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>>();

		// Token: 0x0400067E RID: 1662
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> ComputeDeclaredPropertiesFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputeDeclaredProperties();

		// Token: 0x0400067F RID: 1663
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> propertiesDictionaryCache = new Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>>();

		// Token: 0x04000680 RID: 1664
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputePropertiesDictionary();
	}
}
