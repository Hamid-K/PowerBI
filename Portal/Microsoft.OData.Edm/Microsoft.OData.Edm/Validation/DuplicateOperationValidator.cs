using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200013F RID: 319
	internal class DuplicateOperationValidator
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x00013755 File Offset: 0x00011955
		internal DuplicateOperationValidator(ValidationContext context)
		{
			this.context = context;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00013788 File Offset: 0x00011988
		public static bool IsDuplicateOperation(IEdmOperation operation, IEnumerable<IEdmOperation> candidateDuplicateOperations)
		{
			DuplicateOperationValidator duplicateOperationValidator = new DuplicateOperationValidator(null);
			foreach (IEdmOperation edmOperation in candidateDuplicateOperations)
			{
				duplicateOperationValidator.ValidateNotDuplicate(edmOperation, true);
			}
			return duplicateOperationValidator.ValidateNotDuplicate(operation, true);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000137E4 File Offset: 0x000119E4
		public bool ValidateNotDuplicate(IEdmOperation operation, bool skipError)
		{
			bool flag = false;
			string text = operation.FullName();
			IEdmFunction edmFunction = operation as IEdmFunction;
			if (edmFunction != null)
			{
				string text2 = DuplicateOperationValidator.BuildInternalUniqueParameterNameFunctionString(edmFunction);
				if (this.functionsParameterNameHash.Contains(text2))
				{
					flag = true;
					if (!skipError)
					{
						this.context.AddError(edmFunction.Location(), EdmErrorCode.DuplicateFunctions, edmFunction.IsBound ? Strings.EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames(text) : Strings.EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames(text));
					}
				}
				else
				{
					this.functionsParameterNameHash.Add(text2);
				}
				string text3 = DuplicateOperationValidator.BuildInternalUniqueParameterTypeFunctionString(edmFunction);
				if (this.functionsParameterTypeHash.Contains(text3))
				{
					flag = true;
					if (!skipError)
					{
						this.context.AddError(edmFunction.Location(), EdmErrorCode.DuplicateFunctions, edmFunction.IsBound ? Strings.EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes(text) : Strings.EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes(text));
					}
				}
				else
				{
					this.functionsParameterTypeHash.Add(text3);
				}
			}
			else
			{
				IEdmAction edmAction = operation as IEdmAction;
				string text4 = DuplicateOperationValidator.BuildInternalUniqueActionString(edmAction);
				if (this.actionsNameHash.Contains(text4))
				{
					flag = true;
					if (!skipError)
					{
						this.context.AddError(edmAction.Location(), EdmErrorCode.DuplicateActions, edmAction.IsBound ? Strings.EdmModel_Validator_Semantic_ModelDuplicateBoundActions(text) : Strings.EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions(text));
					}
				}
				else
				{
					this.actionsNameHash.Add(text4);
				}
			}
			return flag;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00013920 File Offset: 0x00011B20
		private static string BuildInternalUniqueParameterNameFunctionString(IEdmFunction function)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(function.IsBound);
			stringBuilder.Append("-");
			stringBuilder.Append(function.Namespace);
			stringBuilder.Append("-");
			stringBuilder.Append(function.Name);
			stringBuilder.Append("-");
			if (!function.Parameters.Any<IEdmOperationParameter>())
			{
				return stringBuilder.ToString();
			}
			if (function.IsBound)
			{
				IEdmOperationParameter edmOperationParameter = function.Parameters.FirstOrDefault<IEdmOperationParameter>();
				stringBuilder.Append(edmOperationParameter.Type.FullName());
				stringBuilder.Append("-");
				using (IEnumerator<IEdmOperationParameter> enumerator = (from p in function.Parameters.Skip(1)
					orderby p.Name
					select p).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmOperationParameter edmOperationParameter2 = enumerator.Current;
						stringBuilder.Append(edmOperationParameter2.Name);
						stringBuilder.Append("-");
					}
					goto IL_0173;
				}
			}
			foreach (IEdmOperationParameter edmOperationParameter3 in function.Parameters.OrderBy((IEdmOperationParameter p) => p.Name))
			{
				stringBuilder.Append(edmOperationParameter3.Name);
				stringBuilder.Append("-");
			}
			IL_0173:
			return stringBuilder.ToString();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00013AC4 File Offset: 0x00011CC4
		private static string BuildInternalUniqueParameterTypeFunctionString(IEdmFunction function)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(function.IsBound);
			stringBuilder.Append("-");
			stringBuilder.Append(function.Namespace);
			stringBuilder.Append("-");
			stringBuilder.Append(function.Name);
			stringBuilder.Append("-");
			foreach (IEdmOperationParameter edmOperationParameter in function.Parameters)
			{
				stringBuilder.Append(edmOperationParameter.Type.FullName());
				stringBuilder.Append("-");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00013B80 File Offset: 0x00011D80
		private static string BuildInternalUniqueActionString(IEdmAction action)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(action.IsBound);
			stringBuilder.Append("-");
			stringBuilder.Append(action.Namespace);
			stringBuilder.Append("-");
			stringBuilder.Append(action.Name);
			stringBuilder.Append("-");
			if (!action.Parameters.Any<IEdmOperationParameter>())
			{
				return stringBuilder.ToString();
			}
			if (action.IsBound)
			{
				IEdmOperationParameter edmOperationParameter = action.Parameters.FirstOrDefault<IEdmOperationParameter>();
				stringBuilder.Append(edmOperationParameter.Type.FullName());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000385 RID: 901
		private readonly HashSetInternal<string> functionsParameterNameHash = new HashSetInternal<string>();

		// Token: 0x04000386 RID: 902
		private readonly HashSetInternal<string> functionsParameterTypeHash = new HashSetInternal<string>();

		// Token: 0x04000387 RID: 903
		private readonly HashSetInternal<string> actionsNameHash = new HashSetInternal<string>();

		// Token: 0x04000388 RID: 904
		private readonly ValidationContext context;
	}
}
