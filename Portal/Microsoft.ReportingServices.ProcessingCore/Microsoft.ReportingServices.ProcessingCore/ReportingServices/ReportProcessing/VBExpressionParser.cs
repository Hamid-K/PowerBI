using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000789 RID: 1929
	internal sealed class VBExpressionParser : ExpressionParser
	{
		// Token: 0x06006B7E RID: 27518 RVA: 0x001B3400 File Offset: 0x001B1600
		internal VBExpressionParser(ErrorContext errorContext)
			: base(errorContext)
		{
			this.m_regexes = VBExpressionParser.ReportRegularExpressions.Value;
			this.m_numberOfAggregates = 0;
			this.m_numberOfRunningValues = 0;
			this.m_bodyRefersToReportItems = false;
			this.m_pageSectionRefersToReportItems = false;
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x001B3430 File Offset: 0x001B1630
		internal override CodeDomProvider GetCodeCompiler()
		{
			return new VBCodeProvider();
		}

		// Token: 0x06006B80 RID: 27520 RVA: 0x001B3437 File Offset: 0x001B1637
		internal override string GetCompilerArguments()
		{
			return "/optimize+";
		}

		// Token: 0x06006B81 RID: 27521 RVA: 0x001B3440 File Offset: 0x001B1640
		internal override ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context)
		{
			Global.Tracer.Assert(expression != null);
			string text;
			return this.Lex(expression, context, out text);
		}

		// Token: 0x06006B82 RID: 27522 RVA: 0x001B3468 File Offset: 0x001B1668
		internal override ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.DetectionFlags flag, out bool reportParameterReferenced, out string reportParameterName, out bool userCollectionReferenced)
		{
			string text;
			ExpressionInfo expressionInfo = this.Lex(expression, context, out text);
			reportParameterReferenced = false;
			reportParameterName = null;
			userCollectionReferenced = false;
			if (expressionInfo.Type == ExpressionInfo.Types.Expression)
			{
				if ((flag & ExpressionParser.DetectionFlags.ParameterReference) != (ExpressionParser.DetectionFlags)0)
				{
					reportParameterReferenced = true;
					reportParameterName = this.GetReferencedReportParameters(text);
				}
				if ((flag & ExpressionParser.DetectionFlags.UserReference) != (ExpressionParser.DetectionFlags)0)
				{
					userCollectionReferenced = this.DetectUserReference(text);
				}
			}
			return expressionInfo;
		}

		// Token: 0x06006B83 RID: 27523 RVA: 0x001B34B8 File Offset: 0x001B16B8
		internal override ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, out bool userCollectionReferenced)
		{
			string text;
			ExpressionInfo expressionInfo = this.Lex(expression, context, out text);
			userCollectionReferenced = false;
			if (expressionInfo.Type == ExpressionInfo.Types.Expression)
			{
				userCollectionReferenced = this.DetectUserReference(text);
			}
			return expressionInfo;
		}

		// Token: 0x06006B84 RID: 27524 RVA: 0x001B34E3 File Offset: 0x001B16E3
		internal override void ConvertField2ComplexExpr(ref ExpressionInfo info)
		{
			Global.Tracer.Assert(info.Type == ExpressionInfo.Types.Field);
			info.Type = ExpressionInfo.Types.Expression;
			info.TransformedExpression = "Fields!" + info.Value + ".Value";
		}

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x06006B85 RID: 27525 RVA: 0x001B351E File Offset: 0x001B171E
		internal override bool BodyRefersToReportItems
		{
			get
			{
				return this.m_bodyRefersToReportItems;
			}
		}

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x06006B86 RID: 27526 RVA: 0x001B3526 File Offset: 0x001B1726
		internal override bool PageSectionRefersToReportItems
		{
			get
			{
				return this.m_pageSectionRefersToReportItems;
			}
		}

		// Token: 0x17002564 RID: 9572
		// (get) Token: 0x06006B87 RID: 27527 RVA: 0x001B352E File Offset: 0x001B172E
		internal override int NumberOfAggregates
		{
			get
			{
				return this.m_numberOfAggregates;
			}
		}

		// Token: 0x17002565 RID: 9573
		// (get) Token: 0x06006B88 RID: 27528 RVA: 0x001B3536 File Offset: 0x001B1736
		internal override int LastID
		{
			get
			{
				return this.m_numberOfAggregates + this.m_numberOfRunningValues;
			}
		}

		// Token: 0x06006B89 RID: 27529 RVA: 0x001B3548 File Offset: 0x001B1748
		private ExpressionInfo Lex(string expression, ExpressionParser.ExpressionContext context, out string vbExpression)
		{
			vbExpression = null;
			this.m_context = context;
			ExpressionInfo expressionInfo = (context.ParseExtended ? new ExpressionInfoExtended() : new ExpressionInfo());
			expressionInfo.OriginalText = expression;
			Match match = this.m_regexes.NonConstant.Match(expression);
			if (!match.Success)
			{
				expressionInfo.Type = ExpressionInfo.Types.Constant;
				switch (context.ConstantType)
				{
				case ExpressionParser.ConstantType.String:
					expressionInfo.Value = expression;
					break;
				case ExpressionParser.ConstantType.Boolean:
				{
					bool flag;
					try
					{
						flag = XmlConvert.ToBoolean(expression);
					}
					catch
					{
						flag = false;
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidBooleanConstant, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { expression });
					}
					expressionInfo.BoolValue = flag;
					break;
				}
				case ExpressionParser.ConstantType.Integer:
				{
					int num;
					try
					{
						num = XmlConvert.ToInt32(expression);
					}
					catch
					{
						num = 0;
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidIntegerConstant, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { expression });
					}
					expressionInfo.IntValue = num;
					break;
				}
				default:
					Global.Tracer.Assert(false);
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
			}
			else
			{
				ExpressionParser.GrammarFlags grammarFlags;
				if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
				{
					grammarFlags = (ExpressionParser.GrammarFlags)(ExpressionParser.ExpressionType2Restrictions(this.m_context.ExpressionType) | ExpressionParser.Restrictions.InPageSection);
				}
				else
				{
					grammarFlags = (ExpressionParser.GrammarFlags)(ExpressionParser.ExpressionType2Restrictions(this.m_context.ExpressionType) | ExpressionParser.Restrictions.InBody);
				}
				vbExpression = expression.Substring(match.Length);
				this.VBLex(vbExpression, false, grammarFlags, expressionInfo);
			}
			return expressionInfo;
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x001B3700 File Offset: 0x001B1900
		private string GetReferencedReportParameters(string expression)
		{
			string text = null;
			Match match = this.m_regexes.ParameterOnly.Match(expression);
			if (match.Success)
			{
				text = match.Result("${parametername}");
			}
			return text;
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x001B3736 File Offset: 0x001B1936
		private bool DetectUserReference(string expression)
		{
			return this.Detected(expression, this.m_regexes.UserDetection);
		}

		// Token: 0x06006B8C RID: 27532 RVA: 0x001B374C File Offset: 0x001B194C
		private void VBLex(string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, ExpressionInfo expressionInfo)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyFields) == (ExpressionParser.GrammarFlags)0)
			{
				Match match = this.m_regexes.FieldOnly.Match(expression);
				if (match.Success)
				{
					string text = match.Result("${fieldname}");
					expressionInfo.AddReferencedField(text);
					expressionInfo.Type = ExpressionInfo.Types.Field;
					expressionInfo.Value = text;
					return;
				}
				if (this.m_context.ParseExtended)
				{
					match = this.m_regexes.FieldWithExtendedProperty.Match(expression);
					if (match.Success)
					{
						((ExpressionInfoExtended)expressionInfo).IsExtendedSimpleFieldReference = true;
					}
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyDataSets) == (ExpressionParser.GrammarFlags)0)
			{
				Match match2 = this.m_regexes.RewrittenCommandText.Match(expression);
				if (match2.Success)
				{
					string text2 = match2.Result("${datasetname}");
					expressionInfo.AddReferencedDataSet(text2);
					expressionInfo.Type = ExpressionInfo.Types.Token;
					expressionInfo.Value = text2;
					return;
				}
			}
			this.EnforceRestrictions(ref expression, isParameter, grammarFlags);
			string text3 = string.Empty;
			int i = 0;
			bool flag = false;
			while (i < expression.Length)
			{
				Match match3 = this.m_regexes.SpecialFunction.Match(expression, i);
				if (!match3.Success)
				{
					text3 += expression.Substring(i);
					i = expression.Length;
				}
				else
				{
					text3 += expression.Substring(i, match3.Index - i);
					string text4 = match3.Result("${sfname}");
					if (text4 == null || text4.Length == 0)
					{
						text3 += match3.Value;
						i = match3.Index + match3.Length;
					}
					else
					{
						text3 += match3.Result("${prefix}");
						i = match3.Index + match3.Length;
						string text5 = this.CreateAggregateID();
						if (string.Compare(text4, "Previous", StringComparison.OrdinalIgnoreCase) == 0)
						{
							RunningValueInfo runningValueInfo;
							this.GetPreviousAggregate(i, text4, expression, isParameter, grammarFlags, out i, out runningValueInfo);
							runningValueInfo.Name = text5;
							expressionInfo.AddRunningValue(runningValueInfo);
						}
						else if (string.Compare(text4, "RunningValue", StringComparison.OrdinalIgnoreCase) == 0)
						{
							RunningValueInfo runningValueInfo2;
							this.GetRunningValue(i, text4, expression, isParameter, grammarFlags, out i, out runningValueInfo2);
							runningValueInfo2.Name = text5;
							expressionInfo.AddRunningValue(runningValueInfo2);
						}
						else if (string.Compare(text4, "RowNumber", StringComparison.OrdinalIgnoreCase) == 0)
						{
							RunningValueInfo runningValueInfo3;
							this.GetRowNumber(i, text4, expression, isParameter, grammarFlags, out i, out runningValueInfo3);
							runningValueInfo3.Name = text5;
							expressionInfo.AddRunningValue(runningValueInfo3);
						}
						else
						{
							DataAggregateInfo dataAggregateInfo;
							this.GetAggregate(i, text4, expression, isParameter, grammarFlags, out i, out dataAggregateInfo);
							dataAggregateInfo.Name = text5;
							expressionInfo.AddAggregate(dataAggregateInfo);
						}
						if (!flag)
						{
							flag = true;
							if (text3.Trim().Length == 0 && expression.Substring(i).Trim().Length == 0)
							{
								expressionInfo.Type = ExpressionInfo.Types.Aggregate;
								expressionInfo.Value = text5;
								return;
							}
						}
						if (expressionInfo.TransformedExpressionAggregatePositions == null)
						{
							expressionInfo.TransformedExpressionAggregatePositions = new IntList();
							expressionInfo.TransformedExpressionAggregateIDs = new StringList();
						}
						expressionInfo.TransformedExpressionAggregatePositions.Add(text3.Length);
						expressionInfo.TransformedExpressionAggregateIDs.Add(text5);
						text3 = text3 + "Aggregates!" + text5;
					}
				}
			}
			this.GetReferencedFieldNames(text3, expressionInfo);
			this.GetReferencedReportItemNames(text3, expressionInfo);
			this.GetReferencedParameterNames(text3, expressionInfo);
			this.GetReferencedDataSetNames(text3, expressionInfo);
			this.GetReferencedDataSourceNames(text3, expressionInfo);
			expressionInfo.Type = ExpressionInfo.Types.Expression;
			expressionInfo.TransformedExpression = text3;
			if (this.m_context.ObjectType == ObjectType.Textbox && this.Detected(expressionInfo.TransformedExpression, this.m_regexes.MeDotValueDetection))
			{
				base.SetValueReferenced();
			}
		}

		// Token: 0x06006B8D RID: 27533 RVA: 0x001B3AB4 File Offset: 0x001B1CB4
		private void EnforceRestrictions(ref string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyFields) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.FieldDetection))
			{
				if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
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
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			int num = this.NumberOfTimesDetected(expression, this.m_regexes.ReportItemsDetection);
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyReportItems) != (ExpressionParser.GrammarFlags)0 && 0 < num)
			{
				if (isParameter)
				{
					Global.Tracer.Assert((this.m_context.Location & LocationFlags.InPageSection) == (LocationFlags)0);
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
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0 && 1 < num)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsMultiReportItemsInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if (0 < num)
			{
				if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
				{
					this.m_pageSectionRefersToReportItems = true;
				}
				else
				{
					this.m_bodyRefersToReportItems = true;
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyPageGlobals) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.PageGlobalsDetection))
			{
				Global.Tracer.Assert((this.m_context.Location & LocationFlags.InPageSection) == (LocationFlags)0);
				this.m_errorContext.Register(ProcessingErrorCode.rsPageNumberInBody, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if (this.Detected(expression, this.m_regexes.AggregatesDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsGlobalNotDefined, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyDataSets) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.DataSetsDetection))
			{
				if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
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
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyDataSources) != (ExpressionParser.GrammarFlags)0 && this.Detected(expression, this.m_regexes.DataSourcesDetection))
			{
				if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
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
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			this.RemoveLineTerminators(ref expression, this.m_regexes.LineTerminatorDetection);
			if (this.Detected(expression, this.m_regexes.IllegalCharacterDetection))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidCharacterInExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
		}

		// Token: 0x06006B8E RID: 27534 RVA: 0x001B42C8 File Offset: 0x001B24C8
		private void GetReferencedReportItemNames(string expression, ExpressionInfo expressionInfo)
		{
			MatchCollection matchCollection = this.m_regexes.ReportItemName.Matches(expression);
			for (int i = 0; i < matchCollection.Count; i++)
			{
				string text = matchCollection[i].Result("${reportitemname}");
				if (text != null && text.Length != 0)
				{
					expressionInfo.AddReferencedReportItem(text);
				}
			}
		}

		// Token: 0x06006B8F RID: 27535 RVA: 0x001B431C File Offset: 0x001B251C
		private void GetReferencedFieldNames(string expression, ExpressionInfo expressionInfo)
		{
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
		}

		// Token: 0x06006B90 RID: 27536 RVA: 0x001B4454 File Offset: 0x001B2654
		private void GetReferencedParameterNames(string expression, ExpressionInfo expressionInfo)
		{
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

		// Token: 0x06006B91 RID: 27537 RVA: 0x001B44A8 File Offset: 0x001B26A8
		private void GetReferencedDataSetNames(string expression, ExpressionInfo expressionInfo)
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

		// Token: 0x06006B92 RID: 27538 RVA: 0x001B44FC File Offset: 0x001B26FC
		private void GetReferencedDataSourceNames(string expression, ExpressionInfo expressionInfo)
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

		// Token: 0x06006B93 RID: 27539 RVA: 0x001B4550 File Offset: 0x001B2750
		private bool Detected(string expression, Regex detectionRegex)
		{
			return this.NumberOfTimesDetected(expression, detectionRegex) != 0;
		}

		// Token: 0x06006B94 RID: 27540 RVA: 0x001B4560 File Offset: 0x001B2760
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

		// Token: 0x06006B95 RID: 27541 RVA: 0x001B45AC File Offset: 0x001B27AC
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

		// Token: 0x06006B96 RID: 27542 RVA: 0x001B4638 File Offset: 0x001B2838
		private void GetRunningValue(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out RunningValueInfo runningValue)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyRunningValue) != (ExpressionParser.GrammarFlags)0)
			{
				if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
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
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsRunningValueInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (3 != list.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			runningValue = new RunningValueInfo();
			if (2 <= list.Count)
			{
				bool flag;
				try
				{
					runningValue.AggregateType = (DataAggregateInfo.AggregateTypes)Enum.Parse(typeof(DataAggregateInfo.AggregateTypes), list[1], true);
					flag = DataAggregateInfo.AggregateTypes.Aggregate != runningValue.AggregateType && DataAggregateInfo.AggregateTypes.Previous != runningValue.AggregateType && DataAggregateInfo.AggregateTypes.CountRows != runningValue.AggregateType;
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
				if (DataAggregateInfo.AggregateTypes.Count == runningValue.AggregateType && "*" == list[0].Trim())
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsCountStarRVNotSupported, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else
				{
					runningValue.Expressions = new ExpressionInfo[1];
					runningValue.Expressions[0] = this.GetParameterExpression(list[0], grammarFlags);
				}
			}
			if (3 <= list.Count)
			{
				runningValue.Scope = this.GetScope(list[2], true);
			}
			this.m_numberOfRunningValues++;
		}

		// Token: 0x06006B97 RID: 27543 RVA: 0x001B4A88 File Offset: 0x001B2C88
		private void GetPreviousAggregate(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out RunningValueInfo runningValue)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyPrevious) != (ExpressionParser.GrammarFlags)0)
			{
				if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
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
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsPreviousAggregateInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (1 != list.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			runningValue = new RunningValueInfo();
			runningValue.AggregateType = DataAggregateInfo.AggregateTypes.Previous;
			if (1 <= list.Count)
			{
				runningValue.Expressions = new ExpressionInfo[1];
				runningValue.Expressions[0] = this.GetParameterExpression(list[0], grammarFlags);
			}
			this.m_numberOfRunningValues++;
		}

		// Token: 0x06006B98 RID: 27544 RVA: 0x001B4DC0 File Offset: 0x001B2FC0
		private void GetRowNumber(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out RunningValueInfo rowNumber)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyRowNumber) != (ExpressionParser.GrammarFlags)0)
			{
				if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
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
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsRowNumberInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (1 != list.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			rowNumber = new RunningValueInfo();
			rowNumber.AggregateType = DataAggregateInfo.AggregateTypes.CountRows;
			rowNumber.Expressions = new ExpressionInfo[0];
			if (1 <= list.Count)
			{
				rowNumber.Scope = this.GetScope(list[0], true);
			}
			this.m_numberOfRunningValues++;
		}

		// Token: 0x06006B99 RID: 27545 RVA: 0x001B50A4 File Offset: 0x001B32A4
		private string GetScope(string expression, bool allowNothing)
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
			this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateScope, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			return null;
		}

		// Token: 0x06006B9A RID: 27546 RVA: 0x001B512C File Offset: 0x001B332C
		private bool IsRecursive(string flag)
		{
			ExpressionParser.RecursiveFlags recursiveFlags = ExpressionParser.RecursiveFlags.Simple;
			try
			{
				recursiveFlags = (ExpressionParser.RecursiveFlags)Enum.Parse(typeof(ExpressionParser.RecursiveFlags), flag, true);
			}
			catch
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateRecursiveFlag, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
			}
			return ExpressionParser.RecursiveFlags.Recursive == recursiveFlags;
		}

		// Token: 0x06006B9B RID: 27547 RVA: 0x001B51A8 File Offset: 0x001B33A8
		private void GetAggregate(int currentPos, string functionName, string expression, bool isParameter, ExpressionParser.GrammarFlags grammarFlags, out int newPos, out DataAggregateInfo aggregate)
		{
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyAggregates) != (ExpressionParser.GrammarFlags)0)
			{
				if (isParameter)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateofAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.DataSetFilters == this.m_context.ExpressionType || ExpressionParser.ExpressionType.DataRegionFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.GroupExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInGroupExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
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
				else
				{
					Global.Tracer.Assert(ExpressionParser.ExpressionType.ReportParameter == this.m_context.ExpressionType);
					this.m_errorContext.Register(ProcessingErrorCode.rsAggregateInReportParameterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			List<string> list;
			this.GetArguments(currentPos, expression, out newPos, out list);
			if (list.Count != 0 && 1 != list.Count && 2 != list.Count && 3 != list.Count)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
			}
			aggregate = new DataAggregateInfo();
			aggregate.AggregateType = (DataAggregateInfo.AggregateTypes)Enum.Parse(typeof(DataAggregateInfo.AggregateTypes), functionName, true);
			if ((grammarFlags & ExpressionParser.GrammarFlags.DenyPostSortAggregate) != (ExpressionParser.GrammarFlags)0 && aggregate.IsPostSortAggregate())
			{
				if (ExpressionParser.ExpressionType.GroupingFilters == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPostSortAggregateInGroupFilterExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
				else if (ExpressionParser.ExpressionType.SortExpression == this.m_context.ExpressionType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsPostSortAggregateInSortExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
				}
			}
			if (DataAggregateInfo.AggregateTypes.CountRows == aggregate.AggregateType)
			{
				aggregate.AggregateType = DataAggregateInfo.AggregateTypes.CountRows;
				aggregate.Expressions = new ExpressionInfo[0];
				if (1 == list.Count)
				{
					aggregate.SetScope(this.GetScope(list[0], false));
				}
				else if (2 == list.Count)
				{
					aggregate.Recursive = this.IsRecursive(list[1]);
				}
				else if (list.Count != 0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsWrongNumberOfParameters, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, new string[] { functionName });
				}
				if (DataAggregateInfo.AggregateTypes.CountRows == aggregate.AggregateType && (this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsCountRowsInPageSectionExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
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
					if (DataAggregateInfo.AggregateTypes.Count == aggregate.AggregateType && "*" == list[0].Trim())
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsCountStarNotSupported, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else
					{
						aggregate.Expressions = new ExpressionInfo[1];
						aggregate.Expressions[0] = this.GetParameterExpression(list[0], grammarFlags);
						if (DataAggregateInfo.AggregateTypes.Aggregate == aggregate.AggregateType && ExpressionInfo.Types.Field != aggregate.Expressions[0].Type)
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidCustomAggregateExpression, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
						}
					}
				}
				if (2 <= list.Count)
				{
					aggregate.SetScope(this.GetScope(list[1], false));
				}
				if (3 <= list.Count)
				{
					if (aggregate.IsPostSortAggregate() || DataAggregateInfo.AggregateTypes.Aggregate == aggregate.AggregateType)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidRecursiveAggregate, Severity.Error, this.m_context.ObjectType, this.m_context.ObjectName, this.m_context.PropertyName, Array.Empty<string>());
					}
					else
					{
						aggregate.Recursive = this.IsRecursive(list[2]);
					}
				}
			}
			this.m_numberOfAggregates++;
		}

		// Token: 0x06006B9C RID: 27548 RVA: 0x001B57C8 File Offset: 0x001B39C8
		private ExpressionInfo GetParameterExpression(string parameterExpression, ExpressionParser.GrammarFlags grammarFlags)
		{
			ExpressionInfo expressionInfo = new ExpressionInfo();
			expressionInfo.OriginalText = parameterExpression;
			if ((this.m_context.Location & LocationFlags.InPageSection) != (LocationFlags)0)
			{
				grammarFlags |= ExpressionParser.GrammarFlags.DenyAggregates | ExpressionParser.GrammarFlags.DenyRunningValue | ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyPrevious;
			}
			else
			{
				grammarFlags |= ExpressionParser.GrammarFlags.DenyAggregates | ExpressionParser.GrammarFlags.DenyRunningValue | ExpressionParser.GrammarFlags.DenyRowNumber | ExpressionParser.GrammarFlags.DenyReportItems | ExpressionParser.GrammarFlags.DenyPrevious;
			}
			this.VBLex(parameterExpression, true, grammarFlags, expressionInfo);
			return expressionInfo;
		}

		// Token: 0x06006B9D RID: 27549 RVA: 0x001B5814 File Offset: 0x001B3A14
		private void GetArguments(int currentPos, string expression, out int newPos, out List<string> arguments)
		{
			int num = 1;
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
					else if (text4 != null && text4.Length != 0)
					{
						if (1 == num)
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

		// Token: 0x06006B9E RID: 27550 RVA: 0x001B5A1C File Offset: 0x001B3C1C
		private string CreateAggregateID()
		{
			return "Aggregate" + (this.m_numberOfAggregates + this.m_numberOfRunningValues).ToString();
		}

		// Token: 0x04003613 RID: 13843
		private const string RunningValue = "RunningValue";

		// Token: 0x04003614 RID: 13844
		private const string RowNumber = "RowNumber";

		// Token: 0x04003615 RID: 13845
		private const string Previous = "Previous";

		// Token: 0x04003616 RID: 13846
		private const string Star = "*";

		// Token: 0x04003617 RID: 13847
		private VBExpressionParser.ReportRegularExpressions m_regexes;

		// Token: 0x04003618 RID: 13848
		private int m_numberOfAggregates;

		// Token: 0x04003619 RID: 13849
		private int m_numberOfRunningValues;

		// Token: 0x0400361A RID: 13850
		private bool m_bodyRefersToReportItems;

		// Token: 0x0400361B RID: 13851
		private bool m_pageSectionRefersToReportItems;

		// Token: 0x0400361C RID: 13852
		private ExpressionParser.ExpressionContext m_context;

		// Token: 0x02000CE4 RID: 3300
		private sealed class ReportRegularExpressions
		{
			// Token: 0x06008D68 RID: 36200 RVA: 0x0023EEB8 File Offset: 0x0023D0B8
			private ReportRegularExpressions()
			{
				RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;
				this.NonConstant = new Regex("^\\s*=", regexOptions);
				string text = Regex.Escape("-+()#,:&*/\\^<=>");
				string text2 = Regex.Escape("!");
				string text3 = Regex.Escape(".");
				string text4 = "[" + text2 + text3 + "]";
				string text5 = "(^|[" + text + "\\s])";
				string text6 = string.Concat(new string[] { "($|[", text, text2, text3, "\\s])" });
				this.FieldDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Fields)" + text6, regexOptions);
				this.ReportItemsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>ReportItems)" + text6, regexOptions);
				this.ParametersDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Parameters)" + text6, regexOptions);
				this.PageGlobalsDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text5, "(?<detected>(Globals", text4, "PageNumber)|(Globals", text4, "TotalPages))", text6 }), regexOptions);
				this.AggregatesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Aggregates)" + text6, regexOptions);
				this.UserDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>User)" + text6, regexOptions);
				this.DataSetsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>DataSets)" + text6, regexOptions);
				this.DataSourcesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>DataSources)" + text6, regexOptions);
				this.MeDotValueDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>(?:Me.)?Value)" + text6, regexOptions);
				string text7 = Regex.Escape(":");
				string text8 = Regex.Escape("#");
				string text9 = string.Concat(new string[] { "(", text8, "[^", text8, "]*", text8, ")" });
				string text10 = Regex.Escape(":=");
				this.LineTerminatorDetection = new Regex("(?<detected>(\\u000D\\u000A)|([\\u000D\\u000A\\u2028\\u2029]))", regexOptions);
				this.IllegalCharacterDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text9, "|", text10, "|(?<detected>", text7, ")" }), regexOptions);
				string text11 = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]*";
				string text12 = string.Concat(new string[] { "ReportItems", text2, "(?<reportitemname>", text11, ")" });
				string text13 = string.Concat(new string[] { "Fields", text2, "(?<fieldname>", text11, ")" });
				string text14 = string.Concat(new string[] { "Parameters", text2, "(?<parametername>", text11, ")" });
				string text15 = string.Concat(new string[] { "DataSets", text2, "(?<datasetname>", text11, ")" });
				string text16 = string.Concat(new string[] { "DataSources", text2, "(?<datasourcename>", text11, ")" });
				string text17 = string.Concat(new string[]
				{
					"Fields((",
					text2,
					"(?<fieldname>",
					text11,
					"))|((",
					text3,
					"Item)?",
					Regex.Escape("("),
					"\"(?<fieldname>",
					text11,
					")\"",
					Regex.Escape(")"),
					"))"
				});
				this.ExtendedPropertyName = new Regex("(Value|IsMissing|UniqueName|BackgroundColor|Color|FontFamily|Fontsize|FontWeight|FontStyle|TextDecoration|FormattedValue|Key|LevelNumber|ParentUniqueName)", regexOptions);
				string text18 = string.Concat(new string[]
				{
					"(",
					text3,
					"Properties)?",
					Regex.Escape("("),
					"\"(?<propertyname>",
					text11,
					")\"",
					Regex.Escape(")")
				});
				string[] array = new string[8];
				array[0] = "^\\s*";
				array[1] = text17;
				array[2] = "((";
				array[3] = text3;
				int num = 4;
				Regex extendedPropertyName = this.ExtendedPropertyName;
				array[num] = ((extendedPropertyName != null) ? extendedPropertyName.ToString() : null);
				array[5] = ")|(";
				array[6] = text18;
				array[7] = "))\\s*$";
				this.FieldWithExtendedProperty = new Regex(string.Concat(array), regexOptions);
				this.DynamicFieldReference = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|",
					text5,
					"(?<detected>(Fields(",
					text3,
					"Item)?",
					Regex.Escape("("),
					"))"
				}), regexOptions);
				this.DynamicFieldPropertyReference = new Regex(string.Concat(new string[]
				{
					"(\"((\"\")|[^\"])*\")|",
					text5,
					text13,
					"(",
					text3,
					"Properties)?",
					Regex.Escape("(")
				}), regexOptions);
				this.StaticFieldPropertyReference = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text5, text13, text3, "(?<propertyname>", text11, ")" }), regexOptions);
				this.FieldOnly = new Regex("^\\s*" + text13 + text3 + "Value\\s*$", regexOptions);
				this.RewrittenCommandText = new Regex("^\\s*" + text15 + text3 + "RewrittenCommandText\\s*$", regexOptions);
				this.ParameterOnly = new Regex("^\\s*" + text14 + text3 + "Value\\s*$", regexOptions);
				this.StringLiteralOnly = new Regex("^\\s*\"(?<string>((\"\")|[^\"])*)\"\\s*$", regexOptions);
				this.NothingOnly = new Regex("^\\s*Nothing\\s*$", regexOptions);
				this.ReportItemName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text12, regexOptions);
				this.FieldName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text13, regexOptions);
				this.ParameterName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text14, regexOptions);
				this.DataSetName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text15, regexOptions);
				this.DataSourceName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text16, regexOptions);
				this.SpecialFunction = new Regex("(\"((\"\")|[^\"])*\")|(?<prefix>" + text5 + ")(?<sfname>RunningValue|RowNumber|First|Last|Previous|Sum|Avg|Max|Min|CountDistinct|Count|CountRows|StDevP|VarP|StDev|Var|Aggregate)\\s*\\(", regexOptions);
				string text19 = Regex.Escape("(");
				string text20 = Regex.Escape(")");
				string text21 = Regex.Escape(",");
				this.Arguments = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|(?<openParen>", text19, ")|(?<closeParen>", text20, ")|(?<comma>", text21, ")" }), regexOptions);
			}

			// Token: 0x04004F46 RID: 20294
			internal Regex NonConstant;

			// Token: 0x04004F47 RID: 20295
			internal Regex FieldDetection;

			// Token: 0x04004F48 RID: 20296
			internal Regex ReportItemsDetection;

			// Token: 0x04004F49 RID: 20297
			internal Regex ParametersDetection;

			// Token: 0x04004F4A RID: 20298
			internal Regex PageGlobalsDetection;

			// Token: 0x04004F4B RID: 20299
			internal Regex AggregatesDetection;

			// Token: 0x04004F4C RID: 20300
			internal Regex UserDetection;

			// Token: 0x04004F4D RID: 20301
			internal Regex DataSetsDetection;

			// Token: 0x04004F4E RID: 20302
			internal Regex DataSourcesDetection;

			// Token: 0x04004F4F RID: 20303
			internal Regex MeDotValueDetection;

			// Token: 0x04004F50 RID: 20304
			internal Regex IllegalCharacterDetection;

			// Token: 0x04004F51 RID: 20305
			internal Regex LineTerminatorDetection;

			// Token: 0x04004F52 RID: 20306
			internal Regex FieldOnly;

			// Token: 0x04004F53 RID: 20307
			internal Regex ParameterOnly;

			// Token: 0x04004F54 RID: 20308
			internal Regex StringLiteralOnly;

			// Token: 0x04004F55 RID: 20309
			internal Regex NothingOnly;

			// Token: 0x04004F56 RID: 20310
			internal Regex ReportItemName;

			// Token: 0x04004F57 RID: 20311
			internal Regex FieldName;

			// Token: 0x04004F58 RID: 20312
			internal Regex ParameterName;

			// Token: 0x04004F59 RID: 20313
			internal Regex DataSetName;

			// Token: 0x04004F5A RID: 20314
			internal Regex DataSourceName;

			// Token: 0x04004F5B RID: 20315
			internal Regex SpecialFunction;

			// Token: 0x04004F5C RID: 20316
			internal Regex Arguments;

			// Token: 0x04004F5D RID: 20317
			internal Regex DynamicFieldReference;

			// Token: 0x04004F5E RID: 20318
			internal Regex DynamicFieldPropertyReference;

			// Token: 0x04004F5F RID: 20319
			internal Regex StaticFieldPropertyReference;

			// Token: 0x04004F60 RID: 20320
			internal Regex RewrittenCommandText;

			// Token: 0x04004F61 RID: 20321
			internal Regex ExtendedPropertyName;

			// Token: 0x04004F62 RID: 20322
			internal Regex FieldWithExtendedProperty;

			// Token: 0x04004F63 RID: 20323
			internal static readonly VBExpressionParser.ReportRegularExpressions Value = new VBExpressionParser.ReportRegularExpressions();
		}
	}
}
