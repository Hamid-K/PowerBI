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
		// Token: 0x06000568 RID: 1384 RVA: 0x00020336 File Offset: 0x0001E536
		public AdomdConnection()
		{
			this.xmlaClientProvider = new AdomdConnection.XmlaClientProvider(this);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00020356 File Offset: 0x0001E556
		public AdomdConnection(string connectionString)
			: this()
		{
			this.ConnectionString = connectionString;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00020365 File Offset: 0x0001E565
		public AdomdConnection(AdomdConnection connection)
		{
			this.xmlaClientProvider = new AdomdConnection.XmlaClientProvider(this, connection.xmlaClientProvider);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0002038B File Offset: 0x0001E58B
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00020398 File Offset: 0x0001E598
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000203BA File Offset: 0x0001E5BA
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x000203C7 File Offset: 0x0001E5C7
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000203E9 File Offset: 0x0001E5E9
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000203F1 File Offset: 0x0001E5F1
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

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0002042A File Offset: 0x0001E62A
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00020432 File Offset: 0x0001E632
		public Func<AccessToken, AccessToken> OnAccessTokenExpired { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0002043B File Offset: 0x0001E63B
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0002046F File Offset: 0x0001E66F
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000204A3 File Offset: 0x0001E6A3
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000204D7 File Offset: 0x0001E6D7
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

		// Token: 0x06000577 RID: 1399 RVA: 0x0002050B File Offset: 0x0001E70B
		public AdomdTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00020518 File Offset: 0x0001E718
		public AdomdTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			AdomdUtils.CheckConnectionOpened(this);
			if (isolationLevel == IsolationLevel.ReadCommitted)
			{
				return new AdomdTransaction(this);
			}
			throw new NotSupportedException();
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00020534 File Offset: 0x0001E734
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00020544 File Offset: 0x0001E744
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00020604 File Offset: 0x0001E804
		[Browsable(false)]
		public int ConnectionTimeout
		{
			get
			{
				return this.XmlaClientProviderEx.ConnectionTimeout;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00020611 File Offset: 0x0001E811
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

		// Token: 0x0600057D RID: 1405 RVA: 0x00020640 File Offset: 0x0001E840
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

		// Token: 0x0600057E RID: 1406 RVA: 0x000206C8 File Offset: 0x0001E8C8
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000207DC File Offset: 0x0001E9DC
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

		// Token: 0x06000580 RID: 1408 RVA: 0x000207F6 File Offset: 0x0001E9F6
		public void Close()
		{
			this.Close(true);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00020800 File Offset: 0x0001EA00
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

		// Token: 0x06000582 RID: 1410 RVA: 0x0002084C File Offset: 0x0001EA4C
		public AdomdCommand CreateCommand()
		{
			return new AdomdCommand
			{
				Connection = this
			};
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002085C File Offset: 0x0001EA5C
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0002097C File Offset: 0x0001EB7C
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x000209A3 File Offset: 0x0001EBA3
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000209CC File Offset: 0x0001EBCC
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00020A03 File Offset: 0x0001EC03
		public AdomdPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00020A0C File Offset: 0x0001EC0C
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

		// Token: 0x06000589 RID: 1417 RVA: 0x00020A82 File Offset: 0x0001EC82
		public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
		{
			return this.GetSchemaDataSet(schemaName, null, restrictions, throwOnInlineErrors);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00020A8E File Offset: 0x0001EC8E
		public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
		{
			return this.GetSchemaDataSet(schemaName, schemaNamespace, restrictions, throwOnInlineErrors, null);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00020A9C File Offset: 0x0001EC9C
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

		// Token: 0x0600058C RID: 1420 RVA: 0x00020B21 File Offset: 0x0001ED21
		public DataSet GetSchemaDataSet(Guid schema, object[] restrictions)
		{
			return this.GetSchemaDataSet(schema, restrictions, true);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00020B2C File Offset: 0x0001ED2C
		public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions)
		{
			return this.GetSchemaDataSet(schemaName, null, restrictions, true);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00020B38 File Offset: 0x0001ED38
		public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions)
		{
			return this.GetSchemaDataSet(schemaName, schemaNamespace, restrictions, true);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00020B44 File Offset: 0x0001ED44
		public bool IsCloudAnalysisServices()
		{
			if (this.XmlaClientProviderEx.ConnectionInfo == null)
			{
				throw new InvalidOperationException(SR.Connection_ConnectionString_NotInitialized);
			}
			return this.XmlaClientProviderEx.ConnectionInfo.IsAsAzure || this.XmlaClientProviderEx.ConnectionInfo.IsPbiPremiumXmlaEp;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00020B82 File Offset: 0x0001ED82
		public CloudConnectionAuthenticationProperties GetCloudConnectionAuthenticationProperties()
		{
			if (!this.IsCloudAnalysisServices())
			{
				return null;
			}
			return this.XmlaClientProviderEx.ConnectionInfo.GetCloudConnectionAuthenticationPropertiesForPaaSInfrastructure(this);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00020B9F File Offset: 0x0001ED9F
		public void RefreshMetadata()
		{
			this.ClearMetadataCache();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00020BA7 File Offset: 0x0001EDA7
		public AdomdConnection Clone()
		{
			return new AdomdConnection(this);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00020BB0 File Offset: 0x0001EDB0
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

		// Token: 0x06000594 RID: 1428 RVA: 0x00020C10 File Offset: 0x0001EE10
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00020C18 File Offset: 0x0001EE18
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginTransaction();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00020C20 File Offset: 0x0001EE20
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginTransaction(isolationLevel);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00020C29 File Offset: 0x0001EE29
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17000162 RID: 354
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00020C31 File Offset: 0x0001EE31
		internal object OpenedReader
		{
			set
			{
				this.openedReader = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00020C3A File Offset: 0x0001EE3A
		internal bool UserOpened
		{
			get
			{
				return this.userOpened;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00020C42 File Offset: 0x0001EE42
		internal IDiscoverProvider IDiscoverProvider
		{
			get
			{
				return this.xmlaClientProvider;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00020C4A File Offset: 0x0001EE4A
		internal IExecuteProvider IExecuteProvider
		{
			get
			{
				return this.xmlaClientProvider;
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00020C52 File Offset: 0x0001EE52
		internal bool IsPostYukonProvider()
		{
			return this.XmlaClientProviderEx.IsPostYukonProvider();
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00020C5F File Offset: 0x0001EE5F
		internal object GetObjectData(SchemaObjectType schemaObjectType, string cubeName, string uniqueName)
		{
			return this.Cubes[cubeName].InternalGetSchemaObject(schemaObjectType, uniqueName);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00020C74 File Offset: 0x0001EE74
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

		// Token: 0x0600059F RID: 1439 RVA: 0x00020CBC File Offset: 0x0001EEBC
		private string GetProperty(string propName)
		{
			return this.GetProperty(propName, false);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00020CC6 File Offset: 0x0001EEC6
		private string GetProperty(string propName, bool sendNSCompatibility)
		{
			return this.XmlaClientProviderEx.GetPropertyFromServer(propName, sendNSCompatibility);
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00020CD5 File Offset: 0x0001EED5
		internal string CatalogConnectionStringProperty
		{
			get
			{
				return this.XmlaClientProviderEx.GetXmlaProperty("Catalog");
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		internal bool HasAutoSyncTimeElapsed(DateTime origTime, DateTime nowTime)
		{
			return this.AutoSyncPeriod > 0U && (nowTime - origTime).TotalMilliseconds > this.AutoSyncPeriod;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00020D1C File Offset: 0x0001EF1C
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

		// Token: 0x060005A4 RID: 1444 RVA: 0x00020D8C File Offset: 0x0001EF8C
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

		// Token: 0x060005A5 RID: 1445 RVA: 0x00020DDC File Offset: 0x0001EFDC
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

		// Token: 0x060005A6 RID: 1446 RVA: 0x00020E80 File Offset: 0x0001F080
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

		// Token: 0x060005A7 RID: 1447 RVA: 0x00020F44 File Offset: 0x0001F144
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

		// Token: 0x060005A8 RID: 1448 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		private DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, IDictionary adomdRestrictions, bool throwOnInlineErrors, IDictionary requestProperties)
		{
			DataSet rowsetDataset = this.XmlaClientProviderEx.Discover(schemaName, schemaNamespace, adomdRestrictions, throwOnInlineErrors ? InlineErrorHandlingType.Throw : InlineErrorHandlingType.StoreInErrorsCollection, requestProperties).RowsetDataset;
			AdomdConnection.XmlaMDSchemas.MungeMembersSchemaColumnNames(schemaName, schemaNamespace, rowsetDataset);
			return rowsetDataset;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00020FE8 File Offset: 0x0001F1E8
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

		// Token: 0x060005AA RID: 1450 RVA: 0x00021070 File Offset: 0x0001F270
		private void SetPropertiesForAfterConnectionOpen()
		{
			if (this.XmlaClientProviderEx.ConnectionInfo.AuthHandle != null)
			{
				this.properties.IsReadOnly = false;
				this.XmlaClientProviderEx.ConnectionInfo.AuthHandle.AddUserRelatedProperties(this.properties);
				this.properties.IsReadOnly = true;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000210C2 File Offset: 0x0001F2C2
		private uint AutoSyncPeriod
		{
			get
			{
				return this.XmlaClientProviderEx.AutoSyncPeriod;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000210D0 File Offset: 0x0001F2D0
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0002113C File Offset: 0x0001F33C
		private AdomdConnection.IXmlaClientProviderEx XmlaClientProviderEx
		{
			get
			{
				return this.xmlaClientProvider;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00021144 File Offset: 0x0001F344
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

		// Token: 0x060005AF RID: 1455 RVA: 0x0002119C File Offset: 0x0001F39C
		private void ResetInternalState()
		{
			this.ClearMetadataCache();
			this.providerVersion = null;
			this.serverVersion = null;
			this.XmlaClientProviderEx.ResetInternalState();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000211C0 File Offset: 0x0001F3C0
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

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002124C File Offset: 0x0001F44C
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

		// Token: 0x060005B2 RID: 1458 RVA: 0x000212A0 File Offset: 0x0001F4A0
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

		// Token: 0x060005B3 RID: 1459 RVA: 0x000213B4 File Offset: 0x0001F5B4
		private DataSet GetEmptyActionsDataSet(IDictionary restrictions)
		{
			if (this.cachedActionsDataSet == null)
			{
				RowsetFormatter rowsetFormatter = this.XmlaClientProviderEx.DiscoverSchema("MDSCHEMA_ACTIONS", restrictions, InlineErrorHandlingType.Throw);
				this.cachedActionsDataSet = rowsetFormatter.RowsetDataset;
			}
			return this.cachedActionsDataSet.Clone();
		}

		// Token: 0x04000405 RID: 1029
		private CubeCollection cubes;

		// Token: 0x04000406 RID: 1030
		private AdomdConnection.XmlaClientProvider xmlaClientProvider;

		// Token: 0x04000407 RID: 1031
		private AccessToken accessToken;

		// Token: 0x04000408 RID: 1032
		private MiningModelCollection miningModels;

		// Token: 0x04000409 RID: 1033
		private MiningStructureCollection miningStructures;

		// Token: 0x0400040A RID: 1034
		private MiningServiceCollection miningServices;

		// Token: 0x0400040B RID: 1035
		private string providerVersion;

		// Token: 0x0400040C RID: 1036
		private string serverVersion;

		// Token: 0x0400040D RID: 1037
		private AdomdConnection.XmlaMDSchemas schemasInfos;

		// Token: 0x0400040E RID: 1038
		private DataSet cachedActionsDataSet;

		// Token: 0x0400040F RID: 1039
		private string clientVersion;

		// Token: 0x04000410 RID: 1040
		private AdomdPropertyCollection properties = new AdomdPropertyCollection(true);

		// Token: 0x04000411 RID: 1041
		private const string propertyValueColumn = "Value";

		// Token: 0x04000412 RID: 1042
		private const string catalogPropertyName = "Catalog";

		// Token: 0x04000413 RID: 1043
		private const string providerType = "ProviderType";

		// Token: 0x04000414 RID: 1044
		private const string discoverDatasources = "DISCOVER_DATASOURCES";

		// Token: 0x04000415 RID: 1045
		private const string dataSourceInfo = "DataSourceInfo";

		// Token: 0x04000416 RID: 1046
		private const string discoverSchemaRowsets = "DISCOVER_SCHEMA_ROWSETS";

		// Token: 0x04000417 RID: 1047
		private const string schemaNameProp = "SchemaName";

		// Token: 0x04000418 RID: 1048
		private const string schemaGuidProp = "SchemaGuid";

		// Token: 0x04000419 RID: 1049
		private const string restrictionsProp = "Restrictions";

		// Token: 0x0400041A RID: 1050
		private const string nameProp = "Name";

		// Token: 0x0400041B RID: 1051
		private const string typeProp = "Type";

		// Token: 0x0400041C RID: 1052
		private const string actionsSchemaRowsetName = "MDSCHEMA_ACTIONS";

		// Token: 0x0400041D RID: 1053
		private const string actionsTypeRestrictionName = "ACTION_TYPE";

		// Token: 0x0400041E RID: 1054
		private const string actionsContentName = "CONTENT";

		// Token: 0x0400041F RID: 1055
		private const string showHiddenCubesPropery = "ShowHiddenCubes";

		// Token: 0x04000420 RID: 1056
		private const int MDACTION_TYPE_URL = 1;

		// Token: 0x04000421 RID: 1057
		private const int MDACTION_TYPE_HTML = 2;

		// Token: 0x04000422 RID: 1058
		private const int MDACTION_TYPE_COMMANDLINE = 32;

		// Token: 0x04000423 RID: 1059
		private const string multidimentionalProviderRestriction = "<MDP/>";

		// Token: 0x04000424 RID: 1060
		private object openedReader;

		// Token: 0x04000425 RID: 1061
		private bool userOpened;

		// Token: 0x0200019F RID: 415
		private interface IXmlaClientProviderEx
		{
			// Token: 0x0600126B RID: 4715
			RowsetFormatter Discover(string requestType, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling, bool sendNamespaceCompatibility);

			// Token: 0x0600126C RID: 4716
			RowsetFormatter Discover(string requestType, string requestnNamespace, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling, IDictionary requestProperties);

			// Token: 0x0600126D RID: 4717
			RowsetFormatter DiscoverSchema(string requestType, IDictionary restrictions, InlineErrorHandlingType inlineErrorHandling);

			// Token: 0x0600126E RID: 4718
			RowsetFormatter DiscoverWithCreateSession(string requestType, bool sendNamespaceCompatibility);

			// Token: 0x0600126F RID: 4719
			void CreateSession(bool sendNamespaceCompatibility);

			// Token: 0x06001270 RID: 4720
			void EndSession();

			// Token: 0x06001271 RID: 4721
			void ConnectXmla();

			// Token: 0x06001272 RID: 4722
			void Connect();

			// Token: 0x06001273 RID: 4723
			void Disconnect(bool endSession);

			// Token: 0x06001274 RID: 4724
			void CancelCommand(string sessionID);

			// Token: 0x06001275 RID: 4725
			string GetPropertyFromServer(string propName, bool sendNSCompatibility);

			// Token: 0x17000668 RID: 1640
			// (get) Token: 0x06001276 RID: 4726
			// (set) Token: 0x06001277 RID: 4727
			string ConnectionString { get; set; }

			// Token: 0x06001278 RID: 4728
			void SetXmlaProperty(string propertyName, string propertyValue);

			// Token: 0x06001279 RID: 4729
			string GetXmlaProperty(string propertyName);

			// Token: 0x0600127A RID: 4730
			void ResetSession();

			// Token: 0x17000669 RID: 1641
			// (get) Token: 0x0600127B RID: 4731
			bool IsExternalSession { get; }

			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x0600127C RID: 4732
			// (set) Token: 0x0600127D RID: 4733
			string SessionID { get; set; }

			// Token: 0x1700066B RID: 1643
			// (get) Token: 0x0600127E RID: 4734
			// (set) Token: 0x0600127F RID: 4735
			bool ShowHiddenObjects { get; set; }

			// Token: 0x1700066C RID: 1644
			// (get) Token: 0x06001280 RID: 4736
			int ConnectionTimeout { get; }

			// Token: 0x1700066D RID: 1645
			// (get) Token: 0x06001281 RID: 4737
			string ServerName { get; }

			// Token: 0x1700066E RID: 1646
			// (get) Token: 0x06001282 RID: 4738
			string InstanceName { get; }

			// Token: 0x1700066F RID: 1647
			// (get) Token: 0x06001283 RID: 4739
			ConnectTo ConnectTo { get; }

			// Token: 0x17000670 RID: 1648
			// (get) Token: 0x06001284 RID: 4740
			ConnectionType ConnectionType { get; }

			// Token: 0x17000671 RID: 1649
			// (get) Token: 0x06001285 RID: 4741
			ConnectionInfo ConnectionInfo { get; }

			// Token: 0x17000672 RID: 1650
			// (get) Token: 0x06001286 RID: 4742
			uint AutoSyncPeriod { get; }

			// Token: 0x17000673 RID: 1651
			// (get) Token: 0x06001287 RID: 4743
			SafetyOptions SafetyOptions { get; }

			// Token: 0x17000674 RID: 1652
			// (get) Token: 0x06001288 RID: 4744
			bool IsXmlaClientConnected { get; }

			// Token: 0x17000675 RID: 1653
			// (get) Token: 0x06001289 RID: 4745
			// (set) Token: 0x0600128A RID: 4746
			bool UseEU { get; set; }

			// Token: 0x17000676 RID: 1654
			// (get) Token: 0x0600128B RID: 4747
			string ServerVersion { get; }

			// Token: 0x17000677 RID: 1655
			// (get) Token: 0x0600128C RID: 4748
			string ProviderVersion { get; }

			// Token: 0x0600128D RID: 4749
			void ResetInternalState();

			// Token: 0x0600128E RID: 4750
			bool IsPostYukonProvider();

			// Token: 0x0600128F RID: 4751
			void MarkConnectionStringRestricted();
		}

		// Token: 0x020001A0 RID: 416
		private class XmlaClientProvider : IDiscoverProvider, IExecuteProvider, AdomdConnection.IXmlaClientProviderEx
		{
			// Token: 0x06001290 RID: 4752 RVA: 0x000407D4 File Offset: 0x0003E9D4
			private Dictionary<string, bool> GetTimeConversionMap(string discoverType)
			{
				Dictionary<string, bool> dictionary = null;
				if (AdomdConnection.XmlaClientProvider.localTimeConversionMap.TryGetValue(discoverType, out dictionary) && !this.IsPostYukonProvider)
				{
					dictionary = null;
				}
				return dictionary;
			}

			// Token: 0x06001291 RID: 4753 RVA: 0x00040800 File Offset: 0x0003EA00
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

			// Token: 0x06001292 RID: 4754 RVA: 0x0004093C File Offset: 0x0003EB3C
			internal XmlaClientProvider(AdomdConnection owner)
			{
				this.owner = owner;
			}

			// Token: 0x06001293 RID: 4755 RVA: 0x0004094B File Offset: 0x0003EB4B
			internal XmlaClientProvider(AdomdConnection owner, AdomdConnection.XmlaClientProvider provider)
				: this(owner, new ConnectionInfo(provider.connectionInfo))
			{
			}

			// Token: 0x06001294 RID: 4756 RVA: 0x0004095F File Offset: 0x0003EB5F
			internal XmlaClientProvider(AdomdConnection owner, ConnectionInfo info)
			{
				this.owner = owner;
				this.connectionInfo = info;
			}

			// Token: 0x06001295 RID: 4757 RVA: 0x00040978 File Offset: 0x0003EB78
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

			// Token: 0x06001296 RID: 4758 RVA: 0x00040A14 File Offset: 0x0003EC14
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

			// Token: 0x06001297 RID: 4759 RVA: 0x00040AB4 File Offset: 0x0003ECB4
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

			// Token: 0x06001298 RID: 4760 RVA: 0x00040B84 File Offset: 0x0003ED84
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

			// Token: 0x06001299 RID: 4761 RVA: 0x00040C20 File Offset: 0x0003EE20
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

			// Token: 0x0600129A RID: 4762 RVA: 0x00040C7C File Offset: 0x0003EE7C
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

			// Token: 0x0600129B RID: 4763 RVA: 0x00040CD8 File Offset: 0x0003EED8
			void AdomdConnection.IXmlaClientProviderEx.ConnectXmla()
			{
				this.Connect();
			}

			// Token: 0x0600129C RID: 4764 RVA: 0x00040CE0 File Offset: 0x0003EEE0
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

			// Token: 0x0600129D RID: 4765 RVA: 0x00040D70 File Offset: 0x0003EF70
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

			// Token: 0x0600129E RID: 4766 RVA: 0x00040DBC File Offset: 0x0003EFBC
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

			// Token: 0x0600129F RID: 4767 RVA: 0x00040E18 File Offset: 0x0003F018
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

			// Token: 0x060012A0 RID: 4768 RVA: 0x00040E6C File Offset: 0x0003F06C
			string AdomdConnection.IXmlaClientProviderEx.GetPropertyFromServer(string propName, bool sendNSCompatibility)
			{
				DataRowCollection rows = ((AdomdConnection.IXmlaClientProviderEx)this).Discover("DISCOVER_PROPERTIES", new ListDictionary { { "PropertyName", propName } }, InlineErrorHandlingType.StoreInCell, sendNSCompatibility).MainRowsetTable.Rows;
				if (rows.Count != 1)
				{
					throw new NotSupportedException(SR.Connection_InvalidProperty(propName));
				}
				return rows[0]["Value"].ToString();
			}

			// Token: 0x17000678 RID: 1656
			// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00040ECD File Offset: 0x0003F0CD
			// (set) Token: 0x060012A2 RID: 4770 RVA: 0x00040EE4 File Offset: 0x0003F0E4
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

			// Token: 0x060012A3 RID: 4771 RVA: 0x00040F64 File Offset: 0x0003F164
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
				if ((this.connectionInfo.IsPbiPremiumInternal || this.connectionInfo.IsPbiPremiumXmlaEp) && string.Compare(propertyName, "Catalog", StringComparison.InvariantCulture) == 0)
				{
					this.connectionInfo.PbipCoreServiceRoutingHint = null;
					this.connectionInfo.PbipWorkloadResourceMoniker = null;
				}
				this.discoverProperties = null;
				this.executeProperties = null;
			}

			// Token: 0x060012A4 RID: 4772 RVA: 0x00041000 File Offset: 0x0003F200
			string AdomdConnection.IXmlaClientProviderEx.GetXmlaProperty(string propertyName)
			{
				return this.connectionInfo.ExtendedProperties[propertyName] as string;
			}

			// Token: 0x060012A5 RID: 4773 RVA: 0x00041018 File Offset: 0x0003F218
			void AdomdConnection.IXmlaClientProviderEx.ResetSession()
			{
				this.client.SessionID = this.sessionID;
			}

			// Token: 0x17000679 RID: 1657
			// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004102B File Offset: 0x0003F22B
			bool AdomdConnection.IXmlaClientProviderEx.IsExternalSession
			{
				get
				{
					return this.sessionID != null;
				}
			}

			// Token: 0x060012A7 RID: 4775 RVA: 0x00041036 File Offset: 0x0003F236
			void AdomdConnection.IXmlaClientProviderEx.MarkConnectionStringRestricted()
			{
				if (this.connectionInfo != null)
				{
					this.connectionInfo.RestrictConnectionString();
				}
			}

			// Token: 0x1700067A RID: 1658
			// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0004104B File Offset: 0x0003F24B
			// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00041067 File Offset: 0x0003F267
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

			// Token: 0x1700067B RID: 1659
			// (get) Token: 0x060012AA RID: 4778 RVA: 0x00041070 File Offset: 0x0003F270
			// (set) Token: 0x060012AB RID: 4779 RVA: 0x00041078 File Offset: 0x0003F278
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

			// Token: 0x1700067C RID: 1660
			// (get) Token: 0x060012AC RID: 4780 RVA: 0x00041081 File Offset: 0x0003F281
			// (set) Token: 0x060012AD RID: 4781 RVA: 0x00041089 File Offset: 0x0003F289
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

			// Token: 0x1700067D RID: 1661
			// (get) Token: 0x060012AE RID: 4782 RVA: 0x000410A2 File Offset: 0x0003F2A2
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

			// Token: 0x1700067E RID: 1662
			// (get) Token: 0x060012AF RID: 4783 RVA: 0x000410BA File Offset: 0x0003F2BA
			string AdomdConnection.IXmlaClientProviderEx.ServerName
			{
				get
				{
					return this.connectionInfo.Server;
				}
			}

			// Token: 0x1700067F RID: 1663
			// (get) Token: 0x060012B0 RID: 4784 RVA: 0x000410C7 File Offset: 0x0003F2C7
			string AdomdConnection.IXmlaClientProviderEx.InstanceName
			{
				get
				{
					return this.connectionInfo.InstanceName;
				}
			}

			// Token: 0x17000680 RID: 1664
			// (get) Token: 0x060012B1 RID: 4785 RVA: 0x000410D4 File Offset: 0x0003F2D4
			ConnectTo AdomdConnection.IXmlaClientProviderEx.ConnectTo
			{
				get
				{
					return this.connectionInfo.ConnectTo;
				}
			}

			// Token: 0x17000681 RID: 1665
			// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000410E1 File Offset: 0x0003F2E1
			ConnectionType AdomdConnection.IXmlaClientProviderEx.ConnectionType
			{
				get
				{
					return this.connectionInfo.ConnectionType;
				}
			}

			// Token: 0x17000682 RID: 1666
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x000410EE File Offset: 0x0003F2EE
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

			// Token: 0x17000683 RID: 1667
			// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00041109 File Offset: 0x0003F309
			SafetyOptions AdomdConnection.IXmlaClientProviderEx.SafetyOptions
			{
				get
				{
					return this.connectionInfo.SafetyOptions;
				}
			}

			// Token: 0x17000684 RID: 1668
			// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00041116 File Offset: 0x0003F316
			private bool IsXmlaClientConnected
			{
				get
				{
					return this.client != null && this.client.IsConnected;
				}
			}

			// Token: 0x17000685 RID: 1669
			// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0004112D File Offset: 0x0003F32D
			bool AdomdConnection.IXmlaClientProviderEx.IsXmlaClientConnected
			{
				get
				{
					return this.IsXmlaClientConnected;
				}
			}

			// Token: 0x17000686 RID: 1670
			// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00041135 File Offset: 0x0003F335
			string AdomdConnection.IXmlaClientProviderEx.ServerVersion
			{
				get
				{
					return this.ServerVersion;
				}
			}

			// Token: 0x17000687 RID: 1671
			// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0004113D File Offset: 0x0003F33D
			string AdomdConnection.IXmlaClientProviderEx.ProviderVersion
			{
				get
				{
					return this.ProviderVersion;
				}
			}

			// Token: 0x060012B9 RID: 4793 RVA: 0x00041145 File Offset: 0x0003F345
			void AdomdConnection.IXmlaClientProviderEx.ResetInternalState()
			{
				this.serverVersionObject = null;
				this.serverVersion = null;
				this.providerVersion = null;
			}

			// Token: 0x060012BA RID: 4794 RVA: 0x0004115C File Offset: 0x0003F35C
			bool AdomdConnection.IXmlaClientProviderEx.IsPostYukonProvider()
			{
				return this.IsPostYukonProvider;
			}

			// Token: 0x17000688 RID: 1672
			// (get) Token: 0x060012BB RID: 4795 RVA: 0x00041164 File Offset: 0x0003F364
			ConnectionInfo AdomdConnection.IXmlaClientProviderEx.ConnectionInfo
			{
				get
				{
					return this.connectionInfo;
				}
			}

			// Token: 0x17000689 RID: 1673
			// (get) Token: 0x060012BC RID: 4796 RVA: 0x0004116C File Offset: 0x0003F36C
			// (set) Token: 0x060012BD RID: 4797 RVA: 0x00041183 File Offset: 0x0003F383
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

			// Token: 0x060012BE RID: 4798 RVA: 0x0004119C File Offset: 0x0003F39C
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

			// Token: 0x060012BF RID: 4799 RVA: 0x00041218 File Offset: 0x0003F418
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

			// Token: 0x060012C0 RID: 4800 RVA: 0x000412F0 File Offset: 0x0003F4F0
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

			// Token: 0x060012C1 RID: 4801 RVA: 0x0004138C File Offset: 0x0003F58C
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

			// Token: 0x060012C2 RID: 4802 RVA: 0x000414C8 File Offset: 0x0003F6C8
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

			// Token: 0x060012C3 RID: 4803 RVA: 0x00041600 File Offset: 0x0003F800
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

			// Token: 0x060012C4 RID: 4804 RVA: 0x00041804 File Offset: 0x0003FA04
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

			// Token: 0x060012C5 RID: 4805 RVA: 0x0004190C File Offset: 0x0003FB0C
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

			// Token: 0x060012C6 RID: 4806 RVA: 0x000419F0 File Offset: 0x0003FBF0
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

			// Token: 0x1700068A RID: 1674
			// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00041A2C File Offset: 0x0003FC2C
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

			// Token: 0x1700068B RID: 1675
			// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00041B10 File Offset: 0x0003FD10
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

			// Token: 0x060012C9 RID: 4809 RVA: 0x00041BDC File Offset: 0x0003FDDC
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

			// Token: 0x060012CA RID: 4810 RVA: 0x00041C54 File Offset: 0x0003FE54
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

			// Token: 0x060012CB RID: 4811 RVA: 0x00041D08 File Offset: 0x0003FF08
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

			// Token: 0x060012CC RID: 4812 RVA: 0x00041DC8 File Offset: 0x0003FFC8
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

			// Token: 0x060012CD RID: 4813 RVA: 0x00041ED0 File Offset: 0x000400D0
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

			// Token: 0x1700068C RID: 1676
			// (get) Token: 0x060012CE RID: 4814 RVA: 0x00041F45 File Offset: 0x00040145
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

			// Token: 0x1700068D RID: 1677
			// (get) Token: 0x060012CF RID: 4815 RVA: 0x00041F6C File Offset: 0x0004016C
			private bool IsPostYukonProvider
			{
				get
				{
					return AdomdUtils.IsPostYukonVersion(this.ServerVersionObject);
				}
			}

			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00041F7C File Offset: 0x0004017C
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

			// Token: 0x1700068F RID: 1679
			// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00041FCC File Offset: 0x000401CC
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

			// Token: 0x04000C78 RID: 3192
			private const string contentProperty = "Content";

			// Token: 0x04000C79 RID: 3193
			private const string dataContent = "Data";

			// Token: 0x04000C7A RID: 3194
			private const string schemaContent = "Schema";

			// Token: 0x04000C7B RID: 3195
			private const string schemaDataContent = "SchemaData";

			// Token: 0x04000C7C RID: 3196
			private const string metadataContent = "Metadata";

			// Token: 0x04000C7D RID: 3197
			private const string noneContent = "None";

			// Token: 0x04000C7E RID: 3198
			private const string axisFormatProperty = "AxisFormat";

			// Token: 0x04000C7F RID: 3199
			private const string tupleFormat = "TupleFormat";

			// Token: 0x04000C80 RID: 3200
			private const string formatProperty = "Format";

			// Token: 0x04000C81 RID: 3201
			private const string tabularFormat = "Tabular";

			// Token: 0x04000C82 RID: 3202
			private const string multidimensionalFormat = "Multidimensional";

			// Token: 0x04000C83 RID: 3203
			private const string nativeFormat = "Native";

			// Token: 0x04000C84 RID: 3204
			private const string beginRange = "BeginRange";

			// Token: 0x04000C85 RID: 3205
			private const string endRange = "EndRange";

			// Token: 0x04000C86 RID: 3206
			private const string executionModeProperty = "ExecutionMode";

			// Token: 0x04000C87 RID: 3207
			private const string prepare = "Prepare";

			// Token: 0x04000C88 RID: 3208
			private const string providerVersionProp = "ProviderVersion";

			// Token: 0x04000C89 RID: 3209
			private const string serverVersionProp = "DBMSVersion";

			// Token: 0x04000C8A RID: 3210
			private const string propertyNameRest = "PropertyName";

			// Token: 0x04000C8B RID: 3211
			private const string discoverPropertiesType = "DISCOVER_PROPERTIES";

			// Token: 0x04000C8C RID: 3212
			private const int PFE_MDX_CREATE_LOCAL_CUBE = -1056309049;

			// Token: 0x04000C8D RID: 3213
			private const string fileNameStr = "FILENAME|";

			// Token: 0x04000C8E RID: 3214
			private const string ddlStr = "|DDL|";

			// Token: 0x04000C8F RID: 3215
			private static readonly XmlaPropertyKey ContentPropertyXmlaKey = new XmlaPropertyKey("Content", null);

			// Token: 0x04000C90 RID: 3216
			private static readonly Dictionary<string, Dictionary<string, bool>> localTimeConversionMap = new Dictionary<string, Dictionary<string, bool>>(4);

			// Token: 0x04000C91 RID: 3217
			private AdomdConnection owner;

			// Token: 0x04000C92 RID: 3218
			private ListDictionary discoverProperties;

			// Token: 0x04000C93 RID: 3219
			private ListDictionary executeProperties;

			// Token: 0x04000C94 RID: 3220
			private ConnectionInfo connectionInfo;

			// Token: 0x04000C95 RID: 3221
			private string sessionID;

			// Token: 0x04000C96 RID: 3222
			private bool showHiddenObjects;

			// Token: 0x04000C97 RID: 3223
			private string originalConnectionStringShowHiddenCubePropertyValue;

			// Token: 0x04000C98 RID: 3224
			private XmlaClient client;

			// Token: 0x04000C99 RID: 3225
			private Version serverVersionObject;

			// Token: 0x04000C9A RID: 3226
			private string providerVersion;

			// Token: 0x04000C9B RID: 3227
			private string serverVersion;

			// Token: 0x02000240 RID: 576
			private class StringCommandContentProvider : ICommandContentProvider
			{
				// Token: 0x06001599 RID: 5529 RVA: 0x00048359 File Offset: 0x00046559
				internal StringCommandContentProvider(string commandText, bool isMdx)
				{
					this.commandText = commandText;
					this.isMdx = isMdx;
				}

				// Token: 0x1700074F RID: 1871
				// (get) Token: 0x0600159A RID: 5530 RVA: 0x00048381 File Offset: 0x00046581
				Stream ICommandContentProvider.CommandStream
				{
					get
					{
						return null;
					}
				}

				// Token: 0x17000750 RID: 1872
				// (get) Token: 0x0600159B RID: 5531 RVA: 0x00048384 File Offset: 0x00046584
				string ICommandContentProvider.CommandText
				{
					get
					{
						return this.commandText;
					}
				}

				// Token: 0x17000751 RID: 1873
				// (get) Token: 0x0600159C RID: 5532 RVA: 0x0004838C File Offset: 0x0004658C
				bool ICommandContentProvider.IsContentMdx
				{
					get
					{
						return this.isMdx;
					}
				}

				// Token: 0x04000F5C RID: 3932
				private string commandText = string.Empty;

				// Token: 0x04000F5D RID: 3933
				private bool isMdx = true;
			}
		}

		// Token: 0x020001A1 RID: 417
		internal class XmlaMDSchemas
		{
			// Token: 0x060012D2 RID: 4818 RVA: 0x00041FEE File Offset: 0x000401EE
			internal XmlaMDSchemas(RowsetFormatter formatter)
			{
				this.PopulateSchemasInfos(formatter);
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x00041FFD File Offset: 0x000401FD
			internal XmlaMDSchemas()
			{
				this.schemasByGuid = AdomdConnection.ShilohSchemas.SchemasByGuid;
				this.schemasByName = AdomdConnection.ShilohSchemas.SchemasByName;
			}

			// Token: 0x060012D4 RID: 4820 RVA: 0x0004201B File Offset: 0x0004021B
			internal AdomdConnection.XmlaMDSchema GetSchemaInfo(Guid schemaGuid)
			{
				if (!this.schemasByGuid.ContainsKey(schemaGuid))
				{
					throw new ArgumentException(SR.Schema_InvalidGuid, "schemaGuid");
				}
				return (AdomdConnection.XmlaMDSchema)this.schemasByGuid[schemaGuid];
			}

			// Token: 0x060012D5 RID: 4821 RVA: 0x00042058 File Offset: 0x00040258
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

			// Token: 0x060012D6 RID: 4822 RVA: 0x00042290 File Offset: 0x00040490
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

			// Token: 0x060012D7 RID: 4823 RVA: 0x00042368 File Offset: 0x00040568
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

			// Token: 0x04000C9C RID: 3228
			private Hashtable schemasByName;

			// Token: 0x04000C9D RID: 3229
			private Hashtable schemasByGuid;

			// Token: 0x04000C9E RID: 3230
			private const string MembersSchemaName = "MDSCHEMA_MEMBERS";
		}

		// Token: 0x020001A2 RID: 418
		private static class ShilohSchemas
		{
			// Token: 0x060012D8 RID: 4824 RVA: 0x0004240C File Offset: 0x0004060C
			static ShilohSchemas()
			{
				foreach (AdomdConnection.XmlaMDSchema xmlaMDSchema in AdomdConnection.ShilohSchemas.shilohSchemas)
				{
					AdomdConnection.ShilohSchemas.schemasByGuid[xmlaMDSchema.SchemaGuid] = xmlaMDSchema;
					AdomdConnection.ShilohSchemas.schemasByName[xmlaMDSchema.SchemaName] = xmlaMDSchema;
				}
			}

			// Token: 0x17000690 RID: 1680
			// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004352A File Offset: 0x0004172A
			internal static Hashtable SchemasByGuid
			{
				get
				{
					return AdomdConnection.ShilohSchemas.schemasByGuid;
				}
			}

			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x060012DA RID: 4826 RVA: 0x00043531 File Offset: 0x00041731
			internal static Hashtable SchemasByName
			{
				get
				{
					return AdomdConnection.ShilohSchemas.schemasByName;
				}
			}

			// Token: 0x04000C9F RID: 3231
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

			// Token: 0x04000CA0 RID: 3232
			private static Hashtable schemasByGuid = new Hashtable(AdomdConnection.ShilohSchemas.shilohSchemas.Length);

			// Token: 0x04000CA1 RID: 3233
			private static Hashtable schemasByName = new Hashtable(AdomdConnection.ShilohSchemas.shilohSchemas.Length);
		}

		// Token: 0x020001A3 RID: 419
		internal struct XmlaMDSchema
		{
			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x060012DB RID: 4827 RVA: 0x00043538 File Offset: 0x00041738
			internal string SchemaName
			{
				get
				{
					return this.schemaName;
				}
			}

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x060012DC RID: 4828 RVA: 0x00043540 File Offset: 0x00041740
			internal Guid SchemaGuid
			{
				get
				{
					return this.schemaGuid;
				}
			}

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x060012DD RID: 4829 RVA: 0x00043548 File Offset: 0x00041748
			internal AdomdConnection.XmlaMDSchemaRestriction[] Restrictions
			{
				get
				{
					return this.restictions;
				}
			}

			// Token: 0x060012DE RID: 4830 RVA: 0x00043550 File Offset: 0x00041750
			internal XmlaMDSchema(string schemaName, Guid schemaGuid, AdomdConnection.XmlaMDSchemaRestriction[] restrictions)
			{
				this.schemaName = schemaName;
				this.schemaGuid = schemaGuid;
				this.restictions = restrictions;
			}

			// Token: 0x04000CA2 RID: 3234
			private string schemaName;

			// Token: 0x04000CA3 RID: 3235
			private Guid schemaGuid;

			// Token: 0x04000CA4 RID: 3236
			private AdomdConnection.XmlaMDSchemaRestriction[] restictions;
		}

		// Token: 0x020001A4 RID: 420
		internal struct XmlaMDSchemaRestriction
		{
			// Token: 0x17000695 RID: 1685
			// (get) Token: 0x060012DF RID: 4831 RVA: 0x00043567 File Offset: 0x00041767
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000696 RID: 1686
			// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0004356F File Offset: 0x0004176F
			internal Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x060012E1 RID: 4833 RVA: 0x00043577 File Offset: 0x00041777
			internal XmlaMDSchemaRestriction(string name, Type type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x04000CA5 RID: 3237
			private string name;

			// Token: 0x04000CA6 RID: 3238
			private Type type;
		}
	}
}
