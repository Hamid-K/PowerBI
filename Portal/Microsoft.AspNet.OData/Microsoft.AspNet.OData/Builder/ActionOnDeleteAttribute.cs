using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200010B RID: 267
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class ActionOnDeleteAttribute : Attribute
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00026694 File Offset: 0x00024894
		public ActionOnDeleteAttribute(EdmOnDeleteAction onDeleteAction)
		{
			this.OnDeleteAction = onDeleteAction;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x000266A3 File Offset: 0x000248A3
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x000266AB File Offset: 0x000248AB
		public EdmOnDeleteAction OnDeleteAction { get; private set; }
	}
}
