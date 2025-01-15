using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000193 RID: 403
	internal class UnresolvedOperation : BadElement, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x0001E5FC File Offset: 0x0001C7FC
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0001E65A File Offset: 0x0001C85A
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0001E662 File Offset: 0x0001C862
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001E66A File Offset: 0x0001C86A
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001E672 File Offset: 0x0001C872
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return Enumerable.Empty<IEdmOperationParameter>();
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool IsBound
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00008EC3 File Offset: 0x000070C3
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.None;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmOperationParameter FindParameter(string name)
		{
			return null;
		}

		// Token: 0x0400065C RID: 1628
		private readonly string namespaceName;

		// Token: 0x0400065D RID: 1629
		private readonly string name;

		// Token: 0x0400065E RID: 1630
		private readonly IEdmTypeReference returnType;
	}
}
