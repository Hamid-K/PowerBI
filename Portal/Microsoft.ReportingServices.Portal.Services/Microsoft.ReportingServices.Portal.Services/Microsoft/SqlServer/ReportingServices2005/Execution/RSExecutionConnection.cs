using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Web.Services.Protocols;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.SqlServer.ReportingServices;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000D9 RID: 217
	internal class RSExecutionConnection : ReportExecutionService
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x0001BFDD File Offset: 0x0001A1DD
		public RSExecutionConnection(string reportServerLocation, EndpointVersion version)
		{
			this.InitializeReportServerUrl(reportServerLocation);
			this.m_endpointVersion = version;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		public void ValidateConnection()
		{
			try
			{
				this.IsSecureMethod("");
				if (base.ServerInfoHeaderValue == null)
				{
					base.ListSecureMethods();
				}
			}
			catch (SoapException ex)
			{
				this.OnSoapException(ex);
				throw;
			}
			catch (WebException ex2)
			{
				RSExecutionConnection.MissingEndpointException.ThrowIfEndpointMissing(ex2);
				throw;
			}
			catch (InvalidOperationException ex3)
			{
				throw new RSExecutionConnection.MissingEndpointException(ex3);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001C060 File Offset: 0x0001A260
		private void SetConnectionSSLForMethod(string methodname)
		{
			this.SetConnectionSSL(this.IsSecureMethod(methodname));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001C06F File Offset: 0x0001A26F
		private void SetConnectionSSL(bool useSSL)
		{
			if (this.m_currentlyUsingSSL != useSSL)
			{
				this.m_currentlyUsingSSL = useSSL;
				base.Url = this.GetSoapURL(this.m_currentlyUsingSSL);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001C094 File Offset: 0x0001A294
		private void InitializeReportServerUrl(string reportServerLocation)
		{
			if (reportServerLocation == null)
			{
				return;
			}
			this.m_secureMethods = null;
			UriBuilder uriBuilder = new UriBuilder(reportServerLocation);
			if (string.Compare(uriBuilder.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_alwaysUseSSL = true;
				this.m_nonsecureServerUrl = null;
				this.m_secureServerUrl = uriBuilder.Uri.AbsoluteUri;
				this.m_currentlyUsingSSL = true;
			}
			else
			{
				this.m_alwaysUseSSL = false;
				this.m_nonsecureServerUrl = uriBuilder.Uri.AbsoluteUri;
				uriBuilder.Port = -1;
				uriBuilder.Scheme = Uri.UriSchemeHttps;
				this.m_secureServerUrl = uriBuilder.Uri.AbsoluteUri;
				this.m_currentlyUsingSSL = false;
			}
			base.Url = this.GetSoapURL(this.m_currentlyUsingSSL);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001C142 File Offset: 0x0001A342
		internal string GetSoapURL(bool useSSL)
		{
			return this.GetServerURL(useSSL) + "/ReportExecution2005.asmx";
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C155 File Offset: 0x0001A355
		internal string GetServerURL(bool useSSL)
		{
			if (useSSL)
			{
				return this.m_secureServerUrl;
			}
			return this.m_nonsecureServerUrl;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001C167 File Offset: 0x0001A367
		internal string UrlForRender
		{
			get
			{
				return this.GetServerURL(this.IsSecureMethod("UrlRender"));
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001C17C File Offset: 0x0001A37C
		protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)base.GetWebRequest(uri);
			if (5 == Environment.OSVersion.Version.Major && this.m_unsafeHeaderServerIsIIS5 && httpWebRequest.Credentials == CredentialCache.DefaultCredentials)
			{
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
				httpWebRequest.ConnectionGroupName = WindowsIdentity.GetCurrent().Name;
			}
			return httpWebRequest;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
		protected override WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse webResponse = base.GetWebResponse(request);
			if (string.Compare(webResponse.Headers["Server"], "Microsoft-IIS/5.0", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(webResponse.Headers["Server"], "Microsoft-IIS/5.1", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unsafeHeaderServerIsIIS5 = true;
			}
			return webResponse;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001C22F File Offset: 0x0001A42F
		protected virtual void OnSoapException(SoapException e)
		{
			RSExecutionConnection.SoapVersionMismatchException.ThrowIfVersionMismatch(e, "ReportExecution2005.asmx", SoapExceptionStrings.VersionMismatch, true);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001C244 File Offset: 0x0001A444
		private string[] GetSecureMethods()
		{
			string[] array;
			try
			{
				this.SetConnectionSSL(this.m_alwaysUseSSL);
				array = base.ListSecureMethods();
			}
			catch (Exception ex)
			{
				if (this.m_alwaysUseSSL)
				{
					throw ex;
				}
				this.m_alwaysUseSSL = true;
				this.SetConnectionSSL(true);
				try
				{
					array = base.ListSecureMethods();
				}
				catch
				{
					this.m_alwaysUseSSL = false;
					WebException ex2 = ex as WebException;
					if (ex2 != null)
					{
						HttpWebResponse httpWebResponse = ex2.Response as HttpWebResponse;
						if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Forbidden)
						{
							throw;
						}
					}
					throw ex;
				}
			}
			return array;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		private bool IsSecureMethod(string methodname)
		{
			if (this.m_alwaysUseSSL)
			{
				return true;
			}
			if (this.m_secureMethods == null)
			{
				string[] secureMethods = this.GetSecureMethods();
				if (this.m_alwaysUseSSL)
				{
					return true;
				}
				this.m_secureMethods = new RSExecutionConnection.SecureMethodsList();
				foreach (string text in secureMethods)
				{
					this.m_secureMethods.Add(text, null);
				}
			}
			return this.m_secureMethods.ContainsKey(methodname);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001C344 File Offset: 0x0001A544
		private bool CanUseKatmaiMethods
		{
			get
			{
				switch (this.m_endpointVersion)
				{
				case EndpointVersion.Yukon:
					return false;
				case EndpointVersion.Katmai:
					return true;
				case EndpointVersion.Automatic:
					return !this.m_failedUsingKatmai;
				default:
					return false;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001C380 File Offset: 0x0001A580
		private bool CanUseSql16Methods
		{
			get
			{
				switch (this.m_endpointVersion)
				{
				case EndpointVersion.Sql16:
					return true;
				case EndpointVersion.Yukon:
					return false;
				case EndpointVersion.Katmai:
					return false;
				case EndpointVersion.Automatic:
					return !this.m_failedUsingSql16;
				default:
					return false;
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		private bool CheckForDownlevelRetry(SoapException e)
		{
			EndpointVersion endpointVersion = this.m_endpointVersion;
			return endpointVersion != EndpointVersion.Sql16 && endpointVersion - EndpointVersion.Yukon <= 2 && RSExecutionConnection.SoapVersionMismatchException.IsVersionMismatch(e, "ReportExecution2005.asmx");
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001C3EC File Offset: 0x0001A5EC
		private void MarkAsFailedUsingKatmai()
		{
			this.m_failedUsingKatmai = true;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001C3F5 File Offset: 0x0001A5F5
		private void MarkAsFailedUsingSql16()
		{
			this.m_failedUsingSql16 = true;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001C400 File Offset: 0x0001A600
		public new ExecutionInfo LoadReport(string Report, string HistoryID)
		{
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadReport3", () => this.LoadReport3(Report, HistoryID));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadReport2", () => this.LoadReport2(Report, HistoryID));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadReport", () => this.LoadReport(Report, HistoryID));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001C478 File Offset: 0x0001A678
		public new ExecutionInfo LoadReportDefinition(byte[] Definition, out Warning[] warnings)
		{
			Warning[] w = null;
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadReportDefinition3", () => this.LoadReportDefinition3(Definition, out w));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadReportDefinition2", () => this.LoadReportDefinition2(Definition, out w));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadReportDefinition", () => this.LoadReportDefinition(Definition, out w));
			ExecutionInfo executionInfo = RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
			warnings = w;
			return executionInfo;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		public new ExecutionInfo SetExecutionCredentials(DataSourceCredentials[] Credentials)
		{
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("SetExecutionCredentials3", () => this.SetExecutionCredentials3(Credentials));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("SetExecutionCredentials2", () => this.SetExecutionCredentials2(Credentials));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("SetExecutionCredentials", () => this.SetExecutionCredentials(Credentials));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001C568 File Offset: 0x0001A768
		public new ExecutionInfo SetExecutionParameters(ParameterValue[] Parameters, string ParameterLanguage)
		{
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("SetExecutionParameters3", () => this.SetExecutionParameters3(Parameters, ParameterLanguage));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("SetExecutionParameters2", () => this.SetExecutionParameters2(Parameters, ParameterLanguage));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("SetExecutionParameters", () => this.SetExecutionParameters(Parameters, ParameterLanguage));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public new ExecutionInfo ResetExecution()
		{
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("ResetExecution3", () => base.ResetExecution3());
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("ResetExecution2", () => base.ResetExecution2());
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("ResetExecution", () => base.ResetExecution());
			return RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001C63C File Offset: 0x0001A83C
		public new byte[] Render(string Format, string DeviceInfo, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			return this.Render(Format, DeviceInfo, PageCountMode.Actual, out Extension, out MimeType, out Encoding, out Warnings, out StreamIds);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001C65C File Offset: 0x0001A85C
		public byte[] Render(string Format, string DeviceInfo, PageCountMode PaginationMode, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			string ext = null;
			string mime = null;
			string enc = null;
			Warning[] w = null;
			string[] sids = null;
			RSExecutionConnection.ProxyMethod<byte[]> proxyMethod = new RSExecutionConnection.ProxyMethod<byte[]>("Render2", () => this.Render2(Format, DeviceInfo, PaginationMode, out ext, out mime, out enc, out w, out sids));
			RSExecutionConnection.ProxyMethod<byte[]> proxyMethod2 = new RSExecutionConnection.ProxyMethod<byte[]>("Render", () => this.Render(Format, DeviceInfo, out ext, out mime, out enc, out w, out sids));
			byte[] array = RSExecutionConnection.ProxyMethodInvocation.Execute<byte[]>(this, proxyMethod, proxyMethod2);
			Extension = ext;
			MimeType = mime;
			Encoding = enc;
			Warnings = w;
			StreamIds = sids;
			return array;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001C714 File Offset: 0x0001A914
		public new byte[] RenderStream(string Format, string StreamID, string DeviceInfo, out string Encoding, out string MimeType)
		{
			string enc = null;
			string mime = null;
			RSExecutionConnection.ProxyMethod<byte[]> proxyMethod = new RSExecutionConnection.ProxyMethod<byte[]>("RenderStream", () => this.RenderStream(Format, StreamID, DeviceInfo, out enc, out mime));
			byte[] array = RSExecutionConnection.ProxyMethodInvocation.Execute<byte[]>(this, proxyMethod);
			Encoding = enc;
			MimeType = mime;
			return array;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001C784 File Offset: 0x0001A984
		public new void DeliverReportItem(string Format, string DeviceInfo, ExtensionSettings ExtensionSettings, string Description, string EventType, string MatchData)
		{
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("DeliverReportItem", delegate
			{
				this.DeliverReportItem(Format, DeviceInfo, ExtensionSettings, Description, EventType, MatchData);
				return 0;
			});
			RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001C7EC File Offset: 0x0001A9EC
		public new ExecutionInfo GetExecutionInfo()
		{
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("GetExecutionInfo3", () => base.GetExecutionInfo3());
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("GetExecutionInfo2", () => base.GetExecutionInfo2());
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("GetExecutionInfo", () => base.GetExecutionInfo());
			return RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001C848 File Offset: 0x0001AA48
		public new DocumentMapNode GetDocumentMap()
		{
			RSExecutionConnection.ProxyMethod<DocumentMapNode> proxyMethod = new RSExecutionConnection.ProxyMethod<DocumentMapNode>("GetDocumentMap", () => base.GetDocumentMap());
			return RSExecutionConnection.ProxyMethodInvocation.Execute<DocumentMapNode>(this, proxyMethod);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001C874 File Offset: 0x0001AA74
		public new ExecutionInfo LoadDrillthroughTarget(string DrillthroughID)
		{
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadDrillthroughTarget3", () => this.LoadDrillthroughTarget3(DrillthroughID));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod2 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadDrillthroughTarget2", () => this.LoadDrillthroughTarget2(DrillthroughID));
			RSExecutionConnection.ProxyMethod<ExecutionInfo> proxyMethod3 = new RSExecutionConnection.ProxyMethod<ExecutionInfo>("LoadDrillthroughTarget", () => this.LoadDrillthroughTarget(DrillthroughID));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<ExecutionInfo>(this, proxyMethod, proxyMethod2, proxyMethod3);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001C8E4 File Offset: 0x0001AAE4
		public new bool ToggleItem(string ToggleID)
		{
			RSExecutionConnection.ProxyMethod<bool> proxyMethod = new RSExecutionConnection.ProxyMethod<bool>("ToggleItem", () => this.ToggleItem(ToggleID));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<bool>(this, proxyMethod);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001C924 File Offset: 0x0001AB24
		public new int NavigateDocumentMap(string DocMapID)
		{
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("NavigateDocumentMap", () => this.NavigateDocumentMap(DocMapID));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001C964 File Offset: 0x0001AB64
		public new int NavigateBookmark(string BookmarkID, out string UniqueName)
		{
			string name = null;
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("NavigateBookmark", () => this.NavigateBookmark(BookmarkID, out name));
			int num = RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
			UniqueName = name;
			return num;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		public new int Sort(string SortItem, SortDirectionEnum Direction, bool Clear, out string ReportItem, out int NumPages)
		{
			string rptItem = null;
			int nPages = 0;
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("Sort", () => this.Sort(SortItem, Direction, Clear, out rptItem, out nPages));
			int num = RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
			ReportItem = rptItem;
			NumPages = nPages;
			return num;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001CA24 File Offset: 0x0001AC24
		public int Sort(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, out string ReportItem, out ExecutionInfo ExecutionInfo, out int NumPages)
		{
			string rptItem = null;
			int nPages = 0;
			ExecutionInfo2 execInfo = null;
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("Sort2", delegate
			{
				int num2 = this.Sort2(SortItem, Direction, Clear, PaginationMode, out rptItem, out execInfo);
				if (execInfo != null)
				{
					nPages = execInfo.NumPages;
				}
				return num2;
			});
			RSExecutionConnection.ProxyMethod<int> proxyMethod2 = new RSExecutionConnection.ProxyMethod<int>("Sort", () => this.Sort(SortItem, Direction, Clear, out rptItem, out nPages));
			int num = RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod, proxyMethod2);
			ExecutionInfo = execInfo;
			NumPages = nPages;
			ReportItem = rptItem;
			return num;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		public new int FindString(int startPage, int endPage, string findValue)
		{
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("FindString", () => this.FindString(startPage, endPage, findValue));
			return RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001CB14 File Offset: 0x0001AD14
		public new byte[] GetRenderResource(string Format, string DeviceInfo, out string MimeType)
		{
			string mimeType = null;
			RSExecutionConnection.ProxyMethod<byte[]> proxyMethod = new RSExecutionConnection.ProxyMethod<byte[]>("GetRenderResource", () => this.GetRenderResource(Format, DeviceInfo, out mimeType));
			byte[] array = RSExecutionConnection.ProxyMethodInvocation.Execute<byte[]>(this, proxyMethod);
			MimeType = mimeType;
			return array;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001CB6C File Offset: 0x0001AD6C
		public new Extension[] ListRenderingExtensions()
		{
			RSExecutionConnection.ProxyMethod<Extension[]> proxyMethod = new RSExecutionConnection.ProxyMethod<Extension[]>("ListRenderingExtensions", () => base.ListRenderingExtensions());
			return RSExecutionConnection.ProxyMethodInvocation.Execute<Extension[]>(this, proxyMethod);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001CB98 File Offset: 0x0001AD98
		public new void LogonUser(string userName, string password, string authority)
		{
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>(null, delegate
			{
				this.LogonUser(userName, password, authority);
				return 0;
			});
			RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		public new void Logoff()
		{
			RSExecutionConnection.ProxyMethod<int> proxyMethod = new RSExecutionConnection.ProxyMethod<int>("Logoff", delegate
			{
				base.Logoff();
				return 0;
			});
			RSExecutionConnection.ProxyMethodInvocation.Execute<int>(this, proxyMethod);
		}

		// Token: 0x040001EF RID: 495
		internal const string SoapEndpoint = "ReportExecution2005.asmx";

		// Token: 0x040001F0 RID: 496
		private string m_secureServerUrl;

		// Token: 0x040001F1 RID: 497
		private string m_nonsecureServerUrl;

		// Token: 0x040001F2 RID: 498
		private bool m_currentlyUsingSSL;

		// Token: 0x040001F3 RID: 499
		private bool m_alwaysUseSSL;

		// Token: 0x040001F4 RID: 500
		private bool m_failedUsingKatmai;

		// Token: 0x040001F5 RID: 501
		private bool m_failedUsingSql16;

		// Token: 0x040001F6 RID: 502
		private readonly EndpointVersion m_endpointVersion;

		// Token: 0x040001F7 RID: 503
		private RSExecutionConnection.SecureMethodsList m_secureMethods;

		// Token: 0x040001F8 RID: 504
		private bool m_unsafeHeaderServerIsIIS5;

		// Token: 0x020001FF RID: 511
		[Serializable]
		internal sealed class MissingEndpointException : Exception
		{
			// Token: 0x06000A15 RID: 2581 RVA: 0x00020BC2 File Offset: 0x0001EDC2
			public MissingEndpointException(Exception inner)
				: base(SoapExceptionStrings.MissingEndpoint, inner)
			{
			}

			// Token: 0x06000A16 RID: 2582 RVA: 0x000218BB File Offset: 0x0001FABB
			private MissingEndpointException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			// Token: 0x06000A17 RID: 2583 RVA: 0x000218C8 File Offset: 0x0001FAC8
			public static void ThrowIfEndpointMissing(WebException e)
			{
				if (e.Status == WebExceptionStatus.ProtocolError && e.Response != null)
				{
					HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
					if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.NotFound)
					{
						throw new RSExecutionConnection.MissingEndpointException(e);
					}
				}
			}
		}

		// Token: 0x02000200 RID: 512
		[Serializable]
		internal sealed class SoapVersionMismatchException : Exception
		{
			// Token: 0x06000A18 RID: 2584 RVA: 0x00021909 File Offset: 0x0001FB09
			private SoapVersionMismatchException(string message, Exception inner)
				: base(message, inner)
			{
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x000218BB File Offset: 0x0001FABB
			private SoapVersionMismatchException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x00021913 File Offset: 0x0001FB13
			public static void ThrowIfVersionMismatch(SoapException e, string expectedEndpoint, string message, bool includeInnerException)
			{
				if (!RSExecutionConnection.SoapVersionMismatchException.IsVersionMismatch(e, expectedEndpoint))
				{
					return;
				}
				if (includeInnerException)
				{
					throw new RSExecutionConnection.SoapVersionMismatchException(message, e);
				}
				throw new RSExecutionConnection.SoapVersionMismatchException(message, null);
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x00021931 File Offset: 0x0001FB31
			public static bool IsVersionMismatch(SoapException e, string expectedEndpoint)
			{
				return e.Code == SoapException.ClientFaultCode && !e.Actor.EndsWith(expectedEndpoint, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x02000201 RID: 513
		private sealed class SecureMethodsList : Dictionary<string, object>
		{
			// Token: 0x06000A1C RID: 2588 RVA: 0x00021957 File Offset: 0x0001FB57
			public SecureMethodsList()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}
		}

		// Token: 0x02000202 RID: 514
		private static class ProxyMethodInvocation
		{
			// Token: 0x06000A1D RID: 2589 RVA: 0x00021964 File Offset: 0x0001FB64
			internal static TReturn Execute<TReturn>(RSExecutionConnection connection, RSExecutionConnection.ProxyMethod<TReturn> method)
			{
				return RSExecutionConnection.ProxyMethodInvocation.Execute<TReturn>(connection, method, null);
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x00021970 File Offset: 0x0001FB70
			internal static TReturn Execute<TReturn>(RSExecutionConnection connection, RSExecutionConnection.ProxyMethod<TReturn> initialMethod, RSExecutionConnection.ProxyMethod<TReturn> retryMethod)
			{
				using (MonitoredScope.NewConcat("ProxyMethodInvocation.Execute - Method : ", initialMethod.MethodName))
				{
					if (connection == null)
					{
						throw new ArgumentNullException("connection");
					}
					if (initialMethod == null)
					{
						throw new ArgumentNullException("initialMethod");
					}
					RSExecutionConnection.ProxyMethod<TReturn>[] array;
					if (retryMethod != null && !connection.CanUseKatmaiMethods)
					{
						array = new RSExecutionConnection.ProxyMethod<TReturn>[] { retryMethod };
					}
					else if (retryMethod == null)
					{
						array = new RSExecutionConnection.ProxyMethod<TReturn>[] { initialMethod };
					}
					else
					{
						array = new RSExecutionConnection.ProxyMethod<TReturn>[] { initialMethod, retryMethod };
					}
					for (int i = 0; i < array.Length; i++)
					{
						RSExecutionConnection.ProxyMethod<TReturn> proxyMethod = array[i];
						try
						{
							if (!string.IsNullOrEmpty(proxyMethod.MethodName))
							{
								connection.SetConnectionSSLForMethod(proxyMethod.MethodName);
							}
							return proxyMethod.Method();
						}
						catch (SoapException ex)
						{
							if (i >= array.Length - 1 || !connection.CheckForDownlevelRetry(ex))
							{
								connection.OnSoapException(ex);
								throw;
							}
							connection.MarkAsFailedUsingKatmai();
						}
						catch (WebException ex2)
						{
							RSExecutionConnection.MissingEndpointException.ThrowIfEndpointMissing(ex2);
							throw;
						}
						catch (InvalidOperationException ex3)
						{
							throw new RSExecutionConnection.MissingEndpointException(ex3);
						}
					}
					throw new InvalidOperationException("Failed to execute method");
				}
				TReturn treturn;
				return treturn;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00021A9C File Offset: 0x0001FC9C
			internal static TReturn Execute<TReturn>(RSExecutionConnection connection, RSExecutionConnection.ProxyMethod<TReturn> sql16Method, RSExecutionConnection.ProxyMethod<TReturn> katmaiMethod, RSExecutionConnection.ProxyMethod<TReturn> yukonMethod)
			{
				using (MonitoredScope.NewConcat("ProxyMethodInvocation.Execute - Method : ", katmaiMethod.MethodName))
				{
					if (connection == null)
					{
						throw new ArgumentNullException("connection");
					}
					if (katmaiMethod == null)
					{
						throw new ArgumentNullException("initialMethod");
					}
					bool flag = yukonMethod != null;
					bool flag2 = katmaiMethod != null;
					RSExecutionConnection.ProxyMethod<TReturn>[] array;
					if (flag && !connection.CanUseKatmaiMethods && !connection.CanUseSql16Methods)
					{
						array = new RSExecutionConnection.ProxyMethod<TReturn>[] { yukonMethod };
					}
					else if (!flag && flag2 && !connection.CanUseSql16Methods)
					{
						array = new RSExecutionConnection.ProxyMethod<TReturn>[] { katmaiMethod };
					}
					else
					{
						array = new RSExecutionConnection.ProxyMethod<TReturn>[] { sql16Method, katmaiMethod, yukonMethod };
					}
					for (int i = 0; i < array.Length; i++)
					{
						RSExecutionConnection.ProxyMethod<TReturn> proxyMethod = array[i];
						try
						{
							if (!string.IsNullOrEmpty(proxyMethod.MethodName))
							{
								connection.SetConnectionSSLForMethod(proxyMethod.MethodName);
							}
							return proxyMethod.Method();
						}
						catch (SoapException ex)
						{
							if (i >= array.Length - 1 || !connection.CheckForDownlevelRetry(ex))
							{
								connection.OnSoapException(ex);
								throw;
							}
							if (connection.m_endpointVersion == EndpointVersion.Katmai)
							{
								connection.MarkAsFailedUsingKatmai();
							}
							else if (connection.m_endpointVersion == EndpointVersion.Sql16)
							{
								connection.MarkAsFailedUsingSql16();
							}
						}
						catch (WebException ex2)
						{
							RSExecutionConnection.MissingEndpointException.ThrowIfEndpointMissing(ex2);
							throw;
						}
						catch (InvalidOperationException ex3)
						{
							throw new RSExecutionConnection.MissingEndpointException(ex3);
						}
					}
					throw new InvalidOperationException("Failed to execute method");
				}
				TReturn treturn;
				return treturn;
			}
		}

		// Token: 0x02000203 RID: 515
		private sealed class ProxyMethod<TReturn>
		{
			// Token: 0x06000A20 RID: 2592 RVA: 0x00021C44 File Offset: 0x0001FE44
			internal ProxyMethod(string methodName, RSExecutionConnection.ProxyMethod<TReturn>.ProxyMethodCallback method)
			{
				if (method == null)
				{
					throw new ArgumentNullException("method");
				}
				this.m_methodName = methodName;
				this.m_method = method;
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00021C68 File Offset: 0x0001FE68
			internal RSExecutionConnection.ProxyMethod<TReturn>.ProxyMethodCallback Method
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_method;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00021C70 File Offset: 0x0001FE70
			internal string MethodName
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_methodName;
				}
			}

			// Token: 0x0400060C RID: 1548
			private readonly RSExecutionConnection.ProxyMethod<TReturn>.ProxyMethodCallback m_method;

			// Token: 0x0400060D RID: 1549
			private readonly string m_methodName;

			// Token: 0x02000219 RID: 537
			// (Invoke) Token: 0x06000A54 RID: 2644
			internal delegate TReturn ProxyMethodCallback();
		}
	}
}
