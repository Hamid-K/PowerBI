using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000563 RID: 1379
	internal sealed class ExprHostCompiler
	{
		// Token: 0x06004D9A RID: 19866 RVA: 0x0013E555 File Offset: 0x0013C755
		internal ExprHostCompiler(ExpressionParser langParser, ErrorContext errorContext)
		{
			this.m_langParser = langParser;
			this.m_errorContext = errorContext;
			this.m_builder = new ExprHostBuilder();
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x0013E576 File Offset: 0x0013C776
		internal ExprHostBuilder Builder
		{
			get
			{
				return this.m_builder;
			}
		}

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x0013E57E File Offset: 0x0013C77E
		internal bool BodyRefersToReportItems
		{
			get
			{
				return this.m_langParser.BodyRefersToReportItems;
			}
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06004D9D RID: 19869 RVA: 0x0013E58B File Offset: 0x0013C78B
		internal bool PageSectionRefersToReportItems
		{
			get
			{
				return this.m_langParser.PageSectionRefersToReportItems;
			}
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06004D9E RID: 19870 RVA: 0x0013E598 File Offset: 0x0013C798
		internal bool PageSectionRefersToOverallTotalPages
		{
			get
			{
				return this.m_langParser.PageSectionRefersToOverallTotalPages;
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06004D9F RID: 19871 RVA: 0x0013E5A5 File Offset: 0x0013C7A5
		internal bool PageSectionRefersToTotalPages
		{
			get
			{
				return this.m_langParser.PageSectionRefersToTotalPages;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x06004DA0 RID: 19872 RVA: 0x0013E5B2 File Offset: 0x0013C7B2
		internal int NumberOfAggregates
		{
			get
			{
				return this.m_langParser.NumberOfAggregates;
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x0013E5BF File Offset: 0x0013C7BF
		internal int LastAggregateID
		{
			get
			{
				return this.m_langParser.LastID;
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06004DA2 RID: 19874 RVA: 0x0013E5CC File Offset: 0x0013C7CC
		internal int LastLookupID
		{
			get
			{
				return this.m_langParser.LastLookupID;
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x0013E5D9 File Offset: 0x0013C7D9
		internal bool PreviousAggregateUsed
		{
			get
			{
				return this.m_langParser.PreviousAggregateUsed;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x06004DA4 RID: 19876 RVA: 0x0013E5E6 File Offset: 0x0013C7E6
		internal bool AggregateOfAggregateUsed
		{
			get
			{
				return this.m_langParser.AggregateOfAggregatesUsed;
			}
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x0013E5F3 File Offset: 0x0013C7F3
		internal bool AggregateOfAggregateUsedInUserSort
		{
			get
			{
				return this.m_langParser.AggregateOfAggregatesUsedInUserSort;
			}
		}

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x0013E600 File Offset: 0x0013C800
		internal bool ValueReferenced
		{
			get
			{
				return this.m_langParser.ValueReferenced;
			}
		}

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x0013E60D File Offset: 0x0013C80D
		internal bool ValueReferencedGlobal
		{
			get
			{
				return this.m_langParser.ValueReferencedGlobal;
			}
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x0013E61C File Offset: 0x0013C81C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ParseExpression(string expression, ExpressionParser.EvaluationMode evaluationMode, ExpressionParser.ExpressionContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context, evaluationMode);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x0013E644 File Offset: 0x0013C844
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.EvaluationMode evaluationMode, out bool userCollectionReferenced)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.m_langParser.ParseExpression(expression, context, evaluationMode, out userCollectionReferenced);
			this.ProcessExpression(expressionInfo, context);
			return expressionInfo;
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x0013E66B File Offset: 0x0013C86B
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo CreateScopedFirstAggregate(string fieldName, string dataSetName)
		{
			return this.m_langParser.CreateScopedFirstAggregate(fieldName, dataSetName);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x0013E67C File Offset: 0x0013C87C
		internal void ConvertFields2ComplexExpr()
		{
			if (this.m_reportLevelFieldReferences != null)
			{
				for (int i = this.m_reportLevelFieldReferences.Count - 1; i >= 0; i--)
				{
					ExprCompileTimeInfo exprCompileTimeInfo = (ExprCompileTimeInfo)this.m_reportLevelFieldReferences[i];
					this.m_langParser.ConvertField2ComplexExpr(ref exprCompileTimeInfo.ExpressionInfo);
					this.RegisterExpression(exprCompileTimeInfo);
				}
			}
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0013E6D3 File Offset: 0x0013C8D3
		internal void ResetValueReferencedFlag()
		{
			this.m_langParser.ResetValueReferencedFlag();
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x0013E6E0 File Offset: 0x0013C8E0
		internal void ResetPageSectionRefersFlags()
		{
			this.m_langParser.ResetPageSectionRefersFlags();
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x0013E6F0 File Offset: 0x0013C8F0
		internal byte[] Compile(IExpressionHostAssemblyHolder expressionHostAssemblyHolder, AppDomain compilationTempAppDomain, bool refusePermissions, PublishingVersioning versioning)
		{
			byte[] result = null;
			if (this.m_builder.HasExpressions && versioning.IsRdlFeatureRestricted(RdlFeatures.ComplexExpression))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidComplexExpressionInReport, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.Report, "Report", "Body", Array.Empty<string>());
			}
			else
			{
				this.m_expressionHostAssemblyHolder = expressionHostAssemblyHolder;
				RevertImpersonationContext.Run(delegate
				{
					result = this.InternalCompile(compilationTempAppDomain, refusePermissions);
				});
			}
			return result;
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x0013E77C File Offset: 0x0013C97C
		private void ProcessExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
			{
				this.RegisterExpression(new ExprCompileTimeInfo(expression, context));
				this.ProcessAggregateParams(expression, context);
				return;
			}
			if (expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate)
			{
				this.ProcessAggregateParams(expression, context);
				return;
			}
			if (expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field && context.Location == Microsoft.ReportingServices.ReportPublishing.LocationFlags.None)
			{
				if (this.m_reportLevelFieldReferences == null)
				{
					this.m_reportLevelFieldReferences = new ArrayList();
				}
				this.m_reportLevelFieldReferences.Add(new ExprCompileTimeInfo(expression, context));
			}
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0013E7F2 File Offset: 0x0013C9F2
		private void RegisterExpression(ExprCompileTimeInfo exprCTInfo)
		{
			if (this.m_ctExprList == null)
			{
				this.m_ctExprList = new ExprCompileTimeInfoList();
			}
			exprCTInfo.ExpressionInfo.CompileTimeID = this.m_ctExprList.Add(exprCTInfo);
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0013E820 File Offset: 0x0013CA20
		private void ProcessAggregateParams(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ExpressionParser.ExpressionContext context)
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

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0013E894 File Offset: 0x0013CA94
		private void ProcessAggregateParam(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo aggregate, ExpressionParser.ExpressionContext context)
		{
			if (aggregate != null && aggregate.Expressions != null)
			{
				for (int i = 0; i < aggregate.Expressions.Length; i++)
				{
					this.ProcessAggregateParam(aggregate.Expressions[i], context);
				}
			}
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x0013E8CE File Offset: 0x0013CACE
		private void ProcessAggregateParam(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ExpressionParser.ExpressionContext context)
		{
			if (expression != null && expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
			{
				this.RegisterExpression(new ExprCompileTimeInfo(expression, context));
			}
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x0013E8E8 File Offset: 0x0013CAE8
		private byte[] InternalCompile(AppDomain compilationTempAppDomain, bool refusePermissions)
		{
			if (this.m_builder.HasExpressions)
			{
				CompilerParameters compilerParameters = new CompilerParameters();
				compilerParameters.OutputAssembly = string.Format(CultureInfo.InvariantCulture, "{0}{1}.dll", Path.GetTempPath(), this.m_expressionHostAssemblyHolder.ExprHostAssemblyName);
				compilerParameters.GenerateExecutable = false;
				compilerParameters.GenerateInMemory = false;
				compilerParameters.IncludeDebugInformation = false;
				compilerParameters.ReferencedAssemblies.Add("System.dll");
				compilerParameters.ReferencedAssemblies.Add(typeof(ReportObjectModelProxy).Assembly.Location);
				compilerParameters.ReferencedAssemblies.Add(typeof(SqlGeography).Assembly.Location);
				CompilerParameters compilerParameters2 = compilerParameters;
				compilerParameters2.CompilerOptions += this.m_langParser.GetCompilerArguments();
				if (this.m_expressionHostAssemblyHolder.CodeModules != null)
				{
					this.ResolveAssemblylocations(this.m_expressionHostAssemblyHolder.CodeModules, compilerParameters, this.m_errorContext, compilationTempAppDomain);
				}
				ProcessingIntermediateFormatVersion processingIntermediateFormatVersion = new ProcessingIntermediateFormatVersion(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatVersion.Current);
				CodeCompileUnit exprHost = this.m_builder.GetExprHost(processingIntermediateFormatVersion, refusePermissions);
				this.m_expressionHostAssemblyHolder.CompiledCodeGeneratedWithRefusedPermissions = refusePermissions;
				CodeDomProvider codeCompiler = this.m_langParser.GetCodeCompiler();
				return CompilerFactory.CreateCompiler().Compile(codeCompiler, this.m_expressionHostAssemblyHolder, compilerParameters, exprHost, this.m_errorContext, this.m_ctExprList, this.m_ctClassInstDeclList, ref this.m_customCodeNumErrors, ref this.m_customCodeNumWarnings);
			}
			return new byte[0];
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0013EA44 File Offset: 0x0013CC44
		private void ResolveAssemblylocations(List<string> codeModules, CompilerParameters options, ErrorContext errorContext, AppDomain compilationTempAppDomain)
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
					ProcessingMessage processingMessage = errorContext.Register(ProcessingErrorCode.rsErrorLoadingCodeModule, Severity.Error, this.m_expressionHostAssemblyHolder.ObjectType, null, null, new string[]
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

		// Token: 0x04002866 RID: 10342
		private ExpressionParser m_langParser;

		// Token: 0x04002867 RID: 10343
		private ErrorContext m_errorContext;

		// Token: 0x04002868 RID: 10344
		private ExprHostBuilder m_builder;

		// Token: 0x04002869 RID: 10345
		private ExprCompileTimeInfoList m_ctExprList;

		// Token: 0x0400286A RID: 10346
		private CodeModuleClassInstanceDeclCompileTimeInfoList m_ctClassInstDeclList;

		// Token: 0x0400286B RID: 10347
		private int m_customCodeNumErrors;

		// Token: 0x0400286C RID: 10348
		private int m_customCodeNumWarnings;

		// Token: 0x0400286D RID: 10349
		private ArrayList m_reportLevelFieldReferences;

		// Token: 0x0400286E RID: 10350
		private IExpressionHostAssemblyHolder m_expressionHostAssemblyHolder;
	}
}
