using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000140 RID: 320
	public class DirectRouteFactoryContext
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00015D10 File Offset: 0x00013F10
		public DirectRouteFactoryContext(string prefix, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver inlineConstraintResolver, bool targetIsAction)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (inlineConstraintResolver == null)
			{
				throw new ArgumentNullException("inlineConstraintResolver");
			}
			this._prefix = prefix;
			this._actions = actions;
			this._inlineConstraintResolver = inlineConstraintResolver;
			HttpActionDescriptor httpActionDescriptor = actions.FirstOrDefault<HttpActionDescriptor>();
			if (httpActionDescriptor != null)
			{
				this._actionName = httpActionDescriptor.ActionName;
			}
			this._targetIsAction = targetIsAction;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00015D72 File Offset: 0x00013F72
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00015D7A File Offset: 0x00013F7A
		public IReadOnlyCollection<HttpActionDescriptor> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00015D82 File Offset: 0x00013F82
		public IInlineConstraintResolver InlineConstraintResolver
		{
			get
			{
				return this._inlineConstraintResolver;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00015D8A File Offset: 0x00013F8A
		public bool TargetIsAction
		{
			get
			{
				return this._targetIsAction;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00015D92 File Offset: 0x00013F92
		public IDirectRouteBuilder CreateBuilder(string template)
		{
			return this.CreateBuilderInternal(template);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00015D9B File Offset: 0x00013F9B
		internal virtual IDirectRouteBuilder CreateBuilderInternal(string template)
		{
			return this.CreateBuilder(template, this._inlineConstraintResolver);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00015DAC File Offset: 0x00013FAC
		public IDirectRouteBuilder CreateBuilder(string template, IInlineConstraintResolver constraintResolver)
		{
			DirectRouteBuilder directRouteBuilder = new DirectRouteBuilder(this._actions, this._targetIsAction);
			string text = DirectRouteFactoryContext.BuildRouteTemplate(this._prefix, template);
			this.ValidateTemplate(text);
			if (constraintResolver != null)
			{
				HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
				HttpRouteValueDictionary httpRouteValueDictionary2 = new HttpRouteValueDictionary();
				string text2 = InlineRouteTemplateParser.ParseRouteTemplate(text, httpRouteValueDictionary, httpRouteValueDictionary2, constraintResolver);
				HttpParsedRoute httpParsedRoute = RouteParser.Parse(text2);
				decimal num = RoutePrecedence.Compute(httpParsedRoute, httpRouteValueDictionary2);
				directRouteBuilder.Defaults = httpRouteValueDictionary;
				directRouteBuilder.Constraints = httpRouteValueDictionary2;
				directRouteBuilder.Template = text2;
				directRouteBuilder.Precedence = num;
				directRouteBuilder.ParsedRoute = httpParsedRoute;
			}
			else
			{
				directRouteBuilder.Template = text;
			}
			return directRouteBuilder;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00015E3C File Offset: 0x0001403C
		private static string BuildRouteTemplate(string routePrefix, string routeTemplate)
		{
			if (string.IsNullOrEmpty(routeTemplate))
			{
				return routePrefix ?? string.Empty;
			}
			if (routeTemplate.StartsWith("~/", StringComparison.Ordinal))
			{
				return routeTemplate.Substring(2);
			}
			if (string.IsNullOrEmpty(routePrefix))
			{
				return routeTemplate;
			}
			return routePrefix + "/" + routeTemplate;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00015E88 File Offset: 0x00014088
		private void ValidateTemplate(string template)
		{
			if (template != null && template.StartsWith("/", StringComparison.Ordinal))
			{
				throw new InvalidOperationException(Error.Format(SRResources.AttributeRoutes_InvalidTemplate, new object[] { template, this._actionName }));
			}
		}

		// Token: 0x04000261 RID: 609
		private readonly string _actionName;

		// Token: 0x04000262 RID: 610
		private readonly string _prefix;

		// Token: 0x04000263 RID: 611
		private readonly IReadOnlyCollection<HttpActionDescriptor> _actions;

		// Token: 0x04000264 RID: 612
		private readonly IInlineConstraintResolver _inlineConstraintResolver;

		// Token: 0x04000265 RID: 613
		private readonly bool _targetIsAction;
	}
}
