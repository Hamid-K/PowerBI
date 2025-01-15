using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000013 RID: 19
	internal sealed class MashupConnectionPool2
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000032D4 File Offset: 0x000014D4
		public MashupConnectionPool2(string identity)
		{
			this.syncRoot = new object();
			this.identity = identity;
			this.dataCacheSettings = new MashupCacheSettings2();
			this.softCancellationTimeout = TimeSpan.FromSeconds(10.0);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003346 File Offset: 0x00001546
		public static MashupConnectionPool2 DefaultPool
		{
			get
			{
				return MashupConnectionPool2.defaultPool;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000334D File Offset: 0x0000154D
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003358 File Offset: 0x00001558
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000339C File Offset: 0x0000159C
		public int ContainerMaxCount
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.maxContainerCount;
				}
				return num;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.maxContainerCount != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						if (value < 1 || value < this.ContainerMinCount)
						{
							throw new ArgumentOutOfRangeException(ProviderErrorStrings.ContainerMaxCountOutOfRange);
						}
						this.maxContainerCount = value;
					}
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003414 File Offset: 0x00001614
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003458 File Offset: 0x00001658
		public int ContainerMinCount
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.minContainerCount;
				}
				return num;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.minContainerCount != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						if (value < 0 || value > this.ContainerMaxCount)
						{
							throw new ArgumentOutOfRangeException(ProviderErrorStrings.ContainerMinCountOutOfRange);
						}
						this.minContainerCount = value;
					}
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000034D0 File Offset: 0x000016D0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003514 File Offset: 0x00001714
		public int SharedMaxWorkingSetInMB
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.sharedMaxWorkingSetInMB;
				}
				return num;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.sharedMaxWorkingSetInMB != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						if (value != -1 && value < 1)
						{
							throw new ArgumentOutOfRangeException();
						}
						this.sharedMaxWorkingSetInMB = value;
					}
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003584 File Offset: 0x00001784
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000035C8 File Offset: 0x000017C8
		public int SharedMaxCommitInMB
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.sharedMaxCommitInMB;
				}
				return num;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.sharedMaxCommitInMB != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.ValidateJobOptions(value, this.containerMaxCommitInMB, this.containerProcessorAffinity, this.containerInheritsHostJob);
						if (value != -1 && value < 1)
						{
							throw new ArgumentOutOfRangeException();
						}
						this.sharedMaxCommitInMB = value;
					}
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003650 File Offset: 0x00001850
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003694 File Offset: 0x00001894
		public int ContainerMaxWorkingSetInMB
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.containerMaxWorkingSetInMB;
				}
				return num;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.containerMaxWorkingSetInMB != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						bool flag2 = IntPtr.Size == 8;
						if (value < 1 || (!flag2 && 1048576L * (long)value > 2147483647L))
						{
							throw new ArgumentOutOfRangeException();
						}
						this.containerMaxWorkingSetInMB = value;
					}
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000371C File Offset: 0x0000191C
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003760 File Offset: 0x00001960
		public int ContainerMaxCommitInMB
		{
			get
			{
				object obj = this.syncRoot;
				int num;
				lock (obj)
				{
					num = this.containerMaxCommitInMB;
				}
				return num;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.containerMaxCommitInMB != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.ValidateJobOptions(this.sharedMaxCommitInMB, value, this.containerProcessorAffinity, this.containerInheritsHostJob);
						bool flag2 = IntPtr.Size == 8;
						if ((value != -1 && value < 1) || (!flag2 && 1048576L * (long)value > (long)((ulong)(-1))))
						{
							throw new ArgumentOutOfRangeException();
						}
						this.containerMaxCommitInMB = value;
					}
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003800 File Offset: 0x00001A00
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003844 File Offset: 0x00001A44
		public TimeSpan? ContainerTimeToLive
		{
			get
			{
				object obj = this.syncRoot;
				TimeSpan? timeSpan;
				lock (obj)
				{
					timeSpan = this.containerTimeToLive;
				}
				return timeSpan;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!(this.containerTimeToLive == value))
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						if (value < TimeSpan.Zero)
						{
							throw new ArgumentOutOfRangeException();
						}
						this.containerTimeToLive = value;
					}
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003904 File Offset: 0x00001B04
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003948 File Offset: 0x00001B48
		public bool[] ContainerProcessorAffinity
		{
			get
			{
				object obj = this.syncRoot;
				bool[] array;
				lock (obj)
				{
					array = this.containerProcessorAffinity;
				}
				return array;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!this.cleanedUp)
					{
						throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
					}
					this.ValidateJobOptions(this.sharedMaxCommitInMB, this.containerMaxCommitInMB, value, this.containerInheritsHostJob);
					if (value != null)
					{
						if (!WindowsVersion.IsAtLeast(WindowsVersion.Windows8))
						{
							throw new InvalidOperationException(ProviderErrorStrings.ProcessorAffinityWindowsVersionNotSupported);
						}
						if (value.Length > MachineInfo.GetTotalProcessorCount())
						{
							throw new ArgumentOutOfRangeException(ProviderErrorStrings.MachineProcessorCountExceeded);
						}
						if (value.All((bool f) => !f))
						{
							throw new ArgumentOutOfRangeException(ProviderErrorStrings.NoProcessorAffinity);
						}
					}
					this.containerProcessorAffinity = value;
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003A18 File Offset: 0x00001C18
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003A5C File Offset: 0x00001C5C
		internal bool ContainerInheritsHostJob
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.containerInheritsHostJob;
				}
				return flag2;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.containerInheritsHostJob != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.ValidateJobOptions(this.sharedMaxCommitInMB, this.containerMaxCommitInMB, this.containerProcessorAffinity, value);
						this.containerInheritsHostJob = value;
					}
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003AD4 File Offset: 0x00001CD4
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003B18 File Offset: 0x00001D18
		internal bool InProcess
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.inProcess;
				}
				return flag2;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.inProcess != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.inProcess = value;
					}
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003B78 File Offset: 0x00001D78
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003BC0 File Offset: 0x00001DC0
		internal static bool AllowNet45Containers
		{
			get
			{
				object obj = MashupConnectionPool2.defaultPool.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnectionPool2.allowNet45Containers;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnectionPool2.defaultPool.syncRoot;
				lock (obj)
				{
					if (MashupConnectionPool2.allowNet45Containers != value)
					{
						if (!MashupConnectionPool2.defaultPool.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						MashupConnectionPool2.allowNet45Containers = value;
					}
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003C28 File Offset: 0x00001E28
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003C70 File Offset: 0x00001E70
		internal static bool UseNativeContainerLoader
		{
			get
			{
				object obj = MashupConnectionPool2.defaultPool.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnectionPool2.useNativeContainerLoader;
				}
				return flag2;
			}
			set
			{
				if (value && FxVersionDetector.InstalledFxVersion == ClrVersion.Net35)
				{
					throw new NotSupportedException("Native loader does not support .NET 3.5");
				}
				object obj = MashupConnectionPool2.defaultPool.syncRoot;
				lock (obj)
				{
					if (MashupConnectionPool2.useNativeContainerLoader != value)
					{
						if (!MashupConnectionPool2.defaultPool.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						MashupConnectionPool2.useNativeContainerLoader = value;
					}
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003CEC File Offset: 0x00001EEC
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003D34 File Offset: 0x00001F34
		internal static bool UseHostFrameworkVersion
		{
			get
			{
				object obj = MashupConnectionPool2.defaultPool.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnectionPool2.useHostFrameworkVersion;
				}
				return flag2;
			}
			set
			{
				if (value && FxVersionDetector.InstalledFxVersion == ClrVersion.Net35)
				{
					throw new NotSupportedException("Use host framework version does not support .NET 3.5");
				}
				object obj = MashupConnectionPool2.defaultPool.syncRoot;
				lock (obj)
				{
					if (MashupConnectionPool2.useHostFrameworkVersion != value)
					{
						if (!MashupConnectionPool2.defaultPool.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						MashupConnectionPool2.useHostFrameworkVersion = value;
					}
				}
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003DB0 File Offset: 0x00001FB0
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public TimeSpan SessionTimeToLive
		{
			get
			{
				object obj = this.syncRoot;
				TimeSpan timeSpan;
				lock (obj)
				{
					timeSpan = this.sessionTimeToLive;
				}
				return timeSpan;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!(this.sessionTimeToLive == value))
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						if (value < TimeSpan.Zero)
						{
							throw new ArgumentOutOfRangeException();
						}
						this.sessionTimeToLive = value;
					}
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003E6C File Offset: 0x0000206C
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003EB4 File Offset: 0x000020B4
		public TimeSpan CacheTimeToLive
		{
			get
			{
				object obj = this.syncRoot;
				TimeSpan timeToLive;
				lock (obj)
				{
					timeToLive = this.dataCacheSettings.TimeToLive;
				}
				return timeToLive;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!(this.dataCacheSettings.TimeToLive == value))
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						if (value < TimeSpan.Zero)
						{
							throw new ArgumentOutOfRangeException();
						}
						this.dataCacheSettings.TimeToLive = value;
					}
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003F38 File Offset: 0x00002138
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003F7C File Offset: 0x0000217C
		public MashupCacheSettings2 DataCacheSettings
		{
			get
			{
				object obj = this.syncRoot;
				MashupCacheSettings2 mashupCacheSettings;
				lock (obj)
				{
					mashupCacheSettings = this.dataCacheSettings;
				}
				return mashupCacheSettings;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!this.cleanedUp)
					{
						throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
					}
					if (value == null)
					{
						throw new ArgumentNullException();
					}
					this.dataCacheSettings = value;
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003FDC File Offset: 0x000021DC
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00004020 File Offset: 0x00002220
		public MashupCacheSettings2 MetadataCacheSettings
		{
			get
			{
				object obj = this.syncRoot;
				MashupCacheSettings2 mashupCacheSettings;
				lock (obj)
				{
					mashupCacheSettings = this.metadataCacheSettings;
				}
				return mashupCacheSettings;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!this.cleanedUp)
					{
						throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
					}
					this.metadataCacheSettings = value;
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004074 File Offset: 0x00002274
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000040B8 File Offset: 0x000022B8
		public bool UseConnectionStringAsSession
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.useConnectionStringAsSession;
				}
				return flag2;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.useConnectionStringAsSession != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.useConnectionStringAsSession = value;
					}
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004118 File Offset: 0x00002318
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000415C File Offset: 0x0000235C
		public TimeSpan SoftCancellationTimeout
		{
			get
			{
				object obj = this.syncRoot;
				TimeSpan timeSpan;
				lock (obj)
				{
					timeSpan = this.softCancellationTimeout;
				}
				return timeSpan;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (!(this.softCancellationTimeout == value))
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.softCancellationTimeout = value;
					}
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000041C0 File Offset: 0x000023C0
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004204 File Offset: 0x00002404
		public bool GCBetweenEvaluations
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.gcBetweenEvaluations;
				}
				return flag2;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.gcBetweenEvaluations != value)
					{
						if (!this.cleanedUp)
						{
							throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
						}
						this.gcBetweenEvaluations = value;
					}
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004264 File Offset: 0x00002464
		internal bool CleanedUp
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.cleanedUp;
				}
				return flag2;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000042A8 File Offset: 0x000024A8
		internal EvaluatorContext CreateEvaluatorContext()
		{
			object obj = this.syncRoot;
			EvaluatorContext evaluatorContext;
			lock (obj)
			{
				if (this.evaluatorContext == null)
				{
					EvaluatorConfiguration evaluatorConfiguration = new EvaluatorConfiguration
					{
						Identity = this.identity,
						IsRemote = !this.inProcess,
						ContainerExe = Path.Combine(ProviderContext.ContainerExecutableFolderPath, MashupConnectionPool2.GetContainerFileName()),
						ContainerCount = this.maxContainerCount,
						ContainerMinCount = this.minContainerCount,
						SharedMaxCommitInMB = this.sharedMaxCommitInMB,
						ContainerMaxCommitInMB = this.containerMaxCommitInMB,
						ContainerTimeToLive = this.containerTimeToLive,
						SharedMaxWorkingSetInMB = this.sharedMaxWorkingSetInMB,
						ContainerMaxWorkingSetInMB = this.containerMaxWorkingSetInMB,
						ContainerProcessorAffinity = this.containerProcessorAffinity,
						SessionTimeToLive = this.sessionTimeToLive,
						SoftCancelTimeout = this.softCancellationTimeout,
						TargetFrameworkName = ((MashupConnectionPool2.useNativeContainerLoader && MashupConnectionPool2.useHostFrameworkVersion) ? MashupConnectionPool2.targetFrameworkName : null)
					};
					this.evaluatorContext = new EvaluatorContext(evaluatorConfiguration);
					this.cleanedUp = false;
				}
				evaluatorContext = this.evaluatorContext;
			}
			return evaluatorContext;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000043D4 File Offset: 0x000025D4
		internal void OnConnectionOpened()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.openConnections++;
				this.cleanedUp = false;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004424 File Offset: 0x00002624
		internal void OnConnectionClosed()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.openConnections--;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000446C File Offset: 0x0000266C
		public bool TryCleanup(TimeSpan timeout)
		{
			object obj = this.syncRoot;
			bool flag = false;
			bool flag2;
			try
			{
				Monitor.Enter(obj, ref flag);
				if (this.openConnections != 0)
				{
					throw new InvalidOperationException(ProviderErrorStrings.ConnectionCleanOpen);
				}
				if (this.evaluatorContext == null)
				{
					flag2 = true;
				}
				else
				{
					EvaluatorContext oldContext = this.evaluatorContext;
					Thread thread = new Thread(SafeThread2.CreateThreadStart(delegate
					{
						oldContext.Dispose();
					}));
					thread.Start();
					this.cleanedUp = thread.Join(timeout);
					this.evaluatorContext = null;
					flag2 = this.cleanedUp;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(obj);
				}
			}
			return flag2;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004514 File Offset: 0x00002714
		public static bool VerifyCleanedUp()
		{
			object obj = MashupConnectionPool2.staticSyncRoot;
			bool flag2;
			lock (obj)
			{
				flag2 = !MashupConnectionPool2.namedPools.Values.Any((MashupConnectionPool2 c) => !c.CleanedUp);
			}
			return flag2;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004580 File Offset: 0x00002780
		public static MashupConnectionPool2 GetPool(string identity)
		{
			object obj = MashupConnectionPool2.staticSyncRoot;
			MashupConnectionPool2 mashupConnectionPool;
			lock (obj)
			{
				if (!MashupConnectionPool2.namedPools.TryGetValue(identity, out mashupConnectionPool))
				{
					throw new ArgumentException(ProviderErrorStrings.UnknownPool(identity), "Pool");
				}
			}
			return mashupConnectionPool;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000045DC File Offset: 0x000027DC
		public static void RegisterPool(string identity, MashupConnectionPool2 pool)
		{
			object obj = MashupConnectionPool2.staticSyncRoot;
			lock (obj)
			{
				MashupConnectionPool2.namedPools.Add(identity, pool);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004624 File Offset: 0x00002824
		public static void UnregisterPool(string identity)
		{
			object obj = MashupConnectionPool2.staticSyncRoot;
			lock (obj)
			{
				MashupConnectionPool2.namedPools.Remove(identity);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000466C File Offset: 0x0000286C
		internal static string GetContainerFileName()
		{
			if (MashupConnectionPool2.useNativeContainerLoader)
			{
				return "Microsoft.Mashup.Container.Loader.exe";
			}
			ClrVersion clrVersion = FxVersionDetector.FxVersion;
			if (clrVersion == ClrVersion.Net45 && !MashupConnectionPool2.AllowNet45Containers)
			{
				clrVersion = ClrVersion.Net40;
			}
			return EvaluatorConfiguration.GetContainerFileName(clrVersion);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000046A0 File Offset: 0x000028A0
		private static string GetTargetFrameworkName()
		{
			PropertyInfo property = typeof(object).Assembly.GetType("System.AppDomainSetup").GetProperty("TargetFrameworkName");
			return ((property != null) ? property.GetValue(AppDomain.CurrentDomain.SetupInformation, null) : null) as string;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000046EC File Offset: 0x000028EC
		private void ValidateJobOptions(int sharedMaxCommitInMB, int containerMaxCommitInMB, bool[] containerProcessorAffinity, bool containerInheritsHostJob)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (containerInheritsHostJob && (sharedMaxCommitInMB != -1 || containerMaxCommitInMB != -1 || containerProcessorAffinity != null))
				{
					throw new InvalidOperationException(ProviderErrorStrings.JobOptionsForbidden);
				}
			}
		}

		// Token: 0x04000025 RID: 37
		private const int MBInBytes = 1048576;

		// Token: 0x04000026 RID: 38
		private static readonly object staticSyncRoot = new object();

		// Token: 0x04000027 RID: 39
		private static readonly Dictionary<string, MashupConnectionPool2> namedPools = new Dictionary<string, MashupConnectionPool2>();

		// Token: 0x04000028 RID: 40
		private static readonly MashupConnectionPool2 defaultPool = new MashupConnectionPool2(null);

		// Token: 0x04000029 RID: 41
		private static readonly string targetFrameworkName = MashupConnectionPool2.GetTargetFrameworkName();

		// Token: 0x0400002A RID: 42
		private static bool allowNet45Containers = true;

		// Token: 0x0400002B RID: 43
		private static bool useNativeContainerLoader = false;

		// Token: 0x0400002C RID: 44
		private static bool useHostFrameworkVersion = false;

		// Token: 0x0400002D RID: 45
		private readonly object syncRoot;

		// Token: 0x0400002E RID: 46
		private readonly string identity;

		// Token: 0x0400002F RID: 47
		private bool cleanedUp = true;

		// Token: 0x04000030 RID: 48
		private int openConnections;

		// Token: 0x04000031 RID: 49
		private bool inProcess;

		// Token: 0x04000032 RID: 50
		private bool useConnectionStringAsSession;

		// Token: 0x04000033 RID: 51
		private int maxContainerCount = 6;

		// Token: 0x04000034 RID: 52
		private int minContainerCount;

		// Token: 0x04000035 RID: 53
		private int sharedMaxWorkingSetInMB = -1;

		// Token: 0x04000036 RID: 54
		private int sharedMaxCommitInMB = -1;

		// Token: 0x04000037 RID: 55
		private int containerMaxWorkingSetInMB = 256;

		// Token: 0x04000038 RID: 56
		private int containerMaxCommitInMB = -1;

		// Token: 0x04000039 RID: 57
		private bool containerInheritsHostJob;

		// Token: 0x0400003A RID: 58
		private TimeSpan? containerTimeToLive;

		// Token: 0x0400003B RID: 59
		private TimeSpan sessionTimeToLive;

		// Token: 0x0400003C RID: 60
		private bool[] containerProcessorAffinity;

		// Token: 0x0400003D RID: 61
		private EvaluatorContext evaluatorContext;

		// Token: 0x0400003E RID: 62
		private MashupCacheSettings2 dataCacheSettings;

		// Token: 0x0400003F RID: 63
		private MashupCacheSettings2 metadataCacheSettings;

		// Token: 0x04000040 RID: 64
		private TimeSpan softCancellationTimeout;

		// Token: 0x04000041 RID: 65
		private bool gcBetweenEvaluations;
	}
}
