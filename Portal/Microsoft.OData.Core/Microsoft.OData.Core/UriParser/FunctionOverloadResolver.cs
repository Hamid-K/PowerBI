using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000152 RID: 338
	internal static class FunctionOverloadResolver
	{
		// Token: 0x0600117A RID: 4474 RVA: 0x0003171C File Offset: 0x0002F91C
		internal static bool ResolveOperationImportFromList(string identifier, IList<string> parameterNames, IEdmModel model, out IEdmOperationImport matchingOperationImport, ODataUriResolver resolver)
		{
			IEnumerable<IEdmOperationImport> enumerable = null;
			IList<IEdmOperationImport> list = new List<IEdmOperationImport>();
			try
			{
				if (parameterNames.Count > 0)
				{
					enumerable = resolver.ResolveOperationImports(model, identifier).RemoveActionImports(out list).FilterOperationsByParameterNames(parameterNames, resolver.EnableCaseInsensitive);
				}
				else
				{
					enumerable = resolver.ResolveOperationImports(model, identifier);
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
			if (list.Count > 0)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
			}
			if (enumerable.Any((IEdmOperationImport f) => f.IsActionImport()))
			{
				if (enumerable.Count<IEdmOperationImport>() > 1)
				{
					if (enumerable.Any((IEdmOperationImport o) => o.IsFunctionImport()))
					{
						throw new ODataException(Strings.FunctionOverloadResolver_MultipleOperationImportOverloads(identifier));
					}
					throw new ODataException(Strings.FunctionOverloadResolver_MultipleActionImportOverloads(identifier));
				}
				else
				{
					if (parameterNames.Count<string>() != 0)
					{
						throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
					}
					matchingOperationImport = enumerable.Single<IEdmOperationImport>();
					return true;
				}
			}
			else
			{
				if (enumerable.Count<IEdmOperationImport>() > 1 && parameterNames.Count == 0)
				{
					enumerable = enumerable.Where((IEdmOperationImport operationImport) => operationImport.Operation.Parameters.Count<IEdmOperationParameter>() == 0);
				}
				if (!enumerable.HasAny<IEdmOperationImport>())
				{
					matchingOperationImport = null;
					return false;
				}
				if (enumerable.Count<IEdmOperationImport>() > 1)
				{
					enumerable = enumerable.FindBestOverloadBasedOnParameters(parameterNames, false);
				}
				if (enumerable.Count<IEdmOperationImport>() > 1)
				{
					throw new ODataException(Strings.FunctionOverloadResolver_MultipleOperationImportOverloads(identifier));
				}
				matchingOperationImport = enumerable.Single<IEdmOperationImport>();
				return matchingOperationImport != null;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000318A8 File Offset: 0x0002FAA8
		internal static bool ResolveOperationFromList(string identifier, IEnumerable<string> parameterNames, IEdmType bindingType, IEdmModel model, out IEdmOperation matchingOperation, ODataUriResolver resolver)
		{
			if (bindingType != null && bindingType.IsOpen() && !identifier.Contains(".") && resolver.GetType() == typeof(ODataUriResolver))
			{
				matchingOperation = null;
				return false;
			}
			IEnumerable<IEdmOperation> enumerable = null;
			try
			{
				if (bindingType != null)
				{
					enumerable = resolver.ResolveBoundOperations(model, identifier, bindingType);
				}
				else
				{
					enumerable = resolver.ResolveUnboundOperations(model, identifier);
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
			IList<IEdmOperation> list = new List<IEdmOperation>();
			bool flag = parameterNames.Count<string>() > 0;
			if (bindingType != null)
			{
				enumerable.EnsureOperationsBoundWithBindingParameter();
			}
			if (flag)
			{
				enumerable = enumerable.RemoveActions(out list).FilterOperationsByParameterNames(parameterNames, resolver.EnableCaseInsensitive);
			}
			else if (bindingType != null)
			{
				enumerable = enumerable.Where(delegate(IEdmOperation o)
				{
					if (o.IsFunction())
					{
						if (o.Parameters.Count<IEdmOperationParameter>() != 1)
						{
							if (!o.Parameters.Skip(1).All((IEdmOperationParameter p) => p is IEdmOptionalParameter))
							{
								goto IL_0048;
							}
						}
						return true;
					}
					IL_0048:
					return o.IsAction();
				});
			}
			else
			{
				enumerable = enumerable.Where((IEdmOperation o) => (o.IsFunction() && !o.Parameters.Any<IEdmOperationParameter>()) || o.IsAction());
			}
			if (enumerable.Count<IEdmOperation>() > 1)
			{
				enumerable = enumerable.FilterBoundOperationsWithSameTypeHierarchyToTypeClosestToBindingType(bindingType);
			}
			if (enumerable.Any((IEdmOperation f) => f.IsAction()))
			{
				if (enumerable.Count<IEdmOperation>() > 1)
				{
					if (enumerable.Any((IEdmOperation o) => o.IsFunction()))
					{
						throw new ODataException(Strings.FunctionOverloadResolver_MultipleOperationOverloads(identifier));
					}
					throw new ODataException(Strings.FunctionOverloadResolver_MultipleActionOverloads(identifier));
				}
				else
				{
					if (flag)
					{
						throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
					}
					matchingOperation = enumerable.Single<IEdmOperation>();
					return true;
				}
			}
			else
			{
				if (list.Count > 0)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SegmentDoesNotSupportKeyPredicates(identifier));
				}
				if (enumerable.Count<IEdmOperation>() > 1)
				{
					enumerable = enumerable.FindBestOverloadBasedOnParameters(parameterNames, false);
				}
				if (enumerable.Count<IEdmOperation>() > 1)
				{
					throw new ODataException(Strings.FunctionOverloadResolver_NoSingleMatchFound(identifier, string.Join(",", parameterNames.ToArray<string>())));
				}
				matchingOperation = enumerable.SingleOrDefault<IEdmOperation>();
				return matchingOperation != null;
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00031AA8 File Offset: 0x0002FCA8
		internal static bool HasAny<T>(this IEnumerable<T> enumerable) where T : class
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list.Count > 0;
			}
			T[] array = enumerable as T[];
			if (array != null)
			{
				return array.Length != 0;
			}
			return enumerable.FirstOrDefault<T>() != null;
		}
	}
}
