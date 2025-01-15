using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200055E RID: 1374
	internal static class CompilerResultHelper
	{
		// Token: 0x06004A2B RID: 18987 RVA: 0x00139440 File Offset: 0x00137640
		public static void ParseErrors(ErrorContext errorContext, CompilerResultsWrapper results, IExpressionHostAssemblyHolder expressionHostAssemblyHolder, ExprCompileTimeInfoList exprCompileTime, CodeModuleClassInstanceDeclCompileTimeInfoList codeModule, ref int customCodeNumErrors, ref int customCodeNumWarnings)
		{
			int count = results.Errors.Count;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in results.Output)
			{
				stringBuilder.AppendLine(text);
			}
			if (results.NativeCompilerReturnValue != 0 && count == 0)
			{
				errorContext.Register(stringBuilder.ToString(), ProcessingErrorCode.rsUnexpectedCompilerError, Severity.Error, expressionHostAssemblyHolder.ObjectType, null, null, null, new string[] { results.NativeCompilerReturnValue.ToString(CultureInfo.InvariantCulture) });
			}
			for (int i = 0; i < count; i++)
			{
				CompilerError compilerError = results.Errors[i];
				int num;
				switch (ExprHostBuilder.ParseErrorSource(compilerError, out num))
				{
				case ExprHostBuilder.ErrorSource.Expression:
				{
					ExprCompileTimeInfo exprCompileTimeInfo = exprCompileTime[num];
					CompilerResultHelper.RegisterError(errorContext, compilerError, ref exprCompileTimeInfo.NumErrors, ref exprCompileTimeInfo.NumWarnings, exprCompileTimeInfo.OwnerObjectType, exprCompileTimeInfo.OwnerObjectName, exprCompileTimeInfo.OwnerPropertyName, ProcessingErrorCode.rsCompilerErrorInExpression, stringBuilder.ToString());
					break;
				}
				case ExprHostBuilder.ErrorSource.CodeModuleClassInstanceDecl:
				{
					if (codeModule == null)
					{
						codeModule = new CodeModuleClassInstanceDeclCompileTimeInfoList();
					}
					CodeModuleClassInstanceDeclCompileTimeInfo codeModuleClassInstanceDeclCompileTimeInfo = codeModule[num];
					CompilerResultHelper.RegisterError(errorContext, compilerError, ref codeModuleClassInstanceDeclCompileTimeInfo.NumErrors, ref codeModuleClassInstanceDeclCompileTimeInfo.NumWarnings, ObjectType.CodeClass, expressionHostAssemblyHolder.CodeClasses[num].ClassName, null, ProcessingErrorCode.rsCompilerErrorInClassInstanceDeclaration, stringBuilder.ToString());
					break;
				}
				case ExprHostBuilder.ErrorSource.CustomCode:
					CompilerResultHelper.RegisterError(errorContext, compilerError, ref customCodeNumErrors, ref customCodeNumWarnings, expressionHostAssemblyHolder.ObjectType, null, null, ProcessingErrorCode.rsCompilerErrorInCode, stringBuilder.ToString());
					break;
				default:
					errorContext.Register(stringBuilder.ToString(), ProcessingErrorCode.rsUnexpectedCompilerError, Severity.Error, expressionHostAssemblyHolder.ObjectType, null, null, null, new string[] { CompilerResultHelper.FormatError(compilerError) });
					throw new ReportProcessingException(errorContext.Messages);
				}
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0013961C File Offset: 0x0013781C
		private static void RegisterError(ErrorContext errorContext, CompilerError error, ref int numErrors, ref int numWarnings, ObjectType objectType, string objectName, string propertyName, ProcessingErrorCode errorCode, string diagnosticDetails)
		{
			if ((error.IsWarning ? numWarnings : numErrors) < 1)
			{
				bool flag;
				Severity severity;
				if (error.IsWarning)
				{
					flag = true;
					severity = Severity.Warning;
					numWarnings++;
				}
				else
				{
					flag = true;
					severity = Severity.Error;
					numErrors++;
				}
				if (flag)
				{
					errorContext.Register(diagnosticDetails, errorCode, severity, objectType, objectName, propertyName, null, new string[]
					{
						CompilerResultHelper.FormatError(error),
						error.Line.ToString(CultureInfo.InvariantCulture)
					});
				}
			}
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x00139695 File Offset: 0x00137895
		private static string FormatError(CompilerError error)
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0}] {1}", error.ErrorNumber, error.ErrorText);
		}
	}
}
