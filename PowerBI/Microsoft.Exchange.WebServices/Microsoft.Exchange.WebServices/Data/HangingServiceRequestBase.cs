using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000123 RID: 291
	internal abstract class HangingServiceRequestBase : ServiceRequestBase
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000E3D RID: 3645 RVA: 0x0002BC3D File Offset: 0x0002AC3D
		// (remove) Token: 0x06000E3E RID: 3646 RVA: 0x0002BC56 File Offset: 0x0002AC56
		internal event HangingServiceRequestBase.HangingRequestDisconnectHandler OnDisconnect;

		// Token: 0x06000E3F RID: 3647 RVA: 0x0002BC6F File Offset: 0x0002AC6F
		internal HangingServiceRequestBase(ExchangeService service, HangingServiceRequestBase.HandleResponseObject handler, int heartbeatFrequency)
			: base(service)
		{
			this.responseHandler = handler;
			this.heartbeatFrequencyMilliseconds = heartbeatFrequency;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0002BC94 File Offset: 0x0002AC94
		internal void InternalExecute()
		{
			lock (this.lockObject)
			{
				this.response = base.ValidateAndEmitRequest(out this.request);
				this.InternalOnConnect();
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0002BCE0 File Offset: 0x0002ACE0
		private void ParseResponses(object state)
		{
			try
			{
				Guid empty = Guid.Empty;
				HangingTraceStream hangingTraceStream = null;
				MemoryStream memoryStream = null;
				try
				{
					bool flag = base.Service.IsTraceEnabledFor(TraceFlags.EwsResponse);
					using (Stream responseStream = this.response.GetResponseStream())
					{
						responseStream.ReadTimeout = 2 * this.heartbeatFrequencyMilliseconds;
						hangingTraceStream = new HangingTraceStream(responseStream, base.Service);
						if (flag)
						{
							memoryStream = new MemoryStream();
							hangingTraceStream.SetResponseCopy(memoryStream);
						}
						EwsServiceMultiResponseXmlReader ewsServiceMultiResponseXmlReader = EwsServiceMultiResponseXmlReader.Create(hangingTraceStream, base.Service);
						while (this.IsConnected)
						{
							object obj = null;
							if (flag)
							{
								try
								{
									obj = base.ReadResponse(ewsServiceMultiResponseXmlReader);
								}
								finally
								{
									base.Service.TraceXml(TraceFlags.EwsResponse, memoryStream);
								}
								memoryStream.Close();
								memoryStream = new MemoryStream();
								hangingTraceStream.SetResponseCopy(memoryStream);
							}
							else
							{
								obj = base.ReadResponse(ewsServiceMultiResponseXmlReader);
							}
							this.responseHandler(obj);
						}
					}
				}
				catch (TimeoutException ex)
				{
					this.Disconnect(HangingRequestDisconnectReason.Timeout, ex);
				}
				catch (IOException ex2)
				{
					this.Disconnect(HangingRequestDisconnectReason.Exception, ex2);
				}
				catch (HttpException ex3)
				{
					this.Disconnect(HangingRequestDisconnectReason.Exception, ex3);
				}
				catch (WebException ex4)
				{
					this.Disconnect(HangingRequestDisconnectReason.Exception, ex4);
				}
				catch (ObjectDisposedException ex5)
				{
					this.Disconnect(HangingRequestDisconnectReason.Exception, ex5);
				}
				catch (NotSupportedException)
				{
					this.Disconnect(HangingRequestDisconnectReason.UserInitiated, null);
				}
				catch (XmlException ex6)
				{
					this.Disconnect(HangingRequestDisconnectReason.UserInitiated, ex6);
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Dispose();
						memoryStream = null;
					}
				}
			}
			catch (ServiceLocalException ex7)
			{
				this.Disconnect(HangingRequestDisconnectReason.Exception, ex7);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0002BF34 File Offset: 0x0002AF34
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x0002BF3C File Offset: 0x0002AF3C
		internal bool IsConnected { get; private set; }

		// Token: 0x06000E44 RID: 3652 RVA: 0x0002BF48 File Offset: 0x0002AF48
		internal void Disconnect()
		{
			lock (this.lockObject)
			{
				this.request.Abort();
				this.response.Close();
				this.Disconnect(HangingRequestDisconnectReason.UserInitiated, null);
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0002BF9C File Offset: 0x0002AF9C
		internal void Disconnect(HangingRequestDisconnectReason reason, Exception exception)
		{
			if (this.IsConnected)
			{
				this.response.Close();
				this.InternalOnDisconnect(reason, exception);
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002BFB9 File Offset: 0x0002AFB9
		private void InternalOnConnect()
		{
			if (!this.IsConnected)
			{
				this.IsConnected = true;
				base.Service.ProcessHttpResponseHeaders(TraceFlags.EwsResponseHttpHeaders, this.response);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ParseResponses));
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002BFEF File Offset: 0x0002AFEF
		private void InternalOnDisconnect(HangingRequestDisconnectReason reason, Exception exception)
		{
			if (this.IsConnected)
			{
				this.IsConnected = false;
				this.OnDisconnect(this, new HangingRequestDisconnectEventArgs(reason, exception));
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002C013 File Offset: 0x0002B013
		protected override void ReadPreamble(EwsServiceXmlReader ewsXmlReader)
		{
		}

		// Token: 0x0400091B RID: 2331
		private const int BufferSize = 4096;

		// Token: 0x0400091C RID: 2332
		internal static bool LogAllWireBytes;

		// Token: 0x0400091D RID: 2333
		private HangingServiceRequestBase.HandleResponseObject responseHandler;

		// Token: 0x0400091E RID: 2334
		private IEwsHttpWebResponse response;

		// Token: 0x0400091F RID: 2335
		private IEwsHttpWebRequest request;

		// Token: 0x04000920 RID: 2336
		protected int heartbeatFrequencyMilliseconds;

		// Token: 0x04000921 RID: 2337
		private object lockObject = new object();

		// Token: 0x02000124 RID: 292
		// (Invoke) Token: 0x06000E4B RID: 3659
		internal delegate void HandleResponseObject(object response);

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x06000E4F RID: 3663
		internal delegate void HangingRequestDisconnectHandler(object sender, HangingRequestDisconnectEventArgs args);
	}
}
