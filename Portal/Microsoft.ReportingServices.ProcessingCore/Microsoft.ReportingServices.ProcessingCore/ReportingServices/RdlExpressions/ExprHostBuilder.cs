using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000562 RID: 1378
	public sealed class ExprHostBuilder
	{
		// Token: 0x06004A39 RID: 19001 RVA: 0x00139795 File Offset: 0x00137995
		internal ExprHostBuilder()
		{
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06004A3A RID: 19002 RVA: 0x0013979D File Offset: 0x0013799D
		internal bool HasExpressions
		{
			get
			{
				return this.m_rootTypeDecl != null && this.m_rootTypeDecl.HasExpressions;
			}
		}

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06004A3B RID: 19003 RVA: 0x001397B4 File Offset: 0x001379B4
		internal bool CustomCode
		{
			get
			{
				return this.m_setCode;
			}
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x001397BC File Offset: 0x001379BC
		internal void SetCustomCode()
		{
			this.m_setCode = true;
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x001397C8 File Offset: 0x001379C8
		internal CodeCompileUnit GetExprHost(ProcessingIntermediateFormatVersion version, bool refusePermissions)
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
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.SqlServer.Types"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.ReportingServices.ReportProcessing.ReportObjectModel"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel"));
				codeNamespace.Types.Add(this.m_rootTypeDecl.Type);
			}
			this.m_rootTypeDecl = null;
			return codeCompileUnit;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x001399B4 File Offset: 0x00137BB4
		internal static ExprHostBuilder.ErrorSource ParseErrorSource(CompilerError error, out int id)
		{
			Global.Tracer.Assert(error.FileName != null, "(error.FileName != null)");
			id = -1;
			if (error.FileName.StartsWith("CustomCode", StringComparison.Ordinal))
			{
				return ExprHostBuilder.ErrorSource.CustomCode;
			}
			try
			{
				Match match = ExprHostBuilder.m_findCodeModuleClassInstanceDeclNumber.Match(error.FileName);
				if (match.Success)
				{
					id = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
					return ExprHostBuilder.ErrorSource.CodeModuleClassInstanceDecl;
				}
				match = ExprHostBuilder.m_findExprNumber.Match(error.FileName);
				if (match.Success)
				{
					id = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
					return ExprHostBuilder.ErrorSource.Expression;
				}
			}
			catch (FormatException)
			{
			}
			return ExprHostBuilder.ErrorSource.Unknown;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00139A80 File Offset: 0x00137C80
		internal void SharedDataSetStart()
		{
			this.m_currentTypeDecl = (this.m_rootTypeDecl = new ExprHostBuilder.RootTypeDecl(this.m_setCode));
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x00139AA7 File Offset: 0x00137CA7
		internal void SharedDataSetEnd()
		{
			this.m_rootTypeDecl.CompleteConstructorCreation();
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x00139AB4 File Offset: 0x00137CB4
		internal void ReportStart()
		{
			this.m_currentTypeDecl = (this.m_rootTypeDecl = new ExprHostBuilder.RootTypeDecl(this.m_setCode));
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x00139ADB File Offset: 0x00137CDB
		internal void ReportEnd()
		{
			this.m_rootTypeDecl.CompleteConstructorCreation();
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x00139AE8 File Offset: 0x00137CE8
		internal void ReportLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReportLanguageExpr", expression);
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00139AF6 File Offset: 0x00137CF6
		internal void ReportAutoRefresh(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoRefreshExpr", expression);
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00139B04 File Offset: 0x00137D04
		internal void ReportInitialPageName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InitialPageNameExpr", expression);
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00139B12 File Offset: 0x00137D12
		internal void GenericLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00139B20 File Offset: 0x00137D20
		internal void GenericValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00139B2E File Offset: 0x00137D2E
		internal void GenericNoRows(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NoRowsExpr", expression);
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00139B3C File Offset: 0x00137D3C
		internal void GenericVisibilityHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibilityHiddenExpr", expression);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x00139B4A File Offset: 0x00137D4A
		internal void AggregateParamExprAdd(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.AggregateStart();
			this.GenericValue(expression);
			expression.ExprHostID = this.AggregateEnd();
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00139B65 File Offset: 0x00137D65
		internal void CustomCodeProxyStart()
		{
			Global.Tracer.Assert(this.m_setCode, "(m_setCode)");
			this.m_currentTypeDecl = new ExprHostBuilder.CustomCodeProxyDecl(this.m_currentTypeDecl);
			this.m_currentTypeDecl.HasExpressions = true;
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x00139B99 File Offset: 0x00137D99
		internal void CustomCodeProxyEnd()
		{
			this.m_rootTypeDecl.Type.Members.Add(this.m_currentTypeDecl.Type);
			this.TypeEnd(this.m_rootTypeDecl);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00139BC8 File Offset: 0x00137DC8
		internal void CustomCodeClassInstance(string className, string instanceName, int id)
		{
			((ExprHostBuilder.CustomCodeProxyDecl)this.m_currentTypeDecl).AddClassInstance(className, instanceName, id);
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x00139BDD File Offset: 0x00137DDD
		internal void ReportCode(string code)
		{
			((ExprHostBuilder.CustomCodeProxyDecl)this.m_currentTypeDecl).AddCode(code);
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x00139BF0 File Offset: 0x00137DF0
		internal void ReportParameterStart(string name)
		{
			this.TypeStart(name, "ReportParamExprHost");
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x00139BFE File Offset: 0x00137DFE
		internal int ReportParameterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_rootTypeDecl, "m_reportParameterHostsRemotable", ref this.m_rootTypeDecl.ReportParameters);
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x00139C22 File Offset: 0x00137E22
		internal void ReportParameterValidationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValidationExpressionExpr", expression);
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x00139C30 File Offset: 0x00137E30
		internal void ReportParameterPromptExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PromptExpr", expression);
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x00139C3E File Offset: 0x00137E3E
		internal void ReportParameterDefaultValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00139C47 File Offset: 0x00137E47
		internal void ReportParameterValidValuesStart()
		{
			this.TypeStart("ReportParameterValidValues", "IndexedExprHost");
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x00139C59 File Offset: 0x00137E59
		internal void ReportParameterValidValuesEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValuesHost");
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00139C78 File Offset: 0x00137E78
		internal void ReportParameterValidValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00139C81 File Offset: 0x00137E81
		internal void ReportParameterValidValueLabelsStart()
		{
			this.TypeStart("ReportParameterValidValueLabels", "IndexedExprHost");
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00139C93 File Offset: 0x00137E93
		internal void ReportParameterValidValueLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValueLabelsHost");
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00139CB2 File Offset: 0x00137EB2
		internal void ReportParameterValidValueLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00139CBB File Offset: 0x00137EBB
		internal void CalcFieldStart(string name)
		{
			this.TypeStart(name, "CalcFieldExprHost");
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x00139CC9 File Offset: 0x00137EC9
		internal int CalcFieldEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_fieldHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Fields);
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x00139CF6 File Offset: 0x00137EF6
		internal void QueryParametersStart()
		{
			this.TypeStart("QueryParameters", "IndexedExprHost");
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00139D08 File Offset: 0x00137F08
		internal void QueryParametersEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "QueryParametersHost");
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00139D27 File Offset: 0x00137F27
		internal void QueryParameterValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00139D30 File Offset: 0x00137F30
		internal void DataSourceStart(string name)
		{
			this.TypeStart(name, "DataSourceExprHost");
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00139D3E File Offset: 0x00137F3E
		internal int DataSourceEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_dataSourceHostsRemotable", ref this.m_rootTypeDecl.DataSources);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x00139D5C File Offset: 0x00137F5C
		internal void DataSourceConnectString(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ConnectStringExpr", expression);
		}

		// Token: 0x06004A62 RID: 19042 RVA: 0x00139D6A File Offset: 0x00137F6A
		internal void DataSetStart(string name)
		{
			this.TypeStart(name, "DataSetExprHost");
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x00139D78 File Offset: 0x00137F78
		internal int DataSetEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_dataSetHostsRemotable", ref this.m_rootTypeDecl.DataSets);
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x00139D96 File Offset: 0x00137F96
		internal void DataSetQueryCommandText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("QueryCommandTextExpr", expression);
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x00139DA4 File Offset: 0x00137FA4
		internal void PageSectionStart()
		{
			this.TypeStart(this.CreateTypeName("PageSection", this.m_rootTypeDecl.PageSections), "StyleExprHost");
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x00139DC7 File Offset: 0x00137FC7
		internal int PageSectionEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_pageSectionHostsRemotable", ref this.m_rootTypeDecl.PageSections);
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x00139DE5 File Offset: 0x00137FE5
		internal void PageStart()
		{
			this.TypeStart(this.CreateTypeName("Page", this.m_rootTypeDecl.Pages), "StyleExprHost");
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00139E08 File Offset: 0x00138008
		internal int PageEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_pageHostsRemotable", ref this.m_rootTypeDecl.Pages);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00139E26 File Offset: 0x00138026
		internal void ReportSectionStart()
		{
			this.TypeStart(this.CreateTypeName("ReportSection", this.m_rootTypeDecl.ReportSections), "ReportSectionExprHost");
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x00139E49 File Offset: 0x00138049
		internal int ReportSectionEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_reportSectionHostsRemotable", ref this.m_rootTypeDecl.ReportSections);
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x00139E67 File Offset: 0x00138067
		internal void ParameterOmit(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OmitExpr", expression);
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x00139E75 File Offset: 0x00138075
		internal void StyleAttribute(string name, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd(name + "Expr", expression);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00139E89 File Offset: 0x00138089
		internal void ActionInfoStart()
		{
			this.TypeStart("ActionInfo", "ActionInfoExprHost");
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x00139E9B File Offset: 0x0013809B
		internal void ActionInfoEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ActionInfoHost");
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00139EB4 File Offset: 0x001380B4
		internal void ActionStart()
		{
			this.TypeStart(this.CreateTypeName("Action", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Actions), "ActionExprHost");
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x00139EDC File Offset: 0x001380DC
		internal int ActionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_actionItemHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Actions);
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00139F09 File Offset: 0x00138109
		internal void ActionHyperlink(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HyperlinkExpr", expression);
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00139F17 File Offset: 0x00138117
		internal void ActionDrillThroughReportName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughReportNameExpr", expression);
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00139F25 File Offset: 0x00138125
		internal void ActionDrillThroughBookmarkLink(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughBookmarkLinkExpr", expression);
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00139F33 File Offset: 0x00138133
		internal void ActionBookmarkLink(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkLinkExpr", expression);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00139F41 File Offset: 0x00138141
		internal void ActionDrillThroughParameterStart()
		{
			this.ParameterStart();
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00139F49 File Offset: 0x00138149
		internal int ActionDrillThroughParameterEnd()
		{
			return this.ParameterEnd("m_drillThroughParameterHostsRemotable");
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x00139F56 File Offset: 0x00138156
		internal void ReportItemBookmark(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkExpr", expression);
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x00139F64 File Offset: 0x00138164
		internal void ReportItemToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00139F72 File Offset: 0x00138172
		internal void LineStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00139F80 File Offset: 0x00138180
		internal int LineEnd()
		{
			return this.ReportItemEnd("m_lineHostsRemotable", ref this.m_rootTypeDecl.Lines);
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00139F98 File Offset: 0x00138198
		internal void RectangleStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00139FA6 File Offset: 0x001381A6
		internal int RectangleEnd()
		{
			return this.ReportItemEnd("m_rectangleHostsRemotable", ref this.m_rootTypeDecl.Rectangles);
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00139FBE File Offset: 0x001381BE
		internal void TextBoxStart(string name)
		{
			this.TypeStart(name, "TextBoxExprHost");
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00139FCC File Offset: 0x001381CC
		internal int TextBoxEnd()
		{
			return this.ReportItemEnd("m_textBoxHostsRemotable", ref this.m_rootTypeDecl.TextBoxes);
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00139FE4 File Offset: 0x001381E4
		internal void TextBoxToggleImageInitialState(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToggleImageInitialStateExpr", expression);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00139FF2 File Offset: 0x001381F2
		internal void UserSortExpressionsStart()
		{
			this.TypeStart("UserSort", "IndexedExprHost");
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x0013A004 File Offset: 0x00138204
		internal void UserSortExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "UserSortExpressionsHost");
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x0013A023 File Offset: 0x00138223
		internal void UserSortExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x0013A02C File Offset: 0x0013822C
		internal void ImageStart(string name)
		{
			this.TypeStart(name, "ImageExprHost");
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0013A03A File Offset: 0x0013823A
		internal int ImageEnd()
		{
			return this.ReportItemEnd("m_imageHostsRemotable", ref this.m_rootTypeDecl.Images);
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x0013A052 File Offset: 0x00138252
		internal void ImageMIMEType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x0013A060 File Offset: 0x00138260
		internal void ImageTag(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TagExpr", expression);
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x0013A06E File Offset: 0x0013826E
		internal void SubreportStart(string name)
		{
			this.TypeStart(name, "SubreportExprHost");
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x0013A07C File Offset: 0x0013827C
		internal int SubreportEnd()
		{
			return this.ReportItemEnd("m_subreportHostsRemotable", ref this.m_rootTypeDecl.Subreports);
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0013A094 File Offset: 0x00138294
		internal void SubreportParameterStart()
		{
			this.ParameterStart();
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x0013A09C File Offset: 0x0013829C
		internal int SubreportParameterEnd()
		{
			return this.ParameterEnd("m_parameterHostsRemotable");
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x0013A0A9 File Offset: 0x001382A9
		internal void SortStart()
		{
			this.TypeStart("Sort", "SortExprHost");
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x0013A0BB File Offset: 0x001382BB
		internal void SortEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "m_sortHost");
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x0013A0DA File Offset: 0x001382DA
		internal void SortExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x0013A0E3 File Offset: 0x001382E3
		internal void SortDirectionsStart()
		{
			this.TypeStart("SortDirections", "IndexedExprHost");
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x0013A0F5 File Offset: 0x001382F5
		internal void SortDirectionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SortDirectionHosts");
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x0013A114 File Offset: 0x00138314
		internal void SortDirection(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x0013A11D File Offset: 0x0013831D
		internal void FilterStart()
		{
			this.TypeStart(this.CreateTypeName("Filter", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Filters), "FilterExprHost");
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x0013A145 File Offset: 0x00138345
		internal int FilterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_filterHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Filters);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x0013A178 File Offset: 0x00138378
		internal void FilterExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FilterExpressionExpr", expression);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x0013A186 File Offset: 0x00138386
		internal void FilterValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x0013A18F File Offset: 0x0013838F
		internal void GroupStart(string typeName)
		{
			this.TypeStart(typeName, "GroupExprHost");
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x0013A19D File Offset: 0x0013839D
		internal void GroupEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "m_groupHost");
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0013A1BC File Offset: 0x001383BC
		internal void GroupExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x0013A1C5 File Offset: 0x001383C5
		internal void GroupParentExpressionsStart()
		{
			this.TypeStart("Parent", "IndexedExprHost");
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x0013A1D7 File Offset: 0x001383D7
		internal void GroupParentExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ParentExpressionsHost");
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0013A1F6 File Offset: 0x001383F6
		internal void GroupParentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x0013A1FF File Offset: 0x001383FF
		internal void ReGroupExpressionsStart()
		{
			this.TypeStart("ReGroup", "IndexedExprHost");
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0013A211 File Offset: 0x00138411
		internal void ReGroupExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ReGroupExpressionsHost");
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0013A230 File Offset: 0x00138430
		internal void ReGroupExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0013A239 File Offset: 0x00138439
		internal void VariableValuesStart()
		{
			this.TypeStart("Variables", "IndexedExprHost");
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0013A24B File Offset: 0x0013844B
		internal void VariableValuesEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "VariableValueHosts");
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0013A26A File Offset: 0x0013846A
		internal void VariableValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x0013A274 File Offset: 0x00138474
		internal void DataRegionStart(ExprHostBuilder.DataRegionMode mode, string dataregionName)
		{
			switch (mode)
			{
			case ExprHostBuilder.DataRegionMode.Tablix:
				this.TypeStart(dataregionName, "TablixExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.Chart:
				this.TypeStart(dataregionName, "ChartExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.GaugePanel:
				this.TypeStart(dataregionName, "GaugePanelExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.CustomReportItem:
				this.TypeStart(dataregionName, "CustomReportItemExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.MapDataRegion:
				this.TypeStart(dataregionName, "MapDataRegionExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.DataShape:
				this.TypeStart(dataregionName, "DataShapeExprHost");
				return;
			default:
				return;
			}
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0013A2F0 File Offset: 0x001384F0
		internal int DataRegionEnd(ExprHostBuilder.DataRegionMode mode)
		{
			int num = -1;
			switch (mode)
			{
			case ExprHostBuilder.DataRegionMode.Tablix:
				num = this.ReportItemEnd("m_tablixHostsRemotable", ref this.m_rootTypeDecl.Tablices);
				break;
			case ExprHostBuilder.DataRegionMode.Chart:
				num = this.ReportItemEnd("m_chartHostsRemotable", ref this.m_rootTypeDecl.Charts);
				break;
			case ExprHostBuilder.DataRegionMode.GaugePanel:
				num = this.ReportItemEnd("m_gaugePanelHostsRemotable", ref this.m_rootTypeDecl.GaugePanels);
				break;
			case ExprHostBuilder.DataRegionMode.CustomReportItem:
				num = this.ReportItemEnd("m_customReportItemHostsRemotable", ref this.m_rootTypeDecl.CustomReportItems);
				break;
			case ExprHostBuilder.DataRegionMode.MapDataRegion:
				num = this.ReportItemEnd("m_mapDataRegionHostsRemotable", ref this.m_rootTypeDecl.CustomReportItems);
				break;
			case ExprHostBuilder.DataRegionMode.DataShape:
				num = this.ReportItemEnd("DataShapeExprHost", ref this.m_rootTypeDecl.DataShapes);
				break;
			}
			return num;
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0013A3B8 File Offset: 0x001385B8
		internal void DataGroupStart(ExprHostBuilder.DataRegionMode mode, bool column)
		{
			string text = (column ? "Column" : "Row");
			switch (mode)
			{
			case ExprHostBuilder.DataRegionMode.Tablix:
				this.TypeStart(this.CreateTypeName("TablixMember" + text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).TablixMembers), "TablixMemberExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.Chart:
				this.TypeStart(this.CreateTypeName("ChartMember" + text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartMembers), "ChartMemberExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.GaugePanel:
				this.TypeStart(this.CreateTypeName("GaugeMember" + text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).GaugeMembers), "GaugeMemberExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.CustomReportItem:
				this.TypeStart(this.CreateTypeName("DataGroup" + text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataGroups), "DataGroupExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.MapDataRegion:
				this.TypeStart(this.CreateTypeName("MapMember" + text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapMembers), "MapMemberExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.DataShape:
				this.TypeStart(this.CreateTypeName("DataShapeMember" + text, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataShapeMembers), "DataShapeMemberExprHost");
				return;
			default:
				return;
			}
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0013A504 File Offset: 0x00138704
		internal int DataGroupEnd(ExprHostBuilder.DataRegionMode mode, bool column)
		{
			switch (mode)
			{
			case ExprHostBuilder.DataRegionMode.Tablix:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).TablixMembers);
			case ExprHostBuilder.DataRegionMode.Chart:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartMembers);
			case ExprHostBuilder.DataRegionMode.GaugePanel:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeMembers);
			case ExprHostBuilder.DataRegionMode.CustomReportItem:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataGroups);
			case ExprHostBuilder.DataRegionMode.MapDataRegion:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapMembers);
			case ExprHostBuilder.DataRegionMode.DataShape:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataShapeMembers);
			default:
				return -1;
			}
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x0013A640 File Offset: 0x00138840
		internal void DataCellStart(ExprHostBuilder.DataRegionMode mode)
		{
			switch (mode)
			{
			case ExprHostBuilder.DataRegionMode.Tablix:
				this.TypeStart(this.CreateTypeName("TablixCell", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).TablixCells), "TablixCellExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.Chart:
				this.TypeStart(this.CreateTypeName("ChartDataPoint", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataPoints), "ChartDataPointExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.GaugePanel:
				this.TypeStart(this.CreateTypeName("GaugeCell", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).GaugeCells), "GaugeCellExprHost");
				return;
			case ExprHostBuilder.DataRegionMode.CustomReportItem:
				this.TypeStart(this.CreateTypeName("DataCell", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataCells), "DataCellExprHost");
				break;
			case ExprHostBuilder.DataRegionMode.MapDataRegion:
				break;
			case ExprHostBuilder.DataRegionMode.DataShape:
				this.TypeStart(this.CreateTypeName("DataShapeIntersection", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).DataShapeIntersections), "DataShapeIntersectionExprHost");
				return;
			default:
				return;
			}
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x0013A730 File Offset: 0x00138930
		internal int DataCellEnd(ExprHostBuilder.DataRegionMode mode)
		{
			switch (mode)
			{
			case ExprHostBuilder.DataRegionMode.Tablix:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).TablixCells);
			case ExprHostBuilder.DataRegionMode.Chart:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataPoints);
			case ExprHostBuilder.DataRegionMode.GaugePanel:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeCells);
			case ExprHostBuilder.DataRegionMode.CustomReportItem:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataCells);
			case ExprHostBuilder.DataRegionMode.DataShape:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataShapeIntersections);
			}
			return -1;
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x0013A83D File Offset: 0x00138A3D
		internal void MarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string marginPosition)
		{
			this.ExpressionAdd(marginPosition + "Expr", expression);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x0013A851 File Offset: 0x00138A51
		internal void ChartTitleStart(string titleName)
		{
			this.TypeStart(this.CreateTypeName("ChartTitle" + titleName, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartTitles), "ChartTitleExprHost");
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x0013A87F File Offset: 0x00138A7F
		internal void ChartTitlePosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartTitlePositionExpr", expression);
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x0013A88D File Offset: 0x00138A8D
		internal void ChartTitleHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0013A89B File Offset: 0x00138A9B
		internal void ChartTitleDocking(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockingExpr", expression);
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x0013A8A9 File Offset: 0x00138AA9
		internal void ChartTitleDockOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockingOffsetExpr", expression);
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x0013A8B7 File Offset: 0x00138AB7
		internal void ChartTitleDockOutsideChartArea(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockOutsideChartAreaExpr", expression);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x0013A8C5 File Offset: 0x00138AC5
		internal void ChartTitleToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0013A8D3 File Offset: 0x00138AD3
		internal void ChartTitleTextOrientation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextOrientationExpr", expression);
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0013A8E1 File Offset: 0x00138AE1
		internal int ChartTitleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_titlesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartTitles);
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x0013A90E File Offset: 0x00138B0E
		internal void ChartNoDataMessageStart()
		{
			this.TypeStart("ChartTitle", "ChartTitleExprHost");
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0013A920 File Offset: 0x00138B20
		internal void ChartNoDataMessageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "NoDataMessageHost");
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0013A939 File Offset: 0x00138B39
		internal void ChartCaption(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0013A947 File Offset: 0x00138B47
		internal void ChartAxisTitleStart()
		{
			this.TypeStart("ChartAxisTitle", "ChartAxisTitleExprHost");
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0013A959 File Offset: 0x00138B59
		internal void ChartAxisTitleTextOrientation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextOrientationExpr", expression);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0013A967 File Offset: 0x00138B67
		internal void ChartAxisTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TitleHost");
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0013A980 File Offset: 0x00138B80
		internal void ChartLegendTitleStart()
		{
			this.TypeStart("ChartLegendTitle", "ChartLegendTitleExprHost");
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0013A992 File Offset: 0x00138B92
		internal void ChartLegendTitleSeparator(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleSeparatorExpr", expression);
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x0013A9A0 File Offset: 0x00138BA0
		internal void ChartLegendTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TitleExprHost");
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x0013A9B9 File Offset: 0x00138BB9
		internal void AxisMin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinExpr", expression);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x0013A9C7 File Offset: 0x00138BC7
		internal void AxisMax(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMaxExpr", expression);
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x0013A9D5 File Offset: 0x00138BD5
		internal void AxisCrossAt(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisCrossAtExpr", expression);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0013A9E3 File Offset: 0x00138BE3
		internal void AxisMajorInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMajorIntervalExpr", expression);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0013A9F1 File Offset: 0x00138BF1
		internal void AxisMinorInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinorIntervalExpr", expression);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x0013A9FF File Offset: 0x00138BFF
		internal void ChartPalette(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PaletteExpr", expression);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x0013AA0D File Offset: 0x00138C0D
		internal void ChartPaletteHatchBehavior(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PaletteHatchBehaviorExpr", expression);
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x0013AA1B File Offset: 0x00138C1B
		internal void DynamicWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DynamicWidthExpr", expression);
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x0013AA29 File Offset: 0x00138C29
		internal void DynamicHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DynamicHeightExpr", expression);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x0013AA38 File Offset: 0x00138C38
		internal void ChartAxisStart(string axisName, bool isValueAxis)
		{
			if (isValueAxis)
			{
				this.TypeStart(this.CreateTypeName("ValueAxis" + axisName, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ValueAxes), "ChartAxisExprHost");
				return;
			}
			this.TypeStart(this.CreateTypeName("CategoryAxis" + axisName, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).CategoryAxes), "ChartAxisExprHost");
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0013AAA1 File Offset: 0x00138CA1
		internal void ChartAxisVisible(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibleExpr", expression);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x0013AAAF File Offset: 0x00138CAF
		internal void ChartAxisMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarginExpr", expression);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0013AABD File Offset: 0x00138CBD
		internal void ChartAxisInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0013AACB File Offset: 0x00138CCB
		internal void ChartAxisIntervalType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x0013AAD9 File Offset: 0x00138CD9
		internal void ChartAxisIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0013AAE7 File Offset: 0x00138CE7
		internal void ChartAxisIntervalOffsetType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0013AAF5 File Offset: 0x00138CF5
		internal void ChartAxisMarksAlwaysAtPlotEdge(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarksAlwaysAtPlotEdgeExpr", expression);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0013AB03 File Offset: 0x00138D03
		internal void ChartAxisReverse(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReverseExpr", expression);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x0013AB11 File Offset: 0x00138D11
		internal void ChartAxisLocation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LocationExpr", expression);
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0013AB1F File Offset: 0x00138D1F
		internal void ChartAxisInterlaced(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedExpr", expression);
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x0013AB2D File Offset: 0x00138D2D
		internal void ChartAxisInterlacedColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedColorExpr", expression);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x0013AB3B File Offset: 0x00138D3B
		internal void ChartAxisLogScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogScaleExpr", expression);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x0013AB49 File Offset: 0x00138D49
		internal void ChartAxisLogBase(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogBaseExpr", expression);
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x0013AB57 File Offset: 0x00138D57
		internal void ChartAxisHideLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideLabelsExpr", expression);
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x0013AB65 File Offset: 0x00138D65
		internal void ChartAxisAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x0013AB73 File Offset: 0x00138D73
		internal void ChartAxisArrows(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ArrowsExpr", expression);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x0013AB81 File Offset: 0x00138D81
		internal void ChartAxisPreventFontShrink(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventFontShrinkExpr", expression);
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x0013AB8F File Offset: 0x00138D8F
		internal void ChartAxisPreventFontGrow(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventFontGrowExpr", expression);
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x0013AB9D File Offset: 0x00138D9D
		internal void ChartAxisPreventLabelOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventLabelOffsetExpr", expression);
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x0013ABAB File Offset: 0x00138DAB
		internal void ChartAxisPreventWordWrap(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventWordWrapExpr", expression);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x0013ABB9 File Offset: 0x00138DB9
		internal void ChartAxisAllowLabelRotation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowLabelRotationExpr", expression);
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x0013ABC7 File Offset: 0x00138DC7
		internal void ChartAxisIncludeZero(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IncludeZeroExpr", expression);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x0013ABD5 File Offset: 0x00138DD5
		internal void ChartAxisLabelsAutoFitDisabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelsAutoFitDisabledExpr", expression);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x0013ABE3 File Offset: 0x00138DE3
		internal void ChartAxisMinFontSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinFontSizeExpr", expression);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0013ABF1 File Offset: 0x00138DF1
		internal void ChartAxisMaxFontSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxFontSizeExpr", expression);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0013ABFF File Offset: 0x00138DFF
		internal void ChartAxisOffsetLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetLabelsExpr", expression);
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0013AC0D File Offset: 0x00138E0D
		internal void ChartAxisHideEndLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideEndLabelsExpr", expression);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0013AC1B File Offset: 0x00138E1B
		internal void ChartAxisVariableAutoInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VariableAutoIntervalExpr", expression);
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x0013AC29 File Offset: 0x00138E29
		internal void ChartAxisLabelInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalExpr", expression);
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x0013AC37 File Offset: 0x00138E37
		internal void ChartAxisLabelIntervalType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalTypeExpr", expression);
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x0013AC45 File Offset: 0x00138E45
		internal void ChartAxisLabelIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalOffsetExpr", expression);
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x0013AC53 File Offset: 0x00138E53
		internal void ChartAxisLabelIntervalOffsetType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalOffsetTypeExpr", expression);
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x0013AC64 File Offset: 0x00138E64
		internal int ChartAxisEnd(bool isValueAxis)
		{
			if (isValueAxis)
			{
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_valueAxesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ValueAxes);
			}
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_categoryAxesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).CategoryAxes);
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x0013ACCB File Offset: 0x00138ECB
		internal void ChartGridLinesStart(bool isMajor)
		{
			this.TypeStart("ChartGridLines" + (isMajor ? "MajorGridLinesHost" : "MinorGridLinesHost"), "ChartGridLinesExprHost");
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x0013ACF1 File Offset: 0x00138EF1
		internal void ChartGridLinesEnd(bool isMajor)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMajor ? "MajorGridLinesHost" : "MinorGridLinesHost");
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x0013AD14 File Offset: 0x00138F14
		internal void ChartGridLinesIntervalOffsetType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x0013AD22 File Offset: 0x00138F22
		internal void ChartGridLinesIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x0013AD30 File Offset: 0x00138F30
		internal void ChartGridLinesEnabledIntervalType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x0013AD3E File Offset: 0x00138F3E
		internal void ChartGridLinesInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x0013AD4C File Offset: 0x00138F4C
		internal void ChartGridLinesEnabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x0013AD5A File Offset: 0x00138F5A
		internal void ChartLegendStart(string legendName)
		{
			this.TypeStart(this.CreateTypeName("ChartLegend" + legendName, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartLegends), "ChartLegendExprHost");
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x0013AD88 File Offset: 0x00138F88
		internal void ChartLegendHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x0013AD96 File Offset: 0x00138F96
		internal void ChartLegendPosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartLegendPositionExpr", expression);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x0013ADA4 File Offset: 0x00138FA4
		internal void ChartLegendLayout(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LayoutExpr", expression);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x0013ADB2 File Offset: 0x00138FB2
		internal void ChartLegendDockOutsideChartArea(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockOutsideChartAreaExpr", expression);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x0013ADC0 File Offset: 0x00138FC0
		internal void ChartLegendAutoFitTextDisabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoFitTextDisabledExpr", expression);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x0013ADCE File Offset: 0x00138FCE
		internal void ChartLegendMinFontSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinFontSizeExpr", expression);
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x0013ADDC File Offset: 0x00138FDC
		internal void ChartLegendHeaderSeparator(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeaderSeparatorExpr", expression);
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x0013ADEA File Offset: 0x00138FEA
		internal void ChartLegendHeaderSeparatorColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeaderSeparatorColorExpr", expression);
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x0013ADF8 File Offset: 0x00138FF8
		internal void ChartLegendColumnSeparator(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnSeparatorExpr", expression);
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x0013AE06 File Offset: 0x00139006
		internal void ChartLegendColumnSeparatorColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnSeparatorColorExpr", expression);
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x0013AE14 File Offset: 0x00139014
		internal void ChartLegendColumnSpacing(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnSpacingExpr", expression);
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x0013AE22 File Offset: 0x00139022
		internal void ChartLegendInterlacedRows(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsExpr", expression);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x0013AE30 File Offset: 0x00139030
		internal void ChartLegendInterlacedRowsColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsColorExpr", expression);
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x0013AE3E File Offset: 0x0013903E
		internal void ChartLegendEquallySpacedItems(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EquallySpacedItemsExpr", expression);
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x0013AE4C File Offset: 0x0013904C
		internal void ChartLegendReversed(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReversedExpr", expression);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x0013AE5A File Offset: 0x0013905A
		internal void ChartLegendMaxAutoSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxAutoSizeExpr", expression);
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0013AE68 File Offset: 0x00139068
		internal void ChartLegendTextWrapThreshold(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextWrapThresholdExpr", expression);
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0013AE76 File Offset: 0x00139076
		internal int ChartLegendEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegends);
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x0013AEA3 File Offset: 0x001390A3
		internal void ChartSeriesStart()
		{
			this.TypeStart("ChartSeries", "ChartSeriesExprHost");
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x0013AEB5 File Offset: 0x001390B5
		internal void ChartSeriesType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x0013AEC3 File Offset: 0x001390C3
		internal void ChartSeriesSubtype(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SubtypeExpr", expression);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x0013AED1 File Offset: 0x001390D1
		internal void ChartSeriesLegendName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendNameExpr", expression);
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x0013AEDF File Offset: 0x001390DF
		internal void ChartSeriesLegendText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendTextExpr", expression);
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x0013AEED File Offset: 0x001390ED
		internal void ChartSeriesChartAreaName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartAreaNameExpr", expression);
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x0013AEFB File Offset: 0x001390FB
		internal void ChartSeriesValueAxisName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueAxisNameExpr", expression);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x0013AF09 File Offset: 0x00139109
		internal void ChartSeriesCategoryAxisName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CategoryAxisNameExpr", expression);
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x0013AF17 File Offset: 0x00139117
		internal void ChartSeriesHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x0013AF25 File Offset: 0x00139125
		internal void ChartSeriesHideInLegend(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideInLegendExpr", expression);
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x0013AF33 File Offset: 0x00139133
		internal void ChartSeriesToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x0013AF41 File Offset: 0x00139141
		internal void ChartSeriesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ChartSeriesHost");
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x0013AF5A File Offset: 0x0013915A
		internal void ChartNoMoveDirectionsStart()
		{
			this.TypeStart("ChartNoMoveDirections", "ChartNoMoveDirectionsExprHost");
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x0013AF6C File Offset: 0x0013916C
		internal void ChartNoMoveDirectionsUp(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UpExpr", expression);
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x0013AF7A File Offset: 0x0013917A
		internal void ChartNoMoveDirectionsDown(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DownExpr", expression);
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0013AF88 File Offset: 0x00139188
		internal void ChartNoMoveDirectionsLeft(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0013AF96 File Offset: 0x00139196
		internal void ChartNoMoveDirectionsRight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightExpr", expression);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x0013AFA4 File Offset: 0x001391A4
		internal void ChartNoMoveDirectionsUpLeft(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UpLeftExpr", expression);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x0013AFB2 File Offset: 0x001391B2
		internal void ChartNoMoveDirectionsUpRight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UpRightExpr", expression);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x0013AFC0 File Offset: 0x001391C0
		internal void ChartNoMoveDirectionsDownLeft(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DownLeftExpr", expression);
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x0013AFCE File Offset: 0x001391CE
		internal void ChartNoMoveDirectionsDownRight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DownRightExpr", expression);
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x0013AFDC File Offset: 0x001391DC
		internal void ChartNoMoveDirectionsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "NoMoveDirectionsHost");
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x0013AFF5 File Offset: 0x001391F5
		internal void ChartElementPositionStart(bool innerPlot)
		{
			this.TypeStart(innerPlot ? "ChartInnerPlotPosition" : "ChartElementPosition", "ChartElementPositionExprHost");
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x0013B011 File Offset: 0x00139211
		internal void ChartElementPositionEnd(bool innerPlot)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, innerPlot ? "ChartInnerPlotPositionHost" : "ChartElementPositionHost");
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x0013B034 File Offset: 0x00139234
		internal void ChartElementPositionTop(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopExpr", expression);
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x0013B042 File Offset: 0x00139242
		internal void ChartElementPositionLeft(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x0013B050 File Offset: 0x00139250
		internal void ChartElementPositionHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeightExpr", expression);
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x0013B05E File Offset: 0x0013925E
		internal void ChartElementPositionWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0013B06C File Offset: 0x0013926C
		internal void ChartSmartLabelStart()
		{
			this.TypeStart("ChartSmartLabel", "ChartSmartLabelExprHost");
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x0013B07E File Offset: 0x0013927E
		internal void ChartSmartLabelAllowOutSidePlotArea(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowOutSidePlotAreaExpr", expression);
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x0013B08C File Offset: 0x0013928C
		internal void ChartSmartLabelCalloutBackColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutBackColorExpr", expression);
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x0013B09A File Offset: 0x0013929A
		internal void ChartSmartLabelCalloutLineAnchor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineAnchorExpr", expression);
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x0013B0A8 File Offset: 0x001392A8
		internal void ChartSmartLabelCalloutLineColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineColorExpr", expression);
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x0013B0B6 File Offset: 0x001392B6
		internal void ChartSmartLabelCalloutLineStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineStyleExpr", expression);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x0013B0C4 File Offset: 0x001392C4
		internal void ChartSmartLabelCalloutLineWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineWidthExpr", expression);
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x0013B0D2 File Offset: 0x001392D2
		internal void ChartSmartLabelCalloutStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutStyleExpr", expression);
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x0013B0E0 File Offset: 0x001392E0
		internal void ChartSmartLabelShowOverlapped(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowOverlappedExpr", expression);
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x0013B0EE File Offset: 0x001392EE
		internal void ChartSmartLabelMarkerOverlapping(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkerOverlappingExpr", expression);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x0013B0FC File Offset: 0x001392FC
		internal void ChartSmartLabelDisabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DisabledExpr", expression);
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x0013B10A File Offset: 0x0013930A
		internal void ChartSmartLabelMaxMovingDistance(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxMovingDistanceExpr", expression);
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x0013B118 File Offset: 0x00139318
		internal void ChartSmartLabelMinMovingDistance(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinMovingDistanceExpr", expression);
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x0013B126 File Offset: 0x00139326
		internal void ChartSmartLabelEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SmartLabelHost");
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x0013B13F File Offset: 0x0013933F
		internal void ChartAxisScaleBreakStart()
		{
			this.TypeStart("ChartAxisScaleBreak", "ChartAxisScaleBreakExprHost");
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x0013B151 File Offset: 0x00139351
		internal void ChartAxisScaleBreakEnabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x0013B15F File Offset: 0x0013935F
		internal void ChartAxisScaleBreakBreakLineType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BreakLineTypeExpr", expression);
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x0013B16D File Offset: 0x0013936D
		internal void ChartAxisScaleBreakCollapsibleSpaceThreshold(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CollapsibleSpaceThresholdExpr", expression);
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x0013B17B File Offset: 0x0013937B
		internal void ChartAxisScaleBreakMaxNumberOfBreaks(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxNumberOfBreaksExpr", expression);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x0013B189 File Offset: 0x00139389
		internal void ChartAxisScaleBreakSpacing(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpacingExpr", expression);
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x0013B197 File Offset: 0x00139397
		internal void ChartAxisScaleBreakIncludeZero(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IncludeZeroExpr", expression);
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x0013B1A5 File Offset: 0x001393A5
		internal void ChartAxisScaleBreakEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "AxisScaleBreakHost");
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x0013B1BE File Offset: 0x001393BE
		internal void ChartBorderSkinStart()
		{
			this.TypeStart("ChartBorderSkin", "ChartBorderSkinExprHost");
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x0013B1D0 File Offset: 0x001393D0
		internal void ChartBorderSkinBorderSkinType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BorderSkinTypeExpr", expression);
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x0013B1DE File Offset: 0x001393DE
		internal void ChartBorderSkinEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "BorderSkinHost");
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x0013B1F7 File Offset: 0x001393F7
		internal void ChartItemInLegendStart()
		{
			this.TypeStart("ChartItemInLegend", "ChartDataPointInLegendExprHost");
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x0013B209 File Offset: 0x00139409
		internal void ChartItemInLegendLegendText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendTextExpr", expression);
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x0013B217 File Offset: 0x00139417
		internal void ChartItemInLegendToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x0013B225 File Offset: 0x00139425
		internal void ChartItemInLegendHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x0013B233 File Offset: 0x00139433
		internal void ChartItemInLegendEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "DataPointInLegendHost");
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x0013B24C File Offset: 0x0013944C
		internal void ChartTickMarksStart(bool isMajor)
		{
			this.TypeStart("ChartTickMarks" + (isMajor ? "MajorTickMarksHost" : "MinorTickMarksHost"), "ChartTickMarksExprHost");
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x0013B272 File Offset: 0x00139472
		internal void ChartTickMarksEnd(bool isMajor)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMajor ? "MajorTickMarksHost" : "MinorTickMarksHost");
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x0013B295 File Offset: 0x00139495
		internal void ChartTickMarksEnabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x0013B2A3 File Offset: 0x001394A3
		internal void ChartTickMarksType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x0013B2B1 File Offset: 0x001394B1
		internal void ChartTickMarksLength(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LengthExpr", expression);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x0013B2BF File Offset: 0x001394BF
		internal void ChartTickMarksInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x0013B2CD File Offset: 0x001394CD
		internal void ChartTickMarksIntervalType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x0013B2DB File Offset: 0x001394DB
		internal void ChartTickMarksIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x0013B2E9 File Offset: 0x001394E9
		internal void ChartTickMarksIntervalOffsetType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x0013B2F7 File Offset: 0x001394F7
		internal void ChartEmptyPointsStart()
		{
			this.TypeStart("ChartEmptyPoints", "ChartEmptyPointsExprHost");
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x0013B309 File Offset: 0x00139509
		internal void ChartEmptyPointsAxisLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisLabelExpr", expression);
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x0013B317 File Offset: 0x00139517
		internal void ChartEmptyPointsToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x0013B325 File Offset: 0x00139525
		internal void ChartEmptyPointsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "EmptyPointsHost");
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x0013B33E File Offset: 0x0013953E
		internal void ChartLegendColumnHeaderStart()
		{
			this.TypeStart("ChartLegendColumnHeader", "ChartLegendColumnHeaderExprHost");
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x0013B350 File Offset: 0x00139550
		internal void ChartLegendColumnHeaderValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0013B35E File Offset: 0x0013955E
		internal void ChartLegendColumnHeaderEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ChartLegendColumnHeaderHost");
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x0013B377 File Offset: 0x00139577
		internal void ChartCustomPaletteColorStart(int index)
		{
			this.TypeStart(this.CreateTypeName("ChartCustomPaletteColor" + index.ToString(CultureInfo.InvariantCulture), ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartCustomPaletteColors), "ChartCustomPaletteColorExprHost");
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x0013B3B0 File Offset: 0x001395B0
		internal int ChartCustomPaletteColorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_customPaletteColorHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartCustomPaletteColors);
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x0013B3DD File Offset: 0x001395DD
		internal void ChartCustomPaletteColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorExpr", expression);
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x0013B3EB File Offset: 0x001395EB
		internal void ChartLegendCustomItemCellStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartLegendCustomItemCell" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartLegendCustomItemCells), "ChartLegendCustomItemCellExprHost");
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x0013B419 File Offset: 0x00139619
		internal void ChartLegendCustomItemCellCellType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CellTypeExpr", expression);
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x0013B427 File Offset: 0x00139627
		internal void ChartLegendCustomItemCellText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0013B435 File Offset: 0x00139635
		internal void ChartLegendCustomItemCellCellSpan(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CellSpanExpr", expression);
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x0013B443 File Offset: 0x00139643
		internal void ChartLegendCustomItemCellToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x0013B451 File Offset: 0x00139651
		internal void ChartLegendCustomItemCellImageWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ImageWidthExpr", expression);
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x0013B45F File Offset: 0x0013965F
		internal void ChartLegendCustomItemCellImageHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ImageHeightExpr", expression);
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x0013B46D File Offset: 0x0013966D
		internal void ChartLegendCustomItemCellSymbolHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SymbolHeightExpr", expression);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x0013B47B File Offset: 0x0013967B
		internal void ChartLegendCustomItemCellSymbolWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SymbolWidthExpr", expression);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x0013B489 File Offset: 0x00139689
		internal void ChartLegendCustomItemCellAlignment(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AlignmentExpr", expression);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x0013B497 File Offset: 0x00139697
		internal void ChartLegendCustomItemCellTopMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopMarginExpr", expression);
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x0013B4A5 File Offset: 0x001396A5
		internal void ChartLegendCustomItemCellBottomMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BottomMarginExpr", expression);
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x0013B4B3 File Offset: 0x001396B3
		internal void ChartLegendCustomItemCellLeftMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftMarginExpr", expression);
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x0013B4C1 File Offset: 0x001396C1
		internal void ChartLegendCustomItemCellRightMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightMarginExpr", expression);
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x0013B4CF File Offset: 0x001396CF
		internal int ChartLegendCustomItemCellEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendCustomItemCellHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegendCustomItemCells);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x0013B4FC File Offset: 0x001396FC
		internal void ChartDerivedSeriesStart(int index)
		{
			this.TypeStart(this.CreateTypeName("ChartDerivedSeries" + index.ToString(CultureInfo.InvariantCulture), ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartDerivedSeriesCollection), "ChartDerivedSeriesExprHost");
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x0013B535 File Offset: 0x00139735
		internal void ChartDerivedSeriesSourceChartSeriesName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceChartSeriesNameExpr", expression);
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x0013B543 File Offset: 0x00139743
		internal void ChartDerivedSeriesDerivedSeriesFormula(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DerivedSeriesFormulaExpr", expression);
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x0013B551 File Offset: 0x00139751
		internal int ChartDerivedSeriesEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_derivedSeriesCollectionHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartDerivedSeriesCollection);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x0013B57E File Offset: 0x0013977E
		internal void ChartStripLineStart(int index)
		{
			this.TypeStart(this.CreateTypeName("ChartStripLine" + index.ToString(CultureInfo.InvariantCulture), ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartStripLines), "ChartStripLineExprHost");
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x0013B5B7 File Offset: 0x001397B7
		internal void ChartStripLineTitle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleExpr", expression);
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x0013B5C5 File Offset: 0x001397C5
		internal void ChartStripLineTitleAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleAngleExpr", expression);
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x0013B5D3 File Offset: 0x001397D3
		internal void ChartStripLineTextOrientation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextOrientationExpr", expression);
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x0013B5E1 File Offset: 0x001397E1
		internal void ChartStripLineToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x0013B5EF File Offset: 0x001397EF
		internal void ChartStripLineInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x0013B5FD File Offset: 0x001397FD
		internal void ChartStripLineIntervalType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x0013B60B File Offset: 0x0013980B
		internal void ChartStripLineIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x0013B619 File Offset: 0x00139819
		internal void ChartStripLineIntervalOffsetType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x0013B627 File Offset: 0x00139827
		internal void ChartStripLineStripWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StripWidthExpr", expression);
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x0013B635 File Offset: 0x00139835
		internal void ChartStripLineStripWidthType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StripWidthTypeExpr", expression);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x0013B643 File Offset: 0x00139843
		internal int ChartStripLineEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_stripLinesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartStripLines);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x0013B670 File Offset: 0x00139870
		internal void ChartFormulaParameterStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartFormulaParameter" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartFormulaParameters), "ChartFormulaParameterExprHost");
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0013B69E File Offset: 0x0013989E
		internal void ChartFormulaParameterValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0013B6AC File Offset: 0x001398AC
		internal int ChartFormulaParameterEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_formulaParametersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartFormulaParameters);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x0013B6D9 File Offset: 0x001398D9
		internal void ChartLegendColumnStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartLegendColumn" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartLegendColumns), "ChartLegendColumnExprHost");
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x0013B707 File Offset: 0x00139907
		internal void ChartLegendColumnColumnType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnTypeExpr", expression);
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x0013B715 File Offset: 0x00139915
		internal void ChartLegendColumnValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x0013B723 File Offset: 0x00139923
		internal void ChartLegendColumnToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0013B731 File Offset: 0x00139931
		internal void ChartLegendColumnMinimumWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumWidthExpr", expression);
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x0013B73F File Offset: 0x0013993F
		internal void ChartLegendColumnMaximumWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumWidthExpr", expression);
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x0013B74D File Offset: 0x0013994D
		internal void ChartLegendColumnSeriesSymbolWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeriesSymbolWidthExpr", expression);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0013B75B File Offset: 0x0013995B
		internal void ChartLegendColumnSeriesSymbolHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeriesSymbolHeightExpr", expression);
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x0013B769 File Offset: 0x00139969
		internal int ChartLegendColumnEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendColumnsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegendColumns);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x0013B796 File Offset: 0x00139996
		internal void ChartLegendCustomItemStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartLegendCustomItem" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartLegendCustomItems), "ChartLegendCustomItemExprHost");
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x0013B7C4 File Offset: 0x001399C4
		internal void ChartLegendCustomItemSeparator(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorExpr", expression);
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x0013B7D2 File Offset: 0x001399D2
		internal void ChartLegendCustomItemSeparatorColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorColorExpr", expression);
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x0013B7E0 File Offset: 0x001399E0
		internal void ChartLegendCustomItemToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x0013B7EE File Offset: 0x001399EE
		internal int ChartLegendCustomItemEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendCustomItemsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegendCustomItems);
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x0013B81B File Offset: 0x00139A1B
		internal void ChartAreaStart(string chartAreaName)
		{
			this.TypeStart(this.CreateTypeName("ChartArea" + chartAreaName, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ChartAreas), "ChartAreaExprHost");
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x0013B849 File Offset: 0x00139A49
		internal void Chart3DPropertiesStart()
		{
			this.TypeStart("Chart3DProperties", "Chart3DPropertiesExprHost");
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x0013B85B File Offset: 0x00139A5B
		internal void Chart3DPropertiesEnabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x0013B869 File Offset: 0x00139A69
		internal void Chart3DPropertiesRotation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotationExpr", expression);
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x0013B877 File Offset: 0x00139A77
		internal void Chart3DPropertiesProjectionMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ProjectionModeExpr", expression);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x0013B885 File Offset: 0x00139A85
		internal void Chart3DPropertiesInclination(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InclinationExpr", expression);
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x0013B893 File Offset: 0x00139A93
		internal void Chart3DPropertiesPerspective(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PerspectiveExpr", expression);
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0013B8A1 File Offset: 0x00139AA1
		internal void Chart3DPropertiesDepthRatio(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DepthRatioExpr", expression);
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x0013B8AF File Offset: 0x00139AAF
		internal void Chart3DPropertiesShading(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShadingExpr", expression);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0013B8BD File Offset: 0x00139ABD
		internal void Chart3DPropertiesGapDepth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GapDepthExpr", expression);
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x0013B8CB File Offset: 0x00139ACB
		internal void Chart3DPropertiesWallThickness(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WallThicknessExpr", expression);
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x0013B8D9 File Offset: 0x00139AD9
		internal void Chart3DPropertiesClustered(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ClusteredExpr", expression);
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x0013B8E7 File Offset: 0x00139AE7
		internal void Chart3DPropertiesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "Chart3DPropertiesHost");
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x0013B900 File Offset: 0x00139B00
		internal void ChartAreaHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x0013B90E File Offset: 0x00139B0E
		internal void ChartAreaAlignOrientation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AlignOrientationExpr", expression);
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0013B91C File Offset: 0x00139B1C
		internal void ChartAreaEquallySizedAxesFont(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EquallySizedAxesFontExpr", expression);
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x0013B92A File Offset: 0x00139B2A
		internal void ChartAlignTypePosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartAlignTypePositionExpr", expression);
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x0013B938 File Offset: 0x00139B38
		internal void ChartAlignTypeInnerPlotPosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InnerPlotPositionExpr", expression);
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x0013B946 File Offset: 0x00139B46
		internal void ChartAlignTypCursor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CursorExpr", expression);
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0013B954 File Offset: 0x00139B54
		internal void ChartAlignTypeAxesView(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxesViewExpr", expression);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0013B962 File Offset: 0x00139B62
		internal int ChartAreaEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_chartAreasHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartAreas);
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0013B98F File Offset: 0x00139B8F
		internal void ChartDataPointValueX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesXExpr", expression);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0013B99D File Offset: 0x00139B9D
		internal void ChartDataPointValueY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesYExpr", expression);
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x0013B9AB File Offset: 0x00139BAB
		internal void ChartDataPointValueSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesSizeExpr", expression);
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0013B9B9 File Offset: 0x00139BB9
		internal void ChartDataPointValueHigh(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighExpr", expression);
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0013B9C7 File Offset: 0x00139BC7
		internal void ChartDataPointValueLow(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesLowExpr", expression);
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0013B9D5 File Offset: 0x00139BD5
		internal void ChartDataPointValueStart(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesStartExpr", expression);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0013B9E3 File Offset: 0x00139BE3
		internal void ChartDataPointValueEnd(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesEndExpr", expression);
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x0013B9F1 File Offset: 0x00139BF1
		internal void ChartDataPointValueMean(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesMeanExpr", expression);
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0013B9FF File Offset: 0x00139BFF
		internal void ChartDataPointValueMedian(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesMedianExpr", expression);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0013BA0D File Offset: 0x00139C0D
		internal void ChartDataPointValueHighlightX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighlightXExpr", expression);
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0013BA1B File Offset: 0x00139C1B
		internal void ChartDataPointValueHighlightY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighlightYExpr", expression);
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0013BA29 File Offset: 0x00139C29
		internal void ChartDataPointValueHighlightSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighlightSizeExpr", expression);
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0013BA37 File Offset: 0x00139C37
		internal void ChartDataPointValueFormatX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueFormatXExpr", expression);
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0013BA45 File Offset: 0x00139C45
		internal void ChartDataPointValueFormatY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueFormatYExpr", expression);
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0013BA53 File Offset: 0x00139C53
		internal void ChartDataPointValueFormatSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueFormatSizeExpr", expression);
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0013BA61 File Offset: 0x00139C61
		internal void ChartDataPointValueCurrencyLanguageX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueCurrencyLanguageXExpr", expression);
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0013BA6F File Offset: 0x00139C6F
		internal void ChartDataPointValueCurrencyLanguageY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueCurrencyLanguageYExpr", expression);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0013BA7D File Offset: 0x00139C7D
		internal void ChartDataPointValueCurrencyLanguageSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueCurrencyLanguageSizeExpr", expression);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0013BA8B File Offset: 0x00139C8B
		internal void ChartDataPointAxisLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisLabelExpr", expression);
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0013BA99 File Offset: 0x00139C99
		internal void ChartDataPointToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0013BAA7 File Offset: 0x00139CA7
		internal void DataLabelStart()
		{
			this.TypeStart("DataLabel", "ChartDataLabelExprHost");
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0013BAB9 File Offset: 0x00139CB9
		internal void DataLabelLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0013BAC7 File Offset: 0x00139CC7
		internal void DataLabelVisible(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibleExpr", expression);
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x0013BAD5 File Offset: 0x00139CD5
		internal void DataLabelPosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataLabelPositionExpr", expression);
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0013BAE3 File Offset: 0x00139CE3
		internal void DataLabelRotation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotationExpr", expression);
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x0013BAF1 File Offset: 0x00139CF1
		internal void DataLabelUseValueAsLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseValueAsLabelExpr", expression);
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0013BAFF File Offset: 0x00139CFF
		internal void ChartDataLabelToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x0013BB0D File Offset: 0x00139D0D
		internal void DataLabelEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "DataLabelHost");
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0013BB26 File Offset: 0x00139D26
		internal void DataPointStyleStart()
		{
			this.StyleStart("Style");
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0013BB33 File Offset: 0x00139D33
		internal void DataPointStyleEnd()
		{
			this.StyleEnd("StyleHost");
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0013BB40 File Offset: 0x00139D40
		internal void DataPointMarkerStart()
		{
			this.TypeStart("ChartMarker", "ChartMarkerExprHost");
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0013BB52 File Offset: 0x00139D52
		internal void DataPointMarkerSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SizeExpr", expression);
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0013BB60 File Offset: 0x00139D60
		internal void DataPointMarkerType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x0013BB6E File Offset: 0x00139D6E
		internal void DataPointMarkerEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ChartMarkerHost");
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0013BB87 File Offset: 0x00139D87
		internal void ChartMemberLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MemberLabelExpr", expression);
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0013BB95 File Offset: 0x00139D95
		internal void ChartMemberStyleStart()
		{
			this.StyleStart("MemberStyle");
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0013BBA2 File Offset: 0x00139DA2
		internal void ChartMemberStyleEnd()
		{
			this.StyleEnd("MemberStyleHost");
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0013BBAF File Offset: 0x00139DAF
		internal void DataValueStart()
		{
			this.TypeStart(this.CreateTypeName("DataValue", this.m_currentTypeDecl.DataValues), "DataValueExprHost");
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0013BBD2 File Offset: 0x00139DD2
		internal int DataValueEnd(bool isCustomProperty)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, isCustomProperty ? "m_customPropertyHostsRemotable" : "m_dataValueHostsRemotable", ref this.m_currentTypeDecl.Parent.DataValues);
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x0013BC04 File Offset: 0x00139E04
		internal void DataValueName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueNameExpr", expression);
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0013BC12 File Offset: 0x00139E12
		internal void DataValueValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueValueExpr", expression);
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0013BC20 File Offset: 0x00139E20
		internal void BaseGaugeImageSource(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0013BC2E File Offset: 0x00139E2E
		internal void BaseGaugeImageValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0013BC3C File Offset: 0x00139E3C
		internal void BaseGaugeImageMIMEType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0013BC4A File Offset: 0x00139E4A
		internal void BaseGaugeImageTransparentColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparentColorExpr", expression);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0013BC58 File Offset: 0x00139E58
		internal void CapImageStart()
		{
			this.TypeStart("CapImage", "CapImageExprHost");
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0013BC6A File Offset: 0x00139E6A
		internal void CapImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "CapImageHost");
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0013BC83 File Offset: 0x00139E83
		internal void CapImageHueColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x0013BC91 File Offset: 0x00139E91
		internal void CapImageOffsetX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetXExpr", expression);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0013BC9F File Offset: 0x00139E9F
		internal void CapImageOffsetY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetYExpr", expression);
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0013BCAD File Offset: 0x00139EAD
		internal void FrameImageStart()
		{
			this.TypeStart("FrameImage", "FrameImageExprHost");
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0013BCBF File Offset: 0x00139EBF
		internal void FrameImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "FrameImageHost");
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0013BCD8 File Offset: 0x00139ED8
		internal void FrameImageHueColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0013BCE6 File Offset: 0x00139EE6
		internal void FrameImageTransparency(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0013BCF4 File Offset: 0x00139EF4
		internal void FrameImageClipImage(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ClipImageExpr", expression);
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x0013BD02 File Offset: 0x00139F02
		internal void PointerImageStart()
		{
			this.TypeStart("PointerImage", "PointerImageExprHost");
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0013BD14 File Offset: 0x00139F14
		internal void PointerImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "PointerImageHost");
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0013BD2D File Offset: 0x00139F2D
		internal void PointerImageHueColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0013BD3B File Offset: 0x00139F3B
		internal void PointerImageTransparency(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0013BD49 File Offset: 0x00139F49
		internal void PointerImageOffsetX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetXExpr", expression);
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0013BD57 File Offset: 0x00139F57
		internal void PointerImageOffsetY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetYExpr", expression);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0013BD65 File Offset: 0x00139F65
		internal void TopImageStart()
		{
			this.TypeStart("TopImage", "TopImageExprHost");
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0013BD77 File Offset: 0x00139F77
		internal void TopImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TopImageHost");
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0013BD90 File Offset: 0x00139F90
		internal void TopImageHueColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0013BD9E File Offset: 0x00139F9E
		internal void BackFrameStart()
		{
			this.TypeStart("BackFrame", "BackFrameExprHost");
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0013BDB0 File Offset: 0x00139FB0
		internal void BackFrameEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "BackFrameHost");
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0013BDC9 File Offset: 0x00139FC9
		internal void BackFrameFrameStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FrameStyleExpr", expression);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0013BDD7 File Offset: 0x00139FD7
		internal void BackFrameFrameShape(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FrameShapeExpr", expression);
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0013BDE5 File Offset: 0x00139FE5
		internal void BackFrameFrameWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FrameWidthExpr", expression);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0013BDF3 File Offset: 0x00139FF3
		internal void BackFrameGlassEffect(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GlassEffectExpr", expression);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0013BE01 File Offset: 0x0013A001
		internal void FrameBackgroundStart()
		{
			this.TypeStart("FrameBackground", "FrameBackgroundExprHost");
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0013BE13 File Offset: 0x0013A013
		internal void FrameBackgroundEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "FrameBackgroundHost");
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0013BE2C File Offset: 0x0013A02C
		internal void CustomLabelStart(string name)
		{
			this.TypeStart(this.CreateTypeName("CustomLabel" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).CustomLabels), "CustomLabelExprHost");
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x0013BE5A File Offset: 0x0013A05A
		internal int CustomLabelEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_customLabelsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).CustomLabels);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x0013BE87 File Offset: 0x0013A087
		internal void CustomLabelText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0013BE95 File Offset: 0x0013A095
		internal void CustomLabelAllowUpsideDown(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowUpsideDownExpr", expression);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0013BEA3 File Offset: 0x0013A0A3
		internal void CustomLabelDistanceFromScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0013BEB1 File Offset: 0x0013A0B1
		internal void CustomLabelFontAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FontAngleExpr", expression);
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0013BEBF File Offset: 0x0013A0BF
		internal void CustomLabelPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0013BECD File Offset: 0x0013A0CD
		internal void CustomLabelRotateLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotateLabelExpr", expression);
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0013BEDB File Offset: 0x0013A0DB
		internal void CustomLabelValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0013BEE9 File Offset: 0x0013A0E9
		internal void CustomLabelHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0013BEF7 File Offset: 0x0013A0F7
		internal void CustomLabelUseFontPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0013BF05 File Offset: 0x0013A105
		internal void GaugeClipContent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ClipContentExpr", expression);
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0013BF13 File Offset: 0x0013A113
		internal void GaugeImageStart(string name)
		{
			this.TypeStart(this.CreateTypeName("GaugeImage" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).GaugeImages), "GaugeImageExprHost");
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0013BF41 File Offset: 0x0013A141
		internal int GaugeImageEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_gaugeImagesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeImages);
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0013BF6E File Offset: 0x0013A16E
		internal void GaugeAspectRatio(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AspectRatioExpr", expression);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0013BF7C File Offset: 0x0013A17C
		internal void GaugeInputValueStart(int index)
		{
			this.TypeStart(this.CreateTypeName("GaugeInputValue" + index.ToString(CultureInfo.InvariantCulture), ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).GaugeInputValues), "GaugeInputValueExprHost");
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0013BFB5 File Offset: 0x0013A1B5
		internal int GaugeInputValueEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_gaugeInputValueHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeInputValues);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0013BFE2 File Offset: 0x0013A1E2
		internal void GaugeInputValueValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0013BFF0 File Offset: 0x0013A1F0
		internal void GaugeInputValueFormula(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FormulaExpr", expression);
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0013BFFE File Offset: 0x0013A1FE
		internal void GaugeInputValueMinPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinPercentExpr", expression);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0013C00C File Offset: 0x0013A20C
		internal void GaugeInputValueMaxPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxPercentExpr", expression);
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0013C01A File Offset: 0x0013A21A
		internal void GaugeInputValueMultiplier(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MultiplierExpr", expression);
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0013C028 File Offset: 0x0013A228
		internal void GaugeInputValueAddConstant(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AddConstantExpr", expression);
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0013C036 File Offset: 0x0013A236
		internal void GaugeLabelStart(string name)
		{
			this.TypeStart(this.CreateTypeName("GaugeLabel" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).GaugeLabels), "GaugeLabelExprHost");
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0013C064 File Offset: 0x0013A264
		internal int GaugeLabelEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_gaugeLabelsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeLabels);
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0013C091 File Offset: 0x0013A291
		internal void GaugeLabelText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0013C09F File Offset: 0x0013A29F
		internal void GaugeLabelAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x0013C0AD File Offset: 0x0013A2AD
		internal void GaugeLabelResizeMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0013C0BB File Offset: 0x0013A2BB
		internal void GaugeLabelTextShadowOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextShadowOffsetExpr", expression);
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0013C0C9 File Offset: 0x0013A2C9
		internal void GaugeLabelUseFontPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0013C0D7 File Offset: 0x0013A2D7
		internal void GaugePanelAntiAliasing(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AntiAliasingExpr", expression);
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0013C0E5 File Offset: 0x0013A2E5
		internal void GaugePanelAutoLayout(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoLayoutExpr", expression);
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0013C0F3 File Offset: 0x0013A2F3
		internal void GaugePanelShadowIntensity(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShadowIntensityExpr", expression);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0013C101 File Offset: 0x0013A301
		internal void GaugePanelTextAntiAliasingQuality(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextAntiAliasingQualityExpr", expression);
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0013C10F File Offset: 0x0013A30F
		internal void GaugePanelItemTop(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopExpr", expression);
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0013C11D File Offset: 0x0013A31D
		internal void GaugePanelItemLeft(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0013C12B File Offset: 0x0013A32B
		internal void GaugePanelItemHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeightExpr", expression);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0013C139 File Offset: 0x0013A339
		internal void GaugePanelItemWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0013C147 File Offset: 0x0013A347
		internal void GaugePanelItemZIndex(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ZIndexExpr", expression);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x0013C155 File Offset: 0x0013A355
		internal void GaugePanelItemHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0013C163 File Offset: 0x0013A363
		internal void GaugePanelItemToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0013C171 File Offset: 0x0013A371
		internal void GaugePointerBarStart(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BarStartExpr", expression);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0013C17F File Offset: 0x0013A37F
		internal void GaugePointerDistanceFromScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0013C18D File Offset: 0x0013A38D
		internal void GaugePointerMarkerLength(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkerLengthExpr", expression);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0013C19B File Offset: 0x0013A39B
		internal void GaugePointerMarkerStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkerStyleExpr", expression);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0013C1A9 File Offset: 0x0013A3A9
		internal void GaugePointerPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0013C1B7 File Offset: 0x0013A3B7
		internal void GaugePointerSnappingEnabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingEnabledExpr", expression);
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x0013C1C5 File Offset: 0x0013A3C5
		internal void GaugePointerSnappingInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingIntervalExpr", expression);
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x0013C1D3 File Offset: 0x0013A3D3
		internal void GaugePointerToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0013C1E1 File Offset: 0x0013A3E1
		internal void GaugePointerHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0013C1EF File Offset: 0x0013A3EF
		internal void GaugePointerWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0013C1FD File Offset: 0x0013A3FD
		internal void GaugeScaleInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x0013C20B File Offset: 0x0013A40B
		internal void GaugeScaleIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x0013C219 File Offset: 0x0013A419
		internal void GaugeScaleLogarithmic(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogarithmicExpr", expression);
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x0013C227 File Offset: 0x0013A427
		internal void GaugeScaleLogarithmicBase(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogarithmicBaseExpr", expression);
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0013C235 File Offset: 0x0013A435
		internal void GaugeScaleMultiplier(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MultiplierExpr", expression);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0013C243 File Offset: 0x0013A443
		internal void GaugeScaleReversed(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReversedExpr", expression);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0013C251 File Offset: 0x0013A451
		internal void GaugeScaleTickMarksOnTop(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TickMarksOnTopExpr", expression);
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0013C25F File Offset: 0x0013A45F
		internal void GaugeScaleToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x0013C26D File Offset: 0x0013A46D
		internal void GaugeScaleHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x0013C27B File Offset: 0x0013A47B
		internal void GaugeScaleWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x0013C289 File Offset: 0x0013A489
		internal void GaugeTickMarksStart(bool isMajor)
		{
			this.TypeStart("GaugeTickMarks" + (isMajor ? "GaugeMajorTickMarksHost" : "GaugeMinorTickMarksHost"), "GaugeTickMarksExprHost");
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x0013C2AF File Offset: 0x0013A4AF
		internal void GaugeTickMarksEnd(bool isMajor)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMajor ? "GaugeMajorTickMarksHost" : "GaugeMinorTickMarksHost");
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x0013C2D2 File Offset: 0x0013A4D2
		internal void TickMarkStyleStart()
		{
			this.TypeStart("TickMarkStyle", "TickMarkStyleExprHost");
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x0013C2E4 File Offset: 0x0013A4E4
		internal void TickMarkStyleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TickMarkStyleHost");
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0013C2FD File Offset: 0x0013A4FD
		internal void GaugeTickMarksInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0013C30B File Offset: 0x0013A50B
		internal void GaugeTickMarksIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0013C319 File Offset: 0x0013A519
		internal void LinearGaugeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("LinearGauge" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).LinearGauges), "LinearGaugeExprHost");
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0013C347 File Offset: 0x0013A547
		internal int LinearGaugeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_linearGaugesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).LinearGauges);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0013C374 File Offset: 0x0013A574
		internal void LinearGaugeOrientation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OrientationExpr", expression);
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x0013C382 File Offset: 0x0013A582
		internal void LinearPointerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("LinearPointer" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).LinearPointers), "LinearPointerExprHost");
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0013C3B0 File Offset: 0x0013A5B0
		internal int LinearPointerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_linearPointersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).LinearPointers);
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0013C3DD File Offset: 0x0013A5DD
		internal void LinearPointerType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0013C3EB File Offset: 0x0013A5EB
		internal void LinearScaleStart(string name)
		{
			this.TypeStart(this.CreateTypeName("LinearScale" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).LinearScales), "LinearScaleExprHost");
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0013C419 File Offset: 0x0013A619
		internal int LinearScaleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_linearScalesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).LinearScales);
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0013C446 File Offset: 0x0013A646
		internal void LinearScaleStartMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartMarginExpr", expression);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0013C454 File Offset: 0x0013A654
		internal void LinearScaleEndMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndMarginExpr", expression);
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0013C462 File Offset: 0x0013A662
		internal void LinearScalePosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PositionExpr", expression);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0013C470 File Offset: 0x0013A670
		internal void NumericIndicatorStart(string name)
		{
			this.TypeStart(this.CreateTypeName("NumericIndicator" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).NumericIndicators), "NumericIndicatorExprHost");
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0013C49E File Offset: 0x0013A69E
		internal int NumericIndicatorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_numericIndicatorsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).NumericIndicators);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0013C4CB File Offset: 0x0013A6CB
		internal void NumericIndicatorDecimalDigitColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DecimalDigitColorExpr", expression);
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x0013C4D9 File Offset: 0x0013A6D9
		internal void NumericIndicatorDigitColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DigitColorExpr", expression);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0013C4E7 File Offset: 0x0013A6E7
		internal void NumericIndicatorUseFontPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x0013C4F5 File Offset: 0x0013A6F5
		internal void NumericIndicatorDecimalDigits(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DecimalDigitsExpr", expression);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0013C503 File Offset: 0x0013A703
		internal void NumericIndicatorDigits(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DigitsExpr", expression);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0013C511 File Offset: 0x0013A711
		internal void NumericIndicatorMultiplier(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MultiplierExpr", expression);
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0013C51F File Offset: 0x0013A71F
		internal void NumericIndicatorNonNumericString(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NonNumericStringExpr", expression);
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0013C52D File Offset: 0x0013A72D
		internal void NumericIndicatorOutOfRangeString(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OutOfRangeStringExpr", expression);
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0013C53B File Offset: 0x0013A73B
		internal void NumericIndicatorResizeMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0013C549 File Offset: 0x0013A749
		internal void NumericIndicatorShowDecimalPoint(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowDecimalPointExpr", expression);
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0013C557 File Offset: 0x0013A757
		internal void NumericIndicatorShowLeadingZeros(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowLeadingZerosExpr", expression);
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x0013C565 File Offset: 0x0013A765
		internal void NumericIndicatorIndicatorStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IndicatorStyleExpr", expression);
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x0013C573 File Offset: 0x0013A773
		internal void NumericIndicatorShowSign(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowSignExpr", expression);
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x0013C581 File Offset: 0x0013A781
		internal void NumericIndicatorSnappingEnabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingEnabledExpr", expression);
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x0013C58F File Offset: 0x0013A78F
		internal void NumericIndicatorSnappingInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingIntervalExpr", expression);
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x0013C59D File Offset: 0x0013A79D
		internal void NumericIndicatorLedDimColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LedDimColorExpr", expression);
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0013C5AB File Offset: 0x0013A7AB
		internal void NumericIndicatorSeparatorWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorWidthExpr", expression);
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0013C5B9 File Offset: 0x0013A7B9
		internal void NumericIndicatorSeparatorColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorColorExpr", expression);
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0013C5C7 File Offset: 0x0013A7C7
		internal void NumericIndicatorRangeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("NumericIndicatorRange" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).NumericIndicatorRanges), "NumericIndicatorRangeExprHost");
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0013C5F5 File Offset: 0x0013A7F5
		internal int NumericIndicatorRangeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_numericIndicatorRangesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).NumericIndicatorRanges);
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0013C622 File Offset: 0x0013A822
		internal void NumericIndicatorRangeDecimalDigitColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DecimalDigitColorExpr", expression);
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0013C630 File Offset: 0x0013A830
		internal void NumericIndicatorRangeDigitColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DigitColorExpr", expression);
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0013C63E File Offset: 0x0013A83E
		internal void PinLabelStart()
		{
			this.TypeStart("PinLabel", "PinLabelExprHost");
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0013C650 File Offset: 0x0013A850
		internal void PinLabelEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "PinLabelHost");
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0013C669 File Offset: 0x0013A869
		internal void PinLabelText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0013C677 File Offset: 0x0013A877
		internal void PinLabelAllowUpsideDown(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowUpsideDownExpr", expression);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0013C685 File Offset: 0x0013A885
		internal void PinLabelDistanceFromScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0013C693 File Offset: 0x0013A893
		internal void PinLabelFontAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FontAngleExpr", expression);
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0013C6A1 File Offset: 0x0013A8A1
		internal void PinLabelPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0013C6AF File Offset: 0x0013A8AF
		internal void PinLabelRotateLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotateLabelExpr", expression);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0013C6BD File Offset: 0x0013A8BD
		internal void PinLabelUseFontPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0013C6CB File Offset: 0x0013A8CB
		internal void PointerCapStart()
		{
			this.TypeStart("PointerCap", "PointerCapExprHost");
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0013C6DD File Offset: 0x0013A8DD
		internal void PointerCapEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "PointerCapHost");
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0013C6F6 File Offset: 0x0013A8F6
		internal void PointerCapOnTop(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OnTopExpr", expression);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0013C704 File Offset: 0x0013A904
		internal void PointerCapReflection(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReflectionExpr", expression);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0013C712 File Offset: 0x0013A912
		internal void PointerCapCapStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CapStyleExpr", expression);
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0013C720 File Offset: 0x0013A920
		internal void PointerCapHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0013C72E File Offset: 0x0013A92E
		internal void PointerCapWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0013C73C File Offset: 0x0013A93C
		internal void RadialGaugeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("RadialGauge" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).RadialGauges), "RadialGaugeExprHost");
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0013C76A File Offset: 0x0013A96A
		internal int RadialGaugeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_radialGaugesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).RadialGauges);
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0013C797 File Offset: 0x0013A997
		internal void RadialGaugePivotX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PivotXExpr", expression);
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0013C7A5 File Offset: 0x0013A9A5
		internal void RadialGaugePivotY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PivotYExpr", expression);
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0013C7B3 File Offset: 0x0013A9B3
		internal void RadialPointerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("RadialPointer" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).RadialPointers), "RadialPointerExprHost");
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0013C7E1 File Offset: 0x0013A9E1
		internal int RadialPointerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_radialPointersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).RadialPointers);
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x0013C80E File Offset: 0x0013AA0E
		internal void RadialPointerType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0013C81C File Offset: 0x0013AA1C
		internal void RadialPointerNeedleStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NeedleStyleExpr", expression);
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0013C82A File Offset: 0x0013AA2A
		internal void RadialScaleStart(string name)
		{
			this.TypeStart(this.CreateTypeName("RadialScale" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).RadialScales), "RadialScaleExprHost");
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0013C858 File Offset: 0x0013AA58
		internal int RadialScaleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_radialScalesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).RadialScales);
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0013C885 File Offset: 0x0013AA85
		internal void RadialScaleRadius(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RadiusExpr", expression);
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x0013C893 File Offset: 0x0013AA93
		internal void RadialScaleStartAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartAngleExpr", expression);
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0013C8A1 File Offset: 0x0013AAA1
		internal void RadialScaleSweepAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SweepAngleExpr", expression);
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x0013C8AF File Offset: 0x0013AAAF
		internal void ScaleLabelsStart()
		{
			this.TypeStart("ScaleLabels", "ScaleLabelsExprHost");
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0013C8C1 File Offset: 0x0013AAC1
		internal void ScaleLabelsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ScaleLabelsHost");
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0013C8DA File Offset: 0x0013AADA
		internal void ScaleLabelsInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0013C8E8 File Offset: 0x0013AAE8
		internal void ScaleLabelsIntervalOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0013C8F6 File Offset: 0x0013AAF6
		internal void ScaleLabelsAllowUpsideDown(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowUpsideDownExpr", expression);
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0013C904 File Offset: 0x0013AB04
		internal void ScaleLabelsDistanceFromScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0013C912 File Offset: 0x0013AB12
		internal void ScaleLabelsFontAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FontAngleExpr", expression);
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0013C920 File Offset: 0x0013AB20
		internal void ScaleLabelsPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		// Token: 0x06004C64 RID: 19556 RVA: 0x0013C92E File Offset: 0x0013AB2E
		internal void ScaleLabelsRotateLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotateLabelsExpr", expression);
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0013C93C File Offset: 0x0013AB3C
		internal void ScaleLabelsShowEndLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowEndLabelsExpr", expression);
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x0013C94A File Offset: 0x0013AB4A
		internal void ScaleLabelsHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x0013C958 File Offset: 0x0013AB58
		internal void ScaleLabelsUseFontPercent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x0013C966 File Offset: 0x0013AB66
		internal void ScalePinStart(bool isMaximum)
		{
			this.TypeStart("ScalePin" + (isMaximum ? "MaximumPinHost" : "MinimumPinHost"), "ScalePinExprHost");
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x0013C98C File Offset: 0x0013AB8C
		internal void ScalePinEnd(bool isMaximum)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMaximum ? "MaximumPinHost" : "MinimumPinHost");
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x0013C9AF File Offset: 0x0013ABAF
		internal void ScalePinLocation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LocationExpr", expression);
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x0013C9BD File Offset: 0x0013ABBD
		internal void ScalePinEnable(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnableExpr", expression);
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0013C9CB File Offset: 0x0013ABCB
		internal void ScaleRangeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ScaleRange" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).ScaleRanges), "ScaleRangeExprHost");
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x0013C9F9 File Offset: 0x0013ABF9
		internal int ScaleRangeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_scaleRangesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).ScaleRanges);
		}

		// Token: 0x06004C6E RID: 19566 RVA: 0x0013CA26 File Offset: 0x0013AC26
		internal void ScaleRangeDistanceFromScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		// Token: 0x06004C6F RID: 19567 RVA: 0x0013CA34 File Offset: 0x0013AC34
		internal void ScaleRangeStartWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartWidthExpr", expression);
		}

		// Token: 0x06004C70 RID: 19568 RVA: 0x0013CA42 File Offset: 0x0013AC42
		internal void ScaleRangeEndWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndWidthExpr", expression);
		}

		// Token: 0x06004C71 RID: 19569 RVA: 0x0013CA50 File Offset: 0x0013AC50
		internal void ScaleRangeInRangeBarPointerColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InRangeBarPointerColorExpr", expression);
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x0013CA5E File Offset: 0x0013AC5E
		internal void ScaleRangeInRangeLabelColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InRangeLabelColorExpr", expression);
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x0013CA6C File Offset: 0x0013AC6C
		internal void ScaleRangeInRangeTickMarksColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InRangeTickMarksColorExpr", expression);
		}

		// Token: 0x06004C74 RID: 19572 RVA: 0x0013CA7A File Offset: 0x0013AC7A
		internal void ScaleRangeBackgroundGradientType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BackgroundGradientTypeExpr", expression);
		}

		// Token: 0x06004C75 RID: 19573 RVA: 0x0013CA88 File Offset: 0x0013AC88
		internal void ScaleRangePlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		// Token: 0x06004C76 RID: 19574 RVA: 0x0013CA96 File Offset: 0x0013AC96
		internal void ScaleRangeToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0013CAA4 File Offset: 0x0013ACA4
		internal void ScaleRangeHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C78 RID: 19576 RVA: 0x0013CAB2 File Offset: 0x0013ACB2
		internal void IndicatorImageStart()
		{
			this.TypeStart("IndicatorImage", "IndicatorImageExprHost");
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x0013CAC4 File Offset: 0x0013ACC4
		internal void IndicatorImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "IndicatorImageHost");
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0013CADD File Offset: 0x0013ACDD
		internal void IndicatorImageHueColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0013CAEB File Offset: 0x0013ACEB
		internal void IndicatorImageTransparency(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0013CAF9 File Offset: 0x0013ACF9
		internal void StateIndicatorStart(string name)
		{
			this.TypeStart(this.CreateTypeName("StateIndicator" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).StateIndicators), "StateIndicatorExprHost");
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0013CB27 File Offset: 0x0013AD27
		internal int StateIndicatorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_stateIndicatorsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).StateIndicators);
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0013CB54 File Offset: 0x0013AD54
		internal void StateIndicatorIndicatorStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IndicatorStyleExpr", expression);
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0013CB62 File Offset: 0x0013AD62
		internal void StateIndicatorScaleFactor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleFactorExpr", expression);
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0013CB70 File Offset: 0x0013AD70
		internal void StateIndicatorResizeMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0013CB7E File Offset: 0x0013AD7E
		internal void StateIndicatorAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0013CB8C File Offset: 0x0013AD8C
		internal void StateIndicatorTransformationType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransformationTypeExpr", expression);
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x0013CB9A File Offset: 0x0013AD9A
		internal void ThermometerStart()
		{
			this.TypeStart("Thermometer", "ThermometerExprHost");
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x0013CBAC File Offset: 0x0013ADAC
		internal void ThermometerEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ThermometerHost");
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0013CBC5 File Offset: 0x0013ADC5
		internal void ThermometerBulbOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BulbOffsetExpr", expression);
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0013CBD3 File Offset: 0x0013ADD3
		internal void ThermometerBulbSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BulbSizeExpr", expression);
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x0013CBE1 File Offset: 0x0013ADE1
		internal void ThermometerThermometerStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ThermometerStyleExpr", expression);
		}

		// Token: 0x06004C88 RID: 19592 RVA: 0x0013CBEF File Offset: 0x0013ADEF
		internal void TickMarkStyleDistanceFromScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x0013CBFD File Offset: 0x0013ADFD
		internal void TickMarkStylePlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		// Token: 0x06004C8A RID: 19594 RVA: 0x0013CC0B File Offset: 0x0013AE0B
		internal void TickMarkStyleEnableGradient(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnableGradientExpr", expression);
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x0013CC19 File Offset: 0x0013AE19
		internal void TickMarkStyleGradientDensity(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GradientDensityExpr", expression);
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0013CC27 File Offset: 0x0013AE27
		internal void TickMarkStyleLength(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LengthExpr", expression);
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0013CC35 File Offset: 0x0013AE35
		internal void TickMarkStyleWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0013CC43 File Offset: 0x0013AE43
		internal void TickMarkStyleShape(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShapeExpr", expression);
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x0013CC51 File Offset: 0x0013AE51
		internal void TickMarkStyleHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0013CC5F File Offset: 0x0013AE5F
		internal void IndicatorStateStart(string name)
		{
			this.TypeStart(this.CreateTypeName("IndicatorState" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).IndicatorStates), "IndicatorStateExprHost");
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0013CC8D File Offset: 0x0013AE8D
		internal int IndicatorStateEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_indicatorStatesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).IndicatorStates);
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0013CCBA File Offset: 0x0013AEBA
		internal void IndicatorStateColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorExpr", expression);
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0013CCC8 File Offset: 0x0013AEC8
		internal void IndicatorStateScaleFactor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleFactorExpr", expression);
		}

		// Token: 0x06004C94 RID: 19604 RVA: 0x0013CCD6 File Offset: 0x0013AED6
		internal void IndicatorStateIndicatorStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IndicatorStyleExpr", expression);
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x0013CCE4 File Offset: 0x0013AEE4
		internal void MapViewZoom(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ZoomExpr", expression);
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x0013CCF2 File Offset: 0x0013AEF2
		internal void MapElementViewStart()
		{
			this.TypeStart("MapElementView", "MapElementViewExprHost");
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0013CD04 File Offset: 0x0013AF04
		internal void MapElementViewEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewHost");
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0013CD1D File Offset: 0x0013AF1D
		internal void MapElementViewLayerName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LayerNameExpr", expression);
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0013CD2B File Offset: 0x0013AF2B
		internal void MapCustomViewStart()
		{
			this.TypeStart("MapCustomView", "MapCustomViewExprHost");
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x0013CD3D File Offset: 0x0013AF3D
		internal void MapCustomViewEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewHost");
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0013CD56 File Offset: 0x0013AF56
		internal void MapCustomViewCenterX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterXExpr", expression);
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x0013CD64 File Offset: 0x0013AF64
		internal void MapCustomViewCenterY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterYExpr", expression);
		}

		// Token: 0x06004C9D RID: 19613 RVA: 0x0013CD72 File Offset: 0x0013AF72
		internal void MapDataBoundViewStart()
		{
			this.TypeStart("MapDataBoundView", "MapDataBoundViewExprHost");
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0013CD84 File Offset: 0x0013AF84
		internal void MapDataBoundViewEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewHost");
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0013CD9D File Offset: 0x0013AF9D
		internal void MapBorderSkinStart()
		{
			this.TypeStart("MapBorderSkin", "MapBorderSkinExprHost");
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0013CDAF File Offset: 0x0013AFAF
		internal void MapBorderSkinEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapBorderSkinHost");
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0013CDC8 File Offset: 0x0013AFC8
		internal void MapBorderSkinMapBorderSkinType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapBorderSkinTypeExpr", expression);
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0013CDD6 File Offset: 0x0013AFD6
		internal void MapAntiAliasing(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AntiAliasingExpr", expression);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0013CDE4 File Offset: 0x0013AFE4
		internal void MapTextAntiAliasingQuality(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextAntiAliasingQualityExpr", expression);
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0013CDF2 File Offset: 0x0013AFF2
		internal void MapShadowIntensity(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShadowIntensityExpr", expression);
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x0013CE00 File Offset: 0x0013B000
		internal void MapTileLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TileLanguageExpr", expression);
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0013CE0E File Offset: 0x0013B00E
		internal void MapVectorLayerMapDataRegionName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapDataRegionNameExpr", expression);
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0013CE1C File Offset: 0x0013B01C
		internal void MapTileLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapTileLayer" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapTileLayers), "MapTileLayerExprHost");
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0013CE4A File Offset: 0x0013B04A
		internal int MapTileLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapTileLayersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapTileLayers);
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0013CE77 File Offset: 0x0013B077
		internal void MapTileLayerServiceUrl(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ServiceUrlExpr", expression);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0013CE85 File Offset: 0x0013B085
		internal void MapTileLayerTileStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TileStyleExpr", expression);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0013CE93 File Offset: 0x0013B093
		internal void MapTileLayerUseSecureConnection(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseSecureConnectionExpr", expression);
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x0013CEA1 File Offset: 0x0013B0A1
		internal void MapTileStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapTile" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapTiles), "MapTileExprHost");
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0013CECF File Offset: 0x0013B0CF
		internal int MapTileEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapTilesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapTiles);
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0013CEFC File Offset: 0x0013B0FC
		internal void MapPointLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPointLayer" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapPointLayers), "MapPointLayerExprHost");
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0013CF2A File Offset: 0x0013B12A
		internal int MapPointLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPointLayersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPointLayers);
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0013CF57 File Offset: 0x0013B157
		internal void MapSpatialDataSetStart()
		{
			this.TypeStart("MapSpatialDataSet", "MapSpatialDataSetExprHost");
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0013CF69 File Offset: 0x0013B169
		internal void MapSpatialDataSetEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSpatialDataHost");
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0013CF82 File Offset: 0x0013B182
		internal void MapSpatialDataSetDataSetName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataSetNameExpr", expression);
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0013CF90 File Offset: 0x0013B190
		internal void MapSpatialDataSetSpatialField(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpatialFieldExpr", expression);
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0013CF9E File Offset: 0x0013B19E
		internal void MapSpatialDataRegionStart()
		{
			this.TypeStart("MapSpatialDataRegion", "MapSpatialDataRegionExprHost");
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0013CFB0 File Offset: 0x0013B1B0
		internal void MapSpatialDataRegionEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSpatialDataHost");
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x0013CFC9 File Offset: 0x0013B1C9
		internal void MapSpatialDataRegionVectorData(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VectorDataExpr", expression);
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0013CFD7 File Offset: 0x0013B1D7
		internal void MapPolygonLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPolygonLayer" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapPolygonLayers), "MapPolygonLayerExprHost");
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0013D005 File Offset: 0x0013B205
		internal int MapPolygonLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPolygonLayersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPolygonLayers);
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0013D032 File Offset: 0x0013B232
		internal void MapShapefileStart()
		{
			this.TypeStart("MapShapefile", "MapShapefileExprHost");
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0013D044 File Offset: 0x0013B244
		internal void MapShapefileEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSpatialDataHost");
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0013D05D File Offset: 0x0013B25D
		internal void MapShapefileSource(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0013D06B File Offset: 0x0013B26B
		internal void MapLineLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapLineLayer" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapLineLayers), "MapLineLayerExprHost");
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0013D099 File Offset: 0x0013B299
		internal int MapLineLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapLineLayersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapLineLayers);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0013D0C6 File Offset: 0x0013B2C6
		internal void MapLayerVisibilityMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibilityModeExpr", expression);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0013D0D4 File Offset: 0x0013B2D4
		internal void MapLayerMinimumZoom(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumZoomExpr", expression);
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0013D0E2 File Offset: 0x0013B2E2
		internal void MapLayerMaximumZoom(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumZoomExpr", expression);
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0013D0F0 File Offset: 0x0013B2F0
		internal void MapLayerTransparency(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0013D0FE File Offset: 0x0013B2FE
		internal void MapFieldNameStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapFieldName" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapFieldNames), "MapFieldNameExprHost");
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0013D12C File Offset: 0x0013B32C
		internal int MapFieldNameEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapFieldNamesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapFieldNames);
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0013D159 File Offset: 0x0013B359
		internal void MapFieldNameName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NameExpr", expression);
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0013D167 File Offset: 0x0013B367
		internal void MapPointStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPoint" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapPoints), "MapPointExprHost");
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x0013D195 File Offset: 0x0013B395
		internal int MapPointEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPointsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPoints);
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0013D1C2 File Offset: 0x0013B3C2
		internal void MapPointUseCustomPointTemplate(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomPointTemplateExpr", expression);
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0013D1D0 File Offset: 0x0013B3D0
		internal void MapPolygonStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPolygon" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapPolygons), "MapPolygonExprHost");
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x0013D1FE File Offset: 0x0013B3FE
		internal int MapPolygonEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPolygonsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPolygons);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0013D22B File Offset: 0x0013B42B
		internal void MapPolygonUseCustomPolygonTemplate(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomPolygonTemplateExpr", expression);
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0013D239 File Offset: 0x0013B439
		internal void MapPolygonUseCustomCenterPointTemplate(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomPointTemplateExpr", expression);
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0013D247 File Offset: 0x0013B447
		internal void MapLineStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapLine" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapLines), "MapLineExprHost");
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x0013D275 File Offset: 0x0013B475
		internal int MapLineEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapLinesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapLines);
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x0013D2A2 File Offset: 0x0013B4A2
		internal void MapLineUseCustomLineTemplate(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomLineTemplateExpr", expression);
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x0013D2B0 File Offset: 0x0013B4B0
		internal void MapSpatialElementTemplateHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x0013D2BE File Offset: 0x0013B4BE
		internal void MapSpatialElementTemplateOffsetX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetXExpr", expression);
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x0013D2CC File Offset: 0x0013B4CC
		internal void MapSpatialElementTemplateOffsetY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetYExpr", expression);
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0013D2DA File Offset: 0x0013B4DA
		internal void MapSpatialElementTemplateLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x0013D2E8 File Offset: 0x0013B4E8
		internal void MapSpatialElementTemplateDataElementLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataElementLabelExpr", expression);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x0013D2F6 File Offset: 0x0013B4F6
		internal void MapSpatialElementTemplateToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x0013D304 File Offset: 0x0013B504
		internal void MapPointTemplateSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SizeExpr", expression);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0013D312 File Offset: 0x0013B512
		internal void MapPointTemplateLabelPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0013D320 File Offset: 0x0013B520
		internal void MapMarkerTemplateStart()
		{
			this.TypeStart("MapMarkerTemplate", "MapMarkerTemplateExprHost");
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0013D332 File Offset: 0x0013B532
		internal void MapMarkerTemplateEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPointTemplateHost");
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0013D34B File Offset: 0x0013B54B
		internal void MapPolygonTemplateStart()
		{
			this.TypeStart("MapPolygonTemplate", "MapPolygonTemplateExprHost");
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0013D35D File Offset: 0x0013B55D
		internal void MapPolygonTemplateEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPolygonTemplateHost");
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0013D376 File Offset: 0x0013B576
		internal void MapPolygonTemplateScaleFactor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleFactorExpr", expression);
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0013D384 File Offset: 0x0013B584
		internal void MapPolygonTemplateCenterPointOffsetX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterPointOffsetXExpr", expression);
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0013D392 File Offset: 0x0013B592
		internal void MapPolygonTemplateCenterPointOffsetY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterPointOffsetYExpr", expression);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0013D3A0 File Offset: 0x0013B5A0
		internal void MapPolygonTemplateShowLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowLabelExpr", expression);
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0013D3AE File Offset: 0x0013B5AE
		internal void MapPolygonTemplateLabelPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0013D3BC File Offset: 0x0013B5BC
		internal void MapLineTemplateStart()
		{
			this.TypeStart("MapLineTemplate", "MapLineTemplateExprHost");
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0013D3CE File Offset: 0x0013B5CE
		internal void MapLineTemplateEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLineTemplateHost");
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0013D3E7 File Offset: 0x0013B5E7
		internal void MapLineTemplateWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0013D3F5 File Offset: 0x0013B5F5
		internal void MapLineTemplateLabelPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0013D403 File Offset: 0x0013B603
		internal void MapCustomColorRuleStart()
		{
			this.TypeStart("MapCustomColorRule", "MapCustomColorRuleExprHost");
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0013D415 File Offset: 0x0013B615
		internal void MapCustomColorRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorRuleHost");
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0013D42E File Offset: 0x0013B62E
		internal void MapCustomColorStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapCustomColor" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapCustomColors), "MapCustomColorExprHost");
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0013D45C File Offset: 0x0013B65C
		internal int MapCustomColorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapCustomColorsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapCustomColors);
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0013D489 File Offset: 0x0013B689
		internal void MapCustomColorColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorExpr", expression);
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0013D497 File Offset: 0x0013B697
		internal void MapPointRulesStart()
		{
			this.TypeStart("MapPointRules", "MapPointRulesExprHost");
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0013D4A9 File Offset: 0x0013B6A9
		internal void MapPointRulesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPointRulesHost");
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0013D4C2 File Offset: 0x0013B6C2
		internal void MapMarkerRuleStart()
		{
			this.TypeStart("MapMarkerRule", "MapMarkerRuleExprHost");
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0013D4D4 File Offset: 0x0013B6D4
		internal void MapMarkerRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapMarkerRuleHost");
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0013D4ED File Offset: 0x0013B6ED
		internal void MapMarkerStart()
		{
			this.TypeStart("MapMarker", "MapMarkerExprHost");
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0013D4FF File Offset: 0x0013B6FF
		internal void MapMarkerEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapMarkerHost");
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0013D518 File Offset: 0x0013B718
		internal void MapMarkerInCollectionStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapMarker" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapMarkers), "MapMarkerExprHost");
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0013D546 File Offset: 0x0013B746
		internal int MapMarkerInCollectionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapMarkersHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapMarkers);
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0013D573 File Offset: 0x0013B773
		internal void MapMarkerMapMarkerStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapMarkerStyleExpr", expression);
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0013D581 File Offset: 0x0013B781
		internal void MapMarkerImageStart()
		{
			this.TypeStart("MapMarkerImage", "MapMarkerImageExprHost");
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0013D593 File Offset: 0x0013B793
		internal void MapMarkerImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapMarkerImageHost");
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0013D5AC File Offset: 0x0013B7AC
		internal void MapMarkerImageSource(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0013D5BA File Offset: 0x0013B7BA
		internal void MapMarkerImageValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0013D5C8 File Offset: 0x0013B7C8
		internal void MapMarkerImageMIMEType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0013D5D6 File Offset: 0x0013B7D6
		internal void MapMarkerImageTransparentColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparentColorExpr", expression);
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0013D5E4 File Offset: 0x0013B7E4
		internal void MapMarkerImageResizeMode(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x0013D5F2 File Offset: 0x0013B7F2
		internal void MapSizeRuleStart()
		{
			this.TypeStart("MapSizeRule", "MapSizeRuleExprHost");
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0013D604 File Offset: 0x0013B804
		internal void MapSizeRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSizeRuleHost");
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0013D61D File Offset: 0x0013B81D
		internal void MapSizeRuleStartSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartSizeExpr", expression);
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0013D62B File Offset: 0x0013B82B
		internal void MapSizeRuleEndSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndSizeExpr", expression);
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0013D639 File Offset: 0x0013B839
		internal void MapPolygonRulesStart()
		{
			this.TypeStart("MapPolygonRules", "MapPolygonRulesExprHost");
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0013D64B File Offset: 0x0013B84B
		internal void MapPolygonRulesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPolygonRulesHost");
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0013D664 File Offset: 0x0013B864
		internal void MapLineRulesStart()
		{
			this.TypeStart("MapLineRules", "MapLineRulesExprHost");
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x0013D676 File Offset: 0x0013B876
		internal void MapLineRulesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLineRulesHost");
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0013D68F File Offset: 0x0013B88F
		internal void MapColorRuleShowInColorScale(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowInColorScaleExpr", expression);
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x0013D69D File Offset: 0x0013B89D
		internal void MapColorRangeRuleStart()
		{
			this.TypeStart("MapColorRangeRule", "MapColorRangeRuleExprHost");
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0013D6AF File Offset: 0x0013B8AF
		internal void MapColorRangeRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorRuleHost");
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0013D6C8 File Offset: 0x0013B8C8
		internal void MapColorRangeRuleStartColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartColorExpr", expression);
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0013D6D6 File Offset: 0x0013B8D6
		internal void MapColorRangeRuleMiddleColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MiddleColorExpr", expression);
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0013D6E4 File Offset: 0x0013B8E4
		internal void MapColorRangeRuleEndColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndColorExpr", expression);
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0013D6F2 File Offset: 0x0013B8F2
		internal void MapColorPaletteRuleStart()
		{
			this.TypeStart("MapColorPaletteRule", "MapColorPaletteRuleExprHost");
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0013D704 File Offset: 0x0013B904
		internal void MapColorPaletteRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorRuleHost");
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0013D71D File Offset: 0x0013B91D
		internal void MapColorPaletteRulePalette(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PaletteExpr", expression);
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0013D72B File Offset: 0x0013B92B
		internal void MapBucketStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapBucket" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapBuckets), "MapBucketExprHost");
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x0013D759 File Offset: 0x0013B959
		internal int MapBucketEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapBucketsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapBuckets);
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x0013D786 File Offset: 0x0013B986
		internal void MapBucketStartValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartValueExpr", expression);
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x0013D794 File Offset: 0x0013B994
		internal void MapBucketEndValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndValueExpr", expression);
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x0013D7A2 File Offset: 0x0013B9A2
		internal void MapAppearanceRuleDataValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueExpr", expression);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x0013D7B0 File Offset: 0x0013B9B0
		internal void MapAppearanceRuleDistributionType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistributionTypeExpr", expression);
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x0013D7BE File Offset: 0x0013B9BE
		internal void MapAppearanceRuleBucketCount(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BucketCountExpr", expression);
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x0013D7CC File Offset: 0x0013B9CC
		internal void MapAppearanceRuleStartValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartValueExpr", expression);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0013D7DA File Offset: 0x0013B9DA
		internal void MapAppearanceRuleEndValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndValueExpr", expression);
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0013D7E8 File Offset: 0x0013B9E8
		internal void MapAppearanceRuleLegendText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendTextExpr", expression);
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x0013D7F6 File Offset: 0x0013B9F6
		internal void MapLegendTitleStart()
		{
			this.TypeStart("MapLegendTitle", "MapLegendTitleExprHost");
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x0013D808 File Offset: 0x0013BA08
		internal void MapLegendTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLegendTitleHost");
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0013D821 File Offset: 0x0013BA21
		internal void MapLegendTitleCaption(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0013D82F File Offset: 0x0013BA2F
		internal void MapLegendTitleTitleSeparator(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleSeparatorExpr", expression);
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x0013D83D File Offset: 0x0013BA3D
		internal void MapLegendTitleTitleSeparatorColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleSeparatorColorExpr", expression);
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x0013D84B File Offset: 0x0013BA4B
		internal void MapLegendStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapLegend" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapLegends), "MapLegendExprHost");
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x0013D879 File Offset: 0x0013BA79
		internal int MapLegendEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapLegendsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapLegends);
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x0013D8A6 File Offset: 0x0013BAA6
		internal void MapLegendLayout(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LayoutExpr", expression);
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0013D8B4 File Offset: 0x0013BAB4
		internal void MapLegendAutoFitTextDisabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoFitTextDisabledExpr", expression);
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x0013D8C2 File Offset: 0x0013BAC2
		internal void MapLegendMinFontSize(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinFontSizeExpr", expression);
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x0013D8D0 File Offset: 0x0013BAD0
		internal void MapLegendInterlacedRows(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsExpr", expression);
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x0013D8DE File Offset: 0x0013BADE
		internal void MapLegendInterlacedRowsColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsColorExpr", expression);
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x0013D8EC File Offset: 0x0013BAEC
		internal void MapLegendEquallySpacedItems(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EquallySpacedItemsExpr", expression);
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0013D8FA File Offset: 0x0013BAFA
		internal void MapLegendTextWrapThreshold(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextWrapThresholdExpr", expression);
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0013D908 File Offset: 0x0013BB08
		internal void MapTitleStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapTitle" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapTitles), "MapTitleExprHost");
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x0013D936 File Offset: 0x0013BB36
		internal int MapTitleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapTitlesHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapTitles);
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x0013D963 File Offset: 0x0013BB63
		internal void MapTitleText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x0013D971 File Offset: 0x0013BB71
		internal void MapTitleAngle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0013D97F File Offset: 0x0013BB7F
		internal void MapTitleTextShadowOffset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextShadowOffsetExpr", expression);
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x0013D98D File Offset: 0x0013BB8D
		internal void MapDistanceScaleStart()
		{
			this.TypeStart("MapDistanceScale", "MapDistanceScaleExprHost");
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0013D99F File Offset: 0x0013BB9F
		internal void MapDistanceScaleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapDistanceScaleHost");
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0013D9B8 File Offset: 0x0013BBB8
		internal void MapDistanceScaleScaleColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleColorExpr", expression);
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x0013D9C6 File Offset: 0x0013BBC6
		internal void MapDistanceScaleScaleBorderColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleBorderColorExpr", expression);
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0013D9D4 File Offset: 0x0013BBD4
		internal void MapColorScaleTitleStart()
		{
			this.TypeStart("MapColorScaleTitle", "MapColorScaleTitleExprHost");
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0013D9E6 File Offset: 0x0013BBE6
		internal void MapColorScaleTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorScaleTitleHost");
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0013D9FF File Offset: 0x0013BBFF
		internal void MapColorScaleTitleCaption(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0013DA0D File Offset: 0x0013BC0D
		internal void MapColorScaleStart()
		{
			this.TypeStart("MapColorScale", "MapColorScaleExprHost");
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0013DA1F File Offset: 0x0013BC1F
		internal void MapColorScaleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorScaleHost");
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0013DA38 File Offset: 0x0013BC38
		internal void MapColorScaleTickMarkLength(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TickMarkLengthExpr", expression);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0013DA46 File Offset: 0x0013BC46
		internal void MapColorScaleColorBarBorderColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorBarBorderColorExpr", expression);
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0013DA54 File Offset: 0x0013BC54
		internal void MapColorScaleLabelInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalExpr", expression);
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0013DA62 File Offset: 0x0013BC62
		internal void MapColorScaleLabelFormat(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelFormatExpr", expression);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0013DA70 File Offset: 0x0013BC70
		internal void MapColorScaleLabelPlacement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0013DA7E File Offset: 0x0013BC7E
		internal void MapColorScaleLabelBehavior(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelBehaviorExpr", expression);
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0013DA8C File Offset: 0x0013BC8C
		internal void MapColorScaleHideEndLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideEndLabelsExpr", expression);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x0013DA9A File Offset: 0x0013BC9A
		internal void MapColorScaleRangeGapColor(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RangeGapColorExpr", expression);
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0013DAA8 File Offset: 0x0013BCA8
		internal void MapColorScaleNoDataText(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NoDataTextExpr", expression);
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x0013DAB6 File Offset: 0x0013BCB6
		internal void MapStart(string name)
		{
			this.TypeStart(name, "MapExprHost");
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0013DAC4 File Offset: 0x0013BCC4
		internal int MapEnd()
		{
			return this.ReportItemEnd("m_mapHostsRemotable", ref this.m_rootTypeDecl.Maps);
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0013DADC File Offset: 0x0013BCDC
		internal void MapLocationStart()
		{
			this.TypeStart("MapLocation", "MapLocationExprHost");
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0013DAEE File Offset: 0x0013BCEE
		internal void MapLocationEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLocationHost");
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0013DB07 File Offset: 0x0013BD07
		internal void MapLocationLeft(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0013DB15 File Offset: 0x0013BD15
		internal void MapLocationTop(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopExpr", expression);
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x0013DB23 File Offset: 0x0013BD23
		internal void MapLocationUnit(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UnitExpr", expression);
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x0013DB31 File Offset: 0x0013BD31
		internal void MapSizeStart()
		{
			this.TypeStart("MapSize", "MapSizeExprHost");
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x0013DB43 File Offset: 0x0013BD43
		internal void MapSizeEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSizeHost");
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x0013DB5C File Offset: 0x0013BD5C
		internal void MapSizeWidth(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x0013DB6A File Offset: 0x0013BD6A
		internal void MapSizeHeight(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeightExpr", expression);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0013DB78 File Offset: 0x0013BD78
		internal void MapSizeUnit(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UnitExpr", expression);
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0013DB86 File Offset: 0x0013BD86
		internal void MapGridLinesStart(bool isMeridian)
		{
			this.TypeStart("MapGridLines" + (isMeridian ? "MapMeridiansHost" : "MapParallelsHost"), "MapGridLinesExprHost");
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0013DBAC File Offset: 0x0013BDAC
		internal void MapGridLinesEnd(bool isMeridian)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMeridian ? "MapMeridiansHost" : "MapParallelsHost");
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0013DBCF File Offset: 0x0013BDCF
		internal void MapGridLinesHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0013DBDD File Offset: 0x0013BDDD
		internal void MapGridLinesInterval(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0013DBEB File Offset: 0x0013BDEB
		internal void MapGridLinesShowLabels(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowLabelsExpr", expression);
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x0013DBF9 File Offset: 0x0013BDF9
		internal void MapGridLinesLabelPosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPositionExpr", expression);
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0013DC07 File Offset: 0x0013BE07
		internal void MapDockableSubItemPosition(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PositionExpr", expression);
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x0013DC15 File Offset: 0x0013BE15
		internal void MapDockableSubItemDockOutsideViewport(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockOutsideViewportExpr", expression);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0013DC23 File Offset: 0x0013BE23
		internal void MapDockableSubItemHidden(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0013DC31 File Offset: 0x0013BE31
		internal void MapDockableSubItemToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0013DC3F File Offset: 0x0013BE3F
		internal void MapSubItemLeftMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftMarginExpr", expression);
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0013DC4D File Offset: 0x0013BE4D
		internal void MapSubItemRightMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightMarginExpr", expression);
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0013DC5B File Offset: 0x0013BE5B
		internal void MapSubItemTopMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopMarginExpr", expression);
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0013DC69 File Offset: 0x0013BE69
		internal void MapSubItemBottomMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BottomMarginExpr", expression);
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0013DC77 File Offset: 0x0013BE77
		internal void MapSubItemZIndex(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ZIndexExpr", expression);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0013DC85 File Offset: 0x0013BE85
		internal void MapBindingFieldPairStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapBindingFieldPair" + name, ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).MapBindingFieldPairs), "MapBindingFieldPairExprHost");
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0013DCB3 File Offset: 0x0013BEB3
		internal int MapBindingFieldPairEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapBindingFieldPairsHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapBindingFieldPairs);
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0013DCE0 File Offset: 0x0013BEE0
		internal void MapBindingFieldPairFieldName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FieldNameExpr", expression);
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0013DCEE File Offset: 0x0013BEEE
		internal void MapBindingFieldPairBindingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BindingExpressionExpr", expression);
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0013DCFC File Offset: 0x0013BEFC
		internal void MapViewportStart()
		{
			this.TypeStart("MapViewport", "MapViewportExprHost");
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0013DD0E File Offset: 0x0013BF0E
		internal void MapViewportEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewportHost");
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0013DD27 File Offset: 0x0013BF27
		internal void MapViewportSimplificationResolution(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SimplificationResolutionExpr", expression);
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0013DD35 File Offset: 0x0013BF35
		internal void MapViewportMapCoordinateSystem(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapCoordinateSystemExpr", expression);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0013DD43 File Offset: 0x0013BF43
		internal void MapViewportMapProjection(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapProjectionExpr", expression);
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0013DD51 File Offset: 0x0013BF51
		internal void MapViewportProjectionCenterX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ProjectionCenterXExpr", expression);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0013DD5F File Offset: 0x0013BF5F
		internal void MapViewportProjectionCenterY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ProjectionCenterYExpr", expression);
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0013DD6D File Offset: 0x0013BF6D
		internal void MapViewportMaximumZoom(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumZoomExpr", expression);
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x0013DD7B File Offset: 0x0013BF7B
		internal void MapViewportMinimumZoom(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumZoomExpr", expression);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x0013DD89 File Offset: 0x0013BF89
		internal void MapViewportContentMargin(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ContentMarginExpr", expression);
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x0013DD97 File Offset: 0x0013BF97
		internal void MapViewportGridUnderContent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GridUnderContentExpr", expression);
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x0013DDA5 File Offset: 0x0013BFA5
		internal void MapLimitsStart()
		{
			this.TypeStart("MapLimits", "MapLimitsExprHost");
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x0013DDB7 File Offset: 0x0013BFB7
		internal void MapLimitsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLimitsHost");
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0013DDD0 File Offset: 0x0013BFD0
		internal void MapLimitsMinimumX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumXExpr", expression);
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x0013DDDE File Offset: 0x0013BFDE
		internal void MapLimitsMinimumY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumYExpr", expression);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x0013DDEC File Offset: 0x0013BFEC
		internal void MapLimitsMaximumX(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumXExpr", expression);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x0013DDFA File Offset: 0x0013BFFA
		internal void MapLimitsMaximumY(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumYExpr", expression);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x0013DE08 File Offset: 0x0013C008
		internal void MapLimitsLimitToData(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LimitToDataExpr", expression);
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x0013DE16 File Offset: 0x0013C016
		internal void ParagraphStart(int index)
		{
			this.TypeStart(this.CreateTypeName("Paragraph" + index.ToString(CultureInfo.InvariantCulture), ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Paragraphs), "ParagraphExprHost");
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x0013DE4F File Offset: 0x0013C04F
		internal int ParagraphEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_paragraphHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Paragraphs);
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x0013DE7C File Offset: 0x0013C07C
		internal void ParagraphLeftIndent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftIndentExpr", expression);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x0013DE8A File Offset: 0x0013C08A
		internal void ParagraphRightIndent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightIndentExpr", expression);
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x0013DE98 File Offset: 0x0013C098
		internal void ParagraphHangingIndent(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HangingIndentExpr", expression);
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x0013DEA6 File Offset: 0x0013C0A6
		internal void ParagraphSpaceBefore(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpaceBeforeExpr", expression);
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x0013DEB4 File Offset: 0x0013C0B4
		internal void ParagraphSpaceAfter(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpaceAfterExpr", expression);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0013DEC2 File Offset: 0x0013C0C2
		internal void ParagraphListStyle(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ListStyleExpr", expression);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0013DED0 File Offset: 0x0013C0D0
		internal void ParagraphListLevel(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ListLevelExpr", expression);
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0013DEDE File Offset: 0x0013C0DE
		internal void TextRunStart(int index)
		{
			this.TypeStart(this.CreateTypeName("TextRun" + index.ToString(CultureInfo.InvariantCulture), ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).TextRuns), "TextRunExprHost");
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x0013DF17 File Offset: 0x0013C117
		internal int TextRunEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_textRunHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).TextRuns);
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0013DF44 File Offset: 0x0013C144
		internal void TextRunToolTip(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x0013DF52 File Offset: 0x0013C152
		internal void TextRunValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x0013DF60 File Offset: 0x0013C160
		internal void TextRunMarkupType(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkupTypeExpr", expression);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x0013DF6E File Offset: 0x0013C16E
		internal void LookupStart()
		{
			this.TypeStart(this.CreateTypeName("Lookup", this.m_rootTypeDecl.Lookups), "LookupExprHost");
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0013DF91 File Offset: 0x0013C191
		internal void LookupSourceExpr(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0013DF9F File Offset: 0x0013C19F
		internal void LookupResultExpr(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResultExpr", expression);
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x0013DFAD File Offset: 0x0013C1AD
		internal int LookupEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_lookupExprHostsRemotable", ref this.m_rootTypeDecl.Lookups);
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0013DFCB File Offset: 0x0013C1CB
		internal void LookupDestStart()
		{
			this.TypeStart(this.CreateTypeName("LookupDest", this.m_rootTypeDecl.LookupDests), "LookupDestExprHost");
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x0013DFEE File Offset: 0x0013C1EE
		internal void LookupDestExpr(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DestExpr", expression);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x0013DFFC File Offset: 0x0013C1FC
		internal int LookupDestEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_lookupDestExprHostsRemotable", ref this.m_rootTypeDecl.LookupDests);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x0013E01A File Offset: 0x0013C21A
		internal void PageBreakStart()
		{
			this.TypeStart("PageBreak", "PageBreakExprHost");
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x0013E02C File Offset: 0x0013C22C
		internal bool PageBreakEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "PageBreakExprHost");
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x0013E044 File Offset: 0x0013C244
		internal void Disabled(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DisabledExpr", expression);
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x0013E052 File Offset: 0x0013C252
		internal void PageName(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PageNameExpr", expression);
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x0013E060 File Offset: 0x0013C260
		internal void ResetPageNumber(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResetPageNumberExpr", expression);
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x0013E06E File Offset: 0x0013C26E
		internal void JoinConditionStart()
		{
			this.TypeStart(this.CreateTypeName("JoinCondition", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).JoinConditions), "JoinConditionExprHost");
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x0013E096 File Offset: 0x0013C296
		internal void JoinConditionForeignKeyExpr(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ForeignKeyExpr", expression);
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x0013E0A4 File Offset: 0x0013C2A4
		internal void JoinConditionPrimaryKeyExpr(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PrimaryKeyExpr", expression);
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x0013E0B2 File Offset: 0x0013C2B2
		internal int JoinConditionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_joinConditionExprHostsRemotable", ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).JoinConditions);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x0013E0DF File Offset: 0x0013C2DF
		private void TypeStart(string typeName, string baseType)
		{
			this.m_currentTypeDecl = new ExprHostBuilder.NonRootTypeDecl(typeName, baseType, this.m_currentTypeDecl, this.m_setCode);
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x0013E0FC File Offset: 0x0013C2FC
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

		// Token: 0x06004D8A RID: 19850 RVA: 0x0013E13F File Offset: 0x0013C33F
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

		// Token: 0x06004D8B RID: 19851 RVA: 0x0013E168 File Offset: 0x0013C368
		private void TypeEnd(ExprHostBuilder.TypeDecl container)
		{
			Global.Tracer.Assert(this.m_currentTypeDecl.Parent != null && container != null, "(m_currentTypeDecl.Parent != null && container != null)");
			container.HasExpressions |= this.m_currentTypeDecl.HasExpressions;
			this.m_currentTypeDecl = this.m_currentTypeDecl.Parent;
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x0013E1C1 File Offset: 0x0013C3C1
		private int ReportItemEnd(string name, ref CodeExpressionCollection initializers)
		{
			return this.TypeEnd(this.m_rootTypeDecl, name, ref initializers);
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x0013E1D1 File Offset: 0x0013C3D1
		private void ParameterStart()
		{
			this.TypeStart(this.CreateTypeName("Parameter", ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl).Parameters), "ParamExprHost");
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x0013E1F9 File Offset: 0x0013C3F9
		private int ParameterEnd(string propName)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, propName, ref ((ExprHostBuilder.NonRootTypeDecl)this.m_currentTypeDecl.Parent).Parameters);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x0013E222 File Offset: 0x0013C422
		private void StyleStart(string typeName)
		{
			this.TypeStart(typeName, "StyleExprHost");
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0013E230 File Offset: 0x0013C430
		private void StyleEnd(string propName)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, propName);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0013E245 File Offset: 0x0013C445
		private void AggregateStart()
		{
			this.TypeStart(this.CreateTypeName("Aggregate", this.m_rootTypeDecl.Aggregates), "AggregateParamExprHost");
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0013E268 File Offset: 0x0013C468
		private int AggregateEnd()
		{
			return this.TypeEnd(this.m_rootTypeDecl, "m_aggregateParamHostsRemotable", ref this.m_rootTypeDecl.Aggregates);
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0013E288 File Offset: 0x0013C488
		private string CreateTypeName(string template, CodeExpressionCollection initializers)
		{
			return template + ((initializers == null) ? "0" : initializers.Count.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x0013E2B8 File Offset: 0x0013C4B8
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
				codeMemberProperty.GetStatements.Add(this.ExprIndexerTree(nonRootTypeDecl.IndexedExpressions, 0, count - 1));
			}
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0013E3A4 File Offset: 0x0013C5A4
		private CodeStatement ExprIndexerTree(ExprHostBuilder.ReturnStatementList indexedExpressions, int leftIndex, int rightIndex)
		{
			if (leftIndex == rightIndex)
			{
				return indexedExpressions[leftIndex];
			}
			int num = rightIndex - leftIndex >> 1;
			return new CodeConditionStatement
			{
				Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("index"), CodeBinaryOperatorType.LessThanOrEqual, new CodePrimitiveExpression(leftIndex + num)),
				TrueStatements = { this.ExprIndexerTree(indexedExpressions, leftIndex, leftIndex + num) },
				FalseStatements = { this.ExprIndexerTree(indexedExpressions, leftIndex + num + 1, rightIndex) }
			};
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x0013E41C File Offset: 0x0013C61C
		private void IndexedExpressionAdd(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			if (expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
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

		// Token: 0x06004D97 RID: 19863 RVA: 0x0013E470 File Offset: 0x0013C670
		private void ExpressionAdd(string name, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			if (expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
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

		// Token: 0x06004D98 RID: 19864 RVA: 0x0013E4E8 File Offset: 0x0013C6E8
		private CodeMethodReturnStatement CreateExprReturnStatement(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			return new CodeMethodReturnStatement(new CodeSnippetExpression(expression.TransformedExpression))
			{
				LinePragma = new CodeLinePragma("Expr" + expression.CompileTimeID.ToString(CultureInfo.InvariantCulture) + "end", 0)
			};
		}

		// Token: 0x0400285B RID: 10331
		internal const string RootType = "ReportExprHostImpl";

		// Token: 0x0400285C RID: 10332
		internal const int InvalidExprHostId = -1;

		// Token: 0x0400285D RID: 10333
		private ExprHostBuilder.RootTypeDecl m_rootTypeDecl;

		// Token: 0x0400285E RID: 10334
		private ExprHostBuilder.TypeDecl m_currentTypeDecl;

		// Token: 0x0400285F RID: 10335
		private bool m_setCode;

		// Token: 0x04002860 RID: 10336
		private const string EndSrcMarker = "end";

		// Token: 0x04002861 RID: 10337
		private const string ExprSrcMarker = "Expr";

		// Token: 0x04002862 RID: 10338
		private static readonly Regex m_findExprNumber = new Regex("^Expr([0-9]+)end", RegexOptions.Compiled);

		// Token: 0x04002863 RID: 10339
		private const string CustomCodeSrcMarker = "CustomCode";

		// Token: 0x04002864 RID: 10340
		private const string CodeModuleClassInstanceDeclSrcMarker = "CMCID";

		// Token: 0x04002865 RID: 10341
		private static readonly Regex m_findCodeModuleClassInstanceDeclNumber = new Regex("^CMCID([0-9]+)end", RegexOptions.Compiled);

		// Token: 0x0200099E RID: 2462
		internal enum ErrorSource
		{
			// Token: 0x040041A7 RID: 16807
			Expression,
			// Token: 0x040041A8 RID: 16808
			CodeModuleClassInstanceDecl,
			// Token: 0x040041A9 RID: 16809
			CustomCode,
			// Token: 0x040041AA RID: 16810
			Unknown
		}

		// Token: 0x0200099F RID: 2463
		public enum DataRegionMode
		{
			// Token: 0x040041AC RID: 16812
			Tablix,
			// Token: 0x040041AD RID: 16813
			Chart,
			// Token: 0x040041AE RID: 16814
			GaugePanel,
			// Token: 0x040041AF RID: 16815
			CustomReportItem,
			// Token: 0x040041B0 RID: 16816
			MapDataRegion,
			// Token: 0x040041B1 RID: 16817
			DataShape
		}

		// Token: 0x020009A0 RID: 2464
		private static class Constants
		{
			// Token: 0x040041B2 RID: 16818
			internal const string ReportObjectModelNS = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel";

			// Token: 0x040041B3 RID: 16819
			internal const string ExprHostObjectModelNS = "Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel";

			// Token: 0x040041B4 RID: 16820
			internal const string ReportExprHost = "ReportExprHost";

			// Token: 0x040041B5 RID: 16821
			internal const string IndexedExprHost = "IndexedExprHost";

			// Token: 0x040041B6 RID: 16822
			internal const string ReportParamExprHost = "ReportParamExprHost";

			// Token: 0x040041B7 RID: 16823
			internal const string CalcFieldExprHost = "CalcFieldExprHost";

			// Token: 0x040041B8 RID: 16824
			internal const string DataSourceExprHost = "DataSourceExprHost";

			// Token: 0x040041B9 RID: 16825
			internal const string DataSetExprHost = "DataSetExprHost";

			// Token: 0x040041BA RID: 16826
			internal const string ReportItemExprHost = "ReportItemExprHost";

			// Token: 0x040041BB RID: 16827
			internal const string ActionExprHost = "ActionExprHost";

			// Token: 0x040041BC RID: 16828
			internal const string ActionInfoExprHost = "ActionInfoExprHost";

			// Token: 0x040041BD RID: 16829
			internal const string TextBoxExprHost = "TextBoxExprHost";

			// Token: 0x040041BE RID: 16830
			internal const string ImageExprHost = "ImageExprHost";

			// Token: 0x040041BF RID: 16831
			internal const string ParamExprHost = "ParamExprHost";

			// Token: 0x040041C0 RID: 16832
			internal const string SubreportExprHost = "SubreportExprHost";

			// Token: 0x040041C1 RID: 16833
			internal const string SortExprHost = "SortExprHost";

			// Token: 0x040041C2 RID: 16834
			internal const string FilterExprHost = "FilterExprHost";

			// Token: 0x040041C3 RID: 16835
			internal const string GroupExprHost = "GroupExprHost";

			// Token: 0x040041C4 RID: 16836
			internal const string StyleExprHost = "StyleExprHost";

			// Token: 0x040041C5 RID: 16837
			internal const string AggregateParamExprHost = "AggregateParamExprHost";

			// Token: 0x040041C6 RID: 16838
			internal const string LookupExprHost = "LookupExprHost";

			// Token: 0x040041C7 RID: 16839
			internal const string LookupDestExprHost = "LookupDestExprHost";

			// Token: 0x040041C8 RID: 16840
			internal const string ReportSectionExprHost = "ReportSectionExprHost";

			// Token: 0x040041C9 RID: 16841
			internal const string JoinConditionExprHost = "JoinConditionExprHost";

			// Token: 0x040041CA RID: 16842
			internal const string IncludeParametersParam = "includeParameters";

			// Token: 0x040041CB RID: 16843
			internal const string ParametersOnlyParam = "parametersOnly";

			// Token: 0x040041CC RID: 16844
			internal const string CustomCodeProxy = "CustomCodeProxy";

			// Token: 0x040041CD RID: 16845
			internal const string CustomCodeProxyBase = "CustomCodeProxyBase";

			// Token: 0x040041CE RID: 16846
			internal const string ReportObjectModelParam = "reportObjectModel";

			// Token: 0x040041CF RID: 16847
			internal const string SetReportObjectModel = "SetReportObjectModel";

			// Token: 0x040041D0 RID: 16848
			internal const string Code = "Code";

			// Token: 0x040041D1 RID: 16849
			internal const string CodeProxyBase = "m_codeProxyBase";

			// Token: 0x040041D2 RID: 16850
			internal const string CodeParam = "code";

			// Token: 0x040041D3 RID: 16851
			internal const string Report = "Report";

			// Token: 0x040041D4 RID: 16852
			internal const string RemoteArrayWrapper = "RemoteArrayWrapper";

			// Token: 0x040041D5 RID: 16853
			internal const string RemoteMemberArrayWrapper = "RemoteMemberArrayWrapper";

			// Token: 0x040041D6 RID: 16854
			internal const string LabelExpr = "LabelExpr";

			// Token: 0x040041D7 RID: 16855
			internal const string ValueExpr = "ValueExpr";

			// Token: 0x040041D8 RID: 16856
			internal const string NoRowsExpr = "NoRowsExpr";

			// Token: 0x040041D9 RID: 16857
			internal const string ParameterHosts = "m_parameterHostsRemotable";

			// Token: 0x040041DA RID: 16858
			internal const string IndexParam = "index";

			// Token: 0x040041DB RID: 16859
			internal const string FilterHosts = "m_filterHostsRemotable";

			// Token: 0x040041DC RID: 16860
			internal const string SortHost = "m_sortHost";

			// Token: 0x040041DD RID: 16861
			internal const string GroupHost = "m_groupHost";

			// Token: 0x040041DE RID: 16862
			internal const string VisibilityHiddenExpr = "VisibilityHiddenExpr";

			// Token: 0x040041DF RID: 16863
			internal const string SortDirectionHosts = "SortDirectionHosts";

			// Token: 0x040041E0 RID: 16864
			internal const string DataValueHosts = "m_dataValueHostsRemotable";

			// Token: 0x040041E1 RID: 16865
			internal const string CustomPropertyHosts = "m_customPropertyHostsRemotable";

			// Token: 0x040041E2 RID: 16866
			internal const string VariableValueHosts = "VariableValueHosts";

			// Token: 0x040041E3 RID: 16867
			internal const string ReportLanguageExpr = "ReportLanguageExpr";

			// Token: 0x040041E4 RID: 16868
			internal const string AutoRefreshExpr = "AutoRefreshExpr";

			// Token: 0x040041E5 RID: 16869
			internal const string AggregateParamHosts = "m_aggregateParamHostsRemotable";

			// Token: 0x040041E6 RID: 16870
			internal const string ReportParameterHosts = "m_reportParameterHostsRemotable";

			// Token: 0x040041E7 RID: 16871
			internal const string DataSourceHosts = "m_dataSourceHostsRemotable";

			// Token: 0x040041E8 RID: 16872
			internal const string DataSetHosts = "m_dataSetHostsRemotable";

			// Token: 0x040041E9 RID: 16873
			internal const string PageSectionHosts = "m_pageSectionHostsRemotable";

			// Token: 0x040041EA RID: 16874
			internal const string PageHosts = "m_pageHostsRemotable";

			// Token: 0x040041EB RID: 16875
			internal const string ReportSectionHosts = "m_reportSectionHostsRemotable";

			// Token: 0x040041EC RID: 16876
			internal const string LineHosts = "m_lineHostsRemotable";

			// Token: 0x040041ED RID: 16877
			internal const string RectangleHosts = "m_rectangleHostsRemotable";

			// Token: 0x040041EE RID: 16878
			internal const string TextBoxHosts = "m_textBoxHostsRemotable";

			// Token: 0x040041EF RID: 16879
			internal const string ImageHosts = "m_imageHostsRemotable";

			// Token: 0x040041F0 RID: 16880
			internal const string SubreportHosts = "m_subreportHostsRemotable";

			// Token: 0x040041F1 RID: 16881
			internal const string TablixHosts = "m_tablixHostsRemotable";

			// Token: 0x040041F2 RID: 16882
			internal const string ChartHosts = "m_chartHostsRemotable";

			// Token: 0x040041F3 RID: 16883
			internal const string GaugePanelHosts = "m_gaugePanelHostsRemotable";

			// Token: 0x040041F4 RID: 16884
			internal const string CustomReportItemHosts = "m_customReportItemHostsRemotable";

			// Token: 0x040041F5 RID: 16885
			internal const string LookupExprHosts = "m_lookupExprHostsRemotable";

			// Token: 0x040041F6 RID: 16886
			internal const string LookupDestExprHosts = "m_lookupDestExprHostsRemotable";

			// Token: 0x040041F7 RID: 16887
			internal const string ReportInitialPageName = "InitialPageNameExpr";

			// Token: 0x040041F8 RID: 16888
			internal const string ConnectStringExpr = "ConnectStringExpr";

			// Token: 0x040041F9 RID: 16889
			internal const string FieldHosts = "m_fieldHostsRemotable";

			// Token: 0x040041FA RID: 16890
			internal const string QueryParametersHost = "QueryParametersHost";

			// Token: 0x040041FB RID: 16891
			internal const string QueryCommandTextExpr = "QueryCommandTextExpr";

			// Token: 0x040041FC RID: 16892
			internal const string JoinConditionHosts = "m_joinConditionExprHostsRemotable";

			// Token: 0x040041FD RID: 16893
			internal const string PromptExpr = "PromptExpr";

			// Token: 0x040041FE RID: 16894
			internal const string ValidValuesHost = "ValidValuesHost";

			// Token: 0x040041FF RID: 16895
			internal const string ValidValueLabelsHost = "ValidValueLabelsHost";

			// Token: 0x04004200 RID: 16896
			internal const string ValidationExpressionExpr = "ValidationExpressionExpr";

			// Token: 0x04004201 RID: 16897
			internal const string ActionInfoHost = "ActionInfoHost";

			// Token: 0x04004202 RID: 16898
			internal const string ActionHost = "ActionHost";

			// Token: 0x04004203 RID: 16899
			internal const string ActionItemHosts = "m_actionItemHostsRemotable";

			// Token: 0x04004204 RID: 16900
			internal const string BookmarkExpr = "BookmarkExpr";

			// Token: 0x04004205 RID: 16901
			internal const string ToolTipExpr = "ToolTipExpr";

			// Token: 0x04004206 RID: 16902
			internal const string ToggleImageInitialStateExpr = "ToggleImageInitialStateExpr";

			// Token: 0x04004207 RID: 16903
			internal const string UserSortExpressionsHost = "UserSortExpressionsHost";

			// Token: 0x04004208 RID: 16904
			internal const string MIMETypeExpr = "MIMETypeExpr";

			// Token: 0x04004209 RID: 16905
			internal const string TagExpr = "TagExpr";

			// Token: 0x0400420A RID: 16906
			internal const string OmitExpr = "OmitExpr";

			// Token: 0x0400420B RID: 16907
			internal const string HyperlinkExpr = "HyperlinkExpr";

			// Token: 0x0400420C RID: 16908
			internal const string DrillThroughReportNameExpr = "DrillThroughReportNameExpr";

			// Token: 0x0400420D RID: 16909
			internal const string DrillThroughParameterHosts = "m_drillThroughParameterHostsRemotable";

			// Token: 0x0400420E RID: 16910
			internal const string DrillThroughBookmakLinkExpr = "DrillThroughBookmarkLinkExpr";

			// Token: 0x0400420F RID: 16911
			internal const string BookmarkLinkExpr = "BookmarkLinkExpr";

			// Token: 0x04004210 RID: 16912
			internal const string FilterExpressionExpr = "FilterExpressionExpr";

			// Token: 0x04004211 RID: 16913
			internal const string ParentExpressionsHost = "ParentExpressionsHost";

			// Token: 0x04004212 RID: 16914
			internal const string ReGroupExpressionsHost = "ReGroupExpressionsHost";

			// Token: 0x04004213 RID: 16915
			internal const string DataValueExprHost = "DataValueExprHost";

			// Token: 0x04004214 RID: 16916
			internal const string DataValueNameExpr = "DataValueNameExpr";

			// Token: 0x04004215 RID: 16917
			internal const string DataValueValueExpr = "DataValueValueExpr";

			// Token: 0x04004216 RID: 16918
			internal const string TablixExprHost = "TablixExprHost";

			// Token: 0x04004217 RID: 16919
			internal const string DataShapeExprHost = "DataShapeExprHost";

			// Token: 0x04004218 RID: 16920
			internal const string ChartExprHost = "ChartExprHost";

			// Token: 0x04004219 RID: 16921
			internal const string GaugePanelExprHost = "GaugePanelExprHost";

			// Token: 0x0400421A RID: 16922
			internal const string CustomReportItemExprHost = "CustomReportItemExprHost";

			// Token: 0x0400421B RID: 16923
			internal const string MapDataRegionExprHost = "MapDataRegionExprHost";

			// Token: 0x0400421C RID: 16924
			internal const string TablixMemberExprHost = "TablixMemberExprHost";

			// Token: 0x0400421D RID: 16925
			internal const string DataShapeMemberExprHost = "DataShapeMemberExprHost";

			// Token: 0x0400421E RID: 16926
			internal const string ChartMemberExprHost = "ChartMemberExprHost";

			// Token: 0x0400421F RID: 16927
			internal const string GaugeMemberExprHost = "GaugeMemberExprHost";

			// Token: 0x04004220 RID: 16928
			internal const string DataGroupExprHost = "DataGroupExprHost";

			// Token: 0x04004221 RID: 16929
			internal const string TablixCellExprHost = "TablixCellExprHost";

			// Token: 0x04004222 RID: 16930
			internal const string DataShapeIntersectionExprHost = "DataShapeIntersectionExprHost";

			// Token: 0x04004223 RID: 16931
			internal const string ChartDataPointExprHost = "ChartDataPointExprHost";

			// Token: 0x04004224 RID: 16932
			internal const string GaugeCellExprHost = "GaugeCellExprHost";

			// Token: 0x04004225 RID: 16933
			internal const string DataCellExprHost = "DataCellExprHost";

			// Token: 0x04004226 RID: 16934
			internal const string MemberTreeHosts = "m_memberTreeHostsRemotable";

			// Token: 0x04004227 RID: 16935
			internal const string DataCellHosts = "m_cellHostsRemotable";

			// Token: 0x04004228 RID: 16936
			internal const string MapMemberExprHost = "MapMemberExprHost";

			// Token: 0x04004229 RID: 16937
			internal const string TablixCornerCellHosts = "m_cornerCellHostsRemotable";

			// Token: 0x0400422A RID: 16938
			internal const string ChartTitleExprHost = "ChartTitleExprHost";

			// Token: 0x0400422B RID: 16939
			internal const string ChartAxisTitleExprHost = "ChartAxisTitleExprHost";

			// Token: 0x0400422C RID: 16940
			internal const string ChartLegendTitleExprHost = "ChartLegendTitleExprHost";

			// Token: 0x0400422D RID: 16941
			internal const string ChartLegendExprHost = "ChartLegendExprHost";

			// Token: 0x0400422E RID: 16942
			internal const string ChartTitleHost = "TitleHost";

			// Token: 0x0400422F RID: 16943
			internal const string ChartNoDataMessageHost = "NoDataMessageHost";

			// Token: 0x04004230 RID: 16944
			internal const string ChartLegendTitleHost = "TitleExprHost";

			// Token: 0x04004231 RID: 16945
			internal const string PaletteExpr = "PaletteExpr";

			// Token: 0x04004232 RID: 16946
			internal const string PaletteHatchBehaviorExpr = "PaletteHatchBehaviorExpr";

			// Token: 0x04004233 RID: 16947
			internal const string ChartAreaExprHost = "ChartAreaExprHost";

			// Token: 0x04004234 RID: 16948
			internal const string ChartNoMoveDirectionsExprHost = "ChartNoMoveDirectionsExprHost";

			// Token: 0x04004235 RID: 16949
			internal const string ChartNoMoveDirectionsHost = "NoMoveDirectionsHost";

			// Token: 0x04004236 RID: 16950
			internal const string UpExpr = "UpExpr";

			// Token: 0x04004237 RID: 16951
			internal const string DownExpr = "DownExpr";

			// Token: 0x04004238 RID: 16952
			internal const string LeftExpr = "LeftExpr";

			// Token: 0x04004239 RID: 16953
			internal const string RightExpr = "RightExpr";

			// Token: 0x0400423A RID: 16954
			internal const string UpLeftExpr = "UpLeftExpr";

			// Token: 0x0400423B RID: 16955
			internal const string UpRightExpr = "UpRightExpr";

			// Token: 0x0400423C RID: 16956
			internal const string DownLeftExpr = "DownLeftExpr";

			// Token: 0x0400423D RID: 16957
			internal const string DownRightExpr = "DownRightExpr";

			// Token: 0x0400423E RID: 16958
			internal const string ChartSmartLabelExprHost = "ChartSmartLabelExprHost";

			// Token: 0x0400423F RID: 16959
			internal const string ChartSmartLabelHost = "SmartLabelHost";

			// Token: 0x04004240 RID: 16960
			internal const string AllowOutSidePlotAreaExpr = "AllowOutSidePlotAreaExpr";

			// Token: 0x04004241 RID: 16961
			internal const string CalloutBackColorExpr = "CalloutBackColorExpr";

			// Token: 0x04004242 RID: 16962
			internal const string CalloutLineAnchorExpr = "CalloutLineAnchorExpr";

			// Token: 0x04004243 RID: 16963
			internal const string CalloutLineColorExpr = "CalloutLineColorExpr";

			// Token: 0x04004244 RID: 16964
			internal const string CalloutLineStyleExpr = "CalloutLineStyleExpr";

			// Token: 0x04004245 RID: 16965
			internal const string CalloutLineWidthExpr = "CalloutLineWidthExpr";

			// Token: 0x04004246 RID: 16966
			internal const string CalloutStyleExpr = "CalloutStyleExpr";

			// Token: 0x04004247 RID: 16967
			internal const string HideOverlappedExpr = "HideOverlappedExpr";

			// Token: 0x04004248 RID: 16968
			internal const string MarkerOverlappingExpr = "MarkerOverlappingExpr";

			// Token: 0x04004249 RID: 16969
			internal const string MaxMovingDistanceExpr = "MaxMovingDistanceExpr";

			// Token: 0x0400424A RID: 16970
			internal const string MinMovingDistanceExpr = "MinMovingDistanceExpr";

			// Token: 0x0400424B RID: 16971
			internal const string DisabledExpr = "DisabledExpr";

			// Token: 0x0400424C RID: 16972
			internal const string ChartAxisScaleBreakExprHost = "ChartAxisScaleBreakExprHost";

			// Token: 0x0400424D RID: 16973
			internal const string ChartAxisScaleBreakHost = "AxisScaleBreakHost";

			// Token: 0x0400424E RID: 16974
			internal const string ChartBorderSkinExprHost = "ChartBorderSkinExprHost";

			// Token: 0x0400424F RID: 16975
			internal const string ChartBorderSkinHost = "BorderSkinHost";

			// Token: 0x04004250 RID: 16976
			internal const string TitleSeparatorExpr = "TitleSeparatorExpr";

			// Token: 0x04004251 RID: 16977
			internal const string ColumnTypeExpr = "ColumnTypeExpr";

			// Token: 0x04004252 RID: 16978
			internal const string MinimumWidthExpr = "MinimumWidthExpr";

			// Token: 0x04004253 RID: 16979
			internal const string MaximumWidthExpr = "MaximumWidthExpr";

			// Token: 0x04004254 RID: 16980
			internal const string SeriesSymbolWidthExpr = "SeriesSymbolWidthExpr";

			// Token: 0x04004255 RID: 16981
			internal const string SeriesSymbolHeightExpr = "SeriesSymbolHeightExpr";

			// Token: 0x04004256 RID: 16982
			internal const string CellTypeExpr = "CellTypeExpr";

			// Token: 0x04004257 RID: 16983
			internal const string TextExpr = "TextExpr";

			// Token: 0x04004258 RID: 16984
			internal const string CellSpanExpr = "CellSpanExpr";

			// Token: 0x04004259 RID: 16985
			internal const string ImageWidthExpr = "ImageWidthExpr";

			// Token: 0x0400425A RID: 16986
			internal const string ImageHeightExpr = "ImageHeightExpr";

			// Token: 0x0400425B RID: 16987
			internal const string SymbolHeightExpr = "SymbolHeightExpr";

			// Token: 0x0400425C RID: 16988
			internal const string SymbolWidthExpr = "SymbolWidthExpr";

			// Token: 0x0400425D RID: 16989
			internal const string AlignmentExpr = "AlignmentExpr";

			// Token: 0x0400425E RID: 16990
			internal const string TopMarginExpr = "TopMarginExpr";

			// Token: 0x0400425F RID: 16991
			internal const string BottomMarginExpr = "BottomMarginExpr";

			// Token: 0x04004260 RID: 16992
			internal const string LeftMarginExpr = "LeftMarginExpr";

			// Token: 0x04004261 RID: 16993
			internal const string RightMarginExpr = "RightMarginExpr";

			// Token: 0x04004262 RID: 16994
			internal const string VisibleExpr = "VisibleExpr";

			// Token: 0x04004263 RID: 16995
			internal const string MarginExpr = "MarginExpr";

			// Token: 0x04004264 RID: 16996
			internal const string IntervalExpr = "IntervalExpr";

			// Token: 0x04004265 RID: 16997
			internal const string IntervalTypeExpr = "IntervalTypeExpr";

			// Token: 0x04004266 RID: 16998
			internal const string IntervalOffsetExpr = "IntervalOffsetExpr";

			// Token: 0x04004267 RID: 16999
			internal const string IntervalOffsetTypeExpr = "IntervalOffsetTypeExpr";

			// Token: 0x04004268 RID: 17000
			internal const string MarksAlwaysAtPlotEdgeExpr = "MarksAlwaysAtPlotEdgeExpr";

			// Token: 0x04004269 RID: 17001
			internal const string ReverseExpr = "ReverseExpr";

			// Token: 0x0400426A RID: 17002
			internal const string LocationExpr = "LocationExpr";

			// Token: 0x0400426B RID: 17003
			internal const string InterlacedExpr = "InterlacedExpr";

			// Token: 0x0400426C RID: 17004
			internal const string InterlacedColorExpr = "InterlacedColorExpr";

			// Token: 0x0400426D RID: 17005
			internal const string LogScaleExpr = "LogScaleExpr";

			// Token: 0x0400426E RID: 17006
			internal const string LogBaseExpr = "LogBaseExpr";

			// Token: 0x0400426F RID: 17007
			internal const string HideLabelsExpr = "HideLabelsExpr";

			// Token: 0x04004270 RID: 17008
			internal const string AngleExpr = "AngleExpr";

			// Token: 0x04004271 RID: 17009
			internal const string ArrowsExpr = "ArrowsExpr";

			// Token: 0x04004272 RID: 17010
			internal const string PreventFontShrinkExpr = "PreventFontShrinkExpr";

			// Token: 0x04004273 RID: 17011
			internal const string PreventFontGrowExpr = "PreventFontGrowExpr";

			// Token: 0x04004274 RID: 17012
			internal const string PreventLabelOffsetExpr = "PreventLabelOffsetExpr";

			// Token: 0x04004275 RID: 17013
			internal const string PreventWordWrapExpr = "PreventWordWrapExpr";

			// Token: 0x04004276 RID: 17014
			internal const string AllowLabelRotationExpr = "AllowLabelRotationExpr";

			// Token: 0x04004277 RID: 17015
			internal const string IncludeZeroExpr = "IncludeZeroExpr";

			// Token: 0x04004278 RID: 17016
			internal const string LabelsAutoFitDisabledExpr = "LabelsAutoFitDisabledExpr";

			// Token: 0x04004279 RID: 17017
			internal const string MinFontSizeExpr = "MinFontSizeExpr";

			// Token: 0x0400427A RID: 17018
			internal const string MaxFontSizeExpr = "MaxFontSizeExpr";

			// Token: 0x0400427B RID: 17019
			internal const string OffsetLabelsExpr = "OffsetLabelsExpr";

			// Token: 0x0400427C RID: 17020
			internal const string HideEndLabelsExpr = "HideEndLabelsExpr";

			// Token: 0x0400427D RID: 17021
			internal const string ChartTickMarksExprHost = "ChartTickMarksExprHost";

			// Token: 0x0400427E RID: 17022
			internal const string ChartTickMarksHost = "TickMarksHost";

			// Token: 0x0400427F RID: 17023
			internal const string ChartGridLinesExprHost = "ChartGridLinesExprHost";

			// Token: 0x04004280 RID: 17024
			internal const string ChartGridLinesHost = "GridLinesHost";

			// Token: 0x04004281 RID: 17025
			internal const string ChartDataPointInLegendExprHost = "ChartDataPointInLegendExprHost";

			// Token: 0x04004282 RID: 17026
			internal const string ChartDataPointInLegendHost = "DataPointInLegendHost";

			// Token: 0x04004283 RID: 17027
			internal const string ChartEmptyPointsExprHost = "ChartEmptyPointsExprHost";

			// Token: 0x04004284 RID: 17028
			internal const string ChartEmptyPointsHost = "EmptyPointsHost";

			// Token: 0x04004285 RID: 17029
			internal const string AxisLabelExpr = "AxisLabelExpr";

			// Token: 0x04004286 RID: 17030
			internal const string LegendTextExpr = "LegendTextExpr";

			// Token: 0x04004287 RID: 17031
			internal const string ChartLegendColumnHeaderExprHost = "ChartLegendColumnHeaderExprHost";

			// Token: 0x04004288 RID: 17032
			internal const string ChartLegendColumnHeaderHost = "ChartLegendColumnHeaderHost";

			// Token: 0x04004289 RID: 17033
			internal const string ChartCustomPaletteColorExprHost = "ChartCustomPaletteColorExprHost";

			// Token: 0x0400428A RID: 17034
			internal const string ChartCustomPaletteColorHosts = "m_customPaletteColorHostsRemotable";

			// Token: 0x0400428B RID: 17035
			internal const string ChartLegendCustomItemCellExprHost = "ChartLegendCustomItemCellExprHost";

			// Token: 0x0400428C RID: 17036
			internal const string ChartLegendCustomItemCellsHosts = "m_legendCustomItemCellHostsRemotable";

			// Token: 0x0400428D RID: 17037
			internal const string ChartDerivedSeriesExprHost = "ChartDerivedSeriesExprHost";

			// Token: 0x0400428E RID: 17038
			internal const string ChartDerivedSeriesCollectionHosts = "m_derivedSeriesCollectionHostsRemotable";

			// Token: 0x0400428F RID: 17039
			internal const string SourceChartSeriesNameExpr = "SourceChartSeriesNameExpr";

			// Token: 0x04004290 RID: 17040
			internal const string DerivedSeriesFormulaExpr = "DerivedSeriesFormulaExpr";

			// Token: 0x04004291 RID: 17041
			internal const string SizeExpr = "SizeExpr";

			// Token: 0x04004292 RID: 17042
			internal const string TypeExpr = "TypeExpr";

			// Token: 0x04004293 RID: 17043
			internal const string SubtypeExpr = "SubtypeExpr";

			// Token: 0x04004294 RID: 17044
			internal const string LegendNameExpr = "LegendNameExpr";

			// Token: 0x04004295 RID: 17045
			internal const string ChartAreaNameExpr = "ChartAreaNameExpr";

			// Token: 0x04004296 RID: 17046
			internal const string ValueAxisNameExpr = "ValueAxisNameExpr";

			// Token: 0x04004297 RID: 17047
			internal const string CategoryAxisNameExpr = "CategoryAxisNameExpr";

			// Token: 0x04004298 RID: 17048
			internal const string ChartStripLineExprHost = "ChartStripLineExprHost";

			// Token: 0x04004299 RID: 17049
			internal const string ChartStripLinesHosts = "m_stripLinesHostsRemotable";

			// Token: 0x0400429A RID: 17050
			internal const string ChartSeriesExprHost = "ChartSeriesExprHost";

			// Token: 0x0400429B RID: 17051
			internal const string ChartSeriesHost = "ChartSeriesHost";

			// Token: 0x0400429C RID: 17052
			internal const string TitleExpr = "TitleExpr";

			// Token: 0x0400429D RID: 17053
			internal const string TitleAngleExpr = "TitleAngleExpr";

			// Token: 0x0400429E RID: 17054
			internal const string StripWidthExpr = "StripWidthExpr";

			// Token: 0x0400429F RID: 17055
			internal const string StripWidthTypeExpr = "StripWidthTypeExpr";

			// Token: 0x040042A0 RID: 17056
			internal const string HiddenExpr = "HiddenExpr";

			// Token: 0x040042A1 RID: 17057
			internal const string ChartFormulaParameterExprHost = "ChartFormulaParameterExprHost";

			// Token: 0x040042A2 RID: 17058
			internal const string ChartFormulaParametersHosts = "m_formulaParametersHostsRemotable";

			// Token: 0x040042A3 RID: 17059
			internal const string ChartLegendColumnExprHost = "ChartLegendColumnExprHost";

			// Token: 0x040042A4 RID: 17060
			internal const string ChartLegendColumnsHosts = "m_legendColumnsHostsRemotable";

			// Token: 0x040042A5 RID: 17061
			internal const string ChartLegendCustomItemExprHost = "ChartLegendCustomItemExprHost";

			// Token: 0x040042A6 RID: 17062
			internal const string ChartLegendCustomItemsHosts = "m_legendCustomItemsHostsRemotable";

			// Token: 0x040042A7 RID: 17063
			internal const string SeparatorExpr = "SeparatorExpr";

			// Token: 0x040042A8 RID: 17064
			internal const string SeparatorColorExpr = "SeparatorColorExpr";

			// Token: 0x040042A9 RID: 17065
			internal const string ChartValueAxesHosts = "m_valueAxesHostsRemotable";

			// Token: 0x040042AA RID: 17066
			internal const string ChartCategoryAxesHosts = "m_categoryAxesHostsRemotable";

			// Token: 0x040042AB RID: 17067
			internal const string ChartTitlesHosts = "m_titlesHostsRemotable";

			// Token: 0x040042AC RID: 17068
			internal const string ChartLegendsHosts = "m_legendsHostsRemotable";

			// Token: 0x040042AD RID: 17069
			internal const string ChartAreasHosts = "m_chartAreasHostsRemotable";

			// Token: 0x040042AE RID: 17070
			internal const string ChartAxisExprHost = "ChartAxisExprHost";

			// Token: 0x040042AF RID: 17071
			internal const string MemberLabelExpr = "MemberLabelExpr";

			// Token: 0x040042B0 RID: 17072
			internal const string MemberStyleHost = "MemberStyleHost";

			// Token: 0x040042B1 RID: 17073
			internal const string DataLabelStyleHost = "DataLabelStyleHost";

			// Token: 0x040042B2 RID: 17074
			internal const string StyleHost = "StyleHost";

			// Token: 0x040042B3 RID: 17075
			internal const string MarkerStyleHost = "MarkerStyleHost";

			// Token: 0x040042B4 RID: 17076
			internal const string CaptionExpr = "CaptionExpr";

			// Token: 0x040042B5 RID: 17077
			internal const string CategoryAxisHost = "CategoryAxisHost";

			// Token: 0x040042B6 RID: 17078
			internal const string PlotAreaHost = "PlotAreaHost";

			// Token: 0x040042B7 RID: 17079
			internal const string AxisMinExpr = "AxisMinExpr";

			// Token: 0x040042B8 RID: 17080
			internal const string AxisMaxExpr = "AxisMaxExpr";

			// Token: 0x040042B9 RID: 17081
			internal const string AxisCrossAtExpr = "AxisCrossAtExpr";

			// Token: 0x040042BA RID: 17082
			internal const string AxisMajorIntervalExpr = "AxisMajorIntervalExpr";

			// Token: 0x040042BB RID: 17083
			internal const string AxisMinorIntervalExpr = "AxisMinorIntervalExpr";

			// Token: 0x040042BC RID: 17084
			internal const string ChartDataPointValueXExpr = "DataPointValuesXExpr";

			// Token: 0x040042BD RID: 17085
			internal const string ChartDataPointValueYExpr = "DataPointValuesYExpr";

			// Token: 0x040042BE RID: 17086
			internal const string ChartDataPointValueSizeExpr = "DataPointValuesSizeExpr";

			// Token: 0x040042BF RID: 17087
			internal const string ChartDataPointValueHighExpr = "DataPointValuesHighExpr";

			// Token: 0x040042C0 RID: 17088
			internal const string ChartDataPointValueLowExpr = "DataPointValuesLowExpr";

			// Token: 0x040042C1 RID: 17089
			internal const string ChartDataPointValueStartExpr = "DataPointValuesStartExpr";

			// Token: 0x040042C2 RID: 17090
			internal const string ChartDataPointValueEndExpr = "DataPointValuesEndExpr";

			// Token: 0x040042C3 RID: 17091
			internal const string ChartDataPointValueMeanExpr = "DataPointValuesMeanExpr";

			// Token: 0x040042C4 RID: 17092
			internal const string ChartDataPointValueMedianExpr = "DataPointValuesMedianExpr";

			// Token: 0x040042C5 RID: 17093
			internal const string ChartDataPointValueHighlightXExpr = "DataPointValuesHighlightXExpr";

			// Token: 0x040042C6 RID: 17094
			internal const string ChartDataPointValueHighlightYExpr = "DataPointValuesHighlightYExpr";

			// Token: 0x040042C7 RID: 17095
			internal const string ChartDataPointValueHighlightSizeExpr = "DataPointValuesHighlightSizeExpr";

			// Token: 0x040042C8 RID: 17096
			internal const string ChartDataPointValueFormatXExpr = "ChartDataPointValueFormatXExpr";

			// Token: 0x040042C9 RID: 17097
			internal const string ChartDataPointValueFormatYExpr = "ChartDataPointValueFormatYExpr";

			// Token: 0x040042CA RID: 17098
			internal const string ChartDataPointValueFormatSizeExpr = "ChartDataPointValueFormatSizeExpr";

			// Token: 0x040042CB RID: 17099
			internal const string ChartDataPointValueCurrencyLanguageXExpr = "ChartDataPointValueCurrencyLanguageXExpr";

			// Token: 0x040042CC RID: 17100
			internal const string ChartDataPointValueCurrencyLanguageYExpr = "ChartDataPointValueCurrencyLanguageYExpr";

			// Token: 0x040042CD RID: 17101
			internal const string ChartDataPointValueCurrencyLanguageSizeExpr = "ChartDataPointValueCurrencyLanguageSizeExpr";

			// Token: 0x040042CE RID: 17102
			internal const string ColorExpr = "ColorExpr";

			// Token: 0x040042CF RID: 17103
			internal const string BorderSkinTypeExpr = "BorderSkinTypeExpr";

			// Token: 0x040042D0 RID: 17104
			internal const string LengthExpr = "LengthExpr";

			// Token: 0x040042D1 RID: 17105
			internal const string EnabledExpr = "EnabledExpr";

			// Token: 0x040042D2 RID: 17106
			internal const string BreakLineTypeExpr = "BreakLineTypeExpr";

			// Token: 0x040042D3 RID: 17107
			internal const string CollapsibleSpaceThresholdExpr = "CollapsibleSpaceThresholdExpr";

			// Token: 0x040042D4 RID: 17108
			internal const string MaxNumberOfBreaksExpr = "MaxNumberOfBreaksExpr";

			// Token: 0x040042D5 RID: 17109
			internal const string SpacingExpr = "SpacingExpr";

			// Token: 0x040042D6 RID: 17110
			internal const string AxesViewExpr = "AxesViewExpr";

			// Token: 0x040042D7 RID: 17111
			internal const string CursorExpr = "CursorExpr";

			// Token: 0x040042D8 RID: 17112
			internal const string InnerPlotPositionExpr = "InnerPlotPositionExpr";

			// Token: 0x040042D9 RID: 17113
			internal const string ChartAlignTypePositionExpr = "ChartAlignTypePositionExpr";

			// Token: 0x040042DA RID: 17114
			internal const string EquallySizedAxesFontExpr = "EquallySizedAxesFontExpr";

			// Token: 0x040042DB RID: 17115
			internal const string AlignOrientationExpr = "AlignOrientationExpr";

			// Token: 0x040042DC RID: 17116
			internal const string Chart3DPropertiesExprHost = "Chart3DPropertiesExprHost";

			// Token: 0x040042DD RID: 17117
			internal const string Chart3DPropertiesHost = "Chart3DPropertiesHost";

			// Token: 0x040042DE RID: 17118
			internal const string LayoutExpr = "LayoutExpr";

			// Token: 0x040042DF RID: 17119
			internal const string DockOutsideChartAreaExpr = "DockOutsideChartAreaExpr";

			// Token: 0x040042E0 RID: 17120
			internal const string TitleExprHost = "TitleExprHost";

			// Token: 0x040042E1 RID: 17121
			internal const string AutoFitTextDisabledExpr = "AutoFitTextDisabledExpr";

			// Token: 0x040042E2 RID: 17122
			internal const string HeaderSeparatorExpr = "HeaderSeparatorExpr";

			// Token: 0x040042E3 RID: 17123
			internal const string HeaderSeparatorColorExpr = "HeaderSeparatorColorExpr";

			// Token: 0x040042E4 RID: 17124
			internal const string ColumnSeparatorExpr = "ColumnSeparatorExpr";

			// Token: 0x040042E5 RID: 17125
			internal const string ColumnSeparatorColorExpr = "ColumnSeparatorColorExpr";

			// Token: 0x040042E6 RID: 17126
			internal const string ColumnSpacingExpr = "ColumnSpacingExpr";

			// Token: 0x040042E7 RID: 17127
			internal const string InterlacedRowsExpr = "InterlacedRowsExpr";

			// Token: 0x040042E8 RID: 17128
			internal const string InterlacedRowsColorExpr = "InterlacedRowsColorExpr";

			// Token: 0x040042E9 RID: 17129
			internal const string EquallySpacedItemsExpr = "EquallySpacedItemsExpr";

			// Token: 0x040042EA RID: 17130
			internal const string ReversedExpr = "ReversedExpr";

			// Token: 0x040042EB RID: 17131
			internal const string MaxAutoSizeExpr = "MaxAutoSizeExpr";

			// Token: 0x040042EC RID: 17132
			internal const string TextWrapThresholdExpr = "TextWrapThresholdExpr";

			// Token: 0x040042ED RID: 17133
			internal const string DockingExpr = "DockingExpr";

			// Token: 0x040042EE RID: 17134
			internal const string ChartTitlePositionExpr = "ChartTitlePositionExpr";

			// Token: 0x040042EF RID: 17135
			internal const string DockingOffsetExpr = "DockingOffsetExpr";

			// Token: 0x040042F0 RID: 17136
			internal const string ChartLegendPositionExpr = "ChartLegendPositionExpr";

			// Token: 0x040042F1 RID: 17137
			internal const string DockOutsideChartArea = "DockOutsideChartArea";

			// Token: 0x040042F2 RID: 17138
			internal const string AutoFitTextDisabled = "AutoFitTextDisabled";

			// Token: 0x040042F3 RID: 17139
			internal const string MinFontSize = "MinFontSize";

			// Token: 0x040042F4 RID: 17140
			internal const string HeaderSeparator = "HeaderSeparator";

			// Token: 0x040042F5 RID: 17141
			internal const string HeaderSeparatorColor = "HeaderSeparatorColor";

			// Token: 0x040042F6 RID: 17142
			internal const string ColumnSeparator = "ColumnSeparator";

			// Token: 0x040042F7 RID: 17143
			internal const string ColumnSeparatorColor = "ColumnSeparatorColor";

			// Token: 0x040042F8 RID: 17144
			internal const string ColumnSpacing = "ColumnSpacing";

			// Token: 0x040042F9 RID: 17145
			internal const string InterlacedRows = "InterlacedRows";

			// Token: 0x040042FA RID: 17146
			internal const string InterlacedRowsColor = "InterlacedRowsColor";

			// Token: 0x040042FB RID: 17147
			internal const string EquallySpacedItems = "EquallySpacedItems";

			// Token: 0x040042FC RID: 17148
			internal const string HideInLegendExpr = "HideInLegendExpr";

			// Token: 0x040042FD RID: 17149
			internal const string ShowOverlappedExpr = "ShowOverlappedExpr";

			// Token: 0x040042FE RID: 17150
			internal const string MajorChartTickMarksHost = "MajorTickMarksHost";

			// Token: 0x040042FF RID: 17151
			internal const string MinorChartTickMarksHost = "MinorTickMarksHost";

			// Token: 0x04004300 RID: 17152
			internal const string MajorChartGridLinesHost = "MajorGridLinesHost";

			// Token: 0x04004301 RID: 17153
			internal const string MinorChartGridLinesHost = "MinorGridLinesHost";

			// Token: 0x04004302 RID: 17154
			internal const string RotationExpr = "RotationExpr";

			// Token: 0x04004303 RID: 17155
			internal const string ProjectionModeExpr = "ProjectionModeExpr";

			// Token: 0x04004304 RID: 17156
			internal const string InclinationExpr = "InclinationExpr";

			// Token: 0x04004305 RID: 17157
			internal const string PerspectiveExpr = "PerspectiveExpr";

			// Token: 0x04004306 RID: 17158
			internal const string DepthRatioExpr = "DepthRatioExpr";

			// Token: 0x04004307 RID: 17159
			internal const string ShadingExpr = "ShadingExpr";

			// Token: 0x04004308 RID: 17160
			internal const string GapDepthExpr = "GapDepthExpr";

			// Token: 0x04004309 RID: 17161
			internal const string WallThicknessExpr = "WallThicknessExpr";

			// Token: 0x0400430A RID: 17162
			internal const string ClusteredExpr = "ClusteredExpr";

			// Token: 0x0400430B RID: 17163
			internal const string ChartDataLabelExprHost = "ChartDataLabelExprHost";

			// Token: 0x0400430C RID: 17164
			internal const string ChartDataLabelPositionExpr = "ChartDataLabelPositionExpr";

			// Token: 0x0400430D RID: 17165
			internal const string UseValueAsLabelExpr = "UseValueAsLabelExpr";

			// Token: 0x0400430E RID: 17166
			internal const string ChartDataLabelHost = "DataLabelHost";

			// Token: 0x0400430F RID: 17167
			internal const string ChartMarkerExprHost = "ChartMarkerExprHost";

			// Token: 0x04004310 RID: 17168
			internal const string ChartMarkerHost = "ChartMarkerHost";

			// Token: 0x04004311 RID: 17169
			internal const string VariableAutoIntervalExpr = "VariableAutoIntervalExpr";

			// Token: 0x04004312 RID: 17170
			internal const string LabelIntervalExpr = "LabelIntervalExpr";

			// Token: 0x04004313 RID: 17171
			internal const string LabelIntervalTypeExpr = "LabelIntervalTypeExpr";

			// Token: 0x04004314 RID: 17172
			internal const string LabelIntervalOffsetExpr = "LabelIntervalOffsetExpr";

			// Token: 0x04004315 RID: 17173
			internal const string LabelIntervalOffsetTypeExpr = "LabelIntervalOffsetTypeExpr";

			// Token: 0x04004316 RID: 17174
			internal const string DynamicWidthExpr = "DynamicWidthExpr";

			// Token: 0x04004317 RID: 17175
			internal const string DynamicHeightExpr = "DynamicHeightExpr";

			// Token: 0x04004318 RID: 17176
			internal const string TextOrientationExpr = "TextOrientationExpr";

			// Token: 0x04004319 RID: 17177
			internal const string ChartElementPositionExprHost = "ChartElementPositionExprHost";

			// Token: 0x0400431A RID: 17178
			internal const string ChartElementPositionHost = "ChartElementPositionHost";

			// Token: 0x0400431B RID: 17179
			internal const string ChartInnerPlotPositionHost = "ChartInnerPlotPositionHost";

			// Token: 0x0400431C RID: 17180
			internal const string BaseGaugeImageExprHost = "BaseGaugeImageExprHost";

			// Token: 0x0400431D RID: 17181
			internal const string BaseGaugeImageHost = "BaseGaugeImageHost";

			// Token: 0x0400431E RID: 17182
			internal const string SourceExpr = "SourceExpr";

			// Token: 0x0400431F RID: 17183
			internal const string TransparentColorExpr = "TransparentColorExpr";

			// Token: 0x04004320 RID: 17184
			internal const string CapImageExprHost = "CapImageExprHost";

			// Token: 0x04004321 RID: 17185
			internal const string CapImageHost = "CapImageHost";

			// Token: 0x04004322 RID: 17186
			internal const string TopImageHost = "TopImageHost";

			// Token: 0x04004323 RID: 17187
			internal const string TopImageExprHost = "TopImageExprHost";

			// Token: 0x04004324 RID: 17188
			internal const string HueColorExpr = "HueColorExpr";

			// Token: 0x04004325 RID: 17189
			internal const string OffsetXExpr = "OffsetXExpr";

			// Token: 0x04004326 RID: 17190
			internal const string OffsetYExpr = "OffsetYExpr";

			// Token: 0x04004327 RID: 17191
			internal const string FrameImageExprHost = "FrameImageExprHost";

			// Token: 0x04004328 RID: 17192
			internal const string FrameImageHost = "FrameImageHost";

			// Token: 0x04004329 RID: 17193
			internal const string IndicatorImageExprHost = "IndicatorImageExprHost";

			// Token: 0x0400432A RID: 17194
			internal const string IndicatorImageHost = "IndicatorImageHost";

			// Token: 0x0400432B RID: 17195
			internal const string TransparencyExpr = "TransparencyExpr";

			// Token: 0x0400432C RID: 17196
			internal const string ClipImageExpr = "ClipImageExpr";

			// Token: 0x0400432D RID: 17197
			internal const string PointerImageExprHost = "PointerImageExprHost";

			// Token: 0x0400432E RID: 17198
			internal const string PointerImageHost = "PointerImageHost";

			// Token: 0x0400432F RID: 17199
			internal const string BackFrameExprHost = "BackFrameExprHost";

			// Token: 0x04004330 RID: 17200
			internal const string BackFrameHost = "BackFrameHost";

			// Token: 0x04004331 RID: 17201
			internal const string FrameStyleExpr = "FrameStyleExpr";

			// Token: 0x04004332 RID: 17202
			internal const string FrameShapeExpr = "FrameShapeExpr";

			// Token: 0x04004333 RID: 17203
			internal const string FrameWidthExpr = "FrameWidthExpr";

			// Token: 0x04004334 RID: 17204
			internal const string GlassEffectExpr = "GlassEffectExpr";

			// Token: 0x04004335 RID: 17205
			internal const string FrameBackgroundExprHost = "FrameBackgroundExprHost";

			// Token: 0x04004336 RID: 17206
			internal const string FrameBackgroundHost = "FrameBackgroundHost";

			// Token: 0x04004337 RID: 17207
			internal const string CustomLabelExprHost = "CustomLabelExprHost";

			// Token: 0x04004338 RID: 17208
			internal const string FontAngleExpr = "FontAngleExpr";

			// Token: 0x04004339 RID: 17209
			internal const string UseFontPercentExpr = "UseFontPercentExpr";

			// Token: 0x0400433A RID: 17210
			internal const string GaugeExprHost = "GaugeExprHost";

			// Token: 0x0400433B RID: 17211
			internal const string ClipContentExpr = "ClipContentExpr";

			// Token: 0x0400433C RID: 17212
			internal const string GaugeImageExprHost = "GaugeImageExprHost";

			// Token: 0x0400433D RID: 17213
			internal const string AspectRatioExpr = "AspectRatioExpr";

			// Token: 0x0400433E RID: 17214
			internal const string GaugeInputValueExprHost = "GaugeInputValueExprHost";

			// Token: 0x0400433F RID: 17215
			internal const string FormulaExpr = "FormulaExpr";

			// Token: 0x04004340 RID: 17216
			internal const string MinPercentExpr = "MinPercentExpr";

			// Token: 0x04004341 RID: 17217
			internal const string MaxPercentExpr = "MaxPercentExpr";

			// Token: 0x04004342 RID: 17218
			internal const string MultiplierExpr = "MultiplierExpr";

			// Token: 0x04004343 RID: 17219
			internal const string AddConstantExpr = "AddConstantExpr";

			// Token: 0x04004344 RID: 17220
			internal const string GaugeLabelExprHost = "GaugeLabelExprHost";

			// Token: 0x04004345 RID: 17221
			internal const string AntiAliasingExpr = "AntiAliasingExpr";

			// Token: 0x04004346 RID: 17222
			internal const string AutoLayoutExpr = "AutoLayoutExpr";

			// Token: 0x04004347 RID: 17223
			internal const string ShadowIntensityExpr = "ShadowIntensityExpr";

			// Token: 0x04004348 RID: 17224
			internal const string TileLanguageExpr = "TileLanguageExpr";

			// Token: 0x04004349 RID: 17225
			internal const string TextAntiAliasingQualityExpr = "TextAntiAliasingQualityExpr";

			// Token: 0x0400434A RID: 17226
			internal const string GaugePanelItemExprHost = "GaugePanelItemExprHost";

			// Token: 0x0400434B RID: 17227
			internal const string TopExpr = "TopExpr";

			// Token: 0x0400434C RID: 17228
			internal const string HeightExpr = "HeightExpr";

			// Token: 0x0400434D RID: 17229
			internal const string GaugePointerExprHost = "GaugePointerExprHost";

			// Token: 0x0400434E RID: 17230
			internal const string BarStartExpr = "BarStartExpr";

			// Token: 0x0400434F RID: 17231
			internal const string MarkerLengthExpr = "MarkerLengthExpr";

			// Token: 0x04004350 RID: 17232
			internal const string MarkerStyleExpr = "MarkerStyleExpr";

			// Token: 0x04004351 RID: 17233
			internal const string SnappingEnabledExpr = "SnappingEnabledExpr";

			// Token: 0x04004352 RID: 17234
			internal const string SnappingIntervalExpr = "SnappingIntervalExpr";

			// Token: 0x04004353 RID: 17235
			internal const string GaugeScaleExprHost = "GaugeScaleExprHost";

			// Token: 0x04004354 RID: 17236
			internal const string LogarithmicExpr = "LogarithmicExpr";

			// Token: 0x04004355 RID: 17237
			internal const string LogarithmicBaseExpr = "LogarithmicBaseExpr";

			// Token: 0x04004356 RID: 17238
			internal const string TickMarksOnTopExpr = "TickMarksOnTopExpr";

			// Token: 0x04004357 RID: 17239
			internal const string GaugeTickMarksExprHost = "GaugeTickMarksExprHost";

			// Token: 0x04004358 RID: 17240
			internal const string LinearGaugeExprHost = "LinearGaugeExprHost";

			// Token: 0x04004359 RID: 17241
			internal const string OrientationExpr = "OrientationExpr";

			// Token: 0x0400435A RID: 17242
			internal const string LinearPointerExprHost = "LinearPointerExprHost";

			// Token: 0x0400435B RID: 17243
			internal const string LinearScaleExprHost = "LinearScaleExprHost";

			// Token: 0x0400435C RID: 17244
			internal const string StartMarginExpr = "StartMarginExpr";

			// Token: 0x0400435D RID: 17245
			internal const string EndMarginExpr = "EndMarginExpr";

			// Token: 0x0400435E RID: 17246
			internal const string NumericIndicatorExprHost = "NumericIndicatorExprHost";

			// Token: 0x0400435F RID: 17247
			internal const string PinLabelExprHost = "PinLabelExprHost";

			// Token: 0x04004360 RID: 17248
			internal const string AllowUpsideDownExpr = "AllowUpsideDownExpr";

			// Token: 0x04004361 RID: 17249
			internal const string RotateLabelExpr = "RotateLabelExpr";

			// Token: 0x04004362 RID: 17250
			internal const string PointerCapExprHost = "PointerCapExprHost";

			// Token: 0x04004363 RID: 17251
			internal const string OnTopExpr = "OnTopExpr";

			// Token: 0x04004364 RID: 17252
			internal const string ReflectionExpr = "ReflectionExpr";

			// Token: 0x04004365 RID: 17253
			internal const string CapStyleExpr = "CapStyleExpr";

			// Token: 0x04004366 RID: 17254
			internal const string RadialGaugeExprHost = "RadialGaugeExprHost";

			// Token: 0x04004367 RID: 17255
			internal const string PivotXExpr = "PivotXExpr";

			// Token: 0x04004368 RID: 17256
			internal const string PivotYExpr = "PivotYExpr";

			// Token: 0x04004369 RID: 17257
			internal const string RadialPointerExprHost = "RadialPointerExprHost";

			// Token: 0x0400436A RID: 17258
			internal const string NeedleStyleExpr = "NeedleStyleExpr";

			// Token: 0x0400436B RID: 17259
			internal const string RadialScaleExprHost = "RadialScaleExprHost";

			// Token: 0x0400436C RID: 17260
			internal const string RadiusExpr = "RadiusExpr";

			// Token: 0x0400436D RID: 17261
			internal const string StartAngleExpr = "StartAngleExpr";

			// Token: 0x0400436E RID: 17262
			internal const string SweepAngleExpr = "SweepAngleExpr";

			// Token: 0x0400436F RID: 17263
			internal const string ScaleLabelsExprHost = "ScaleLabelsExprHost";

			// Token: 0x04004370 RID: 17264
			internal const string RotateLabelsExpr = "RotateLabelsExpr";

			// Token: 0x04004371 RID: 17265
			internal const string ShowEndLabelsExpr = "ShowEndLabelsExpr";

			// Token: 0x04004372 RID: 17266
			internal const string ScalePinExprHost = "ScalePinExprHost";

			// Token: 0x04004373 RID: 17267
			internal const string EnableExpr = "EnableExpr";

			// Token: 0x04004374 RID: 17268
			internal const string ScaleRangeExprHost = "ScaleRangeExprHost";

			// Token: 0x04004375 RID: 17269
			internal const string DistanceFromScaleExpr = "DistanceFromScaleExpr";

			// Token: 0x04004376 RID: 17270
			internal const string StartWidthExpr = "StartWidthExpr";

			// Token: 0x04004377 RID: 17271
			internal const string EndWidthExpr = "EndWidthExpr";

			// Token: 0x04004378 RID: 17272
			internal const string InRangeBarPointerColorExpr = "InRangeBarPointerColorExpr";

			// Token: 0x04004379 RID: 17273
			internal const string InRangeLabelColorExpr = "InRangeLabelColorExpr";

			// Token: 0x0400437A RID: 17274
			internal const string InRangeTickMarksColorExpr = "InRangeTickMarksColorExpr";

			// Token: 0x0400437B RID: 17275
			internal const string BackgroundGradientTypeExpr = "BackgroundGradientTypeExpr";

			// Token: 0x0400437C RID: 17276
			internal const string PlacementExpr = "PlacementExpr";

			// Token: 0x0400437D RID: 17277
			internal const string StateIndicatorExprHost = "StateIndicatorExprHost";

			// Token: 0x0400437E RID: 17278
			internal const string ThermometerExprHost = "ThermometerExprHost";

			// Token: 0x0400437F RID: 17279
			internal const string BulbOffsetExpr = "BulbOffsetExpr";

			// Token: 0x04004380 RID: 17280
			internal const string BulbSizeExpr = "BulbSizeExpr";

			// Token: 0x04004381 RID: 17281
			internal const string ThermometerStyleExpr = "ThermometerStyleExpr";

			// Token: 0x04004382 RID: 17282
			internal const string TickMarkStyleExprHost = "TickMarkStyleExprHost";

			// Token: 0x04004383 RID: 17283
			internal const string EnableGradientExpr = "EnableGradientExpr";

			// Token: 0x04004384 RID: 17284
			internal const string GradientDensityExpr = "GradientDensityExpr";

			// Token: 0x04004385 RID: 17285
			internal const string GaugeMajorTickMarksHost = "GaugeMajorTickMarksHost";

			// Token: 0x04004386 RID: 17286
			internal const string GaugeMinorTickMarksHost = "GaugeMinorTickMarksHost";

			// Token: 0x04004387 RID: 17287
			internal const string GaugeMaximumPinHost = "MaximumPinHost";

			// Token: 0x04004388 RID: 17288
			internal const string GaugeMinimumPinHost = "MinimumPinHost";

			// Token: 0x04004389 RID: 17289
			internal const string GaugeInputValueHost = "GaugeInputValueHost";

			// Token: 0x0400438A RID: 17290
			internal const string WidthExpr = "WidthExpr";

			// Token: 0x0400438B RID: 17291
			internal const string ZIndexExpr = "ZIndexExpr";

			// Token: 0x0400438C RID: 17292
			internal const string PositionExpr = "PositionExpr";

			// Token: 0x0400438D RID: 17293
			internal const string ShapeExpr = "ShapeExpr";

			// Token: 0x0400438E RID: 17294
			internal const string ScaleLabelsHost = "ScaleLabelsHost";

			// Token: 0x0400438F RID: 17295
			internal const string ScalePinHost = "ScalePinHost";

			// Token: 0x04004390 RID: 17296
			internal const string PinLabelHost = "PinLabelHost";

			// Token: 0x04004391 RID: 17297
			internal const string PointerCapHost = "PointerCapHost";

			// Token: 0x04004392 RID: 17298
			internal const string ThermometerHost = "ThermometerHost";

			// Token: 0x04004393 RID: 17299
			internal const string TickMarkStyleHost = "TickMarkStyleHost";

			// Token: 0x04004394 RID: 17300
			internal const string ResizeModeExpr = "ResizeModeExpr";

			// Token: 0x04004395 RID: 17301
			internal const string TextShadowOffsetExpr = "TextShadowOffsetExpr";

			// Token: 0x04004396 RID: 17302
			internal const string CustomLabelsHosts = "m_customLabelsHostsRemotable";

			// Token: 0x04004397 RID: 17303
			internal const string GaugeImagesHosts = "m_gaugeImagesHostsRemotable";

			// Token: 0x04004398 RID: 17304
			internal const string GaugeLabelsHosts = "m_gaugeLabelsHostsRemotable";

			// Token: 0x04004399 RID: 17305
			internal const string LinearGaugesHosts = "m_linearGaugesHostsRemotable";

			// Token: 0x0400439A RID: 17306
			internal const string RadialGaugesHosts = "m_radialGaugesHostsRemotable";

			// Token: 0x0400439B RID: 17307
			internal const string LinearPointersHosts = "m_linearPointersHostsRemotable";

			// Token: 0x0400439C RID: 17308
			internal const string RadialPointersHosts = "m_radialPointersHostsRemotable";

			// Token: 0x0400439D RID: 17309
			internal const string LinearScalesHosts = "m_linearScalesHostsRemotable";

			// Token: 0x0400439E RID: 17310
			internal const string RadialScalesHosts = "m_radialScalesHostsRemotable";

			// Token: 0x0400439F RID: 17311
			internal const string ScaleRangesHosts = "m_scaleRangesHostsRemotable";

			// Token: 0x040043A0 RID: 17312
			internal const string NumericIndicatorsHosts = "m_numericIndicatorsHostsRemotable";

			// Token: 0x040043A1 RID: 17313
			internal const string StateIndicatorsHosts = "m_stateIndicatorsHostsRemotable";

			// Token: 0x040043A2 RID: 17314
			internal const string GaugeInputValuesHosts = "m_gaugeInputValueHostsRemotable";

			// Token: 0x040043A3 RID: 17315
			internal const string NumericIndicatorRangesHosts = "m_numericIndicatorRangesHostsRemotable";

			// Token: 0x040043A4 RID: 17316
			internal const string IndicatorStatesHosts = "m_indicatorStatesHostsRemotable";

			// Token: 0x040043A5 RID: 17317
			internal const string NumericIndicatorHost = "NumericIndicatorHost";

			// Token: 0x040043A6 RID: 17318
			internal const string DecimalDigitColorExpr = "DecimalDigitColorExpr";

			// Token: 0x040043A7 RID: 17319
			internal const string DigitColorExpr = "DigitColorExpr";

			// Token: 0x040043A8 RID: 17320
			internal const string DecimalDigitsExpr = "DecimalDigitsExpr";

			// Token: 0x040043A9 RID: 17321
			internal const string DigitsExpr = "DigitsExpr";

			// Token: 0x040043AA RID: 17322
			internal const string NonNumericStringExpr = "NonNumericStringExpr";

			// Token: 0x040043AB RID: 17323
			internal const string OutOfRangeStringExpr = "OutOfRangeStringExpr";

			// Token: 0x040043AC RID: 17324
			internal const string ShowDecimalPointExpr = "ShowDecimalPointExpr";

			// Token: 0x040043AD RID: 17325
			internal const string ShowLeadingZerosExpr = "ShowLeadingZerosExpr";

			// Token: 0x040043AE RID: 17326
			internal const string IndicatorStyleExpr = "IndicatorStyleExpr";

			// Token: 0x040043AF RID: 17327
			internal const string ShowSignExpr = "ShowSignExpr";

			// Token: 0x040043B0 RID: 17328
			internal const string LedDimColorExpr = "LedDimColorExpr";

			// Token: 0x040043B1 RID: 17329
			internal const string SeparatorWidthExpr = "SeparatorWidthExpr";

			// Token: 0x040043B2 RID: 17330
			internal const string NumericIndicatorRangeExprHost = "NumericIndicatorRangeExprHost";

			// Token: 0x040043B3 RID: 17331
			internal const string NumericIndicatorRangeHost = "NumericIndicatorRangeHost";

			// Token: 0x040043B4 RID: 17332
			internal const string StateIndicatorHost = "StateIndicatorHost";

			// Token: 0x040043B5 RID: 17333
			internal const string IndicatorStateExprHost = "IndicatorStateExprHost";

			// Token: 0x040043B6 RID: 17334
			internal const string IndicatorStateHost = "IndicatorStateHost";

			// Token: 0x040043B7 RID: 17335
			internal const string TransformationTypeExpr = "TransformationTypeExpr";

			// Token: 0x040043B8 RID: 17336
			internal const string MapViewExprHost = "MapViewExprHost";

			// Token: 0x040043B9 RID: 17337
			internal const string MapViewHost = "MapViewHost";

			// Token: 0x040043BA RID: 17338
			internal const string ZoomExpr = "ZoomExpr";

			// Token: 0x040043BB RID: 17339
			internal const string MapElementViewExprHost = "MapElementViewExprHost";

			// Token: 0x040043BC RID: 17340
			internal const string MapElementViewHost = "MapElementViewHost";

			// Token: 0x040043BD RID: 17341
			internal const string LayerNameExpr = "LayerNameExpr";

			// Token: 0x040043BE RID: 17342
			internal const string MapDataBoundViewExprHost = "MapDataBoundViewExprHost";

			// Token: 0x040043BF RID: 17343
			internal const string MapDataBoundViewHost = "MapDataBoundViewHost";

			// Token: 0x040043C0 RID: 17344
			internal const string MapCustomViewExprHost = "MapCustomViewExprHost";

			// Token: 0x040043C1 RID: 17345
			internal const string MapCustomViewHost = "MapCustomViewHost";

			// Token: 0x040043C2 RID: 17346
			internal const string CenterXExpr = "CenterXExpr";

			// Token: 0x040043C3 RID: 17347
			internal const string CenterYExpr = "CenterYExpr";

			// Token: 0x040043C4 RID: 17348
			internal const string MapBorderSkinExprHost = "MapBorderSkinExprHost";

			// Token: 0x040043C5 RID: 17349
			internal const string MapBorderSkinHost = "MapBorderSkinHost";

			// Token: 0x040043C6 RID: 17350
			internal const string MapBorderSkinTypeExpr = "MapBorderSkinTypeExpr";

			// Token: 0x040043C7 RID: 17351
			internal const string MapDataRegionNameExpr = "MapDataRegionNameExpr";

			// Token: 0x040043C8 RID: 17352
			internal const string MapTileLayerExprHost = "MapTileLayerExprHost";

			// Token: 0x040043C9 RID: 17353
			internal const string MapTileLayerHost = "MapTileLayerHost";

			// Token: 0x040043CA RID: 17354
			internal const string ServiceUrlExpr = "ServiceUrlExpr";

			// Token: 0x040043CB RID: 17355
			internal const string TileStyleExpr = "TileStyleExpr";

			// Token: 0x040043CC RID: 17356
			internal const string MapTileExprHost = "MapTileExprHost";

			// Token: 0x040043CD RID: 17357
			internal const string MapTileHost = "MapTileHost";

			// Token: 0x040043CE RID: 17358
			internal const string UseSecureConnectionExpr = "UseSecureConnectionExpr";

			// Token: 0x040043CF RID: 17359
			internal const string MapPolygonLayerExprHost = "MapPolygonLayerExprHost";

			// Token: 0x040043D0 RID: 17360
			internal const string MapPointLayerExprHost = "MapPointLayerExprHost";

			// Token: 0x040043D1 RID: 17361
			internal const string MapLineLayerExprHost = "MapLineLayerExprHost";

			// Token: 0x040043D2 RID: 17362
			internal const string MapSpatialDataSetExprHost = "MapSpatialDataSetExprHost";

			// Token: 0x040043D3 RID: 17363
			internal const string DataSetNameExpr = "DataSetNameExpr";

			// Token: 0x040043D4 RID: 17364
			internal const string SpatialFieldExpr = "SpatialFieldExpr";

			// Token: 0x040043D5 RID: 17365
			internal const string MapSpatialDataRegionExprHost = "MapSpatialDataRegionExprHost";

			// Token: 0x040043D6 RID: 17366
			internal const string VectorDataExpr = "VectorDataExpr";

			// Token: 0x040043D7 RID: 17367
			internal const string MapSpatialDataExprHost = "MapSpatialDataExprHost";

			// Token: 0x040043D8 RID: 17368
			internal const string MapSpatialDataHost = "MapSpatialDataHost";

			// Token: 0x040043D9 RID: 17369
			internal const string SimplificationResolutionExpr = "SimplificationResolutionExpr";

			// Token: 0x040043DA RID: 17370
			internal const string MapShapefileExprHost = "MapShapefileExprHost";

			// Token: 0x040043DB RID: 17371
			internal const string MapLayerExprHost = "MapLayerExprHost";

			// Token: 0x040043DC RID: 17372
			internal const string MapLayerHost = "MapLayerHost";

			// Token: 0x040043DD RID: 17373
			internal const string VisibilityModeExpr = "VisibilityModeExpr";

			// Token: 0x040043DE RID: 17374
			internal const string MapFieldNameExprHost = "MapFieldNameExprHost";

			// Token: 0x040043DF RID: 17375
			internal const string MapFieldNameHost = "MapFieldNameHost";

			// Token: 0x040043E0 RID: 17376
			internal const string NameExpr = "NameExpr";

			// Token: 0x040043E1 RID: 17377
			internal const string MapFieldDefinitionExprHost = "MapFieldDefinitionExprHost";

			// Token: 0x040043E2 RID: 17378
			internal const string MapFieldDefinitionHost = "MapFieldDefinitionHost";

			// Token: 0x040043E3 RID: 17379
			internal const string MapPointExprHost = "MapPointExprHost";

			// Token: 0x040043E4 RID: 17380
			internal const string MapPointHost = "MapPointHost";

			// Token: 0x040043E5 RID: 17381
			internal const string MapSpatialElementExprHost = "MapSpatialElementExprHost";

			// Token: 0x040043E6 RID: 17382
			internal const string MapSpatialElementHost = "MapSpatialElementHost";

			// Token: 0x040043E7 RID: 17383
			internal const string MapPolygonExprHost = "MapPolygonExprHost";

			// Token: 0x040043E8 RID: 17384
			internal const string MapPolygonHost = "MapPolygonHost";

			// Token: 0x040043E9 RID: 17385
			internal const string UseCustomPolygonTemplateExpr = "UseCustomPolygonTemplateExpr";

			// Token: 0x040043EA RID: 17386
			internal const string UseCustomPointTemplateExpr = "UseCustomPointTemplateExpr";

			// Token: 0x040043EB RID: 17387
			internal const string MapLineExprHost = "MapLineExprHost";

			// Token: 0x040043EC RID: 17388
			internal const string MapLineHost = "MapLineHost";

			// Token: 0x040043ED RID: 17389
			internal const string UseCustomLineTemplateExpr = "UseCustomLineTemplateExpr";

			// Token: 0x040043EE RID: 17390
			internal const string MapFieldExprHost = "MapFieldExprHost";

			// Token: 0x040043EF RID: 17391
			internal const string MapFieldHost = "MapFieldHost";

			// Token: 0x040043F0 RID: 17392
			internal const string MapPointTemplateExprHost = "MapPointTemplateExprHost";

			// Token: 0x040043F1 RID: 17393
			internal const string MapPointTemplateHost = "MapPointTemplateHost";

			// Token: 0x040043F2 RID: 17394
			internal const string MapMarkerTemplateExprHost = "MapMarkerTemplateExprHost";

			// Token: 0x040043F3 RID: 17395
			internal const string MapMarkerTemplateHost = "MapMarkerTemplateHost";

			// Token: 0x040043F4 RID: 17396
			internal const string MapPolygonTemplateExprHost = "MapPolygonTemplateExprHost";

			// Token: 0x040043F5 RID: 17397
			internal const string MapPolygonTemplateHost = "MapPolygonTemplateHost";

			// Token: 0x040043F6 RID: 17398
			internal const string ScaleFactorExpr = "ScaleFactorExpr";

			// Token: 0x040043F7 RID: 17399
			internal const string CenterPointOffsetXExpr = "CenterPointOffsetXExpr";

			// Token: 0x040043F8 RID: 17400
			internal const string CenterPointOffsetYExpr = "CenterPointOffsetYExpr";

			// Token: 0x040043F9 RID: 17401
			internal const string ShowLabelExpr = "ShowLabelExpr";

			// Token: 0x040043FA RID: 17402
			internal const string MapLineTemplateExprHost = "MapLineTemplateExprHost";

			// Token: 0x040043FB RID: 17403
			internal const string MapLineTemplateHost = "MapLineTemplateHost";

			// Token: 0x040043FC RID: 17404
			internal const string MapCustomColorRuleExprHost = "MapCustomColorRuleExprHost";

			// Token: 0x040043FD RID: 17405
			internal const string MapCustomColorExprHost = "MapCustomColorExprHost";

			// Token: 0x040043FE RID: 17406
			internal const string MapCustomColorHost = "MapCustomColorHost";

			// Token: 0x040043FF RID: 17407
			internal const string MapPointRulesExprHost = "MapPointRulesExprHost";

			// Token: 0x04004400 RID: 17408
			internal const string MapPointRulesHost = "MapPointRulesHost";

			// Token: 0x04004401 RID: 17409
			internal const string MapMarkerRuleExprHost = "MapMarkerRuleExprHost";

			// Token: 0x04004402 RID: 17410
			internal const string MapMarkerRuleHost = "MapMarkerRuleHost";

			// Token: 0x04004403 RID: 17411
			internal const string MapMarkerExprHost = "MapMarkerExprHost";

			// Token: 0x04004404 RID: 17412
			internal const string MapMarkerHost = "MapMarkerHost";

			// Token: 0x04004405 RID: 17413
			internal const string MapMarkerStyleExpr = "MapMarkerStyleExpr";

			// Token: 0x04004406 RID: 17414
			internal const string MapMarkerImageExprHost = "MapMarkerImageExprHost";

			// Token: 0x04004407 RID: 17415
			internal const string MapMarkerImageHost = "MapMarkerImageHost";

			// Token: 0x04004408 RID: 17416
			internal const string MapSizeRuleExprHost = "MapSizeRuleExprHost";

			// Token: 0x04004409 RID: 17417
			internal const string MapSizeRuleHost = "MapSizeRuleHost";

			// Token: 0x0400440A RID: 17418
			internal const string StartSizeExpr = "StartSizeExpr";

			// Token: 0x0400440B RID: 17419
			internal const string EndSizeExpr = "EndSizeExpr";

			// Token: 0x0400440C RID: 17420
			internal const string MapPolygonRulesExprHost = "MapPolygonRulesExprHost";

			// Token: 0x0400440D RID: 17421
			internal const string MapPolygonRulesHost = "MapPolygonRulesHost";

			// Token: 0x0400440E RID: 17422
			internal const string MapLineRulesExprHost = "MapLineRulesExprHost";

			// Token: 0x0400440F RID: 17423
			internal const string MapLineRulesHost = "MapLineRulesHost";

			// Token: 0x04004410 RID: 17424
			internal const string MapColorRuleExprHost = "MapColorRuleExprHost";

			// Token: 0x04004411 RID: 17425
			internal const string MapColorRuleHost = "MapColorRuleHost";

			// Token: 0x04004412 RID: 17426
			internal const string ShowInColorScaleExpr = "ShowInColorScaleExpr";

			// Token: 0x04004413 RID: 17427
			internal const string MapColorRangeRuleExprHost = "MapColorRangeRuleExprHost";

			// Token: 0x04004414 RID: 17428
			internal const string StartColorExpr = "StartColorExpr";

			// Token: 0x04004415 RID: 17429
			internal const string MiddleColorExpr = "MiddleColorExpr";

			// Token: 0x04004416 RID: 17430
			internal const string EndColorExpr = "EndColorExpr";

			// Token: 0x04004417 RID: 17431
			internal const string MapColorPaletteRuleExprHost = "MapColorPaletteRuleExprHost";

			// Token: 0x04004418 RID: 17432
			internal const string MapBucketExprHost = "MapBucketExprHost";

			// Token: 0x04004419 RID: 17433
			internal const string MapBucketHost = "MapBucketHost";

			// Token: 0x0400441A RID: 17434
			internal const string MapAppearanceRuleExprHost = "MapAppearanceRuleExprHost";

			// Token: 0x0400441B RID: 17435
			internal const string MapAppearanceRuleHost = "MapAppearanceRuleHost";

			// Token: 0x0400441C RID: 17436
			internal const string DataValueExpr = "DataValueExpr";

			// Token: 0x0400441D RID: 17437
			internal const string DistributionTypeExpr = "DistributionTypeExpr";

			// Token: 0x0400441E RID: 17438
			internal const string BucketCountExpr = "BucketCountExpr";

			// Token: 0x0400441F RID: 17439
			internal const string StartValueExpr = "StartValueExpr";

			// Token: 0x04004420 RID: 17440
			internal const string EndValueExpr = "EndValueExpr";

			// Token: 0x04004421 RID: 17441
			internal const string MapLegendTitleExprHost = "MapLegendTitleExprHost";

			// Token: 0x04004422 RID: 17442
			internal const string MapLegendTitleHost = "MapLegendTitleHost";

			// Token: 0x04004423 RID: 17443
			internal const string TitleSeparatorColorExpr = "TitleSeparatorColorExpr";

			// Token: 0x04004424 RID: 17444
			internal const string MapLegendExprHost = "MapLegendExprHost";

			// Token: 0x04004425 RID: 17445
			internal const string MapLegendHost = "MapLegendHost";

			// Token: 0x04004426 RID: 17446
			internal const string MapLocationExprHost = "MapLocationExprHost";

			// Token: 0x04004427 RID: 17447
			internal const string MapLocationHost = "MapLocationHost";

			// Token: 0x04004428 RID: 17448
			internal const string MapSizeExprHost = "MapSizeExprHost";

			// Token: 0x04004429 RID: 17449
			internal const string MapSizeHost = "MapSizeHost";

			// Token: 0x0400442A RID: 17450
			internal const string UnitExpr = "UnitExpr";

			// Token: 0x0400442B RID: 17451
			internal const string MapGridLinesExprHost = "MapGridLinesExprHost";

			// Token: 0x0400442C RID: 17452
			internal const string MapMeridiansHost = "MapMeridiansHost";

			// Token: 0x0400442D RID: 17453
			internal const string MapParallelsHost = "MapParallelsHost";

			// Token: 0x0400442E RID: 17454
			internal const string ShowLabelsExpr = "ShowLabelsExpr";

			// Token: 0x0400442F RID: 17455
			internal const string LabelPositionExpr = "LabelPositionExpr";

			// Token: 0x04004430 RID: 17456
			internal const string MapHosts = "m_mapHostsRemotable";

			// Token: 0x04004431 RID: 17457
			internal const string MapDataRegionHosts = "m_mapDataRegionHostsRemotable";

			// Token: 0x04004432 RID: 17458
			internal const string MapDockableSubItemExprHost = "MapDockableSubItemExprHost";

			// Token: 0x04004433 RID: 17459
			internal const string MapDockableSubItemHost = "MapDockableSubItemHost";

			// Token: 0x04004434 RID: 17460
			internal const string DockOutsideViewportExpr = "DockOutsideViewportExpr";

			// Token: 0x04004435 RID: 17461
			internal const string MapSubItemExprHost = "MapSubItemExprHost";

			// Token: 0x04004436 RID: 17462
			internal const string MapSubItemHost = "MapSubItemHost";

			// Token: 0x04004437 RID: 17463
			internal const string MapBindingFieldPairExprHost = "MapBindingFieldPairExprHost";

			// Token: 0x04004438 RID: 17464
			internal const string MapBindingFieldPairHost = "MapBindingFieldPairHost";

			// Token: 0x04004439 RID: 17465
			internal const string FieldNameExpr = "FieldNameExpr";

			// Token: 0x0400443A RID: 17466
			internal const string BindingExpressionExpr = "BindingExpressionExpr";

			// Token: 0x0400443B RID: 17467
			internal const string ZoomEnabledExpr = "ZoomEnabledExpr";

			// Token: 0x0400443C RID: 17468
			internal const string MapViewportExprHost = "MapViewportExprHost";

			// Token: 0x0400443D RID: 17469
			internal const string MapViewportHost = "MapViewportHost";

			// Token: 0x0400443E RID: 17470
			internal const string MapCoordinateSystemExpr = "MapCoordinateSystemExpr";

			// Token: 0x0400443F RID: 17471
			internal const string MapProjectionExpr = "MapProjectionExpr";

			// Token: 0x04004440 RID: 17472
			internal const string ProjectionCenterXExpr = "ProjectionCenterXExpr";

			// Token: 0x04004441 RID: 17473
			internal const string ProjectionCenterYExpr = "ProjectionCenterYExpr";

			// Token: 0x04004442 RID: 17474
			internal const string MaximumZoomExpr = "MaximumZoomExpr";

			// Token: 0x04004443 RID: 17475
			internal const string MinimumZoomExpr = "MinimumZoomExpr";

			// Token: 0x04004444 RID: 17476
			internal const string ContentMarginExpr = "ContentMarginExpr";

			// Token: 0x04004445 RID: 17477
			internal const string GridUnderContentExpr = "GridUnderContentExpr";

			// Token: 0x04004446 RID: 17478
			internal const string MapBindingFieldPairsHosts = "m_mapBindingFieldPairsHostsRemotable";

			// Token: 0x04004447 RID: 17479
			internal const string MapLimitsExprHost = "MapLimitsExprHost";

			// Token: 0x04004448 RID: 17480
			internal const string MapLimitsHost = "MapLimitsHost";

			// Token: 0x04004449 RID: 17481
			internal const string MinimumXExpr = "MinimumXExpr";

			// Token: 0x0400444A RID: 17482
			internal const string MinimumYExpr = "MinimumYExpr";

			// Token: 0x0400444B RID: 17483
			internal const string MaximumXExpr = "MaximumXExpr";

			// Token: 0x0400444C RID: 17484
			internal const string MaximumYExpr = "MaximumYExpr";

			// Token: 0x0400444D RID: 17485
			internal const string LimitToDataExpr = "LimitToDataExpr";

			// Token: 0x0400444E RID: 17486
			internal const string MapColorScaleExprHost = "MapColorScaleExprHost";

			// Token: 0x0400444F RID: 17487
			internal const string MapColorScaleHost = "MapColorScaleHost";

			// Token: 0x04004450 RID: 17488
			internal const string TickMarkLengthExpr = "TickMarkLengthExpr";

			// Token: 0x04004451 RID: 17489
			internal const string ColorBarBorderColorExpr = "ColorBarBorderColorExpr";

			// Token: 0x04004452 RID: 17490
			internal const string LabelFormatExpr = "LabelFormatExpr";

			// Token: 0x04004453 RID: 17491
			internal const string LabelPlacementExpr = "LabelPlacementExpr";

			// Token: 0x04004454 RID: 17492
			internal const string LabelBehaviorExpr = "LabelBehaviorExpr";

			// Token: 0x04004455 RID: 17493
			internal const string RangeGapColorExpr = "RangeGapColorExpr";

			// Token: 0x04004456 RID: 17494
			internal const string NoDataTextExpr = "NoDataTextExpr";

			// Token: 0x04004457 RID: 17495
			internal const string MapColorScaleTitleExprHost = "MapColorScaleTitleExprHost";

			// Token: 0x04004458 RID: 17496
			internal const string MapColorScaleTitleHost = "MapColorScaleTitleHost";

			// Token: 0x04004459 RID: 17497
			internal const string MapDistanceScaleExprHost = "MapDistanceScaleExprHost";

			// Token: 0x0400445A RID: 17498
			internal const string MapDistanceScaleHost = "MapDistanceScaleHost";

			// Token: 0x0400445B RID: 17499
			internal const string ScaleColorExpr = "ScaleColorExpr";

			// Token: 0x0400445C RID: 17500
			internal const string ScaleBorderColorExpr = "ScaleBorderColorExpr";

			// Token: 0x0400445D RID: 17501
			internal const string MapTitleExprHost = "MapTitleExprHost";

			// Token: 0x0400445E RID: 17502
			internal const string MapTitleHost = "MapTitleHost";

			// Token: 0x0400445F RID: 17503
			internal const string MapLegendsHosts = "m_mapLegendsHostsRemotable";

			// Token: 0x04004460 RID: 17504
			internal const string MapTitlesHosts = "m_mapTitlesHostsRemotable";

			// Token: 0x04004461 RID: 17505
			internal const string MapMarkersHosts = "m_mapMarkersHostsRemotable";

			// Token: 0x04004462 RID: 17506
			internal const string MapBucketsHosts = "m_mapBucketsHostsRemotable";

			// Token: 0x04004463 RID: 17507
			internal const string MapCustomColorsHosts = "m_mapCustomColorsHostsRemotable";

			// Token: 0x04004464 RID: 17508
			internal const string MapPointsHosts = "m_mapPointsHostsRemotable";

			// Token: 0x04004465 RID: 17509
			internal const string MapPolygonsHosts = "m_mapPolygonsHostsRemotable";

			// Token: 0x04004466 RID: 17510
			internal const string MapLinesHosts = "m_mapLinesHostsRemotable";

			// Token: 0x04004467 RID: 17511
			internal const string MapTileLayersHosts = "m_mapTileLayersHostsRemotable";

			// Token: 0x04004468 RID: 17512
			internal const string MapTilesHosts = "m_mapTilesHostsRemotable";

			// Token: 0x04004469 RID: 17513
			internal const string MapPointLayersHosts = "m_mapPointLayersHostsRemotable";

			// Token: 0x0400446A RID: 17514
			internal const string MapPolygonLayersHosts = "m_mapPolygonLayersHostsRemotable";

			// Token: 0x0400446B RID: 17515
			internal const string MapLineLayersHosts = "m_mapLineLayersHostsRemotable";

			// Token: 0x0400446C RID: 17516
			internal const string MapFieldNamesHosts = "m_mapFieldNamesHostsRemotable";

			// Token: 0x0400446D RID: 17517
			internal const string MapExprHost = "MapExprHost";

			// Token: 0x0400446E RID: 17518
			internal const string DataElementLabelExpr = "DataElementLabelExpr";

			// Token: 0x0400446F RID: 17519
			internal const string ParagraphExprHost = "ParagraphExprHost";

			// Token: 0x04004470 RID: 17520
			internal const string ParagraphHosts = "m_paragraphHostsRemotable";

			// Token: 0x04004471 RID: 17521
			internal const string LeftIndentExpr = "LeftIndentExpr";

			// Token: 0x04004472 RID: 17522
			internal const string RightIndentExpr = "RightIndentExpr";

			// Token: 0x04004473 RID: 17523
			internal const string HangingIndentExpr = "HangingIndentExpr";

			// Token: 0x04004474 RID: 17524
			internal const string SpaceBeforeExpr = "SpaceBeforeExpr";

			// Token: 0x04004475 RID: 17525
			internal const string SpaceAfterExpr = "SpaceAfterExpr";

			// Token: 0x04004476 RID: 17526
			internal const string ListStyleExpr = "ListStyleExpr";

			// Token: 0x04004477 RID: 17527
			internal const string ListLevelExpr = "ListLevelExpr";

			// Token: 0x04004478 RID: 17528
			internal const string TextRunExprHost = "TextRunExprHost";

			// Token: 0x04004479 RID: 17529
			internal const string TextRunHosts = "m_textRunHostsRemotable";

			// Token: 0x0400447A RID: 17530
			internal const string MarkupTypeExpr = "MarkupTypeExpr";

			// Token: 0x0400447B RID: 17531
			internal const string LookupSourceExpr = "SourceExpr";

			// Token: 0x0400447C RID: 17532
			internal const string LookupDestExpr = "DestExpr";

			// Token: 0x0400447D RID: 17533
			internal const string LookupResultExpr = "ResultExpr";

			// Token: 0x0400447E RID: 17534
			internal const string PageBreakExprHost = "PageBreakExprHost";

			// Token: 0x0400447F RID: 17535
			internal const string PageBreakDisabledExpr = "DisabledExpr";

			// Token: 0x04004480 RID: 17536
			internal const string PageBreakPageNameExpr = "PageNameExpr";

			// Token: 0x04004481 RID: 17537
			internal const string PageBreakResetPageNumberExpr = "ResetPageNumberExpr";

			// Token: 0x04004482 RID: 17538
			internal const string JoinConditionForeignKeyExpr = "ForeignKeyExpr";

			// Token: 0x04004483 RID: 17539
			internal const string JoinConditionPrimaryKeyExpr = "PrimaryKeyExpr";
		}

		// Token: 0x020009A1 RID: 2465
		private abstract class TypeDecl
		{
			// Token: 0x0600811E RID: 33054 RVA: 0x002138F4 File Offset: 0x00211AF4
			internal void NestedTypeAdd(string name, CodeTypeDeclaration nestedType)
			{
				this.ConstructorCreate();
				this.Type.Members.Add(nestedType);
				this.Constructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), this.CreateTypeCreateExpression(nestedType.Name)));
			}

			// Token: 0x0600811F RID: 33055 RVA: 0x00213946 File Offset: 0x00211B46
			internal int NestedTypeColAdd(string name, string baseTypeName, ref CodeExpressionCollection initializers, CodeTypeDeclaration nestedType)
			{
				this.Type.Members.Add(nestedType);
				this.TypeColInit(name, baseTypeName, ref initializers);
				return initializers.Add(this.CreateTypeCreateExpression(nestedType.Name));
			}

			// Token: 0x06008120 RID: 33056 RVA: 0x00213978 File Offset: 0x00211B78
			protected TypeDecl(string typeName, string baseTypeName, ExprHostBuilder.TypeDecl parent, bool setCode)
			{
				this.BaseTypeName = baseTypeName;
				this.Parent = parent;
				this.m_setCode = setCode;
				this.Type = this.CreateType(typeName, baseTypeName);
			}

			// Token: 0x06008121 RID: 33057 RVA: 0x002139A4 File Offset: 0x00211BA4
			protected void ConstructorCreate()
			{
				if (this.Constructor == null)
				{
					this.Constructor = this.CreateConstructor();
					this.Type.Members.Add(this.Constructor);
				}
			}

			// Token: 0x06008122 RID: 33058 RVA: 0x002139D1 File Offset: 0x00211BD1
			protected virtual CodeConstructor CreateConstructor()
			{
				return new CodeConstructor
				{
					Attributes = MemberAttributes.Public
				};
			}

			// Token: 0x06008123 RID: 33059 RVA: 0x002139E4 File Offset: 0x00211BE4
			protected CodeAssignStatement CreateTypeColInitStatement(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
				codeObjectCreateExpression.CreateType = new CodeTypeReference((name == "m_memberTreeHostsRemotable") ? "RemoteMemberArrayWrapper" : "RemoteArrayWrapper", new CodeTypeReference[]
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

			// Token: 0x06008124 RID: 33060 RVA: 0x00213A54 File Offset: 0x00211C54
			protected virtual CodeTypeDeclaration CreateType(string name, string baseType)
			{
				Global.Tracer.Assert(name != null, "(name != null)");
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
				if (baseType != null)
				{
					codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(baseType));
				}
				codeTypeDeclaration.Attributes = (MemberAttributes)24578;
				return codeTypeDeclaration;
			}

			// Token: 0x06008125 RID: 33061 RVA: 0x00213A9C File Offset: 0x00211C9C
			private void TypeColInit(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				this.ConstructorCreate();
				if (initializers == null)
				{
					this.Constructor.Statements.Add(this.CreateTypeColInitStatement(name, baseTypeName, ref initializers));
				}
			}

			// Token: 0x06008126 RID: 33062 RVA: 0x00213AC2 File Offset: 0x00211CC2
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

			// Token: 0x04004484 RID: 17540
			internal CodeTypeDeclaration Type;

			// Token: 0x04004485 RID: 17541
			internal string BaseTypeName;

			// Token: 0x04004486 RID: 17542
			internal ExprHostBuilder.TypeDecl Parent;

			// Token: 0x04004487 RID: 17543
			internal CodeConstructor Constructor;

			// Token: 0x04004488 RID: 17544
			internal bool HasExpressions;

			// Token: 0x04004489 RID: 17545
			internal CodeExpressionCollection DataValues;

			// Token: 0x0400448A RID: 17546
			protected readonly bool m_setCode;
		}

		// Token: 0x020009A2 RID: 2466
		private sealed class RootTypeDecl : ExprHostBuilder.TypeDecl
		{
			// Token: 0x06008127 RID: 33063 RVA: 0x00213AF1 File Offset: 0x00211CF1
			internal RootTypeDecl(bool setCode)
				: base("ReportExprHostImpl", "ReportExprHost", null, setCode)
			{
			}

			// Token: 0x06008128 RID: 33064 RVA: 0x00213B08 File Offset: 0x00211D08
			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "includeParameters"));
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "parametersOnly"));
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(typeof(object), "reportObjectModel");
				codeConstructor.Parameters.Add(codeParameterDeclarationExpression);
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				this.ReportParameters = new CodeExpressionCollection();
				this.DataSources = new CodeExpressionCollection();
				this.DataSets = new CodeExpressionCollection();
				return codeConstructor;
			}

			// Token: 0x06008129 RID: 33065 RVA: 0x00213BB4 File Offset: 0x00211DB4
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

			// Token: 0x0600812A RID: 33066 RVA: 0x00213BFC File Offset: 0x00211DFC
			internal void CompleteConstructorCreation()
			{
				if (this.HasExpressions)
				{
					if (this.Constructor == null)
					{
						base.ConstructorCreate();
					}
					else
					{
						CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
						codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("parametersOnly"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
						codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement());
						this.Constructor.Statements.Insert(0, codeConditionStatement);
						if (this.ReportParameters.Count > 0)
						{
							CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
							codeConditionStatement2.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("includeParameters"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
							codeConditionStatement2.TrueStatements.Add(base.CreateTypeColInitStatement("m_reportParameterHostsRemotable", "ReportParamExprHost", ref this.ReportParameters));
							this.Constructor.Statements.Insert(0, codeConditionStatement2);
						}
						if (this.DataSources.Count > 0)
						{
							this.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSourceHostsRemotable", "DataSourceExprHost", ref this.DataSources));
						}
						if (this.DataSets.Count > 0)
						{
							this.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSetHostsRemotable", "DataSetExprHost", ref this.DataSets));
						}
					}
					Global.Tracer.Assert(this.Constructor != null, "Invalid EH constructor");
					this.CreateCustomCodeInitialization();
				}
			}

			// Token: 0x0600812B RID: 33067 RVA: 0x00213D64 File Offset: 0x00211F64
			private void CreateCustomCodeInitialization()
			{
				if (this.m_setCode)
				{
					this.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "m_codeProxyBase"), new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code")));
					this.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeObjectCreateExpression("CustomCodeProxy", new CodeExpression[]
					{
						new CodeThisReferenceExpression()
					})));
				}
			}

			// Token: 0x0400448B RID: 17547
			internal CodeExpressionCollection Aggregates;

			// Token: 0x0400448C RID: 17548
			internal CodeExpressionCollection PageSections;

			// Token: 0x0400448D RID: 17549
			internal CodeExpressionCollection ReportParameters;

			// Token: 0x0400448E RID: 17550
			internal CodeExpressionCollection DataSources;

			// Token: 0x0400448F RID: 17551
			internal CodeExpressionCollection DataSets;

			// Token: 0x04004490 RID: 17552
			internal CodeExpressionCollection Lines;

			// Token: 0x04004491 RID: 17553
			internal CodeExpressionCollection Rectangles;

			// Token: 0x04004492 RID: 17554
			internal CodeExpressionCollection TextBoxes;

			// Token: 0x04004493 RID: 17555
			internal CodeExpressionCollection Images;

			// Token: 0x04004494 RID: 17556
			internal CodeExpressionCollection Subreports;

			// Token: 0x04004495 RID: 17557
			internal CodeExpressionCollection Tablices;

			// Token: 0x04004496 RID: 17558
			internal CodeExpressionCollection DataShapes;

			// Token: 0x04004497 RID: 17559
			internal CodeExpressionCollection Charts;

			// Token: 0x04004498 RID: 17560
			internal CodeExpressionCollection GaugePanels;

			// Token: 0x04004499 RID: 17561
			internal CodeExpressionCollection CustomReportItems;

			// Token: 0x0400449A RID: 17562
			internal CodeExpressionCollection Lookups;

			// Token: 0x0400449B RID: 17563
			internal CodeExpressionCollection LookupDests;

			// Token: 0x0400449C RID: 17564
			internal CodeExpressionCollection Pages;

			// Token: 0x0400449D RID: 17565
			internal CodeExpressionCollection ReportSections;

			// Token: 0x0400449E RID: 17566
			internal CodeExpressionCollection Maps;
		}

		// Token: 0x020009A3 RID: 2467
		private sealed class NonRootTypeDecl : ExprHostBuilder.TypeDecl
		{
			// Token: 0x0600812C RID: 33068 RVA: 0x00213DEA File Offset: 0x00211FEA
			internal NonRootTypeDecl(string typeName, string baseTypeName, ExprHostBuilder.TypeDecl parent, bool setCode)
				: base(typeName, baseTypeName, parent, setCode)
			{
				if (setCode)
				{
					base.ConstructorCreate();
				}
			}

			// Token: 0x0600812D RID: 33069 RVA: 0x00213E04 File Offset: 0x00212004
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

			// Token: 0x0600812E RID: 33070 RVA: 0x00213E68 File Offset: 0x00212068
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

			// Token: 0x0400449F RID: 17567
			internal CodeExpressionCollection Parameters;

			// Token: 0x040044A0 RID: 17568
			internal CodeExpressionCollection Filters;

			// Token: 0x040044A1 RID: 17569
			internal CodeExpressionCollection Actions;

			// Token: 0x040044A2 RID: 17570
			internal CodeExpressionCollection Fields;

			// Token: 0x040044A3 RID: 17571
			internal CodeExpressionCollection ValueAxes;

			// Token: 0x040044A4 RID: 17572
			internal CodeExpressionCollection CategoryAxes;

			// Token: 0x040044A5 RID: 17573
			internal CodeExpressionCollection ChartTitles;

			// Token: 0x040044A6 RID: 17574
			internal CodeExpressionCollection ChartLegends;

			// Token: 0x040044A7 RID: 17575
			internal CodeExpressionCollection ChartAreas;

			// Token: 0x040044A8 RID: 17576
			internal CodeExpressionCollection TablixMembers;

			// Token: 0x040044A9 RID: 17577
			internal CodeExpressionCollection DataShapeMembers;

			// Token: 0x040044AA RID: 17578
			internal CodeExpressionCollection ChartMembers;

			// Token: 0x040044AB RID: 17579
			internal CodeExpressionCollection GaugeMembers;

			// Token: 0x040044AC RID: 17580
			internal CodeExpressionCollection DataGroups;

			// Token: 0x040044AD RID: 17581
			internal CodeExpressionCollection TablixCells;

			// Token: 0x040044AE RID: 17582
			internal CodeExpressionCollection DataShapeIntersections;

			// Token: 0x040044AF RID: 17583
			internal CodeExpressionCollection DataPoints;

			// Token: 0x040044B0 RID: 17584
			internal CodeExpressionCollection DataCells;

			// Token: 0x040044B1 RID: 17585
			internal CodeExpressionCollection ChartLegendCustomItemCells;

			// Token: 0x040044B2 RID: 17586
			internal CodeExpressionCollection ChartCustomPaletteColors;

			// Token: 0x040044B3 RID: 17587
			internal CodeExpressionCollection ChartStripLines;

			// Token: 0x040044B4 RID: 17588
			internal CodeExpressionCollection ChartSeriesCollection;

			// Token: 0x040044B5 RID: 17589
			internal CodeExpressionCollection ChartDerivedSeriesCollection;

			// Token: 0x040044B6 RID: 17590
			internal CodeExpressionCollection ChartFormulaParameters;

			// Token: 0x040044B7 RID: 17591
			internal CodeExpressionCollection ChartLegendColumns;

			// Token: 0x040044B8 RID: 17592
			internal CodeExpressionCollection ChartLegendCustomItems;

			// Token: 0x040044B9 RID: 17593
			internal CodeExpressionCollection Paragraphs;

			// Token: 0x040044BA RID: 17594
			internal CodeExpressionCollection TextRuns;

			// Token: 0x040044BB RID: 17595
			internal CodeExpressionCollection GaugeCells;

			// Token: 0x040044BC RID: 17596
			internal CodeExpressionCollection CustomLabels;

			// Token: 0x040044BD RID: 17597
			internal CodeExpressionCollection GaugeImages;

			// Token: 0x040044BE RID: 17598
			internal CodeExpressionCollection GaugeLabels;

			// Token: 0x040044BF RID: 17599
			internal CodeExpressionCollection LinearGauges;

			// Token: 0x040044C0 RID: 17600
			internal CodeExpressionCollection RadialGauges;

			// Token: 0x040044C1 RID: 17601
			internal CodeExpressionCollection RadialPointers;

			// Token: 0x040044C2 RID: 17602
			internal CodeExpressionCollection LinearPointers;

			// Token: 0x040044C3 RID: 17603
			internal CodeExpressionCollection LinearScales;

			// Token: 0x040044C4 RID: 17604
			internal CodeExpressionCollection RadialScales;

			// Token: 0x040044C5 RID: 17605
			internal CodeExpressionCollection ScaleRanges;

			// Token: 0x040044C6 RID: 17606
			internal CodeExpressionCollection NumericIndicators;

			// Token: 0x040044C7 RID: 17607
			internal CodeExpressionCollection StateIndicators;

			// Token: 0x040044C8 RID: 17608
			internal CodeExpressionCollection GaugeInputValues;

			// Token: 0x040044C9 RID: 17609
			internal CodeExpressionCollection NumericIndicatorRanges;

			// Token: 0x040044CA RID: 17610
			internal CodeExpressionCollection IndicatorStates;

			// Token: 0x040044CB RID: 17611
			internal CodeExpressionCollection MapMembers;

			// Token: 0x040044CC RID: 17612
			internal CodeExpressionCollection MapBindingFieldPairs;

			// Token: 0x040044CD RID: 17613
			internal CodeExpressionCollection MapLegends;

			// Token: 0x040044CE RID: 17614
			internal CodeExpressionCollection MapTitles;

			// Token: 0x040044CF RID: 17615
			internal CodeExpressionCollection MapMarkers;

			// Token: 0x040044D0 RID: 17616
			internal CodeExpressionCollection MapBuckets;

			// Token: 0x040044D1 RID: 17617
			internal CodeExpressionCollection MapCustomColors;

			// Token: 0x040044D2 RID: 17618
			internal CodeExpressionCollection MapPoints;

			// Token: 0x040044D3 RID: 17619
			internal CodeExpressionCollection MapPolygons;

			// Token: 0x040044D4 RID: 17620
			internal CodeExpressionCollection MapLines;

			// Token: 0x040044D5 RID: 17621
			internal CodeExpressionCollection MapTileLayers;

			// Token: 0x040044D6 RID: 17622
			internal CodeExpressionCollection MapTiles;

			// Token: 0x040044D7 RID: 17623
			internal CodeExpressionCollection MapPointLayers;

			// Token: 0x040044D8 RID: 17624
			internal CodeExpressionCollection MapPolygonLayers;

			// Token: 0x040044D9 RID: 17625
			internal CodeExpressionCollection MapLineLayers;

			// Token: 0x040044DA RID: 17626
			internal CodeExpressionCollection MapFieldNames;

			// Token: 0x040044DB RID: 17627
			internal CodeExpressionCollection JoinConditions;

			// Token: 0x040044DC RID: 17628
			internal ExprHostBuilder.ReturnStatementList IndexedExpressions;
		}

		// Token: 0x020009A4 RID: 2468
		private sealed class CustomCodeProxyDecl : ExprHostBuilder.TypeDecl
		{
			// Token: 0x0600812F RID: 33071 RVA: 0x00213EBF File Offset: 0x002120BF
			internal CustomCodeProxyDecl(ExprHostBuilder.TypeDecl parent)
				: base("CustomCodeProxy", "CustomCodeProxyBase", parent, false)
			{
				base.ConstructorCreate();
			}

			// Token: 0x06008130 RID: 33072 RVA: 0x00213ED9 File Offset: 0x002120D9
			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IReportObjectModelProxyForCustomCode), "reportObjectModel"));
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				return codeConstructor;
			}

			// Token: 0x06008131 RID: 33073 RVA: 0x00213F18 File Offset: 0x00212118
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

			// Token: 0x06008132 RID: 33074 RVA: 0x00213FF4 File Offset: 0x002121F4
			internal void AddCode(string code)
			{
				CodeTypeMember codeTypeMember = new CodeSnippetTypeMember(code);
				codeTypeMember.LinePragma = new CodeLinePragma("CustomCode", 0);
				this.Type.Members.Add(codeTypeMember);
			}
		}

		// Token: 0x020009A5 RID: 2469
		private sealed class ReturnStatementList
		{
			// Token: 0x06008133 RID: 33075 RVA: 0x0021402B File Offset: 0x0021222B
			internal int Add(CodeMethodReturnStatement retStatement)
			{
				return this.m_list.Add(retStatement);
			}

			// Token: 0x170029B4 RID: 10676
			internal CodeMethodReturnStatement this[int index]
			{
				get
				{
					return (CodeMethodReturnStatement)this.m_list[index];
				}
			}

			// Token: 0x170029B5 RID: 10677
			// (get) Token: 0x06008135 RID: 33077 RVA: 0x0021404C File Offset: 0x0021224C
			internal int Count
			{
				get
				{
					return this.m_list.Count;
				}
			}

			// Token: 0x040044DD RID: 17629
			private ArrayList m_list = new ArrayList();
		}
	}
}
