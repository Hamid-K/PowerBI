using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000355 RID: 853
	[Serializable]
	internal class MirrorToClause : TSqlFragment
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x00169CC9 File Offset: 0x00167EC9
		public IList<DeviceInfo> Devices
		{
			get
			{
				return this._devices;
			}
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x00169CD1 File Offset: 0x00167ED1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x00169CE0 File Offset: 0x00167EE0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Devices.Count;
			while (i < count)
			{
				this.Devices[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CEE RID: 7406
		private List<DeviceInfo> _devices = new List<DeviceInfo>();
	}
}
