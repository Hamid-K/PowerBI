using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Expressions;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200018F RID: 399
	public class ETag : DynamicObject
	{
		// Token: 0x06000D05 RID: 3333 RVA: 0x00033F4E File Offset: 0x0003214E
		public ETag()
		{
			this.IsWellFormed = true;
		}

		// Token: 0x17000390 RID: 912
		public object this[string key]
		{
			get
			{
				if (!this.IsWellFormed)
				{
					throw Error.InvalidOperation(SRResources.ETagNotWellFormed, new object[0]);
				}
				return this.ConcurrencyProperties[key];
			}
			set
			{
				this.ConcurrencyProperties[key] = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00033F9E File Offset: 0x0003219E
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00033FA6 File Offset: 0x000321A6
		public bool IsWellFormed { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00033FAF File Offset: 0x000321AF
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00033FB7 File Offset: 0x000321B7
		public Type EntityType { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00033FC0 File Offset: 0x000321C0
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00033FC8 File Offset: 0x000321C8
		public bool IsAny { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00033FD1 File Offset: 0x000321D1
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00033FD9 File Offset: 0x000321D9
		public bool IsIfNoneMatch { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00033FE2 File Offset: 0x000321E2
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00033FEA File Offset: 0x000321EA
		internal IDictionary<string, object> ConcurrencyProperties
		{
			get
			{
				return this._concurrencyProperties;
			}
			set
			{
				this._concurrencyProperties = value;
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00033FF4 File Offset: 0x000321F4
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (binder == null)
			{
				throw Error.ArgumentNull("binder");
			}
			if (!this.IsWellFormed)
			{
				throw Error.InvalidOperation(SRResources.ETagNotWellFormed, new object[0]);
			}
			string name = binder.Name;
			return this.ConcurrencyProperties.TryGetValue(name, out result);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003403C File Offset: 0x0003223C
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			if (binder == null)
			{
				throw Error.ArgumentNull("binder");
			}
			this.ConcurrencyProperties[binder.Name] = value;
			return true;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00034060 File Offset: 0x00032260
		public virtual IQueryable ApplyTo(IQueryable query)
		{
			if (this.IsAny)
			{
				return query;
			}
			Type entityType = this.EntityType;
			ParameterExpression parameterExpression = Expression.Parameter(entityType);
			Expression expression = null;
			foreach (KeyValuePair<string, object> keyValuePair in this.ConcurrencyProperties)
			{
				Expression expression2 = Expression.Property(parameterExpression, keyValuePair.Key);
				object value = keyValuePair.Value;
				Expression expression3 = ((value != null) ? LinqParameterContainer.Parameterize(value.GetType(), value) : Expression.Constant(null));
				BinaryExpression binaryExpression = Expression.Equal(expression2, expression3);
				expression = ((expression == null) ? binaryExpression : Expression.AndAlso(expression, binaryExpression));
			}
			if (expression == null)
			{
				return query;
			}
			if (this.IsIfNoneMatch)
			{
				expression = Expression.Not(expression);
			}
			Expression expression4 = Expression.Lambda(expression, new ParameterExpression[] { parameterExpression });
			return ExpressionHelpers.Where(query, expression4, entityType);
		}

		// Token: 0x040003B6 RID: 950
		private IDictionary<string, object> _concurrencyProperties = new Dictionary<string, object>();
	}
}
