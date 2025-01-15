using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000164 RID: 356
	public class HttpVirtualPathData : IHttpVirtualPathData
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x00018C64 File Offset: 0x00016E64
		public HttpVirtualPathData(IHttpRoute route, string virtualPath)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (virtualPath == null)
			{
				throw Error.ArgumentNull("virtualPath");
			}
			this.Route = route;
			this.VirtualPath = virtualPath;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00018C96 File Offset: 0x00016E96
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00018C9E File Offset: 0x00016E9E
		public IHttpRoute Route { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00018CA7 File Offset: 0x00016EA7
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00018CAF File Offset: 0x00016EAF
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._virtualPath = value;
			}
		}

		// Token: 0x04000296 RID: 662
		private string _virtualPath;
	}
}
