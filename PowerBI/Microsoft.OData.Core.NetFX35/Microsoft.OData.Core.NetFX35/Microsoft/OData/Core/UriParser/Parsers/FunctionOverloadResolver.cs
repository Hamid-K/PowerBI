using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001FA RID: 506
	internal static class FunctionOverloadResolver
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x00042F44 File Offset: 0x00041144
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "ExceptionUtils.IsCatchableExceptionType is being used correctly")]
		internal static bool ResolveOperationImportFromList(string identifier, IList<string> parameterNames, IEdmModel model, out IEdmOperationImport matchingOperationImport, ODataUriResolver resolver)
		{
			IList<IEdmOperationImport> list = null;
			IList<IEdmActionImport> list2 = new List<IEdmActionImport>();
			try
			{
				if (parameterNames.Count > 0)
				{
					list = Enumerable.ToList<IEdmOperationImport>(Enumerable.Cast<IEdmOperationImport>(resolver.ResolveOperationImports(model, identifier).RemoveActionImports(out list2).FilterFunctionsByParameterNames(parameterNames, resolver.EnableCaseInsensitive)));
				}
				else
				{
					list = Enumerable.ToList<IEdmOperationImport>(resolver.ResolveOperationImports(model, identifier));
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw new ODataException(Strings.FunctionOverloadResolver_FoundInvalidOperationImport(identifier), ex);
			}
			if (list2.Count > 0)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
			}
			if (Enumerable.Any<IEdmOperationImport>(list, (IEdmOperationImport f) => f.IsActionImport()))
			{
				if (list.Count > 1)
				{
					if (Enumerable.Any<IEdmOperationImport>(list, (IEdmOperationImport o) => o.IsFunctionImport()))
					{
						throw new ODataException(Strings.FunctionOverloadResolver_MultipleOperationImportOverloads(identifier));
					}
					throw new ODataException(Strings.FunctionOverloadResolver_MultipleActionImportOverloads(identifier));
				}
				else
				{
					if (Enumerable.Count<string>(parameterNames) != 0)
					{
						throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
					}
					matchingOperationImport = Enumerable.Single<IEdmOperationImport>(list);
					return true;
				}
			}
			else
			{
				if (list.Count > 1 && parameterNames.Count == 0)
				{
					list = Enumerable.ToList<IEdmOperationImport>(Enumerable.Where<IEdmOperationImport>(list, (IEdmOperationImport operationImport) => Enumerable.Count<IEdmOperationParameter>(operationImport.Operation.Parameters) == 0));
				}
				if (list.Count == 0)
				{
					matchingOperationImport = null;
					return false;
				}
				if (list.Count > 1)
				{
					throw new ODataException(Strings.FunctionOverloadResolver_MultipleOperationImportOverloads(identifier));
				}
				matchingOperationImport = Enumerable.Single<IEdmOperationImport>(list);
				return matchingOperationImport != null;
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00043120 File Offset: 0x00041320
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "ExceptionUtils.IsCatchableExceptionType is being used correctly")]
		internal static bool ResolveOperationFromList(string identifier, IEnumerable<string> parameterNames, IEdmType bindingType, IEdmModel model, out IEdmOperation matchingOperation, ODataUriResolver resolver)
		{
			if (bindingType != null && bindingType.IsOpenType() && !identifier.Contains(".") && resolver.GetType() == typeof(ODataUriResolver))
			{
				matchingOperation = null;
				return false;
			}
			IList<IEdmOperation> list = null;
			try
			{
				if (bindingType != null)
				{
					list = Enumerable.ToList<IEdmOperation>(resolver.ResolveBoundOperations(model, identifier, bindingType));
				}
				else
				{
					list = Enumerable.ToList<IEdmOperation>(resolver.ResolveUnboundOperations(model, identifier));
				}
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw new ODataException(Strings.FunctionOverloadResolver_FoundInvalidOperation(identifier), ex);
				}
				throw;
			}
			IList<IEdmAction> list2 = new List<IEdmAction>();
			int num = Enumerable.Count<string>(parameterNames);
			if (bindingType != null)
			{
				list = Enumerable.ToList<IEdmOperation>(list.EnsureOperationsBoundWithBindingParameter());
			}
			if (num > 0)
			{
				list = Enumerable.ToList<IEdmOperation>(Enumerable.Cast<IEdmOperation>(list.RemoveActions(out list2).FilterFunctionsByParameterNames(parameterNames, resolver.EnableCaseInsensitive)));
			}
			else if (bindingType != null)
			{
				list = Enumerable.ToList<IEdmOperation>(Enumerable.Where<IEdmOperation>(list, (IEdmOperation o) => (o.IsFunction() && Enumerable.Count<IEdmOperationParameter>(o.Parameters) == 1) || o.IsAction()));
			}
			else
			{
				list = Enumerable.ToList<IEdmOperation>(Enumerable.Where<IEdmOperation>(list, (IEdmOperation o) => (o.IsFunction() && !Enumerable.Any<IEdmOperationParameter>(o.Parameters)) || o.IsAction()));
			}
			if (list.Count > 1)
			{
				list = Enumerable.ToList<IEdmOperation>(list.FilterBoundOperationsWithSameTypeHierarchyToTypeClosestToBindingType(bindingType));
			}
			if (Enumerable.Any<IEdmOperation>(list, (IEdmOperation f) => f.IsAction()))
			{
				if (list.Count > 1)
				{
					if (Enumerable.Any<IEdmOperation>(list, (IEdmOperation o) => o.IsFunction()))
					{
						throw new ODataException(Strings.FunctionOverloadResolver_MultipleOperationOverloads(identifier));
					}
					throw new ODataException(Strings.FunctionOverloadResolver_MultipleActionOverloads(identifier));
				}
				else
				{
					if (Enumerable.Count<string>(parameterNames) != 0)
					{
						throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
					}
					matchingOperation = Enumerable.Single<IEdmOperation>(list);
					return true;
				}
			}
			else
			{
				if (list2.Count > 0)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
				}
				if (list.Count > 1)
				{
					throw new ODataException(Strings.FunctionOverloadResolver_NoSingleMatchFound(identifier, string.Join(",", Enumerable.ToArray<string>(parameterNames))));
				}
				matchingOperation = Enumerable.SingleOrDefault<IEdmOperation>(list);
				return matchingOperation != null;
			}
		}
	}
}
