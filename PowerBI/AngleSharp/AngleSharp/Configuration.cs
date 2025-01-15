using System;
using System.Collections.Generic;

namespace AngleSharp
{
	// Token: 0x02000009 RID: 9
	public class Configuration : IConfiguration
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002831 File Offset: 0x00000A31
		public Configuration(IEnumerable<object> services = null)
		{
			this._services = services ?? Configuration.standardServices;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002849 File Offset: 0x00000A49
		public static IConfiguration Default
		{
			get
			{
				return Configuration.customConfiguration ?? Configuration.defaultConfiguration;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002859 File Offset: 0x00000A59
		public static void SetDefault(IConfiguration configuration)
		{
			Configuration.customConfiguration = configuration;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002861 File Offset: 0x00000A61
		public IEnumerable<object> Services
		{
			get
			{
				return this._services;
			}
		}

		// Token: 0x0400000F RID: 15
		private readonly IEnumerable<object> _services;

		// Token: 0x04000010 RID: 16
		private static readonly object[] standardServices = new object[]
		{
			Factory.HtmlElements,
			Factory.MathElements,
			Factory.SvgElements,
			Factory.Events,
			Factory.InputTypes,
			Factory.LinkRelations,
			Factory.MediaFeatures,
			Factory.Properties,
			Factory.AttributeSelector,
			Factory.PseudoClassSelector,
			Factory.PseudoElementSelector,
			Factory.Document,
			Factory.Observer,
			Factory.BrowsingContext,
			Factory.Service,
			new Func<IBrowsingContext, IEventLoop>((IBrowsingContext ctx) => new TaskEventLoop())
		};

		// Token: 0x04000011 RID: 17
		private static readonly Configuration defaultConfiguration = new Configuration(null);

		// Token: 0x04000012 RID: 18
		private static IConfiguration customConfiguration;
	}
}
