using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000159 RID: 345
	internal class DuplicateOperationValidator
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x0000EE40 File Offset: 0x0000D040
		internal DuplicateOperationValidator(ValidationContext context)
		{
			this.context = context;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000EE70 File Offset: 0x0000D070
		public static bool IsDuplicateOperation(IEdmOperation operation, IEnumerable<IEdmOperation> candidateDuplicateOperations)
		{
			DuplicateOperationValidator duplicateOperationValidator = new DuplicateOperationValidator(null);
			foreach (IEdmOperation edmOperation in candidateDuplicateOperations)
			{
				duplicateOperationValidator.ValidateNotDuplicate(edmOperation, true);
			}
			return duplicateOperationValidator.ValidateNotDuplicate(operation, true);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000EECC File Offset: 0x0000D0CC
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

		// Token: 0x06000667 RID: 1639 RVA: 0x0000F018 File Offset: 0x0000D218
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
					goto IL_0172;
				}
			}
			foreach (IEdmOperationParameter edmOperationParameter3 in Enumerable.OrderBy<IEdmOperationParameter, string>(function.Parameters, (IEdmOperationParameter p) => p.Name))
			{
				stringBuilder.Append(edmOperationParameter3.Name);
				stringBuilder.Append("-");
			}
			IL_0172:
			return stringBuilder.ToString();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000F1BC File Offset: 0x0000D3BC
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

		// Token: 0x06000669 RID: 1641 RVA: 0x0000F278 File Offset: 0x0000D478
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

		// Token: 0x0400029D RID: 669
		private readonly HashSetInternal<string> functionsParameterNameHash = new HashSetInternal<string>();

		// Token: 0x0400029E RID: 670
		private readonly HashSetInternal<string> functionsParameterTypeHash = new HashSetInternal<string>();

		// Token: 0x0400029F RID: 671
		private readonly HashSetInternal<string> actionsNameHash = new HashSetInternal<string>();

		// Token: 0x040002A0 RID: 672
		private readonly ValidationContext context;
	}
}
