using System;
using System.Collections;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A77 RID: 2679
	public class Options
	{
		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06005300 RID: 21248 RVA: 0x00151EE5 File Offset: 0x001500E5
		// (set) Token: 0x06005301 RID: 21249 RVA: 0x00151EF0 File Offset: 0x001500F0
		public bool BindCheck
		{
			get
			{
				return this.bindCheck;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "bindCheck";
				this.bindCheck = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x00151F1C File Offset: 0x0015011C
		// (set) Token: 0x06005303 RID: 21251 RVA: 0x00151F24 File Offset: 0x00150124
		public OptionsBindAllowErrors BindAllowErrors
		{
			get
			{
				return this.bindAllowErrors;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "bindAllowErrors";
				this.bindAllowErrors = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x00151F50 File Offset: 0x00150150
		// (set) Token: 0x06005305 RID: 21253 RVA: 0x00151F58 File Offset: 0x00150158
		public int PackageDecimalPrecision
		{
			get
			{
				return this.packageDecimalPrecision;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageDecimalPrecision";
				this.packageDecimalPrecision = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06005306 RID: 21254 RVA: 0x00151F84 File Offset: 0x00150184
		// (set) Token: 0x06005307 RID: 21255 RVA: 0x00151F8C File Offset: 0x0015018C
		public int ParallelProcessDegree
		{
			get
			{
				return this.parallelProcessDegree;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "parallelProcessDegree";
				this.parallelProcessDegree = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06005308 RID: 21256 RVA: 0x00151FB8 File Offset: 0x001501B8
		// (set) Token: 0x06005309 RID: 21257 RVA: 0x00151FC0 File Offset: 0x001501C0
		public bool BindAuthorizationKeep
		{
			get
			{
				return this.bindAuthorizationKeep;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "bindAuthorizationKeep";
				this.bindAuthorizationKeep = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x0600530A RID: 21258 RVA: 0x00151FEC File Offset: 0x001501EC
		// (set) Token: 0x0600530B RID: 21259 RVA: 0x00151FF4 File Offset: 0x001501F4
		public OptionsPackageExecuteAuthorization PackageExecuteAuthorization
		{
			get
			{
				return this.packageExecuteAuthorization;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageExecuteAuthorization";
				this.packageExecuteAuthorization = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x0600530C RID: 21260 RVA: 0x00152020 File Offset: 0x00150220
		// (set) Token: 0x0600530D RID: 21261 RVA: 0x00152028 File Offset: 0x00150228
		public int PackageCcsidSbc
		{
			get
			{
				return this.packageCcsidSbc;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageCcsidSbc";
				this.packageCcsidSbc = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x00152054 File Offset: 0x00150254
		// (set) Token: 0x0600530F RID: 21263 RVA: 0x0015205C File Offset: 0x0015025C
		public int PackageCcsidMbc
		{
			get
			{
				return this.packageCcsidMbc;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageCcsidMbc";
				this.packageCcsidMbc = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06005310 RID: 21264 RVA: 0x00152088 File Offset: 0x00150288
		// (set) Token: 0x06005311 RID: 21265 RVA: 0x00152090 File Offset: 0x00150290
		public int PackageCcsidDbc
		{
			get
			{
				return this.packageCcsidDbc;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageCcsidDbc";
				this.packageCcsidDbc = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06005312 RID: 21266 RVA: 0x001520BC File Offset: 0x001502BC
		// (set) Token: 0x06005313 RID: 21267 RVA: 0x001520C4 File Offset: 0x001502C4
		public OptionsPackageCharacterSubtype PackageCharacterSubtype
		{
			get
			{
				return this.packageCharacterSubtype;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageCharacterSubtype";
				this.packageCharacterSubtype = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x001520F0 File Offset: 0x001502F0
		// (set) Token: 0x06005315 RID: 21269 RVA: 0x001520F8 File Offset: 0x001502F8
		public OptionsPackageIsolationLevel PackageIsolationLevel
		{
			get
			{
				return this.packageIsolationLevel;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageIsolationLevel";
				this.packageIsolationLevel = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x00152124 File Offset: 0x00150324
		// (set) Token: 0x06005317 RID: 21271 RVA: 0x0015212C File Offset: 0x0015032C
		public string PackageOwnerIdentifier
		{
			get
			{
				return this.packageOwnerIdentifier;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "packageOwnerIdentifier";
				this.packageOwnerIdentifier = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06005318 RID: 21272 RVA: 0x00152153 File Offset: 0x00150353
		// (set) Token: 0x06005319 RID: 21273 RVA: 0x0015215C File Offset: 0x0015035C
		public bool BindReplace
		{
			get
			{
				return this.bindReplace;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "bindReplace";
				this.bindReplace = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x00152188 File Offset: 0x00150388
		// (set) Token: 0x0600531B RID: 21275 RVA: 0x00152190 File Offset: 0x00150390
		public OptionsBindExplain BindExplain
		{
			get
			{
				return this.bindExplain;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "bindExplain";
				this.bindExplain = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x001521BC File Offset: 0x001503BC
		// (set) Token: 0x0600531D RID: 21277 RVA: 0x001521C4 File Offset: 0x001503C4
		public string BindReplaceVersion
		{
			get
			{
				return this.bindReplaceVersion;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "bindReplaceVersion";
				this.bindReplaceVersion = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x001521EB File Offset: 0x001503EB
		// (set) Token: 0x0600531F RID: 21279 RVA: 0x001521F4 File Offset: 0x001503F4
		public OptionsKeepPreparedStatement KeepPreparedStatement
		{
			get
			{
				return this.keepPreparedStatement;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "keepPreparedStatement";
				this.keepPreparedStatement = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x00152220 File Offset: 0x00150420
		// (set) Token: 0x06005321 RID: 21281 RVA: 0x00152228 File Offset: 0x00150428
		public OptionsStatementQueryProtocol StatementQueryProtocol
		{
			get
			{
				return this.statementQueryProtocol;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "statementQueryProtocol";
				this.statementQueryProtocol = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x00152254 File Offset: 0x00150454
		// (set) Token: 0x06005323 RID: 21283 RVA: 0x0015225C File Offset: 0x0015045C
		public OptionsReleaseDatabaseResources ReleaseDatabaseResources
		{
			get
			{
				return this.releaseDatabaseResources;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "releaseDatabaseResources";
				this.releaseDatabaseResources = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x00152288 File Offset: 0x00150488
		// (set) Token: 0x06005325 RID: 21285 RVA: 0x00152290 File Offset: 0x00150490
		public OptionsStatementDateFormat StatementDateFormat
		{
			get
			{
				return this.statementDateFormat;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "statementDateFormat";
				this.statementDateFormat = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x001522BC File Offset: 0x001504BC
		// (set) Token: 0x06005327 RID: 21287 RVA: 0x001522C4 File Offset: 0x001504C4
		public OptionsStatementDecimalDelimiter StatementDecimalDelimiter
		{
			get
			{
				return this.statementDecimalDelimiter;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "statementDecimalDelimiter";
				this.statementDecimalDelimiter = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x001522F0 File Offset: 0x001504F0
		// (set) Token: 0x06005329 RID: 21289 RVA: 0x001522F8 File Offset: 0x001504F8
		public OptionsStatementStringDelimiter StatementStringDelimiter
		{
			get
			{
				return this.statementStringDelimiter;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "statementStringDelimiter";
				this.statementStringDelimiter = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x00152324 File Offset: 0x00150524
		// (set) Token: 0x0600532B RID: 21291 RVA: 0x0015232C File Offset: 0x0015052C
		public OptionsStatementTimeFormat StatementTimeFormat
		{
			get
			{
				return this.statementTimeFormat;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "statementTimeFormat";
				this.statementTimeFormat = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00152358 File Offset: 0x00150558
		// (set) Token: 0x0600532D RID: 21293 RVA: 0x00152360 File Offset: 0x00150560
		public string DefaultRdbCollection
		{
			get
			{
				return this.defaultRdbCollection;
			}
			set
			{
				Hashtable optionsTable = this._optionsTable;
				object obj = "defaultRdbCollection";
				this.defaultRdbCollection = value;
				optionsTable[obj] = value;
			}
		}

		// Token: 0x04004236 RID: 16950
		private bool bindCheck;

		// Token: 0x04004237 RID: 16951
		private OptionsBindAllowErrors bindAllowErrors = OptionsBindAllowErrors.No;

		// Token: 0x04004238 RID: 16952
		private int packageDecimalPrecision = 15;

		// Token: 0x04004239 RID: 16953
		private int parallelProcessDegree = 1;

		// Token: 0x0400423A RID: 16954
		private bool bindAuthorizationKeep;

		// Token: 0x0400423B RID: 16955
		private OptionsPackageExecuteAuthorization packageExecuteAuthorization = OptionsPackageExecuteAuthorization.Owner;

		// Token: 0x0400423C RID: 16956
		private int packageCcsidSbc = 1208;

		// Token: 0x0400423D RID: 16957
		private int packageCcsidMbc = 1208;

		// Token: 0x0400423E RID: 16958
		private int packageCcsidDbc = 1200;

		// Token: 0x0400423F RID: 16959
		private OptionsPackageCharacterSubtype packageCharacterSubtype = OptionsPackageCharacterSubtype.Default;

		// Token: 0x04004240 RID: 16960
		private OptionsPackageIsolationLevel packageIsolationLevel = OptionsPackageIsolationLevel.ReadCommitted;

		// Token: 0x04004241 RID: 16961
		private string packageOwnerIdentifier = string.Empty;

		// Token: 0x04004242 RID: 16962
		private bool bindReplace;

		// Token: 0x04004243 RID: 16963
		private OptionsBindExplain bindExplain = OptionsBindExplain.ExplainNone;

		// Token: 0x04004244 RID: 16964
		private string bindReplaceVersion = string.Empty;

		// Token: 0x04004245 RID: 16965
		private OptionsKeepPreparedStatement keepPreparedStatement = OptionsKeepPreparedStatement.None;

		// Token: 0x04004246 RID: 16966
		private OptionsStatementQueryProtocol statementQueryProtocol = OptionsStatementQueryProtocol.FixedRow;

		// Token: 0x04004247 RID: 16967
		private OptionsReleaseDatabaseResources releaseDatabaseResources = OptionsReleaseDatabaseResources.Commit;

		// Token: 0x04004248 RID: 16968
		private OptionsStatementDateFormat statementDateFormat = OptionsStatementDateFormat.Default;

		// Token: 0x04004249 RID: 16969
		private OptionsStatementDecimalDelimiter statementDecimalDelimiter;

		// Token: 0x0400424A RID: 16970
		private OptionsStatementStringDelimiter statementStringDelimiter;

		// Token: 0x0400424B RID: 16971
		private OptionsStatementTimeFormat statementTimeFormat = OptionsStatementTimeFormat.Iso;

		// Token: 0x0400424C RID: 16972
		private string defaultRdbCollection = string.Empty;

		// Token: 0x0400424D RID: 16973
		internal Hashtable _optionsTable = new Hashtable();
	}
}
