using System;
using System.Fabric;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.FabricClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000068 RID: 104
	public class FabricEndpoint
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x00010748 File Offset: 0x0000E948
		public FabricEndpoint(IResolvedServiceEndpoint origin)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IResolvedServiceEndpoint>(origin, "Resolved Service Point cannot be null");
			this.Origin = origin;
			Match match = FabricEndpoint.serviceParser.Match(origin.Address);
			if (match.Success)
			{
				this.Host = match.Groups["host"].Value;
				this.Port = int.Parse(match.Groups["port"].Value, CultureInfo.InvariantCulture);
				ExtendedDiagnostics.EnsureArgumentIsBetween(this.Port, 1024, 65536, "Port");
				this.EndPointType = "tcp";
				return;
			}
			match = FabricEndpoint.httpsParser.Match(origin.Address);
			ExtendedDiagnostics.EnsureOperation(match.Success, "Invalid endpoint address: {0}".FormatWithInvariantCulture(new object[] { origin.Address }));
			this.Host = match.Groups["host"].Value;
			this.Port = 443;
			this.EndPointType = "https";
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00010854 File Offset: 0x0000EA54
		public FabricEndpoint(string address)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(address, "Address cannot be null or empty");
			Match match = FabricEndpoint.serviceParser.Match(address);
			if (match.Success)
			{
				this.Host = match.Groups["host"].Value;
				this.Port = int.Parse(match.Groups["port"].Value, CultureInfo.InvariantCulture);
				ExtendedDiagnostics.EnsureArgumentIsBetween(this.Port, 1024, 65536, "Port");
				this.EndPointType = "tcp";
				return;
			}
			match = FabricEndpoint.httpsParser.Match(address);
			ExtendedDiagnostics.EnsureOperation(match.Success, "Invalid endpoint address: {0}".FormatWithInvariantCulture(new object[] { address }));
			this.Host = match.Groups["host"].Value;
			this.Port = 443;
			this.EndPointType = "https";
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00010948 File Offset: 0x0000EB48
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00010950 File Offset: 0x0000EB50
		public IResolvedServiceEndpoint Origin { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00010959 File Offset: 0x0000EB59
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00010961 File Offset: 0x0000EB61
		public string Host { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001096A File Offset: 0x0000EB6A
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00010972 File Offset: 0x0000EB72
		public int Port { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001097B File Offset: 0x0000EB7B
		public ServiceEndpointRole Role
		{
			get
			{
				return this.Origin.Role;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00010988 File Offset: 0x0000EB88
		public string UriAuthority
		{
			get
			{
				return "{0}:{1}".FormatWithInvariantCulture(new object[] { this.Host, this.Port });
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000109B1 File Offset: 0x0000EBB1
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x000109B9 File Offset: 0x0000EBB9
		public string EndPointType { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000109C2 File Offset: 0x0000EBC2
		public Uri ServiceUri
		{
			get
			{
				return new Uri("{0}://{1}".FormatWithInvariantCulture(new object[] { this.EndPointType, this.UriAuthority }));
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000109EB File Offset: 0x0000EBEB
		public override string ToString()
		{
			return "<Address={0}, Role={1}>".FormatWithInvariantCulture(new object[] { this.UriAuthority, this.Role });
		}

		// Token: 0x04000193 RID: 403
		private const int DEFAULT_HTTPS_PORT = 443;

		// Token: 0x04000194 RID: 404
		private const string REGEX_MATCH_HOST = "host";

		// Token: 0x04000195 RID: 405
		private const string REGEX_MATCH_PORT = "port";

		// Token: 0x04000196 RID: 406
		private const string TCP_PROTOCOL = "tcp";

		// Token: 0x04000197 RID: 407
		private const string HTTPS_PROTOCOL = "https";

		// Token: 0x04000198 RID: 408
		private static readonly Regex serviceParser = new Regex("(?<host>[\\w.]+):(?<port>\\d+)", RegexOptions.Compiled);

		// Token: 0x04000199 RID: 409
		private static readonly Regex httpsParser = new Regex("https://(?<host>[\\w.]+)/.+", RegexOptions.Compiled);
	}
}
