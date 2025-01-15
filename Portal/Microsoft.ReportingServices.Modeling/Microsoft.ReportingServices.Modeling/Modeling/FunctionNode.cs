using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000A3 RID: 163
	public sealed class FunctionNode : ExpressionNode
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x0001A6B4 File Offset: 0x000188B4
		public FunctionNode(FunctionName functionName)
			: this(functionName, null)
		{
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001A6BE File Offset: 0x000188BE
		public FunctionNode(FunctionName functionName, params Expression[] arguments)
			: this(functionName, new ExpressionCollection(arguments))
		{
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001A6CD File Offset: 0x000188CD
		private FunctionNode(FunctionName functionName, ExpressionCollection arguments)
		{
			this.FunctionName = functionName;
			this.m_arguments = arguments ?? new ExpressionCollection(2);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001A6ED File Offset: 0x000188ED
		internal FunctionNode()
		{
			this.m_arguments = new ExpressionCollection(2);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001A704 File Offset: 0x00018904
		public bool? IsAggregate
		{
			get
			{
				FunctionInfo approxFunctionInfo = this.GetApproxFunctionInfo();
				if (approxFunctionInfo != null)
				{
					return new bool?(approxFunctionInfo.IsAggregate);
				}
				return null;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001A730 File Offset: 0x00018930
		public override bool IsConstantValue
		{
			get
			{
				FunctionInfo approxFunctionInfo = this.GetApproxFunctionInfo();
				if (approxFunctionInfo != null)
				{
					return approxFunctionInfo.IsStatic;
				}
				return base.IsConstantValue;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001A754 File Offset: 0x00018954
		internal override IQueryEntity SourceEntity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001A757 File Offset: 0x00018957
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001A75F File Offset: 0x0001895F
		public FunctionName FunctionName
		{
			get
			{
				return this.m_functionName;
			}
			set
			{
				if (!EnumUtil.IsDefined<FunctionName>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_functionName = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001A77C File Offset: 0x0001897C
		public ExpressionCollection Arguments
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001A784 File Offset: 0x00018984
		public override ExpressionNode Clone(ExpressionCopyManager copyManager)
		{
			return new FunctionNode(this.m_functionName, this.m_arguments.Clone(copyManager));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public override bool IsSameAs(ExpressionNode other)
		{
			FunctionNode functionNode = other as FunctionNode;
			return functionNode != null && this.m_functionName == functionNode.FunctionName && this.m_arguments.IsSameAs(functionNode.Arguments);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001A7DC File Offset: 0x000189DC
		public override bool IsSubtreeAnchored()
		{
			FunctionInfo functionInfo = this.GetFunctionInfo();
			if (functionInfo == null)
			{
				throw new InvalidOperationException();
			}
			for (int i = 0; i < this.m_arguments.Count; i++)
			{
				if (!functionInfo.IsAggregateArgument(i) && this.m_arguments[i].IsSubtreeAnchored())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001A830 File Offset: 0x00018A30
		internal override bool HasInvalidRefs(Bag<Expression> processedExpressions)
		{
			for (int i = 0; i < this.m_arguments.Count; i++)
			{
				if (this.m_arguments[i].HasInvalidRefs(processedExpressions))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001A86C File Offset: 0x00018A6C
		public FunctionInfo GetFunctionInfo()
		{
			if (base.IsCompiled)
			{
				return this.m_compiledFunctionInfo;
			}
			Expression expression;
			IQueryEntity queryEntity;
			return this.CompileCore(new CompilationContext(false, false, new Expression()), out expression, out queryEntity);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "FunctionName")
				{
					string text = xr.ReadValueAsString();
					if (!EnumUtil.TryParse<FunctionName>(text, out this.m_functionName))
					{
						xr.Validation.AddScopedError(ModelingErrorCode.InvalidFunctionName, SRErrors.InvalidFunctionName(xr.Validation.CurrentObjectDescriptor, text));
					}
					return true;
				}
				if (localName == "Arguments")
				{
					this.m_arguments.Load(xr, false);
					return true;
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001A923 File Offset: 0x00018B23
		internal override void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Function");
			xw.WriteElement("FunctionName", this.m_functionName);
			this.m_arguments.WriteTo(xw, "Arguments");
			xw.WriteEndElement();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001A960 File Offset: 0x00018B60
		internal override ResultType? Compile(CompilationContext ctx, bool topLevel, out Expression replacementExpr)
		{
			base.Compile(ctx.ShouldPersist);
			IQueryEntity queryEntity;
			FunctionInfo functionInfo = this.CompileCore(ctx, out replacementExpr, out queryEntity);
			if (ctx.ShouldPersist)
			{
				this.m_compiledFunctionInfo = functionInfo;
			}
			if (functionInfo == null)
			{
				return null;
			}
			if (replacementExpr != null)
			{
				return new ResultType?(replacementExpr.GetResultType());
			}
			if (queryEntity != null && ctx.ShouldPersist)
			{
				return new ResultType?(this.ProcessFunctionEntityKeyTargets(ctx, functionInfo.ReturnType, queryEntity));
			}
			return new ResultType?(functionInfo.ReturnType);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001A9DB File Offset: 0x00018BDB
		internal override void SetEntityKeyTarget(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			this.SetEntityKeyTargetOnArguments(entityKeyTarget, ctx);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001A9E8 File Offset: 0x00018BE8
		private FunctionInfo CompileCore(CompilationContext ctx, out Expression replacementExpr, out IQueryEntity firstEntityKeyTarget)
		{
			replacementExpr = null;
			firstEntityKeyTarget = null;
			FunctionInfo approxFunctionInfo = this.GetApproxFunctionInfo();
			if (ctx.ShouldNormalize && approxFunctionInfo != null && approxFunctionInfo.FunctionName == FunctionName.Filter)
			{
				Expression expression = this.m_arguments[0];
				Expression expression2 = this.m_arguments[1];
				if (expression.Path.IsEmpty && expression.NodeAsFunction != null && expression.NodeAsFunction.FunctionName == FunctionName.Filter)
				{
					return this.CombineFiltersAndCompile(ctx, expression, expression2, out replacementExpr, out firstEntityKeyTarget);
				}
			}
			ResultType[] array = this.m_arguments.Compile(ctx, ExpressionCompilationFlags.None, out firstEntityKeyTarget);
			if (array == null)
			{
				return null;
			}
			FunctionInfo functionInfo = FunctionInfo.Resolve(this.m_functionName, array, this.m_arguments, ctx);
			if (functionInfo != null)
			{
				if (functionInfo.FunctionName != this.m_functionName || (!functionInfo.RepeatingArguments && functionInfo.Arguments.Count != this.m_arguments.Count) || (functionInfo.RepeatingArguments && this.m_arguments.Count % functionInfo.Arguments.Count != 0))
				{
					throw new InternalModelingException("Function resolution returned wrong function info.");
				}
				if (!this.CheckArgsRange(functionInfo, ctx))
				{
					return null;
				}
				if (functionInfo.FunctionName == FunctionName.Aggregate)
				{
					replacementExpr = this.ResolveAggregateAndCompile(ctx);
				}
				else if (functionInfo.IsStatic)
				{
					replacementExpr = this.ResolveStaticAndCompile(ctx, functionInfo);
				}
				if (ctx.ShouldNormalize && replacementExpr == null && functionInfo.ReturnType.DataType == DataType.Null && functionInfo.IsScalar)
				{
					if (!ctx.ShouldPersist)
					{
						throw new InternalModelingException("Normalize without persist.");
					}
					replacementExpr = new Expression(new NullNode());
					if (functionInfo.ReturnType != replacementExpr.Compile(ctx, ExpressionCompilationFlags.None))
					{
						throw new InternalModelingException("Result type of the function node does not match the result type of the replacement expression.");
					}
				}
			}
			return functionInfo;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001ABA8 File Offset: 0x00018DA8
		private FunctionInfo CombineFiltersAndCompile(CompilationContext ctx, Expression outerFilterItemsArg, Expression outerFilterCondArg, out Expression replacementExpr, out IQueryEntity firstEntityKeyTarget)
		{
			Expression expression = outerFilterItemsArg.NodeAsFunction.Arguments[0];
			Expression expression2 = outerFilterItemsArg.NodeAsFunction.Arguments[1];
			Expression expression3 = new Expression(new FunctionNode(FunctionName.And, new Expression[] { outerFilterCondArg, expression2 }));
			this.m_arguments = new ExpressionCollection(2);
			this.m_arguments.Add(expression);
			this.m_arguments.Add(expression3);
			return this.CompileCore(ctx, out replacementExpr, out firstEntityKeyTarget);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001AC24 File Offset: 0x00018E24
		private Expression ResolveAggregateAndCompile(CompilationContext ctx)
		{
			Expression floatPath = this.m_arguments[0];
			if (ctx.ShouldNormalize && !floatPath.IsCompiled)
			{
				throw new InternalModelingException("ResolveAggregateAndCompile: aggregate argument must be compiled.");
			}
			Expression floatRootExpr = this.FindFirstNonPassThruExpression(floatPath);
			ExpressionNode node = floatRootExpr.Node;
			if (node.IsSubtreeAnchored())
			{
				ctx.AddScopedError(ModelingErrorCode.AggregateWithNonAggregateArgument, SRErrors.AggregateWithNonAggregateArgument(ctx.CurrentObjectDescriptor));
				return null;
			}
			if (node is AttributeRefNode && !((AttributeRefNode)node).ReplaceWithExpression)
			{
				return null;
			}
			if (!ctx.ShouldNormalize)
			{
				return null;
			}
			if (!ctx.ShouldPersist)
			{
				throw new InternalModelingException("Normalize without persist.");
			}
			Expression expression2 = new Expression(node.Clone());
			expression2.MarkAsSkipSecurityFilters();
			expression2 = expression2.Node.VisitAggregationFloatPoints(delegate(Expression expression, bool allowExprModification)
			{
				if (!allowExprModification)
				{
					throw new InternalModelingException("allowExprModification must be true for Aggregate resolution.");
				}
				if (floatPath == floatRootExpr)
				{
					expression.Path.InsertRange(0, floatPath.Path);
					return null;
				}
				Expression expression3 = floatPath.Clone();
				Expression expression4 = this.FindFirstNonPassThruExpression(expression3);
				expression4.Path.AddRange(expression.Path);
				expression4.Node = expression.Node.Clone();
				return expression3;
			}, true) ?? expression2;
			if (expression2.Compile(ctx, ExpressionCompilationFlags.None) != null)
			{
				return expression2;
			}
			return null;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001AD28 File Offset: 0x00018F28
		private ResultType ProcessFunctionEntityKeyTargets(CompilationContext ctx, ResultType resultType, IQueryEntity entityKeyTarget)
		{
			if (entityKeyTarget == null)
			{
				throw new InternalModelingException("entityKeyTarget is null.");
			}
			if (ctx.ShouldPersist && !this.m_arguments.IsReadOnly)
			{
				throw new InternalModelingException("m_arguments must be compiled if ctx.ShouldPersist is true.");
			}
			if (this.m_arguments.Count > 1)
			{
				this.SetEntityKeyTargetOnArguments(entityKeyTarget, ctx);
			}
			if (resultType.DataType == DataType.EntityKey || resultType.DataType == DataType.Null)
			{
				resultType.SetEntityKeyTarget(entityKeyTarget);
			}
			return resultType;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001AD98 File Offset: 0x00018F98
		private void SetEntityKeyTargetOnArguments(IQueryEntity entityKeyTarget, CompilationContext ctx)
		{
			if (ctx.ShouldPersist && !this.m_arguments.IsReadOnly)
			{
				throw new InternalModelingException("m_arguments must be compiled if ctx.ShouldPersist is true.");
			}
			for (int i = 0; i < this.m_arguments.Count; i++)
			{
				ResultType resultType = this.m_arguments[i].GetResultType();
				if (resultType.DataType == DataType.EntityKey || resultType.DataType == DataType.Null)
				{
					if (resultType.EntityKeyTarget == null)
					{
						this.m_arguments[i].SetEntityKeyTarget(entityKeyTarget, ctx);
					}
					else if (resultType.EntityKeyTarget != entityKeyTarget)
					{
						ctx.AddScopedError(ModelingErrorCode.EntityKeyTypeMismatch, SRErrors.EntityKeyTypeMismatch(ctx.CurrentObjectDescriptor, this.m_functionName, i + 1, resultType.EntityKeyTarget.Name, entityKeyTarget.Name));
					}
				}
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001AE60 File Offset: 0x00019060
		private Expression FindFirstNonPassThruExpression(Expression expression)
		{
			for (;;)
			{
				FunctionNode nodeAsFunction = expression.NodeAsFunction;
				FunctionInfo functionInfo = ((nodeAsFunction != null) ? nodeAsFunction.GetApproxFunctionInfo() : null);
				if (nodeAsFunction == null || !functionInfo.IsPassthrough)
				{
					break;
				}
				expression = nodeAsFunction.Arguments[functionInfo.PassthroughArgument.Value];
			}
			return expression;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001AEAC File Offset: 0x000190AC
		private Expression ResolveStaticAndCompile(CompilationContext ctx, FunctionInfo fInfo)
		{
			if (!ctx.ShouldResolveStaticFunctions)
			{
				return null;
			}
			if (!ctx.ShouldPersist)
			{
				throw new InternalModelingException("Normalize without persist.");
			}
			FunctionName functionName = fInfo.FunctionName;
			Expression expression;
			if (functionName <= FunctionName.Today)
			{
				if (functionName == FunctionName.Now)
				{
					expression = new Expression(ctx.Now.Clone());
					goto IL_00B3;
				}
				if (functionName == FunctionName.Today)
				{
					expression = new Expression(ctx.Today.Clone());
					goto IL_00B3;
				}
			}
			else
			{
				if (functionName == FunctionName.GetUserID)
				{
					expression = new Expression(ctx.UserID.Clone());
					goto IL_00B3;
				}
				if (functionName == FunctionName.GetUserCulture)
				{
					expression = new Expression(ctx.UserCulture.Clone());
					goto IL_00B3;
				}
			}
			throw new InternalModelingException("Unrecognized static function: " + fInfo.FunctionName.ToString());
			IL_00B3:
			if (expression == null)
			{
				throw new InternalModelingException("Failed to compile static function: " + fInfo.FunctionName.ToString());
			}
			if (expression.Compile(ctx, ExpressionCompilationFlags.None) == null)
			{
				return null;
			}
			return expression;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001AFAC File Offset: 0x000191AC
		private bool CheckArgsRange(FunctionInfo fInfo, CompilationContext ctx)
		{
			bool flag = false;
			FunctionName functionName = fInfo.FunctionName;
			if (functionName <= FunctionName.Right)
			{
				if (functionName != FunctionName.Substring)
				{
					if (functionName - FunctionName.Left <= 1)
					{
						if (this.m_arguments.Count != 2)
						{
							throw new InternalModelingException("Invalid number of arguments in Right/Left function.");
						}
						flag = !FunctionNode.CheckIntArgumentRange(this.m_arguments[1], 1L, null, fInfo.FunctionName, 2, ctx);
					}
				}
				else
				{
					if (this.m_arguments.Count != 3)
					{
						throw new InternalModelingException("Invalid number of arguments in Substring function.");
					}
					flag = !FunctionNode.CheckIntArgumentRange(this.m_arguments[1], 1L, null, fInfo.FunctionName, 2, ctx);
					flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[2], 1L, null, fInfo.FunctionName, 3, ctx);
				}
			}
			else if (functionName != FunctionName.Date)
			{
				if (functionName == FunctionName.DateTime)
				{
					if (this.m_arguments.Count != 6)
					{
						throw new InternalModelingException("Invalid number of arguments in DateTime function.");
					}
					flag = !FunctionNode.CheckIntArgumentRange(this.m_arguments[0], 1L, new long?(9999L), fInfo.FunctionName, 1, ctx);
					flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[1], 1L, new long?(12L), fInfo.FunctionName, 2, ctx);
					flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[2], 1L, new long?(31L), fInfo.FunctionName, 3, ctx);
					flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[3], 0L, new long?(23L), fInfo.FunctionName, 4, ctx);
					flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[4], 0L, new long?(59L), fInfo.FunctionName, 5, ctx);
					flag |= !FunctionNode.CheckFltArgumentRange(this.m_arguments[5], 0f, new float?((float)60), fInfo.FunctionName, 6, ctx);
				}
			}
			else if (this.m_arguments.Count != 1)
			{
				if (this.m_arguments.Count != 3)
				{
					throw new InternalModelingException("Invalid number of arguments in Date function.");
				}
				flag = !FunctionNode.CheckIntArgumentRange(this.m_arguments[0], 1L, new long?(9999L), fInfo.FunctionName, 1, ctx);
				flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[1], 1L, new long?(12L), fInfo.FunctionName, 2, ctx);
				flag |= !FunctionNode.CheckIntArgumentRange(this.m_arguments[2], 1L, new long?(31L), fInfo.FunctionName, 3, ctx);
			}
			return !flag;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001B268 File Offset: 0x00019468
		private static bool CheckIntArgumentRange(Expression arg, long min, long? max, FunctionName functionName, int argIndex, CompilationContext ctx)
		{
			if (arg.NodeAsLiteral != null && arg.NodeAsLiteral.DataType == DataType.Integer)
			{
				long valueAsInteger = arg.NodeAsLiteral.ValueAsInteger;
				if (min > valueAsInteger || (max != null && max.Value < valueAsInteger))
				{
					ctx.AddScopedError(ModelingErrorCode.ArgumentValueOutOfRange, SRErrors.ArgumentValueOutOfRange(ctx.CurrentObjectDescriptor, functionName, argIndex));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001B2CC File Offset: 0x000194CC
		private static bool CheckFltArgumentRange(Expression arg, float min, float? max, FunctionName functionName, int argIndex, CompilationContext ctx)
		{
			if (arg.NodeAsLiteral != null && (arg.NodeAsLiteral.DataType == DataType.Integer || arg.NodeAsLiteral.DataType == DataType.Decimal || arg.NodeAsLiteral.DataType == DataType.Float))
			{
				double num = (double)Convert.ChangeType(arg.NodeAsLiteral.Value, typeof(double), CultureInfo.InvariantCulture);
				if ((double)min > num || (max != null && (double)max.Value < num))
				{
					ctx.AddScopedError(ModelingErrorCode.ArgumentValueOutOfRange, SRErrors.ArgumentValueOutOfRange(ctx.CurrentObjectDescriptor, functionName, argIndex));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001B367 File Offset: 0x00019567
		internal FunctionInfo GetApproxFunctionInfo()
		{
			return FunctionInfo.GetFirst(this.m_functionName, this.m_arguments.Count);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B380 File Offset: 0x00019580
		internal override Expression VisitAggregationFloatPoints(ExpressionNode.FloatPointVisitor visitor, bool allowExprModification)
		{
			FunctionInfo approxFunctionInfo = this.GetApproxFunctionInfo();
			if (approxFunctionInfo == null)
			{
				return null;
			}
			if (approxFunctionInfo.IsPassthrough)
			{
				throw new InternalModelingException("VisitAggregationFloatPoints is called on a passthru function: " + this.m_functionName.ToString());
			}
			for (int i = 0; i < this.m_arguments.Count; i++)
			{
				Expression expression;
				if (approxFunctionInfo.IsAggregateArgument(i))
				{
					expression = visitor(this.m_arguments[i], allowExprModification);
				}
				else
				{
					expression = this.m_arguments[i].Node.VisitAggregationFloatPoints(visitor, allowExprModification);
				}
				if (expression != null)
				{
					if (!allowExprModification)
					{
						throw new InternalModelingException("VisitAggregationFloatPoints: attempt to modify function argument when allowExprModification is false.");
					}
					this.m_arguments[i] = expression;
				}
			}
			return null;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001B434 File Offset: 0x00019634
		internal override ExpressionNode CloneFor(SemanticModel newModel)
		{
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("FunctionNode is not compiled");
			}
			ExpressionCollection expressionCollection = this.m_arguments.CloneFor(newModel);
			if (expressionCollection == null)
			{
				return null;
			}
			FunctionNode functionNode = new FunctionNode(this.m_functionName, expressionCollection);
			functionNode.m_compiledFunctionInfo = this.m_compiledFunctionInfo;
			functionNode.SetCompiledIndicator();
			return functionNode;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B484 File Offset: 0x00019684
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(FunctionNode.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Arguments)
					{
						if (memberName != MemberName.CompiledFunctionInfoID)
						{
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
						writer.Write((this.m_compiledFunctionInfo != null) ? this.m_compiledFunctionInfo.ID : (-1));
					}
					else
					{
						((IPersistable)this.Arguments).Serialize(writer);
					}
				}
				else
				{
					writer.Write((ushort)this.m_functionName);
				}
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B538 File Offset: 0x00019738
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(FunctionNode.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Arguments)
					{
						if (memberName != MemberName.CompiledFunctionInfoID)
						{
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
						int num = reader.ReadInt32();
						if (num >= 0)
						{
							this.m_compiledFunctionInfo = FunctionInfo.GetInfo(num);
						}
					}
					else
					{
						((IPersistable)this.Arguments).Deserialize(reader);
					}
				}
				else
				{
					this.m_functionName = (FunctionName)reader.ReadUInt16();
				}
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001B5E4 File Offset: 0x000197E4
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001B5F0 File Offset: 0x000197F0
		internal override ObjectType GetObjectType()
		{
			return ObjectType.FunctionNode;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001B5F4 File Offset: 0x000197F4
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref FunctionNode.__declaration, FunctionNode.__declarationLock, () => new Declaration(ObjectType.FunctionNode, ObjectType.ExpressionNode, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Name, Token.UInt16),
					new MemberInfo(MemberName.Arguments, ObjectType.ExpressionCollection),
					new MemberInfo(MemberName.CompiledFunctionInfoID, Token.Int32)
				}));
			}
		}

		// Token: 0x040003B7 RID: 951
		internal const string FunctionElem = "Function";

		// Token: 0x040003B8 RID: 952
		private const string FunctionNameElem = "FunctionName";

		// Token: 0x040003B9 RID: 953
		private const string ArgumentsElem = "Arguments";

		// Token: 0x040003BA RID: 954
		private const int DefaultArgCapacity = 2;

		// Token: 0x040003BB RID: 955
		private FunctionName m_functionName;

		// Token: 0x040003BC RID: 956
		private ExpressionCollection m_arguments;

		// Token: 0x040003BD RID: 957
		private FunctionInfo m_compiledFunctionInfo;

		// Token: 0x040003BE RID: 958
		private static Declaration __declaration;

		// Token: 0x040003BF RID: 959
		private static readonly object __declarationLock = new object();
	}
}
