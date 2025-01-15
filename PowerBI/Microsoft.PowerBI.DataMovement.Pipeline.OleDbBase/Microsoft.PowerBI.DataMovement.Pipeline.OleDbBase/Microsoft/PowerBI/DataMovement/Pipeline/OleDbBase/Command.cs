using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000036 RID: 54
	public abstract class Command : IAccessor, ICommand, ICommandText, ICommandWithParameters, ICommandProperties, IConvertType, IColumnsInfo, ISupportErrorInfo
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00005BEE File Offset: 0x00003DEE
		protected Command()
		{
			this.convert = DataConvert.GetInstance();
			this.dialect = DBGUID.Default;
			this.commandText = string.Empty;
			this.parameters = new DbParameters();
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001CE RID: 462
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract IDBProperties Properties
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00005C2D File Offset: 0x00003E2D
		protected Guid Dialect
		{
			get
			{
				return this.dialect;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00005C35 File Offset: 0x00003E35
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00005C3D File Offset: 0x00003E3D
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public string CommandText
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.commandText;
			}
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			set
			{
				this.commandText = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005C46 File Offset: 0x00003E46
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private IAccessor Accessor
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				if (this.accessor == null)
				{
					this.accessor = new Accessor();
				}
				return this.accessor;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00005C61 File Offset: 0x00003E61
		[global::System.Runtime.CompilerServices.Nullable(1)]
		protected DbParameters Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060001D4 RID: 468
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public unsafe abstract IRowset CreateRowset(bool forColumnInfo, DBPARAMS* parameters);

		// Token: 0x060001D5 RID: 469
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public unsafe abstract IMDDataset CreateMdDataset(DBPARAMS* parameters);

		// Token: 0x060001D6 RID: 470
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public unsafe abstract IMultipleResults CreateMultipleResults(bool forColumnInfo, DBPARAMS* parameters);

		// Token: 0x060001D7 RID: 471 RVA: 0x00005C69 File Offset: 0x00003E69
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public virtual IRowset CreateRowset(bool forColumnInfo)
		{
			return this.CreateRowset(forColumnInfo, null);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005C74 File Offset: 0x00003E74
		protected virtual bool IsDialectSupported(Guid guid)
		{
			return guid == DBGUID.Default;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00005C84 File Offset: 0x00003E84
		public virtual void Cancel()
		{
			object obj = this.executionLock;
			lock (obj)
			{
				if (this.currentExecution != null)
				{
					this.currentExecution.Abort(new HCHAPTER
					{
						Value = 0UL
					}, DBASYNCHOP.OPEN);
					this.currentExecution = null;
				}
				else
				{
					this.pendingCancel = true;
				}
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005CF4 File Offset: 0x00003EF4
		unsafe void IAccessor.AddRefAccessor(HACCESSOR accessor, uint* refCount)
		{
			this.Accessor.AddRefAccessor(accessor, refCount);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005D03 File Offset: 0x00003F03
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS accessorFlags, DBCOUNTITEM bindingCount, DBBINDING* bindings, DBLENGTH rowSize, out HACCESSOR accessor, DBBINDSTATUS* status)
		{
			this.Accessor.CreateAccessor(accessorFlags, bindingCount, bindings, rowSize, out accessor, status);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005D19 File Offset: 0x00003F19
		unsafe void IAccessor.GetBindings(HACCESSOR accessor, out DBACCESSORFLAGS accessorFlags, out DBCOUNTITEM bindingCount, out DBBINDING* bindings)
		{
			this.Accessor.GetBindings(accessor, out accessorFlags, out bindingCount, out bindings);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005D2B File Offset: 0x00003F2B
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR accessor, uint* refCount)
		{
			this.Accessor.ReleaseAccessor(accessor, refCount);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005D3A File Offset: 0x00003F3A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected Binder GetBinder(HACCESSOR accessor)
		{
			return ((Accessor)this.Accessor).GetBinder(accessor);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005D4D File Offset: 0x00003F4D
		void ICommand.Cancel()
		{
			((ICommandText)this).Cancel();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005D55 File Offset: 0x00003F55
		unsafe int ICommand.Execute(IntPtr punkOuter, ref Guid iid, DBPARAMS* parameters, DBROWCOUNT* rowsAffected, out IntPtr ppv)
		{
			return ((ICommandText)this).Execute(punkOuter, ref iid, parameters, rowsAffected, out ppv);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005D64 File Offset: 0x00003F64
		void ICommand.GetDBSession(ref Guid iid, out IntPtr session)
		{
			((ICommandText)this).GetDBSession(ref iid, out session);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005D6E File Offset: 0x00003F6E
		unsafe int ICommandProperties.GetProperties(uint countPropertyIDSets, DBPROPIDSET* nativePropertyIDSets, out uint countPropertySets, out DBPROPSET* nativePropertySets)
		{
			return this.Properties.GetProperties(countPropertyIDSets, nativePropertyIDSets, out countPropertySets, out nativePropertySets);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005D80 File Offset: 0x00003F80
		unsafe int ICommandProperties.SetProperties(uint propertySetCount, DBPROPSET* propertySets)
		{
			return this.Properties.SetProperties(propertySetCount, propertySets);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005D8F File Offset: 0x00003F8F
		unsafe int ICommandWithParameters.GetParameterInfo(out DB_UPARAMS paramCount, out DBPARAMINFO* paramInfo, char** namesBuffer)
		{
			return this.parameters.GetParameterInfo(out paramCount, out paramInfo, namesBuffer);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005D9F File Offset: 0x00003F9F
		unsafe int ICommandWithParameters.MapParameterNames(DB_UPARAMS paramNameCount, char** paramNames, DB_LPARAMS* paramOrdinals)
		{
			return this.parameters.MapParameterNames(paramNameCount, paramNames, paramOrdinals);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005DAF File Offset: 0x00003FAF
		unsafe int ICommandWithParameters.SetParameterInfo(DB_UPARAMS paramCount, DB_UPARAMS* paramOrdinals, DBPARAMBINDINFO* paramBindInfo)
		{
			return this.parameters.SetParameterInfo(paramCount, paramOrdinals, paramBindInfo);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005DC0 File Offset: 0x00003FC0
		unsafe int ICommandText.Execute(IntPtr punkOuter, ref Guid iid, DBPARAMS* parameters, DBROWCOUNT* rowsAffected, out IntPtr ppv)
		{
			if (rowsAffected != null)
			{
				*rowsAffected = new DBROWCOUNT
				{
					Value = -1L
				};
			}
			if (iid == IID.IMultipleResults)
			{
				IMultipleResults multipleResults = this.CreateMultipleResults(false, parameters);
				return Aggregator.AggregateMultipleResults(punkOuter, multipleResults, ref iid, out ppv);
			}
			return this.CreateSingleResult(punkOuter, ref iid, parameters, out ppv);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005E1D File Offset: 0x0000401D
		void ICommandText.GetDBSession(ref Guid iid, out IntPtr session)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005E24 File Offset: 0x00004024
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

		// Token: 0x060001EA RID: 490 RVA: 0x00005EB4 File Offset: 0x000040B4
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

		// Token: 0x060001EB RID: 491 RVA: 0x00005EE3 File Offset: 0x000040E3
		int IConvertType.CanConvert(DBTYPE fromType, DBTYPE toType, DBCONVERTFLAGS convertFlags)
		{
			return this.convert.CanConvert(fromType, toType);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005EF4 File Offset: 0x000040F4
		unsafe void IColumnsInfo.GetColumnInfo(out DBORDINAL countColumnInfos, out DBCOLUMNINFO* nativeColumnInfos, out char* nativeStrings)
		{
			using (RowsetHolder rowsetHolder = new RowsetHolder(this.CreateRowset(true)))
			{
				((IColumnsInfo)rowsetHolder.Rowset).GetColumnInfo(out countColumnInfos, out nativeColumnInfos, out nativeStrings);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005F44 File Offset: 0x00004144
		unsafe void IColumnsInfo.MapColumnIDs(DBORDINAL columnIDCount, DBID* columnIDs, DBORDINAL* columns)
		{
			using (RowsetHolder rowsetHolder = new RowsetHolder(this.CreateRowset(true)))
			{
				((IColumnsInfo)rowsetHolder.Rowset).MapColumnIDs(columnIDCount, columnIDs, columns);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005F94 File Offset: 0x00004194
		int ISupportErrorInfo.InterfaceSupportsErrorInfo(ref Guid iid)
		{
			if (iid == IID.ICommand || iid == IID.ICommandText)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005FC0 File Offset: 0x000041C0
		private unsafe int CreateSingleResult(IntPtr punkOuter, ref Guid iid, DBPARAMS* parameters, out IntPtr ppv)
		{
			IMDDataset imddataset = null;
			IRowset rowset = null;
			object obj;
			if (this.Dialect == DBGUID.Mdx)
			{
				imddataset = this.CreateMdDataset(parameters);
				obj = imddataset;
			}
			else
			{
				rowset = this.CreateRowset(false, parameters);
				obj = rowset;
			}
			object obj2 = this.executionLock;
			lock (obj2)
			{
				this.currentExecution = (IDBAsynchStatus)obj;
				if (this.pendingCancel)
				{
					this.pendingCancel = false;
					this.currentExecution.Abort(new HCHAPTER
					{
						Value = 0UL
					}, DBASYNCHOP.OPEN);
					this.currentExecution = null;
					ppv = IntPtr.Zero;
					return -2147217842;
				}
			}
			int num;
			if (imddataset != null)
			{
				num = Aggregator.AggregateMdDataset(punkOuter, imddataset, ref iid, out ppv);
			}
			else
			{
				num = Aggregator.AggregateRowset(punkOuter, rowset, ref iid, out ppv);
			}
			if (num < 0)
			{
				return num;
			}
			int num2;
			if (this.Properties.TryGetValue(DBPROPGROUP.Rowset, DBPROPID.ROWSET_ASYNCH, out num2) && num2 != 0)
			{
				return 265936;
			}
			if (rowset != null)
			{
				rowset.AddRefRows(new DBCOUNTITEM
				{
					Value = 0UL
				}, null, null, null);
			}
			obj2 = this.executionLock;
			lock (obj2)
			{
				this.currentExecution = null;
			}
			return 0;
		}

		// Token: 0x04000074 RID: 116
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly IDataConvert convert;

		// Token: 0x04000075 RID: 117
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly object executionLock = new object();

		// Token: 0x04000076 RID: 118
		private Guid dialect;

		// Token: 0x04000077 RID: 119
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private string commandText;

		// Token: 0x04000078 RID: 120
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private Accessor accessor;

		// Token: 0x04000079 RID: 121
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private DbParameters parameters;

		// Token: 0x0400007A RID: 122
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private IDBAsynchStatus currentExecution;

		// Token: 0x0400007B RID: 123
		private bool pendingCancel;
	}
}
