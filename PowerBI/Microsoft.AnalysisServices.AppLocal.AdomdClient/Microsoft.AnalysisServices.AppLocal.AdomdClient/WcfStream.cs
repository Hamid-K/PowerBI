using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200003C RID: 60
	internal class WcfStream : TransportCapabilitiesAwareXmlaStream
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00012614 File Offset: 0x00010814
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

		// Token: 0x06000310 RID: 784 RVA: 0x000126D8 File Offset: 0x000108D8
		public override void WriteSoapActionHeader(string action)
		{
			this.soapAction = action;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000126E4 File Offset: 0x000108E4
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

		// Token: 0x06000312 RID: 786 RVA: 0x00012750 File Offset: 0x00010950
		public override void WriteEndOfMessage()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00012764 File Offset: 0x00010964
		public override XmlaDataType GetResponseDataType()
		{
			XmlaDataType xmlaDataType;
			try
			{
				this.GetResponseStream();
				Match match = WcfStream.contentTypeRegex.Match(this.responseContentType);
				if (!match.Success)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnsupportedDataFormat(this.responseContentType), "");
				}
				XmlaDataType dataTypeFromString = DataTypes.GetDataTypeFromString(match.Groups["content_type"].Value);
				if (dataTypeFromString == XmlaDataType.Undetermined || dataTypeFromString == XmlaDataType.Unknown)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnsupportedDataFormat(this.responseContentType), "");
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

		// Token: 0x06000314 RID: 788 RVA: 0x00012810 File Offset: 0x00010A10
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

		// Token: 0x06000315 RID: 789 RVA: 0x000128B8 File Offset: 0x00010AB8
		public override void Flush()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000128CC File Offset: 0x00010ACC
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

		// Token: 0x06000317 RID: 791 RVA: 0x00012920 File Offset: 0x00010B20
		public override void Close()
		{
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00012924 File Offset: 0x00010B24
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

		// Token: 0x06000319 RID: 793 RVA: 0x000129A8 File Offset: 0x00010BA8
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

		// Token: 0x0600031A RID: 794 RVA: 0x00012A00 File Offset: 0x00010C00
		private void GetResponseStream()
		{
			if (this.outdatedVersion)
			{
				throw new AdomdConnectionException(XmlaSR.Connection_WorkbookIsOutdated, null);
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
								goto IL_007A;
							}
						}
						this.GetResponseStreamHelper();
						IL_007A:;
					}
					if (this.outdatedVersion)
					{
						throw new AdomdConnectionException(XmlaSR.Connection_WorkbookIsOutdated, null);
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

		// Token: 0x0600031B RID: 795 RVA: 0x00012B14 File Offset: 0x00010D14
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

		// Token: 0x0600031C RID: 796 RVA: 0x00012CF4 File Offset: 0x00010EF4
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

		// Token: 0x04000250 RID: 592
		public const string DefaultHttpContentType = "text/xml";

		// Token: 0x04000251 RID: 593
		public const string DefaultNegotiationFlags = "1,0,0,0,0";

		// Token: 0x04000252 RID: 594
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

		// Token: 0x04000253 RID: 595
		private const string contentTypeParam = "content_type";

		// Token: 0x04000254 RID: 596
		private static readonly string contentRegExpr = "^(\\s*)(?<content_type>(" + WcfStream.recognizedContentTypes + "))(\\s*)((;(.*))|)(\\s*)\\z";

		// Token: 0x04000255 RID: 597
		private static readonly Regex contentTypeRegex = new Regex(WcfStream.contentRegExpr, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000256 RID: 598
		private List<byte[]> buffers = new List<byte[]>();

		// Token: 0x04000257 RID: 599
		private Stream responseStream;

		// Token: 0x04000258 RID: 600
		private WindowsPrincipal logonWindowsPrincipal;

		// Token: 0x04000259 RID: 601
		private WindowsIdentity logonWindowsIdentity;

		// Token: 0x0400025A RID: 602
		private IDisposable spSite;

		// Token: 0x0400025B RID: 603
		private string loginName;

		// Token: 0x0400025C RID: 604
		private string serverEndpointAddress;

		// Token: 0x0400025D RID: 605
		private string databaseId;

		// Token: 0x0400025E RID: 606
		private string applicationName;

		// Token: 0x0400025F RID: 607
		private bool isFirstRequest = true;

		// Token: 0x04000260 RID: 608
		private bool specificVersion;

		// Token: 0x04000261 RID: 609
		private bool outdatedVersion;

		// Token: 0x04000262 RID: 610
		private string userAgent = "ADOMD.NET";

		// Token: 0x04000263 RID: 611
		private string userAddress = string.Empty;

		// Token: 0x04000264 RID: 612
		private string responseFlags;

		// Token: 0x04000265 RID: 613
		private string responseContentType;

		// Token: 0x04000266 RID: 614
		private string soapAction;
	}
}
