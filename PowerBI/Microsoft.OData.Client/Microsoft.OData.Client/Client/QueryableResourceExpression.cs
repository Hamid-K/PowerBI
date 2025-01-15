using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.OData.Client
{
	// Token: 0x02000016 RID: 22
	[DebuggerDisplay("QueryableResourceExpression {Source}.{MemberExpression}")]
	internal abstract class QueryableResourceExpression : ResourceExpression
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004400 File Offset: 0x00002600
		internal QueryableResourceExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion)
			: this(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion, null, null, false)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004428 File Offset: 0x00002628
		internal QueryableResourceExpression(Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion, string operationName, Dictionary<string, string> operationParameters, bool isAction)
			: base(source, type, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion, operationName, operationParameters, isAction)
		{
			this.member = memberExpression;
			this.resourceType = resourceType;
			this.sequenceQueryOptions = new List<QueryOptionExpression>();
			this.keyPredicateConjuncts = new List<Expression>();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004474 File Offset: 0x00002674
		internal Expression MemberExpression
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000447C File Offset: 0x0000267C
		internal override Type ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004484 File Offset: 0x00002684
		internal bool HasTransparentScope
		{
			get
			{
				return this.transparentScope != null;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000448F File Offset: 0x0000268F
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004497 File Offset: 0x00002697
		internal QueryableResourceExpression.TransparentAccessors TransparentScope
		{
			get
			{
				return this.transparentScope;
			}
			set
			{
				this.transparentScope = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000044A0 File Offset: 0x000026A0
		internal ReadOnlyCollection<Expression> KeyPredicateConjuncts
		{
			get
			{
				return new ReadOnlyCollection<Expression>(this.keyPredicateConjuncts);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000044AD File Offset: 0x000026AD
		internal override bool HasQueryOptions
		{
			get
			{
				return this.sequenceQueryOptions.Count > 0 || this.ExpandPaths.Count > 0 || this.CountOption == CountOption.CountQuery || this.CustomQueryOptions.Count > 0 || base.Projection != null;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000044ED File Offset: 0x000026ED
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000044F5 File Offset: 0x000026F5
		internal bool UseFilterAsPredicate { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000044FE File Offset: 0x000026FE
		internal FilterQueryOptionExpression Filter
		{
			get
			{
				return this.sequenceQueryOptions.OfType<FilterQueryOptionExpression>().SingleOrDefault<FilterQueryOptionExpression>();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004510 File Offset: 0x00002710
		internal OrderByQueryOptionExpression OrderBy
		{
			get
			{
				return this.sequenceQueryOptions.OfType<OrderByQueryOptionExpression>().SingleOrDefault<OrderByQueryOptionExpression>();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004522 File Offset: 0x00002722
		internal SkipQueryOptionExpression Skip
		{
			get
			{
				return this.sequenceQueryOptions.OfType<SkipQueryOptionExpression>().SingleOrDefault<SkipQueryOptionExpression>();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004534 File Offset: 0x00002734
		internal TakeQueryOptionExpression Take
		{
			get
			{
				return this.sequenceQueryOptions.OfType<TakeQueryOptionExpression>().SingleOrDefault<TakeQueryOptionExpression>();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004546 File Offset: 0x00002746
		internal IEnumerable<QueryOptionExpression> SequenceQueryOptions
		{
			get
			{
				return this.sequenceQueryOptions.ToList<QueryOptionExpression>();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004553 File Offset: 0x00002753
		internal bool HasSequenceQueryOptions
		{
			get
			{
				return this.sequenceQueryOptions.Count > 0;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004564 File Offset: 0x00002764
		internal override ResourceExpression CreateCloneWithNewType(Type type)
		{
			QueryableResourceExpression queryableResourceExpression = this.CreateCloneWithNewTypes(type, TypeSystem.GetElementType(type));
			if (this.keyPredicateConjuncts != null && this.keyPredicateConjuncts.Count > 0)
			{
				queryableResourceExpression.SetKeyPredicate(this.keyPredicateConjuncts);
			}
			queryableResourceExpression.keyFilter = this.keyFilter;
			queryableResourceExpression.sequenceQueryOptions = this.sequenceQueryOptions;
			queryableResourceExpression.transparentScope = this.transparentScope;
			return queryableResourceExpression;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000045C8 File Offset: 0x000027C8
		internal static QueryableResourceExpression CreateNavigationResourceExpression(ExpressionType expressionType, Type type, Expression source, Expression memberExpression, Type resourceType, List<string> expandPaths, CountOption countOption, Dictionary<ConstantExpression, ConstantExpression> customQueryOptions, ProjectionQueryOptionExpression projection, Type resourceTypeAs, Version uriVersion, string operationName, Dictionary<string, string> operationParameters)
		{
			QueryableResourceExpression queryableResourceExpression = null;
			if (expressionType == (ExpressionType)10000 || expressionType == (ExpressionType)10002)
			{
				queryableResourceExpression = new ResourceSetExpression(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion);
			}
			if (expressionType == (ExpressionType)10001)
			{
				queryableResourceExpression = new SingletonResourceExpression(type, source, memberExpression, resourceType, expandPaths, countOption, customQueryOptions, projection, resourceTypeAs, uriVersion);
			}
			if (queryableResourceExpression != null)
			{
				queryableResourceExpression.OperationName = operationName;
				queryableResourceExpression.OperationParameters = operationParameters;
				return queryableResourceExpression;
			}
			return null;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004634 File Offset: 0x00002834
		internal QueryableResourceExpression CreateCloneForTransparentScope(Type type)
		{
			Type elementType = TypeSystem.GetElementType(type);
			Type type2 = typeof(IOrderedQueryable<>).MakeGenericType(new Type[] { elementType });
			QueryableResourceExpression queryableResourceExpression = this.CreateCloneWithNewTypes(type2, this.ResourceType);
			if (this.keyPredicateConjuncts != null && this.keyPredicateConjuncts.Count > 0)
			{
				queryableResourceExpression.SetKeyPredicate(this.keyPredicateConjuncts);
			}
			queryableResourceExpression.keyFilter = this.keyFilter;
			queryableResourceExpression.sequenceQueryOptions = this.sequenceQueryOptions;
			queryableResourceExpression.transparentScope = this.transparentScope;
			return queryableResourceExpression;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000046B7 File Offset: 0x000028B7
		internal void ConvertKeyToFilterExpression()
		{
			if (this.keyPredicateConjuncts.Count > 0)
			{
				this.AddFilter(this.keyPredicateConjuncts);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000046D4 File Offset: 0x000028D4
		internal void AddFilter(IEnumerable<Expression> predicateConjuncts)
		{
			if (this.Skip != null)
			{
				throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "skip"));
			}
			if (this.Take != null)
			{
				throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "top"));
			}
			if (base.Projection != null)
			{
				throw new NotSupportedException(Strings.ALinq_QueryOptionOutOfOrder("filter", "select"));
			}
			if (this.Filter == null)
			{
				this.AddSequenceQueryOption(new FilterQueryOptionExpression(this.Type));
			}
			this.Filter.AddPredicateConjuncts(predicateConjuncts);
			this.keyPredicateConjuncts.Clear();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004768 File Offset: 0x00002968
		internal void AddSequenceQueryOption(QueryOptionExpression qoe)
		{
			QueryOptionExpression queryOptionExpression = this.sequenceQueryOptions.Where((QueryOptionExpression o) => o.GetType() == qoe.GetType()).FirstOrDefault<QueryOptionExpression>();
			if (queryOptionExpression != null)
			{
				qoe = qoe.ComposeMultipleSpecification(queryOptionExpression);
				this.sequenceQueryOptions.Remove(queryOptionExpression);
			}
			this.sequenceQueryOptions.Add(qoe);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000047D2 File Offset: 0x000029D2
		internal void RemoveFilterExpression()
		{
			if (this.Filter != null)
			{
				this.sequenceQueryOptions.Remove(this.Filter);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000047F0 File Offset: 0x000029F0
		internal void OverrideInputReference(QueryableResourceExpression newInput)
		{
			InputReferenceExpression inputRef = newInput.inputRef;
			if (inputRef != null)
			{
				this.inputRef = inputRef;
				inputRef.OverrideTarget(this);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004818 File Offset: 0x00002A18
		internal void SetKeyPredicate(IEnumerable<Expression> keyValues)
		{
			this.keyPredicateConjuncts.Clear();
			foreach (Expression expression in keyValues)
			{
				this.keyPredicateConjuncts.Add(expression);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004870 File Offset: 0x00002A70
		internal Dictionary<PropertyInfo, ConstantExpression> GetKeyProperties()
		{
			Dictionary<PropertyInfo, ConstantExpression> dictionary = new Dictionary<PropertyInfo, ConstantExpression>(EqualityComparer<PropertyInfo>.Default);
			if (this.keyPredicateConjuncts.Count > 0)
			{
				foreach (Expression expression in this.keyPredicateConjuncts)
				{
					PropertyInfo propertyInfo;
					ConstantExpression constantExpression;
					if (ResourceBinder.PatternRules.MatchKeyComparison(expression, out propertyInfo, out constantExpression))
					{
						dictionary.Add(propertyInfo, constantExpression);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060000B0 RID: 176
		protected abstract QueryableResourceExpression CreateCloneWithNewTypes(Type newType, Type newResourceType);

		// Token: 0x04000037 RID: 55
		private readonly List<Expression> keyPredicateConjuncts;

		// Token: 0x04000038 RID: 56
		private readonly Type resourceType;

		// Token: 0x04000039 RID: 57
		private readonly Expression member;

		// Token: 0x0400003A RID: 58
		private Dictionary<PropertyInfo, ConstantExpression> keyFilter;

		// Token: 0x0400003B RID: 59
		private List<QueryOptionExpression> sequenceQueryOptions;

		// Token: 0x0400003C RID: 60
		private QueryableResourceExpression.TransparentAccessors transparentScope;

		// Token: 0x02000149 RID: 329
		[DebuggerDisplay("{ToString()}")]
		internal class TransparentAccessors
		{
			// Token: 0x06000CF7 RID: 3319 RVA: 0x0002D9F1 File Offset: 0x0002BBF1
			internal TransparentAccessors(string acc, Dictionary<string, Expression> sourceAccesors)
			{
				this.Accessor = acc;
				this.SourceAccessors = sourceAccesors;
			}

			// Token: 0x06000CF8 RID: 3320 RVA: 0x0002DA08 File Offset: 0x0002BC08
			public override string ToString()
			{
				string text = "SourceAccessors=[" + string.Join(",", this.SourceAccessors.Keys.ToArray<string>());
				return text + "] ->* Accessor=" + this.Accessor;
			}

			// Token: 0x040006BE RID: 1726
			internal readonly string Accessor;

			// Token: 0x040006BF RID: 1727
			internal readonly Dictionary<string, Expression> SourceAccessors;
		}
	}
}
