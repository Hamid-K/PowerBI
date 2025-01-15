using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200055A RID: 1370
	internal interface ICompiler
	{
		// Token: 0x06004A26 RID: 18982
		byte[] Compile(CodeDomProvider compiler, IExpressionHostAssemblyHolder expressionHostAssemblyHolder, CompilerParameters options, CodeCompileUnit compilationUnits, ErrorContext errorContext, ExprCompileTimeInfoList ctExprList, CodeModuleClassInstanceDeclCompileTimeInfoList ctClassInstDeclList, ref int customCodeNumErrors, ref int customCodeNumWarnings);
	}
}
