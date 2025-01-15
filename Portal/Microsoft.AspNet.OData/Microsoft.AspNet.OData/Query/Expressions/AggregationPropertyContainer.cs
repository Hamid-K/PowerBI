using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E3 RID: 227
	internal class AggregationPropertyContainer : PropertyContainer.NamedProperty<object>
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001B900 File Offset: 0x00019B00
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0001B90D File Offset: 0x00019B0D
		public GroupByWrapper NestedValue
		{
			get
			{
				return (GroupByWrapper)base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001B916 File Offset: 0x00019B16
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0001B91E File Offset: 0x00019B1E
		public AggregationPropertyContainer Next { get; set; }

		// Token: 0x0600079D RID: 1949 RVA: 0x0001B927 File Offset: 0x00019B27
		public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
		{
			base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			if (this.Next != null)
			{
				this.Next.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001B948 File Offset: 0x00019B48
		public override object GetValue()
		{
			if (base.Value == DBNull.Value)
			{
				return null;
			}
			return base.GetValue();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001B960 File Offset: 0x00019B60
		public static Expression CreateNextNamedPropertyContainer(IList<NamedPropertyExpression> properties)
		{
			Expression expression = null;
			foreach (NamedPropertyExpression namedPropertyExpression in properties)
			{
				expression = AggregationPropertyContainer.CreateNextNamedPropertyCreationExpression(namedPropertyExpression, expression);
			}
			return expression;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001B9AC File Offset: 0x00019BAC
		private static Expression CreateNextNamedPropertyCreationExpression(NamedPropertyExpression property, Expression next)
		{
			Type type;
			if (next != null)
			{
				if (property.Value.Type == typeof(GroupByWrapper))
				{
					type = typeof(AggregationPropertyContainer.NestedProperty);
				}
				else
				{
					type = typeof(AggregationPropertyContainer);
				}
			}
			else if (property.Value.Type == typeof(GroupByWrapper))
			{
				type = typeof(AggregationPropertyContainer.NestedPropertyLastInChain);
			}
			else
			{
				type = typeof(AggregationPropertyContainer.LastInChain);
			}
			List<MemberBinding> list = new List<MemberBinding>();
			list.Add(Expression.Bind(type.GetProperty("Name"), property.Name));
			if (property.Value.Type == typeof(GroupByWrapper))
			{
				list.Add(Expression.Bind(type.GetProperty("NestedValue"), property.Value));
			}
			else
			{
				list.Add(Expression.Bind(type.GetProperty("Value"), property.Value));
			}
			if (next != null)
			{
				list.Add(Expression.Bind(type.GetProperty("Next"), next));
			}
			if (property.NullCheck != null)
			{
				list.Add(Expression.Bind(type.GetProperty("IsNull"), property.NullCheck));
			}
			return Expression.MemberInit(Expression.New(type), list);
		}

		// Token: 0x020002AC RID: 684
		private class LastInChain : AggregationPropertyContainer
		{
		}

		// Token: 0x020002AD RID: 685
		private class NestedPropertyLastInChain : AggregationPropertyContainer
		{
		}

		// Token: 0x020002AE RID: 686
		private class NestedProperty : AggregationPropertyContainer
		{
		}
	}
}
