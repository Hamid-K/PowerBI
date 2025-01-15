using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A69 RID: 2665
	internal abstract class MashupHttpWebRequest : WebRequest
	{
		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06004A66 RID: 19046
		// (set) Token: 0x06004A67 RID: 19047
		public abstract string Accept { get; set; }

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06004A68 RID: 19048
		public abstract Uri Address { get; }

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06004A69 RID: 19049
		// (set) Token: 0x06004A6A RID: 19050
		public abstract bool AllowAutoRedirect { get; set; }

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06004A6B RID: 19051
		// (set) Token: 0x06004A6C RID: 19052
		public abstract bool AllowWriteStreamBuffering { get; set; }

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06004A6D RID: 19053
		// (set) Token: 0x06004A6E RID: 19054
		public abstract DecompressionMethods AutomaticDecompression { get; set; }

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06004A6F RID: 19055
		// (set) Token: 0x06004A70 RID: 19056
		public abstract X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06004A71 RID: 19057
		// (set) Token: 0x06004A72 RID: 19058
		public abstract string Connection { get; set; }

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06004A73 RID: 19059
		// (set) Token: 0x06004A74 RID: 19060
		public abstract HttpContinueDelegate ContinueDelegate { get; set; }

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06004A75 RID: 19061
		// (set) Token: 0x06004A76 RID: 19062
		public abstract CookieContainer CookieContainer { get; set; }

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x06004A77 RID: 19063
		// (set) Token: 0x06004A78 RID: 19064
		public abstract string Expect { get; set; }

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06004A79 RID: 19065
		public abstract bool HaveResponse { get; }

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06004A7A RID: 19066
		// (set) Token: 0x06004A7B RID: 19067
		public abstract DateTime IfModifiedSince { get; set; }

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06004A7C RID: 19068
		// (set) Token: 0x06004A7D RID: 19069
		public abstract bool KeepAlive { get; set; }

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06004A7E RID: 19070
		// (set) Token: 0x06004A7F RID: 19071
		public abstract int MaximumAutomaticRedirections { get; set; }

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06004A80 RID: 19072
		// (set) Token: 0x06004A81 RID: 19073
		public abstract int MaximumResponseHeadersLength { get; set; }

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06004A82 RID: 19074
		// (set) Token: 0x06004A83 RID: 19075
		public abstract string MediaType { get; set; }

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x06004A84 RID: 19076
		// (set) Token: 0x06004A85 RID: 19077
		public abstract bool Pipelined { get; set; }

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06004A86 RID: 19078
		// (set) Token: 0x06004A87 RID: 19079
		public abstract Version ProtocolVersion { get; set; }

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x06004A88 RID: 19080
		// (set) Token: 0x06004A89 RID: 19081
		public abstract int ReadWriteTimeout { get; set; }

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x06004A8A RID: 19082
		// (set) Token: 0x06004A8B RID: 19083
		public abstract string Referer { get; set; }

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06004A8C RID: 19084
		// (set) Token: 0x06004A8D RID: 19085
		public abstract bool SendChunked { get; set; }

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06004A8E RID: 19086
		public abstract ServicePoint ServicePoint { get; }

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06004A8F RID: 19087
		// (set) Token: 0x06004A90 RID: 19088
		public abstract string TransferEncoding { get; set; }

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06004A91 RID: 19089
		// (set) Token: 0x06004A92 RID: 19090
		public abstract bool UnsafeAuthenticatedConnectionSharing { get; set; }

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06004A93 RID: 19091
		// (set) Token: 0x06004A94 RID: 19092
		public abstract string UserAgent { get; set; }

		// Token: 0x06004A95 RID: 19093
		public abstract void AddRange(int range);

		// Token: 0x06004A96 RID: 19094
		public abstract void AddRange(int from, int to);

		// Token: 0x06004A97 RID: 19095
		public abstract void AddRange(string rangeSpecifier, int range);

		// Token: 0x06004A98 RID: 19096
		public abstract void AddRange(string rangeSpecifier, int from, int to);

		// Token: 0x06004A99 RID: 19097
		public abstract Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context);

		// Token: 0x06004A9A RID: 19098
		public abstract Stream GetRequestStream(out TransportContext context);
	}
}
