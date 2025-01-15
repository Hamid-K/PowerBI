using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006A RID: 106
	public abstract class EdmOperation : EdmNamedElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0000BC95 File Offset: 0x00009E95
		protected EdmOperation(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			this.ReturnType = returnType;
			this.Namespace = namespaceName;
			this.IsBound = isBound;
			this.EntitySetPath = entitySetPathExpression;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000BCD3 File Offset: 0x00009ED3
		protected EdmOperation(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, false, null)
		{
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		public bool IsBound { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000BCF1 File Offset: 0x00009EF1
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000BCF9 File Offset: 0x00009EF9
		public IEdmPathExpression EntitySetPath { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D6 RID: 982
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000BD02 File Offset: 0x00009F02
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000BD0A File Offset: 0x00009F0A
		public string Namespace { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000BD13 File Offset: 0x00009F13
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000BD1B File Offset: 0x00009F1B
		public IEdmTypeReference ReturnType { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000BD24 File Offset: 0x00009F24
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public IEdmOperationParameter FindParameter(string name)
		{
			foreach (IEdmOperationParameter edmOperationParameter in this.Parameters)
			{
				if (edmOperationParameter.Name == name)
				{
					return edmOperationParameter;
				}
			}
			return null;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000BD88 File Offset: 0x00009F88
		public EdmOperationParameter AddParameter(string name, IEdmTypeReference type)
		{
			EdmOperationParameter edmOperationParameter = new EdmOperationParameter(this, name, type);
			this.parameters.Add(edmOperationParameter);
			return edmOperationParameter;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000BDAB File Offset: 0x00009FAB
		public EdmOptionalParameter AddOptionalParameter(string name, IEdmTypeReference type)
		{
			return this.AddOptionalParameter(name, type, null);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public EdmOptionalParameter AddOptionalParameter(string name, IEdmTypeReference type, string defaultValue)
		{
			EdmOptionalParameter edmOptionalParameter = new EdmOptionalParameter(this, name, type, defaultValue);
			this.parameters.Add(edmOptionalParameter);
			return edmOptionalParameter;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000BDDC File Offset: 0x00009FDC
		public void AddParameter(IEdmOperationParameter parameter)
		{
			EdmUtil.CheckArgumentNull<IEdmOperationParameter>(parameter, "parameter");
			this.parameters.Add(parameter);
		}

		// Token: 0x040000EA RID: 234
		private readonly List<IEdmOperationParameter> parameters = new List<IEdmOperationParameter>();
	}
}
