using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000C0 RID: 192
	[Guid("C71AE9B5-7FA4-4A48-8A96-3E2A789214B1")]
	public sealed class SynchronizeInfo
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x0002976A File Offset: 0x0002796A
		public SynchronizeInfo()
			: this(null, null, SynchronizeSecurity.SkipMembership, false)
		{
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00029776 File Offset: 0x00027976
		public SynchronizeInfo(string databaseID, string source)
			: this(databaseID, source, SynchronizeSecurity.SkipMembership, false)
		{
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00029782 File Offset: 0x00027982
		public SynchronizeInfo(string databaseID, string source, SynchronizeSecurity synchronizeSecurity)
			: this(databaseID, source, synchronizeSecurity, false)
		{
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002978E File Offset: 0x0002798E
		public SynchronizeInfo(string databaseID, string source, bool applyCompression)
			: this(databaseID, source, SynchronizeSecurity.SkipMembership, applyCompression)
		{
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002979A File Offset: 0x0002799A
		public SynchronizeInfo(string databaseID, string source, SynchronizeSecurity synchronizeSecurity, bool applyCompression)
		{
			this.databaseID = databaseID;
			this.source = source;
			this.synchronizeSecurity = synchronizeSecurity;
			this.applyCompression = applyCompression;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000297BF File Offset: 0x000279BF
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x000297C8 File Offset: 0x000279C8
		public string DatabaseID
		{
			get
			{
				return this.databaseID;
			}
			set
			{
				value = Utils.Trim(value);
				string text;
				if (value != null && !Utils.IsSyntacticallyValidID(value, typeof(Database), out text))
				{
					throw new ArgumentException(text);
				}
				this.databaseID = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00029802 File Offset: 0x00027A02
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0002980A File Offset: 0x00027A0A
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00029813 File Offset: 0x00027A13
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0002981B File Offset: 0x00027A1B
		public SynchronizeSecurity SynchronizeSecurity
		{
			get
			{
				return this.synchronizeSecurity;
			}
			set
			{
				this.synchronizeSecurity = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00029824 File Offset: 0x00027A24
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0002982C File Offset: 0x00027A2C
		public bool ApplyCompression
		{
			get
			{
				return this.applyCompression;
			}
			set
			{
				this.applyCompression = value;
			}
		}

		// Token: 0x04000518 RID: 1304
		private string databaseID;

		// Token: 0x04000519 RID: 1305
		private string source;

		// Token: 0x0400051A RID: 1306
		private SynchronizeSecurity synchronizeSecurity;

		// Token: 0x0400051B RID: 1307
		private bool applyCompression;
	}
}
