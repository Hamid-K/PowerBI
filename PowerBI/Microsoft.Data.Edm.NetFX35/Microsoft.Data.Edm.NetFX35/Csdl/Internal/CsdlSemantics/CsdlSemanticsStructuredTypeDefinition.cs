using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000167 RID: 359
	internal abstract class CsdlSemanticsStructuredTypeDefinition : CsdlSemanticsTypeDefinition, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x000148B5 File Offset: 0x00012AB5
		protected CsdlSemanticsStructuredTypeDefinition(CsdlSemanticsSchema context, CsdlStructuredType type)
			: base(type)
		{
			this.context = context;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000148DB File Offset: 0x00012ADB
		public virtual bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000148DE File Offset: 0x00012ADE
		public virtual bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000785 RID: 1925
		public abstract IEdmStructuredType BaseType { get; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000148E1 File Offset: 0x00012AE1
		public override CsdlElement Element
		{
			get
			{
				return this.MyStructured;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x000148E9 File Offset: 0x00012AE9
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000148F6 File Offset: 0x00012AF6
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00014903 File Offset: 0x00012B03
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001490B File Offset: 0x00012B0B
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredPropertiesCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputeDeclaredPropertiesFunc, null);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600078B RID: 1931
		protected abstract CsdlStructuredType MyStructured { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001491F File Offset: 0x00012B1F
		private IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionaryCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00014934 File Offset: 0x00012B34
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			this.PropertiesDictionary.TryGetValue(name, ref edmProperty);
			return edmProperty;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00014954 File Offset: 0x00012B54
		protected virtual List<IEdmProperty> ComputeDeclaredProperties()
		{
			List<IEdmProperty> list = new List<IEdmProperty>();
			foreach (CsdlProperty csdlProperty in this.MyStructured.Properties)
			{
				list.Add(new CsdlSemanticsProperty(this, csdlProperty));
			}
			return list;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000149B4 File Offset: 0x00012BB4
		protected string GetCyclicBaseTypeName(string baseTypeName)
		{
			IEdmSchemaType edmSchemaType = this.context.FindType(baseTypeName);
			if (edmSchemaType == null)
			{
				return baseTypeName;
			}
			return edmSchemaType.FullName();
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000149D9 File Offset: 0x00012BD9
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000149F0 File Offset: 0x00012BF0
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x040003C7 RID: 967
		private readonly CsdlSemanticsSchema context;

		// Token: 0x040003C8 RID: 968
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> declaredPropertiesCache = new Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>>();

		// Token: 0x040003C9 RID: 969
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> ComputeDeclaredPropertiesFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputeDeclaredProperties();

		// Token: 0x040003CA RID: 970
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> propertiesDictionaryCache = new Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>>();

		// Token: 0x040003CB RID: 971
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputePropertiesDictionary();
	}
}
