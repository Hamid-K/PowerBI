using System;
using System.Data.Common;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x020005E4 RID: 1508
	internal class EntityAdapter : IEntityAdapter
	{
		// Token: 0x060049A3 RID: 18851 RVA: 0x00105472 File Offset: 0x00103672
		public EntityAdapter(ObjectContext context)
			: this(context, (EntityAdapter a) => new UpdateTranslator(a))
		{
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x0010549A File Offset: 0x0010369A
		protected EntityAdapter(ObjectContext context, Func<EntityAdapter, UpdateTranslator> updateTranslatorFactory)
		{
			this._context = context;
			this._updateTranslatorFactory = updateTranslatorFactory;
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x001054B7 File Offset: 0x001036B7
		public ObjectContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x001054BF File Offset: 0x001036BF
		// (set) Token: 0x060049A7 RID: 18855 RVA: 0x001054C7 File Offset: 0x001036C7
		DbConnection IEntityAdapter.Connection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (EntityConnection)value;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x001054D5 File Offset: 0x001036D5
		// (set) Token: 0x060049A9 RID: 18857 RVA: 0x001054DD File Offset: 0x001036DD
		public EntityConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				this._connection = value;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x001054E6 File Offset: 0x001036E6
		// (set) Token: 0x060049AB RID: 18859 RVA: 0x001054EE File Offset: 0x001036EE
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this._acceptChangesDuringUpdate;
			}
			set
			{
				this._acceptChangesDuringUpdate = value;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x001054F7 File Offset: 0x001036F7
		// (set) Token: 0x060049AD RID: 18861 RVA: 0x001054FF File Offset: 0x001036FF
		public int? CommandTimeout { get; set; }

		// Token: 0x060049AE RID: 18862 RVA: 0x00105508 File Offset: 0x00103708
		public int Update()
		{
			return this.Update<int>(0, (UpdateTranslator ut) => ut.Update());
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x00105530 File Offset: 0x00103730
		public Task<int> UpdateAsync(CancellationToken cancellationToken)
		{
			return this.Update<Task<int>>(Task.FromResult<int>(0), (UpdateTranslator ut) => ut.UpdateAsync(cancellationToken));
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x00105564 File Offset: 0x00103764
		private T Update<T>(T noChangesResult, Func<UpdateTranslator, T> updateFunction)
		{
			if (!EntityAdapter.IsStateManagerDirty(this._context.ObjectStateManager))
			{
				return noChangesResult;
			}
			if (this._connection == null)
			{
				throw Error.EntityClient_NoConnectionForAdapter();
			}
			if (this._connection.StoreProviderFactory == null || this._connection.StoreConnection == null)
			{
				throw Error.EntityClient_NoStoreConnectionForUpdate();
			}
			if (ConnectionState.Open != this._connection.State)
			{
				throw Error.EntityClient_ClosedConnectionForUpdate();
			}
			UpdateTranslator updateTranslator = this._updateTranslatorFactory(this);
			return updateFunction(updateTranslator);
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x001055DB File Offset: 0x001037DB
		private static bool IsStateManagerDirty(ObjectStateManager entityCache)
		{
			return entityCache.HasChanges();
		}

		// Token: 0x040019FF RID: 6655
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x04001A00 RID: 6656
		private EntityConnection _connection;

		// Token: 0x04001A01 RID: 6657
		private readonly ObjectContext _context;

		// Token: 0x04001A02 RID: 6658
		private readonly Func<EntityAdapter, UpdateTranslator> _updateTranslatorFactory;
	}
}
