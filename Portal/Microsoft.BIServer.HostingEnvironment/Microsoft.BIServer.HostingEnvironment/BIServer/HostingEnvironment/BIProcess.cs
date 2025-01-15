using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000006 RID: 6
	public class BIProcess
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024EC File Offset: 0x000006EC
		public BIProcess(string processName, string nickname, string endpointAddress)
		{
			this._nickname = nickname;
			this._processName = processName;
			this._endpointAddress = endpointAddress;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002509 File Offset: 0x00000709
		public void Update()
		{
			this._exists = Process.GetProcesses().FirstOrDefault((Process p) => p.ProcessName == this._processName) != null;
			if (!this._exists)
			{
				this._endpointActive = false;
				return;
			}
			this.GetEndpointResponse();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002540 File Offset: 0x00000740
		public void GetEndpointResponse()
		{
			if (this._endpointAddress == null || !this.Exists)
			{
				this._endpointActive = true;
				return;
			}
			WebRequest webRequest = WebRequest.Create(this._endpointAddress);
			webRequest.UseDefaultCredentials = true;
			this._requestCount++;
			webRequest.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), webRequest);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000259C File Offset: 0x0000079C
		public void GetResponseCallback(IAsyncResult ar)
		{
			this._requestCount--;
			try
			{
				new StreamReader(((HttpWebResponse)((HttpWebRequest)ar.AsyncState).EndGetResponse(ar)).GetResponseStream()).ReadToEnd();
				this._endpointActive = true;
			}
			catch (WebException ex)
			{
				if (ex.ToString().Contains("(401)"))
				{
					this._endpointActive = true;
				}
				else
				{
					this._endpointActive = false;
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000261C File Offset: 0x0000081C
		public BIProcess.BIProcessState ProcessState
		{
			get
			{
				if (!this._exists)
				{
					return BIProcess.BIProcessState.Dead;
				}
				if (!this._endpointActive)
				{
					return BIProcess.BIProcessState.Running;
				}
				return BIProcess.BIProcessState.Up;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002633 File Offset: 0x00000833
		public bool AllRequestsClosed
		{
			get
			{
				return this._requestCount == 0;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000263E File Offset: 0x0000083E
		public bool Exists
		{
			get
			{
				return this._exists;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002646 File Offset: 0x00000846
		public string Name
		{
			get
			{
				return this._processName;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000264E File Offset: 0x0000084E
		public string Nickname
		{
			get
			{
				return this._nickname;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002656 File Offset: 0x00000856
		public bool EndpointActive
		{
			get
			{
				return this._endpointActive;
			}
		}

		// Token: 0x04000032 RID: 50
		private readonly string _processName;

		// Token: 0x04000033 RID: 51
		private readonly string _nickname;

		// Token: 0x04000034 RID: 52
		private bool _endpointActive;

		// Token: 0x04000035 RID: 53
		private readonly string _endpointAddress;

		// Token: 0x04000036 RID: 54
		private bool _exists;

		// Token: 0x04000037 RID: 55
		private int _requestCount;

		// Token: 0x0200004C RID: 76
		public enum BIProcessState
		{
			// Token: 0x0400011D RID: 285
			Dead,
			// Token: 0x0400011E RID: 286
			Running,
			// Token: 0x0400011F RID: 287
			Up
		}
	}
}
