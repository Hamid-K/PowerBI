using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000048 RID: 72
	internal class UnresolvedOperation : BadElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IUnresolvedElement
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00003A28 File Offset: 0x00001C28
		public UnresolvedOperation(string qualifiedName, string errorMessage, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedOperation, errorMessage)
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
			this.returnType = new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003A88 File Offset: 0x00001C88
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003A90 File Offset: 0x00001C90
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003A98 File Offset: 0x00001C98
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return Enumerable.Empty<IEdmOperationParameter>();
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public bool IsBound
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003AAA File Offset: 0x00001CAA
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003AAD File Offset: 0x00001CAD
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.None;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public IEdmOperationParameter FindParameter(string name)
		{
			return null;
		}

		// Token: 0x04000062 RID: 98
		private readonly string namespaceName;

		// Token: 0x04000063 RID: 99
		private readonly string name;

		// Token: 0x04000064 RID: 100
		private readonly IEdmTypeReference returnType;
	}
}
