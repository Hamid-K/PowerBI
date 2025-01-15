using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;

namespace System.Web.Http.Description
{
	// Token: 0x020000E8 RID: 232
	public class ApiParameterDescription
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000FA2E File Offset: 0x0000DC2E
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000FA36 File Offset: 0x0000DC36
		public string Name { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000FA3F File Offset: 0x0000DC3F
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0000FA47 File Offset: 0x0000DC47
		public string Documentation { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000FA50 File Offset: 0x0000DC50
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public ApiParameterSource Source { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000FA61 File Offset: 0x0000DC61
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000FA69 File Offset: 0x0000DC69
		public HttpParameterDescriptor ParameterDescriptor { get; set; }

		// Token: 0x0600061B RID: 1563 RVA: 0x0000FA72 File Offset: 0x0000DC72
		internal IEnumerable<PropertyInfo> GetBindableProperties()
		{
			return ApiParameterDescription.GetBindableProperties(this.ParameterDescriptor.ParameterType);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000FA84 File Offset: 0x0000DC84
		internal bool CanConvertPropertiesFromString()
		{
			return this.GetBindableProperties().All((PropertyInfo p) => TypeHelper.CanConvertFromString(p.PropertyType));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		internal static IEnumerable<PropertyInfo> GetBindableProperties(Type type)
		{
			return from p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				where p.GetGetMethod() != null && p.GetSetMethod() != null
				select p;
		}
	}
}
