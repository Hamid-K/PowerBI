using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200026C RID: 620
	internal class CacheKeyBuilderUtilities
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x00058E64 File Offset: 0x00057064
		internal static string CollectionToKey(NameValueCollection collection)
		{
			if (collection == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in collection.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append(":");
				stringBuilder.Append(CacheKeyBuilderUtilities.EscapeSeperator(text));
				stringBuilder.Append(":");
				stringBuilder.Append(CacheKeyBuilderUtilities.EscapeSeperator(collection[text]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00058F04 File Offset: 0x00057104
		internal static string EscapeSeperator(string value)
		{
			if (value != null && (value.Contains(":") || value.Contains("&")))
			{
				return value.Replace(":", "::").Replace("&", "&&");
			}
			return value;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00058F44 File Offset: 0x00057144
		internal static void RemoveViewerParameters(NameValueCollection renderingParameters)
		{
			if (renderingParameters[RSRequestParameters.CacheDeviceInfoTags.Parameters.ToString()] != null)
			{
				renderingParameters.Remove(RSRequestParameters.CacheDeviceInfoTags.Parameters.ToString());
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00058F80 File Offset: 0x00057180
		internal static void RemoveDeviceInfoForSecondaryStreams(NameValueCollection renderingParameters)
		{
			if (renderingParameters[RSRequestParameters.CacheDeviceInfoTags.ReplacementRoot.ToString()] != null)
			{
				renderingParameters.Remove(RSRequestParameters.CacheDeviceInfoTags.ReplacementRoot.ToString());
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00058FBC File Offset: 0x000571BC
		internal static bool HasNonCacheableParameter(NameValueCollection contextParameters, CatalogItemContext context, string parameterName)
		{
			bool flag = false;
			if (contextParameters[parameterName] != null)
			{
				if (RSTrace.CacheTracer.TraceVerbose)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Item not cacheable: - {0} {1}", new object[]
					{
						parameterName,
						context.OriginalItemPath.Value
					});
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0005900C File Offset: 0x0005720C
		internal static bool HasNonCacheableParameterWithValue(NameValueCollection contextParameters, CatalogItemContext context, string parameterName, IEnumerable<string> invalidValues, IComparer<string> comparison)
		{
			if (comparison == null)
			{
				comparison = StringComparer.InvariantCultureIgnoreCase;
			}
			string text = contextParameters[parameterName];
			if (text != null)
			{
				foreach (string text2 in invalidValues)
				{
					if (comparison.Compare(text2, text) == 0)
					{
						if (RSTrace.CacheTracer.TraceVerbose)
						{
							RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Item not cacheable: - {0}={1} {2}", new object[]
							{
								parameterName,
								text,
								context.OriginalItemPath.Value
							});
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000590B0 File Offset: 0x000572B0
		internal static bool IsInHtmlRenderer(CatalogItemContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			bool flag = false;
			if (CacheKeyBuilderUtilities.HasNonCacheableParameter(context.RSRequestParameters.RenderingParameters, context, RSRequestParameters.CacheDeviceInfoTags.StreamRoot.ToString()))
			{
				flag = true;
			}
			else if (CacheKeyBuilderUtilities.HasNonCacheableParameterWithValue(context.RSRequestParameters.CatalogParameters, context, "Format".ToString(), new string[] { "HTML5", "HTML4.0" }, StringComparer.OrdinalIgnoreCase))
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00059130 File Offset: 0x00057330
		internal static void AddPaginationMode(CatalogItemContext context, NameValueCollection contextParameters)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (contextParameters == null)
			{
				throw new ArgumentNullException("contextParameters");
			}
			string text = new PageCountModeValue(context.RSRequestParameters.PaginationModeValue).Mode.ToString();
			contextParameters.Add("PageCountMode", text);
		}

		// Token: 0x04000827 RID: 2087
		internal const string RenderedReportRoot = "__RenderedSnapshotResult__";

		// Token: 0x04000828 RID: 2088
		internal const string InCollectionSeparator = ":";

		// Token: 0x04000829 RID: 2089
		internal const string InCollectionSeparatorEscaped = "::";

		// Token: 0x0400082A RID: 2090
		internal const string InterCollectionSeparator = "&";

		// Token: 0x0400082B RID: 2091
		internal const string InterCollectionSeparatorEscaped = "&&";

		// Token: 0x0400082C RID: 2092
		internal const string PrimaryStreamKeyName = "__PrimaryStream__";
	}
}
