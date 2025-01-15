using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000131 RID: 305
	public interface IODataUriParserModelExtensions
	{
		// Token: 0x060007DC RID: 2012
		IEnumerable<IEdmFunctionImport> FindFunctionImportsByBindingParameterTypeHierarchy(IEdmType bindingType, string functionImportName);

		// Token: 0x060007DD RID: 2013
		IEdmEntitySet FindEntitySetFromContainerQualifiedName(string containerQualifiedEntitySetName);

		// Token: 0x060007DE RID: 2014
		IEdmFunctionImport FindServiceOperation(string serviceOperationName);

		// Token: 0x060007DF RID: 2015
		IEdmFunctionImport FindFunctionImportByBindingParameterType(IEdmType bindingType, string functionImportName, IEnumerable<string> nonBindingParameterNamesFromUri);
	}
}
