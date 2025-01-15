using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Http.Properties;
using System.Web.Http.Routing.Constraints;

namespace System.Web.Http.Routing
{
	// Token: 0x02000154 RID: 340
	internal class InlineRouteTemplateParser
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x0001769C File Offset: 0x0001589C
		public static string ParseRouteTemplate(string routeTemplate, IDictionary<string, object> defaults, IDictionary<string, object> constraints, IInlineConstraintResolver constraintResolver)
		{
			foreach (object obj in InlineRouteTemplateParser._parameterRegex.Matches(routeTemplate))
			{
				Match match = (Match)obj;
				string text = match.Groups["parameterName"].Value;
				if (text.StartsWith("*", StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(1);
				}
				object defaultValue = InlineRouteTemplateParser.GetDefaultValue(match.Groups["defaultValue"]);
				if (defaultValue != null)
				{
					defaults.Add(text, defaultValue);
				}
				Group group = match.Groups["constraint"];
				bool flag = defaultValue == RouteParameter.Optional;
				IHttpRouteConstraint inlineConstraint = InlineRouteTemplateParser.GetInlineConstraint(group, flag, constraintResolver);
				if (inlineConstraint != null)
				{
					constraints.Add(text, inlineConstraint);
				}
			}
			return InlineRouteTemplateParser._parameterRegex.Replace(routeTemplate, "{${parameterName}}");
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00017788 File Offset: 0x00015988
		private static object GetDefaultValue(Group defaultValueGroup)
		{
			if (!defaultValueGroup.Success)
			{
				return null;
			}
			string value = defaultValueGroup.Value;
			if (value == "?")
			{
				return RouteParameter.Optional;
			}
			return value.Substring(1);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000177C0 File Offset: 0x000159C0
		private static IHttpRouteConstraint GetInlineConstraint(Group constraintGroup, bool isOptional, IInlineConstraintResolver constraintResolver)
		{
			List<IHttpRouteConstraint> list = new List<IHttpRouteConstraint>();
			foreach (object obj in constraintGroup.Captures)
			{
				string value = ((Capture)obj).Value;
				IHttpRouteConstraint httpRouteConstraint = constraintResolver.ResolveConstraint(value);
				if (httpRouteConstraint == null)
				{
					throw Error.InvalidOperation(SRResources.HttpRouteBuilder_CouldNotResolveConstraint, new object[]
					{
						constraintResolver.GetType().Name,
						value
					});
				}
				list.Add(httpRouteConstraint);
			}
			if (list.Count > 0)
			{
				IHttpRouteConstraint httpRouteConstraint3;
				if (list.Count != 1)
				{
					IHttpRouteConstraint httpRouteConstraint2 = new CompoundRouteConstraint(list);
					httpRouteConstraint3 = httpRouteConstraint2;
				}
				else
				{
					httpRouteConstraint3 = list[0];
				}
				IHttpRouteConstraint httpRouteConstraint4 = httpRouteConstraint3;
				if (isOptional)
				{
					httpRouteConstraint4 = new OptionalRouteConstraint(httpRouteConstraint4);
				}
				return httpRouteConstraint4;
			}
			return null;
		}

		// Token: 0x0400027C RID: 636
		private const string ParameterNameRegex = "(?<parameterName>.+?)";

		// Token: 0x0400027D RID: 637
		private const string ConstraintRegex = "(:(?<constraint>.*?(\\(.*?\\))?))*";

		// Token: 0x0400027E RID: 638
		private const string DefaultValueRegex = "(?<defaultValue>\\?|(=.*?))?";

		// Token: 0x0400027F RID: 639
		private static readonly Regex _parameterRegex = new Regex("{(?<parameterName>.+?)(:(?<constraint>.*?(\\(.*?\\))?))*(?<defaultValue>\\?|(=.*?))?}", RegexOptions.Compiled);
	}
}
