using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Design.RdlModel;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x02000381 RID: 897
	internal sealed class RdlUtility
	{
		// Token: 0x06001DB2 RID: 7602 RVA: 0x00079634 File Offset: 0x00077834
		public RdlUtility(UnitType unitType, Namespace initialItemNamespace)
		{
			this.m_unitType = unitType;
			this.m_itemNames = new Namespace(initialItemNamespace);
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000796A2 File Offset: 0x000778A2
		public UnitType UnitType
		{
			get
			{
				return this.m_unitType;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x000796AA File Offset: 0x000778AA
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x000796B2 File Offset: 0x000778B2
		public bool ForceServerTotals
		{
			get
			{
				return this.m_forceServerTotals;
			}
			set
			{
				this.m_forceServerTotals = value;
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000796BC File Offset: 0x000778BC
		public string ToUnitString(float value)
		{
			return new Unit((double)value, this.m_unitType).ToString();
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000796E4 File Offset: 0x000778E4
		public Unit ToUnit(float value)
		{
			return new Unit((double)value, this.m_unitType);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000796F4 File Offset: 0x000778F4
		public float ToValue(Unit rdlUnit)
		{
			return (float)rdlUnit.ChangeType(this.m_unitType).Value;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00079717 File Offset: 0x00077917
		public PointF ToPointF(Unit x, Unit y)
		{
			return new PointF(this.ToValue(x), this.ToValue(y));
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0007972C File Offset: 0x0007792C
		public SizeF ToSizeF(Unit w, Unit h)
		{
			return new SizeF(this.ToValue(w), this.ToValue(h));
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00079741 File Offset: 0x00077941
		public Microsoft.ReportingServices.Design.RdlModel.Expression FieldRdlExpressionObject(Microsoft.ReportingServices.Modeling.Expression fieldExpr)
		{
			return new Microsoft.ReportingServices.Design.RdlModel.Expression(this.FieldRdlExpression(fieldExpr));
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0007974F File Offset: 0x0007794F
		public string FieldRdlExpression(Microsoft.ReportingServices.Modeling.Expression fieldExpr)
		{
			return "=" + this.FieldRdlSubExpression(fieldExpr);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00079762 File Offset: 0x00077962
		public string FieldRdlSubExpression(Microsoft.ReportingServices.Modeling.Expression fieldExpr)
		{
			return "Fields!" + this.GetRdlFieldName(fieldExpr) + ".Value";
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0007977A File Offset: 0x0007797A
		public static bool IsRdlExpression(string value)
		{
			return value.StartsWith("=", StringComparison.Ordinal);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00079788 File Offset: 0x00077988
		public static List<string> GetRdlFieldReferences(string rdlExpr)
		{
			List<string> list = new List<string>();
			foreach (object obj in RdlUtility.m_fieldRefPattern.Matches(rdlExpr))
			{
				Match match = (Match)obj;
				list.Add(match.Groups[1].Value);
			}
			return list;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00079800 File Offset: 0x00077A00
		public RdlTotalExpression AddNewRdlTotalExpression(object key, Microsoft.ReportingServices.Modeling.Expression expr)
		{
			RdlTotalExpression rdlTotalExpression = new RdlTotalExpression(expr, this.m_forceServerTotals);
			this.m_totalExpressions.Add(key, rdlTotalExpression);
			return rdlTotalExpression;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00079828 File Offset: 0x00077A28
		public RdlTotalExpression GetRdlTotalExpression(object key)
		{
			return this.m_totalExpressions[key];
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00079836 File Offset: 0x00077A36
		public string BuildRdlValueExpression(object key, bool usingTotals, bool forTotalScope, object rdlScopeKey)
		{
			return this.GetRdlTotalExpression(key).BuildRdlExpression(this, usingTotals, forTotalScope, rdlScopeKey);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00079849 File Offset: 0x00077A49
		public string GetRdlFieldName(Microsoft.ReportingServices.Modeling.Expression fieldExpr)
		{
			return this.m_fieldNamesByExpr[fieldExpr];
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00079858 File Offset: 0x00077A58
		public Microsoft.ReportingServices.Modeling.Expression GetFieldExpression(string rdlFieldName)
		{
			foreach (KeyValuePair<Microsoft.ReportingServices.Modeling.Expression, string> keyValuePair in this.m_fieldNamesByExpr)
			{
				if (keyValuePair.Value == rdlFieldName)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000798C0 File Offset: 0x00077AC0
		public string GetRdlScopeName(object key)
		{
			return this.m_scopeNamesByKey[key];
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000798D0 File Offset: 0x00077AD0
		public string AddNewRdlFieldName(object scopeKey, Microsoft.ReportingServices.Modeling.Expression fieldExpr)
		{
			string text = this.GetFieldNamespace(scopeKey).Add(fieldExpr.Name);
			this.m_fieldNamesByExpr.Add(fieldExpr, text);
			return text;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000798FE File Offset: 0x00077AFE
		public void SetRdlFieldName(Microsoft.ReportingServices.Modeling.Expression fieldExpr, object scopeKey, string rdlFieldName)
		{
			if (this.GetFieldNamespace(scopeKey).Add(rdlFieldName) != rdlFieldName)
			{
				throw new ArgumentException(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("The RDL field name {0} is already defined", new object[] { rdlFieldName }));
			}
			this.m_fieldNamesByExpr.Add(fieldExpr, rdlFieldName);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0007993C File Offset: 0x00077B3C
		private Namespace GetFieldNamespace(object scopeKey)
		{
			Namespace @namespace;
			if (!this.m_fieldNamespaceByScopeKey.TryGetValue(scopeKey, out @namespace))
			{
				@namespace = new Namespace("field", true);
				this.m_fieldNamespaceByScopeKey.Add(scopeKey, @namespace);
			}
			return @namespace;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00079974 File Offset: 0x00077B74
		public string AddNewRdlScopeName(object key, string candidateName, object dataRegionKey)
		{
			string text = ((dataRegionKey == null) ? string.Empty : (this.GetRdlScopeName(dataRegionKey) + "_")) + candidateName;
			string text2 = this.m_scopeNames.Add(text);
			this.m_scopeNamesByKey.Add(key, text2);
			return text2;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000799C0 File Offset: 0x00077BC0
		public string AddNewRdlDataRegionName(object key, string candidateName)
		{
			string text = candidateName;
			string text2;
			string text3;
			for (;;)
			{
				text2 = this.m_itemNames.Add(text);
				text3 = this.m_scopeNames.Add(text2);
				if (text3 == text2)
				{
					break;
				}
				this.m_itemNames.Remove(text2);
				this.m_scopeNames.Remove(text3);
				text = text3;
			}
			this.m_scopeNamesByKey.Add(key, text3);
			return text2;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00079A1C File Offset: 0x00077C1C
		public string AddNewRdlItemName(string candidateName)
		{
			return this.m_itemNames.Add(candidateName);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00079A2A File Offset: 0x00077C2A
		public string CreateRdlFieldHiddenExpression(object key, bool usingTotals)
		{
			return this.CreateRdlFieldHiddenExpression(this.GetRdlTotalExpression(key), usingTotals);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00079A3C File Offset: 0x00077C3C
		public string CreateRdlFieldHiddenExpression(RdlTotalExpression rdlTotalExpression, bool usingTotals)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Microsoft.ReportingServices.Modeling.Expression expression in rdlTotalExpression.GetQueryExpressions(usingTotals))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" Or ");
				}
				stringBuilder.AppendFormat("Fields!{0}.IsMissing", this.GetRdlFieldName(expression));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00079AB8 File Offset: 0x00077CB8
		public static string GetRdlStringLiteral(string s)
		{
			StringBuilder stringBuilder = new StringBuilder(s);
			stringBuilder.Replace("\"", "\"\"");
			stringBuilder.Replace(Environment.NewLine, "\" & Environment.NewLine & \"");
			stringBuilder.Replace(":", "\" & Chr(58) & \"");
			stringBuilder.Insert(0, '"');
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00079B18 File Offset: 0x00077D18
		public static string GetStringFromRdlStringLiteral(string rdlStringLiteral)
		{
			StringBuilder stringBuilder = new StringBuilder(rdlStringLiteral.Length);
			if (rdlStringLiteral.Length < 1)
			{
				return null;
			}
			if (rdlStringLiteral[0] != '"')
			{
				return null;
			}
			for (int i = 1; i < rdlStringLiteral.Length; i++)
			{
				if (rdlStringLiteral[i] == '"')
				{
					if (i == rdlStringLiteral.Length - 1)
					{
						return stringBuilder.ToString();
					}
					if (rdlStringLiteral[i + 1] == '"')
					{
						stringBuilder.Append('"');
						i++;
					}
					else if (RdlUtility.SubstringEquals(rdlStringLiteral, i, "\" & Environment.NewLine & \""))
					{
						stringBuilder.Append("\n");
						i += "\" & Environment.NewLine & \"".Length - 1;
					}
					else
					{
						if (!RdlUtility.SubstringEquals(rdlStringLiteral, i, "\" & Chr(58) & \""))
						{
							return null;
						}
						stringBuilder.Append(':');
						i += "\" & Chr(58) & \"".Length - 1;
					}
				}
				else
				{
					stringBuilder.Append(rdlStringLiteral[i]);
				}
			}
			return null;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00079C00 File Offset: 0x00077E00
		private static bool SubstringEquals(string s, int index, string matchString)
		{
			return index + matchString.Length <= s.Length && s.Substring(index, matchString.Length) == matchString;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00079C27 File Offset: 0x00077E27
		public TextboxItem CreateRdlTextbox(string value, Style style)
		{
			return new TextboxItem
			{
				Value = value,
				Style = style,
				CanGrow = true
			};
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00079C44 File Offset: 0x00077E44
		public string AddEmbeddedImage(global::System.Drawing.Image image)
		{
			EmbeddedImage embeddedImage = image.Tag as EmbeddedImage;
			bool flag = false;
			foreach (global::System.Drawing.Image image2 in this.EmbeddedImages)
			{
				EmbeddedImage embeddedImage2 = image2.Tag as EmbeddedImage;
				if (embeddedImage.ImageData.Equals(embeddedImage2.ImageData))
				{
					return embeddedImage2.Name;
				}
				if (embeddedImage2.Name == embeddedImage.Name)
				{
					flag = true;
				}
			}
			this.EmbeddedImages.Add(image);
			if (flag)
			{
				embeddedImage.Name += this.EmbeddedImages.Count;
			}
			return embeddedImage.Name;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00079D10 File Offset: 0x00077F10
		public global::System.Drawing.Image FindEmbeddedImage(string name)
		{
			foreach (global::System.Drawing.Image image in this.EmbeddedImages)
			{
				if ((image.Tag as EmbeddedImage).Name == name)
				{
					return image;
				}
			}
			return null;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00079D78 File Offset: 0x00077F78
		public static ReportParameter AddRdlParameters(Report report, DataSet dataSet, Microsoft.ReportingServices.Modeling.Parameter queryParameter)
		{
			ReportParameter reportParameter = new ReportParameter();
			reportParameter.Name = Microsoft.ReportingServices.Common.StringUtil.GetClsCompliantIdentifier(queryParameter.Name, "param");
			reportParameter.Prompt = queryParameter.Name;
			switch (queryParameter.DataType)
			{
			case DataType.String:
				reportParameter.DataType = ReportParameters.DataType.String;
				goto IL_00C2;
			case DataType.Integer:
				reportParameter.DataType = ReportParameters.DataType.Integer;
				goto IL_00C2;
			case DataType.Decimal:
				reportParameter.DataType = ReportParameters.DataType.Float;
				goto IL_00C2;
			case DataType.Float:
				reportParameter.DataType = ReportParameters.DataType.Float;
				goto IL_00C2;
			case DataType.Boolean:
				reportParameter.DataType = ReportParameters.DataType.Boolean;
				goto IL_00C2;
			case DataType.DateTime:
				reportParameter.DataType = ReportParameters.DataType.DateTime;
				goto IL_00C2;
			case DataType.EntityKey:
				reportParameter.DataType = ReportParameters.DataType.String;
				goto IL_00C2;
			case DataType.Time:
				reportParameter.DataType = ReportParameters.DataType.String;
				goto IL_00C2;
			}
			throw new ArgumentException("Unsupported parameter data type: " + queryParameter.DataType);
			IL_00C2:
			reportParameter.MultiValue = queryParameter.Cardinality == Cardinality.Many;
			reportParameter.Nullable = !reportParameter.MultiValue && queryParameter.Nullable;
			if (queryParameter.DefaultValue != null)
			{
				reportParameter.DefaultValue = new ReportParameters.DefaultValue();
				reportParameter.DefaultValue.Values = new ArrayList(RdlUtility.GetDefaultParameterValues(queryParameter));
			}
			RdlUtility.AddReportParameter(report, reportParameter);
			RdlUtility.AddQueryParameter(dataSet, new QueryParameter
			{
				Name = queryParameter.Name,
				Value = new Microsoft.ReportingServices.Design.RdlModel.Expression(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("=Parameters!{0}.Value", new object[] { reportParameter.Name }))
			});
			return reportParameter;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00079EDC File Offset: 0x000780DC
		private static Microsoft.ReportingServices.Design.RdlModel.Expression[] GetDefaultParameterValues(Microsoft.ReportingServices.Modeling.Parameter queryParameter)
		{
			List<Microsoft.ReportingServices.Design.RdlModel.Expression> list = new List<Microsoft.ReportingServices.Design.RdlModel.Expression>();
			if (queryParameter.DefaultValue.GetResultType().Cardinality == Cardinality.Many)
			{
				if (queryParameter.DefaultValue.NodeAsLiteral == null)
				{
					throw new ArgumentException("Default value set must be a literal set");
				}
				object[] array = queryParameter.DefaultValue.NodeAsLiteral.ToObjectArray();
				for (int i = 0; i < array.Length; i++)
				{
					string text = RdlTotalExpression.DecomposeConstant(new Microsoft.ReportingServices.Modeling.Expression(LiteralNode.FromObject(array[i])));
					list.Add(new Microsoft.ReportingServices.Design.RdlModel.Expression("=" + text));
				}
			}
			else
			{
				string text2 = RdlTotalExpression.DecomposeConstant(queryParameter.DefaultValue);
				if (text2 != null)
				{
					list.Add(new Microsoft.ReportingServices.Design.RdlModel.Expression("=" + text2));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00079F98 File Offset: 0x00078198
		public static void AddRdlDrillthroughParameters(Report report, DataSet dataSet)
		{
			ReportParameter reportParameter = new ReportParameter();
			reportParameter.Name = "DrillthroughSourceQuery";
			reportParameter.Prompt = reportParameter.Name;
			reportParameter.DataType = ReportParameters.DataType.String;
			reportParameter.Hidden = true;
			RdlUtility.AddReportParameter(report, reportParameter);
			RdlUtility.AddQueryParameter(dataSet, new QueryParameter
			{
				Name = reportParameter.Name,
				Value = new Microsoft.ReportingServices.Design.RdlModel.Expression(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("=Parameters!{0}.Value", new object[] { reportParameter.Name }))
			});
			reportParameter = new ReportParameter();
			reportParameter.Name = "DrillthroughContext";
			reportParameter.Prompt = reportParameter.Name;
			reportParameter.DataType = ReportParameters.DataType.String;
			reportParameter.Hidden = true;
			RdlUtility.AddReportParameter(report, reportParameter);
			RdlUtility.AddQueryParameter(dataSet, new QueryParameter
			{
				Name = reportParameter.Name,
				Value = new Microsoft.ReportingServices.Design.RdlModel.Expression(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("=Parameters!{0}.Value", new object[] { reportParameter.Name }))
			});
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0007A083 File Offset: 0x00078283
		private static void AddReportParameter(Report report, ReportParameter param)
		{
			if (report.ReportParameters == null)
			{
				report.ReportParameters = new ReportParameters();
			}
			report.ReportParameters.Add(param);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0007A0A5 File Offset: 0x000782A5
		private static void AddQueryParameter(DataSet dataSet, QueryParameter param)
		{
			if (dataSet.QueryParameters == null)
			{
				dataSet.QueryParameters = new QueryParameters();
			}
			dataSet.QueryParameters.Add(param);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0007A0C8 File Offset: 0x000782C8
		public Microsoft.ReportingServices.Design.RdlModel.Action CreateRdlDrillthroughAction(ModelEntity contextEntity, RdlTotalExpression totalExpr, bool useTotalExprs, IList<Microsoft.ReportingServices.Modeling.Expression> groupExprs, string modelPath, SemanticQuery query)
		{
			Microsoft.ReportingServices.Design.RdlModel.Action action = new Microsoft.ReportingServices.Design.RdlModel.Action();
			Microsoft.ReportingServices.Design.RdlModel.Parameter parameter = new Microsoft.ReportingServices.Design.RdlModel.Parameter();
			Microsoft.ReportingServices.Design.RdlModel.Parameter parameter2 = new Microsoft.ReportingServices.Design.RdlModel.Parameter();
			Microsoft.ReportingServices.Design.RdlModel.Parameter parameter3 = new Microsoft.ReportingServices.Design.RdlModel.Parameter();
			Microsoft.ReportingServices.Design.RdlModel.Parameter parameter4 = new Microsoft.ReportingServices.Design.RdlModel.Parameter();
			ExpressionPath drillPath = RdlUtility.GetDrillPath(totalExpr.OriginalExpression);
			ModelEntity modelEntity = (drillPath.IsEmpty ? contextEntity : drillPath.LastItem.TargetEntity.ModelEntity);
			action.ReportName = "=DataSources!dataSource1.DataSourceReference";
			if (action.Parameters == null)
			{
				action.Parameters = new Parameters();
			}
			parameter.Name = "rs:EntityID";
			parameter.Value = new Microsoft.ReportingServices.Design.RdlModel.Expression(modelEntity.ID.ToString());
			action.Parameters.Add(parameter);
			parameter2.Name = "rs:DrillType";
			string text = ((drillPath.GetCardinality() == Cardinality.Many) ? "List" : "Detail");
			if (useTotalExprs)
			{
				text = "List";
			}
			parameter2.Value = new Microsoft.ReportingServices.Design.RdlModel.Expression(text);
			action.Parameters.Add(parameter2);
			Microsoft.ReportingServices.Design.RdlModel.Parameter parameter5 = new Microsoft.ReportingServices.Design.RdlModel.Parameter("rs:Command", "Drillthrough");
			action.Parameters.Add(parameter5);
			parameter3.Name = "DrillthroughSourceQuery";
			parameter3.Value = new Microsoft.ReportingServices.Design.RdlModel.Expression("=DataSets!" + this.GetRdlScopeName(query) + ".RewrittenCommandText");
			action.Parameters.Add(parameter3);
			parameter4.Name = "DrillthroughContext";
			parameter4.Value = new Microsoft.ReportingServices.Design.RdlModel.Expression("=CreateDrillthroughContext()");
			action.Parameters.Add(parameter4);
			return action;
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0007A244 File Offset: 0x00078444
		private static ExpressionPath GetDrillPath(Microsoft.ReportingServices.Modeling.Expression expr)
		{
			ExpressionPath expressionPath = expr.GetMaxDrillablePath();
			while (!expressionPath.IsEmpty)
			{
				ExpressionPath segment = expressionPath.GetSegment(0, expressionPath.Length - 1);
				RolePathItem rolePathItem = expressionPath.LastItem as RolePathItem;
				if (rolePathItem == null || !ModelUtil.ShouldExpandInline(segment, rolePathItem.Role))
				{
					break;
				}
				expressionPath = segment;
			}
			return expressionPath;
		}

		// Token: 0x04000C77 RID: 3191
		public const string DrillthroughContextBuilderClass = "Microsoft.ReportingServices.SemanticQueryReportLibrary.DrillthroughContextBuilder";

		// Token: 0x04000C78 RID: 3192
		public const string EntityIdParameter = "rs:EntityID";

		// Token: 0x04000C79 RID: 3193
		public const string RsCommandParameter = "rs:Command";

		// Token: 0x04000C7A RID: 3194
		public const string DrillthroughCommandValue = "Drillthrough";

		// Token: 0x04000C7B RID: 3195
		public const string DrillTypeParameter = "rs:DrillType";

		// Token: 0x04000C7C RID: 3196
		public const string DrillTypeSingleInstance = "Detail";

		// Token: 0x04000C7D RID: 3197
		public const string DrillTypeMultipleInstance = "List";

		// Token: 0x04000C7E RID: 3198
		public const string DefaultFieldName = "field";

		// Token: 0x04000C7F RID: 3199
		public const string DefaultGroupName = "group";

		// Token: 0x04000C80 RID: 3200
		public const string DefaultTextboxName = "textbox";

		// Token: 0x04000C81 RID: 3201
		public const string DefaultImageName = "image";

		// Token: 0x04000C82 RID: 3202
		public const string DefaultTableName = "table";

		// Token: 0x04000C83 RID: 3203
		private static readonly Regex m_fieldRefPattern = new Regex("Fields\\!(" + Microsoft.ReportingServices.Common.StringUtil.ClsCompliantIdentifierPattern + ")\\.Value");

		// Token: 0x04000C84 RID: 3204
		private const string RdlStringLiteralNewLine = "\" & Environment.NewLine & \"";

		// Token: 0x04000C85 RID: 3205
		private const string RdlStringLiteralColon = "\" & Chr(58) & \"";

		// Token: 0x04000C86 RID: 3206
		private readonly UnitType m_unitType;

		// Token: 0x04000C87 RID: 3207
		private readonly Namespace m_itemNames;

		// Token: 0x04000C88 RID: 3208
		private readonly Dictionary<object, Namespace> m_fieldNamespaceByScopeKey = new Dictionary<object, Namespace>();

		// Token: 0x04000C89 RID: 3209
		private readonly Namespace m_scopeNames = new Namespace("group", true);

		// Token: 0x04000C8A RID: 3210
		private readonly Dictionary<Microsoft.ReportingServices.Modeling.Expression, string> m_fieldNamesByExpr = new Dictionary<Microsoft.ReportingServices.Modeling.Expression, string>();

		// Token: 0x04000C8B RID: 3211
		private readonly Dictionary<object, string> m_scopeNamesByKey = new Dictionary<object, string>();

		// Token: 0x04000C8C RID: 3212
		private readonly Dictionary<object, RdlTotalExpression> m_totalExpressions = new Dictionary<object, RdlTotalExpression>();

		// Token: 0x04000C8D RID: 3213
		private bool m_forceServerTotals;

		// Token: 0x04000C8E RID: 3214
		public readonly IList<global::System.Drawing.Image> EmbeddedImages = new List<global::System.Drawing.Image>();
	}
}
