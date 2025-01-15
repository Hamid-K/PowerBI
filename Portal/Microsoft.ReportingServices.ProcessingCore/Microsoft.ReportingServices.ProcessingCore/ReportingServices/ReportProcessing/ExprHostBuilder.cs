using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000684 RID: 1668
	internal sealed class ExprHostBuilder
	{
		// Token: 0x06005B5D RID: 23389 RVA: 0x00177DC1 File Offset: 0x00175FC1
		internal ExprHostBuilder()
		{
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x00177DC9 File Offset: 0x00175FC9
		internal bool HasExpressions
		{
			get
			{
				return this.m_rootTypeDecl != null && this.m_rootTypeDecl.HasExpressions;
			}
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06005B5F RID: 23391 RVA: 0x00177DE0 File Offset: 0x00175FE0
		internal bool CustomCode
		{
			get
			{
				return this.m_setCode;
			}
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x00177DE8 File Offset: 0x00175FE8
		internal void SetCustomCode()
		{
			this.m_setCode = true;
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x00177DF4 File Offset: 0x00175FF4
		internal CodeCompileUnit GetExprHost(IntermediateFormatVersion version, bool refusePermissions)
		{
			Global.Tracer.Assert(this.m_rootTypeDecl != null && this.m_currentTypeDecl.Parent == null, "(m_rootTypeDecl != null && m_currentTypeDecl.Parent == null)");
			CodeCompileUnit codeCompileUnit = null;
			if (this.HasExpressions)
			{
				codeCompileUnit = new CodeCompileUnit();
				codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Reflection.AssemblyVersion", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(version.ToString()))
				}));
				if (refusePermissions)
				{
					codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Security.Permissions.SecurityPermission", new CodeAttributeArgument[]
					{
						new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityAction)), "RequestMinimum")),
						new CodeAttributeArgument("Execution", new CodePrimitiveExpression(true))
					}));
					codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Security.Permissions.SecurityPermission", new CodeAttributeArgument[]
					{
						new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityAction)), "RequestOptional")),
						new CodeAttributeArgument("Execution", new CodePrimitiveExpression(true))
					}));
				}
				CodeNamespace codeNamespace = new CodeNamespace();
				codeCompileUnit.Namespaces.Add(codeNamespace);
				codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Convert"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Math"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.VisualBasic"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.ReportingServices.ReportProcessing.ReportObjectModel"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel"));
				codeNamespace.Types.Add(this.m_rootTypeDecl.Type);
			}
			this.m_rootTypeDecl = null;
			return codeCompileUnit;
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00177FCC File Offset: 0x001761CC
		internal ExprHostBuilder.ErrorSource ParseErrorSource(CompilerError error, out int id)
		{
			Global.Tracer.Assert(error.FileName != null, "(error.FileName != null)");
			id = -1;
			if (error.FileName.StartsWith("CustomCode", StringComparison.Ordinal))
			{
				return ExprHostBuilder.ErrorSource.CustomCode;
			}
			Match match = ExprHostBuilder.m_findCodeModuleClassInstanceDeclNumber.Match(error.FileName);
			if (match.Success && int.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out id))
			{
				return ExprHostBuilder.ErrorSource.CodeModuleClassInstanceDecl;
			}
			match = ExprHostBuilder.m_findExprNumber.Match(error.FileName);
			if (match.Success && int.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out id))
			{
				return ExprHostBuilder.ErrorSource.Expression;
			}
			return ExprHostBuilder.ErrorSource.Unknown;
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x00178080 File Offset: 0x00176280
		internal void ReportStart()
		{
			this.m_currentTypeDecl = (this.m_rootTypeDecl = new ExprHostBuilder.RootTypeDecl(this.m_setCode));
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x001780A7 File Offset: 0x001762A7
		internal void ReportEnd()
		{
			this.m_rootTypeDecl.CompleteConstructorCreation();
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x001780B4 File Offset: 0x001762B4
		internal void ReportLanguage(ExpressionInfo expression)
		{
			this.ExpressionAdd("ReportLanguageExpr", expression);
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x001780C2 File Offset: 0x001762C2
		internal void GenericLabel(ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x001780D0 File Offset: 0x001762D0
		internal void GenericValue(ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x001780DE File Offset: 0x001762DE
		internal void GenericNoRows(ExpressionInfo expression)
		{
			this.ExpressionAdd("NoRowsExpr", expression);
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x001780EC File Offset: 0x001762EC
		internal void GenericVisibilityHidden(ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibilityHiddenExpr", expression);
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x001780FA File Offset: 0x001762FA
		internal void AggregateParamExprAdd(ExpressionInfo expression)
		{
			this.AggregateStart();
			this.GenericValue(expression);
			expression.ExprHostID = this.AggregateEnd();
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x00178115 File Offset: 0x00176315
		internal void CustomCodeProxyStart()
		{
			Global.Tracer.Assert(this.m_setCode, "(m_setCode)");
			this.m_currentTypeDecl = new ExprHostBuilder.CustomCodeProxyDecl(this.m_currentTypeDecl);
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x0017813D File Offset: 0x0017633D
		internal void CustomCodeProxyEnd()
		{
			this.m_rootTypeDecl.Type.Members.Add(this.m_currentTypeDecl.Type);
			this.TypeEnd(this.m_rootTypeDecl);
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x0017816C File Offset: 0x0017636C
		internal void CustomCodeClassInstance(string className, string instanceName, int id)
		{
			((ExprHostBuilder.CustomCodeProxyDecl)this.m_currentTypeDecl).AddClassInstance(className, instanceName, id);
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x00178181 File Offset: 0x00176381
		internal void ReportCode(string code)
		{
			((ExprHostBuilder.CustomCodeProxyDecl)this.m_currentTypeDecl).AddCode(code);
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x00178194 File Offset: 0x00176394
		internal void ReportParameterStart(string name)
		{
			this.TypeStart(name, "ReportParamExprHost");
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x001781A2 File Offset: 0x001763A2
		internal int ReportParameterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_rootTypeDecl, "m_reportParameterHostsRemotable", ref this.m_rootTypeDecl.ReportParameters);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x001781C6 File Offset: 0x001763C6
		internal void ReportParameterValidationExpression(ExpressionInfo expression)
		{
			this.ExpressionAdd("ValidationExpressionExpr", expression);
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x001781D4 File Offset: 0x001763D4
		internal void ReportParameterDefaultValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x001781DD File Offset: 0x001763DD
		internal void ReportParameterValidValuesStart()
		{
			this.TypeStart("ReportParameterValidValues", "IndexedExprHost");
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x001781EF File Offset: 0x001763EF
		internal void ReportParameterValidValuesEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValuesHost");
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x0017820E File Offset: 0x0017640E
		internal void ReportParameterValidValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00178217 File Offset: 0x00176417
		internal void ReportParameterValidValueLabelsStart()
		{
			this.TypeStart("ReportParameterValidValueLabels", "IndexedExprHost");
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x00178229 File Offset: 0x00176429
		internal void ReportParameterValidValueLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValueLabelsHost");
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x00178248 File Offset: 0x00176448
		internal void ReportParameterValidValueLabel(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00178251 File Offset: 0x00176451
		internal void CalcFieldStart(string name)
		{
			this.TypeStart(name, "CalcFieldExprHost");
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x0017825F File Offset: 0x0017645F
		internal int CalcFieldEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_fieldHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Fields);
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x0017828C File Offset: 0x0017648C
		internal void QueryParametersStart()
		{
			this.TypeStart("QueryParameters", "IndexedExprHost");
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0017829E File Offset: 0x0017649E
		internal void QueryParametersEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "QueryParametersHost");
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x001782BD File Offset: 0x001764BD
		internal void QueryParameterValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x001782C6 File Offset: 0x001764C6
		internal void DataSourceStart(string name)
		{
			this.TypeStart(name, "DataSourceExprHost");
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x001782D4 File Offset: 0x001764D4
		internal int DataSourceEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_dataSourceHostsRemotable", ref this.m_rootTypeDecl.DataSources);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x001782F2 File Offset: 0x001764F2
		internal void DataSourceConnectString(ExpressionInfo expression)
		{
			this.ExpressionAdd("ConnectStringExpr", expression);
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x00178300 File Offset: 0x00176500
		internal void DataSetStart(string name)
		{
			this.TypeStart(name, "DataSetExprHost");
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0017830E File Offset: 0x0017650E
		internal int DataSetEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_dataSetHostsRemotable", ref this.m_rootTypeDecl.DataSets);
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0017832C File Offset: 0x0017652C
		internal void DataSetQueryCommandText(ExpressionInfo expression)
		{
			this.ExpressionAdd("QueryCommandTextExpr", expression);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0017833A File Offset: 0x0017653A
		internal void PageSectionStart()
		{
			this.TypeStart(this.CreateTypeName("PageSection", this.m_rootTypeDecl.PageSections), "StyleExprHost");
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0017835D File Offset: 0x0017655D
		internal int PageSectionEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_pageSectionHostsRemotable", ref this.m_rootTypeDecl.PageSections);
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0017837B File Offset: 0x0017657B
		internal void ParameterOmit(ExpressionInfo expression)
		{
			this.ExpressionAdd("OmitExpr", expression);
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x00178389 File Offset: 0x00176589
		internal void StyleAttribute(string name, ExpressionInfo expression)
		{
			this.ExpressionAdd(name + "Expr", expression);
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x0017839D File Offset: 0x0017659D
		internal void ActionInfoStart()
		{
			this.TypeStart("ActionInfo", "ActionInfoExprHost");
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x001783AF File Offset: 0x001765AF
		internal void ActionInfoEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ActionInfoHost");
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x001783C8 File Offset: 0x001765C8
		internal void ActionStart()
		{
			this.TypeStart(this.CreateTypeName("Action", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Actions), "ActionExprHost");
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x001783F0 File Offset: 0x001765F0
		internal int ActionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_actionItemHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Actions);
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x0017841D File Offset: 0x0017661D
		internal void ActionHyperlink(ExpressionInfo expression)
		{
			this.ExpressionAdd("HyperlinkExpr", expression);
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x0017842B File Offset: 0x0017662B
		internal void ActionDrillThroughReportName(ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughReportNameExpr", expression);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x00178439 File Offset: 0x00176639
		internal void ActionDrillThroughBookmarkLink(ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughBookmarkLinkExpr", expression);
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x00178447 File Offset: 0x00176647
		internal void ActionBookmarkLink(ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkLinkExpr", expression);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x00178455 File Offset: 0x00176655
		internal void ActionDrillThroughParameterStart()
		{
			this.ParameterStart();
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0017845D File Offset: 0x0017665D
		internal int ActionDrillThroughParameterEnd()
		{
			return this.ParameterEnd("m_drillThroughParameterHostsRemotable");
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0017846A File Offset: 0x0017666A
		internal void ReportItemBookmark(ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkExpr", expression);
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00178478 File Offset: 0x00176678
		internal void ReportItemToolTip(ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00178486 File Offset: 0x00176686
		internal void LineStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x00178494 File Offset: 0x00176694
		internal int LineEnd()
		{
			return this.ReportItemEnd("m_lineHostsRemotable", ref this.m_rootTypeDecl.Lines);
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x001784AC File Offset: 0x001766AC
		internal void RectangleStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x001784BA File Offset: 0x001766BA
		internal int RectangleEnd()
		{
			return this.ReportItemEnd("m_rectangleHostsRemotable", ref this.m_rootTypeDecl.Rectangles);
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x001784D2 File Offset: 0x001766D2
		internal void TextBoxStart(string name)
		{
			this.TypeStart(name, "TextBoxExprHost");
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x001784E0 File Offset: 0x001766E0
		internal int TextBoxEnd()
		{
			return this.ReportItemEnd("m_textBoxHostsRemotable", ref this.m_rootTypeDecl.TextBoxes);
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x001784F8 File Offset: 0x001766F8
		internal void TextBoxToggleImageInitialState(ExpressionInfo expression)
		{
			this.ExpressionAdd("ToggleImageInitialStateExpr", expression);
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x00178506 File Offset: 0x00176706
		internal void UserSortExpressionsStart()
		{
			this.TypeStart("UserSort", "IndexedExprHost");
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x00178518 File Offset: 0x00176718
		internal void UserSortExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "UserSortExpressionsHost");
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x00178537 File Offset: 0x00176737
		internal void UserSortExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x00178540 File Offset: 0x00176740
		internal void ImageStart(string name)
		{
			this.TypeStart(name, "ImageExprHost");
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x0017854E File Offset: 0x0017674E
		internal int ImageEnd()
		{
			return this.ReportItemEnd("m_imageHostsRemotable", ref this.m_rootTypeDecl.Images);
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x00178566 File Offset: 0x00176766
		internal void ImageMIMEType(ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x00178574 File Offset: 0x00176774
		internal void SubreportStart(string name)
		{
			this.TypeStart(name, "SubreportExprHost");
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x00178582 File Offset: 0x00176782
		internal int SubreportEnd()
		{
			return this.ReportItemEnd("m_subreportHostsRemotable", ref this.m_rootTypeDecl.Subreports);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x0017859A File Offset: 0x0017679A
		internal void SubreportParameterStart()
		{
			this.ParameterStart();
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x001785A2 File Offset: 0x001767A2
		internal int SubreportParameterEnd()
		{
			return this.ParameterEnd("m_parameterHostsRemotable");
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x001785AF File Offset: 0x001767AF
		internal void ActiveXControlStart(string name)
		{
			this.TypeStart(name, "ActiveXControlExprHost");
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x001785BD File Offset: 0x001767BD
		internal int ActiveXControlEnd()
		{
			return this.ReportItemEnd("m_activeXControlHostsRemotable", ref this.m_rootTypeDecl.ActiveXControls);
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x001785D5 File Offset: 0x001767D5
		internal void ActiveXControlParameterStart()
		{
			this.ParameterStart();
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001785DD File Offset: 0x001767DD
		internal int ActiveXControlParameterEnd()
		{
			return this.ParameterEnd("m_parameterHostsRemotable");
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001785EA File Offset: 0x001767EA
		internal void SortingStart()
		{
			this.TypeStart("Sorting", "SortingExprHost");
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x001785FC File Offset: 0x001767FC
		internal void SortingEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SortingHost");
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x0017861B File Offset: 0x0017681B
		internal void SortingExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x00178624 File Offset: 0x00176824
		internal void SortDirectionsStart()
		{
			this.TypeStart("SortDirections", "IndexedExprHost");
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x00178636 File Offset: 0x00176836
		internal void SortDirectionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SortDirectionHosts");
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x00178655 File Offset: 0x00176855
		internal void SortDirection(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0017865E File Offset: 0x0017685E
		internal void FilterStart()
		{
			this.TypeStart(this.CreateTypeName("Filter", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Filters), "FilterExprHost");
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x00178686 File Offset: 0x00176886
		internal int FilterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_filterHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Filters);
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x001786B9 File Offset: 0x001768B9
		internal void FilterExpression(ExpressionInfo expression)
		{
			this.ExpressionAdd("FilterExpressionExpr", expression);
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x001786C7 File Offset: 0x001768C7
		internal void FilterValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x001786D0 File Offset: 0x001768D0
		internal void GroupingStart(string typeName)
		{
			this.TypeStart(typeName, "GroupingExprHost");
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001786DE File Offset: 0x001768DE
		internal void GroupingEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "GroupingHost");
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x001786FD File Offset: 0x001768FD
		internal void GroupingExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x00178706 File Offset: 0x00176906
		internal void GroupingParentExpressionsStart()
		{
			this.TypeStart("Parent", "IndexedExprHost");
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x00178718 File Offset: 0x00176918
		internal void GroupingParentExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ParentExpressionsHost");
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x00178737 File Offset: 0x00176937
		internal void GroupingParentExpression(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x00178740 File Offset: 0x00176940
		internal void ListStart(string name)
		{
			this.TypeStart(name, "ListExprHost");
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x0017874E File Offset: 0x0017694E
		internal int ListEnd()
		{
			return this.ReportItemEnd("m_listHostsRemotable", ref this.m_rootTypeDecl.Lists);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x00178766 File Offset: 0x00176966
		internal void MatrixDynamicGroupStart(string name)
		{
			this.TypeStart("MatrixDynamicGroup_" + name, "MatrixDynamicGroupExprHost");
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x00178780 File Offset: 0x00176980
		internal bool MatrixDynamicGroupEnd(bool column)
		{
			string baseTypeName = this.m_currentTypeDecl.Parent.BaseTypeName;
			if (!(baseTypeName == "MatrixExprHost"))
			{
				if (!(baseTypeName == "MatrixDynamicGroupExprHost"))
				{
					Global.Tracer.Assert(false);
					return false;
				}
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "SubGroupHost");
			}
			else
			{
				if (column)
				{
					return this.TypeEnd(this.m_currentTypeDecl.Parent, "ColumnGroupingsHost");
				}
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "RowGroupingsHost");
			}
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0017880E File Offset: 0x00176A0E
		internal void SubtotalStart()
		{
			this.TypeStart("Subtotal", "StyleExprHost");
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x00178820 File Offset: 0x00176A20
		internal void SubtotalEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SubtotalHost");
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x00178839 File Offset: 0x00176A39
		internal void MatrixStart(string name)
		{
			this.TypeStart(name, "MatrixExprHost");
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x00178847 File Offset: 0x00176A47
		internal int MatrixEnd()
		{
			return this.ReportItemEnd("m_matrixHostsRemotable", ref this.m_rootTypeDecl.Matrices);
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0017885F File Offset: 0x00176A5F
		internal void MultiChartStart()
		{
			this.TypeStart("MultiChart", "MultiChartExprHost");
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x00178871 File Offset: 0x00176A71
		internal void MultiChartEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MultiChartHost");
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0017888A File Offset: 0x00176A8A
		internal void ChartDynamicGroupStart(string name)
		{
			this.TypeStart("ChartDynamicGroup_" + name, "ChartDynamicGroupExprHost");
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x001788A4 File Offset: 0x00176AA4
		internal bool ChartDynamicGroupEnd(bool column)
		{
			string baseTypeName = this.m_currentTypeDecl.Parent.BaseTypeName;
			if (!(baseTypeName == "ChartExprHost"))
			{
				if (!(baseTypeName == "ChartDynamicGroupExprHost"))
				{
					Global.Tracer.Assert(false);
					return false;
				}
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "SubGroupHost");
			}
			else
			{
				if (column)
				{
					return this.TypeEnd(this.m_currentTypeDecl.Parent, "ColumnGroupingsHost");
				}
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "RowGroupingsHost");
			}
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x00178932 File Offset: 0x00176B32
		internal void ChartHeadingLabel(ExpressionInfo expression)
		{
			this.ExpressionAdd("HeadingLabelExpr", expression);
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x00178940 File Offset: 0x00176B40
		internal void ChartDataPointStart()
		{
			this.TypeStart(this.CreateTypeName("DataPoint", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataPoints), "ChartDataPointExprHost");
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x00178968 File Offset: 0x00176B68
		internal int ChartDataPointEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_chartDataPointHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataPoints);
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0017899B File Offset: 0x00176B9B
		internal void ChartDataPointDataValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x001789A4 File Offset: 0x00176BA4
		internal void DataLabelValue(ExpressionInfo expression)
		{
			this.ExpressionAdd("DataLabelValueExpr", expression);
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x001789B2 File Offset: 0x00176BB2
		internal void DataLabelStyleStart()
		{
			this.StyleStart("DataLabelStyle");
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x001789BF File Offset: 0x00176BBF
		internal void DataLabelStyleEnd()
		{
			this.StyleEnd("DataLabelStyleHost");
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x001789CC File Offset: 0x00176BCC
		internal void DataPointStyleStart()
		{
			this.StyleStart("Style");
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x001789D9 File Offset: 0x00176BD9
		internal void DataPointStyleEnd()
		{
			this.StyleEnd("StyleHost");
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x001789E6 File Offset: 0x00176BE6
		internal void DataPointMarkerStyleStart()
		{
			this.StyleStart("DataPointMarkerStyle");
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x001789F3 File Offset: 0x00176BF3
		internal void DataPointMarkerStyleEnd()
		{
			this.StyleEnd("MarkerStyleHost");
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x00178A00 File Offset: 0x00176C00
		internal void ChartTitleStart()
		{
			this.TypeStart("Title", "ChartTitleExprHost");
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x00178A12 File Offset: 0x00176C12
		internal void ChartTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TitleHost");
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x00178A2B File Offset: 0x00176C2B
		internal void ChartCaption(ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x00178A39 File Offset: 0x00176C39
		internal void MajorGridLinesStyleStart()
		{
			this.StyleStart("MajorGridLinesStyle");
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x00178A46 File Offset: 0x00176C46
		internal void MajorGridLinesStyleEnd()
		{
			this.StyleEnd("MajorGridLinesHost");
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x00178A53 File Offset: 0x00176C53
		internal void MinorGridLinesStyleStart()
		{
			this.StyleStart("MinorGridLinesStyle");
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x00178A60 File Offset: 0x00176C60
		internal void MinorGridLinesStyleEnd()
		{
			this.StyleEnd("MinorGridLinesHost");
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x00178A6D File Offset: 0x00176C6D
		internal void AxisMin(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinExpr", expression);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x00178A7B File Offset: 0x00176C7B
		internal void AxisMax(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMaxExpr", expression);
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x00178A89 File Offset: 0x00176C89
		internal void AxisCrossAt(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisCrossAtExpr", expression);
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x00178A97 File Offset: 0x00176C97
		internal void AxisMajorInterval(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMajorIntervalExpr", expression);
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x00178AA5 File Offset: 0x00176CA5
		internal void AxisMinorInterval(ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinorIntervalExpr", expression);
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x00178AB3 File Offset: 0x00176CB3
		internal void ChartStaticRowLabelsStart()
		{
			this.TypeStart("ChartStaticRowLabels", "IndexedExprHost");
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x00178AC5 File Offset: 0x00176CC5
		internal void ChartStaticRowLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "StaticRowLabelsHost");
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x00178AE4 File Offset: 0x00176CE4
		internal void ChartStaticColumnLabelsStart()
		{
			this.TypeStart("ChartStaticColumnLabels", "IndexedExprHost");
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x00178AF6 File Offset: 0x00176CF6
		internal void ChartStaticColumnLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "StaticColumnLabelsHost");
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x00178B15 File Offset: 0x00176D15
		internal void ChartStaticColumnRowLabel(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x00178B1E File Offset: 0x00176D1E
		internal void ChartStart(string name)
		{
			this.TypeStart(name, "ChartExprHost");
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x00178B2C File Offset: 0x00176D2C
		internal int ChartEnd()
		{
			return this.ReportItemEnd("m_chartHostsRemotable", ref this.m_rootTypeDecl.Charts);
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x00178B44 File Offset: 0x00176D44
		internal void ChartCategoryAxisStart()
		{
			this.AxisStart("CategoryAxis");
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x00178B51 File Offset: 0x00176D51
		internal void ChartCategoryAxisEnd()
		{
			this.AxisEnd("CategoryAxisHost");
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00178B5E File Offset: 0x00176D5E
		internal void ChartValueAxisStart()
		{
			this.AxisStart("ValueAxis");
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x00178B6B File Offset: 0x00176D6B
		internal void ChartValueAxisEnd()
		{
			this.AxisEnd("ValueAxisHost");
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x00178B78 File Offset: 0x00176D78
		internal void ChartLegendStart()
		{
			this.StyleStart("Legend");
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00178B85 File Offset: 0x00176D85
		internal void ChartLegendEnd()
		{
			this.StyleEnd("LegendHost");
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x00178B92 File Offset: 0x00176D92
		internal void ChartPlotAreaStart()
		{
			this.StyleStart("PlotArea");
		}

		// Token: 0x06005BEA RID: 23530 RVA: 0x00178B9F File Offset: 0x00176D9F
		internal void ChartPlotAreaEnd()
		{
			this.StyleEnd("PlotAreaHost");
		}

		// Token: 0x06005BEB RID: 23531 RVA: 0x00178BAC File Offset: 0x00176DAC
		internal void TableGroupStart(string name)
		{
			this.TypeStart("TableGroup_" + name, "TableGroupExprHost");
		}

		// Token: 0x06005BEC RID: 23532 RVA: 0x00178BC4 File Offset: 0x00176DC4
		internal bool TableGroupEnd()
		{
			string baseTypeName = this.m_currentTypeDecl.Parent.BaseTypeName;
			if (baseTypeName == "TableExprHost")
			{
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "TableGroupsHost");
			}
			if (!(baseTypeName == "TableGroupExprHost"))
			{
				Global.Tracer.Assert(false);
				return false;
			}
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "SubGroupHost");
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x00178C38 File Offset: 0x00176E38
		internal void TableRowVisibilityHiddenExpressionsStart()
		{
			this.TypeStart("TableRowVisibilityHiddenExpressionsClass", "IndexedExprHost");
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x00178C4A File Offset: 0x00176E4A
		internal void TableRowVisibilityHiddenExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TableRowVisibilityHiddenExpressions");
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x00178C69 File Offset: 0x00176E69
		internal void TableRowColVisibilityHiddenExpressionsExpr(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x00178C72 File Offset: 0x00176E72
		internal void TableStart(string name)
		{
			this.TypeStart(name, "TableExprHost");
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x00178C80 File Offset: 0x00176E80
		internal int TableEnd()
		{
			return this.ReportItemEnd("m_tableHostsRemotable", ref this.m_rootTypeDecl.Tables);
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x00178C98 File Offset: 0x00176E98
		internal void TableColumnVisibilityHiddenExpressionsStart()
		{
			this.TypeStart("TableColumnVisibilityHiddenExpressions", "IndexedExprHost");
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x00178CAA File Offset: 0x00176EAA
		internal void TableColumnVisibilityHiddenExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TableColumnVisibilityHiddenExpressions");
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x00178CC9 File Offset: 0x00176EC9
		internal void OWCChartStart(string name)
		{
			this.TypeStart(name, "OWCChartExprHost");
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x00178CD7 File Offset: 0x00176ED7
		internal int OWCChartEnd()
		{
			return this.ReportItemEnd("m_OWCChartHostsRemotable", ref this.m_rootTypeDecl.OWCCharts);
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x00178CEF File Offset: 0x00176EEF
		internal void OWCChartColumnsStart()
		{
			this.TypeStart("OWCChartColumns", "IndexedExprHost");
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x00178D01 File Offset: 0x00176F01
		internal void OWCChartColumnsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "OWCChartColumnHosts");
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x00178D20 File Offset: 0x00176F20
		internal void OWCChartColumnsValue(ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x00178D29 File Offset: 0x00176F29
		internal void DataValueStart()
		{
			this.TypeStart(this.CreateTypeName("DataValue", this.m_currentTypeDecl.DataValues), "DataValueExprHost");
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x00178D4C File Offset: 0x00176F4C
		internal int DataValueEnd(bool isCustomProperty)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, isCustomProperty ? "m_customPropertyHostsRemotable" : "m_dataValueHostsRemotable", ref this.m_currentTypeDecl.Parent.DataValues);
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x00178D7E File Offset: 0x00176F7E
		internal void DataValueName(ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueNameExpr", expression);
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x00178D8C File Offset: 0x00176F8C
		internal void DataValueValue(ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueValueExpr", expression);
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x00178D9A File Offset: 0x00176F9A
		internal void CustomReportItemStart(string name)
		{
			this.TypeStart(name, "CustomReportItemExprHost");
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x00178DA8 File Offset: 0x00176FA8
		internal int CustomReportItemEnd()
		{
			return this.ReportItemEnd("m_customReportItemHostsRemotable", ref this.m_rootTypeDecl.CustomReportItems);
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x00178DC0 File Offset: 0x00176FC0
		internal void DataGroupingStart(bool column)
		{
			string text = "DataGrouping" + (column ? "Column" : "Row");
			this.TypeStart(this.CreateTypeName(text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataGroupings), "DataGroupingExprHost");
		}

		// Token: 0x06005C00 RID: 23552 RVA: 0x00178E0C File Offset: 0x0017700C
		internal int DataGroupingEnd(bool column)
		{
			Global.Tracer.Assert("CustomReportItemExprHost" == this.m_currentTypeDecl.Parent.BaseTypeName || "DataGroupingExprHost" == this.m_currentTypeDecl.Parent.BaseTypeName);
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_dataGroupingHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataGroupings);
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x00178E87 File Offset: 0x00177087
		internal void DataCellStart()
		{
			this.TypeStart(this.CreateTypeName("DataCell", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataCells), "DataCellExprHost");
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x00178EAF File Offset: 0x001770AF
		internal int DataCellEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_dataCellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataCells);
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x00178EDC File Offset: 0x001770DC
		private void TypeStart(string typeName, string baseType)
		{
			this.m_currentTypeDecl = new ExprHostBuilder.NonRootTypeDecl(typeName, baseType, this.m_currentTypeDecl, this.m_setCode);
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x00178EF8 File Offset: 0x001770F8
		private int TypeEnd(ExprHostBuilder.TypeDecl container, string name, ref CodeExpressionCollection initializers)
		{
			int num = -1;
			if (this.m_currentTypeDecl.HasExpressions)
			{
				num = container.NestedTypeColAdd(name, this.m_currentTypeDecl.BaseTypeName, ref initializers, this.m_currentTypeDecl.Type);
			}
			this.TypeEnd(container);
			return num;
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x00178F3B File Offset: 0x0017713B
		private bool TypeEnd(ExprHostBuilder.TypeDecl container, string name)
		{
			bool hasExpressions = this.m_currentTypeDecl.HasExpressions;
			if (hasExpressions)
			{
				container.NestedTypeAdd(name, this.m_currentTypeDecl.Type);
			}
			this.TypeEnd(container);
			return hasExpressions;
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x00178F64 File Offset: 0x00177164
		private void TypeEnd(ExprHostBuilder.TypeDecl container)
		{
			Global.Tracer.Assert(this.m_currentTypeDecl.Parent != null && container != null, "(m_currentTypeDecl.Parent != null && container != null)");
			container.HasExpressions |= this.m_currentTypeDecl.HasExpressions;
			this.m_currentTypeDecl = this.m_currentTypeDecl.Parent;
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x00178FBD File Offset: 0x001771BD
		private int ReportItemEnd(string name, ref CodeExpressionCollection initializers)
		{
			return this.TypeEnd(this.m_rootTypeDecl, name, ref initializers);
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x00178FCD File Offset: 0x001771CD
		private void ParameterStart()
		{
			this.TypeStart(this.CreateTypeName("Parameter", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Parameters), "ParamExprHost");
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x00178FF5 File Offset: 0x001771F5
		private int ParameterEnd(string propName)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, propName, ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Parameters);
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x0017901E File Offset: 0x0017721E
		private void StyleStart(string typeName)
		{
			this.TypeStart(typeName, "StyleExprHost");
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x0017902C File Offset: 0x0017722C
		private void StyleEnd(string propName)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, propName);
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00179041 File Offset: 0x00177241
		private void AxisStart(string typeName)
		{
			this.TypeStart(typeName, "AxisExprHost");
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x0017904F File Offset: 0x0017724F
		private void AxisEnd(string propName)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, propName);
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x00179064 File Offset: 0x00177264
		private void AggregateStart()
		{
			this.TypeStart(this.CreateTypeName("Aggregate", this.m_rootTypeDecl.Aggregates), "AggregateParamExprHost");
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x00179087 File Offset: 0x00177287
		private int AggregateEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_aggregateParamHostsRemotable", ref this.m_rootTypeDecl.Aggregates);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x001790A8 File Offset: 0x001772A8
		private string CreateTypeName(string template, CodeExpressionCollection initializers)
		{
			return template + ((initializers == null) ? "0" : initializers.Count.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x001790D8 File Offset: 0x001772D8
		private void ExprIndexerCreate()
		{
			ExprHostBuilder.NonRootTypeDecl nonRootTypeDecl = (ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl;
			if (nonRootTypeDecl.IndexedExpressions != null)
			{
				Global.Tracer.Assert(nonRootTypeDecl.IndexedExpressions.Count > 0, "(currentTypeDecl.IndexedExpressions.Count > 0)");
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = "Item";
				codeMemberProperty.Attributes = (MemberAttributes)24580;
				codeMemberProperty.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
				codeMemberProperty.Type = new CodeTypeReference(typeof(object));
				nonRootTypeDecl.Type.Members.Add(codeMemberProperty);
				int count = nonRootTypeDecl.IndexedExpressions.Count;
				if (count == 1)
				{
					codeMemberProperty.GetStatements.Add(nonRootTypeDecl.IndexedExpressions[0]);
					return;
				}
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeMemberProperty.GetStatements.Add(codeConditionStatement);
				int num = count - 1;
				int num2 = count - 2;
				for (int i = 0; i < num; i++)
				{
					codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("index"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(i));
					codeConditionStatement.TrueStatements.Add(nonRootTypeDecl.IndexedExpressions[i]);
					if (i < num2)
					{
						CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
						codeConditionStatement.FalseStatements.Add(codeConditionStatement2);
						codeConditionStatement = codeConditionStatement2;
					}
				}
				codeConditionStatement.FalseStatements.Add(nonRootTypeDecl.IndexedExpressions[num]);
			}
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x00179248 File Offset: 0x00177448
		private void IndexedExpressionAdd(ExpressionInfo expression)
		{
			if (expression.Type == ExpressionInfo.Types.Expression)
			{
				ExprHostBuilder.NonRootTypeDecl nonRootTypeDecl = (ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl;
				if (nonRootTypeDecl.IndexedExpressions == null)
				{
					nonRootTypeDecl.IndexedExpressions = new ExprHostBuilder.ReturnStatementList();
				}
				nonRootTypeDecl.HasExpressions = true;
				expression.ExprHostID = nonRootTypeDecl.IndexedExpressions.Add(this.CreateExprReturnStatement(expression));
			}
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x0017929C File Offset: 0x0017749C
		private void ExpressionAdd(string name, ExpressionInfo expression)
		{
			if (expression.Type == ExpressionInfo.Types.Expression)
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = name;
				codeMemberProperty.Type = new CodeTypeReference(typeof(object));
				codeMemberProperty.Attributes = (MemberAttributes)24580;
				codeMemberProperty.GetStatements.Add(this.CreateExprReturnStatement(expression));
				this.m_currentTypeDecl.Type.Members.Add(codeMemberProperty);
				this.m_currentTypeDecl.HasExpressions = true;
			}
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x00179314 File Offset: 0x00177514
		private CodeMethodReturnStatement CreateExprReturnStatement(ExpressionInfo expression)
		{
			return new CodeMethodReturnStatement(new CodeSnippetExpression(expression.TransformedExpression))
			{
				LinePragma = new CodeLinePragma("Expr" + expression.CompileTimeID.ToString(CultureInfo.InvariantCulture) + "end", 0)
			};
		}

		// Token: 0x04002F84 RID: 12164
		internal const string RootType = "ReportExprHostImpl";

		// Token: 0x04002F85 RID: 12165
		private ExprHostBuilder.RootTypeDecl m_rootTypeDecl;

		// Token: 0x04002F86 RID: 12166
		private ExprHostBuilder.TypeDecl m_currentTypeDecl;

		// Token: 0x04002F87 RID: 12167
		private bool m_setCode;

		// Token: 0x04002F88 RID: 12168
		private const string EndSrcMarker = "end";

		// Token: 0x04002F89 RID: 12169
		private const string ExprSrcMarker = "Expr";

		// Token: 0x04002F8A RID: 12170
		private static readonly Regex m_findExprNumber = new Regex("^Expr([0-9]+)end", RegexOptions.Compiled);

		// Token: 0x04002F8B RID: 12171
		private const string CustomCodeSrcMarker = "CustomCode";

		// Token: 0x04002F8C RID: 12172
		private const string CodeModuleClassInstanceDeclSrcMarker = "CMCID";

		// Token: 0x04002F8D RID: 12173
		private static readonly Regex m_findCodeModuleClassInstanceDeclNumber = new Regex("^CMCID([0-9]+)end", RegexOptions.Compiled);

		// Token: 0x02000CB2 RID: 3250
		internal enum ErrorSource
		{
			// Token: 0x04004DA5 RID: 19877
			Expression,
			// Token: 0x04004DA6 RID: 19878
			CodeModuleClassInstanceDecl,
			// Token: 0x04004DA7 RID: 19879
			CustomCode,
			// Token: 0x04004DA8 RID: 19880
			Unknown
		}

		// Token: 0x02000CB3 RID: 3251
		private static class Constants
		{
			// Token: 0x04004DA9 RID: 19881
			internal const string ReportObjectModelNS = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel";

			// Token: 0x04004DAA RID: 19882
			internal const string ExprHostObjectModelNS = "Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel";

			// Token: 0x04004DAB RID: 19883
			internal const string ReportExprHost = "ReportExprHost";

			// Token: 0x04004DAC RID: 19884
			internal const string IndexedExprHost = "IndexedExprHost";

			// Token: 0x04004DAD RID: 19885
			internal const string ReportParamExprHost = "ReportParamExprHost";

			// Token: 0x04004DAE RID: 19886
			internal const string CalcFieldExprHost = "CalcFieldExprHost";

			// Token: 0x04004DAF RID: 19887
			internal const string DataSourceExprHost = "DataSourceExprHost";

			// Token: 0x04004DB0 RID: 19888
			internal const string DataSetExprHost = "DataSetExprHost";

			// Token: 0x04004DB1 RID: 19889
			internal const string ReportItemExprHost = "ReportItemExprHost";

			// Token: 0x04004DB2 RID: 19890
			internal const string ActionExprHost = "ActionExprHost";

			// Token: 0x04004DB3 RID: 19891
			internal const string ActionInfoExprHost = "ActionInfoExprHost";

			// Token: 0x04004DB4 RID: 19892
			internal const string TextBoxExprHost = "TextBoxExprHost";

			// Token: 0x04004DB5 RID: 19893
			internal const string ImageExprHost = "ImageExprHost";

			// Token: 0x04004DB6 RID: 19894
			internal const string ParamExprHost = "ParamExprHost";

			// Token: 0x04004DB7 RID: 19895
			internal const string SubreportExprHost = "SubreportExprHost";

			// Token: 0x04004DB8 RID: 19896
			internal const string ActiveXControlExprHost = "ActiveXControlExprHost";

			// Token: 0x04004DB9 RID: 19897
			internal const string SortingExprHost = "SortingExprHost";

			// Token: 0x04004DBA RID: 19898
			internal const string FilterExprHost = "FilterExprHost";

			// Token: 0x04004DBB RID: 19899
			internal const string GroupingExprHost = "GroupingExprHost";

			// Token: 0x04004DBC RID: 19900
			internal const string ListExprHost = "ListExprHost";

			// Token: 0x04004DBD RID: 19901
			internal const string TableGroupExprHost = "TableGroupExprHost";

			// Token: 0x04004DBE RID: 19902
			internal const string TableExprHost = "TableExprHost";

			// Token: 0x04004DBF RID: 19903
			internal const string MatrixDynamicGroupExprHost = "MatrixDynamicGroupExprHost";

			// Token: 0x04004DC0 RID: 19904
			internal const string MatrixExprHost = "MatrixExprHost";

			// Token: 0x04004DC1 RID: 19905
			internal const string ChartExprHost = "ChartExprHost";

			// Token: 0x04004DC2 RID: 19906
			internal const string OWCChartExprHost = "OWCChartExprHost";

			// Token: 0x04004DC3 RID: 19907
			internal const string StyleExprHost = "StyleExprHost";

			// Token: 0x04004DC4 RID: 19908
			internal const string AggregateParamExprHost = "AggregateParamExprHost";

			// Token: 0x04004DC5 RID: 19909
			internal const string MultiChartExprHost = "MultiChartExprHost";

			// Token: 0x04004DC6 RID: 19910
			internal const string ChartDynamicGroupExprHost = "ChartDynamicGroupExprHost";

			// Token: 0x04004DC7 RID: 19911
			internal const string ChartDataPointExprHost = "ChartDataPointExprHost";

			// Token: 0x04004DC8 RID: 19912
			internal const string ChartTitleExprHost = "ChartTitleExprHost";

			// Token: 0x04004DC9 RID: 19913
			internal const string AxisExprHost = "AxisExprHost";

			// Token: 0x04004DCA RID: 19914
			internal const string DataValueExprHost = "DataValueExprHost";

			// Token: 0x04004DCB RID: 19915
			internal const string CustomReportItemExprHost = "CustomReportItemExprHost";

			// Token: 0x04004DCC RID: 19916
			internal const string DataGroupingExprHost = "DataGroupingExprHost";

			// Token: 0x04004DCD RID: 19917
			internal const string DataCellExprHost = "DataCellExprHost";

			// Token: 0x04004DCE RID: 19918
			internal const string ParametersOnlyParam = "parametersOnly";

			// Token: 0x04004DCF RID: 19919
			internal const string CustomCodeProxy = "CustomCodeProxy";

			// Token: 0x04004DD0 RID: 19920
			internal const string CustomCodeProxyBase = "CustomCodeProxyBase";

			// Token: 0x04004DD1 RID: 19921
			internal const string ReportObjectModelParam = "reportObjectModel";

			// Token: 0x04004DD2 RID: 19922
			internal const string SetReportObjectModel = "SetReportObjectModel";

			// Token: 0x04004DD3 RID: 19923
			internal const string Code = "Code";

			// Token: 0x04004DD4 RID: 19924
			internal const string CodeProxyBase = "m_codeProxyBase";

			// Token: 0x04004DD5 RID: 19925
			internal const string CodeParam = "code";

			// Token: 0x04004DD6 RID: 19926
			internal const string Report = "Report";

			// Token: 0x04004DD7 RID: 19927
			internal const string RemoteArrayWrapper = "RemoteArrayWrapper";

			// Token: 0x04004DD8 RID: 19928
			internal const string LabelExpr = "LabelExpr";

			// Token: 0x04004DD9 RID: 19929
			internal const string ValueExpr = "ValueExpr";

			// Token: 0x04004DDA RID: 19930
			internal const string NoRowsExpr = "NoRowsExpr";

			// Token: 0x04004DDB RID: 19931
			internal const string ParameterHosts = "m_parameterHostsRemotable";

			// Token: 0x04004DDC RID: 19932
			internal const string IndexParam = "index";

			// Token: 0x04004DDD RID: 19933
			internal const string FilterHosts = "m_filterHostsRemotable";

			// Token: 0x04004DDE RID: 19934
			internal const string SortingHost = "SortingHost";

			// Token: 0x04004DDF RID: 19935
			internal const string GroupingHost = "GroupingHost";

			// Token: 0x04004DE0 RID: 19936
			internal const string SubgroupHost = "SubgroupHost";

			// Token: 0x04004DE1 RID: 19937
			internal const string VisibilityHiddenExpr = "VisibilityHiddenExpr";

			// Token: 0x04004DE2 RID: 19938
			internal const string SortDirectionHosts = "SortDirectionHosts";

			// Token: 0x04004DE3 RID: 19939
			internal const string DataValueHosts = "m_dataValueHostsRemotable";

			// Token: 0x04004DE4 RID: 19940
			internal const string CustomPropertyHosts = "m_customPropertyHostsRemotable";

			// Token: 0x04004DE5 RID: 19941
			internal const string ReportLanguageExpr = "ReportLanguageExpr";

			// Token: 0x04004DE6 RID: 19942
			internal const string AggregateParamHosts = "m_aggregateParamHostsRemotable";

			// Token: 0x04004DE7 RID: 19943
			internal const string ReportParameterHosts = "m_reportParameterHostsRemotable";

			// Token: 0x04004DE8 RID: 19944
			internal const string DataSourceHosts = "m_dataSourceHostsRemotable";

			// Token: 0x04004DE9 RID: 19945
			internal const string DataSetHosts = "m_dataSetHostsRemotable";

			// Token: 0x04004DEA RID: 19946
			internal const string PageSectionHosts = "m_pageSectionHostsRemotable";

			// Token: 0x04004DEB RID: 19947
			internal const string LineHosts = "m_lineHostsRemotable";

			// Token: 0x04004DEC RID: 19948
			internal const string RectangleHosts = "m_rectangleHostsRemotable";

			// Token: 0x04004DED RID: 19949
			internal const string TextBoxHosts = "m_textBoxHostsRemotable";

			// Token: 0x04004DEE RID: 19950
			internal const string ImageHosts = "m_imageHostsRemotable";

			// Token: 0x04004DEF RID: 19951
			internal const string SubreportHosts = "m_subreportHostsRemotable";

			// Token: 0x04004DF0 RID: 19952
			internal const string ActiveXControlHosts = "m_activeXControlHostsRemotable";

			// Token: 0x04004DF1 RID: 19953
			internal const string ListHosts = "m_listHostsRemotable";

			// Token: 0x04004DF2 RID: 19954
			internal const string TableHosts = "m_tableHostsRemotable";

			// Token: 0x04004DF3 RID: 19955
			internal const string MatrixHosts = "m_matrixHostsRemotable";

			// Token: 0x04004DF4 RID: 19956
			internal const string ChartHosts = "m_chartHostsRemotable";

			// Token: 0x04004DF5 RID: 19957
			internal const string OWCChartHosts = "m_OWCChartHostsRemotable";

			// Token: 0x04004DF6 RID: 19958
			internal const string CustomReportItemHosts = "m_customReportItemHostsRemotable";

			// Token: 0x04004DF7 RID: 19959
			internal const string ConnectStringExpr = "ConnectStringExpr";

			// Token: 0x04004DF8 RID: 19960
			internal const string FieldHosts = "m_fieldHostsRemotable";

			// Token: 0x04004DF9 RID: 19961
			internal const string QueryParametersHost = "QueryParametersHost";

			// Token: 0x04004DFA RID: 19962
			internal const string QueryCommandTextExpr = "QueryCommandTextExpr";

			// Token: 0x04004DFB RID: 19963
			internal const string ValidValuesHost = "ValidValuesHost";

			// Token: 0x04004DFC RID: 19964
			internal const string ValidValueLabelsHost = "ValidValueLabelsHost";

			// Token: 0x04004DFD RID: 19965
			internal const string ValidationExpressionExpr = "ValidationExpressionExpr";

			// Token: 0x04004DFE RID: 19966
			internal const string ActionInfoHost = "ActionInfoHost";

			// Token: 0x04004DFF RID: 19967
			internal const string ActionHost = "ActionHost";

			// Token: 0x04004E00 RID: 19968
			internal const string ActionItemHosts = "m_actionItemHostsRemotable";

			// Token: 0x04004E01 RID: 19969
			internal const string BookmarkExpr = "BookmarkExpr";

			// Token: 0x04004E02 RID: 19970
			internal const string ToolTipExpr = "ToolTipExpr";

			// Token: 0x04004E03 RID: 19971
			internal const string ToggleImageInitialStateExpr = "ToggleImageInitialStateExpr";

			// Token: 0x04004E04 RID: 19972
			internal const string UserSortExpressionsHost = "UserSortExpressionsHost";

			// Token: 0x04004E05 RID: 19973
			internal const string MIMETypeExpr = "MIMETypeExpr";

			// Token: 0x04004E06 RID: 19974
			internal const string OmitExpr = "OmitExpr";

			// Token: 0x04004E07 RID: 19975
			internal const string HyperlinkExpr = "HyperlinkExpr";

			// Token: 0x04004E08 RID: 19976
			internal const string DrillThroughReportNameExpr = "DrillThroughReportNameExpr";

			// Token: 0x04004E09 RID: 19977
			internal const string DrillThroughParameterHosts = "m_drillThroughParameterHostsRemotable";

			// Token: 0x04004E0A RID: 19978
			internal const string DrillThroughBookmakLinkExpr = "DrillThroughBookmarkLinkExpr";

			// Token: 0x04004E0B RID: 19979
			internal const string BookmarkLinkExpr = "BookmarkLinkExpr";

			// Token: 0x04004E0C RID: 19980
			internal const string FilterExpressionExpr = "FilterExpressionExpr";

			// Token: 0x04004E0D RID: 19981
			internal const string ParentExpressionsHost = "ParentExpressionsHost";

			// Token: 0x04004E0E RID: 19982
			internal const string SubGroupHost = "SubGroupHost";

			// Token: 0x04004E0F RID: 19983
			internal const string SubtotalHost = "SubtotalHost";

			// Token: 0x04004E10 RID: 19984
			internal const string RowGroupingsHost = "RowGroupingsHost";

			// Token: 0x04004E11 RID: 19985
			internal const string ColumnGroupingsHost = "ColumnGroupingsHost";

			// Token: 0x04004E12 RID: 19986
			internal const string SeriesGroupingsHost = "SeriesGroupingsHost";

			// Token: 0x04004E13 RID: 19987
			internal const string CategoryGroupingsHost = "CategoryGroupingsHost";

			// Token: 0x04004E14 RID: 19988
			internal const string MultiChartHost = "MultiChartHost";

			// Token: 0x04004E15 RID: 19989
			internal const string HeadingLabelExpr = "HeadingLabelExpr";

			// Token: 0x04004E16 RID: 19990
			internal const string ChartDataPointHosts = "m_chartDataPointHostsRemotable";

			// Token: 0x04004E17 RID: 19991
			internal const string DataLabelValueExpr = "DataLabelValueExpr";

			// Token: 0x04004E18 RID: 19992
			internal const string DataLabelStyleHost = "DataLabelStyleHost";

			// Token: 0x04004E19 RID: 19993
			internal const string StyleHost = "StyleHost";

			// Token: 0x04004E1A RID: 19994
			internal const string MarkerStyleHost = "MarkerStyleHost";

			// Token: 0x04004E1B RID: 19995
			internal const string TitleHost = "TitleHost";

			// Token: 0x04004E1C RID: 19996
			internal const string CaptionExpr = "CaptionExpr";

			// Token: 0x04004E1D RID: 19997
			internal const string MajorGridLinesHost = "MajorGridLinesHost";

			// Token: 0x04004E1E RID: 19998
			internal const string MinorGridLinesHost = "MinorGridLinesHost";

			// Token: 0x04004E1F RID: 19999
			internal const string StaticRowLabelsHost = "StaticRowLabelsHost";

			// Token: 0x04004E20 RID: 20000
			internal const string StaticColumnLabelsHost = "StaticColumnLabelsHost";

			// Token: 0x04004E21 RID: 20001
			internal const string CategoryAxisHost = "CategoryAxisHost";

			// Token: 0x04004E22 RID: 20002
			internal const string ValueAxisHost = "ValueAxisHost";

			// Token: 0x04004E23 RID: 20003
			internal const string LegendHost = "LegendHost";

			// Token: 0x04004E24 RID: 20004
			internal const string PlotAreaHost = "PlotAreaHost";

			// Token: 0x04004E25 RID: 20005
			internal const string AxisMinExpr = "AxisMinExpr";

			// Token: 0x04004E26 RID: 20006
			internal const string AxisMaxExpr = "AxisMaxExpr";

			// Token: 0x04004E27 RID: 20007
			internal const string AxisCrossAtExpr = "AxisCrossAtExpr";

			// Token: 0x04004E28 RID: 20008
			internal const string AxisMajorIntervalExpr = "AxisMajorIntervalExpr";

			// Token: 0x04004E29 RID: 20009
			internal const string AxisMinorIntervalExpr = "AxisMinorIntervalExpr";

			// Token: 0x04004E2A RID: 20010
			internal const string TableGroupsHost = "TableGroupsHost";

			// Token: 0x04004E2B RID: 20011
			internal const string TableRowVisibilityHiddenExpressions = "TableRowVisibilityHiddenExpressions";

			// Token: 0x04004E2C RID: 20012
			internal const string TableColumnVisibilityHiddenExpressions = "TableColumnVisibilityHiddenExpressions";

			// Token: 0x04004E2D RID: 20013
			internal const string OWCChartColumnHosts = "OWCChartColumnHosts";

			// Token: 0x04004E2E RID: 20014
			internal const string DataValueNameExpr = "DataValueNameExpr";

			// Token: 0x04004E2F RID: 20015
			internal const string DataValueValueExpr = "DataValueValueExpr";

			// Token: 0x04004E30 RID: 20016
			internal const string DataGroupingHosts = "m_dataGroupingHostsRemotable";

			// Token: 0x04004E31 RID: 20017
			internal const string DataCellHosts = "m_dataCellHostsRemotable";
		}

		// Token: 0x02000CB4 RID: 3252
		private abstract class TypeDecl
		{
			// Token: 0x06008CCB RID: 36043 RVA: 0x0023CBC8 File Offset: 0x0023ADC8
			internal void NestedTypeAdd(string name, CodeTypeDeclaration nestedType)
			{
				this.ConstructorCreate();
				this.Type.Members.Add(nestedType);
				this.Constructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), this.CreateTypeCreateExpression(nestedType.Name)));
			}

			// Token: 0x06008CCC RID: 36044 RVA: 0x0023CC1A File Offset: 0x0023AE1A
			internal int NestedTypeColAdd(string name, string baseTypeName, ref CodeExpressionCollection initializers, CodeTypeDeclaration nestedType)
			{
				this.Type.Members.Add(nestedType);
				this.TypeColInit(name, baseTypeName, ref initializers);
				return initializers.Add(this.CreateTypeCreateExpression(nestedType.Name));
			}

			// Token: 0x06008CCD RID: 36045 RVA: 0x0023CC4C File Offset: 0x0023AE4C
			protected TypeDecl(string typeName, string baseTypeName, ExprHostBuilder.TypeDecl parent, bool setCode)
			{
				this.BaseTypeName = baseTypeName;
				this.Parent = parent;
				this.m_setCode = setCode;
				this.Type = this.CreateType(typeName, baseTypeName);
			}

			// Token: 0x06008CCE RID: 36046 RVA: 0x0023CC78 File Offset: 0x0023AE78
			protected void ConstructorCreate()
			{
				if (this.Constructor == null)
				{
					this.Constructor = this.CreateConstructor();
					this.Type.Members.Add(this.Constructor);
				}
			}

			// Token: 0x06008CCF RID: 36047 RVA: 0x0023CCA5 File Offset: 0x0023AEA5
			protected virtual CodeConstructor CreateConstructor()
			{
				return new CodeConstructor
				{
					Attributes = MemberAttributes.Public
				};
			}

			// Token: 0x06008CD0 RID: 36048 RVA: 0x0023CCB8 File Offset: 0x0023AEB8
			protected CodeAssignStatement CreateTypeColInitStatement(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
				codeObjectCreateExpression.CreateType = new CodeTypeReference("RemoteArrayWrapper", new CodeTypeReference[]
				{
					new CodeTypeReference(baseTypeName)
				});
				if (initializers != null)
				{
					codeObjectCreateExpression.Parameters.AddRange(initializers);
				}
				initializers = codeObjectCreateExpression.Parameters;
				return new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), codeObjectCreateExpression);
			}

			// Token: 0x06008CD1 RID: 36049 RVA: 0x0023CD14 File Offset: 0x0023AF14
			protected virtual CodeTypeDeclaration CreateType(string name, string baseType)
			{
				Global.Tracer.Assert(name != null);
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
				if (baseType != null)
				{
					codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(baseType));
				}
				codeTypeDeclaration.Attributes = (MemberAttributes)24578;
				return codeTypeDeclaration;
			}

			// Token: 0x06008CD2 RID: 36050 RVA: 0x0023CD57 File Offset: 0x0023AF57
			private void TypeColInit(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				this.ConstructorCreate();
				if (initializers == null)
				{
					this.Constructor.Statements.Add(this.CreateTypeColInitStatement(name, baseTypeName, ref initializers));
				}
			}

			// Token: 0x06008CD3 RID: 36051 RVA: 0x0023CD7D File Offset: 0x0023AF7D
			private CodeObjectCreateExpression CreateTypeCreateExpression(string typeName)
			{
				if (this.m_setCode)
				{
					return new CodeObjectCreateExpression(typeName, new CodeExpression[]
					{
						new CodeArgumentReferenceExpression("Code")
					});
				}
				return new CodeObjectCreateExpression(typeName, Array.Empty<CodeExpression>());
			}

			// Token: 0x04004E32 RID: 20018
			internal CodeTypeDeclaration Type;

			// Token: 0x04004E33 RID: 20019
			internal string BaseTypeName;

			// Token: 0x04004E34 RID: 20020
			internal ExprHostBuilder.TypeDecl Parent;

			// Token: 0x04004E35 RID: 20021
			internal CodeConstructor Constructor;

			// Token: 0x04004E36 RID: 20022
			internal bool HasExpressions;

			// Token: 0x04004E37 RID: 20023
			internal CodeExpressionCollection DataValues;

			// Token: 0x04004E38 RID: 20024
			protected bool m_setCode;
		}

		// Token: 0x02000CB5 RID: 3253
		private sealed class RootTypeDecl : ExprHostBuilder.TypeDecl
		{
			// Token: 0x06008CD4 RID: 36052 RVA: 0x0023CDAC File Offset: 0x0023AFAC
			internal RootTypeDecl(bool setCode)
				: base("ReportExprHostImpl", "ReportExprHost", null, setCode)
			{
			}

			// Token: 0x06008CD5 RID: 36053 RVA: 0x0023CDC0 File Offset: 0x0023AFC0
			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "parametersOnly"));
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(typeof(object), "reportObjectModel");
				codeConstructor.Parameters.Add(codeParameterDeclarationExpression);
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				this.ReportParameters = new CodeExpressionCollection();
				this.DataSources = new CodeExpressionCollection();
				this.DataSets = new CodeExpressionCollection();
				return codeConstructor;
			}

			// Token: 0x06008CD6 RID: 36054 RVA: 0x0023CE4C File Offset: 0x0023B04C
			protected override CodeTypeDeclaration CreateType(string name, string baseType)
			{
				CodeTypeDeclaration codeTypeDeclaration = base.CreateType(name, baseType);
				if (this.m_setCode)
				{
					CodeMemberField codeMemberField = new CodeMemberField("CustomCodeProxy", "Code");
					codeMemberField.Attributes = (MemberAttributes)20482;
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				return codeTypeDeclaration;
			}

			// Token: 0x06008CD7 RID: 36055 RVA: 0x0023CE94 File Offset: 0x0023B094
			internal void CompleteConstructorCreation()
			{
				if (this.HasExpressions)
				{
					if (this.Constructor == null)
					{
						base.ConstructorCreate();
						return;
					}
					CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
					codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("parametersOnly"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
					if (this.ReportParameters.Count > 0)
					{
						codeConditionStatement.TrueStatements.Add(base.CreateTypeColInitStatement("m_reportParameterHostsRemotable", "ReportParamExprHost", ref this.ReportParameters));
					}
					codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement());
					this.Constructor.Statements.Insert(0, codeConditionStatement);
					if (this.DataSources.Count > 0)
					{
						this.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSourceHostsRemotable", "DataSourceExprHost", ref this.DataSources));
					}
					if (this.DataSets.Count > 0)
					{
						this.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSetHostsRemotable", "DataSetExprHost", ref this.DataSets));
					}
					if (this.m_setCode)
					{
						this.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "m_codeProxyBase"), new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code")));
						this.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeObjectCreateExpression("CustomCodeProxy", new CodeExpression[]
						{
							new CodeThisReferenceExpression()
						})));
					}
				}
			}

			// Token: 0x04004E39 RID: 20025
			internal CodeExpressionCollection Aggregates;

			// Token: 0x04004E3A RID: 20026
			internal CodeExpressionCollection PageSections;

			// Token: 0x04004E3B RID: 20027
			internal CodeExpressionCollection ReportParameters;

			// Token: 0x04004E3C RID: 20028
			internal CodeExpressionCollection DataSources;

			// Token: 0x04004E3D RID: 20029
			internal CodeExpressionCollection DataSets;

			// Token: 0x04004E3E RID: 20030
			internal CodeExpressionCollection Lines;

			// Token: 0x04004E3F RID: 20031
			internal CodeExpressionCollection Rectangles;

			// Token: 0x04004E40 RID: 20032
			internal CodeExpressionCollection TextBoxes;

			// Token: 0x04004E41 RID: 20033
			internal CodeExpressionCollection Images;

			// Token: 0x04004E42 RID: 20034
			internal CodeExpressionCollection Subreports;

			// Token: 0x04004E43 RID: 20035
			internal CodeExpressionCollection ActiveXControls;

			// Token: 0x04004E44 RID: 20036
			internal CodeExpressionCollection Lists;

			// Token: 0x04004E45 RID: 20037
			internal CodeExpressionCollection Tables;

			// Token: 0x04004E46 RID: 20038
			internal CodeExpressionCollection Matrices;

			// Token: 0x04004E47 RID: 20039
			internal CodeExpressionCollection Charts;

			// Token: 0x04004E48 RID: 20040
			internal CodeExpressionCollection OWCCharts;

			// Token: 0x04004E49 RID: 20041
			internal CodeExpressionCollection CustomReportItems;
		}

		// Token: 0x02000CB6 RID: 3254
		private sealed class NonRootTypeDecl : ExprHostBuilder.TypeDecl
		{
			// Token: 0x06008CD8 RID: 36056 RVA: 0x0023D018 File Offset: 0x0023B218
			internal NonRootTypeDecl(string typeName, string baseTypeName, ExprHostBuilder.TypeDecl parent, bool setCode)
				: base(typeName, baseTypeName, parent, setCode)
			{
				if (setCode)
				{
					base.ConstructorCreate();
				}
			}

			// Token: 0x06008CD9 RID: 36057 RVA: 0x0023D030 File Offset: 0x0023B230
			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				if (this.m_setCode)
				{
					codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression("CustomCodeProxy", "code"));
					codeConstructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeArgumentReferenceExpression("code")));
				}
				return codeConstructor;
			}

			// Token: 0x06008CDA RID: 36058 RVA: 0x0023D094 File Offset: 0x0023B294
			protected override CodeTypeDeclaration CreateType(string name, string baseType)
			{
				CodeTypeDeclaration codeTypeDeclaration = base.CreateType(string.Format(CultureInfo.InvariantCulture, "{0}_{1}", name, baseType), baseType);
				if (this.m_setCode)
				{
					CodeMemberField codeMemberField = new CodeMemberField("CustomCodeProxy", "Code");
					codeMemberField.Attributes = (MemberAttributes)20482;
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				return codeTypeDeclaration;
			}

			// Token: 0x04004E4A RID: 20042
			internal CodeExpressionCollection Parameters;

			// Token: 0x04004E4B RID: 20043
			internal CodeExpressionCollection Filters;

			// Token: 0x04004E4C RID: 20044
			internal CodeExpressionCollection Actions;

			// Token: 0x04004E4D RID: 20045
			internal CodeExpressionCollection Fields;

			// Token: 0x04004E4E RID: 20046
			internal CodeExpressionCollection DataPoints;

			// Token: 0x04004E4F RID: 20047
			internal CodeExpressionCollection DataGroupings;

			// Token: 0x04004E50 RID: 20048
			internal CodeExpressionCollection DataCells;

			// Token: 0x04004E51 RID: 20049
			internal ExprHostBuilder.ReturnStatementList IndexedExpressions;
		}

		// Token: 0x02000CB7 RID: 3255
		private sealed class CustomCodeProxyDecl : ExprHostBuilder.TypeDecl
		{
			// Token: 0x06008CDB RID: 36059 RVA: 0x0023D0EB File Offset: 0x0023B2EB
			internal CustomCodeProxyDecl(ExprHostBuilder.TypeDecl parent)
				: base("CustomCodeProxy", "CustomCodeProxyBase", parent, false)
			{
				base.ConstructorCreate();
			}

			// Token: 0x06008CDC RID: 36060 RVA: 0x0023D105 File Offset: 0x0023B305
			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IReportObjectModelProxyForCustomCode), "reportObjectModel"));
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				return codeConstructor;
			}

			// Token: 0x06008CDD RID: 36061 RVA: 0x0023D144 File Offset: 0x0023B344
			internal void AddClassInstance(string className, string instanceName, int id)
			{
				string text = "CMCID" + id.ToString(CultureInfo.InvariantCulture) + "end";
				CodeMemberField codeMemberField = new CodeMemberField(className, "m_" + instanceName);
				codeMemberField.Attributes = (MemberAttributes)20482;
				codeMemberField.InitExpression = new CodeObjectCreateExpression(className, Array.Empty<CodeExpression>());
				codeMemberField.LinePragma = new CodeLinePragma(text, 0);
				this.Type.Members.Add(codeMemberField);
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Type = new CodeTypeReference(className);
				codeMemberProperty.Name = instanceName;
				codeMemberProperty.Attributes = (MemberAttributes)24578;
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), codeMemberField.Name)));
				codeMemberProperty.LinePragma = new CodeLinePragma(text, 2);
				this.Type.Members.Add(codeMemberProperty);
			}

			// Token: 0x06008CDE RID: 36062 RVA: 0x0023D220 File Offset: 0x0023B420
			internal void AddCode(string code)
			{
				CodeTypeMember codeTypeMember = new CodeSnippetTypeMember(code);
				codeTypeMember.LinePragma = new CodeLinePragma("CustomCode", 0);
				this.Type.Members.Add(codeTypeMember);
			}
		}

		// Token: 0x02000CB8 RID: 3256
		private sealed class ReturnStatementList
		{
			// Token: 0x06008CDF RID: 36063 RVA: 0x0023D257 File Offset: 0x0023B457
			internal int Add(CodeMethodReturnStatement retStatement)
			{
				return this.m_list.Add(retStatement);
			}

			// Token: 0x17002B49 RID: 11081
			internal CodeMethodReturnStatement this[int index]
			{
				get
				{
					return (CodeMethodReturnStatement)this.m_list[index];
				}
			}

			// Token: 0x17002B4A RID: 11082
			// (get) Token: 0x06008CE1 RID: 36065 RVA: 0x0023D278 File Offset: 0x0023B478
			internal int Count
			{
				get
				{
					return this.m_list.Count;
				}
			}

			// Token: 0x04004E52 RID: 20050
			private ArrayList m_list = new ArrayList();
		}
	}
}
