using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000391 RID: 913
	internal class RdlSandboxValidator
	{
		// Token: 0x0600252E RID: 9518 RVA: 0x000B1C40 File Offset: 0x000AFE40
		internal RdlSandboxValidator(IRdlSandboxConfig sandboxingConfig, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, AppDomain compileAppDomain, ErrorContext errorContext)
		{
			this.m_errorContext = errorContext;
			this.m_parserEnvironment = this.SetupRdlSandboxParserEnvironment(sandboxingConfig, report, compileAppDomain);
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000B1C5F File Offset: 0x000AFE5F
		internal RdlSandboxValidator.ExpressionValidator CreateExpressionValidator()
		{
			return new RdlSandboxValidator.ExpressionValidator(this);
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000B1C67 File Offset: 0x000AFE67
		internal RdlSandboxValidator.ClassValidator CreateClassValidator()
		{
			return new RdlSandboxValidator.ClassValidator(this);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000B1C70 File Offset: 0x000AFE70
		private void Validate(string expressionText, RdlSandboxValidator.IRdlSandboxValidationErrorHandler errorHandler)
		{
			Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ExpressionParser expressionParser = new Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ExpressionParser(this.m_parserEnvironment);
			try
			{
				expressionParser.Parse(expressionText);
			}
			catch (ExpressionParserInvalidTypeNameException ex)
			{
				errorHandler.HandleInvalidTypeOrMember(ex.TypeName);
			}
			catch (ExpressionParserInvalidMemberNameException ex2)
			{
				errorHandler.HandleInvalidTypeOrMember(ex2.MemberName);
			}
			catch (ExpressionParserUnknownIdentifierException ex3)
			{
				errorHandler.HandleInvalidTypeOrMember(ex3.Name);
			}
			catch (ExpressionParserInvalidArrayTypeException ex4)
			{
				errorHandler.HandleInvalidNewType(ex4.TypeName);
			}
			catch (ExpressionParserInvalidNewTypeException ex5)
			{
				errorHandler.HandleInvalidNewType(ex5.TypeName);
			}
			catch (Exception ex6)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex6))
				{
					throw;
				}
				errorHandler.HandleParseError();
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000B1D40 File Offset: 0x000AFF40
		private EnvironmentContext SetupRdlSandboxParserEnvironment(IRdlSandboxConfig sandboxConfig, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, AppDomain compilationTempAppDomain)
		{
			if (RdlSandboxValidator.m_cachedRdlSandboxParserEnvironment == null || sandboxConfig != RdlSandboxValidator.m_cachedRdlSandboxParserEnvironmentGuard)
			{
				RdlSandboxValidator.m_cachedRdlSandboxFilter = new RdlSandboxEnvironmentFilter(sandboxConfig);
				RdlSandboxValidator.m_cachedRdlSandboxParserEnvironment = new EnvironmentContext(new ReportObjectModelContext(), RdlSandboxValidator.m_cachedRdlSandboxFilter);
				RdlSandboxValidator.m_cachedRdlSandboxParserEnvironmentGuard = sandboxConfig;
			}
			RdlSandboxEnvironmentFilter cachedRdlSandboxFilter = RdlSandboxValidator.m_cachedRdlSandboxFilter;
			EnvironmentContext environmentContext = RdlSandboxValidator.m_cachedRdlSandboxParserEnvironment;
			List<string> codeModules = ((IExpressionHostAssemblyHolder)report).CodeModules;
			if (codeModules != null && codeModules.Count > 0)
			{
				if (compilationTempAppDomain != null)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsSandboxingCodeModuleUnavailableMode, Severity.Error, ObjectType.Report, "Report", "CodeModule", Array.Empty<string>());
					return null;
				}
				List<Assembly> list = new List<Assembly>(codeModules.Count);
				foreach (string text in codeModules)
				{
					try
					{
						list.Add(Assembly.Load(text));
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						this.m_errorContext.Register(ProcessingErrorCode.rsSandboxingInvalidCodeModule, Severity.Error, ObjectType.Report, "CodeModules", "CodeModule", new string[] { text });
					}
				}
				environmentContext = environmentContext.InitializeCustomAssemblies(list);
			}
			return environmentContext;
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000B1E6C File Offset: 0x000B006C
		private ErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x040015C6 RID: 5574
		private EnvironmentContext m_parserEnvironment;

		// Token: 0x040015C7 RID: 5575
		protected ErrorContext m_errorContext;

		// Token: 0x040015C8 RID: 5576
		private static EnvironmentContext m_cachedRdlSandboxParserEnvironment;

		// Token: 0x040015C9 RID: 5577
		private static RdlSandboxEnvironmentFilter m_cachedRdlSandboxFilter;

		// Token: 0x040015CA RID: 5578
		private static object m_cachedRdlSandboxParserEnvironmentGuard;

		// Token: 0x02000956 RID: 2390
		private interface IRdlSandboxValidationErrorHandler
		{
			// Token: 0x06007FF9 RID: 32761
			void HandleInvalidTypeOrMember(string typeOrMemberName);

			// Token: 0x06007FFA RID: 32762
			void HandleInvalidNewType(string typeOrMemberName);

			// Token: 0x06007FFB RID: 32763
			void HandleParseError();
		}

		// Token: 0x02000957 RID: 2391
		internal sealed class ExpressionValidator : RdlSandboxValidator.IRdlSandboxValidationErrorHandler
		{
			// Token: 0x06007FFC RID: 32764 RVA: 0x002104FF File Offset: 0x0020E6FF
			internal ExpressionValidator(RdlSandboxValidator validator)
			{
				this.m_validator = validator;
			}

			// Token: 0x06007FFD RID: 32765 RVA: 0x00210510 File Offset: 0x0020E710
			void RdlSandboxValidator.IRdlSandboxValidationErrorHandler.HandleInvalidTypeOrMember(string typeOrMemberName)
			{
				this.m_validator.ErrorContext.Register(ProcessingErrorCode.rsSandboxingInvalidTypeOrMemberName, Severity.Error, this.m_objectType, this.m_objectName, this.m_propertyName, new string[] { typeOrMemberName });
			}

			// Token: 0x06007FFE RID: 32766 RVA: 0x00210550 File Offset: 0x0020E750
			void RdlSandboxValidator.IRdlSandboxValidationErrorHandler.HandleInvalidNewType(string typeName)
			{
				this.m_validator.ErrorContext.Register(ProcessingErrorCode.rsSandboxingInvalidNewType, Severity.Error, this.m_objectType, this.m_objectName, this.m_propertyName, new string[] { typeName });
			}

			// Token: 0x06007FFF RID: 32767 RVA: 0x00210590 File Offset: 0x0020E790
			void RdlSandboxValidator.IRdlSandboxValidationErrorHandler.HandleParseError()
			{
				this.m_validator.ErrorContext.Register(ProcessingErrorCode.rsSandboxingInvalidExpression, Severity.Error, this.m_objectType, this.m_objectName, this.m_propertyName, Array.Empty<string>());
			}

			// Token: 0x06008000 RID: 32768 RVA: 0x002105C0 File Offset: 0x0020E7C0
			internal void Validate(string expressionText, ObjectType objectType, string objectName, string propertyName)
			{
				this.m_objectType = objectType;
				this.m_objectName = objectName;
				this.m_propertyName = propertyName;
				this.m_validator.Validate(expressionText, this);
			}

			// Token: 0x04004079 RID: 16505
			private ObjectType m_objectType;

			// Token: 0x0400407A RID: 16506
			private string m_objectName;

			// Token: 0x0400407B RID: 16507
			private string m_propertyName;

			// Token: 0x0400407C RID: 16508
			private RdlSandboxValidator m_validator;
		}

		// Token: 0x02000958 RID: 2392
		internal sealed class ClassValidator : RdlSandboxValidator.IRdlSandboxValidationErrorHandler
		{
			// Token: 0x06008001 RID: 32769 RVA: 0x002105E5 File Offset: 0x0020E7E5
			internal ClassValidator(RdlSandboxValidator validator)
			{
				this.m_validator = validator;
			}

			// Token: 0x06008002 RID: 32770 RVA: 0x002105F4 File Offset: 0x0020E7F4
			void RdlSandboxValidator.IRdlSandboxValidationErrorHandler.HandleInvalidTypeOrMember(string typeOrMemberName)
			{
				this.m_validator.ErrorContext.Register(ProcessingErrorCode.rsSandboxingInvalidClassName, Severity.Error, ObjectType.CodeClass, this.m_instanceName, this.m_className, Array.Empty<string>());
			}

			// Token: 0x06008003 RID: 32771 RVA: 0x00210620 File Offset: 0x0020E820
			void RdlSandboxValidator.IRdlSandboxValidationErrorHandler.HandleInvalidNewType(string typeOrMemberName)
			{
				this.m_validator.ErrorContext.Register(ProcessingErrorCode.rsSandboxingInvalidClassName, Severity.Error, ObjectType.CodeClass, this.m_instanceName, this.m_className, Array.Empty<string>());
			}

			// Token: 0x06008004 RID: 32772 RVA: 0x0021064C File Offset: 0x0020E84C
			void RdlSandboxValidator.IRdlSandboxValidationErrorHandler.HandleParseError()
			{
				this.m_validator.ErrorContext.Register(ProcessingErrorCode.rsSandboxingInvalidClassName, Severity.Error, ObjectType.CodeClass, this.m_instanceName, this.m_className, Array.Empty<string>());
			}

			// Token: 0x06008005 RID: 32773 RVA: 0x00210678 File Offset: 0x0020E878
			internal void Validate(Microsoft.ReportingServices.ReportIntermediateFormat.CodeClass codeClass)
			{
				this.m_className = codeClass.ClassName;
				this.m_instanceName = codeClass.InstanceName;
				StringBuilder stringBuilder = new StringBuilder("=New ");
				stringBuilder.Append(codeClass.ClassName);
				stringBuilder.Append("()");
				this.m_validator.Validate(stringBuilder.ToString(), this);
			}

			// Token: 0x0400407D RID: 16509
			private string m_className;

			// Token: 0x0400407E RID: 16510
			private string m_instanceName;

			// Token: 0x0400407F RID: 16511
			private RdlSandboxValidator m_validator;
		}
	}
}
