using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200010F RID: 271
	public abstract class EdmOperation : EdmNamedElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		protected EdmOperation(string namespaceName, string name, IEdmTypeReference returnType, bool isBound, IEdmPathExpression entitySetPathExpression)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			this.ReturnType = returnType;
			this.Namespace = namespaceName;
			this.IsBound = isBound;
			this.EntitySetPath = entitySetPathExpression;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000DA15 File Offset: 0x0000BC15
		protected EdmOperation(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, false, null)
		{
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000DA22 File Offset: 0x0000BC22
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x0000DA2A File Offset: 0x0000BC2A
		public bool IsBound { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000DA33 File Offset: 0x0000BC33
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000DA3B File Offset: 0x0000BC3B
		public IEdmPathExpression EntitySetPath { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000558 RID: 1368
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000DA44 File Offset: 0x0000BC44
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public string Namespace { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000DA55 File Offset: 0x0000BC55
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000DA5D File Offset: 0x0000BC5D
		public IEdmTypeReference ReturnType { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000DA66 File Offset: 0x0000BC66
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000DA70 File Offset: 0x0000BC70
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

		// Token: 0x0600055F RID: 1375 RVA: 0x0000DACC File Offset: 0x0000BCCC
		public EdmOperationParameter AddParameter(string name, IEdmTypeReference type)
		{
			EdmOperationParameter edmOperationParameter = new EdmOperationParameter(this, name, type);
			this.parameters.Add(edmOperationParameter);
			return edmOperationParameter;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000DAEF File Offset: 0x0000BCEF
		public void AddParameter(IEdmOperationParameter parameter)
		{
			EdmUtil.CheckArgumentNull<IEdmOperationParameter>(parameter, "parameter");
			this.parameters.Add(parameter);
		}

		// Token: 0x04000209 RID: 521
		private readonly List<IEdmOperationParameter> parameters = new List<IEdmOperationParameter>();
	}
}
