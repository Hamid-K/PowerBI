using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AD RID: 429
	internal abstract class CsdlSemanticsStructuredTypeDefinition : CsdlSemanticsTypeDefinition, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x00021C10 File Offset: 0x0001FE10
		protected CsdlSemanticsStructuredTypeDefinition(CsdlSemanticsSchema context, CsdlStructuredType type)
			: base(type)
		{
			this.context = context;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000026A6 File Offset: 0x000008A6
		public virtual bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x000026A6 File Offset: 0x000008A6
		public virtual bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000C12 RID: 3090
		public abstract IEdmStructuredType BaseType { get; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00021C36 File Offset: 0x0001FE36
		public override CsdlElement Element
		{
			get
			{
				return this.MyStructured;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00021C3E File Offset: 0x0001FE3E
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00021C4B File Offset: 0x0001FE4B
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00021C58 File Offset: 0x0001FE58
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00021C60 File Offset: 0x0001FE60
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredPropertiesCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputeDeclaredPropertiesFunc, null);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000C18 RID: 3096
		protected abstract CsdlStructuredType MyStructured { get; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00021C74 File Offset: 0x0001FE74
		private IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionaryCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00021C88 File Offset: 0x0001FE88
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			this.PropertiesDictionary.TryGetValue(name, out edmProperty);
			return edmProperty;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00021CA8 File Offset: 0x0001FEA8
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

		// Token: 0x06000C1C RID: 3100 RVA: 0x00021D50 File Offset: 0x0001FF50
		protected string GetCyclicBaseTypeName(string baseTypeName)
		{
			IEdmSchemaType edmSchemaType = this.context.FindType(baseTypeName);
			if (edmSchemaType == null)
			{
				return baseTypeName;
			}
			return edmSchemaType.FullName();
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00021D75 File Offset: 0x0001FF75
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00021D8C File Offset: 0x0001FF8C
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x04000707 RID: 1799
		private readonly CsdlSemanticsSchema context;

		// Token: 0x04000708 RID: 1800
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> declaredPropertiesCache = new Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>>();

		// Token: 0x04000709 RID: 1801
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> ComputeDeclaredPropertiesFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputeDeclaredProperties();

		// Token: 0x0400070A RID: 1802
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> propertiesDictionaryCache = new Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>>();

		// Token: 0x0400070B RID: 1803
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputePropertiesDictionary();
	}
}
