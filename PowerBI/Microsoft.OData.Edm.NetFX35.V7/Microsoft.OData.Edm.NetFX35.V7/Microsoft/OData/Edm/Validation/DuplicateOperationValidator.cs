using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D0 RID: 208
	internal class DuplicateOperationValidator
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0000E46A File Offset: 0x0000C66A
		internal DuplicateOperationValidator(ValidationContext context)
		{
			this.context = context;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000E49C File Offset: 0x0000C69C
		public static bool IsDuplicateOperation(IEdmOperation operation, IEnumerable<IEdmOperation> candidateDuplicateOperations)
		{
			DuplicateOperationValidator duplicateOperationValidator = new DuplicateOperationValidator(null);
			foreach (IEdmOperation edmOperation in candidateDuplicateOperations)
			{
				duplicateOperationValidator.ValidateNotDuplicate(edmOperation, true);
			}
			return duplicateOperationValidator.ValidateNotDuplicate(operation, true);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
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

		// Token: 0x0600060B RID: 1547 RVA: 0x0000E634 File Offset: 0x0000C834
		private static string BuildInternalUniqueParameterNameFunctionString(IEdmFunction function)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(function.IsBound);
			stringBuilder.Append("-");
			stringBuilder.Append(function.Namespace);
			stringBuilder.Append("-");
			stringBuilder.Append(function.Name);
			stringBuilder.Append("-");
			if (!Enumerable.Any<IEdmOperationParameter>(function.Parameters))
			{
				return stringBuilder.ToString();
			}
			if (function.IsBound)
			{
				IEdmOperationParameter edmOperationParameter = Enumerable.FirstOrDefault<IEdmOperationParameter>(function.Parameters);
				stringBuilder.Append(edmOperationParameter.Type.FullName());
				stringBuilder.Append("-");
				using (IEnumerator<IEdmOperationParameter> enumerator = Enumerable.OrderBy<IEdmOperationParameter, string>(Enumerable.Skip<IEdmOperationParameter>(function.Parameters, 1), (IEdmOperationParameter p) => p.Name).GetEnumerator())
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
			foreach (IEdmOperationParameter edmOperationParameter3 in Enumerable.OrderBy<IEdmOperationParameter, string>(function.Parameters, (IEdmOperationParameter p) => p.Name))
			{
				stringBuilder.Append(edmOperationParameter3.Name);
				stringBuilder.Append("-");
			}
			IL_0173:
			return stringBuilder.ToString();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
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

		// Token: 0x0600060D RID: 1549 RVA: 0x0000E894 File Offset: 0x0000CA94
		private static string BuildInternalUniqueActionString(IEdmAction action)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(action.IsBound);
			stringBuilder.Append("-");
			stringBuilder.Append(action.Namespace);
			stringBuilder.Append("-");
			stringBuilder.Append(action.Name);
			stringBuilder.Append("-");
			if (!Enumerable.Any<IEdmOperationParameter>(action.Parameters))
			{
				return stringBuilder.ToString();
			}
			if (action.IsBound)
			{
				IEdmOperationParameter edmOperationParameter = Enumerable.FirstOrDefault<IEdmOperationParameter>(action.Parameters);
				stringBuilder.Append(edmOperationParameter.Type.FullName());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002A3 RID: 675
		private readonly HashSetInternal<string> functionsParameterNameHash = new HashSetInternal<string>();

		// Token: 0x040002A4 RID: 676
		private readonly HashSetInternal<string> functionsParameterTypeHash = new HashSetInternal<string>();

		// Token: 0x040002A5 RID: 677
		private readonly HashSetInternal<string> actionsNameHash = new HashSetInternal<string>();

		// Token: 0x040002A6 RID: 678
		private readonly ValidationContext context;
	}
}
