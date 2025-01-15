using System;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004BE RID: 1214
	public class AsynchronousConnection
	{
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x0007CFDC File Offset: 0x0007B1DC
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x0007D020 File Offset: 0x0007B220
		public ConnectionLocation Location
		{
			get
			{
				object obj = this.lockObject;
				ConnectionLocation connectionLocation;
				lock (obj)
				{
					connectionLocation = this.location;
				}
				return connectionLocation;
			}
			private set
			{
				object obj = this.lockObject;
				lock (obj)
				{
					this.location = value;
				}
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0007D064 File Offset: 0x0007B264
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x0007D0A8 File Offset: 0x0007B2A8
		public AsynchronousConnection OtherEnd
		{
			get
			{
				object obj = this.lockObject;
				AsynchronousConnection asynchronousConnection;
				lock (obj)
				{
					asynchronousConnection = this.otherEnd;
				}
				return asynchronousConnection;
			}
			set
			{
				object obj = this.lockObject;
				lock (obj)
				{
					this.otherEnd = value;
				}
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0007D0EC File Offset: 0x0007B2EC
		public AsynchronousConnection(ConnectionLocation location)
		{
			this.Location = location;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0007D106 File Offset: 0x0007B306
		public void Send(AsynchronousConnectionMessage message)
		{
			this.OtherEnd.Receive(message);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0007D114 File Offset: 0x0007B314
		public void Receive(AsynchronousConnectionMessage message)
		{
			this.Location.Receive(message);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0007D122 File Offset: 0x0007B322
		internal void Remove()
		{
			this.Location.RemoveConnection();
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0007D12F File Offset: 0x0007B32F
		internal void Remove(int determinant, bool needToInformOtherEnd)
		{
			this.Location.RemoveConnection(determinant, needToInformOtherEnd);
		}

		// Token: 0x04001876 RID: 6262
		private ConnectionLocation location;

		// Token: 0x04001877 RID: 6263
		private AsynchronousConnection otherEnd;

		// Token: 0x04001878 RID: 6264
		private object lockObject = new object();
	}
}
