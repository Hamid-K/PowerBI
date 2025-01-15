using System;
using System.Dynamic;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000054 RID: 84
	internal class NoThrowGetBinderMember : GetMemberBinder
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001511C File Offset: 0x0001331C
		public NoThrowGetBinderMember(GetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00015138 File Offset: 0x00013338
		public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, CollectionUtils.ArrayEmpty<DynamicMetaObject>());
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x040001C0 RID: 448
		private readonly GetMemberBinder _innerBinder;
	}
}
