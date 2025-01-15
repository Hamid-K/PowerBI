using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E88 RID: 7816
	public abstract class Command : IAccessor, ICommand, ICommandText, ICommandProperties, IConvertType, IColumnsInfo, ISupportErrorInfo
	{
		// Token: 0x0600C126 RID: 49446 RVA: 0x0026D827 File Offset: 0x0026BA27
		protected Command(IInteropServices interopServices, IManagedDataConvert convert, bool supportCancelAfterRowsetReturn = false)
		{
			this.interopServices = interopServices;
			this.supportCancelAfterRowsetReturn = supportCancelAfterRowsetReturn;
			this.convert = convert;
			this.dialect = DBGUID.Default;
			this.commandText = string.Empty;
		}

		// Token: 0x17002F32 RID: 12082
		// (get) Token: 0x0600C127 RID: 49447
		public abstract IDBProperties Properties { get; }

		// Token: 0x17002F33 RID: 12083
		// (get) Token: 0x0600C128 RID: 49448 RVA: 0x0026D865 File Offset: 0x0026BA65
		public IInteropServices InteropServices
		{
			get
			{
				return this.interopServices;
			}
		}

		// Token: 0x17002F34 RID: 12084
		// (get) Token: 0x0600C129 RID: 49449 RVA: 0x0026D86D File Offset: 0x0026BA6D
		// (set) Token: 0x0600C12A RID: 49450 RVA: 0x0026D875 File Offset: 0x0026BA75
		public Guid Dialect
		{
			get
			{
				return this.dialect;
			}
			set
			{
				this.dialect = value;
			}
		}

		// Token: 0x17002F35 RID: 12085
		// (get) Token: 0x0600C12B RID: 49451 RVA: 0x0026D87E File Offset: 0x0026BA7E
		// (set) Token: 0x0600C12C RID: 49452 RVA: 0x0026D886 File Offset: 0x0026BA86
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
			}
		}

		// Token: 0x17002F36 RID: 12086
		// (get) Token: 0x0600C12D RID: 49453 RVA: 0x0026D88F File Offset: 0x0026BA8F
		private IAccessor Accessor
		{
			get
			{
				if (this.accessor == null)
				{
					this.accessor = new Accessor();
				}
				return this.accessor;
			}
		}

		// Token: 0x0600C12E RID: 49454
		public abstract IRowset CreateRowset(bool forColumnInfo);

		// Token: 0x0600C12F RID: 49455 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual IMultipleResults CreateMultipleResults()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C130 RID: 49456 RVA: 0x0026D8AA File Offset: 0x0026BAAA
		protected virtual bool IsDialectSupported(Guid guid)
		{
			return guid == DBGUID.Default;
		}

		// Token: 0x0600C131 RID: 49457 RVA: 0x0026D8B8 File Offset: 0x0026BAB8
		public void Cancel()
		{
			object obj = this.executionLock;
			lock (obj)
			{
				if (this.currentExecution != null)
				{
					this.currentExecution.Abort(new HCHAPTER
					{
						value = 0UL
					}, DBASYNCHOP.OPEN);
					this.currentExecution = null;
				}
				else
				{
					this.pendingCancel = true;
				}
			}
		}

		// Token: 0x0600C132 RID: 49458 RVA: 0x0026D928 File Offset: 0x0026BB28
		unsafe void IAccessor.AddRefAccessor(HACCESSOR hAccessor, uint* pcRefCount)
		{
			this.Accessor.AddRefAccessor(hAccessor, pcRefCount);
		}

		// Token: 0x0600C133 RID: 49459 RVA: 0x0026D937 File Offset: 0x0026BB37
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS dwAccessorFlags, DBCOUNTITEM cBindings, DBBINDING* rgBindings, DBLENGTH cbRowSize, out HACCESSOR hAccessor, DBBINDSTATUS* rgStatus)
		{
			this.Accessor.CreateAccessor(dwAccessorFlags, cBindings, rgBindings, cbRowSize, out hAccessor, rgStatus);
		}

		// Token: 0x0600C134 RID: 49460 RVA: 0x0026D94D File Offset: 0x0026BB4D
		unsafe void IAccessor.GetBindings(HACCESSOR hAccessor, out DBACCESSORFLAGS dwAccessorFlags, out DBCOUNTITEM pcBindings, out DBBINDING* rgBindings)
		{
			this.Accessor.GetBindings(hAccessor, out dwAccessorFlags, out pcBindings, out rgBindings);
		}

		// Token: 0x0600C135 RID: 49461 RVA: 0x0026D95F File Offset: 0x0026BB5F
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR hAccessor, uint* pcRefCount)
		{
			this.Accessor.ReleaseAccessor(hAccessor, pcRefCount);
		}

		// Token: 0x0600C136 RID: 49462 RVA: 0x0026D96E File Offset: 0x0026BB6E
		void ICommand.Cancel()
		{
			((ICommandText)this).Cancel();
		}

		// Token: 0x0600C137 RID: 49463 RVA: 0x0026D976 File Offset: 0x0026BB76
		unsafe int ICommand.Execute(IntPtr pUnkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* pcRowsAffected, out IntPtr ppv)
		{
			return ((ICommandText)this).Execute(pUnkOuter, ref iid, pParams, pcRowsAffected, out ppv);
		}

		// Token: 0x0600C138 RID: 49464 RVA: 0x0026D985 File Offset: 0x0026BB85
		void ICommand.GetDBSession(ref Guid iid, out IntPtr session)
		{
			((ICommandText)this).GetDBSession(ref iid, out session);
		}

		// Token: 0x0600C139 RID: 49465 RVA: 0x0026D98F File Offset: 0x0026BB8F
		unsafe int ICommandProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.Properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x0600C13A RID: 49466 RVA: 0x0026D9A1 File Offset: 0x0026BBA1
		unsafe int ICommandProperties.SetProperties(uint cPropertySets, DBPROPSET* rgPropertySets)
		{
			return this.Properties.SetProperties(cPropertySets, rgPropertySets);
		}

		// Token: 0x0600C13B RID: 49467 RVA: 0x0026D9B0 File Offset: 0x0026BBB0
		unsafe int ICommandText.Execute(IntPtr punkOuter, ref Guid iid, DBPARAMS* pParams, DBROWCOUNT* pcRowsAffected, out IntPtr ppv)
		{
			if (pcRowsAffected != null)
			{
				*pcRowsAffected = new DBROWCOUNT
				{
					value = -1L
				};
			}
			if (iid == IID.IMultipleResults)
			{
				return this.Execute<IMultipleResults>(punkOuter, this.CreateMultipleResults(), ref iid, new OleDbAggregator(this.InteropServices.AggregateMultipleResults), out ppv);
			}
			return this.Execute<IRowset>(punkOuter, this.CreateRowset(false), ref iid, new OleDbAggregator(this.InteropServices.AggregateRowset), out ppv);
		}

		// Token: 0x0600C13C RID: 49468 RVA: 0x0026DA34 File Offset: 0x0026BC34
		private int Execute<T>(IntPtr punkOuter, T output, ref Guid iid, OleDbAggregator aggregator, out IntPtr ppv)
		{
			object obj = this.executionLock;
			lock (obj)
			{
				this.currentExecution = (IDBAsynchStatus)((object)output);
				if (this.pendingCancel)
				{
					this.pendingCancel = false;
					this.currentExecution.Abort(new HCHAPTER
					{
						value = 0UL
					}, DBASYNCHOP.OPEN);
					this.currentExecution = null;
					ppv = IntPtr.Zero;
					return -2147217842;
				}
			}
			int num = aggregator(punkOuter, output, ref iid, out ppv);
			int num2;
			if (this.Properties.TryGetValue(DBPROPGROUP.Rowset, DBPROPID.ROWSET_ASYNCH, out num2) && num2 != 0)
			{
				num = 265936;
			}
			else if (num == 0)
			{
				try
				{
					IEvaluationResultSource evaluationResultSource = output as IEvaluationResultSource;
					if (evaluationResultSource != null)
					{
						evaluationResultSource.WaitForResults();
					}
				}
				catch
				{
					this.InteropServices.Release(ppv);
					ppv = IntPtr.Zero;
					throw;
				}
				if (!this.supportCancelAfterRowsetReturn)
				{
					obj = this.executionLock;
					lock (obj)
					{
						this.currentExecution = null;
					}
				}
			}
			return num;
		}

		// Token: 0x0600C13D RID: 49469 RVA: 0x000091AE File Offset: 0x000073AE
		void ICommandText.GetDBSession(ref Guid iid, out IntPtr session)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600C13E RID: 49470 RVA: 0x0026DB7C File Offset: 0x0026BD7C
		unsafe int ICommandText.GetCommandText(Guid* nativeDialect, out char* nativeCommandText)
		{
			Guid guid = Guid.Empty;
			if (nativeDialect != null)
			{
				guid = *nativeDialect;
				*nativeDialect = Guid.Empty;
			}
			nativeCommandText = (IntPtr)((UIntPtr)0);
			int num2;
			using (ComHeap comHeap = new ComHeap())
			{
				nativeCommandText = comHeap.AllocString(this.commandText);
				comHeap.Commit();
				int num = 0;
				if (nativeDialect != null)
				{
					*nativeDialect = this.dialect;
					if (guid != this.dialect)
					{
						num = 265933;
					}
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x0600C13F RID: 49471 RVA: 0x0026DC0C File Offset: 0x0026BE0C
		unsafe int ICommandText.SetCommandText(ref Guid dialect, char* commandText)
		{
			if (!this.IsDialectSupported(dialect))
			{
				return -2147217898;
			}
			this.dialect = dialect;
			this.commandText = new string(commandText);
			return 0;
		}

		// Token: 0x0600C140 RID: 49472 RVA: 0x0026DC3B File Offset: 0x0026BE3B
		int IConvertType.CanConvert(DBTYPE wFromType, DBTYPE wToType, DBCONVERTFLAGS dwConvertFlags)
		{
			return this.convert.CanConvert(wFromType, wToType);
		}

		// Token: 0x0600C141 RID: 49473 RVA: 0x0026DC4C File Offset: 0x0026BE4C
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings)
		{
			using (RowsetHolder rowsetHolder = new RowsetHolder(this.CreateRowset(true)))
			{
				((IColumnsInfo)rowsetHolder.Rowset).GetColumnInfo(out countColumnInfos, out nativeColumnInfos, out nativeStrings);
			}
		}

		// Token: 0x0600C142 RID: 49474 RVA: 0x0026DC9C File Offset: 0x0026BE9C
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL cColumnIDs, DBID* rgColumnIDs, DBORDINAL* rgColumns)
		{
			using (RowsetHolder rowsetHolder = new RowsetHolder(this.CreateRowset(true)))
			{
				((IColumnsInfo)rowsetHolder.Rowset).MapColumnIDs(cColumnIDs, rgColumnIDs, rgColumns);
			}
		}

		// Token: 0x0600C143 RID: 49475 RVA: 0x0026DCEC File Offset: 0x0026BEEC
		int ISupportErrorInfo.InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.ICommand || iid == IID.ICommandText)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x04006185 RID: 24965
		private readonly IInteropServices interopServices;

		// Token: 0x04006186 RID: 24966
		private readonly bool supportCancelAfterRowsetReturn;

		// Token: 0x04006187 RID: 24967
		private readonly IManagedDataConvert convert;

		// Token: 0x04006188 RID: 24968
		private Guid dialect;

		// Token: 0x04006189 RID: 24969
		private string commandText;

		// Token: 0x0400618A RID: 24970
		private Accessor accessor;

		// Token: 0x0400618B RID: 24971
		private IDBAsynchStatus currentExecution;

		// Token: 0x0400618C RID: 24972
		private bool pendingCancel;

		// Token: 0x0400618D RID: 24973
		private readonly object executionLock = new object();
	}
}
