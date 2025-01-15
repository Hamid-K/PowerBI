using System;
using System.Collections.Generic;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200010B RID: 267
	public class ControllerServices : ServicesContainer
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x000117C7 File Offset: 0x0000F9C7
		public ControllerServices(ServicesContainer parent)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			this._parent = parent;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public override bool IsSingleService(Type serviceType)
		{
			return this._parent.IsSingleService(serviceType);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public override object GetService(Type serviceType)
		{
			object obj;
			if (this._overrideSingle != null && this._overrideSingle.TryGetValue(serviceType, out obj))
			{
				return obj;
			}
			return this._parent.GetService(serviceType);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00011828 File Offset: 0x0000FA28
		public override IEnumerable<object> GetServices(Type serviceType)
		{
			List<object> list;
			if (this._overrideMulti != null && this._overrideMulti.TryGetValue(serviceType, out list))
			{
				return list;
			}
			return this._parent.GetServices(serviceType);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001185B File Offset: 0x0000FA5B
		protected override void ReplaceSingle(Type serviceType, object service)
		{
			if (this._overrideSingle == null)
			{
				this._overrideSingle = new Dictionary<Type, object>();
			}
			this._overrideSingle[serviceType] = service;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001187D File Offset: 0x0000FA7D
		protected override void ClearSingle(Type serviceType)
		{
			if (this._overrideSingle == null)
			{
				return;
			}
			this._overrideSingle.Remove(serviceType);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00011898 File Offset: 0x0000FA98
		protected override List<object> GetServiceInstances(Type serviceType)
		{
			if (this._overrideMulti == null)
			{
				this._overrideMulti = new Dictionary<Type, List<object>>();
			}
			List<object> list;
			if (!this._overrideMulti.TryGetValue(serviceType, out list))
			{
				list = new List<object>(this._parent.GetServices(serviceType));
				this._overrideMulti[serviceType] = list;
			}
			return list;
		}

		// Token: 0x040001CA RID: 458
		private Dictionary<Type, object> _overrideSingle;

		// Token: 0x040001CB RID: 459
		private Dictionary<Type, List<object>> _overrideMulti;

		// Token: 0x040001CC RID: 460
		private readonly ServicesContainer _parent;
	}
}
