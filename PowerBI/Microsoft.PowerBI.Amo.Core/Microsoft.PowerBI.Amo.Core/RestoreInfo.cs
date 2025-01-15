using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B2 RID: 178
	[Guid("F7F08C2C-3E0F-4B29-A1E2-B77E13DD6252")]
	public sealed class RestoreInfo
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x000282A0 File Offset: 0x000264A0
		public RestoreInfo()
			: this(null, null, false, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000282BC File Offset: 0x000264BC
		public RestoreInfo(string file)
			: this(file, null, false, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000282D8 File Offset: 0x000264D8
		public RestoreInfo(string file, string databaseName)
			: this(file, databaseName, false, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000282F4 File Offset: 0x000264F4
		public RestoreInfo(string file, string databaseName, bool allowOverwrite)
			: this(file, databaseName, allowOverwrite, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00028310 File Offset: 0x00026510
		public RestoreInfo(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations)
			: this(file, databaseName, allowOverwrite, locations, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00028330 File Offset: 0x00026530
		public RestoreInfo(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security)
			: this(file, databaseName, allowOverwrite, locations, security, null, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00028350 File Offset: 0x00026550
		public RestoreInfo(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password)
			: this(file, databaseName, allowOverwrite, locations, security, password, null, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00028370 File Offset: 0x00026570
		public RestoreInfo(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password, string dbStorageLocation)
			: this(file, databaseName, allowOverwrite, locations, security, password, dbStorageLocation, ReadWriteMode.ReadWrite, false)
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00028390 File Offset: 0x00026590
		public RestoreInfo(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password, string dbStorageLocation, ReadWriteMode readWriteMode)
			: this(file, databaseName, allowOverwrite, locations, security, password, dbStorageLocation, readWriteMode, false)
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000283B4 File Offset: 0x000265B4
		public RestoreInfo(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password, string dbStorageLocation, ReadWriteMode readWriteMode, bool ignoreIncompatibilities)
		{
			this.File = file;
			this.DatabaseName = databaseName;
			this.AllowOverwrite = allowOverwrite;
			this.DatabaseReadWriteMode = readWriteMode;
			this.locations = new RestoreLocationCollection();
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
			this.Security = security;
			this.DbStorageLocation = dbStorageLocation;
			this.IgnoreIncompatibilities = ignoreIncompatibilities;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00028432 File Offset: 0x00026632
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0002843A File Offset: 0x0002663A
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				Utils.CheckValidPath(value);
				this.file = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00028449 File Offset: 0x00026649
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00028454 File Offset: 0x00026654
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
			set
			{
				value = Utils.Trim(value);
				string text;
				if (value != null && !Utils.IsSyntacticallyValidName(value, typeof(Database), out text))
				{
					throw new ArgumentException(text);
				}
				this.databaseName = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0002848E File Offset: 0x0002668E
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00028496 File Offset: 0x00026696
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0002849F File Offset: 0x0002669F
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x000284A7 File Offset: 0x000266A7
		public bool IgnoreIncompatibilities { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x000284B0 File Offset: 0x000266B0
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x000284B8 File Offset: 0x000266B8
		public bool ForceRestore { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x000284C1 File Offset: 0x000266C1
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x000284C9 File Offset: 0x000266C9
		public ReadWriteMode DatabaseReadWriteMode
		{
			get
			{
				return this.readWriteMode;
			}
			set
			{
				this.readWriteMode = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000284D2 File Offset: 0x000266D2
		public RestoreLocationCollection Locations
		{
			get
			{
				return this.locations;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x000284DA File Offset: 0x000266DA
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x000284E2 File Offset: 0x000266E2
		public RestoreSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				this.security = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000284EB File Offset: 0x000266EB
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000284F3 File Offset: 0x000266F3
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

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000284FC File Offset: 0x000266FC
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x00028504 File Offset: 0x00026704
		public string DbStorageLocation
		{
			get
			{
				return this.dbStorageLocation;
			}
			set
			{
				this.dbStorageLocation = value;
			}
		}

		// Token: 0x040004D3 RID: 1235
		private string file;

		// Token: 0x040004D4 RID: 1236
		private string databaseName;

		// Token: 0x040004D5 RID: 1237
		private bool allowOverwrite;

		// Token: 0x040004D6 RID: 1238
		private RestoreLocationCollection locations;

		// Token: 0x040004D7 RID: 1239
		private RestoreSecurity security;

		// Token: 0x040004D8 RID: 1240
		private string password;

		// Token: 0x040004D9 RID: 1241
		private string dbStorageLocation;

		// Token: 0x040004DA RID: 1242
		private ReadWriteMode readWriteMode;
	}
}
