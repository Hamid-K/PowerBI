using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000074 RID: 116
	[Guid("27116194-AD33-468A-95CC-66AA2175DE5F")]
	public sealed class BackupInfo
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00022EF6 File Offset: 0x000210F6
		public BackupInfo()
			: this(null, false, false, null, true, null)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00022F04 File Offset: 0x00021104
		public BackupInfo(string file)
			: this(file, false, false, null, true, null)
		{
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00022F12 File Offset: 0x00021112
		public BackupInfo(string file, bool allowOverwrite)
			: this(file, allowOverwrite, false, null, true, null)
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00022F20 File Offset: 0x00021120
		public BackupInfo(string file, bool allowOverwrite, bool backupRemotePartitions)
			: this(file, allowOverwrite, backupRemotePartitions, null, true, null)
		{
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00022F2E File Offset: 0x0002112E
		public BackupInfo(string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations)
			: this(file, allowOverwrite, backupRemotePartitions, locations, true, null)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00022F3D File Offset: 0x0002113D
		public BackupInfo(string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations, bool applyCompression)
			: this(file, allowOverwrite, backupRemotePartitions, locations, applyCompression, null)
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00022F50 File Offset: 0x00021150
		public BackupInfo(string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations, bool applyCompression, string password)
		{
			this.File = file;
			this.AllowOverwrite = allowOverwrite;
			this.BackupRemotePartitions = backupRemotePartitions;
			this.locations = new BackupLocationCollection();
			if (locations != null)
			{
				int i = 0;
				int num = locations.Length;
				while (i < num)
				{
					if (locations[i] != null)
					{
						this.locations.Add(locations[i]);
					}
					i++;
				}
			}
			this.ApplyCompression = applyCompression;
			this.Password = password;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00022FBE File Offset: 0x000211BE
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00022FC6 File Offset: 0x000211C6
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				Utils.CheckValidPath(value);
				this.file = Utils.Trim(value);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00022FDA File Offset: 0x000211DA
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00022FE2 File Offset: 0x000211E2
		public bool AllowOverwrite
		{
			get
			{
				return this.allowOverwrite;
			}
			set
			{
				this.allowOverwrite = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00022FEB File Offset: 0x000211EB
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00022FF3 File Offset: 0x000211F3
		public bool BackupRemotePartitions
		{
			get
			{
				return this.backupRemotePartitions;
			}
			set
			{
				this.backupRemotePartitions = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00022FFC File Offset: 0x000211FC
		public BackupLocationCollection Locations
		{
			get
			{
				return this.locations;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00023004 File Offset: 0x00021204
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0002300C File Offset: 0x0002120C
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00023015 File Offset: 0x00021215
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0002301D File Offset: 0x0002121D
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x04000417 RID: 1047
		private string file;

		// Token: 0x04000418 RID: 1048
		private bool allowOverwrite;

		// Token: 0x04000419 RID: 1049
		private bool backupRemotePartitions;

		// Token: 0x0400041A RID: 1050
		private BackupLocationCollection locations;

		// Token: 0x0400041B RID: 1051
		private bool applyCompression;

		// Token: 0x0400041C RID: 1052
		private string password;
	}
}
