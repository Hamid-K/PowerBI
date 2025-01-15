using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000023 RID: 35
	public class UrlApplications
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000520E File Offset: 0x0000340E
		public UrlApplications(List<Application> urlApplications)
		{
			this._urlApplications = urlApplications;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005220 File Offset: 0x00003420
		public void RegisterAll()
		{
			foreach (Application application in this._urlApplications)
			{
				Logger.Info("Reserving Urls for application {0}", new object[] { application.Name });
				foreach (URL url in application.URLs)
				{
					application.Register(url);
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000052A8 File Offset: 0x000034A8
		public void UnregisterAll()
		{
			foreach (Application application in this._urlApplications)
			{
				Logger.Info("Removing reserved Urls for application {0}", new object[] { application.Name });
				foreach (URL url in application.URLs)
				{
					application.Unregister(url);
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005330 File Offset: 0x00003530
		public void Add(IEnumerable<Application> apps)
		{
			foreach (Application application in (apps ?? Enumerable.Empty<Application>()))
			{
				this.Add(application);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005384 File Offset: 0x00003584
		public void Add(Application app)
		{
			if (!this._urlApplications.Exists((Application appInList) => appInList.Name == app.Name))
			{
				this._urlApplications.Add(app);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000053C8 File Offset: 0x000035C8
		public void Remove(Application other)
		{
			foreach (Application application in this._urlApplications)
			{
				if (application.Equals(other))
				{
					this._urlApplications.Remove(application);
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000542C File Offset: 0x0000362C
		public int Size()
		{
			return this._urlApplications.Count;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005439 File Offset: 0x00003639
		public IEnumerable<Application> Values
		{
			get
			{
				return this._urlApplications;
			}
		}

		// Token: 0x040000EE RID: 238
		private readonly List<Application> _urlApplications;
	}
}
