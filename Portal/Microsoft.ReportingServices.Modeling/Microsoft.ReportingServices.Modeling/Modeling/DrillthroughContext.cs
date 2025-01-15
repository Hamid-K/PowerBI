using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000099 RID: 153
	internal sealed class DrillthroughContext : IXmlLoadable, IValidationScope
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x00017EFB File Offset: 0x000160FB
		private DrillthroughContext(SemanticModel model)
		{
			this.m_sourceQuery = new SemanticQuery(model);
			this.m_selectedItems = new List<Expression>();
			this.m_groupingValues = new Dictionary<Grouping, string>();
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00017F25 File Offset: 0x00016125
		private SemanticQuery SourceQuery
		{
			get
			{
				return this.m_sourceQuery;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00017F2D File Offset: 0x0001612D
		private List<Expression> SelectedItems
		{
			get
			{
				return this.m_selectedItems;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00017F35 File Offset: 0x00016135
		private Dictionary<Grouping, string> GroupingValues
		{
			get
			{
				return this.m_groupingValues;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00017F40 File Offset: 0x00016140
		internal void Process(SemanticQuery targetQuery, CompilationContext compileCtx)
		{
			Grouping grouping;
			if (!this.CompileSelectedItems(compileCtx, out grouping))
			{
				return;
			}
			IQueryEntity queryEntity = DrillthroughContext.GetBaseEntity(this.m_sourceQuery);
			IQueryEntity baseEntity = DrillthroughContext.GetBaseEntity(targetQuery);
			if (grouping != null)
			{
				if (!grouping.IsEntityGrouping)
				{
					throw new InternalModelingException("Unexpected non-entity parent grouping");
				}
				queryEntity = grouping.Expression.NodeAsEntityRef.Entity;
			}
			if (!this.CompileSelectedPath(compileCtx, queryEntity, baseEntity))
			{
				return;
			}
			List<DrillthroughContext.FilteredPath> list = DrillthroughContext.CalculateBranchesAlgorithm.Calculate(this.m_selectedItems, this.m_selectedPath);
			ExpressionCopyManager expressionCopyManager = new ExpressionCopyManager(targetQuery);
			Expression expression = DrillthroughContext.ProcessBranchesAlgorithm.Process(list, grouping, this.m_sourceQuery, this.m_groupingValues, compileCtx, expressionCopyManager);
			if (expression != null)
			{
				if (targetQuery.Hierarchies.Count == 0)
				{
					targetQuery.Hierarchies.Add(new Hierarchy(baseEntity));
				}
				if (targetQuery.Hierarchies[0].Filter == null)
				{
					targetQuery.Hierarchies[0].Filter = expression;
				}
				else
				{
					targetQuery.Hierarchies[0].Filter = new Expression(new FunctionNode(FunctionName.And, new Expression[]
					{
						targetQuery.Hierarchies[0].Filter,
						expression
					}));
				}
			}
			foreach (CustomProperty customProperty in this.m_sourceQuery.CustomProperties)
			{
				if (!targetQuery.CustomProperties.Contains(customProperty.Name))
				{
					targetQuery.CustomProperties.Add(customProperty.Clone());
				}
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000180C4 File Offset: 0x000162C4
		private bool CompileSelectedItems(CompilationContext compileCtx, out Grouping parentGrouping)
		{
			if (this.m_selectedItems.Count == 0)
			{
				throw new InternalModelingException("m_selectedItems is empty");
			}
			parentGrouping = null;
			foreach (Grouping grouping in this.m_sourceQuery.GetAllGroupings())
			{
				if (grouping.Expression == this.m_selectedItems[0])
				{
					if (this.m_selectedItems.Count > 1)
					{
						compileCtx.AddScopedError(ModelingErrorCode.InvalidDrillSelectedItems, SRErrors.InvalidDrillSelectedItems);
						return false;
					}
					return true;
				}
				else if (grouping.Details.Contains(this.m_selectedItems[0]))
				{
					for (int i = 1; i < this.m_selectedItems.Count; i++)
					{
						if (!grouping.Details.Contains(this.m_selectedItems[i]))
						{
							compileCtx.AddScopedError(ModelingErrorCode.InvalidDrillSelectedItems, SRErrors.InvalidDrillSelectedItems);
							return false;
						}
					}
					parentGrouping = grouping;
					return true;
				}
			}
			foreach (MeasureGroup measureGroup in this.m_sourceQuery.MeasureGroups)
			{
				if (measureGroup.Measures.Contains(this.m_selectedItems[0]))
				{
					for (int j = 1; j < this.m_selectedItems.Count; j++)
					{
						if (!measureGroup.Measures.Contains(this.m_selectedItems[j]))
						{
							compileCtx.AddScopedError(ModelingErrorCode.InvalidDrillSelectedItems, SRErrors.InvalidDrillSelectedItems);
							return false;
						}
					}
					return true;
				}
			}
			throw new InternalModelingException("m_selectedItems contains an unknown item");
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00018288 File Offset: 0x00016488
		private bool CompileSelectedPath(CompilationContext compileCtx, IQueryEntity sourceEntity, IQueryEntity targetEntity)
		{
			if (this.m_selectedPath == null)
			{
				this.m_selectedPath = DrillthroughContext.CalculateMaxDrillablePathAlgorithm.Calculate(this.m_selectedItems);
			}
			else
			{
				ExpressionPath expressionPath = DrillthroughContext.CalculateMaxDrillablePathAlgorithm.Calculate(this.m_selectedItems);
				if (!expressionPath.StartsWith(this.m_selectedPath))
				{
					compileCtx.AddScopedError(ModelingErrorCode.InvalidDrillSelectedPath, SRErrors.InvalidDrillSelectedPath(this.m_selectedPath, expressionPath));
					return false;
				}
			}
			compileCtx.PushContextEntity(sourceEntity);
			try
			{
				ExpressionPath expressionPath2 = this.m_selectedPath.Clone();
				int num = expressionPath2.Length;
				DrillthroughContext.SelectedPathCompilationResult selectedPathCompilationResult;
				for (;;)
				{
					selectedPathCompilationResult = DrillthroughContext.TryCompileSelectedPathSubset(expressionPath2, compileCtx, targetEntity, false);
					if (selectedPathCompilationResult == DrillthroughContext.SelectedPathCompilationResult.Success)
					{
						goto IL_0095;
					}
					if (selectedPathCompilationResult == DrillthroughContext.SelectedPathCompilationResult.CompilationFailed)
					{
						break;
					}
					num--;
					if (num >= 0)
					{
						expressionPath2 = this.m_selectedPath.GetSegment(0, num);
					}
					if (num < 0)
					{
						goto IL_0095;
					}
				}
				return false;
				IL_0095:
				if (selectedPathCompilationResult != DrillthroughContext.SelectedPathCompilationResult.Success)
				{
					DrillthroughContext.TryCompileSelectedPathSubset(this.m_selectedPath, compileCtx, targetEntity, true);
					return false;
				}
				this.m_selectedPath = expressionPath2;
			}
			finally
			{
				compileCtx.PopContextEntity();
			}
			return true;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00018368 File Offset: 0x00016568
		private static DrillthroughContext.SelectedPathCompilationResult TryCompileSelectedPathSubset(ExpressionPath selectedPathSubset, CompilationContext compileCtx, IQueryEntity targetEntity, bool registerError)
		{
			IQueryEntity queryEntity;
			if (selectedPathSubset.Compile(compileCtx, targetEntity, out queryEntity) == null)
			{
				return DrillthroughContext.SelectedPathCompilationResult.CompilationFailed;
			}
			compileCtx.PushContextEntity(queryEntity);
			try
			{
				if (!compileCtx.MatchesContextEntity(targetEntity))
				{
					if (registerError)
					{
						compileCtx.AddScopedError(ModelingErrorCode.InvalidDrillTargetEntity, SRErrors.InvalidDrillTargetEntity(compileCtx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(targetEntity), compileCtx.ContextRootEntityDescriptor));
					}
					return DrillthroughContext.SelectedPathCompilationResult.TargetEntityMismatch;
				}
			}
			finally
			{
				compileCtx.PopContextEntity();
			}
			return DrillthroughContext.SelectedPathCompilationResult.Success;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000183D8 File Offset: 0x000165D8
		internal static DrillthroughContext FromString(string sourceQuery, string context, SemanticModel model)
		{
			DrillthroughContext drillthroughContext = new DrillthroughContext(model);
			using (XmlReader xmlReader = XmlRWFactory.CreateReader(new StringReader(sourceQuery)))
			{
				using (XmlReader xmlReader2 = XmlRWFactory.CreateReader(new StringReader(context)))
				{
					drillthroughContext.Load(xmlReader, xmlReader2);
				}
			}
			return drillthroughContext;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00018440 File Offset: 0x00016640
		private static IQueryEntity GetBaseEntity(SemanticQuery query)
		{
			if (query.Hierarchies.Count > 1 || query.MeasureGroups.Count > 1)
			{
				throw new InternalModelingException("Drillthrough with multiple hierarchies and/or measure groups is not implemented");
			}
			if (query.Hierarchies.Count == 0 && query.MeasureGroups.Count == 0)
			{
				throw new InternalModelingException("Query is empty");
			}
			if (query.Hierarchies.Count != 1)
			{
				return query.MeasureGroups[0].BaseEntity;
			}
			return query.Hierarchies[0].BaseEntity;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000184CC File Offset: 0x000166CC
		private void Load(XmlReader sourceQuery, XmlReader context)
		{
			CompilationContext compilationContext = new CompilationContext(true, true);
			DeserializationContext deserializationContext = new DeserializationContext(this.m_sourceQuery.Model, compilationContext);
			ModelingXmlReader sourceQueryMxr = new ModelingXmlReader(sourceQuery, SemanticModelingSchema.Strict, deserializationContext);
			ModelingXmlReader contextMxr = new ModelingXmlReader(context, SemanticModelingSchema.Strict, deserializationContext);
			XmlUtil.WrapXmlExceptions(delegate
			{
				this.LoadCore(sourceQueryMxr, contextMxr);
			}, ModelingErrorCode.InvalidDrillthroughContext, new XmlUtil.ErrorMessageWrap(SRErrors.InvalidDrillthroughContext));
			compilationContext.ThrowIfErrors();
			deserializationContext.CompleteLoad();
			compilationContext.ThrowIfErrors();
			this.m_sourceQuery.Compile(compilationContext);
			compilationContext.ThrowIfErrors();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00018564 File Offset: 0x00016764
		private void LoadCore(ModelingXmlReader sourceQueryReader, ModelingXmlReader contextReader)
		{
			this.m_sourceQuery.Load(sourceQueryReader);
			if (sourceQueryReader.Validation.HasErrors)
			{
				return;
			}
			contextReader.Validation.PushScope(this.m_sourceQuery);
			contextReader.Validation.PushScope(this);
			try
			{
				XmlUtil.CheckElement(contextReader.Reader, "DrillthroughContext", "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");
				contextReader.LoadObject(this);
			}
			finally
			{
				contextReader.Validation.PopScope();
				contextReader.Validation.PopScope();
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000185F0 File Offset: 0x000167F0
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			return false;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000185F4 File Offset: 0x000167F4
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "SemanticQuery")
				{
					this.m_sourceQuery.Load(xr);
					return true;
				}
				if (localName == "SelectedItems")
				{
					xr.LoadObject(new DrillthroughContext.SelectedItemsLoader(this));
					return true;
				}
				if (localName == "SelectedPath")
				{
					this.m_selectedPath = new ExpressionPath();
					this.m_selectedPath.Load(xr);
					return true;
				}
				if (localName == "GroupingValues")
				{
					xr.LoadObject(new DrillthroughContext.GroupingValuesLoader(this));
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00018690 File Offset: 0x00016890
		internal static void WriteXml(XmlWriter writer, ICollection<string> selectedItems, string selectedPathXml, IDictionary<string, object> groupingValues)
		{
			if (selectedItems == null || selectedItems.Count == 0)
			{
				throw new ArgumentNullException("selectedItems");
			}
			ModelingXmlWriter modelingXmlWriter = new ModelingXmlWriter(writer, SemanticModelingSchema.Relaxed, ModelingSerializationOptions.None);
			modelingXmlWriter.WriteStartElement("DrillthroughContext");
			DrillthroughContext.WriteSelectedItems(modelingXmlWriter, selectedItems);
			if (!string.IsNullOrEmpty(selectedPathXml))
			{
				modelingXmlWriter.Writer.WriteRaw(selectedPathXml);
			}
			DrillthroughContext.WriteGroupingValues(modelingXmlWriter, groupingValues);
			modelingXmlWriter.WriteEndElement();
			modelingXmlWriter.Writer.Flush();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00018700 File Offset: 0x00016900
		internal static void WriteSelectedPathXml(XmlWriter writer, ExpressionPath selectedPath)
		{
			ModelingXmlWriter modelingXmlWriter = new ModelingXmlWriter(writer, SemanticModelingSchema.Relaxed, ModelingSerializationOptions.None);
			if (selectedPath.IsEmpty)
			{
				modelingXmlWriter.WriteStartElement("SelectedPath");
				modelingXmlWriter.WriteEndElement();
			}
			else
			{
				selectedPath.WriteTo(modelingXmlWriter, "SelectedPath");
			}
			modelingXmlWriter.Writer.Flush();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001874C File Offset: 0x0001694C
		private static void WriteSelectedItems(ModelingXmlWriter xw, ICollection<string> selectedItems)
		{
			xw.WriteStartElement("SelectedItems");
			foreach (string text in selectedItems)
			{
				xw.WriteElement("SelectedItemName", text);
			}
			xw.WriteEndElement();
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000187AC File Offset: 0x000169AC
		private static void WriteGroupingValues(ModelingXmlWriter xw, IDictionary<string, object> groupingValues)
		{
			if (groupingValues == null || groupingValues.Count == 0)
			{
				return;
			}
			xw.WriteStartElement("GroupingValues");
			foreach (KeyValuePair<string, object> keyValuePair in groupingValues)
			{
				xw.WriteStartElement("GroupingValue");
				xw.WriteAttribute("Name", keyValuePair.Key);
				if (keyValuePair.Value == null)
				{
					xw.WriteNilAttribute();
				}
				else
				{
					xw.WriteValue(keyValuePair.Value);
				}
				xw.WriteEndElement();
			}
			xw.WriteEndElement();
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001884C File Offset: 0x00016A4C
		string IValidationScope.ObjectType
		{
			get
			{
				return "DrillthroughContext";
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00018853 File Offset: 0x00016A53
		string IValidationScope.ObjectID
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001885A File Offset: 0x00016A5A
		string IValidationScope.ObjectName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000383 RID: 899
		private const string DrillthroughContextElem = "DrillthroughContext";

		// Token: 0x04000384 RID: 900
		private const string SelectedItemsElem = "SelectedItems";

		// Token: 0x04000385 RID: 901
		private const string SelectedItemNameElem = "SelectedItemName";

		// Token: 0x04000386 RID: 902
		private const string SelectedPathElem = "SelectedPath";

		// Token: 0x04000387 RID: 903
		private const string GroupingValuesElem = "GroupingValues";

		// Token: 0x04000388 RID: 904
		private const string GroupingValueElem = "GroupingValue";

		// Token: 0x04000389 RID: 905
		private const string NameAttr = "Name";

		// Token: 0x0400038A RID: 906
		private const string NameProperty = "GroupingValue.Name";

		// Token: 0x0400038B RID: 907
		private SemanticQuery m_sourceQuery;

		// Token: 0x0400038C RID: 908
		private readonly List<Expression> m_selectedItems;

		// Token: 0x0400038D RID: 909
		private ExpressionPath m_selectedPath;

		// Token: 0x0400038E RID: 910
		private readonly Dictionary<Grouping, string> m_groupingValues;

		// Token: 0x02000183 RID: 387
		private enum SelectedPathCompilationResult
		{
			// Token: 0x040006A7 RID: 1703
			Success,
			// Token: 0x040006A8 RID: 1704
			CompilationFailed,
			// Token: 0x040006A9 RID: 1705
			TargetEntityMismatch
		}

		// Token: 0x02000184 RID: 388
		private sealed class SelectedItemsLoader : ModelingXmlLoaderBase<DrillthroughContext>, IDeserializationCallback
		{
			// Token: 0x0600100C RID: 4108 RVA: 0x000324F3 File Offset: 0x000306F3
			internal SelectedItemsLoader(DrillthroughContext item)
				: base(item)
			{
			}

			// Token: 0x0600100D RID: 4109 RVA: 0x000324FC File Offset: 0x000306FC
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "SelectedItemName")
				{
					xr.Context.AddReference(this, xr.ReadReferenceByName("SelectedItemName", false));
					return true;
				}
				return base.LoadXmlElement(xr);
			}

			// Token: 0x0600100E RID: 4110 RVA: 0x0003253C File Offset: 0x0003073C
			bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
			{
				if (reference.PropertyName == "SelectedItemName")
				{
					Expression expression = SemanticQuery.LookupResultExpressionChecked(reference, ctx.Validation);
					base.Item.SelectedItems.Add(expression);
					return true;
				}
				return false;
			}
		}

		// Token: 0x02000185 RID: 389
		private sealed class GroupingValuesLoader : ModelingXmlLoaderBase<DrillthroughContext>
		{
			// Token: 0x0600100F RID: 4111 RVA: 0x0003257D File Offset: 0x0003077D
			internal GroupingValuesLoader(DrillthroughContext item)
				: base(item)
			{
			}

			// Token: 0x06001010 RID: 4112 RVA: 0x00032586 File Offset: 0x00030786
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "GroupingValue")
				{
					new DrillthroughContext.GroupingValueLoader(base.Item).Load(xr);
					return true;
				}
				return base.LoadXmlElement(xr);
			}
		}

		// Token: 0x02000186 RID: 390
		private sealed class GroupingValueLoader : IDeserializationCallback
		{
			// Token: 0x06001011 RID: 4113 RVA: 0x000325BC File Offset: 0x000307BC
			internal GroupingValueLoader(DrillthroughContext item)
			{
				this.m_item = item;
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x000325CC File Offset: 0x000307CC
			internal void Load(ModelingXmlReader xr)
			{
				if (xr.MoveToAttribute("Name"))
				{
					xr.Context.AddReference(this, xr.ReadReferenceByName("GroupingValue.Name", true));
					xr.MoveToElement();
				}
				if (xr.IsNil)
				{
					this.m_value = null;
					xr.Skip();
					return;
				}
				this.m_value = xr.ReadValueAsString();
			}

			// Token: 0x06001013 RID: 4115 RVA: 0x00032628 File Offset: 0x00030828
			bool IDeserializationCallback.ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
			{
				if (!(reference.PropertyName == "GroupingValue.Name"))
				{
					return false;
				}
				Grouping grouping = SemanticQuery.TryGetGrouping(reference, true, ctx.Validation);
				if (grouping.IsInvalidRefTarget)
				{
					throw new ValidationException(ctx.Validation.CreateScopedError(ModelingErrorCode.GroupingNotFound, SRErrors.GroupingNotFound_MultipleProperties(reference.PropertyName, ctx.Validation.CurrentObjectDescriptor, reference.ReferenceString)));
				}
				this.m_item.GroupingValues.Add(grouping, this.m_value);
				return true;
			}

			// Token: 0x040006AA RID: 1706
			private readonly DrillthroughContext m_item;

			// Token: 0x040006AB RID: 1707
			private string m_value;
		}

		// Token: 0x02000187 RID: 391
		internal sealed class CalculateMaxDrillablePathAlgorithm
		{
			// Token: 0x06001014 RID: 4116 RVA: 0x000326A9 File Offset: 0x000308A9
			private CalculateMaxDrillablePathAlgorithm()
			{
			}

			// Token: 0x06001015 RID: 4117 RVA: 0x000326BC File Offset: 0x000308BC
			internal static ExpressionPath Calculate(IList<Expression> exprs)
			{
				return new DrillthroughContext.CalculateMaxDrillablePathAlgorithm().CalculateCore(exprs);
			}

			// Token: 0x06001016 RID: 4118 RVA: 0x000326CC File Offset: 0x000308CC
			internal static bool ConsiderArgumentDrillPath(Expression expr)
			{
				if (!expr.Path.IsEmpty)
				{
					return true;
				}
				if (expr.NodeAsFunction != null)
				{
					using (List<Expression>.Enumerator enumerator = expr.NodeAsFunction.Arguments.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (DrillthroughContext.CalculateMaxDrillablePathAlgorithm.ConsiderArgumentDrillPath(enumerator.Current))
							{
								return true;
							}
						}
					}
					return false;
				}
				return !expr.Node.IsConstantValue;
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x00032754 File Offset: 0x00030954
			private ExpressionPath CalculateCore(IList<Expression> exprs)
			{
				ExpressionPath expressionPath = null;
				foreach (Expression expression in Iterators.Filter<Expression>(exprs, new Predicate<Expression>(DrillthroughContext.CalculateMaxDrillablePathAlgorithm.ConsiderArgumentDrillPath)))
				{
					ExpressionPath expressionPath2 = this.CalculateOne(expression);
					if (expressionPath == null)
					{
						expressionPath = expressionPath2;
					}
					else
					{
						expressionPath.TrimToMatchingSegment(expressionPath2);
					}
				}
				return expressionPath ?? ExpressionPath.Empty;
			}

			// Token: 0x06001018 RID: 4120 RVA: 0x000327C8 File Offset: 0x000309C8
			private ExpressionPath CalculateOne(Expression expr)
			{
				if (!this.PushUniqueExpression(expr))
				{
					throw new InvalidOperationException(SRErrors.CyclicExpression_ExpressionObject(SRObjectDescriptor.FromScope(expr)));
				}
				ExpressionPath expressionPath3;
				try
				{
					ExpressionPath expressionPath = expr.Path.Clone();
					if (expr.NodeAsFunction != null)
					{
						FunctionNode nodeAsFunction = expr.NodeAsFunction;
						ExpressionPath expressionPath2 = null;
						int? passthroughArgument = FunctionInfo.GetPassthroughArgument(nodeAsFunction.FunctionName);
						if (passthroughArgument != null)
						{
							Expression expression = nodeAsFunction.Arguments[passthroughArgument.Value];
							if (DrillthroughContext.CalculateMaxDrillablePathAlgorithm.ConsiderArgumentDrillPath(expression))
							{
								expressionPath2 = this.CalculateOne(expression);
							}
						}
						else
						{
							expressionPath2 = DrillthroughContext.CalculateMaxDrillablePathAlgorithm.Calculate(expr.NodeAsFunction.Arguments);
						}
						if (expressionPath2 != null)
						{
							expressionPath.AddRange(expressionPath2);
						}
					}
					else if (expr.NodeAsAttributeRef != null && expr.NodeAsAttributeRef.CalculatedAttribute != null && DrillthroughContext.CalculateMaxDrillablePathAlgorithm.ConsiderArgumentDrillPath(expr.NodeAsAttributeRef.CalculatedAttribute))
					{
						expressionPath.AddRange(this.CalculateOne(expr.NodeAsAttributeRef.CalculatedAttribute));
					}
					expressionPath3 = expressionPath;
				}
				finally
				{
					this.PopUniqueExpression();
				}
				return expressionPath3;
			}

			// Token: 0x06001019 RID: 4121 RVA: 0x000328C4 File Offset: 0x00030AC4
			private bool PushUniqueExpression(Expression expression)
			{
				if (this.m_expressionStack.Contains(expression))
				{
					return false;
				}
				this.m_expressionStack.Push(expression);
				return true;
			}

			// Token: 0x0600101A RID: 4122 RVA: 0x000328E3 File Offset: 0x00030AE3
			private void PopUniqueExpression()
			{
				this.m_expressionStack.Pop();
			}

			// Token: 0x040006AC RID: 1708
			private readonly Stack<Expression> m_expressionStack = new Stack<Expression>();
		}

		// Token: 0x02000188 RID: 392
		private sealed class CalculateBranchesAlgorithm
		{
			// Token: 0x0600101B RID: 4123 RVA: 0x000328F1 File Offset: 0x00030AF1
			private CalculateBranchesAlgorithm(List<Expression> selectedItems, ExpressionPath selectedPath)
			{
				this.m_selectedItems = selectedItems;
				this.m_selectedPath = selectedPath;
				this.m_branches = new List<DrillthroughContext.FilteredPath>();
				this.m_currentBranch = new DrillthroughContext.FilteredPath();
			}

			// Token: 0x0600101C RID: 4124 RVA: 0x0003291D File Offset: 0x00030B1D
			internal static List<DrillthroughContext.FilteredPath> Calculate(List<Expression> selectedItems, ExpressionPath selectedPath)
			{
				return new DrillthroughContext.CalculateBranchesAlgorithm(selectedItems, selectedPath).Calculate();
			}

			// Token: 0x0600101D RID: 4125 RVA: 0x0003292C File Offset: 0x00030B2C
			internal List<DrillthroughContext.FilteredPath> Calculate()
			{
				foreach (Expression expression in this.m_selectedItems)
				{
					this.CalculateBranches(expression, 0);
				}
				return this.m_branches;
			}

			// Token: 0x0600101E RID: 4126 RVA: 0x00032988 File Offset: 0x00030B88
			private void CalculateBranches(Expression expr, int pathIndex)
			{
				if (this.m_terminateAlgorithm)
				{
					return;
				}
				if (!DrillthroughContext.CalculateMaxDrillablePathAlgorithm.ConsiderArgumentDrillPath(expr))
				{
					return;
				}
				int length = this.m_currentBranch.Length;
				bool flag = true;
				if (this.AddMatchingPathSegment(expr.Path, ref pathIndex))
				{
					if (expr.NodeAsFunction != null && expr.NodeAsFunction.Arguments.Count > 0)
					{
						if (expr.NodeAsFunction.FunctionName == FunctionName.Filter)
						{
							if (expr.NodeAsFunction.Arguments.Count != 2)
							{
								throw new InternalModelingException("Wrong number of arguments to Filter function");
							}
							this.GetOrCreateCurrentItem().Filters.Add(expr.NodeAsFunction.Arguments[1]);
							this.CalculateBranches(expr.NodeAsFunction.Arguments[0], pathIndex);
						}
						else
						{
							foreach (Expression expression in expr.NodeAsFunction.Arguments)
							{
								this.CalculateBranches(expression, pathIndex);
							}
						}
						flag = false;
					}
					else if (expr.NodeAsAttributeRef != null && expr.NodeAsAttributeRef.CalculatedAttribute != null)
					{
						this.CalculateBranches(expr.NodeAsAttributeRef.CalculatedAttribute, pathIndex);
						flag = false;
					}
				}
				if (flag)
				{
					this.TerminateCurrentBranch();
				}
				if (this.m_currentBranch.Length > length)
				{
					this.m_currentBranch.RemoveRange(length, this.m_currentBranch.Length - length);
				}
			}

			// Token: 0x0600101F RID: 4127 RVA: 0x00032AFC File Offset: 0x00030CFC
			private bool AddMatchingPathSegment(ExpressionPath path, ref int pathIndex)
			{
				int matchingSegmentLength = this.m_selectedPath.GetMatchingSegmentLength(pathIndex, path);
				for (int i = 0; i < matchingSegmentLength; i++)
				{
					this.m_currentBranch.Add(new DrillthroughContext.FilteredPathItem(path[i]));
				}
				pathIndex += matchingSegmentLength;
				return matchingSegmentLength == path.Length;
			}

			// Token: 0x06001020 RID: 4128 RVA: 0x00032B4A File Offset: 0x00030D4A
			private DrillthroughContext.FilteredPathItem GetOrCreateCurrentItem()
			{
				if (this.m_currentBranch.Length == 0)
				{
					this.m_currentBranch.Add(new DrillthroughContext.FilteredPathItem(null));
				}
				return this.m_currentBranch.LastItem;
			}

			// Token: 0x06001021 RID: 4129 RVA: 0x00032B78 File Offset: 0x00030D78
			private void TerminateCurrentBranch()
			{
				int num = 0;
				if (this.m_currentBranch.Length > 0)
				{
					if (this.m_currentBranch[0].PathItem != null)
					{
						num = this.m_currentBranch.Length;
					}
					else
					{
						num = this.m_currentBranch.Length - 1;
					}
				}
				if (num < this.m_selectedPath.Length)
				{
					throw new InternalModelingException("Attempt to terminate with partial branch");
				}
				if (!object.Equals((this.m_currentBranch.Length > 0) ? this.m_currentBranch.LastItem.PathItem : null, this.m_selectedPath.LastItem))
				{
					throw new InternalModelingException("m_currentBranch.LastItem does not match m_selectedPath.LastItem");
				}
				bool flag = false;
				using (List<DrillthroughContext.FilteredPathItem>.Enumerator enumerator = this.m_currentBranch.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Filters.Count > 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this.m_branches.Clear();
					this.m_branches.Add(this.m_currentBranch.Clone());
					this.m_terminateAlgorithm = true;
					return;
				}
				foreach (DrillthroughContext.FilteredPath filteredPath in this.m_branches)
				{
					if (this.m_currentBranch.IsSameAs(filteredPath))
					{
						return;
					}
				}
				this.m_branches.Add(this.m_currentBranch.Clone());
			}

			// Token: 0x040006AD RID: 1709
			private readonly List<Expression> m_selectedItems;

			// Token: 0x040006AE RID: 1710
			private readonly ExpressionPath m_selectedPath;

			// Token: 0x040006AF RID: 1711
			private readonly List<DrillthroughContext.FilteredPath> m_branches;

			// Token: 0x040006B0 RID: 1712
			private DrillthroughContext.FilteredPath m_currentBranch;

			// Token: 0x040006B1 RID: 1713
			private bool m_terminateAlgorithm;
		}

		// Token: 0x02000189 RID: 393
		private sealed class ProcessBranchesAlgorithm
		{
			// Token: 0x06001022 RID: 4130 RVA: 0x00032CFC File Offset: 0x00030EFC
			private ProcessBranchesAlgorithm(List<DrillthroughContext.FilteredPath> branches, Grouping parentGrouping, SemanticQuery sourceQuery, Dictionary<Grouping, string> groupingValues, CompilationContext compileCtx, ExpressionCopyManager copyManager)
			{
				this.m_branches = branches;
				this.m_parentGrouping = parentGrouping;
				this.m_sourceQuery = sourceQuery;
				this.m_groupingValues = groupingValues;
				this.m_compileCtx = compileCtx;
				this.m_copyManager = copyManager;
			}

			// Token: 0x06001023 RID: 4131 RVA: 0x00032D31 File Offset: 0x00030F31
			internal static Expression Process(List<DrillthroughContext.FilteredPath> branches, Grouping parentGrouping, SemanticQuery sourceQuery, Dictionary<Grouping, string> groupingValues, CompilationContext compileCtx, ExpressionCopyManager copyManager)
			{
				return new DrillthroughContext.ProcessBranchesAlgorithm(branches, parentGrouping, sourceQuery, groupingValues, compileCtx, copyManager).Process();
			}

			// Token: 0x06001024 RID: 4132 RVA: 0x00032D48 File Offset: 0x00030F48
			private Expression Process()
			{
				List<Expression> list = new List<Expression>();
				foreach (DrillthroughContext.FilteredPath filteredPath in this.m_branches)
				{
					this.m_rootExpr = null;
					this.m_currentExpr = null;
					this.m_curScalarPath = new ExpressionPath();
					this.m_nextPath = new ExpressionPath();
					this.ProcessBranch(filteredPath);
					if (this.m_rootExpr != null)
					{
						list.Add(this.m_rootExpr);
					}
				}
				return DrillthroughContext.ProcessBranchesAlgorithm.OrExpressions(list);
			}

			// Token: 0x06001025 RID: 4133 RVA: 0x00032DE0 File Offset: 0x00030FE0
			private void ProcessBranch(DrillthroughContext.FilteredPath branch)
			{
				foreach (DrillthroughContext.FilteredPathItem filteredPathItem in Iterators.Reverse<DrillthroughContext.FilteredPathItem>(branch))
				{
					if (filteredPathItem.Filters.Count > 0)
					{
						this.SetNextFilterExpr(DrillthroughContext.ProcessBranchesAlgorithm.AndExpressions(this.CloneForTarget(filteredPathItem.Filters)));
					}
					if (filteredPathItem.PathItem != null)
					{
						this.m_nextPath.Add(filteredPathItem.PathItem.CreateReverse());
					}
				}
				List<Expression> list = new List<Expression>();
				ExpressionPath expressionPath = null;
				if (this.m_parentGrouping != null && !this.m_parentGrouping.Expression.Path.IsEmpty)
				{
					expressionPath = this.m_parentGrouping.Expression.Path.Clone();
					expressionPath.Reverse();
				}
				if (this.m_sourceQuery.Hierarchies.Count > 1)
				{
					throw new InternalModelingException("Multiple hierarchy support is not implemented.");
				}
				if (this.m_sourceQuery.Hierarchies.Count == 1 && this.m_sourceQuery.Hierarchies[0].Filter != null)
				{
					list.Add(this.CloneForTarget(this.m_sourceQuery.Hierarchies[0].Filter));
				}
				bool flag = false;
				foreach (KeyValuePair<Grouping, string> keyValuePair in this.m_groupingValues)
				{
					if (keyValuePair.Key == this.m_parentGrouping && keyValuePair.Value != null)
					{
						Expression expression = this.CloneForTarget(keyValuePair.Key.Expression);
						expression.Path.Clear();
						expressionPath = null;
						list.Clear();
						list.Add(this.CreateGroupingEqualsExpr(expression, keyValuePair.Value));
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					foreach (KeyValuePair<Grouping, string> keyValuePair2 in this.m_groupingValues)
					{
						if (this.m_sourceQuery.Hierarchies.Count == 1 && keyValuePair2.Key.IsEntityGrouping && keyValuePair2.Key.Expression.NodeAsEntityRef.Entity == this.m_sourceQuery.Hierarchies[0].BaseEntity && keyValuePair2.Value != null)
						{
							list.Clear();
							list.Add(this.CreateGroupingEqualsExpr(this.CloneForTarget(keyValuePair2.Key.Expression), keyValuePair2.Value));
							break;
						}
						list.Add(this.CreateGroupingEqualsExpr(this.CloneForTarget(keyValuePair2.Key.Expression), keyValuePair2.Value));
					}
					flag = true;
				}
				if (!flag)
				{
					throw new InternalModelingException("Some grouping value filters have not been applied.");
				}
				if (expressionPath != null)
				{
					this.m_nextPath.AddRange(expressionPath);
				}
				if (list.Count > 0)
				{
					this.SetNextFilterExpr(DrillthroughContext.ProcessBranchesAlgorithm.AndExpressions(list));
				}
				if (!this.m_curScalarPath.IsEmpty || !this.m_nextPath.IsEmpty)
				{
					bool flag2 = false;
					using (Dictionary<Grouping, string>.ValueCollection.Enumerator enumerator3 = this.m_groupingValues.Values.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							if (enumerator3.Current != null)
							{
								flag2 = true;
								break;
							}
						}
					}
					if (!flag2)
					{
						Expression expression2 = new Expression();
						expression2.Path.AddRange(this.m_curScalarPath);
						expression2.Path.AddRange(this.m_nextPath);
						expression2.Node = new EntityRefNode(DrillthroughContext.GetBaseEntity(this.m_sourceQuery));
						if (expression2.Path.GetCardinality() == Cardinality.Many)
						{
							Expression expression3 = DrillthroughContext.ProcessBranchesAlgorithm.CreateCountGreaterThanZeroExpr(expression2);
							this.AndWithCurrentExpr(expression3);
							return;
						}
						Expression expression4 = new Expression(new FunctionNode(FunctionName.NotEquals, new Expression[]
						{
							expression2,
							new Expression(new NullNode())
						}));
						this.AndWithCurrentExpr(expression4);
					}
				}
			}

			// Token: 0x06001026 RID: 4134 RVA: 0x000331E4 File Offset: 0x000313E4
			private void SetNextFilterExpr(Expression nextExpr)
			{
				if (this.m_nextPath.GetCardinality() == Cardinality.Many)
				{
					Expression expression = new Expression(new FunctionNode(FunctionName.Filter, new Expression[]
					{
						new Expression(new EntityRefNode(this.m_nextPath.LastItem.TargetEntity)),
						nextExpr
					}));
					expression.Path.AddRange(this.m_curScalarPath);
					expression.Path.AddRange(this.m_nextPath);
					Expression expression2 = DrillthroughContext.ProcessBranchesAlgorithm.CreateCountGreaterThanZeroExpr(expression);
					this.AndWithCurrentExpr(expression2);
					this.m_currentExpr = nextExpr;
					this.m_curScalarPath.Clear();
					this.m_nextPath.Clear();
					return;
				}
				this.m_curScalarPath.AddRange(this.m_nextPath);
				this.m_nextPath.Clear();
				if (!this.m_curScalarPath.IsEmpty)
				{
					this.PrependScalarPath(nextExpr);
				}
				this.AndWithCurrentExpr(nextExpr);
			}

			// Token: 0x06001027 RID: 4135 RVA: 0x000332BC File Offset: 0x000314BC
			private void AndWithCurrentExpr(Expression expr)
			{
				if (this.m_currentExpr == null)
				{
					this.m_currentExpr = expr;
				}
				else
				{
					Expression expression = new Expression(this.m_currentExpr.Node, this.m_currentExpr.Path);
					this.m_currentExpr.Path.Clear();
					this.m_currentExpr.Node = new FunctionNode(FunctionName.And, new Expression[] { expression, expr });
				}
				if (this.m_rootExpr == null)
				{
					this.m_rootExpr = this.m_currentExpr;
				}
			}

			// Token: 0x06001028 RID: 4136 RVA: 0x0003333C File Offset: 0x0003153C
			private void PrependScalarPath(Expression expr)
			{
				if (expr.NodeAsFunction != null)
				{
					if (FunctionInfo.IsScalarFunction(expr.NodeAsFunction.FunctionName))
					{
						foreach (Expression expression in expr.NodeAsFunction.Arguments)
						{
							this.PrependScalarPath(expression);
						}
						return;
					}
				}
				else if (expr.Node.IsConstantValue)
				{
					return;
				}
				expr.Path.InsertRange(0, this.m_curScalarPath);
			}

			// Token: 0x06001029 RID: 4137 RVA: 0x000333D0 File Offset: 0x000315D0
			private Expression CloneForTarget(Expression expr)
			{
				return expr.Clone(this.m_copyManager);
			}

			// Token: 0x0600102A RID: 4138 RVA: 0x000333E0 File Offset: 0x000315E0
			private Expression[] CloneForTarget(IList<Expression> exprs)
			{
				Expression[] array = new Expression[exprs.Count];
				for (int i = 0; i < exprs.Count; i++)
				{
					array[i] = this.CloneForTarget(exprs[i]);
				}
				return array;
			}

			// Token: 0x0600102B RID: 4139 RVA: 0x0003341C File Offset: 0x0003161C
			private Expression CreateGroupingEqualsExpr(Expression groupingExpr, string groupingValue)
			{
				DataType dataType = groupingExpr.GetResultType().DataType;
				Expression expression;
				if (groupingValue == null)
				{
					expression = new Expression(new NullNode());
				}
				else
				{
					expression = new Expression(LiteralNode.FromString(groupingValue, dataType, this.m_compileCtx));
				}
				return new Expression(new FunctionNode(FunctionName.Equals, new Expression[] { groupingExpr, expression }));
			}

			// Token: 0x0600102C RID: 4140 RVA: 0x00033474 File Offset: 0x00031674
			private static Expression CreateCountGreaterThanZeroExpr(Expression countArgExpr)
			{
				return new Expression(new FunctionNode(FunctionName.GreaterThan, new Expression[]
				{
					new Expression(new FunctionNode(FunctionName.Count, new Expression[] { countArgExpr })),
					new Expression(new LiteralNode(0L))
				}));
			}

			// Token: 0x0600102D RID: 4141 RVA: 0x000334BB File Offset: 0x000316BB
			private static Expression AndExpressions(IList<Expression> exprs)
			{
				return DrillthroughContext.ProcessBranchesAlgorithm.JoinExpressions(exprs, FunctionName.And);
			}

			// Token: 0x0600102E RID: 4142 RVA: 0x000334C5 File Offset: 0x000316C5
			private static Expression OrExpressions(IList<Expression> exprs)
			{
				return DrillthroughContext.ProcessBranchesAlgorithm.JoinExpressions(exprs, FunctionName.Or);
			}

			// Token: 0x0600102F RID: 4143 RVA: 0x000334D0 File Offset: 0x000316D0
			private static Expression JoinExpressions(IList<Expression> exprs, FunctionName logicalFunction)
			{
				Expression expression = null;
				foreach (Expression expression2 in exprs)
				{
					if (expression == null)
					{
						expression = expression2;
					}
					else
					{
						expression = new Expression(new FunctionNode(logicalFunction, new Expression[] { expression, expression2 }));
					}
				}
				return expression;
			}

			// Token: 0x040006B2 RID: 1714
			private readonly List<DrillthroughContext.FilteredPath> m_branches;

			// Token: 0x040006B3 RID: 1715
			private readonly Grouping m_parentGrouping;

			// Token: 0x040006B4 RID: 1716
			private readonly SemanticQuery m_sourceQuery;

			// Token: 0x040006B5 RID: 1717
			private readonly Dictionary<Grouping, string> m_groupingValues;

			// Token: 0x040006B6 RID: 1718
			private readonly CompilationContext m_compileCtx;

			// Token: 0x040006B7 RID: 1719
			private readonly ExpressionCopyManager m_copyManager;

			// Token: 0x040006B8 RID: 1720
			private Expression m_rootExpr;

			// Token: 0x040006B9 RID: 1721
			private Expression m_currentExpr;

			// Token: 0x040006BA RID: 1722
			private ExpressionPath m_curScalarPath;

			// Token: 0x040006BB RID: 1723
			private ExpressionPath m_nextPath;
		}

		// Token: 0x0200018A RID: 394
		private sealed class FilteredPath : List<DrillthroughContext.FilteredPathItem>
		{
			// Token: 0x06001030 RID: 4144 RVA: 0x00033538 File Offset: 0x00031738
			public FilteredPath()
			{
			}

			// Token: 0x06001031 RID: 4145 RVA: 0x00033540 File Offset: 0x00031740
			public FilteredPath(IEnumerable<DrillthroughContext.FilteredPathItem> items)
				: base(items)
			{
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06001032 RID: 4146 RVA: 0x00033549 File Offset: 0x00031749
			public int Length
			{
				get
				{
					return base.Count;
				}
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06001033 RID: 4147 RVA: 0x00033551 File Offset: 0x00031751
			[EditorBrowsable(EditorBrowsableState.Never)]
			[Obsolete("The Length property must be used instead.", true)]
			public new int Count
			{
				get
				{
					return base.Count;
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06001034 RID: 4148 RVA: 0x00033559 File Offset: 0x00031759
			public DrillthroughContext.FilteredPathItem LastItem
			{
				get
				{
					if (this.Length <= 0)
					{
						return null;
					}
					return base[this.Length - 1];
				}
			}

			// Token: 0x06001035 RID: 4149 RVA: 0x00033574 File Offset: 0x00031774
			public DrillthroughContext.FilteredPath Clone()
			{
				return new DrillthroughContext.FilteredPath(this);
			}

			// Token: 0x06001036 RID: 4150 RVA: 0x0003357C File Offset: 0x0003177C
			public bool IsSameAs(DrillthroughContext.FilteredPath other)
			{
				if (this.Length != other.Length)
				{
					return false;
				}
				for (int i = 0; i < this.Length; i++)
				{
					if (!base[i].IsSameAs(other[i]))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x0200018B RID: 395
		private sealed class FilteredPathItem
		{
			// Token: 0x06001037 RID: 4151 RVA: 0x000335C2 File Offset: 0x000317C2
			public FilteredPathItem(PathItem item)
			{
				this.m_item = item;
				this.m_filters = new List<Expression>();
			}

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x06001038 RID: 4152 RVA: 0x000335DC File Offset: 0x000317DC
			public PathItem PathItem
			{
				get
				{
					return this.m_item;
				}
			}

			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x06001039 RID: 4153 RVA: 0x000335E4 File Offset: 0x000317E4
			public List<Expression> Filters
			{
				get
				{
					return this.m_filters;
				}
			}

			// Token: 0x0600103A RID: 4154 RVA: 0x000335EC File Offset: 0x000317EC
			public bool IsSameAs(DrillthroughContext.FilteredPathItem other)
			{
				if (!object.Equals(this.m_item, other.PathItem))
				{
					return false;
				}
				if (this.m_filters.Count != other.Filters.Count)
				{
					return false;
				}
				for (int i = 0; i < this.m_filters.Count; i++)
				{
					if (!this.m_filters[i].IsSameAs(other.Filters[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040006BC RID: 1724
			private readonly PathItem m_item;

			// Token: 0x040006BD RID: 1725
			private readonly List<Expression> m_filters;
		}
	}
}
