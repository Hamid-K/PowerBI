using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A4 RID: 420
	internal abstract class CsdlSemanticsStructuredTypeDefinition : CsdlSemanticsTypeDefinition, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x00016071 File Offset: 0x00014271
		protected CsdlSemanticsStructuredTypeDefinition(CsdlSemanticsSchema context, CsdlStructuredType type)
			: base(type)
		{
			this.context = context;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00016097 File Offset: 0x00014297
		public virtual bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001609A File Offset: 0x0001429A
		public virtual bool IsOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000871 RID: 2161
		public abstract IEdmStructuredType BaseType { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001609D File Offset: 0x0001429D
		public override CsdlElement Element
		{
			get
			{
				return this.MyStructured;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x000160A5 File Offset: 0x000142A5
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x000160B2 File Offset: 0x000142B2
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000160BF File Offset: 0x000142BF
		public CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000160C7 File Offset: 0x000142C7
		public IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredPropertiesCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputeDeclaredPropertiesFunc, null);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000877 RID: 2167
		protected abstract CsdlStructuredType MyStructured { get; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x000160DB File Offset: 0x000142DB
		private IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionaryCache.GetValue(this, CsdlSemanticsStructuredTypeDefinition.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000160F0 File Offset: 0x000142F0
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			this.PropertiesDictionary.TryGetValue(name, ref edmProperty);
			return edmProperty;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00016110 File Offset: 0x00014310
		protected virtual List<IEdmProperty> ComputeDeclaredProperties()
		{
			List<IEdmProperty> list = new List<IEdmProperty>();
			foreach (CsdlProperty csdlProperty in this.MyStructured.Properties)
			{
				list.Add(new CsdlSemanticsProperty(this, csdlProperty));
			}
			return list;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00016170 File Offset: 0x00014370
		protected string GetCyclicBaseTypeName(string baseTypeName)
		{
			IEdmSchemaType edmSchemaType = this.context.FindType(baseTypeName);
			if (edmSchemaType == null)
			{
				return baseTypeName;
			}
			return edmSchemaType.FullName();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00016195 File Offset: 0x00014395
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.context);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000161AC File Offset: 0x000143AC
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x0400042D RID: 1069
		private readonly CsdlSemanticsSchema context;

		// Token: 0x0400042E RID: 1070
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> declaredPropertiesCache = new Cache<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>>();

		// Token: 0x0400042F RID: 1071
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, List<IEdmProperty>> ComputeDeclaredPropertiesFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputeDeclaredProperties();

		// Token: 0x04000430 RID: 1072
		private readonly Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> propertiesDictionaryCache = new Cache<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>>();

		// Token: 0x04000431 RID: 1073
		private static readonly Func<CsdlSemanticsStructuredTypeDefinition, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (CsdlSemanticsStructuredTypeDefinition me) => me.ComputePropertiesDictionary();
	}
}
