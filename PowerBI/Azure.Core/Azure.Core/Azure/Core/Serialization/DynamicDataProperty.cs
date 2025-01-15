using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Azure.Core.Serialization
{
	// Token: 0x020000C3 RID: 195
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct DynamicDataProperty : IDynamicMetaObjectProvider
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001655B File Offset: 0x0001475B
		internal DynamicDataProperty(string name, DynamicData value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001656B File Offset: 0x0001476B
		public string Name { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00016573 File Offset: 0x00014773
		public DynamicData Value { get; }

		// Token: 0x06000688 RID: 1672 RVA: 0x0001657B File Offset: 0x0001477B
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new DynamicDataProperty.MetaObject(parameter, this);
		}

		// Token: 0x02000152 RID: 338
		[Nullable(0)]
		private class MetaObject : DynamicMetaObject
		{
			// Token: 0x060008E1 RID: 2273 RVA: 0x00021C59 File Offset: 0x0001FE59
			internal MetaObject(Expression parameter, IDynamicMetaObjectProvider value)
				: base(parameter, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x00021C68 File Offset: 0x0001FE68
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				return DynamicDataProperty.MetaObject._memberNames;
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x00021C70 File Offset: 0x0001FE70
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				Expression expression = Expression.Property(Expression.Convert(base.Expression, base.LimitType), binder.Name);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			// Token: 0x040004FF RID: 1279
			private static readonly string[] _memberNames = new string[] { "Name", "Value" };
		}
	}
}
