using System;
using System.Runtime.Serialization;
using Microsoft.AnalysisServices.Azure.Common.Utils;
using Microsoft.Cloud.ModelCommon;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000051 RID: 81
	[DataContract]
	public sealed class DatabaseMoniker : IEquatable<DatabaseMoniker>, IContainsPrivateInformation
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public DatabaseMoniker(string virtualServerName, string databaseName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(virtualServerName, "virtualServerName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(databaseName, "databaseName");
			if (!NamingRules.IsValidVirtualServerName(virtualServerName))
			{
				throw new InvalidVirtualServerNameException(virtualServerName, "The provided virtual server name {0} is invalid".FormatWithInvariantCulture(new object[] { virtualServerName }));
			}
			if (!NamingRules.IsValidDatabaseName(databaseName))
			{
				throw new InvalidDatabaseNameException(databaseName, "The provided database name {0} is invalid".FormatWithInvariantCulture(new object[] { databaseName }));
			}
			this.VirtualServerName = CommonUtils.NormalizeCase(virtualServerName);
			this.DatabaseName = CommonUtils.NormalizeCase(databaseName);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		public bool IsPowerBiOnPremiseVirtualServer
		{
			get
			{
				return Utils.IsPowerBiOnPremiseVirtualServer(this.VirtualServerName);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		[DataMember]
		public string VirtualServerName { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000FCD1 File Offset: 0x0000DED1
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000FCD9 File Offset: 0x0000DED9
		[DataMember]
		public string DatabaseName { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000FCE2 File Offset: 0x0000DEE2
		public string FullName
		{
			get
			{
				return Utils.GetDatabaseFullName(this.VirtualServerName, this.DatabaseName);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000FCF5 File Offset: 0x0000DEF5
		public string EngineFriendlyFullName
		{
			get
			{
				return "{0}-{1}".FormatWithInvariantCulture(new object[] { this.VirtualServerName, this.DatabaseName });
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000FD19 File Offset: 0x0000DF19
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000FD21 File Offset: 0x0000DF21
		public bool Equals(DatabaseMoniker other)
		{
			return other != null && this.VirtualServerName.Equals(other.VirtualServerName, StringComparison.OrdinalIgnoreCase) && this.DatabaseName.Equals(other.DatabaseName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000FD4E File Offset: 0x0000DF4E
		public override bool Equals(object other)
		{
			return this.Equals(other as DatabaseMoniker);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000FD5C File Offset: 0x0000DF5C
		public override int GetHashCode()
		{
			return this.VirtualServerName.GetHashCode() ^ this.DatabaseName.GetHashCode();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000FD78 File Offset: 0x0000DF78
		public static DatabaseMoniker Parse(string databaseFullName)
		{
			DatabaseMoniker databaseMoniker;
			DatabaseMoniker.InnerTryParse(databaseFullName, '|', out databaseMoniker);
			ExtendedDiagnostics.EnsureNotNull<DatabaseMoniker>(databaseMoniker, "Delimiter '{0}' is missing in database full name '{1}'.".FormatWithInvariantCulture(new object[] { '|', databaseFullName }));
			return databaseMoniker;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000FDB5 File Offset: 0x0000DFB5
		public static bool TryParseEngineFriendlyFullName(string databaseFullName, out DatabaseMoniker databaseMoniker)
		{
			return DatabaseMoniker.InnerTryParse(databaseFullName, '-', out databaseMoniker);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		public string ToPrivateString()
		{
			return this.ToString().MarkAsPrivate();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000FCE2 File Offset: 0x0000DEE2
		public string ToInternalString()
		{
			return Utils.GetDatabaseFullName(this.VirtualServerName, this.DatabaseName);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000FDCD File Offset: 0x0000DFCD
		public string ToOriginalString()
		{
			return this.ToString();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		private static bool InnerTryParse(string databaseFullName, char delimiter, out DatabaseMoniker databaseMoniker)
		{
			int num = databaseFullName.IndexOf(delimiter);
			if (num <= 0)
			{
				databaseMoniker = null;
				return false;
			}
			string text = databaseFullName.Substring(0, num);
			string text2 = databaseFullName.Substring(num + 1);
			databaseMoniker = new DatabaseMoniker(text, text2);
			return true;
		}

		// Token: 0x04000136 RID: 310
		private const char VS_DATABASE_DELIMITER = '|';

		// Token: 0x04000137 RID: 311
		private const char VS_DATABASE_DELIMITER_ENGINE_FRIENDLY = '-';

		// Token: 0x04000138 RID: 312
		private const string ENGINE_FRIENDLY_DATABASE_NAME_FORMAT = "{0}-{1}";
	}
}
