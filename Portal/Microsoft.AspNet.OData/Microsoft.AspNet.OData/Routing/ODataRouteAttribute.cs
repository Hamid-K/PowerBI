using System;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ODataRouteAttribute : Attribute
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000F25C File Offset: 0x0000D45C
		public ODataRouteAttribute()
			: this(null)
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000F265 File Offset: 0x0000D465
		public ODataRouteAttribute(string pathTemplate)
		{
			this.PathTemplate = pathTemplate ?? string.Empty;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000F27D File Offset: 0x0000D47D
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000F285 File Offset: 0x0000D485
		public string PathTemplate { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000F28E File Offset: 0x0000D48E
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000F296 File Offset: 0x0000D496
		public string RouteName { get; set; }
	}
}
