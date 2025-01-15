using System;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class RoutePrefixAttribute : Attribute, IRoutePrefix
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003B27 File Offset: 0x00001D27
		protected RoutePrefixAttribute()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003B2F File Offset: 0x00001D2F
		public RoutePrefixAttribute(string prefix)
		{
			if (prefix == null)
			{
				throw Error.ArgumentNull("prefix");
			}
			this.Prefix = prefix;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003B4C File Offset: 0x00001D4C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003B54 File Offset: 0x00001D54
		public virtual string Prefix { get; private set; }
	}
}
