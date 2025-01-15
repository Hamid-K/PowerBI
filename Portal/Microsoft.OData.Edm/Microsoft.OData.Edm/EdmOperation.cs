using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B7 RID: 183
	public abstract class EdmOperation : EdmNamedElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000B200 File Offset: 0x00009400
		protected EdmOperation(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			this.Return = ((returnType == null) ? null : new EdmOperationReturn(this, returnType));
			this.Namespace = namespaceName;
			this.IsBound = isBound;
			this.EntitySetPath = entitySetPathExpression;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(namespaceName, base.Name);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000B267 File Offset: 0x00009467
		protected EdmOperation(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, false, null)
		{
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000B274 File Offset: 0x00009474
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000B27C File Offset: 0x0000947C
		public bool IsBound { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000B285 File Offset: 0x00009485
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000B28D File Offset: 0x0000948D
		public IEdmPathExpression EntitySetPath { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600044F RID: 1103
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000B296 File Offset: 0x00009496
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000B29E File Offset: 0x0000949E
		public string Namespace { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000B2A7 File Offset: 0x000094A7
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000B2AF File Offset: 0x000094AF
		public IEdmTypeReference ReturnType
		{
			get
			{
				if (this.Return != null)
				{
					return this.Return.Type;
				}
				return null;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000B2C6 File Offset: 0x000094C6
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000B2CE File Offset: 0x000094CE
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000B2D6 File Offset: 0x000094D6
		internal IEdmOperationReturn Return { get; private set; }

		// Token: 0x06000457 RID: 1111 RVA: 0x0000B2E0 File Offset: 0x000094E0
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

		// Token: 0x06000458 RID: 1112 RVA: 0x0000B33C File Offset: 0x0000953C
		public EdmOperationParameter AddParameter(string name, IEdmTypeReference type)
		{
			EdmOperationParameter edmOperationParameter = new EdmOperationParameter(this, name, type);
			this.parameters.Add(edmOperationParameter);
			return edmOperationParameter;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000B35F File Offset: 0x0000955F
		public EdmOptionalParameter AddOptionalParameter(string name, IEdmTypeReference type)
		{
			return this.AddOptionalParameter(name, type, null);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000B36C File Offset: 0x0000956C
		public EdmOptionalParameter AddOptionalParameter(string name, IEdmTypeReference type, string defaultValue)
		{
			EdmOptionalParameter edmOptionalParameter = new EdmOptionalParameter(this, name, type, defaultValue);
			this.parameters.Add(edmOptionalParameter);
			return edmOptionalParameter;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000B390 File Offset: 0x00009590
		public void AddParameter(IEdmOperationParameter parameter)
		{
			EdmUtil.CheckArgumentNull<IEdmOperationParameter>(parameter, "parameter");
			this.parameters.Add(parameter);
		}

		// Token: 0x04000152 RID: 338
		private readonly string fullName;

		// Token: 0x04000153 RID: 339
		private readonly List<IEdmOperationParameter> parameters = new List<IEdmOperationParameter>();
	}
}
