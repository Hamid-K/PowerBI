using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000EB RID: 235
	[XmlRoot(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine")]
	[Guid("7EC085CF-4008-45FF-9346-64F1D1E44E12")]
	public abstract class Server : MajorObject, IMajorObject, INamedComponent, IModelComponent, IComponent, IDisposable, IConnectivityOwner
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00031AA9 File Offset: 0x0002FCA9
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00031AB1 File Offset: 0x0002FCB1
		[XmlIgnore]
		public AccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
			set
			{
				AccessToken.ValidateTokenInput(value);
				if (this.analysisServicesClient.ConnectionInfo != null && this.analysisServicesClient.ConnectionInfo.Password != null)
				{
					throw new ArgumentException(RuntimeSR.NonRefreshableToken_AlreadyPresented);
				}
				this.accessToken = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00031AEA File Offset: 0x0002FCEA
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00031AF2 File Offset: 0x0002FCF2
		[XmlIgnore]
		public Func<AccessToken, AccessToken> OnAccessTokenExpired { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00031AFB File Offset: 0x0002FCFB
		Database IMajorObject.ParentDatabase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00031AFE File Offset: 0x0002FCFE
		Server IMajorObject.ParentServer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00031B01 File Offset: 0x0002FD01
		void IMajorObject.WriteRef(XmlWriter writer)
		{
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00031B03 File Offset: 0x0002FD03
		Type IMajorObject.BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00031B0B File Offset: 0x0002FD0B
		void IMajorObject.CreateBody()
		{
			base.CreateBody();
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00031B13 File Offset: 0x0002FD13
		string IMajorObject.Path
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00031B1A File Offset: 0x0002FD1A
		IObjectReference IMajorObject.ObjectReference
		{
			get
			{
				return this.GetObjectReference();
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00031B22 File Offset: 0x0002FD22
		bool IMajorObject.DependsOn(IMajorObject obj)
		{
			return false;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00031B25 File Offset: 0x0002FD25
		internal override IObjectReference GetObjectReference()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00031B2C File Offset: 0x0002FD2C
		internal override Type GetBaseType()
		{
			return typeof(Server);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00031B38 File Offset: 0x0002FD38
		protected internal override void CopyTo(MajorObject destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Server server = destination as Server;
			if (server == null)
			{
				throw new ArgumentException(SR.Copy_InvalidDestination, "destination");
			}
			base.CopyTo(destination, forceBodyLoading);
			if (!base.IsLoaded && !forceBodyLoading)
			{
				return;
			}
			server.Version = this.Version;
			server.Edition = this.Edition;
			server.EditionID = this.EditionID;
			server.ProductLevel = this.ProductLevel;
			server.ProductName = this.ProductName;
			server.ServerMode = this.ServerMode;
			server.DefaultCompatibilityLevel = this.DefaultCompatibilityLevel;
			server.SupportedCompatibilityLevels = this.SupportedCompatibilityLevels;
			server.ServerLocation = this.ServerLocation;
			server.CompatibilityMode = this.CompatibilityMode;
			this.ServerProperties.CopyTo(server.ServerProperties);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00031C0A File Offset: 0x0002FE0A
		internal Server()
		{
			this.captureLog = new StringCollection();
			this.analysisServicesClient = new AnalysisServicesClient(this, this.captureLog);
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00031C3A File Offset: 0x0002FE3A
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x00031C42 File Offset: 0x0002FE42
		internal ITransaction CurrentTransaction { get; set; }

		// Token: 0x06000E92 RID: 3730
		internal abstract bool AnyTMDatabaseHasLocalChanges();

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00031C4B File Offset: 0x0002FE4B
		[XmlIgnore]
		public string ConnectionString
		{
			get
			{
				if (this.analysisServicesClient.ConnectionInfo != null)
				{
					return this.analysisServicesClient.ConnectionInfo.ConnectionString;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00031C70 File Offset: 0x0002FE70
		[XmlIgnore]
		public ConnectionInfo ConnectionInfo
		{
			get
			{
				return this.analysisServicesClient.ConnectionInfo;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00031C7D File Offset: 0x0002FE7D
		[XmlIgnore]
		public string SessionID
		{
			get
			{
				if (this.analysisServicesClient != null)
				{
					return this.analysisServicesClient.SessionID;
				}
				return null;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00031C94 File Offset: 0x0002FE94
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00031CA1 File Offset: 0x0002FEA1
		[XmlIgnore]
		[Browsable(false)]
		public bool CaptureXml
		{
			get
			{
				return this.analysisServicesClient.CaptureXml;
			}
			set
			{
				this.analysisServicesClient.CaptureXml = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00031CAF File Offset: 0x0002FEAF
		[XmlIgnore]
		[Browsable(false)]
		public StringCollection CaptureLog
		{
			get
			{
				return this.captureLog;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00031CB7 File Offset: 0x0002FEB7
		[XmlIgnore]
		[Browsable(false)]
		internal XmlaClient Connection
		{
			get
			{
				return this.analysisServicesClient;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00031CBF File Offset: 0x0002FEBF
		[XmlIgnore]
		[Browsable(false)]
		public bool Connected
		{
			get
			{
				return this.analysisServicesClient.IsConnected;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00031CCC File Offset: 0x0002FECC
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00031CD9 File Offset: 0x0002FED9
		[XmlElement(IsNullable = false)]
		[ReadOnly(true)]
		public string Version
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().Version;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().Version = Utils.Trim(value);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00031CEC File Offset: 0x0002FEEC
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x00031CF9 File Offset: 0x0002FEF9
		[ReadOnly(true)]
		public ServerEdition Edition
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().Edition;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().Edition = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00031D07 File Offset: 0x0002FF07
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x00031D14 File Offset: 0x0002FF14
		[ReadOnly(true)]
		[DefaultValue(-1L)]
		public long EditionID
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().EditionID;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().EditionID = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00031D22 File Offset: 0x0002FF22
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x00031D2F File Offset: 0x0002FF2F
		[XmlElement(IsNullable = false)]
		[ReadOnly(true)]
		public string ProductLevel
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().ProductLevel;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().ProductLevel = Utils.Trim(value);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00031D42 File Offset: 0x0002FF42
		[XmlArray]
		public ServerPropertyCollection ServerProperties
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().Properties;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00031D4F File Offset: 0x0002FF4F
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00031D5C File Offset: 0x0002FF5C
		[XmlElement(IsNullable = false)]
		public string ProductName
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().ProductName;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().ProductName = Utils.Trim(value);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00031D70 File Offset: 0x0002FF70
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00031D9E File Offset: 0x0002FF9E
		[ReadOnly(true)]
		[DefaultValue(CompatibilityMode.Unknown)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2019/engine/900")]
		public CompatibilityMode CompatibilityMode
		{
			get
			{
				Server.ServerBodyBase body = base.GetBody<Server.ServerBodyBase>();
				if (body.CompatibilityMode != CompatibilityMode.Unknown)
				{
					return body.CompatibilityMode;
				}
				if (this.Connected)
				{
					return CompatibilityMode.AnalysisServices;
				}
				return CompatibilityMode.PowerBI;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().CompatibilityMode = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00031DAC File Offset: 0x0002FFAC
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00031DDE File Offset: 0x0002FFDE
		[ReadOnly(true)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2011/engine/300")]
		public ServerMode ServerMode
		{
			get
			{
				Server.ServerBodyBase body = base.GetBody<Server.ServerBodyBase>();
				if (body.ServerMode == ServerMode.Default && this.Connected)
				{
					body.ServerMode = ServerMode.Multidimensional;
				}
				return body.ServerMode;
			}
			set
			{
				base.GetBody<Server.ServerBodyBase>().ServerMode = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00031DEC File Offset: 0x0002FFEC
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00031E2B File Offset: 0x0003002B
		[ReadOnly(true)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2012/engine/400")]
		public int DefaultCompatibilityLevel
		{
			get
			{
				Server.ServerBodyBase body = base.GetBody<Server.ServerBodyBase>();
				if (body.DefaultCompatibilityLevel != 0 || !this.Connected)
				{
					return body.DefaultCompatibilityLevel;
				}
				if (body.ServerMode == ServerMode.Default)
				{
					return 1050;
				}
				return 1100;
			}
			private set
			{
				base.GetBody<Server.ServerBodyBase>().DefaultCompatibilityLevel = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00031E3C File Offset: 0x0003003C
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00031F2E File Offset: 0x0003012E
		[ReadOnly(true)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2013/engine/600")]
		public string SupportedCompatibilityLevels
		{
			get
			{
				Server.ServerBodyBase body = base.GetBody<Server.ServerBodyBase>();
				if (!string.IsNullOrEmpty(body.SupportedCompatibilityLevels) || !this.Connected)
				{
					return body.SupportedCompatibilityLevels;
				}
				if (body.ServerMode == ServerMode.Default)
				{
					return 1050.ToString();
				}
				if (this.DefaultCompatibilityLevel < 1100)
				{
					return 1050.ToString();
				}
				if (this.DefaultCompatibilityLevel < 1103)
				{
					return string.Format("{0},{1}", 1050, 1100);
				}
				if (this.DefaultCompatibilityLevel < 1200)
				{
					return string.Format("{0},{1},{2}", 1050, 1100, 1103);
				}
				return string.Format("{0},{1},{2}", 1100, 1103, 1200);
			}
			private set
			{
				base.GetBody<Server.ServerBodyBase>().SupportedCompatibilityLevels = value;
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00031F3C File Offset: 0x0003013C
		public int[] GetSupportedCompatibilityLevels()
		{
			if (this.supportedCompatibilityLevels == null)
			{
				if (string.IsNullOrEmpty(base.GetBody<Server.ServerBodyBase>().SupportedCompatibilityLevels))
				{
					return new int[0];
				}
				string[] array = this.SupportedCompatibilityLevels.Split(new char[] { ',' });
				this.supportedCompatibilityLevels = new List<int>();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					int num;
					if (int.TryParse(array2[i], out num))
					{
						this.supportedCompatibilityLevels.Add(num);
					}
				}
			}
			return this.supportedCompatibilityLevels.ToArray();
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00031FBD File Offset: 0x000301BD
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x00031FCA File Offset: 0x000301CA
		[ReadOnly(true)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2012/engine/400")]
		public ServerLocation ServerLocation
		{
			get
			{
				return base.GetBody<Server.ServerBodyBase>().ServerLocation;
			}
			private set
			{
				base.GetBody<Server.ServerBodyBase>().ServerLocation = value;
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00031FD8 File Offset: 0x000301D8
		internal override void RefreshMajorChildren(RefreshType type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00031FDF File Offset: 0x000301DF
		public Server CopyTo(Server obj)
		{
			this.CopyTo(obj, true);
			return obj;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00031FEA File Offset: 0x000301EA
		public void Connect(string connectionString, bool propertiesOnly)
		{
			this.Connect(connectionString, null, propertiesOnly ? ObjectExpansion.ObjectProperties : ObjectExpansion.Partial);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00031FFB File Offset: 0x000301FB
		public void Connect(string connectionString)
		{
			this.Connect(connectionString, null);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00032005 File Offset: 0x00030205
		public void Connect(string connectionString, string sessionId)
		{
			this.Connect(connectionString, sessionId, ObjectExpansion.Partial);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00032010 File Offset: 0x00030210
		private void Connect(string connectionString, string sessionId, ObjectExpansion expansionType)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.transactionsCount = 0;
				if (sessionId != null)
				{
					this.analysisServicesClient.Connect(connectionString, sessionId);
				}
				else
				{
					this.analysisServicesClient.Connect(connectionString, true);
				}
				try
				{
					this.CreateSessionTrace();
					base.ResetBody();
					base.Refresh(expansionType);
					this.analysisServicesClient.ConnectionInfo.RestrictConnectionString();
				}
				catch
				{
					this.Disconnect(sessionId == null);
					throw;
				}
			}
		}

		// Token: 0x06000EB7 RID: 3767
		internal abstract void CreateSessionTrace();

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000320B0 File Offset: 0x000302B0
		public void Disconnect()
		{
			bool flag = this.analysisServicesClient.ConnectionInfo != null && this.analysisServicesClient.ConnectionInfo.ConnectionType != ConnectionType.LocalServer;
			this.Disconnect(flag);
		}

		// Token: 0x06000EB9 RID: 3769
		public abstract void Disconnect(bool endSession);

		// Token: 0x06000EBA RID: 3770 RVA: 0x000320EC File Offset: 0x000302EC
		public void Reconnect()
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.transactionsCount = 0;
				this.analysisServicesClient.Reconnect();
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00032138 File Offset: 0x00030338
		public ConnectionState GetConnectionState(bool pingServer)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			ConnectionState connectionState;
			lock (analysisServicesClient)
			{
				connectionState = this.analysisServicesClient.GetConnectionState(pingServer);
			}
			return connectionState;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00032180 File Offset: 0x00030380
		public XmlaResultCollection Execute(string command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlaResultCollection xmlaResultCollection;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(true);
				xmlaResultCollection = this.analysisServicesClient.Execute(command);
			}
			return xmlaResultCollection;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000321E0 File Offset: 0x000303E0
		public XmlaResultCollection Execute(string command, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (impactResult == null)
			{
				throw new ArgumentNullException("impactResult");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlaResultCollection xmlaResultCollection;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(true);
				if (this.CaptureXml)
				{
					if (analyzeImpactOnly)
					{
						this.analysisServicesClient.Execute(command);
					}
					xmlaResultCollection = null;
				}
				else
				{
					int count = impactResult.Count;
					XmlaResultCollection xmlaResultCollection2 = this.analysisServicesClient.Execute(command, impactResult);
					if (!xmlaResultCollection2.ContainsErrors)
					{
						this.ResolveImpactReferences(impactResult, count);
						if (!analyzeImpactOnly)
						{
							xmlaResultCollection2 = this.analysisServicesClient.Execute(command);
						}
					}
					xmlaResultCollection = xmlaResultCollection2;
				}
			}
			return xmlaResultCollection;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00032298 File Offset: 0x00030498
		public XmlaResultCollection ExecuteCaptureLog(bool transactional, bool parallel)
		{
			return this.ExecuteCaptureLog(transactional, parallel, false, false);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000322A4 File Offset: 0x000304A4
		public XmlaResultCollection ExecuteCaptureLog(bool transactional, bool parallel, bool processAffected)
		{
			return this.ExecuteCaptureLog(transactional, parallel, processAffected, false);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000322B0 File Offset: 0x000304B0
		public XmlaResultCollection ExecuteCaptureLog(bool transactional, bool parallel, bool processAffected, bool skipVolatileObjects)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlaResultCollection xmlaResultCollection;
			lock (analysisServicesClient)
			{
				bool captureXml = this.CaptureXml;
				try
				{
					this.CaptureXml = false;
					this.CheckIfConnected(false);
					xmlaResultCollection = this.analysisServicesClient.Execute(this.CaptureLog, transactional, parallel, processAffected, skipVolatileObjects);
				}
				finally
				{
					this.CaptureXml = captureXml;
				}
			}
			return xmlaResultCollection;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003232C File Offset: 0x0003052C
		public string ConcatenateCaptureLog(bool transactional, bool parallel)
		{
			return this.ConcatenateCaptureLog(transactional, parallel, false);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00032338 File Offset: 0x00030538
		public string ConcatenateCaptureLog(bool transactional, bool parallel, bool processAffected)
		{
			StringCollection stringCollection = this.CaptureLog;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<Batch xmlns='http://schemas.microsoft.com/analysisservices/2003/engine' Transaction='" + XmlConvert.ToString(transactional) + "'>");
			if (parallel)
			{
				stringBuilder.AppendLine("<Parallel>");
			}
			int i = 0;
			int count = stringCollection.Count;
			while (i < count)
			{
				stringBuilder.AppendLine(stringCollection[i]);
				i++;
			}
			if (parallel)
			{
				stringBuilder.AppendLine("</Parallel>");
			}
			stringBuilder.AppendLine("</Batch>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x000323C0 File Offset: 0x000305C0
		public XmlWriter StartXmlaRequest(XmlaRequestType type)
		{
			this.CheckIfConnected(false);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlWriter xmlWriter;
			lock (analysisServicesClient)
			{
				string soapActionHeader = this.GetSoapActionHeader(type);
				xmlWriter = this.analysisServicesClient.StartRequest(soapActionHeader);
			}
			return xmlWriter;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00032418 File Offset: 0x00030618
		public XmlReader EndXmlaRequest()
		{
			this.CheckIfConnected(false);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlReader readerToReturnToPublic;
			lock (analysisServicesClient)
			{
				readerToReturnToPublic = XmlaClient.GetReaderToReturnToPublic(this.analysisServicesClient.EndRequest());
			}
			return readerToReturnToPublic;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003246C File Offset: 0x0003066C
		public XmlaResultCollection EndXmlaRequestAndGetResults()
		{
			XmlaResultCollection xmlaResultCollection;
			using (XmlReader xmlReader = this.EndXmlaRequest())
			{
				xmlaResultCollection = XmlaClient.ReadResponse(xmlReader, true, false);
			}
			return xmlaResultCollection;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x000324A8 File Offset: 0x000306A8
		public XmlReader SendXmlaRequest(XmlaRequestType type, Stream requestStream)
		{
			this.CheckIfConnected(false);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlReader readerToReturnToPublic;
			lock (analysisServicesClient)
			{
				string soapActionHeader = this.GetSoapActionHeader(type);
				readerToReturnToPublic = XmlaClient.GetReaderToReturnToPublic(this.analysisServicesClient.SendRequest(soapActionHeader, requestStream));
			}
			return readerToReturnToPublic;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00032508 File Offset: 0x00030708
		public XmlReader SendXmlaRequest(XmlaRequestType type, TextReader request)
		{
			this.CheckIfConnected(false);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			XmlReader readerToReturnToPublic;
			lock (analysisServicesClient)
			{
				string soapActionHeader = this.GetSoapActionHeader(type);
				readerToReturnToPublic = XmlaClient.GetReaderToReturnToPublic(this.analysisServicesClient.SendRequest(soapActionHeader, request));
			}
			return readerToReturnToPublic;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00032568 File Offset: 0x00030768
		public AmoDataReader EndXmlaRequestWithReader(out XmlaResultCollection results)
		{
			XmlReader xmlReader = null;
			AmoDataReader amoDataReader;
			try
			{
				xmlReader = this.EndXmlaRequest();
				if (this.CaptureXml)
				{
					results = null;
					amoDataReader = null;
				}
				else
				{
					XmlaResult xmlaResult = XmlaClient.ReadToXmlaResponse(xmlReader);
					if (xmlaResult != null)
					{
						results = new XmlaResultCollection();
						results.Add(xmlaResult);
						amoDataReader = null;
					}
					else
					{
						AmoDataReader amoDataReader2 = null;
						try
						{
							amoDataReader2 = new AmoDataReader(xmlReader);
						}
						catch (OperationException ex)
						{
							results = ex.Results;
							return null;
						}
						results = null;
						xmlReader = null;
						amoDataReader = amoDataReader2;
					}
				}
			}
			finally
			{
				if (xmlReader != null)
				{
					((IDisposable)xmlReader).Dispose();
				}
			}
			return amoDataReader;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x000325F4 File Offset: 0x000307F4
		public AmoDataReader ExecuteReader(string command, out XmlaResultCollection results, IDictionary properties = null, bool wrapCommand = true)
		{
			return this.ExecuteReaderImpl(command, XmlaClient.GetCommandFormat(command), out results, properties, wrapCommand);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00032608 File Offset: 0x00030808
		internal AmoDataReader ExecuteReaderImpl(string command, XmlaCommandFormat format, out XmlaResultCollection results, IDictionary properties, bool wrapCommand)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			AmoDataReader amoDataReader;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				if (wrapCommand)
				{
					this.analysisServicesClient.WriteStartCommand(ref dictionary);
				}
				this.analysisServicesClient.WriteCommandTextImpl(command, format);
				if (wrapCommand)
				{
					if (properties != null && dictionary != null)
					{
						foreach (object obj in properties.Keys)
						{
							dictionary[obj] = properties[obj];
						}
					}
					this.analysisServicesClient.WriteEndCommand(false, dictionary);
				}
				this.analysisServicesClient.EndMessage();
				amoDataReader = this.EndXmlaRequestWithReader(out results);
			}
			return amoDataReader;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000326FC File Offset: 0x000308FC
		private string GetSoapActionHeader(XmlaRequestType type)
		{
			switch (type)
			{
			case XmlaRequestType.Undefined:
				return null;
			case XmlaRequestType.Discover:
				return "SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"";
			case XmlaRequestType.Execute:
				return "SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"";
			default:
				return null;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00032724 File Offset: 0x00030924
		private void Refresh(IMajorObject obj, ObjectExpansion expansion)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				JaXmlSerializer serializer = this.GetSerializer(true, true);
				MajorObject majorObject = this.analysisServicesClient.Discover(obj, expansion, serializer);
				if (majorObject == null)
				{
					throw new AmoException(SR.ObjectDoesNotExistOrYouDontHaveReadPermissions);
				}
				if (!majorObject.IsLoaded)
				{
					majorObject.CreateBody();
				}
				try
				{
					((MajorObject)obj).internalState = MajorObjectState.Loading;
					majorObject.CopyTo((MajorObject)obj, false);
				}
				finally
				{
					((MajorObject)obj).internalState = MajorObjectState.Ready;
				}
				IContainer container = ((ModelComponent)obj).Container;
				if (container != null)
				{
					((ModelComponent)obj).AddToContainer(container);
				}
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000327E8 File Offset: 0x000309E8
		internal void Delete(IMajorObject obj, ImpactDetailCollection impactResult, bool ignoreFailures)
		{
			int num = ((impactResult == null) ? 0 : impactResult.Count);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.analysisServicesClient.Delete(obj, impactResult, ignoreFailures);
			}
			if (impactResult != null)
			{
				this.ResolveImpactReferences(impactResult, num);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00032848 File Offset: 0x00030A48
		private void Update(IMajorObject obj, UpdateOptions options, UpdateMode mode, XmlaWarningCollection warnings, ImpactDetailCollection impactResult)
		{
			int num = ((impactResult == null) ? 0 : impactResult.Count);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				bool flag2 = (options & UpdateOptions.ExpandFull) > UpdateOptions.Default;
				bool flag3 = (options & UpdateOptions.AlterDependents) > UpdateOptions.Default;
				ObjectExpansion objectExpansion = (flag2 ? ObjectExpansion.Full : ObjectExpansion.Partial);
				JaXmlSerializer serializer = this.GetSerializer(objectExpansion);
				if (flag2)
				{
					obj.Refresh(true, RefreshType.UnloadedObjectsOnly);
				}
				else if (!obj.IsLoaded)
				{
					obj.Refresh();
				}
				if (flag3)
				{
					ImpactDetailCollection impactDetailCollection = new ImpactDetailCollection();
					this.Update(obj, options ^ UpdateOptions.AlterDependents, mode, warnings, impactDetailCollection);
					ArrayList invalidObjects = impactDetailCollection.GetInvalidObjects();
					foreach (object obj2 in invalidObjects)
					{
						((IMajorObject)obj2).Refresh(true, RefreshType.UnloadedObjectsOnly);
					}
					this.analysisServicesClient.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
					IDictionary dictionary = null;
					this.analysisServicesClient.WriteStartCommand(ref dictionary);
					this.analysisServicesClient.WriteStartBatch(true, false, false);
					switch (mode)
					{
					case UpdateMode.Default:
						this.analysisServicesClient.WriteAlter(obj, objectExpansion, true, serializer);
						break;
					case UpdateMode.Update:
						this.analysisServicesClient.WriteAlter(obj, objectExpansion, false, serializer);
						break;
					case UpdateMode.Create:
						this.analysisServicesClient.WriteCreate(obj, objectExpansion, false, serializer);
						break;
					case UpdateMode.CreateOrReplace:
						this.analysisServicesClient.WriteCreate(obj, objectExpansion, true, serializer);
						break;
					default:
						throw new NotImplementedException();
					}
					IMajorObject[] array = new IMajorObject[invalidObjects.Count];
					invalidObjects.CopyTo(array);
					foreach (IMajorObject majorObject in this.OrderDependentObjects(array))
					{
						if (majorObject != obj)
						{
							this.analysisServicesClient.WriteAlter(majorObject, ObjectExpansion.Full, false, this.GetSerializer(ObjectExpansion.Full));
						}
					}
					this.analysisServicesClient.WriteEndBatch();
					this.analysisServicesClient.WriteEndCommand(impactResult != null, dictionary);
					this.analysisServicesClient.EndMessage();
					this.analysisServicesClient.CopyXmlaWarnings(this.analysisServicesClient.SendExecuteAndReadResponse(impactResult, true, true), warnings);
				}
				else
				{
					switch (mode)
					{
					case UpdateMode.Default:
						this.analysisServicesClient.Alter(obj, objectExpansion, impactResult, true, warnings, serializer);
						break;
					case UpdateMode.Update:
						this.analysisServicesClient.Alter(obj, objectExpansion, impactResult, false, warnings, serializer);
						break;
					case UpdateMode.Create:
						this.analysisServicesClient.Create((IMajorObject)obj.Parent, obj, objectExpansion, impactResult, false, warnings, serializer);
						break;
					case UpdateMode.CreateOrReplace:
						this.analysisServicesClient.Create((IMajorObject)obj.Parent, obj, objectExpansion, impactResult, true, warnings, serializer);
						break;
					default:
						throw new NotImplementedException();
					}
				}
				if (impactResult != null)
				{
					this.ResolveImpactReferences(impactResult, num);
				}
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00032B28 File Offset: 0x00030D28
		internal void RefreshDatabaseID(Database database)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { { "CATALOG_NAME", database.Name } };
			using (AmoDataReader amoDataReader = new AmoDataReader(XmlaClient.GetReaderToReturnToPublic(this.analysisServicesClient.Discover("DBSCHEMA_CATALOGS", dictionary))))
			{
				DataSet dataSet = new DataSet();
				using (AmoDataAdapter amoDataAdapter = new AmoDataAdapter(amoDataReader))
				{
					amoDataAdapter.Fill(dataSet);
				}
				if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new AmoException(SR.ObjectDoesNotExistOrYouDontHaveReadPermissions);
				}
				int ordinal = dataSet.Tables[0].Columns["DATABASE_ID"].Ordinal;
				string text = (string)dataSet.Tables[0].Rows[0][ordinal];
				if (database.ID != text)
				{
					ModelComponentCollection owningCollection = database.OwningCollection;
					owningCollection.Remove(database);
					database.ID = text;
					owningCollection.Add(database);
				}
			}
		}

		// Token: 0x06000ED0 RID: 3792
		internal abstract IMajorObject[] OrderDependentObjects(IMajorObject[] objects);

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00032C58 File Offset: 0x00030E58
		protected void RenameTable(string databaseId, string tableId, string name, FixUpExpressions fixupExpressions)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.analysisServicesClient.RenameTable(databaseId, tableId, name, fixupExpressions);
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00032CA4 File Offset: 0x00030EA4
		protected void RenameTableColumn(string databaseId, string tableId, string columnId, string name, FixUpExpressions fixupExpressions)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.analysisServicesClient.RenameTableColumn(databaseId, tableId, columnId, name, fixupExpressions);
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00032CF0 File Offset: 0x00030EF0
		protected void RenameScriptMeasure(string databaseId, string tableId, string measureName, string name, FixUpExpressions fixupExpressions)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.analysisServicesClient.RenameScriptMeasure(databaseId, tableId, measureName, name, fixupExpressions);
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00032D3C File Offset: 0x00030F3C
		internal void Process(IMajorObject obj, ProcessType processType, IBinding source, ErrorConfiguration errorConfig, WriteBackTableCreation writebackOption, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				if (this.CaptureXml)
				{
					this.analysisServicesClient.Process(obj, processType, source, errorConfig, writebackOption, null, null, this.GetSerializer(true, true));
				}
				else if (analyzeImpactOnly)
				{
					if (impactResult != null)
					{
						int count = impactResult.Count;
						this.analysisServicesClient.Process(obj, processType, source, errorConfig, writebackOption, impactResult, warnings, this.GetSerializer(true, true));
						this.ResolveImpactReferences(impactResult, count);
					}
				}
				else
				{
					ImpactDetailCollection impactDetailCollection = ((impactResult == null) ? new ImpactDetailCollection() : impactResult);
					int count2 = impactDetailCollection.Count;
					this.analysisServicesClient.Process(obj, processType, source, errorConfig, writebackOption, impactDetailCollection, null, this.GetSerializer(true, true));
					this.ResolveImpactReferences(impactDetailCollection, count2);
					this.analysisServicesClient.Process(obj, processType, source, errorConfig, writebackOption, null, warnings, this.GetSerializer(true, true));
					int i = count2;
					int count3 = impactDetailCollection.Count;
					while (i < count3)
					{
						ProcessableMajorObject processableMajorObject = impactDetailCollection[i].Object as ProcessableMajorObject;
						if (processableMajorObject != null)
						{
							this.analysisServicesClient.RefreshStateAndLastProcessed(processableMajorObject);
						}
						i++;
					}
				}
			}
		}

		// Token: 0x06000ED5 RID: 3797
		internal abstract void SendBatch(ArrayList objectsToDelete, ArrayList objectToAlter);

		// Token: 0x06000ED6 RID: 3798
		internal abstract JaXmlSerializer GetSerializer(ObjectExpansion objectExpansion);

		// Token: 0x06000ED7 RID: 3799
		internal abstract JaXmlSerializer GetSerializer(bool writeMajorChildren, bool writeReadOnlyProperties);

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00032E84 File Offset: 0x00031084
		internal static void SendCreate(IModelComponent parent, IMajorObject obj)
		{
			if (obj == null || parent == null || ((MajorObject)obj).InternalState == MajorObjectState.Loading)
			{
				return;
			}
			if (!obj.IsLoaded)
			{
				obj.CreateBody();
			}
			Server parentServer = ((IMajorObject)parent).ParentServer;
			if (parentServer != null && parentServer.Connected)
			{
				parentServer.pendingCreations[obj] = parent;
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00032ED8 File Offset: 0x000310D8
		internal static void SendUpdate(IMajorObject obj, UpdateOptions options, UpdateMode mode, XmlaWarningCollection warnings, ImpactDetailCollection impactResult)
		{
			if (obj is Server && !((Server)obj).CaptureXml && (options & UpdateOptions.ExpandFull) != UpdateOptions.Default)
			{
				throw new InvalidOperationException(SR.Server_CannotUpdateFull);
			}
			Server parentServer = obj.ParentServer;
			if (parentServer == null || (!parentServer.Connected && !parentServer.CaptureXml))
			{
				throw new InvalidOperationException(SR.MajorObject_Update_NoConnectedParentServer(obj.GetType().Name, obj.Name));
			}
			parentServer.Update(obj, options, mode, warnings, impactResult);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00032F4C File Offset: 0x0003114C
		internal static void SendRefresh(IMajorObject obj, ObjectExpansion expansion)
		{
			if (((MajorObject)obj).InternalState != MajorObjectState.Ready)
			{
				return;
			}
			Server parentServer = obj.ParentServer;
			if (obj == parentServer)
			{
				parentServer.CheckIfConnected(false);
			}
			else if (parentServer == null || !parentServer.Connected)
			{
				throw new InvalidOperationException(SR.MajorObject_Refresh_NoConnectedParentServer(obj.GetType().Name, obj.Name));
			}
			parentServer.Refresh(obj, expansion);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00032FAC File Offset: 0x000311AC
		internal static void SendProcess(IMajorObject obj, ProcessType processType, IBinding source, ErrorConfiguration errorConfig, WriteBackTableCreation writebackOption, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly)
		{
			Server parentServer = obj.ParentServer;
			if (parentServer == null || !parentServer.Connected)
			{
				throw new InvalidOperationException(SR.MajorObject_Process_NoConnectedParentServer(obj.GetType().Name, obj.Name));
			}
			parentServer.Process(obj, processType, source, errorConfig, writebackOption, warnings, impactResult, analyzeImpactOnly);
		}

		// Token: 0x06000EDC RID: 3804
		internal abstract void ResolveImpactReferences(ImpactDetailCollection impacts, int startIndex);

		// Token: 0x06000EDD RID: 3805
		internal abstract MajorObject GetReferenceMajorObject(IObjectReference objectReference);

		// Token: 0x06000EDE RID: 3806 RVA: 0x00032FF9 File Offset: 0x000311F9
		public void CancelCommand()
		{
			this.CancelCommand(this.SessionID);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00033008 File Offset: 0x00031208
		public void CancelCommand(string sessionId)
		{
			if (sessionId == null || sessionId.Length == 0)
			{
				throw new ArgumentException(XmlaSR.Cancel_SessionIDNotSpecified);
			}
			this.CheckIfConnected(false);
			XmlaClient xmlaClient = new XmlaClient(this);
			xmlaClient.Connect(new ConnectionInfo(this.ConnectionInfo)
			{
				IsLightweightConnection = true
			}, false);
			try
			{
				xmlaClient.CancelCommand(sessionId);
			}
			finally
			{
				xmlaClient.Disconnect(false);
				xmlaClient = null;
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00033078 File Offset: 0x00031278
		public void CancelSession()
		{
			this.CancelSession(this.SessionID, false);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00033087 File Offset: 0x00031287
		public void CancelSession(string sessionId)
		{
			this.CancelSession(sessionId, false);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00033094 File Offset: 0x00031294
		public void CancelSession(string sessionId, bool cancelAssociated)
		{
			if (sessionId == null || sessionId.Length == 0)
			{
				throw new ArgumentException(XmlaSR.Cancel_SessionIDNotSpecified);
			}
			this.CheckIfConnected(false);
			XmlaClient xmlaClient = new XmlaClient(this);
			xmlaClient.Connect(this.ConnectionInfo, false);
			try
			{
				xmlaClient.CancelSession(sessionId, cancelAssociated);
			}
			finally
			{
				xmlaClient.Disconnect(false);
				xmlaClient = null;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000330F8 File Offset: 0x000312F8
		public void CancelSession(int sessionId)
		{
			this.CancelSession(sessionId, false);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00033104 File Offset: 0x00031304
		public void CancelSession(int sessionId, bool cancelAssociated)
		{
			this.CheckIfConnected(false);
			XmlaClient xmlaClient = new XmlaClient(this);
			xmlaClient.Connect(this.ConnectionInfo, false);
			try
			{
				xmlaClient.CancelSession(sessionId, cancelAssociated);
			}
			finally
			{
				xmlaClient.Disconnect(false);
				xmlaClient = null;
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00033150 File Offset: 0x00031350
		public void CancelConnection(int connectionId)
		{
			this.CancelConnection(connectionId, false);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003315C File Offset: 0x0003135C
		public void CancelConnection(int connectionId, bool cancelAssociated)
		{
			this.CheckIfConnected(false);
			XmlaClient xmlaClient = new XmlaClient(this);
			xmlaClient.Connect(this.ConnectionInfo, false);
			try
			{
				xmlaClient.CancelConnection(connectionId, cancelAssociated);
			}
			finally
			{
				xmlaClient.Disconnect(false);
				xmlaClient = null;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000331A8 File Offset: 0x000313A8
		internal void Backup(Database database, string file, bool allowOverwrite, bool backupRemotePartitions, BackupLocation[] locations, bool applyCompression, string password)
		{
			if (this.Edition == ServerEdition.LocalCube || this.Edition == ServerEdition.LocalCube64)
			{
				throw new AmoException(ValidationSR.Server_BackupNotAllowed);
			}
			Utils.CheckValidPath(file);
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.Backup(database, file, allowOverwrite, backupRemotePartitions, locations, applyCompression, password);
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00033224 File Offset: 0x00031424
		internal void Backup(Database database, BackupInfo backupInfo)
		{
			if (this.Edition == ServerEdition.LocalCube || this.Edition == ServerEdition.LocalCube64)
			{
				throw new AmoException(ValidationSR.Server_BackupNotAllowed);
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.Backup(database, backupInfo.File, backupInfo.AllowOverwrite, backupInfo.BackupRemotePartitions, backupInfo.Locations, backupInfo.ApplyCompression, backupInfo.Password);
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000332B4 File Offset: 0x000314B4
		public void Restore(string file)
		{
			this.Restore(file, null, false, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x000332D0 File Offset: 0x000314D0
		public void Restore(string file, string databaseName)
		{
			this.Restore(file, databaseName, false, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000332EC File Offset: 0x000314EC
		public void Restore(string file, string databaseName, bool allowOverwrite)
		{
			this.Restore(file, databaseName, allowOverwrite, null, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00033308 File Offset: 0x00031508
		public void Restore(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations)
		{
			this.Restore(file, databaseName, allowOverwrite, locations, RestoreSecurity.CopyAll, null, null, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00033328 File Offset: 0x00031528
		public void Restore(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security)
		{
			this.Restore(file, databaseName, allowOverwrite, locations, security, null, null, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00033348 File Offset: 0x00031548
		public void Restore(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password)
		{
			this.Restore(file, databaseName, allowOverwrite, locations, security, password, null, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00033368 File Offset: 0x00031568
		public void Restore(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password, string dbStorageLocation)
		{
			this.Restore(file, databaseName, allowOverwrite, locations, security, password, dbStorageLocation, ReadWriteMode.ReadWrite, false);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00033388 File Offset: 0x00031588
		public void Restore(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password, string dbStorageLocation, ReadWriteMode readWriteMode)
		{
			this.Restore(file, databaseName, allowOverwrite, locations, security, password, dbStorageLocation, readWriteMode, false);
		}

		// Token: 0x06000EF1 RID: 3825
		internal abstract string TryLookupDatabaseId(string databaseName);

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000333AC File Offset: 0x000315AC
		public void Restore(string file, string databaseName, bool allowOverwrite, RestoreLocation[] locations, RestoreSecurity security, string password, string dbStorageLocation, ReadWriteMode readWriteMode, bool ignoreIncompatibilities)
		{
			if (this.Edition == ServerEdition.LocalCube || this.Edition == ServerEdition.LocalCube64)
			{
				throw new AmoException(ValidationSR.Server_RestoreNotAllowed);
			}
			Utils.CheckValidPath(file);
			databaseName = Utils.Trim(databaseName);
			string text;
			if (databaseName != null && !Utils.IsSyntacticallyValidName(databaseName, typeof(Database), out text))
			{
				throw new ArgumentException(text, "databaseName");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				string text2 = this.TryLookupDatabaseId(databaseName);
				this.analysisServicesClient.Restore(file, databaseName, text2, allowOverwrite, locations, security, password, dbStorageLocation, readWriteMode, ignoreIncompatibilities, false);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00033460 File Offset: 0x00031660
		public void Restore(RestoreInfo restoreInfo)
		{
			if (this.Edition == ServerEdition.LocalCube || this.Edition == ServerEdition.LocalCube64)
			{
				throw new AmoException(ValidationSR.Server_RestoreNotAllowed);
			}
			if (restoreInfo == null)
			{
				throw new ArgumentNullException("restoreInfo");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				string text = this.TryLookupDatabaseId(restoreInfo.DatabaseName);
				this.analysisServicesClient.Restore(restoreInfo.File, restoreInfo.DatabaseName, text, restoreInfo.AllowOverwrite, restoreInfo.Locations, restoreInfo.Security, restoreInfo.Password, restoreInfo.DbStorageLocation, restoreInfo.DatabaseReadWriteMode, restoreInfo.IgnoreIncompatibilities, restoreInfo.ForceRestore);
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00033524 File Offset: 0x00031724
		public void Synchronize(string databaseID, string source)
		{
			if (string.IsNullOrEmpty(databaseID))
			{
				throw new ArgumentNullException("databaseID");
			}
			if (string.IsNullOrEmpty(source))
			{
				throw new ArgumentNullException("source");
			}
			this.SynchronizeImpl(databaseID, source, SynchronizeSecurity.SkipMembership, false);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00033556 File Offset: 0x00031756
		public void Synchronize(string databaseID, string source, SynchronizeSecurity synchronizeSecurity, bool applyCompression)
		{
			if (string.IsNullOrEmpty(databaseID))
			{
				throw new ArgumentNullException("databaseID");
			}
			if (string.IsNullOrEmpty(source))
			{
				throw new ArgumentNullException("source");
			}
			this.SynchronizeImpl(databaseID, source, synchronizeSecurity, applyCompression);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003358C File Offset: 0x0003178C
		public void Synchronize(SynchronizeInfo synchronizeInfo)
		{
			if (synchronizeInfo == null)
			{
				throw new ArgumentNullException("synchronizeInfo");
			}
			if (string.IsNullOrEmpty(synchronizeInfo.DatabaseID))
			{
				throw new ArgumentNullException("synchronizeInfo.DatabaseID");
			}
			if (string.IsNullOrEmpty(synchronizeInfo.Source))
			{
				throw new ArgumentNullException("synchronizeInfo.Source");
			}
			this.SynchronizeImpl(synchronizeInfo.DatabaseID, synchronizeInfo.Source, synchronizeInfo.SynchronizeSecurity, synchronizeInfo.ApplyCompression);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000335F8 File Offset: 0x000317F8
		private void SynchronizeImpl(string databaseID, string source, SynchronizeSecurity synchronizeSecurity, bool applyCompression)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.Synchronize(databaseID, source, synchronizeSecurity, applyCompression);
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003364C File Offset: 0x0003184C
		internal void Export(Database database, ExportLayout exportLayout, ExportType exportType)
		{
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.Export(database, exportLayout, exportType);
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000336AC File Offset: 0x000318AC
		[Browsable(false)]
		internal DateTime GetLastSchemaUpdate(IMajorObject obj)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			DateTime lastSchemaUpdate;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				lastSchemaUpdate = this.analysisServicesClient.GetLastSchemaUpdate(obj);
			}
			return lastSchemaUpdate;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000336FC File Offset: 0x000318FC
		public void ImageLoad(ImageLoadInfo imageLoadInfo)
		{
			if (imageLoadInfo == null)
			{
				throw new ArgumentNullException("imageLoadInfo");
			}
			if (imageLoadInfo.SourceDbStream == null)
			{
				throw new ArgumentNullException("SourceDbStream");
			}
			if (string.IsNullOrEmpty(imageLoadInfo.DatabaseId))
			{
				throw new ArgumentNullException("DatabaseId");
			}
			if (string.IsNullOrEmpty(imageLoadInfo.DatabaseName))
			{
				throw new ArgumentNullException("DatabaseName");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.ImageLoad(imageLoadInfo.DatabaseName, imageLoadInfo.DatabaseId, imageLoadInfo.SourceDbStream, false, imageLoadInfo.DatabaseReadWriteMode.ToString());
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000337C0 File Offset: 0x000319C0
		public void ImageLoad(string databaseName, string databaseId, Stream sourceDbStream, ReadWriteMode databaseReadWriteMode)
		{
			this.ImageLoad(new ImageLoadInfo
			{
				DatabaseName = databaseName,
				DatabaseId = databaseId,
				SourceDbStream = sourceDbStream,
				DatabaseReadWriteMode = databaseReadWriteMode
			});
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x000337EA File Offset: 0x000319EA
		public void ImageLoad(string databaseName, string databaseId, Stream sourceDbStream)
		{
			this.ImageLoad(new ImageLoadInfo
			{
				DatabaseName = databaseName,
				DatabaseId = databaseId,
				SourceDbStream = sourceDbStream
			});
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003380C File Offset: 0x00031A0C
		public void ImageSave(ImageSaveInfo imageSaveInfo)
		{
			if (imageSaveInfo == null)
			{
				throw new ArgumentNullException("imageSaveInfo");
			}
			if (imageSaveInfo.TargetDbStream == null)
			{
				throw new ArgumentNullException("TargetDbStream");
			}
			if (string.IsNullOrEmpty(imageSaveInfo.DatabaseId))
			{
				throw new ArgumentNullException("DatabaseId");
			}
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.ImageSave(imageSaveInfo.DatabaseId, imageSaveInfo.TargetDbStream);
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000338A0 File Offset: 0x00031AA0
		public void ImageSave(string databaseId, Stream targetDbStream)
		{
			this.ImageSave(new ImageSaveInfo
			{
				DatabaseId = databaseId,
				TargetDbStream = targetDbStream
			});
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000338BB File Offset: 0x00031ABB
		internal bool IsInTransactionInternal()
		{
			return this.transactionsCount > 0;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x000338C8 File Offset: 0x00031AC8
		internal virtual void BeginTransactionInternal()
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.BeginTransaction();
				this.transactionsCount++;
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00033924 File Offset: 0x00031B24
		internal virtual void RollbackTransactionInternal()
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				try
				{
					this.CheckIfConnected(false);
					this.analysisServicesClient.RollbackTransaction();
					this.transactionsCount = 0;
				}
				catch (OperationException ex)
				{
					bool flag2 = false;
					foreach (object obj in ((IEnumerable)ex.Results))
					{
						foreach (object obj2 in ((IEnumerable)((XmlaResult)obj).Messages))
						{
							XmlaMessage xmlaMessage = (XmlaMessage)obj2;
							if (xmlaMessage is XmlaError && ((XmlaError)xmlaMessage).ErrorCode == -1056178164)
							{
								flag2 = true;
								break;
							}
						}
					}
					if (this.transactionsCount <= 0 || !flag2)
					{
						throw;
					}
					this.transactionsCount = 0;
				}
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00033A4C File Offset: 0x00031C4C
		internal virtual void CommitTransactionInternal()
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.CommitTransaction();
				if (this.transactionsCount > 0)
				{
					this.transactionsCount--;
				}
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00033AB0 File Offset: 0x00031CB0
		internal virtual void CancelTransactionInternal()
		{
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00033AB2 File Offset: 0x00031CB2
		public void Attach(string folder)
		{
			this.Attach(folder, ReadWriteMode.ReadWrite, null);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00033ABD File Offset: 0x00031CBD
		public void Attach(string folder, ReadWriteMode readWriteMode)
		{
			this.Attach(folder, readWriteMode, null);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00033AC8 File Offset: 0x00031CC8
		public void Attach(string folder, ReadWriteMode readWriteMode, string password)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.Attach(folder, readWriteMode, password);
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00033B18 File Offset: 0x00031D18
		internal void Detach(Database db, string password)
		{
			AnalysisServicesClient analysisServicesClient = this.analysisServicesClient;
			lock (analysisServicesClient)
			{
				this.CheckIfConnected(false);
				this.analysisServicesClient.Detach(db, password);
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00033B68 File Offset: 0x00031D68
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Disconnect();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00033B98 File Offset: 0x00031D98
		public override bool Validate(ValidationErrorCollection errors, bool includeDetailedErrors, ServerEdition serverEdition)
		{
			if (!base.Validate(errors, includeDetailedErrors, serverEdition))
			{
				return false;
			}
			int count = errors.Count;
			this.ValidateAssemblies(errors, serverEdition);
			return count == errors.Count;
		}

		// Token: 0x06000F0A RID: 3850
		internal abstract void ValidateAssemblies(ValidationErrorCollection errors, ServerEdition serverEdition);

		// Token: 0x06000F0B RID: 3851
		internal abstract void CheckIfConnected(bool offlineCaptureXmlAllowed = false);

		// Token: 0x06000F0C RID: 3852
		internal abstract IEnumerable<Database> GetDatabases();

		// Token: 0x06000F0D RID: 3853 RVA: 0x00033BC0 File Offset: 0x00031DC0
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
				throw new ConnectionException(RuntimeSR.TokenRefreshFailure, ex);
			}
			if (!flag)
			{
				throw new ConnectionException(RuntimeSR.TokenRefreshFailure);
			}
			this.accessToken = accessToken;
		}

		// Token: 0x040007E8 RID: 2024
		private const bool BeginSessionOnConnect = true;

		// Token: 0x040007E9 RID: 2025
		private const bool EndSessionOnDisconnect = true;

		// Token: 0x040007EA RID: 2026
		private List<int> supportedCompatibilityLevels;

		// Token: 0x040007EB RID: 2027
		internal AnalysisServicesClient analysisServicesClient;

		// Token: 0x040007EC RID: 2028
		private AccessToken accessToken;

		// Token: 0x040007ED RID: 2029
		internal StringCollection captureLog;

		// Token: 0x040007EE RID: 2030
		internal int transactionsCount;

		// Token: 0x040007EF RID: 2031
		internal Hashtable pendingCreations = new Hashtable();

		// Token: 0x020001AA RID: 426
		internal abstract class ServerBodyBase : MajorObject.MajorObjectBody
		{
			// Token: 0x0600133C RID: 4924 RVA: 0x0004367C File Offset: 0x0004187C
			private protected ServerBodyBase(Server owner)
				: base(owner)
			{
				this.Version = null;
				this.Edition = ServerEdition.Standard;
				this.EditionID = -1L;
				this.ProductLevel = null;
				this.Properties = new ServerPropertyCollection();
				this.ProductName = null;
				this.ServerMode = ServerMode.Default;
				this.DefaultCompatibilityLevel = 0;
				this.SupportedCompatibilityLevels = null;
				this.ServerLocation = ServerLocation.OnPremise;
				this.CompatibilityMode = CompatibilityMode.Unknown;
			}

			// Token: 0x040010D6 RID: 4310
			public string Version;

			// Token: 0x040010D7 RID: 4311
			public ServerEdition Edition;

			// Token: 0x040010D8 RID: 4312
			public long EditionID;

			// Token: 0x040010D9 RID: 4313
			public string ProductLevel;

			// Token: 0x040010DA RID: 4314
			public ServerPropertyCollection Properties;

			// Token: 0x040010DB RID: 4315
			public string ProductName;

			// Token: 0x040010DC RID: 4316
			public ServerMode ServerMode;

			// Token: 0x040010DD RID: 4317
			public int DefaultCompatibilityLevel;

			// Token: 0x040010DE RID: 4318
			public string SupportedCompatibilityLevels;

			// Token: 0x040010DF RID: 4319
			public ServerLocation ServerLocation;

			// Token: 0x040010E0 RID: 4320
			public CompatibilityMode CompatibilityMode;
		}
	}
}
