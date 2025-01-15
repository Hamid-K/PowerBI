using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000058 RID: 88
	internal class WcfStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x060003AF RID: 943 RVA: 0x000154AC File Offset: 0x000136AC
		public WcfStream(string dataSource, string serverEndpointAddress, bool specificVersion, string loginName, string databaseId, XmlaDataType desiredRequestType, XmlaDataType desiredResponseType, string applicationName)
			: base(false, desiredRequestType, desiredResponseType)
		{
			try
			{
				this.Init();
				this.spSite = SPConnectivityManager.CreateLegacyFarmSPSite(dataSource);
				this.serverEndpointAddress = serverEndpointAddress;
				this.loginName = loginName;
				this.databaseId = databaseId;
				this.applicationName = applicationName;
				this.specificVersion = specificVersion;
				this.logonWindowsIdentity = WindowsIdentity.GetCurrent();
				this.logonWindowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			}
			catch (XmlaStreamException ex)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
				throw;
			}
			catch (Exception ex2)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
				throw new XmlaStreamException(ex2);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00015570 File Offset: 0x00013770
		public override void WriteSoapActionHeader(string action)
		{
			this.soapAction = action;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001557C File Offset: 0x0001377C
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				byte[] array = new byte[size];
				Array.Copy(buffer, offset, array, 0, size);
				this.buffers.Add(array);
			}
			catch (XmlaStreamException ex)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
				throw;
			}
			catch (Exception ex2)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
				throw new XmlaStreamException(ex2);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000155E8 File Offset: 0x000137E8
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000155FC File Offset: 0x000137FC
		public override XmlaDataType GetResponseDataType()
		{
			XmlaDataType xmlaDataType;
			try
			{
				this.GetResponseStream();
				Match match = WcfStream.contentTypeRegex.Match(this.responseContentType);
				if (!match.Success)
				{
					throw new ResponseFormatException(XmlaSR.UnsupportedDataFormat(this.responseContentType), "");
				}
				XmlaDataType dataTypeFromString = DataTypes.GetDataTypeFromString(match.Groups["content_type"].Value);
				if (dataTypeFromString == XmlaDataType.Undetermined || dataTypeFromString == XmlaDataType.Unknown)
				{
					throw new ResponseFormatException(XmlaSR.UnsupportedDataFormat(this.responseContentType), "");
				}
				xmlaDataType = dataTypeFromString;
			}
			catch (XmlaStreamException ex)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
				throw;
			}
			catch (Exception ex2)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
				throw new XmlaStreamException(ex2);
			}
			return xmlaDataType;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000156A8 File Offset: 0x000138A8
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size + offset > buffer.Length)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument, "buffer");
			}
			int num;
			try
			{
				this.GetResponseStream();
				num = this.responseStream.Read(buffer, offset, size);
			}
			catch (XmlaStreamException ex)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
				throw;
			}
			catch (Exception ex2)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
				throw new XmlaStreamException(ex2);
			}
			return num;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00015750 File Offset: 0x00013950
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015764 File Offset: 0x00013964
		public override void Skip()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
			try
			{
				this.Init();
			}
			catch (XmlaStreamException ex)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
				throw;
			}
			catch (Exception ex2)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
				throw new XmlaStreamException(ex2);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000157B8 File Offset: 0x000139B8
		public override void Close()
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000157BC File Offset: 0x000139BC
		public override void Dispose()
		{
			try
			{
				this.logonWindowsPrincipal = null;
				if (this.logonWindowsIdentity != null)
				{
					this.logonWindowsIdentity.Dispose();
					this.logonWindowsIdentity = null;
				}
				if (this.spSite != null)
				{
					this.spSite.Dispose();
					this.spSite = null;
				}
				if (this.responseStream != null)
				{
					this.responseStream.Dispose();
					this.responseStream = null;
				}
				this.disposed = true;
			}
			finally
			{
				base.Dispose(true);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00015840 File Offset: 0x00013A40
		private void Init()
		{
			if (this.buffers.Count != 0)
			{
				this.buffers = new List<byte[]>();
			}
			if (this.responseStream != null)
			{
				this.responseStream.Dispose();
				this.responseStream = null;
			}
			this.responseFlags = string.Empty;
			this.responseContentType = string.Empty;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00015898 File Offset: 0x00013A98
		private void GetResponseStream()
		{
			if (this.outdatedVersion)
			{
				throw new ConnectionException(XmlaSR.Connection_WorkbookIsOutdated);
			}
			if (this.responseStream == null)
			{
				IPrincipal currentPrincipal = Thread.CurrentPrincipal;
				try
				{
					Thread.CurrentPrincipal = this.logonWindowsPrincipal;
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						if (this.logonWindowsIdentity.User != current.User)
						{
							using (this.logonWindowsIdentity.Impersonate())
							{
								this.GetResponseStreamHelper();
								goto IL_0071;
							}
						}
						this.GetResponseStreamHelper();
						IL_0071:;
					}
					if (this.outdatedVersion)
					{
						throw new ConnectionException(XmlaSR.Connection_WorkbookIsOutdated);
					}
					this.DetermineNegotiatedOptions();
				}
				catch (XmlaStreamException ex)
				{
					SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
					throw;
				}
				catch (Exception ex2)
				{
					SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
					throw new XmlaStreamException(ex2);
				}
				finally
				{
					Thread.CurrentPrincipal = currentPrincipal;
				}
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00015998 File Offset: 0x00013B98
		private void GetResponseStreamHelper()
		{
			this.responseFlags = "1,0,0,0,0";
			this.responseContentType = "text/xml";
			try
			{
				long num = 0L;
				foreach (byte[] array in this.buffers)
				{
					num += (long)array.Length;
				}
				byte[] array2;
				if (1 == this.buffers.Count)
				{
					array2 = this.buffers[0];
				}
				else
				{
					array2 = new byte[num];
					long num2 = 0L;
					foreach (byte[] array3 in this.buffers)
					{
						array3.CopyTo(array2, num2);
						num2 += (long)array3.Length;
					}
				}
				this.responseStream = SPConnectivityManager.GetLegacyFarmSPSiteResponseStream(this.spSite, new MemoryStream(array2), this.serverEndpointAddress, this.loginName, this.databaseId, this.specificVersion, this.isFirstRequest, this.userAgent, this.applicationName, this.userAddress, "1,0,0,0,0", "text/xml", ref this.responseFlags, ref this.responseContentType, ref this.outdatedVersion);
				this.isFirstRequest = false;
			}
			catch (XmlaStreamException ex)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex);
				throw;
			}
			catch (Exception ex2)
			{
				SPConnectivityManager.TraceExceptionToLegacyFarm(ex2);
				throw new XmlaStreamException(ex2);
			}
			finally
			{
				if (this.buffers.Count != 0)
				{
					this.buffers = new List<byte[]>();
				}
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015B78 File Offset: 0x00013D78
		private void DetermineNegotiatedOptions()
		{
			if (!base.NegotiatedOptions)
			{
				this.GetResponseStream();
				TransportCapabilities transportCapabilities = new TransportCapabilities();
				if (!string.IsNullOrEmpty(this.responseFlags))
				{
					transportCapabilities.FromString(this.responseFlags);
				}
				transportCapabilities.ContentTypeNegotiated = true;
				base.SetTransportCapabilities(transportCapabilities);
			}
		}

		// Token: 0x04000294 RID: 660
		public const string DefaultHttpContentType = "text/xml";

		// Token: 0x04000295 RID: 661
		public const string DefaultNegotiationFlags = "1,0,0,0,0";

		// Token: 0x04000296 RID: 662
		private static readonly string recognizedContentTypes = string.Concat(new string[]
		{
			"(",
			"text/xml".Replace("+", "\\+"),
			")|(",
			"application/xml+xpress".Replace("+", "\\+"),
			")|(",
			"application/sx".Replace("+", "\\+"),
			")|(",
			"application/sx+xpress".Replace("+", "\\+"),
			")"
		});

		// Token: 0x04000297 RID: 663
		private const string contentTypeParam = "content_type";

		// Token: 0x04000298 RID: 664
		private static readonly string contentRegExpr = "^(\\s*)(?<content_type>(" + WcfStream.recognizedContentTypes + "))(\\s*)((;(.*))|)(\\s*)\\z";

		// Token: 0x04000299 RID: 665
		private static readonly Regex contentTypeRegex = new Regex(WcfStream.contentRegExpr, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400029A RID: 666
		private List<byte[]> buffers = new List<byte[]>();

		// Token: 0x0400029B RID: 667
		private Stream responseStream;

		// Token: 0x0400029C RID: 668
		private WindowsPrincipal logonWindowsPrincipal;

		// Token: 0x0400029D RID: 669
		private WindowsIdentity logonWindowsIdentity;

		// Token: 0x0400029E RID: 670
		private IDisposable spSite;

		// Token: 0x0400029F RID: 671
		private string loginName;

		// Token: 0x040002A0 RID: 672
		private string serverEndpointAddress;

		// Token: 0x040002A1 RID: 673
		private string databaseId;

		// Token: 0x040002A2 RID: 674
		private string applicationName;

		// Token: 0x040002A3 RID: 675
		private bool isFirstRequest = true;

		// Token: 0x040002A4 RID: 676
		private bool specificVersion;

		// Token: 0x040002A5 RID: 677
		private bool outdatedVersion;

		// Token: 0x040002A6 RID: 678
		private string userAgent = "AMO";

		// Token: 0x040002A7 RID: 679
		private string userAddress = string.Empty;

		// Token: 0x040002A8 RID: 680
		private string responseFlags;

		// Token: 0x040002A9 RID: 681
		private string responseContentType;

		// Token: 0x040002AA RID: 682
		private string soapAction;
	}
}
