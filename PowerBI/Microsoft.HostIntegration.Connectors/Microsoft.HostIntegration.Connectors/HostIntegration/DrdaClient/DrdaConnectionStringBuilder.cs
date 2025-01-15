using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009F0 RID: 2544
	public sealed class DrdaConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x06004EB7 RID: 20151 RVA: 0x0013CB68 File Offset: 0x0013AD68
		static DrdaConnectionStringBuilder()
		{
			DrdaConnectionProperty[] array = new DrdaConnectionProperty[59];
			array[0] = new DrdaConnectionProperty("Data Source", ConnectionKey.KEY_DSN, string.Empty, new string[0], null, 128);
			array[1] = new DrdaConnectionProperty("User Id", ConnectionKey.KEY_USERID, string.Empty, new string[] { "uid" }, null, 128);
			array[2] = new DrdaConnectionProperty("CertificateCN", ConnectionKey.KEY_CERTIFICATECN, string.Empty, new string[] { "certificate common name" }, null, 128);
			array[3] = new DrdaConnectionProperty("Password", ConnectionKey.KEY_PWD, string.Empty, new string[] { "pwd" }, null, 128);
			int num = 4;
			string text = "Authentication";
			ConnectionKey connectionKey = ConnectionKey.KEY_AUTHENTICATION;
			string text2 = "Server";
			string[] array2 = new string[] { "Auth Method" };
			object[] array3 = new string[] { "Server", "Server_Encrypt_Pwd", "Server_Encrypt_UsrPwd", "Data_Encrypt" };
			array[num] = new DrdaConnectionProperty(text, connectionKey, text2, array2, array3, 128);
			array[5] = new DrdaConnectionProperty("Initial Catalog", ConnectionKey.KEY_RDBNAME, string.Empty, new string[] { "rdb", "initcat", "rdbname" }, null, 128);
			array[6] = new DrdaConnectionProperty("Appc Remote LU Alias", ConnectionKey.KEY_REMOTELU, string.Empty, new string[] { "remoatelu", "rlu", "plu" }, null, 128);
			array[7] = new DrdaConnectionProperty("Appc Local LU Alias", ConnectionKey.KEY_LOCALLU, string.Empty, new string[] { "locallu", "llu" }, null, 128);
			array[8] = new DrdaConnectionProperty("Appc Mode Name", ConnectionKey.KEY_APPCMODE, string.Empty, new string[] { "appcmode", "modename" }, null, 128);
			array[9] = new DrdaConnectionProperty("Network Transport Library", ConnectionKey.KEY_NETLIB, "TCP", new string[] { "netlib" }, null, 128);
			array[10] = new DrdaConnectionProperty("Host CCSID", ConnectionKey.KEY_HOSTCCSID, 1208, new string[] { "ccsid", "hostlocale" }, 0, 65535);
			array[11] = new DrdaConnectionProperty("PC Code Page", ConnectionKey.KEY_PCCODEPAGE, 1252, new string[] { "pclocale", "pccodepage", "codepage" }, 0, 65535);
			array[12] = new DrdaConnectionProperty("Network Address", ConnectionKey.KEY_NETADDRESS, string.Empty, new string[] { "remote_system", "netaddr" }, null, 128);
			array[13] = new DrdaConnectionProperty("Network Port", ConnectionKey.KEY_NETPORT, 446, new string[] { "netport" }, 0, 65535);
			array[14] = new DrdaConnectionProperty("Package Collection", ConnectionKey.KEY_PACKAGECOL, string.Empty, new string[] { "pkgcol" }, null, 128);
			array[15] = new DrdaConnectionProperty("Default Schema", ConnectionKey.KEY_CATALOGCOL, string.Empty, new string[] { "defsch" }, null, 128);
			array[16] = new DrdaConnectionProperty("Alternate TP Name", ConnectionKey.KEY_TPNAME, string.Empty, new string[] { "tpname" }, null, 128);
			array[17] = new DrdaConnectionProperty("Process Binary as Character", ConnectionKey.KEY_BINASCHAR, false, new string[] { "binaschar" }, null);
			array[18] = new DrdaConnectionProperty("Units of Work", ConnectionKey.KEY_UNITSOFWORK, UnitsOfWorkType.RUW, new string[] { "uow" }, new object[]
			{
				UnitsOfWorkType.RUW,
				UnitsOfWorkType.DUW
			});
			int num2 = 19;
			string text3 = "Provider";
			ConnectionKey connectionKey2 = ConnectionKey.KEY_PROVIDER;
			string empty = string.Empty;
			string[] array4 = new string[0];
			array3 = new string[] { "DB2DOTNET", "DB2OLEDB", "DB2OLEDB.1", "MSINFORMIX", "MSINFORMIX.1" };
			array[num2] = new DrdaConnectionProperty(text3, connectionKey2, empty, array4, array3, 18);
			array[20] = new DrdaConnectionProperty("Default Qualifier", ConnectionKey.KEY_QUALIFIERCOL, string.Empty, new string[] { "defqual" }, null, 128);
			array[21] = new DrdaConnectionProperty("DBMS Platform", ConnectionKey.KEY_DBMSPLATFORM, DBMSPlatform.DB2MVS, null, new object[]
			{
				DBMSPlatform.DB26000,
				DBMSPlatform.DB2AS400,
				DBMSPlatform.DB2MVS,
				DBMSPlatform.DB2NT
			});
			array[22] = new DrdaConnectionProperty("Affiliate Application", ConnectionKey.KEY_AFFILIATEAPP, string.Empty, new string[0], null, 128);
			array[23] = new DrdaConnectionProperty("Appc Security Type", ConnectionKey.KEY_APPCSECTYPE, AppcSecurity.Program, new string[] { "appcsec" }, new object[]
			{
				AppcSecurity.Program,
				AppcSecurity.Same
			});
			array[24] = new DrdaConnectionProperty("Use Early Metadata", ConnectionKey.KEY_EARLYMETADATA, false, new string[] { "early metadata", "earlymetadata" }, null);
			array[25] = new DrdaConnectionProperty("Client Application Name", ConnectionKey.KEY_CLIENTAPPNAME, string.Empty, new string[] { "client app name", "clientappname" }, null, 18);
			array[26] = new DrdaConnectionProperty("Client User ID", ConnectionKey.KEY_CLIENTUSERID, string.Empty, new string[] { "clientuserid" }, null, 16);
			array[27] = new DrdaConnectionProperty("Client Accounting", ConnectionKey.KEY_CLIENTACCOUNTING, string.Empty, new string[] { "clientaccounting" }, null, 255);
			array[28] = new DrdaConnectionProperty("Client Workstation Name", ConnectionKey.KEY_CLIENTWORKSTATION, string.Empty, new string[] { "clientworkstation" }, null, 18);
			array[29] = new DrdaConnectionProperty("Defer Prepare", ConnectionKey.KEY_DEFERPREPARE, false, new string[] { "defprep" }, null);
			array[30] = new DrdaConnectionProperty("File Name", ConnectionKey.KEY_FILENAME, string.Empty, new string[] { "filename", "file" }, null, 512);
			int num3 = 31;
			string text4 = "Integrated Security";
			ConnectionKey connectionKey3 = ConnectionKey.KEY_INTSECURITY;
			string empty2 = string.Empty;
			string[] array5 = new string[0];
			array3 = new string[] { "SSPI" };
			array[num3] = new DrdaConnectionProperty(text4, connectionKey3, empty2, array5, array3, 128);
			array[32] = new DrdaConnectionProperty("Persist Security Info", ConnectionKey.KEY_PERSISTSECINFO, false, null, null);
			array[33] = new DrdaConnectionProperty("Mode", ConnectionKey.KEY_MODE, string.Empty, null, null, 32);
			array[34] = new DrdaConnectionProperty("Extended Properties", ConnectionKey.KEY_EXTPROPS, string.Empty, new string[0], null, 1024);
			array[35] = new DrdaConnectionProperty("Pooling", ConnectionKey.KEY_CONNPOOLING, false, new string[] { "connection pooling", "cache authentication" }, null);
			array[36] = new DrdaConnectionProperty("Principle Name", ConnectionKey.KEY_SECPRINCIPLE, string.Empty, new string[0], null, 128);
			array[37] = new DrdaConnectionProperty("Shadow Catalog", ConnectionKey.KEY_SHADOWCATALOG, string.Empty, new string[0], null, 128);
			array[38] = new DrdaConnectionProperty("AllowNullChars", ConnectionKey.KEY_ALLOWNULLCHARS, false, new string[] { "allow null chars" }, null);
			array[39] = new DrdaConnectionProperty("Rowset Cache Size", ConnectionKey.KEY_ROWSETCACHESIZE, 0, new string[] { "rowsetcachesize" }, 0, 300);
			array[40] = new DrdaConnectionProperty("DateTime as Char", ConnectionKey.KEY_DATETIMEASCHAR, false, new string[] { "datetimeaschar" }, null);
			array[41] = new DrdaConnectionProperty("Decimal As Numeric", ConnectionKey.KEY_DECIMALASNUMERIC, false, new string[] { "decimalasnumeric" }, null);
			array[42] = new DrdaConnectionProperty("Connect Timeout", ConnectionKey.KEY_TIMEOUT, 15, new string[] { "connection timeout" }, int.MinValue, int.MaxValue);
			array[43] = new DrdaConnectionProperty("Max Pool Size", ConnectionKey.KEY_MAXPOOLSIZE, 100, new string[0], -1, int.MaxValue);
			array[44] = new DrdaConnectionProperty("BinaryCodePage", ConnectionKey.KEY_BINARYCODEPAGE, 0, new string[] { "binary codepage" }, 0, 65535);
			array[45] = new DrdaConnectionProperty("Datetime As Date", ConnectionKey.KEY_DATETIMEASDATE, false, new string[] { "datetimeasdate" }, null);
			array[46] = new DrdaConnectionProperty("Database Name", ConnectionKey.KEY_DATABASENAME, string.Empty, new string[] { "databasename" }, null, 128);
			array[47] = new DrdaConnectionProperty("AutoCommit", ConnectionKey.KEY_AUTOCOMMIT, true, new string[] { "auto commit" }, null);
			array[48] = new DrdaConnectionProperty("LoadBalancing", ConnectionKey.KEY_LOADBALANCING, false, new string[] { "load balancing" }, null);
			array[49] = new DrdaConnectionProperty("LiteralReplacement", ConnectionKey.KEY_LITERALREPLACEMENT, false, new string[] { "literal replacement" }, null);
			array[50] = new DrdaConnectionProperty("SpecialRegisters", ConnectionKey.KEY_SPECIALREGISTERS, string.Empty, new string[] { "special registers" }, null, int.MaxValue);
			array[51] = new DrdaConnectionProperty("Gateway", ConnectionKey.KEY_GATEWAY, false, new string[] { "ibmgateway" }, null);
			array[52] = new DrdaConnectionProperty("Convert to BigEndian", ConnectionKey.KEY_CONVERTTOBIGENDIAN, false, new string[] { "convert to big endian" }, null);
			array[53] = new DrdaConnectionProperty("Use Accelerator", ConnectionKey.KEY_USEACCELERATOR, false, new string[] { "useaccelerator" }, null);
			array[54] = new DrdaConnectionProperty("Use HIS2013 Constants", ConnectionKey.KEY_USEHIS2013CONSTANTS, false, new string[] { "useHIS2013constants" }, null);
			array[55] = new DrdaConnectionProperty("XML As Binary", ConnectionKey.KEY_XMLASBINARY, false, new string[] { "xmlasbinary" }, null);
			array[56] = new DrdaConnectionProperty("Bulk Copy Schema", ConnectionKey.KEY_BULKCOPYSCHEMA, false, new string[] { "bulkcopyschema" }, null);
			int num4 = 57;
			string text5 = "Encryption Algorithm";
			ConnectionKey connectionKey4 = ConnectionKey.KEY_ENCRYPTIONALGORITHM;
			string text6 = "Encryption Algorithm";
			string[] array6 = new string[] { "encryptionalgorithm" };
			array3 = new string[] { "DES", "AES" };
			array[num4] = new DrdaConnectionProperty(text5, connectionKey4, text6, array6, array3, 128);
			array[58] = new DrdaConnectionProperty("IMS DB", ConnectionKey.KEY_IMSDB, false, new string[] { "imsdb" }, null);
			DrdaConnectionStringBuilder._properties = array;
			Dictionary<string, DrdaConnectionProperty> dictionary = new Dictionary<string, DrdaConnectionProperty>(50, StringComparer.OrdinalIgnoreCase);
			DrdaConnectionStringBuilder._validKeywords = new string[DrdaConnectionStringBuilder._properties.Length];
			DrdaConnectionStringBuilder._propertyIndex = new DrdaConnectionProperty[64];
			for (int i = 0; i < DrdaConnectionStringBuilder._properties.Length; i++)
			{
				DrdaConnectionStringBuilder._propertyIndex[(int)DrdaConnectionStringBuilder._properties[i].Key] = DrdaConnectionStringBuilder._properties[i];
				DrdaConnectionStringBuilder._validKeywords[i] = DrdaConnectionStringBuilder._properties[i].Name;
				dictionary.Add(DrdaConnectionStringBuilder._properties[i].Name, DrdaConnectionStringBuilder._properties[i]);
				for (int j = 0; j < DrdaConnectionStringBuilder._properties[i].Synonyms.Length; j++)
				{
					dictionary.Add(DrdaConnectionStringBuilder._properties[i].Synonyms[j], DrdaConnectionStringBuilder._properties[i]);
				}
			}
			DrdaConnectionStringBuilder._keywords = dictionary;
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0013D60B File Offset: 0x0013B80B
		public DrdaConnectionStringBuilder()
		{
			this._connInfo = new string[64];
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x0013D620 File Offset: 0x0013B820
		public DrdaConnectionStringBuilder(string connectionString)
			: this()
		{
			if (connectionString != null && connectionString.Length > 0)
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0013D63C File Offset: 0x0013B83C
		private ConnectionKey GetIndex(string keyword)
		{
			DrdaException.CheckArgumentNull(keyword, "keyword");
			DrdaConnectionProperty drdaConnectionProperty;
			if (DrdaConnectionStringBuilder._keywords.TryGetValue(keyword, out drdaConnectionProperty))
			{
				return drdaConnectionProperty.Key;
			}
			throw DrdaException.KeywordNotSupported(keyword);
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0013D670 File Offset: 0x0013B870
		public override bool ContainsKey(string keyword)
		{
			DrdaException.CheckArgumentNull(keyword, "keyword");
			return DrdaConnectionStringBuilder._keywords.ContainsKey(keyword);
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x0013D688 File Offset: 0x0013B888
		public override void Clear()
		{
			base.Clear();
			for (int i = 0; i < DrdaConnectionStringBuilder._properties.Length; i++)
			{
				this.Reset(DrdaConnectionStringBuilder._properties[i]);
			}
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0013D6BC File Offset: 0x0013B8BC
		public override bool Remove(string keyword)
		{
			DrdaException.CheckArgumentNull(keyword, "keyword");
			DrdaConnectionProperty drdaConnectionProperty;
			if (DrdaConnectionStringBuilder._keywords.TryGetValue(keyword, out drdaConnectionProperty))
			{
				base.Remove(drdaConnectionProperty.Name);
				this.Reset(drdaConnectionProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x0013D6FA File Offset: 0x0013B8FA
		private void Reset(DrdaConnectionProperty property)
		{
			this._connInfo[(int)property.Key] = property.ConvertToString(property.DefaultValue);
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x0013D715 File Offset: 0x0013B915
		public override ICollection Keys
		{
			get
			{
				return new ArrayList(DrdaConnectionStringBuilder._validKeywords);
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x0013D724 File Offset: 0x0013B924
		public override ICollection Values
		{
			get
			{
				object[] array = new object[DrdaConnectionStringBuilder._properties.Length];
				for (int i = 0; i < DrdaConnectionStringBuilder._properties.Length; i++)
				{
					array[i] = this.GetAt(DrdaConnectionStringBuilder._properties[i].Key);
				}
				return new ArrayList(array);
			}
		}

		// Token: 0x1700130B RID: 4875
		public override object this[string keyword]
		{
			get
			{
				ConnectionKey index = this.GetIndex(keyword);
				return this.GetAt(index);
			}
			set
			{
				if (keyword.ToUpper(CultureInfo.InvariantCulture).Equals("Derive Parameters".ToUpper(CultureInfo.InvariantCulture)))
				{
					return;
				}
				Trace.MessageVerboseTrace("DrdaConnectionStringBuilder.set_Item keyword='{0}'\n", keyword);
				if (value != null)
				{
					switch (this.GetIndex(keyword))
					{
					case ConnectionKey.KEY_DSN:
						this.DataSource = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_USERID:
						this.UserId = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_PWD:
						this.Password = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_RDBNAME:
						this.InitialCatalog = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_REMOTELU:
						this.RemoteLU = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_LOCALLU:
						this.LocalLU = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_APPCMODE:
						this.AppcMode = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_NETLIB:
						this.NetworkTransportLibrary = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_HOSTCCSID:
						this.HostCCSID = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_PCCODEPAGE:
						this.PCCodePage = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_NETADDRESS:
						this.NetworkAddress = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_NETPORT:
						this.NetworkPort = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_PACKAGECOL:
						this.PackageCollection = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_CATALOGCOL:
						this.DefaultSchema = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_TPNAME:
						this.AlternateTPName = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_TIMEOUT:
						this.ConnectTimeout = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_UNITSOFWORK:
						this.UnitsOfWork = DrdaConnectionStringBuilder.ConvertToUnitsOfWorkType(value);
						return;
					case ConnectionKey.KEY_PROVIDER:
						this.Provider = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_QUALIFIERCOL:
						this.DefaultQualifier = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_DBMSPLATFORM:
						this.DBMSPlatform = DrdaConnectionStringBuilder.ConvertToDBMSPlatform(value);
						return;
					case ConnectionKey.KEY_AFFILIATEAPP:
						this.AffiliateApplication = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_APPCSECTYPE:
						this.AppcSecurityType = DrdaConnectionStringBuilder.ConvertToAppcSecurityType(value);
						return;
					case ConnectionKey.KEY_EARLYMETADATA:
						this.UseEarlyMetadata = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_CLIENTAPPNAME:
						this.ClientApplicationName = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_DEFERPREPARE:
						this.DeferPrepare = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_SECPRINCIPLE:
						this.PrincipleName = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_DATETIMEASCHAR:
						this.DateTimeAsChar = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_ROWSETCACHESIZE:
						this.RowsetCacheSize = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_BINARYCODEPAGE:
						this.BinaryCodePage = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_MAXPOOLSIZE:
						this.MaxPoolSize = DrdaConnectionStringBuilder.ConvertToInt32(value);
						return;
					case ConnectionKey.KEY_DATETIMEASDATE:
						this.DateTimeAsDate = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_AUTOCOMMIT:
						this.AutoCommit = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_DATABASENAME:
						this.DatabaseName = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_AUTHENTICATION:
						this.Authentication = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_CERTIFICATECN:
						this.CertificateCN = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_CLIENTWORKSTATION:
						this.ClientWorkstationName = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_CLIENTACCOUNTING:
						this.ClientAccounting = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_CLIENTUSERID:
						this.ClientUserID = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_DECIMALASNUMERIC:
						this.DecimalAsNumeric = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_ALLOWNULLCHARS:
						this.AllowNullChars = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_LOADBALANCING:
						this.LoadBalancing = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_SHADOWCATALOG:
						this.ShadowCatalog = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_BINASCHAR:
						this.BinaryAsCharacter = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_FILENAME:
						this.FileName = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_INTSECURITY:
						this.IntegratedSecurity = DrdaConnectionStringBuilder.ConvertToIntegratedSecurity(value);
						return;
					case ConnectionKey.KEY_PERSISTSECINFO:
						this.PersistSecurityInfo = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_MODE:
						this.SetPropertyValue(ConnectionKey.KEY_MODE, value);
						return;
					case ConnectionKey.KEY_EXTPROPS:
						this.ExtendedProperties = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_CONNPOOLING:
						this.Pooling = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_LITERALREPLACEMENT:
						this.LiteralReplacement = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_SPECIALREGISTERS:
						this.SpecialRegisters = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_GATEWAY:
						this.Gateway = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_CONVERTTOBIGENDIAN:
						this.ConvertToBigEndian = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_USEHIS2013CONSTANTS:
						this.UseHIS2013Constants = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_XMLASBINARY:
						this.XMLAsBinary = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_ENCRYPTIONALGORITHM:
						this.EncryptionAlgorithm = DrdaConnectionStringBuilder.ConvertToString(value);
						return;
					case ConnectionKey.KEY_USEACCELERATOR:
						this.UseAccelerator = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_BULKCOPYSCHEMA:
						this.BulkCopySchema = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					case ConnectionKey.KEY_IMSDB:
						this.IMSDB = DrdaConnectionStringBuilder.ConvertToBoolean(value);
						return;
					}
					throw DrdaException.KeywordNotSupported(keyword);
				}
				this.Remove(keyword);
			}
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x0013DBE8 File Offset: 0x0013BDE8
		private object GetAt(ConnectionKey index)
		{
			switch (index)
			{
			case ConnectionKey.KEY_DSN:
				return this.DataSource;
			case ConnectionKey.KEY_USERID:
				return this.UserId;
			case ConnectionKey.KEY_PWD:
				return this.Password;
			case ConnectionKey.KEY_RDBNAME:
				return this.InitialCatalog;
			case ConnectionKey.KEY_REMOTELU:
				return this.RemoteLU;
			case ConnectionKey.KEY_LOCALLU:
				return this.LocalLU;
			case ConnectionKey.KEY_APPCMODE:
				return this.AppcMode;
			case ConnectionKey.KEY_NETLIB:
				return this.NetworkTransportLibrary;
			case ConnectionKey.KEY_HOSTCCSID:
				return this.HostCCSID;
			case ConnectionKey.KEY_PCCODEPAGE:
				return this.PCCodePage;
			case ConnectionKey.KEY_NETADDRESS:
				return this.NetworkAddress;
			case ConnectionKey.KEY_NETPORT:
				return this.NetworkPort;
			case ConnectionKey.KEY_PACKAGECOL:
				return this.PackageCollection;
			case ConnectionKey.KEY_CATALOGCOL:
				return this.DefaultSchema;
			case ConnectionKey.KEY_TPNAME:
				return this.AlternateTPName;
			case ConnectionKey.KEY_TIMEOUT:
				return this.ConnectTimeout;
			case ConnectionKey.KEY_UNITSOFWORK:
				return this.UnitsOfWork;
			case ConnectionKey.KEY_PROVIDER:
				return this.Provider;
			case ConnectionKey.KEY_QUALIFIERCOL:
				return this.DefaultQualifier;
			case ConnectionKey.KEY_DBMSPLATFORM:
				return this.DBMSPlatform;
			case ConnectionKey.KEY_AFFILIATEAPP:
				return this.AffiliateApplication;
			case ConnectionKey.KEY_APPCSECTYPE:
				return this.AppcSecurityType;
			case ConnectionKey.KEY_EARLYMETADATA:
				return this.UseEarlyMetadata;
			case ConnectionKey.KEY_CLIENTAPPNAME:
				return this.ClientApplicationName;
			case ConnectionKey.KEY_DEFERPREPARE:
				return this.DeferPrepare;
			case ConnectionKey.KEY_SECPRINCIPLE:
				return this.PrincipleName;
			case ConnectionKey.KEY_DATETIMEASCHAR:
				return this.DateTimeAsChar;
			case ConnectionKey.KEY_ROWSETCACHESIZE:
				return this.RowsetCacheSize;
			case ConnectionKey.KEY_BINARYCODEPAGE:
				return this.BinaryCodePage;
			case ConnectionKey.KEY_MAXPOOLSIZE:
				return this.MaxPoolSize;
			case ConnectionKey.KEY_DATETIMEASDATE:
				return this.DateTimeAsDate;
			case ConnectionKey.KEY_AUTOCOMMIT:
				return this.AutoCommit;
			case ConnectionKey.KEY_DATABASENAME:
				return this.DatabaseName;
			case ConnectionKey.KEY_AUTHENTICATION:
				return this.Authentication;
			case ConnectionKey.KEY_CERTIFICATECN:
				return this.CertificateCN;
			case ConnectionKey.KEY_CLIENTWORKSTATION:
				return this.ClientWorkstationName;
			case ConnectionKey.KEY_CLIENTACCOUNTING:
				return this.ClientAccounting;
			case ConnectionKey.KEY_CLIENTUSERID:
				return this.ClientUserID;
			case ConnectionKey.KEY_DECIMALASNUMERIC:
				return this.DecimalAsNumeric;
			case ConnectionKey.KEY_ALLOWNULLCHARS:
				return this.AllowNullChars;
			case ConnectionKey.KEY_LOADBALANCING:
				return this.LoadBalancing;
			case ConnectionKey.KEY_SHADOWCATALOG:
				return this.ShadowCatalog;
			case ConnectionKey.KEY_BINASCHAR:
				return this.BinaryAsCharacter;
			case ConnectionKey.KEY_FILENAME:
				return this.FileName;
			case ConnectionKey.KEY_INTSECURITY:
				return this.IntegratedSecurity;
			case ConnectionKey.KEY_PERSISTSECINFO:
				return this.PersistSecurityInfo;
			case ConnectionKey.KEY_MODE:
				return (string)this.GetPropertyValue(ConnectionKey.KEY_MODE);
			case ConnectionKey.KEY_EXTPROPS:
				return this.ExtendedProperties;
			case ConnectionKey.KEY_CONNPOOLING:
				return this.Pooling;
			case ConnectionKey.KEY_LITERALREPLACEMENT:
				return this.LiteralReplacement;
			case ConnectionKey.KEY_SPECIALREGISTERS:
				return this.SpecialRegisters;
			case ConnectionKey.KEY_GATEWAY:
				return this.Gateway;
			case ConnectionKey.KEY_CONVERTTOBIGENDIAN:
				return this.ConvertToBigEndian;
			case ConnectionKey.KEY_USEHIS2013CONSTANTS:
				return this.UseHIS2013Constants;
			case ConnectionKey.KEY_XMLASBINARY:
				return this.XMLAsBinary;
			case ConnectionKey.KEY_ENCRYPTIONALGORITHM:
				return this.EncryptionAlgorithm;
			case ConnectionKey.KEY_USEACCELERATOR:
				return this.UseAccelerator;
			case ConnectionKey.KEY_BULKCOPYSCHEMA:
				return this.BulkCopySchema;
			case ConnectionKey.KEY_IMSDB:
				return this.IMSDB;
			}
			throw DrdaException.KeywordNotSupported(index.ToString());
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x0013DF48 File Offset: 0x0013C148
		private Attribute[] GetAttributesFromCollection(AttributeCollection collection)
		{
			Attribute[] array = new Attribute[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0013DF6A File Offset: 0x0013C16A
		protected override void GetProperties(Hashtable propertyDescriptors)
		{
			base.GetProperties(propertyDescriptors);
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x0013DF74 File Offset: 0x0013C174
		private object GetPropertyValue(ConnectionKey key)
		{
			DrdaConnectionProperty drdaConnectionProperty = DrdaConnectionStringBuilder._propertyIndex[(int)key];
			if (this._connInfo[(int)drdaConnectionProperty.Key] != null)
			{
				return drdaConnectionProperty.ConvertFromString(this._connInfo[(int)drdaConnectionProperty.Key]);
			}
			return string.Empty;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x0013DFB4 File Offset: 0x0013C1B4
		private void SetPropertyValue(ConnectionKey key, object val)
		{
			DrdaConnectionProperty drdaConnectionProperty = DrdaConnectionStringBuilder._propertyIndex[(int)key];
			drdaConnectionProperty.ValidateValue(val);
			this._connInfo[(int)drdaConnectionProperty.Key] = drdaConnectionProperty.ConvertToString(val);
			base[drdaConnectionProperty.Name] = this._connInfo[(int)drdaConnectionProperty.Key];
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x0013DFFD File Offset: 0x0013C1FD
		// (set) Token: 0x06004ECA RID: 20170 RVA: 0x0013E00B File Offset: 0x0013C20B
		[DisplayName("Data Source")]
		[RefreshProperties(RefreshProperties.All)]
		public string DataSource
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_DSN);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DSN, value);
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x0013E015 File Offset: 0x0013C215
		// (set) Token: 0x06004ECC RID: 20172 RVA: 0x0013E024 File Offset: 0x0013C224
		[DisplayName("Database Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string DatabaseName
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_DATABASENAME);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DATABASENAME, value);
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004ECD RID: 20173 RVA: 0x0013E02F File Offset: 0x0013C22F
		// (set) Token: 0x06004ECE RID: 20174 RVA: 0x0013E03D File Offset: 0x0013C23D
		[DisplayName("User Id")]
		[RefreshProperties(RefreshProperties.All)]
		public string UserId
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_USERID);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_USERID, value);
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x0013E047 File Offset: 0x0013C247
		// (set) Token: 0x06004ED0 RID: 20176 RVA: 0x0013E04F File Offset: 0x0013C24F
		[DisplayName("User Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string UserName
		{
			get
			{
				return this.UserId;
			}
			set
			{
				this.UserId = value;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x0013E058 File Offset: 0x0013C258
		// (set) Token: 0x06004ED2 RID: 20178 RVA: 0x0013E066 File Offset: 0x0013C266
		[DisplayName("Password")]
		[RefreshProperties(RefreshProperties.All)]
		[PasswordPropertyText(true)]
		public string Password
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_PWD);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_PWD, value);
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x0013E070 File Offset: 0x0013C270
		// (set) Token: 0x06004ED4 RID: 20180 RVA: 0x0013E07F File Offset: 0x0013C27F
		[DisplayName("New Password")]
		[RefreshProperties(RefreshProperties.All)]
		[PasswordPropertyText(true)]
		public string NewPassword
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_NEWPWD);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_NEWPWD, value);
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06004ED5 RID: 20181 RVA: 0x0013E08A File Offset: 0x0013C28A
		// (set) Token: 0x06004ED6 RID: 20182 RVA: 0x0013E092 File Offset: 0x0013C292
		[Obsolete("DeriveParameters property has been deprecated.")]
		[DisplayName("Derive Parameters")]
		[RefreshProperties(RefreshProperties.All)]
		public bool DeriveParameters { get; set; }

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x0013E09B File Offset: 0x0013C29B
		// (set) Token: 0x06004ED8 RID: 20184 RVA: 0x0013E0A3 File Offset: 0x0013C2A3
		[Obsolete("FastLoadOptimize property has been deprecated.")]
		[DisplayName("Fast Load Optimize")]
		[RefreshProperties(RefreshProperties.All)]
		public bool FastLoadOptimize { get; set; }

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0013E0AC File Offset: 0x0013C2AC
		// (set) Token: 0x06004EDA RID: 20186 RVA: 0x0013E0BB File Offset: 0x0013C2BB
		[DisplayName("Authentication")]
		[RefreshProperties(RefreshProperties.All)]
		public string Authentication
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_AUTHENTICATION);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_AUTHENTICATION, value);
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x0013E0C6 File Offset: 0x0013C2C6
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x0013E0D5 File Offset: 0x0013C2D5
		[DisplayName("CertificateCN")]
		[RefreshProperties(RefreshProperties.All)]
		public string CertificateCN
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_CERTIFICATECN);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CERTIFICATECN, value);
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x0013E0E0 File Offset: 0x0013C2E0
		// (set) Token: 0x06004EDE RID: 20190 RVA: 0x0013E0EE File Offset: 0x0013C2EE
		[DisplayName("Appc Remote LU Alias")]
		[RefreshProperties(RefreshProperties.All)]
		public string RemoteLU
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_REMOTELU);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_REMOTELU, value);
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x0013E0F8 File Offset: 0x0013C2F8
		// (set) Token: 0x06004EE0 RID: 20192 RVA: 0x0013E106 File Offset: 0x0013C306
		[DisplayName("Initial Catalog")]
		[RefreshProperties(RefreshProperties.All)]
		public string InitialCatalog
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_RDBNAME);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_RDBNAME, value);
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x0013E110 File Offset: 0x0013C310
		// (set) Token: 0x06004EE2 RID: 20194 RVA: 0x0013E11F File Offset: 0x0013C31F
		[DisplayName("Provider")]
		[RefreshProperties(RefreshProperties.All)]
		public string Provider
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_PROVIDER);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_PROVIDER, value);
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x0013E12A File Offset: 0x0013C32A
		// (set) Token: 0x06004EE4 RID: 20196 RVA: 0x0013E138 File Offset: 0x0013C338
		[DisplayName("Appc Local LU Alias")]
		[RefreshProperties(RefreshProperties.All)]
		public string LocalLU
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_LOCALLU);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_LOCALLU, value);
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x0013E142 File Offset: 0x0013C342
		// (set) Token: 0x06004EE6 RID: 20198 RVA: 0x0013E150 File Offset: 0x0013C350
		[DisplayName("Appc Mode Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string AppcMode
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_APPCMODE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_APPCMODE, value);
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0013E15A File Offset: 0x0013C35A
		// (set) Token: 0x06004EE8 RID: 20200 RVA: 0x0013E168 File Offset: 0x0013C368
		[DisplayName("Network Transport Library")]
		[RefreshProperties(RefreshProperties.All)]
		public string NetworkTransportLibrary
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_NETLIB);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_NETLIB, value);
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x0013E172 File Offset: 0x0013C372
		// (set) Token: 0x06004EEA RID: 20202 RVA: 0x0013E181 File Offset: 0x0013C381
		[DisplayName("Network Address")]
		[RefreshProperties(RefreshProperties.All)]
		public string NetworkAddress
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_NETADDRESS);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_NETADDRESS, value);
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x0013E18C File Offset: 0x0013C38C
		// (set) Token: 0x06004EEC RID: 20204 RVA: 0x0013E19B File Offset: 0x0013C39B
		[DisplayName("Package Collection")]
		[RefreshProperties(RefreshProperties.All)]
		public string PackageCollection
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_PACKAGECOL);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_PACKAGECOL, value);
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x0013E1A6 File Offset: 0x0013C3A6
		// (set) Token: 0x06004EEE RID: 20206 RVA: 0x0013E1B5 File Offset: 0x0013C3B5
		[DisplayName("Default Schema")]
		[RefreshProperties(RefreshProperties.All)]
		public string DefaultSchema
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_CATALOGCOL);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CATALOGCOL, value);
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x0013E1C0 File Offset: 0x0013C3C0
		// (set) Token: 0x06004EF0 RID: 20208 RVA: 0x0013E1CF File Offset: 0x0013C3CF
		[DisplayName("Alternate TP Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string AlternateTPName
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_TPNAME);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_TPNAME, value);
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x0013E1DA File Offset: 0x0013C3DA
		// (set) Token: 0x06004EF2 RID: 20210 RVA: 0x0013E1E9 File Offset: 0x0013C3E9
		[DisplayName("Default Qualifier")]
		[RefreshProperties(RefreshProperties.All)]
		public string DefaultQualifier
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_QUALIFIERCOL);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_QUALIFIERCOL, value);
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x0013E1F4 File Offset: 0x0013C3F4
		// (set) Token: 0x06004EF4 RID: 20212 RVA: 0x0013E203 File Offset: 0x0013C403
		[DisplayName("Affiliate Application")]
		[RefreshProperties(RefreshProperties.All)]
		public string AffiliateApplication
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_AFFILIATEAPP);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_AFFILIATEAPP, value);
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x0013E20E File Offset: 0x0013C40E
		// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x0013E21D File Offset: 0x0013C41D
		[DisplayName("Principle Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string PrincipleName
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_SECPRINCIPLE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_SECPRINCIPLE, value);
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x0013E228 File Offset: 0x0013C428
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x0013E237 File Offset: 0x0013C437
		[DisplayName("Client Application Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string ClientApplicationName
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_CLIENTAPPNAME);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CLIENTAPPNAME, value);
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x0013E242 File Offset: 0x0013C442
		// (set) Token: 0x06004EFA RID: 20218 RVA: 0x0013E251 File Offset: 0x0013C451
		[DisplayName("Client User ID")]
		[RefreshProperties(RefreshProperties.All)]
		public string ClientUserID
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_CLIENTUSERID);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CLIENTUSERID, value);
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x0013E25C File Offset: 0x0013C45C
		// (set) Token: 0x06004EFC RID: 20220 RVA: 0x0013E26B File Offset: 0x0013C46B
		[DisplayName("Client Accounting")]
		[RefreshProperties(RefreshProperties.All)]
		public string ClientAccounting
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_CLIENTACCOUNTING);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CLIENTACCOUNTING, value);
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0013E276 File Offset: 0x0013C476
		// (set) Token: 0x06004EFE RID: 20222 RVA: 0x0013E285 File Offset: 0x0013C485
		[DisplayName("Client Workstation Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string ClientWorkstationName
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_CLIENTWORKSTATION);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CLIENTWORKSTATION, value);
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x0013E290 File Offset: 0x0013C490
		// (set) Token: 0x06004F00 RID: 20224 RVA: 0x0013E29F File Offset: 0x0013C49F
		[DisplayName("Shadow Catalog")]
		[RefreshProperties(RefreshProperties.All)]
		public string ShadowCatalog
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_SHADOWCATALOG);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_SHADOWCATALOG, value);
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x0013E2AA File Offset: 0x0013C4AA
		// (set) Token: 0x06004F02 RID: 20226 RVA: 0x0013E2B9 File Offset: 0x0013C4B9
		[DisplayName("Rowset Cache Size")]
		[RefreshProperties(RefreshProperties.All)]
		public int RowsetCacheSize
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_ROWSETCACHESIZE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_ROWSETCACHESIZE, value);
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x0013E2C9 File Offset: 0x0013C4C9
		// (set) Token: 0x06004F04 RID: 20228 RVA: 0x0013E2D8 File Offset: 0x0013C4D8
		[DisplayName("DateTime as Char")]
		[RefreshProperties(RefreshProperties.All)]
		public bool DateTimeAsChar
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_DATETIMEASCHAR);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DATETIMEASCHAR, value);
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0013E2E8 File Offset: 0x0013C4E8
		// (set) Token: 0x06004F06 RID: 20230 RVA: 0x0013E2F7 File Offset: 0x0013C4F7
		[DisplayName("Decimal As Numeric")]
		[RefreshProperties(RefreshProperties.All)]
		public bool DecimalAsNumeric
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_DECIMALASNUMERIC);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DECIMALASNUMERIC, value);
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x0013E307 File Offset: 0x0013C507
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x0013E316 File Offset: 0x0013C516
		[DisplayName("Datetime As Date")]
		[RefreshProperties(RefreshProperties.All)]
		public bool DateTimeAsDate
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_DATETIMEASDATE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DATETIMEASDATE, value);
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x0013E326 File Offset: 0x0013C526
		// (set) Token: 0x06004F0A RID: 20234 RVA: 0x0013E335 File Offset: 0x0013C535
		[DisplayName("AutoCommit")]
		[RefreshProperties(RefreshProperties.All)]
		public bool AutoCommit
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_AUTOCOMMIT);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_AUTOCOMMIT, value);
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0013E345 File Offset: 0x0013C545
		// (set) Token: 0x06004F0C RID: 20236 RVA: 0x0013E354 File Offset: 0x0013C554
		[DisplayName("File Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string FileName
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_FILENAME);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_FILENAME, value);
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06004F0D RID: 20237 RVA: 0x0013E35F File Offset: 0x0013C55F
		// (set) Token: 0x06004F0E RID: 20238 RVA: 0x0013E36E File Offset: 0x0013C56E
		[DisplayName("Extended Properties")]
		[RefreshProperties(RefreshProperties.All)]
		public string ExtendedProperties
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_EXTPROPS);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_EXTPROPS, value);
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06004F0F RID: 20239 RVA: 0x0013E379 File Offset: 0x0013C579
		// (set) Token: 0x06004F10 RID: 20240 RVA: 0x0013E387 File Offset: 0x0013C587
		[DisplayName("Host CCSID")]
		[RefreshProperties(RefreshProperties.All)]
		public int HostCCSID
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_HOSTCCSID);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_HOSTCCSID, value);
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x0013E396 File Offset: 0x0013C596
		// (set) Token: 0x06004F12 RID: 20242 RVA: 0x0013E3A5 File Offset: 0x0013C5A5
		[DisplayName("PC Code Page")]
		[RefreshProperties(RefreshProperties.All)]
		public int PCCodePage
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_PCCODEPAGE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_PCCODEPAGE, value);
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06004F13 RID: 20243 RVA: 0x0013E3B5 File Offset: 0x0013C5B5
		// (set) Token: 0x06004F14 RID: 20244 RVA: 0x0013E3C4 File Offset: 0x0013C5C4
		[DisplayName("Network Port")]
		[RefreshProperties(RefreshProperties.All)]
		public int NetworkPort
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_NETPORT);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_NETPORT, value);
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0013E3D4 File Offset: 0x0013C5D4
		// (set) Token: 0x06004F16 RID: 20246 RVA: 0x0013E3E3 File Offset: 0x0013C5E3
		[DisplayName("Max Pool Size")]
		[RefreshProperties(RefreshProperties.All)]
		public int MaxPoolSize
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_MAXPOOLSIZE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_MAXPOOLSIZE, value);
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0013E3F3 File Offset: 0x0013C5F3
		// (set) Token: 0x06004F18 RID: 20248 RVA: 0x0013E402 File Offset: 0x0013C602
		[DisplayName("Connect Timeout")]
		[RefreshProperties(RefreshProperties.All)]
		public int ConnectTimeout
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_TIMEOUT);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_TIMEOUT, value);
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06004F19 RID: 20249 RVA: 0x0013E412 File Offset: 0x0013C612
		// (set) Token: 0x06004F1A RID: 20250 RVA: 0x0013E421 File Offset: 0x0013C621
		[DisplayName("BinaryCodePage")]
		[RefreshProperties(RefreshProperties.All)]
		public int BinaryCodePage
		{
			get
			{
				return (int)this.GetPropertyValue(ConnectionKey.KEY_BINARYCODEPAGE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_BINARYCODEPAGE, value);
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x0013E431 File Offset: 0x0013C631
		// (set) Token: 0x06004F1C RID: 20252 RVA: 0x0013E440 File Offset: 0x0013C640
		[DisplayName("Units of Work")]
		[RefreshProperties(RefreshProperties.All)]
		public UnitsOfWorkType UnitsOfWork
		{
			get
			{
				return (UnitsOfWorkType)this.GetPropertyValue(ConnectionKey.KEY_UNITSOFWORK);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_UNITSOFWORK, value);
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x0013E450 File Offset: 0x0013C650
		// (set) Token: 0x06004F1E RID: 20254 RVA: 0x0013E45F File Offset: 0x0013C65F
		[DisplayName("DBMS Platform")]
		[RefreshProperties(RefreshProperties.All)]
		public DBMSPlatform DBMSPlatform
		{
			get
			{
				return (DBMSPlatform)this.GetPropertyValue(ConnectionKey.KEY_DBMSPLATFORM);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DBMSPLATFORM, value);
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0013E46F File Offset: 0x0013C66F
		// (set) Token: 0x06004F20 RID: 20256 RVA: 0x0013E47E File Offset: 0x0013C67E
		[DisplayName("Appc Security Type")]
		[RefreshProperties(RefreshProperties.All)]
		public AppcSecurity AppcSecurityType
		{
			get
			{
				return (AppcSecurity)this.GetPropertyValue(ConnectionKey.KEY_APPCSECTYPE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_APPCSECTYPE, value);
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0013E48E File Offset: 0x0013C68E
		// (set) Token: 0x06004F22 RID: 20258 RVA: 0x0013E49D File Offset: 0x0013C69D
		[DisplayName("Process Binary as Character")]
		[RefreshProperties(RefreshProperties.All)]
		public bool BinaryAsCharacter
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_BINASCHAR);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_BINASCHAR, value);
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x0013E4AD File Offset: 0x0013C6AD
		// (set) Token: 0x06004F24 RID: 20260 RVA: 0x0013E4BC File Offset: 0x0013C6BC
		[DisplayName("Use Early Metadata")]
		[RefreshProperties(RefreshProperties.All)]
		public bool UseEarlyMetadata
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_EARLYMETADATA);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_EARLYMETADATA, value);
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x0013E4CC File Offset: 0x0013C6CC
		// (set) Token: 0x06004F26 RID: 20262 RVA: 0x0013E4DB File Offset: 0x0013C6DB
		[DisplayName("Defer Prepare")]
		[RefreshProperties(RefreshProperties.All)]
		public bool DeferPrepare
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_DEFERPREPARE);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_DEFERPREPARE, value);
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x0013E4EB File Offset: 0x0013C6EB
		// (set) Token: 0x06004F28 RID: 20264 RVA: 0x0013E4FA File Offset: 0x0013C6FA
		[DisplayName("Pooling")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Pooling
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_CONNPOOLING);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CONNPOOLING, value);
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004F29 RID: 20265 RVA: 0x0013E50A File Offset: 0x0013C70A
		// (set) Token: 0x06004F2A RID: 20266 RVA: 0x0013E512 File Offset: 0x0013C712
		[DisplayName("Connection Pooling")]
		[RefreshProperties(RefreshProperties.All)]
		public bool ConnectionPooling
		{
			get
			{
				return this.Pooling;
			}
			set
			{
				this.Pooling = value;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004F2B RID: 20267 RVA: 0x0013E51B File Offset: 0x0013C71B
		// (set) Token: 0x06004F2C RID: 20268 RVA: 0x0013E52A File Offset: 0x0013C72A
		[DisplayName("Integrated Security")]
		[RefreshProperties(RefreshProperties.All)]
		public string IntegratedSecurity
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_INTSECURITY);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_INTSECURITY, value);
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x0013E535 File Offset: 0x0013C735
		// (set) Token: 0x06004F2E RID: 20270 RVA: 0x0013E544 File Offset: 0x0013C744
		[DisplayName("Persist Security Info")]
		[RefreshProperties(RefreshProperties.All)]
		public bool PersistSecurityInfo
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_PERSISTSECINFO);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_PERSISTSECINFO, value);
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x0013E554 File Offset: 0x0013C754
		// (set) Token: 0x06004F30 RID: 20272 RVA: 0x0013E563 File Offset: 0x0013C763
		[DisplayName("AllowNullChars")]
		[RefreshProperties(RefreshProperties.All)]
		public bool AllowNullChars
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_ALLOWNULLCHARS);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_ALLOWNULLCHARS, value);
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x0013E573 File Offset: 0x0013C773
		// (set) Token: 0x06004F32 RID: 20274 RVA: 0x0013E582 File Offset: 0x0013C782
		[DisplayName("LoadBalancing")]
		[RefreshProperties(RefreshProperties.All)]
		public bool LoadBalancing
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_LOADBALANCING);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_LOADBALANCING, value);
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x0013E592 File Offset: 0x0013C792
		// (set) Token: 0x06004F34 RID: 20276 RVA: 0x0013E5A1 File Offset: 0x0013C7A1
		[DisplayName("LiteralReplacement")]
		[RefreshProperties(RefreshProperties.All)]
		public bool LiteralReplacement
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_LITERALREPLACEMENT);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_LITERALREPLACEMENT, value);
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004F35 RID: 20277 RVA: 0x0013E5B1 File Offset: 0x0013C7B1
		// (set) Token: 0x06004F36 RID: 20278 RVA: 0x0013E5C0 File Offset: 0x0013C7C0
		[DisplayName("SpecialRegisters")]
		[RefreshProperties(RefreshProperties.All)]
		public string SpecialRegisters
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_SPECIALREGISTERS);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_SPECIALREGISTERS, value);
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x0013E5CB File Offset: 0x0013C7CB
		// (set) Token: 0x06004F38 RID: 20280 RVA: 0x0013E5DA File Offset: 0x0013C7DA
		[DisplayName("Gateway")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Gateway
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_GATEWAY);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_GATEWAY, value);
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x0013E5EA File Offset: 0x0013C7EA
		// (set) Token: 0x06004F3A RID: 20282 RVA: 0x0013E5F9 File Offset: 0x0013C7F9
		[DisplayName("Convert to BigEndian")]
		[RefreshProperties(RefreshProperties.All)]
		public bool ConvertToBigEndian
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_CONVERTTOBIGENDIAN);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_CONVERTTOBIGENDIAN, value);
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x0013E609 File Offset: 0x0013C809
		// (set) Token: 0x06004F3C RID: 20284 RVA: 0x0013E618 File Offset: 0x0013C818
		[DisplayName("Use Accelerator")]
		[RefreshProperties(RefreshProperties.All)]
		public bool UseAccelerator
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_USEACCELERATOR);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_USEACCELERATOR, value);
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x0013E628 File Offset: 0x0013C828
		// (set) Token: 0x06004F3E RID: 20286 RVA: 0x0013E637 File Offset: 0x0013C837
		[DisplayName("Use HIS2013 Constants")]
		[RefreshProperties(RefreshProperties.All)]
		public bool UseHIS2013Constants
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_USEHIS2013CONSTANTS);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_USEHIS2013CONSTANTS, value);
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x0013E647 File Offset: 0x0013C847
		// (set) Token: 0x06004F40 RID: 20288 RVA: 0x0013E656 File Offset: 0x0013C856
		[DisplayName("XML As Binary")]
		[RefreshProperties(RefreshProperties.All)]
		public bool XMLAsBinary
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_XMLASBINARY);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_XMLASBINARY, value);
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x0013E666 File Offset: 0x0013C866
		// (set) Token: 0x06004F42 RID: 20290 RVA: 0x0013E675 File Offset: 0x0013C875
		[DisplayName("Encryption Algorithm")]
		[RefreshProperties(RefreshProperties.All)]
		public string EncryptionAlgorithm
		{
			get
			{
				return (string)this.GetPropertyValue(ConnectionKey.KEY_ENCRYPTIONALGORITHM);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_ENCRYPTIONALGORITHM, value);
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x0013E680 File Offset: 0x0013C880
		// (set) Token: 0x06004F44 RID: 20292 RVA: 0x0013E68F File Offset: 0x0013C88F
		[DisplayName("Bulk Copy Schema")]
		[RefreshProperties(RefreshProperties.All)]
		public bool BulkCopySchema
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_BULKCOPYSCHEMA);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_BULKCOPYSCHEMA, value);
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x0013E69F File Offset: 0x0013C89F
		// (set) Token: 0x06004F46 RID: 20294 RVA: 0x0013E6AE File Offset: 0x0013C8AE
		[DisplayName("IMS DB")]
		[RefreshProperties(RefreshProperties.All)]
		public bool IMSDB
		{
			get
			{
				return (bool)this.GetPropertyValue(ConnectionKey.KEY_IMSDB);
			}
			set
			{
				this.SetPropertyValue(ConnectionKey.KEY_IMSDB, value);
			}
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x0013E6C0 File Offset: 0x0013C8C0
		private static bool ConvertToBoolean(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
				}
				catch (InvalidCastException ex)
				{
					throw DrdaException.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes") || StringComparer.OrdinalIgnoreCase.Equals(text, "1"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no") || StringComparer.OrdinalIgnoreCase.Equals(text, "0"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || StringComparer.OrdinalIgnoreCase.Equals(text, "1") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && !StringComparer.OrdinalIgnoreCase.Equals(text, "0") && bool.Parse(text));
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x0013E808 File Offset: 0x0013CA08
		private static string ConvertToIntegratedSecurity(object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				if (text.Equals("SSPI", StringComparison.OrdinalIgnoreCase))
				{
					return "SSPI";
				}
			}
			return string.Empty;
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x0013E840 File Offset: 0x0013CA40
		private static int ConvertToInt32(object value)
		{
			int num;
			try
			{
				num = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException ex)
			{
				throw DrdaException.ConvertFailed(value.GetType(), typeof(int), ex);
			}
			return num;
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0013E88C File Offset: 0x0013CA8C
		private static string ConvertToString(object value)
		{
			string text;
			try
			{
				text = ((IConvertible)value).ToString(CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException ex)
			{
				throw DrdaException.ConvertFailed(value.GetType(), typeof(string), ex);
			}
			return text;
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x0013E8D8 File Offset: 0x0013CAD8
		private static AppcSecurity ConvertToAppcSecurityType(object value)
		{
			string text = value.ToString();
			if (text == null)
			{
				throw DrdaException.ConvertFailed(value.GetType(), typeof(AppcSecurity), null);
			}
			text = text.Trim();
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "SAME") || StringComparer.OrdinalIgnoreCase.Equals(text, AppcSecurity.Same.ToString()))
			{
				return AppcSecurity.Same;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "PROGRAM") || StringComparer.OrdinalIgnoreCase.Equals(text, AppcSecurity.Program.ToString()))
			{
				return AppcSecurity.Program;
			}
			throw DrdaException.ConvertFailed(value.GetType(), typeof(AppcSecurity), null);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x0013E984 File Offset: 0x0013CB84
		private static UnitsOfWorkType ConvertToUnitsOfWorkType(object value)
		{
			string text = value.ToString();
			if (text == null)
			{
				throw DrdaException.ConvertFailed(value.GetType(), typeof(UnitsOfWorkType), null);
			}
			text = text.Trim();
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "RUW") || StringComparer.OrdinalIgnoreCase.Equals(text, UnitsOfWorkType.RUW.ToString()) || StringComparer.OrdinalIgnoreCase.Equals(text, "RemoteUnitOfWork"))
			{
				return UnitsOfWorkType.RUW;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "DUW") || StringComparer.OrdinalIgnoreCase.Equals(text, UnitsOfWorkType.DUW.ToString()) || StringComparer.OrdinalIgnoreCase.Equals(text, "DistributedUnitOfWork"))
			{
				return UnitsOfWorkType.DUW;
			}
			throw DrdaException.ConvertFailed(value.GetType(), typeof(UnitsOfWorkType), null);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0013EA58 File Offset: 0x0013CC58
		private static DBMSPlatform ConvertToDBMSPlatform(object value)
		{
			string text = value.ToString();
			if (text == null)
			{
				throw DrdaException.ConvertFailed(null, typeof(DBMSPlatform), null);
			}
			text = text.Trim();
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "DB2/AS400") || StringComparer.OrdinalIgnoreCase.Equals(text, DBMSPlatform.DB2AS400.ToString()))
			{
				return DBMSPlatform.DB2AS400;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "DB2/MVS") || StringComparer.OrdinalIgnoreCase.Equals(text, DBMSPlatform.DB2MVS.ToString()))
			{
				return DBMSPlatform.DB2MVS;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "DB2/NT") || StringComparer.OrdinalIgnoreCase.Equals(text, DBMSPlatform.DB2NT.ToString()))
			{
				return DBMSPlatform.DB2NT;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "DB2/6000") || StringComparer.OrdinalIgnoreCase.Equals(text, DBMSPlatform.DB26000.ToString()))
			{
				return DBMSPlatform.DB26000;
			}
			throw DrdaException.ConvertFailed(value.GetType(), typeof(UnitsOfWorkType), null);
		}

		// Token: 0x04003EFB RID: 16123
		private static DrdaConnectionProperty[] _properties;

		// Token: 0x04003EFC RID: 16124
		private static DrdaConnectionProperty[] _propertyIndex;

		// Token: 0x04003EFD RID: 16125
		private static readonly Dictionary<string, DrdaConnectionProperty> _keywords;

		// Token: 0x04003EFE RID: 16126
		private static string[] _validKeywords;

		// Token: 0x04003EFF RID: 16127
		private string[] _connInfo;
	}
}
