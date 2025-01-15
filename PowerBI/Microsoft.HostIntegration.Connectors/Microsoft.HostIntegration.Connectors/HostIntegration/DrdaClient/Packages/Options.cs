using System;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A4E RID: 2638
	public class Options
	{
		// Token: 0x06005243 RID: 21059 RVA: 0x0014EC87 File Offset: 0x0014CE87
		internal Options(Options options)
		{
			this._options = options;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0014EC96 File Offset: 0x0014CE96
		public Options()
		{
			this._options = new Options();
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x0014ECA9 File Offset: 0x0014CEA9
		// (set) Token: 0x06005246 RID: 21062 RVA: 0x0014ECB6 File Offset: 0x0014CEB6
		public bool BindCheck
		{
			get
			{
				return this._options.BindCheck;
			}
			set
			{
				this._options.BindCheck = value;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06005247 RID: 21063 RVA: 0x0014ECC4 File Offset: 0x0014CEC4
		// (set) Token: 0x06005248 RID: 21064 RVA: 0x0014ECD1 File Offset: 0x0014CED1
		public OptionsBindAllowErrors BindAllowErrors
		{
			get
			{
				return (OptionsBindAllowErrors)this._options.BindAllowErrors;
			}
			set
			{
				this._options.BindAllowErrors = (OptionsBindAllowErrors)value;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06005249 RID: 21065 RVA: 0x0014ECDF File Offset: 0x0014CEDF
		// (set) Token: 0x0600524A RID: 21066 RVA: 0x0014ECEC File Offset: 0x0014CEEC
		public int PackageDecimalPrecision
		{
			get
			{
				return this._options.PackageDecimalPrecision;
			}
			set
			{
				this._options.PackageDecimalPrecision = value;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x0600524B RID: 21067 RVA: 0x0014ECFA File Offset: 0x0014CEFA
		// (set) Token: 0x0600524C RID: 21068 RVA: 0x0014ED07 File Offset: 0x0014CF07
		public int ParallelProcessDegree
		{
			get
			{
				return this._options.ParallelProcessDegree;
			}
			set
			{
				this._options.ParallelProcessDegree = value;
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x0600524D RID: 21069 RVA: 0x0014ED15 File Offset: 0x0014CF15
		// (set) Token: 0x0600524E RID: 21070 RVA: 0x0014ED22 File Offset: 0x0014CF22
		public bool BindAuthorizationKeep
		{
			get
			{
				return this._options.BindAuthorizationKeep;
			}
			set
			{
				this._options.BindAuthorizationKeep = value;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x0014ED30 File Offset: 0x0014CF30
		// (set) Token: 0x06005250 RID: 21072 RVA: 0x0014ED3D File Offset: 0x0014CF3D
		public OptionsPackageExecuteAuthorization PackageExecuteAuthorization
		{
			get
			{
				return (OptionsPackageExecuteAuthorization)this._options.PackageExecuteAuthorization;
			}
			set
			{
				this._options.PackageExecuteAuthorization = (OptionsPackageExecuteAuthorization)value;
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06005251 RID: 21073 RVA: 0x0014ED4B File Offset: 0x0014CF4B
		// (set) Token: 0x06005252 RID: 21074 RVA: 0x0014ED58 File Offset: 0x0014CF58
		public int PackageCcsidSbc
		{
			get
			{
				return this._options.PackageCcsidSbc;
			}
			set
			{
				this._options.PackageCcsidSbc = value;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06005253 RID: 21075 RVA: 0x0014ED66 File Offset: 0x0014CF66
		// (set) Token: 0x06005254 RID: 21076 RVA: 0x0014ED73 File Offset: 0x0014CF73
		public int PackageCcsidMbc
		{
			get
			{
				return this._options.PackageCcsidMbc;
			}
			set
			{
				this._options.PackageCcsidMbc = value;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06005255 RID: 21077 RVA: 0x0014ED81 File Offset: 0x0014CF81
		// (set) Token: 0x06005256 RID: 21078 RVA: 0x0014ED8E File Offset: 0x0014CF8E
		public int PackageCcsidDbc
		{
			get
			{
				return this._options.PackageCcsidDbc;
			}
			set
			{
				this._options.PackageCcsidDbc = value;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06005257 RID: 21079 RVA: 0x0014ED9C File Offset: 0x0014CF9C
		// (set) Token: 0x06005258 RID: 21080 RVA: 0x0014EDA9 File Offset: 0x0014CFA9
		public OptionsPackageCharacterSubtype PackageCharacterSubtype
		{
			get
			{
				return (OptionsPackageCharacterSubtype)this._options.PackageCharacterSubtype;
			}
			set
			{
				this._options.PackageCharacterSubtype = (OptionsPackageCharacterSubtype)value;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06005259 RID: 21081 RVA: 0x0014EDB7 File Offset: 0x0014CFB7
		// (set) Token: 0x0600525A RID: 21082 RVA: 0x0014EDC4 File Offset: 0x0014CFC4
		public OptionsPackageIsolationLevel PackageIsolationLevel
		{
			get
			{
				return (OptionsPackageIsolationLevel)this._options.PackageIsolationLevel;
			}
			set
			{
				this._options.PackageIsolationLevel = (OptionsPackageIsolationLevel)value;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x0600525B RID: 21083 RVA: 0x0014EDD2 File Offset: 0x0014CFD2
		// (set) Token: 0x0600525C RID: 21084 RVA: 0x0014EDDF File Offset: 0x0014CFDF
		public string PackageOwnerIdentifier
		{
			get
			{
				return this._options.PackageOwnerIdentifier;
			}
			set
			{
				this._options.PackageOwnerIdentifier = value;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x0600525D RID: 21085 RVA: 0x0014EDED File Offset: 0x0014CFED
		// (set) Token: 0x0600525E RID: 21086 RVA: 0x0014EDFA File Offset: 0x0014CFFA
		public bool BindReplace
		{
			get
			{
				return this._options.BindReplace;
			}
			set
			{
				this._options.BindReplace = value;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x0600525F RID: 21087 RVA: 0x0014EE08 File Offset: 0x0014D008
		// (set) Token: 0x06005260 RID: 21088 RVA: 0x0014EE15 File Offset: 0x0014D015
		public OptionsBindExplain BindExplain
		{
			get
			{
				return (OptionsBindExplain)this._options.BindExplain;
			}
			set
			{
				this._options.BindExplain = (OptionsBindExplain)value;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06005261 RID: 21089 RVA: 0x0014EE23 File Offset: 0x0014D023
		// (set) Token: 0x06005262 RID: 21090 RVA: 0x0014EE30 File Offset: 0x0014D030
		public string BindReplaceVersion
		{
			get
			{
				return this._options.BindReplaceVersion;
			}
			set
			{
				this._options.BindReplaceVersion = value;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06005263 RID: 21091 RVA: 0x0014EE3E File Offset: 0x0014D03E
		// (set) Token: 0x06005264 RID: 21092 RVA: 0x0014EE4B File Offset: 0x0014D04B
		public OptionsKeepPreparedStatement KeepPreparedStatement
		{
			get
			{
				return (OptionsKeepPreparedStatement)this._options.KeepPreparedStatement;
			}
			set
			{
				this._options.KeepPreparedStatement = (OptionsKeepPreparedStatement)value;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06005265 RID: 21093 RVA: 0x0014EE59 File Offset: 0x0014D059
		// (set) Token: 0x06005266 RID: 21094 RVA: 0x0014EE66 File Offset: 0x0014D066
		public OptionsStatementQueryProtocol StatementQueryProtocol
		{
			get
			{
				return (OptionsStatementQueryProtocol)this._options.StatementQueryProtocol;
			}
			set
			{
				this._options.StatementQueryProtocol = (OptionsStatementQueryProtocol)value;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06005267 RID: 21095 RVA: 0x0014EE74 File Offset: 0x0014D074
		// (set) Token: 0x06005268 RID: 21096 RVA: 0x0014EE81 File Offset: 0x0014D081
		public OptionsReleaseDatabaseResources ReleaseDatabaseResources
		{
			get
			{
				return (OptionsReleaseDatabaseResources)this._options.ReleaseDatabaseResources;
			}
			set
			{
				this._options.ReleaseDatabaseResources = (OptionsReleaseDatabaseResources)value;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06005269 RID: 21097 RVA: 0x0014EE8F File Offset: 0x0014D08F
		// (set) Token: 0x0600526A RID: 21098 RVA: 0x0014EE9C File Offset: 0x0014D09C
		public OptionsStatementDateFormat StatementDateFormat
		{
			get
			{
				return (OptionsStatementDateFormat)this._options.StatementDateFormat;
			}
			set
			{
				this._options.StatementDateFormat = (OptionsStatementDateFormat)value;
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x0014EEAA File Offset: 0x0014D0AA
		// (set) Token: 0x0600526C RID: 21100 RVA: 0x0014EEB7 File Offset: 0x0014D0B7
		public OptionsStatementDecimalDelimiter StatementDecimalDelimiter
		{
			get
			{
				return (OptionsStatementDecimalDelimiter)this._options.StatementDecimalDelimiter;
			}
			set
			{
				this._options.StatementDecimalDelimiter = (OptionsStatementDecimalDelimiter)value;
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x0014EEC5 File Offset: 0x0014D0C5
		// (set) Token: 0x0600526E RID: 21102 RVA: 0x0014EED2 File Offset: 0x0014D0D2
		public OptionsStatementStringDelimiter StatementStringDelimiter
		{
			get
			{
				return (OptionsStatementStringDelimiter)this._options.StatementStringDelimiter;
			}
			set
			{
				this._options.StatementStringDelimiter = (OptionsStatementStringDelimiter)value;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x0014EEE0 File Offset: 0x0014D0E0
		// (set) Token: 0x06005270 RID: 21104 RVA: 0x0014EEED File Offset: 0x0014D0ED
		public OptionsStatementTimeFormat StatementTimeFormat
		{
			get
			{
				return (OptionsStatementTimeFormat)this._options.StatementTimeFormat;
			}
			set
			{
				this._options.StatementTimeFormat = (OptionsStatementTimeFormat)value;
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06005271 RID: 21105 RVA: 0x0014EEFB File Offset: 0x0014D0FB
		// (set) Token: 0x06005272 RID: 21106 RVA: 0x0014EF08 File Offset: 0x0014D108
		public string DefaultRdbCollection
		{
			get
			{
				return this._options.DefaultRdbCollection;
			}
			set
			{
				this._options.DefaultRdbCollection = value;
			}
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0014EF16 File Offset: 0x0014D116
		internal Options ToOptions()
		{
			return this._options;
		}

		// Token: 0x040040E4 RID: 16612
		private Options _options;
	}
}
