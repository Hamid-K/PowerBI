using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200009D RID: 157
	public sealed class Expression : ExtensibleModelingObject, ICloneable, IQueryAttribute, IValidationScope, IQueryAttributeInternal, IPersistable, IXmlLoadable, IXmlWriteable, ILazyCloneable<Expression>
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x00018C4A File Offset: 0x00016E4A
		public Expression()
			: this(null, null)
		{
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00018C54 File Offset: 0x00016E54
		public Expression(ExpressionNode node)
			: this(node, null)
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00018C5E File Offset: 0x00016E5E
		public Expression(ExpressionNode node, ExpressionPath path)
			: this(node, path, true)
		{
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00018C69 File Offset: 0x00016E69
		public static Expression CreateInvalidRefTarget(string name)
		{
			return new Expression
			{
				Name = name,
				m_invalidRefTarget = true
			};
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00018C80 File Offset: 0x00016E80
		private Expression(ExpressionNode node, ExpressionPath path, bool clonePath)
		{
			this.m_name = string.Empty;
			base..ctor();
			if (path == null)
			{
				this.m_path = new ExpressionPath();
			}
			else if (clonePath)
			{
				this.m_path = path.Clone();
			}
			else
			{
				this.m_path = path;
			}
			this.m_node = node;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00018CD0 File Offset: 0x00016ED0
		private Expression(Expression exprToCopy, ExpressionCopyManager copyManager)
		{
			this.m_name = string.Empty;
			base..ctor(exprToCopy);
			this.m_path = exprToCopy.Path.Clone(copyManager);
			this.m_node = ((exprToCopy.Node != null) ? exprToCopy.Node.Clone(copyManager) : null);
			this.m_name = exprToCopy.Name;
			this.m_invalidRefTarget = exprToCopy.IsInvalidRefTarget;
			this.m_skipSecurityFilters = exprToCopy.SkipSecurityFilters;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00018D42 File Offset: 0x00016F42
		ModelAttribute IQueryAttribute.ModelAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00018D45 File Offset: 0x00016F45
		Expression IQueryAttribute.CalculatedAttribute
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00018D48 File Offset: 0x00016F48
		bool IQueryAttributeInternal.ReplaceWithExpression
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00018D4B File Offset: 0x00016F4B
		bool IQueryAttribute.IsAnchored()
		{
			return this.IsSubtreeAnchored();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00018D53 File Offset: 0x00016F53
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00018D5B File Offset: 0x00016F5B
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_name;
			}
			set
			{
				base.CheckWriteable();
				this.m_name = value ?? string.Empty;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00018D73 File Offset: 0x00016F73
		public ExpressionPath Path
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_path;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00018D7B File Offset: 0x00016F7B
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00018D83 File Offset: 0x00016F83
		public ExpressionNode Node
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node;
			}
			set
			{
				base.CheckWriteable();
				this.m_node = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00018D92 File Offset: 0x00016F92
		public FunctionNode NodeAsFunction
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node as FunctionNode;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00018D9F File Offset: 0x00016F9F
		public AttributeRefNode NodeAsAttributeRef
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node as AttributeRefNode;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00018DAC File Offset: 0x00016FAC
		public EntityRefNode NodeAsEntityRef
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node as EntityRefNode;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00018DB9 File Offset: 0x00016FB9
		public ParameterRefNode NodeAsParameterRef
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node as ParameterRefNode;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00018DC6 File Offset: 0x00016FC6
		public LiteralNode NodeAsLiteral
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node as LiteralNode;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00018DD3 File Offset: 0x00016FD3
		public NullNode NodeAsNull
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_node as NullNode;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00018DE0 File Offset: 0x00016FE0
		IQueryEntity IQueryAttributeInternal.SourceEntity
		{
			get
			{
				if (!this.m_path.IsEmpty)
				{
					return this.m_path.SourceEntity;
				}
				if (this.m_node != null)
				{
					return this.m_node.SourceEntity;
				}
				return null;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00018E10 File Offset: 0x00017010
		public bool IsSubtreeAnchored()
		{
			return this.m_path.SourceEntity != null || (this.m_node != null && this.m_node.IsSubtreeAnchored());
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00018E36 File Offset: 0x00017036
		public bool IsInvalidRefTarget
		{
			get
			{
				return this.m_invalidRefTarget;
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00018E3E File Offset: 0x0001703E
		public void SetInvalidRefTarget(bool value)
		{
			base.CheckWriteable();
			this.m_invalidRefTarget = value;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00018E4D File Offset: 0x0001704D
		public bool HasInvalidRefs()
		{
			return this.HasInvalidRefs(new Bag<Expression>());
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00018E5C File Offset: 0x0001705C
		internal bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			if (processedExpressions.Contains(this))
			{
				return false;
			}
			processedExpressions.Add(this);
			bool flag = this.m_path.HasInvalidRefs() || (this.m_node != null && this.m_node.HasInvalidRefs(processedExpressions));
			processedExpressions.Remove(this);
			return flag;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00018EAA File Offset: 0x000170AA
		internal bool SkipSecurityFilters
		{
			get
			{
				return this.m_skipSecurityFilters;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00018EB2 File Offset: 0x000170B2
		internal void MarkAsSkipSecurityFilters()
		{
			base.CheckWriteable();
			this.m_skipSecurityFilters = true;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00018EC1 File Offset: 0x000170C1
		public Expression Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00018ECA File Offset: 0x000170CA
		public Expression Clone(ExpressionCopyManager copyManager)
		{
			return new Expression(this, copyManager);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00018ED3 File Offset: 0x000170D3
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00018EDC File Offset: 0x000170DC
		public bool IsSameAs(Expression other)
		{
			return this.m_invalidRefTarget == other.m_invalidRefTarget && this.m_skipSecurityFilters == other.m_skipSecurityFilters && this.m_path.IsSameAs(other.Path) && this.m_node != null && this.m_node.IsSameAs(other.Node);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00018F34 File Offset: 0x00017134
		public ResultType GetResultType()
		{
			ResultType? resultType = this.TryGetResultType();
			if (resultType == null)
			{
				throw new InvalidOperationException();
			}
			return resultType.Value;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00018F5E File Offset: 0x0001715E
		public ResultType? TryGetResultType()
		{
			return this.TryGetResultType(false);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00018F67 File Offset: 0x00017167
		public ResultType? TryGetResultType(bool checkInvalidRefs)
		{
			if (base.IsCompiled)
			{
				return this.m_compiledResultType;
			}
			return this.CompileCore(new CompilationContext(false, checkInvalidRefs, this), ExpressionCompilationFlags.None);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00018F8C File Offset: 0x0001718C
		internal void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("SetEntityKeyTarget is called on a non-compiled expression.");
			}
			ResultType value = this.m_compiledResultType.Value;
			value.SetEntityKeyTarget(entityKeyTarget);
			this.m_compiledResultType = new ResultType?(value);
			this.m_node.SetEntityKeyTarget(entityKeyTarget, ctx);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00018FDC File Offset: 0x000171DC
		public ExpressionPath GetCommonFloatPath()
		{
			if (this.m_node == null || this.IsSubtreeAnchored())
			{
				throw new InvalidOperationException();
			}
			ExpressionPath commonPath = null;
			this.m_node.VisitAggregationFloatPoints(delegate(Expression expression, bool allowExprModification)
			{
				if (commonPath == null)
				{
					commonPath = expression.Path.Clone();
				}
				else
				{
					commonPath.TrimToMatchingSegment(expression.Path);
				}
				return null;
			}, false);
			return commonPath ?? ExpressionPath.Empty;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00019034 File Offset: 0x00017234
		public void Float(ExpressionPath floatBy)
		{
			if (floatBy == null)
			{
				throw new ArgumentNullException("floatBy");
			}
			if (this.m_node == null || this.IsSubtreeAnchored())
			{
				throw new InvalidOperationException();
			}
			base.CheckWriteable();
			this.CopyExpressionFrom(this.m_node.VisitAggregationFloatPoints(delegate(Expression expression, bool allowExprModification)
			{
				if (!allowExprModification)
				{
					throw new InternalModelingException("allowExprModification must be true for Float operation.");
				}
				expression.Path.InsertRange(0, floatBy);
				return null;
			}, true));
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001909C File Offset: 0x0001729C
		private void CopyExpressionFrom(Expression replacementExpr)
		{
			if (replacementExpr != null)
			{
				this.m_path.ReplaceWith(replacementExpr.Path);
				this.m_node = replacementExpr.Node;
			}
			if (this.m_path.IsEmpty)
			{
				FunctionNode nodeAsFunction = this.NodeAsFunction;
				if (nodeAsFunction != null && nodeAsFunction.FunctionName == FunctionName.Aggregate && nodeAsFunction.Arguments.Count == 1 && nodeAsFunction.Arguments[0].Path.IsEmpty)
				{
					ResultType? resultType = nodeAsFunction.Arguments[0].TryGetResultType();
					if (resultType != null && resultType.Value.Cardinality == Cardinality.One)
					{
						this.m_node = nodeAsFunction.Arguments[0].Node;
					}
				}
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00019154 File Offset: 0x00017354
		public void Sink(ExpressionPath sinkBy)
		{
			if (sinkBy == null)
			{
				throw new ArgumentNullException("sinkBy");
			}
			if (this.m_node == null || this.IsSubtreeAnchored())
			{
				throw new InvalidOperationException();
			}
			base.CheckWriteable();
			this.CopyExpressionFrom(this.m_node.VisitAggregationFloatPoints(delegate(Expression expression, bool allowExprModification)
			{
				if (!allowExprModification)
				{
					throw new InternalModelingException("allowExprModification must be true for Sink operation.");
				}
				if (!expression.Path.StartsWith(sinkBy))
				{
					throw new InvalidOperationException();
				}
				expression.Path.RemoveRange(0, sinkBy.Length);
				return null;
			}, true));
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000191BB File Offset: 0x000173BB
		public ExpressionPath GetMaxDrillablePath()
		{
			return DrillthroughContext.CalculateMaxDrillablePathAlgorithm.Calculate(new Expression[] { this });
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000191CC File Offset: 0x000173CC
		public override string ToString()
		{
			string text;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				ModelingXmlWriter modelingXmlWriter = new ModelingXmlWriter(xmlTextWriter, SemanticModelingSchema.Fragment, ModelingSerializationOptions.NameComments);
				this.WriteTo(modelingXmlWriter);
				xmlTextWriter.Flush();
				text = stringWriter.ToString();
			}
			catch
			{
				text = base.ToString();
			}
			return text;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001922C File Offset: 0x0001742C
		internal void Load(ModelingXmlReader xr, bool named)
		{
			base.CheckWriteable();
			xr.CheckElement("Expression");
			if (named)
			{
				xr.Validation.PushScope(this);
				try
				{
					xr.LoadObject(this);
					return;
				}
				finally
				{
					xr.Validation.PopScope();
				}
			}
			xr.LoadObject(this);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00019288 File Offset: 0x00017488
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "Name")
			{
				this.Name = xr.ReadValueAsString();
				return true;
			}
			return false;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000192B4 File Offset: 0x000174B4
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName != null)
				{
					int length = localName.Length;
					switch (length)
					{
					case 4:
					{
						char c = localName[0];
						if (c != 'N')
						{
							if (c == 'P')
							{
								if (localName == "Path")
								{
									this.Path.Load(xr);
									return true;
								}
							}
						}
						else if (localName == "Null")
						{
							this.LoadExpressionNode(xr, new NullNode());
							return true;
						}
						break;
					}
					case 5:
					case 6:
					case 10:
					case 11:
						break;
					case 7:
						if (localName == "Literal")
						{
							this.LoadExpressionNode(xr, new LiteralNode());
							return true;
						}
						break;
					case 8:
						if (localName == "Function")
						{
							this.LoadExpressionNode(xr, new FunctionNode());
							return true;
						}
						break;
					case 9:
						if (localName == "EntityRef")
						{
							this.LoadExpressionNode(xr, new EntityRefNode());
							return true;
						}
						break;
					case 12:
					{
						char c = localName[0];
						if (c != 'A')
						{
							if (c == 'P')
							{
								if (localName == "ParameterRef")
								{
									this.LoadExpressionNode(xr, new ParameterRefNode());
									return true;
								}
							}
						}
						else if (localName == "AttributeRef")
						{
							this.LoadExpressionNode(xr, new AttributeRefNode());
							return true;
						}
						break;
					}
					default:
						if (length == 16)
						{
							if (localName == "CustomProperties")
							{
								base.CustomProperties.Load(xr);
								return true;
							}
						}
						break;
					}
				}
			}
			return false;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00019448 File Offset: 0x00017648
		private void LoadExpressionNode(ModelingXmlReader xr, ExpressionNode node)
		{
			if (this.Node != null)
			{
				this.AddInvalidExpressionError(xr.Validation);
				xr.Skip();
				return;
			}
			node.Load(xr, xr.Validation.CurrentScope == this);
			this.Node = node;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00019484 File Offset: 0x00017684
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Expression");
			xw.WriteAttributeIfNonDefault<string>("Name", this.m_name);
			this.m_path.WriteTo(xw);
			if (this.m_node != null)
			{
				this.m_node.WriteTo(xw);
			}
			base.WriteCustomProperties(xw);
			xw.WriteEndElement();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000194DA File Offset: 0x000176DA
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000194E4 File Offset: 0x000176E4
		public ValidationMessageCollection Validate(IQueryEntity contextEntity, ExpressionCompilationFlags flags, bool throwOnError)
		{
			CompilationContext compilationContext = new CompilationContext(false, false, this);
			compilationContext.PushContextEntity(contextEntity);
			try
			{
				this.Compile(compilationContext, flags);
			}
			finally
			{
				compilationContext.PopContextEntity();
			}
			if (throwOnError)
			{
				compilationContext.ThrowIfErrors();
			}
			return compilationContext.GetMessages();
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00019534 File Offset: 0x00017734
		internal ResultType? Compile(CompilationContext ctx, ExpressionCompilationFlags flags)
		{
			ResultType? resultType;
			if (!ctx.PushUniqueExpression(this))
			{
				string text;
				if (ctx.CurrentScope is Expression)
				{
					text = SRErrors.CyclicExpression_ExpressionObject(ctx.CurrentObjectDescriptor);
				}
				else
				{
					text = SRErrors.CyclicExpression(ctx.CurrentObjectDescriptor);
				}
				ctx.AddScopedError(ModelingErrorCode.CyclicExpression, text);
				resultType = null;
				return resultType;
			}
			try
			{
				if (flags.AllowPrecompiled && base.IsCompiled)
				{
					resultType = this.m_compiledResultType;
				}
				else
				{
					bool flag = false;
					bool flag2 = false;
					try
					{
						if (flags.Named)
						{
							if (this.m_name.Length == 0)
							{
								ctx.AddScopedError(ModelingErrorCode.MissingExpressionName, SRErrors.MissingExpressionName(ctx.CurrentObjectDescriptor));
							}
							ctx.PushScope(this);
							flag = true;
						}
						if (flags.IsCalculatedAttribute)
						{
							ctx.PushContextEntity(null);
							flag2 = true;
						}
						ResultType? resultType2 = this.CompileCore(ctx, flags);
						if (resultType2 != null && !this.ProcessCompilationFlags(ctx, flags, resultType2.Value))
						{
							resultType = null;
						}
						else
						{
							resultType = resultType2;
						}
					}
					finally
					{
						if (flag2)
						{
							ctx.PopContextEntity();
						}
						if (flag)
						{
							ctx.PopScope();
						}
					}
				}
			}
			finally
			{
				ctx.PopUniqueExpression();
			}
			return resultType;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00019658 File Offset: 0x00017858
		private ResultType? CompileCore(CompilationContext ctx, ExpressionCompilationFlags flags)
		{
			base.Compile(ctx.ShouldPersist);
			ResultType resultType = default(ResultType);
			IQueryEntity queryEntity;
			IQueryEntity queryEntity2;
			ExpressionPath expressionPath = this.CompilePath(ctx, out queryEntity, out queryEntity2);
			if (expressionPath == null)
			{
				ResultType? resultType2 = null;
				return resultType2;
			}
			if (this.ApplySecurityFilters(ctx, flags))
			{
				expressionPath = this.CompilePath(ctx, out queryEntity, out queryEntity2);
				if (expressionPath == null)
				{
					throw new InternalModelingException("Failed to compile expression path after applying security filters.");
				}
			}
			ctx.PushContextEntity(queryEntity2);
			try
			{
				if (this.m_node == null)
				{
					this.AddInvalidExpressionError(ctx);
					ResultType? resultType2 = null;
					return resultType2;
				}
				Expression expression;
				ResultType? resultType3 = this.m_node.Compile(ctx, ctx.CurrentScope == this, out expression);
				if (resultType3 == null)
				{
					return null;
				}
				if (expression != null)
				{
					if (!expression.IsCompiled)
					{
						throw new InternalModelingException("replacementExpr must be compiled");
					}
					this.m_path.AddRangeInternal(expression.Path);
					this.m_node = expression.Node;
				}
				resultType = resultType3.Value;
				if (expressionPath.GetCardinality() == Cardinality.Many)
				{
					resultType.Cardinality = Cardinality.Many;
				}
				if (expressionPath.GetOptionality() == Optionality.Optional)
				{
					resultType.Nullable = true;
				}
			}
			finally
			{
				ctx.PopContextEntity();
			}
			if (ctx.ShouldPersist)
			{
				this.m_compiledResultType = new ResultType?(resultType);
			}
			return new ResultType?(resultType);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000197A8 File Offset: 0x000179A8
		private ExpressionPath CompilePath(CompilationContext ctx, out IQueryEntity desiredTargetEntity, out IQueryEntity actualTargetEntity)
		{
			desiredTargetEntity = ((this.m_node != null) ? this.m_node.SourceEntity : null);
			return this.m_path.Compile(ctx, desiredTargetEntity, out actualTargetEntity);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000197D4 File Offset: 0x000179D4
		private bool ApplySecurityFilters(CompilationContext ctx, ExpressionCompilationFlags flags)
		{
			if (!ctx.ShouldApplySecurityFilters || flags.IsGroupingExpression)
			{
				return false;
			}
			ExpressionPath expressionPath;
			ExpressionPath expressionPath2;
			Expression expression = this.m_path.TryGetFirstSecurityFilterCondition(ctx, out expressionPath, out expressionPath2);
			if (expression != null)
			{
				this.m_path = expressionPath;
				Expression expression2 = new Expression(this.m_node, expressionPath2);
				expression = expression.Clone();
				if (ctx.IsInSetArgumentContext())
				{
					this.m_node = new FunctionNode(FunctionName.Filter, new Expression[] { expression2, expression });
				}
				else
				{
					this.m_node = new FunctionNode(FunctionName.If, new Expression[]
					{
						expression,
						expression2,
						new Expression(new NullNode())
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00019874 File Offset: 0x00017A74
		private bool ProcessCompilationFlags(CompilationContext ctx, ExpressionCompilationFlags flags, ResultType result)
		{
			bool flag = true;
			if (flags.QueryResult)
			{
				if (ctx.ContextEntity == null)
				{
					throw new InternalModelingException("ContextEntity is null in Expression.ProcessCompilationFlags(flags.QueryResult)");
				}
				if (result.Cardinality == Cardinality.Many)
				{
					ctx.AddScopedError(ModelingErrorCode.TopLevelSetExpression, SRErrors.TopLevelSetExpression(ctx.CurrentObjectDescriptor, ctx.ContextEntityDescriptor));
					flag = false;
				}
			}
			if (flags.MustFloat && this.IsSubtreeAnchored())
			{
				if (flags.IsModelAttribute)
				{
					ctx.AddScopedError(ModelingErrorCode.NonAggregateExpression, SRErrors.NonAggregateExpression_Attribute(ctx.CurrentObjectDescriptor));
				}
				else
				{
					if (!flags.IsMeasure)
					{
						throw new InternalModelingException("Unknown flags combination with flags.MustFloat");
					}
					ctx.AddScopedError(ModelingErrorCode.NonAggregateExpression, SRErrors.NonAggregateExpression_Measure(ctx.CurrentObjectDescriptor));
				}
				flag = false;
			}
			if (flags.IsFilter)
			{
				if (result.DataType != DataType.Boolean && result.DataType != DataType.Null)
				{
					ctx.AddScopedError(ModelingErrorCode.InvalidFilter, SRErrors.InvalidFilter(ctx.CurrentObjectDescriptor, result.DataType));
					flag = false;
				}
				if (result.Cardinality == Cardinality.Many)
				{
					ctx.AddScopedError(ModelingErrorCode.TopLevelSetExpression, SRErrors.TopLevelSetExpression_Filter(ctx.CurrentObjectDescriptor, ctx.ContextEntityDescriptor));
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001997B File Offset: 0x00017B7B
		private void AddInvalidExpressionError(ValidationContext ctx)
		{
			if (ctx.CurrentScope == this)
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidExpression, SRErrors.InvalidExpression_TopLevel(ctx.CurrentObjectDescriptor));
				return;
			}
			ctx.AddScopedError(ModelingErrorCode.InvalidExpression, SRErrors.InvalidExpression(ctx.CurrentObjectDescriptor));
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000199B0 File Offset: 0x00017BB0
		bool IQueryAttributeInternal.CheckReference(CompilationContext ctx, string propertyName, bool multipleInScope)
		{
			return this.Compile(ctx, ExpressionCompilationFlags.CalculatedAttribute) != null;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000199D1 File Offset: 0x00017BD1
		string IValidationScope.ObjectType
		{
			get
			{
				return "Expression";
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x000199D8 File Offset: 0x00017BD8
		string IValidationScope.ObjectID
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x000199E0 File Offset: 0x00017BE0
		string IValidationScope.ObjectName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000199E8 File Offset: 0x00017BE8
		internal Expression CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("Expression is not compiled");
			}
			ExpressionPath expressionPath = this.m_path.CloneFor(newModel);
			if (expressionPath == null)
			{
				return null;
			}
			ExpressionNode expressionNode = this.m_node.CloneFor(newModel);
			if (expressionNode == null)
			{
				return null;
			}
			Expression expression = new Expression(expressionNode, expressionPath, false);
			expression.Name = this.m_name;
			expression.CustomProperties = base.CustomProperties;
			expression.m_compiledResultType = this.m_compiledResultType;
			expression.SetCompiledIndicator();
			return expression;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00019A5E File Offset: 0x00017C5E
		Expression ILazyCloneable<Expression>.CloneFor(SemanticModel newModel)
		{
			return this.CloneFor(newModel);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00019A67 File Offset: 0x00017C67
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00019A70 File Offset: 0x00017C70
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Expression.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					switch (memberName)
					{
					case MemberName.Path:
						((IPersistable)this.m_path).Serialize(writer);
						break;
					case MemberName.Node:
						writer.Write(this.m_node);
						break;
					case MemberName.InvalidTargetRef:
						writer.Write(this.m_invalidRefTarget);
						break;
					case MemberName.SkipSecurityFilter:
						writer.Write(this.m_skipSecurityFilters);
						break;
					case MemberName.CompiledResultType:
					{
						Expression.ResultTypePersistable resultTypePersistable = ((this.m_compiledResultType != null) ? new Expression.ResultTypePersistable(this.m_compiledResultType.Value) : null);
						writer.Write(resultTypePersistable);
						break;
					}
					default:
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
				}
				else
				{
					writer.Write(this.m_name);
				}
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00019B76 File Offset: 0x00017D76
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00019B80 File Offset: 0x00017D80
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Expression.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					switch (memberName)
					{
					case MemberName.Path:
						((IPersistable)this.m_path).Deserialize(reader);
						break;
					case MemberName.Node:
						this.m_node = (ExpressionNode)reader.ReadRIFObject();
						break;
					case MemberName.InvalidTargetRef:
						this.m_invalidRefTarget = reader.ReadBoolean();
						break;
					case MemberName.SkipSecurityFilter:
						this.m_skipSecurityFilters = reader.ReadBoolean();
						break;
					case MemberName.CompiledResultType:
					{
						Expression.ResultTypePersistable resultTypePersistable = (Expression.ResultTypePersistable)reader.ReadRIFObject();
						if (resultTypePersistable != null)
						{
							resultTypePersistable.SetOwner(this);
							this.m_compiledResultType = new ResultType?(resultTypePersistable.GetResultType());
						}
						break;
					}
					default:
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
				}
				else
				{
					this.m_name = reader.ReadString();
				}
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019C8A File Offset: 0x00017E8A
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019C96 File Offset: 0x00017E96
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.Expression;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00019C9A File Offset: 0x00017E9A
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref Expression.__declaration, Expression.__declarationLock, () => new Declaration(ObjectType.Expression, ObjectType.ExtensibleModelingObject, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Path, ObjectType.ExpressionPath),
					new MemberInfo(MemberName.Node, ObjectType.ExpressionNode),
					new MemberInfo(MemberName.Name, Token.String),
					new MemberInfo(MemberName.InvalidTargetRef, Token.Boolean),
					new MemberInfo(MemberName.SkipSecurityFilter, Token.Boolean),
					new MemberInfo(MemberName.CompiledResultType, ObjectType.ResultTypePersistable)
				}));
			}
		}

		// Token: 0x04000397 RID: 919
		internal const string ExpressionElem = "Expression";

		// Token: 0x04000398 RID: 920
		private const string NameAttr = "Name";

		// Token: 0x04000399 RID: 921
		private ExpressionPath m_path;

		// Token: 0x0400039A RID: 922
		private ExpressionNode m_node;

		// Token: 0x0400039B RID: 923
		private string m_name;

		// Token: 0x0400039C RID: 924
		private bool m_invalidRefTarget;

		// Token: 0x0400039D RID: 925
		private bool m_skipSecurityFilters;

		// Token: 0x0400039E RID: 926
		private ResultType? m_compiledResultType;

		// Token: 0x0400039F RID: 927
		private static Declaration __declaration;

		// Token: 0x040003A0 RID: 928
		private static readonly object __declarationLock = new object();

		// Token: 0x02000191 RID: 401
		internal sealed class ResultTypePersistable : IPersistable
		{
			// Token: 0x06001073 RID: 4211 RVA: 0x00033D14 File Offset: 0x00031F14
			internal ResultTypePersistable()
			{
			}

			// Token: 0x06001074 RID: 4212 RVA: 0x00033D1C File Offset: 0x00031F1C
			internal ResultTypePersistable(ResultType resultType)
				: this()
			{
				this.m_resultType = resultType;
			}

			// Token: 0x06001075 RID: 4213 RVA: 0x00033D2B File Offset: 0x00031F2B
			internal ResultType GetResultType()
			{
				return this.m_resultType;
			}

			// Token: 0x06001076 RID: 4214 RVA: 0x00033D33 File Offset: 0x00031F33
			internal void SetOwner(Expression owner)
			{
				this.m_owner = owner;
			}

			// Token: 0x06001077 RID: 4215 RVA: 0x00033D3C File Offset: 0x00031F3C
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(Expression.ResultTypePersistable.Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.DataType)
					{
						switch (memberName)
						{
						case MemberName.Cardinality:
							writer.Write((byte)this.m_resultType.Cardinality);
							break;
						case MemberName.Nullable:
							writer.Write(this.m_resultType.Nullable);
							break;
						case MemberName.EntityKeyTarget:
							PersistenceHelper.WriteIQueryEntityReference(ref writer, this.m_resultType.EntityKeyTarget);
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						writer.Write((byte)this.m_resultType.DataType);
					}
				}
			}

			// Token: 0x06001078 RID: 4216 RVA: 0x00033E08 File Offset: 0x00032008
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				this.Deserialize(reader);
			}

			// Token: 0x06001079 RID: 4217 RVA: 0x00033E14 File Offset: 0x00032014
			internal void Deserialize(IntermediateFormatReader reader)
			{
				DataType? dataType = null;
				Cardinality? cardinality = null;
				bool? flag = null;
				IQueryEntity queryEntity = null;
				reader.RegisterDeclaration(Expression.ResultTypePersistable.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.DataType)
					{
						switch (memberName)
						{
						case MemberName.Cardinality:
							cardinality = new Cardinality?((Cardinality)reader.ReadByte());
							break;
						case MemberName.Nullable:
							flag = new bool?(reader.ReadBoolean());
							break;
						case MemberName.EntityKeyTarget:
							queryEntity = PersistenceHelper.ReadIQueryEntityReference(ref reader, this);
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						dataType = new DataType?((DataType)reader.ReadByte());
					}
				}
				if (dataType == null)
				{
					throw new InternalModelingException("Can not complete ResultType deserialization: DataType is not deserialized.");
				}
				if (cardinality == null)
				{
					throw new InternalModelingException("Can not complete ResultType deserialization: Cardinality is not deserialized.");
				}
				if (flag == null)
				{
					throw new InternalModelingException("Can not complete ResultType deserialization: Nullable is not deserialized.");
				}
				this.m_resultType = new ResultType(dataType.Value, cardinality.Value, flag.Value);
				if (queryEntity != null)
				{
					this.m_resultType.SetEntityKeyTarget(queryEntity);
				}
			}

			// Token: 0x0600107A RID: 4218 RVA: 0x00033F54 File Offset: 0x00032154
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				List<MemberReference> list;
				if (memberReferencesCollection.TryGetValue(Expression.ResultTypePersistable.Declaration.ObjectType, out list))
				{
					foreach (MemberReference memberReference in list)
					{
						if (memberReference.MemberName != MemberName.EntityKeyTarget)
						{
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
						IQueryEntity queryEntity = PersistenceHelper.ResolveIQueryEntityReference(referenceableItems[memberReference.RefID]);
						this.m_owner.m_compiledResultType.Value.SetEntityKeyTarget(queryEntity);
					}
				}
			}

			// Token: 0x0600107B RID: 4219 RVA: 0x00034010 File Offset: 0x00032210
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.ResultTypePersistable;
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x0600107C RID: 4220 RVA: 0x00034014 File Offset: 0x00032214
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref Expression.ResultTypePersistable.__declaration, Expression.ResultTypePersistable.__declarationLock, () => new Declaration(ObjectType.ResultTypePersistable, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.DataType, Token.Byte),
						new MemberInfo(MemberName.Cardinality, Token.Byte),
						new MemberInfo(MemberName.Nullable, Token.Boolean),
						new MemberInfo(MemberName.EntityKeyTarget, ObjectType.ModelingObject, Token.Reference)
					}));
				}
			}

			// Token: 0x040006C8 RID: 1736
			private Expression m_owner;

			// Token: 0x040006C9 RID: 1737
			private ResultType m_resultType;

			// Token: 0x040006CA RID: 1738
			private static Declaration __declaration;

			// Token: 0x040006CB RID: 1739
			private static readonly object __declarationLock = new object();
		}
	}
}
