using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000148 RID: 328
	internal static class RouteParser
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0001611C File Offset: 0x0001431C
		private static string GetLiteral(string segmentLiteral)
		{
			string text = segmentLiteral.Replace("{{", string.Empty).Replace("}}", string.Empty);
			if (text.Contains("{") || text.Contains("}"))
			{
				return null;
			}
			return segmentLiteral.Replace("{{", "{").Replace("}}", "}");
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00016184 File Offset: 0x00014384
		private static int IndexOfFirstOpenParameter(string segment, int startIndex)
		{
			for (;;)
			{
				startIndex = segment.IndexOf('{', startIndex);
				if (startIndex == -1)
				{
					break;
				}
				if (startIndex + 1 == segment.Length || (startIndex + 1 < segment.Length && segment[startIndex + 1] != '{'))
				{
					return startIndex;
				}
				startIndex += 2;
			}
			return -1;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000161C2 File Offset: 0x000143C2
		internal static bool IsSeparator(string s)
		{
			return string.Equals(s, "/", StringComparison.Ordinal);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000161D0 File Offset: 0x000143D0
		private static bool IsValidParameterName(string parameterName)
		{
			if (parameterName.Length == 0)
			{
				return false;
			}
			foreach (char c in parameterName)
			{
				if (c == '/' || c == '{' || c == '}')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00016212 File Offset: 0x00014412
		internal static bool IsInvalidRouteTemplate(string routeTemplate)
		{
			return routeTemplate.StartsWith("~", StringComparison.Ordinal) || routeTemplate.StartsWith("/", StringComparison.Ordinal) || routeTemplate.IndexOf('?') != -1;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00016240 File Offset: 0x00014440
		public static HttpParsedRoute Parse(string routeTemplate)
		{
			if (routeTemplate == null)
			{
				routeTemplate = string.Empty;
			}
			if (RouteParser.IsInvalidRouteTemplate(routeTemplate))
			{
				throw Error.Argument("routeTemplate", SRResources.Route_InvalidRouteTemplate, new object[0]);
			}
			List<string> list = RouteParser.SplitUriToPathSegmentStrings(routeTemplate);
			Exception ex = RouteParser.ValidateUriParts(list);
			if (ex != null)
			{
				throw ex;
			}
			return new HttpParsedRoute(RouteParser.SplitUriToPathSegments(list));
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00016294 File Offset: 0x00014494
		private static List<PathSubsegment> ParseUriSegment(string segment, out Exception exception)
		{
			int i = 0;
			List<PathSubsegment> list = new List<PathSubsegment>();
			while (i < segment.Length)
			{
				int num = RouteParser.IndexOfFirstOpenParameter(segment, i);
				if (num == -1)
				{
					string literal = RouteParser.GetLiteral(segment.Substring(i));
					if (literal == null)
					{
						exception = Error.Argument("routeTemplate", SRResources.Route_MismatchedParameter, new object[] { segment });
						return null;
					}
					if (literal.Length > 0)
					{
						list.Add(new PathLiteralSubsegment(literal));
						break;
					}
					break;
				}
				else
				{
					int num2 = segment.IndexOf('}', num + 1);
					if (num2 == -1)
					{
						exception = Error.Argument("routeTemplate", SRResources.Route_MismatchedParameter, new object[] { segment });
						return null;
					}
					string literal2 = RouteParser.GetLiteral(segment.Substring(i, num - i));
					if (literal2 == null)
					{
						exception = Error.Argument("routeTemplate", SRResources.Route_MismatchedParameter, new object[] { segment });
						return null;
					}
					if (literal2.Length > 0)
					{
						list.Add(new PathLiteralSubsegment(literal2));
					}
					string text = segment.Substring(num + 1, num2 - num - 1);
					list.Add(new PathParameterSubsegment(text));
					i = num2 + 1;
				}
			}
			exception = null;
			return list;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000163B0 File Offset: 0x000145B0
		private static List<PathSegment> SplitUriToPathSegments(List<string> uriParts)
		{
			List<PathSegment> list = new List<PathSegment>();
			foreach (string text in uriParts)
			{
				if (RouteParser.IsSeparator(text))
				{
					list.Add(new PathSeparatorSegment());
				}
				else
				{
					Exception ex;
					List<PathSubsegment> list2 = RouteParser.ParseUriSegment(text, out ex);
					list.Add(new PathContentSegment(list2));
				}
			}
			return list;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001642C File Offset: 0x0001462C
		internal static List<string> SplitUriToPathSegmentStrings(string uri)
		{
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(uri))
			{
				return list;
			}
			int i = 0;
			while (i < uri.Length)
			{
				int num = uri.IndexOf('/', i);
				if (num == -1)
				{
					string text = uri.Substring(i);
					if (text.Length > 0)
					{
						list.Add(text);
						break;
					}
					break;
				}
				else
				{
					string text2 = uri.Substring(i, num - i);
					if (text2.Length > 0)
					{
						list.Add(text2);
					}
					list.Add("/");
					i = num + 1;
				}
			}
			return list;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000164AC File Offset: 0x000146AC
		private static Exception ValidateUriParts(List<string> pathSegments)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			bool? flag = null;
			bool flag2 = false;
			foreach (string text in pathSegments)
			{
				if (flag2)
				{
					return Error.Argument("routeTemplate", SRResources.Route_CatchAllMustBeLast, new object[] { "routeTemplate" });
				}
				bool flag3;
				if (flag == null)
				{
					flag = new bool?(RouteParser.IsSeparator(text));
					flag3 = flag.Value;
				}
				else
				{
					flag3 = RouteParser.IsSeparator(text);
					if (flag3 && flag.Value)
					{
						return Error.Argument("routeTemplate", SRResources.Route_CannotHaveConsecutiveSeparators, new object[0]);
					}
					flag = new bool?(flag3);
				}
				if (!flag3)
				{
					Exception ex;
					List<PathSubsegment> list = RouteParser.ParseUriSegment(text, out ex);
					if (ex != null)
					{
						return ex;
					}
					ex = RouteParser.ValidateUriSegment(list, hashSet);
					if (ex != null)
					{
						return ex;
					}
					flag2 = list.Any((PathSubsegment seg) => seg is PathParameterSubsegment && ((PathParameterSubsegment)seg).IsCatchAll);
				}
			}
			return null;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000165E8 File Offset: 0x000147E8
		private static Exception ValidateUriSegment(List<PathSubsegment> pathSubsegments, HashSet<string> usedParameterNames)
		{
			bool flag = false;
			Type type = null;
			foreach (PathSubsegment pathSubsegment in pathSubsegments)
			{
				if (type != null && type == pathSubsegment.GetType())
				{
					return Error.Argument("routeTemplate", SRResources.Route_CannotHaveConsecutiveParameters, new object[0]);
				}
				type = pathSubsegment.GetType();
				if (!(pathSubsegment is PathLiteralSubsegment))
				{
					PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
					if (pathParameterSubsegment != null)
					{
						string parameterName = pathParameterSubsegment.ParameterName;
						if (pathParameterSubsegment.IsCatchAll)
						{
							flag = true;
						}
						if (!RouteParser.IsValidParameterName(parameterName))
						{
							return Error.Argument("routeTemplate", SRResources.Route_InvalidParameterName, new object[] { parameterName });
						}
						if (usedParameterNames.Contains(parameterName))
						{
							return Error.Argument("routeTemplate", SRResources.Route_RepeatedParameter, new object[] { parameterName });
						}
						usedParameterNames.Add(parameterName);
					}
				}
			}
			if (flag && pathSubsegments.Count != 1)
			{
				return Error.Argument("routeTemplate", SRResources.Route_CannotHaveCatchAllInMultiSegment, new object[0]);
			}
			return null;
		}
	}
}
