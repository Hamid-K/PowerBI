using System;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000036 RID: 54
	public class ClrPropertyInfoAnnotation
	{
		// Token: 0x06000146 RID: 326 RVA: 0x000064DB File Offset: 0x000046DB
		public ClrPropertyInfoAnnotation(PropertyInfo clrPropertyInfo)
		{
			if (clrPropertyInfo == null)
			{
				throw Error.ArgumentNull("clrPropertyInfo");
			}
			this.ClrPropertyInfo = clrPropertyInfo;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000064FE File Offset: 0x000046FE
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006506 File Offset: 0x00004706
		public PropertyInfo ClrPropertyInfo { get; private set; }
	}
}
