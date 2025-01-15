using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A4C RID: 2636
	internal abstract class DelegatingHttpWebRequest : MashupHttpWebRequest
	{
		// Token: 0x0600494C RID: 18764 RVA: 0x000F55F4 File Offset: 0x000F37F4
		public DelegatingHttpWebRequest(MashupHttpWebRequest request)
		{
			this.request = request;
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x000F5603 File Offset: 0x000F3803
		// (set) Token: 0x0600494E RID: 18766 RVA: 0x000F5610 File Offset: 0x000F3810
		public override string Accept
		{
			get
			{
				return this.request.Accept;
			}
			set
			{
				this.request.Accept = value;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x000F561E File Offset: 0x000F381E
		public override Uri Address
		{
			get
			{
				return this.request.Address;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06004950 RID: 18768 RVA: 0x000F562B File Offset: 0x000F382B
		// (set) Token: 0x06004951 RID: 18769 RVA: 0x000F5638 File Offset: 0x000F3838
		public override bool AllowAutoRedirect
		{
			get
			{
				return this.request.AllowAutoRedirect;
			}
			set
			{
				this.request.AllowAutoRedirect = value;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x000F5646 File Offset: 0x000F3846
		// (set) Token: 0x06004953 RID: 18771 RVA: 0x000F5653 File Offset: 0x000F3853
		public override bool AllowWriteStreamBuffering
		{
			get
			{
				return this.request.AllowWriteStreamBuffering;
			}
			set
			{
				this.request.AllowWriteStreamBuffering = value;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x000F5661 File Offset: 0x000F3861
		// (set) Token: 0x06004955 RID: 18773 RVA: 0x000F566E File Offset: 0x000F386E
		public override DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.request.AutomaticDecompression;
			}
			set
			{
				this.request.AutomaticDecompression = value;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x000F567C File Offset: 0x000F387C
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x000F5689 File Offset: 0x000F3889
		public override X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.request.ClientCertificates;
			}
			set
			{
				this.request.ClientCertificates = value;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x000F5697 File Offset: 0x000F3897
		// (set) Token: 0x06004959 RID: 18777 RVA: 0x000F56A4 File Offset: 0x000F38A4
		public override string Connection
		{
			get
			{
				return this.request.Connection;
			}
			set
			{
				this.request.Connection = value;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x000F56B2 File Offset: 0x000F38B2
		// (set) Token: 0x0600495B RID: 18779 RVA: 0x000F56BF File Offset: 0x000F38BF
		public override string ConnectionGroupName
		{
			get
			{
				return this.request.ConnectionGroupName;
			}
			set
			{
				this.request.ConnectionGroupName = value;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x000F56CD File Offset: 0x000F38CD
		// (set) Token: 0x0600495D RID: 18781 RVA: 0x000F56DA File Offset: 0x000F38DA
		public override long ContentLength
		{
			get
			{
				return this.request.ContentLength;
			}
			set
			{
				this.request.ContentLength = value;
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x000F56E8 File Offset: 0x000F38E8
		// (set) Token: 0x0600495F RID: 18783 RVA: 0x000F56F5 File Offset: 0x000F38F5
		public override string ContentType
		{
			get
			{
				return this.request.ContentType;
			}
			set
			{
				this.request.ContentType = value;
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x000F5703 File Offset: 0x000F3903
		// (set) Token: 0x06004961 RID: 18785 RVA: 0x000F5710 File Offset: 0x000F3910
		public override HttpContinueDelegate ContinueDelegate
		{
			get
			{
				return this.request.ContinueDelegate;
			}
			set
			{
				this.request.ContinueDelegate = value;
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06004962 RID: 18786 RVA: 0x000F571E File Offset: 0x000F391E
		// (set) Token: 0x06004963 RID: 18787 RVA: 0x000F572B File Offset: 0x000F392B
		public override CookieContainer CookieContainer
		{
			get
			{
				return this.request.CookieContainer;
			}
			set
			{
				this.request.CookieContainer = value;
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x000F5739 File Offset: 0x000F3939
		// (set) Token: 0x06004965 RID: 18789 RVA: 0x000F5746 File Offset: 0x000F3946
		public override ICredentials Credentials
		{
			get
			{
				return this.request.Credentials;
			}
			set
			{
				this.request.Credentials = value;
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06004966 RID: 18790 RVA: 0x000F5754 File Offset: 0x000F3954
		// (set) Token: 0x06004967 RID: 18791 RVA: 0x000F5761 File Offset: 0x000F3961
		public override string Expect
		{
			get
			{
				return this.request.Expect;
			}
			set
			{
				this.request.Expect = value;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06004968 RID: 18792 RVA: 0x000F576F File Offset: 0x000F396F
		public override bool HaveResponse
		{
			get
			{
				return this.request.HaveResponse;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x000F577C File Offset: 0x000F397C
		// (set) Token: 0x0600496A RID: 18794 RVA: 0x000F5789 File Offset: 0x000F3989
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.request.Headers;
			}
			set
			{
				this.request.Headers = value;
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x000F5797 File Offset: 0x000F3997
		// (set) Token: 0x0600496C RID: 18796 RVA: 0x000F57A4 File Offset: 0x000F39A4
		public override DateTime IfModifiedSince
		{
			get
			{
				return this.request.IfModifiedSince;
			}
			set
			{
				this.request.IfModifiedSince = value;
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x000F57B2 File Offset: 0x000F39B2
		// (set) Token: 0x0600496E RID: 18798 RVA: 0x000F57BF File Offset: 0x000F39BF
		public override bool KeepAlive
		{
			get
			{
				return this.request.KeepAlive;
			}
			set
			{
				this.request.KeepAlive = value;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x0600496F RID: 18799 RVA: 0x000F57CD File Offset: 0x000F39CD
		// (set) Token: 0x06004970 RID: 18800 RVA: 0x000F57DA File Offset: 0x000F39DA
		public override int MaximumAutomaticRedirections
		{
			get
			{
				return this.request.MaximumAutomaticRedirections;
			}
			set
			{
				this.request.MaximumAutomaticRedirections = value;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06004971 RID: 18801 RVA: 0x000F57E8 File Offset: 0x000F39E8
		// (set) Token: 0x06004972 RID: 18802 RVA: 0x000F57F5 File Offset: 0x000F39F5
		public override int MaximumResponseHeadersLength
		{
			get
			{
				return this.request.MaximumResponseHeadersLength;
			}
			set
			{
				this.request.MaximumResponseHeadersLength = value;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06004973 RID: 18803 RVA: 0x000F5803 File Offset: 0x000F3A03
		// (set) Token: 0x06004974 RID: 18804 RVA: 0x000F5810 File Offset: 0x000F3A10
		public override string MediaType
		{
			get
			{
				return this.request.MediaType;
			}
			set
			{
				this.request.MediaType = value;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06004975 RID: 18805 RVA: 0x000F581E File Offset: 0x000F3A1E
		// (set) Token: 0x06004976 RID: 18806 RVA: 0x000F582B File Offset: 0x000F3A2B
		public override string Method
		{
			get
			{
				return this.request.Method;
			}
			set
			{
				this.request.Method = value;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06004977 RID: 18807 RVA: 0x000F5839 File Offset: 0x000F3A39
		// (set) Token: 0x06004978 RID: 18808 RVA: 0x000F5846 File Offset: 0x000F3A46
		public override bool Pipelined
		{
			get
			{
				return this.request.Pipelined;
			}
			set
			{
				this.request.Pipelined = value;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06004979 RID: 18809 RVA: 0x000F5854 File Offset: 0x000F3A54
		// (set) Token: 0x0600497A RID: 18810 RVA: 0x000F5861 File Offset: 0x000F3A61
		public override bool PreAuthenticate
		{
			get
			{
				return this.request.PreAuthenticate;
			}
			set
			{
				this.request.PreAuthenticate = value;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x000F586F File Offset: 0x000F3A6F
		// (set) Token: 0x0600497C RID: 18812 RVA: 0x000F587C File Offset: 0x000F3A7C
		public override Version ProtocolVersion
		{
			get
			{
				return this.request.ProtocolVersion;
			}
			set
			{
				this.request.ProtocolVersion = value;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x000F588A File Offset: 0x000F3A8A
		// (set) Token: 0x0600497E RID: 18814 RVA: 0x000F5897 File Offset: 0x000F3A97
		public override IWebProxy Proxy
		{
			get
			{
				return this.request.Proxy;
			}
			set
			{
				this.request.Proxy = value;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x0600497F RID: 18815 RVA: 0x000F58A5 File Offset: 0x000F3AA5
		// (set) Token: 0x06004980 RID: 18816 RVA: 0x000F58B2 File Offset: 0x000F3AB2
		public override int ReadWriteTimeout
		{
			get
			{
				return this.request.ReadWriteTimeout;
			}
			set
			{
				this.request.ReadWriteTimeout = value;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x000F58C0 File Offset: 0x000F3AC0
		// (set) Token: 0x06004982 RID: 18818 RVA: 0x000F58CD File Offset: 0x000F3ACD
		public override string Referer
		{
			get
			{
				return this.request.Referer;
			}
			set
			{
				this.request.Referer = value;
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x000F58DB File Offset: 0x000F3ADB
		public override Uri RequestUri
		{
			get
			{
				return this.request.RequestUri;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x000F58E8 File Offset: 0x000F3AE8
		// (set) Token: 0x06004985 RID: 18821 RVA: 0x000F58F5 File Offset: 0x000F3AF5
		public override bool SendChunked
		{
			get
			{
				return this.request.SendChunked;
			}
			set
			{
				this.request.SendChunked = value;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x000F5903 File Offset: 0x000F3B03
		public override ServicePoint ServicePoint
		{
			get
			{
				return this.request.ServicePoint;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x000F5910 File Offset: 0x000F3B10
		// (set) Token: 0x06004988 RID: 18824 RVA: 0x000F591D File Offset: 0x000F3B1D
		public override int Timeout
		{
			get
			{
				return this.request.Timeout;
			}
			set
			{
				this.request.Timeout = value;
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x000F592B File Offset: 0x000F3B2B
		// (set) Token: 0x0600498A RID: 18826 RVA: 0x000F5938 File Offset: 0x000F3B38
		public override string TransferEncoding
		{
			get
			{
				return this.request.TransferEncoding;
			}
			set
			{
				this.request.TransferEncoding = value;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x000F5946 File Offset: 0x000F3B46
		// (set) Token: 0x0600498C RID: 18828 RVA: 0x000F5953 File Offset: 0x000F3B53
		public override bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.request.UnsafeAuthenticatedConnectionSharing;
			}
			set
			{
				this.request.UnsafeAuthenticatedConnectionSharing = value;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x000F5961 File Offset: 0x000F3B61
		// (set) Token: 0x0600498E RID: 18830 RVA: 0x000F596E File Offset: 0x000F3B6E
		public override bool UseDefaultCredentials
		{
			get
			{
				return this.request.UseDefaultCredentials;
			}
			set
			{
				this.request.UseDefaultCredentials = value;
			}
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x000F597C File Offset: 0x000F3B7C
		// (set) Token: 0x06004990 RID: 18832 RVA: 0x000F5989 File Offset: 0x000F3B89
		public override string UserAgent
		{
			get
			{
				return this.request.UserAgent;
			}
			set
			{
				this.request.UserAgent = value;
			}
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x000F5997 File Offset: 0x000F3B97
		public override void Abort()
		{
			this.request.Abort();
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x000F59A4 File Offset: 0x000F3BA4
		public override void AddRange(int range)
		{
			this.request.AddRange(range);
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x000F59B2 File Offset: 0x000F3BB2
		public override void AddRange(int from, int to)
		{
			this.request.AddRange(from, to);
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x000F59C1 File Offset: 0x000F3BC1
		public override void AddRange(string rangeSpecifier, int range)
		{
			this.request.AddRange(rangeSpecifier, range);
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x000F59D0 File Offset: 0x000F3BD0
		public override void AddRange(string rangeSpecifier, int from, int to)
		{
			this.request.AddRange(rangeSpecifier, from, to);
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x000F59E0 File Offset: 0x000F3BE0
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.request.BeginGetRequestStream(callback, state);
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x000F59EF File Offset: 0x000F3BEF
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.request.BeginGetResponse(callback, state);
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x000F59FE File Offset: 0x000F3BFE
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.request.EndGetRequestStream(asyncResult);
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x000F5A0C File Offset: 0x000F3C0C
		public override Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
		{
			return this.request.EndGetRequestStream(asyncResult, out context);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x000F5A1B File Offset: 0x000F3C1B
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			return this.request.EndGetResponse(asyncResult);
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x000F5A29 File Offset: 0x000F3C29
		public override Stream GetRequestStream()
		{
			return this.request.GetRequestStream();
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x000F5A36 File Offset: 0x000F3C36
		public override Stream GetRequestStream(out TransportContext context)
		{
			return this.request.GetRequestStream(out context);
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x000F5A44 File Offset: 0x000F3C44
		public override WebResponse GetResponse()
		{
			return this.request.GetResponse();
		}

		// Token: 0x04002745 RID: 10053
		private readonly MashupHttpWebRequest request;
	}
}
