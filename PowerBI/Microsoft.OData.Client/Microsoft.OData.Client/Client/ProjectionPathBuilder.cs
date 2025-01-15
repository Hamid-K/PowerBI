using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x0200005D RID: 93
	internal class ProjectionPathBuilder
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000B1E8 File Offset: 0x000093E8
		internal ProjectionPathBuilder()
		{
			this.entityInScope = new Stack<bool>();
			this.rewrites = new List<ProjectionPathBuilder.MemberInitRewrite>();
			this.parameterExpressions = new Stack<ParameterExpression>();
			this.parameterExpressionTypes = new Stack<Expression>();
			this.parameterEntries = new Stack<Expression>();
			this.parameterProjectionTypes = new Stack<Type>();
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B23D File Offset: 0x0000943D
		internal bool CurrentIsEntity
		{
			get
			{
				return this.entityInScope.Peek();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B24A File Offset: 0x0000944A
		internal Expression ExpectedParamTypeInScope
		{
			get
			{
				return this.parameterExpressionTypes.Peek();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B257 File Offset: 0x00009457
		internal bool HasRewrites
		{
			get
			{
				return this.rewrites.Count > 0;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B267 File Offset: 0x00009467
		internal Expression LambdaParameterInScope
		{
			get
			{
				return this.parameterExpressions.Peek();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B274 File Offset: 0x00009474
		internal Expression ParameterEntryInScope
		{
			get
			{
				return this.parameterEntries.Peek();
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B284 File Offset: 0x00009484
		public override string ToString()
		{
			string text = "ProjectionPathBuilder: ";
			if (this.parameterExpressions.Count == 0)
			{
				text += "(empty)";
			}
			else
			{
				text = string.Concat(new object[]
				{
					text,
					"entity:",
					this.CurrentIsEntity.ToString(),
					" param:",
					this.ParameterEntryInScope
				});
			}
			return text;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B2F0 File Offset: 0x000094F0
		internal void EnterLambdaScope(LambdaExpression lambda, Expression entry, Expression expectedType)
		{
			ParameterExpression parameterExpression = lambda.Parameters[0];
			Type type = lambda.Body.Type;
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(type);
			this.entityInScope.Push(flag);
			this.parameterExpressions.Push(parameterExpression);
			this.parameterExpressionTypes.Push(expectedType);
			this.parameterEntries.Push(entry);
			this.parameterProjectionTypes.Push(type);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B35C File Offset: 0x0000955C
		internal void EnterMemberInit(MemberInitExpression init)
		{
			bool flag = ClientTypeUtil.TypeOrElementTypeIsEntity(init.Type);
			this.entityInScope.Push(flag);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B384 File Offset: 0x00009584
		internal Expression GetRewrite(Expression expression)
		{
			List<string> list = new List<string>();
			expression = ResourceBinder.StripTo<Expression>(expression);
			while (expression.NodeType == ExpressionType.MemberAccess || expression.NodeType == ExpressionType.TypeAs)
			{
				if (expression.NodeType == ExpressionType.MemberAccess)
				{
					MemberExpression memberExpression = (MemberExpression)expression;
					list.Add(memberExpression.Member.Name);
					expression = ResourceBinder.StripTo<Expression>(memberExpression.Expression);
				}
				else
				{
					expression = ResourceBinder.StripTo<Expression>(((UnaryExpression)expression).Operand);
				}
			}
			Expression expression2 = null;
			foreach (ProjectionPathBuilder.MemberInitRewrite memberInitRewrite in this.rewrites)
			{
				if (memberInitRewrite.Root == expression && list.Count == memberInitRewrite.MemberNames.Length)
				{
					bool flag = true;
					int num = 0;
					while (num < list.Count && num < memberInitRewrite.MemberNames.Length)
					{
						if (list[list.Count - num - 1] != memberInitRewrite.MemberNames[num])
						{
							flag = false;
							break;
						}
						num++;
					}
					if (flag)
					{
						expression2 = memberInitRewrite.RewriteExpression;
						break;
					}
				}
			}
			return expression2;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B4B4 File Offset: 0x000096B4
		internal void LeaveLambdaScope()
		{
			this.entityInScope.Pop();
			this.parameterExpressions.Pop();
			this.parameterExpressionTypes.Pop();
			this.parameterEntries.Pop();
			this.parameterProjectionTypes.Pop();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B4F2 File Offset: 0x000096F2
		internal void LeaveMemberInit()
		{
			this.entityInScope.Pop();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B500 File Offset: 0x00009700
		internal void RegisterRewrite(Expression root, string[] names, Expression rewriteExpression)
		{
			this.rewrites.Add(new ProjectionPathBuilder.MemberInitRewrite
			{
				Root = root,
				MemberNames = names,
				RewriteExpression = rewriteExpression
			});
			this.parameterEntries.Push(rewriteExpression);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B534 File Offset: 0x00009734
		internal void RevokeRewrite(Expression root, string[] names)
		{
			for (int i = 0; i < this.rewrites.Count; i++)
			{
				if (this.rewrites[i].Root == root && names.Length == this.rewrites[i].MemberNames.Length)
				{
					bool flag = true;
					for (int j = 0; j < names.Length; j++)
					{
						if (names[j] != this.rewrites[i].MemberNames[j])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.rewrites.RemoveAt(i);
						this.parameterEntries.Pop();
						return;
					}
				}
			}
		}

		// Token: 0x040000F8 RID: 248
		private readonly Stack<bool> entityInScope;

		// Token: 0x040000F9 RID: 249
		private readonly List<ProjectionPathBuilder.MemberInitRewrite> rewrites;

		// Token: 0x040000FA RID: 250
		private readonly Stack<ParameterExpression> parameterExpressions;

		// Token: 0x040000FB RID: 251
		private readonly Stack<Expression> parameterExpressionTypes;

		// Token: 0x040000FC RID: 252
		private readonly Stack<Expression> parameterEntries;

		// Token: 0x040000FD RID: 253
		private readonly Stack<Type> parameterProjectionTypes;

		// Token: 0x02000169 RID: 361
		internal class MemberInitRewrite
		{
			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0002EA8E File Offset: 0x0002CC8E
			// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0002EA96 File Offset: 0x0002CC96
			internal string[] MemberNames { get; set; }

			// Token: 0x17000353 RID: 851
			// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0002EA9F File Offset: 0x0002CC9F
			// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0002EAA7 File Offset: 0x0002CCA7
			internal Expression Root { get; set; }

			// Token: 0x17000354 RID: 852
			// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
			// (set) Token: 0x06000D73 RID: 3443 RVA: 0x0002EAB8 File Offset: 0x0002CCB8
			internal Expression RewriteExpression { get; set; }
		}
	}
}
