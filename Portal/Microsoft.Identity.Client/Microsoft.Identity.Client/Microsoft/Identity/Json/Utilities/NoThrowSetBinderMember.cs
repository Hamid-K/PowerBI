using System;
using System.Dynamic;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000055 RID: 85
	internal class NoThrowSetBinderMember : SetMemberBinder
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00015172 File Offset: 0x00013372
		public NoThrowSetBinderMember(SetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00015190 File Offset: 0x00013390
		public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, new DynamicMetaObject[] { value });
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x040001C1 RID: 449
		private readonly SetMemberBinder _innerBinder;
	}
}
