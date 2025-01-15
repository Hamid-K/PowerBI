using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.EnterpriseSingleSignOn.Interop;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009E7 RID: 2535
	internal class DrdaConnectionString
	{
		// Token: 0x06004E2F RID: 20015 RVA: 0x0013B29C File Offset: 0x0013949C
		public DrdaConnectionString()
		{
			this._connInfo = new string[64];
			this._connectionString = string.Empty;
			this._integratedSecurity = string.Empty;
			this.Clear();
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x0013B2D0 File Offset: 0x001394D0
		public void CopyInto(DrdaConnectionString copy)
		{
			copy._connectionString = this._connectionString;
			copy._isInitialized = this._isInitialized;
			for (int i = 0; i < 64; i++)
			{
				copy.DrdaConnectInfo[i] = this.DrdaConnectInfo[i];
			}
			copy.ConnectionPooling = this.ConnectionPooling;
			copy.IsEssoRetrieved = this.IsEssoRetrieved;
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x0013B32A File Offset: 0x0013952A
		public string[] DrdaConnectInfo
		{
			get
			{
				return this._connInfo;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x0013B332 File Offset: 0x00139532
		// (set) Token: 0x06004E33 RID: 20019 RVA: 0x0013B33A File Offset: 0x0013953A
		internal bool IsEssoRetrieved { get; private set; }

		// Token: 0x06004E34 RID: 20020 RVA: 0x0013B344 File Offset: 0x00139544
		public void ParseESSOConnectionString()
		{
			if (this.IsEssoRetrieved)
			{
				return;
			}
			if (string.Compare(this._integratedSecurity, "SSPI", true) == 0 && string.IsNullOrEmpty(this.PrincipleName))
			{
				string text = this.RetrieveESSOConnectionString();
				if (!string.IsNullOrEmpty(text))
				{
					string stringProperty = this.GetStringProperty(ConnectionKey.KEY_CLIENTAPPNAME);
					string stringProperty2 = this.GetStringProperty(ConnectionKey.KEY_CLIENTACCOUNTING);
					string stringProperty3 = this.GetStringProperty(ConnectionKey.KEY_CLIENTUSERID);
					string stringProperty4 = this.GetStringProperty(ConnectionKey.KEY_CLIENTWORKSTATION);
					this.Clear();
					this.ParseInternal(text.ToCharArray(), DrdaConnectionString.UdlSupport.LoadFromFile);
					this.SetStringProperty(ConnectionKey.KEY_CLIENTAPPNAME, stringProperty);
					this.SetStringProperty(ConnectionKey.KEY_CLIENTWORKSTATION, stringProperty4);
					this.SetStringProperty(ConnectionKey.KEY_CLIENTACCOUNTING, stringProperty2);
					this.SetStringProperty(ConnectionKey.KEY_CLIENTUSERID, stringProperty3);
					this.ProcessConnectionString();
				}
			}
			this.IsEssoRetrieved = true;
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x0013B3F8 File Offset: 0x001395F8
		private string RetrieveESSOConnectionString()
		{
			string text = string.Empty;
			ISSOLookup2 issolookup = (ISSOLookup2)new SSOLookup();
			string text2 = null;
			string stringProperty = this.GetStringProperty(ConnectionKey.KEY_AFFILIATEAPP);
			try
			{
				string[] credentials = issolookup.GetCredentials(stringProperty, 4, out text2);
				if (credentials.Length != 0)
				{
					string text3 = credentials[0];
					if (credentials.Length > 1)
					{
						string[] array;
						int[] array2;
						((ISSOMapper2)new SSOMapper()).GetFieldInfo(stringProperty, out array, out array2);
						int num = 1;
						while (num < array.Length && num < credentials.Length + 1)
						{
							if (string.Compare(array[num], "ConnectionString") == 0)
							{
								text = credentials[num - 1];
								if (!text.Contains("Affiliate Application"))
								{
									text = text + "Affiliate Application=" + stringProperty + ";";
								}
								if (!text.Contains("Integrated Security"))
								{
									text += "Integrated Security=SSPI;";
								}
							}
							num++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Trace.MessageTrace("Failed to retrieve mapping for specified affiliate app: " + stringProperty + "\r\n" + ex.ToString());
			}
			return text;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0013B4FC File Offset: 0x001396FC
		private void ProcessConnectionString()
		{
			if (this._integratedSecurity.Equals("SSPI", StringComparison.OrdinalIgnoreCase))
			{
				this.IntegratedSecurity = "SSPI";
			}
			if (this.GetStringProperty(ConnectionKey.KEY_NETLIB).Length != 0 && !this.GetStringProperty(ConnectionKey.KEY_NETLIB).Equals("TCP") && !this.GetStringProperty(ConnectionKey.KEY_NETLIB).Equals("TCPIP") && !this.GetStringProperty(ConnectionKey.KEY_NETLIB).Equals("IP") && (this.GetStringProperty(ConnectionKey.KEY_APPCMODE).Length <= 0 || this.GetStringProperty(ConnectionKey.KEY_REMOTELU).Length <= 0 || this.GetStringProperty(ConnectionKey.KEY_LOCALLU).Length <= 0))
			{
				this._isInitialized = false;
				return;
			}
			if (this._connectionString.Length > 0)
			{
				this._isInitialized = true;
				return;
			}
			this._isInitialized = false;
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x0013B5C0 File Offset: 0x001397C0
		// (set) Token: 0x06004E38 RID: 20024 RVA: 0x0013B5C8 File Offset: 0x001397C8
		public string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				if (this._connectionString == null)
				{
					this._connectionString = value;
					return;
				}
				this.Clear();
				this._connectionString = value;
				this.ParseInternal(this._connectionString.ToCharArray(), DrdaConnectionString.UdlSupport.LoadFromFile);
				this.ProcessConnectionString();
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x0013B600 File Offset: 0x00139800
		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x0013B608 File Offset: 0x00139808
		// (set) Token: 0x06004E3B RID: 20027 RVA: 0x0013B611 File Offset: 0x00139811
		public string DataSource
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_DSN);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_DSN, value);
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004E3C RID: 20028 RVA: 0x0013B61B File Offset: 0x0013981B
		// (set) Token: 0x06004E3D RID: 20029 RVA: 0x0013B624 File Offset: 0x00139824
		public string Database
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_RDBNAME);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_RDBNAME, value);
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x0013B62E File Offset: 0x0013982E
		// (set) Token: 0x06004E3F RID: 20031 RVA: 0x0013B637 File Offset: 0x00139837
		public string UserName
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_USERID);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_USERID, value);
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004E40 RID: 20032 RVA: 0x0013B641 File Offset: 0x00139841
		// (set) Token: 0x06004E41 RID: 20033 RVA: 0x0013B64A File Offset: 0x0013984A
		public string Password
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_PWD);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_PWD, value);
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x0013B654 File Offset: 0x00139854
		// (set) Token: 0x06004E43 RID: 20035 RVA: 0x0013B65E File Offset: 0x0013985E
		public string Authentication
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_AUTHENTICATION);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_AUTHENTICATION, value);
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06004E44 RID: 20036 RVA: 0x0013B669 File Offset: 0x00139869
		// (set) Token: 0x06004E45 RID: 20037 RVA: 0x0013B673 File Offset: 0x00139873
		public string CertificateCN
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_CERTIFICATECN);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_CERTIFICATECN, value);
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x0013B67E File Offset: 0x0013987E
		// (set) Token: 0x06004E47 RID: 20039 RVA: 0x0013B688 File Offset: 0x00139888
		public string NewPassword
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_NEWPWD);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_NEWPWD, value);
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x0013B693 File Offset: 0x00139893
		// (set) Token: 0x06004E49 RID: 20041 RVA: 0x0013B69C File Offset: 0x0013989C
		public int HostCCSID
		{
			get
			{
				return this.GetIntProperty(ConnectionKey.KEY_HOSTCCSID);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_HOSTCCSID, value);
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x0013B6A6 File Offset: 0x001398A6
		// (set) Token: 0x06004E4B RID: 20043 RVA: 0x0013B6B0 File Offset: 0x001398B0
		public int PCCodepage
		{
			get
			{
				return this.GetIntProperty(ConnectionKey.KEY_PCCODEPAGE);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_PCCODEPAGE, value);
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x0013B6BB File Offset: 0x001398BB
		// (set) Token: 0x06004E4D RID: 20045 RVA: 0x0013B6C5 File Offset: 0x001398C5
		public string PackageCollection
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_PACKAGECOL);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_PACKAGECOL, value);
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x0013B6D0 File Offset: 0x001398D0
		// (set) Token: 0x06004E4F RID: 20047 RVA: 0x0013B6DA File Offset: 0x001398DA
		public string DefaultSchema
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_CATALOGCOL);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_CATALOGCOL, value);
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x0013B6E5 File Offset: 0x001398E5
		// (set) Token: 0x06004E51 RID: 20049 RVA: 0x0013B6EF File Offset: 0x001398EF
		public int ConnectionTimeout
		{
			get
			{
				return this.GetIntProperty(ConnectionKey.KEY_TIMEOUT);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_TIMEOUT, value);
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x0013B6FA File Offset: 0x001398FA
		// (set) Token: 0x06004E53 RID: 20051 RVA: 0x0013B704 File Offset: 0x00139904
		public int MaxPoolSize
		{
			get
			{
				return this.GetIntProperty(ConnectionKey.KEY_MAXPOOLSIZE);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_MAXPOOLSIZE, value);
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x0013B70F File Offset: 0x0013990F
		// (set) Token: 0x06004E55 RID: 20053 RVA: 0x0013B719 File Offset: 0x00139919
		public int RowsetCacheSize
		{
			get
			{
				return this.GetIntProperty(ConnectionKey.KEY_ROWSETCACHESIZE);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_ROWSETCACHESIZE, value);
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004E56 RID: 20054 RVA: 0x0013B724 File Offset: 0x00139924
		// (set) Token: 0x06004E57 RID: 20055 RVA: 0x0013B72E File Offset: 0x0013992E
		public int BinaryCodePage
		{
			get
			{
				return this.GetIntProperty(ConnectionKey.KEY_BINARYCODEPAGE);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_BINARYCODEPAGE, value);
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x0013B73C File Offset: 0x0013993C
		// (set) Token: 0x06004E59 RID: 20057 RVA: 0x0013B7B8 File Offset: 0x001399B8
		public string IntegratedSecurity
		{
			get
			{
				if ((this.UserName.Equals("MS$SAME") && (this.Password.Length == 0 || this.Password.Equals("MS$SAME"))) || (this.UserName.Equals("MS$KERBEROS") && (this.Password.Length == 0 || this.Password.Equals("MS$KERBEROS"))))
				{
					return "SSPI";
				}
				return string.Empty;
			}
			set
			{
				if (value.Equals("SSPI", StringComparison.OrdinalIgnoreCase))
				{
					if (string.IsNullOrEmpty(this.PrincipleName))
					{
						this.UserName = "MS$SAME";
						this.Password = "MS$SAME";
						return;
					}
					this.UserName = "MS$KERBEROS";
					this.Password = "MS$KERBEROS";
				}
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x0013B80D File Offset: 0x00139A0D
		// (set) Token: 0x06004E5B RID: 20059 RVA: 0x0013B817 File Offset: 0x00139A17
		[Browsable(false)]
		[Obsolete("BinaryAsCharacter has been deprecated.")]
		public bool BinaryAsCharacter
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_BINASCHAR);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_BINASCHAR, value);
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x0013B822 File Offset: 0x00139A22
		// (set) Token: 0x06004E5D RID: 20061 RVA: 0x0013B82C File Offset: 0x00139A2C
		public bool DateTimeAsCharacter
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_DATETIMEASCHAR);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_DATETIMEASCHAR, value);
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x0013B837 File Offset: 0x00139A37
		// (set) Token: 0x06004E5F RID: 20063 RVA: 0x0013B841 File Offset: 0x00139A41
		public bool DecimalAsNumeric
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_DECIMALASNUMERIC);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_DECIMALASNUMERIC, value);
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x0013B84C File Offset: 0x00139A4C
		// (set) Token: 0x06004E61 RID: 20065 RVA: 0x0013B856 File Offset: 0x00139A56
		public string DatabaseName
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_DATABASENAME);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_DATABASENAME, value);
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x0013B861 File Offset: 0x00139A61
		// (set) Token: 0x06004E63 RID: 20067 RVA: 0x0013B86B File Offset: 0x00139A6B
		public bool DateTimeAsDate
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_DATETIMEASDATE);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_DATETIMEASDATE, value);
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x0013B876 File Offset: 0x00139A76
		// (set) Token: 0x06004E65 RID: 20069 RVA: 0x0013B892 File Offset: 0x00139A92
		public bool AutoCommit
		{
			get
			{
				return string.IsNullOrWhiteSpace(this._connInfo[34]) || this.GetBooleanProperty(ConnectionKey.KEY_AUTOCOMMIT);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_AUTOCOMMIT, value);
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x0013B89D File Offset: 0x00139A9D
		// (set) Token: 0x06004E67 RID: 20071 RVA: 0x0013B8C0 File Offset: 0x00139AC0
		public UnitsOfWorkType UnitsOfWork
		{
			get
			{
				if (!this.GetStringProperty(ConnectionKey.KEY_UNITSOFWORK).ToUpper(CultureInfo.InvariantCulture).Equals("DUW"))
				{
					return UnitsOfWorkType.RUW;
				}
				return UnitsOfWorkType.DUW;
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_UNITSOFWORK, (value == UnitsOfWorkType.DUW) ? "DUW" : "RUW");
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x0013B8DA File Offset: 0x00139ADA
		// (set) Token: 0x06004E69 RID: 20073 RVA: 0x0013B8E4 File Offset: 0x00139AE4
		public string DefaultQualifier
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_QUALIFIERCOL);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_QUALIFIERCOL, value);
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x0013B8EF File Offset: 0x00139AEF
		// (set) Token: 0x06004E6B RID: 20075 RVA: 0x0013B8F9 File Offset: 0x00139AF9
		public DBMSPlatform DBMSPlatform
		{
			get
			{
				return (DBMSPlatform)this.GetIntProperty(ConnectionKey.KEY_DBMSPLATFORM);
			}
			set
			{
				this.SetIntProperty(ConnectionKey.KEY_DBMSPLATFORM, (int)value);
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x0013B904 File Offset: 0x00139B04
		// (set) Token: 0x06004E6D RID: 20077 RVA: 0x0013B90E File Offset: 0x00139B0E
		public string AffiliateApplication
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_AFFILIATEAPP);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_AFFILIATEAPP, value);
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x0013B919 File Offset: 0x00139B19
		// (set) Token: 0x06004E6F RID: 20079 RVA: 0x0013B924 File Offset: 0x00139B24
		public string PrincipleName
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_SECPRINCIPLE);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_SECPRINCIPLE, value);
				if (this.IntegratedSecurity.Equals("SSPI", StringComparison.OrdinalIgnoreCase))
				{
					if (string.IsNullOrEmpty(value))
					{
						this.UserName = "MS$SAME";
						this.Password = "MS$SAME";
						return;
					}
					this.UserName = "MS$KERBEROS";
					this.Password = "MS$KERBEROS";
				}
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x0013B982 File Offset: 0x00139B82
		// (set) Token: 0x06004E71 RID: 20081 RVA: 0x0013B98C File Offset: 0x00139B8C
		public bool ConnectionPooling
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_CONNPOOLING);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_CONNPOOLING, value);
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x0013B997 File Offset: 0x00139B97
		// (set) Token: 0x06004E73 RID: 20083 RVA: 0x0013B9A1 File Offset: 0x00139BA1
		public bool UseEarlyMetaData
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_EARLYMETADATA);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_EARLYMETADATA, value);
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x0013B9AC File Offset: 0x00139BAC
		// (set) Token: 0x06004E75 RID: 20085 RVA: 0x0013B9B6 File Offset: 0x00139BB6
		public string ClientApplicationName
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_CLIENTAPPNAME);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_CLIENTAPPNAME, value.Substring(0, Math.Min(value.Length, 18)));
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004E76 RID: 20086 RVA: 0x0013B9D4 File Offset: 0x00139BD4
		// (set) Token: 0x06004E77 RID: 20087 RVA: 0x0013B9DE File Offset: 0x00139BDE
		public string ClientUserID
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_CLIENTUSERID);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_CLIENTUSERID, value.Substring(0, Math.Min(value.Length, 16)));
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004E78 RID: 20088 RVA: 0x0013B9FC File Offset: 0x00139BFC
		// (set) Token: 0x06004E79 RID: 20089 RVA: 0x0013BA06 File Offset: 0x00139C06
		public string ClientWorkstationName
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_CLIENTWORKSTATION);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_CLIENTWORKSTATION, value.Substring(0, Math.Min(value.Length, 18)));
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x0013BA24 File Offset: 0x00139C24
		// (set) Token: 0x06004E7B RID: 20091 RVA: 0x0013BA2E File Offset: 0x00139C2E
		public string ClientAccounting
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_CLIENTACCOUNTING);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_CLIENTACCOUNTING, value.Substring(0, Math.Min(value.Length, 255)));
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x0013BA4F File Offset: 0x00139C4F
		// (set) Token: 0x06004E7D RID: 20093 RVA: 0x0013BA57 File Offset: 0x00139C57
		public string ShadowCatalog
		{
			get
			{
				return this._shadowCatalog;
			}
			set
			{
				this._shadowCatalog = value.Substring(0, Math.Min(value.Length, 128));
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x0013BA76 File Offset: 0x00139C76
		// (set) Token: 0x06004E7F RID: 20095 RVA: 0x0013BA80 File Offset: 0x00139C80
		public bool DeferPrepare
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_DEFERPREPARE);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_DEFERPREPARE, value);
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06004E80 RID: 20096 RVA: 0x0013BA8B File Offset: 0x00139C8B
		// (set) Token: 0x06004E81 RID: 20097 RVA: 0x0013BA95 File Offset: 0x00139C95
		public bool AllowNullChars
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_ALLOWNULLCHARS);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_ALLOWNULLCHARS, value);
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x0013BAA0 File Offset: 0x00139CA0
		// (set) Token: 0x06004E83 RID: 20099 RVA: 0x0013BAAA File Offset: 0x00139CAA
		public bool LoadBalancing
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_LOADBALANCING);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_LOADBALANCING, value);
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x0013BAB5 File Offset: 0x00139CB5
		// (set) Token: 0x06004E85 RID: 20101 RVA: 0x0013BABF File Offset: 0x00139CBF
		public bool LiteralReplacement
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_LITERALREPLACEMENT);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_LITERALREPLACEMENT, value);
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x0013BACA File Offset: 0x00139CCA
		// (set) Token: 0x06004E87 RID: 20103 RVA: 0x0013BAD4 File Offset: 0x00139CD4
		public string SpecialRegisters
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_SPECIALREGISTERS);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_SPECIALREGISTERS, value);
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x0013BADF File Offset: 0x00139CDF
		// (set) Token: 0x06004E89 RID: 20105 RVA: 0x0013BAE9 File Offset: 0x00139CE9
		public string EncryptionAlgorithm
		{
			get
			{
				return this.GetStringProperty(ConnectionKey.KEY_ENCRYPTIONALGORITHM);
			}
			set
			{
				this.SetStringProperty(ConnectionKey.KEY_ENCRYPTIONALGORITHM, value);
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x0013BAF4 File Offset: 0x00139CF4
		// (set) Token: 0x06004E8B RID: 20107 RVA: 0x0013BAFE File Offset: 0x00139CFE
		public bool Gateway
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_GATEWAY);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_GATEWAY, value);
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06004E8C RID: 20108 RVA: 0x0013BB09 File Offset: 0x00139D09
		// (set) Token: 0x06004E8D RID: 20109 RVA: 0x0013BB13 File Offset: 0x00139D13
		public bool ConvertToBigEndian
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_CONVERTTOBIGENDIAN);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_CONVERTTOBIGENDIAN, value);
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06004E8E RID: 20110 RVA: 0x0013BB1E File Offset: 0x00139D1E
		// (set) Token: 0x06004E8F RID: 20111 RVA: 0x0013BB28 File Offset: 0x00139D28
		public bool UseHIS2013Constants
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_USEHIS2013CONSTANTS);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_USEHIS2013CONSTANTS, value);
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x0013BB33 File Offset: 0x00139D33
		// (set) Token: 0x06004E91 RID: 20113 RVA: 0x0013BB3D File Offset: 0x00139D3D
		public bool XMLAsBinary
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_XMLASBINARY);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_XMLASBINARY, value);
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x0013BB48 File Offset: 0x00139D48
		// (set) Token: 0x06004E93 RID: 20115 RVA: 0x0013BB52 File Offset: 0x00139D52
		public bool UseAccelerator
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_USEACCELERATOR);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_USEACCELERATOR, value);
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x0013BB5D File Offset: 0x00139D5D
		// (set) Token: 0x06004E95 RID: 20117 RVA: 0x0013BB67 File Offset: 0x00139D67
		public bool BulkCopySchema
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_BULKCOPYSCHEMA);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_BULKCOPYSCHEMA, value);
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x0013BB72 File Offset: 0x00139D72
		// (set) Token: 0x06004E97 RID: 20119 RVA: 0x0013BB7C File Offset: 0x00139D7C
		public bool IMSDB
		{
			get
			{
				return this.GetBooleanProperty(ConnectionKey.KEY_IMSDB);
			}
			set
			{
				this.SetBooleanProperty(ConnectionKey.KEY_IMSDB, value);
			}
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x0013BB88 File Offset: 0x00139D88
		private void Clear()
		{
			for (int i = 0; i < 64; i++)
			{
				this.SetStringProperty((ConnectionKey)i, string.Empty);
			}
			this._shadowCatalog = string.Empty;
			this.SetIntProperty(ConnectionKey.KEY_NETPORT, 446);
			this.SetIntProperty(ConnectionKey.KEY_HOSTCCSID, 1208);
			this.SetIntProperty(ConnectionKey.KEY_PCCODEPAGE, 1252);
			this.SetBooleanProperty(ConnectionKey.KEY_BINASCHAR, false);
			this.SetStringProperty(ConnectionKey.KEY_UNITSOFWORK, "RUW");
			this.SetIntProperty(ConnectionKey.KEY_DBMSPLATFORM, 0);
			this.SetIntProperty(ConnectionKey.KEY_TIMEOUT, 15);
			this.SetIntProperty(ConnectionKey.KEY_ROWSETCACHESIZE, 0);
			this.SetBooleanProperty(ConnectionKey.KEY_EARLYMETADATA, false);
			this.SetBooleanProperty(ConnectionKey.KEY_DEFERPREPARE, false);
			this.SetStringProperty(ConnectionKey.KEY_CLIENTAPPNAME, string.Empty);
			this.SetStringProperty(ConnectionKey.KEY_SHADOWCATALOG, string.Empty);
			this.SetStringProperty(ConnectionKey.KEY_AUTHENTICATION, string.Empty);
			this.SetBooleanProperty(ConnectionKey.KEY_ALLOWNULLCHARS, false);
			this.SetBooleanProperty(ConnectionKey.KEY_LOADBALANCING, false);
			this.SetBooleanProperty(ConnectionKey.KEY_LITERALREPLACEMENT, false);
			this.SetStringProperty(ConnectionKey.KEY_SPECIALREGISTERS, string.Empty);
			this.SetBooleanProperty(ConnectionKey.KEY_GATEWAY, false);
			this.SetBooleanProperty(ConnectionKey.KEY_CONVERTTOBIGENDIAN, false);
			this.SetBooleanProperty(ConnectionKey.KEY_USEHIS2013CONSTANTS, false);
			this.SetBooleanProperty(ConnectionKey.KEY_XMLASBINARY, false);
			this.SetStringProperty(ConnectionKey.KEY_ENCRYPTIONALGORITHM, "DES");
			this.SetBooleanProperty(ConnectionKey.KEY_USEACCELERATOR, false);
			this.SetBooleanProperty(ConnectionKey.KEY_BULKCOPYSCHEMA, false);
			this.SetBooleanProperty(ConnectionKey.KEY_IMSDB, false);
			this._isInitialized = false;
			this._integratedSecurity = string.Empty;
			this.IsEssoRetrieved = false;
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0013BCD7 File Offset: 0x00139ED7
		public string GetStringProperty(ConnectionKey key)
		{
			if (this._connInfo[(int)key] != null)
			{
				return this._connInfo[(int)key];
			}
			return string.Empty;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0013BCF1 File Offset: 0x00139EF1
		public void SetStringProperty(ConnectionKey key, string val)
		{
			this._connInfo[(int)key] = val;
			this._isInitialized = true;
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0013BD03 File Offset: 0x00139F03
		public int GetIntProperty(ConnectionKey key)
		{
			if (this._connInfo[(int)key] != null)
			{
				return int.Parse(this._connInfo[(int)key], CultureInfo.InvariantCulture);
			}
			return 0;
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0013BD23 File Offset: 0x00139F23
		public void SetIntProperty(ConnectionKey key, int val)
		{
			this._connInfo[(int)key] = val.ToString(CultureInfo.InvariantCulture);
			this._isInitialized = true;
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0013BD40 File Offset: 0x00139F40
		public bool GetBooleanProperty(ConnectionKey key)
		{
			string text = this._connInfo[(int)key];
			return text != null && (string.Compare(text, "1", true) == 0 || string.Compare(text, "true", true) == 0 || string.Compare(text, "yes", true) == 0);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0013BD8C File Offset: 0x00139F8C
		public void SetBooleanProperty(ConnectionKey key, bool val)
		{
			this._connInfo[(int)key] = (val ? "1" : "0");
			this._isInitialized = true;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0013BDAC File Offset: 0x00139FAC
		private int GetKeyValuePair(char[] connectionString, int currentPosition, out string key, char[] valuebuf, out int vallength, out bool isempty)
		{
			DrdaConnectionString.PARSERSTATE parserstate = DrdaConnectionString.PARSERSTATE.NothingYet;
			int num = 0;
			int num2 = currentPosition;
			key = null;
			vallength = -1;
			isempty = false;
			char c = '\0';
			while (currentPosition < connectionString.Length)
			{
				c = connectionString[currentPosition];
				switch (parserstate)
				{
				case DrdaConnectionString.PARSERSTATE.NothingYet:
					if (';' != c && !char.IsWhiteSpace(c))
					{
						num2 = currentPosition;
						if (c == '\0')
						{
							parserstate = DrdaConnectionString.PARSERSTATE.NullTermination;
						}
						else
						{
							if (char.IsControl(c))
							{
								throw DrdaException.ConnectionStringSyntax(currentPosition);
							}
							parserstate = DrdaConnectionString.PARSERSTATE.Key;
							num = 0;
							goto IL_01D2;
						}
					}
					break;
				case DrdaConnectionString.PARSERSTATE.Key:
					if ('=' == c)
					{
						parserstate = DrdaConnectionString.PARSERSTATE.KeyEqual;
					}
					else
					{
						if (!char.IsWhiteSpace(c) && char.IsControl(c))
						{
							throw DrdaException.ConnectionStringSyntax(currentPosition);
						}
						goto IL_01D2;
					}
					break;
				case DrdaConnectionString.PARSERSTATE.KeyEqual:
					if ('=' == c)
					{
						parserstate = DrdaConnectionString.PARSERSTATE.Key;
						goto IL_01D2;
					}
					key = this.GetKey(valuebuf, num);
					num = 0;
					parserstate = DrdaConnectionString.PARSERSTATE.KeyEnd;
					goto IL_00D5;
				case DrdaConnectionString.PARSERSTATE.KeyEnd:
					goto IL_00D5;
				case DrdaConnectionString.PARSERSTATE.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_01D2;
					}
					if (char.IsControl(c))
					{
						goto IL_020E;
					}
					if (';' == c)
					{
						goto IL_020E;
					}
					goto IL_01D2;
				case DrdaConnectionString.PARSERSTATE.DoubleQuoteValue:
					if ('"' == c)
					{
						parserstate = DrdaConnectionString.PARSERSTATE.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw DrdaException.ConnectionStringSyntax(currentPosition);
						}
						goto IL_01D2;
					}
					break;
				case DrdaConnectionString.PARSERSTATE.DoubleQuoteValueQuote:
					if ('"' != c)
					{
						parserstate = DrdaConnectionString.PARSERSTATE.DoubleQuoteValueEnd;
						goto IL_0164;
					}
					parserstate = DrdaConnectionString.PARSERSTATE.DoubleQuoteValue;
					break;
				case DrdaConnectionString.PARSERSTATE.DoubleQuoteValueEnd:
					goto IL_0164;
				case DrdaConnectionString.PARSERSTATE.SingleQuoteValue:
					if ('\'' == c)
					{
						parserstate = DrdaConnectionString.PARSERSTATE.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw DrdaException.ConnectionStringSyntax(currentPosition);
						}
						goto IL_01D2;
					}
					break;
				case DrdaConnectionString.PARSERSTATE.SingleQuoteValueQuote:
					if ('\'' != c)
					{
						parserstate = DrdaConnectionString.PARSERSTATE.SingleQuoteValueEnd;
						goto IL_01A4;
					}
					parserstate = DrdaConnectionString.PARSERSTATE.SingleQuoteValue;
					break;
				case DrdaConnectionString.PARSERSTATE.SingleQuoteValueEnd:
					goto IL_01A4;
				case DrdaConnectionString.PARSERSTATE.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw DrdaException.ConnectionStringSyntax(num2);
					}
					break;
				default:
					goto IL_01D2;
				}
				IL_01DB:
				currentPosition++;
				continue;
				IL_00D5:
				if (char.IsWhiteSpace(c))
				{
					goto IL_01DB;
				}
				if ('\'' == c)
				{
					parserstate = DrdaConnectionString.PARSERSTATE.SingleQuoteValue;
					goto IL_01DB;
				}
				if ('"' == c)
				{
					parserstate = DrdaConnectionString.PARSERSTATE.DoubleQuoteValue;
					goto IL_01DB;
				}
				if (';' == c || c == '\0')
				{
					goto IL_020E;
				}
				if (char.IsControl(c))
				{
					throw DrdaException.ConnectionStringSyntax(currentPosition);
				}
				parserstate = DrdaConnectionString.PARSERSTATE.UnquotedValue;
				goto IL_01D2;
				IL_0164:
				if (char.IsWhiteSpace(c))
				{
					goto IL_01DB;
				}
				if (';' == c)
				{
					goto IL_020E;
				}
				if (c == '\0')
				{
					parserstate = DrdaConnectionString.PARSERSTATE.NullTermination;
					goto IL_01DB;
				}
				throw DrdaException.ConnectionStringSyntax(currentPosition);
				IL_01A4:
				if (char.IsWhiteSpace(c))
				{
					goto IL_01DB;
				}
				if (';' == c)
				{
					goto IL_020E;
				}
				if (c == '\0')
				{
					parserstate = DrdaConnectionString.PARSERSTATE.NullTermination;
					goto IL_01DB;
				}
				throw DrdaException.ConnectionStringSyntax(currentPosition);
				IL_01D2:
				valuebuf[num++] = c;
				goto IL_01DB;
				IL_020E:
				if (DrdaConnectionString.PARSERSTATE.UnquotedValue == parserstate)
				{
					num = this.TrimWhiteSpace(valuebuf, num);
					if ('\'' == valuebuf[num - 1] || '"' == valuebuf[num - 1])
					{
						throw DrdaException.ConnectionStringSyntax(currentPosition - 1);
					}
				}
				else if (DrdaConnectionString.PARSERSTATE.KeyEqual != parserstate && DrdaConnectionString.PARSERSTATE.KeyEnd != parserstate)
				{
					isempty = num == 0;
				}
				if (';' == c && currentPosition < connectionString.Length)
				{
					currentPosition++;
				}
				vallength = num;
				return currentPosition;
			}
			if (DrdaConnectionString.PARSERSTATE.KeyEqual == parserstate)
			{
				key = this.GetKey(valuebuf, num);
				num = 0;
			}
			if (DrdaConnectionString.PARSERSTATE.Key == parserstate || DrdaConnectionString.PARSERSTATE.DoubleQuoteValue == parserstate || DrdaConnectionString.PARSERSTATE.SingleQuoteValue == parserstate)
			{
				throw DrdaException.ConnectionStringSyntax(num2);
			}
			goto IL_020E;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0013C016 File Offset: 0x0013A216
		private string GetKey(char[] valuebuf, int bufPosition)
		{
			bufPosition = this.TrimWhiteSpace(valuebuf, bufPosition);
			return Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(valuebuf, 0, bufPosition)).ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0013C044 File Offset: 0x0013A244
		private string GetValue(char[] valuebuf, int valstart, int vallength, bool isempty)
		{
			string text = (isempty ? string.Empty : null);
			if (0 < vallength)
			{
				text = new string(valuebuf, valstart, vallength);
			}
			return text;
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0013C06C File Offset: 0x0013A26C
		private int TrimWhiteSpace(char[] valuebuf, int bufPosition)
		{
			while (0 < bufPosition && char.IsWhiteSpace(valuebuf[bufPosition - 1]))
			{
				bufPosition--;
			}
			return bufPosition;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0013C088 File Offset: 0x0013A288
		private char[] ParseInternal(char[] connectionString, DrdaConnectionString.UdlSupport checkForUdl)
		{
			int i = 0;
			int num = connectionString.Length;
			char[] array = new char[connectionString.Length];
			string text = null;
			int num2 = 0;
			char[] array2 = null;
			int num3 = 0;
			char[] array3 = connectionString;
			while (i < num)
			{
				int num4 = i;
				string text2;
				int num5;
				bool flag;
				i = this.GetKeyValuePair(array3, num4, out text2, array, out num5, out flag);
				if (!this.IsEmpty(text2))
				{
					ConnectionKey connectionKey = ConnectionKey.KEY_UNKNOWN;
					if (!DrdaConnectionString._keywords.TryGetValue(text2, out connectionKey))
					{
						connectionKey = ConnectionKey.KEY_UNKNOWN;
					}
					if (ConnectionKey.KEY_FILENAME == connectionKey)
					{
						string text3 = this.GetValue(array, 0, num5, flag);
						if (text3 != null)
						{
							switch (checkForUdl)
							{
							case DrdaConnectionString.UdlSupport.LoadFromFile:
							{
								text = text3;
								int num6 = i - num4;
								if (array2 == null)
								{
									num3 = connectionString.Length;
									array2 = new char[num3];
									this.CopyChars(connectionString, 0, array2, 0, num4);
								}
								num2 = num4 - (connectionString.Length - num3);
								this.CopyChars(connectionString, i, array2, num2, connectionString.Length - i);
								num3 -= num6;
								continue;
							}
							case DrdaConnectionString.UdlSupport.UdlAsKeyword:
								continue;
							}
							throw DrdaException.KeywordNotSupported(text2);
						}
						throw DrdaException.InvalidUDL();
					}
					else if (connectionKey == ConnectionKey.KEY_PROVIDER)
					{
						string text3 = this.GetValue(array, 0, num5, flag);
						if (text3 == null || (text3 != "DB2DOTNET" && text3 != "DB2OLEDB" && text3 != "DB2OLEDB.1" && text3 != "MSINFORMIX" && text3 != "MSINFORMIX.1"))
						{
							throw DrdaException.InvalidUDL();
						}
					}
					else if (connectionKey == ConnectionKey.KEY_EXTPROPS)
					{
						string text3 = this.GetValue(array, 0, num5, flag);
						if (text3 != null)
						{
							char[] array4 = text3.ToCharArray();
							this.ParseInternal(array4, DrdaConnectionString.UdlSupport.UdlAsKeyword);
						}
					}
					else if (connectionKey == ConnectionKey.KEY_UNKNOWN)
					{
						if (!text2.ToUpper(CultureInfo.InvariantCulture).Equals("Derive Parameters".ToUpper(CultureInfo.InvariantCulture)))
						{
							throw DrdaException.KeywordNotSupported(text2);
						}
						string text3 = this.GetValue(array, 0, num5, flag);
					}
					else if ((short)connectionKey < 64)
					{
						string text3 = this.GetValue(array, 0, num5, flag);
						if (text3 != null)
						{
							if (connectionKey <= ConnectionKey.KEY_UNITSOFWORK)
							{
								if (connectionKey - ConnectionKey.KEY_USERID > 1)
								{
									if (connectionKey == ConnectionKey.KEY_UNITSOFWORK)
									{
										if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("RUW"))
										{
											this.UnitsOfWork = UnitsOfWorkType.RUW;
											continue;
										}
										if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("DUW"))
										{
											this.UnitsOfWork = UnitsOfWorkType.DUW;
											continue;
										}
										continue;
									}
								}
								else
								{
									if (this.IntegratedSecurity.Length == 0)
									{
										this.SetStringProperty(connectionKey, text3);
										continue;
									}
									continue;
								}
							}
							else if (connectionKey != ConnectionKey.KEY_DBMSPLATFORM)
							{
								if (connectionKey != ConnectionKey.KEY_AUTHENTICATION)
								{
									switch (connectionKey)
									{
									case ConnectionKey.KEY_SHADOWCATALOG:
										this.ShadowCatalog = text3;
										continue;
									case ConnectionKey.KEY_INTSECURITY:
										this._integratedSecurity = text3;
										continue;
									case ConnectionKey.KEY_CONNPOOLING:
										this.ConnectionPooling = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.ConnectionPooling = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_GATEWAY:
										this.Gateway = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.Gateway = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_CONVERTTOBIGENDIAN:
										this.ConvertToBigEndian = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.ConvertToBigEndian = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_USEHIS2013CONSTANTS:
										this.UseHIS2013Constants = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.UseHIS2013Constants = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_XMLASBINARY:
										this.XMLAsBinary = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.XMLAsBinary = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_ENCRYPTIONALGORITHM:
										this.EncryptionAlgorithm = "DES";
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("AES"))
										{
											this.EncryptionAlgorithm = text3;
											continue;
										}
										continue;
									case ConnectionKey.KEY_USEACCELERATOR:
										this.UseAccelerator = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.UseAccelerator = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_BULKCOPYSCHEMA:
										this.BulkCopySchema = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.BulkCopySchema = true;
											continue;
										}
										continue;
									case ConnectionKey.KEY_IMSDB:
										this.IMSDB = false;
										if (text3 == null)
										{
											continue;
										}
										text3 = text3.ToUpper(CultureInfo.InvariantCulture);
										if (text3.Equals("T") || text3.Equals("1") || text3.Equals("TRUE"))
										{
											this.IMSDB = true;
											continue;
										}
										continue;
									}
								}
								else
								{
									if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("Server_Encrypt_Pwd".ToUpper()))
									{
										this.Authentication = "Server_Encrypt_Pwd";
										continue;
									}
									if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("Server_Encrypt_UsrPwd".ToUpper()))
									{
										this.Authentication = "Server_Encrypt_UsrPwd";
										continue;
									}
									if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("Data_Encrypt".ToUpper()))
									{
										this.Authentication = "Data_Encrypt";
										continue;
									}
									if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("Server".ToUpper()))
									{
										this.Authentication = "Server";
										continue;
									}
									continue;
								}
							}
							else
							{
								if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("DB2/AS400"))
								{
									this.DBMSPlatform = DBMSPlatform.DB2AS400;
									continue;
								}
								if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("DB2/MVS"))
								{
									this.DBMSPlatform = DBMSPlatform.DB2MVS;
									continue;
								}
								if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("DB2/NT"))
								{
									this.DBMSPlatform = DBMSPlatform.DB2NT;
									continue;
								}
								if (text3.ToUpper(CultureInfo.InvariantCulture).Equals("DB2/6000"))
								{
									this.DBMSPlatform = DBMSPlatform.DB26000;
									continue;
								}
								continue;
							}
							this.SetStringProperty(connectionKey, text3);
						}
					}
				}
			}
			if (array2 != null)
			{
				if (checkForUdl == DrdaConnectionString.UdlSupport.LoadFromFile && text != null)
				{
					char[] array5 = this.LoadStringFromStorage(text);
					if (array5 != null && array5.Length != 0)
					{
						array3 = new char[num3 + array5.Length];
						this.CopyChars(array2, 0, array3, 0, num2);
						this.CopyChars(array5, 0, array3, num2, array5.Length);
						this.CopyChars(array2, num2, array3, array5.Length + num2, num3 - num2);
						array3 = this.ParseInternal(array3, DrdaConnectionString.UdlSupport.UdlAsKeyword);
					}
				}
				else
				{
					array3 = array2;
				}
			}
			return array3;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x0013C840 File Offset: 0x0013AA40
		private void CopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int length)
		{
			Buffer.BlockCopy(src, 2 * srcOffset, dst, 2 * dstOffset, 2 * length);
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x0013C854 File Offset: 0x0013AA54
		private bool IsEmpty(string str)
		{
			return str == null || str.Length == 0;
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0013C864 File Offset: 0x0013AA64
		private char[] LoadStringFromStorage(string filePath)
		{
			char[] array;
			try
			{
				FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				if (streamReader.ReadLine() != "[oledb]")
				{
					throw DrdaException.InvalidUDL();
				}
				if (streamReader.ReadLine() != "; Everything after this line is an OLE DB initstring")
				{
					throw DrdaException.InvalidUDL();
				}
				string text = streamReader.ReadLine();
				streamReader.Close();
				fileStream.Close();
				array = text.ToCharArray();
			}
			catch (Exception ex)
			{
				throw DrdaException.UdlFileError(ex);
			}
			return array;
		}

		// Token: 0x04003E33 RID: 15923
		private string _connectionString;

		// Token: 0x04003E34 RID: 15924
		private bool _isInitialized;

		// Token: 0x04003E35 RID: 15925
		private string[] _connInfo;

		// Token: 0x04003E36 RID: 15926
		private string _shadowCatalog;

		// Token: 0x04003E37 RID: 15927
		private string _integratedSecurity;

		// Token: 0x04003E38 RID: 15928
		private static Dictionary<string, ConnectionKey> _keywords = new Dictionary<string, ConnectionKey>(128, StringComparer.OrdinalIgnoreCase)
		{
			{
				"Data Source",
				ConnectionKey.KEY_DSN
			},
			{
				"User Id",
				ConnectionKey.KEY_USERID
			},
			{
				"Password",
				ConnectionKey.KEY_PWD
			},
			{
				"Authentication",
				ConnectionKey.KEY_AUTHENTICATION
			},
			{
				"CertificateCN",
				ConnectionKey.KEY_CERTIFICATECN
			},
			{
				"Initial Catalog",
				ConnectionKey.KEY_RDBNAME
			},
			{
				"Appc Remote LU Alias",
				ConnectionKey.KEY_REMOTELU
			},
			{
				"Appc Local LU Alias",
				ConnectionKey.KEY_LOCALLU
			},
			{
				"Appc Mode Name",
				ConnectionKey.KEY_APPCMODE
			},
			{
				"Network Transport Library",
				ConnectionKey.KEY_NETLIB
			},
			{
				"Host CCSID",
				ConnectionKey.KEY_HOSTCCSID
			},
			{
				"PC Code Page",
				ConnectionKey.KEY_PCCODEPAGE
			},
			{
				"Network Address",
				ConnectionKey.KEY_NETADDRESS
			},
			{
				"Network Port",
				ConnectionKey.KEY_NETPORT
			},
			{
				"Package Collection",
				ConnectionKey.KEY_PACKAGECOL
			},
			{
				"Default Schema",
				ConnectionKey.KEY_CATALOGCOL
			},
			{
				"Alternate TP Name",
				ConnectionKey.KEY_TPNAME
			},
			{
				"Units of Work",
				ConnectionKey.KEY_UNITSOFWORK
			},
			{
				"Default Qualifier",
				ConnectionKey.KEY_QUALIFIERCOL
			},
			{
				"DBMS Platform",
				ConnectionKey.KEY_DBMSPLATFORM
			},
			{
				"Affiliate Application",
				ConnectionKey.KEY_AFFILIATEAPP
			},
			{
				"Appc Security Type",
				ConnectionKey.KEY_APPCSECTYPE
			},
			{
				"Use Early Metadata",
				ConnectionKey.KEY_EARLYMETADATA
			},
			{
				"Client Application Name",
				ConnectionKey.KEY_CLIENTAPPNAME
			},
			{
				"Client User ID",
				ConnectionKey.KEY_CLIENTUSERID
			},
			{
				"Client Accounting",
				ConnectionKey.KEY_CLIENTACCOUNTING
			},
			{
				"Client Workstation Name",
				ConnectionKey.KEY_CLIENTWORKSTATION
			},
			{
				"Defer Prepare",
				ConnectionKey.KEY_DEFERPREPARE
			},
			{
				"Principle Name",
				ConnectionKey.KEY_SECPRINCIPLE
			},
			{
				"Shadow Catalog",
				ConnectionKey.KEY_SHADOWCATALOG
			},
			{
				"Mode",
				ConnectionKey.KEY_MODE
			},
			{
				"File Name",
				ConnectionKey.KEY_FILENAME
			},
			{
				"Integrated Security",
				ConnectionKey.KEY_INTSECURITY
			},
			{
				"Persist Security Info",
				ConnectionKey.KEY_PERSISTSECINFO
			},
			{
				"Extended Properties",
				ConnectionKey.KEY_EXTPROPS
			},
			{
				"Pooling",
				ConnectionKey.KEY_CONNPOOLING
			},
			{
				"Provider",
				ConnectionKey.KEY_PROVIDER
			},
			{
				"AllowNullChars",
				ConnectionKey.KEY_ALLOWNULLCHARS
			},
			{
				"Rowset Cache Size",
				ConnectionKey.KEY_ROWSETCACHESIZE
			},
			{
				"DateTime as Char",
				ConnectionKey.KEY_DATETIMEASCHAR
			},
			{
				"Decimal As Numeric",
				ConnectionKey.KEY_DECIMALASNUMERIC
			},
			{
				"Connect Timeout",
				ConnectionKey.KEY_TIMEOUT
			},
			{
				"Max Pool Size",
				ConnectionKey.KEY_MAXPOOLSIZE
			},
			{
				"Dictionary",
				ConnectionKey.KEY_DICTIONARY
			},
			{
				"BinaryCodePage",
				ConnectionKey.KEY_BINARYCODEPAGE
			},
			{
				"Datetime As Date",
				ConnectionKey.KEY_DATETIMEASDATE
			},
			{
				"AutoCommit",
				ConnectionKey.KEY_AUTOCOMMIT
			},
			{
				"Database Name",
				ConnectionKey.KEY_DATABASENAME
			},
			{
				"LoadBalancing",
				ConnectionKey.KEY_LOADBALANCING
			},
			{
				"LiteralReplacement",
				ConnectionKey.KEY_LITERALREPLACEMENT
			},
			{
				"SpecialRegisters",
				ConnectionKey.KEY_SPECIALREGISTERS
			},
			{
				"Process Binary as Character",
				ConnectionKey.KEY_BINASCHAR
			},
			{
				"Gateway",
				ConnectionKey.KEY_GATEWAY
			},
			{
				"Convert to BigEndian",
				ConnectionKey.KEY_CONVERTTOBIGENDIAN
			},
			{
				"Use HIS2013 Constants",
				ConnectionKey.KEY_USEHIS2013CONSTANTS
			},
			{
				"XML As Binary",
				ConnectionKey.KEY_XMLASBINARY
			},
			{
				"Encryption Algorithm",
				ConnectionKey.KEY_ENCRYPTIONALGORITHM
			},
			{
				"Use Accelerator",
				ConnectionKey.KEY_USEACCELERATOR
			},
			{
				"Bulk Copy Schema",
				ConnectionKey.KEY_BULKCOPYSCHEMA
			},
			{
				"IMS DB",
				ConnectionKey.KEY_IMSDB
			},
			{
				"uid",
				ConnectionKey.KEY_USERID
			},
			{
				"pwd",
				ConnectionKey.KEY_PWD
			},
			{
				"Auth Method",
				ConnectionKey.KEY_AUTHENTICATION
			},
			{
				"certificate common name",
				ConnectionKey.KEY_CERTIFICATECN
			},
			{
				"rdb",
				ConnectionKey.KEY_RDBNAME
			},
			{
				"initcat",
				ConnectionKey.KEY_RDBNAME
			},
			{
				"rdbname",
				ConnectionKey.KEY_RDBNAME
			},
			{
				"defsch",
				ConnectionKey.KEY_CATALOGCOL
			},
			{
				"pkgcol",
				ConnectionKey.KEY_CATALOGCOL
			},
			{
				"ccsid",
				ConnectionKey.KEY_HOSTCCSID
			},
			{
				"hostlocale",
				ConnectionKey.KEY_HOSTCCSID
			},
			{
				"pclocale",
				ConnectionKey.KEY_PCCODEPAGE
			},
			{
				"pccodepage",
				ConnectionKey.KEY_PCCODEPAGE
			},
			{
				"codepage",
				ConnectionKey.KEY_PCCODEPAGE
			},
			{
				"timeout",
				ConnectionKey.KEY_TIMEOUT
			},
			{
				"time out",
				ConnectionKey.KEY_TIMEOUT
			},
			{
				"defprep",
				ConnectionKey.KEY_DEFERPREPARE
			},
			{
				"uow",
				ConnectionKey.KEY_UNITSOFWORK
			},
			{
				"netlib",
				ConnectionKey.KEY_NETLIB
			},
			{
				"defqual",
				ConnectionKey.KEY_QUALIFIERCOL
			},
			{
				"appcsec",
				ConnectionKey.KEY_APPCSECTYPE
			},
			{
				"early metadata",
				ConnectionKey.KEY_EARLYMETADATA
			},
			{
				"earlymetadata",
				ConnectionKey.KEY_EARLYMETADATA
			},
			{
				"client app name",
				ConnectionKey.KEY_CLIENTAPPNAME
			},
			{
				"clientappname",
				ConnectionKey.KEY_CLIENTAPPNAME
			},
			{
				"clientuserid",
				ConnectionKey.KEY_CLIENTUSERID
			},
			{
				"clientaccounting",
				ConnectionKey.KEY_CLIENTACCOUNTING
			},
			{
				"clientworkstation",
				ConnectionKey.KEY_CLIENTWORKSTATION
			},
			{
				"netport",
				ConnectionKey.KEY_NETPORT
			},
			{
				"remote_system",
				ConnectionKey.KEY_NETADDRESS
			},
			{
				"netaddr",
				ConnectionKey.KEY_NETADDRESS
			},
			{
				"locallu",
				ConnectionKey.KEY_LOCALLU
			},
			{
				"llu",
				ConnectionKey.KEY_LOCALLU
			},
			{
				"remoatelu",
				ConnectionKey.KEY_REMOTELU
			},
			{
				"rlu",
				ConnectionKey.KEY_REMOTELU
			},
			{
				"plu",
				ConnectionKey.KEY_REMOTELU
			},
			{
				"appcmode",
				ConnectionKey.KEY_MODE
			},
			{
				"modename",
				ConnectionKey.KEY_MODE
			},
			{
				"tpname",
				ConnectionKey.KEY_TPNAME
			},
			{
				"filename",
				ConnectionKey.KEY_FILENAME
			},
			{
				"file",
				ConnectionKey.KEY_FILENAME
			},
			{
				"connection pooling",
				ConnectionKey.KEY_CONNPOOLING
			},
			{
				"cache authentication",
				ConnectionKey.KEY_CONNPOOLING
			},
			{
				"allow null chars",
				ConnectionKey.KEY_ALLOWNULLCHARS
			},
			{
				"datetimeaschar",
				ConnectionKey.KEY_DATETIMEASCHAR
			},
			{
				"decimalasnumeric",
				ConnectionKey.KEY_DECIMALASNUMERIC
			},
			{
				"rowsetcachesize",
				ConnectionKey.KEY_ROWSETCACHESIZE
			},
			{
				"connection timeout",
				ConnectionKey.KEY_TIMEOUT
			},
			{
				"binary codepage",
				ConnectionKey.KEY_BINARYCODEPAGE
			},
			{
				"datetimeasdate",
				ConnectionKey.KEY_DATETIMEASDATE
			},
			{
				"auto commit",
				ConnectionKey.KEY_AUTOCOMMIT
			},
			{
				"databasename",
				ConnectionKey.KEY_DATABASENAME
			},
			{
				"load balancing",
				ConnectionKey.KEY_LOADBALANCING
			},
			{
				"literal replacement",
				ConnectionKey.KEY_LITERALREPLACEMENT
			},
			{
				"special registers",
				ConnectionKey.KEY_SPECIALREGISTERS
			},
			{
				"shadowcatalog",
				ConnectionKey.KEY_SHADOWCATALOG
			},
			{
				"binaschar",
				ConnectionKey.KEY_BINASCHAR
			},
			{
				"ibmgateway",
				ConnectionKey.KEY_GATEWAY
			},
			{
				"convert to big endian",
				ConnectionKey.KEY_CONVERTTOBIGENDIAN
			},
			{
				"useHIS2013constants",
				ConnectionKey.KEY_USEHIS2013CONSTANTS
			},
			{
				"xmlasbinary",
				ConnectionKey.KEY_XMLASBINARY
			},
			{
				"encryptionalgorithm",
				ConnectionKey.KEY_ENCRYPTIONALGORITHM
			},
			{
				"useaccelerator",
				ConnectionKey.KEY_USEACCELERATOR
			},
			{
				"bulkcopyschema",
				ConnectionKey.KEY_BULKCOPYSCHEMA
			},
			{
				"imsdb",
				ConnectionKey.KEY_IMSDB
			}
		};

		// Token: 0x04003E39 RID: 15929
		internal const string ConnectionStringFieldName = "ConnectionString";

		// Token: 0x020009E8 RID: 2536
		private enum PARSERSTATE
		{
			// Token: 0x04003E3C RID: 15932
			NothingYet = 1,
			// Token: 0x04003E3D RID: 15933
			Key,
			// Token: 0x04003E3E RID: 15934
			KeyEqual,
			// Token: 0x04003E3F RID: 15935
			KeyEnd,
			// Token: 0x04003E40 RID: 15936
			UnquotedValue,
			// Token: 0x04003E41 RID: 15937
			DoubleQuoteValue,
			// Token: 0x04003E42 RID: 15938
			DoubleQuoteValueQuote,
			// Token: 0x04003E43 RID: 15939
			DoubleQuoteValueEnd,
			// Token: 0x04003E44 RID: 15940
			SingleQuoteValue,
			// Token: 0x04003E45 RID: 15941
			SingleQuoteValueQuote,
			// Token: 0x04003E46 RID: 15942
			SingleQuoteValueEnd,
			// Token: 0x04003E47 RID: 15943
			NullTermination
		}

		// Token: 0x020009E9 RID: 2537
		private enum UdlSupport
		{
			// Token: 0x04003E49 RID: 15945
			LoadFromFile,
			// Token: 0x04003E4A RID: 15946
			UdlAsKeyword,
			// Token: 0x04003E4B RID: 15947
			ThrowIfFound
		}
	}
}
