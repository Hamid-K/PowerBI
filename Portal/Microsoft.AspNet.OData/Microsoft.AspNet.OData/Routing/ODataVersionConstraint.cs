using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200006A RID: 106
	public class ODataVersionConstraint : IHttpRouteConstraint
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0000D14C File Offset: 0x0000B34C
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (routeDirection == 1)
			{
				return true;
			}
			IDictionary<string, IEnumerable<string>> dictionary = request.Headers.ToDictionary((KeyValuePair<string, IEnumerable<string>> kvp) => kvp.Key, (KeyValuePair<string, IEnumerable<string>> kvp) => kvp.Value);
			ODataVersion? odataServiceVersion = request.ODataProperties().ODataServiceVersion;
			ODataVersion? odataMaxServiceVersion = request.ODataProperties().ODataMaxServiceVersion;
			return this.IsVersionMatch(dictionary, odataServiceVersion, odataMaxServiceVersion);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D1D9 File Offset: 0x0000B3D9
		public ODataVersionConstraint()
		{
			this.Version = ODataVersion.V4;
			this.IsRelaxedMatch = true;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		public ODataVersion Version { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000D200 File Offset: 0x0000B400
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000D208 File Offset: 0x0000B408
		public bool IsRelaxedMatch { get; set; }

		// Token: 0x06000416 RID: 1046 RVA: 0x0000D214 File Offset: 0x0000B414
		private bool IsVersionMatch(IDictionary<string, IEnumerable<string>> headers, ODataVersion? serviceVersion, ODataVersion? maxServiceVersion)
		{
			if (!this.ValidateVersionHeaders(headers))
			{
				return false;
			}
			ODataVersion? version = this.GetVersion(headers, serviceVersion, maxServiceVersion);
			return version != null && version.Value >= this.Version;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000D254 File Offset: 0x0000B454
		private bool ValidateVersionHeaders(IDictionary<string, IEnumerable<string>> headers)
		{
			bool flag = headers.ContainsKey("DataServiceVersion") || headers.ContainsKey("MinDataServiceVersion") || headers.ContainsKey("MaxDataServiceVersion");
			bool flag2 = headers.ContainsKey("MaxDataServiceVersion") && !headers.ContainsKey("DataServiceVersion") && !headers.ContainsKey("MinDataServiceVersion");
			bool flag3 = headers.ContainsKey("OData-MaxVersion");
			if (!this.IsRelaxedMatch)
			{
				return !flag;
			}
			return !flag || (flag3 && flag2);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		private ODataVersion? GetVersion(IDictionary<string, IEnumerable<string>> headers, ODataVersion? serviceVersion, ODataVersion? maxServiceVersion)
		{
			int headerCount = ODataVersionConstraint.GetHeaderCount("OData-Version", headers);
			int headerCount2 = ODataVersionConstraint.GetHeaderCount("OData-MaxVersion", headers);
			if (headerCount == 1 && serviceVersion != null)
			{
				return serviceVersion;
			}
			if (headerCount == 0 && headerCount2 == 1 && maxServiceVersion != null)
			{
				return maxServiceVersion;
			}
			if (headerCount == 0 && headerCount2 == 0)
			{
				return new ODataVersion?(this.Version);
			}
			return null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D33C File Offset: 0x0000B53C
		private static int GetHeaderCount(string headerName, IDictionary<string, IEnumerable<string>> headers)
		{
			IEnumerable<string> enumerable;
			if (headers.TryGetValue(headerName, out enumerable))
			{
				return enumerable.Count<string>();
			}
			return 0;
		}

		// Token: 0x040000D1 RID: 209
		internal const string ODataServiceVersionHeader = "OData-Version";

		// Token: 0x040000D2 RID: 210
		internal const string ODataMaxServiceVersionHeader = "OData-MaxVersion";

		// Token: 0x040000D3 RID: 211
		internal const string ODataMinServiceVersionHeader = "OData-MinVersion";

		// Token: 0x040000D4 RID: 212
		internal const ODataVersion DefaultODataVersion = ODataVersion.V4;

		// Token: 0x040000D5 RID: 213
		private const string PreviousODataVersionHeaderName = "DataServiceVersion";

		// Token: 0x040000D6 RID: 214
		private const string PreviousODataMaxVersionHeaderName = "MaxDataServiceVersion";

		// Token: 0x040000D7 RID: 215
		private const string PreviousODataMinVersionHeaderName = "MinDataServiceVersion";
	}
}
