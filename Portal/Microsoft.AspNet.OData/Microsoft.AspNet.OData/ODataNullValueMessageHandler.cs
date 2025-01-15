using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000009 RID: 9
	public class ODataNullValueMessageHandler : DelegatingHandler
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002868 File Offset: 0x00000A68
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);
			ObjectContent objectContent = ((httpResponseMessage == null) ? null : (httpResponseMessage.Content as ObjectContent));
			if (request.Method == HttpMethod.Get && objectContent != null && objectContent.Value == null && httpResponseMessage.StatusCode == HttpStatusCode.OK)
			{
				HttpStatusCode? updatedResponseStatusCodeOrNull = ODataNullValueMessageHandler.GetUpdatedResponseStatusCodeOrNull(request.ODataProperties().Path);
				if (updatedResponseStatusCodeOrNull != null)
				{
					httpResponseMessage = HttpRequestMessageExtensions.CreateResponse(request, updatedResponseStatusCodeOrNull.Value);
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022B5 File Offset: 0x000004B5
		internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
		{
			return this.SendAsync(request, CancellationToken.None);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028C0 File Offset: 0x00000AC0
		internal static HttpStatusCode? GetUpdatedResponseStatusCodeOrNull(Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			if (oDataPath == null || oDataPath.Segments == null || oDataPath.Segments.Count == 0)
			{
				return null;
			}
			int num = oDataPath.Segments.Count - 1;
			ReadOnlyCollection<ODataPathSegment> segments = oDataPath.Segments;
			while (num >= 0 && segments[num] is TypeSegment)
			{
				num--;
			}
			if (num >= 0 && segments[num] is ValueSegment)
			{
				num--;
			}
			if (num < 0)
			{
				return null;
			}
			if (segments[num] is KeySegment)
			{
				num--;
				while (num >= 0 && segments[num] is TypeSegment)
				{
					num--;
				}
				if (num < 0)
				{
					return null;
				}
				if (segments[num] is EntitySetSegment)
				{
					return new HttpStatusCode?(HttpStatusCode.NotFound);
				}
				if (segments[num] is NavigationPropertySegment)
				{
					return new HttpStatusCode?(HttpStatusCode.NoContent);
				}
				return null;
			}
			else
			{
				PropertySegment propertySegment = segments[num] as PropertySegment;
				if (propertySegment != null)
				{
					return ODataNullValueMessageHandler.GetChangedStatusCodeForProperty(propertySegment);
				}
				NavigationPropertySegment navigationPropertySegment = segments[num] as NavigationPropertySegment;
				if (navigationPropertySegment != null)
				{
					return ODataNullValueMessageHandler.GetChangedStatusCodeForNavigationProperty(navigationPropertySegment);
				}
				if (segments[num] is SingletonSegment)
				{
					return new HttpStatusCode?(HttpStatusCode.NotFound);
				}
				return null;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A0C File Offset: 0x00000C0C
		private static HttpStatusCode? GetChangedStatusCodeForNavigationProperty(NavigationPropertySegment navigation)
		{
			EdmMultiplicity edmMultiplicity = navigation.NavigationProperty.TargetMultiplicity();
			if (edmMultiplicity != EdmMultiplicity.ZeroOrOne && edmMultiplicity != EdmMultiplicity.One)
			{
				return null;
			}
			return new HttpStatusCode?(HttpStatusCode.NoContent);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A44 File Offset: 0x00000C44
		private static HttpStatusCode? GetChangedStatusCodeForProperty(PropertySegment propertySegment)
		{
			IEdmTypeReference type = propertySegment.Property.Type;
			if (!type.IsPrimitive() && !type.IsComplex())
			{
				return null;
			}
			return new HttpStatusCode?(HttpStatusCode.NoContent);
		}
	}
}
