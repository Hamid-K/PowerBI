using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200004D RID: 77
	[NonValidatingParameterBinding]
	public abstract class Delta : DynamicObject, IDelta
	{
		// Token: 0x060001C6 RID: 454
		public abstract void Clear();

		// Token: 0x060001C7 RID: 455
		public abstract bool TrySetPropertyValue(string name, object value);

		// Token: 0x060001C8 RID: 456
		public abstract bool TryGetPropertyValue(string name, out object value);

		// Token: 0x060001C9 RID: 457
		public abstract bool TryGetPropertyType(string name, out Type type);

		// Token: 0x060001CA RID: 458 RVA: 0x00007DCD File Offset: 0x00005FCD
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			if (binder == null)
			{
				throw Error.ArgumentNull("binder");
			}
			return this.TrySetPropertyValue(binder.Name, value);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007DEA File Offset: 0x00005FEA
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if (binder == null)
			{
				throw Error.ArgumentNull("binder");
			}
			return this.TryGetPropertyValue(binder.Name, out result);
		}

		// Token: 0x060001CC RID: 460
		public abstract IEnumerable<string> GetChangedPropertyNames();

		// Token: 0x060001CD RID: 461
		public abstract IEnumerable<string> GetUnchangedPropertyNames();
	}
}
