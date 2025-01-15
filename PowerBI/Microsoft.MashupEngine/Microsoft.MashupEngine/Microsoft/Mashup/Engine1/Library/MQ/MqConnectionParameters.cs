using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000920 RID: 2336
	public class MqConnectionParameters
	{
		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x000E08A6 File Offset: 0x000DEAA6
		// (set) Token: 0x060042A0 RID: 17056 RVA: 0x000E08AE File Offset: 0x000DEAAE
		public string Host { get; private set; }

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x000E08B7 File Offset: 0x000DEAB7
		// (set) Token: 0x060042A2 RID: 17058 RVA: 0x000E08BF File Offset: 0x000DEABF
		public string QueueManager { get; private set; }

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x000E08C8 File Offset: 0x000DEAC8
		// (set) Token: 0x060042A4 RID: 17060 RVA: 0x000E08D0 File Offset: 0x000DEAD0
		public string Channel { get; private set; }

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x000E08D9 File Offset: 0x000DEAD9
		// (set) Token: 0x060042A6 RID: 17062 RVA: 0x000E08E1 File Offset: 0x000DEAE1
		public string Queue { get; private set; }

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x000E08EA File Offset: 0x000DEAEA
		// (set) Token: 0x060042A8 RID: 17064 RVA: 0x000E08F2 File Offset: 0x000DEAF2
		public int Port { get; private set; }

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x000E08FB File Offset: 0x000DEAFB
		// (set) Token: 0x060042AA RID: 17066 RVA: 0x000E0903 File Offset: 0x000DEB03
		public string ConnectAs { get; set; }

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x000E090C File Offset: 0x000DEB0C
		// (set) Token: 0x060042AC RID: 17068 RVA: 0x000E0914 File Offset: 0x000DEB14
		public string Username { get; set; }

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x000E091D File Offset: 0x000DEB1D
		// (set) Token: 0x060042AE RID: 17070 RVA: 0x000E0925 File Offset: 0x000DEB25
		public string Password { get; set; }

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x000E092E File Offset: 0x000DEB2E
		// (set) Token: 0x060042B0 RID: 17072 RVA: 0x000E0936 File Offset: 0x000DEB36
		public bool UseSsl { get; set; }

		// Token: 0x060042B1 RID: 17073 RVA: 0x000E0940 File Offset: 0x000DEB40
		public MqConnectionParameters(string server, string queueManager, string channel, string queue)
		{
			this.Queue = queue;
			this.QueueManager = queueManager;
			this.Channel = channel;
			string[] array = server.Split(new char[] { ':' });
			if (array.Length >= 1)
			{
				this.Host = array[0];
			}
			int num;
			if (array.Length > 1 && int.TryParse(array[1], out num))
			{
				this.Port = num;
			}
			this.UseSsl = false;
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x000E09AA File Offset: 0x000DEBAA
		public IResource Resource
		{
			get
			{
				return MqResource.New(this);
			}
		}
	}
}
