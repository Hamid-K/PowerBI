using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x0200001D RID: 29
	internal sealed class DataShapeDefinitionReconciler
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003A8C File Offset: 0x00001C8C
		private DataShapeDefinitionReconciler(IDataShapingDataSourceInfo dataSourceInfo)
		{
			this._dataSourceInfo = dataSourceInfo;
			this._lookupTable = new ResultTableLookup();
			this._expressionReconciler = new ExpressionReconciler(this._lookupTable);
			this._scopeTable = new ScopeLookupTable();
			this._restartIndexCount = 0;
			this._relationshipsToPostProcess = new List<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship>();
			this._encodingHints = new WritableResultEncodingHints();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003AEA File Offset: 0x00001CEA
		internal static Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShapeDefinition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShapeDefinition dsd, IDataShapingDataSourceInfo dataSourceInfo)
		{
			return new DataShapeDefinitionReconciler(dataSourceInfo).ReconcileInternal(dsd);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003AF8 File Offset: 0x00001CF8
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShapeDefinition ReconcileInternal(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShapeDefinition dsd)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSource dataSource = this.Reconcile(dsd.DataSource);
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSet> readOnlyCollection = dsd.DataSets.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataSet, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSet>(this.Reconcile));
			this.Reconcile(dsd.ResultEncodingHints);
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransform> readOnlyCollection2 = dsd.DataTransforms.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransform>(this.Reconcile));
			this._lookupTable.GenerateResultTableMetadata();
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape = this.Reconcile(dsd.DataShape);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShapeDefinition(dataSource, readOnlyCollection, readOnlyCollection2, dataShape, this._lookupTable.ResultTableInfos, this._lookupTable.ResultTableMetadata, this._encodingHints);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003B89 File Offset: 0x00001D89
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSource Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataSource dataSource)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSource(dataSource.Id, dataSource.DataSourceName, this.Reconcile(dataSource.Collation), this._dataSourceInfo);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003BAE File Offset: 0x00001DAE
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Collation Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Collation collation)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Collation(collation.Culture, collation.IgnoreCase, collation.IgnoreNonSpace, collation.IgnoreKanaType, collation.IgnoreWidth, collation.PreferOrdinalStringEquality, collation.UseOrdinalStringKeyGeneration);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSet Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataSet dataSet)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSet dataSet2 = new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataSet(dataSet.Id, dataSet.DataSourceId, dataSet.Query, dataSet.ResultTables.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ResultTable, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ResultTable>(this.Reconcile)), dataSet.QuerySourceMap);
			this._lookupTable.AddDataSet(dataSet2);
			return dataSet2;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003C2F File Offset: 0x00001E2F
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ResultTable Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ResultTable table)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ResultTable(table.Id, table.Fields.ReconcileWritable(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Field, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Field>(this.Reconcile)), table.IsReusable);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003C59 File Offset: 0x00001E59
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Field Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Field field)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Field(field.Id, field.DataField, field.TargetRole, false, this.Reconcile(field.SortInformation));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003C80 File Offset: 0x00001E80
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransform Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransform dataTransform)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransform dataTransform2 = new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransform(dataTransform.Id, dataTransform.Algorithm, this.Reconcile(dataTransform.Input), this.Reconcile(dataTransform.Output));
			this._lookupTable.AddDataTransform(dataTransform2);
			return dataTransform2;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003CC4 File Offset: 0x00001EC4
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformParameter Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformParameter dataTransformParam)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformParameter(dataTransformParam.Name, this.Reconcile(dataTransformParam.Value));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003CE0 File Offset: 0x00001EE0
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformInput Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformInput dataTransformInput)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ResultTable resultTable = this._lookupTable.GetResultTable(dataTransformInput.TableId);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformInput(resultTable, this._lookupTable.GetResultTableIndex(resultTable.Id), this.Reconcile(dataTransformInput.Schema), dataTransformInput.Parameters.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformParameter, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformParameter>(this.Reconcile)));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003D39 File Offset: 0x00001F39
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformTableSchema Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformTableSchema schema)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformTableSchema(schema.Columns.ReconcileWritable(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformColumn, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformColumn>(this.Reconcile)));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003D57 File Offset: 0x00001F57
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformColumn Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformColumn column)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformColumn(column.Name, column.Role);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003D6A File Offset: 0x00001F6A
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortInformation Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortInformation sortInformation)
		{
			if (sortInformation == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortInformation(sortInformation.SortIndex, this.Reconcile(sortInformation.SortDirection));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003D88 File Offset: 0x00001F88
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformOutput Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataTransformOutput dataTransformOutput)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformOutput(this.Reconcile(dataTransformOutput.Table));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003D9C File Offset: 0x00001F9C
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape dataShape)
		{
			this._parentDataShapesCount++;
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> readOnlyCollection = dataShape.Calculations.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation>(this.Reconcile));
			this._encodingHints.ExcludeCalculationsForDictionaryEncoding(readOnlyCollection);
			this._isInSecondary = true;
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember> readOnlyCollection2 = dataShape.SecondaryHierarchy.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember>(this.Reconcile));
			this._isInSecondary = false;
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember> readOnlyCollection3 = dataShape.PrimaryHierarchy.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember>(this.Reconcile));
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape> readOnlyCollection4 = dataShape.DataShapes.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding dataBinding = this.Reconcile(dataShape.DataBinding);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimits dataLimits = this.Reconcile(dataShape.DataLimits);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FieldValueExpressionNode fieldValueExpressionNode = this.Reconcile(dataShape.CorrelationExpression);
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message> readOnlyCollection5 = dataShape.Messages.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.Message, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message>(this.Reconcile));
			ReadOnlyCollection<IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>> readOnlyCollection6 = dataShape.RestartDefinitions.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>(this.Reconcile));
			ReadOnlyCollection<int> readOnlyCollection7 = dataShape.SegmentationTableIds.Reconcile(new Func<string, int>(this._lookupTable.GetResultTableIndex));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindows dataWindows = this.Reconcile(dataShape.DataWindows);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow dataWindow = this.ReconcileLegacyDataWindow(dataShape.DataWindow, readOnlyCollection6);
			if (this._restartIndicesWithStartPosition != null)
			{
				this._restartIndicesWithStartPosition.Sort();
			}
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape2 = new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape(dataShape.Id, readOnlyCollection, readOnlyCollection2, readOnlyCollection3, readOnlyCollection4, dataBinding, dataWindow, dataLimits, fieldValueExpressionNode, readOnlyCollection5, dataShape.HasReusableSecondary, readOnlyCollection6, readOnlyCollection7, dataWindows, dataShape.CorrelationMode, this._restartIndicesWithStartPosition);
			this._scopeTable.Add(dataShape2);
			CellScopeToIntersectionRangeGenerator.Generate(dataShape2);
			this.PostProcessRelationships();
			this.PostProcessDataLimits(dataShape2);
			this.PostProcessDataWindows(dataShape2);
			this._parentDataShapesCount--;
			return dataShape2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003F4C File Offset: 0x0000214C
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimits Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataLimits dataLimits)
		{
			if (dataLimits == null)
			{
				return null;
			}
			IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimit> list = dataLimits.Limits.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataLimit, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimit>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding dataBinding = this.Reconcile(dataLimits.DataBinding);
			TelemetryItems telemetryItems = this.Reconcile(dataLimits.TelemetryItems);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimits(list, dataBinding, telemetryItems);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003F98 File Offset: 0x00002198
		private void Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ResultEncodingHints resultEncodingHints)
		{
			if (resultEncodingHints == null)
			{
				return;
			}
			this._encodingHints.SetDisableDictionaryEncoding(resultEncodingHints.DisableDictionaryEncoding);
			if (resultEncodingHints.CalculationsWithSharedValues != null)
			{
				foreach (IList<string> list in resultEncodingHints.CalculationsWithSharedValues)
				{
					HashSet<string> hashSet = list.ToSet<string>();
					foreach (string text in list)
					{
						this._encodingHints.SetCalculationWithSharedValues(text, hashSet);
					}
				}
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004040 File Offset: 0x00002240
		private TelemetryItems Reconcile(IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TelemetryItem> telemetryItems)
		{
			if (telemetryItems.IsNullOrEmpty<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TelemetryItem>())
			{
				return null;
			}
			TelemetryItems telemetryItems2 = new TelemetryItems();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TelemetryItem telemetryItem in telemetryItems)
			{
				telemetryItems2.Add(telemetryItem.Name, this.Reconcile(telemetryItem.Expression));
			}
			return telemetryItems2;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000040AC File Offset: 0x000022AC
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation calculation)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation(calculation.Id, this.Reconcile(calculation.Value));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000040C5 File Offset: 0x000022C5
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode node)
		{
			return this._expressionReconciler.Visit(node);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000040D3 File Offset: 0x000022D3
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FieldValueExpressionNode Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.FieldValueExpressionNode node)
		{
			if (node == null)
			{
				return null;
			}
			return (Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.FieldValueExpressionNode)this._expressionReconciler.Visit(node);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000040EC File Offset: 0x000022EC
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember member)
		{
			this._dataMemberDepth++;
			int num = -1;
			if (!this._isInSecondary && this._parentDataShapesCount == 1)
			{
				int restartIndexCount = this._restartIndexCount;
				this._restartIndexCount = restartIndexCount + 1;
				num = restartIndexCount;
			}
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember> readOnlyCollection = member.DataMembers.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataMember, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember>(this.Reconcile));
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> readOnlyCollection2 = member.Calculations.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Group group = this.Reconcile(member.Group);
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection> readOnlyCollection3 = member.Intersections.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataIntersection, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding dataBinding = this.Reconcile(member.DataBinding);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.MatchCondition matchCondition = this.Reconcile(member.MatchCondition);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.StartPosition startPosition = this.Reconcile(member.StartPosition);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.RestartKindDefinition restartKindDefinition = this.Reconcile(member.RestartKindDefinition);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DiscardCondition discardCondition = this.Reconcile(member.DiscardCondition);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember dataMember = new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember(member.Id, readOnlyCollection, readOnlyCollection2, group, readOnlyCollection3, dataBinding, matchCondition, startPosition, restartKindDefinition, discardCondition);
			this._scopeTable.Add(dataMember);
			if (num != -1)
			{
				dataMember.RestartIndex = num;
				if (dataMember.HasStartPosition)
				{
					Util.AddToLazyList<int>(ref this._restartIndicesWithStartPosition, num);
				}
			}
			this.DetermineEligibilityForDictionaryEncoding(dataMember);
			this._dataMemberDepth--;
			return dataMember;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004230 File Offset: 0x00002430
		private void DetermineEligibilityForDictionaryEncoding(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember resolvedMember)
		{
			if (this._dataMemberDepth != 1)
			{
				return;
			}
			if (!resolvedMember.IsDynamic)
			{
				this._encodingHints.ExcludeMemberCalculationsFromDictionaryEncoding(resolvedMember);
				return;
			}
			if (resolvedMember.Group.GroupKeys.Count == 1 || !resolvedMember.DataMembers.IsNullOrEmpty<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember>())
			{
				this._encodingHints.ExcludeCalculationsForDictionaryEncoding(resolvedMember.Calculations);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004290 File Offset: 0x00002490
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Group Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Group group)
		{
			if (group == null)
			{
				return null;
			}
			IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.GroupKey> list = group.GroupKeys.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.GroupKey>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ScopeIdDefinition scopeIdDefinition = this.Reconcile(group.ScopeIdDefinition);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Group(list, scopeIdDefinition);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000042CC File Offset: 0x000024CC
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.GroupKey Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.GroupKey key)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.GroupKey(this.Reconcile(key.Value));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000042DF File Offset: 0x000024DF
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ScopeIdDefinition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ScopeIdDefinition scopeIdDefn)
		{
			if (scopeIdDefn == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ScopeIdDefinition(scopeIdDefn.ScopeKeys.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ScopeKey, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ScopeKey>(this.Reconcile)));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004302 File Offset: 0x00002502
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ScopeKey Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.ScopeKey key)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.ScopeKey(this.Reconcile(key.Value));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004318 File Offset: 0x00002518
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataIntersection intersection)
		{
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape> readOnlyCollection = intersection.DataShapes.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataShape, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape>(this.Reconcile));
			ReadOnlyCollection<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation> readOnlyCollection2 = intersection.Calculations.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Calculation, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Calculation>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding dataBinding = this.Reconcile(intersection.DataBinding);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection dataIntersection = new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection(intersection.Id, readOnlyCollection, readOnlyCollection2, dataBinding);
			this._scopeTable.Add(dataIntersection);
			return dataIntersection;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004380 File Offset: 0x00002580
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataBinding binding)
		{
			if (binding == null)
			{
				return null;
			}
			int resultTableIndex = this._lookupTable.GetResultTableIndex(binding.TableId);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding(binding.TableId, resultTableIndex, binding.Relationships.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Relationship, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship>(this.Reconcile)), binding.RestoreContext);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000043D0 File Offset: 0x000025D0
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Relationship relationship)
		{
			if (relationship == null)
			{
				return null;
			}
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship relationship2 = new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship(relationship.ParentScope, relationship.JoinConditions.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.JoinCondition>(this.Reconcile)));
			this._relationshipsToPostProcess.Add(relationship2);
			return relationship2;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004412 File Offset: 0x00002612
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.JoinCondition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.JoinCondition joinCondition)
		{
			if (joinCondition == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.JoinCondition(this.Reconcile(joinCondition.PrimaryKey), this.Reconcile(joinCondition.SecondaryKey), this.Reconcile(joinCondition.SortDirection));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004442 File Offset: 0x00002642
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection sortDirection)
		{
			if (sortDirection == Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection.Ascending)
			{
				return Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Ascending;
			}
			if (sortDirection == Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SortDirection.Descending)
			{
				return Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Descending;
			}
			Contract.RetailFail("Unsupported SortDirection {0}", sortDirection);
			throw new InvalidOperationException();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004464 File Offset: 0x00002664
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.MatchCondition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.MatchCondition condition)
		{
			if (condition == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.MatchCondition(this.Reconcile(condition.Field), condition.Value);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004482 File Offset: 0x00002682
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DiscardCondition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DiscardCondition condition)
		{
			if (condition == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DiscardCondition(this.Reconcile(condition.Field), condition.Value, condition.Operator);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000044A8 File Offset: 0x000026A8
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindows Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataWindows dataWindows)
		{
			if (dataWindows == null)
			{
				return null;
			}
			IReadOnlyList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow> readOnlyList = dataWindows.Windows.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataWindow, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow>(this.Reconcile));
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataBinding dataBinding = this.Reconcile(dataWindows.DataBinding);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindows(readOnlyList, dataBinding);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000044E4 File Offset: 0x000026E4
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataWindow window)
		{
			if (window == null)
			{
				return null;
			}
			IList<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>> restartDefinitions = window.RestartDefinitions;
			ReadOnlyCollection<IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>> readOnlyCollection = ((restartDefinitions != null) ? restartDefinitions.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>(this.Reconcile)) : null);
			IList<string> segmentationTableIds = window.SegmentationTableIds;
			ReadOnlyCollection<int> readOnlyCollection2 = ((segmentationTableIds != null) ? segmentationTableIds.Reconcile(new Func<string, int>(this._lookupTable.GetResultTableIndex)) : null);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode expressionNode = ((window.IsExceededDbCount != null) ? this.Reconcile(window.IsExceededDbCount) : null);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode expressionNode2 = ((window.DbCount != null) ? this.Reconcile(window.DbCount) : null);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow(window.Id, this.Reconcile(window.Count), expressionNode, expressionNode2, readOnlyCollection, readOnlyCollection2, window.Targets, window.AppliesTo, window.ExceededDetection, window.TelemetryId);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000459C File Offset: 0x0000279C
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow ReconcileLegacyDataWindow(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataWindow window, IList<IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>> restartDefinitions)
		{
			if (window == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataWindow(this.Reconcile(window.Count), restartDefinitions ?? Util.EmptyArray<IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>>(), window.TelemetryId);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000045C4 File Offset: 0x000027C4
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimit Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataLimit limit)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimitOperator dataLimitOperator = this.Reconcile(limit.Operator);
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimit(limit.Id, dataLimitOperator, limit.Targets, limit.Within, limit.AppliesTo, limit.TelemetryId);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004604 File Offset: 0x00002804
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimitOperator Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.DataLimitOperator op)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode expressionNode = this.Reconcile(op.Count);
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode expressionNode2 = null;
			if (op.DbCount != null)
			{
				expressionNode2 = this.Reconcile(op.DbCount);
			}
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode expressionNode3 = null;
			if (op.WarningCount != null)
			{
				expressionNode3 = this.Reconcile(op.WarningCount);
			}
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode expressionNode4 = null;
			if (op.IsExceededDbCount != null)
			{
				expressionNode4 = this.Reconcile(op.IsExceededDbCount);
			}
			if (op is Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TopLimitOperator)
			{
				return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.TopLimitOperator(expressionNode, expressionNode2, expressionNode4, op.Kind.GetValueOrDefault(), expressionNode3);
			}
			if (op is Microsoft.DataShaping.InternalContracts.DataShapeDefinition.BottomLimitOperator)
			{
				return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.BottomLimitOperator(expressionNode, expressionNode2, expressionNode4, op.Kind.GetValueOrDefault(), expressionNode3);
			}
			if (op is Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SampleLimitOperator)
			{
				return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SampleLimitOperator(expressionNode, expressionNode2, expressionNode4, op.Kind.GetValueOrDefault(), expressionNode3);
			}
			if (op is Microsoft.DataShaping.InternalContracts.DataShapeDefinition.BinnedLineSampleLimitOperator)
			{
				return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.BinnedLineSampleLimitOperator(expressionNode, expressionNode2, expressionNode4, op.Kind ?? ExceededDetectionKind.DbCountVsInstances, expressionNode3);
			}
			if (op is Microsoft.DataShaping.InternalContracts.DataShapeDefinition.OverlappingPointsSampleLimitOperator)
			{
				return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.OverlappingPointsSampleLimitOperator(expressionNode, expressionNode2, expressionNode4, op.Kind ?? ExceededDetectionKind.DbCountVsInstances, expressionNode3);
			}
			if (op is Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TopNPerLevelLimitOperator)
			{
				return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.TopNPerLevelLimitOperator(expressionNode, expressionNode2, expressionNode4, op.Kind.GetValueOrDefault(), expressionNode3);
			}
			Contract.RetailFail("Unknown limit operator");
			throw new InvalidOperationException("Unknown limit operator");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004755 File Offset: 0x00002955
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message Reconcile(Microsoft.DataShaping.InternalContracts.Message message)
		{
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message(message.Code, message.Severity, message.Text, message.ObjectName, message.ObjectType, message.PropertyName);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004780 File Offset: 0x00002980
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.StartPosition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.StartPosition startPosition)
		{
			if (startPosition == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.StartPosition(startPosition.Values, startPosition.Expressions.Reconcile(new Func<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions.ExpressionNode>(this.Reconcile)));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000047A9 File Offset: 0x000029A9
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.RestartKindDefinition Reconcile(Microsoft.DataShaping.InternalContracts.DataShapeDefinition.RestartKindDefinition restartKindDefinition)
		{
			if (restartKindDefinition == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.RestartKindDefinition(this.Reconcile(restartKindDefinition.RestartIndicator));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000047C1 File Offset: 0x000029C1
		private void PostProcessDataLimits(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape)
		{
			DataLimitReconciler.ReconcileLimitsAndMembers(dataShape.DataLimits, dataShape.PrimaryHierarchy, dataShape.SecondaryHierarchy, this._scopeTable);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000047E0 File Offset: 0x000029E0
		private void PostProcessDataWindows(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataShape dataShape)
		{
			DataWindowReconciler.ReconcileWindowsAndMembers(dataShape.DataWindow, dataShape.DataWindows, dataShape.PrimaryHierarchy, dataShape.SecondaryHierarchy, this._scopeTable);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004808 File Offset: 0x00002A08
		private void PostProcessRelationships()
		{
			foreach (Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship relationship in this._relationshipsToPostProcess)
			{
				relationship.ParentScope = this._scopeTable.Get(relationship.ParentScopeId);
			}
		}

		// Token: 0x0400008D RID: 141
		private const int DisabledRestartIndex = -1;

		// Token: 0x0400008E RID: 142
		private readonly ResultTableLookup _lookupTable;

		// Token: 0x0400008F RID: 143
		private readonly ExpressionReconciler _expressionReconciler;

		// Token: 0x04000090 RID: 144
		private readonly ScopeLookupTable _scopeTable;

		// Token: 0x04000091 RID: 145
		private readonly IDataShapingDataSourceInfo _dataSourceInfo;

		// Token: 0x04000092 RID: 146
		private readonly IList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Relationship> _relationshipsToPostProcess;

		// Token: 0x04000093 RID: 147
		private int _parentDataShapesCount;

		// Token: 0x04000094 RID: 148
		private bool _isInSecondary;

		// Token: 0x04000095 RID: 149
		private int _restartIndexCount;

		// Token: 0x04000096 RID: 150
		private List<int> _restartIndicesWithStartPosition;

		// Token: 0x04000097 RID: 151
		private int _dataMemberDepth;

		// Token: 0x04000098 RID: 152
		private WritableResultEncodingHints _encodingHints;
	}
}
