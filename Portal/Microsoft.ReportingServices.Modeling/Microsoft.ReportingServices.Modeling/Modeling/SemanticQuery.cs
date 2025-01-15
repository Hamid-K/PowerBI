using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B9 RID: 185
	public sealed class SemanticQuery : ExtensibleModelingObject, IXmlLoadable, IValidationScope
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x00022DF0 File Offset: 0x00020FF0
		public SemanticQuery()
		{
			this.m_hierarchies = new SemanticQuery.HierarchyCollection(this);
			this.m_measureGroups = new SemanticQuery.MeasureGroupCollection(this);
			this.m_calculatedAttrs = new ExpressionCollection();
			this.m_parameters = new SemanticQuery.ParameterCollection();
			this.m_namespacePrefixes = new XmlNamespacePrefixCollection();
			this.Reset();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00022E42 File Offset: 0x00021042
		public SemanticQuery(SemanticModel model)
			: this()
		{
			this.m_model = model;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00022E54 File Offset: 0x00021054
		private void Reset()
		{
			base.CheckWriteable();
			this.m_hierarchies.Clear();
			this.m_measureGroups.Clear();
			this.m_calculatedAttrs.Clear();
			this.m_parameters.Clear();
			this.m_enableDrillthrough = false;
			this.m_namespacePrefixes.Clear();
			this.m_namespacePrefixes.AddRange(SemanticModelingSchema.DefaultNamespacePrefixes);
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00022EB5 File Offset: 0x000210B5
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x00022EBD File Offset: 0x000210BD
		public SemanticModel Model
		{
			get
			{
				return this.m_model;
			}
			set
			{
				base.CheckWriteable();
				this.m_model = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00022ECC File Offset: 0x000210CC
		public SemanticQuery.HierarchyCollection Hierarchies
		{
			get
			{
				return this.m_hierarchies;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00022ED4 File Offset: 0x000210D4
		public SemanticQuery.MeasureGroupCollection MeasureGroups
		{
			get
			{
				return this.m_measureGroups;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00022EDC File Offset: 0x000210DC
		public ExpressionCollection CalculatedAttributes
		{
			get
			{
				return this.m_calculatedAttrs;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00022EE4 File Offset: 0x000210E4
		public SemanticQuery.ParameterCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00022EEC File Offset: 0x000210EC
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00022EF4 File Offset: 0x000210F4
		public bool EnableDrillthrough
		{
			get
			{
				return this.m_enableDrillthrough;
			}
			set
			{
				base.CheckWriteable();
				this.m_enableDrillthrough = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00022F03 File Offset: 0x00021103
		public XmlNamespacePrefixCollection NamespacePrefixes
		{
			get
			{
				return this.m_namespacePrefixes;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00022F0B File Offset: 0x0002110B
		public IEnumerable<Grouping> GetAllGroupings()
		{
			foreach (Hierarchy hierarchy in this.m_hierarchies)
			{
				foreach (Grouping grouping in hierarchy.Groupings)
				{
					yield return grouping;
				}
				List<Grouping>.Enumerator enumerator2 = default(List<Grouping>.Enumerator);
			}
			List<Hierarchy>.Enumerator enumerator = default(List<Hierarchy>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00022F1B File Offset: 0x0002111B
		public IEnumerable<Expression> GetAllTopLevelExpressions()
		{
			foreach (Expression expression in this.GetAllResultExpressions())
			{
				yield return expression;
			}
			IEnumerator<Expression> enumerator = null;
			foreach (Expression expression2 in this.m_calculatedAttrs)
			{
				yield return expression2;
			}
			List<Expression>.Enumerator enumerator2 = default(List<Expression>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00022F2B File Offset: 0x0002112B
		public IEnumerable<Expression> GetAllResultExpressions()
		{
			foreach (Hierarchy hierarchy in this.m_hierarchies)
			{
				foreach (Grouping g in hierarchy.Groupings)
				{
					if (g.Expression != null)
					{
						yield return g.Expression;
					}
					foreach (Expression expression in g.Details)
					{
						yield return expression;
					}
					List<Expression>.Enumerator enumerator3 = default(List<Expression>.Enumerator);
					g = null;
				}
				List<Grouping>.Enumerator enumerator2 = default(List<Grouping>.Enumerator);
			}
			List<Hierarchy>.Enumerator enumerator = default(List<Hierarchy>.Enumerator);
			foreach (MeasureGroup measureGroup in this.m_measureGroups)
			{
				foreach (Expression expression2 in measureGroup.Measures)
				{
					yield return expression2;
				}
				List<Expression>.Enumerator enumerator3 = default(List<Expression>.Enumerator);
			}
			List<MeasureGroup>.Enumerator enumerator4 = default(List<MeasureGroup>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00022F3B File Offset: 0x0002113B
		public IEnumerable<Expression> GetAllMeasures()
		{
			foreach (MeasureGroup measureGroup in this.m_measureGroups)
			{
				foreach (Expression expression in measureGroup.Measures)
				{
					yield return expression;
				}
				List<Expression>.Enumerator enumerator2 = default(List<Expression>.Enumerator);
			}
			List<MeasureGroup>.Enumerator enumerator = default(List<MeasureGroup>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00022F4B File Offset: 0x0002114B
		public IEnumerable<Parameter> GetAllParameters()
		{
			foreach (Parameter parameter in this.m_parameters)
			{
				yield return parameter;
			}
			List<Parameter>.Enumerator enumerator = default(List<Parameter>.Enumerator);
			if (this.m_enableDrillthrough)
			{
				yield return Parameter.DrillthroughSourceQueryInstance;
				yield return Parameter.DrillthroughContextInstance;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00022F5C File Offset: 0x0002115C
		public ValidationMessageCollection Load(XmlReader xr)
		{
			if (this.m_model == null)
			{
				throw new InvalidOperationException(DevExceptionMessages.SemanticQuery_NullModel);
			}
			ValidationContext validationContext = new ValidationContext();
			this.Load(xr, validationContext, SemanticModelingSchema.Relaxed);
			return validationContext.GetMessages();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00022F98 File Offset: 0x00021198
		private void Load(XmlReader xr, ValidationContext validationCtx, SemanticModelingSchema schema)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			base.CheckWriteable();
			DeserializationContext deserializationContext = new DeserializationContext(this.m_model, validationCtx);
			ModelingXmlReader modelingXmlReader = new ModelingXmlReader(xr, schema, deserializationContext);
			this.Load(modelingXmlReader);
			deserializationContext.Validation.ThrowIfErrors();
			deserializationContext.CompleteLoad();
			deserializationContext.Validation.ThrowIfErrors();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00022FF2 File Offset: 0x000211F2
		internal void Load(ModelingXmlReader xr)
		{
			this.Reset();
			XmlUtil.WrapXmlExceptions(delegate
			{
				this.LoadCore(xr);
			}, ModelingErrorCode.InvalidSemanticQuery, new XmlUtil.ErrorMessageWrap(SRErrors.InvalidSemanticQuery));
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002302C File Offset: 0x0002122C
		private void LoadCore(ModelingXmlReader xr)
		{
			XmlUtil.CheckElement(xr.Reader, "SemanticQuery", "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling");
			xr.Validation.PushScope(this);
			try
			{
				xr.LoadObject("SemanticQuery", this);
			}
			finally
			{
				xr.Validation.PopScope();
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00023084 File Offset: 0x00021284
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				this.m_namespacePrefixes.AddFromReader(xr);
				return true;
			}
			return false;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000230A8 File Offset: 0x000212A8
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "Hierarchies")
				{
					this.m_hierarchies.Load(xr);
					return true;
				}
				if (localName == "MeasureGroups")
				{
					this.m_measureGroups.Load(xr);
					return true;
				}
				if (localName == "CalculatedAttributes")
				{
					this.m_calculatedAttrs.Load(xr, true);
					return true;
				}
				if (localName == "Parameters")
				{
					this.m_parameters.Load(xr);
					return true;
				}
				if (localName == "EnableDrillthrough")
				{
					this.m_enableDrillthrough = xr.ReadValueAsBoolean();
					return true;
				}
				if (localName == "CustomProperties")
				{
					base.CustomProperties.Load(xr);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00023170 File Offset: 0x00021370
		internal static Grouping TryGetGrouping(ModelingReference itemRef, bool useExprName, ValidationContext ctx)
		{
			if (itemRef.ReferenceByName == null)
			{
				throw new InternalModelingException("itemRef is not a by-name reference");
			}
			Grouping grouping = null;
			if (ctx.CurrentQuery != null)
			{
				foreach (Grouping grouping2 in ctx.CurrentQuery.GetAllGroupings())
				{
					string text;
					if (useExprName)
					{
						text = ((grouping2.Expression != null) ? grouping2.Expression.Name : null);
					}
					else
					{
						text = grouping2.Name;
					}
					if (text == itemRef.ReferenceByName)
					{
						grouping = grouping2;
						break;
					}
				}
			}
			if (grouping == null)
			{
				grouping = Grouping.CreateInvalidRefTarget(itemRef.ReferenceByName);
				ctx.SetInvalidRefsFlag();
				ctx.AddMessage(SemanticQuery.CreateInvalidReferenceMessage(itemRef, ctx, ModelingErrorCode.GroupingNotFound, new SRInvalidReferenceMethod(SRErrors.GroupingNotFound), new SRInvalidReferenceMethod(SRErrors.GroupingNotFound_MultipleProperties), ctx.ShouldCheckInvalidRefsDuringTryGet ? Severity.Error : Severity.Warning));
			}
			return grouping;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002325C File Offset: 0x0002145C
		internal static Expression TryGetMeasure(ModelingReference itemRef, ValidationContext ctx)
		{
			if (itemRef.ReferenceByName == null)
			{
				throw new InternalModelingException("itemRef is not a by-name reference");
			}
			Expression expression = null;
			if (ctx.CurrentQuery != null)
			{
				foreach (Expression expression2 in ctx.CurrentQuery.GetAllMeasures())
				{
					if (expression2.Name == itemRef.ReferenceByName)
					{
						expression = expression2;
						break;
					}
				}
			}
			if (expression == null)
			{
				expression = Expression.CreateInvalidRefTarget(itemRef.ReferenceByName);
				ctx.SetInvalidRefsFlag();
				ctx.AddMessage(SemanticQuery.CreateInvalidReferenceMessage(itemRef, ctx, ModelingErrorCode.MeasureNotFound, new SRInvalidReferenceMethod(SRErrors.MeasureNotFound), new SRInvalidReferenceMethod(SRErrors.MeasureNotFound_MultipleProperties), ctx.ShouldCheckInvalidRefsDuringTryGet ? Severity.Error : Severity.Warning));
			}
			return expression;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00023328 File Offset: 0x00021528
		internal static Expression TryGetCalculatedAttribute(ModelingReference itemRef, ValidationContext ctx)
		{
			if (itemRef.ReferenceByName == null)
			{
				throw new InternalModelingException("itemRef is not a by-name reference");
			}
			Expression expression = null;
			if (ctx.CurrentQuery != null)
			{
				expression = ctx.CurrentQuery.CalculatedAttributes[itemRef.ReferenceByName];
			}
			if (expression == null)
			{
				expression = Expression.CreateInvalidRefTarget(itemRef.ReferenceByName);
				ctx.SetInvalidRefsFlag();
				ctx.AddMessage(SemanticQuery.CreateInvalidReferenceMessage(itemRef, ctx, ModelingErrorCode.CalculatedAttributeNotFound, new SRInvalidReferenceMethod(SRErrors.CalculatedAttributeNotFound), new SRInvalidReferenceMethod(SRErrors.CalculatedAttributeNotFound_MultipleProperties), ctx.ShouldCheckInvalidRefsDuringTryGet ? Severity.Error : Severity.Warning));
			}
			return expression;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000233B8 File Offset: 0x000215B8
		internal static Parameter TryGetParameter(ModelingReference itemRef, ValidationContext ctx)
		{
			if (itemRef.ReferenceByName == null)
			{
				throw new InternalModelingException("itemRef is not a by-name reference");
			}
			Parameter parameter = null;
			if (ctx.CurrentQuery != null)
			{
				parameter = ctx.CurrentQuery.Parameters[itemRef.ReferenceByName];
			}
			if (parameter == null)
			{
				parameter = Parameter.CreateInvalidRefTarget(itemRef.ReferenceByName);
				ctx.SetInvalidRefsFlag();
				ctx.AddMessage(SemanticQuery.CreateInvalidReferenceMessage(itemRef, ctx, ModelingErrorCode.ParameterNotFound, new SRInvalidReferenceMethod(SRErrors.ParameterNotFound), new SRInvalidReferenceMethod(SRErrors.ParameterNotFound_MultipleProperties), ctx.ShouldCheckInvalidRefsDuringTryGet ? Severity.Error : Severity.Warning));
			}
			return parameter;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00023448 File Offset: 0x00021648
		internal static Expression LookupResultExpressionChecked(ModelingReference itemRef, ValidationContext ctx)
		{
			if (itemRef.ReferenceByName == null)
			{
				throw new InternalModelingException("itemRef is not a by-name reference");
			}
			if (ctx.CurrentQuery != null)
			{
				foreach (Expression expression in ctx.CurrentQuery.GetAllResultExpressions())
				{
					if (expression.Name == itemRef.ReferenceByName)
					{
						return expression;
					}
				}
			}
			throw new ValidationException(SemanticQuery.CreateInvalidReferenceMessage(itemRef, ctx, ModelingErrorCode.ResultExpressionNotFound, new SRInvalidReferenceMethod(SRErrors.ResultExpressionNotFound), new SRInvalidReferenceMethod(SRErrors.ResultExpressionNotFound_MultipleProperties), Severity.Error));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000234F0 File Offset: 0x000216F0
		private static ValidationMessage CreateInvalidReferenceMessage(ModelingReference itemRef, ValidationContext ctx, ModelingErrorCode code, SRInvalidReferenceMethod singleProperty, SRInvalidReferenceMethod multipleProperties, Severity severity)
		{
			string text;
			if (itemRef.MultipleInScope)
			{
				text = multipleProperties(itemRef.PropertyName, ctx.CurrentObjectDescriptor, itemRef.ReferenceString);
			}
			else
			{
				text = singleProperty(itemRef.PropertyName, ctx.CurrentObjectDescriptor, itemRef.ReferenceString);
			}
			if (severity == Severity.Error)
			{
				return ctx.CreateScopedError(code, text);
			}
			return ctx.CreateScopedWarning(code, text);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00023554 File Offset: 0x00021754
		public void WriteTo(XmlWriter xw)
		{
			this.WriteTo(xw, ModelingSerializationOptions.None);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00023560 File Offset: 0x00021760
		public void WriteTo(XmlWriter xw, ModelingSerializationOptions options)
		{
			if (xw == null)
			{
				throw new ArgumentNullException("xw");
			}
			ModelingXmlWriter modelingXmlWriter = new ModelingXmlWriter(xw, SemanticModelingSchema.Relaxed, options);
			modelingXmlWriter.WriteStartElement("SemanticQuery");
			this.WriteXmlAttributes(modelingXmlWriter);
			this.WriteXmlElements(modelingXmlWriter);
			modelingXmlWriter.WriteEndElement();
			modelingXmlWriter.Writer.Flush();
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000235B2 File Offset: 0x000217B2
		private void WriteXmlAttributes(ModelingXmlWriter xw)
		{
			xw.WriteDefaultNamespace();
			xw.WriteNamespacePrefixes(this.m_namespacePrefixes);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000235C8 File Offset: 0x000217C8
		private void WriteXmlElements(ModelingXmlWriter xw)
		{
			this.m_hierarchies.WriteTo(xw);
			this.m_measureGroups.WriteTo(xw);
			this.m_calculatedAttrs.WriteTo(xw, "CalculatedAttributes");
			this.m_parameters.WriteTo(xw);
			xw.WriteElementIfNonDefault<bool>("EnableDrillthrough", this.m_enableDrillthrough);
			base.WriteCustomProperties(xw);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00023624 File Offset: 0x00021824
		public ValidationMessageCollection Compile(XmlReader xr)
		{
			return this.Compile(xr, QueryCompilationOptions.None, null, null, null, null);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00023645 File Offset: 0x00021845
		public ValidationMessageCollection Compile(XmlReader xr, QueryCompilationOptions options, string userID, string userCulture, DateTime? now, Predicate<ModelItem> includeSecurityFilter)
		{
			return this.Compile(xr, options, null, userID, userCulture, now, includeSecurityFilter);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00023658 File Offset: 0x00021858
		public ValidationMessageCollection Compile(XmlReader xr, QueryCompilationOptions options, IDictionary<string, object> parameterValues, string userID, string userCulture, DateTime? now, Predicate<ModelItem> includeSecurityFilter)
		{
			if (includeSecurityFilter == null && (options & QueryCompilationOptions.Normalize) != QueryCompilationOptions.None)
			{
				throw new ArgumentNullException("includeSecurityFilter");
			}
			if (this.m_model == null || !this.m_model.IsCompiled)
			{
				throw new InvalidOperationException(DevExceptionMessages.SemanticQuery_ModelMustBeCompiled);
			}
			CompilationContext compilationContext = new CompilationContext(true, options, userID, userCulture, now, includeSecurityFilter);
			compilationContext.ParameterValues = new Dictionary<string, object>();
			if (parameterValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in parameterValues)
				{
					compilationContext.ParameterValues.Add(keyValuePair);
				}
			}
			this.Load(xr, compilationContext, SemanticModelingSchema.Strict);
			this.Compile(compilationContext);
			compilationContext.ThrowIfErrors();
			return compilationContext.GetMessages();
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00023718 File Offset: 0x00021918
		private static bool SubsetQuery(SemanticQuery query)
		{
			for (int i = query.CalculatedAttributes.Count - 1; i >= 0; i--)
			{
				if (query.CalculatedAttributes[i].HasInvalidRefs())
				{
					query.CalculatedAttributes[i].SetInvalidRefTarget(true);
					query.CalculatedAttributes.RemoveAt(i);
				}
			}
			for (int j = query.Hierarchies.Count - 1; j >= 0; j--)
			{
				if (!SemanticQuery.SubsetHierarchy(query.Hierarchies[j]))
				{
					return false;
				}
			}
			for (int k = query.MeasureGroups.Count - 1; k >= 0; k--)
			{
				if (!SemanticQuery.SubsetMeasureGroup(query.MeasureGroups[k]))
				{
					query.MeasureGroups.RemoveAt(k);
				}
			}
			return !CollectionUtil.IsEmpty<Expression>(query.GetAllResultExpressions());
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000237E0 File Offset: 0x000219E0
		private static bool SubsetHierarchy(Hierarchy hierarchy)
		{
			if (hierarchy.BaseEntity.IsInvalidRefTarget || (hierarchy.Filter != null && hierarchy.Filter.HasInvalidRefs()))
			{
				return false;
			}
			for (int i = hierarchy.Groupings.Count - 1; i >= 0; i--)
			{
				if (!SemanticQuery.SubsetGrouping(hierarchy.Groupings[i]))
				{
					hierarchy.Groupings.RemoveAt(i);
				}
			}
			return true;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002384C File Offset: 0x00021A4C
		private static bool SubsetGrouping(Grouping grouping)
		{
			if (grouping.Expression.HasInvalidRefs())
			{
				return false;
			}
			for (int i = grouping.Details.Count - 1; i >= 0; i--)
			{
				if (grouping.Details[i].HasInvalidRefs())
				{
					grouping.Details.RemoveAt(i);
				}
			}
			return true;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000238A0 File Offset: 0x00021AA0
		private static bool SubsetMeasureGroup(MeasureGroup measureGroup)
		{
			if (measureGroup.BaseEntity.IsInvalidRefTarget)
			{
				return false;
			}
			for (int i = measureGroup.Measures.Count - 1; i >= 0; i--)
			{
				if (measureGroup.Measures[i].HasInvalidRefs())
				{
					measureGroup.Measures.RemoveAt(i);
				}
			}
			for (int j = measureGroup.SubtotalSets.Count - 1; j >= 0; j--)
			{
				if (!SemanticQuery.SubsetSubtotalSet(measureGroup.SubtotalSets[j]))
				{
					measureGroup.SubtotalSets.RemoveAt(j);
				}
			}
			return measureGroup.Measures.Count > 0;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00023938 File Offset: 0x00021B38
		private static bool SubsetSubtotalSet(SubtotalSet subtotalSet)
		{
			for (int i = subtotalSet.SubtotalMeasures.Count - 1; i >= 0; i--)
			{
				if (subtotalSet.SubtotalMeasures[i].HasInvalidRefs())
				{
					subtotalSet.SubtotalMeasures.RemoveAt(i);
				}
			}
			for (int j = subtotalSet.SubtotalGroupings.Count - 1; j >= 0; j--)
			{
				if (!subtotalSet.SubtotalGroupings[j].IsInvalidRefTarget)
				{
					subtotalSet.SubtotalGroupings.RemoveAt(j);
				}
			}
			return subtotalSet.SubtotalMeasures.Count > 0;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000239C4 File Offset: 0x00021BC4
		public ValidationMessageCollection Validate(bool throwOnError)
		{
			CompilationContext compilationContext = new CompilationContext(false, false);
			this.Compile(compilationContext);
			if (throwOnError)
			{
				compilationContext.ThrowIfErrors();
			}
			return compilationContext.GetMessages();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000239F0 File Offset: 0x00021BF0
		internal void Compile(CompilationContext ctx)
		{
			ctx.PushScope(this);
			try
			{
				this.CompileParameters(ctx);
				base.Compile(ctx.ShouldPersist);
				if (ctx.HasInvalidRefs && ctx.ShouldSubset && !SemanticQuery.SubsetQuery(this))
				{
					ctx.AddScopedError(ModelingErrorCode.EmptySemanticQuery, SRErrors.EmptySemanticQuery(SRObjectDescriptor.FromScope(this)));
					return;
				}
				foreach (MeasureGroup measureGroup in this.m_measureGroups)
				{
					measureGroup.ApplySecurityFilters(ctx);
				}
				if (this.m_model == null)
				{
					throw new InvalidOperationException(DevExceptionMessages.SemanticQuery_NullModel);
				}
				if (this.m_hierarchies.Count > 1 || this.m_measureGroups.Count > 1)
				{
					if (this.m_hierarchies.Count > 1)
					{
						ctx.AddScopedError(ModelingErrorCode.MultipleHierarchies, SRErrors.MultipleHierarchies(ctx.CurrentObjectDescriptor));
					}
					if (this.m_measureGroups.Count > 1)
					{
						ctx.AddScopedError(ModelingErrorCode.MultipleMeasureGroups, SRErrors.MultipleMeasureGroups(ctx.CurrentObjectDescriptor));
					}
					return;
				}
				if (CollectionUtil.IsEmpty<Expression>(this.GetAllResultExpressions()))
				{
					ctx.AddScopedError(ModelingErrorCode.EmptySemanticQuery, SRErrors.EmptySemanticQuery(ctx.CurrentObjectDescriptor));
					return;
				}
				ctx.CheckNameUniqueness<Grouping>(this.GetAllGroupings(), ModelingErrorCode.DuplicateGroupingName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateGroupingName));
				ctx.CheckNameUniqueness<Expression>(this.GetAllTopLevelExpressions(), ModelingErrorCode.DuplicateExpressionName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateExpressionName));
				this.m_calculatedAttrs.Compile(ctx, ExpressionCompilationFlags.CalculatedAttribute);
				this.m_hierarchies.Compile(ctx);
				this.m_measureGroups.Compile(ctx);
				if (ctx.ShouldReplaceParameterRefs)
				{
					this.m_parameters.ClearPostCompile();
				}
			}
			finally
			{
				ctx.PopScope();
			}
			this.m_namespacePrefixes.MakeReadOnly();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00023BD4 File Offset: 0x00021DD4
		private void CompileParameters(CompilationContext ctx)
		{
			this.m_parameters.Compile(ctx);
			if (!ctx.CheckNameUniqueness<Parameter>(this.m_parameters, ModelingErrorCode.DuplicateParameterName, new CompilationContext.SRDuplicateNameMethod(SRErrors.DuplicateParameterName)))
			{
				ctx.ParameterValues = null;
			}
			if (ctx.ParameterValues != null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (Parameter parameter in this.GetAllParameters())
				{
					if (ctx.ParameterValues.ContainsKey(parameter.Name))
					{
						if (parameter.CreateReplacementExpression(ctx) != null)
						{
							dictionary.Add(parameter.Name, ctx.ParameterValues[parameter.Name]);
						}
						ctx.ParameterValues.Remove(parameter.Name);
					}
					else if (!parameter.IsOptional && ctx.ShouldReplaceParameterRefs)
					{
						ctx.AddScopedError(ModelingErrorCode.MissingParameterValue, SRErrors.MissingParameterValue(parameter.Name));
					}
				}
				foreach (string text in ctx.ParameterValues.Keys)
				{
					ctx.AddScopedWarning(ModelingErrorCode.UnusedParameterValue, SRErrors.UnusedParameterValue(ctx.CurrentObjectDescriptor, text));
				}
				ctx.ParameterValues = dictionary;
				if (this.m_enableDrillthrough && dictionary.ContainsKey("DrillthroughSourceQuery") && dictionary.ContainsKey("DrillthroughContext"))
				{
					object obj = dictionary["DrillthroughSourceQuery"];
					object obj2 = dictionary["DrillthroughContext"];
					if (obj != null && obj2 != null)
					{
						DrillthroughContext.FromString(obj.ToString(), obj2.ToString(), this.m_model).Process(this, ctx);
					}
				}
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00023D88 File Offset: 0x00021F88
		internal static void AddWrongSemanticQueryError(CompilationContext ctx, IValidationScope referencedItem, string propertyName, bool multipleInScope)
		{
			string text;
			if (multipleInScope)
			{
				text = SRErrors.WrongSemanticQuery_MultipleProperties(propertyName, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(referencedItem));
			}
			else
			{
				text = SRErrors.WrongSemanticQuery(propertyName, ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(referencedItem));
			}
			ctx.AddScopedError(ModelingErrorCode.WrongSemanticQuery, text);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00023DC9 File Offset: 0x00021FC9
		string IValidationScope.ObjectType
		{
			get
			{
				return "SemanticQuery";
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00023DD0 File Offset: 0x00021FD0
		string IValidationScope.ObjectID
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00023DD7 File Offset: 0x00021FD7
		string IValidationScope.ObjectName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000480 RID: 1152
		internal const string SemanticQueryElem = "SemanticQuery";

		// Token: 0x04000481 RID: 1153
		private const string HierarchiesElem = "Hierarchies";

		// Token: 0x04000482 RID: 1154
		private const string MeasureGroupsElem = "MeasureGroups";

		// Token: 0x04000483 RID: 1155
		private const string CalculatedAttributesElem = "CalculatedAttributes";

		// Token: 0x04000484 RID: 1156
		private const string ParametersElem = "Parameters";

		// Token: 0x04000485 RID: 1157
		private const string EnableDrillthroughElem = "EnableDrillthrough";

		// Token: 0x04000486 RID: 1158
		private SemanticModel m_model;

		// Token: 0x04000487 RID: 1159
		private readonly SemanticQuery.HierarchyCollection m_hierarchies;

		// Token: 0x04000488 RID: 1160
		private readonly SemanticQuery.MeasureGroupCollection m_measureGroups;

		// Token: 0x04000489 RID: 1161
		private readonly ExpressionCollection m_calculatedAttrs;

		// Token: 0x0400048A RID: 1162
		private readonly SemanticQuery.ParameterCollection m_parameters;

		// Token: 0x0400048B RID: 1163
		private bool m_enableDrillthrough;

		// Token: 0x0400048C RID: 1164
		private readonly XmlNamespacePrefixCollection m_namespacePrefixes;

		// Token: 0x020001B2 RID: 434
		public sealed class HierarchyCollection : OwnedCollection<Hierarchy, SemanticQuery>, IXmlLoadable
		{
			// Token: 0x060010E6 RID: 4326 RVA: 0x000350B4 File Offset: 0x000332B4
			internal HierarchyCollection(SemanticQuery owner)
				: base(owner)
			{
			}

			// Token: 0x060010E7 RID: 4327 RVA: 0x000350BD File Offset: 0x000332BD
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject("Hierarchies", this);
			}

			// Token: 0x060010E8 RID: 4328 RVA: 0x000350D1 File Offset: 0x000332D1
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010E9 RID: 4329 RVA: 0x000350D4 File Offset: 0x000332D4
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Hierarchy")
				{
					Hierarchy hierarchy = new Hierarchy();
					hierarchy.Load(xr);
					base.Add(hierarchy);
					return true;
				}
				return false;
			}

			// Token: 0x060010EA RID: 4330 RVA: 0x00035112 File Offset: 0x00033312
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<Hierarchy>("Hierarchies", this);
			}

			// Token: 0x060010EB RID: 4331 RVA: 0x00035120 File Offset: 0x00033320
			internal void Compile(CompilationContext ctx)
			{
				CheckedCollection<Hierarchy>.CompileItems<Hierarchy>(this, ctx);
			}
		}

		// Token: 0x020001B3 RID: 435
		public sealed class MeasureGroupCollection : OwnedCollection<MeasureGroup, SemanticQuery>, IXmlLoadable
		{
			// Token: 0x060010EC RID: 4332 RVA: 0x00035129 File Offset: 0x00033329
			internal MeasureGroupCollection(SemanticQuery owner)
				: base(owner)
			{
			}

			// Token: 0x060010ED RID: 4333 RVA: 0x00035132 File Offset: 0x00033332
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject("MeasureGroups", this);
			}

			// Token: 0x060010EE RID: 4334 RVA: 0x00035146 File Offset: 0x00033346
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010EF RID: 4335 RVA: 0x0003514C File Offset: 0x0003334C
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "MeasureGroup")
				{
					MeasureGroup measureGroup = new MeasureGroup();
					measureGroup.Load(xr);
					base.Add(measureGroup);
					return true;
				}
				return false;
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x0003518A File Offset: 0x0003338A
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<MeasureGroup>("MeasureGroups", this);
			}

			// Token: 0x060010F1 RID: 4337 RVA: 0x00035198 File Offset: 0x00033398
			internal void Compile(CompilationContext ctx)
			{
				CheckedCollection<MeasureGroup>.CompileItems<MeasureGroup>(this, ctx);
			}
		}

		// Token: 0x020001B4 RID: 436
		public sealed class ParameterCollection : CheckedCollection<Parameter>, IXmlLoadable
		{
			// Token: 0x060010F2 RID: 4338 RVA: 0x000351A1 File Offset: 0x000333A1
			internal ParameterCollection()
			{
			}

			// Token: 0x170003EE RID: 1006
			public Parameter this[string name]
			{
				get
				{
					return base.Items.Find((Parameter p) => p.Name == name);
				}
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x000351DD File Offset: 0x000333DD
			internal void ClearPostCompile()
			{
				if (!this.IsReadOnly)
				{
					throw new InternalModelingException("ClearPostCompile called on uncompiled collection");
				}
				base.Items.Clear();
			}

			// Token: 0x060010F5 RID: 4341 RVA: 0x000351FD File Offset: 0x000333FD
			internal void Load(ModelingXmlReader xr)
			{
				base.CheckWriteable();
				xr.LoadObject("Parameters", this);
			}

			// Token: 0x060010F6 RID: 4342 RVA: 0x00035211 File Offset: 0x00033411
			bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
			{
				return false;
			}

			// Token: 0x060010F7 RID: 4343 RVA: 0x00035214 File Offset: 0x00033414
			bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.IsDefaultNamespace && xr.LocalName == "Parameter")
				{
					Parameter parameter = new Parameter();
					parameter.Load(xr);
					base.Add(parameter);
					return true;
				}
				return false;
			}

			// Token: 0x060010F8 RID: 4344 RVA: 0x00035252 File Offset: 0x00033452
			internal void WriteTo(ModelingXmlWriter xw)
			{
				xw.WriteCollectionElement<Parameter>("Parameters", this);
			}

			// Token: 0x060010F9 RID: 4345 RVA: 0x00035260 File Offset: 0x00033460
			internal void Compile(CompilationContext ctx)
			{
				CheckedCollection<Parameter>.CompileItems<Parameter>(this, ctx);
			}
		}
	}
}
