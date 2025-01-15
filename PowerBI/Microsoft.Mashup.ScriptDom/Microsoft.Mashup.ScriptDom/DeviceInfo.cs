using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	internal class DeviceInfo : TSqlFragment
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x00169C43 File Offset: 0x00167E43
		// (set) Token: 0x06002C1B RID: 11291 RVA: 0x00169C4B File Offset: 0x00167E4B
		public IdentifierOrValueExpression LogicalDevice
		{
			get
			{
				return this._logicalDevice;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._logicalDevice = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x00169C5B File Offset: 0x00167E5B
		// (set) Token: 0x06002C1D RID: 11293 RVA: 0x00169C63 File Offset: 0x00167E63
		public ValueExpression PhysicalDevice
		{
			get
			{
				return this._physicalDevice;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._physicalDevice = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x00169C73 File Offset: 0x00167E73
		// (set) Token: 0x06002C1F RID: 11295 RVA: 0x00169C7B File Offset: 0x00167E7B
		public DeviceType DeviceType
		{
			get
			{
				return this._deviceType;
			}
			set
			{
				this._deviceType = value;
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x00169C84 File Offset: 0x00167E84
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x00169C90 File Offset: 0x00167E90
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.LogicalDevice != null)
			{
				this.LogicalDevice.Accept(visitor);
			}
			if (this.PhysicalDevice != null)
			{
				this.PhysicalDevice.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CEB RID: 7403
		private IdentifierOrValueExpression _logicalDevice;

		// Token: 0x04001CEC RID: 7404
		private ValueExpression _physicalDevice;

		// Token: 0x04001CED RID: 7405
		private DeviceType _deviceType;
	}
}
