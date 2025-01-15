using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000FC RID: 252
	internal class EagerInternalContext : InternalContext
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x00030423 File Offset: 0x0002E623
		public EagerInternalContext(DbContext owner)
			: base(owner, null)
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00030430 File Offset: 0x0002E630
		public EagerInternalContext(DbContext owner, ObjectContext objectContext, bool objectContextOwned)
			: base(owner, null)
		{
			this._objectContext = objectContext;
			this._objectContextOwned = objectContextOwned;
			this._originalConnectionString = InternalConnection.GetStoreConnectionString(this._objectContext.Connection);
			this._objectContext.InterceptionContext = this._objectContext.InterceptionContext.WithDbContext(owner);
			base.LoadContextConfigs();
			base.ResetDbSets();
			this._objectContext.InitializeMappingViewCacheFactory(base.Owner);
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000304A2 File Offset: 0x0002E6A2
		public override ObjectContext ObjectContext
		{
			get
			{
				base.Initialize();
				return this.ObjectContextInUse;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000304B0 File Offset: 0x0002E6B0
		public override ObjectContext GetObjectContextWithoutDatabaseInitialization()
		{
			this.InitializeContext();
			return this.ObjectContextInUse;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000304BE File Offset: 0x0002E6BE
		private ObjectContext ObjectContextInUse
		{
			get
			{
				return base.TempObjectContext ?? this._objectContext;
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000304D0 File Offset: 0x0002E6D0
		protected override void InitializeContext()
		{
			base.CheckContextNotDisposed();
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000304D8 File Offset: 0x0002E6D8
		public override void MarkDatabaseNotInitialized()
		{
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000304DA File Offset: 0x0002E6DA
		public override void MarkDatabaseInitialized()
		{
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000304DC File Offset: 0x0002E6DC
		protected override void InitializeDatabase()
		{
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x000304DE File Offset: 0x0002E6DE
		public override IDatabaseInitializer<DbContext> DefaultInitializer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000304E1 File Offset: 0x0002E6E1
		public override void DisposeContext(bool disposing)
		{
			if (!base.IsDisposed)
			{
				base.DisposeContext(disposing);
				if (disposing && this._objectContextOwned)
				{
					this._objectContext.Dispose();
				}
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00030508 File Offset: 0x0002E708
		public override DbConnection Connection
		{
			get
			{
				base.CheckContextNotDisposed();
				return ((EntityConnection)this._objectContext.Connection).StoreConnection;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00030525 File Offset: 0x0002E725
		public override string OriginalConnectionString
		{
			get
			{
				return this._originalConnectionString;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0003052D File Offset: 0x0002E72D
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				return DbConnectionStringOrigin.UserCode;
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00030530 File Offset: 0x0002E730
		public override void OverrideConnection(IInternalConnection connection)
		{
			throw Error.EagerInternalContext_CannotSetConnectionInfo();
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00030537 File Offset: 0x0002E737
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00030549 File Offset: 0x0002E749
		public override bool EnsureTransactionsForFunctionsAndCommands
		{
			get
			{
				return this.ObjectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0003055C File Offset: 0x0002E75C
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x0003056E File Offset: 0x0002E76E
		public override bool LazyLoadingEnabled
		{
			get
			{
				return this.ObjectContextInUse.ContextOptions.LazyLoadingEnabled;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.LazyLoadingEnabled = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00030581 File Offset: 0x0002E781
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00030593 File Offset: 0x0002E793
		public override bool ProxyCreationEnabled
		{
			get
			{
				return this.ObjectContextInUse.ContextOptions.ProxyCreationEnabled;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.ProxyCreationEnabled = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x000305A6 File Offset: 0x0002E7A6
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x000305BB File Offset: 0x0002E7BB
		public override bool UseDatabaseNullSemantics
		{
			get
			{
				return !this.ObjectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior = !value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000305D1 File Offset: 0x0002E7D1
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x000305E6 File Offset: 0x0002E7E6
		public override bool DisableFilterOverProjectionSimplificationForCustomFunctions
		{
			get
			{
				return !this.ObjectContextInUse.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions = !value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000305FC File Offset: 0x0002E7FC
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00030609 File Offset: 0x0002E809
		public override int? CommandTimeout
		{
			get
			{
				return this.ObjectContextInUse.CommandTimeout;
			}
			set
			{
				this.ObjectContextInUse.CommandTimeout = value;
			}
		}

		// Token: 0x0400091A RID: 2330
		private readonly ObjectContext _objectContext;

		// Token: 0x0400091B RID: 2331
		private readonly bool _objectContextOwned;

		// Token: 0x0400091C RID: 2332
		private readonly string _originalConnectionString;
	}
}
