using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200057D RID: 1405
	internal sealed class VBExpressionParser : ExpressionParser
	{
		// Token: 0x060050F1 RID: 20721 RVA: 0x001541ED File Offset: 0x001523ED
		internal VBExpressionParser(ErrorContext errorContext)
			: base(errorContext)
		{
			this.m_regexes = VBExpressionParser.ReportRegularExpressions.Value;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0015420C File Offset: 0x0015240C
		internal VBExpressionParser(ErrorContext errorContext, ExpressionUsage expressionUsage)
			: base(errorContext)
		{
			this.m_regexes = VBExpressionParser.ReportRegularExpressions.Value;
			this.m_expressionUsage = expressionUsage;
			this.m_expressionExtractor = new ExpressionExtractor(this.m_expressionUsage);
			this.m_safeExpressionsValidator = new SafeExpressionsValidator();
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00154259 File Offset: 0x00152459
		internal override CodeDomProvider GetCodeCompiler()
		{
			return new VBExpressionCodeProvider();
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x00154260 File Offset: 0x00152460
		internal override string GetCompilerArguments()
		{
			return "/optimize+";
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00154268 File Offset: 0x00152468
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.EvaluationMode evaluationMode)
		{
			Global.Tracer.Assert(expression != null, "(null != expression)");
			string text;
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.Lex(expression, context, evaluationMode, out text);
			this.CheckSafeExpressionsRuntimeSupport(expressionInfo);
			this.AddToExpressionInstrumentation(expressionInfo);
			return expressionInfo;
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x001542A4 File Offset: 0x001524A4
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.EvaluationMode evaluationMode, out bool userCollectionReferenced)
		{
			string text;
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = this.Lex(expression, context, evaluationMode, out text);
			this.CheckSafeExpressionsRuntimeSupport(expressionInfo);
			this.AddToExpressionInstrumentation(expressionInfo);
			userCollectionReferenced = false;
			if (expressionInfo.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
			{
				userCollectionReferenced = this.DetectUserReference(text);
				expressionInfo.SimpleParameterName = this.GetSimpleParameterReferenceName(text);
			}
			return expressionInfo;
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x001542F0 File Offset: 0x001524F0
		private void CheckSafeExpressionsRuntimeSupport(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if (this.m_expressionUsage == null || !this.m_expressionUsage.CanUseSafeExpressionsRuntime)
			{
				return;
			}
			try
			{
				ExpressionValidationResult expressionValidationResult = this.m_safeExpressionsValidator.ValidateExpression(expressionInfo);
				this.m_expressionUsage.CanUseSafeExpressionsRuntime = expressionValidationResult.Supported;
				if (this.m_expressionUsage.IsEnabledForSafeExpressionsRuntime)
				{
					this.m_expressionUsage.IsEnabledForSafeExpressionsRuntime = expressionValidationResult.Enabled;
				}
			}
			catch (Exception ex)
			{
				Global.Tracer.Trace(TraceLevel.Error, string.Format("An exception occurred when checking support for Safe Expressions Runtime. Details: {0}", ex));
				this.m_expressionUsage.CanUseSafeExpressionsRuntime = false;
			}
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0015438C File Offset: 0x0015258C
		private void AddToExpressionInstrumentation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if (this.m_expressionUsage == null || expressionInfo.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
			{
				return;
			}
			try
			{
				this.m_expressionExtractor.ExtractExpressionTelemetry(expressionInfo);
			}
			catch (Exception ex)
			{
				Global.Tracer.Trace(TraceLevel.Warning, string.Format("An exception occurred when extracting info about expression usage. Details: {0}", ex));
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x001543E4 File Offset: 0x001525E4
		internal override void ConvertField2ComplexExpr(ref Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo info)
		{
			Global.Tracer.Assert(info.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field, "(info.Type == ExpressionInfo.Types.Field)");
			info.Type = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression;
			info.TransformedExpression = "Fields!" + info.StringValue + ".Value";
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x00154424 File Offset: 0x00152624
		internal override void ResetPageSectionRefersFlags()
		{
			this.m_state.PageSectionRefersToReportItems = false;
			this.m_state.PageSectionRefersToOverallTotalPages = false;
			this.m_state.PageSectionRefersToTotalPages = false;
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x0015444A File Offset: 0x0015264A
		internal override bool BodyRefersToReportItems
		{
			get
			{
				return this.m_state.BodyRefersToReportItems;
			}
		}

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x00154457 File Offset: 0x00152657
		internal override bool PageSectionRefersToReportItems
		{
			get
			{
				return this.m_state.PageSectionRefersToReportItems;
			}
		}

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x00154464 File Offset: 0x00152664
		internal override bool PageSectionRefersToOverallTotalPages
		{
			get
			{
				return this.m_state.PageSectionRefersToOverallTotalPages;
			}
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x00154471 File Offset: 0x00152671
		internal override bool PageSectionRefersToTotalPages
		{
			get
			{
				return this.m_state.PageSectionRefersToTotalPages;
			}
		}

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x0015447E File Offset: 0x0015267E
		internal override int NumberOfAggregates
		{
			get
			{
				return this.m_state.NumberOfAggregates;
			}
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x0015448B File Offset: 0x0015268B
		internal override int LastID
		{
			get
			{
				return this.m_state.LastID;
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x00154498 File Offset: 0x00152698
		internal override int LastLookupID
		{
			get
			{
				return this.m_state.LastLookupID;
			}
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x001544A5 File Offset: 0x001526A5
		internal override bool PreviousAggregateUsed
		{
			get
			{
				return this.m_state.PreviousAggregateUsed;
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x001544B2 File Offset: 0x001526B2
		internal override bool AggregateOfAggregatesUsed
		{
			get
			{
				return this.m_state.AggregateOfAggregatesUsed;
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x001544BF File Offset: 0x001526BF
		internal override bool AggregateOfAggregatesUsedInUserSort
		{
			get
			{
				return this.m_state.AggregateOfAggregatesUsedInUserSort;
			}
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x001544CC File Offset: 0x001526CC
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo Lex(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.EvaluationMode evaluationMode, out string vbExpression)
		{
			vbExpression = null;
			this.m_context = context;
			if (context.MaxExpressionLength != -1 && expression != null && expression.Length > context.MaxExpressionLength)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsSandboxingExpressionExceedsMaximumLength, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { Convert.ToString(context.MaxExpressionLength, CultureInfo.InvariantCulture) });
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.OriginalText = expression;
			bool flag = evaluationMode == ExpressionParser.EvaluationMode.Constant;
			if (!flag)
			{
				Match match = this.m_regexes.NonConstant.Match(expression);
				if (match.Success)
				{
					vbExpression = expression.Substring(match.Length);
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				ExpressionParser.ParseRDLConstant(expression, expressionInfo, context.ConstantType, this.m_errorContext, context.ObjectType, context.ObjectName, context.PropertyName);
			}
			else
			{
				ExpressionParser.GrammarFlags grammarFlags;
				if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					grammarFlags = (ExpressionParser.GrammarFlags)(ExpressionParser.ExpressionType2Restrictions(this.m_context.ExpressionType) | ExpressionParser.Restrictions.InPageSection);
				}
				else
				{
					grammarFlags = (ExpressionParser.GrammarFlags)(ExpressionParser.ExpressionType2Restrictions(this.m_context.ExpressionType) | ExpressionParser.Restrictions.InBody);
				}
				if (this.m_context.ObjectType == ObjectType.Paragraph)
				{
					grammarFlags |= ExpressionParser.GrammarFlags.DenyMeDotValue;
				}
				if (this.m_regexes.HasLevelWithNoScope.Match(expression).Success)
				{
					expressionInfo.NullLevelDetected = true;
				}
				this.VBLex(vbExpression, false, grammarFlags, expressionInfo);
			}
			if (this.m_state.AggregateOfAggregatesUsed && context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.AggregatesOfAggregates))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpressionAggregatesOfAggregates, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			return expressionInfo;
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x00154698 File Offset: 0x00152898
		private string GetSimpleParameterReferenceName(string expression)
		{
			string text = null;
			Match match = this.m_regexes.ParameterOnly.Match(expression);
			if (match.Success)
			{
				text = match.Result("${parametername}");
			}
			return text;
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x001546CE File Offset: 0x001528CE
		private bool DetectUserReference(string expression)
		{
			return this.Detected(expression, this.m_regexes.UserDetection);
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x001546E4 File Offset: 0x001528E4
		private void VBLex(string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			VBExpressionParser.ParserState parserState = this.m_state.Save();
			if (!this.TryVBLex(expression, isParameter, grammarFlags, expressionInfo, true))
			{
				this.m_state = parserState;
				this.TryVBLex(expression, isParameter, grammarFlags, expressionInfo, false);
			}
			if (expressionInfo.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression && this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.ComplexExpression))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidComplexExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x00154774 File Offset: 0x00152974
		private bool TryVBLex(string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, bool parseSpecialFunctions)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyFields) == (ExpressionParser.GrammarFlags)0)
			{
				Match match = this.m_regexes.FieldOnly.Match(expression);
				if (match.Success)
				{
					string text = match.Result("${fieldname}");
					expressionInfo.SetAsSimpleFieldReference(text);
					return true;
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyDataSets) == (ExpressionParser.GrammarFlags)0)
			{
				Match match2 = this.m_regexes.RewrittenCommandText.Match(expression);
				if (match2.Success)
				{
					string text2 = match2.Result("${datasetname}");
					expressionInfo.AddReferencedDataSet(text2);
					expressionInfo.Type = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Token;
					expressionInfo.StringValue = text2;
					return true;
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyFields) == (ExpressionParser.GrammarFlags)0 && (grammarFlags & ExpressionParser.GrammarFlags.DenyScopes) == (ExpressionParser.GrammarFlags)0 && !this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.ScopesCollection))
			{
				Match match3 = this.m_regexes.ScopedFieldReferenceOnly.Match(expression);
				if (match3.Success)
				{
					string text3 = match3.Result("${scopename}");
					string text4 = match3.Result("${fieldname}");
					expressionInfo.SetAsScopedFieldReference(text3, text4);
					return true;
				}
			}
			this.EnforceRestrictions(ref expression, isParameter, grammarFlags, expressionInfo);
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			bool flag = false;
			while (i < expression.Length)
			{
				if (parseSpecialFunctions)
				{
					Match match4 = this.m_regexes.RdlFunction.Match(expression, i);
					if (match4.Success)
					{
						string text5 = match4.Result("${functionName}");
						if (string.IsNullOrEmpty(text5))
						{
							return false;
						}
						i = match4.Length;
						return !this.ParseRdlFunction(expressionInfo, i, text5, expression, grammarFlags, out i) && expression.Substring(i).Trim().Length <= 0;
					}
					else
					{
						parseSpecialFunctions = false;
					}
				}
				Match match5 = this.m_regexes.SpecialFunction.Match(expression, i);
				if (!match5.Success)
				{
					stringBuilder.Append(expression.Substring(i));
					i = expression.Length;
				}
				else
				{
					stringBuilder.Append(expression.Substring(i, match5.Index - i));
					string text6 = match5.Result("${sfname}");
					if (string.IsNullOrEmpty(text6))
					{
						stringBuilder.Append(match5.Value);
						i = match5.Index + match5.Length;
					}
					else
					{
						stringBuilder.Append(match5.Result("${prefix}"));
						i = match5.Index + match5.Length;
						bool flag2 = false;
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types types;
						string text7;
						if (this.AreEqualOrdinalIgnoreCase(text6, "Lookup"))
						{
							types = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_OneValue;
							LookupInfo lookup = this.GetLookup(i, text6, expression, isParameter, grammarFlags, LookupType.Lookup, out i, out text7);
							expressionInfo.AddLookup(lookup);
						}
						else if (this.AreEqualOrdinalIgnoreCase(text6, "LookupSet"))
						{
							types = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_MultiValue;
							LookupInfo lookup2 = this.GetLookup(i, text6, expression, isParameter, grammarFlags, LookupType.LookupSet, out i, out text7);
							expressionInfo.AddLookup(lookup2);
						}
						else if (this.AreEqualOrdinalIgnoreCase(text6, "MultiLookup"))
						{
							types = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_MultiValue;
							LookupInfo lookup3 = this.GetLookup(i, text6, expression, isParameter, grammarFlags, LookupType.MultiLookup, out i, out text7);
							expressionInfo.AddLookup(lookup3);
						}
						else
						{
							text7 = this.CreateAggregateID();
							VBExpressionParser.ParserState state = this.m_state;
							int numberOfAggregates = state.NumberOfAggregates;
							state.NumberOfAggregates = numberOfAggregates + 1;
							types = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate;
							if (this.AreEqualOrdinalIgnoreCase(text6, "Previous"))
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo;
								this.GetPreviousAggregate(i, text6, expression, isParameter, grammarFlags, out i, out runningValueInfo);
								runningValueInfo.Name = text7;
								expressionInfo.AddRunningValue(runningValueInfo);
								flag2 = true;
							}
							else if (this.AreEqualOrdinalIgnoreCase(text6, "RunningValue"))
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo2;
								this.GetRunningValue(i, text6, expression, isParameter, grammarFlags, out i, out runningValueInfo2);
								runningValueInfo2.Name = text7;
								expressionInfo.AddRunningValue(runningValueInfo2);
								flag2 = true;
							}
							else if (this.AreEqualOrdinalIgnoreCase(text6, "RowNumber"))
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo3;
								this.GetRowNumber(i, text6, expression, isParameter, grammarFlags, out i, out runningValueInfo3);
								runningValueInfo3.Name = text7;
								expressionInfo.AddRunningValue(runningValueInfo3);
								flag2 = true;
							}
							else
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo;
								this.GetAggregate(i, text6, expression, isParameter, grammarFlags, out i, out dataAggregateInfo);
								dataAggregateInfo.Name = text7;
								expressionInfo.AddAggregate(dataAggregateInfo);
								flag2 = false;
							}
						}
						if (!flag)
						{
							flag = true;
							string text8 = stringBuilder.ToString();
							if (text8.Trim().Length == 0 && expression.Substring(i).Trim().Length == 0)
							{
								expressionInfo.Type = types;
								expressionInfo.StringValue = text7;
								return true;
							}
						}
						if (types != Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate)
						{
							if (types - Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_OneValue > 1)
							{
								Global.Tracer.Assert(false, "Unexpected special function type {0}", new object[] { types });
							}
							else
							{
								expressionInfo.AddTransformedExpressionLookupInfo(stringBuilder.Length, text7);
								stringBuilder.Append("Lookups!");
								stringBuilder.Append(text7);
								if (types == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_OneValue)
								{
									stringBuilder.Append(".Value");
								}
								else
								{
									stringBuilder.Append(".Values");
								}
							}
						}
						else
						{
							expressionInfo.AddTransformedExpressionAggregateInfo(stringBuilder.Length, text7, flag2);
							stringBuilder.Append("Aggregates!");
							stringBuilder.Append(text7);
						}
					}
				}
			}
			string text9 = stringBuilder.ToString();
			this.GetReferencedFieldNames(text9, expressionInfo);
			this.GetReferencedParameterNames(text9, expressionInfo);
			this.GetReferencedDataSetNames(text9, expressionInfo);
			this.GetReferencedDataSourceNames(text9, expressionInfo);
			this.GetReferencedVariableNames(text9, expressionInfo);
			this.GetReferencedScopesAndScopedFields(text9, expressionInfo);
			this.GetReferencedReportItemNames(text9, expressionInfo);
			this.GetReferencedReportItemNames(expressionInfo);
			this.GetMeDotValueReferences(text9, expressionInfo);
			this.GetMeDotValueReferences(expressionInfo);
			expressionInfo.Type = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression;
			expressionInfo.TransformedExpression = text9;
			if ((this.m_context.ObjectType == ObjectType.Textbox || this.m_context.ObjectType == ObjectType.TextRun) && expressionInfo.MeDotValueDetected)
			{
				base.SetValueReferenced();
			}
			if (expressionInfo.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression && this.Detected(text9, this.m_regexes.ScopesDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsScopeReferenceInComplexExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			return true;
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00154D08 File Offset: 0x00152F08
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo CreateScopedFirstAggregate(string fieldName, string scopeName)
		{
			string text = this.CreateAggregateID();
			VBExpressionParser.ParserState state = this.m_state;
			int numberOfAggregates = state.NumberOfAggregates;
			state.NumberOfAggregates = numberOfAggregates + 1;
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.SetAsSimpleFieldReference(fieldName);
			expressionInfo.OriginalText = fieldName;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo();
			dataAggregateInfo.AggregateType = Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.First;
			dataAggregateInfo.Name = text;
			dataAggregateInfo.Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[] { expressionInfo };
			dataAggregateInfo.SetScope(scopeName);
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo2 = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo2.AddAggregate(dataAggregateInfo);
			expressionInfo2.HasAnyFieldReferences = true;
			expressionInfo2.Type = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate;
			expressionInfo2.StringValue = text;
			return expressionInfo2;
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x00154D91 File Offset: 0x00152F91
		private bool AreEqualOrdinalIgnoreCase(string str1, string str2)
		{
			return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x00154D9C File Offset: 0x00152F9C
		private void EnforceRestrictions(ref string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyRenderFormatAll) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.RenderFormatAnyDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidRenderFormatUsage, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.InScope) && this.Detected(expression, this.m_regexes.InScope))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { "InScope" });
			}
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Level) && this.Detected(expression, this.m_regexes.Level))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { "Level" });
			}
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.CreateDrillthroughContext) && this.Detected(expression, this.m_regexes.CreateDrillthroughContext))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { "CreateDrillthroughContext" });
			}
			bool flag = this.Detected(expression, this.m_regexes.ScopesDetection);
			if (flag && this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.ScopesCollection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { "Scopes" });
			}
			if (flag && (grammarFlags & ExpressionParser.GrammarFlags.DenyScopes) != (ExpressionParser.GrammarFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidScopeCollectionReference, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyFields) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.FieldDetection))
			{
				if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType, "(ExpressionType.ReportParameter == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyVariables) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.VariablesDetection))
			{
				if (this.m_context.InPrevious)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInPreviousAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofVariable, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (this.m_context.InLookup)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsLookupOfVariable, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataRegionSortExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInDataRowSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInDataRegionOrDataSetFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.JoinExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInJoinExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.FieldValue == this.m_context.ExpressionType, "(ExpressionType.FieldValue == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsVariableInCalculatedFieldExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			int num = this.NumberOfTimesDetected(expression, this.m_regexes.ReportItemsDetection);
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyReportItems) != (ExpressionParser.GrammarFlags)0 && 0 < num)
			{
				if (isParameter)
				{
					Global.Tracer.Assert((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 == (m_context.Location & LocationFlags.InPageSection))");
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateReportItemInBody, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.VariableValue == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupVariableValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInVariableExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.UserSortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionSortExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.JoinExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInJoinExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (this.m_context.InLookup)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInLookupDestinationOrResult, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(false, "Unknown ExpressionType: {0} denying ReportItems.", new object[] { this.m_context.ExpressionType });
				}
			}
			if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && 1 < num)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsMultiReportItemsInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if (0 < num)
			{
				if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_state.PageSectionRefersToReportItems = true;
				}
				else
				{
					this.m_state.BodyRefersToReportItems = true;
				}
			}
			if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && this.Detected(expression, this.m_regexes.OverallTotalPagesDetection))
			{
				this.m_state.PageSectionRefersToOverallTotalPages = true;
			}
			if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && this.Detected(expression, this.m_regexes.TotalPagesDetection))
			{
				this.m_state.PageSectionRefersToTotalPages = true;
			}
			if (this.Detected(expression, this.m_regexes.OverallPageGlobalsDetection))
			{
				expressionInfo.ReferencedOverallPageGlobals = true;
				if ((grammarFlags & ExpressionParser.GrammarFlags.DenyOverallPageGlobals) != (ExpressionParser.GrammarFlags)0)
				{
					Global.Tracer.Assert((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 == (m_context.Location & LocationFlags.InPageSection))");
					this.m_errorContext.Register(ProcessingErrorCode.rsOverallPageNumberInBody, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if (this.Detected(expression, this.m_regexes.PageGlobalsDetection))
			{
				expressionInfo.ReferencedPageGlobals = true;
				if ((grammarFlags & ExpressionParser.GrammarFlags.DenyPageGlobals) != (ExpressionParser.GrammarFlags)0)
				{
					Global.Tracer.Assert((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 == (m_context.Location & LocationFlags.InPageSection))");
					this.m_errorContext.Register(ProcessingErrorCode.rsPageNumberInBody, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if (this.Detected(expression, this.m_regexes.AggregatesDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsGlobalNotDefined, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyDataSets) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.DataSetsDetection))
			{
				if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType, "(ExpressionType.ReportParameter == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyDataSources) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.DataSourcesDetection))
			{
				if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType, "(ExpressionType.ReportParameter == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyMeDotValue) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.MeDotValueDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidMeDotValueInExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			this.RemoveLineTerminators(ref expression, this.m_regexes.LineTerminatorDetection);
			if (this.Detected(expression, this.m_regexes.IllegalCharacterDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidCharacterInExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x00155DFB File Offset: 0x00153FFB
		private void GetMeDotValueReferences(string strTransformedExpression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			this.GetMeDotValueReferences(strTransformedExpression, expressionInfo, true);
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x00155E06 File Offset: 0x00154006
		private void GetMeDotValueReferences(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			this.GetMeDotValueReferences(expressionInfo.OriginalText, expressionInfo, false);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x00155E18 File Offset: 0x00154018
		private void GetMeDotValueReferences(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, bool inTransformedExpression)
		{
			MatchCollection matchCollection = this.m_regexes.MeDotValueExpression.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Group group = matchCollection[i].Groups["medotvalue"];
				if (group.Value != null && group.Value.Length > 0)
				{
					if (inTransformedExpression)
					{
						expressionInfo.AddMeDotValueInTransformedExpression(group.Index);
					}
					else
					{
						expressionInfo.AddMeDotValueInOriginalText(group.Index);
					}
				}
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x00155E92 File Offset: 0x00154092
		private void GetReferencedReportItemNames(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			this.GetReferencedReportItemNames(expressionInfo.OriginalText, expressionInfo, false);
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00155EA2 File Offset: 0x001540A2
		private void GetReferencedReportItemNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			this.GetReferencedReportItemNames(expression, expressionInfo, true);
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00155EB0 File Offset: 0x001540B0
		private void GetReferencedReportItemNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, bool inTransformedExpression)
		{
			MatchCollection matchCollection = this.m_regexes.ReportItemName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Group group = matchCollection[i].Groups["reportitemname"];
				if (group.Value != null && group.Value.Length > 0)
				{
					if (inTransformedExpression)
					{
						expressionInfo.AddReferencedReportItemInTransformedExpression(group.Value, group.Index);
					}
					else
					{
						expressionInfo.AddReferencedReportItemInOriginalText(group.Value, group.Index);
					}
				}
			}
			matchCollection = this.m_regexes.SimpleDynamicReportItemReference.Matches(expression);
			for (int j = 0; j < matchCollection.Count; j++)
			{
				Group group2 = matchCollection[j].Groups["reportitemname"];
				if (group2.Value != null)
				{
					if (inTransformedExpression)
					{
						expressionInfo.AddReferencedReportItemInTransformedExpression(group2.Value, group2.Index);
					}
					else
					{
						expressionInfo.AddReferencedReportItemInOriginalText(group2.Value, group2.Index);
					}
				}
			}
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00155FA8 File Offset: 0x001541A8
		private void GetReferencedVariableNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			MatchCollection matchCollection = this.m_regexes.VariableName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				Group group = matchCollection[i].Groups["variablename"];
				if (group.Value != null && group.Value.Length > 0)
				{
					expressionInfo.AddReferencedVariable(group.Value, group.Index);
				}
			}
			matchCollection = this.m_regexes.SimpleDynamicVariableReference.Matches(expression);
			for (int j = 0; j < matchCollection.Count; j++)
			{
				Group group2 = matchCollection[j].Groups["variablename"];
				if (group2.Value != null)
				{
					expressionInfo.AddReferencedVariable(group2.Value, group2.Index);
				}
			}
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x00156070 File Offset: 0x00154270
		private void GetReferencedFieldNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if (this.Detected(expression, this.m_regexes.FieldDetection))
			{
				expressionInfo.HasAnyFieldReferences = true;
			}
			if (this.Detected(expression, this.m_regexes.DynamicFieldReference))
			{
				expressionInfo.DynamicFieldReferences = true;
			}
			else
			{
				MatchCollection matchCollection = this.m_regexes.DynamicFieldPropertyReference.Matches(expression);
				for (int i = 0; i < matchCollection.Count; i++)
				{
					string text = matchCollection[i].Result("${fieldname}");
					if (text != null && text.Length != 0)
					{
						expressionInfo.AddDynamicPropertyReference(text);
					}
				}
				matchCollection = this.m_regexes.StaticFieldPropertyReference.Matches(expression);
				for (int j = 0; j < matchCollection.Count; j++)
				{
					string text2 = matchCollection[j].Result("${fieldname}");
					string text3 = matchCollection[j].Result("${propertyname}");
					if (text2 != null && text2.Length != 0 && text3 != null && text3.Length != 0)
					{
						expressionInfo.AddStaticPropertyReference(text2, text3);
					}
				}
			}
			MatchCollection matchCollection2 = this.m_regexes.FieldName.Matches(expression);
			for (int k = 0; k < matchCollection2.Count; k++)
			{
				string text4 = matchCollection2[k].Result("${fieldname}");
				if (text4 != null && text4.Length != 0)
				{
					expressionInfo.AddReferencedField(text4);
				}
			}
			matchCollection2 = this.m_regexes.SimpleDynamicFieldReference.Matches(expression);
			for (int l = 0; l < matchCollection2.Count; l++)
			{
				Group group = matchCollection2[l].Groups["fieldname"];
				if (group.Value != null && group.Value.Length > 0)
				{
					expressionInfo.AddReferencedField(group.Value);
				}
			}
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x00156228 File Offset: 0x00154428
		private void GetReferencedScopesAndScopedFields(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			MatchCollection matchCollection = this.m_regexes.ScopeName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				this.HandleMatchedScopeReference(expression, expressionInfo, matchCollection[i]);
			}
			matchCollection = this.m_regexes.SimpleDynamicScopeReference.Matches(expression);
			for (int j = 0; j < matchCollection.Count; j++)
			{
				this.HandleMatchedScopeReference(expression, expressionInfo, matchCollection[j]);
			}
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0015629C File Offset: 0x0015449C
		private void HandleMatchedScopeReference(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, Match match)
		{
			Group group = match.Groups["scopename"];
			string value = group.Value;
			if (group.Success && !string.IsNullOrEmpty(value))
			{
				string text = null;
				if (match.Groups["hasfields"].Success)
				{
					int num = match.Index + match.Length;
					Match match2 = this.m_regexes.DictionaryOpWithIdentifier.Match(expression, num);
					if (!match2.Success)
					{
						match2 = this.m_regexes.IndexerWithIdentifier.Match(expression, num);
					}
					if (match2.Success)
					{
						Group group2 = match2.Groups["fieldname"];
						if (group2.Success)
						{
							text = group2.Value;
						}
					}
				}
				ScopeReference scopeReference;
				if (!string.IsNullOrEmpty(text))
				{
					scopeReference = new ScopeReference(value, text);
				}
				else
				{
					scopeReference = new ScopeReference(value);
				}
				expressionInfo.AddReferencedScope(scopeReference);
			}
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x0015637C File Offset: 0x0015457C
		private void GetReferencedParameterNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			if (this.Detected(expression, this.m_regexes.DynamicParameterReference))
			{
				expressionInfo.HasDynamicParameterReference = true;
			}
			MatchCollection matchCollection = this.m_regexes.ParameterName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = matchCollection[i].Result("${parametername}");
				if (text != null && text.Length != 0)
				{
					expressionInfo.AddReferencedParameter(text);
				}
			}
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x001563EC File Offset: 0x001545EC
		private bool HasRenderFormatNonIsInteractiveReference(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, out string referencedRenderFormatProperty)
		{
			referencedRenderFormatProperty = null;
			MatchCollection matchCollection = this.m_regexes.RenderFormatPropertyName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				referencedRenderFormatProperty = matchCollection[i].Result("${propertyname}");
				if (!string.IsNullOrEmpty(referencedRenderFormatProperty) && ReportProcessing.CompareWithInvariantCulture("IsInteractive", referencedRenderFormatProperty, true) != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00156450 File Offset: 0x00154650
		private void GetReferencedDataSetNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			MatchCollection matchCollection = this.m_regexes.DataSetName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = matchCollection[i].Result("${datasetname}");
				if (text != null && text.Length != 0)
				{
					expressionInfo.AddReferencedDataSet(text);
				}
			}
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x001564A4 File Offset: 0x001546A4
		private void GetReferencedDataSourceNames(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo)
		{
			MatchCollection matchCollection = this.m_regexes.DataSourceName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = matchCollection[i].Result("${datasourcename}");
				if (text != null && text.Length != 0)
				{
					expressionInfo.AddReferencedDataSource(text);
				}
			}
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x001564F8 File Offset: 0x001546F8
		private bool Detected(string expression, Regex detectionRegex)
		{
			return this.NumberOfTimesDetected(expression, detectionRegex) != 0;
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x00156508 File Offset: 0x00154708
		private int NumberOfTimesDetected(string expression, Regex detectionRegex)
		{
			int num = 0;
			MatchCollection matchCollection = detectionRegex.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = matchCollection[i].Result("${detected}");
				if (text != null && text.Length != 0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x00156554 File Offset: 0x00154754
		private void RemoveLineTerminators(ref string expression, Regex detectionRegex)
		{
			if (expression == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(expression, expression.Length);
			MatchCollection matchCollection = detectionRegex.Matches(expression);
			for (int i = matchCollection.Count - 1; i >= 0; i--)
			{
				string text = matchCollection[i].Result("${detected}");
				if (text != null && text.Length != 0)
				{
					stringBuilder.Remove(matchCollection[i].Index, matchCollection[i].Length);
				}
			}
			if (matchCollection.Count != 0)
			{
				expression = stringBuilder.ToString();
			}
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x001565E0 File Offset: 0x001547E0
		private void GetRunningValue(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValue)
		{
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.RunningValue))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyRunningValue) != (ExpressionParser.GrammarFlags)0)
			{
				if (this.m_context.InPrevious)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInPreviousAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInAggregateExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.VariableValue == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupVariableValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInVariableExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.FieldValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInCalculatedFieldExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.JoinExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInJoinExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.UserSortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionSortExpression == this.m_context.ExpressionType, "(SortExpression == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (3 != list.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			runningValue = new Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo();
			if (2 <= list.Count)
			{
				bool flag;
				try
				{
					runningValue.AggregateType = (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes)Enum.Parse(typeof(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes), list[1], true);
					flag = Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate != runningValue.AggregateType && Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Previous != runningValue.AggregateType && Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.CountRows != runningValue.AggregateType;
				}
				catch (ArgumentException)
				{
					flag = false;
				}
				if (!flag)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidRunningValueAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if (1 <= list.Count)
			{
				if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Count == runningValue.AggregateType && "*" == list[0].Trim())
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsCountStarRVNotSupported, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					runningValue.Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[1];
					runningValue.Expressions[0] = this.GetParameterExpression(runningValue, list[0], grammarFlags);
				}
			}
			if (3 <= list.Count)
			{
				runningValue.Scope = this.GetAggregateScope(list[2], true);
			}
			this.DetectAggregateFieldReferences(runningValue);
			VBExpressionParser.ParserState state = this.m_state;
			int numberOfRunningValues = state.NumberOfRunningValues;
			state.NumberOfRunningValues = numberOfRunningValues + 1;
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x00156BF8 File Offset: 0x00154DF8
		private void GetPreviousAggregate(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValue)
		{
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Previous))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyPrevious) != (ExpressionParser.GrammarFlags)0)
			{
				if (this.m_context.InPrevious)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousInPreviousAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (this.m_context.InLookup)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousInLookupDestinationOrResult, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousInAggregateExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.FieldValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInCalculatedFieldExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.VariableValue == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupVariableValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInVariableExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.JoinExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInJoinExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.UserSortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionSortExpression == this.m_context.ExpressionType, "(SortExpression == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			this.m_state.PreviousAggregateUsed = true;
			this.m_context.InPrevious = true;
			VBExpressionParser.ParserState state = this.m_state;
			int numberOfRunningValues = state.NumberOfRunningValues;
			state.NumberOfRunningValues = numberOfRunningValues + 1;
			runningValue = new Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo();
			runningValue.AggregateType = Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Previous;
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (list.Count == 1 || list.Count == 2)
			{
				runningValue.Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[1];
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo parameterExpression = this.GetParameterExpression(runningValue, list[0], grammarFlags);
				parameterExpression.InPrevious = true;
				runningValue.Expressions[0] = parameterExpression;
				if (this.HasInScopeOrLevel(list[0]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInScopeOrLevelInPreviousAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				if (list.Count == 2)
				{
					runningValue.Scope = this.GetAggregateScope(list[1], false);
				}
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			this.m_context.InPrevious = false;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x001571CC File Offset: 0x001553CC
		private bool HasInScopeOrLevel(string expression)
		{
			return this.m_regexes.InScopeOrLevel.Match(expression).Success;
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x001571E4 File Offset: 0x001553E4
		private void GetRowNumber(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo rowNumber)
		{
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.RowNumber))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyRowNumber) != (ExpressionParser.GrammarFlags)0)
			{
				if (this.m_context.InPrevious)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInPreviousAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				if (this.m_context.InLookup)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInLookupDestinationOrResult, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.VariableValue == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupVariableValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInVariableExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.FieldValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInCalculatedFieldExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.UserSortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionSortExpression == this.m_context.ExpressionType, "(SortExpression == m_context.ExpressionType)");
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (1 != list.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			rowNumber = new Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo();
			rowNumber.AggregateType = Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.CountRows;
			rowNumber.Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[0];
			if (1 <= list.Count)
			{
				rowNumber.Scope = this.GetAggregateScope(list[0], true);
			}
			VBExpressionParser.ParserState state = this.m_state;
			int numberOfRunningValues = state.NumberOfRunningValues;
			state.NumberOfRunningValues = numberOfRunningValues + 1;
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00157675 File Offset: 0x00155875
		private string GetAggregateScope(string expression, bool allowNothing)
		{
			return this.GetScope(expression, allowNothing, ProcessingErrorCode.rsInvalidAggregateScope);
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x00157681 File Offset: 0x00155881
		private string GetLookupScope(string expression)
		{
			return this.GetScope(expression, false, ProcessingErrorCode.rsInvalidLookupScope);
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x00157690 File Offset: 0x00155890
		private string GetScope(string expression, bool allowNothing, ProcessingErrorCode errorCode)
		{
			if (this.m_regexes.NothingOnly.Match(expression).Success)
			{
				if (allowNothing)
				{
					return null;
				}
			}
			else
			{
				Match match = this.m_regexes.StringLiteralOnly.Match(expression);
				if (match.Success)
				{
					return match.Result("${string}");
				}
			}
			this.m_errorContext.Register(errorCode, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			return null;
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00157718 File Offset: 0x00155918
		private bool IsRecursive(string flag)
		{
			ExpressionParser.RecursiveFlags recursiveFlags = ExpressionParser.RecursiveFlags.Simple;
			try
			{
				recursiveFlags = (ExpressionParser.RecursiveFlags)Enum.Parse(typeof(ExpressionParser.RecursiveFlags), flag, true);
			}
			catch (Exception)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateRecursiveFlag, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			return ExpressionParser.RecursiveFlags.Recursive == recursiveFlags;
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00157794 File Offset: 0x00155994
		private LookupInfo GetLookup(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, LookupType lookupType, out int newPos, out string lookupID)
		{
			lookupID = this.CreateLookupID();
			if (this.m_context.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.Lookup))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidFeatureRdlExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyLookups) != (ExpressionParser.GrammarFlags)0)
			{
				if (this.m_context.InLookup)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsNestedLookups, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsLookupInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.FieldValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInCalculatedFieldExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(false, "Unknown ExpressionType for Lookup restriction: {0}", new object[] { this.m_context.ExpressionType });
				}
			}
			this.m_context.InLookup = true;
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (list.Count != 4)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			LookupInfo lookupInfo = new LookupInfo();
			lookupInfo.LookupType = lookupType;
			lookupInfo.Name = lookupID;
			lookupInfo.DestinationInfo = new LookupDestinationInfo();
			lookupInfo.DestinationInfo.IsMultiValue = lookupType == LookupType.LookupSet;
			if (list.Count > 3)
			{
				lookupInfo.DestinationInfo.Scope = this.GetLookupScope(list[3]);
			}
			if (list.Count > 2)
			{
				lookupInfo.ResultExpr = this.GetLookupParameterExpr(list[2], ExpressionParser.GrammarFlags.DenyAggregates | ExpressionParser.GrammarFlags.DenyRunningValue | ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyReportItems | ExpressionParser.GrammarFlags.DenyOverallPageGlobals | ExpressionParser.GrammarFlags.DenyPrevious | ExpressionParser.GrammarFlags.DenyVariables | ExpressionParser.GrammarFlags.DenyLookups | ExpressionParser.GrammarFlags.DenyPageGlobals | ExpressionParser.GrammarFlags.DenyRenderFormatAll | ExpressionParser.GrammarFlags.DenyScopes, isParameter);
			}
			if (list.Count > 1)
			{
				lookupInfo.DestinationInfo.DestinationExpr = this.GetLookupParameterExpr(list[1], ExpressionParser.GrammarFlags.DenyAggregates | ExpressionParser.GrammarFlags.DenyRunningValue | ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyReportItems | ExpressionParser.GrammarFlags.DenyOverallPageGlobals | ExpressionParser.GrammarFlags.DenyPrevious | ExpressionParser.GrammarFlags.DenyVariables | ExpressionParser.GrammarFlags.DenyLookups | ExpressionParser.GrammarFlags.DenyPageGlobals | ExpressionParser.GrammarFlags.DenyRenderFormatAll | ExpressionParser.GrammarFlags.DenyScopes, isParameter);
			}
			if (list.Count > 0)
			{
				lookupInfo.SourceExpr = this.GetLookupParameterExpr(list[0], grammarFlags | (ExpressionParser.GrammarFlags.DenyVariables | ExpressionParser.GrammarFlags.DenyLookups | ExpressionParser.GrammarFlags.DenyRenderFormatAll), isParameter);
			}
			VBExpressionParser.ParserState state = this.m_state;
			int numberOfLookups = state.NumberOfLookups;
			state.NumberOfLookups = numberOfLookups + 1;
			return lookupInfo;
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x00157B24 File Offset: 0x00155D24
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo GetLookupParameterExpr(string parameterExpression, ExpressionParser.GrammarFlags grammarFlags, bool isParameter)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.OriginalText = parameterExpression;
			if (this.m_context.InAggregate)
			{
				grammarFlags |= ExpressionParser.GrammarFlags.DenyAggregates;
			}
			this.VBLex(parameterExpression, isParameter, grammarFlags, expressionInfo);
			return expressionInfo;
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00157B5C File Offset: 0x00155D5C
		private bool ParseRdlFunction(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, int currentPos, string functionName, string expression, ExpressionParser.GrammarFlags grammarFlags, out int newPos)
		{
			newPos = currentPos;
			RdlFunctionInfo rdlFunctionInfo = new RdlFunctionInfo();
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (list.Count < 2)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> list2 = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo>(list.Count);
			foreach (string text in list)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo2 = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
				list2.Add(expressionInfo2);
				expressionInfo2.OriginalText = text;
				this.VBLex(text, false, grammarFlags, expressionInfo2);
				if (expressionInfo2.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
				{
					return true;
				}
			}
			expressionInfo.SetAsRdlFunction(rdlFunctionInfo);
			rdlFunctionInfo.SetFunctionType(functionName);
			rdlFunctionInfo.Expressions = list2;
			return false;
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00157C5C File Offset: 0x00155E5C
		private void GetAggregate(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo aggregate)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyAggregates) != (ExpressionParser.GrammarFlags)0)
			{
				if (isParameter)
				{
					if (this.m_context.InLookup)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsNestedAggregateViaLookup, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsNestedAggregateInPageSection, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataRegionSortExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInDataRowSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.QueryParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInQueryParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { this.m_context.DataSetName });
				}
				else if (ExpressionParser.ExpressionType.ReportLanguage == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInReportLanguageExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.FieldValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInCalculatedFieldExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.JoinExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInJoinExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (this.m_context.InLookup)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInLookupDestinationOrResult, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					Global.Tracer.Assert(this.m_context.InPrevious, "(m_context.InPrevious)");
				}
			}
			bool inPrevious = this.m_context.InPrevious;
			this.m_context.InPrevious = false;
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (list.Count > 3)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			aggregate = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo();
			aggregate.AggregateType = (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes)Enum.Parse(typeof(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes), functionName, true);
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyPostSortAggregate) != (ExpressionParser.GrammarFlags)0 && aggregate.IsPostSortAggregate())
			{
				if (this.m_context.InAggregate)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPostSortAggregateInAggregateExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPostSortAggregateInGroupFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.UserSortExpression == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPostSortAggregateInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.VariableValue == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupVariableValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPostSortAggregateInVariableExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.CountRows == aggregate.AggregateType)
			{
				aggregate.AggregateType = Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.CountRows;
				aggregate.Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[0];
				if (1 <= list.Count)
				{
					aggregate.SetScope(this.GetAggregateScope(list[0], false));
				}
				if (2 <= list.Count)
				{
					if (this.m_context.InAggregate)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidNestedRecursiveAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else
					{
						aggregate.Recursive = this.IsRecursive(list[1]);
					}
				}
				if (3 <= list.Count)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
				}
			}
			else
			{
				if (list.Count == 0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
				}
				else if (1 <= list.Count)
				{
					if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Count == aggregate.AggregateType && "*" == list[0].Trim())
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsCountStarNotSupported, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else
					{
						aggregate.Expressions = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo[1];
						aggregate.Expressions[0] = this.GetParameterExpression(aggregate, list[0], grammarFlags);
						if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate == aggregate.AggregateType)
						{
							if (Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field != aggregate.Expressions[0].Type)
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsInvalidCustomAggregateExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
							}
							if (aggregate.AggregateType == Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate && inPrevious)
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInPreviousAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
							}
						}
					}
				}
				if (2 <= list.Count)
				{
					aggregate.SetScope(this.GetAggregateScope(list[1], false));
				}
				if (3 <= list.Count)
				{
					if (aggregate.IsPostSortAggregate() || Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate == aggregate.AggregateType || inPrevious)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidRecursiveAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else if (this.m_context.InAggregate)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidNestedRecursiveAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else if (aggregate.IsAggregateOfAggregate)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsRecursiveAggregateOfAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else
					{
						aggregate.Recursive = this.IsRecursive(list[2]);
					}
				}
			}
			if (this.m_context.OuterAggregate != null)
			{
				this.m_context.OuterAggregate.AddNestedAggregate(aggregate);
			}
			this.m_state.AggregateOfAggregatesUsed |= aggregate.IsAggregateOfAggregate;
			if (aggregate.IsAggregateOfAggregate && this.m_context.ExpressionType == ExpressionParser.ExpressionType.UserSortExpression)
			{
				this.m_state.AggregateOfAggregatesUsedInUserSort = true;
			}
			this.DetectAggregateFieldReferences(aggregate);
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyAggregatesOfAggregates) != (ExpressionParser.GrammarFlags)0 && aggregate.IsAggregateOfAggregate)
			{
				if (ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsNestedAggregateInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					return;
				}
				if (ExpressionParser.ExpressionType.GroupVariableValue == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsNestedAggregateInGroupVariable, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x001586A8 File Offset: 0x001568A8
		private void DetectAggregateFieldReferences(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo aggregate)
		{
			if (aggregate.Expressions != null && aggregate.Expressions.Length != 0)
			{
				for (int i = 0; i < aggregate.Expressions.Length; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = aggregate.Expressions[i];
					if (expressionInfo.HasAnyFieldReferences)
					{
						aggregate.PublishingInfo.HasAnyFieldReferences = true;
						return;
					}
					if (expressionInfo.Lookups != null)
					{
						using (List<LookupInfo>.Enumerator enumerator = expressionInfo.Lookups.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.SourceExpr.HasAnyFieldReferences)
								{
									aggregate.PublishingInfo.HasAnyFieldReferences = true;
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x00158760 File Offset: 0x00156960
		private Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo GetParameterExpression(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo outerAggregate, string parameterExpression, ExpressionParser.GrammarFlags grammarFlags)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo();
			expressionInfo.OriginalText = parameterExpression;
			if ((this.m_context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				grammarFlags |= ExpressionParser.GrammarFlags.DenyAggregates | ExpressionParser.GrammarFlags.DenyRunningValue | ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyPostSortAggregate | ExpressionParser.GrammarFlags.DenyPrevious | ExpressionParser.GrammarFlags.DenyVariables | ExpressionParser.GrammarFlags.DenyScopes;
			}
			else if (!this.m_context.InPrevious)
			{
				grammarFlags |= ExpressionParser.GrammarFlags.DenyRunningValue | ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyReportItems | ExpressionParser.GrammarFlags.DenyPostSortAggregate | ExpressionParser.GrammarFlags.DenyPrevious | ExpressionParser.GrammarFlags.DenyVariables | ExpressionParser.GrammarFlags.DenyScopes;
			}
			else
			{
				grammarFlags |= ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyReportItems | ExpressionParser.GrammarFlags.DenyPrevious | ExpressionParser.GrammarFlags.DenyVariables | ExpressionParser.GrammarFlags.DenyScopes;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo outerAggregate2 = this.m_context.OuterAggregate;
			this.m_context.OuterAggregate = outerAggregate;
			this.VBLex(parameterExpression, true, grammarFlags, expressionInfo);
			this.m_context.OuterAggregate = outerAggregate2;
			return expressionInfo;
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x001587E8 File Offset: 0x001569E8
		private void GetArguments(int currentPos, string expression, out int newPos, out List<string> arguments)
		{
			int num = 1;
			int num2 = 0;
			arguments = new List<string>();
			string text = string.Empty;
			while (0 < num && currentPos < expression.Length)
			{
				Match match = this.m_regexes.Arguments.Match(expression, currentPos);
				if (!match.Success)
				{
					text += expression.Substring(currentPos);
					currentPos = expression.Length;
				}
				else
				{
					string text2 = match.Result("${openParen}");
					string text3 = match.Result("${closeParen}");
					string text4 = match.Result("${comma}");
					string text5 = match.Result("${openCurly}");
					string text6 = match.Result("${closeCurly}");
					if (text2 != null && text2.Length != 0)
					{
						num++;
						text += expression.Substring(currentPos, match.Index - currentPos + match.Length);
					}
					else if (text3 != null && text3.Length != 0)
					{
						num--;
						if (num == 0)
						{
							text += expression.Substring(currentPos, match.Index - currentPos);
							if (text.Trim().Length != 0)
							{
								arguments.Add(text);
								text = string.Empty;
							}
						}
						else
						{
							text += expression.Substring(currentPos, match.Index - currentPos + match.Length);
						}
					}
					else if (text5 != null && text5.Length != 0)
					{
						num2++;
						text += expression.Substring(currentPos, match.Index - currentPos + match.Length);
					}
					else if (text6 != null && text6.Length != 0)
					{
						num2--;
						text += expression.Substring(currentPos, match.Index - currentPos + match.Length);
					}
					else if (text4 != null && text4.Length != 0)
					{
						if (1 == num && num2 == 0)
						{
							text += expression.Substring(currentPos, match.Index - currentPos);
							arguments.Add(text);
							text = string.Empty;
						}
						else
						{
							text += expression.Substring(currentPos, match.Index - currentPos + match.Length);
						}
					}
					else
					{
						text += expression.Substring(currentPos, match.Index - currentPos + match.Length);
					}
					currentPos = match.Index + match.Length;
				}
			}
			if (num > 0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsExpressionMissingCloseParen, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				if (text.Trim().Length != 0)
				{
					arguments.Add(text);
					text = string.Empty;
				}
			}
			newPos = currentPos;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x00158A78 File Offset: 0x00156C78
		private string CreateAggregateID()
		{
			return "Aggregate" + this.m_state.LastID.ToString();
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x00158AA4 File Offset: 0x00156CA4
		private string CreateLookupID()
		{
			return "Lookup" + this.m_state.LastLookupID.ToString();
		}

		// Token: 0x040028D8 RID: 10456
		private const string RunningValue = "RunningValue";

		// Token: 0x040028D9 RID: 10457
		private const string RowNumber = "RowNumber";

		// Token: 0x040028DA RID: 10458
		private const string Previous = "Previous";

		// Token: 0x040028DB RID: 10459
		private const string Lookup = "Lookup";

		// Token: 0x040028DC RID: 10460
		private const string LookupSet = "LookupSet";

		// Token: 0x040028DD RID: 10461
		private const string MultiLookup = "MultiLookup";

		// Token: 0x040028DE RID: 10462
		private const string Fields = "Fields";

		// Token: 0x040028DF RID: 10463
		private const string ReportItems = "ReportItems";

		// Token: 0x040028E0 RID: 10464
		private const string Parameters = "Parameters";

		// Token: 0x040028E1 RID: 10465
		private const string Globals = "Globals";

		// Token: 0x040028E2 RID: 10466
		private const string User = "User";

		// Token: 0x040028E3 RID: 10467
		private const string Aggregates = "Aggregates";

		// Token: 0x040028E4 RID: 10468
		private const string DataSets = "DataSets";

		// Token: 0x040028E5 RID: 10469
		private const string DataSources = "DataSources";

		// Token: 0x040028E6 RID: 10470
		private const string Variables = "Variables";

		// Token: 0x040028E7 RID: 10471
		private const string MinValue = "MinValue";

		// Token: 0x040028E8 RID: 10472
		private const string MaxValue = "MaxValue";

		// Token: 0x040028E9 RID: 10473
		private const string Scopes = "Scopes";

		// Token: 0x040028EA RID: 10474
		private const string Star = "*";

		// Token: 0x040028EB RID: 10475
		private VBExpressionParser.ReportRegularExpressions m_regexes;

		// Token: 0x040028EC RID: 10476
		private ExpressionParser.ExpressionContext m_context;

		// Token: 0x040028ED RID: 10477
		private VBExpressionParser.ParserState m_state = new VBExpressionParser.ParserState();

		// Token: 0x040028EE RID: 10478
		private ExpressionUsage m_expressionUsage;

		// Token: 0x040028EF RID: 10479
		private ExpressionExtractor m_expressionExtractor;

		// Token: 0x040028F0 RID: 10480
		private SafeExpressionsValidator m_safeExpressionsValidator;

		// Token: 0x02000BFF RID: 3071
		private sealed class ParserState
		{
			// Token: 0x170029C4 RID: 10692
			// (get) Token: 0x06008611 RID: 34321 RVA: 0x00218338 File Offset: 0x00216538
			// (set) Token: 0x06008612 RID: 34322 RVA: 0x00218340 File Offset: 0x00216540
			internal bool BodyRefersToReportItems { get; set; }

			// Token: 0x170029C5 RID: 10693
			// (get) Token: 0x06008613 RID: 34323 RVA: 0x00218349 File Offset: 0x00216549
			// (set) Token: 0x06008614 RID: 34324 RVA: 0x00218351 File Offset: 0x00216551
			internal bool PageSectionRefersToReportItems { get; set; }

			// Token: 0x170029C6 RID: 10694
			// (get) Token: 0x06008615 RID: 34325 RVA: 0x0021835A File Offset: 0x0021655A
			// (set) Token: 0x06008616 RID: 34326 RVA: 0x00218362 File Offset: 0x00216562
			internal bool PageSectionRefersToOverallTotalPages { get; set; }

			// Token: 0x170029C7 RID: 10695
			// (get) Token: 0x06008617 RID: 34327 RVA: 0x0021836B File Offset: 0x0021656B
			// (set) Token: 0x06008618 RID: 34328 RVA: 0x00218373 File Offset: 0x00216573
			internal bool PageSectionRefersToTotalPages { get; set; }

			// Token: 0x170029C8 RID: 10696
			// (get) Token: 0x06008619 RID: 34329 RVA: 0x0021837C File Offset: 0x0021657C
			// (set) Token: 0x0600861A RID: 34330 RVA: 0x00218384 File Offset: 0x00216584
			internal int NumberOfAggregates { get; set; }

			// Token: 0x170029C9 RID: 10697
			// (get) Token: 0x0600861B RID: 34331 RVA: 0x0021838D File Offset: 0x0021658D
			// (set) Token: 0x0600861C RID: 34332 RVA: 0x00218395 File Offset: 0x00216595
			internal int NumberOfRunningValues { get; set; }

			// Token: 0x170029CA RID: 10698
			// (get) Token: 0x0600861D RID: 34333 RVA: 0x0021839E File Offset: 0x0021659E
			// (set) Token: 0x0600861E RID: 34334 RVA: 0x002183A6 File Offset: 0x002165A6
			internal int NumberOfLookups { get; set; }

			// Token: 0x170029CB RID: 10699
			// (get) Token: 0x0600861F RID: 34335 RVA: 0x002183AF File Offset: 0x002165AF
			internal int LastID
			{
				get
				{
					return this.NumberOfAggregates + this.NumberOfRunningValues;
				}
			}

			// Token: 0x170029CC RID: 10700
			// (get) Token: 0x06008620 RID: 34336 RVA: 0x002183BE File Offset: 0x002165BE
			internal int LastLookupID
			{
				get
				{
					return this.NumberOfLookups;
				}
			}

			// Token: 0x170029CD RID: 10701
			// (get) Token: 0x06008621 RID: 34337 RVA: 0x002183C6 File Offset: 0x002165C6
			// (set) Token: 0x06008622 RID: 34338 RVA: 0x002183CE File Offset: 0x002165CE
			internal bool PreviousAggregateUsed { get; set; }

			// Token: 0x170029CE RID: 10702
			// (get) Token: 0x06008623 RID: 34339 RVA: 0x002183D7 File Offset: 0x002165D7
			// (set) Token: 0x06008624 RID: 34340 RVA: 0x002183DF File Offset: 0x002165DF
			internal bool AggregateOfAggregatesUsed { get; set; }

			// Token: 0x170029CF RID: 10703
			// (get) Token: 0x06008625 RID: 34341 RVA: 0x002183E8 File Offset: 0x002165E8
			// (set) Token: 0x06008626 RID: 34342 RVA: 0x002183F0 File Offset: 0x002165F0
			internal bool AggregateOfAggregatesUsedInUserSort { get; set; }

			// Token: 0x06008627 RID: 34343 RVA: 0x002183F9 File Offset: 0x002165F9
			internal VBExpressionParser.ParserState Save()
			{
				return (VBExpressionParser.ParserState)base.MemberwiseClone();
			}
		}

		// Token: 0x02000C00 RID: 3072
		private sealed class ReportRegularExpressions
		{
			// Token: 0x06008629 RID: 34345 RVA: 0x00218410 File Offset: 0x00216610
			private ReportRegularExpressions()
			{
				RegexOptions regexOptions = RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;
				this.NonConstant = new Regex("^\\s*=", regexOptions);
				string text = Regex.Escape("-+()#,:&*/\\^<=>");
				string text2 = Regex.Escape("!");
				string text3 = Regex.Escape(".");
				string text4 = "[" + text2 + text3 + "]";
				string text5 = "[" + text + "\\s]";
				string text6 = "(^|" + text5 + ")";
				string text7 = string.Concat(new string[] { "($|[", text, text2, text3, "\\s])" });
				string text8 = "($|[" + text + text3 + "\\s])";
				string text9 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Fields");
				string text10 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Value");
				string text11 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Scopes");
				string text12 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("ReportItems");
				string text13 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Parameters");
				string text14 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Globals");
				string text15 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("RenderFormat");
				string text16 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("OverallTotalPages");
				string text17 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("TotalPages");
				string text18 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("DataSets");
				string text19 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("DataSources");
				string text20 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Variables");
				string text21 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Me");
				string text22 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Item");
				string text23 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("InScope");
				string text24 = VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Level");
				this.FieldDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text9, ")", text7 }), regexOptions);
				this.ScopesDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text11, ")", text7 }), regexOptions);
				this.ReportItemsDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text12, ")", text7 }), regexOptions);
				this.ParametersDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text13, ")", text7 }), regexOptions);
				this.RenderFormatAnyDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>(", text14, text4, text15, text7, "))" }), regexOptions);
				this.OverallPageGlobalsDetection = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|",
					text6,
					"(?<detected>(",
					text14,
					text4,
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("OverallPageNumber"),
					")|(",
					text14,
					text4,
					text16,
					"))",
					text7
				}), regexOptions);
				this.PageGlobalsDetection = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|",
					text6,
					"(?<detected>(",
					text14,
					text4,
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("PageNumber"),
					")|(",
					text14,
					text4,
					text17,
					")|(",
					text14,
					text4,
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("PageName"),
					"))",
					text7
				}), regexOptions);
				this.OverallTotalPagesDetection = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>(", text14, text4, text16, text7, ")|(", text14, text5,
					"))"
				}), regexOptions);
				this.TotalPagesDetection = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>(", text14, text4, text17, text7, ")|(", text14, text5,
					"))"
				}), regexOptions);
				this.AggregatesDetection = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|",
					text6,
					"(?<detected>",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Aggregates"),
					")",
					text7
				}), regexOptions);
				this.UserDetection = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|",
					text6,
					"(?<detected>",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("User"),
					")",
					text7
				}), regexOptions);
				this.DataSetsDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text18, ")", text7 }), regexOptions);
				this.DataSourcesDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text19, ")", text7 }), regexOptions);
				this.VariablesDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text20, ")", text7 }), regexOptions);
				this.MeDotValueDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>(", text21, text3, ")?", text10, ")", text7 }), regexOptions);
				this.MeDotValueExpression = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<medotvalue>(", text21, text3, ")?", text10, ")*", text7 }), regexOptions);
				string text25 = Regex.Escape(":");
				string text26 = Regex.Escape("#");
				string text27 = string.Concat(new string[] { "(", text26, "[^", text26, "]*", text26, ")" });
				string text28 = Regex.Escape(":=");
				this.LineTerminatorDetection = new Regex("(?<detected>(\\u000D\\u000A)|([\\u000D\\u000A\\u2028\\u2029]))", regexOptions);
				this.IllegalCharacterDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text27, "|", text28, "|(?<detected>", text25, ")" }), regexOptions);
				string text29 = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]*";
				string text30 = string.Concat(new string[] { text12, text2, "(?<reportitemname>", text29, ")" });
				string text31 = string.Concat(new string[] { text9, text2, "(?<fieldname>", text29, ")" });
				string text32 = string.Concat(new string[] { text13, text2, "(?<parametername>", text29, ")" });
				string text33 = string.Concat(new string[] { text18, text2, "(?<datasetname>", text29, ")" });
				string text34 = string.Concat(new string[] { text19, text2, "(?<datasourcename>", text29, ")" });
				string text35 = string.Concat(new string[] { text20, text2, "(?<variablename>", text29, ")" });
				string text36 = string.Concat(new string[] { text11, text2, "(?<scopename>", text29, ")" });
				string text37 = string.Concat(new string[] { text14, text4, text15, text4, "(?<propertyname>", text29, ")" });
				this.SimpleDynamicReportItemReference = new Regex(string.Concat(new string[]
				{
					text6,
					"(?<detected>(",
					text12,
					"(",
					text3,
					text22,
					")?",
					Regex.Escape("("),
					"[ \t]*",
					Regex.Escape("\""),
					"(?<reportitemname>",
					text29,
					")",
					Regex.Escape("\""),
					"[ \t]*",
					Regex.Escape(")"),
					"))"
				}), regexOptions);
				this.SimpleDynamicVariableReference = new Regex(string.Concat(new string[]
				{
					text6,
					"(?<detected>(",
					text20,
					"(",
					text3,
					text22,
					")?",
					Regex.Escape("("),
					"[ \t]*",
					Regex.Escape("\""),
					"(?<variablename>",
					text29,
					")",
					Regex.Escape("\""),
					"[ \t]*",
					Regex.Escape(")"),
					"))"
				}), regexOptions);
				this.SimpleDynamicFieldReference = new Regex(string.Concat(new string[]
				{
					text6,
					"(?<detected>(",
					text9,
					"(",
					text3,
					text22,
					")?",
					Regex.Escape("("),
					"[ \t]*",
					Regex.Escape("\""),
					"(?<fieldname>",
					text29,
					")",
					Regex.Escape("\""),
					"[ \t]*",
					Regex.Escape(")"),
					"))"
				}), regexOptions);
				this.DynamicFieldReference = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|",
					text6,
					"(?<detected>(",
					text9,
					"(",
					text3,
					text22,
					")?",
					Regex.Escape("("),
					"))"
				}), regexOptions);
				this.DynamicFieldPropertyReference = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text31 + Regex.Escape("("), regexOptions);
				this.StaticFieldPropertyReference = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, text31, text3, "(?<propertyname>", text29, ")" }), regexOptions);
				this.FieldOnly = new Regex(string.Concat(new string[] { "^\\s*", text31, text3, text10, "\\s*$" }), regexOptions);
				string text38 = "(?<hasfields>" + text3 + text9 + ")?";
				this.SimpleDynamicScopeReference = new Regex(string.Concat(new string[]
				{
					text6,
					"(?<detected>(",
					text11,
					"(",
					text3,
					text22,
					")?",
					Regex.Escape("("),
					"[ \t]*",
					Regex.Escape("\""),
					"(?<scopename>",
					text29,
					")",
					Regex.Escape("\""),
					"[ \t]*",
					Regex.Escape(")"),
					")",
					text38,
					")"
				}), regexOptions);
				this.ScopeName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text36 + text38, regexOptions);
				string text39 = "(?<fieldname>" + text29 + ")";
				this.DictionaryOpWithIdentifier = new Regex("\\G" + text2 + text39, regexOptions);
				this.IndexerWithIdentifier = new Regex(string.Concat(new string[]
				{
					"\\G(",
					text3,
					text22,
					")?",
					Regex.Escape("("),
					"[ \t]*",
					Regex.Escape("\""),
					text39,
					Regex.Escape("\""),
					"[ \t]*",
					Regex.Escape(")")
				}), regexOptions);
				this.ScopedFieldReferenceOnly = new Regex(string.Concat(new string[] { "^\\s*", text36, text3, text31, text3, text10, "\\s*$" }), regexOptions);
				this.RewrittenCommandText = new Regex(string.Concat(new string[]
				{
					"^\\s*",
					text33,
					text3,
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("RewrittenCommandText"),
					"\\s*$"
				}), regexOptions);
				this.ParameterOnly = new Regex(string.Concat(new string[] { "^\\s*", text32, text3, text10, "\\s*$" }), regexOptions);
				this.StringLiteralOnly = new Regex("^\\s*\"(?<string>((\"\")|[^\"])*)\"\\s*$", regexOptions);
				this.NothingOnly = new Regex("^\\s*" + VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Nothing") + "\\s*$", regexOptions);
				this.InScopeOrLevel = new Regex(string.Concat(new string[] { "((\"((\"\")|[^\"])*\")|('.*)|", text6, ")*(", text23, "|", text24, ")\\s*\\(" }), regexOptions);
				string text40 = "(\"((\"\")|[^\"])*\")|('.*)|" + text6 + "(?<detected>";
				string text41 = ")\\s*\\(";
				this.InScope = new Regex(text40 + text23 + text41, regexOptions);
				this.Level = new Regex(text40 + text24 + text41, regexOptions);
				this.CreateDrillthroughContext = new Regex(text40 + VBExpressionParser.ReportRegularExpressions.CaseInsensitive("CreateDrillthroughContext") + text41, regexOptions);
				this.ReportItemName = new Regex(text6 + text30, regexOptions);
				this.FieldName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text31, regexOptions);
				this.ParameterName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text32, regexOptions);
				this.DynamicParameterReference = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|('.*)|", text6, "(?<detected>", text13, text8, ")" }), regexOptions);
				this.DataSetName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text33, regexOptions);
				this.DataSourceName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text34, regexOptions);
				this.VariableName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text35, regexOptions);
				this.RenderFormatPropertyName = new Regex("(\"((\"\")|[^\"])*\")|('.*)|" + text6 + text37, regexOptions);
				this.SpecialFunction = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|(?<prefix>",
					text6,
					")(?<sfname>",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("RunningValue"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("RowNumber"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Lookup"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("LookupSet"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("MultiLookup"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("First"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Last"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Previous"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Sum"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Avg"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Max"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Min"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("CountDistinct"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Count"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("CountRows"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("StDevP"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("VarP"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("StDev"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Var"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Aggregate"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("Union"),
					")\\s*\\("
				}), regexOptions);
				this.RdlFunction = new Regex(string.Concat(new string[]
				{
					"^\\s*(?<functionName>",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("MinValue"),
					"|",
					VBExpressionParser.ReportRegularExpressions.CaseInsensitive("MaxValue"),
					")\\s*\\("
				}), regexOptions);
				string text42 = Regex.Escape("(");
				string text43 = Regex.Escape(")");
				string text44 = Regex.Escape(",");
				string text45 = Regex.Escape("{");
				string text46 = Regex.Escape("}");
				this.Arguments = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|('.*)|(?<openParen>", text42, ")|(?<closeParen>", text43, ")|(?<openCurly>", text45, ")|(?<closeCurly>", text46, ")|(?<comma>", text44,
					")"
				}), regexOptions);
				this.HasLevelWithNoScope = new Regex(string.Concat(new string[] { text24, "\\s*", text42, "\\s*", text43 }));
			}

			// Token: 0x0600862A RID: 34346 RVA: 0x002196C4 File Offset: 0x002178C4
			private static string CaseInsensitive(string input)
			{
				StringBuilder stringBuilder = new StringBuilder(input.Length * 4);
				foreach (char c in input)
				{
					stringBuilder.Append("[");
					stringBuilder.Append(char.ToUpperInvariant(c));
					stringBuilder.Append(char.ToLowerInvariant(c));
					stringBuilder.Append("]");
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040047B3 RID: 18355
			internal Regex NonConstant;

			// Token: 0x040047B4 RID: 18356
			internal Regex FieldDetection;

			// Token: 0x040047B5 RID: 18357
			internal Regex ReportItemsDetection;

			// Token: 0x040047B6 RID: 18358
			internal Regex ParametersDetection;

			// Token: 0x040047B7 RID: 18359
			internal Regex RenderFormatAnyDetection;

			// Token: 0x040047B8 RID: 18360
			internal Regex OverallPageGlobalsDetection;

			// Token: 0x040047B9 RID: 18361
			internal Regex PageGlobalsDetection;

			// Token: 0x040047BA RID: 18362
			internal Regex OverallTotalPagesDetection;

			// Token: 0x040047BB RID: 18363
			internal Regex TotalPagesDetection;

			// Token: 0x040047BC RID: 18364
			internal Regex AggregatesDetection;

			// Token: 0x040047BD RID: 18365
			internal Regex UserDetection;

			// Token: 0x040047BE RID: 18366
			internal Regex DataSetsDetection;

			// Token: 0x040047BF RID: 18367
			internal Regex DataSourcesDetection;

			// Token: 0x040047C0 RID: 18368
			internal Regex VariablesDetection;

			// Token: 0x040047C1 RID: 18369
			internal Regex MeDotValueExpression;

			// Token: 0x040047C2 RID: 18370
			internal Regex MeDotValueDetection;

			// Token: 0x040047C3 RID: 18371
			internal Regex IllegalCharacterDetection;

			// Token: 0x040047C4 RID: 18372
			internal Regex LineTerminatorDetection;

			// Token: 0x040047C5 RID: 18373
			internal Regex FieldOnly;

			// Token: 0x040047C6 RID: 18374
			internal Regex ParameterOnly;

			// Token: 0x040047C7 RID: 18375
			internal Regex StringLiteralOnly;

			// Token: 0x040047C8 RID: 18376
			internal Regex NothingOnly;

			// Token: 0x040047C9 RID: 18377
			internal Regex InScopeOrLevel;

			// Token: 0x040047CA RID: 18378
			internal Regex InScope;

			// Token: 0x040047CB RID: 18379
			internal Regex Level;

			// Token: 0x040047CC RID: 18380
			internal Regex CreateDrillthroughContext;

			// Token: 0x040047CD RID: 18381
			internal Regex ReportItemName;

			// Token: 0x040047CE RID: 18382
			internal Regex FieldName;

			// Token: 0x040047CF RID: 18383
			internal Regex ParameterName;

			// Token: 0x040047D0 RID: 18384
			internal Regex DynamicParameterReference;

			// Token: 0x040047D1 RID: 18385
			internal Regex DataSetName;

			// Token: 0x040047D2 RID: 18386
			internal Regex DataSourceName;

			// Token: 0x040047D3 RID: 18387
			internal Regex SpecialFunction;

			// Token: 0x040047D4 RID: 18388
			internal Regex Arguments;

			// Token: 0x040047D5 RID: 18389
			internal Regex DynamicFieldReference;

			// Token: 0x040047D6 RID: 18390
			internal Regex DynamicFieldPropertyReference;

			// Token: 0x040047D7 RID: 18391
			internal Regex StaticFieldPropertyReference;

			// Token: 0x040047D8 RID: 18392
			internal Regex RewrittenCommandText;

			// Token: 0x040047D9 RID: 18393
			internal Regex SimpleDynamicFieldReference;

			// Token: 0x040047DA RID: 18394
			internal Regex SimpleDynamicReportItemReference;

			// Token: 0x040047DB RID: 18395
			internal Regex SimpleDynamicVariableReference;

			// Token: 0x040047DC RID: 18396
			internal Regex VariableName;

			// Token: 0x040047DD RID: 18397
			internal Regex RenderFormatPropertyName;

			// Token: 0x040047DE RID: 18398
			internal Regex HasLevelWithNoScope;

			// Token: 0x040047DF RID: 18399
			internal Regex RdlFunction;

			// Token: 0x040047E0 RID: 18400
			internal Regex ScopedFieldReferenceOnly;

			// Token: 0x040047E1 RID: 18401
			internal Regex ScopesDetection;

			// Token: 0x040047E2 RID: 18402
			internal Regex SimpleDynamicScopeReference;

			// Token: 0x040047E3 RID: 18403
			internal Regex ScopeName;

			// Token: 0x040047E4 RID: 18404
			internal Regex DictionaryOpWithIdentifier;

			// Token: 0x040047E5 RID: 18405
			internal Regex IndexerWithIdentifier;

			// Token: 0x040047E6 RID: 18406
			internal static readonly VBExpressionParser.ReportRegularExpressions Value = new VBExpressionParser.ReportRegularExpressions();
		}
	}
}
