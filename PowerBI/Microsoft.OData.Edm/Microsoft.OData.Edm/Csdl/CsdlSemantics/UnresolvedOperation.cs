using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A3 RID: 419
	internal class UnresolvedOperation : BadElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement, IEdmFullNamedElement
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public UnresolvedOperation(string qualifiedName, string errorMessage, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedOperation, errorMessage)
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
			this.returnType = new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00020D70 File Offset: 0x0001EF70
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00020D78 File Offset: 0x0001EF78
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00020D80 File Offset: 0x0001EF80
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00020D88 File Offset: 0x0001EF88
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00020D90 File Offset: 0x0001EF90
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return Enumerable.Empty<IEdmOperationParameter>();
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsBound
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000026A6 File Offset: 0x000008A6
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.None;
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmOperationParameter FindParameter(string name)
		{
			return null;
		}

		// Token: 0x040006E3 RID: 1763
		private readonly string namespaceName;

		// Token: 0x040006E4 RID: 1764
		private readonly string name;

		// Token: 0x040006E5 RID: 1765
		private readonly string fullName;

		// Token: 0x040006E6 RID: 1766
		private readonly IEdmTypeReference returnType;
	}
}
