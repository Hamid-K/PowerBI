using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Http.Routing
{
	// Token: 0x0200015F RID: 351
	internal sealed class HttpParsedRoute
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x00017B3B File Offset: 0x00015D3B
		public HttpParsedRoute(List<PathSegment> pathSegments)
		{
			this.PathSegments = pathSegments;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00017B4A File Offset: 0x00015D4A
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00017B52 File Offset: 0x00015D52
		public List<PathSegment> PathSegments { get; private set; }

		// Token: 0x06000967 RID: 2407 RVA: 0x00017B5C File Offset: 0x00015D5C
		public BoundRouteTemplate Bind(IDictionary<string, object> currentValues, IDictionary<string, object> values, HttpRouteValueDictionary defaultValues, HttpRouteValueDictionary constraints)
		{
			if (currentValues == null)
			{
				currentValues = new HttpRouteValueDictionary();
			}
			if (values == null)
			{
				values = new HttpRouteValueDictionary();
			}
			if (defaultValues == null)
			{
				defaultValues = new HttpRouteValueDictionary();
			}
			HttpRouteValueDictionary acceptedValues = new HttpRouteValueDictionary();
			HashSet<string> unusedNewValues = new HashSet<string>(values.Keys, StringComparer.OrdinalIgnoreCase);
			HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				string parameterName = parameterSubsegment.ParameterName;
				object obj5;
				bool flag5 = values.TryGetValue(parameterName, out obj5);
				if (flag5)
				{
					unusedNewValues.Remove(parameterName);
				}
				object obj6;
				bool flag6 = currentValues.TryGetValue(parameterName, out obj6);
				if (flag5 && flag6 && !HttpParsedRoute.RoutePartsEqual(obj6, obj5))
				{
					return false;
				}
				if (flag5)
				{
					if (HttpParsedRoute.IsRoutePartNonEmpty(obj5))
					{
						acceptedValues.Add(parameterName, obj5);
					}
				}
				else if (flag6)
				{
					acceptedValues.Add(parameterName, obj6);
				}
				return true;
			});
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				if (HttpParsedRoute.IsRoutePartNonEmpty(keyValuePair.Value) && !acceptedValues.ContainsKey(keyValuePair.Key))
				{
					acceptedValues.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in currentValues)
			{
				string key = keyValuePair2.Key;
				if (!acceptedValues.ContainsKey(key) && HttpParsedRoute.GetParameterSubsegment(this.PathSegments, key) == null)
				{
					acceptedValues.Add(key, keyValuePair2.Value);
				}
			}
			HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				object obj7;
				if (!acceptedValues.ContainsKey(parameterSubsegment.ParameterName) && !HttpParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out obj7))
				{
					acceptedValues.Add(parameterSubsegment.ParameterName, obj7);
				}
				return true;
			});
			if (!HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				object obj8;
				return !HttpParsedRoute.IsParameterRequired(parameterSubsegment, defaultValues, out obj8) || acceptedValues.ContainsKey(parameterSubsegment.ParameterName);
			}))
			{
				return null;
			}
			HttpRouteValueDictionary otherDefaultValues = new HttpRouteValueDictionary(defaultValues);
			HttpParsedRoute.ForEachParameter(this.PathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				otherDefaultValues.Remove(parameterSubsegment.ParameterName);
				return true;
			});
			foreach (KeyValuePair<string, object> keyValuePair3 in otherDefaultValues)
			{
				object obj;
				if (values.TryGetValue(keyValuePair3.Key, out obj))
				{
					unusedNewValues.Remove(keyValuePair3.Key);
					if (!HttpParsedRoute.RoutePartsEqual(obj, keyValuePair3.Value))
					{
						return null;
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (pathSegment is PathSeparatorSegment)
				{
					if (flag && stringBuilder2.Length > 0)
					{
						if (flag2)
						{
							return null;
						}
						stringBuilder.Append(stringBuilder2.ToString());
						stringBuilder2.Length = 0;
					}
					flag = false;
					if (stringBuilder2.Length > 0 && stringBuilder2[stringBuilder2.Length - 1] == '/')
					{
						if (flag2)
						{
							return null;
						}
						stringBuilder.Append(stringBuilder2.ToString(0, stringBuilder2.Length - 1));
						stringBuilder2.Length = 0;
						flag2 = true;
					}
					else
					{
						stringBuilder2.Append("/");
					}
				}
				else
				{
					PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
					if (pathContentSegment != null)
					{
						bool flag3 = false;
						for (int j = 0; j < pathContentSegment.Subsegments.Count; j++)
						{
							PathSubsegment pathSubsegment = pathContentSegment.Subsegments[j];
							PathLiteralSubsegment pathLiteralSubsegment = pathSubsegment as PathLiteralSubsegment;
							if (pathLiteralSubsegment != null)
							{
								flag = true;
								stringBuilder2.Append(pathLiteralSubsegment.Literal);
							}
							else
							{
								PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
								if (pathParameterSubsegment != null)
								{
									if (flag && stringBuilder2.Length > 0)
									{
										if (flag2)
										{
											return null;
										}
										stringBuilder.Append(stringBuilder2.ToString());
										stringBuilder2.Length = 0;
										flag3 = true;
									}
									flag = false;
									object obj2;
									if (acceptedValues.TryGetValue(pathParameterSubsegment.ParameterName, out obj2))
									{
										unusedNewValues.Remove(pathParameterSubsegment.ParameterName);
									}
									object obj3;
									defaultValues.TryGetValue(pathParameterSubsegment.ParameterName, out obj3);
									if (HttpParsedRoute.RoutePartsEqual(obj2, obj3))
									{
										stringBuilder2.Append(Convert.ToString(obj2, CultureInfo.InvariantCulture));
									}
									else
									{
										if (flag2)
										{
											return null;
										}
										if (stringBuilder2.Length > 0)
										{
											stringBuilder.Append(stringBuilder2.ToString());
											stringBuilder2.Length = 0;
										}
										stringBuilder.Append(Convert.ToString(obj2, CultureInfo.InvariantCulture));
										flag3 = true;
									}
								}
							}
						}
						if (flag3 && stringBuilder2.Length > 0)
						{
							if (flag2)
							{
								return null;
							}
							stringBuilder.Append(stringBuilder2.ToString());
							stringBuilder2.Length = 0;
						}
					}
				}
			}
			if (flag && stringBuilder2.Length > 0)
			{
				if (flag2)
				{
					return null;
				}
				stringBuilder.Append(stringBuilder2.ToString());
			}
			if (constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair4 in constraints)
				{
					unusedNewValues.Remove(keyValuePair4.Key);
				}
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			stringBuilder3.Append(HttpParsedRoute.UriEncode(stringBuilder.ToString()));
			stringBuilder = stringBuilder3;
			if (unusedNewValues.Count > 0)
			{
				bool flag4 = true;
				foreach (string text in unusedNewValues)
				{
					object obj4;
					if (acceptedValues.TryGetValue(text, out obj4))
					{
						stringBuilder.Append(flag4 ? '?' : '&');
						flag4 = false;
						stringBuilder.Append(Uri.EscapeDataString(text));
						stringBuilder.Append('=');
						stringBuilder.Append(Uri.EscapeDataString(Convert.ToString(obj4, CultureInfo.InvariantCulture)));
					}
				}
			}
			return new BoundRouteTemplate
			{
				BoundTemplate = stringBuilder.ToString(),
				Values = acceptedValues
			};
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00018138 File Offset: 0x00016338
		private static string EscapeReservedCharacters(Match m)
		{
			return Uri.HexEscape(m.Value[0]);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001814C File Offset: 0x0001634C
		private static bool ForEachParameter(List<PathSegment> pathSegments, Func<PathParameterSubsegment, bool> action)
		{
			for (int i = 0; i < pathSegments.Count; i++)
			{
				PathSegment pathSegment = pathSegments[i];
				if (!(pathSegment is PathSeparatorSegment))
				{
					PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
					if (pathContentSegment != null)
					{
						for (int j = 0; j < pathContentSegment.Subsegments.Count; j++)
						{
							PathSubsegment pathSubsegment = pathContentSegment.Subsegments[j];
							if (!(pathSubsegment is PathLiteralSubsegment))
							{
								PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
								if (pathParameterSubsegment != null && !action(pathParameterSubsegment))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000181CC File Offset: 0x000163CC
		private static PathParameterSubsegment GetParameterSubsegment(List<PathSegment> pathSegments, string parameterName)
		{
			PathParameterSubsegment foundParameterSubsegment = null;
			HttpParsedRoute.ForEachParameter(pathSegments, delegate(PathParameterSubsegment parameterSubsegment)
			{
				if (string.Equals(parameterName, parameterSubsegment.ParameterName, StringComparison.OrdinalIgnoreCase))
				{
					foundParameterSubsegment = parameterSubsegment;
					return false;
				}
				return true;
			});
			return foundParameterSubsegment;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00018206 File Offset: 0x00016406
		private static bool IsParameterRequired(PathParameterSubsegment parameterSubsegment, HttpRouteValueDictionary defaultValues, out object defaultValue)
		{
			if (parameterSubsegment.IsCatchAll)
			{
				defaultValue = null;
				return false;
			}
			return !defaultValues.TryGetValue(parameterSubsegment.ParameterName, out defaultValue);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00018228 File Offset: 0x00016428
		private static bool IsRoutePartNonEmpty(object routePart)
		{
			string text = routePart as string;
			if (text != null)
			{
				return text.Length > 0;
			}
			return routePart != null;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00018250 File Offset: 0x00016450
		public HttpRouteValueDictionary Match(RoutingContext context, HttpRouteValueDictionary defaultValues)
		{
			List<string> pathSegments = context.PathSegments;
			if (defaultValues == null)
			{
				defaultValues = new HttpRouteValueDictionary();
			}
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.PathSegments.Count; i++)
			{
				PathSegment pathSegment = this.PathSegments[i];
				if (pathSegments.Count <= i)
				{
					flag = true;
				}
				string text = (flag ? null : pathSegments[i]);
				if (pathSegment is PathSeparatorSegment)
				{
					if (!flag && !string.Equals(text, "/", StringComparison.Ordinal))
					{
						return null;
					}
				}
				else
				{
					PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
					if (pathContentSegment != null)
					{
						if (pathContentSegment.IsCatchAll)
						{
							HttpParsedRoute.MatchCatchAll(pathContentSegment, pathSegments.Skip(i), defaultValues, httpRouteValueDictionary);
							flag2 = true;
						}
						else if (!HttpParsedRoute.MatchContentPathSegment(pathContentSegment, text, defaultValues, httpRouteValueDictionary))
						{
							return null;
						}
					}
				}
			}
			if (!flag2 && this.PathSegments.Count < pathSegments.Count)
			{
				for (int j = this.PathSegments.Count; j < pathSegments.Count; j++)
				{
					if (!RouteParser.IsSeparator(pathSegments[j]))
					{
						return null;
					}
				}
			}
			if (defaultValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in defaultValues)
				{
					if (!httpRouteValueDictionary.ContainsKey(keyValuePair.Key))
					{
						httpRouteValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return httpRouteValueDictionary;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000183C4 File Offset: 0x000165C4
		private static void MatchCatchAll(PathContentSegment contentPathSegment, IEnumerable<string> remainingRequestSegments, HttpRouteValueDictionary defaultValues, HttpRouteValueDictionary matchedValues)
		{
			string text = string.Join(string.Empty, remainingRequestSegments.ToArray<string>());
			PathParameterSubsegment pathParameterSubsegment = contentPathSegment.Subsegments[0] as PathParameterSubsegment;
			object obj;
			if (text.Length > 0)
			{
				obj = text;
			}
			else
			{
				defaultValues.TryGetValue(pathParameterSubsegment.ParameterName, out obj);
			}
			matchedValues.Add(pathParameterSubsegment.ParameterName, obj);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00018420 File Offset: 0x00016620
		private static bool MatchContentPathSegment(PathContentSegment routeSegment, string requestPathSegment, HttpRouteValueDictionary defaultValues, HttpRouteValueDictionary matchedValues)
		{
			if (string.IsNullOrEmpty(requestPathSegment))
			{
				if (routeSegment.Subsegments.Count > 1)
				{
					return false;
				}
				PathParameterSubsegment pathParameterSubsegment = routeSegment.Subsegments[0] as PathParameterSubsegment;
				if (pathParameterSubsegment == null)
				{
					return false;
				}
				object obj;
				if (defaultValues.TryGetValue(pathParameterSubsegment.ParameterName, out obj))
				{
					matchedValues.Add(pathParameterSubsegment.ParameterName, obj);
					return true;
				}
				return false;
			}
			else
			{
				if (routeSegment.Subsegments.Count == 1)
				{
					return HttpParsedRoute.MatchSingleContentPathSegment(routeSegment.Subsegments[0], requestPathSegment, matchedValues);
				}
				int num = requestPathSegment.Length;
				int i = routeSegment.Subsegments.Count - 1;
				PathParameterSubsegment pathParameterSubsegment2 = null;
				PathLiteralSubsegment pathLiteralSubsegment = null;
				while (i >= 0)
				{
					int num2 = num;
					PathParameterSubsegment pathParameterSubsegment3 = routeSegment.Subsegments[i] as PathParameterSubsegment;
					if (pathParameterSubsegment3 != null)
					{
						pathParameterSubsegment2 = pathParameterSubsegment3;
					}
					else
					{
						PathLiteralSubsegment pathLiteralSubsegment2 = routeSegment.Subsegments[i] as PathLiteralSubsegment;
						if (pathLiteralSubsegment2 != null)
						{
							pathLiteralSubsegment = pathLiteralSubsegment2;
							int num3 = num - 1;
							if (pathParameterSubsegment2 != null)
							{
								num3--;
							}
							if (num3 < 0)
							{
								return false;
							}
							int num4 = requestPathSegment.LastIndexOf(pathLiteralSubsegment2.Literal, num3, StringComparison.OrdinalIgnoreCase);
							if (num4 == -1)
							{
								return false;
							}
							if (i == routeSegment.Subsegments.Count - 1 && num4 + pathLiteralSubsegment2.Literal.Length != requestPathSegment.Length)
							{
								return false;
							}
							num2 = num4;
						}
					}
					if (pathParameterSubsegment2 != null && ((pathLiteralSubsegment != null && pathParameterSubsegment3 == null) || i == 0))
					{
						int num5;
						int num6;
						if (pathLiteralSubsegment == null)
						{
							if (i == 0)
							{
								num5 = 0;
							}
							else
							{
								num5 = num2;
							}
							num6 = num;
						}
						else if (i == 0 && pathParameterSubsegment3 != null)
						{
							num5 = 0;
							num6 = num;
						}
						else
						{
							num5 = num2 + pathLiteralSubsegment.Literal.Length;
							num6 = num - num5;
						}
						string text = requestPathSegment.Substring(num5, num6);
						if (string.IsNullOrEmpty(text))
						{
							return false;
						}
						matchedValues.Add(pathParameterSubsegment2.ParameterName, text);
						pathParameterSubsegment2 = null;
						pathLiteralSubsegment = null;
					}
					num = num2;
					i--;
				}
				return num == 0 || routeSegment.Subsegments[0] is PathParameterSubsegment;
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000185EC File Offset: 0x000167EC
		private static bool MatchSingleContentPathSegment(PathSubsegment pathSubsegment, string requestPathSegment, HttpRouteValueDictionary matchedValues)
		{
			PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
			if (pathParameterSubsegment == null)
			{
				return (pathSubsegment as PathLiteralSubsegment).Literal.Equals(requestPathSegment, StringComparison.OrdinalIgnoreCase);
			}
			matchedValues.Add(pathParameterSubsegment.ParameterName, requestPathSegment);
			return true;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00018624 File Offset: 0x00016824
		private static bool RoutePartsEqual(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Equals(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			if (a != null && b != null)
			{
				return a.Equals(b);
			}
			return a == b;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00018660 File Offset: 0x00016860
		private static string UriEncode(string str)
		{
			return Regex.Replace(Uri.EscapeUriString(str), "([#?])", new MatchEvaluator(HttpParsedRoute.EscapeReservedCharacters));
		}
	}
}
