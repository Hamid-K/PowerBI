using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000057 RID: 87
	public sealed class AdomdConnection : Component, IDbConnection, IDisposable, ICloneable, IConnectivityOwner
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00020666 File Offset: 0x0001E866
		public AdomdConnection()
		{
			this.xmlaClientProvider = new AdomdConnection.XmlaClientProvider(this);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00020686 File Offset: 0x0001E886
		public AdomdConnection(string connectionString)
			: this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00020695 File Offset: 0x0001E895
		public AdomdConnection(AdomdConnection connection)
		{
			this.xmlaClientProvider = new AdomdConnection.XmlaClientProvider(this, connection.xmlaClientProvider);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000206BB File Offset: 0x0001E8BB
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000206C8 File Offset: 0x0001E8C8
		public string SessionID
		{
			get
			{
				return this.XmlaClientProviderEx.SessionID;
			}
			set
			{
				if (ConnectionState.Open == this.State)
				{
					throw new InvalidOperationException(SR.Connection_SessionID_SessionIsAlreadyOpen);
				}
				this.XmlaClientProviderEx.SessionID = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000206EA File Offset: 0x0001E8EA
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x000206F7 File Offset: 0x0001E8F7
		public bool ShowHiddenObjects
		{
			get
			{
				return this.XmlaClientProviderEx.ShowHiddenObjects;
			}
			set
			{
				if (this.State == ConnectionState.Open)
				{
					throw new InvalidOperationException(SR.Connection_ShowHiddenObjects_ConnectionAlreadyOpen);
				}
				this.XmlaClientProviderEx.ShowHiddenObjects = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00020719 File Offset: 0x0001E919
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00020721 File Offset: 0x0001E921
		public AccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
			set
			{
				AccessToken.ValidateTokenInput(value);
				if (this.XmlaClientProviderEx.ConnectionInfo != null && this.XmlaClientProviderEx.ConnectionInfo.Password != null)
				{
					throw new ArgumentException(RuntimeSR.NonRefreshableToken_AlreadyPresented);
				}
				this.accessToken = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0002075A File Offset: 0x0001E95A
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00020762 File Offset: 0x0001E962
		public Func<AccessToken, AccessToken> OnAccessTokenExpired { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0002076B File Offset: 0x0001E96B
		[Browsable(false)]
		public CubeCollection Cubes
		{
			get
			{
				AdomdUtils.CheckConnectionOpened(this);
				if (this.cubes == null)
				{
					this.cubes = new CubeCollection(this);
				}
				else
				{
					this.cubes.CollectionInternal.CheckCache();
				}
				return this.cubes;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0002079F File Offset: 0x0001E99F
		[Browsable(false)]
		public MiningModelCollection MiningModels
		{
			get
			{
				AdomdUtils.CheckConnectionOpened(this);
				if (this.miningModels == null)
				{
					this.miningModels = new MiningModelCollection(this);
				}
				else
				{
					this.miningModels.CollectionInternal.CheckCache();
				}
				return this.miningModels;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000207D3 File Offset: 0x0001E9D3
		[Browsable(false)]
		public MiningStructureCollection MiningStructures
		{
			get
			{
				AdomdUtils.CheckConnectionOpened(this);
				if (this.miningStructures == null)
				{
					this.miningStructures = new MiningStructureCollection(this);
				}
				else
				{
					this.miningStructures.CollectionInternal.CheckCache();
				}
				return this.miningStructures;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00020807 File Offset: 0x0001EA07
		[Browsable(false)]
		public MiningServiceCollection MiningServices
		{
			get
			{
				AdomdUtils.CheckConnectionOpened(this);
				if (this.miningServices == null)
				{
					this.miningServices = new MiningServiceCollection(this);
				}
				else
				{
					this.miningServices.CollectionInternal.CheckCache();
				}
				return this.miningServices;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002083B File Offset: 0x0001EA3B
		public AdomdTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00020848 File Offset: 0x0001EA48
		public AdomdTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			AdomdUtils.CheckConnectionOpened(this);
			if (isolationLevel == IsolationLevel.ReadCommitted)
			{
				return new AdomdTransaction(this);
			}
			throw new NotSupportedException();
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00020864 File Offset: 0x0001EA64
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00020874 File Offset: 0x0001EA74
		public string ConnectionString
		{
			get
			{
				return this.XmlaClientProviderEx.ConnectionString;
			}
			set
			{
				if (this.State == ConnectionState.Open)
				{
					throw new InvalidOperationException(SR.Server_IsAlreadyConnected);
				}
				this.XmlaClientProviderEx.ConnectionString = value;
				if (this.XmlaClientProviderEx.ConnectionInfo != null)
				{
					this.properties.IsReadOnly = false;
					foreach (object obj in this.XmlaClientProviderEx.ConnectionInfo.OriginalConnStringProps)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						this.properties.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
					this.properties.IsReadOnly = true;
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00020934 File Offset: 0x0001EB34
		[Browsable(false)]
		public int ConnectionTimeout
		{
			get
			{
				return this.XmlaClientProviderEx.ConnectionTimeout;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00020941 File Offset: 0x0001EB41
		[Browsable(false)]
		public string Database
		{
			get
			{
				if (this.State != ConnectionState.Open)
				{
					return this.XmlaClientProviderEx.GetXmlaProperty("Catalog");
				}
				string empty = string.Empty;
				return this.GetProperty("Catalog");
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00020970 File Offset: 0x0001EB70
		public void ChangeEffectiveUser(string effectiveUserName)
		{
			if (string.IsNullOrEmpty(effectiveUserName))
			{
				throw new ArgumentException(SR.Connection_EffectiveUserNameEmpty);
			}
			AdomdUtils.CheckConnectionOpened(this);
			this.ResetInternalState();
			this.CloseOpenedReader();
			try
			{
				this.XmlaClientProviderEx.EndSession();
				this.XmlaClientProviderEx.SetXmlaProperty("EffectiveUserName", effectiveUserName);
				this.XmlaClientProviderEx.CreateSession(false);
			}
			catch (AdomdException)
			{
				if (this.XmlaClientProviderEx.IsXmlaClientConnected)
				{
					this.XmlaClientProviderEx.Disconnect(true);
				}
				throw;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000209F8 File Offset: 0x0001EBF8
		public void ChangeDatabase(string database)
		{
			if (database == null || database.Trim().Length == 0)
			{
				throw new ArgumentException(SR.Connection_DatabaseNameEmpty);
			}
			AdomdUtils.CheckConnectionOpened(this);
			if (this.IsPostYukonProvider())
			{
				this.SetProperty("Catalog", database);
			}
			else
			{
				string database2 = this.Database;
				try
				{
					if (database2 != database)
					{
						if (this.XmlaClientProviderEx.SessionID != null && !this.XmlaClientProviderEx.IsExternalSession)
						{
							this.XmlaClientProviderEx.EndSession();
						}
						else
						{
							this.XmlaClientProviderEx.SessionID = null;
						}
						this.XmlaClientProviderEx.SetXmlaProperty("Catalog", database);
						this.XmlaClientProviderEx.CreateSession(false);
					}
				}
				catch (AdomdException)
				{
					if (database2 == null || database2.Length != 0)
					{
						this.XmlaClientProviderEx.SetXmlaProperty("Catalog", database2);
					}
					if (this.XmlaClientProviderEx.IsXmlaClientConnected)
					{
						if (this.XmlaClientProviderEx.IsExternalSession)
						{
							this.XmlaClientProviderEx.ResetSession();
						}
						else
						{
							this.XmlaClientProviderEx.CreateSession(false);
						}
					}
					throw;
				}
			}
			this.ResetInternalState();
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00020B0C File Offset: 0x0001ED0C
		[Browsable(false)]
		public ConnectionState State
		{
			get
			{
				if (this.XmlaClientProviderEx == null || !this.XmlaClientProviderEx.IsXmlaClientConnected)
				{
					return ConnectionState.Closed;
				}
				return ConnectionState.Open;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00020B26 File Offset: 0x0001ED26
		public void Close()
		{
			this.Close(true);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00020B30 File Offset: 0x0001ED30
		public void Close(bool endSession)
		{
			ConnectionState state = this.State;
			if (state != ConnectionState.Closed)
			{
				if (state == ConnectionState.Open)
				{
					this.CloseOpenedReader();
					this.XmlaClientProviderEx.Disconnect(endSession);
				}
			}
			else if (this.XmlaClientProviderEx != null)
			{
				this.XmlaClientProviderEx.Disconnect(false);
			}
			this.userOpened = false;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00020B7C File Offset: 0x0001ED7C
		public AdomdCommand CreateCommand()
		{
			return new AdomdCommand
			{
				Connection = this
			};
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00020B8C File Offset: 0x0001ED8C
		public void Open()
		{
			if (this.State == ConnectionState.Open)
			{
				throw new InvalidOperationException(SR.Server_IsAlreadyConnected);
			}
			if (this.XmlaClientProviderEx.ConnectionString == null)
			{
				throw new InvalidOperationException(SR.Connection_ConnectionString_NotInitialized);
			}
			if (this.XmlaClientProviderEx.ConnectionType == ConnectionType.LocalServer)
			{
				throw new InvalidOperationException(SR.Connection_ConnectionToLocalServerNotSupported);
			}
			this.cachedActionsDataSet = null;
			string serverName = this.XmlaClientProviderEx.ServerName;
			if (string.IsNullOrEmpty(serverName))
			{
				throw new InvalidOperationException(SR.Server_NoServerName);
			}
			bool flag = !this.XmlaClientProviderEx.IsExternalSession;
			bool flag2 = ConnectivityHelper.IsHttpConnection(serverName);
			try
			{
				if (ConnectionInfo.IsBism(serverName))
				{
					try
					{
						this.ConnectToXMLA(flag, flag2);
						goto IL_00C1;
					}
					catch (AdomdConnectionException ex)
					{
						if (ex.InnerException is IOException)
						{
							this.XmlaClientProviderEx.UseEU = true;
							this.ConnectToXMLA(flag, flag2);
							goto IL_00C1;
						}
						throw;
					}
				}
				this.ConnectToXMLA(flag, flag2);
				IL_00C1:;
			}
			catch (AdomdException)
			{
				if (this.XmlaClientProviderEx.IsXmlaClientConnected)
				{
					this.XmlaClientProviderEx.Disconnect(flag);
				}
				throw;
			}
			this.ResetInternalState();
			this.XmlaClientProviderEx.MarkConnectionStringRestricted();
			this.userOpened = true;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00020CAC File Offset: 0x0001EEAC
		[Browsable(false)]
		public string ProviderVersion
		{
			get
			{
				if (this.providerVersion == null)
				{
					AdomdUtils.CheckConnectionOpened(this);
					this.providerVersion = this.XmlaClientProviderEx.ProviderVersion;
				}
				return this.providerVersion;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00020CD3 File Offset: 0x0001EED3
		[Browsable(false)]
		public string ServerVersion
		{
			get
			{
				if (this.serverVersion == null)
				{
					AdomdUtils.CheckConnectionOpened(this);
					this.serverVersion = this.XmlaClientProviderEx.ServerVersion;
				}
				return this.serverVersion;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public string ClientVersion
		{
			get
			{
				if (this.clientVersion == null)
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
					this.clientVersion = versionInfo.FileVersion;
				}
				return this.clientVersion;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00020D33 File Offset: 0x0001EF33
		public AdomdPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00020D3C File Offset: 0x0001EF3C
		public DataSet GetSchemaDataSet(Guid schema, object[] restrictions, bool throwOnInlineErrors)
		{
			AdomdUtils.CheckConnectionOpened(this);
			if (this.schemasInfos == null)
			{
				this.RetrieveSchemaRowsets(false);
			}
			AdomdConnection.XmlaMDSchema schemaInfo = this.schemasInfos.GetSchemaInfo(schema);
			ListDictionary listDictionary = new ListDictionary();
			if (restrictions != null)
			{
				AdomdConnection.XmlaMDSchemas.ConvertOleDbRestrictionsToXmlA(schemaInfo, restrictions, listDictionary);
			}
			bool flag = false;
			DataSet dataSet = this.CheckOnActionsAndSafety(schemaInfo.SchemaName, null, ref flag, listDictionary);
			if (dataSet == null)
			{
				dataSet = this.GetSchemaDataSet(schemaInfo.SchemaName, null, listDictionary, throwOnInlineErrors, null);
				if (flag)
				{
					this.FilterActionsOnSafety(dataSet);
				}
				return dataSet;
			}
			return dataSet;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00020DB2 File Offset: 0x0001EFB2
		public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
		{
			return this.GetSchemaDataSet(schemaName, null, restrictions, throwOnInlineErrors);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00020DBE File Offset: 0x0001EFBE
		public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
		{
			return this.GetSchemaDataSet(schemaName, schemaNamespace, restrictions, throwOnInlineErrors, null);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00020DCC File Offset: 0x0001EFCC
		public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors, AdomdPropertyCollection requestProperties)
		{
			if (string.IsNullOrEmpty(schemaName))
			{
				throw new ArgumentNullException("schemaName");
			}
			AdomdUtils.CheckConnectionOpened(this);
			bool flag = false;
			DataSet dataSet;
			if (restrictions == null)
			{
				dataSet = this.CheckOnActionsAndSafety(schemaName, schemaNamespace, ref flag, null);
			}
			else
			{
				dataSet = this.CheckOnActionsAndSafety(schemaName, schemaNamespace, ref flag, restrictions.InternalCollection);
			}
			if (dataSet == null)
			{
				AdomdRestrictionCollectionInternal adomdRestrictionCollectionInternal = ((restrictions == null) ? null : restrictions.InternalCollection);
				AdomdPropertyCollectionInternal adomdPropertyCollectionInternal = ((requestProperties == null) ? null : requestProperties.InternalCollection);
				dataSet = this.GetSchemaDataSet(schemaName, schemaNamespace, adomdRestrictionCollectionInternal, throwOnInlineErrors, adomdPropertyCollectionInternal);
				if (flag)
				{
					this.FilterActionsOnSafety(dataSet);
				}
				return dataSet;
			}
			return dataSet;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00020E51 File Offset: 0x0001F051
		public DataSet GetSchemaDataSet(Guid schema, object[] restrictions)
		{
			return this.GetSchemaDataSet(schema, restrictions, true);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00020E5C File Offset: 0x0001F05C
		public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions)
		{
			return this.GetSchemaDataSet(schemaName, null, restrictions, true);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00020E68 File Offset: 0x0001F068
		public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions)
		{
			return this.GetSchemaDataSet(schemaName, schemaNamespace, restrictions, true);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00020E74 File Offset: 0x0001F074
		public bool IsCloudAnalysisServices()
		{
			if (this.XmlaClientProviderEx.ConnectionInfo == null)
			{
				throw new InvalidOperationException(SR.Connection_ConnectionString_NotInitialized);
			}
			return this.XmlaClientProviderEx.ConnectionInfo.IsAsAzure || this.XmlaClientProviderEx.ConnectionInfo.IsPbiPremiumXmlaEp;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00020EB2 File Offset: 0x0001F0B2
		public CloudConnectionAuthenticationProperties GetCloudConnectionAuthenticationProperties()
		{
			if (!this.IsCloudAnalysisServices())
			{
				return null;
			}
			return this.XmlaClientProviderEx.ConnectionInfo.GetCloudConnectionAuthenticationPropertiesForPaaSInfrastructure(this);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00020ECF File Offset: 0x0001F0CF
		public void RefreshMetadata()
		{
			this.ClearMetadataCache();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00020ED7 File Offset: 0x0001F0D7
		public AdomdConnection Clone()
		{
			return new AdomdConnection(this);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00020EE0 File Offset: 0x0001F0E0
		void IConnectivityOwner.RefreshAccessToken()
		{
			bool flag = false;
			AccessToken accessToken;
			try
			{
				flag = AccessToken.TryRefreshToken(this.accessToken, this.OnAccessTokenExpired, out accessToken);
			}
			catch (Exception ex)
			{
				throw new AdomdConnectionException(RuntimeSR.TokenRefreshFailure, ex);
			}
			if (!flag)
			{
				throw new AdomdConnectionException(RuntimeSR.TokenRefreshFailure, null);
			}
			this.accessToken = accessToken;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00020F40 File Offset: 0x0001F140
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00020F48 File Offset: 0x0001F148
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginTransaction();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00020F50 File Offset: 0x0001F150
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginTransaction(isolationLevel);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00020F59 File Offset: 0x0001F159
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17000168 RID: 360
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00020F61 File Offset: 0x0001F161
		internal object OpenedReader
		{
			set
			{
				this.openedReader = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00020F6A File Offset: 0x0001F16A
		internal bool UserOpened
		{
			get
			{
				return this.userOpened;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00020F72 File Offset: 0x0001F172
		internal IDiscoverProvider IDiscoverProvider
		{
			get
			{
				return this.xmlaClientProvider;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00020F7A File Offset: 0x0001F17A
		internal IExecuteProvider IExecuteProvider
		{
			get
			{
				return this.xmlaClientProvider;
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00020F82 File Offset: 0x0001F182
		internal bool IsPostYukonProvider()
		{
			return this.XmlaClientProviderEx.IsPostYukonProvider();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00020F8F File Offset: 0x0001F18F
		internal object GetObjectData(SchemaObjectType schemaObjectType, string cubeName, string uniqueName)
		{
			return this.Cubes[cubeName].InternalGetSchemaObject(schemaObjectType, uniqueName);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		internal static void CancelCommand(AdomdConnection originalConnection)
		{
			AdomdConnection.IXmlaClientProviderEx xmlaClientProviderEx = new AdomdConnection.XmlaClientProvider(originalConnection, originalConnection.xmlaClientProvider);
			xmlaClientProviderEx.Connect();
			try
			{
				xmlaClientProviderEx.CancelCommand(originalConnection.SessionID);
			}
			finally
			{
				xmlaClientProviderEx.Disconnect(false);
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00020FEC File Offset: 0x0001F1EC
		private string GetProperty(string propName)
		{
			return this.GetProperty(propName, false);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00020FF6 File Offset: 0x0001F1F6
		private string GetProperty(string propName, bool sendNSCompatibility)
		{
			return this.XmlaClientProviderEx.GetPropertyFromServer(propName, sendNSCompatibility);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00021005 File Offset: 0x0001F205
		internal string CatalogConnectionStringProperty
		{
			get
			{
				return this.XmlaClientProviderEx.GetXmlaProperty("Catalog");
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00021018 File Offset: 0x0001F218
		internal bool HasAutoSyncTimeElapsed(DateTime origTime, DateTime nowTime)
		{
			return this.AutoSyncPeriod > 0U && (nowTime - origTime).TotalMilliseconds > this.AutoSyncPeriod;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002104C File Offset: 0x0001F24C
		internal void MarkCacheNeedsCheckForValidness()
		{
			if (this.cubes != null)
			{
				this.cubes.CollectionInternal.MarkCacheAsNeedCheckForValidness();
			}
			if (this.miningModels != null)
			{
				this.miningModels.CollectionInternal.MarkCacheAsNeedCheckForValidness();
			}
			if (this.miningServices != null)
			{
				this.miningServices.CollectionInternal.MarkCacheAsNeedCheckForValidness();
			}
			if (this.miningStructures != null)
			{
				this.miningStructures.CollectionInternal.MarkCacheAsNeedCheckForValidness();
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000210BC File Offset: 0x0001F2BC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					try
					{
						this.Close();
					}
					catch (AdomdException)
					{
					}
					this.xmlaClientProvider = null;
					this.userOpened = false;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002110C File Offset: 0x0001F30C
		private void ConnectToXMLA(bool createSession, bool isHTTP)
		{
			try
			{
				this.XmlaClientProviderEx.ConnectXmla();
				if (isHTTP && !this.XmlaClientProviderEx.ConnectionInfo.IsPaaSInfrastructure && !this.XmlaClientProviderEx.ConnectionInfo.IsPbiDataset)
				{
					this.ReadDataSourceInfo();
				}
				if (createSession)
				{
					this.XmlaClientProviderEx.CreateSession(true);
				}
				else
				{
					this.GetProperty("Catalog", true);
				}
			}
			catch
			{
				if (this.XmlaClientProviderEx.IsXmlaClientConnected)
				{
					this.XmlaClientProviderEx.Disconnect(createSession);
				}
				throw;
			}
			finally
			{
				this.SetPropertiesForAfterConnectionOpen();
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000211B0 File Offset: 0x0001F3B0
		private void ReadDataSourceInfo()
		{
			new ListDictionary()["ProviderType"] = "<MDP/>";
			RowsetFormatter rowsetFormatter = this.XmlaClientProviderEx.Discover("DISCOVER_DATASOURCES", null, InlineErrorHandlingType.StoreInCell, false);
			DataRowCollection rows = rowsetFormatter.MainRowsetTable.Rows;
			if (rows.Count <= 0)
			{
				throw new AdomdUnknownResponseException(SR.Connection_NoInformationAboutDataSourcesReturned, "");
			}
			DataRow dataRow = rows[0];
			if (!rowsetFormatter.MainRowsetTable.Columns.Contains("DataSourceInfo"))
			{
				throw new AdomdUnknownResponseException(SR.Connection_NoInformationAboutDataSourcesReturned, "");
			}
			if (this.XmlaClientProviderEx.GetXmlaProperty("DataSourceInfo") == null)
			{
				string text = AdomdUtils.GetProperty(dataRow, "DataSourceInfo") as string;
				if (!string.IsNullOrEmpty(text))
				{
					this.XmlaClientProviderEx.SetXmlaProperty("DataSourceInfo", text);
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00021274 File Offset: 0x0001F474
		private void RetrieveSchemaRowsets(bool createSession)
		{
			RowsetFormatter rowsetFormatter;
			if (createSession)
			{
				rowsetFormatter = this.XmlaClientProviderEx.DiscoverWithCreateSession("DISCOVER_SCHEMA_ROWSETS", false);
			}
			else
			{
				rowsetFormatter = this.XmlaClientProviderEx.Discover("DISCOVER_SCHEMA_ROWSETS", null, InlineErrorHandlingType.StoreInCell, false);
			}
			if (rowsetFormatter.RowsetDataset.Tables.Count <= 1)
			{
				throw new AdomdUnknownResponseException(SR.Schema_UnexpectedResponseForSchema("DISCOVER_SCHEMA_ROWSETS"), "");
			}
			this.schemasInfos = new AdomdConnection.XmlaMDSchemas(rowsetFormatter);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000212E4 File Offset: 0x0001F4E4
		private DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, IDictionary adomdRestrictions, bool throwOnInlineErrors, IDictionary requestProperties)
		{
			DataSet rowsetDataset = this.XmlaClientProviderEx.Discover(schemaName, schemaNamespace, adomdRestrictions, throwOnInlineErrors ? InlineErrorHandlingType.Throw : InlineErrorHandlingType.StoreInErrorsCollection, requestProperties).RowsetDataset;
			AdomdConnection.XmlaMDSchemas.MungeMembersSchemaColumnNames(schemaName, schemaNamespace, rowsetDataset);
			return rowsetDataset;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00021318 File Offset: 0x0001F518
		private void SetProperty(string propertyName, string propValue)
		{
			if (propertyName == null || propertyName.Trim().Length == 0)
			{
				throw new ArgumentException(SR.Connection_PropertyNameEmpty, "propertyName");
			}
			string xmlaProperty = this.XmlaClientProviderEx.GetXmlaProperty(propertyName);
			this.XmlaClientProviderEx.SetXmlaProperty(propertyName, propValue);
			try
			{
				if (this.GetProperty(propertyName) != propValue)
				{
					throw new NotSupportedException(SR.Connection_FailedToSetProperty(propertyName, propValue));
				}
			}
			catch
			{
				this.XmlaClientProviderEx.SetXmlaProperty(propertyName, xmlaProperty);
				throw;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000213A0 File Offset: 0x0001F5A0
		private void SetPropertiesForAfterConnectionOpen()
		{
			if (this.XmlaClientProviderEx.ConnectionInfo.AuthHandle != null)
			{
				this.properties.IsReadOnly = false;
				this.XmlaClientProviderEx.ConnectionInfo.AuthHandle.AddUserRelatedProperties(this.properties);
				this.properties.IsReadOnly = true;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000213F2 File Offset: 0x0001F5F2
		private uint AutoSyncPeriod
		{
			get
			{
				return this.XmlaClientProviderEx.AutoSyncPeriod;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00021400 File Offset: 0x0001F600
		private static bool AddressHasExtension(string serverName)
		{
			bool flag = false;
			try
			{
				string extension = Path.GetExtension(new Uri(serverName).LocalPath);
				flag = extension != null && extension.Length > 0;
			}
			catch (ArgumentNullException)
			{
				flag = false;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			catch (UriFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0002146C File Offset: 0x0001F66C
		private AdomdConnection.IXmlaClientProviderEx XmlaClientProviderEx
		{
			get
			{
				return this.xmlaClientProvider;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00021474 File Offset: 0x0001F674
		private void CloseOpenedReader()
		{
			if (this.openedReader != null)
			{
				if (this.openedReader is AdomdDataReader)
				{
					(this.openedReader as AdomdDataReader).Close();
				}
				else if (this.openedReader is XmlReader)
				{
					(this.openedReader as XmlReader).Close();
				}
			}
			this.openedReader = null;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000214CC File Offset: 0x0001F6CC
		private void ResetInternalState()
		{
			this.ClearMetadataCache();
			this.providerVersion = null;
			this.serverVersion = null;
			this.XmlaClientProviderEx.ResetInternalState();
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000214F0 File Offset: 0x0001F6F0
		private void ClearMetadataCache()
		{
			if (this.cubes != null)
			{
				this.cubes.CollectionInternal.AbandonCache();
				this.cubes = null;
			}
			if (this.miningModels != null)
			{
				this.miningModels.CollectionInternal.AbandonCache();
				this.miningModels = null;
			}
			if (this.miningServices != null)
			{
				this.miningServices.CollectionInternal.AbandonCache();
				this.miningServices = null;
			}
			if (this.miningStructures != null)
			{
				this.miningStructures.CollectionInternal.AbandonCache();
				this.miningStructures = null;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002157C File Offset: 0x0001F77C
		private DataSet CheckOnActionsAndSafety(string schemaName, string schemaNamespace, ref bool restrictActionsOnSafe, IDictionary restrictions)
		{
			if (this.XmlaClientProviderEx.SafetyOptions != SafetyOptions.All && schemaName == "MDSCHEMA_ACTIONS" && (schemaNamespace == null || schemaNamespace.Length == 0))
			{
				if (this.XmlaClientProviderEx.SafetyOptions == SafetyOptions.None)
				{
					return this.GetEmptyActionsDataSet(restrictions);
				}
				restrictActionsOnSafe = true;
			}
			else
			{
				restrictActionsOnSafe = false;
			}
			return null;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000215D0 File Offset: 0x0001F7D0
		private void FilterActionsOnSafety(DataSet dataSet)
		{
			if (dataSet.Tables.Count != 1)
			{
				throw new AdomdUnknownResponseException(SR.Schema_UnexpectedResponseForSchema("MDSCHEMA_ACTIONS"), "");
			}
			DataTable dataTable = dataSet.Tables[0];
			string empty = string.Empty;
			int i = 0;
			while (i < dataTable.Rows.Count)
			{
				string text = dataTable.Rows[i].GetColumnError("ACTION_TYPE");
				if (!string.IsNullOrEmpty(text))
				{
					throw new AdomdErrorResponseException(text);
				}
				int num = (int)dataTable.Rows[i]["ACTION_TYPE"];
				if (num == 2 || num == 32)
				{
					dataTable.Rows.RemoveAt(i);
				}
				else
				{
					if (num == 1)
					{
						text = dataTable.Rows[i].GetColumnError("CONTENT");
						if (!string.IsNullOrEmpty(text))
						{
							throw new AdomdErrorResponseException(text);
						}
						if (!ConnectivityHelper.IsHttpConnection(dataTable.Rows[i]["CONTENT"] as string))
						{
							dataTable.Rows.RemoveAt(i);
							continue;
						}
					}
					i++;
				}
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000216E4 File Offset: 0x0001F8E4
		private DataSet GetEmptyActionsDataSet(IDictionary restrictions)
		{
			if (this.cachedActionsDataSet == null)
			{
				RowsetFormatter rowsetFormatter = this.XmlaClientProviderEx.DiscoverSchema("MDSCHEMA_ACTIONS", restrictions, InlineErrorHandlingType.Throw);
				this.cachedActionsDataSet = rowsetFormatter.RowsetDataset;
			}
			return this.cachedActionsDataSet.Clone();
		}

		// Token: 0x04000412 RID: 1042
		private CubeCollection cubes;

		// Token: 0x04000413 RID: 1043
		private AdomdConnection.XmlaClientProvider xmlaClientProvider;

		// Token: 0x04000414 RID: 1044
		private AccessToken accessToken;

		// Token: 0x04000415 RID: 1045
		private MiningModelCollection miningModels;

		// Token: 0x04000416 RID: 1046
		private MiningStructureCollection miningStructures;

		// Token: 0x04000417 RID: 1047
		private MiningServiceCollection miningServices;

		// Token: 0x04000418 RID: 1048
		private string providerVersion;

		// Token: 0x04000419 RID: 1049
		private string serverVersion;

		// Token: 0x0400041A RID: 1050
		private AdomdConnection.XmlaMDSchemas schemasInfos;

		// Token: 0x0400041B RID: 1051
		private DataSet cachedActionsDataSet;

		// Token: 0x0400041C RID: 1052
		private string clientVersion;

		// Token: 0x0400041D RID: 1053
		private AdomdPropertyCollection properties = new AdomdPropertyCollection(true);

		// Token: 0x0400041E RID: 1054
		private const string propertyValueColumn = "Value";

		// Token: 0x0400041F RID: 1055
		private const string catalogPropertyName = "Catalog";

		// Token: 0x04000420 RID: 1056
		private const string providerType = "ProviderType";

		// Token: 0x04000421 RID: 1057
		private const string discoverDatasources = "DISCOVER_DATASOURCES";

		// Token: 0x04000422 RID: 1058
		private const string dataSourceInfo = "DataSourceInfo";

		// Token: 0x04000423 RID: 1059
		private const string discoverSchemaRowsets = "DISCOVER_SCHEMA_ROWSETS";

		// Token: 0x04000424 RID: 1060
		private const string schemaNameProp = "SchemaName";

		// Token: 0x04000425 RID: 1061
		private const string schemaGuidProp = "SchemaGuid";

		// Token: 0x04000426 RID: 1062
		private const string restrictionsProp = "Restrictions";

		// Token: 0x04000427 RID: 1063
		private const string nameProp = "Name";

		// Token: 0x04000428 RID: 1064
		private const string typeProp = "Type";

		// Token: 0x04000429 RID: 1065
		private const string actionsSchemaRowsetName = "MDSCHEMA_ACTIONS";

		// Token: 0x0400042A RID: 1066
		private const string actionsTypeRestrictionName = "ACTION_TYPE";

		// Token: 0x0400042B RID: 1067
		private const string actionsContentName = "CONTENT";

		// Token: 0x0400042C RID: 1068
		private const string showHiddenCubesPropery = "ShowHiddenCubes";

		// Token: 0x0400042D RID: 1069
		private const int MDACTION_TYPE_URL = 1;

		// Token: 0x0400042E RID: 1070
		private const int MDACTION_TYPE_HTML = 2;

		// Token: 0x0400042F RID: 1071
		private const int MDACTION_TYPE_COMMANDLINE = 32;

		// Token: 0x04000430 RID: 1072
		private const string multidimentionalProviderRestriction = "<MDP/>";

		// Token: 0x04000431 RID: 1073
		private object openedReader;

		// Token: 0x04000432 RID: 1074
		private bool userOpened;

		// Token: 0x0200019F RID: 415
		private interface IXmlaClientProviderEx
		{
			// Token: 0x06001278 RID: 4728
			RowsetFormatter Discover(string requestType, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling, bool sendNamespaceCompatibility);

			// Token: 0x06001279 RID: 4729
			RowsetFormatter Discover(string requestType, string requestnNamespace, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling, IDictionary requestProperties);

			// Token: 0x0600127A RID: 4730
			RowsetFormatter DiscoverSchema(string requestType, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling);

			// Token: 0x0600127B RID: 4731
			RowsetFormatter DiscoverWithCreateSession(string requestType, bool sendNamespaceCompatibility);

			// Token: 0x0600127C RID: 4732
			void CreateSession(bool sendNamespaceCompatibility);

			// Token: 0x0600127D RID: 4733
			void EndSession();

			// Token: 0x0600127E RID: 4734
			void ConnectXmla();

			// Token: 0x0600127F RID: 4735
			void Connect();

			// Token: 0x06001280 RID: 4736
			void Disconnect(bool endSession);

			// Token: 0x06001281 RID: 4737
			void CancelCommand(string sessionID);

			// Token: 0x06001282 RID: 4738
			string GetPropertyFromServer(string propName, bool sendNSCompatibility);

			// Token: 0x1700066E RID: 1646
			// (get) Token: 0x06001283 RID: 4739
			// (set) Token: 0x06001284 RID: 4740
			string ConnectionString { get; set; }

			// Token: 0x06001285 RID: 4741
			void SetXmlaProperty(string propertyName, string propertyValue);

			// Token: 0x06001286 RID: 4742
			string GetXmlaProperty(string propertyName);

			// Token: 0x06001287 RID: 4743
			void ResetSession();

			// Token: 0x1700066F RID: 1647
			// (get) Token: 0x06001288 RID: 4744
			bool IsExternalSession { get; }

			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x06001289 RID: 4745
			// (set) Token: 0x0600128A RID: 4746
			string SessionID { get; set; }

			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x0600128B RID: 4747
			// (set) Token: 0x0600128C RID: 4748
			bool ShowHiddenObjects { get; set; }

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x0600128D RID: 4749
			int ConnectionTimeout { get; }

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x0600128E RID: 4750
			string ServerName { get; }

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x0600128F RID: 4751
			string InstanceName { get; }

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001290 RID: 4752
			ConnectTo ConnectTo { get; }

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x06001291 RID: 4753
			ConnectionType ConnectionType { get; }

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x06001292 RID: 4754
			ConnectionInfo ConnectionInfo { get; }

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x06001293 RID: 4755
			uint AutoSyncPeriod { get; }

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x06001294 RID: 4756
			SafetyOptions SafetyOptions { get; }

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x06001295 RID: 4757
			bool IsXmlaClientConnected { get; }

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x06001296 RID: 4758
			// (set) Token: 0x06001297 RID: 4759
			bool UseEU { get; set; }

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x06001298 RID: 4760
			string ServerVersion { get; }

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x06001299 RID: 4761
			string ProviderVersion { get; }

			// Token: 0x0600129A RID: 4762
			void ResetInternalState();

			// Token: 0x0600129B RID: 4763
			bool IsPostYukonProvider();

			// Token: 0x0600129C RID: 4764
			void MarkConnectionStringRestricted();
		}

		// Token: 0x020001A0 RID: 416
		private class XmlaClientProvider : IDiscoverProvider, IExecuteProvider, AdomdConnection.IXmlaClientProviderEx
		{
			// Token: 0x0600129D RID: 4765 RVA: 0x00040D04 File Offset: 0x0003EF04
			private Dictionary<string, bool> GetTimeConversionMap(string discoverType)
			{
				Dictionary<string, bool> dictionary = null;
				if (AdomdConnection.XmlaClientProvider.localTimeConversionMap.TryGetValue(discoverType, out dictionary) && !this.IsPostYukonProvider)
				{
					dictionary = null;
				}
				return dictionary;
			}

			// Token: 0x0600129E RID: 4766 RVA: 0x00040D30 File Offset: 0x0003EF30
			static XmlaClientProvider()
			{
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				dictionary["DATE_MODIFIED"] = true;
				AdomdConnection.XmlaClientProvider.localTimeConversionMap["DBSCHEMA_CATALOGS"] = dictionary;
				dictionary = new Dictionary<string, bool>();
				dictionary["DATE_MODIFIED"] = true;
				AdomdConnection.XmlaClientProvider.localTimeConversionMap["DBSCHEMA_TABLES"] = dictionary;
				dictionary = new Dictionary<string, bool>();
				dictionary["LAST_SCHEMA_UPDATE"] = true;
				dictionary["LAST_DATA_UPDATE"] = true;
				AdomdConnection.XmlaClientProvider.localTimeConversionMap["MDSCHEMA_CUBES"] = dictionary;
				dictionary = new Dictionary<string, bool>();
				dictionary["CREATED_ON"] = true;
				dictionary["LAST_SCHEMA_UPDATE"] = true;
				AdomdConnection.XmlaClientProvider.localTimeConversionMap["MDSCHEMA_INPUT_DATASOURCES"] = dictionary;
				dictionary = new Dictionary<string, bool>();
				dictionary["DATE_CREATED"] = true;
				dictionary["DATE_MODIFIED"] = true;
				dictionary["LAST_PROCESSED"] = true;
				AdomdConnection.XmlaClientProvider.localTimeConversionMap["DMSCHEMA_MINING_MODELS"] = dictionary;
				dictionary = new Dictionary<string, bool>();
				dictionary["DATE_CREATED"] = true;
				dictionary["DATE_MODIFIED"] = true;
				dictionary["LAST_PROCESSED"] = true;
				AdomdConnection.XmlaClientProvider.localTimeConversionMap["DMSCHEMA_MINING_STRUCTURES"] = dictionary;
			}

			// Token: 0x0600129F RID: 4767 RVA: 0x00040E6C File Offset: 0x0003F06C
			internal XmlaClientProvider(AdomdConnection owner)
			{
				this.owner = owner;
			}

			// Token: 0x060012A0 RID: 4768 RVA: 0x00040E7B File Offset: 0x0003F07B
			internal XmlaClientProvider(AdomdConnection owner, AdomdConnection.XmlaClientProvider provider)
				: this(owner, new ConnectionInfo(provider.connectionInfo))
			{
			}

			// Token: 0x060012A1 RID: 4769 RVA: 0x00040E8F File Offset: 0x0003F08F
			internal XmlaClientProvider(AdomdConnection owner, ConnectionInfo info)
			{
				this.owner = owner;
				this.connectionInfo = info;
			}

			// Token: 0x060012A2 RID: 4770 RVA: 0x00040EA8 File Offset: 0x0003F0A8
			RowsetFormatter AdomdConnection.IXmlaClientProviderEx.Discover(string requestType, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling, bool sendNamespaceCompatibility)
			{
				RowsetFormatter rowsetFormatter;
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.Discover(requestType, this.DiscoverProperties, restrictions, sendNamespaceCompatibility);
					ResultsetFormatter resultsetFormatter = new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, inlineErrorHandling, null, timeConversionMap);
					if (!(resultsetFormatter is RowsetFormatter))
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, "");
					}
					rowsetFormatter = (RowsetFormatter)resultsetFormatter;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return rowsetFormatter;
			}

			// Token: 0x060012A3 RID: 4771 RVA: 0x00040F44 File Offset: 0x0003F144
			RowsetFormatter AdomdConnection.IXmlaClientProviderEx.Discover(string requestType, string requestNamespace, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling, IDictionary requestProperties)
			{
				RowsetFormatter rowsetFormatter;
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.Discover(requestType, requestNamespace, this.DiscoverProperties, restrictions, requestProperties);
					ResultsetFormatter resultsetFormatter = new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, inlineErrorHandling, null, timeConversionMap);
					if (!(resultsetFormatter is RowsetFormatter))
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, "");
					}
					rowsetFormatter = (RowsetFormatter)resultsetFormatter;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return rowsetFormatter;
			}

			// Token: 0x060012A4 RID: 4772 RVA: 0x00040FE4 File Offset: 0x0003F1E4
			RowsetFormatter AdomdConnection.IXmlaClientProviderEx.DiscoverSchema(string requestType, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling)
			{
				this.DiscoverProperties["Content"] = "Schema";
				RowsetFormatter rowsetFormatter;
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.Discover(requestType, this.DiscoverProperties, restrictions);
					ResultsetFormatter resultsetFormatter = new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, inlineErrorHandling, null, timeConversionMap);
					if (!(resultsetFormatter is RowsetFormatter))
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, "");
					}
					rowsetFormatter = (RowsetFormatter)resultsetFormatter;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				finally
				{
					this.DiscoverProperties["Content"] = "SchemaData";
				}
				return rowsetFormatter;
			}

			// Token: 0x060012A5 RID: 4773 RVA: 0x000410B4 File Offset: 0x0003F2B4
			RowsetFormatter AdomdConnection.IXmlaClientProviderEx.DiscoverWithCreateSession(string requestType, bool sendNamespaceCompatibility)
			{
				RowsetFormatter rowsetFormatter;
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.DiscoverWithCreateSession(requestType, this.DiscoverProperties, sendNamespaceCompatibility);
					ResultsetFormatter resultsetFormatter = new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, InlineErrorHandlingType.StoreInCell, null, timeConversionMap);
					if (!(resultsetFormatter is RowsetFormatter))
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, "");
					}
					rowsetFormatter = (RowsetFormatter)resultsetFormatter;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return rowsetFormatter;
			}

			// Token: 0x060012A6 RID: 4774 RVA: 0x00041150 File Offset: 0x0003F350
			void AdomdConnection.IXmlaClientProviderEx.CreateSession(bool sendNamespaceCompatibility)
			{
				try
				{
					this.client.CreateSession(this.connectionInfo.ExtendedProperties, sendNamespaceCompatibility);
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012A7 RID: 4775 RVA: 0x000411AC File Offset: 0x0003F3AC
			void AdomdConnection.IXmlaClientProviderEx.EndSession()
			{
				try
				{
					this.client.EndSession(this.connectionInfo.ExtendedProperties);
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012A8 RID: 4776 RVA: 0x00041208 File Offset: 0x0003F408
			void AdomdConnection.IXmlaClientProviderEx.ConnectXmla()
			{
				this.Connect();
			}

			// Token: 0x060012A9 RID: 4777 RVA: 0x00041210 File Offset: 0x0003F410
			private void Connect()
			{
				try
				{
					this.discoverProperties = null;
					this.executeProperties = null;
					if (this.client == null)
					{
						this.client = new AdomdClient(this.owner);
					}
					this.client.Connect(this.connectionInfo, false);
					this.client.SessionID = this.sessionID;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012AA RID: 4778 RVA: 0x000412A0 File Offset: 0x0003F4A0
			void AdomdConnection.IXmlaClientProviderEx.Connect()
			{
				try
				{
					((AdomdConnection.IXmlaClientProviderEx)this).ConnectXmla();
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012AB RID: 4779 RVA: 0x000412EC File Offset: 0x0003F4EC
			void AdomdConnection.IXmlaClientProviderEx.Disconnect(bool endSession)
			{
				try
				{
					if (this.client != null)
					{
						this.client.Disconnect(endSession);
					}
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012AC RID: 4780 RVA: 0x00041348 File Offset: 0x0003F548
			void AdomdConnection.IXmlaClientProviderEx.CancelCommand(string sessionID)
			{
				try
				{
					this.client.CancelCommand(sessionID);
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012AD RID: 4781 RVA: 0x0004139C File Offset: 0x0003F59C
			string AdomdConnection.IXmlaClientProviderEx.GetPropertyFromServer(string propName, bool sendNSCompatibility)
			{
				DataRowCollection rows = ((AdomdConnection.IXmlaClientProviderEx)this).Discover("DISCOVER_PROPERTIES", new ListDictionary { { "PropertyName", propName } }, InlineErrorHandlingType.StoreInCell, sendNSCompatibility).MainRowsetTable.Rows;
				if (rows.Count != 1)
				{
					throw new NotSupportedException(SR.Connection_InvalidProperty(propName));
				}
				return rows[0]["Value"].ToString();
			}

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x060012AE RID: 4782 RVA: 0x000413FD File Offset: 0x0003F5FD
			// (set) Token: 0x060012AF RID: 4783 RVA: 0x00041414 File Offset: 0x0003F614
			string AdomdConnection.IXmlaClientProviderEx.ConnectionString
			{
				get
				{
					if (this.connectionInfo == null)
					{
						return null;
					}
					return this.connectionInfo.ConnectionString;
				}
				set
				{
					if (this.connectionInfo == null)
					{
						this.connectionInfo = new ConnectionInfo(value);
					}
					else
					{
						this.connectionInfo.ConnectionString = value;
					}
					if (!string.IsNullOrEmpty(this.connectionInfo.SessionID))
					{
						this.SessionID = this.connectionInfo.SessionID;
					}
					this.originalConnectionStringShowHiddenCubePropertyValue = ((AdomdConnection.IXmlaClientProviderEx)this).GetXmlaProperty("ShowHiddenCubes");
					if (this.showHiddenObjects)
					{
						this.UpdateShowHiddenCubesProperty(true);
					}
					this.discoverProperties = null;
					this.executeProperties = null;
				}
			}

			// Token: 0x060012B0 RID: 4784 RVA: 0x00041494 File Offset: 0x0003F694
			void AdomdConnection.IXmlaClientProviderEx.SetXmlaProperty(string propertyName, string propertyValue)
			{
				if (string.IsNullOrEmpty(propertyValue))
				{
					if (!this.connectionInfo.ExtendedProperties.Contains(propertyName))
					{
						return;
					}
					this.connectionInfo.ExtendedProperties.Remove(propertyName);
				}
				else
				{
					this.connectionInfo.ExtendedProperties[propertyName] = propertyValue;
				}
				if ((this.connectionInfo.IsPbiPremiumInternal || this.connectionInfo.IsPbiPremiumXmlaEp || this.connectionInfo.IsWorkloadDirectConnection) && string.Compare(propertyName, "Catalog", StringComparison.InvariantCulture) == 0)
				{
					this.connectionInfo.PbipCoreServiceRoutingHint = null;
					this.connectionInfo.PbipWorkloadResourceMoniker = null;
				}
				this.discoverProperties = null;
				this.executeProperties = null;
			}

			// Token: 0x060012B1 RID: 4785 RVA: 0x0004153D File Offset: 0x0003F73D
			string AdomdConnection.IXmlaClientProviderEx.GetXmlaProperty(string propertyName)
			{
				return this.connectionInfo.ExtendedProperties[propertyName] as string;
			}

			// Token: 0x060012B2 RID: 4786 RVA: 0x00041555 File Offset: 0x0003F755
			void AdomdConnection.IXmlaClientProviderEx.ResetSession()
			{
				this.client.SessionID = this.sessionID;
			}

			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00041568 File Offset: 0x0003F768
			bool AdomdConnection.IXmlaClientProviderEx.IsExternalSession
			{
				get
				{
					return this.sessionID != null;
				}
			}

			// Token: 0x060012B4 RID: 4788 RVA: 0x00041573 File Offset: 0x0003F773
			void AdomdConnection.IXmlaClientProviderEx.MarkConnectionStringRestricted()
			{
				if (this.connectionInfo != null)
				{
					this.connectionInfo.RestrictConnectionString();
				}
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00041588 File Offset: 0x0003F788
			// (set) Token: 0x060012B6 RID: 4790 RVA: 0x000415A4 File Offset: 0x0003F7A4
			private string SessionID
			{
				get
				{
					if (this.client != null)
					{
						return this.client.SessionID;
					}
					return this.sessionID;
				}
				set
				{
					this.sessionID = value;
				}
			}

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000415AD File Offset: 0x0003F7AD
			// (set) Token: 0x060012B8 RID: 4792 RVA: 0x000415B5 File Offset: 0x0003F7B5
			string AdomdConnection.IXmlaClientProviderEx.SessionID
			{
				get
				{
					return this.SessionID;
				}
				set
				{
					this.SessionID = value;
				}
			}

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000415BE File Offset: 0x0003F7BE
			// (set) Token: 0x060012BA RID: 4794 RVA: 0x000415C6 File Offset: 0x0003F7C6
			bool AdomdConnection.IXmlaClientProviderEx.ShowHiddenObjects
			{
				get
				{
					return this.showHiddenObjects;
				}
				set
				{
					if (this.showHiddenObjects != value)
					{
						this.showHiddenObjects = value;
						this.UpdateShowHiddenCubesProperty(value);
					}
				}
			}

			// Token: 0x17000683 RID: 1667
			// (get) Token: 0x060012BB RID: 4795 RVA: 0x000415DF File Offset: 0x0003F7DF
			int AdomdConnection.IXmlaClientProviderEx.ConnectionTimeout
			{
				get
				{
					if (this.connectionInfo == null)
					{
						return 60;
					}
					return this.connectionInfo.ConnectTimeout;
				}
			}

			// Token: 0x17000684 RID: 1668
			// (get) Token: 0x060012BC RID: 4796 RVA: 0x000415F7 File Offset: 0x0003F7F7
			string AdomdConnection.IXmlaClientProviderEx.ServerName
			{
				get
				{
					return this.connectionInfo.Server;
				}
			}

			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x060012BD RID: 4797 RVA: 0x00041604 File Offset: 0x0003F804
			string AdomdConnection.IXmlaClientProviderEx.InstanceName
			{
				get
				{
					return this.connectionInfo.InstanceName;
				}
			}

			// Token: 0x17000686 RID: 1670
			// (get) Token: 0x060012BE RID: 4798 RVA: 0x00041611 File Offset: 0x0003F811
			ConnectTo AdomdConnection.IXmlaClientProviderEx.ConnectTo
			{
				get
				{
					return this.connectionInfo.ConnectTo;
				}
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004161E File Offset: 0x0003F81E
			ConnectionType AdomdConnection.IXmlaClientProviderEx.ConnectionType
			{
				get
				{
					return this.connectionInfo.ConnectionType;
				}
			}

			// Token: 0x17000688 RID: 1672
			// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0004162B File Offset: 0x0003F82B
			uint AdomdConnection.IXmlaClientProviderEx.AutoSyncPeriod
			{
				get
				{
					if (this.connectionInfo == null)
					{
						return 10000U;
					}
					return this.connectionInfo.AutoSyncPeriod;
				}
			}

			// Token: 0x17000689 RID: 1673
			// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00041646 File Offset: 0x0003F846
			SafetyOptions AdomdConnection.IXmlaClientProviderEx.SafetyOptions
			{
				get
				{
					return this.connectionInfo.SafetyOptions;
				}
			}

			// Token: 0x1700068A RID: 1674
			// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00041653 File Offset: 0x0003F853
			private bool IsXmlaClientConnected
			{
				get
				{
					return this.client != null && this.client.IsConnected;
				}
			}

			// Token: 0x1700068B RID: 1675
			// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004166A File Offset: 0x0003F86A
			bool AdomdConnection.IXmlaClientProviderEx.IsXmlaClientConnected
			{
				get
				{
					return this.IsXmlaClientConnected;
				}
			}

			// Token: 0x1700068C RID: 1676
			// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00041672 File Offset: 0x0003F872
			string AdomdConnection.IXmlaClientProviderEx.ServerVersion
			{
				get
				{
					return this.ServerVersion;
				}
			}

			// Token: 0x1700068D RID: 1677
			// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0004167A File Offset: 0x0003F87A
			string AdomdConnection.IXmlaClientProviderEx.ProviderVersion
			{
				get
				{
					return this.ProviderVersion;
				}
			}

			// Token: 0x060012C6 RID: 4806 RVA: 0x00041682 File Offset: 0x0003F882
			void AdomdConnection.IXmlaClientProviderEx.ResetInternalState()
			{
				this.serverVersionObject = null;
				this.serverVersion = null;
				this.providerVersion = null;
			}

			// Token: 0x060012C7 RID: 4807 RVA: 0x00041699 File Offset: 0x0003F899
			bool AdomdConnection.IXmlaClientProviderEx.IsPostYukonProvider()
			{
				return this.IsPostYukonProvider;
			}

			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000416A1 File Offset: 0x0003F8A1
			ConnectionInfo AdomdConnection.IXmlaClientProviderEx.ConnectionInfo
			{
				get
				{
					return this.connectionInfo;
				}
			}

			// Token: 0x1700068F RID: 1679
			// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000416A9 File Offset: 0x0003F8A9
			// (set) Token: 0x060012CA RID: 4810 RVA: 0x000416C0 File Offset: 0x0003F8C0
			bool AdomdConnection.IXmlaClientProviderEx.UseEU
			{
				get
				{
					return this.connectionInfo != null && this.connectionInfo.UseEU;
				}
				set
				{
					if (this.connectionInfo != null)
					{
						this.connectionInfo.UseEU = value;
					}
				}
			}

			// Token: 0x060012CB RID: 4811 RVA: 0x000416D8 File Offset: 0x0003F8D8
			void IDiscoverProvider.Discover(string requestType, IDictionary restrictions, DataTable table)
			{
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.Discover(requestType, this.DiscoverProperties, restrictions);
					new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, InlineErrorHandlingType.StoreInCell, table, false, timeConversionMap);
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012CC RID: 4812 RVA: 0x00041754 File Offset: 0x0003F954
			void IDiscoverProvider.DiscoverData(string requestType, IDictionary restrictions, DataTable table)
			{
				if (XmlaClient.IsBinaryDesired(this.connectionInfo))
				{
					this.DiscoverProperties["Content"] = "SchemaData";
				}
				else
				{
					this.DiscoverProperties["Content"] = "Data";
				}
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.Discover(requestType, this.DiscoverProperties, restrictions);
					new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, InlineErrorHandlingType.StoreInCell, table, false, timeConversionMap);
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				finally
				{
					this.DiscoverProperties["Content"] = "SchemaData";
				}
			}

			// Token: 0x060012CD RID: 4813 RVA: 0x0004182C File Offset: 0x0003FA2C
			RowsetFormatter IDiscoverProvider.Discover(string requestType, IDictionary restrictions)
			{
				RowsetFormatter rowsetFormatter;
				try
				{
					Dictionary<string, bool> timeConversionMap = this.GetTimeConversionMap(requestType);
					XmlaReader xmlaReader = (XmlaReader)this.client.Discover(requestType, this.DiscoverProperties, restrictions);
					ResultsetFormatter resultsetFormatter = new SoapFormatter(this.client).ReadDiscoverResponse(xmlaReader, InlineErrorHandlingType.StoreInCell, null, false, timeConversionMap);
					if (!(resultsetFormatter is RowsetFormatter))
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, "");
					}
					rowsetFormatter = (RowsetFormatter)resultsetFormatter;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return rowsetFormatter;
			}

			// Token: 0x060012CE RID: 4814 RVA: 0x000418C8 File Offset: 0x0003FAC8
			MDDatasetFormatter IExecuteProvider.ExecuteMultidimensional(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters)
			{
				MDDatasetFormatter mddatasetFormatter;
				try
				{
					IDictionary executionCommandProperties = this.GetExecutionCommandProperties(commandProperties, new AdomdPropertyCollection
					{
						{ "Format", "Multidimensional" },
						{ "AxisFormat", "TupleFormat" },
						{
							"Content",
							this.GetContentAtLeastMetadata(commandProperties)
						}
					});
					XmlaReader xmlaReader;
					if (contentProvider.CommandText != null)
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStatement(contentProvider.CommandText, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
					else
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStream(contentProvider.CommandStream, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
					ResultsetFormatter resultsetFormatter = new SoapFormatter(this.client).ReadResponse(xmlaReader);
					if (!(resultsetFormatter is MDDatasetFormatter))
					{
						throw new AdomdUnknownResponseException(SR.Resultset_IsNotDataset, "");
					}
					mddatasetFormatter = (MDDatasetFormatter)resultsetFormatter;
				}
				catch (AdomdErrorResponseException ex)
				{
					if (ex.ErrorCode == -1056309049)
					{
						throw new AdomdUnknownResponseException(SR.Resultset_IsNotDataset, ex);
					}
					throw;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return mddatasetFormatter;
			}

			// Token: 0x060012CF RID: 4815 RVA: 0x00041A04 File Offset: 0x0003FC04
			XmlaReader IExecuteProvider.ExecuteTabular(CommandBehavior behavior, ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters)
			{
				XmlaReader xmlaReader;
				try
				{
					AdomdPropertyCollection adomdPropertyCollection = new AdomdPropertyCollection();
					adomdPropertyCollection.Add("Format", "Tabular");
					if ((behavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
					{
						adomdPropertyCollection.Add("Content", "Schema");
					}
					else
					{
						adomdPropertyCollection.Add("Content", this.GetContentAtLeastSchema(commandProperties));
						if ((behavior & CommandBehavior.SingleRow) != CommandBehavior.Default)
						{
							adomdPropertyCollection.Add("BeginRange", -1);
							adomdPropertyCollection.Add("EndRange", 0);
						}
					}
					IDictionary executionCommandProperties = this.GetExecutionCommandProperties(commandProperties, adomdPropertyCollection);
					if (contentProvider.CommandText != null)
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStatement(contentProvider.CommandText, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
					else
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStream(contentProvider.CommandStream, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
				}
				catch (AdomdErrorResponseException ex)
				{
					if (ex.ErrorCode == -1056309049)
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, ex);
					}
					throw;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return xmlaReader;
			}

			// Token: 0x060012D0 RID: 4816 RVA: 0x00041B3C File Offset: 0x0003FD3C
			void IExecuteProvider.ExecuteAny(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters)
			{
				try
				{
					IDictionary executionCommandProperties = this.GetExecutionCommandProperties(commandProperties, new AdomdPropertyCollection { { "Format", "Native" } });
					XmlaReader xmlaReader = null;
					if (contentProvider.CommandText != null)
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStatement(contentProvider.CommandText, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
					else
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStream(contentProvider.CommandStream, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
					try
					{
						if (!XmlaClient.IsExecuteResponseS(xmlaReader))
						{
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected element {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis", "ExecuteResponse", xmlaReader.Name));
						}
						XmlaClient.ReadExecuteResponse(xmlaReader);
					}
					catch (XmlException ex)
					{
						throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
					}
					catch (COMException ex2)
					{
						if (this.client != null)
						{
							this.client.Disconnect(false);
						}
						throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
					}
					catch (IOException ex3)
					{
						if (this.client != null)
						{
							this.client.Disconnect(false);
						}
						throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex3);
					}
					finally
					{
						xmlaReader.Close();
					}
				}
				catch (AdomdErrorResponseException ex4)
				{
					if (ex4.ErrorCode != -1056309049)
					{
						throw;
					}
					this.HandleCreateLocalCube(ex4);
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012D1 RID: 4817 RVA: 0x00041D40 File Offset: 0x0003FF40
			XmlaReader IExecuteProvider.Execute(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters)
			{
				XmlaReader xmlaReader;
				try
				{
					IDictionary executionCommandProperties = this.GetExecutionCommandProperties(commandProperties, new AdomdPropertyCollection
					{
						{ "Format", "Native" },
						{ "AxisFormat", "TupleFormat" },
						{
							"Content",
							this.GetContentAtLeastMetadata(commandProperties)
						}
					});
					if (contentProvider.CommandText != null)
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStatement(contentProvider.CommandText, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
					else
					{
						xmlaReader = (XmlaReader)this.client.ExecuteStream(contentProvider.CommandStream, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx);
					}
				}
				catch (AdomdErrorResponseException ex)
				{
					if (ex.ErrorCode != -1056309049)
					{
						throw;
					}
					this.HandleCreateLocalCube(ex);
					xmlaReader = null;
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
				return xmlaReader;
			}

			// Token: 0x060012D2 RID: 4818 RVA: 0x00041E48 File Offset: 0x00040048
			void IExecuteProvider.Prepare(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters)
			{
				try
				{
					IDictionary executionCommandProperties = this.GetExecutionCommandProperties(commandProperties, new AdomdPropertyCollection
					{
						{ "Format", "Native" },
						{ "Content", "Metadata" },
						{ "ExecutionMode", "Prepare" }
					});
					if (contentProvider.CommandText != null)
					{
						((XmlaReader)this.client.ExecuteStatement(contentProvider.CommandText, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx)).Close();
					}
					else
					{
						((XmlaReader)this.client.ExecuteStream(contentProvider.CommandStream, this.ExecuteProperties, executionCommandProperties, parameters, contentProvider.IsContentMdx)).Close();
					}
				}
				catch (AdomdException)
				{
					throw;
				}
				catch
				{
					if (this.client != null)
					{
						this.client.Disconnect(false);
					}
					throw;
				}
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x00041F2C File Offset: 0x0004012C
			private void UpdateShowHiddenCubesProperty(bool newValue)
			{
				if (this.connectionInfo != null)
				{
					if (newValue)
					{
						((AdomdConnection.IXmlaClientProviderEx)this).SetXmlaProperty("ShowHiddenCubes", "true");
						return;
					}
					((AdomdConnection.IXmlaClientProviderEx)this).SetXmlaProperty("ShowHiddenCubes", this.originalConnectionStringShowHiddenCubePropertyValue);
				}
			}

			// Token: 0x17000690 RID: 1680
			// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00041F68 File Offset: 0x00040168
			private ListDictionary DiscoverProperties
			{
				get
				{
					if (this.discoverProperties == null)
					{
						this.discoverProperties = new ListDictionary();
						foreach (object obj in this.connectionInfo.ExtendedProperties.Keys)
						{
							string text = (string)obj;
							this.discoverProperties[text] = this.connectionInfo.ExtendedProperties[text];
						}
						if (this.discoverProperties.Contains("AxisFormat"))
						{
							this.discoverProperties.Remove("AxisFormat");
						}
						this.discoverProperties["Content"] = "SchemaData";
						this.discoverProperties["Format"] = "Tabular";
					}
					return this.discoverProperties;
				}
			}

			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004204C File Offset: 0x0004024C
			private ListDictionary ExecuteProperties
			{
				get
				{
					if (this.executeProperties == null)
					{
						this.executeProperties = new ListDictionary();
						foreach (object obj in this.connectionInfo.ExtendedProperties.Keys)
						{
							string text = (string)obj;
							if (text != "AxisFormat" && text != "Content" && text != "Format" && text != "ExecutionMode")
							{
								this.executeProperties[text] = this.connectionInfo.ExtendedProperties[text];
							}
						}
					}
					return this.executeProperties;
				}
			}

			// Token: 0x060012D6 RID: 4822 RVA: 0x00042118 File Offset: 0x00040318
			private IDictionary GetExecutionCommandProperties(AdomdPropertyCollection commandProperties, AdomdPropertyCollection overrides)
			{
				if (commandProperties == null && overrides == null)
				{
					return null;
				}
				if (commandProperties == null)
				{
					return overrides.InternalCollection;
				}
				if (overrides == null)
				{
					return commandProperties.InternalCollection;
				}
				foreach (AdomdProperty adomdProperty in commandProperties)
				{
					if (!overrides.InternalCollection.Contains(adomdProperty))
					{
						overrides.Add(new AdomdProperty(adomdProperty.Name, adomdProperty.Namespace, adomdProperty.Value));
					}
				}
				return overrides.InternalCollection;
			}

			// Token: 0x060012D7 RID: 4823 RVA: 0x00042190 File Offset: 0x00040390
			private string GetContentAtLeastMetadata(AdomdPropertyCollection commandProperties)
			{
				string text = "SchemaData";
				if (commandProperties != null && commandProperties.InternalCollection.Contains(AdomdConnection.XmlaClientProvider.ContentPropertyXmlaKey))
				{
					IXmlaProperty xmlaProperty = commandProperties.InternalCollection[AdomdConnection.XmlaClientProvider.ContentPropertyXmlaKey] as IXmlaProperty;
					if (xmlaProperty != null && xmlaProperty.Value != null)
					{
						text = xmlaProperty.Value as string;
					}
				}
				else if (this.connectionInfo.ExtendedProperties.Contains("Content"))
				{
					text = this.connectionInfo.ExtendedProperties["Content"] as string;
				}
				if (text == "Data")
				{
					text = "SchemaData";
				}
				else if (text != "SchemaData")
				{
					text = "Metadata";
				}
				return text;
			}

			// Token: 0x060012D8 RID: 4824 RVA: 0x00042244 File Offset: 0x00040444
			private string GetContentAtLeastSchema(AdomdPropertyCollection commandProperties)
			{
				string text = "SchemaData";
				if (commandProperties != null && commandProperties.InternalCollection.Contains(AdomdConnection.XmlaClientProvider.ContentPropertyXmlaKey))
				{
					IXmlaProperty xmlaProperty = commandProperties.InternalCollection[AdomdConnection.XmlaClientProvider.ContentPropertyXmlaKey] as IXmlaProperty;
					if (xmlaProperty != null && xmlaProperty.Value != null)
					{
						text = xmlaProperty.Value as string;
					}
				}
				else if (this.connectionInfo.ExtendedProperties.Contains("Content"))
				{
					text = this.connectionInfo.ExtendedProperties["Content"] as string;
				}
				if (text == "Data")
				{
					text = "SchemaData";
				}
				else if (text != "Metadata" && text != "SchemaData")
				{
					text = "Schema";
				}
				return text;
			}

			// Token: 0x060012D9 RID: 4825 RVA: 0x00042304 File Offset: 0x00040504
			private void HandleCreateLocalCube(AdomdErrorResponseException ex)
			{
				string text = null;
				string text2 = null;
				if (!AdomdConnection.XmlaClientProvider.ParseCreateGlobalCubeErrorMessage(ex.Message, out text, out text2))
				{
					throw new AdomdUnknownResponseException(ex);
				}
				AdomdConnection.IXmlaClientProviderEx xmlaClientProviderEx = new AdomdConnection.XmlaClientProvider(this.owner, ConnectionInfo.GetModifiedConnectionInfo(this.connectionInfo, text));
				xmlaClientProviderEx.Connect();
				try
				{
					XmlaReader xmlaReader = (xmlaClientProviderEx as IExecuteProvider).Execute(new AdomdConnection.XmlaClientProvider.StringCommandContentProvider(text2, false), null, null);
					try
					{
						if (!XmlaClient.IsExecuteResponseS(xmlaReader))
						{
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected element {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis", "ExecuteResponse", xmlaReader.Name));
						}
						XmlaClient.ReadExecuteResponse(xmlaReader);
					}
					finally
					{
						xmlaReader.Close();
					}
				}
				catch (XmlException ex2)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex2);
				}
				catch (IOException ex3)
				{
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex3);
				}
				finally
				{
					if (xmlaClientProviderEx != null && xmlaClientProviderEx.IsXmlaClientConnected)
					{
						xmlaClientProviderEx.Disconnect(false);
					}
				}
			}

			// Token: 0x060012DA RID: 4826 RVA: 0x0004240C File Offset: 0x0004060C
			private static bool ParseCreateGlobalCubeErrorMessage(string message, out string filename, out string ddl)
			{
				filename = null;
				ddl = null;
				if (string.IsNullOrEmpty(message))
				{
					return false;
				}
				int num = message.IndexOf("FILENAME|", 0, StringComparison.Ordinal);
				if (num == -1)
				{
					return false;
				}
				num += "FILENAME|".Length;
				int num2 = message.IndexOf("|DDL|", num, StringComparison.Ordinal);
				if (num2 == -1)
				{
					return false;
				}
				filename = message.Substring(num, num2 - num);
				num2 += "|DDL|".Length;
				ddl = message.Substring(num2);
				return true;
			}

			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x060012DB RID: 4827 RVA: 0x00042481 File Offset: 0x00040681
			private Version ServerVersionObject
			{
				get
				{
					if (null == this.serverVersionObject)
					{
						this.serverVersionObject = AdomdUtils.ConvertVersionStringToVersionObject(this.ServerVersion);
					}
					return this.serverVersionObject;
				}
			}

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x060012DC RID: 4828 RVA: 0x000424A8 File Offset: 0x000406A8
			private bool IsPostYukonProvider
			{
				get
				{
					return AdomdUtils.IsPostYukonVersion(this.ServerVersionObject);
				}
			}

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x060012DD RID: 4829 RVA: 0x000424B8 File Offset: 0x000406B8
			private string ServerVersion
			{
				get
				{
					if (this.serverVersion == null)
					{
						try
						{
							this.serverVersion = ((AdomdConnection.IXmlaClientProviderEx)this).GetPropertyFromServer("DBMSVersion", false);
						}
						catch (NotSupportedException)
						{
							this.serverVersion = this.ProviderVersion;
						}
					}
					return this.serverVersion;
				}
			}

			// Token: 0x17000695 RID: 1685
			// (get) Token: 0x060012DE RID: 4830 RVA: 0x00042508 File Offset: 0x00040708
			private string ProviderVersion
			{
				get
				{
					if (this.providerVersion == null)
					{
						this.providerVersion = ((AdomdConnection.IXmlaClientProviderEx)this).GetPropertyFromServer("ProviderVersion", false);
					}
					return this.providerVersion;
				}
			}

			// Token: 0x04000C89 RID: 3209
			private const string contentProperty = "Content";

			// Token: 0x04000C8A RID: 3210
			private const string dataContent = "Data";

			// Token: 0x04000C8B RID: 3211
			private const string schemaContent = "Schema";

			// Token: 0x04000C8C RID: 3212
			private const string schemaDataContent = "SchemaData";

			// Token: 0x04000C8D RID: 3213
			private const string metadataContent = "Metadata";

			// Token: 0x04000C8E RID: 3214
			private const string noneContent = "None";

			// Token: 0x04000C8F RID: 3215
			private const string axisFormatProperty = "AxisFormat";

			// Token: 0x04000C90 RID: 3216
			private const string tupleFormat = "TupleFormat";

			// Token: 0x04000C91 RID: 3217
			private const string formatProperty = "Format";

			// Token: 0x04000C92 RID: 3218
			private const string tabularFormat = "Tabular";

			// Token: 0x04000C93 RID: 3219
			private const string multidimensionalFormat = "Multidimensional";

			// Token: 0x04000C94 RID: 3220
			private const string nativeFormat = "Native";

			// Token: 0x04000C95 RID: 3221
			private const string beginRange = "BeginRange";

			// Token: 0x04000C96 RID: 3222
			private const string endRange = "EndRange";

			// Token: 0x04000C97 RID: 3223
			private const string executionModeProperty = "ExecutionMode";

			// Token: 0x04000C98 RID: 3224
			private const string prepare = "Prepare";

			// Token: 0x04000C99 RID: 3225
			private const string providerVersionProp = "ProviderVersion";

			// Token: 0x04000C9A RID: 3226
			private const string serverVersionProp = "DBMSVersion";

			// Token: 0x04000C9B RID: 3227
			private const string propertyNameRest = "PropertyName";

			// Token: 0x04000C9C RID: 3228
			private const string discoverPropertiesType = "DISCOVER_PROPERTIES";

			// Token: 0x04000C9D RID: 3229
			private const int PFE_MDX_CREATE_LOCAL_CUBE = -1056309049;

			// Token: 0x04000C9E RID: 3230
			private const string fileNameStr = "FILENAME|";

			// Token: 0x04000C9F RID: 3231
			private const string ddlStr = "|DDL|";

			// Token: 0x04000CA0 RID: 3232
			private static readonly XmlaPropertyKey ContentPropertyXmlaKey = new XmlaPropertyKey("Content", null);

			// Token: 0x04000CA1 RID: 3233
			private static readonly Dictionary<string, Dictionary<string, bool>> localTimeConversionMap = new Dictionary<string, Dictionary<string, bool>>(4);

			// Token: 0x04000CA2 RID: 3234
			private AdomdConnection owner;

			// Token: 0x04000CA3 RID: 3235
			private ListDictionary discoverProperties;

			// Token: 0x04000CA4 RID: 3236
			private ListDictionary executeProperties;

			// Token: 0x04000CA5 RID: 3237
			private ConnectionInfo connectionInfo;

			// Token: 0x04000CA6 RID: 3238
			private string sessionID;

			// Token: 0x04000CA7 RID: 3239
			private bool showHiddenObjects;

			// Token: 0x04000CA8 RID: 3240
			private string originalConnectionStringShowHiddenCubePropertyValue;

			// Token: 0x04000CA9 RID: 3241
			private XmlaClient client;

			// Token: 0x04000CAA RID: 3242
			private Version serverVersionObject;

			// Token: 0x04000CAB RID: 3243
			private string providerVersion;

			// Token: 0x04000CAC RID: 3244
			private string serverVersion;

			// Token: 0x02000240 RID: 576
			private class StringCommandContentProvider : ICommandContentProvider
			{
				// Token: 0x060015A7 RID: 5543 RVA: 0x0004892D File Offset: 0x00046B2D
				internal StringCommandContentProvider(string commandText, bool isMdx)
				{
					this.commandText = commandText;
					this.isMdx = isMdx;
				}

				// Token: 0x17000755 RID: 1877
				// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00048955 File Offset: 0x00046B55
				Stream ICommandContentProvider.CommandStream
				{
					get
					{
						return null;
					}
				}

				// Token: 0x17000756 RID: 1878
				// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00048958 File Offset: 0x00046B58
				string ICommandContentProvider.CommandText
				{
					get
					{
						return this.commandText;
					}
				}

				// Token: 0x17000757 RID: 1879
				// (get) Token: 0x060015AA RID: 5546 RVA: 0x00048960 File Offset: 0x00046B60
				bool ICommandContentProvider.IsContentMdx
				{
					get
					{
						return this.isMdx;
					}
				}

				// Token: 0x04000F74 RID: 3956
				private string commandText = string.Empty;

				// Token: 0x04000F75 RID: 3957
				private bool isMdx = true;
			}
		}

		// Token: 0x020001A1 RID: 417
		internal class XmlaMDSchemas
		{
			// Token: 0x060012DF RID: 4831 RVA: 0x0004252A File Offset: 0x0004072A
			internal XmlaMDSchemas(RowsetFormatter formatter)
			{
				this.PopulateSchemasInfos(formatter);
			}

			// Token: 0x060012E0 RID: 4832 RVA: 0x00042539 File Offset: 0x00040739
			internal XmlaMDSchemas()
			{
				this.schemasByGuid = AdomdConnection.ShilohSchemas.SchemasByGuid;
				this.schemasByName = AdomdConnection.ShilohSchemas.SchemasByName;
			}

			// Token: 0x060012E1 RID: 4833 RVA: 0x00042557 File Offset: 0x00040757
			internal AdomdConnection.XmlaMDSchema GetSchemaInfo(Guid schemaGuid)
			{
				if (!this.schemasByGuid.ContainsKey(schemaGuid))
				{
					throw new ArgumentException(SR.Schema_InvalidGuid, "schemaGuid");
				}
				return (AdomdConnection.XmlaMDSchema)this.schemasByGuid[schemaGuid];
			}

			// Token: 0x060012E2 RID: 4834 RVA: 0x00042594 File Offset: 0x00040794
			private void PopulateSchemasInfos(RowsetFormatter formatter)
			{
				this.schemasByGuid = new Hashtable(formatter.MainRowsetTable.Rows.Count);
				this.schemasByName = new Hashtable(formatter.MainRowsetTable.Rows.Count);
				foreach (object obj in formatter.MainRowsetTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = AdomdUtils.GetProperty(dataRow, "SchemaName") as string;
					if (text == null)
					{
						throw new AdomdUnknownResponseException(SR.Schema_PropertyIsMissingOrOfAnUnexpectedType("DISCOVER_SCHEMA_ROWSETS", "SchemaName"), "");
					}
					Guid guid = Guid.Empty;
					object obj2 = null;
					if (formatter.MainRowsetTable.Columns.Contains("SchemaGuid"))
					{
						obj2 = AdomdUtils.GetProperty(dataRow, "SchemaGuid");
						if (obj2 is DBNull)
						{
							obj2 = null;
						}
					}
					if (obj2 != null && obj2 is Guid)
					{
						guid = (Guid)obj2;
					}
					DataRow[] childRows = dataRow.GetChildRows(formatter.MainRowsetTable.TableName + "Restrictions");
					AdomdConnection.XmlaMDSchemaRestriction[] array = new AdomdConnection.XmlaMDSchemaRestriction[childRows.Length];
					int num = 0;
					foreach (DataRow dataRow2 in childRows)
					{
						string text2 = AdomdUtils.GetProperty(dataRow2, "Name") as string;
						if (text2 == null)
						{
							throw new AdomdUnknownResponseException(SR.Schema_PropertyIsMissingOrOfAnUnexpectedType("DISCOVER_SCHEMA_ROWSETS", "Name"), "");
						}
						string text3 = AdomdUtils.GetProperty(dataRow2, "Type") as string;
						if (text3 == null)
						{
							throw new AdomdUnknownResponseException(SR.Schema_PropertyIsMissingOrOfAnUnexpectedType("DISCOVER_SCHEMA_ROWSETS", "Type"), "");
						}
						array[num] = new AdomdConnection.XmlaMDSchemaRestriction(text2, XmlaTypeHelper.GetNetTypeWithPrefix(text3));
						num++;
					}
					AdomdConnection.XmlaMDSchema xmlaMDSchema = new AdomdConnection.XmlaMDSchema(text, guid, array);
					this.schemasByName[xmlaMDSchema.SchemaName] = xmlaMDSchema;
					if (guid != Guid.Empty)
					{
						this.schemasByGuid[xmlaMDSchema.SchemaGuid] = xmlaMDSchema;
					}
				}
			}

			// Token: 0x060012E3 RID: 4835 RVA: 0x000427CC File Offset: 0x000409CC
			internal static void ConvertOleDbRestrictionsToXmlA(AdomdConnection.XmlaMDSchema schemaInfo, object[] restrictions, ListDictionary xmlaRestrictions)
			{
				if (restrictions.Length > schemaInfo.Restrictions.Length)
				{
					throw new ArgumentException(SR.Schema_RestOutOfRange, "restrictions");
				}
				for (int i = 0; i < restrictions.Length; i++)
				{
					object obj = restrictions[i];
					if (obj != null)
					{
						try
						{
							obj = Convert.ChangeType(obj, schemaInfo.Restrictions[i].Type, CultureInfo.InvariantCulture);
						}
						catch (InvalidCastException ex)
						{
							throw new ArgumentException(SR.Restrictions_TypesMismatch(schemaInfo.Restrictions[i].Name, schemaInfo.Restrictions[i].Type.FullName, restrictions[i].GetType().FullName), ex);
						}
					}
					if (obj != null)
					{
						xmlaRestrictions.Add(schemaInfo.Restrictions[i].Name, obj);
					}
				}
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x000428A4 File Offset: 0x00040AA4
			internal static void MungeMembersSchemaColumnNames(string schemaName, string schemaNamespace, DataSet dataSet)
			{
				if (dataSet != null && dataSet.Tables.Count == 1 && schemaName == "MDSCHEMA_MEMBERS" && (schemaNamespace == null || schemaNamespace.Length == 0))
				{
					foreach (object obj in dataSet.Tables[0].Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						try
						{
							dataColumn.ColumnName = dataColumn.Caption;
						}
						catch (DuplicateNameException)
						{
						}
					}
				}
			}

			// Token: 0x04000CAD RID: 3245
			private Hashtable schemasByName;

			// Token: 0x04000CAE RID: 3246
			private Hashtable schemasByGuid;

			// Token: 0x04000CAF RID: 3247
			private const string MembersSchemaName = "MDSCHEMA_MEMBERS";
		}

		// Token: 0x020001A2 RID: 418
		private static class ShilohSchemas
		{
			// Token: 0x060012E5 RID: 4837 RVA: 0x00042948 File Offset: 0x00040B48
			static ShilohSchemas()
			{
				foreach (AdomdConnection.XmlaMDSchema xmlaMDSchema in AdomdConnection.ShilohSchemas.shilohSchemas)
				{
					AdomdConnection.ShilohSchemas.schemasByGuid[xmlaMDSchema.SchemaGuid] = xmlaMDSchema;
					AdomdConnection.ShilohSchemas.schemasByName[xmlaMDSchema.SchemaName] = xmlaMDSchema;
				}
			}

			// Token: 0x17000696 RID: 1686
			// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00043A66 File Offset: 0x00041C66
			internal static Hashtable SchemasByGuid
			{
				get
				{
					return AdomdConnection.ShilohSchemas.schemasByGuid;
				}
			}

			// Token: 0x17000697 RID: 1687
			// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00043A6D File Offset: 0x00041C6D
			internal static Hashtable SchemasByName
			{
				get
				{
					return AdomdConnection.ShilohSchemas.schemasByName;
				}
			}

			// Token: 0x04000CB0 RID: 3248
			private static AdomdConnection.XmlaMDSchema[] shilohSchemas = new AdomdConnection.XmlaMDSchema[]
			{
				new AdomdConnection.XmlaMDSchema("DBSCHEMA_CATALOGS", new Guid("{C8B52211-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DBSCHEMA_TABLES", new Guid("{C8B52229-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_TYPE", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DBSCHEMA_TABLES_INFO", new Guid("{C8B522E0-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_TYPE", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DBSCHEMA_COLUMNS", new Guid("{C8B52214-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TABLE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("COLUMN_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DBSCHEMA_PROVIDER_TYPES", new Guid("{C8B5222C-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("DATA_TYPE", typeof(ushort)),
					new AdomdConnection.XmlaMDSchemaRestriction("BEST_MATCH", typeof(bool))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_CUBES", new Guid("{C8B522D8-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_DIMENSIONS", new Guid("{C8B522D9-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("DIMENSION_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("DIMENSION_UNIQUE_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_HIERARCHIES", new Guid("{C8B522DA-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("DIMENSION_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("HIERARCHY_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("HIERARCHY_UNIQUE_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_LEVELS", new Guid("{C8B522DB-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("DIMENSION_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("HIERARCHY_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("LEVEL_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("LEVEL_UNIQUE_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_MEASURES", new Guid("{C8B522DC-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEASURE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEASURE_UNIQUE_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_PROPERTIES", new Guid("{C8B522DD-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("DIMENSION_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("HIERARCHY_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("LEVEL_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEMBER_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("PROPERTY_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("PROPERTY_TYPE", typeof(short))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_MEMBERS", new Guid("{C8B522DE-5CF3-11CE-ADE5-00AA0044773D}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("DIMENSION_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("HIERARCHY_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("LEVEL_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("LEVEL_NUMBER", typeof(uint)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEMBER_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEMBER_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEMBER_CAPTION", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MEMBER_TYPE", typeof(int)),
					new AdomdConnection.XmlaMDSchemaRestriction("TREE_OP", typeof(int))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_FUNCTIONS", new Guid("{A07CCD07-8148-11D0-87BB-00C04FC33942}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("LIBRARY_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("INTERFACE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("FUNCTION_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("ORIGIN", typeof(int)),
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_ACTIONS", new Guid("{A07CCD08-8148-11D0-87BB-00C04FC33942}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("ACTION_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("ACTION_TYPE", typeof(int)),
					new AdomdConnection.XmlaMDSchemaRestriction("COORDINATE", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("COORDINATE_TYPE", typeof(int)),
					new AdomdConnection.XmlaMDSchemaRestriction("INVOCATION", typeof(int))
				}),
				new AdomdConnection.XmlaMDSchema("MDSCHEMA_SETS", new Guid("{A07CCD0B-8148-11D0-87BB-00C04FC33942}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("CATALOG_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCHEMA_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("CUBE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SET_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SCOPE", typeof(int))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_SERVICES", new Guid("{3ADD8A95-D8B9-11D2-8D2A-00E029154FDE}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("SERVICE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SERVICE_TYPE_ID", typeof(uint))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_SERVICE_PARAMETERS", new Guid("{3ADD8A75-D8B9-11D2-8D2A-00E029154FDE}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("SERVICE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("PARAMETER_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_MODEL_CONTENT", new Guid("{3ADD8A76-D8B9-11D2-8D2A-00E029154FDE}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("ATTRIBUTE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("NODE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("NODE_UNIQUE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("NODE_TYPE", typeof(int)),
					new AdomdConnection.XmlaMDSchemaRestriction("NODE_GUID", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("NODE_CAPTION", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("TREE_OPERATION", typeof(uint))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_MODELS", new Guid("{3ADD8A77-D8B9-11D2-8D2A-00E029154FDE}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_TYPE", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SERVICE_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("SERVICE_TYPE_ID", typeof(uint))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_COLUMNS", new Guid("{3ADD8A78-D8B9-11D2-8D2A-00E029154FDE}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("COLUMN_NAME", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_MODEL_XML", new Guid("{4290B2D5-0E9C-4AA7-9369-98C95CFD9D13}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_TYPE", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DMSCHEMA_MINING_MODEL_PMML", new Guid("{4290B2D5-0E9C-4AA7-9369-98C95CFD9D13}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_CATALOG", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_SCHEMA", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_NAME", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("MODEL_TYPE", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DISCOVER_DATASOURCES", new Guid("{06C03D41-F66D-49F3-B1B8-987F7AF4CF18}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("DataSourceName", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("URL", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("ProviderName", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("ProviderType", typeof(string)),
					new AdomdConnection.XmlaMDSchemaRestriction("AuthenticationMode", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DISCOVER_PROPERTIES", new Guid("{4B40ADFB-8B09-4758-97BB-636E8AE97BCF}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("PropertyName", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DISCOVER_SCHEMA_ROWSETS", new Guid("{EEA0302B-7922-4992-8991-0E605D0E5593}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("SchemaName", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DISCOVER_ENUMERATORS", new Guid("{55A9E78B-ACCB-45B4-95A6-94C5065617A7}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("EnumName", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DISCOVER_KEYWORDS", new Guid("{1426C443-4CDD-4A40-8F45-572FAB9BBAA1}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("Keyword", typeof(string))
				}),
				new AdomdConnection.XmlaMDSchema("DISCOVER_LITERALS", new Guid("{C3EF5ECB-0A07-4665-A140-B075722DBDC2}"), new AdomdConnection.XmlaMDSchemaRestriction[]
				{
					new AdomdConnection.XmlaMDSchemaRestriction("LiteralName", typeof(string))
				})
			};

			// Token: 0x04000CB1 RID: 3249
			private static Hashtable schemasByGuid = new Hashtable(AdomdConnection.ShilohSchemas.shilohSchemas.Length);

			// Token: 0x04000CB2 RID: 3250
			private static Hashtable schemasByName = new Hashtable(AdomdConnection.ShilohSchemas.shilohSchemas.Length);
		}

		// Token: 0x020001A3 RID: 419
		internal struct XmlaMDSchema
		{
			// Token: 0x17000698 RID: 1688
			// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00043A74 File Offset: 0x00041C74
			internal string SchemaName
			{
				get
				{
					return this.schemaName;
				}
			}

			// Token: 0x17000699 RID: 1689
			// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00043A7C File Offset: 0x00041C7C
			internal Guid SchemaGuid
			{
				get
				{
					return this.schemaGuid;
				}
			}

			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x060012EA RID: 4842 RVA: 0x00043A84 File Offset: 0x00041C84
			internal AdomdConnection.XmlaMDSchemaRestriction[] Restrictions
			{
				get
				{
					return this.restictions;
				}
			}

			// Token: 0x060012EB RID: 4843 RVA: 0x00043A8C File Offset: 0x00041C8C
			internal XmlaMDSchema(string schemaName, Guid schemaGuid, AdomdConnection.XmlaMDSchemaRestriction[] restrictions)
			{
				this.schemaName = schemaName;
				this.schemaGuid = schemaGuid;
				this.restictions = restrictions;
			}

			// Token: 0x04000CB3 RID: 3251
			private string schemaName;

			// Token: 0x04000CB4 RID: 3252
			private Guid schemaGuid;

			// Token: 0x04000CB5 RID: 3253
			private AdomdConnection.XmlaMDSchemaRestriction[] restictions;
		}

		// Token: 0x020001A4 RID: 420
		internal struct XmlaMDSchemaRestriction
		{
			// Token: 0x1700069B RID: 1691
			// (get) Token: 0x060012EC RID: 4844 RVA: 0x00043AA3 File Offset: 0x00041CA3
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700069C RID: 1692
			// (get) Token: 0x060012ED RID: 4845 RVA: 0x00043AAB File Offset: 0x00041CAB
			internal Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x060012EE RID: 4846 RVA: 0x00043AB3 File Offset: 0x00041CB3
			internal XmlaMDSchemaRestriction(string name, Type type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x04000CB6 RID: 3254
			private string name;

			// Token: 0x04000CB7 RID: 3255
			private Type type;
		}
	}
}
