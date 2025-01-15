using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000146 RID: 326
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public abstract class RouteFactoryAttribute : Attribute, IDirectRouteFactory
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00015EF2 File Offset: 0x000140F2
		protected RouteFactoryAttribute(string template)
		{
			this._template = template;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00015F01 File Offset: 0x00014101
		public string Template
		{
			get
			{
				return this._template;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00015F09 File Offset: 0x00014109
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00015F11 File Offset: 0x00014111
		public string Name { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00015F1A File Offset: 0x0001411A
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00015F22 File Offset: 0x00014122
		public int Order { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0000413B File Offset: 0x0000233B
		public virtual IDictionary<string, object> Defaults
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0000413B File Offset: 0x0000233B
		public virtual IDictionary<string, object> Constraints
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0000413B File Offset: 0x0000233B
		public virtual IDictionary<string, object> DataTokens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00015F2C File Offset: 0x0001412C
		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			IDirectRouteBuilder directRouteBuilder = context.CreateBuilder(this.Template);
			directRouteBuilder.Name = this.Name;
			directRouteBuilder.Order = this.Order;
			IDictionary<string, object> defaults = directRouteBuilder.Defaults;
			if (defaults == null)
			{
				directRouteBuilder.Defaults = this.Defaults;
			}
			else
			{
				IDictionary<string, object> defaults2 = this.Defaults;
				if (defaults2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in defaults2)
					{
						defaults[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			IDictionary<string, object> constraints = directRouteBuilder.Constraints;
			if (constraints == null)
			{
				directRouteBuilder.Constraints = this.Constraints;
			}
			else
			{
				IDictionary<string, object> constraints2 = this.Constraints;
				if (constraints2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair2 in constraints2)
					{
						constraints[keyValuePair2.Key] = keyValuePair2.Value;
					}
				}
			}
			IDictionary<string, object> dataTokens = directRouteBuilder.DataTokens;
			if (dataTokens == null)
			{
				directRouteBuilder.DataTokens = this.DataTokens;
			}
			else
			{
				IDictionary<string, object> dataTokens2 = this.DataTokens;
				if (dataTokens2 != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair3 in dataTokens2)
					{
						dataTokens[keyValuePair3.Key] = keyValuePair3.Value;
					}
				}
			}
			return directRouteBuilder.Build();
		}

		// Token: 0x04000268 RID: 616
		private readonly string _template;
	}
}
