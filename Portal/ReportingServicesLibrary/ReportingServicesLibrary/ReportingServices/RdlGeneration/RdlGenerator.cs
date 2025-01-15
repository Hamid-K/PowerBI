using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Design.RdlModel;
using Microsoft.ReportingServices.Design.Serialization;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x0200037C RID: 892
	public class RdlGenerator
	{
		// Token: 0x06001D66 RID: 7526 RVA: 0x000769FB File Offset: 0x00074BFB
		private RdlGenerator(string modelPath, ModelEntity entity, ModelDrillthroughType drillType)
		{
			this.m_modelPath = modelPath;
			this.m_entity = entity;
			this.m_drillType = drillType;
			this.m_totalExpressions = new List<RdlTotalExpression>();
			this.m_expressionNames = new Namespace("expr", false);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00076A34 File Offset: 0x00074C34
		public static byte[] CreateDrillthroughReport(string modelPath, ModelEntity entity, ModelDrillthroughType drillType)
		{
			return new RdlGenerator(modelPath, entity, drillType).Generate();
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00076A44 File Offset: 0x00074C44
		private byte[] Generate()
		{
			Type typeFromHandle = typeof(RdlGenerator);
			Stream stream = null;
			if (this.m_drillType == ModelDrillthroughType.List)
			{
				stream = typeFromHandle.Assembly.GetManifestResourceStream(typeFromHandle, "MultiInstance.rdl");
			}
			else if (this.m_drillType == ModelDrillthroughType.Detail)
			{
				stream = typeFromHandle.Assembly.GetManifestResourceStream(typeFromHandle, "SingleInstance.rdl");
			}
			Report report = this.LoadReport(stream);
			report.Language = this.m_entity.Model.Culture.Name;
			this.m_rdlUtil = new RdlUtility(UnitType.Inch, new Namespace("item", true));
			this.RegisterExistingContent(report);
			report.DataSources.Add(this.CreateDataSource());
			DataSet dataSet = this.CreateDataSet();
			report.DataSets.Add(dataSet);
			RdlUtility.AddRdlDrillthroughParameters(report, dataSet);
			ReportItem reportItem = null;
			List<TableItem> list = null;
			foreach (object obj in report.Body.ReportItems)
			{
				ReportItem reportItem2 = (ReportItem)obj;
				if (reportItem2 is TextboxItem)
				{
					this.FillTextbox(reportItem2 as TextboxItem);
				}
				else if (reportItem2 is TableItem)
				{
					if (this.m_drillType == ModelDrillthroughType.List)
					{
						this.FillTable(reportItem2 as TableItem);
					}
					else if (this.m_drillType == ModelDrillthroughType.Detail)
					{
						reportItem = reportItem2;
						list = this.MakeList(reportItem2 as TableItem);
					}
				}
			}
			if (list != null)
			{
				foreach (TableItem tableItem in list)
				{
					report.Body.ReportItems.Add(tableItem);
				}
			}
			if (reportItem != null)
			{
				report.Body.ReportItems.Remove(reportItem);
			}
			return this.SaveReport(report);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x00076C1C File Offset: 0x00074E1C
		private Report LoadReport(Stream rdlStream)
		{
			Report report;
			using (StreamReader streamReader = new StreamReader(rdlStream))
			{
				report = (Report)new RdlSerializer().DeserializeComponent(streamReader, typeof(Report));
			}
			return report;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00076C68 File Offset: 0x00074E68
		private byte[] SaveReport(Report r)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
				{
					RdlSerializer rdlSerializer = new RdlSerializer();
					xmlTextWriter.Formatting = Formatting.Indented;
					rdlSerializer.SerializeComponent(xmlTextWriter, typeof(Report), r);
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00076CE0 File Offset: 0x00074EE0
		private void RegisterExistingContent(Report report)
		{
			foreach (object obj in report.Body.ReportItems)
			{
				ReportItem reportItem = (ReportItem)obj;
				if (reportItem is DataRegionItem)
				{
					if (this.m_dataRegion != null)
					{
						throw new ArgumentException();
					}
					this.m_dataRegion = reportItem as DataRegionItem;
					if (this.m_rdlUtil.AddNewRdlDataRegionName(reportItem, reportItem.Name) != reportItem.Name)
					{
						throw new ArgumentException();
					}
					TableItem tableItem = reportItem as TableItem;
					if (tableItem == null)
					{
						continue;
					}
					if (tableItem.Details.Grouping != null)
					{
						this.m_rdlUtil.AddNewRdlScopeName(tableItem.Details, tableItem.Details.Grouping.Name, tableItem);
					}
					using (List<TableGroup>.Enumerator enumerator2 = tableItem.TableGroups.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							TableGroup tableGroup = enumerator2.Current;
							if (this.m_rdlUtil.AddNewRdlScopeName(tableGroup, tableGroup.Grouping.Name, tableItem) != reportItem.Name)
							{
								throw new ArgumentException();
							}
						}
						continue;
					}
				}
				if (this.m_rdlUtil.AddNewRdlItemName(reportItem.Name) != reportItem.Name)
				{
					throw new ArgumentException();
				}
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00076E6C File Offset: 0x0007506C
		private void BuildQuery()
		{
			this.m_query = new SemanticQuery(this.m_entity.Model);
			this.m_query.EnableDrillthrough = true;
			this.m_query.Hierarchies.Add(new Hierarchy());
			this.m_query.Hierarchies[0].BaseEntity = this.m_entity;
			this.m_query.MeasureGroups.Add(new MeasureGroup());
			this.m_query.MeasureGroups[0].BaseEntity = this.m_entity;
			this.AddFieldsToQuery();
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00076F04 File Offset: 0x00075104
		private void AddFieldsToQuery()
		{
			Microsoft.ReportingServices.Modeling.Expression expression = new Microsoft.ReportingServices.Modeling.Expression(new EntityRefNode(this.m_entity));
			expression.Name = this.m_expressionNames.Add(this.m_entity.Name);
			expression.Name = this.m_rdlUtil.AddNewRdlFieldName(this.m_query, expression);
			this.m_rdlUtil.AddNewRdlScopeName(expression, expression.Name, this.m_dataRegion);
			Microsoft.ReportingServices.Modeling.Grouping grouping = new Microsoft.ReportingServices.Modeling.Grouping(expression);
			grouping.Name = expression.Name;
			this.m_query.Hierarchies[0].Groupings.Add(grouping);
			Bag<Microsoft.ReportingServices.Modeling.Expression> bag = new Bag<Microsoft.ReportingServices.Modeling.Expression>(new SameExpressionComparer());
			this.AddDefaultDetailsToQuery(grouping, bag);
			foreach (ModelEntity modelEntity in ModelUtil.GetDirectAncestors(this.m_entity, true))
			{
				this.AddEntityFieldsToQuery(ExpressionPath.Empty, modelEntity, grouping, bag);
			}
			if (this.m_query.MeasureGroups[0].Measures.Count == 0)
			{
				this.m_query.MeasureGroups.Clear();
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00077034 File Offset: 0x00075234
		private void AddDefaultDetailsToQuery(Microsoft.ReportingServices.Modeling.Grouping grouping, Bag<Microsoft.ReportingServices.Modeling.Expression> detailExprs)
		{
			AttributeReferenceCollection attributeReferenceCollection = this.m_entity.DefaultDetailAttributes;
			if (attributeReferenceCollection.IsEmpty)
			{
				attributeReferenceCollection = this.m_entity.IdentifyingAttributes;
			}
			foreach (AttributeReference attributeReference in attributeReferenceCollection)
			{
				Microsoft.ReportingServices.Modeling.Expression expression = this.CreateQueryExpression(null, null, attributeReference);
				this.AddQueryExpression(expression, grouping);
				detailExprs.Add(expression);
			}
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000770B4 File Offset: 0x000752B4
		private void AddEntityFieldsToQuery(ExpressionPath pathToEntity, ModelEntity entity, Microsoft.ReportingServices.Modeling.Grouping grouping, Bag<Microsoft.ReportingServices.Modeling.Expression> defaultDetailExprs)
		{
			Bag<ModelItem> bag = new Bag<ModelItem>();
			bool flag = this.m_drillType == ModelDrillthroughType.Detail;
			foreach (ModelField modelField in entity.GetAllFields())
			{
				if (!modelField.Hidden && !ModelUtil.IsHiddenBasedOnContext(pathToEntity, modelField) && !modelField.IsVariation)
				{
					ModelFieldFolder modelFieldFolder = modelField.ParentItem as ModelFieldFolder;
					bool flag2 = false;
					while (modelFieldFolder != null)
					{
						if (bag.Contains(modelFieldFolder))
						{
							flag2 = true;
							break;
						}
						if (modelFieldFolder.Hidden || ModelUtil.IsHiddenBasedOnContext(pathToEntity, modelFieldFolder))
						{
							bag.Add(modelFieldFolder);
							flag2 = true;
							break;
						}
						modelFieldFolder = modelFieldFolder.ParentItem as ModelFieldFolder;
					}
					if (!flag2)
					{
						if (flag && modelField is ModelAttribute)
						{
							Microsoft.ReportingServices.Modeling.Expression expression = this.CreateQueryExpression(pathToEntity, (ModelAttribute)modelField);
							if (!defaultDetailExprs.Contains(expression))
							{
								this.AddQueryExpression(expression, grouping);
							}
						}
						else if (modelField is ModelRole)
						{
							ModelRole modelRole = modelField as ModelRole;
							if (ModelUtil.ShouldExpandInline(pathToEntity, modelRole))
							{
								ExpressionPath expressionPath = new ExpressionPath(pathToEntity, new PathItem[]
								{
									new RolePathItem(modelRole)
								});
								this.AddEntityFieldsToQuery(expressionPath, modelRole.RelatedEntity, grouping, defaultDetailExprs);
							}
							else if (flag && ModelUtil.IsLookupRole(modelRole))
							{
								this.AddLookupFieldsToQuery(pathToEntity, modelRole, grouping, defaultDetailExprs);
							}
							else if (modelRole.Cardinality == Cardinality.Many)
							{
								this.AddDefaultAggregatesToQuery(pathToEntity, modelRole, defaultDetailExprs);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00077240 File Offset: 0x00075440
		private void AddLookupFieldsToQuery(ExpressionPath pathToEntity, ModelRole role, Microsoft.ReportingServices.Modeling.Grouping grouping, Bag<Microsoft.ReportingServices.Modeling.Expression> defaultDetailExprs)
		{
			List<ExpressionPath> promotedLookupPaths = ModelUtil.GetPromotedLookupPaths(role);
			promotedLookupPaths.Insert(0, new ExpressionPath(new PathItem[]
			{
				new RolePathItem(role)
			}));
			foreach (ExpressionPath expressionPath in promotedLookupPaths)
			{
				ExpressionPath expressionPath2 = new ExpressionPath(pathToEntity, expressionPath);
				ModelRole modelRole = ModelUtil.PopLastRole(expressionPath);
				if (!ModelUtil.IsHiddenBasedOnContext(new ExpressionPath(pathToEntity, expressionPath), modelRole))
				{
					AttributeReference attributeReference = modelRole.RelatedEntity.IdentifyingAttributes[0];
					Microsoft.ReportingServices.Modeling.Expression expression = this.CreateQueryExpression(expressionPath2, null, attributeReference);
					if (!defaultDetailExprs.Contains(expression))
					{
						this.AddQueryExpression(expression, grouping);
					}
				}
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00077300 File Offset: 0x00075500
		private void AddDefaultAggregatesToQuery(ExpressionPath pathToRole, ModelRole role, Bag<Microsoft.ReportingServices.Modeling.Expression> defaultDetailExprs)
		{
			if (role.Cardinality != Cardinality.Many)
			{
				throw new ArgumentException();
			}
			foreach (AttributeReference attributeReference in role.RelatedEntity.DefaultAggregateAttributes)
			{
				Microsoft.ReportingServices.Modeling.Expression expression = this.CreateQueryExpression(pathToRole, role, attributeReference);
				if (!defaultDetailExprs.Contains(expression))
				{
					this.AddQueryExpression(expression, null);
				}
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0007737C File Offset: 0x0007557C
		private Microsoft.ReportingServices.Modeling.Expression CreateQueryExpression(ExpressionPath path, ModelRole extraRole, AttributeReference aref)
		{
			ExpressionPath expressionPath = new ExpressionPath();
			if (path != null)
			{
				expressionPath.AddRange(path);
			}
			if (extraRole != null)
			{
				expressionPath.Add(new RolePathItem(extraRole));
			}
			expressionPath.AddRange(aref.Path);
			return this.CreateQueryExpression(expressionPath, aref.Attribute);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000773C4 File Offset: 0x000755C4
		private Microsoft.ReportingServices.Modeling.Expression CreateQueryExpression(ExpressionPath path, ModelAttribute attrib)
		{
			Microsoft.ReportingServices.Modeling.Expression expression = new Microsoft.ReportingServices.Modeling.Expression(new AttributeRefNode(attrib));
			if (attrib.IsAggregate)
			{
				expression.Float(path);
			}
			else
			{
				expression.Path.InsertRange(0, path);
			}
			return expression;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000773FC File Offset: 0x000755FC
		private void AddQueryExpression(Microsoft.ReportingServices.Modeling.Expression expr, Microsoft.ReportingServices.Modeling.Grouping grouping)
		{
			expr.Name = this.m_expressionNames.Add(this.GetExpressionDisplayName(expr));
			RdlTotalExpression rdlTotalExpression = new RdlTotalExpression(expr);
			this.m_totalExpressions.Add(rdlTotalExpression);
			if (rdlTotalExpression.CanTotal)
			{
				if (rdlTotalExpression.IsDecomposed)
				{
					expr.CustomProperties.Add(RdlGenerator.OriginalExpressionPropertyName);
					this.m_query.CalculatedAttributes.Add(expr);
					using (IEnumerator<Microsoft.ReportingServices.Modeling.Expression> enumerator = rdlTotalExpression.GetQueryExpressions(true).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Microsoft.ReportingServices.Modeling.Expression expression = enumerator.Current;
							expression.Name = this.m_expressionNames.Add(expression.Name);
							expression.CustomProperties.Add(new Microsoft.ReportingServices.Modeling.CustomProperty(RdlGenerator.OriginalExpressionPropertyName, expr.Name));
							this.m_query.MeasureGroups[0].Measures.Add(expression);
							this.m_rdlUtil.AddNewRdlFieldName(this.m_query, expression);
						}
						return;
					}
				}
				this.m_query.MeasureGroups[0].Measures.Add(expr);
				this.m_rdlUtil.AddNewRdlFieldName(this.m_query, expr);
				return;
			}
			if (grouping == null)
			{
				throw new ArgumentException();
			}
			grouping.Details.Add(expr);
			this.m_rdlUtil.AddNewRdlFieldName(this.m_query, expr);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00077560 File Offset: 0x00075760
		private string GetExpressionDisplayName(Microsoft.ReportingServices.Modeling.Expression expr)
		{
			string text = "{0} {1}";
			ExpressionPath expressionPath;
			ModelAttribute modelAttribute = ModelUtil.GetModelAttribute(expr, out expressionPath);
			return ModelUtil.GetContextualName(expressionPath, modelAttribute, text);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00077584 File Offset: 0x00075784
		private DataSource CreateDataSource()
		{
			return new DataSource
			{
				Name = "dataSource1",
				DataSourceReference = this.m_modelPath
			};
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000775A4 File Offset: 0x000757A4
		private DataSet CreateDataSet()
		{
			this.BuildQuery();
			DataSet dataSet = new DataSet();
			dataSet.Name = this.m_rdlUtil.AddNewRdlScopeName(this.m_query, "dataSet1", null);
			dataSet.DataSourceName = "dataSource1";
			dataSet.CommandText = new Microsoft.ReportingServices.Design.RdlModel.Expression(Microsoft.ReportingServices.Common.XmlFragmentUtil.ToXmlString(delegate(XmlWriter xw)
			{
				this.m_query.WriteTo(xw, ModelingSerializationOptions.NameComments);
			}));
			Microsoft.ReportingServices.Modeling.Grouping grouping = this.m_query.Hierarchies[0].Groupings[0];
			dataSet.Fields.Add(this.CreateField(grouping.Expression));
			foreach (RdlTotalExpression rdlTotalExpression in this.m_totalExpressions)
			{
				foreach (Microsoft.ReportingServices.Modeling.Expression expression in rdlTotalExpression.GetQueryExpressions(rdlTotalExpression.CanTotal))
				{
					dataSet.Fields.Add(this.CreateField(expression));
				}
			}
			return dataSet;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000776C4 File Offset: 0x000758C4
		private Field CreateField(Microsoft.ReportingServices.Modeling.Expression expr)
		{
			return new Field
			{
				DataField = expr.Name,
				Name = this.m_rdlUtil.GetRdlFieldName(expr)
			};
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000776E9 File Offset: 0x000758E9
		private void FillTextbox(TextboxItem tb)
		{
			if (tb.Value == "Title")
			{
				tb.Value = ((this.m_drillType == ModelDrillthroughType.List) ? this.m_entity.CollectionName : this.m_entity.Name);
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00077724 File Offset: 0x00075924
		private static string SwitchTextDirection(string ta)
		{
			if (string.Compare(ta, "Left", StringComparison.Ordinal) == 0)
			{
				return "Right";
			}
			if (string.Compare(ta, "Right", StringComparison.Ordinal) == 0)
			{
				return "Left";
			}
			return ta;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00077750 File Offset: 0x00075950
		private void FillTable(TableItem t)
		{
			TableRow tableRow = t.Header.TableRows[0];
			TableRow tableRow2 = t.Details.TableRows[0];
			TableRow tableRow3 = t.Footer.TableRows[0];
			TableCell tableCell = tableRow.TableCells[0];
			TableCell tableCell2 = tableRow2.TableCells[0];
			TableCell tableCell3 = tableRow3.TableCells[0];
			t.TableColumns.Clear();
			tableRow.TableCells.Clear();
			tableRow2.TableCells.Clear();
			tableRow3.TableCells.Clear();
			Microsoft.ReportingServices.Modeling.Grouping grouping = this.m_query.Hierarchies[0].Groupings[0];
			Microsoft.ReportingServices.Design.RdlModel.Grouping grouping2 = new Microsoft.ReportingServices.Design.RdlModel.Grouping();
			grouping2.Name = this.m_rdlUtil.GetRdlScopeName(grouping.Expression);
			grouping2.GroupExpressions.Add(this.m_rdlUtil.FieldRdlExpressionObject(grouping.Expression));
			t.Details.Grouping = grouping2;
			using (Bitmap bitmap = new Bitmap(1, 1))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					foreach (RdlTotalExpression rdlTotalExpression in this.m_totalExpressions)
					{
						Microsoft.ReportingServices.Modeling.Expression originalExpression = rdlTotalExpression.OriginalExpression;
						ModelAttribute modelAttribute = ModelUtil.GetModelAttribute(originalExpression);
						string expressionDisplayName = this.GetExpressionDisplayName(originalExpression);
						string text = rdlTotalExpression.BuildRdlExpression(this.m_rdlUtil, rdlTotalExpression.CanTotal, false, null);
						TableColumn tableColumn = new TableColumn();
						string text2 = RdlTextUtility.BuildWidthString('x', modelAttribute.Width);
						tableColumn.Width = RdlGenerator.CalculateColumnWidth(graphics, text2, tableCell2.ReportItem.Style, 1);
						float num = RdlTextUtility.CalculateStringSize(expressionDisplayName, RdlGenerator.GetFont(tableCell.ReportItem.Style), 3, (float)tableColumn.Width.FPixels).Width;
						float num2 = 34.4f;
						num += num2;
						Unit unit = RdlGenerator.AddColumnPadding(graphics, (double)num, tableCell.ReportItem.Style);
						if (unit.Value > tableColumn.Width.Value)
						{
							tableColumn.Width = unit;
						}
						tableColumn.Visibility = new Visibility();
						tableColumn.Visibility.Hidden = new TrueFalseString("=" + this.m_rdlUtil.CreateRdlFieldHiddenExpression(rdlTotalExpression, rdlTotalExpression.CanTotal));
						t.TableColumns.Add(tableColumn);
						TextboxItem textboxItem = this.InitializeTextBoxFrom(tableCell.ReportItem as TextboxItem, modelAttribute);
						textboxItem.Value = expressionDisplayName;
						textboxItem.DataElementOutput = DataElementOutputs.Output;
						textboxItem.UserSort = new UserSort();
						textboxItem.UserSort.SortExpression = new Microsoft.ReportingServices.Design.RdlModel.Expression("=" + text);
						textboxItem.UserSort.SortExpressionScope = this.m_rdlUtil.GetRdlScopeName(grouping.Expression);
						TableCell tableCell4 = new TableCell();
						tableCell4.ReportItem = textboxItem;
						tableRow.TableCells.Add(tableCell4);
						textboxItem = this.InitializeTextBoxFrom(tableCell2.ReportItem as TextboxItem, modelAttribute);
						textboxItem.Value = "=" + text;
						textboxItem.DataElementOutput = DataElementOutputs.Output;
						textboxItem.Style.Format = modelAttribute.Format;
						if (modelAttribute.EnableDrillthrough)
						{
							textboxItem.Action = this.m_rdlUtil.CreateRdlDrillthroughAction(this.m_entity, rdlTotalExpression, rdlTotalExpression.CanTotal, new Microsoft.ReportingServices.Modeling.Expression[] { grouping.Expression }, this.m_modelPath, this.m_query);
						}
						tableCell4 = new TableCell();
						tableCell4.ReportItem = textboxItem;
						tableRow2.TableCells.Add(tableCell4);
						textboxItem = this.InitializeTextBoxFrom(tableCell3.ReportItem as TextboxItem, modelAttribute);
						textboxItem.DataElementOutput = DataElementOutputs.Output;
						if (rdlTotalExpression.CanTotal)
						{
							textboxItem.Value = "=" + rdlTotalExpression.BuildRdlExpression(this.m_rdlUtil, true, true, null);
							textboxItem.Style.Format = modelAttribute.Format;
							if (modelAttribute.EnableDrillthrough)
							{
								textboxItem.Action = this.m_rdlUtil.CreateRdlDrillthroughAction(this.m_entity, rdlTotalExpression, true, new Microsoft.ReportingServices.Modeling.Expression[] { grouping.Expression }, this.m_modelPath, this.m_query);
							}
						}
						else
						{
							textboxItem.Value = string.Empty;
						}
						tableCell4 = new TableCell();
						tableCell4.ReportItem = textboxItem;
						tableRow3.TableCells.Add(tableCell4);
					}
				}
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00077C1C File Offset: 0x00075E1C
		private static Font GetFont(Style rdlStyle)
		{
			string text = "Arial";
			StyleUnit fontSize = rdlStyle.FontSize;
			FontStyle fontStyle = FontStyle.Regular;
			if (!string.IsNullOrEmpty(rdlStyle.FontFamily) && !Microsoft.ReportingServices.Design.RdlModel.Expression.IsExpressionString(rdlStyle.FontFamily))
			{
				text = rdlStyle.FontFamily;
			}
			float num = (fontSize.IsUnit ? ((float)Unit.ConvertToPixels(fontSize.BaseUnit.Value, fontSize.BaseUnit.Type)) : 0f);
			if (num < 1E-45f)
			{
				num = (float)Unit.ConvertToPixels(10.0, UnitType.Point);
			}
			if (rdlStyle.IsFontItalic)
			{
				fontStyle |= FontStyle.Italic;
			}
			if (rdlStyle.IsFontBold)
			{
				fontStyle |= FontStyle.Bold;
			}
			if (rdlStyle.IsFontUnderline)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (rdlStyle.IsFontStrikeout)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			return new Font(text, num, fontStyle, GraphicsUnit.Pixel);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00077CE4 File Offset: 0x00075EE4
		private static Unit CalculateColumnWidth(Graphics graphics, string str, Style style, int maxLines)
		{
			return RdlGenerator.AddColumnPadding(graphics, (double)RdlTextUtility.CalculateStringSize(str, RdlGenerator.GetFont(style), maxLines, 0f, graphics).Width, style);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00077D14 File Offset: 0x00075F14
		private static Unit AddColumnPadding(Graphics graphics, double widthInPixels, Style style)
		{
			return new Unit(widthInPixels / (double)graphics.DpiX + style.PaddingLeft.BaseUnit.ChangeType(UnitType.Inch).Value + style.PaddingRight.BaseUnit.ChangeType(UnitType.Inch).Value, UnitType.Inch);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00077D6A File Offset: 0x00075F6A
		private TextboxItem InitializeTextBoxFrom(TextboxItem other, ModelAttribute a)
		{
			TextboxItem textboxItem = new TextboxItem();
			textboxItem.Name = this.m_rdlUtil.AddNewRdlItemName("textbox");
			RdlGenerator.CopyStyleFrom(textboxItem.Style, other.Style, a);
			textboxItem.CanGrow = other.CanGrow;
			return textboxItem;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00077DA8 File Offset: 0x00075FA8
		private static void CopyStyleFrom(Style to, Style from, ModelAttribute a)
		{
			to.BackgroundColor = from.BackgroundColor;
			to.Color = from.Color;
			to.BorderColor = from.BorderColor;
			to.BorderStyle = from.BorderStyle;
			to.BorderWidth = from.BorderWidth;
			to.FontFamily = from.FontFamily;
			to.FontSize = from.FontSize;
			to.FontStyle = from.FontStyle;
			to.FontWeight = from.FontWeight;
			to.PaddingBottom = from.PaddingBottom;
			to.PaddingLeft = from.PaddingLeft;
			to.PaddingRight = from.PaddingRight;
			to.PaddingTop = from.PaddingTop;
			to.TextAlign = from.TextAlign;
			if (a != null)
			{
				CultureInfo cultureInfo;
				if (a.IsDataCultureSet)
				{
					cultureInfo = a.DataCulture;
				}
				else
				{
					cultureInfo = a.Model.Culture;
				}
				if (cultureInfo != null)
				{
					to.Language = cultureInfo.Name;
					to.NumeralLanguage = cultureInfo.Name;
					if (!cultureInfo.TextInfo.IsRightToLeft)
					{
						to.Direction = "LTR";
						return;
					}
					to.Direction = "RTL";
					to.TextAlign = RdlGenerator.SwitchTextDirection(from.TextAlign);
				}
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00077ED0 File Offset: 0x000760D0
		private List<TableItem> MakeList(TableItem template)
		{
			List<TableItem> list = new List<TableItem>();
			TableRow tableRow = template.Details.TableRows[0];
			TableCell tableCell = tableRow.TableCells[0];
			TableCell tableCell2 = tableRow.TableCells[1];
			Unit width = template.TableColumns[0].Width;
			Unit width2 = template.TableColumns[1].Width;
			Unit unit = template.Top;
			int num = 0;
			foreach (RdlTotalExpression rdlTotalExpression in this.m_totalExpressions)
			{
				num++;
				TableItem tableItem = this.FillList(rdlTotalExpression, tableRow, width, width2, num);
				tableItem.Left = template.Left;
				tableItem.Top = unit;
				unit += tableRow.Height;
				list.Add(tableItem);
			}
			return list;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00077FC4 File Offset: 0x000761C4
		private TableItem FillList(RdlTotalExpression totalExpr, TableRow valueRow, Unit nameWidth, Unit valueWidth, int counter)
		{
			TableItem tableItem = new TableItem("table_a_" + counter, 2);
			tableItem.TableColumns[0].Width = nameWidth;
			tableItem.TableColumns[1].Width = valueWidth;
			TableCell tableCell = valueRow.TableCells[0];
			TableCell tableCell2 = valueRow.TableCells[1];
			Microsoft.ReportingServices.Modeling.Grouping grouping = this.m_query.Hierarchies[0].Groupings[0];
			Microsoft.ReportingServices.Design.RdlModel.Grouping grouping2 = new Microsoft.ReportingServices.Design.RdlModel.Grouping();
			grouping2.Name = tableItem.Name + this.m_rdlUtil.GetRdlScopeName(grouping.Expression);
			grouping2.GroupExpressions.Add(this.m_rdlUtil.FieldRdlExpressionObject(grouping.Expression));
			tableItem.Details.Grouping = grouping2;
			Microsoft.ReportingServices.Modeling.Expression originalExpression = totalExpr.OriginalExpression;
			ModelAttribute modelAttribute = ModelUtil.GetModelAttribute(originalExpression);
			string expressionDisplayName = this.GetExpressionDisplayName(originalExpression);
			TableRow tableRow = new TableRow(2, valueRow.Height);
			tableItem.Details.TableRows.Add(tableRow);
			tableRow.Visibility.Hidden = new TrueFalseString("=" + this.m_rdlUtil.CreateRdlFieldHiddenExpression(totalExpr, totalExpr.CanTotal));
			TextboxItem textboxItem = this.InitializeTextBoxFrom(tableCell.ReportItem as TextboxItem, modelAttribute);
			textboxItem.Value = expressionDisplayName;
			textboxItem.Name = tableItem.Name + "Name";
			tableRow.TableCells[0].ReportItem = textboxItem;
			Microsoft.ReportingServices.Design.RdlModel.Action action = null;
			if (modelAttribute.EnableDrillthrough)
			{
				action = this.m_rdlUtil.CreateRdlDrillthroughAction(this.m_entity, totalExpr, totalExpr.CanTotal, new Microsoft.ReportingServices.Modeling.Expression[] { grouping.Expression }, this.m_modelPath, this.m_query);
			}
			if (this.IsImage(modelAttribute))
			{
				Microsoft.ReportingServices.Design.RdlModel.Image image = new Microsoft.ReportingServices.Design.RdlModel.Image(this.m_rdlUtil.AddNewRdlItemName("image"));
				image.Source = ImageSource.Database;
				image.Value = "=" + totalExpr.BuildRdlExpression(this.m_rdlUtil, false, false, null);
				image.MIMEType.Set(modelAttribute.MimeType);
				image.Style = this.GetDrillthroughImageStyle();
				image.Sizing = ImageSizing.AutoSize;
				image.Action = action;
				tableRow.TableCells[1].ReportItem = image;
			}
			else
			{
				TextboxItem textboxItem2 = this.InitializeTextBoxFrom(tableCell2.ReportItem as TextboxItem, modelAttribute);
				textboxItem2.Name = tableItem.Name + "Value";
				textboxItem2.Value = "=" + totalExpr.BuildRdlExpression(this.m_rdlUtil, totalExpr.CanTotal, false, null);
				textboxItem2.Style.Format = modelAttribute.Format;
				textboxItem2.Action = action;
				tableRow.TableCells[1].ReportItem = textboxItem2;
			}
			return tableItem;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000782A4 File Offset: 0x000764A4
		private Style GetDrillthroughImageStyle()
		{
			Style style = new Style();
			style.PaddingTop.BaseUnit = new Unit(2.0, UnitType.Point);
			style.PaddingRight.BaseUnit = new Unit(2.0, UnitType.Point);
			style.PaddingBottom.BaseUnit = new Unit(2.0, UnitType.Point);
			style.PaddingLeft.BaseUnit = new Unit(2.0, UnitType.Point);
			style.BorderStyle = new BorderStyle("Solid");
			style.BorderWidth.Set(1.0, UnitType.Point);
			style.BorderColor.Set(Color.LightGray);
			return style;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00078354 File Offset: 0x00076554
		private bool IsImage(ModelAttribute a)
		{
			string[] array = new string[] { "image/bmp", "image/jpeg", "image/gif", "image/png", "image/x-png" };
			bool flag = false;
			foreach (string text in array)
			{
				if (string.Equals(a.MimeType, text, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x04000C59 RID: 3161
		public static readonly QName OriginalExpressionPropertyName = new QName("OriginalExpression");

		// Token: 0x04000C5A RID: 3162
		internal const string DataSourceName = "dataSource1";

		// Token: 0x04000C5B RID: 3163
		private const string DataSetName = "dataSet1";

		// Token: 0x04000C5C RID: 3164
		private const string TitleTextbox = "Title";

		// Token: 0x04000C5D RID: 3165
		private const string DefaultExpressionName = "expr";

		// Token: 0x04000C5E RID: 3166
		private const int MaxLinesInHeaderCell = 3;

		// Token: 0x04000C5F RID: 3167
		private string m_modelPath;

		// Token: 0x04000C60 RID: 3168
		private ModelEntity m_entity;

		// Token: 0x04000C61 RID: 3169
		private ModelDrillthroughType m_drillType;

		// Token: 0x04000C62 RID: 3170
		private SemanticQuery m_query;

		// Token: 0x04000C63 RID: 3171
		private DataRegionItem m_dataRegion;

		// Token: 0x04000C64 RID: 3172
		private RdlUtility m_rdlUtil;

		// Token: 0x04000C65 RID: 3173
		private List<RdlTotalExpression> m_totalExpressions;

		// Token: 0x04000C66 RID: 3174
		private Namespace m_expressionNames;
	}
}
