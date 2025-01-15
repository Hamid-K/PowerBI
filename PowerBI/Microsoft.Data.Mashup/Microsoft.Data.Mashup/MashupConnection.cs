using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Security;
using Microsoft.Mashup.Storage;
using Microsoft.Mashup.Storage.Memory;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000025 RID: 37
	public sealed class MashupConnection : DbConnection
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000072B0 File Offset: 0x000054B0
		static MashupConnection()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(DbMetaDataColumnNames.CollectionName, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.NumberOfRestrictions, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.NumberOfIdentifierParts, typeof(int));
			dataTable.Rows.Add(new object[]
			{
				DbMetaDataCollectionNames.MetaDataCollections,
				0,
				0
			});
			dataTable.Rows.Add(new object[]
			{
				DbMetaDataCollectionNames.DataSourceInformation,
				0,
				0
			});
			MashupConnection.schemaTables.Add(DbMetaDataCollectionNames.MetaDataCollections, dataTable);
			DataTable dataTable2 = new DataTable();
			dataTable2.Locale = CultureInfo.InvariantCulture;
			dataTable2.Columns.Add(DbMetaDataColumnNames.DataSourceProductName, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersion, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersionNormalized, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.GroupByBehavior, typeof(GroupByBehavior));
			dataTable2.Columns.Add(DbMetaDataColumnNames.IdentifierPattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.IdentifierCase, typeof(IdentifierCase));
			dataTable2.Columns.Add(DbMetaDataColumnNames.OrderByColumnsInSelect, typeof(bool));
			dataTable2.Columns.Add(DbMetaDataColumnNames.ParameterMarkerFormat, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.ParameterMarkerPattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.ParameterNameMaxLength, typeof(int));
			dataTable2.Columns.Add(DbMetaDataColumnNames.ParameterNamePattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierPattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierCase, typeof(IdentifierCase));
			dataTable2.Columns.Add(DbMetaDataColumnNames.StatementSeparatorPattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.StringLiteralPattern, typeof(string));
			dataTable2.Columns.Add(DbMetaDataColumnNames.SupportedJoinOperators, typeof(SupportedJoinOperators));
			Version version = new Version(MashupEngines.Version1Version);
			string text = string.Format(CultureInfo.InvariantCulture, "{0:D2}.{1:D3}.{2:D4}", version.Major, version.Minor, version.Build);
			dataTable2.Rows.Add(new object[]
			{
				MashupEngines.Name,
				MashupEngines.Version1Version,
				text
			});
			MashupConnection.schemaTables.Add(DbMetaDataCollectionNames.DataSourceInformation, dataTable2);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007778 File Offset: 0x00005978
		public MashupConnection()
		{
			XmlSerializationAssembly.EnsureLoadRelative();
			this.optionalModules = new List<string>();
			this.progress = new Dictionary<Pair<string, string>, DataSourceProgress>();
			this.progressResults = EmptyArray<DataSourceProgress>.Instance;
			this.dataSourceSettingNeeded = new ContextAwareEvent<DataSourceSettingNeededEventArgs>();
			this.diagnosticEvents = new DiagnosticEvents();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000077C7 File Offset: 0x000059C7
		public MashupConnection(string connectionString)
			: this()
		{
			if (!string.IsNullOrEmpty(connectionString))
			{
				this.SetConnectionString(connectionString, "connectionString");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000077E3 File Offset: 0x000059E3
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000077EF File Offset: 0x000059EF
		public static int ContainerMaxCount
		{
			get
			{
				return MashupConnection.defaultPool.ContainerMaxCount;
			}
			set
			{
				MashupConnection.defaultPool.ContainerMaxCount = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000077FC File Offset: 0x000059FC
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00007808 File Offset: 0x00005A08
		internal static int ContainerMinCount
		{
			get
			{
				return MashupConnection.defaultPool.ContainerMinCount;
			}
			set
			{
				MashupConnection.defaultPool.ContainerMinCount = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007815 File Offset: 0x00005A15
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007821 File Offset: 0x00005A21
		public static int SharedMaxWorkingSetInMB
		{
			get
			{
				return MashupConnection.defaultPool.SharedMaxWorkingSetInMB;
			}
			set
			{
				MashupConnection.defaultPool.SharedMaxWorkingSetInMB = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000782E File Offset: 0x00005A2E
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000783A File Offset: 0x00005A3A
		public static int SharedMaxCommitInMB
		{
			get
			{
				return MashupConnection.defaultPool.SharedMaxCommitInMB;
			}
			set
			{
				MashupConnection.defaultPool.SharedMaxCommitInMB = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007847 File Offset: 0x00005A47
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00007853 File Offset: 0x00005A53
		public static int ContainerMaxWorkingSetInMB
		{
			get
			{
				return MashupConnection.defaultPool.ContainerMaxWorkingSetInMB;
			}
			set
			{
				MashupConnection.defaultPool.ContainerMaxWorkingSetInMB = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007860 File Offset: 0x00005A60
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000786C File Offset: 0x00005A6C
		public static int ContainerMaxCommitInMB
		{
			get
			{
				return MashupConnection.defaultPool.ContainerMaxCommitInMB;
			}
			set
			{
				MashupConnection.defaultPool.ContainerMaxCommitInMB = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007879 File Offset: 0x00005A79
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00007885 File Offset: 0x00005A85
		public static TimeSpan? ContainerTimeToLive
		{
			get
			{
				return MashupConnection.defaultPool.ContainerTimeToLive;
			}
			set
			{
				MashupConnection.defaultPool.ContainerTimeToLive = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007892 File Offset: 0x00005A92
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000789E File Offset: 0x00005A9E
		public static bool[] ContainerProcessorAffinity
		{
			get
			{
				return MashupConnection.defaultPool.ContainerProcessorAffinity;
			}
			set
			{
				MashupConnection.defaultPool.ContainerProcessorAffinity = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000078AB File Offset: 0x00005AAB
		public MashupEvaluationCounters EvaluationCounters
		{
			get
			{
				return this.evaluationCounters;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000078B3 File Offset: 0x00005AB3
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000078BF File Offset: 0x00005ABF
		internal static bool ContainerInheritsHostJob
		{
			get
			{
				return MashupConnection.defaultPool.ContainerInheritsHostJob;
			}
			set
			{
				MashupConnection.defaultPool.ContainerInheritsHostJob = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000078CC File Offset: 0x00005ACC
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000790C File Offset: 0x00005B0C
		internal static bool EnablePreviewConnectionStringProperties
		{
			get
			{
				object obj = MashupConnection.staticSyncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnection.enablePreviewConnectionStringProperties;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnection.staticSyncRoot;
				lock (obj)
				{
					if (MashupConnection.enablePreviewConnectionStringProperties != value)
					{
						MashupConnection.VerifyCleanedUp();
						MashupConnection.enablePreviewConnectionStringProperties = value;
					}
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007958 File Offset: 0x00005B58
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00007998 File Offset: 0x00005B98
		internal static bool AllowBrowserAccess
		{
			get
			{
				object obj = MashupConnection.staticSyncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnection.allowBrowserAccess;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnection.staticSyncRoot;
				lock (obj)
				{
					if (value != MashupConnection.allowBrowserAccess)
					{
						MashupConnection.VerifyCleanedUp();
						if (value)
						{
							MashupConnection.EnableModule("Html");
							MashupConnection.EnableModule("WebBrowserContents");
						}
						else
						{
							MashupConnection.DisableModule("Html");
							MashupConnection.DisableModule("WebBrowserContents");
						}
						MashupConnection.allowBrowserAccess = value;
					}
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007A14 File Offset: 0x00005C14
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00007A54 File Offset: 0x00005C54
		internal static bool AllowFileAccess
		{
			get
			{
				object obj = MashupConnection.staticSyncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnection.allowFileAccess;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnection.staticSyncRoot;
				lock (obj)
				{
					if (value != MashupConnection.allowFileAccess)
					{
						MashupConnection.VerifyCleanedUp();
						if (value)
						{
							MashupConnection.EnableModule("File");
						}
						else
						{
							MashupConnection.DisableModule("File");
						}
						MashupConnection.allowFileAccess = value;
					}
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00007ABC File Offset: 0x00005CBC
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00007AFC File Offset: 0x00005CFC
		internal static bool AllowWindowsCredentials
		{
			get
			{
				object obj = MashupConnection.staticSyncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnection.allowWindowsCredentials;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnection.staticSyncRoot;
				lock (obj)
				{
					if (MashupConnection.AllowWindowsCredentials != value)
					{
						MashupConnection.VerifyCleanedUp();
						MashupConnection.allowWindowsCredentials = value;
					}
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00007B48 File Offset: 0x00005D48
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00007B88 File Offset: 0x00005D88
		internal static bool AllowCurrentWorkbook
		{
			get
			{
				object obj = MashupConnection.staticSyncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnection.allowCurrentWorkbook;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnection.staticSyncRoot;
				lock (obj)
				{
					if (value != MashupConnection.allowCurrentWorkbook)
					{
						MashupConnection.VerifyCleanedUp();
						if (value)
						{
							if (MashupConnection.currentWorkbookResource == null)
							{
								throw new InvalidOperationException("Internal error: resource is unexpectedly null");
							}
							Exception ex;
							if (!MashupEngines.Version1.TryRegisterResourceKind(MashupConnection.currentWorkbookResource, "ExcelInterop", out ex))
							{
								throw ex;
							}
							MashupConnection.RestoreModule("ExcelInterop");
						}
						else
						{
							if (!MashupEngines.Version1.TryLookupResourceKind("CurrentWorkbook", out MashupConnection.currentWorkbookResource))
							{
								throw new InvalidOperationException("Internal error: resource is unexpectedly missing");
							}
							MashupEngines.Version1.UnregisterResourceKind("CurrentWorkbook");
							MashupConnection.RemoveModule("ExcelInterop");
						}
						MashupConnection.allowCurrentWorkbook = value;
					}
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007C50 File Offset: 0x00005E50
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00007C5C File Offset: 0x00005E5C
		internal static bool InProcess
		{
			get
			{
				return MashupConnection.defaultPool.InProcess;
			}
			set
			{
				MashupConnection.defaultPool.InProcess = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00007C69 File Offset: 0x00005E69
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00007C75 File Offset: 0x00005E75
		internal static TimeSpan SessionTimeToLive
		{
			get
			{
				return MashupConnection.defaultPool.SessionTimeToLive;
			}
			set
			{
				MashupConnection.defaultPool.SessionTimeToLive = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007C82 File Offset: 0x00005E82
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007C8E File Offset: 0x00005E8E
		internal static TimeSpan CacheTimeToLive
		{
			get
			{
				return MashupConnection.defaultPool.CacheTimeToLive;
			}
			set
			{
				MashupConnection.defaultPool.CacheTimeToLive = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007C9B File Offset: 0x00005E9B
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00007CA7 File Offset: 0x00005EA7
		internal static MashupCacheSettings2 DataCacheSettings
		{
			get
			{
				return MashupConnection.defaultPool.DataCacheSettings;
			}
			set
			{
				MashupConnection.defaultPool.DataCacheSettings = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007CB4 File Offset: 0x00005EB4
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00007CC0 File Offset: 0x00005EC0
		internal static MashupCacheSettings2 MetadataCacheSettings
		{
			get
			{
				return MashupConnection.defaultPool.MetadataCacheSettings;
			}
			set
			{
				MashupConnection.defaultPool.MetadataCacheSettings = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007CCD File Offset: 0x00005ECD
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00007CD9 File Offset: 0x00005ED9
		internal static bool UseConnectionStringAsSession
		{
			get
			{
				return MashupConnection.defaultPool.UseConnectionStringAsSession;
			}
			set
			{
				MashupConnection.defaultPool.UseConnectionStringAsSession = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007CE6 File Offset: 0x00005EE6
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00007CF2 File Offset: 0x00005EF2
		internal static TimeSpan SoftCancellationTimeout
		{
			get
			{
				return MashupConnection.defaultPool.SoftCancellationTimeout;
			}
			set
			{
				MashupConnection.defaultPool.SoftCancellationTimeout = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007CFF File Offset: 0x00005EFF
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00007D0B File Offset: 0x00005F0B
		internal static bool GCBetweenEvaluations
		{
			get
			{
				return MashupConnection.defaultPool.GCBetweenEvaluations;
			}
			set
			{
				MashupConnection.defaultPool.GCBetweenEvaluations = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007D18 File Offset: 0x00005F18
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00007D1F File Offset: 0x00005F1F
		internal static bool ReportStagingProgress
		{
			get
			{
				return MashupConnection.reportStagingProgress;
			}
			set
			{
				MashupConnection.reportStagingProgress = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007D28 File Offset: 0x00005F28
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00007D68 File Offset: 0x00005F68
		internal static bool EnableInformationProtection
		{
			get
			{
				object obj = MashupConnection.staticSyncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = MashupConnection.enableInformationProtection;
				}
				return flag2;
			}
			set
			{
				object obj = MashupConnection.staticSyncRoot;
				lock (obj)
				{
					MashupConnection.enableInformationProtection = value;
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007DA8 File Offset: 0x00005FA8
		internal ISectionDocument[] Documents
		{
			get
			{
				return this.documents;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007DB0 File Offset: 0x00005FB0
		internal MashupCommandBehavior MashupCommandBehavior
		{
			get
			{
				return this.mashupCommandBehavior;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007DB8 File Offset: 0x00005FB8
		private static void VerifyCleanedUp()
		{
			if (!MashupConnection.defaultPool.CleanedUp || !MashupConnectionPool2.VerifyCleanedUp())
			{
				throw new InvalidOperationException(ProviderErrorStrings.ConnectionNotCleaned);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public static void SetProcessContainerDirectory(string directory, bool copyFiles)
		{
			if (string.IsNullOrEmpty(directory))
			{
				throw new ArgumentNullException("directory");
			}
			if (!Path.IsPathRooted(directory))
			{
				throw new ArgumentException(ProviderErrorStrings.PathNotValid(directory), "directory");
			}
			string localPath = new Uri(typeof(MashupConnection).Assembly.GetLocation()).LocalPath;
			if (copyFiles)
			{
				if (File.Exists(directory) || (Directory.Exists(directory) && Directory.GetFiles(directory).Length != 0))
				{
					throw new ArgumentException(ProviderErrorStrings.DirectoryMustBeEmpty(directory), "directory");
				}
				MashupConnection.RecursiveCopy(new DirectoryInfo(Path.GetDirectoryName(localPath)), directory, new HashSet<string>(StringComparer.OrdinalIgnoreCase));
			}
			string text = Path.Combine(directory, Path.GetFileName(localPath));
			if (!Directory.Exists(directory) || !File.Exists(text))
			{
				throw new ArgumentException(ProviderErrorStrings.ExpectedFileNotFound(text), "directory");
			}
			object obj = MashupConnection.staticSyncRoot;
			lock (obj)
			{
				MashupConnection.VerifyCleanedUp();
			}
			ProviderContext.BinariesFolderPath = directory;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007EE0 File Offset: 0x000060E0
		private static void RecursiveCopy(DirectoryInfo sourceDirectoryInfo, string destinationDirectory, HashSet<string> written)
		{
			written.Add(destinationDirectory);
			if (written.Contains(sourceDirectoryInfo.FullName))
			{
				return;
			}
			bool flag = Directory.Exists(destinationDirectory);
			foreach (FileInfo fileInfo in sourceDirectoryInfo.GetFiles())
			{
				string name = fileInfo.Name;
				if (MashupConnection.containerFileList.Contains(name) || (name.IndexOf("mashup", StringComparison.OrdinalIgnoreCase) >= 0 && MashupConnection.extensionList.Contains(fileInfo.Extension)) || name.EndsWith(".resources.dll", StringComparison.OrdinalIgnoreCase))
				{
					if (!flag)
					{
						Directory.CreateDirectory(destinationDirectory);
						flag = true;
					}
					fileInfo.CopyTo(Path.Combine(destinationDirectory, name), false);
				}
			}
			foreach (DirectoryInfo directoryInfo in sourceDirectoryInfo.GetDirectories())
			{
				MashupConnection.RecursiveCopy(directoryInfo, Path.Combine(destinationDirectory, directoryInfo.Name), written);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007FB9 File Offset: 0x000061B9
		internal static void StartContainerPool()
		{
			Impersonation.RunAsProcessUser<EvaluatorContext>(() => MashupConnection.defaultPool.CreateEvaluatorContext());
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007FE0 File Offset: 0x000061E0
		public static bool TryCleanup()
		{
			return MashupConnection.defaultPool.TryCleanup(MashupConnection.defaultCleanupTimeout);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007FF1 File Offset: 0x000061F1
		public static bool TryCleanup(TimeSpan timeout)
		{
			return MashupConnection.defaultPool.TryCleanup(timeout);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007FFE File Offset: 0x000061FE
		public static void SetConfigurationProperty(string propertyName, object propertyValue)
		{
			ConfigurationProperty.Validate(propertyName, propertyValue);
			ConfigurationPropertyService.SetDefaultValue(propertyName, propertyValue);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000800E File Offset: 0x0000620E
		public static void RemoveConfigurationProperty(string propertyName)
		{
			ConfigurationPropertyService.RemoveDefaultValue(propertyName);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008016 File Offset: 0x00006216
		public override DataTable GetSchema()
		{
			return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008023 File Offset: 0x00006223
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008030 File Offset: 0x00006230
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (collectionName == null)
			{
				throw new ArgumentNullException("collectionName");
			}
			DataTable dataTable;
			if (!MashupConnection.schemaTables.TryGetValue(collectionName, out dataTable))
			{
				throw new ArgumentException(ProviderErrorStrings.GetSchemaCollectionNotSupported(collectionName), "collectionName");
			}
			if (restrictionValues != null && restrictionValues.Length != 0)
			{
				throw new ArgumentException(ProviderErrorStrings.GetSchemaTooManyRestrictions(collectionName), "restrictionValues");
			}
			return dataTable;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008084 File Offset: 0x00006284
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			throw new NotSupportedException(ProviderErrorStrings.TransactionsNotSupported);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008090 File Offset: 0x00006290
		public override void Close()
		{
			if (this.State != ConnectionState.Closed)
			{
				this.package = null;
				this.documents = null;
				this.engineFirewallRules = null;
				this.engineCredentials = null;
				this.engineQueryPermissions = null;
				this.firewallPlan = null;
				this.pool.OnConnectionClosed();
				if (this.sessionLease != null)
				{
					this.sessionLease.Dispose();
					this.sessionLease = null;
				}
				if (this.threadIdentity != null)
				{
					this.threadIdentity.Close();
					this.threadIdentity = null;
				}
				if (this.evaluationCounters != null)
				{
					this.evaluationCounters.Close();
					this.evaluationCounters = null;
				}
				this.connectionState = ConnectionState.Closed;
				this.OnStateChange(MashupConnection.StateChangeClosed);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000813D File Offset: 0x0000633D
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotSupportedException(ProviderErrorStrings.ChangeDatabaseNotSupported);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000814C File Offset: 0x0000634C
		public override void Open()
		{
			if (string.IsNullOrEmpty(this.connectionString))
			{
				throw new InvalidOperationException(ProviderErrorStrings.ConnectionStringNotSet);
			}
			if (this.State != ConnectionState.Closed)
			{
				throw new InvalidOperationException(ProviderErrorStrings.CannotReopenConnection);
			}
			this.SetPackage();
			MashupConnection.SetDataSourceSettings(this.dataSourceSettings, out this.engineCredentials, out this.engineFirewallRules, out this.engineQueryPermissions);
			if (this.Session != null)
			{
				this.sessionLease = Impersonation.RunAsProcessUser<IDisposable>(() => this.pool.CreateEvaluatorContext().BeginSession(this.Session));
			}
			if (!this.persistSecurityInfo)
			{
				this.connectionString = new MashupConnectionStringBuilder(this.connectionString)
				{
					DataSourceSettings = null
				}.ConnectionString;
			}
			this.SetPrivacyPartitionDataSources();
			this.pool.OnConnectionOpened();
			this.threadIdentity = ProcessHelpers.GetThreadToken();
			this.evaluationCounters = new MashupEvaluationCounters();
			this.connectionState = ConnectionState.Open;
			this.OnStateChange(MashupConnection.StateChangeOpen);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008228 File Offset: 0x00006428
		private void SetPackage()
		{
			if (this.packageString != null)
			{
				this.package = MashupPackage.CreatePackage(MashupPackage.FromBase64String(this.packageString));
				this.documents = MashupPackage.GetSectionDocuments(this.package);
				return;
			}
			this.package = MashupPackage.CreatePackage(this.mashup, out this.documents);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000827C File Offset: 0x0000647C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00008284 File Offset: 0x00006484
		public override string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.ThrowOnNotClosedConnection();
				if (!string.IsNullOrEmpty(value))
				{
					this.SetConnectionString(value, "value");
					return;
				}
				this.connectionString = value;
				this.packageString = null;
				this.mashup = null;
				this.location = null;
				this.connectionTimeout = 0;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000082C4 File Offset: 0x000064C4
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000082CC File Offset: 0x000064CC
		public DataTable FilterDataTable
		{
			get
			{
				return this.filterDataTable;
			}
			set
			{
				this.filterDataTable = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000082D8 File Offset: 0x000064D8
		public IEnumerable<DataSourceProgress> Progress
		{
			get
			{
				if (Interlocked.CompareExchange(ref this.inUpdatingProgress, 1, 0) == 0)
				{
					try
					{
						Func<IEnumerable<PartitionProgress>> func = this.currentPartitionProgress;
						if (func != null)
						{
							Dictionary<Pair<string, string>, DataSourceProgress> dictionary = this.progress;
							lock (dictionary)
							{
								bool flag2 = this.deleteProgressUpdaterAfterRun;
								IEnumerable<PartitionProgress> enumerable = func();
								if (flag2)
								{
									this.currentPartitionProgress = null;
									this.deleteProgressUpdaterAfterRun = false;
								}
								this.UpdateProgress(enumerable);
							}
						}
					}
					finally
					{
						Interlocked.Exchange(ref this.inUpdatingProgress, 0);
					}
				}
				return Interlocked.CompareExchange<IEnumerable<DataSourceProgress>>(ref this.progressResults, null, null);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001AB RID: 427 RVA: 0x00008378 File Offset: 0x00006578
		// (remove) Token: 0x060001AC RID: 428 RVA: 0x00008386 File Offset: 0x00006586
		public event EventHandler<DataSourceSettingNeededEventArgs> DataSourceSettingNeeded
		{
			add
			{
				this.dataSourceSettingNeeded.AddHandler(value);
			}
			remove
			{
				this.dataSourceSettingNeeded.RemoveHandler(value);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008394 File Offset: 0x00006594
		public void SubscribeDiagnostics(string[] channelNames, Action<DiagnosticEventArgs> handler)
		{
			foreach (string text in channelNames)
			{
				this.diagnosticEvents.AddHandler(text, handler);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000083C4 File Offset: 0x000065C4
		internal IDictionary<DataSource, DataSourceSetting> Credentials
		{
			get
			{
				if (this.State != ConnectionState.Open)
				{
					return null;
				}
				Dictionary<DataSource, DataSourceSetting> dictionary = new Dictionary<DataSource, DataSourceSetting>();
				Credential[] credentials = this.engineCredentials.GetCredentials();
				for (int i = 0; i < credentials.Length; i++)
				{
					DataSource dataSource;
					DataSourceSetting dataSourceSetting = DataSourceSetting.FromCredential(credentials[i], out dataSource);
					dictionary.Add(dataSource, dataSourceSetting);
				}
				foreach (FirewallRule firewallRule in this.engineFirewallRules.GetFirewallRules())
				{
					DataSource dataSource2 = new DataSource(firewallRule.Resource.Kind, firewallRule.Resource.Path);
					DataSourceSetting dataSourceSetting2;
					if (!dictionary.TryGetValue(dataSource2, out dataSourceSetting2))
					{
						dataSourceSetting2 = new DataSourceSetting();
						dictionary.Add(dataSource2, dataSourceSetting2);
					}
					dataSourceSetting2.PrivacySetting = firewallRule.GroupType.ToPrivacyGroup();
					dataSourceSetting2.PrivateGroupName = firewallRule.GroupName;
					dataSourceSetting2.IsTrusted = firewallRule.IsTrusted;
				}
				return dictionary;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000849F File Offset: 0x0000669F
		public override int ConnectionTimeout
		{
			get
			{
				return this.connectionTimeout;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000084A7 File Offset: 0x000066A7
		public override string Database
		{
			get
			{
				return this.Location ?? string.Empty;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000084B8 File Offset: 0x000066B8
		public override ConnectionState State
		{
			get
			{
				return this.connectionState;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000084C0 File Offset: 0x000066C0
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return MashupProviderFactory.Instance;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000084C7 File Offset: 0x000066C7
		public override string DataSource
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000084CE File Offset: 0x000066CE
		public override string ServerVersion
		{
			get
			{
				return MashupEngines.Version1Version;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000084D5 File Offset: 0x000066D5
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000084DD File Offset: 0x000066DD
		protected override void Dispose(bool disposing)
		{
			this.Close();
			base.Dispose(disposing);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000084EC File Offset: 0x000066EC
		public new MashupCommand CreateCommand()
		{
			return new MashupCommand(null, this);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000084F5 File Offset: 0x000066F5
		internal EventHandler<DataSourceSettingNeededEventArgs> DataSourceSettingNeededHandler
		{
			get
			{
				return this.dataSourceSettingNeeded.EventHandler;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008504 File Offset: 0x00006704
		internal void Refresh(DataSourceSettingNeededEventArgs eventArgs)
		{
			EventHandler<DataSourceSettingNeededEventArgs> eventHandler = this.dataSourceSettingNeeded.EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, eventArgs);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008528 File Offset: 0x00006728
		internal void EnableEnvironmentModule()
		{
			this.optionalModules.Add("Environment");
			this.dataSourceSettings[Microsoft.Data.Mashup.DataSource.DefaultForKind("Environment")] = DataSourceSetting.CreateAnonymousCredential();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008554 File Offset: 0x00006754
		private void SetPrivacyPartitionDataSources()
		{
			if (this.privacyPartitionDataSources != null)
			{
				this.firewallPlan = MashupConnection.GetFirewallPlan(this.package, this.privacyPartitionDataSources);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008578 File Offset: 0x00006778
		private void UpdateProgress(IEnumerable<PartitionProgress> progresses)
		{
			if (progresses != null)
			{
				DataSourceProgress dataSourceProgress = null;
				foreach (PartitionProgress partitionProgress in progresses)
				{
					DataSourceProgress mostRecentProgress = partitionProgress.GetMostRecentProgress();
					if (((mostRecentProgress != null && MashupConnection.ReportStagingProgress) || !string.Equals(mostRecentProgress.DataSourceType, "Staging", StringComparison.Ordinal)) && (dataSourceProgress == null || dataSourceProgress.LastProgressAt < mostRecentProgress.LastProgressAt))
					{
						dataSourceProgress = mostRecentProgress;
					}
				}
				if (dataSourceProgress != null)
				{
					Pair<string, string> pair = Pair.New<string, string>(dataSourceProgress.DataSourceType, dataSourceProgress.DataSource);
					DataSourceProgress dataSourceProgress2;
					if (!this.progress.TryGetValue(pair, out dataSourceProgress2))
					{
						dataSourceProgress2 = new DataSourceProgress(dataSourceProgress.DataSourceType, dataSourceProgress.DataSource);
						this.progress.Add(pair, dataSourceProgress2);
					}
					dataSourceProgress2.Bytes = new long?(dataSourceProgress.BytesRead);
					dataSourceProgress2.Rows = new int?((int)dataSourceProgress.RowsRead);
					Interlocked.Exchange<IEnumerable<DataSourceProgress>>(ref this.progressResults, this.progress.Values.ToArray<DataSourceProgress>());
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008680 File Offset: 0x00006880
		internal static void DisableModule(string moduleName)
		{
			MashupEngines.Version1.DisabledModules.Add(moduleName);
			LibraryService.LegacyLibrary.Reset();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000869C File Offset: 0x0000689C
		internal static void EnableModule(string moduleName)
		{
			if (MashupEngines.Version1.DisabledModules.Remove(moduleName))
			{
				LibraryService.LegacyLibrary.Reset();
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000086BA File Offset: 0x000068BA
		internal static void RemoveModule(string moduleName)
		{
			MashupEngines.Version1.RemovedModules.Add(moduleName);
			LibraryService.LegacyLibrary.Reset();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000086D6 File Offset: 0x000068D6
		internal static void RestoreModule(string moduleName)
		{
			if (MashupEngines.Version1.RemovedModules.Remove(moduleName))
			{
				LibraryService.LegacyLibrary.Reset();
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000086F4 File Offset: 0x000068F4
		internal static void ClearConfigurationProperties()
		{
			ConfigurationPropertyService.ClearDefaultValues();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000086FC File Offset: 0x000068FC
		internal static IFirewallPlan GetFirewallPlan(IPackage package, IDictionary<string, DataSource[]> privacyPartitionDataSourcesDictionary)
		{
			IPartitionedDocument partitionedDocument = package.PartitionedDocument(PartitioningScheme.MemberLet1, MashupEngines.Version1);
			IFirewallPlan firewallPlan = new FirewallPlanCreator().CreatePlanForPartitions(partitionedDocument, partitionedDocument.PartitionKeys);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				string text = firewallPartitionPlan.PartitionKey.ToSerializedString();
				DataSource[] array;
				if (privacyPartitionDataSourcesDictionary.TryGetValue(text, out array))
				{
					firewallPartitionPlan.AddResources(array.Select((DataSource ds) => ds.NormalizedResource));
				}
			}
			return firewallPlan;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000087A8 File Offset: 0x000069A8
		internal static void SetDataSourceSettings(IDictionary<DataSource, DataSourceSetting> dataSourceSettings, out CredentialsStorage credentialsStorage, out FirewallStorage firewallStorage, out QueryPermissionsStorage queryPermissionsStorage)
		{
			List<Credential> list = new List<Credential>(dataSourceSettings.Count);
			List<FirewallRule> list2 = new List<FirewallRule>(dataSourceSettings.Count);
			List<QueryPermission> list3 = new List<QueryPermission>();
			foreach (KeyValuePair<DataSource, DataSourceSetting> keyValuePair in dataSourceSettings)
			{
				DataSource key = keyValuePair.Key;
				DataSourceSetting value = keyValuePair.Value;
				Resource normalizedResource = key.NormalizedResource;
				if (value.AuthenticationKind != null)
				{
					MashupConnection.ValidateSetting(key, value);
					list.Add(value.MakeCredential(normalizedResource, key));
				}
				if (value.PrivacySetting != null)
				{
					list2.Add(value.ToFirewallRule(key));
				}
				else
				{
					if (value.PrivateGroupName != null)
					{
						throw new MashupPrivacyException(ProviderErrorStrings.InvalidPrivacyWithGroupName("Private"));
					}
					if (value.IsTrusted != null)
					{
						throw new MashupPrivacyException(ProviderErrorStrings.InvalidPrivacyWithIsTrusted);
					}
				}
				if (key.IsDefaultForKind && value.Permissions.Any<MashupPermission>())
				{
					throw new MashupException(ProviderErrorStrings.WildcardPathsWithPermissionsNotSupported);
				}
				foreach (MashupPermission mashupPermission in value.Permissions)
				{
					string kind = mashupPermission.Kind;
					if (!(kind == "NativeQuery"))
					{
						if (!(kind == "Redirect"))
						{
							throw new NotImplementedException(mashupPermission.Kind);
						}
						list3.Add(new QueryPermissionXml(normalizedResource, QueryPermissionChallengeType.EvaluateExchangeRedirectUnpermitted, mashupPermission.Value));
					}
					else
					{
						int num = 0;
						IEnumerable<string> enumerable = null;
						object obj;
						if (mashupPermission.Properties.TryGetValue(MashupPermission.Parameters, out obj))
						{
							if (obj is int)
							{
								num = (int)obj;
							}
							else if (obj is IList)
							{
								num = ((IList)obj).Count;
								enumerable = ((IList)obj).Cast<string>();
							}
						}
						list3.Add(new NativeQueryXml(normalizedResource, mashupPermission.Value, num, enumerable));
					}
				}
			}
			credentialsStorage = new MemoryCredentialsStorage(list);
			firewallStorage = new MemoryFirewallStorage(list2);
			queryPermissionsStorage = new MemoryQueryPermissionsStorage(list3);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000089EC File Offset: 0x00006BEC
		internal static void ValidateSetting(DataSource dataSource, DataSourceSetting dataSourceSetting)
		{
			DataSourceSetting.ValidateSupported(dataSource, dataSourceSetting);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000089F5 File Offset: 0x00006BF5
		internal IPackage Package
		{
			get
			{
				return this.package;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000089FD File Offset: 0x00006BFD
		internal string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00008A05 File Offset: 0x00006C05
		internal CredentialsStorage EngineCredentials
		{
			get
			{
				return this.engineCredentials;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008A0D File Offset: 0x00006C0D
		internal QueryPermissionsStorage EngineQueryPermissions
		{
			get
			{
				return this.engineQueryPermissions;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008A15 File Offset: 0x00006C15
		internal FirewallStorage EngineFirewallRules
		{
			get
			{
				return this.engineFirewallRules;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008A1D File Offset: 0x00006C1D
		internal IFirewallPlan FirewallPlan
		{
			get
			{
				return this.firewallPlan;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008A25 File Offset: 0x00006C25
		internal bool PartitionValues
		{
			get
			{
				return this.partitionValues;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008A2D File Offset: 0x00006C2D
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00008A35 File Offset: 0x00006C35
		internal bool AllowAutomaticCredentials { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008A3E File Offset: 0x00006C3E
		internal bool AllowWindowsAuthentication
		{
			get
			{
				return MashupConnection.allowWindowsCredentials;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008A45 File Offset: 0x00006C45
		internal bool MemoryCache
		{
			get
			{
				return this.memoryCache;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008A4D File Offset: 0x00006C4D
		internal string CachePath
		{
			get
			{
				return this.cachePath;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008A5E File Offset: 0x00006C5E
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00008A55 File Offset: 0x00006C55
		internal long MaxCacheSize
		{
			get
			{
				return this.maxCacheSize;
			}
			set
			{
				this.maxCacheSize = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008A66 File Offset: 0x00006C66
		internal string TempPath
		{
			get
			{
				return this.tempPath;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008A77 File Offset: 0x00006C77
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00008A6E File Offset: 0x00006C6E
		internal long MaxTempSize
		{
			get
			{
				return this.maxTempSize;
			}
			set
			{
				this.maxTempSize = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008A7F File Offset: 0x00006C7F
		internal string CacheEncryptionCertificateThumbprint
		{
			get
			{
				return this.cacheEncryptionCertificateThumbprint;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008A87 File Offset: 0x00006C87
		internal bool AllowNativeQueries
		{
			get
			{
				return this.allowNativeQueries;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008A8F File Offset: 0x00006C8F
		internal bool FastCombine
		{
			get
			{
				return this.fastCombine;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00008A97 File Offset: 0x00006C97
		internal Guid? ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008A9F File Offset: 0x00006C9F
		internal string CorrelationId
		{
			get
			{
				return this.correlationId;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008AA7 File Offset: 0x00006CA7
		internal bool LegacyRedirects
		{
			get
			{
				return this.legacyRedirects;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00008AAF File Offset: 0x00006CAF
		internal bool ReturnErrorValuesAsNull
		{
			get
			{
				return this.returnErrorValuesAsNull;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008AB7 File Offset: 0x00006CB7
		internal bool ThrowEnumerationErrors
		{
			get
			{
				return this.throwEnumerationErrors;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00008ABF File Offset: 0x00006CBF
		internal bool ThrowFoldingFailures
		{
			get
			{
				return this.throwFoldingFailures;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008AC7 File Offset: 0x00006CC7
		internal bool ThrowOnVolatileFunctions
		{
			get
			{
				return this.throwOnVolatileFunctions;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008ACF File Offset: 0x00006CCF
		internal bool IgnorePreviouslyCachedData
		{
			get
			{
				return this.ignorePreviouslyCachedData;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008AD7 File Offset: 0x00006CD7
		internal SafeHandle ThreadIdentity
		{
			get
			{
				return this.threadIdentity;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00008ADF File Offset: 0x00006CDF
		internal List<string> OptionalModules
		{
			get
			{
				return this.optionalModules;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008AE7 File Offset: 0x00006CE7
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00008AEF File Offset: 0x00006CEF
		internal string Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00008AF8 File Offset: 0x00006CF8
		internal string Pool
		{
			set
			{
				this.pool = ((value == null) ? MashupConnection.defaultPool : MashupConnectionPool2.GetPool(value));
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008B10 File Offset: 0x00006D10
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00008B18 File Offset: 0x00006D18
		internal string DataSourcePool
		{
			get
			{
				return this.dataSourcePool;
			}
			set
			{
				this.dataSourcePool = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008B21 File Offset: 0x00006D21
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00008B29 File Offset: 0x00006D29
		internal DateTimeOffset? Now
		{
			get
			{
				return this.now;
			}
			set
			{
				this.now = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008B32 File Offset: 0x00006D32
		internal IDictionary<string, object> ConfigurationProperties
		{
			get
			{
				return this.configurationProperties;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008B3A File Offset: 0x00006D3A
		internal string[] TracingOptions
		{
			get
			{
				if (!string.IsNullOrEmpty(this.tracingOptions))
				{
					return this.tracingOptions.Split(new char[] { ',' });
				}
				return null;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008B61 File Offset: 0x00006D61
		private void ThrowOnNotClosedConnection()
		{
			if (this.State != ConnectionState.Closed)
			{
				throw new InvalidOperationException(ProviderErrorStrings.OperationRequiresClosedConnection);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008B78 File Offset: 0x00006D78
		private void SetConnectionString(string value, string argumentName)
		{
			MashupConnectionStringBuilder mashupConnectionStringBuilder = new MashupConnectionStringBuilder(value);
			MashupConnection.SetOptionalProperties(mashupConnectionStringBuilder, mashupConnectionStringBuilder.Optional);
			IDictionary<string, object> dictionary = ConfigurationProperty.ToDictionary(mashupConnectionStringBuilder.FeatureSwitches);
			IDictionary<string, object> dictionary2 = ConfigurationProperty.ToDictionary(mashupConnectionStringBuilder.ConfigurationProperties);
			if (dictionary != null && dictionary2 != null)
			{
				throw new ArgumentException(ProviderErrorStrings.PropertiesAndSwitchesCannotBothBeNonEmpty, "FeatureSwitches");
			}
			this.configurationProperties = dictionary2 ?? dictionary;
			object obj = null;
			IDictionary<string, object> dictionary3 = this.configurationProperties;
			if (dictionary3 != null && dictionary3.TryGetValue("ConnectionString[Optional]", out obj))
			{
				this.configurationProperties.Remove("ConnectionString[Optional]");
				MashupConnection.SetOptionalProperties(mashupConnectionStringBuilder, obj as string);
			}
			if (string.IsNullOrEmpty(mashupConnectionStringBuilder.Package) && string.IsNullOrEmpty(mashupConnectionStringBuilder.Mashup))
			{
				throw new ArgumentException(ProviderErrorStrings.MashupAndPackageCannotBeBothNullOrEmpty, argumentName);
			}
			if (!string.IsNullOrEmpty(mashupConnectionStringBuilder.Package) && !string.IsNullOrEmpty(mashupConnectionStringBuilder.Mashup))
			{
				throw new ArgumentException(ProviderErrorStrings.MashupAndPackageCannotBeBothSpecified, argumentName);
			}
			if (mashupConnectionStringBuilder.MaxCacheSize != -1L && mashupConnectionStringBuilder.MaxCacheSize < 20971520L && mashupConnectionStringBuilder.CachePath != "$MemoryCache$")
			{
				throw new ArgumentException(ProviderErrorStrings.MinCacheSizeLimit(20L, 20971520L), argumentName);
			}
			if (mashupConnectionStringBuilder.MaxTempSize != -1L && mashupConnectionStringBuilder.MaxTempSize < 20971520L && mashupConnectionStringBuilder.CachePath != "$MemoryCache$")
			{
				throw new ArgumentException(ProviderErrorStrings.MinTempSizeLimit(20L, 20971520L), argumentName);
			}
			if (mashupConnectionStringBuilder.Timeout < 0)
			{
				throw new ArgumentException(ProviderErrorStrings.ConnectionTimeoutNegative, argumentName);
			}
			this.mashupCommandBehavior = MashupCommandBehavior.Default;
			if (MashupConnection.EnablePreviewConnectionStringProperties)
			{
				this.Pool = (string)mashupConnectionStringBuilder["Pool"];
				this.Session = (string)mashupConnectionStringBuilder["Session"];
				this.throwFoldingFailures = (bool)mashupConnectionStringBuilder["ThrowFoldingFailures"];
				this.throwOnVolatileFunctions = (bool)mashupConnectionStringBuilder["ThrowOnVolatileFunctions"];
				this.DataSourcePool = (string)mashupConnectionStringBuilder["DataSourcePool"];
				this.tracingOptions = (string)mashupConnectionStringBuilder["TracingOptions"];
				this.cacheGroup = (string)mashupConnectionStringBuilder["CacheGroup"];
				this.metadataCache = (string)mashupConnectionStringBuilder["MetadataCache"];
				string text = (string)mashupConnectionStringBuilder["MashupCommandBehavior"];
				if (string.IsNullOrEmpty(text))
				{
					goto IL_02B8;
				}
				try
				{
					this.mashupCommandBehavior = (MashupCommandBehavior)Enum.Parse(typeof(MashupCommandBehavior), text);
					goto IL_02B8;
				}
				catch (ArgumentException)
				{
					throw new MashupException(ProviderErrorStrings.ConnectionStringPropertyNotSupported(text, "MashupCommandBehavior"));
				}
			}
			this.Pool = null;
			this.Session = null;
			this.throwFoldingFailures = false;
			this.throwOnVolatileFunctions = false;
			this.DataSourcePool = null;
			this.cacheGroup = null;
			this.metadataCache = null;
			IL_02B8:
			this.connectionString = value;
			this.connectionTimeout = mashupConnectionStringBuilder.Timeout;
			this.mashup = mashupConnectionStringBuilder.Mashup;
			this.packageString = mashupConnectionStringBuilder.Package;
			this.location = mashupConnectionStringBuilder.Location;
			this.fastCombine = mashupConnectionStringBuilder.FastCombine;
			this.dataSourceSettings = DataSourceSettings.ToDictionary(mashupConnectionStringBuilder.DataSourceSettings);
			this.privacyPartitionDataSources = PrivacyPartitionDataSources.ToDictionary(mashupConnectionStringBuilder.PrivacyPartitionDataSources);
			this.partitionValues = mashupConnectionStringBuilder.PartitionValues;
			this.allowNativeQueries = mashupConnectionStringBuilder.AllowNativeQueries;
			this.activityId = mashupConnectionStringBuilder.ActivityId;
			this.correlationId = mashupConnectionStringBuilder.CorrelationId;
			this.legacyRedirects = mashupConnectionStringBuilder.LegacyRedirects;
			this.returnErrorValuesAsNull = mashupConnectionStringBuilder.ReturnErrorValuesAsNull;
			this.throwEnumerationErrors = mashupConnectionStringBuilder.ThrowEnumerationErrors;
			this.ignorePreviouslyCachedData = mashupConnectionStringBuilder.IgnorePreviouslyCachedData;
			this.persistSecurityInfo = mashupConnectionStringBuilder.PersistSecurityInfo;
			this.cachePath = (string.IsNullOrEmpty(mashupConnectionStringBuilder.CachePath) ? MashupConnection.GetCacheFolder(MashupConnection.DefaultStoragePath) : MashupConnection.GetCacheFolderWithErrorHandling(mashupConnectionStringBuilder.CachePath, argumentName));
			this.maxCacheSize = mashupConnectionStringBuilder.MaxCacheSize;
			this.memoryCache = mashupConnectionStringBuilder.CachePath == "$MemoryCache$";
			this.maxTempSize = ((mashupConnectionStringBuilder.MaxTempSize == -1L) ? (-1L) : mashupConnectionStringBuilder.MaxTempSize);
			this.tempPath = (string.IsNullOrEmpty(mashupConnectionStringBuilder.TempPath) ? MashupConnection.GetTempFolder(MashupConnection.DefaultStoragePath) : MashupConnection.GetTempFolderWithErrorHandling(mashupConnectionStringBuilder.TempPath, argumentName));
			this.cacheEncryptionCertificateThumbprint = mashupConnectionStringBuilder.CacheEncryptionCertificateThumbprint;
			this.debug = mashupConnectionStringBuilder.Debug;
			if (this.pool.UseConnectionStringAsSession && this.session == null)
			{
				this.session = MashupConnection.keyHasher.HashKey(this.connectionString);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008FF4 File Offset: 0x000071F4
		private static void SetOptionalProperties(MashupConnectionStringBuilder builder, string optional)
		{
			if (!string.IsNullOrEmpty(optional))
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
				{
					ConnectionString = optional
				};
				foreach (object obj in dbConnectionStringBuilder.Keys)
				{
					string text = (string)obj;
					object obj2;
					if (!builder.TryGetValue(text, out obj2) || obj2 == null)
					{
						try
						{
							builder[text] = dbConnectionStringBuilder[text];
							continue;
						}
						catch (ArgumentException)
						{
							continue;
						}
					}
					if (!obj2.Equals(dbConnectionStringBuilder[text]))
					{
						throw new MashupException(ProviderErrorStrings.DuplicateConnectionProperty(text));
					}
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000090AC File Offset: 0x000072AC
		internal static string GetCacheFolderWithErrorHandling(string baseFolder, string argumentName)
		{
			string cacheFolder;
			try
			{
				cacheFolder = MashupConnection.GetCacheFolder(baseFolder);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException(ProviderErrorStrings.CachePathNotValid, argumentName, ex);
			}
			return cacheFolder;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000090E4 File Offset: 0x000072E4
		internal static string GetCacheFolder(string baseFolder)
		{
			return Path.Combine(baseFolder, MashupConnection.CacheSubPath);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000090F4 File Offset: 0x000072F4
		internal static string GetTempFolderWithErrorHandling(string baseFolder, string argumentName)
		{
			string tempFolder;
			try
			{
				tempFolder = MashupConnection.GetTempFolder(baseFolder);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException(ProviderErrorStrings.TempPathNotValid, argumentName, ex);
			}
			return tempFolder;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000912C File Offset: 0x0000732C
		internal static string GetTempFolder(string baseFolder)
		{
			return Path.Combine(baseFolder, MashupConnection.TempSubPath);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00009139 File Offset: 0x00007339
		internal static string DefaultCachePath
		{
			get
			{
				return MashupConnection.GetCacheFolder(MashupConnection.DefaultStoragePath);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009145 File Offset: 0x00007345
		internal static string DefaultTempPath
		{
			get
			{
				return MashupConnection.GetTempFolder(MashupConnection.DefaultStoragePath);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009151 File Offset: 0x00007351
		public IEnumerable<MashupDiscovery> FindReferencedDataSources()
		{
			return this.FindReferencedDataSources(MashupDiscoveryOptions.None, MashupPartitionCoordinateType.None);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000915B File Offset: 0x0000735B
		public IEnumerable<MashupDiscovery> FindReferencedDataSources(MashupDiscoveryOptions options)
		{
			return this.FindReferencedDataSources(options, MashupPartitionCoordinateType.None);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009168 File Offset: 0x00007368
		public IEnumerable<MashupDiscovery> FindReferencedDataSources(MashupDiscoveryOptions options, MashupPartitionCoordinateType coordinateType)
		{
			MashupDiscoveryOptions mashupDiscoveryOptions = MashupDiscoveryOptions.ReportNavigationSteps | MashupDiscoveryOptions.MultipleNavigationSteps;
			if ((options & mashupDiscoveryOptions) == MashupDiscoveryOptions.MultipleNavigationSteps)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			IEvaluationConstants evaluationConstants = ((this.ActivityId != null) ? new EvaluationConstants(this.ActivityId.Value, this.CorrelationId, null) : null);
			return DistinctDataSourceDiscoveryVisitor.FindDataSources(this.GetDocuments(), options, coordinateType, evaluationConstants);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000091C8 File Offset: 0x000073C8
		public IEnumerable<DataSourceReference> FindReferencedDataSources(out bool mayHaveMoreDataSources, out bool mayHaveMoreNativeQueries)
		{
			IEnumerable<MashupDiscovery> enumerable = this.FindReferencedDataSources();
			mayHaveMoreDataSources = enumerable.Any((MashupDiscovery d) => d.Kind == MashupDiscoveryKind.UnknownCallSite || d.Kind == MashupDiscoveryKind.Unsupported);
			mayHaveMoreNativeQueries = enumerable.Any((MashupDiscovery d) => d.Kind == MashupDiscoveryKind.UnknownNativeQuery);
			return from d in enumerable
				select d.DataSourceReference into d
				where d != null
				select d;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00009273 File Offset: 0x00007473
		internal IEnumerable<IDocument> GetDocuments()
		{
			return MashupPackage.GetDocuments(this.packageString, this.mashup);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009286 File Offset: 0x00007486
		internal ConnectionContext CreateContext()
		{
			return new MashupConnection.MashupConnectionContext(this);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00009290 File Offset: 0x00007490
		internal void ClearProgress()
		{
			Dictionary<Pair<string, string>, DataSourceProgress> dictionary = this.progress;
			lock (dictionary)
			{
				this.currentPartitionProgress = null;
				this.deleteProgressUpdaterAfterRun = false;
				this.progress.Clear();
				Interlocked.Exchange<IEnumerable<DataSourceProgress>>(ref this.progressResults, EmptyArray<DataSourceProgress>.Instance);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000092F4 File Offset: 0x000074F4
		internal void SetProgressUpdater(Func<IEnumerable<PartitionProgress>> updater)
		{
			Dictionary<Pair<string, string>, DataSourceProgress> dictionary = this.progress;
			lock (dictionary)
			{
				if (updater == null)
				{
					this.deleteProgressUpdaterAfterRun = true;
				}
				else
				{
					this.deleteProgressUpdaterAfterRun = false;
					this.currentPartitionProgress = updater;
				}
			}
		}

		// Token: 0x040000BC RID: 188
		private const long DefaultMaxTempSize = -1L;

		// Token: 0x040000BD RID: 189
		private const string ExcelInteropModule = "ExcelInterop";

		// Token: 0x040000BE RID: 190
		private const string ConnectionStringOptional = "ConnectionString[Optional]";

		// Token: 0x040000BF RID: 191
		internal const int MBInBytes = 1048576;

		// Token: 0x040000C0 RID: 192
		internal const long MinCacheSizeLimitInMB = 20L;

		// Token: 0x040000C1 RID: 193
		internal const long MinCacheSizeLimit = 20971520L;

		// Token: 0x040000C2 RID: 194
		internal const long MinTempSizeLimitInMB = 20L;

		// Token: 0x040000C3 RID: 195
		internal const long MinTempSizeLimit = 20971520L;

		// Token: 0x040000C4 RID: 196
		private static readonly string ProviderSubPath = Path.Combine("Microsoft", "MashupProvider");

		// Token: 0x040000C5 RID: 197
		private static readonly string CacheSubPath = Path.Combine(MashupConnection.ProviderSubPath, "Cache");

		// Token: 0x040000C6 RID: 198
		private static readonly string TempSubPath = Path.Combine(MashupConnection.ProviderSubPath, "Temp");

		// Token: 0x040000C7 RID: 199
		private static readonly KeyHasher keyHasher = new KeyHasher();

		// Token: 0x040000C8 RID: 200
		private static readonly string DefaultStoragePath = Path.GetTempPath();

		// Token: 0x040000C9 RID: 201
		private static readonly Dictionary<string, DataTable> schemaTables = new Dictionary<string, DataTable>();

		// Token: 0x040000CA RID: 202
		private static readonly object staticSyncRoot = new object();

		// Token: 0x040000CB RID: 203
		internal static readonly TimeSpan defaultCleanupTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x040000CC RID: 204
		private static bool enablePreviewConnectionStringProperties;

		// Token: 0x040000CD RID: 205
		private static bool allowFileAccess = true;

		// Token: 0x040000CE RID: 206
		private static bool allowBrowserAccess = true;

		// Token: 0x040000CF RID: 207
		private static bool allowWindowsCredentials = true;

		// Token: 0x040000D0 RID: 208
		private static bool allowCurrentWorkbook = true;

		// Token: 0x040000D1 RID: 209
		private static bool enableInformationProtection = false;

		// Token: 0x040000D2 RID: 210
		private static bool reportStagingProgress;

		// Token: 0x040000D3 RID: 211
		private static readonly MashupConnectionPool2 defaultPool = MashupConnectionPool2.DefaultPool;

		// Token: 0x040000D4 RID: 212
		private static ResourceKindInfo currentWorkbookResource;

		// Token: 0x040000D5 RID: 213
		private string connectionString;

		// Token: 0x040000D6 RID: 214
		private MashupConnectionPool2 pool;

		// Token: 0x040000D7 RID: 215
		private int connectionTimeout;

		// Token: 0x040000D8 RID: 216
		private string mashup;

		// Token: 0x040000D9 RID: 217
		private string packageString;

		// Token: 0x040000DA RID: 218
		private string location;

		// Token: 0x040000DB RID: 219
		private IDictionary<DataSource, DataSourceSetting> dataSourceSettings;

		// Token: 0x040000DC RID: 220
		private IDictionary<string, DataSource[]> privacyPartitionDataSources;

		// Token: 0x040000DD RID: 221
		private bool fastCombine;

		// Token: 0x040000DE RID: 222
		private bool partitionValues;

		// Token: 0x040000DF RID: 223
		private DataTable filterDataTable;

		// Token: 0x040000E0 RID: 224
		private bool allowNativeQueries;

		// Token: 0x040000E1 RID: 225
		private long maxCacheSize;

		// Token: 0x040000E2 RID: 226
		private bool memoryCache;

		// Token: 0x040000E3 RID: 227
		private string cachePath;

		// Token: 0x040000E4 RID: 228
		private long maxTempSize;

		// Token: 0x040000E5 RID: 229
		private string tempPath;

		// Token: 0x040000E6 RID: 230
		private string cacheEncryptionCertificateThumbprint;

		// Token: 0x040000E7 RID: 231
		private string session;

		// Token: 0x040000E8 RID: 232
		private IDisposable sessionLease;

		// Token: 0x040000E9 RID: 233
		private bool legacyRedirects;

		// Token: 0x040000EA RID: 234
		private bool returnErrorValuesAsNull;

		// Token: 0x040000EB RID: 235
		private bool throwEnumerationErrors;

		// Token: 0x040000EC RID: 236
		private bool throwFoldingFailures;

		// Token: 0x040000ED RID: 237
		private bool throwOnVolatileFunctions;

		// Token: 0x040000EE RID: 238
		private bool ignorePreviouslyCachedData;

		// Token: 0x040000EF RID: 239
		private SafeHandle threadIdentity;

		// Token: 0x040000F0 RID: 240
		private List<string> optionalModules;

		// Token: 0x040000F1 RID: 241
		private bool persistSecurityInfo;

		// Token: 0x040000F2 RID: 242
		private string dataSourcePool;

		// Token: 0x040000F3 RID: 243
		private bool debug;

		// Token: 0x040000F4 RID: 244
		private string cacheGroup;

		// Token: 0x040000F5 RID: 245
		private string metadataCache;

		// Token: 0x040000F6 RID: 246
		private DateTimeOffset? now;

		// Token: 0x040000F7 RID: 247
		private IDictionary<string, object> configurationProperties;

		// Token: 0x040000F8 RID: 248
		private string tracingOptions;

		// Token: 0x040000F9 RID: 249
		private MashupCommandBehavior mashupCommandBehavior;

		// Token: 0x040000FA RID: 250
		private ConnectionState connectionState;

		// Token: 0x040000FB RID: 251
		private IPackage package;

		// Token: 0x040000FC RID: 252
		private ISectionDocument[] documents;

		// Token: 0x040000FD RID: 253
		private CredentialsStorage engineCredentials;

		// Token: 0x040000FE RID: 254
		private QueryPermissionsStorage engineQueryPermissions;

		// Token: 0x040000FF RID: 255
		private FirewallStorage engineFirewallRules;

		// Token: 0x04000100 RID: 256
		private IFirewallPlan firewallPlan;

		// Token: 0x04000101 RID: 257
		private Guid? activityId;

		// Token: 0x04000102 RID: 258
		private string correlationId;

		// Token: 0x04000103 RID: 259
		private readonly Dictionary<Pair<string, string>, DataSourceProgress> progress;

		// Token: 0x04000104 RID: 260
		private IEnumerable<DataSourceProgress> progressResults;

		// Token: 0x04000105 RID: 261
		private Func<IEnumerable<PartitionProgress>> currentPartitionProgress;

		// Token: 0x04000106 RID: 262
		private int inUpdatingProgress;

		// Token: 0x04000107 RID: 263
		private bool deleteProgressUpdaterAfterRun;

		// Token: 0x04000108 RID: 264
		private readonly ContextAwareEvent<DataSourceSettingNeededEventArgs> dataSourceSettingNeeded;

		// Token: 0x04000109 RID: 265
		private readonly DiagnosticEvents diagnosticEvents;

		// Token: 0x0400010A RID: 266
		private MashupEvaluationCounters evaluationCounters;

		// Token: 0x0400010B RID: 267
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x0400010C RID: 268
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x0400010D RID: 269
		private static readonly HashSet<string> containerFileList = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Microsoft.Data.Edm.NetFX35.dll", "Microsoft.Data.OData.NetFX35.dll", "Microsoft.Data.OData.Query.NetFX35.dll", "System.Spatial.NetFX35.dll", "Microsoft.OData.Core.NetFX35.dll", "Microsoft.OData.Edm.NetFX35.dll", "Microsoft.Spatial.NetFX35.dll", "Microsoft.OData.Core.NetFX35.V7.dll", "Microsoft.OData.Edm.NetFX35.V7.dll", "Microsoft.Spatial.NetFX35.V7.dll",
			"Microsoft.PowerBI.AdomdClient.dll", "Microsoft.HostIntegration.Connectors.dll", "Microsoft.Exchange.WebServices.dll", "Microsoft.mshtml.dll"
		};

		// Token: 0x0400010E RID: 270
		private static readonly HashSet<string> extensionList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".exe", ".dll", ".config" };

		// Token: 0x02000075 RID: 117
		private sealed class MashupConnectionContext : ConnectionContext, IEvaluationEventHandler
		{
			// Token: 0x060004AB RID: 1195 RVA: 0x000113C4 File Offset: 0x0000F5C4
			public MashupConnectionContext(MashupConnection connection)
				: base(connection.Session, connection.ThreadIdentity)
			{
				this.connection = connection;
				EventHandler<DataSourceSettingNeededEventArgs> eventHandler = this.connection.dataSourceSettingNeeded.EventHandler;
				if (eventHandler != null)
				{
					this.handlers = new ResourceCredentialHandlers();
					this.handlers.DataSourceSettingNeeded.AddContextAwareHandler(eventHandler);
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001141A File Offset: 0x0000F61A
			public override bool IsDataSourceSettingUpdatable
			{
				get
				{
					return base.IsDataSourceSettingUpdatable || this.handlers != null;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001142F File Offset: 0x0000F62F
			public override MashupConnectionPool2 ConnectionPool
			{
				get
				{
					return this.connection.pool;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001143C File Offset: 0x0000F63C
			IEnumerable<IResource> IEvaluationEventHandler.TracedResources
			{
				get
				{
					return EmptyArray<IResource>.Instance;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060004AF RID: 1199 RVA: 0x00011443 File Offset: 0x0000F643
			IEnumerable<string> IEvaluationEventHandler.SubscribedChannels
			{
				get
				{
					return this.connection.diagnosticEvents.Handlers.Keys;
				}
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0001145C File Offset: 0x0000F65C
			public override MashupResourceProperties CreateMashupResourceProperties()
			{
				IPackage package = this.connection.Package;
				bool partitionValues = this.connection.PartitionValues;
				byte[] array = Util.SerializeDataTable(this.connection.FilterDataTable);
				bool allowAutomaticCredentials = this.connection.AllowAutomaticCredentials;
				bool allowWindowsAuthentication = this.connection.AllowWindowsAuthentication;
				bool allowNativeQueries = this.connection.AllowNativeQueries;
				bool fastCombine = this.connection.FastCombine;
				long maxCacheSize = this.connection.MaxCacheSize;
				bool memoryCache = this.connection.MemoryCache;
				string cachePath = this.connection.CachePath;
				long maxTempSize = this.connection.MaxTempSize;
				string tempPath = this.connection.TempPath;
				string cacheEncryptionCertificateThumbprint = this.connection.CacheEncryptionCertificateThumbprint;
				string session = this.connection.Session;
				Guid? activityId = this.connection.ActivityId;
				return new MashupResourceProperties(package, partitionValues, array, allowAutomaticCredentials, allowWindowsAuthentication, allowNativeQueries, fastCombine, maxCacheSize, memoryCache, cachePath, maxTempSize, tempPath, cacheEncryptionCertificateThumbprint, session, (activityId != null) ? activityId : MashupResource.GetActivityId(), this.connection.CorrelationId, this.connection.LegacyRedirects, this.connection.ThrowFoldingFailures, this.connection.ThrowOnVolatileFunctions, this.connection.IgnorePreviouslyCachedData, this.connection.ThreadIdentity, CultureInfo.CurrentUICulture.Name, this.connection.Now, this.connection.ConfigurationProperties, this.connection.TracingOptions, this.connection.EngineCredentials, this.connection.EngineQueryPermissions, this.connection.EngineFirewallRules, this.connection.FirewallPlan, this.connection.DataSourcePool, this.connection.debug, this.connection.cacheGroup, this.connection.metadataCache, delegate(Func<IEnumerable<PartitionProgress>> partitionProgress)
				{
					this.connection.SetProgressUpdater(partitionProgress);
				}, this.connection.evaluationCounters);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x00011606 File Offset: 0x0000F806
			public override void Dispose()
			{
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x00011608 File Offset: 0x0000F808
			protected override IEnumerable<IEvaluationEventHandler> GetEvaluationHandlers()
			{
				IEnumerable<IEvaluationEventHandler> enumerable = base.GetEvaluationHandlers();
				if (this.handlers != null || this.connection.diagnosticEvents.Handlers.Count > 0)
				{
					enumerable = enumerable.ConcatSingle(this);
				}
				return enumerable;
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x00011645 File Offset: 0x0000F845
			ResourceCredentialCollection IEvaluationEventHandler.GetCredentials(ConnectionContext context, IResource resource)
			{
				return this.handlers.GetCredentials(context, resource);
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x00011654 File Offset: 0x0000F854
			ResourceCredentialCollection IEvaluationEventHandler.RefreshCredential(ConnectionContext context, ResourceCredentialCollection credential)
			{
				return this.handlers.RefreshCredential(context, credential);
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x00011663 File Offset: 0x0000F863
			void IEvaluationEventHandler.TraceRequest(RequestTraceData traceData)
			{
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x00011665 File Offset: 0x0000F865
			bool IEvaluationEventHandler.IsQueryExecutionPermitted(ConnectionContext context, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
			{
				return this.handlers.IsQueryExecutionPermitted(context, resource, type, query, parameterCount, parameterNames);
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x0001167B File Offset: 0x0000F87B
			bool IEvaluationEventHandler.TryUpdateFirewallGroup(ConnectionContext context, IResource resource, FirewallGroup2 originalGroup, IValue traits, out FirewallGroup2 newGroup)
			{
				return this.handlers.TryUpdateFirewallGroup(context, resource, originalGroup, traits, out newGroup);
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0001168F File Offset: 0x0000F88F
			void IEvaluationEventHandler.DiagnosticEvent(ConnectionContext context, string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> parameters)
			{
				this.connection.diagnosticEvents.Emit(context, channelName, eventName, eventTime, resource, parameters);
			}

			// Token: 0x04000252 RID: 594
			private readonly MashupConnection connection;

			// Token: 0x04000253 RID: 595
			private readonly ResourceCredentialHandlers handlers;
		}
	}
}
