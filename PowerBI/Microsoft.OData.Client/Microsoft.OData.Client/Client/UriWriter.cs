using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000AB RID: 171
	internal class UriWriter : DataServiceALinqExpressionVisitor
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x00017878 File Offset: 0x00015A78
		private UriWriter(DataServiceContext context)
		{
			this.context = context;
			this.uriVersion = Util.ODataVersion4;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000178C8 File Offset: 0x00015AC8
		internal static void Translate(DataServiceContext context, bool addTrailingParens, Expression e, out Uri uri, out Version version)
		{
			UriWriter uriWriter = new UriWriter(context);
			uriWriter.Visit(e);
			string text = uriWriter.uriBuilder.ToString();
			if (uriWriter.alias.Any<KeyValuePair<string, string>>())
			{
				if (text.IndexOf('?') > -1)
				{
					text += "&";
				}
				else
				{
					text += "?";
				}
				foreach (KeyValuePair<string, string> keyValuePair in uriWriter.alias)
				{
					text += keyValuePair.Key;
					text += "=";
					text += keyValuePair.Value;
					text += "&";
				}
				text = text.Substring(0, text.Length - 1);
			}
			uri = UriUtil.CreateUri(text, UriKind.Absolute);
			version = uriWriter.uriVersion;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000179BC File Offset: 0x00015BBC
		internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			throw Error.MethodNotSupported(m);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000179C4 File Offset: 0x00015BC4
		internal override Expression VisitUnary(UnaryExpression u)
		{
			throw new NotSupportedException(Strings.ALinq_UnaryNotSupported(u.NodeType.ToString()));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000179F0 File Offset: 0x00015BF0
		internal override Expression VisitBinary(BinaryExpression b)
		{
			throw new NotSupportedException(Strings.ALinq_BinaryNotSupported(b.NodeType.ToString()));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017A1B File Offset: 0x00015C1B
		internal override Expression VisitConstant(ConstantExpression c)
		{
			throw new NotSupportedException(Strings.ALinq_ConstantNotSupported(c.Value));
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00017A2D File Offset: 0x00015C2D
		internal override Expression VisitTypeIs(TypeBinaryExpression b)
		{
			throw new NotSupportedException(Strings.ALinq_TypeBinaryNotSupported);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00017A39 File Offset: 0x00015C39
		internal override Expression VisitConditional(ConditionalExpression c)
		{
			throw new NotSupportedException(Strings.ALinq_ConditionalNotSupported);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00017A45 File Offset: 0x00015C45
		internal override Expression VisitParameter(ParameterExpression p)
		{
			throw new NotSupportedException(Strings.ALinq_ParameterNotSupported);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00017A51 File Offset: 0x00015C51
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			throw new NotSupportedException(Strings.ALinq_MemberAccessNotSupported(m.Member.Name));
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00017A68 File Offset: 0x00015C68
		internal override Expression VisitLambda(LambdaExpression lambda)
		{
			throw new NotSupportedException(Strings.ALinq_LambdaNotSupported);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00017A74 File Offset: 0x00015C74
		internal override NewExpression VisitNew(NewExpression nex)
		{
			throw new NotSupportedException(Strings.ALinq_NewNotSupported);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00017A80 File Offset: 0x00015C80
		internal override Expression VisitMemberInit(MemberInitExpression init)
		{
			throw new NotSupportedException(Strings.ALinq_MemberInitNotSupported);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00017A8C File Offset: 0x00015C8C
		internal override Expression VisitListInit(ListInitExpression init)
		{
			throw new NotSupportedException(Strings.ALinq_ListInitNotSupported);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00017A98 File Offset: 0x00015C98
		internal override Expression VisitNewArray(NewArrayExpression na)
		{
			throw new NotSupportedException(Strings.ALinq_NewArrayNotSupported);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017AA4 File Offset: 0x00015CA4
		internal override Expression VisitInvocation(InvocationExpression iv)
		{
			throw new NotSupportedException(Strings.ALinq_InvocationNotSupported);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017AB0 File Offset: 0x00015CB0
		internal override Expression VisitNavigationPropertySingletonExpression(NavigationPropertySingletonExpression npse)
		{
			this.Visit(npse.Source);
			this.uriBuilder.Append('/').Append(this.ExpressionToString(npse.MemberExpression, true));
			this.VisitQueryOptions(npse);
			return npse;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00017AE8 File Offset: 0x00015CE8
		internal override Expression VisitQueryableResourceExpression(QueryableResourceExpression rse)
		{
			if (rse.NodeType == (ExpressionType)10002)
			{
				if (rse.IsOperationInvocation && !(rse.Source is QueryableResourceExpression))
				{
					Dictionary<Expression, Expression> dictionary = new Dictionary<Expression, Expression>(ReferenceEqualityComparer<Expression>.Instance);
					Expression expression = Evaluator.PartialEval(rse.Source);
					expression = ExpressionNormalizer.Normalize(expression, dictionary);
					expression = ResourceBinder.Bind(expression, this.context);
					this.Visit(expression);
				}
				else
				{
					this.Visit(rse.Source);
				}
				this.uriBuilder.Append('/').Append(this.ExpressionToString(rse.MemberExpression, true));
			}
			else if (rse.MemberExpression != null)
			{
				string text = (string)((ConstantExpression)rse.MemberExpression).Value;
				this.uriBuilder.Append(this.context.BaseUriResolver.GetEntitySetUri(text));
			}
			else
			{
				this.uriBuilder.Append(this.context.BaseUriResolver.BaseUriOrNull);
			}
			WebUtil.RaiseVersion(ref this.uriVersion, rse.UriVersion);
			if (rse.ResourceTypeAs != null)
			{
				this.uriBuilder.Append('/');
				UriHelper.AppendTypeSegment(this.uriBuilder, rse.ResourceTypeAs, this.context, true, ref this.uriVersion);
			}
			if (rse.KeyPredicateConjuncts.Count > 0)
			{
				this.context.UrlKeyDelimiter.AppendKeyExpression<KeyValuePair<PropertyInfo, ConstantExpression>>(rse.GetKeyProperties(), (KeyValuePair<PropertyInfo, ConstantExpression> kvp) => ClientTypeUtil.GetServerDefinedName(kvp.Key), (KeyValuePair<PropertyInfo, ConstantExpression> kvp) => kvp.Value.Value, this.uriBuilder);
			}
			if (rse.IsOperationInvocation)
			{
				this.VisitOperationInvocation(rse);
			}
			if (rse.CountOption == CountOption.CountSegment)
			{
				this.uriBuilder.Append('/').Append('$').Append("count");
			}
			this.VisitQueryOptions(rse);
			return rse;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017CC8 File Offset: 0x00015EC8
		internal void VisitOperationInvocation(QueryableResourceExpression rse)
		{
			if (!this.uriBuilder.ToString().EndsWith('/'.ToString(), StringComparison.Ordinal))
			{
				this.uriBuilder.Append('/');
			}
			if (rse.IsOperationInvocation)
			{
				this.uriBuilder.Append(rse.OperationName);
				if (rse.IsAction)
				{
					return;
				}
				this.uriBuilder.Append('(');
				bool flag = false;
				foreach (KeyValuePair<string, string> keyValuePair in rse.OperationParameters.ToArray<KeyValuePair<string, string>>())
				{
					if (flag)
					{
						this.uriBuilder.Append(',');
					}
					this.uriBuilder.Append(keyValuePair.Key);
					this.uriBuilder.Append('=');
					if (!UriHelper.IsPrimitiveValue(keyValuePair.Value))
					{
						string text = "@" + keyValuePair.Key;
						int num = 1;
						while (this.alias.ContainsKey(text))
						{
							text = "@" + keyValuePair.Key + num;
							num++;
						}
						this.uriBuilder.Append(text);
						this.alias.Add(text, keyValuePair.Value);
					}
					else
					{
						this.uriBuilder.Append(keyValuePair.Value);
					}
					flag = true;
				}
				this.uriBuilder.Append(')');
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00017E30 File Offset: 0x00016030
		internal void VisitQueryOptions(ResourceExpression re)
		{
			if (re.HasQueryOptions)
			{
				this.uriBuilder.Append('?');
				QueryableResourceExpression queryableResourceExpression = re as QueryableResourceExpression;
				if (queryableResourceExpression != null)
				{
					foreach (object obj in queryableResourceExpression.SequenceQueryOptions)
					{
						Expression expression = (Expression)obj;
						switch (expression.NodeType)
						{
						case (ExpressionType)10004:
							this.VisitQueryOptionExpression((TakeQueryOptionExpression)expression);
							break;
						case (ExpressionType)10005:
							this.VisitQueryOptionExpression((SkipQueryOptionExpression)expression);
							break;
						case (ExpressionType)10006:
							this.VisitQueryOptionExpression((OrderByQueryOptionExpression)expression);
							break;
						case (ExpressionType)10007:
							this.VisitQueryOptionExpression((FilterQueryOptionExpression)expression);
							break;
						}
					}
				}
				if (re.ExpandPaths.Count > 0)
				{
					this.VisitExpandOptions(re.ExpandPaths);
				}
				if (re.Projection != null && re.Projection.Paths.Count > 0)
				{
					this.VisitProjectionPaths(re.Projection.Paths);
				}
				if (re.CountOption == CountOption.CountQuery)
				{
					this.VisitCountQueryOptions();
				}
				if (re.CustomQueryOptions.Count > 0)
				{
					this.VisitCustomQueryOptions(re.CustomQueryOptions);
				}
				this.AppendCachedQueryOptionsToUriBuilder();
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017F52 File Offset: 0x00016152
		internal void VisitQueryOptionExpression(SkipQueryOptionExpression sqoe)
		{
			this.AddAsCachedQueryOption("$skip", this.ExpressionToString(sqoe.SkipAmount, false));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017F6C File Offset: 0x0001616C
		internal void VisitQueryOptionExpression(TakeQueryOptionExpression tqoe)
		{
			this.AddAsCachedQueryOption("$top", this.ExpressionToString(tqoe.TakeAmount, false));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017F86 File Offset: 0x00016186
		internal void VisitQueryOptionExpression(FilterQueryOptionExpression fqoe)
		{
			this.AddAsCachedQueryOption("$filter", this.ExpressionToString(fqoe.GetPredicate(), false));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00017FA0 File Offset: 0x000161A0
		internal void VisitQueryOptionExpression(OrderByQueryOptionExpression oboe)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				OrderByQueryOptionExpression.Selector selector = oboe.Selectors[num];
				stringBuilder.Append(this.ExpressionToString(selector.Expression, false));
				if (selector.Descending)
				{
					stringBuilder.Append(' ');
					stringBuilder.Append("desc");
				}
				if (++num == oboe.Selectors.Count)
				{
					break;
				}
				stringBuilder.Append(',');
			}
			this.AddAsCachedQueryOption("$orderby", stringBuilder.ToString());
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00018024 File Offset: 0x00016224
		internal void VisitExpandOptions(List<string> paths)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				stringBuilder.Append(paths[num]);
				if (++num == paths.Count)
				{
					break;
				}
				stringBuilder.Append(',');
			}
			this.AddAsCachedQueryOption("$expand", stringBuilder.ToString());
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00018070 File Offset: 0x00016270
		internal void VisitProjectionPaths(List<string> paths)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				string text = paths[num];
				stringBuilder.Append(text);
				if (++num == paths.Count)
				{
					break;
				}
				stringBuilder.Append(',');
			}
			this.AddAsCachedQueryOption("$select", stringBuilder.ToString());
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000180BE File Offset: 0x000162BE
		internal void VisitCountQueryOptions()
		{
			this.AddAsCachedQueryOption("$count", "true");
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000180D0 File Offset: 0x000162D0
		internal void VisitCustomQueryOptions(Dictionary<ConstantExpression, ConstantExpression> options)
		{
			List<ConstantExpression> list = options.Keys.ToList<ConstantExpression>();
			List<ConstantExpression> list2 = options.Values.ToList<ConstantExpression>();
			for (int i = 0; i < list.Count; i++)
			{
				string text = string.Concat(list[i].Value);
				string text2 = string.Concat(list2[i].Value);
				this.AddAsCachedQueryOption(text, text2);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00018134 File Offset: 0x00016334
		private void AddAsCachedQueryOption(string optionKey, string optionValue)
		{
			List<string> list = null;
			if (!this.cachedQueryOptions.TryGetValue(optionKey, out list))
			{
				list = new List<string>();
				this.cachedQueryOptions.Add(optionKey, list);
			}
			list.Add(optionValue);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00018170 File Offset: 0x00016370
		private void AppendCachedQueryOptionsToUriBuilder()
		{
			int num = 0;
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.cachedQueryOptions)
			{
				if (num++ != 0)
				{
					this.uriBuilder.Append('&');
				}
				string key = keyValuePair.Key;
				string text = string.Join(",", keyValuePair.Value);
				this.uriBuilder.Append(key);
				this.uriBuilder.Append('=');
				this.uriBuilder.Append(text);
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00018218 File Offset: 0x00016418
		private string ExpressionToString(Expression expression, bool inPath)
		{
			return ExpressionWriter.ExpressionToString(this.context, expression, inPath, ref this.uriVersion);
		}

		// Token: 0x0400027B RID: 635
		private readonly DataServiceContext context;

		// Token: 0x0400027C RID: 636
		private readonly StringBuilder uriBuilder = new StringBuilder();

		// Token: 0x0400027D RID: 637
		private readonly Dictionary<string, string> alias = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x0400027E RID: 638
		private Version uriVersion;

		// Token: 0x0400027F RID: 639
		private Dictionary<string, List<string>> cachedQueryOptions = new Dictionary<string, List<string>>(StringComparer.Ordinal);
	}
}
