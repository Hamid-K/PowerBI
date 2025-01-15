using System;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004C0 RID: 1216
	public class AsynchronousConnectionMessage
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x0007D13E File Offset: 0x0007B33E
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x0007D146 File Offset: 0x0007B346
		public int Type { get; private set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x0007D14F File Offset: 0x0007B34F
		// (set) Token: 0x0600297B RID: 10619 RVA: 0x0007D157 File Offset: 0x0007B357
		public int SubType { get; private set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x0007D160 File Offset: 0x0007B360
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x0007D168 File Offset: 0x0007B368
		public object Contents { get; private set; }

		// Token: 0x0600297E RID: 10622 RVA: 0x0007D171 File Offset: 0x0007B371
		public AsynchronousConnectionMessage(int type)
			: this(type, 0, null)
		{
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0007D17C File Offset: 0x0007B37C
		public AsynchronousConnectionMessage(int type, int subType)
			: this(type, subType, null)
		{
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0007D187 File Offset: 0x0007B387
		public AsynchronousConnectionMessage(int type, object contents)
			: this(type, 0, contents)
		{
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0007D192 File Offset: 0x0007B392
		public AsynchronousConnectionMessage(int type, int subType, object contents)
		{
			this.Type = type;
			this.SubType = subType;
			this.Contents = contents;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0007D1AF File Offset: 0x0007B3AF
		public void Change(int newType)
		{
			this.Type = newType;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0007D1B8 File Offset: 0x0007B3B8
		public void Change(int newType, int newSubType)
		{
			this.Type = newType;
			this.SubType = newSubType;
		}
	}
}
