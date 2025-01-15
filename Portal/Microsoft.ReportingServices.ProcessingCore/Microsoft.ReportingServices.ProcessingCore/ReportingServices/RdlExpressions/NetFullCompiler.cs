using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000566 RID: 1382
	internal sealed class NetFullCompiler : ICompiler
	{
		// Token: 0x06005094 RID: 20628 RVA: 0x0015290C File Offset: 0x00150B0C
		public byte[] Compile(CodeDomProvider compiler, IExpressionHostAssemblyHolder expressionHostAssemblyHolder, CompilerParameters options, CodeCompileUnit compilationUnits, ErrorContext errorContext, ExprCompileTimeInfoList ctExprList, CodeModuleClassInstanceDeclCompileTimeInfoList ctClassInstDeclList, ref int customCodeNumErrors, ref int customCodeNumWarnings)
		{
			CompilerResults compilerResults = null;
			byte[] array;
			try
			{
				compilerResults = compiler.CompileAssemblyFromDom(options, new CodeCompileUnit[] { compilationUnits });
				if (Global.Tracer.TraceVerbose)
				{
					try
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							IndentedTextWriter indentedTextWriter = new IndentedTextWriter(new StreamWriter(memoryStream), "    ");
							compiler.GenerateCodeFromCompileUnit(compilationUnits, indentedTextWriter, new CodeGeneratorOptions());
							indentedTextWriter.Flush();
							memoryStream.Position = 0L;
							StreamReader streamReader = new StreamReader(memoryStream);
							Global.Tracer.Trace(streamReader.ReadToEnd());
						}
					}
					catch
					{
					}
				}
				if (compilerResults.NativeCompilerReturnValue != 0 || compilerResults.Errors.Count > 0)
				{
					CompilerResultHelper.ParseErrors(errorContext, new CompilerResultsWrapper(compilerResults), expressionHostAssemblyHolder, ctExprList, ctClassInstDeclList, ref customCodeNumErrors, ref customCodeNumWarnings);
					array = new byte[0];
				}
				else
				{
					using (FileStream fileStream = File.OpenRead(compilerResults.PathToAssembly))
					{
						byte[] array2 = new byte[fileStream.Length];
						int num = fileStream.Read(array2, 0, (int)fileStream.Length);
						Global.Tracer.Assert((long)num == fileStream.Length, "(read == fs.Length)");
						array = array2;
					}
				}
			}
			finally
			{
				if (compilerResults != null && compilerResults.PathToAssembly != null)
				{
					File.Delete(compilerResults.PathToAssembly);
				}
			}
			return array;
		}
	}
}
