using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x020000AA RID: 170
	internal class ExpressionWriter : DataServiceALinqExpressionVisitor
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00016690 File Offset: 0x00014890
		private ExpressionWriter(DataServiceContext context, bool inPath)
		{
			this.context = context;
			this.inPath = inPath;
			this.builder = new StringBuilder();
			this.expressionStack = new Stack<Expression>();
			this.expressionStack.Push(null);
			this.uriVersion = Util.ODataVersion4;
			this.scopeCount = 0;
			this.writingFunctionsInQuery = false;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000166EC File Offset: 0x000148EC
		private bool InSubScope
		{
			get
			{
				return this.scopeCount > 0;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000166F8 File Offset: 0x000148F8
		internal static string ExpressionToString(DataServiceContext context, Expression e, bool inPath, ref Version uriVersion)
		{
			ExpressionWriter expressionWriter = new ExpressionWriter(context, inPath);
			string text = expressionWriter.Translate(e);
			WebUtil.RaiseVersion(ref uriVersion, expressionWriter.uriVersion);
			if (expressionWriter.cantTranslateExpression)
			{
				throw new NotSupportedException(Strings.ALinq_CantTranslateExpression(e.ToString()));
			}
			return text;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001673C File Offset: 0x0001493C
		internal override Expression Visit(Expression exp)
		{
			this.parent = this.expressionStack.Peek();
			this.expressionStack.Push(exp);
			Expression expression = base.Visit(exp);
			this.expressionStack.Pop();
			return expression;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001677B File Offset: 0x0001497B
		internal override Expression VisitConditional(ConditionalExpression c)
		{
			this.cantTranslateExpression = true;
			return c;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001677B File Offset: 0x0001497B
		internal override Expression VisitLambda(LambdaExpression lambda)
		{
			this.cantTranslateExpression = true;
			return lambda;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001677B File Offset: 0x0001497B
		internal override NewExpression VisitNew(NewExpression nex)
		{
			this.cantTranslateExpression = true;
			return nex;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001677B File Offset: 0x0001497B
		internal override Expression VisitMemberInit(MemberInitExpression init)
		{
			this.cantTranslateExpression = true;
			return init;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001677B File Offset: 0x0001497B
		internal override Expression VisitListInit(ListInitExpression init)
		{
			this.cantTranslateExpression = true;
			return init;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001677B File Offset: 0x0001497B
		internal override Expression VisitNewArray(NewArrayExpression na)
		{
			this.cantTranslateExpression = true;
			return na;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001677B File Offset: 0x0001497B
		internal override Expression VisitInvocation(InvocationExpression iv)
		{
			this.cantTranslateExpression = true;
			return iv;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016788 File Offset: 0x00014988
		internal override Expression VisitInputReferenceExpression(InputReferenceExpression ire)
		{
			if (this.parent == null || (!this.InSubScope && this.parent.NodeType != ExpressionType.MemberAccess && this.parent.NodeType != ExpressionType.TypeAs && this.parent.NodeType != ExpressionType.Call))
			{
				string text = ((this.parent != null) ? this.parent.ToString() : ire.ToString());
				throw new NotSupportedException(Strings.ALinq_CantTranslateExpression(text));
			}
			if (this.InSubScope || this.parent.NodeType == ExpressionType.Call)
			{
				this.builder.Append("$it");
			}
			return ire;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016824 File Offset: 0x00014A24
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			string text;
			if (TypeSystem.TryGetQueryOptionMethod(m.Method, out text))
			{
				this.builder.Append(text);
				this.builder.Append('(');
				if (text == "contains")
				{
					this.Visit(m.Object);
					this.builder.Append(',');
					this.Visit(m.Arguments[0]);
				}
				else
				{
					if (m.Object != null)
					{
						this.Visit(m.Object);
					}
					if (m.Arguments.Count > 0)
					{
						if (m.Object != null)
						{
							this.builder.Append(',');
						}
						for (int i = 0; i < m.Arguments.Count; i++)
						{
							this.Visit(m.Arguments[i]);
							if (i < m.Arguments.Count - 1)
							{
								this.builder.Append(',');
							}
						}
					}
				}
				this.builder.Append(')');
			}
			else if (m.Method.Name == "HasFlag")
			{
				this.Visit(m.Object);
				this.builder.Append(' ');
				this.builder.Append("has");
				this.builder.Append(' ');
				this.Visit(m.Arguments[0]);
			}
			else
			{
				SequenceMethod sequenceMethod;
				if (!ReflectionUtil.TryIdentifySequenceMethod(m.Method, out sequenceMethod))
				{
					if (m.Object != null)
					{
						this.Visit(m.Object);
					}
					if (m.Method.Name != "GetValue" && m.Method.Name != "GetValueAsync")
					{
						this.builder.Append('/');
						this.writingFunctionsInQuery = true;
						string text2 = this.context.ResolveNameFromTypeInternal(m.Method.DeclaringType);
						if (string.IsNullOrEmpty(text2))
						{
							throw new NotSupportedException(Strings.ALinq_CantTranslateExpression(m.ToString()));
						}
						int num = text2.LastIndexOf('.');
						string text3 = text2.Remove(num + 1);
						string serverDefinedName = ClientTypeUtil.GetServerDefinedName(m.Method);
						this.builder.Append(text3 + serverDefinedName);
						this.builder.Append('(');
						string[] array = (from p in m.Method.GetParameters()
							select p.Name).ToArray<string>();
						for (int j = 0; j < m.Arguments.Count; j++)
						{
							this.builder.Append(array[j]);
							this.builder.Append('=');
							this.scopeCount++;
							this.Visit(m.Arguments[j]);
							this.scopeCount--;
							this.builder.Append(',');
						}
						if (m.Arguments.Any<Expression>())
						{
							this.builder.Remove(this.builder.Length - 1, 1);
						}
						this.builder.Append(')');
						this.writingFunctionsInQuery = false;
					}
					return m;
				}
				if (ReflectionUtil.IsAnyAllMethod(sequenceMethod))
				{
					WebUtil.RaiseVersion(ref this.uriVersion, Util.ODataVersion4);
					this.Visit(m.Arguments[0]);
					this.builder.Append('/');
					if (sequenceMethod == SequenceMethod.All)
					{
						this.builder.Append("all");
					}
					else
					{
						this.builder.Append("any");
					}
					this.builder.Append('(');
					if (sequenceMethod != SequenceMethod.Any)
					{
						LambdaExpression lambdaExpression = (LambdaExpression)m.Arguments[1];
						string name = lambdaExpression.Parameters[0].Name;
						this.builder.Append(name);
						this.builder.Append(':');
						this.scopeCount++;
						this.Visit(lambdaExpression.Body);
						this.scopeCount--;
					}
					this.builder.Append(')');
					return m;
				}
				if (sequenceMethod == SequenceMethod.OfType && this.parent != null)
				{
					MethodCallExpression methodCallExpression = this.parent as MethodCallExpression;
					if (methodCallExpression != null && ReflectionUtil.TryIdentifySequenceMethod(methodCallExpression.Method, out sequenceMethod) && ReflectionUtil.IsAnyAllMethod(sequenceMethod))
					{
						Type type = methodCallExpression.Method.GetGenericArguments().SingleOrDefault<Type>();
						if (ClientTypeUtil.TypeOrElementTypeIsEntity(type))
						{
							this.Visit(m.Arguments[0]);
							this.builder.Append('/');
							UriHelper.AppendTypeSegment(this.builder, type, this.context, this.inPath, ref this.uriVersion);
							return m;
						}
					}
				}
				else if (sequenceMethod == SequenceMethod.Count && this.parent != null)
				{
					if (m.Arguments.Any<Expression>() && m.Arguments[0] != null)
					{
						this.Visit(m.Arguments[0]);
					}
					this.builder.Append('/').Append('$').Append("count");
					return m;
				}
				this.cantTranslateExpression = true;
			}
			return m;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00016D70 File Offset: 0x00014F70
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			if (m.Member is FieldInfo)
			{
				throw new NotSupportedException(Strings.ALinq_CantReferToPublicField(m.Member.Name));
			}
			Expression expression = this.Visit(m.Expression);
			if (m.Member.Name == "Value" && m.Member.DeclaringType.IsGenericType() && m.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return m;
			}
			MethodCallExpression methodCallExpression = m.Expression as MethodCallExpression;
			if (methodCallExpression != null && methodCallExpression.Method.Name == "GetValueAsync" && m.Member.Name == "Result")
			{
				return m;
			}
			if (!this.IsImplicitInputReference(expression) || this.writingFunctionsInQuery)
			{
				this.builder.Append('/');
			}
			Type declaringType = m.Member.DeclaringType;
			Type implementationType = ClientTypeUtil.GetImplementationType(declaringType, typeof(ICollection<>));
			if (!PrimitiveType.IsKnownNullableType(declaringType) && implementationType != null && m.Member.Name.Equals("Count"))
			{
				this.builder.Append('$').Append("count");
			}
			else
			{
				this.builder.Append(ClientTypeUtil.GetServerDefinedName(m.Member));
			}
			return m;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00016ED0 File Offset: 0x000150D0
		internal override Expression VisitConstant(ConstantExpression c)
		{
			if (c.Value == null)
			{
				this.builder.Append("null");
				return c;
			}
			BinaryExpression binaryExpression = this.parent as BinaryExpression;
			MethodCallExpression methodCallExpression = this.parent as MethodCallExpression;
			string text2;
			if ((binaryExpression != null && ExpressionWriter.HasEnumInBinaryExpression(binaryExpression)) || (methodCallExpression != null && methodCallExpression.Method.Name == "HasFlag"))
			{
				c = this.ConvertConstantExpressionForEnum(c);
				ClientEdmModel model = this.context.Model;
				IEdmType orCreateEdmType = model.GetOrCreateEdmType(c.Type.IsEnum() ? c.Type : c.Type.GetGenericArguments()[0]);
				ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(orCreateEdmType);
				string text = this.context.ResolveNameFromTypeInternal(clientTypeAnnotation.ElementType);
				MemberInfo field = clientTypeAnnotation.ElementType.GetField(c.Value.ToString());
				string serverDefinedName = ClientTypeUtil.GetServerDefinedName(field);
				ODataEnumValue odataEnumValue = new ODataEnumValue(serverDefinedName, text ?? clientTypeAnnotation.ElementTypeName);
				text2 = ODataUriUtils.ConvertToUriLiteral(odataEnumValue, CommonUtil.ConvertToODataVersion(this.uriVersion), null);
			}
			else
			{
				try
				{
					text2 = LiteralFormatter.ForConstants.Format(c.Value);
				}
				catch (InvalidOperationException)
				{
					if (this.cantTranslateExpression)
					{
						return c;
					}
					throw new NotSupportedException(Strings.ALinq_CouldNotConvert(c.Value));
				}
			}
			this.builder.Append(text2);
			return c;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001703C File Offset: 0x0001523C
		internal override Expression VisitUnary(UnaryExpression u)
		{
			ExpressionType nodeType = u.NodeType;
			if (nodeType <= ExpressionType.ConvertChecked)
			{
				if (nodeType == ExpressionType.Convert || nodeType == ExpressionType.ConvertChecked)
				{
					if (u.Type != typeof(object))
					{
						if (ExpressionWriter.IsEnumTypeExpression(u))
						{
							this.Visit(u.Operand);
							return u;
						}
						this.builder.Append("cast");
						this.builder.Append('(');
						if (!this.IsImplicitInputReference(u.Operand))
						{
							this.Visit(u.Operand);
							this.builder.Append(',');
						}
						this.builder.Append('\'');
						this.builder.Append(UriHelper.GetTypeNameForUri(u.Type, this.context));
						this.builder.Append('\'');
						this.builder.Append(')');
						return u;
					}
					else
					{
						if (!this.IsImplicitInputReference(u.Operand))
						{
							this.Visit(u.Operand);
							return u;
						}
						return u;
					}
				}
			}
			else
			{
				switch (nodeType)
				{
				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
					this.builder.Append(' ');
					this.builder.Append("-");
					this.VisitOperand(u.Operand);
					return u;
				case ExpressionType.UnaryPlus:
					return u;
				case ExpressionType.New:
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
					break;
				case ExpressionType.Not:
					this.builder.Append("not");
					this.builder.Append(' ');
					this.VisitOperand(u.Operand);
					return u;
				default:
					if (nodeType == ExpressionType.TypeAs)
					{
						if (u.Operand.NodeType == ExpressionType.TypeAs)
						{
							throw new NotSupportedException(Strings.ALinq_CannotUseTypeFiltersMultipleTimes);
						}
						this.Visit(u.Operand);
						if (!this.IsImplicitInputReference(u.Operand))
						{
							this.builder.Append('/');
						}
						UriHelper.AppendTypeSegment(this.builder, u.Type, this.context, this.inPath, ref this.uriVersion);
						return u;
					}
					break;
				}
			}
			this.cantTranslateExpression = true;
			return u;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00017254 File Offset: 0x00015454
		internal override Expression VisitBinary(BinaryExpression b)
		{
			this.VisitOperand(b.Left, new ExpressionType?(b.NodeType), new ExpressionWriter.ChildDirection?(ExpressionWriter.ChildDirection.Left));
			this.builder.Append(' ');
			switch (b.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
				this.builder.Append("add");
				goto IL_01FD;
			case ExpressionType.And:
			case ExpressionType.AndAlso:
				this.builder.Append("and");
				goto IL_01FD;
			case ExpressionType.Divide:
				this.builder.Append("div");
				goto IL_01FD;
			case ExpressionType.Equal:
				this.builder.Append("eq");
				goto IL_01FD;
			case ExpressionType.GreaterThan:
				this.builder.Append("gt");
				goto IL_01FD;
			case ExpressionType.GreaterThanOrEqual:
				this.builder.Append("ge");
				goto IL_01FD;
			case ExpressionType.LessThan:
				this.builder.Append("lt");
				goto IL_01FD;
			case ExpressionType.LessThanOrEqual:
				this.builder.Append("le");
				goto IL_01FD;
			case ExpressionType.Modulo:
				this.builder.Append("mod");
				goto IL_01FD;
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				this.builder.Append("mul");
				goto IL_01FD;
			case ExpressionType.NotEqual:
				this.builder.Append("ne");
				goto IL_01FD;
			case ExpressionType.Or:
			case ExpressionType.OrElse:
				this.builder.Append("or");
				goto IL_01FD;
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				this.builder.Append("sub");
				goto IL_01FD;
			}
			this.cantTranslateExpression = true;
			IL_01FD:
			this.builder.Append(' ');
			this.VisitOperand(b.Right, new ExpressionType?(b.NodeType), new ExpressionWriter.ChildDirection?(ExpressionWriter.ChildDirection.Right));
			return b;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001748C File Offset: 0x0001568C
		internal override Expression VisitTypeIs(TypeBinaryExpression b)
		{
			this.builder.Append("isof");
			this.builder.Append('(');
			if (!this.IsImplicitInputReference(b.Expression))
			{
				this.Visit(b.Expression);
				this.builder.Append(',');
				this.builder.Append(' ');
			}
			this.builder.Append('\'');
			this.builder.Append(UriHelper.GetTypeNameForUri(b.TypeOperand, this.context));
			this.builder.Append('\'');
			this.builder.Append(')');
			return b;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00017537 File Offset: 0x00015737
		internal override Expression VisitParameter(ParameterExpression p)
		{
			if (this.InSubScope)
			{
				this.builder.Append(p.Name);
			}
			return p;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00017554 File Offset: 0x00015754
		private static bool AreExpressionTypesCollapsible(ExpressionType type, ExpressionType parentType, ExpressionWriter.ChildDirection childDirection)
		{
			int num = ExpressionWriter.BinaryPrecedence(type);
			int num2 = ExpressionWriter.BinaryPrecedence(parentType);
			if (num >= 0 && num2 >= 0)
			{
				if (childDirection == ExpressionWriter.ChildDirection.Left)
				{
					if (num <= num2)
					{
						return true;
					}
				}
				else if (num < num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00017588 File Offset: 0x00015788
		private static int BinaryPrecedence(ExpressionType type)
		{
			switch (type)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
				break;
			case ExpressionType.And:
			case ExpressionType.AndAlso:
				return 3;
			default:
				switch (type)
				{
				case ExpressionType.Divide:
				case ExpressionType.Modulo:
				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
					return 0;
				case ExpressionType.Equal:
				case ExpressionType.GreaterThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
					break;
				case ExpressionType.ExclusiveOr:
				case ExpressionType.Invoke:
				case ExpressionType.Lambda:
				case ExpressionType.LeftShift:
				case ExpressionType.ListInit:
				case ExpressionType.MemberAccess:
				case ExpressionType.MemberInit:
					return -1;
				default:
					switch (type)
					{
					case ExpressionType.NotEqual:
						break;
					case ExpressionType.Or:
					case ExpressionType.OrElse:
						return 4;
					case ExpressionType.Parameter:
					case ExpressionType.Power:
					case ExpressionType.Quote:
					case ExpressionType.RightShift:
						return -1;
					case ExpressionType.Subtract:
					case ExpressionType.SubtractChecked:
						return 1;
					default:
						return -1;
					}
					break;
				}
				return 2;
			}
			return 1;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017630 File Offset: 0x00015830
		private void VisitOperand(Expression e)
		{
			this.VisitOperand(e, null, null);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017658 File Offset: 0x00015858
		private void VisitOperand(Expression e, ExpressionType? parentType, ExpressionWriter.ChildDirection? childDirection)
		{
			if (e is BinaryExpression)
			{
				bool flag = parentType == null || !ExpressionWriter.AreExpressionTypesCollapsible(e.NodeType, parentType.Value, childDirection.Value);
				if (flag)
				{
					this.builder.Append('(');
				}
				this.Visit(e);
				if (flag)
				{
					this.builder.Append(')');
					return;
				}
			}
			else
			{
				this.Visit(e);
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000176C9 File Offset: 0x000158C9
		private string Translate(Expression e)
		{
			this.Visit(e);
			return this.builder.ToString();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000176DE File Offset: 0x000158DE
		private bool IsImplicitInputReference(Expression exp)
		{
			return !this.InSubScope && (exp is InputReferenceExpression || exp is ParameterExpression);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000176FD File Offset: 0x000158FD
		private static bool HasEnumInBinaryExpression(BinaryExpression b)
		{
			return ExpressionWriter.IsEnumTypeExpression(b.Left) || ExpressionWriter.IsEnumTypeExpression(b.Right);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001771C File Offset: 0x0001591C
		private static bool IsEnumTypeExpression(Expression e)
		{
			UnaryExpression unaryExpression = e as UnaryExpression;
			return unaryExpression != null && (unaryExpression.Operand.Type.IsEnum() || (unaryExpression.Operand.Type.IsGenericType() && unaryExpression.Operand.Type.GetGenericArguments()[0].IsEnum()));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00017774 File Offset: 0x00015974
		private static Type GetEnumType(Expression e)
		{
			UnaryExpression unaryExpression = e as UnaryExpression;
			if (unaryExpression != null)
			{
				if (!unaryExpression.Operand.Type.IsEnum())
				{
					return unaryExpression.Operand.Type.GetGenericArguments()[0];
				}
				return unaryExpression.Operand.Type;
			}
			else
			{
				if (!e.Type.IsEnum())
				{
					return e.Type.GetGenericArguments()[0];
				}
				return e.Type;
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000177E0 File Offset: 0x000159E0
		private ConstantExpression ConvertConstantExpressionForEnum(ConstantExpression constant)
		{
			Type type = null;
			if (this.parent is BinaryExpression)
			{
				BinaryExpression binaryExpression = this.parent as BinaryExpression;
				if (constant == binaryExpression.Left)
				{
					type = ExpressionWriter.GetEnumType(binaryExpression.Right);
				}
				else
				{
					type = ExpressionWriter.GetEnumType(binaryExpression.Left);
				}
			}
			else
			{
				MethodCallExpression methodCallExpression = this.parent as MethodCallExpression;
				if (methodCallExpression != null && methodCallExpression.Method.Name == "HasFlag")
				{
					type = ExpressionWriter.GetEnumType(methodCallExpression.Object);
				}
			}
			return Expression.Constant(Enum.Parse(type, constant.Value.ToString(), false));
		}

		// Token: 0x04000272 RID: 626
		private readonly StringBuilder builder;

		// Token: 0x04000273 RID: 627
		private readonly DataServiceContext context;

		// Token: 0x04000274 RID: 628
		private readonly Stack<Expression> expressionStack;

		// Token: 0x04000275 RID: 629
		private readonly bool inPath;

		// Token: 0x04000276 RID: 630
		private bool cantTranslateExpression;

		// Token: 0x04000277 RID: 631
		private Expression parent;

		// Token: 0x04000278 RID: 632
		private Version uriVersion;

		// Token: 0x04000279 RID: 633
		private int scopeCount;

		// Token: 0x0400027A RID: 634
		private bool writingFunctionsInQuery;

		// Token: 0x02000186 RID: 390
		private enum ChildDirection
		{
			// Token: 0x04000757 RID: 1879
			Left,
			// Token: 0x04000758 RID: 1880
			Right
		}
	}
}
