using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E1 RID: 225
	internal abstract class PropertyContainer
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x0001AD08 File Offset: 0x00018F08
		public Dictionary<string, object> ToDictionary(IPropertyMapper propertyMapper, bool includeAutoSelected = true)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			this.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			return dictionary;
		}

		// Token: 0x0600078F RID: 1935
		public abstract void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected);

		// Token: 0x06000790 RID: 1936 RVA: 0x0001AD28 File Offset: 0x00018F28
		public static Expression CreatePropertyContainer(IList<NamedPropertyExpression> properties)
		{
			Expression expression = null;
			if (properties.Count >= 1)
			{
				NamedPropertyExpression namedPropertyExpression = properties.First<NamedPropertyExpression>();
				int num = properties.Count - 1;
				List<Expression> list = new List<Expression>();
				int num2 = PropertyContainer.SingleExpandedPropertyTypes.Count - 1;
				int num3 = 0;
				for (int i = num2; i > 0; i--)
				{
					int leftSize = PropertyContainer.GetLeftSize(num - num3, i);
					list.Add(PropertyContainer.CreatePropertyContainer(properties.Skip(1 + num3).Take(leftSize).ToList<NamedPropertyExpression>()));
					num3 += leftSize;
				}
				expression = PropertyContainer.CreateNamedPropertyCreationExpression(namedPropertyExpression, list.Where((Expression e) => e != null).ToList<Expression>());
			}
			return expression;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001ADDE File Offset: 0x00018FDE
		private static int GetLeftSize(int count, int parts)
		{
			if (count % parts != 0)
			{
				return count / parts + 1;
			}
			return count / parts;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		private static Expression CreateNamedPropertyCreationExpression(NamedPropertyExpression property, IList<Expression> expressions)
		{
			Type namedPropertyType = PropertyContainer.GetNamedPropertyType(property, expressions);
			List<MemberBinding> list = new List<MemberBinding>();
			list.Add(Expression.Bind(namedPropertyType.GetProperty("Name"), property.Name));
			if (property.PageSize != null || property.CountOption != null)
			{
				list.Add(Expression.Bind(namedPropertyType.GetProperty("Collection"), property.Value));
				if (property.PageSize != null)
				{
					list.Add(Expression.Bind(namedPropertyType.GetProperty("PageSize"), Expression.Constant(property.PageSize)));
				}
				if (property.CountOption != null && property.CountOption.Value)
				{
					list.Add(Expression.Bind(namedPropertyType.GetProperty("TotalCount"), ExpressionHelpers.ToNullable(property.TotalCount)));
				}
			}
			else
			{
				list.Add(Expression.Bind(namedPropertyType.GetProperty("Value"), property.Value));
			}
			for (int i = 0; i < expressions.Count; i++)
			{
				list.Add(Expression.Bind(namedPropertyType.GetProperty("Next" + i.ToString(CultureInfo.CurrentCulture)), expressions[i]));
			}
			if (property.NullCheck != null)
			{
				list.Add(Expression.Bind(namedPropertyType.GetProperty("IsNull"), property.NullCheck));
			}
			return Expression.MemberInit(Expression.New(namedPropertyType), list);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001AF70 File Offset: 0x00019170
		private static Type GetNamedPropertyType(NamedPropertyExpression property, IList<Expression> expressions)
		{
			Type type;
			if (property.NullCheck != null)
			{
				type = PropertyContainer.SingleExpandedPropertyTypes[expressions.Count];
			}
			else if (property.PageSize != null || property.CountOption != null)
			{
				type = PropertyContainer.CollectionExpandedPropertyTypes[expressions.Count];
			}
			else if (property.AutoSelected)
			{
				type = PropertyContainer.AutoSelectedNamedPropertyTypes[expressions.Count];
			}
			else
			{
				type = PropertyContainer.NamedPropertyTypes[expressions.Count];
			}
			Type type2 = ((property.PageSize == null && property.CountOption == null) ? property.Value.Type : TypeHelper.GetInnerElementType(property.Value.Type));
			return type.MakeGenericType(new Type[] { type2 });
		}

		// Token: 0x0400023A RID: 570
		private static List<Type> SingleExpandedPropertyTypes = new List<Type>
		{
			typeof(PropertyContainer.SingleExpandedProperty<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext0<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext1<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext2<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext3<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext4<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext5<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext6<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext7<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext8<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext9<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext10<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext11<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext12<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext13<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext14<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext15<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext16<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext17<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext18<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext19<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext20<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext21<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext22<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext23<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext24<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext25<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext26<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext27<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext28<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext29<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext30<>),
			typeof(PropertyContainer.SingleExpandedPropertyWithNext31<>)
		};

		// Token: 0x0400023B RID: 571
		private static List<Type> CollectionExpandedPropertyTypes = new List<Type>
		{
			typeof(PropertyContainer.CollectionExpandedProperty<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext0<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext1<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext2<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext3<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext4<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext5<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext6<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext7<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext8<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext9<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext10<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext11<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext12<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext13<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext14<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext15<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext16<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext17<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext18<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext19<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext20<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext21<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext22<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext23<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext24<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext25<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext26<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext27<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext28<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext29<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext30<>),
			typeof(PropertyContainer.CollectionExpandedPropertyWithNext31<>)
		};

		// Token: 0x0400023C RID: 572
		private static List<Type> AutoSelectedNamedPropertyTypes = new List<Type>
		{
			typeof(PropertyContainer.AutoSelectedNamedProperty<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext0<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext1<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext2<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext3<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext4<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext5<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext6<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext7<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext8<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext9<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext10<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext11<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext12<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext13<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext14<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext15<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext16<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext17<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext18<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext19<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext20<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext21<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext22<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext23<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext24<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext25<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext26<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext27<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext28<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext29<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext30<>),
			typeof(PropertyContainer.AutoSelectedNamedPropertyWithNext31<>)
		};

		// Token: 0x0400023D RID: 573
		private static List<Type> NamedPropertyTypes = new List<Type>
		{
			typeof(PropertyContainer.NamedProperty<>),
			typeof(PropertyContainer.NamedPropertyWithNext0<>),
			typeof(PropertyContainer.NamedPropertyWithNext1<>),
			typeof(PropertyContainer.NamedPropertyWithNext2<>),
			typeof(PropertyContainer.NamedPropertyWithNext3<>),
			typeof(PropertyContainer.NamedPropertyWithNext4<>),
			typeof(PropertyContainer.NamedPropertyWithNext5<>),
			typeof(PropertyContainer.NamedPropertyWithNext6<>),
			typeof(PropertyContainer.NamedPropertyWithNext7<>),
			typeof(PropertyContainer.NamedPropertyWithNext8<>),
			typeof(PropertyContainer.NamedPropertyWithNext9<>),
			typeof(PropertyContainer.NamedPropertyWithNext10<>),
			typeof(PropertyContainer.NamedPropertyWithNext11<>),
			typeof(PropertyContainer.NamedPropertyWithNext12<>),
			typeof(PropertyContainer.NamedPropertyWithNext13<>),
			typeof(PropertyContainer.NamedPropertyWithNext14<>),
			typeof(PropertyContainer.NamedPropertyWithNext15<>),
			typeof(PropertyContainer.NamedPropertyWithNext16<>),
			typeof(PropertyContainer.NamedPropertyWithNext17<>),
			typeof(PropertyContainer.NamedPropertyWithNext18<>),
			typeof(PropertyContainer.NamedPropertyWithNext19<>),
			typeof(PropertyContainer.NamedPropertyWithNext20<>),
			typeof(PropertyContainer.NamedPropertyWithNext21<>),
			typeof(PropertyContainer.NamedPropertyWithNext22<>),
			typeof(PropertyContainer.NamedPropertyWithNext23<>),
			typeof(PropertyContainer.NamedPropertyWithNext24<>),
			typeof(PropertyContainer.NamedPropertyWithNext25<>),
			typeof(PropertyContainer.NamedPropertyWithNext26<>),
			typeof(PropertyContainer.NamedPropertyWithNext27<>),
			typeof(PropertyContainer.NamedPropertyWithNext28<>),
			typeof(PropertyContainer.NamedPropertyWithNext29<>),
			typeof(PropertyContainer.NamedPropertyWithNext30<>),
			typeof(PropertyContainer.NamedPropertyWithNext31<>)
		};

		// Token: 0x02000227 RID: 551
		private class SingleExpandedPropertyWithNext0<T> : PropertyContainer.SingleExpandedProperty<T>
		{
			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00040E2A File Offset: 0x0003F02A
			// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00040E32 File Offset: 0x0003F032
			public PropertyContainer Next0 { get; set; }

			// Token: 0x060010A5 RID: 4261 RVA: 0x00040E3B File Offset: 0x0003F03B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next0.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000228 RID: 552
		private class SingleExpandedPropertyWithNext1<T> : PropertyContainer.SingleExpandedPropertyWithNext0<T>
		{
			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00040E5C File Offset: 0x0003F05C
			// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00040E64 File Offset: 0x0003F064
			public PropertyContainer Next1 { get; set; }

			// Token: 0x060010A9 RID: 4265 RVA: 0x00040E6D File Offset: 0x0003F06D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next1.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000229 RID: 553
		private class SingleExpandedPropertyWithNext2<T> : PropertyContainer.SingleExpandedPropertyWithNext1<T>
		{
			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x060010AB RID: 4267 RVA: 0x00040E8E File Offset: 0x0003F08E
			// (set) Token: 0x060010AC RID: 4268 RVA: 0x00040E96 File Offset: 0x0003F096
			public PropertyContainer Next2 { get; set; }

			// Token: 0x060010AD RID: 4269 RVA: 0x00040E9F File Offset: 0x0003F09F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next2.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200022A RID: 554
		private class SingleExpandedPropertyWithNext3<T> : PropertyContainer.SingleExpandedPropertyWithNext2<T>
		{
			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x060010AF RID: 4271 RVA: 0x00040EC0 File Offset: 0x0003F0C0
			// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00040EC8 File Offset: 0x0003F0C8
			public PropertyContainer Next3 { get; set; }

			// Token: 0x060010B1 RID: 4273 RVA: 0x00040ED1 File Offset: 0x0003F0D1
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next3.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200022B RID: 555
		private class SingleExpandedPropertyWithNext4<T> : PropertyContainer.SingleExpandedPropertyWithNext3<T>
		{
			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00040EF2 File Offset: 0x0003F0F2
			// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00040EFA File Offset: 0x0003F0FA
			public PropertyContainer Next4 { get; set; }

			// Token: 0x060010B5 RID: 4277 RVA: 0x00040F03 File Offset: 0x0003F103
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next4.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200022C RID: 556
		private class SingleExpandedPropertyWithNext5<T> : PropertyContainer.SingleExpandedPropertyWithNext4<T>
		{
			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00040F24 File Offset: 0x0003F124
			// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00040F2C File Offset: 0x0003F12C
			public PropertyContainer Next5 { get; set; }

			// Token: 0x060010B9 RID: 4281 RVA: 0x00040F35 File Offset: 0x0003F135
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next5.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200022D RID: 557
		private class SingleExpandedPropertyWithNext6<T> : PropertyContainer.SingleExpandedPropertyWithNext5<T>
		{
			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x060010BB RID: 4283 RVA: 0x00040F56 File Offset: 0x0003F156
			// (set) Token: 0x060010BC RID: 4284 RVA: 0x00040F5E File Offset: 0x0003F15E
			public PropertyContainer Next6 { get; set; }

			// Token: 0x060010BD RID: 4285 RVA: 0x00040F67 File Offset: 0x0003F167
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next6.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200022E RID: 558
		private class SingleExpandedPropertyWithNext7<T> : PropertyContainer.SingleExpandedPropertyWithNext6<T>
		{
			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x060010BF RID: 4287 RVA: 0x00040F88 File Offset: 0x0003F188
			// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00040F90 File Offset: 0x0003F190
			public PropertyContainer Next7 { get; set; }

			// Token: 0x060010C1 RID: 4289 RVA: 0x00040F99 File Offset: 0x0003F199
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next7.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200022F RID: 559
		private class SingleExpandedPropertyWithNext8<T> : PropertyContainer.SingleExpandedPropertyWithNext7<T>
		{
			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00040FBA File Offset: 0x0003F1BA
			// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00040FC2 File Offset: 0x0003F1C2
			public PropertyContainer Next8 { get; set; }

			// Token: 0x060010C5 RID: 4293 RVA: 0x00040FCB File Offset: 0x0003F1CB
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next8.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000230 RID: 560
		private class SingleExpandedPropertyWithNext9<T> : PropertyContainer.SingleExpandedPropertyWithNext8<T>
		{
			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00040FEC File Offset: 0x0003F1EC
			// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00040FF4 File Offset: 0x0003F1F4
			public PropertyContainer Next9 { get; set; }

			// Token: 0x060010C9 RID: 4297 RVA: 0x00040FFD File Offset: 0x0003F1FD
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next9.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000231 RID: 561
		private class SingleExpandedPropertyWithNext10<T> : PropertyContainer.SingleExpandedPropertyWithNext9<T>
		{
			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004101E File Offset: 0x0003F21E
			// (set) Token: 0x060010CC RID: 4300 RVA: 0x00041026 File Offset: 0x0003F226
			public PropertyContainer Next10 { get; set; }

			// Token: 0x060010CD RID: 4301 RVA: 0x0004102F File Offset: 0x0003F22F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next10.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000232 RID: 562
		private class SingleExpandedPropertyWithNext11<T> : PropertyContainer.SingleExpandedPropertyWithNext10<T>
		{
			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x060010CF RID: 4303 RVA: 0x00041050 File Offset: 0x0003F250
			// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00041058 File Offset: 0x0003F258
			public PropertyContainer Next11 { get; set; }

			// Token: 0x060010D1 RID: 4305 RVA: 0x00041061 File Offset: 0x0003F261
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next11.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000233 RID: 563
		private class SingleExpandedPropertyWithNext12<T> : PropertyContainer.SingleExpandedPropertyWithNext11<T>
		{
			// Token: 0x1700041C RID: 1052
			// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00041082 File Offset: 0x0003F282
			// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0004108A File Offset: 0x0003F28A
			public PropertyContainer Next12 { get; set; }

			// Token: 0x060010D5 RID: 4309 RVA: 0x00041093 File Offset: 0x0003F293
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next12.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000234 RID: 564
		private class SingleExpandedPropertyWithNext13<T> : PropertyContainer.SingleExpandedPropertyWithNext12<T>
		{
			// Token: 0x1700041D RID: 1053
			// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000410B4 File Offset: 0x0003F2B4
			// (set) Token: 0x060010D8 RID: 4312 RVA: 0x000410BC File Offset: 0x0003F2BC
			public PropertyContainer Next13 { get; set; }

			// Token: 0x060010D9 RID: 4313 RVA: 0x000410C5 File Offset: 0x0003F2C5
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next13.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000235 RID: 565
		private class SingleExpandedPropertyWithNext14<T> : PropertyContainer.SingleExpandedPropertyWithNext13<T>
		{
			// Token: 0x1700041E RID: 1054
			// (get) Token: 0x060010DB RID: 4315 RVA: 0x000410E6 File Offset: 0x0003F2E6
			// (set) Token: 0x060010DC RID: 4316 RVA: 0x000410EE File Offset: 0x0003F2EE
			public PropertyContainer Next14 { get; set; }

			// Token: 0x060010DD RID: 4317 RVA: 0x000410F7 File Offset: 0x0003F2F7
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next14.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000236 RID: 566
		private class SingleExpandedPropertyWithNext15<T> : PropertyContainer.SingleExpandedPropertyWithNext14<T>
		{
			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x060010DF RID: 4319 RVA: 0x00041118 File Offset: 0x0003F318
			// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00041120 File Offset: 0x0003F320
			public PropertyContainer Next15 { get; set; }

			// Token: 0x060010E1 RID: 4321 RVA: 0x00041129 File Offset: 0x0003F329
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next15.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000237 RID: 567
		private class SingleExpandedPropertyWithNext16<T> : PropertyContainer.SingleExpandedPropertyWithNext15<T>
		{
			// Token: 0x17000420 RID: 1056
			// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0004114A File Offset: 0x0003F34A
			// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00041152 File Offset: 0x0003F352
			public PropertyContainer Next16 { get; set; }

			// Token: 0x060010E5 RID: 4325 RVA: 0x0004115B File Offset: 0x0003F35B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next16.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000238 RID: 568
		private class SingleExpandedPropertyWithNext17<T> : PropertyContainer.SingleExpandedPropertyWithNext16<T>
		{
			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0004117C File Offset: 0x0003F37C
			// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00041184 File Offset: 0x0003F384
			public PropertyContainer Next17 { get; set; }

			// Token: 0x060010E9 RID: 4329 RVA: 0x0004118D File Offset: 0x0003F38D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next17.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000239 RID: 569
		private class SingleExpandedPropertyWithNext18<T> : PropertyContainer.SingleExpandedPropertyWithNext17<T>
		{
			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x060010EB RID: 4331 RVA: 0x000411AE File Offset: 0x0003F3AE
			// (set) Token: 0x060010EC RID: 4332 RVA: 0x000411B6 File Offset: 0x0003F3B6
			public PropertyContainer Next18 { get; set; }

			// Token: 0x060010ED RID: 4333 RVA: 0x000411BF File Offset: 0x0003F3BF
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next18.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200023A RID: 570
		private class SingleExpandedPropertyWithNext19<T> : PropertyContainer.SingleExpandedPropertyWithNext18<T>
		{
			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x060010EF RID: 4335 RVA: 0x000411E0 File Offset: 0x0003F3E0
			// (set) Token: 0x060010F0 RID: 4336 RVA: 0x000411E8 File Offset: 0x0003F3E8
			public PropertyContainer Next19 { get; set; }

			// Token: 0x060010F1 RID: 4337 RVA: 0x000411F1 File Offset: 0x0003F3F1
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next19.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200023B RID: 571
		private class SingleExpandedPropertyWithNext20<T> : PropertyContainer.SingleExpandedPropertyWithNext19<T>
		{
			// Token: 0x17000424 RID: 1060
			// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00041212 File Offset: 0x0003F412
			// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0004121A File Offset: 0x0003F41A
			public PropertyContainer Next20 { get; set; }

			// Token: 0x060010F5 RID: 4341 RVA: 0x00041223 File Offset: 0x0003F423
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next20.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200023C RID: 572
		private class SingleExpandedPropertyWithNext21<T> : PropertyContainer.SingleExpandedPropertyWithNext20<T>
		{
			// Token: 0x17000425 RID: 1061
			// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00041244 File Offset: 0x0003F444
			// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0004124C File Offset: 0x0003F44C
			public PropertyContainer Next21 { get; set; }

			// Token: 0x060010F9 RID: 4345 RVA: 0x00041255 File Offset: 0x0003F455
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next21.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200023D RID: 573
		private class SingleExpandedPropertyWithNext22<T> : PropertyContainer.SingleExpandedPropertyWithNext21<T>
		{
			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x060010FB RID: 4347 RVA: 0x00041276 File Offset: 0x0003F476
			// (set) Token: 0x060010FC RID: 4348 RVA: 0x0004127E File Offset: 0x0003F47E
			public PropertyContainer Next22 { get; set; }

			// Token: 0x060010FD RID: 4349 RVA: 0x00041287 File Offset: 0x0003F487
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next22.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200023E RID: 574
		private class SingleExpandedPropertyWithNext23<T> : PropertyContainer.SingleExpandedPropertyWithNext22<T>
		{
			// Token: 0x17000427 RID: 1063
			// (get) Token: 0x060010FF RID: 4351 RVA: 0x000412A8 File Offset: 0x0003F4A8
			// (set) Token: 0x06001100 RID: 4352 RVA: 0x000412B0 File Offset: 0x0003F4B0
			public PropertyContainer Next23 { get; set; }

			// Token: 0x06001101 RID: 4353 RVA: 0x000412B9 File Offset: 0x0003F4B9
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next23.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200023F RID: 575
		private class SingleExpandedPropertyWithNext24<T> : PropertyContainer.SingleExpandedPropertyWithNext23<T>
		{
			// Token: 0x17000428 RID: 1064
			// (get) Token: 0x06001103 RID: 4355 RVA: 0x000412DA File Offset: 0x0003F4DA
			// (set) Token: 0x06001104 RID: 4356 RVA: 0x000412E2 File Offset: 0x0003F4E2
			public PropertyContainer Next24 { get; set; }

			// Token: 0x06001105 RID: 4357 RVA: 0x000412EB File Offset: 0x0003F4EB
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next24.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000240 RID: 576
		private class SingleExpandedPropertyWithNext25<T> : PropertyContainer.SingleExpandedPropertyWithNext24<T>
		{
			// Token: 0x17000429 RID: 1065
			// (get) Token: 0x06001107 RID: 4359 RVA: 0x0004130C File Offset: 0x0003F50C
			// (set) Token: 0x06001108 RID: 4360 RVA: 0x00041314 File Offset: 0x0003F514
			public PropertyContainer Next25 { get; set; }

			// Token: 0x06001109 RID: 4361 RVA: 0x0004131D File Offset: 0x0003F51D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next25.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000241 RID: 577
		private class SingleExpandedPropertyWithNext26<T> : PropertyContainer.SingleExpandedPropertyWithNext25<T>
		{
			// Token: 0x1700042A RID: 1066
			// (get) Token: 0x0600110B RID: 4363 RVA: 0x0004133E File Offset: 0x0003F53E
			// (set) Token: 0x0600110C RID: 4364 RVA: 0x00041346 File Offset: 0x0003F546
			public PropertyContainer Next26 { get; set; }

			// Token: 0x0600110D RID: 4365 RVA: 0x0004134F File Offset: 0x0003F54F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next26.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000242 RID: 578
		private class SingleExpandedPropertyWithNext27<T> : PropertyContainer.SingleExpandedPropertyWithNext26<T>
		{
			// Token: 0x1700042B RID: 1067
			// (get) Token: 0x0600110F RID: 4367 RVA: 0x00041370 File Offset: 0x0003F570
			// (set) Token: 0x06001110 RID: 4368 RVA: 0x00041378 File Offset: 0x0003F578
			public PropertyContainer Next27 { get; set; }

			// Token: 0x06001111 RID: 4369 RVA: 0x00041381 File Offset: 0x0003F581
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next27.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000243 RID: 579
		private class SingleExpandedPropertyWithNext28<T> : PropertyContainer.SingleExpandedPropertyWithNext27<T>
		{
			// Token: 0x1700042C RID: 1068
			// (get) Token: 0x06001113 RID: 4371 RVA: 0x000413A2 File Offset: 0x0003F5A2
			// (set) Token: 0x06001114 RID: 4372 RVA: 0x000413AA File Offset: 0x0003F5AA
			public PropertyContainer Next28 { get; set; }

			// Token: 0x06001115 RID: 4373 RVA: 0x000413B3 File Offset: 0x0003F5B3
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next28.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000244 RID: 580
		private class SingleExpandedPropertyWithNext29<T> : PropertyContainer.SingleExpandedPropertyWithNext28<T>
		{
			// Token: 0x1700042D RID: 1069
			// (get) Token: 0x06001117 RID: 4375 RVA: 0x000413D4 File Offset: 0x0003F5D4
			// (set) Token: 0x06001118 RID: 4376 RVA: 0x000413DC File Offset: 0x0003F5DC
			public PropertyContainer Next29 { get; set; }

			// Token: 0x06001119 RID: 4377 RVA: 0x000413E5 File Offset: 0x0003F5E5
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next29.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000245 RID: 581
		private class SingleExpandedPropertyWithNext30<T> : PropertyContainer.SingleExpandedPropertyWithNext29<T>
		{
			// Token: 0x1700042E RID: 1070
			// (get) Token: 0x0600111B RID: 4379 RVA: 0x00041406 File Offset: 0x0003F606
			// (set) Token: 0x0600111C RID: 4380 RVA: 0x0004140E File Offset: 0x0003F60E
			public PropertyContainer Next30 { get; set; }

			// Token: 0x0600111D RID: 4381 RVA: 0x00041417 File Offset: 0x0003F617
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next30.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000246 RID: 582
		private class SingleExpandedPropertyWithNext31<T> : PropertyContainer.SingleExpandedPropertyWithNext30<T>
		{
			// Token: 0x1700042F RID: 1071
			// (get) Token: 0x0600111F RID: 4383 RVA: 0x00041438 File Offset: 0x0003F638
			// (set) Token: 0x06001120 RID: 4384 RVA: 0x00041440 File Offset: 0x0003F640
			public PropertyContainer Next31 { get; set; }

			// Token: 0x06001121 RID: 4385 RVA: 0x00041449 File Offset: 0x0003F649
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next31.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000247 RID: 583
		private class CollectionExpandedPropertyWithNext0<T> : PropertyContainer.CollectionExpandedProperty<T>
		{
			// Token: 0x17000430 RID: 1072
			// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004146A File Offset: 0x0003F66A
			// (set) Token: 0x06001124 RID: 4388 RVA: 0x00041472 File Offset: 0x0003F672
			public PropertyContainer Next0 { get; set; }

			// Token: 0x06001125 RID: 4389 RVA: 0x0004147B File Offset: 0x0003F67B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next0.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000248 RID: 584
		private class CollectionExpandedPropertyWithNext1<T> : PropertyContainer.CollectionExpandedPropertyWithNext0<T>
		{
			// Token: 0x17000431 RID: 1073
			// (get) Token: 0x06001127 RID: 4391 RVA: 0x0004149C File Offset: 0x0003F69C
			// (set) Token: 0x06001128 RID: 4392 RVA: 0x000414A4 File Offset: 0x0003F6A4
			public PropertyContainer Next1 { get; set; }

			// Token: 0x06001129 RID: 4393 RVA: 0x000414AD File Offset: 0x0003F6AD
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next1.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000249 RID: 585
		private class CollectionExpandedPropertyWithNext2<T> : PropertyContainer.CollectionExpandedPropertyWithNext1<T>
		{
			// Token: 0x17000432 RID: 1074
			// (get) Token: 0x0600112B RID: 4395 RVA: 0x000414CE File Offset: 0x0003F6CE
			// (set) Token: 0x0600112C RID: 4396 RVA: 0x000414D6 File Offset: 0x0003F6D6
			public PropertyContainer Next2 { get; set; }

			// Token: 0x0600112D RID: 4397 RVA: 0x000414DF File Offset: 0x0003F6DF
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next2.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200024A RID: 586
		private class CollectionExpandedPropertyWithNext3<T> : PropertyContainer.CollectionExpandedPropertyWithNext2<T>
		{
			// Token: 0x17000433 RID: 1075
			// (get) Token: 0x0600112F RID: 4399 RVA: 0x00041500 File Offset: 0x0003F700
			// (set) Token: 0x06001130 RID: 4400 RVA: 0x00041508 File Offset: 0x0003F708
			public PropertyContainer Next3 { get; set; }

			// Token: 0x06001131 RID: 4401 RVA: 0x00041511 File Offset: 0x0003F711
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next3.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200024B RID: 587
		private class CollectionExpandedPropertyWithNext4<T> : PropertyContainer.CollectionExpandedPropertyWithNext3<T>
		{
			// Token: 0x17000434 RID: 1076
			// (get) Token: 0x06001133 RID: 4403 RVA: 0x00041532 File Offset: 0x0003F732
			// (set) Token: 0x06001134 RID: 4404 RVA: 0x0004153A File Offset: 0x0003F73A
			public PropertyContainer Next4 { get; set; }

			// Token: 0x06001135 RID: 4405 RVA: 0x00041543 File Offset: 0x0003F743
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next4.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200024C RID: 588
		private class CollectionExpandedPropertyWithNext5<T> : PropertyContainer.CollectionExpandedPropertyWithNext4<T>
		{
			// Token: 0x17000435 RID: 1077
			// (get) Token: 0x06001137 RID: 4407 RVA: 0x00041564 File Offset: 0x0003F764
			// (set) Token: 0x06001138 RID: 4408 RVA: 0x0004156C File Offset: 0x0003F76C
			public PropertyContainer Next5 { get; set; }

			// Token: 0x06001139 RID: 4409 RVA: 0x00041575 File Offset: 0x0003F775
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next5.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200024D RID: 589
		private class CollectionExpandedPropertyWithNext6<T> : PropertyContainer.CollectionExpandedPropertyWithNext5<T>
		{
			// Token: 0x17000436 RID: 1078
			// (get) Token: 0x0600113B RID: 4411 RVA: 0x00041596 File Offset: 0x0003F796
			// (set) Token: 0x0600113C RID: 4412 RVA: 0x0004159E File Offset: 0x0003F79E
			public PropertyContainer Next6 { get; set; }

			// Token: 0x0600113D RID: 4413 RVA: 0x000415A7 File Offset: 0x0003F7A7
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next6.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200024E RID: 590
		private class CollectionExpandedPropertyWithNext7<T> : PropertyContainer.CollectionExpandedPropertyWithNext6<T>
		{
			// Token: 0x17000437 RID: 1079
			// (get) Token: 0x0600113F RID: 4415 RVA: 0x000415C8 File Offset: 0x0003F7C8
			// (set) Token: 0x06001140 RID: 4416 RVA: 0x000415D0 File Offset: 0x0003F7D0
			public PropertyContainer Next7 { get; set; }

			// Token: 0x06001141 RID: 4417 RVA: 0x000415D9 File Offset: 0x0003F7D9
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next7.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200024F RID: 591
		private class CollectionExpandedPropertyWithNext8<T> : PropertyContainer.CollectionExpandedPropertyWithNext7<T>
		{
			// Token: 0x17000438 RID: 1080
			// (get) Token: 0x06001143 RID: 4419 RVA: 0x000415FA File Offset: 0x0003F7FA
			// (set) Token: 0x06001144 RID: 4420 RVA: 0x00041602 File Offset: 0x0003F802
			public PropertyContainer Next8 { get; set; }

			// Token: 0x06001145 RID: 4421 RVA: 0x0004160B File Offset: 0x0003F80B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next8.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000250 RID: 592
		private class CollectionExpandedPropertyWithNext9<T> : PropertyContainer.CollectionExpandedPropertyWithNext8<T>
		{
			// Token: 0x17000439 RID: 1081
			// (get) Token: 0x06001147 RID: 4423 RVA: 0x0004162C File Offset: 0x0003F82C
			// (set) Token: 0x06001148 RID: 4424 RVA: 0x00041634 File Offset: 0x0003F834
			public PropertyContainer Next9 { get; set; }

			// Token: 0x06001149 RID: 4425 RVA: 0x0004163D File Offset: 0x0003F83D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next9.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000251 RID: 593
		private class CollectionExpandedPropertyWithNext10<T> : PropertyContainer.CollectionExpandedPropertyWithNext9<T>
		{
			// Token: 0x1700043A RID: 1082
			// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004165E File Offset: 0x0003F85E
			// (set) Token: 0x0600114C RID: 4428 RVA: 0x00041666 File Offset: 0x0003F866
			public PropertyContainer Next10 { get; set; }

			// Token: 0x0600114D RID: 4429 RVA: 0x0004166F File Offset: 0x0003F86F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next10.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000252 RID: 594
		private class CollectionExpandedPropertyWithNext11<T> : PropertyContainer.CollectionExpandedPropertyWithNext10<T>
		{
			// Token: 0x1700043B RID: 1083
			// (get) Token: 0x0600114F RID: 4431 RVA: 0x00041690 File Offset: 0x0003F890
			// (set) Token: 0x06001150 RID: 4432 RVA: 0x00041698 File Offset: 0x0003F898
			public PropertyContainer Next11 { get; set; }

			// Token: 0x06001151 RID: 4433 RVA: 0x000416A1 File Offset: 0x0003F8A1
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next11.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000253 RID: 595
		private class CollectionExpandedPropertyWithNext12<T> : PropertyContainer.CollectionExpandedPropertyWithNext11<T>
		{
			// Token: 0x1700043C RID: 1084
			// (get) Token: 0x06001153 RID: 4435 RVA: 0x000416C2 File Offset: 0x0003F8C2
			// (set) Token: 0x06001154 RID: 4436 RVA: 0x000416CA File Offset: 0x0003F8CA
			public PropertyContainer Next12 { get; set; }

			// Token: 0x06001155 RID: 4437 RVA: 0x000416D3 File Offset: 0x0003F8D3
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next12.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000254 RID: 596
		private class CollectionExpandedPropertyWithNext13<T> : PropertyContainer.CollectionExpandedPropertyWithNext12<T>
		{
			// Token: 0x1700043D RID: 1085
			// (get) Token: 0x06001157 RID: 4439 RVA: 0x000416F4 File Offset: 0x0003F8F4
			// (set) Token: 0x06001158 RID: 4440 RVA: 0x000416FC File Offset: 0x0003F8FC
			public PropertyContainer Next13 { get; set; }

			// Token: 0x06001159 RID: 4441 RVA: 0x00041705 File Offset: 0x0003F905
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next13.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000255 RID: 597
		private class CollectionExpandedPropertyWithNext14<T> : PropertyContainer.CollectionExpandedPropertyWithNext13<T>
		{
			// Token: 0x1700043E RID: 1086
			// (get) Token: 0x0600115B RID: 4443 RVA: 0x00041726 File Offset: 0x0003F926
			// (set) Token: 0x0600115C RID: 4444 RVA: 0x0004172E File Offset: 0x0003F92E
			public PropertyContainer Next14 { get; set; }

			// Token: 0x0600115D RID: 4445 RVA: 0x00041737 File Offset: 0x0003F937
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next14.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000256 RID: 598
		private class CollectionExpandedPropertyWithNext15<T> : PropertyContainer.CollectionExpandedPropertyWithNext14<T>
		{
			// Token: 0x1700043F RID: 1087
			// (get) Token: 0x0600115F RID: 4447 RVA: 0x00041758 File Offset: 0x0003F958
			// (set) Token: 0x06001160 RID: 4448 RVA: 0x00041760 File Offset: 0x0003F960
			public PropertyContainer Next15 { get; set; }

			// Token: 0x06001161 RID: 4449 RVA: 0x00041769 File Offset: 0x0003F969
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next15.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000257 RID: 599
		private class CollectionExpandedPropertyWithNext16<T> : PropertyContainer.CollectionExpandedPropertyWithNext15<T>
		{
			// Token: 0x17000440 RID: 1088
			// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004178A File Offset: 0x0003F98A
			// (set) Token: 0x06001164 RID: 4452 RVA: 0x00041792 File Offset: 0x0003F992
			public PropertyContainer Next16 { get; set; }

			// Token: 0x06001165 RID: 4453 RVA: 0x0004179B File Offset: 0x0003F99B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next16.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000258 RID: 600
		private class CollectionExpandedPropertyWithNext17<T> : PropertyContainer.CollectionExpandedPropertyWithNext16<T>
		{
			// Token: 0x17000441 RID: 1089
			// (get) Token: 0x06001167 RID: 4455 RVA: 0x000417BC File Offset: 0x0003F9BC
			// (set) Token: 0x06001168 RID: 4456 RVA: 0x000417C4 File Offset: 0x0003F9C4
			public PropertyContainer Next17 { get; set; }

			// Token: 0x06001169 RID: 4457 RVA: 0x000417CD File Offset: 0x0003F9CD
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next17.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000259 RID: 601
		private class CollectionExpandedPropertyWithNext18<T> : PropertyContainer.CollectionExpandedPropertyWithNext17<T>
		{
			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x0600116B RID: 4459 RVA: 0x000417EE File Offset: 0x0003F9EE
			// (set) Token: 0x0600116C RID: 4460 RVA: 0x000417F6 File Offset: 0x0003F9F6
			public PropertyContainer Next18 { get; set; }

			// Token: 0x0600116D RID: 4461 RVA: 0x000417FF File Offset: 0x0003F9FF
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next18.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200025A RID: 602
		private class CollectionExpandedPropertyWithNext19<T> : PropertyContainer.CollectionExpandedPropertyWithNext18<T>
		{
			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x0600116F RID: 4463 RVA: 0x00041820 File Offset: 0x0003FA20
			// (set) Token: 0x06001170 RID: 4464 RVA: 0x00041828 File Offset: 0x0003FA28
			public PropertyContainer Next19 { get; set; }

			// Token: 0x06001171 RID: 4465 RVA: 0x00041831 File Offset: 0x0003FA31
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next19.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200025B RID: 603
		private class CollectionExpandedPropertyWithNext20<T> : PropertyContainer.CollectionExpandedPropertyWithNext19<T>
		{
			// Token: 0x17000444 RID: 1092
			// (get) Token: 0x06001173 RID: 4467 RVA: 0x00041852 File Offset: 0x0003FA52
			// (set) Token: 0x06001174 RID: 4468 RVA: 0x0004185A File Offset: 0x0003FA5A
			public PropertyContainer Next20 { get; set; }

			// Token: 0x06001175 RID: 4469 RVA: 0x00041863 File Offset: 0x0003FA63
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next20.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200025C RID: 604
		private class CollectionExpandedPropertyWithNext21<T> : PropertyContainer.CollectionExpandedPropertyWithNext20<T>
		{
			// Token: 0x17000445 RID: 1093
			// (get) Token: 0x06001177 RID: 4471 RVA: 0x00041884 File Offset: 0x0003FA84
			// (set) Token: 0x06001178 RID: 4472 RVA: 0x0004188C File Offset: 0x0003FA8C
			public PropertyContainer Next21 { get; set; }

			// Token: 0x06001179 RID: 4473 RVA: 0x00041895 File Offset: 0x0003FA95
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next21.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200025D RID: 605
		private class CollectionExpandedPropertyWithNext22<T> : PropertyContainer.CollectionExpandedPropertyWithNext21<T>
		{
			// Token: 0x17000446 RID: 1094
			// (get) Token: 0x0600117B RID: 4475 RVA: 0x000418B6 File Offset: 0x0003FAB6
			// (set) Token: 0x0600117C RID: 4476 RVA: 0x000418BE File Offset: 0x0003FABE
			public PropertyContainer Next22 { get; set; }

			// Token: 0x0600117D RID: 4477 RVA: 0x000418C7 File Offset: 0x0003FAC7
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next22.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200025E RID: 606
		private class CollectionExpandedPropertyWithNext23<T> : PropertyContainer.CollectionExpandedPropertyWithNext22<T>
		{
			// Token: 0x17000447 RID: 1095
			// (get) Token: 0x0600117F RID: 4479 RVA: 0x000418E8 File Offset: 0x0003FAE8
			// (set) Token: 0x06001180 RID: 4480 RVA: 0x000418F0 File Offset: 0x0003FAF0
			public PropertyContainer Next23 { get; set; }

			// Token: 0x06001181 RID: 4481 RVA: 0x000418F9 File Offset: 0x0003FAF9
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next23.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200025F RID: 607
		private class CollectionExpandedPropertyWithNext24<T> : PropertyContainer.CollectionExpandedPropertyWithNext23<T>
		{
			// Token: 0x17000448 RID: 1096
			// (get) Token: 0x06001183 RID: 4483 RVA: 0x0004191A File Offset: 0x0003FB1A
			// (set) Token: 0x06001184 RID: 4484 RVA: 0x00041922 File Offset: 0x0003FB22
			public PropertyContainer Next24 { get; set; }

			// Token: 0x06001185 RID: 4485 RVA: 0x0004192B File Offset: 0x0003FB2B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next24.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000260 RID: 608
		private class CollectionExpandedPropertyWithNext25<T> : PropertyContainer.CollectionExpandedPropertyWithNext24<T>
		{
			// Token: 0x17000449 RID: 1097
			// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004194C File Offset: 0x0003FB4C
			// (set) Token: 0x06001188 RID: 4488 RVA: 0x00041954 File Offset: 0x0003FB54
			public PropertyContainer Next25 { get; set; }

			// Token: 0x06001189 RID: 4489 RVA: 0x0004195D File Offset: 0x0003FB5D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next25.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000261 RID: 609
		private class CollectionExpandedPropertyWithNext26<T> : PropertyContainer.CollectionExpandedPropertyWithNext25<T>
		{
			// Token: 0x1700044A RID: 1098
			// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004197E File Offset: 0x0003FB7E
			// (set) Token: 0x0600118C RID: 4492 RVA: 0x00041986 File Offset: 0x0003FB86
			public PropertyContainer Next26 { get; set; }

			// Token: 0x0600118D RID: 4493 RVA: 0x0004198F File Offset: 0x0003FB8F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next26.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000262 RID: 610
		private class CollectionExpandedPropertyWithNext27<T> : PropertyContainer.CollectionExpandedPropertyWithNext26<T>
		{
			// Token: 0x1700044B RID: 1099
			// (get) Token: 0x0600118F RID: 4495 RVA: 0x000419B0 File Offset: 0x0003FBB0
			// (set) Token: 0x06001190 RID: 4496 RVA: 0x000419B8 File Offset: 0x0003FBB8
			public PropertyContainer Next27 { get; set; }

			// Token: 0x06001191 RID: 4497 RVA: 0x000419C1 File Offset: 0x0003FBC1
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next27.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000263 RID: 611
		private class CollectionExpandedPropertyWithNext28<T> : PropertyContainer.CollectionExpandedPropertyWithNext27<T>
		{
			// Token: 0x1700044C RID: 1100
			// (get) Token: 0x06001193 RID: 4499 RVA: 0x000419E2 File Offset: 0x0003FBE2
			// (set) Token: 0x06001194 RID: 4500 RVA: 0x000419EA File Offset: 0x0003FBEA
			public PropertyContainer Next28 { get; set; }

			// Token: 0x06001195 RID: 4501 RVA: 0x000419F3 File Offset: 0x0003FBF3
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next28.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000264 RID: 612
		private class CollectionExpandedPropertyWithNext29<T> : PropertyContainer.CollectionExpandedPropertyWithNext28<T>
		{
			// Token: 0x1700044D RID: 1101
			// (get) Token: 0x06001197 RID: 4503 RVA: 0x00041A14 File Offset: 0x0003FC14
			// (set) Token: 0x06001198 RID: 4504 RVA: 0x00041A1C File Offset: 0x0003FC1C
			public PropertyContainer Next29 { get; set; }

			// Token: 0x06001199 RID: 4505 RVA: 0x00041A25 File Offset: 0x0003FC25
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next29.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000265 RID: 613
		private class CollectionExpandedPropertyWithNext30<T> : PropertyContainer.CollectionExpandedPropertyWithNext29<T>
		{
			// Token: 0x1700044E RID: 1102
			// (get) Token: 0x0600119B RID: 4507 RVA: 0x00041A46 File Offset: 0x0003FC46
			// (set) Token: 0x0600119C RID: 4508 RVA: 0x00041A4E File Offset: 0x0003FC4E
			public PropertyContainer Next30 { get; set; }

			// Token: 0x0600119D RID: 4509 RVA: 0x00041A57 File Offset: 0x0003FC57
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next30.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000266 RID: 614
		private class CollectionExpandedPropertyWithNext31<T> : PropertyContainer.CollectionExpandedPropertyWithNext30<T>
		{
			// Token: 0x1700044F RID: 1103
			// (get) Token: 0x0600119F RID: 4511 RVA: 0x00041A78 File Offset: 0x0003FC78
			// (set) Token: 0x060011A0 RID: 4512 RVA: 0x00041A80 File Offset: 0x0003FC80
			public PropertyContainer Next31 { get; set; }

			// Token: 0x060011A1 RID: 4513 RVA: 0x00041A89 File Offset: 0x0003FC89
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next31.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000267 RID: 615
		private class AutoSelectedNamedPropertyWithNext0<T> : PropertyContainer.AutoSelectedNamedProperty<T>
		{
			// Token: 0x17000450 RID: 1104
			// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00041AAA File Offset: 0x0003FCAA
			// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00041AB2 File Offset: 0x0003FCB2
			public PropertyContainer Next0 { get; set; }

			// Token: 0x060011A5 RID: 4517 RVA: 0x00041ABB File Offset: 0x0003FCBB
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next0.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000268 RID: 616
		private class AutoSelectedNamedPropertyWithNext1<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext0<T>
		{
			// Token: 0x17000451 RID: 1105
			// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00041ADC File Offset: 0x0003FCDC
			// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00041AE4 File Offset: 0x0003FCE4
			public PropertyContainer Next1 { get; set; }

			// Token: 0x060011A9 RID: 4521 RVA: 0x00041AED File Offset: 0x0003FCED
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next1.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000269 RID: 617
		private class AutoSelectedNamedPropertyWithNext2<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext1<T>
		{
			// Token: 0x17000452 RID: 1106
			// (get) Token: 0x060011AB RID: 4523 RVA: 0x00041B0E File Offset: 0x0003FD0E
			// (set) Token: 0x060011AC RID: 4524 RVA: 0x00041B16 File Offset: 0x0003FD16
			public PropertyContainer Next2 { get; set; }

			// Token: 0x060011AD RID: 4525 RVA: 0x00041B1F File Offset: 0x0003FD1F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next2.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200026A RID: 618
		private class AutoSelectedNamedPropertyWithNext3<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext2<T>
		{
			// Token: 0x17000453 RID: 1107
			// (get) Token: 0x060011AF RID: 4527 RVA: 0x00041B40 File Offset: 0x0003FD40
			// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00041B48 File Offset: 0x0003FD48
			public PropertyContainer Next3 { get; set; }

			// Token: 0x060011B1 RID: 4529 RVA: 0x00041B51 File Offset: 0x0003FD51
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next3.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200026B RID: 619
		private class AutoSelectedNamedPropertyWithNext4<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext3<T>
		{
			// Token: 0x17000454 RID: 1108
			// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00041B72 File Offset: 0x0003FD72
			// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00041B7A File Offset: 0x0003FD7A
			public PropertyContainer Next4 { get; set; }

			// Token: 0x060011B5 RID: 4533 RVA: 0x00041B83 File Offset: 0x0003FD83
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next4.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200026C RID: 620
		private class AutoSelectedNamedPropertyWithNext5<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext4<T>
		{
			// Token: 0x17000455 RID: 1109
			// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00041BA4 File Offset: 0x0003FDA4
			// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00041BAC File Offset: 0x0003FDAC
			public PropertyContainer Next5 { get; set; }

			// Token: 0x060011B9 RID: 4537 RVA: 0x00041BB5 File Offset: 0x0003FDB5
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next5.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200026D RID: 621
		private class AutoSelectedNamedPropertyWithNext6<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext5<T>
		{
			// Token: 0x17000456 RID: 1110
			// (get) Token: 0x060011BB RID: 4539 RVA: 0x00041BD6 File Offset: 0x0003FDD6
			// (set) Token: 0x060011BC RID: 4540 RVA: 0x00041BDE File Offset: 0x0003FDDE
			public PropertyContainer Next6 { get; set; }

			// Token: 0x060011BD RID: 4541 RVA: 0x00041BE7 File Offset: 0x0003FDE7
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next6.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200026E RID: 622
		private class AutoSelectedNamedPropertyWithNext7<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext6<T>
		{
			// Token: 0x17000457 RID: 1111
			// (get) Token: 0x060011BF RID: 4543 RVA: 0x00041C08 File Offset: 0x0003FE08
			// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00041C10 File Offset: 0x0003FE10
			public PropertyContainer Next7 { get; set; }

			// Token: 0x060011C1 RID: 4545 RVA: 0x00041C19 File Offset: 0x0003FE19
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next7.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200026F RID: 623
		private class AutoSelectedNamedPropertyWithNext8<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext7<T>
		{
			// Token: 0x17000458 RID: 1112
			// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00041C3A File Offset: 0x0003FE3A
			// (set) Token: 0x060011C4 RID: 4548 RVA: 0x00041C42 File Offset: 0x0003FE42
			public PropertyContainer Next8 { get; set; }

			// Token: 0x060011C5 RID: 4549 RVA: 0x00041C4B File Offset: 0x0003FE4B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next8.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000270 RID: 624
		private class AutoSelectedNamedPropertyWithNext9<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext8<T>
		{
			// Token: 0x17000459 RID: 1113
			// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00041C6C File Offset: 0x0003FE6C
			// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00041C74 File Offset: 0x0003FE74
			public PropertyContainer Next9 { get; set; }

			// Token: 0x060011C9 RID: 4553 RVA: 0x00041C7D File Offset: 0x0003FE7D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next9.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000271 RID: 625
		private class AutoSelectedNamedPropertyWithNext10<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext9<T>
		{
			// Token: 0x1700045A RID: 1114
			// (get) Token: 0x060011CB RID: 4555 RVA: 0x00041C9E File Offset: 0x0003FE9E
			// (set) Token: 0x060011CC RID: 4556 RVA: 0x00041CA6 File Offset: 0x0003FEA6
			public PropertyContainer Next10 { get; set; }

			// Token: 0x060011CD RID: 4557 RVA: 0x00041CAF File Offset: 0x0003FEAF
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next10.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000272 RID: 626
		private class AutoSelectedNamedPropertyWithNext11<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext10<T>
		{
			// Token: 0x1700045B RID: 1115
			// (get) Token: 0x060011CF RID: 4559 RVA: 0x00041CD0 File Offset: 0x0003FED0
			// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00041CD8 File Offset: 0x0003FED8
			public PropertyContainer Next11 { get; set; }

			// Token: 0x060011D1 RID: 4561 RVA: 0x00041CE1 File Offset: 0x0003FEE1
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next11.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000273 RID: 627
		private class AutoSelectedNamedPropertyWithNext12<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext11<T>
		{
			// Token: 0x1700045C RID: 1116
			// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00041D02 File Offset: 0x0003FF02
			// (set) Token: 0x060011D4 RID: 4564 RVA: 0x00041D0A File Offset: 0x0003FF0A
			public PropertyContainer Next12 { get; set; }

			// Token: 0x060011D5 RID: 4565 RVA: 0x00041D13 File Offset: 0x0003FF13
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next12.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000274 RID: 628
		private class AutoSelectedNamedPropertyWithNext13<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext12<T>
		{
			// Token: 0x1700045D RID: 1117
			// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00041D34 File Offset: 0x0003FF34
			// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00041D3C File Offset: 0x0003FF3C
			public PropertyContainer Next13 { get; set; }

			// Token: 0x060011D9 RID: 4569 RVA: 0x00041D45 File Offset: 0x0003FF45
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next13.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000275 RID: 629
		private class AutoSelectedNamedPropertyWithNext14<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext13<T>
		{
			// Token: 0x1700045E RID: 1118
			// (get) Token: 0x060011DB RID: 4571 RVA: 0x00041D66 File Offset: 0x0003FF66
			// (set) Token: 0x060011DC RID: 4572 RVA: 0x00041D6E File Offset: 0x0003FF6E
			public PropertyContainer Next14 { get; set; }

			// Token: 0x060011DD RID: 4573 RVA: 0x00041D77 File Offset: 0x0003FF77
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next14.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000276 RID: 630
		private class AutoSelectedNamedPropertyWithNext15<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext14<T>
		{
			// Token: 0x1700045F RID: 1119
			// (get) Token: 0x060011DF RID: 4575 RVA: 0x00041D98 File Offset: 0x0003FF98
			// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00041DA0 File Offset: 0x0003FFA0
			public PropertyContainer Next15 { get; set; }

			// Token: 0x060011E1 RID: 4577 RVA: 0x00041DA9 File Offset: 0x0003FFA9
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next15.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000277 RID: 631
		private class AutoSelectedNamedPropertyWithNext16<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext15<T>
		{
			// Token: 0x17000460 RID: 1120
			// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00041DCA File Offset: 0x0003FFCA
			// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00041DD2 File Offset: 0x0003FFD2
			public PropertyContainer Next16 { get; set; }

			// Token: 0x060011E5 RID: 4581 RVA: 0x00041DDB File Offset: 0x0003FFDB
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next16.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000278 RID: 632
		private class AutoSelectedNamedPropertyWithNext17<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext16<T>
		{
			// Token: 0x17000461 RID: 1121
			// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00041DFC File Offset: 0x0003FFFC
			// (set) Token: 0x060011E8 RID: 4584 RVA: 0x00041E04 File Offset: 0x00040004
			public PropertyContainer Next17 { get; set; }

			// Token: 0x060011E9 RID: 4585 RVA: 0x00041E0D File Offset: 0x0004000D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next17.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000279 RID: 633
		private class AutoSelectedNamedPropertyWithNext18<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext17<T>
		{
			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x060011EB RID: 4587 RVA: 0x00041E2E File Offset: 0x0004002E
			// (set) Token: 0x060011EC RID: 4588 RVA: 0x00041E36 File Offset: 0x00040036
			public PropertyContainer Next18 { get; set; }

			// Token: 0x060011ED RID: 4589 RVA: 0x00041E3F File Offset: 0x0004003F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next18.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200027A RID: 634
		private class AutoSelectedNamedPropertyWithNext19<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext18<T>
		{
			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x060011EF RID: 4591 RVA: 0x00041E60 File Offset: 0x00040060
			// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00041E68 File Offset: 0x00040068
			public PropertyContainer Next19 { get; set; }

			// Token: 0x060011F1 RID: 4593 RVA: 0x00041E71 File Offset: 0x00040071
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next19.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200027B RID: 635
		private class AutoSelectedNamedPropertyWithNext20<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext19<T>
		{
			// Token: 0x17000464 RID: 1124
			// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00041E92 File Offset: 0x00040092
			// (set) Token: 0x060011F4 RID: 4596 RVA: 0x00041E9A File Offset: 0x0004009A
			public PropertyContainer Next20 { get; set; }

			// Token: 0x060011F5 RID: 4597 RVA: 0x00041EA3 File Offset: 0x000400A3
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next20.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200027C RID: 636
		private class AutoSelectedNamedPropertyWithNext21<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext20<T>
		{
			// Token: 0x17000465 RID: 1125
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00041EC4 File Offset: 0x000400C4
			// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00041ECC File Offset: 0x000400CC
			public PropertyContainer Next21 { get; set; }

			// Token: 0x060011F9 RID: 4601 RVA: 0x00041ED5 File Offset: 0x000400D5
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next21.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200027D RID: 637
		private class AutoSelectedNamedPropertyWithNext22<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext21<T>
		{
			// Token: 0x17000466 RID: 1126
			// (get) Token: 0x060011FB RID: 4603 RVA: 0x00041EF6 File Offset: 0x000400F6
			// (set) Token: 0x060011FC RID: 4604 RVA: 0x00041EFE File Offset: 0x000400FE
			public PropertyContainer Next22 { get; set; }

			// Token: 0x060011FD RID: 4605 RVA: 0x00041F07 File Offset: 0x00040107
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next22.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200027E RID: 638
		private class AutoSelectedNamedPropertyWithNext23<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext22<T>
		{
			// Token: 0x17000467 RID: 1127
			// (get) Token: 0x060011FF RID: 4607 RVA: 0x00041F28 File Offset: 0x00040128
			// (set) Token: 0x06001200 RID: 4608 RVA: 0x00041F30 File Offset: 0x00040130
			public PropertyContainer Next23 { get; set; }

			// Token: 0x06001201 RID: 4609 RVA: 0x00041F39 File Offset: 0x00040139
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next23.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200027F RID: 639
		private class AutoSelectedNamedPropertyWithNext24<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext23<T>
		{
			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x06001203 RID: 4611 RVA: 0x00041F5A File Offset: 0x0004015A
			// (set) Token: 0x06001204 RID: 4612 RVA: 0x00041F62 File Offset: 0x00040162
			public PropertyContainer Next24 { get; set; }

			// Token: 0x06001205 RID: 4613 RVA: 0x00041F6B File Offset: 0x0004016B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next24.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000280 RID: 640
		private class AutoSelectedNamedPropertyWithNext25<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext24<T>
		{
			// Token: 0x17000469 RID: 1129
			// (get) Token: 0x06001207 RID: 4615 RVA: 0x00041F8C File Offset: 0x0004018C
			// (set) Token: 0x06001208 RID: 4616 RVA: 0x00041F94 File Offset: 0x00040194
			public PropertyContainer Next25 { get; set; }

			// Token: 0x06001209 RID: 4617 RVA: 0x00041F9D File Offset: 0x0004019D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next25.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000281 RID: 641
		private class AutoSelectedNamedPropertyWithNext26<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext25<T>
		{
			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x0600120B RID: 4619 RVA: 0x00041FBE File Offset: 0x000401BE
			// (set) Token: 0x0600120C RID: 4620 RVA: 0x00041FC6 File Offset: 0x000401C6
			public PropertyContainer Next26 { get; set; }

			// Token: 0x0600120D RID: 4621 RVA: 0x00041FCF File Offset: 0x000401CF
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next26.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000282 RID: 642
		private class AutoSelectedNamedPropertyWithNext27<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext26<T>
		{
			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x0600120F RID: 4623 RVA: 0x00041FF0 File Offset: 0x000401F0
			// (set) Token: 0x06001210 RID: 4624 RVA: 0x00041FF8 File Offset: 0x000401F8
			public PropertyContainer Next27 { get; set; }

			// Token: 0x06001211 RID: 4625 RVA: 0x00042001 File Offset: 0x00040201
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next27.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000283 RID: 643
		private class AutoSelectedNamedPropertyWithNext28<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext27<T>
		{
			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x06001213 RID: 4627 RVA: 0x00042022 File Offset: 0x00040222
			// (set) Token: 0x06001214 RID: 4628 RVA: 0x0004202A File Offset: 0x0004022A
			public PropertyContainer Next28 { get; set; }

			// Token: 0x06001215 RID: 4629 RVA: 0x00042033 File Offset: 0x00040233
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next28.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000284 RID: 644
		private class AutoSelectedNamedPropertyWithNext29<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext28<T>
		{
			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x06001217 RID: 4631 RVA: 0x00042054 File Offset: 0x00040254
			// (set) Token: 0x06001218 RID: 4632 RVA: 0x0004205C File Offset: 0x0004025C
			public PropertyContainer Next29 { get; set; }

			// Token: 0x06001219 RID: 4633 RVA: 0x00042065 File Offset: 0x00040265
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next29.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000285 RID: 645
		private class AutoSelectedNamedPropertyWithNext30<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext29<T>
		{
			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x0600121B RID: 4635 RVA: 0x00042086 File Offset: 0x00040286
			// (set) Token: 0x0600121C RID: 4636 RVA: 0x0004208E File Offset: 0x0004028E
			public PropertyContainer Next30 { get; set; }

			// Token: 0x0600121D RID: 4637 RVA: 0x00042097 File Offset: 0x00040297
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next30.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000286 RID: 646
		private class AutoSelectedNamedPropertyWithNext31<T> : PropertyContainer.AutoSelectedNamedPropertyWithNext30<T>
		{
			// Token: 0x1700046F RID: 1135
			// (get) Token: 0x0600121F RID: 4639 RVA: 0x000420B8 File Offset: 0x000402B8
			// (set) Token: 0x06001220 RID: 4640 RVA: 0x000420C0 File Offset: 0x000402C0
			public PropertyContainer Next31 { get; set; }

			// Token: 0x06001221 RID: 4641 RVA: 0x000420C9 File Offset: 0x000402C9
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next31.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000287 RID: 647
		private class NamedPropertyWithNext0<T> : PropertyContainer.NamedProperty<T>
		{
			// Token: 0x17000470 RID: 1136
			// (get) Token: 0x06001223 RID: 4643 RVA: 0x000420EA File Offset: 0x000402EA
			// (set) Token: 0x06001224 RID: 4644 RVA: 0x000420F2 File Offset: 0x000402F2
			public PropertyContainer Next0 { get; set; }

			// Token: 0x06001225 RID: 4645 RVA: 0x000420FB File Offset: 0x000402FB
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next0.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000288 RID: 648
		private class NamedPropertyWithNext1<T> : PropertyContainer.NamedPropertyWithNext0<T>
		{
			// Token: 0x17000471 RID: 1137
			// (get) Token: 0x06001227 RID: 4647 RVA: 0x0004211C File Offset: 0x0004031C
			// (set) Token: 0x06001228 RID: 4648 RVA: 0x00042124 File Offset: 0x00040324
			public PropertyContainer Next1 { get; set; }

			// Token: 0x06001229 RID: 4649 RVA: 0x0004212D File Offset: 0x0004032D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next1.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000289 RID: 649
		private class NamedPropertyWithNext2<T> : PropertyContainer.NamedPropertyWithNext1<T>
		{
			// Token: 0x17000472 RID: 1138
			// (get) Token: 0x0600122B RID: 4651 RVA: 0x0004214E File Offset: 0x0004034E
			// (set) Token: 0x0600122C RID: 4652 RVA: 0x00042156 File Offset: 0x00040356
			public PropertyContainer Next2 { get; set; }

			// Token: 0x0600122D RID: 4653 RVA: 0x0004215F File Offset: 0x0004035F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next2.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200028A RID: 650
		private class NamedPropertyWithNext3<T> : PropertyContainer.NamedPropertyWithNext2<T>
		{
			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x0600122F RID: 4655 RVA: 0x00042180 File Offset: 0x00040380
			// (set) Token: 0x06001230 RID: 4656 RVA: 0x00042188 File Offset: 0x00040388
			public PropertyContainer Next3 { get; set; }

			// Token: 0x06001231 RID: 4657 RVA: 0x00042191 File Offset: 0x00040391
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next3.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200028B RID: 651
		private class NamedPropertyWithNext4<T> : PropertyContainer.NamedPropertyWithNext3<T>
		{
			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x06001233 RID: 4659 RVA: 0x000421B2 File Offset: 0x000403B2
			// (set) Token: 0x06001234 RID: 4660 RVA: 0x000421BA File Offset: 0x000403BA
			public PropertyContainer Next4 { get; set; }

			// Token: 0x06001235 RID: 4661 RVA: 0x000421C3 File Offset: 0x000403C3
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next4.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200028C RID: 652
		private class NamedPropertyWithNext5<T> : PropertyContainer.NamedPropertyWithNext4<T>
		{
			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x06001237 RID: 4663 RVA: 0x000421E4 File Offset: 0x000403E4
			// (set) Token: 0x06001238 RID: 4664 RVA: 0x000421EC File Offset: 0x000403EC
			public PropertyContainer Next5 { get; set; }

			// Token: 0x06001239 RID: 4665 RVA: 0x000421F5 File Offset: 0x000403F5
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next5.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200028D RID: 653
		private class NamedPropertyWithNext6<T> : PropertyContainer.NamedPropertyWithNext5<T>
		{
			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x0600123B RID: 4667 RVA: 0x00042216 File Offset: 0x00040416
			// (set) Token: 0x0600123C RID: 4668 RVA: 0x0004221E File Offset: 0x0004041E
			public PropertyContainer Next6 { get; set; }

			// Token: 0x0600123D RID: 4669 RVA: 0x00042227 File Offset: 0x00040427
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next6.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200028E RID: 654
		private class NamedPropertyWithNext7<T> : PropertyContainer.NamedPropertyWithNext6<T>
		{
			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x0600123F RID: 4671 RVA: 0x00042248 File Offset: 0x00040448
			// (set) Token: 0x06001240 RID: 4672 RVA: 0x00042250 File Offset: 0x00040450
			public PropertyContainer Next7 { get; set; }

			// Token: 0x06001241 RID: 4673 RVA: 0x00042259 File Offset: 0x00040459
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next7.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200028F RID: 655
		private class NamedPropertyWithNext8<T> : PropertyContainer.NamedPropertyWithNext7<T>
		{
			// Token: 0x17000478 RID: 1144
			// (get) Token: 0x06001243 RID: 4675 RVA: 0x0004227A File Offset: 0x0004047A
			// (set) Token: 0x06001244 RID: 4676 RVA: 0x00042282 File Offset: 0x00040482
			public PropertyContainer Next8 { get; set; }

			// Token: 0x06001245 RID: 4677 RVA: 0x0004228B File Offset: 0x0004048B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next8.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000290 RID: 656
		private class NamedPropertyWithNext9<T> : PropertyContainer.NamedPropertyWithNext8<T>
		{
			// Token: 0x17000479 RID: 1145
			// (get) Token: 0x06001247 RID: 4679 RVA: 0x000422AC File Offset: 0x000404AC
			// (set) Token: 0x06001248 RID: 4680 RVA: 0x000422B4 File Offset: 0x000404B4
			public PropertyContainer Next9 { get; set; }

			// Token: 0x06001249 RID: 4681 RVA: 0x000422BD File Offset: 0x000404BD
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next9.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000291 RID: 657
		private class NamedPropertyWithNext10<T> : PropertyContainer.NamedPropertyWithNext9<T>
		{
			// Token: 0x1700047A RID: 1146
			// (get) Token: 0x0600124B RID: 4683 RVA: 0x000422DE File Offset: 0x000404DE
			// (set) Token: 0x0600124C RID: 4684 RVA: 0x000422E6 File Offset: 0x000404E6
			public PropertyContainer Next10 { get; set; }

			// Token: 0x0600124D RID: 4685 RVA: 0x000422EF File Offset: 0x000404EF
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next10.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000292 RID: 658
		private class NamedPropertyWithNext11<T> : PropertyContainer.NamedPropertyWithNext10<T>
		{
			// Token: 0x1700047B RID: 1147
			// (get) Token: 0x0600124F RID: 4687 RVA: 0x00042310 File Offset: 0x00040510
			// (set) Token: 0x06001250 RID: 4688 RVA: 0x00042318 File Offset: 0x00040518
			public PropertyContainer Next11 { get; set; }

			// Token: 0x06001251 RID: 4689 RVA: 0x00042321 File Offset: 0x00040521
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next11.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000293 RID: 659
		private class NamedPropertyWithNext12<T> : PropertyContainer.NamedPropertyWithNext11<T>
		{
			// Token: 0x1700047C RID: 1148
			// (get) Token: 0x06001253 RID: 4691 RVA: 0x00042342 File Offset: 0x00040542
			// (set) Token: 0x06001254 RID: 4692 RVA: 0x0004234A File Offset: 0x0004054A
			public PropertyContainer Next12 { get; set; }

			// Token: 0x06001255 RID: 4693 RVA: 0x00042353 File Offset: 0x00040553
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next12.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000294 RID: 660
		private class NamedPropertyWithNext13<T> : PropertyContainer.NamedPropertyWithNext12<T>
		{
			// Token: 0x1700047D RID: 1149
			// (get) Token: 0x06001257 RID: 4695 RVA: 0x00042374 File Offset: 0x00040574
			// (set) Token: 0x06001258 RID: 4696 RVA: 0x0004237C File Offset: 0x0004057C
			public PropertyContainer Next13 { get; set; }

			// Token: 0x06001259 RID: 4697 RVA: 0x00042385 File Offset: 0x00040585
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next13.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000295 RID: 661
		private class NamedPropertyWithNext14<T> : PropertyContainer.NamedPropertyWithNext13<T>
		{
			// Token: 0x1700047E RID: 1150
			// (get) Token: 0x0600125B RID: 4699 RVA: 0x000423A6 File Offset: 0x000405A6
			// (set) Token: 0x0600125C RID: 4700 RVA: 0x000423AE File Offset: 0x000405AE
			public PropertyContainer Next14 { get; set; }

			// Token: 0x0600125D RID: 4701 RVA: 0x000423B7 File Offset: 0x000405B7
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next14.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000296 RID: 662
		private class NamedPropertyWithNext15<T> : PropertyContainer.NamedPropertyWithNext14<T>
		{
			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x0600125F RID: 4703 RVA: 0x000423D8 File Offset: 0x000405D8
			// (set) Token: 0x06001260 RID: 4704 RVA: 0x000423E0 File Offset: 0x000405E0
			public PropertyContainer Next15 { get; set; }

			// Token: 0x06001261 RID: 4705 RVA: 0x000423E9 File Offset: 0x000405E9
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next15.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000297 RID: 663
		private class NamedPropertyWithNext16<T> : PropertyContainer.NamedPropertyWithNext15<T>
		{
			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004240A File Offset: 0x0004060A
			// (set) Token: 0x06001264 RID: 4708 RVA: 0x00042412 File Offset: 0x00040612
			public PropertyContainer Next16 { get; set; }

			// Token: 0x06001265 RID: 4709 RVA: 0x0004241B File Offset: 0x0004061B
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next16.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000298 RID: 664
		private class NamedPropertyWithNext17<T> : PropertyContainer.NamedPropertyWithNext16<T>
		{
			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x06001267 RID: 4711 RVA: 0x0004243C File Offset: 0x0004063C
			// (set) Token: 0x06001268 RID: 4712 RVA: 0x00042444 File Offset: 0x00040644
			public PropertyContainer Next17 { get; set; }

			// Token: 0x06001269 RID: 4713 RVA: 0x0004244D File Offset: 0x0004064D
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next17.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x02000299 RID: 665
		private class NamedPropertyWithNext18<T> : PropertyContainer.NamedPropertyWithNext17<T>
		{
			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x0600126B RID: 4715 RVA: 0x0004246E File Offset: 0x0004066E
			// (set) Token: 0x0600126C RID: 4716 RVA: 0x00042476 File Offset: 0x00040676
			public PropertyContainer Next18 { get; set; }

			// Token: 0x0600126D RID: 4717 RVA: 0x0004247F File Offset: 0x0004067F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next18.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200029A RID: 666
		private class NamedPropertyWithNext19<T> : PropertyContainer.NamedPropertyWithNext18<T>
		{
			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x0600126F RID: 4719 RVA: 0x000424A0 File Offset: 0x000406A0
			// (set) Token: 0x06001270 RID: 4720 RVA: 0x000424A8 File Offset: 0x000406A8
			public PropertyContainer Next19 { get; set; }

			// Token: 0x06001271 RID: 4721 RVA: 0x000424B1 File Offset: 0x000406B1
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next19.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200029B RID: 667
		private class NamedPropertyWithNext20<T> : PropertyContainer.NamedPropertyWithNext19<T>
		{
			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x06001273 RID: 4723 RVA: 0x000424D2 File Offset: 0x000406D2
			// (set) Token: 0x06001274 RID: 4724 RVA: 0x000424DA File Offset: 0x000406DA
			public PropertyContainer Next20 { get; set; }

			// Token: 0x06001275 RID: 4725 RVA: 0x000424E3 File Offset: 0x000406E3
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next20.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200029C RID: 668
		private class NamedPropertyWithNext21<T> : PropertyContainer.NamedPropertyWithNext20<T>
		{
			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x06001277 RID: 4727 RVA: 0x00042504 File Offset: 0x00040704
			// (set) Token: 0x06001278 RID: 4728 RVA: 0x0004250C File Offset: 0x0004070C
			public PropertyContainer Next21 { get; set; }

			// Token: 0x06001279 RID: 4729 RVA: 0x00042515 File Offset: 0x00040715
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next21.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200029D RID: 669
		private class NamedPropertyWithNext22<T> : PropertyContainer.NamedPropertyWithNext21<T>
		{
			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x0600127B RID: 4731 RVA: 0x00042536 File Offset: 0x00040736
			// (set) Token: 0x0600127C RID: 4732 RVA: 0x0004253E File Offset: 0x0004073E
			public PropertyContainer Next22 { get; set; }

			// Token: 0x0600127D RID: 4733 RVA: 0x00042547 File Offset: 0x00040747
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next22.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200029E RID: 670
		private class NamedPropertyWithNext23<T> : PropertyContainer.NamedPropertyWithNext22<T>
		{
			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x0600127F RID: 4735 RVA: 0x00042568 File Offset: 0x00040768
			// (set) Token: 0x06001280 RID: 4736 RVA: 0x00042570 File Offset: 0x00040770
			public PropertyContainer Next23 { get; set; }

			// Token: 0x06001281 RID: 4737 RVA: 0x00042579 File Offset: 0x00040779
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next23.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x0200029F RID: 671
		private class NamedPropertyWithNext24<T> : PropertyContainer.NamedPropertyWithNext23<T>
		{
			// Token: 0x17000488 RID: 1160
			// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004259A File Offset: 0x0004079A
			// (set) Token: 0x06001284 RID: 4740 RVA: 0x000425A2 File Offset: 0x000407A2
			public PropertyContainer Next24 { get; set; }

			// Token: 0x06001285 RID: 4741 RVA: 0x000425AB File Offset: 0x000407AB
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next24.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A0 RID: 672
		private class NamedPropertyWithNext25<T> : PropertyContainer.NamedPropertyWithNext24<T>
		{
			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x06001287 RID: 4743 RVA: 0x000425CC File Offset: 0x000407CC
			// (set) Token: 0x06001288 RID: 4744 RVA: 0x000425D4 File Offset: 0x000407D4
			public PropertyContainer Next25 { get; set; }

			// Token: 0x06001289 RID: 4745 RVA: 0x000425DD File Offset: 0x000407DD
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next25.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A1 RID: 673
		private class NamedPropertyWithNext26<T> : PropertyContainer.NamedPropertyWithNext25<T>
		{
			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x0600128B RID: 4747 RVA: 0x000425FE File Offset: 0x000407FE
			// (set) Token: 0x0600128C RID: 4748 RVA: 0x00042606 File Offset: 0x00040806
			public PropertyContainer Next26 { get; set; }

			// Token: 0x0600128D RID: 4749 RVA: 0x0004260F File Offset: 0x0004080F
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next26.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A2 RID: 674
		private class NamedPropertyWithNext27<T> : PropertyContainer.NamedPropertyWithNext26<T>
		{
			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x0600128F RID: 4751 RVA: 0x00042630 File Offset: 0x00040830
			// (set) Token: 0x06001290 RID: 4752 RVA: 0x00042638 File Offset: 0x00040838
			public PropertyContainer Next27 { get; set; }

			// Token: 0x06001291 RID: 4753 RVA: 0x00042641 File Offset: 0x00040841
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next27.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A3 RID: 675
		private class NamedPropertyWithNext28<T> : PropertyContainer.NamedPropertyWithNext27<T>
		{
			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x06001293 RID: 4755 RVA: 0x00042662 File Offset: 0x00040862
			// (set) Token: 0x06001294 RID: 4756 RVA: 0x0004266A File Offset: 0x0004086A
			public PropertyContainer Next28 { get; set; }

			// Token: 0x06001295 RID: 4757 RVA: 0x00042673 File Offset: 0x00040873
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next28.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A4 RID: 676
		private class NamedPropertyWithNext29<T> : PropertyContainer.NamedPropertyWithNext28<T>
		{
			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x06001297 RID: 4759 RVA: 0x00042694 File Offset: 0x00040894
			// (set) Token: 0x06001298 RID: 4760 RVA: 0x0004269C File Offset: 0x0004089C
			public PropertyContainer Next29 { get; set; }

			// Token: 0x06001299 RID: 4761 RVA: 0x000426A5 File Offset: 0x000408A5
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next29.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A5 RID: 677
		private class NamedPropertyWithNext30<T> : PropertyContainer.NamedPropertyWithNext29<T>
		{
			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x0600129B RID: 4763 RVA: 0x000426C6 File Offset: 0x000408C6
			// (set) Token: 0x0600129C RID: 4764 RVA: 0x000426CE File Offset: 0x000408CE
			public PropertyContainer Next30 { get; set; }

			// Token: 0x0600129D RID: 4765 RVA: 0x000426D7 File Offset: 0x000408D7
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next30.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A6 RID: 678
		private class NamedPropertyWithNext31<T> : PropertyContainer.NamedPropertyWithNext30<T>
		{
			// Token: 0x1700048F RID: 1167
			// (get) Token: 0x0600129F RID: 4767 RVA: 0x000426F8 File Offset: 0x000408F8
			// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00042700 File Offset: 0x00040900
			public PropertyContainer Next31 { get; set; }

			// Token: 0x060012A1 RID: 4769 RVA: 0x00042709 File Offset: 0x00040909
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				base.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
				this.Next31.ToDictionaryCore(dictionary, propertyMapper, includeAutoSelected);
			}
		}

		// Token: 0x020002A7 RID: 679
		internal class NamedProperty<T> : PropertyContainer
		{
			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0004272A File Offset: 0x0004092A
			// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00042732 File Offset: 0x00040932
			public string Name { get; set; }

			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0004273B File Offset: 0x0004093B
			// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00042743 File Offset: 0x00040943
			public T Value { get; set; }

			// Token: 0x17000492 RID: 1170
			// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004274C File Offset: 0x0004094C
			// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00042754 File Offset: 0x00040954
			public bool AutoSelected { get; set; }

			// Token: 0x060012A9 RID: 4777 RVA: 0x00042760 File Offset: 0x00040960
			public override void ToDictionaryCore(Dictionary<string, object> dictionary, IPropertyMapper propertyMapper, bool includeAutoSelected)
			{
				if (this.Name != null && (includeAutoSelected || !this.AutoSelected))
				{
					string text = propertyMapper.MapProperty(this.Name);
					if (string.IsNullOrEmpty(text))
					{
						throw Error.InvalidOperation(SRResources.InvalidPropertyMapping, new object[] { this.Name });
					}
					dictionary.Add(text, this.GetValue());
				}
			}

			// Token: 0x060012AA RID: 4778 RVA: 0x000427BC File Offset: 0x000409BC
			public virtual object GetValue()
			{
				return this.Value;
			}
		}

		// Token: 0x020002A8 RID: 680
		private class AutoSelectedNamedProperty<T> : PropertyContainer.NamedProperty<T>
		{
			// Token: 0x060012AC RID: 4780 RVA: 0x000427D1 File Offset: 0x000409D1
			public AutoSelectedNamedProperty()
			{
				base.AutoSelected = true;
			}
		}

		// Token: 0x020002A9 RID: 681
		private class SingleExpandedProperty<T> : PropertyContainer.NamedProperty<T>
		{
			// Token: 0x17000493 RID: 1171
			// (get) Token: 0x060012AD RID: 4781 RVA: 0x000427E0 File Offset: 0x000409E0
			// (set) Token: 0x060012AE RID: 4782 RVA: 0x000427E8 File Offset: 0x000409E8
			public bool IsNull { get; set; }

			// Token: 0x060012AF RID: 4783 RVA: 0x000427F1 File Offset: 0x000409F1
			public override object GetValue()
			{
				if (!this.IsNull)
				{
					return base.Value;
				}
				return null;
			}
		}

		// Token: 0x020002AA RID: 682
		private class CollectionExpandedProperty<T> : PropertyContainer.NamedProperty<T>
		{
			// Token: 0x17000494 RID: 1172
			// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00042810 File Offset: 0x00040A10
			// (set) Token: 0x060012B2 RID: 4786 RVA: 0x00042818 File Offset: 0x00040A18
			public int PageSize { get; set; }

			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00042821 File Offset: 0x00040A21
			// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00042829 File Offset: 0x00040A29
			public long? TotalCount { get; set; }

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00042832 File Offset: 0x00040A32
			// (set) Token: 0x060012B6 RID: 4790 RVA: 0x0004283A File Offset: 0x00040A3A
			public IEnumerable<T> Collection { get; set; }

			// Token: 0x060012B7 RID: 4791 RVA: 0x00042844 File Offset: 0x00040A44
			public override object GetValue()
			{
				if (this.TotalCount == null)
				{
					return new TruncatedCollection<T>(this.Collection, this.PageSize);
				}
				return new TruncatedCollection<T>(this.Collection, this.PageSize, this.TotalCount);
			}
		}
	}
}
