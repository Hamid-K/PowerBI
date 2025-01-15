using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200001B RID: 27
	internal static class FunctionOverloadResolver
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003CC0 File Offset: 0x00001EC0
		internal static IEdmFunctionImport ResolveOverloadsByParameterNames(ICollection<IEdmFunctionImport> functionImports, ICollection<string> parameters, string functionName)
		{
			IEdmFunctionImport edmFunctionImport = null;
			foreach (IEdmFunctionImport edmFunctionImport2 in functionImports)
			{
				IEnumerable<IEdmFunctionParameter> enumerable = edmFunctionImport2.Parameters;
				if (edmFunctionImport2.IsBindable)
				{
					enumerable = Enumerable.Skip<IEdmFunctionParameter>(enumerable, 1);
				}
				List<IEdmFunctionParameter> list = Enumerable.ToList<IEdmFunctionParameter>(enumerable);
				if (list.Count == parameters.Count)
				{
					if (!Enumerable.Any<IEdmFunctionParameter>(list, (IEdmFunctionParameter p) => Enumerable.All<string>(parameters, (string k) => k != p.Name)))
					{
						if (edmFunctionImport != null)
						{
							throw new ODataException(Strings.FunctionOverloadResolver_NoSingleMatchFound(functionName, string.Join(", ", Enumerable.ToArray<string>(parameters))));
						}
						edmFunctionImport = edmFunctionImport2;
					}
				}
			}
			return edmFunctionImport;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003DA4 File Offset: 0x00001FA4
		internal static bool ResolveFunctionsFromList(string identifier, IList<string> parameterNames, IEdmType bindingType, IEdmModel model, out IEdmFunctionImport matchingFunctionImport)
		{
			if (bindingType != null && bindingType.IsOpenType() && !identifier.Contains("."))
			{
				matchingFunctionImport = null;
				return false;
			}
			IODataUriParserModelExtensions iodataUriParserModelExtensions = model as IODataUriParserModelExtensions;
			if (iodataUriParserModelExtensions != null)
			{
				matchingFunctionImport = iodataUriParserModelExtensions.FindFunctionImportByBindingParameterType(bindingType, identifier, parameterNames);
				return matchingFunctionImport != null;
			}
			IList<IEdmFunctionImport> list = Enumerable.ToList<IEdmFunctionImport>(model.FindFunctionImportsBySpecificBindingParameterType(bindingType, identifier));
			if (list.Count == 0)
			{
				matchingFunctionImport = null;
				return false;
			}
			if (!list.AllHaveEqualReturnTypeAndAttributes())
			{
				throw new ODataException(Strings.RequestUriProcessor_FoundInvalidFunctionImport(identifier));
			}
			if (!Enumerable.Any<IEdmFunctionImport>(list, (IEdmFunctionImport f) => f.IsSideEffecting))
			{
				matchingFunctionImport = FunctionOverloadResolver.ResolveOverloadsByParameterNames(list, parameterNames, identifier);
				return matchingFunctionImport != null;
			}
			if (list.Count > 1)
			{
				throw new ODataException(Strings.FunctionOverloadResolver_MultipleActionOverloads(identifier));
			}
			if (Enumerable.Count<string>(parameterNames) != 0)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
			}
			matchingFunctionImport = Enumerable.Single<IEdmFunctionImport>(list);
			return true;
		}
	}
}
