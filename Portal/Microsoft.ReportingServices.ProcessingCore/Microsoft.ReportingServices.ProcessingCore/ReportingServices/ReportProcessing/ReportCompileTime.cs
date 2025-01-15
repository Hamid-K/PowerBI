using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000683 RID: 1667
	internal sealed class ReportCompileTime
	{
		// Token: 0x06005B45 RID: 23365 RVA: 0x0017757D File Offset: 0x0017577D
		internal ReportCompileTime(ExpressionParser langParser, ErrorContext errorContext)
		{
			this.m_langParser = langParser;
			this.m_errorContext = errorContext;
			this.m_builder = new ExprHostBuilder();
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x0017759E File Offset: 0x0017579E
		internal ExprHostBuilder Builder
		{
			get
			{
				return this.m_builder;
			}
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x001775A6 File Offset: 0x001757A6
		internal bool BodyRefersToReportItems
		{
			get
			{
				return this.m_langParser.BodyRefersToReportItems;
			}
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x001775B3 File Offset: 0x001757B3
		internal bool PageSectionRefersToReportItems
		{
			get
			{
				return this.m_langParser.PageSectionRefersToReportItems;
			}
		}

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x001775C0 File Offset: 0x001757C0
		internal int NumberOfAggregates
		{
			get
			{
				return this.m_langParser.NumberOfAggregates;
			}
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x001775CD File Offset: 0x001757CD
		internal int LastAggregateID
		{
			get
			{
				return this.m_langParser.LastID;
			}
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x001775DA File Offset: 0x001757DA
		internal bool ValueReferenced
		{
			get
			{
				return this.m_langParser.ValueReferenced;
			}
		}

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x06005B4C RID: 23372 RVA: 0x001775E7 File Offset: 0x001757E7
		internal bool ValueReferencedGlobal
		{
			get
			{
				return this.m_langParser.ValueReferencedGlobal;
			}
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x001775F4 File Offset: 0x001757F4
		internal ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context)
		{
			ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x00177618 File Offset: 0x00175818
		internal ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.DetectionFlags flag, out bool reportParameterReferenced, out string reportParameterName, out bool userCollectionReferenced)
		{
			ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context, flag, out reportParameterReferenced, out reportParameterName, out userCollectionReferenced);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x00177644 File Offset: 0x00175844
		internal ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, out bool userCollectionReferenced)
		{
			ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context, out userCollectionReferenced);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x0017766C File Offset: 0x0017586C
		internal void ConvertFields2ComplexExpr()
		{
			if (this.m_reportLevelFieldReferences != null)
			{
				for (int i = this.m_reportLevelFieldReferences.Count - 1; i >= 0; i--)
				{
					ReportCompileTime.ExprCompileTimeInfo exprCompileTimeInfo = (ReportCompileTime.ExprCompileTimeInfo)this.m_reportLevelFieldReferences[i];
					this.m_langParser.ConvertField2ComplexExpr(ref exprCompileTimeInfo.ExpressionInfo);
					this.RegisterExpression(exprCompileTimeInfo);
				}
			}
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x001776C3 File Offset: 0x001758C3
		internal void ResetValueReferencedFlag()
		{
			this.m_langParser.ResetValueReferencedFlag();
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x001776D0 File Offset: 0x001758D0
		internal byte[] Compile(Report report, AppDomain compilationTempAppDomain, bool refusePermissions)
		{
			byte[] result = null;
			RevertImpersonationContext.Run(delegate
			{
				result = this.InternalCompile(report, compilationTempAppDomain, refusePermissions);
			});
			return result;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x00177710 File Offset: 0x00175910
		private void ProcessExpression(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression.Type == ExpressionInfo.Types.Expression)
			{
				this.RegisterExpression(new ReportCompileTime.ExprCompileTimeInfo(expression, context));
				this.ProcessAggregateParams(expression, context);
				return;
			}
			if (expression.Type == ExpressionInfo.Types.Aggregate)
			{
				this.ProcessAggregateParams(expression, context);
				return;
			}
			if (expression.Type == ExpressionInfo.Types.Field && context.Location == LocationFlags.None)
			{
				if (this.m_reportLevelFieldReferences == null)
				{
					this.m_reportLevelFieldReferences = new ArrayList();
				}
				this.m_reportLevelFieldReferences.Add(new ReportCompileTime.ExprCompileTimeInfo(expression, context));
			}
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x00177786 File Offset: 0x00175986
		private void RegisterExpression(ReportCompileTime.ExprCompileTimeInfo exprCTInfo)
		{
			if (this.m_ctExprList == null)
			{
				this.m_ctExprList = new ReportCompileTime.ExprCompileTimeInfoList();
			}
			exprCTInfo.ExpressionInfo.CompileTimeID = this.m_ctExprList.Add(exprCTInfo);
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x001777B4 File Offset: 0x001759B4
		private void ProcessAggregateParams(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression.Aggregates != null)
			{
				for (int i = expression.Aggregates.Count - 1; i >= 0; i--)
				{
					this.ProcessAggregateParam(expression.Aggregates[i], context);
				}
			}
			if (expression.RunningValues != null)
			{
				for (int j = expression.RunningValues.Count - 1; j >= 0; j--)
				{
					this.ProcessAggregateParam(expression.RunningValues[j], context);
				}
			}
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x00177828 File Offset: 0x00175A28
		private void ProcessAggregateParam(DataAggregateInfo aggregate, ExpressionParser.ExpressionContext context)
		{
			if (aggregate != null && aggregate.Expressions != null)
			{
				for (int i = 0; i < aggregate.Expressions.Length; i++)
				{
					this.ProcessAggregateParam(aggregate.Expressions[i], context);
				}
			}
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x00177862 File Offset: 0x00175A62
		private void ProcessAggregateParam(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression != null && expression.Type == ExpressionInfo.Types.Expression)
			{
				this.RegisterExpression(new ReportCompileTime.ExprCompileTimeInfo(expression, context));
			}
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x0017787C File Offset: 0x00175A7C
		private byte[] InternalCompile(Report report, AppDomain compilationTempAppDomain, bool refusePermissions)
		{
			if (this.m_builder.HasExpressions)
			{
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerParameters.OutputAssembly = string.Format(CultureInfo.InvariantCulture, "{0}{1}.dll", Path.GetTempPath(), report.ExprHostAssemblyName);
				compilerParameters.GenerateExecutable = false;
				compilerParameters.GenerateInMemory = false;
				compilerParameters.IncludeDebugInformation = false;
				compilerParameters.ReferencedAssemblies.Add("System.dll");
				compilerParameters.ReferencedAssemblies.Add(typeof(ReportObjectModelProxy).Assembly.Location);
				CompilerParameters compilerParameters2 = compilerParameters;
				compilerParameters2.CompilerOptions += this.m_langParser.GetCompilerArguments();
				if (report.CodeModules != null)
				{
					this.ResolveAssemblylocations(report.CodeModules, compilerParameters, this.m_errorContext, compilationTempAppDomain);
				}
				CompilerResults compilerResults = null;
				try
				{
					CodeCompileUnit exprHost = this.m_builder.GetExprHost(report.IntermediateFormatVersion, refusePermissions);
					report.CompiledCodeGeneratedWithRefusedPermissions = refusePermissions;
					CodeDomProvider codeCompiler = this.m_langParser.GetCodeCompiler();
					compilerResults = codeCompiler.CompileAssemblyFromDom(compilerParameters, new CodeCompileUnit[] { exprHost });
					if (compilerResults.NativeCompilerReturnValue != 0 || compilerResults.Errors.Count > 0)
					{
						if (Global.Tracer.TraceVerbose)
						{
							try
							{
								using (MemoryStream memoryStream = new MemoryStream())
								{
									IndentedTextWriter indentedTextWriter = new IndentedTextWriter(new StreamWriter(memoryStream), "    ");
									codeCompiler.GenerateCodeFromCompileUnit(exprHost, indentedTextWriter, new CodeGeneratorOptions());
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
						this.ParseErrors(compilerResults, report.CodeClasses);
						return new byte[0];
					}
					using (FileStream fileStream = File.OpenRead(compilerResults.PathToAssembly))
					{
						byte[] array = new byte[fileStream.Length];
						int num = fileStream.Read(array, 0, (int)fileStream.Length);
						Global.Tracer.Assert((long)num == fileStream.Length, "(read == fs.Length)");
						return array;
					}
				}
				finally
				{
					if (compilerResults != null && compilerResults.PathToAssembly != null)
					{
						File.Delete(compilerResults.PathToAssembly);
					}
				}
			}
			return new byte[0];
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x00177AFC File Offset: 0x00175CFC
		private void ResolveAssemblylocations(StringList codeModules, CompilerParameters options, ErrorContext errorContext, AppDomain compilationTempAppDomain)
		{
			AssemblyLocationResolver assemblyLocationResolver = AssemblyLocationResolver.CreateResolver(compilationTempAppDomain);
			for (int i = codeModules.Count - 1; i >= 0; i--)
			{
				try
				{
					options.ReferencedAssemblies.Add(assemblyLocationResolver.LoadAssemblyAndResolveLocation(codeModules[i]));
				}
				catch (Exception ex)
				{
					ProcessingMessage processingMessage = errorContext.Register(ProcessingErrorCode.rsErrorLoadingCodeModule, Severity.Error, ObjectType.Report, null, null, new string[]
					{
						codeModules[i],
						ex.Message
					});
					if (Global.Tracer.TraceError && processingMessage != null)
					{
						Global.Tracer.Trace(TraceLevel.Error, processingMessage.Message + Environment.NewLine + ex.ToString());
					}
				}
			}
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x00177BAC File Offset: 0x00175DAC
		private void ParseErrors(CompilerResults results, CodeClassList codeClassInstDecls)
		{
			int count = results.Errors.Count;
			if (results.NativeCompilerReturnValue != 0 && count == 0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsUnexpectedCompilerError, Severity.Error, ObjectType.Report, null, null, new string[] { results.NativeCompilerReturnValue.ToString(CultureInfo.CurrentCulture) });
			}
			for (int i = 0; i < count; i++)
			{
				CompilerError compilerError = results.Errors[i];
				int num;
				switch (this.m_builder.ParseErrorSource(compilerError, out num))
				{
				case ExprHostBuilder.ErrorSource.Expression:
				{
					ReportCompileTime.ExprCompileTimeInfo exprCompileTimeInfo = this.m_ctExprList[num];
					this.RegisterError(compilerError, ref exprCompileTimeInfo.NumErrors, ref exprCompileTimeInfo.NumWarnings, exprCompileTimeInfo.OwnerObjectType, exprCompileTimeInfo.OwnerObjectName, exprCompileTimeInfo.OwnerPropertyName, ProcessingErrorCode.rsCompilerErrorInExpression);
					break;
				}
				case ExprHostBuilder.ErrorSource.CodeModuleClassInstanceDecl:
				{
					if (this.m_ctClassInstDeclList == null)
					{
						this.m_ctClassInstDeclList = new ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfoList();
					}
					ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfo codeModuleClassInstanceDeclCompileTimeInfo = this.m_ctClassInstDeclList[num];
					this.RegisterError(compilerError, ref codeModuleClassInstanceDeclCompileTimeInfo.NumErrors, ref codeModuleClassInstanceDeclCompileTimeInfo.NumWarnings, ObjectType.CodeClass, codeClassInstDecls[num].ClassName, null, ProcessingErrorCode.rsCompilerErrorInClassInstanceDeclaration);
					break;
				}
				case ExprHostBuilder.ErrorSource.CustomCode:
					this.RegisterError(compilerError, ref this.m_customCodeNumErrors, ref this.m_customCodeNumWarnings, ObjectType.Report, null, null, ProcessingErrorCode.rsCompilerErrorInCode);
					break;
				default:
					this.m_errorContext.Register(ProcessingErrorCode.rsUnexpectedCompilerError, Severity.Error, ObjectType.Report, null, null, new string[] { this.FormatError(compilerError) });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
			}
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x00177D28 File Offset: 0x00175F28
		private void RegisterError(CompilerError error, ref int numErrors, ref int numWarnings, ObjectType objectType, string objectName, string propertyName, ProcessingErrorCode errorCode)
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
					this.m_errorContext.Register(errorCode, severity, objectType, objectName, propertyName, new string[]
					{
						this.FormatError(error),
						error.Line.ToString(CultureInfo.CurrentCulture)
					});
				}
			}
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x00177DA4 File Offset: 0x00175FA4
		private string FormatError(CompilerError error)
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}] {1}", error.ErrorNumber, error.ErrorText);
		}

		// Token: 0x04002F7C RID: 12156
		private ExpressionParser m_langParser;

		// Token: 0x04002F7D RID: 12157
		private ErrorContext m_errorContext;

		// Token: 0x04002F7E RID: 12158
		private ExprHostBuilder m_builder;

		// Token: 0x04002F7F RID: 12159
		private ReportCompileTime.ExprCompileTimeInfoList m_ctExprList;

		// Token: 0x04002F80 RID: 12160
		private ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfoList m_ctClassInstDeclList;

		// Token: 0x04002F81 RID: 12161
		private int m_customCodeNumErrors;

		// Token: 0x04002F82 RID: 12162
		private int m_customCodeNumWarnings;

		// Token: 0x04002F83 RID: 12163
		private ArrayList m_reportLevelFieldReferences;

		// Token: 0x02000CAD RID: 3245
		private sealed class ExprCompileTimeInfo
		{
			// Token: 0x06008CC3 RID: 36035 RVA: 0x0023CAF4 File Offset: 0x0023ACF4
			internal ExprCompileTimeInfo(ExpressionInfo expression, ExpressionParser.ExpressionContext context)
			{
				this.ExpressionInfo = expression;
				this.OwnerObjectType = context.ObjectType;
				this.OwnerObjectName = context.ObjectName;
				this.OwnerPropertyName = context.PropertyName;
				this.NumErrors = 0;
				this.NumWarnings = 0;
			}

			// Token: 0x04004D97 RID: 19863
			internal ExpressionInfo ExpressionInfo;

			// Token: 0x04004D98 RID: 19864
			internal ObjectType OwnerObjectType;

			// Token: 0x04004D99 RID: 19865
			internal string OwnerObjectName;

			// Token: 0x04004D9A RID: 19866
			internal string OwnerPropertyName;

			// Token: 0x04004D9B RID: 19867
			internal int NumErrors;

			// Token: 0x04004D9C RID: 19868
			internal int NumWarnings;
		}

		// Token: 0x02000CAE RID: 3246
		private sealed class ExprCompileTimeInfoList : ArrayList
		{
			// Token: 0x17002B47 RID: 11079
			internal ReportCompileTime.ExprCompileTimeInfo this[int exprCTId]
			{
				get
				{
					return (ReportCompileTime.ExprCompileTimeInfo)base[exprCTId];
				}
			}
		}

		// Token: 0x02000CAF RID: 3247
		private sealed class CodeModuleClassInstanceDeclCompileTimeInfo
		{
			// Token: 0x04004D9D RID: 19869
			internal int NumErrors;

			// Token: 0x04004D9E RID: 19870
			internal int NumWarnings;
		}

		// Token: 0x02000CB0 RID: 3248
		private sealed class CodeModuleClassInstanceDeclCompileTimeInfoList : Hashtable
		{
			// Token: 0x17002B48 RID: 11080
			internal ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfo this[object id]
			{
				get
				{
					ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfo codeModuleClassInstanceDeclCompileTimeInfo = (ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfo)base[id];
					if (codeModuleClassInstanceDeclCompileTimeInfo == null)
					{
						codeModuleClassInstanceDeclCompileTimeInfo = new ReportCompileTime.CodeModuleClassInstanceDeclCompileTimeInfo();
						base.Add(id, codeModuleClassInstanceDeclCompileTimeInfo);
					}
					return codeModuleClassInstanceDeclCompileTimeInfo;
				}
			}
		}
	}
}
