using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.HostIntegration.CounterTelemetry;
using Microsoft.HostIntegration.CounterTelemetry.DI;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.StaticSqlUtil;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200094D RID: 2381
	internal class Requester : IRequester, IComparable
	{
		// Token: 0x06004A44 RID: 19012 RVA: 0x0011A5D0 File Offset: 0x001187D0
		static Requester()
		{
			Logger.maxTracingLevel = 6;
			Logger.TracePointLogger = new Action<LogData>(Requester.TracePointLog);
			Requester._db2DateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd-HH.mm.ss.ffffff", DateTimeMaskType.DateTime, false));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("HH.mm.ss", DateTimeMaskType.Time, false));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("HH:mm:ss", DateTimeMaskType.Time, false));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd", DateTimeMaskType.Date, false));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("MM/dd/yyyy", DateTimeMaskType.Date, false));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd-HH.mm.ss.ffffff", DateTimeMaskType.DateTime, true));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("HH.mm.ss", DateTimeMaskType.Time, true));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("HH:mm:ss", DateTimeMaskType.Time, true));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd", DateTimeMaskType.Date, true));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("MM/dd/yyyy", DateTimeMaskType.Date, true));
			Requester._db2DateTimeMasks.Add(new DateTimeMask("dd.MM.yyyy", DateTimeMaskType.Date, true));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd-HH.mm.ss.ffffff", DateTimeMaskType.DateTime, false));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("HH:mm:ss", DateTimeMaskType.Time, false));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd", DateTimeMaskType.Date, false));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("MM/dd/yyyy", DateTimeMaskType.Date, false));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd HH:mm:ss.ffffff", DateTimeMaskType.DateTime, true));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd-HH.mm.ss.ffffff", DateTimeMaskType.DateTime, true));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("HH:mm:ss", DateTimeMaskType.Time, true));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("HH.mm.ss", DateTimeMaskType.Time, true));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("yyyy-MM-dd", DateTimeMaskType.Date, true));
			Requester._informixDateTimeMasks.Add(new DateTimeMask("MM/dd/yyyy", DateTimeMaskType.Date, true));
			Requester.drdaArCounterTelemetry = new DrdaArCounterTelemetryContainer();
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0011A82C File Offset: 0x00118A2C
		public Requester(string[] connectInfo)
		{
			this.ConnectionInfo = connectInfo;
			this._connectionString = Requester.GenerateConnectionString(this._connectInfo);
			this._encodedConnectInfo = Requester.EncodeConnectString(this._connectionString);
			this._needPool = Requester.NeedConnectionPool(connectInfo);
			this.IsDuw = string.Compare(this._connectInfo[17], "DUW", true) == 0;
			this.Timeout = 15000;
			this.LiteralReplacement = Utility.ParseBoolean(this._connectInfo[54]);
			this.registerSettings = new List<string>();
			this.LobLength = new List<int>();
			this.Mode = new List<int>();
			this.ProgRef = null;
			this._anyProgRef = 1;
			this.BlobDataOverrun = false;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0011A964 File Offset: 0x00118B64
		public void Initialize(DrdaClientTraceContainer container, Func<string, string, int, int, Exception> exceptionMaker)
		{
			this._state = Requester.RequesterState.Initialized;
			if (container != null)
			{
				this._traceContainer = container;
				this._tracePoint = new ApplicationRequesterTracePoint(container);
				this._commonTracePoint = new DrdaCommonTracePoint(container, 8);
			}
			this._exceptionMaker = exceptionMaker;
			this.NeedSetDefaultQualifierSet = false;
			this.ExternalExceptionMakerEnabled = true;
			this.HadException = false;
			this._connectionManager = ConnectionManager.GetInstance(this);
			this._superVisor = new Supervisor(this);
			this._securityManager = new SecurityManager(this);
			this._sqlManager = new SqlManager(this);
			this._transactionManager = TransactionManager.GetInstance(this);
			if (this._transactionManager is LocalTransactionManager)
			{
				this._localTransactionManager = (LocalTransactionManager)this._transactionManager;
			}
			else
			{
				this._localTransactionManager = new LocalTransactionManager(this);
				this._localTransactionManager.Initialize();
			}
			this._packageManager = new PackageManager(this);
			this._connectionManager.Initialize();
			this._superVisor.Initialize();
			this._securityManager.Initialize();
			this._sqlManager.Initialize();
			this._transactionManager.Initialize();
			this._packageManager.Initialize();
			this.LobLength.Clear();
			this.Mode.Clear();
			this.ProgRef = null;
			this._anyProgRef = 1;
			this.BlobDataOverrun = false;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0011AAA4 File Offset: 0x00118CA4
		public void Reset(DrdaClientTraceContainer container, Func<string, string, int, int, Exception> exceptionMaker)
		{
			if (this._state != Requester.RequesterState.Accrdb)
			{
				this._state = Requester.RequesterState.Initialized;
			}
			if (container != this._traceContainer)
			{
				this._traceContainer = container;
				this._tracePoint = new ApplicationRequesterTracePoint(container);
				this._commonTracePoint = new DrdaCommonTracePoint(container, 8);
			}
			this._exceptionMaker = exceptionMaker;
			this.Timeout = 15000;
			this._isolationLevel = SqlIsolationLevels.ReadCommitted;
			this._listStatements.Clear();
			this._sqlManager.Reset();
			this._transactionManager.Reset();
			if (!(this._transactionManager is LocalTransactionManager))
			{
				this._localTransactionManager.Reset();
			}
			this._packageManager.Reset();
			this.LobLength.Clear();
			this.Mode.Clear();
			this.ProgRef = null;
			this.BlobDataOverrun = false;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x0011AB70 File Offset: 0x00118D70
		public static string GenerateConnectionString(string[] connectInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < connectInfo.Length; i++)
			{
				string text = connectInfo[i];
				ConnectionKey connectionKey = (ConnectionKey)i;
				if (connectionKey == ConnectionKey.KEY_CLIENTACCOUNTING || connectionKey == ConnectionKey.KEY_CLIENTAPPNAME || connectionKey == ConnectionKey.KEY_CLIENTUSERID || connectionKey == ConnectionKey.KEY_CLIENTWORKSTATION || connectionKey == ConnectionKey.KEY_SPECIALREGISTERS)
				{
					text = null;
				}
				if (!string.IsNullOrWhiteSpace(text))
				{
					stringBuilder.Append(text);
				}
				stringBuilder.Append(';');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x0011ABD4 File Offset: 0x00118DD4
		private static string EncodeConnectString(string connectionString)
		{
			int num = connectionString.Length * 2;
			byte[] array = new byte[16 * ((num + 15) / 16)];
			global::System.Buffer.BlockCopy(connectionString.ToCharArray(), 0, array, 0, num);
			ProtectedMemory.Protect(array, MemoryProtectionScope.SameProcess);
			return Convert.ToBase64String(array);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0011AC16 File Offset: 0x00118E16
		public static string EncodeConnectInfo(string[] connectInfo)
		{
			return Requester.EncodeConnectString(Requester.GenerateConnectionString(connectInfo));
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x0011AC23 File Offset: 0x00118E23
		public string EncodedConnectInfo
		{
			get
			{
				return this._encodedConnectInfo;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x0011AC2B File Offset: 0x00118E2B
		// (set) Token: 0x06004A4D RID: 19021 RVA: 0x0011AC33 File Offset: 0x00118E33
		internal string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0011AC3C File Offset: 0x00118E3C
		public bool IsPoolConnection
		{
			get
			{
				return this._needPool;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x0011AC44 File Offset: 0x00118E44
		public string[] ConnectInfo
		{
			get
			{
				return this._connectInfo;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0011AC4C File Offset: 0x00118E4C
		// (set) Token: 0x06004A51 RID: 19025 RVA: 0x0011AC54 File Offset: 0x00118E54
		public RequesterPool Pool
		{
			get
			{
				return this._pool;
			}
			set
			{
				this._pool = value;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x0011AC5D File Offset: 0x00118E5D
		// (set) Token: 0x06004A53 RID: 19027 RVA: 0x0011AC65 File Offset: 0x00118E65
		public bool IsDb2Gateway
		{
			get
			{
				return this._isDb2Gateway;
			}
			set
			{
				this._isDb2Gateway = value;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x0011AC6E File Offset: 0x00118E6E
		// (set) Token: 0x06004A55 RID: 19029 RVA: 0x0011AC76 File Offset: 0x00118E76
		public bool IsIMSDB
		{
			get
			{
				return this._isIMSDB;
			}
			set
			{
				this._isIMSDB = value;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x0011AC7F File Offset: 0x00118E7F
		// (set) Token: 0x06004A57 RID: 19031 RVA: 0x0011AC87 File Offset: 0x00118E87
		public bool IsConvertToBigEndian
		{
			get
			{
				return this._isConvertToBigEndian;
			}
			set
			{
				this._isConvertToBigEndian = value;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004A58 RID: 19032 RVA: 0x0011AC90 File Offset: 0x00118E90
		// (set) Token: 0x06004A59 RID: 19033 RVA: 0x0011AC98 File Offset: 0x00118E98
		public bool UseHIS2013Constants
		{
			get
			{
				return this._useHIS2013Constants;
			}
			set
			{
				this._useHIS2013Constants = value;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x0011ACA1 File Offset: 0x00118EA1
		// (set) Token: 0x06004A5B RID: 19035 RVA: 0x0011ACA9 File Offset: 0x00118EA9
		public bool XMLAsBinary
		{
			get
			{
				return this._xmlAsBinary;
			}
			set
			{
				this._xmlAsBinary = value;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x0011ACB2 File Offset: 0x00118EB2
		// (set) Token: 0x06004A5D RID: 19037 RVA: 0x0011ACBA File Offset: 0x00118EBA
		public bool UseAccelerator
		{
			get
			{
				return this._useAccelerator;
			}
			set
			{
				this._useAccelerator = value;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x0011ACC3 File Offset: 0x00118EC3
		// (set) Token: 0x06004A5F RID: 19039 RVA: 0x0011ACCB File Offset: 0x00118ECB
		public string RetDatabaseName
		{
			get
			{
				return this._retDatabaseName;
			}
			set
			{
				this._retDatabaseName = value;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x0011ACD4 File Offset: 0x00118ED4
		// (set) Token: 0x06004A61 RID: 19041 RVA: 0x0011ACDC File Offset: 0x00118EDC
		public string EncryptionAlgorithm
		{
			get
			{
				return this._encryptionAlgorithm;
			}
			set
			{
				this._encryptionAlgorithm = value;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x0011AC44 File Offset: 0x00118E44
		// (set) Token: 0x06004A63 RID: 19043 RVA: 0x0011ACE5 File Offset: 0x00118EE5
		public string[] ConnectionInfo
		{
			get
			{
				return this._connectInfo;
			}
			set
			{
				this.RdbName = string.Empty;
				this._connectInfo = value;
				if (this._connectInfo != null && !string.IsNullOrEmpty(this._connectInfo[3]))
				{
					this.RdbName = this._connectInfo[3].ToUpperInvariant();
				}
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x0011AD24 File Offset: 0x00118F24
		public string PackageCollection
		{
			get
			{
				if (this._flavor == DrdaFlavor.Informix)
				{
					return "NULLID";
				}
				string text = this._connectInfo[12];
				if (string.IsNullOrWhiteSpace(text))
				{
					text = "NULLID";
				}
				return text.ToUpperInvariant();
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x0011AD5E File Offset: 0x00118F5E
		public DrdaCommonTracePoint CommonTracePoint
		{
			get
			{
				return this._commonTracePoint;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x0011AD66 File Offset: 0x00118F66
		public ApplicationRequesterTracePoint TracePoint
		{
			get
			{
				return this._tracePoint;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x0011AD6E File Offset: 0x00118F6E
		public ConnectionManager ConnectionManager
		{
			get
			{
				return this._connectionManager;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x0011AD76 File Offset: 0x00118F76
		public Supervisor Supervisor
		{
			get
			{
				return this._superVisor;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004A69 RID: 19049 RVA: 0x0011AD7E File Offset: 0x00118F7E
		public SecurityManager SecurityManager
		{
			get
			{
				return this._securityManager;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004A6A RID: 19050 RVA: 0x0011AD86 File Offset: 0x00118F86
		public SqlManager SqlManager
		{
			get
			{
				return this._sqlManager;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x0011AD8E File Offset: 0x00118F8E
		public TransactionManager TransactionManager
		{
			get
			{
				return this._transactionManager;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06004A6C RID: 19052 RVA: 0x0011AD96 File Offset: 0x00118F96
		public LocalTransactionManager LocalTransactionManager
		{
			get
			{
				return this._localTransactionManager;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x0011AD9E File Offset: 0x00118F9E
		public PackageManager PackageManager
		{
			get
			{
				return this._packageManager;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x0011ADA6 File Offset: 0x00118FA6
		public uint ServerMajorVersion
		{
			get
			{
				return this._serverMajorVersion;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x0011ADAE File Offset: 0x00118FAE
		// (set) Token: 0x06004A70 RID: 19056 RVA: 0x0011ADB6 File Offset: 0x00118FB6
		public byte[] RdbInterruptToken
		{
			get
			{
				return this._rdbIntToken;
			}
			set
			{
				this._rdbIntToken = value;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x0011ADBF File Offset: 0x00118FBF
		// (set) Token: 0x06004A72 RID: 19058 RVA: 0x0011ADC7 File Offset: 0x00118FC7
		public IPEndPoint RdbInterruptEndPoint
		{
			get
			{
				return this._interruptEndpoint;
			}
			set
			{
				this._interruptEndpoint = value;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x0011ADD0 File Offset: 0x00118FD0
		// (set) Token: 0x06004A74 RID: 19060 RVA: 0x0011ADD8 File Offset: 0x00118FD8
		public string TypeDefinitionName
		{
			get
			{
				return this._typeDefNam;
			}
			set
			{
				if (string.Compare(this._typeDefNam, value, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					this._typeDefNam = value;
					this._endian = ((this._typeDefNam == "QTDSQLX86") ? EndianType.LittleEndian : EndianType.BigEndian);
					this._connectionManager.DdmWriter.EndianType = this._endian;
					this._connectionManager.DdmReader.EndianType = this._endian;
				}
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x0011AE43 File Offset: 0x00119043
		// (set) Token: 0x06004A76 RID: 19062 RVA: 0x0011AE4B File Offset: 0x0011904B
		public Ccsid CcsidRead
		{
			get
			{
				return this._ccsidRead;
			}
			set
			{
				this._ccsidRead = value;
				this.UpdateCcsid(this._ccsidRead);
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x0011AE60 File Offset: 0x00119060
		// (set) Token: 0x06004A78 RID: 19064 RVA: 0x0011AE68 File Offset: 0x00119068
		public Ccsid CcsidWrite
		{
			get
			{
				return this._ccsidWrite;
			}
			set
			{
				this._ccsidWrite = value;
				this.UpdateCcsid(this._ccsidWrite);
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x0011AE7D File Offset: 0x0011907D
		public Ccsid CcsidHost
		{
			get
			{
				return this._ccsidHost;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x0011AE85 File Offset: 0x00119085
		// (set) Token: 0x06004A7B RID: 19067 RVA: 0x0011AE8D File Offset: 0x0011908D
		public EndianType Endian
		{
			get
			{
				return this._endian;
			}
			set
			{
				this._endian = value;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x0011AE96 File Offset: 0x00119096
		public Requester.RequesterState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x0011AE9E File Offset: 0x0011909E
		public DrdaFlavor Flavor
		{
			get
			{
				return this._flavor;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x0011AEA6 File Offset: 0x001190A6
		// (set) Token: 0x06004A7F RID: 19071 RVA: 0x0011AEAE File Offset: 0x001190AE
		public int AnyProgRef
		{
			get
			{
				return this._anyProgRef;
			}
			set
			{
				this._anyProgRef = value;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06004A80 RID: 19072 RVA: 0x0011AEB7 File Offset: 0x001190B7
		// (set) Token: 0x06004A81 RID: 19073 RVA: 0x0011AEBF File Offset: 0x001190BF
		public string RdbName { get; set; }

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0011AEC8 File Offset: 0x001190C8
		public bool IsUnicodeMgrSupported
		{
			get
			{
				return this._isUnicodeMgrSupported;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x0011AED0 File Offset: 0x001190D0
		public bool AutoCommit
		{
			get
			{
				return this._autoCommit;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x0011AED8 File Offset: 0x001190D8
		public SqlIsolationLevels IsolationLevel
		{
			get
			{
				return this._isolationLevel;
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x0011AEE0 File Offset: 0x001190E0
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x0011AEE8 File Offset: 0x001190E8
		public bool HadException { get; private set; }

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x0011AEF1 File Offset: 0x001190F1
		public static string ProcessName
		{
			get
			{
				return Requester._applicationName;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x0011AEF8 File Offset: 0x001190F8
		public static FileVersionInfo Version
		{
			get
			{
				return Requester._fileVersion;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x0011AEFF File Offset: 0x001190FF
		// (set) Token: 0x06004A8A RID: 19082 RVA: 0x0011AF07 File Offset: 0x00119107
		internal bool NeedSetDefaultQualifierSet { get; set; }

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x0011AF10 File Offset: 0x00119110
		// (set) Token: 0x06004A8C RID: 19084 RVA: 0x0011AF18 File Offset: 0x00119118
		internal bool ExternalExceptionMakerEnabled { get; set; }

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x0011AF21 File Offset: 0x00119121
		// (set) Token: 0x06004A8E RID: 19086 RVA: 0x0011AF29 File Offset: 0x00119129
		internal bool IsDuw { get; set; }

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x0011AF32 File Offset: 0x00119132
		// (set) Token: 0x06004A90 RID: 19088 RVA: 0x0011AF3A File Offset: 0x0011913A
		internal int Timeout { get; private set; }

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0011AF43 File Offset: 0x00119143
		// (set) Token: 0x06004A92 RID: 19090 RVA: 0x0011AF4B File Offset: 0x0011914B
		internal int ConnectionTimeout { get; private set; }

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0011AF54 File Offset: 0x00119154
		// (set) Token: 0x06004A94 RID: 19092 RVA: 0x0011AF5C File Offset: 0x0011915C
		internal bool LiteralReplacement { get; private set; }

		// Token: 0x06004A95 RID: 19093 RVA: 0x0011AF68 File Offset: 0x00119168
		public static bool NeedConnectionPool(string[] connectInfo)
		{
			string text = connectInfo[53].ToUpper(CultureInfo.InvariantCulture);
			return text.Equals("T") || text.Equals("1") || text.Equals("TRUE") || text.Equals("Y") || text.Equals("YES");
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x0011AFC8 File Offset: 0x001191C8
		public Exception MakeException(string errorMessage, string sqlState, int sqlCode)
		{
			return this.MakeException(errorMessage, sqlState, sqlCode, 0);
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0011AFD4 File Offset: 0x001191D4
		public Exception MakeException(string errorMessage, string sqlState, int sqlCode, int errorCode)
		{
			if (Telemetry.Instance.TelemetryEnabled)
			{
				TelemetryExceptionEvent telemetryExceptionEvent = new TelemetryExceptionEvent(this._serverClass, this._serverReleaseLevel, this._providerName, Requester._applicationName, sqlState, sqlCode.ToString(), errorMessage);
				Telemetry.Instance.TrackEvent("DrdaClient", telemetryExceptionEvent);
			}
			this.HadException = true;
			if (this._exceptionMaker != null && this.ExternalExceptionMakerEnabled)
			{
				return this._exceptionMaker(errorMessage, sqlState, sqlCode, errorCode);
			}
			return new DrdaRequesterException(errorMessage, sqlState, sqlCode);
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x0011B053 File Offset: 0x00119253
		public Exception MakeException(Manager.ReplyInfo replyInfo)
		{
			return this.MakeException(replyInfo.Message, replyInfo.SqlState, replyInfo.SqlCode);
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x0011B070 File Offset: 0x00119270
		private static void TracePointLog(LogData logData)
		{
			if (logData.tracePoint == null)
			{
				return;
			}
			DrdaCommonTracePoint drdaCommonTracePoint = logData.tracePoint as DrdaCommonTracePoint;
			if (drdaCommonTracePoint == null)
			{
				return;
			}
			TraceFlags traceFlags = TraceFlags.None;
			if (logData.level >= 6)
			{
				traceFlags = TraceFlags.Data;
			}
			else if (logData.level == 5)
			{
				traceFlags = TraceFlags.Debug;
			}
			else
			{
				if ((logData.evenType & TraceEventType.Critical) != (TraceEventType)0)
				{
					traceFlags |= TraceFlags.Fatal;
				}
				if ((logData.evenType & TraceEventType.Error) != (TraceEventType)0)
				{
					traceFlags |= TraceFlags.Error;
				}
				if ((logData.evenType & TraceEventType.Warning) != (TraceEventType)0)
				{
					traceFlags |= TraceFlags.Warning;
				}
				if ((logData.evenType & TraceEventType.Information) != (TraceEventType)0)
				{
					traceFlags |= TraceFlags.Information;
				}
				if ((logData.evenType & TraceEventType.Verbose) != (TraceEventType)0)
				{
					traceFlags |= TraceFlags.Verbose;
				}
			}
			if (drdaCommonTracePoint.IsEnabled(traceFlags))
			{
				drdaCommonTracePoint.Trace(traceFlags, logData.message);
			}
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0011B114 File Offset: 0x00119314
		private void SetHostType(string hostName)
		{
			if (hostName == "QAS")
			{
				this._hostType = HostType.AS400;
				this._serverClass = "DB2/400";
				return;
			}
			if (hostName == "QMVS")
			{
				this._hostType = HostType.MVS;
				this._serverClass = "MVS/VSAM";
				return;
			}
			if (hostName == "QDB2")
			{
				this._hostType = HostType.DB2;
				this._serverClass = "DB2/MVS";
				return;
			}
			if (hostName == "QDB2/NT" || hostName == "QDB2/NT64" || hostName == "IDS/NT32" || hostName == "IDS/NT64" || hostName == "QDB2/Windows 95" || hostName == "MSAS")
			{
				this._hostType = HostType.NT;
				this._serverClass = "DB2/NT";
				return;
			}
			if (hostName == "QDB2/6s000" || hostName == "QDB2/AIX64" || hostName == "QDB2/6000 PE")
			{
				this._hostType = HostType.RS6000;
				this._serverClass = "DB2/6000";
				return;
			}
			if (hostName == "QSQLDS/VM")
			{
				this._hostType = HostType.SQLDSVM;
				this._serverClass = "SQLDS/VM";
				return;
			}
			if (hostName == "QSQLDS/VSE")
			{
				this._hostType = HostType.SQLDSVSE;
				this._serverClass = "SQLDS/VSE";
				return;
			}
			if (hostName == "QDB2/2 ")
			{
				this._hostType = HostType.OS2;
				this._serverClass = "DB2/2";
				return;
			}
			if (hostName == "QDB2/HPUX" || hostName == "QDB2/HP64" || hostName == "QDB2/HPUX-IA" || hostName == "QDB2/HPUX-IA64")
			{
				this._hostType = HostType.HP;
				this._serverClass = "DB2/HP";
				return;
			}
			if (hostName == "QDB2/SUN" || hostName == "QDB2/SUN64" || hostName == "QDB2/SUNX86" || hostName == "QDB2/SUNX8664")
			{
				this._hostType = HostType.Sun;
				this._serverClass = "DB2/SUN";
				return;
			}
			if (hostName == "QDB2/LINUX" || hostName == "QDB2/LINUX390" || hostName == "QDB2/LINUXIA64" || hostName == "QDB2/LINUXPPC" || hostName == "QDB2/LINUXPPC64" || hostName == "QDB2/LINUXZ64" || hostName == "QDB2/LINUXX8664" || hostName == "QDB2/LINUXPPC64LE")
			{
				this._hostType = HostType.Linux;
				this._serverClass = "DB2/LINUX";
				return;
			}
			if (hostName == "IDS/UNIX32" || hostName == "IDS/UNIX64" || hostName == "QDB2/SCO" || hostName == "QDB2/SGI" || hostName == "QDB2/SNI")
			{
				this._hostType = HostType.Unix;
				this._serverClass = "DB2/UNIX";
				return;
			}
			if (hostName == "DFS")
			{
				this._hostType = HostType.IMSDB;
				this._serverClass = "IMSDB";
				return;
			}
			this._hostType = HostType.Unknown;
			this._serverClass = "DB2";
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x0011B414 File Offset: 0x00119614
		private void SetServerReleaseLevel(string serverReleaseLevel)
		{
			this._serverReleaseLevel = serverReleaseLevel;
			this._serverReleaseLevel.ToCharArray();
			this._serverMajorVersion = 0U;
			this._serverSubVersion = 0U;
			this._serverModification = 0U;
			switch (this._hostType)
			{
			case HostType.AS400:
			case HostType.MVS:
			case HostType.SQLDSVM:
			case HostType.SQLDSVSE:
			{
				for (int i = 0; i < this._serverReleaseLevel.Length; i++)
				{
					if (char.IsDigit(this._serverReleaseLevel[i]))
					{
						IL_00C4:
						while (i < this._serverReleaseLevel.Length)
						{
							if (!char.IsDigit(this._serverReleaseLevel[i]))
							{
								break;
							}
							this._serverMajorVersion = this._serverMajorVersion * 10U + (uint)this._serverReleaseLevel[i] - 48U;
							i++;
						}
						while (i < this._serverReleaseLevel.Length)
						{
							if (char.IsDigit(this._serverReleaseLevel[i]))
							{
								IL_0131:
								while (i < this._serverReleaseLevel.Length)
								{
									if (!char.IsDigit(this._serverReleaseLevel[i]))
									{
										break;
									}
									this._serverSubVersion = this._serverSubVersion * 10U + (uint)this._serverReleaseLevel[i] - 48U;
									i++;
								}
								while (i < this._serverReleaseLevel.Length)
								{
									if (char.IsDigit(this._serverReleaseLevel[i]))
									{
										IL_019E:
										while (i < this._serverReleaseLevel.Length)
										{
											if (!char.IsDigit(this._serverReleaseLevel[i]))
											{
												break;
											}
											this._serverModification = this._serverModification * 10U + (uint)this._serverReleaseLevel[i] - 48U;
											i++;
										}
										goto IL_047D;
									}
									i++;
								}
								goto IL_019E;
							}
							i++;
						}
						goto IL_0131;
					}
				}
				goto IL_00C4;
			}
			case HostType.OS2:
			case HostType.NT:
			case HostType.RS6000:
			case HostType.AIX:
			case HostType.HP:
			case HostType.Sun:
				if (this._serverReleaseLevel.StartsWith("IFX"))
				{
					this._flavor = DrdaFlavor.Informix;
				}
				else
				{
					this._flavor = DrdaFlavor.DB2;
				}
				if (this._serverReleaseLevel.StartsWith("SQL"))
				{
					if (this._serverReleaseLevel.Length >= 8)
					{
						int i = 3;
						this._serverMajorVersion = (uint)((this._serverReleaseLevel[i] - '0') * '\n' + (this._serverReleaseLevel[i + 1] - '0'));
						this._serverSubVersion = (uint)((this._serverReleaseLevel[i + 2] - '0') * '\n' + (this._serverReleaseLevel[i + 3] - '0'));
						this._serverModification = (uint)(this._serverReleaseLevel[i + 4] - '0');
					}
				}
				else if (this._serverReleaseLevel.StartsWith("DB2 UDB "))
				{
					int i = 8;
					while (i < this._serverReleaseLevel.Length && this._serverReleaseLevel[i] != '.')
					{
						i++;
					}
					if (i < this._serverReleaseLevel.Length - 1 && uint.TryParse(this._serverReleaseLevel.Substring(8, i - 8), out this._serverMajorVersion))
					{
						uint.TryParse(this._serverReleaseLevel.Substring(i + 1, this._serverReleaseLevel.Length - i - 1), out this._serverSubVersion);
					}
				}
				else if (this._serverReleaseLevel.StartsWith("MSAS") && this._serverReleaseLevel.Length >= 8)
				{
					int i = 4;
					this._serverMajorVersion = (uint)((this._serverReleaseLevel[i] - '0') * '\n' + (this._serverReleaseLevel[i + 1] - '0'));
					this._serverSubVersion = (uint)(this._serverReleaseLevel[i + 2] - '0');
					this._serverModification = (uint)(this._serverReleaseLevel[i + 3] - '0');
				}
				break;
			case HostType.DB2:
			case HostType.Linux:
			case HostType.Unix:
			{
				if (this._serverReleaseLevel.StartsWith("IFX"))
				{
					this._flavor = DrdaFlavor.Informix;
				}
				else
				{
					this._flavor = DrdaFlavor.DB2;
				}
				int i = 0;
				while (i < this._serverReleaseLevel.Length && !char.IsDigit(this._serverReleaseLevel[i]))
				{
					i++;
				}
				if (i <= this._serverReleaseLevel.Length - 5)
				{
					this._serverMajorVersion = (uint)((this._serverReleaseLevel[i] - '0') * '\n' + (this._serverReleaseLevel[i + 1] - '0'));
					this._serverSubVersion = (uint)((this._serverReleaseLevel[i + 2] - '0') * '\n' + (this._serverReleaseLevel[i + 3] - '0'));
					this._serverModification = (uint)(this._serverReleaseLevel[i + 4] - '0');
				}
				break;
			}
			case HostType.IMSDB:
				this._flavor = DrdaFlavor.IMSDB;
				break;
			default:
				this._flavor = DrdaFlavor.Informix;
				break;
			}
			IL_047D:
			this._serverVersion = string.Format("{0}.{1}.{2}", this._serverMajorVersion, this._serverSubVersion, this._serverModification);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0011B8D0 File Offset: 0x00119AD0
		private List<string> GetRegisterSettings()
		{
			List<string> list = new List<string>();
			string text = this._connectInfo[25];
			if (!string.IsNullOrWhiteSpace(text))
			{
				list.Add(string.Format("SET CLIENT APPLNAME '{0}'", text.Trim()));
			}
			string text2 = this._connectInfo[40];
			if (!string.IsNullOrWhiteSpace(text2))
			{
				list.Add(string.Format("SET CLIENT USERID '{0}'", text2.Trim()));
			}
			string text3 = this._connectInfo[38];
			if (!string.IsNullOrWhiteSpace(text3))
			{
				list.Add(string.Format("SET CLIENT WRKSTNNAME '{0}'", text3.Trim()));
			}
			string text4 = this._connectInfo[39];
			if (!string.IsNullOrWhiteSpace(text4))
			{
				text4 = text4.Trim();
				list.Add(string.Format("SET CLIENT ACCTNG '{0}',X'{1:00}','{2}'", "SQL MSDRDACLIENT                                      ", text4.Length, text4));
			}
			string text5 = this._connectInfo[55];
			if (!string.IsNullOrWhiteSpace(text5))
			{
				string[] array = text5.Split(Requester._splitChars, StringSplitOptions.RemoveEmptyEntries);
				list.AddRange(array);
			}
			return list;
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0011B9C7 File Offset: 0x00119BC7
		private void UpdateCcsid(Ccsid ccsid)
		{
			if (ccsid == null)
			{
				return;
			}
			ccsid._ccsiddbc = (int)Utility.MapCcsidCodeToCodePage((ushort)ccsid._ccsiddbc);
			ccsid._ccsidmbc = (int)Utility.MapCcsidCodeToCodePage((ushort)ccsid._ccsidmbc);
			ccsid._ccsidsbc = (int)Utility.MapCcsidCodeToCodePage((ushort)ccsid._ccsidsbc);
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0011BA04 File Offset: 0x00119C04
		internal async Task Enter(bool isAsync, CancellationToken cancellationToken)
		{
			if (this.IsDuw)
			{
				bool success = true;
				XaManager xaManager = (XaManager)this._transactionManager;
				if (isAsync)
				{
					await this._accessLock.WaitAsync(cancellationToken);
					if (xaManager.IsInTransaction)
					{
						success = await xaManager.XaLock.WaitAsync(this.Timeout, cancellationToken);
					}
					else
					{
						await xaManager.XaLock.WaitAsync(cancellationToken);
					}
				}
				else
				{
					this._accessLock.Wait();
					if (xaManager.IsInTransaction)
					{
						success = xaManager.XaLock.Wait(this.Timeout);
					}
					else
					{
						xaManager.XaLock.Wait();
					}
				}
				if (!success)
				{
					xaManager.Abort();
					this._transactionManager = new XaManager(this);
					this._accessLock.Release();
					throw this.MakeException(RequesterResource.DtcTimeout, "HY000", -343);
				}
				this._accessLock.Release();
				xaManager = null;
			}
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0011BA5C File Offset: 0x00119C5C
		internal void Enter()
		{
			if (this.IsDuw)
			{
				this.Enter(false, CancellationToken.None).GetAwaiter().GetResult();
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0011BA8A File Offset: 0x00119C8A
		internal void Leave()
		{
			if (this.IsDuw)
			{
				((XaManager)this._transactionManager).XaLock.Release();
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x0011BAAC File Offset: 0x00119CAC
		public bool IsUdb
		{
			get
			{
				return this._hostType == HostType.NT || this._hostType == HostType.RS6000 || this._hostType == HostType.AIX || this._hostType == HostType.HP || this._hostType == HostType.Sun || this._hostType == HostType.Linux || this._hostType == HostType.OS2;
			}
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0011BAFD File Offset: 0x00119CFD
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return this.GetHashCode() - obj.GetHashCode();
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06004AA3 RID: 19107 RVA: 0x0011BB11 File Offset: 0x00119D11
		public HostType HostType
		{
			get
			{
				return this._hostType;
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x0011BB1C File Offset: 0x00119D1C
		public string ServerClass
		{
			get
			{
				if (this._flavor == DrdaFlavor.DB2)
				{
					return this._serverClass;
				}
				if (this._flavor == DrdaFlavor.IMSDB)
				{
					return "IMSDB";
				}
				HostType hostType = this._hostType;
				if (hostType == HostType.NT)
				{
					return "IFX/NT";
				}
				if (hostType == HostType.Linux)
				{
					return "IFX/LINUX";
				}
				if (hostType != HostType.Unix)
				{
					return "IFX";
				}
				return "IFX/UNIX";
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06004AA5 RID: 19109 RVA: 0x0011BB75 File Offset: 0x00119D75
		public string ServerVersion
		{
			get
			{
				return this._serverVersion;
			}
		}

		// Token: 0x17001226 RID: 4646
		public object this[RequesterProperties attribute]
		{
			get
			{
				if (attribute == RequesterProperties.AutoCommit)
				{
					return this._autoCommit;
				}
				return this._isolationLevel;
			}
			set
			{
				if (attribute == RequesterProperties.AutoCommit)
				{
					this._autoCommit = (bool)value;
					return;
				}
				this._isolationLevel = (SqlIsolationLevels)value;
			}
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x0011BBB8 File Offset: 0x00119DB8
		public ISqlStatement CreateStatement()
		{
			SqlStatement sqlStatement = new SqlStatement(this);
			this._listStatements.Add(sqlStatement);
			return sqlStatement;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x0011BBD9 File Offset: 0x00119DD9
		public void SetProviderName(string providerName)
		{
			this._providerName = providerName;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x0011BBE4 File Offset: 0x00119DE4
		public async Task ConnectAsync(X509Certificate clientCert, bool isAsync, CancellationToken cancellationToken)
		{
			List<string> settings = this.GetRegisterSettings();
			if (this._state != Requester.RequesterState.Accrdb || !this.registerSettings.SequenceEqual(settings) || this._clientCert != clientCert || !this._connectionManager.Connected)
			{
				ReconnectException reconnectEx = null;
				for (;;)
				{
					await this.Enter(isAsync, cancellationToken);
					try
					{
						if (this._state == Requester.RequesterState.Accrdb)
						{
							this.DropConnection();
							this.ResetRequesterStateForNewConnection();
						}
						else
						{
							this._ccsidHost = Utility.ParseCcsid(this._connectInfo[8]);
							this._autoCommit = Utility.ParseBoolean(this._connectInfo[34], true);
							this.ConnectionTimeout = int.Parse(this._connectInfo[15]);
						}
						this.IsIMSDB = Utility.ParseBoolean(this._connectInfo[63], true);
						this.IsDb2Gateway = Utility.ParseBoolean(this._connectInfo[56], true);
						if (!this.IsDb2Gateway)
						{
							this.IsDb2Gateway = Utility.CheckRegistryKey("DB2Gateway");
						}
						this.UseHIS2013Constants = Utility.ParseBoolean(this._connectInfo[58], true);
						if (!this.UseHIS2013Constants)
						{
							this.UseHIS2013Constants = Utility.CheckRegistryKey("UseHIS2013Constants");
						}
						this.IsConvertToBigEndian = Utility.ParseBoolean(this._connectInfo[57], true);
						this.XMLAsBinary = Utility.ParseBoolean(this._connectInfo[59], true);
						this.UseAccelerator = Utility.ParseBoolean(this._connectInfo[61], false);
						this.EncryptionAlgorithm = this._connectInfo[60];
						this._clientCert = clientCert;
						await this._connectionManager.ConnectAsync(clientCert, isAsync, cancellationToken);
						this._state = Requester.RequesterState.Opened;
						if (this.IsDb2Gateway || this.IsIMSDB)
						{
							await this._superVisor.SubmitExcsatAsync(isAsync, false, cancellationToken);
						}
						else
						{
							await this._superVisor.SubmitExcsatAsync(isAsync, true, cancellationToken);
						}
						this._state = Requester.RequesterState.Excsat;
						if (!this.IsDb2Gateway)
						{
							this._isUnicodeMgrSupported = this._superVisor.GetManagerLevel(ManagerCodePoint.UNICODEMGR) == 1208;
						}
						if (!this.IsDb2Gateway && !this.IsIMSDB)
						{
							this.SetHostType(this._superVisor.Srvclsnm);
							this.SetServerReleaseLevel(this._superVisor.Srvrlslv);
						}
						SecurityMechanism securityMechanism = SecurityMechanism.NotSupported;
						if (reconnectEx != null)
						{
							this._securityManager._algo = reconnectEx.Algorithm;
							securityMechanism = reconnectEx.Secmec;
							reconnectEx = null;
						}
						await this._securityManager.SubmitAccsecAsync(securityMechanism, isAsync, cancellationToken);
						this._state = Requester.RequesterState.Accsec;
						if (this.IsDb2Gateway)
						{
							this.SetHostType(this._superVisor.Srvclsnm);
							this.SetServerReleaseLevel(this._superVisor.Srvrlslv);
							this._isUnicodeMgrSupported = this._superVisor.GetManagerLevel(ManagerCodePoint.UNICODEMGR) == 1208;
							this._ccsidHost = Utility.ParseCcsid("1208");
						}
						else if (this.IsIMSDB)
						{
							this.SetHostType(this._superVisor.Srvclsnm);
							this.SetServerReleaseLevel(this._superVisor.Srvrlslv);
							this._ccsidHost = Utility.ParseCcsid("37");
						}
						if (this._isUnicodeMgrSupported)
						{
							Ccsid ccsid = new Ccsid();
							this._connectionManager.DdmReader.CcsidManager = UnicodeManager.Instance;
							this._connectionManager.DdmReader.Ccsid = ccsid;
							this._connectionManager.DdmWriter.CcsidManager = UnicodeManager.Instance;
							this._connectionManager.DdmWriter.Ccsid = ccsid;
						}
						if (this.IsDb2Gateway)
						{
							await this._sqlManager.SubmitSecchkAndAccrdbAsync(isAsync, cancellationToken);
							this._state = Requester.RequesterState.Accrdb;
						}
						else
						{
							await this._securityManager.SubmitSecchkAsync(isAsync, cancellationToken);
							this._state = Requester.RequesterState.Secchk;
							await this._sqlManager.SubmitAccrdbAsync(isAsync, cancellationToken);
							this._state = Requester.RequesterState.Accrdb;
						}
						if (!this.IsIMSDB)
						{
							this._sqlManager.Level = this._superVisor.GetManagerLevel(ManagerCodePoint.SQLAM);
						}
						if (this._hostType == HostType.AS400 || this._hostType == HostType.RS6000 || this._flavor == DrdaFlavor.Informix)
						{
							((TcpConnectionManager)this._connectionManager).Converter.IsAs400 = true;
						}
						if (this._flavor == DrdaFlavor.DB2)
						{
							((TcpConnectionManager)this._connectionManager).Converter.DateTimeMasks = Requester._db2DateTimeMasks;
						}
						else
						{
							((TcpConnectionManager)this._connectionManager).Converter.DateTimeMasks = Requester._informixDateTimeMasks;
						}
						if (!this._isUnicodeMgrSupported)
						{
							if (this._ccsidRead != null)
							{
								this._connectionManager.DdmReader.Ccsid = this._ccsidRead;
							}
							if (this._ccsidWrite != null)
							{
								this._connectionManager.DdmWriter.Ccsid = this._ccsidWrite;
							}
						}
						Requester.drdaArCounterTelemetry.Increment(DrdArCounterCollection.ServerClass, this.ServerClass);
						Requester.drdaArCounterTelemetry.Increment(DrdArCounterCollection.ServerLevel, this._serverReleaseLevel);
						if (Telemetry.Instance.TelemetryEnabled)
						{
							TelemetryEvent telemetryEvent = new TelemetryEvent(this.ServerClass, this._serverReleaseLevel, this._providerName, Requester._applicationName);
							Telemetry.Instance.TrackEvent("DrdaClient", telemetryEvent);
						}
						if (settings.Count > 0)
						{
							SqlStatement settingStatement = new SqlStatement(this);
							await settingStatement.InternalExecuteSetAsync(settings, isAsync, cancellationToken);
							await settingStatement.CloseAsync(false, isAsync, cancellationToken);
							settingStatement = null;
						}
						this.registerSettings = settings;
						if (!string.IsNullOrWhiteSpace(this._connectInfo[19]))
						{
							this.NeedSetDefaultQualifierSet = true;
						}
					}
					catch (ReconnectException reconnectEx)
					{
						this.DropConnection();
						this.ResetRequesterStateForNewConnection();
						continue;
					}
					finally
					{
						this.Leave();
					}
					break;
				}
			}
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0011BC44 File Offset: 0x00119E44
		public async Task Disconnect(bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				if (this._state == Requester.RequesterState.Closed)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
					{
						this._tracePoint.Trace(TraceFlags.Verbose, "Requester has already been closed.");
					}
				}
				else if (this._transactionManager is XaManager && ((XaManager)this._transactionManager).IsInTransaction)
				{
					if (this._tracePoint.IsEnabled(TraceFlags.Warning))
					{
						this._tracePoint.Trace(TraceFlags.Warning, "Requester is called to close but still in XA transaction.");
					}
					this.isWaitingForTransactionClose = true;
				}
				else
				{
					if (!this.HadException)
					{
						foreach (SqlStatement sqlStatement in this._listStatements)
						{
							await sqlStatement.CloseAsync(true, isAsync, cancellationToken);
						}
						List<SqlStatement>.Enumerator enumerator = default(List<SqlStatement>.Enumerator);
					}
					this._listStatements.Clear();
					if (this._pool != null)
					{
						bool flag = !this.HadException;
						if (flag)
						{
							flag = await this._transactionManager.CanReuseConnection(isAsync, cancellationToken);
						}
						if (flag)
						{
							this._pool.CheckIn(this);
							return;
						}
						this._pool.Remove(this);
					}
					this.DropConnection();
				}
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x0011BC9C File Offset: 0x00119E9C
		public async Task EnlistAsync(Transaction transaction, bool isAsync, CancellationToken cancellationToken)
		{
			await this.EnlistAsync(transaction, 15000, isAsync, cancellationToken);
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x0011BCFC File Offset: 0x00119EFC
		public async Task EnlistAsync(Transaction transaction, int timeOut, bool isAsync, CancellationToken cancellationToken)
		{
			this.Timeout = timeOut;
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._transactionManager.EnlistAsync(transaction, isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x0011BD64 File Offset: 0x00119F64
		public async Task CommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._transactionManager.CommitAsync(isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0011BDBC File Offset: 0x00119FBC
		public async Task RollbackAsync(bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._transactionManager.RollbackAsync(isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0011BE14 File Offset: 0x0011A014
		public async Task InterruptAsync(bool isAsync, CancellationToken cancellationToken)
		{
			byte[] token = this._rdbIntToken;
			IPEndPoint interruptEndpoint = this._interruptEndpoint;
			if (token == null)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Warning))
				{
					this._tracePoint.Trace(TraceFlags.Warning, "Requester can NOT send Interrupt request when interrupt token is not available.");
				}
			}
			else
			{
				string[] array = (string[])this._connectInfo.Clone();
				if (interruptEndpoint != null)
				{
					array[11] = interruptEndpoint.Port.ToString();
					array[10] = interruptEndpoint.Address.ToString();
				}
				Requester requester = RequesterFactory.Instance.GetRequester(array, this._traceContainer, this._exceptionMaker) as Requester;
				requester.ConnectionTimeout = int.Parse(array[15]);
				await requester._connectionManager.ConnectAsync(this._clientCert, isAsync, cancellationToken);
				await requester._superVisor.SubmitExcsatAsync(isAsync, true, cancellationToken);
				requester._sqlManager.Level = requester._superVisor.GetManagerLevel(ManagerCodePoint.SQLAM);
				await requester._sqlManager.SubmitIntrdbrqs(token, isAsync, cancellationToken);
			}
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x0011BE6C File Offset: 0x0011A06C
		public async Task BindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._packageManager.BindPackageAsync(package, options, null, 0, null, progress, isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0011BEDC File Offset: 0x0011A0DC
		public async Task DropPackageAsync(Package package, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._packageManager.DropPackageAsync(package, progress, isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0011BF44 File Offset: 0x0011A144
		public async Task CopyPackageAsync(Package package, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._packageManager.CopyPackageAsync(package, options, targetRdbName, targetCollectionId, progress, isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0011BFC8 File Offset: 0x0011A1C8
		public async Task RebindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._packageManager.RebindPackageAsync(package, options, progress, isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0011C038 File Offset: 0x0011A238
		public async Task SetupHostPackagesAsync(bool releaseCommit, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this.Enter(isAsync, cancellationToken);
			try
			{
				await this._packageManager.SetupHostPackagesAsync(releaseCommit, progress, isAsync, cancellationToken);
			}
			finally
			{
				this.Leave();
			}
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0011C09E File Offset: 0x0011A29E
		public void SetCustomPackageData(XmlReader packageData)
		{
			this._packageManager.BuildAliasMapping(packageData);
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0011C0AC File Offset: 0x0011A2AC
		internal void DisconnectIfWaitingForTransactionClose()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Trying to disconnect requester since it is waiting for XA transaction close...");
			}
			if (!this.isWaitingForTransactionClose)
			{
				return;
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Disconnecting requester...");
			}
			this.Disconnect(false, CancellationToken.None).GetAwaiter().GetResult();
			this.isWaitingForTransactionClose = false;
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Disconnected.");
			}
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0011C13F File Offset: 0x0011A33F
		internal void DropConnection()
		{
			this._connectionManager.Disconnect();
			this._state = Requester.RequesterState.Closed;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x0011C154 File Offset: 0x0011A354
		private void ResetRequesterStateForNewConnection()
		{
			this._connectionManager.Reset();
			this._superVisor.Reset();
			this._typeDefNam = "QTDSQLX86";
			this._rdbIntToken = null;
			this._interruptEndpoint = null;
			this._ccsidRead = null;
			this._ccsidWrite = null;
			this.isWaitingForTransactionClose = false;
		}

		// Token: 0x040038B0 RID: 14512
		private const int XA_Timeout_Value_In_Millisecond = 15000;

		// Token: 0x040038B1 RID: 14513
		private const string FeatureName = "DrdaClient";

		// Token: 0x040038B2 RID: 14514
		private const string NullId = "NULLID";

		// Token: 0x040038B3 RID: 14515
		private static string _applicationName = Process.GetCurrentProcess().ProcessName;

		// Token: 0x040038B4 RID: 14516
		private static FileVersionInfo _fileVersion = FileVersionInfo.GetVersionInfo(typeof(Requester).Assembly.Location);

		// Token: 0x040038B5 RID: 14517
		private static List<DateTimeMask> _db2DateTimeMasks = new List<DateTimeMask>();

		// Token: 0x040038B6 RID: 14518
		private static List<DateTimeMask> _informixDateTimeMasks = new List<DateTimeMask>();

		// Token: 0x040038B7 RID: 14519
		private static char[] _splitChars = new char[] { ';' };

		// Token: 0x040038B8 RID: 14520
		private SemaphoreSlim _accessLock = new SemaphoreSlim(1);

		// Token: 0x040038B9 RID: 14521
		private HostType _hostType;

		// Token: 0x040038BA RID: 14522
		private string _serverClass = string.Empty;

		// Token: 0x040038BB RID: 14523
		private string _serverVersion = string.Empty;

		// Token: 0x040038BC RID: 14524
		private string _serverReleaseLevel;

		// Token: 0x040038BD RID: 14525
		private uint _serverMajorVersion;

		// Token: 0x040038BE RID: 14526
		private uint _serverSubVersion;

		// Token: 0x040038BF RID: 14527
		private uint _serverModification;

		// Token: 0x040038C0 RID: 14528
		private string[] _connectInfo;

		// Token: 0x040038C1 RID: 14529
		private string _encodedConnectInfo;

		// Token: 0x040038C2 RID: 14530
		private string _connectionString;

		// Token: 0x040038C3 RID: 14531
		private bool _needPool;

		// Token: 0x040038C4 RID: 14532
		private RequesterPool _pool;

		// Token: 0x040038C5 RID: 14533
		private ApplicationRequesterTracePoint _tracePoint;

		// Token: 0x040038C6 RID: 14534
		private DrdaClientTraceContainer _traceContainer;

		// Token: 0x040038C7 RID: 14535
		private DrdaCommonTracePoint _commonTracePoint;

		// Token: 0x040038C8 RID: 14536
		private Func<string, string, int, int, Exception> _exceptionMaker;

		// Token: 0x040038C9 RID: 14537
		private Requester.RequesterState _state;

		// Token: 0x040038CA RID: 14538
		private byte[] _rdbIntToken;

		// Token: 0x040038CB RID: 14539
		private IPEndPoint _interruptEndpoint;

		// Token: 0x040038CC RID: 14540
		private string _typeDefNam = "QTDSQLX86";

		// Token: 0x040038CD RID: 14541
		private Ccsid _ccsidRead;

		// Token: 0x040038CE RID: 14542
		private Ccsid _ccsidWrite;

		// Token: 0x040038CF RID: 14543
		private Ccsid _ccsidHost;

		// Token: 0x040038D0 RID: 14544
		private EndianType _endian = EndianType.LittleEndian;

		// Token: 0x040038D1 RID: 14545
		private bool _isUnicodeMgrSupported;

		// Token: 0x040038D2 RID: 14546
		private bool _autoCommit = true;

		// Token: 0x040038D3 RID: 14547
		private SqlIsolationLevels _isolationLevel = SqlIsolationLevels.ReadCommitted;

		// Token: 0x040038D4 RID: 14548
		private DrdaFlavor _flavor;

		// Token: 0x040038D5 RID: 14549
		private List<SqlStatement> _listStatements = new List<SqlStatement>();

		// Token: 0x040038D6 RID: 14550
		private X509Certificate _clientCert;

		// Token: 0x040038D7 RID: 14551
		private string _providerName = "DrdaClient";

		// Token: 0x040038D8 RID: 14552
		private List<string> registerSettings;

		// Token: 0x040038D9 RID: 14553
		private bool isWaitingForTransactionClose;

		// Token: 0x040038DA RID: 14554
		private bool _isDb2Gateway;

		// Token: 0x040038DB RID: 14555
		private bool _useHIS2013Constants;

		// Token: 0x040038DC RID: 14556
		private bool _isConvertToBigEndian;

		// Token: 0x040038DD RID: 14557
		private bool _xmlAsBinary;

		// Token: 0x040038DE RID: 14558
		private string _retDatabaseName = "";

		// Token: 0x040038DF RID: 14559
		private string _encryptionAlgorithm = "";

		// Token: 0x040038E0 RID: 14560
		private bool _useAccelerator;

		// Token: 0x040038E1 RID: 14561
		private bool _isIMSDB;

		// Token: 0x040038E2 RID: 14562
		private ConnectionManager _connectionManager;

		// Token: 0x040038E3 RID: 14563
		private Supervisor _superVisor;

		// Token: 0x040038E4 RID: 14564
		private SecurityManager _securityManager;

		// Token: 0x040038E5 RID: 14565
		private SqlManager _sqlManager;

		// Token: 0x040038E6 RID: 14566
		private TransactionManager _transactionManager;

		// Token: 0x040038E7 RID: 14567
		private LocalTransactionManager _localTransactionManager;

		// Token: 0x040038E8 RID: 14568
		private PackageManager _packageManager;

		// Token: 0x040038E9 RID: 14569
		public byte[][] ProgRef;

		// Token: 0x040038EA RID: 14570
		public List<int> LobLength;

		// Token: 0x040038EB RID: 14571
		public List<int> Mode;

		// Token: 0x040038EC RID: 14572
		private int _anyProgRef;

		// Token: 0x040038ED RID: 14573
		public bool BlobDataOverrun;

		// Token: 0x040038EE RID: 14574
		public string _typeDefNamOrig = "QTDSQLX86";

		// Token: 0x040038EF RID: 14575
		public static DrdaArCounterTelemetryContainer drdaArCounterTelemetry;

		// Token: 0x0200094E RID: 2382
		private class HostClassName
		{
			// Token: 0x040038F8 RID: 14584
			public const string AS400 = "QAS";

			// Token: 0x040038F9 RID: 14585
			public const string MVS = "QMVS";

			// Token: 0x040038FA RID: 14586
			public const string DB2 = "QDB2";

			// Token: 0x040038FB RID: 14587
			public const string OS2 = "QDB2/2 ";

			// Token: 0x040038FC RID: 14588
			public const string NT = "QDB2/NT";

			// Token: 0x040038FD RID: 14589
			public const string NT64 = "QDB2/NT64";

			// Token: 0x040038FE RID: 14590
			public const string RS6000 = "QDB2/6s000";

			// Token: 0x040038FF RID: 14591
			public const string RS6000PE = "QDB2/6000 PE";

			// Token: 0x04003900 RID: 14592
			public const string AIX64 = "QDB2/AIX64";

			// Token: 0x04003901 RID: 14593
			public const string LINUX = "QDB2/LINUX";

			// Token: 0x04003902 RID: 14594
			public const string LINUX390 = "QDB2/LINUX390";

			// Token: 0x04003903 RID: 14595
			public const string LINUXIA64 = "QDB2/LINUXIA64";

			// Token: 0x04003904 RID: 14596
			public const string LINUXPPC = "QDB2/LINUXPPC";

			// Token: 0x04003905 RID: 14597
			public const string LINUXPPC64 = "QDB2/LINUXPPC64";

			// Token: 0x04003906 RID: 14598
			public const string LINUXZ64 = "QDB2/LINUXZ64";

			// Token: 0x04003907 RID: 14599
			public const string LINUXPPC64LE = "QDB2/LINUXPPC64LE";

			// Token: 0x04003908 RID: 14600
			public const string SQLDSVM = "QSQLDS/VM";

			// Token: 0x04003909 RID: 14601
			public const string SQLDSVSE = "QSQLDS/VSE";

			// Token: 0x0400390A RID: 14602
			public const string SUN = "QDB2/SUN";

			// Token: 0x0400390B RID: 14603
			public const string SUN64 = "QDB2/SUN64";

			// Token: 0x0400390C RID: 14604
			public const string HP = "QDB2/HPUX";

			// Token: 0x0400390D RID: 14605
			public const string HP64 = "QDB2/HP64";

			// Token: 0x0400390E RID: 14606
			public const string HP_IA64 = "QDB2/HPUX-IA64";

			// Token: 0x0400390F RID: 14607
			public const string IDS_NT32 = "IDS/NT32";

			// Token: 0x04003910 RID: 14608
			public const string IDS_NT64 = "IDS/NT64";

			// Token: 0x04003911 RID: 14609
			public const string IDS_UNIX32 = "IDS/UNIX32";

			// Token: 0x04003912 RID: 14610
			public const string IDS_UNIX64 = "IDS/UNIX64";

			// Token: 0x04003913 RID: 14611
			public const string QDB2_HPUX_IA = "QDB2/HPUX-IA";

			// Token: 0x04003914 RID: 14612
			public const string QDB2_HPUX_IA64 = "QDB2/HPUX-IA64";

			// Token: 0x04003915 RID: 14613
			public const string QDB2_LINUXX8664 = "QDB2/LINUXX8664";

			// Token: 0x04003916 RID: 14614
			public const string QDB2_SCO = "QDB2/SCO";

			// Token: 0x04003917 RID: 14615
			public const string QDB2_SGI = "QDB2/SGI";

			// Token: 0x04003918 RID: 14616
			public const string QDB2_SNI = "QDB2/SNI";

			// Token: 0x04003919 RID: 14617
			public const string QDB2_Windows95 = "QDB2/Windows 95";

			// Token: 0x0400391A RID: 14618
			public const string QDB2_SUNX86 = "QDB2/SUNX86";

			// Token: 0x0400391B RID: 14619
			public const string QDB2_SUNX8664 = "QDB2/SUNX8664";

			// Token: 0x0400391C RID: 14620
			public const string MSDRDA_AS = "MSAS";

			// Token: 0x0400391D RID: 14621
			public const string IMSDB = "DFS";
		}

		// Token: 0x0200094F RID: 2383
		internal enum RequesterState
		{
			// Token: 0x0400391F RID: 14623
			Initialized,
			// Token: 0x04003920 RID: 14624
			Opened,
			// Token: 0x04003921 RID: 14625
			Excsat,
			// Token: 0x04003922 RID: 14626
			Accsec,
			// Token: 0x04003923 RID: 14627
			Secchk,
			// Token: 0x04003924 RID: 14628
			Accrdb,
			// Token: 0x04003925 RID: 14629
			Closed
		}
	}
}
