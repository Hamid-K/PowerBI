using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine
{
	// Token: 0x02000011 RID: 17
	public sealed class SemanticQueryConnection : IDbConnectionExtension, IDbConnection, IDisposable, IExtension, IDbCollationProperties, IDbPoolableConnection
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000054DD File Offset: 0x000036DD
		[CLSCompliant(false)]
		public void Initialize(SemanticModel userModel, SemanticModel fullModel, string dataExtensionName, string userID, string userCulture)
		{
			this.m_connState.UserModel = userModel;
			this.m_connState.FullModel = fullModel;
			this.m_connState.TargetExtName = dataExtensionName;
			this.m_connState.UserID = userID;
			this.m_connState.UserCulture = userCulture;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000551D File Offset: 0x0000371D
		internal static CultureInfo DefaultModelCulture
		{
			get
			{
				return Localization.DefaultReportServerCulture;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003FB8 File Offset: 0x000021B8
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005524 File Offset: 0x00003724
		public string LocalizedName
		{
			[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
			get
			{
				return SR.SemanticQueryEngineLocalizedName;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000552C File Offset: 0x0000372C
		public void Open()
		{
			if (this.m_connState.TargetConnection != null)
			{
				return;
			}
			if (this.m_connState.TargetExtName == null)
			{
				throw new InvalidOperationException(SR.SemanticQueryEngineNotInitialized);
			}
			IExtension newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(this.m_connState.TargetExtName, "Data");
			if (newInstanceExtensionClass == null)
			{
				throw new DataExtensionNotFoundException(this.m_connState.TargetExtName);
			}
			this.m_connState.TargetConnection = newInstanceExtensionClass as IDbConnection;
			if (this.m_connState.TargetConnection == null)
			{
				throw new ServerConfigurationErrorException(null, Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Report Server Data Extension {0} does not implement Microsoft.ReportingServices.DataProcessing.IDbConnection interface.", new object[] { this.m_connState.TargetExtName }));
			}
			this.m_connState.TargetConnection.ConnectionString = this.ConnectionString;
			if (this.m_connExtState != null)
			{
				IDbConnectionExtension dbConnectionExtension = this.m_connState.TargetConnection as IDbConnectionExtension;
				if (dbConnectionExtension == null)
				{
					throw new ServerConfigurationErrorException(null, Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Report Server Data Extension {0} does not implement Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension interface.", new object[] { this.m_connState.TargetExtName }));
				}
				dbConnectionExtension.IntegratedSecurity = this.m_connExtState.IntegratedSecurity;
				if (this.m_connExtState.Impersonate != null)
				{
					dbConnectionExtension.Impersonate = this.m_connExtState.Impersonate;
				}
				if (this.m_connExtState.GetUserName() != null)
				{
					dbConnectionExtension.UserName = this.m_connExtState.GetUserName();
				}
				if (this.m_connExtState.GetPassword() != null)
				{
					dbConnectionExtension.Password = this.m_connExtState.GetPassword();
				}
			}
			this.m_connState.TargetConnection.Open();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000056A1 File Offset: 0x000038A1
		public void Close()
		{
			if (this.m_connState.TargetConnection != null)
			{
				this.m_connState.TargetConnection.Close();
			}
			this.m_connState = new SemanticQueryConnection.ConnectionState();
			this.m_connExtState = null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000056D4 File Offset: 0x000038D4
		public IDbCommand CreateCommand()
		{
			if (this.m_connState.TargetExtName == null || this.m_connState.TargetConnection == null)
			{
				throw new InvalidOperationException(SR.ConnectionNotOpen);
			}
			IExtension newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(this.m_connState.TargetExtName, "SemanticQuery");
			if (newInstanceExtensionClass == null)
			{
				throw new SemanticQueryExtensionNotFoundException(this.m_connState.TargetExtName);
			}
			ISemanticQueryCommand semanticQueryCommand = newInstanceExtensionClass as ISemanticQueryCommand;
			if (semanticQueryCommand == null)
			{
				throw new ServerConfigurationErrorException(null, Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Semantic Query Extension for {0} does not implement Microsoft.ReportingServices.SemanticQueryEngine.ISemanticQueryCommand interface.", new object[] { this.m_connState.TargetExtName }));
			}
			ISetSemanticQueryConnection setSemanticQueryConnection = semanticQueryCommand as ISetSemanticQueryConnection;
			if (setSemanticQueryConnection != null)
			{
				setSemanticQueryConnection.SetSemanticQueryConnection(this);
			}
			semanticQueryCommand.Initialize(this.m_connState.TargetConnection);
			return new SemanticQueryCommandWrapper(this.m_connState.UserModel, this.m_connState.FullModel, semanticQueryCommand, this.m_connState.UserID, this.m_connState.UserCulture);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000057B1 File Offset: 0x000039B1
		public IDbTransaction BeginTransaction()
		{
			if (this.m_connState.TargetConnection == null)
			{
				throw new InvalidOperationException(SR.ConnectionNotOpen);
			}
			return this.m_connState.TargetConnection.BeginTransaction();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000057DB File Offset: 0x000039DB
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000057E8 File Offset: 0x000039E8
		public string ConnectionString
		{
			get
			{
				return this.m_connState.GetConnectionString();
			}
			set
			{
				this.m_connState.SetConnectionString(value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000057F6 File Offset: 0x000039F6
		public int ConnectionTimeout
		{
			get
			{
				if (this.m_connState.TargetConnection != null)
				{
					return this.m_connState.TargetConnection.ConnectionTimeout;
				}
				return 0;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005817 File Offset: 0x00003A17
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000581F File Offset: 0x00003A1F
		public string Impersonate
		{
			set
			{
				this.GetConnExtState().Impersonate = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000582D File Offset: 0x00003A2D
		public string UserName
		{
			set
			{
				this.GetConnExtState().SetUserName(value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000583B File Offset: 0x00003A3B
		public string Password
		{
			set
			{
				this.GetConnExtState().SetPassword(value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005849 File Offset: 0x00003A49
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005856 File Offset: 0x00003A56
		public bool IntegratedSecurity
		{
			get
			{
				return this.GetConnExtState().IntegratedSecurity;
			}
			set
			{
				this.GetConnExtState().IntegratedSecurity = value;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005864 File Offset: 0x00003A64
		bool IDbCollationProperties.GetCollationProperties(out string cultureName, out bool caseSensitive, out bool accentSensitive, out bool kanatypeSensitive, out bool widthSensitive)
		{
			if (this.m_connState.CollationProperties == null)
			{
				if (this.m_connState.FullModel == null || this.m_connState.TargetConnection == null)
				{
					throw new InvalidOperationException(SR.ConnectionNotOpen);
				}
				this.m_connState.CollationProperties = SemanticQueryConnection.GetCollationPropertiesImpl(this.m_connState.FullModel, this.m_connState.TargetConnection);
			}
			if (this.m_connState.CollationProperties == null)
			{
				throw SQEAssert.AssertFalseAndThrow("m_connState.CollationProperties can not be initialized to null.", Array.Empty<object>());
			}
			cultureName = this.m_connState.CollationProperties.CultureName;
			caseSensitive = this.m_connState.CollationProperties.CaseSensitive;
			accentSensitive = this.m_connState.CollationProperties.AccentSensitive;
			kanatypeSensitive = this.m_connState.CollationProperties.KanatypeSensitive;
			widthSensitive = this.m_connState.CollationProperties.WidthSensitive;
			return true;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005944 File Offset: 0x00003B44
		public bool IsAlive
		{
			get
			{
				if (this.m_connState.TargetConnection != null)
				{
					IDbPoolableConnection dbPoolableConnection = this.m_connState.TargetConnection as IDbPoolableConnection;
					if (dbPoolableConnection != null)
					{
						return dbPoolableConnection.IsAlive;
					}
				}
				return false;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000597A File Offset: 0x00003B7A
		public string GetConnectionStringForPooling()
		{
			return this.ConnectionString;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005982 File Offset: 0x00003B82
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000598A File Offset: 0x00003B8A
		public bool IsFromPool { get; set; }

		// Token: 0x060000ED RID: 237 RVA: 0x00005993 File Offset: 0x00003B93
		private SemanticQueryConnection.ConnectionExtensionState GetConnExtState()
		{
			if (this.m_connExtState == null)
			{
				this.m_connExtState = new SemanticQueryConnection.ConnectionExtensionState();
			}
			return this.m_connExtState;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000059B0 File Offset: 0x00003BB0
		private static SemanticQueryConnection.CollationProperties GetCollationPropertiesImpl(SemanticModel fullModel, IDbConnection targetConnection)
		{
			if (fullModel == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("fullModel"));
			}
			if (targetConnection == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("targetConnection"));
			}
			if (fullModel.DataSourceView != null && fullModel.DataSourceView.CompareInfo != null)
			{
				DsvCompareInfo compareInfo = fullModel.DataSourceView.CompareInfo;
				return new SemanticQueryConnection.CollationProperties(compareInfo.Culture.Name, !compareInfo.IgnoreCase, !compareInfo.IgnoreNonSpace, !compareInfo.IgnoreKanaType, !compareInfo.IgnoreWidth);
			}
			IDbCollationProperties dbCollationProperties = targetConnection as IDbCollationProperties;
			if (dbCollationProperties != null)
			{
				try
				{
					string text;
					bool flag;
					bool flag2;
					bool flag3;
					bool flag4;
					if (dbCollationProperties.GetCollationProperties(out text, out flag, out flag2, out flag3, out flag4))
					{
						return new SemanticQueryConnection.CollationProperties(text, flag, flag2, flag3, flag4);
					}
				}
				catch (Exception ex)
				{
					RSTrace.SQETracer.Trace(TraceLevel.Warning, "Failed to get db collation properties from target connection: {0}", new object[] { ex.ToString() });
				}
			}
			RSTrace.SQETracer.Trace(TraceLevel.Info, "Using default collation properties: all sensitive");
			return new SemanticQueryConnection.CollationProperties((fullModel.Culture ?? SemanticQueryConnection.DefaultModelCulture).Name, true, true, true, true);
		}

		// Token: 0x04000055 RID: 85
		private SemanticQueryConnection.ConnectionState m_connState = new SemanticQueryConnection.ConnectionState();

		// Token: 0x04000056 RID: 86
		private SemanticQueryConnection.ConnectionExtensionState m_connExtState;

		// Token: 0x020000AF RID: 175
		private sealed class ConnectionState
		{
			// Token: 0x06000696 RID: 1686 RVA: 0x0001AA5D File Offset: 0x00018C5D
			internal string GetConnectionString()
			{
				return DataProtectionLocal.LocalUnprotectData(this.m_connectionString);
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0001AA6A File Offset: 0x00018C6A
			internal void SetConnectionString(string connectionString)
			{
				this.m_connectionString = DataProtectionLocal.LocalProtectData(connectionString);
			}

			// Token: 0x0400031E RID: 798
			internal string TargetExtName;

			// Token: 0x0400031F RID: 799
			internal IDbConnection TargetConnection;

			// Token: 0x04000320 RID: 800
			internal SemanticModel UserModel;

			// Token: 0x04000321 RID: 801
			internal SemanticModel FullModel;

			// Token: 0x04000322 RID: 802
			internal string UserID;

			// Token: 0x04000323 RID: 803
			internal string UserCulture;

			// Token: 0x04000324 RID: 804
			internal SemanticQueryConnection.CollationProperties CollationProperties;

			// Token: 0x04000325 RID: 805
			private string m_connectionString;
		}

		// Token: 0x020000B0 RID: 176
		private sealed class CollationProperties
		{
			// Token: 0x06000699 RID: 1689 RVA: 0x0001AA78 File Offset: 0x00018C78
			internal CollationProperties(string cultureName, bool caseSensitive, bool accentSensitive, bool kanatypeSensitive, bool widthSensitive)
			{
				this.CultureName = cultureName;
				this.CaseSensitive = caseSensitive;
				this.AccentSensitive = accentSensitive;
				this.KanatypeSensitive = kanatypeSensitive;
				this.WidthSensitive = widthSensitive;
			}

			// Token: 0x04000326 RID: 806
			internal readonly string CultureName;

			// Token: 0x04000327 RID: 807
			internal readonly bool CaseSensitive;

			// Token: 0x04000328 RID: 808
			internal readonly bool AccentSensitive;

			// Token: 0x04000329 RID: 809
			internal readonly bool KanatypeSensitive;

			// Token: 0x0400032A RID: 810
			internal readonly bool WidthSensitive;
		}

		// Token: 0x020000B1 RID: 177
		private sealed class ConnectionExtensionState
		{
			// Token: 0x0600069A RID: 1690 RVA: 0x0001AAA5 File Offset: 0x00018CA5
			internal string GetPassword()
			{
				return DataProtectionLocal.LocalUnprotectData(this.m_password);
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0001AAB2 File Offset: 0x00018CB2
			internal void SetPassword(string password)
			{
				this.m_password = DataProtectionLocal.LocalProtectData(password);
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x0001AAC0 File Offset: 0x00018CC0
			internal string GetUserName()
			{
				return DataProtectionLocal.LocalUnprotectData(this.m_userName);
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x0001AACD File Offset: 0x00018CCD
			internal void SetUserName(string userName)
			{
				this.m_userName = DataProtectionLocal.LocalProtectData(userName);
			}

			// Token: 0x0400032B RID: 811
			internal string Impersonate;

			// Token: 0x0400032C RID: 812
			internal bool IntegratedSecurity;

			// Token: 0x0400032D RID: 813
			private string m_password;

			// Token: 0x0400032E RID: 814
			private string m_userName;
		}
	}
}
