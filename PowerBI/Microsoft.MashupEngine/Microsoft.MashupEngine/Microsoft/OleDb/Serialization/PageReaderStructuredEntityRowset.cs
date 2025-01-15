using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FA7 RID: 8103
	public class PageReaderStructuredEntityRowset : PageReaderRowset, IStructuredEntityRowset
	{
		// Token: 0x0600C5A1 RID: 50593 RVA: 0x00275EAA File Offset: 0x002740AA
		public PageReaderStructuredEntityRowset(IPageReader reader, IManagedDataConvert dataConvert, IDictionary<Type, DBTYPE> columnTypeProjection, Func<ISerializedException, Exception> pageExceptionHandler, bool cellErrorUnavailableStatus, IEvaluationTimeout evaluationTimeout)
			: base(reader, dataConvert, columnTypeProjection, pageExceptionHandler, cellErrorUnavailableStatus, evaluationTimeout)
		{
		}

		// Token: 0x0600C5A2 RID: 50594 RVA: 0x00275EBB File Offset: 0x002740BB
		public PageReaderStructuredEntityRowset(IPageReader reader, IManagedDataConvert dataConvert, IDictionary<Type, DBTYPE> columnTypeProjection, Func<ISerializedException, Exception> pageExceptionHandler, Func<ISerializedException, Exception> cellErrorHandler, IEvaluationTimeout evaluationTimeout)
			: base(reader, dataConvert, columnTypeProjection, pageExceptionHandler, cellErrorHandler, evaluationTimeout)
		{
		}

		// Token: 0x0600C5A3 RID: 50595 RVA: 0x00275ECC File Offset: 0x002740CC
		public unsafe static bool TryGetData(int row, Binding binding, byte* destBuffer, IManagedDataConvert dataConvert, Func<int, IOleDbColumn> getColumn)
		{
			PageReaderStructuredEntityRowset.StructuredEntityBinding structuredEntityBinding = binding as PageReaderStructuredEntityRowset.StructuredEntityBinding;
			if (structuredEntityBinding == null)
			{
				return false;
			}
			PageReaderRowset.GetData(row, structuredEntityBinding.SubBindings, destBuffer, dataConvert, (Binding b) => getColumn((int)b.Ordinal.value));
			return true;
		}

		// Token: 0x0600C5A4 RID: 50596 RVA: 0x00275F10 File Offset: 0x00274110
		public static bool TrySplitBinding(Binding binding, out Binding firstBinding, out Binding remainingBindings)
		{
			PageReaderStructuredEntityRowset.StructuredEntityBinding structuredEntityBinding = binding as PageReaderStructuredEntityRowset.StructuredEntityBinding;
			if (structuredEntityBinding == null)
			{
				firstBinding = null;
				remainingBindings = null;
				return false;
			}
			return structuredEntityBinding.TrySplitBinding(out firstBinding, out remainingBindings);
		}

		// Token: 0x0600C5A5 RID: 50597 RVA: 0x00275F38 File Offset: 0x00274138
		protected override ColumnInfo[] GetColumnInfos(TableSchema schema, IDictionary<Type, DBTYPE> columnTypeProjection)
		{
			ColumnInfo[] array = new ColumnInfo[schema.ColumnCount];
			int num;
			PageReaderStructuredEntityRowset.FillColumnInfos(schema, columnTypeProjection, array, out num);
			return array;
		}

		// Token: 0x0600C5A6 RID: 50598 RVA: 0x00275F5C File Offset: 0x0027415C
		private static void FillColumnInfos(TableSchema schema, IDictionary<Type, DBTYPE> columnTypeProjection, ColumnInfo[] columnInfos, out int maxOrdinal)
		{
			int num = columnInfos.Length - schema.ColumnCount;
			maxOrdinal = 0;
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				SchemaColumn column = schema.GetColumn(i);
				ColumnsInfo columnsInfo = ((column.ColumnSchema != null) ? new ColumnsInfo(PageReaderStructuredEntityRowset.GetEntityColumnInfos(column.ColumnSchema, columnTypeProjection)) : null);
				Type type;
				ValueWithMetadata.HasMetadata(column.DataType, out type);
				DBTYPE dbtype = ((column.ColumnSchema != null) ? DBTYPE.STRUCTUREDENTITY : PageReaderRowset.GetType(type, columnTypeProjection));
				columnInfos[num + i] = PageReaderStructuredEntityRowset.CreateColumnInfo(column, dbtype, PageReaderStructuredEntityRowset.GetEntityPropertyFormat(type), EntityPropertyFlags.None, columnsInfo);
				maxOrdinal = ((maxOrdinal < column.Ordinal.Value) ? column.Ordinal.Value : maxOrdinal);
			}
		}

		// Token: 0x0600C5A7 RID: 50599 RVA: 0x00276018 File Offset: 0x00274218
		private static ColumnInfo CreateColumnInfo(SchemaColumn column, DBTYPE dbType, EntityPropertyFormat columnFormat, EntityPropertyFlags columnFlags, ColumnsInfo subColumnsInfo)
		{
			DBLENGTH dblength;
			DBCOLUMNFLAGS dbcolumnflags;
			if (dbType == DBTYPE.STRUCTUREDENTITY)
			{
				dblength = DbLength.MaxValue;
				dbcolumnflags = DBCOLUMNFLAGS.NONE;
			}
			else
			{
				dblength = PageReaderRowset.GetTypeInfo(dbType, out dbcolumnflags);
			}
			if (subColumnsInfo != null || columnFlags != EntityPropertyFlags.None || columnFormat != EntityPropertyFormat.None)
			{
				return new PageReaderStructuredEntityRowset.EntityColumnInfo(new ColumnID(column.Name), new DBORDINAL
				{
					value = (ulong)column.Ordinal.Value
				}, dbcolumnflags | (column.Nullable ? ((DBCOLUMNFLAGS)96U) : DBCOLUMNFLAGS.NONE), dblength, dbType, 0, 0, subColumnsInfo, columnFormat, columnFlags);
			}
			return new ColumnInfo(new ColumnID(column.Name), new DBORDINAL
			{
				value = (ulong)column.Ordinal.Value
			}, dbcolumnflags | (column.Nullable ? ((DBCOLUMNFLAGS)96U) : DBCOLUMNFLAGS.NONE), dblength, dbType, 0, 0);
		}

		// Token: 0x0600C5A8 RID: 50600 RVA: 0x002760D4 File Offset: 0x002742D4
		private static ColumnInfo[] GetEntityColumnInfos(TableSchema schema, IDictionary<Type, DBTYPE> columnTypeProjection)
		{
			ColumnInfo[] array = new ColumnInfo[schema.ColumnCount + 1];
			int num;
			PageReaderStructuredEntityRowset.FillColumnInfos(schema, columnTypeProjection, array, out num);
			SchemaColumn schemaColumn = new SchemaColumn(PageReaderStructuredEntityRowset.GetUniqueDisplayName(schema))
			{
				DataType = typeof(string),
				Nullable = true,
				Ordinal = new int?(num + 1)
			};
			DBTYPE type = PageReaderRowset.GetType(schemaColumn.DataType, columnTypeProjection);
			array[0] = PageReaderStructuredEntityRowset.CreateColumnInfo(schemaColumn, type, PageReaderStructuredEntityRowset.GetEntityPropertyFormat(schemaColumn.DataType), EntityPropertyFlags.Display, null);
			return array;
		}

		// Token: 0x0600C5A9 RID: 50601 RVA: 0x00276150 File Offset: 0x00274350
		private static string GetUniqueDisplayName(TableSchema schema)
		{
			string text = "Title";
			int num = 2;
			int num2;
			while (schema.TryGetColumn(text, out num2))
			{
				text = "Title" + num.ToString(CultureInfo.InvariantCulture);
			}
			return text;
		}

		// Token: 0x0600C5AA RID: 50602 RVA: 0x0027618C File Offset: 0x0027438C
		private static EntityPropertyFormat GetEntityPropertyFormat(Type type)
		{
			if (type == typeof(DateTimeOffset) || type == typeof(DateTime))
			{
				return EntityPropertyFormat.DateTime;
			}
			if (type == typeof(TimeSpan))
			{
				return EntityPropertyFormat.Duration;
			}
			if (type == typeof(Time))
			{
				return EntityPropertyFormat.Time;
			}
			if (type == typeof(Date))
			{
				return EntityPropertyFormat.Date;
			}
			return EntityPropertyFormat.None;
		}

		// Token: 0x0600C5AB RID: 50603 RVA: 0x002761FC File Offset: 0x002743FC
		unsafe void IStructuredEntityRowset.GetEntityColumnInfo(DBORDINAL cOrdinals, DBORDINAL* nativeOrdinals, out DBORDINAL countColumnInfos, out EntityDbcolumninfo* _nativeColumnInfos, out char* _nativeStrings)
		{
			ColumnsInfo columnsInfo = base.InternalColumnsInfo;
			uint num = 0U;
			while ((ulong)num < cOrdinals.value)
			{
				PageReaderStructuredEntityRowset.EntityColumnInfo entityColumnInfo = null;
				foreach (ColumnInfo columnInfo in columnsInfo.ColumnInfos)
				{
					if (columnInfo.Ordinal.value == nativeOrdinals[(ulong)num * (ulong)((long)sizeof(DBORDINAL)) / (ulong)sizeof(DBORDINAL)].value)
					{
						entityColumnInfo = columnInfo as PageReaderStructuredEntityRowset.EntityColumnInfo;
						break;
					}
				}
				if (entityColumnInfo == null || entityColumnInfo.SubColumnsInfo == null)
				{
					throw new COMException("Invalid entity column index", -2147217887);
				}
				columnsInfo = entityColumnInfo.SubColumnsInfo;
				num += 1U;
			}
			ColumnInfo[] columnInfos = columnsInfo.ColumnInfos;
			if (columnInfos.Length != 0)
			{
				using (ComHeap comHeap = new ComHeap())
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ColumnInfo columnInfo2 in columnInfos)
					{
						if (columnInfo2.ColumnID.HasName)
						{
							stringBuilder.Append(columnInfo2.ColumnID.Name);
							stringBuilder.Append('\0');
						}
					}
					char* ptr = comHeap.AllocString(stringBuilder.ToString());
					int num2 = 0;
					EntityDbcolumninfo* ptr2 = (EntityDbcolumninfo*)comHeap.AllocArray(columnInfos.Length, sizeof(EntityDbcolumninfo));
					for (int j = 0; j < columnInfos.Length; j++)
					{
						DBCOLUMNINFO* ptr3 = &ptr2[j].dbcolumninfo;
						ColumnInfo columnInfo3 = columnInfos[j];
						ColumnID columnID = columnInfo3.ColumnID;
						ptr3->pwszName = ptr + num2;
						ptr3->pTypeInfo = null;
						ptr3->iOrdinal = columnInfo3.Ordinal;
						ptr3->dwFlags = columnInfo3.Flags;
						ptr3->ulColumnSize = columnInfo3.ColumnSize;
						ptr3->wType = columnInfo3.Type;
						ptr3->bPrecision = columnInfo3.Precision;
						ptr3->bScale = columnInfo3.Scale;
						ptr3->columnid.eKind = Microsoft.OleDb.ColumnsInfo.GetKind(columnID);
						PageReaderStructuredEntityRowset.EntityColumnInfo entityColumnInfo2 = columnInfo3 as PageReaderStructuredEntityRowset.EntityColumnInfo;
						ptr2[j].flags = ((entityColumnInfo2 != null) ? entityColumnInfo2.PropertyFlags : EntityPropertyFlags.None);
						ptr2[j].format = ((entityColumnInfo2 != null) ? entityColumnInfo2.PropertyFormat : EntityPropertyFormat.None);
						if (columnID.HasName)
						{
							ptr3->columnid.pwszName = ptr3->pwszName;
							num2 += columnID.Name.Length + 1;
						}
						if (columnID.HasGuid)
						{
							ptr3->columnid.guid = columnID.Guid;
						}
						if (columnID.HasPropertyID)
						{
							ptr3->columnid.ulPropid = (uint)columnID.PropertyID;
						}
					}
					comHeap.Commit();
					countColumnInfos = new DBORDINAL
					{
						value = (ulong)columnInfos.Length
					};
					_nativeColumnInfos = ptr2;
					_nativeStrings = ptr;
					return;
				}
			}
			countColumnInfos = default(DBORDINAL);
			_nativeColumnInfos = (IntPtr)((UIntPtr)0);
			_nativeStrings = (IntPtr)((UIntPtr)0);
		}

		// Token: 0x0600C5AC RID: 50604 RVA: 0x002764F0 File Offset: 0x002746F0
		void IStructuredEntityRowset.BindAccessor(HACCESSOR hAccessor, DBORDINAL dbOrdinal, HACCESSOR hAccessorChild)
		{
			Binding[] bindings = base.InternalAccessor.GetBinder(hAccessor).Bindings;
			for (int i = 0; i < bindings.Length; i++)
			{
				if (bindings[i].Ordinal.value == dbOrdinal.value)
				{
					Binder binder = base.InternalAccessor.GetBinder(hAccessorChild);
					bindings[i] = new PageReaderStructuredEntityRowset.StructuredEntityBinding(bindings[i], binder.Bindings);
					return;
				}
			}
			throw new COMException("Invalid ordinal parameter", -2147217887);
		}

		// Token: 0x02001FA8 RID: 8104
		private class EntityColumnInfo : ColumnInfo
		{
			// Token: 0x0600C5AD RID: 50605 RVA: 0x00276560 File Offset: 0x00274760
			public EntityColumnInfo(ColumnID columnID, DBORDINAL ordinal, DBCOLUMNFLAGS flags, DBLENGTH columnSize, DBTYPE type, byte precision, byte scale, ColumnsInfo subColumnsInfo, EntityPropertyFormat entityPropertyFormat, EntityPropertyFlags entityPropertyFlags)
				: base(columnID, ordinal, flags, columnSize, type, precision, scale)
			{
				this.subColumnsInfo = subColumnsInfo;
				this.propertyFormat = entityPropertyFormat;
				this.propertyFlags = entityPropertyFlags;
			}

			// Token: 0x17002FF7 RID: 12279
			// (get) Token: 0x0600C5AE RID: 50606 RVA: 0x0027658B File Offset: 0x0027478B
			public ColumnsInfo SubColumnsInfo
			{
				get
				{
					return this.subColumnsInfo;
				}
			}

			// Token: 0x17002FF8 RID: 12280
			// (get) Token: 0x0600C5AF RID: 50607 RVA: 0x00276593 File Offset: 0x00274793
			public EntityPropertyFormat PropertyFormat
			{
				get
				{
					return this.propertyFormat;
				}
			}

			// Token: 0x17002FF9 RID: 12281
			// (get) Token: 0x0600C5B0 RID: 50608 RVA: 0x0027659B File Offset: 0x0027479B
			public EntityPropertyFlags PropertyFlags
			{
				get
				{
					return this.propertyFlags;
				}
			}

			// Token: 0x04006500 RID: 25856
			private readonly ColumnsInfo subColumnsInfo;

			// Token: 0x04006501 RID: 25857
			private readonly EntityPropertyFormat propertyFormat;

			// Token: 0x04006502 RID: 25858
			private readonly EntityPropertyFlags propertyFlags;
		}

		// Token: 0x02001FA9 RID: 8105
		private class StructuredEntityBinding : Binding
		{
			// Token: 0x0600C5B1 RID: 50609 RVA: 0x002765A3 File Offset: 0x002747A3
			public StructuredEntityBinding(Binding binding, Binding[] subBindings)
				: base(binding, binding.Ordinal)
			{
				this.subBindings = subBindings;
			}

			// Token: 0x0600C5B2 RID: 50610 RVA: 0x002765B9 File Offset: 0x002747B9
			private StructuredEntityBinding(Binding binding, DBORDINAL newOrdinal, Binding[] subBindings)
				: base(binding, newOrdinal)
			{
				this.subBindings = subBindings;
			}

			// Token: 0x17002FFA RID: 12282
			// (get) Token: 0x0600C5B3 RID: 50611 RVA: 0x002765CA File Offset: 0x002747CA
			public Binding[] SubBindings
			{
				get
				{
					return this.subBindings;
				}
			}

			// Token: 0x0600C5B4 RID: 50612 RVA: 0x002765D4 File Offset: 0x002747D4
			public bool TrySplitBinding(out Binding firstBinding, out Binding remainingBindings)
			{
				if (this.subBindings == null || this.subBindings.Length == 0)
				{
					firstBinding = null;
					remainingBindings = null;
					return false;
				}
				if (this.remainingBindings == null)
				{
					Binding[] array = new Binding[this.subBindings.Length - 1];
					for (int i = 0; i < array.Length; i++)
					{
						Binding binding = this.subBindings[i + 1];
						Binding[] array2 = array;
						int num = i;
						Binding binding2 = binding;
						DBORDINAL dbordinal = PageReaderStructuredEntityRowset.StructuredEntityBinding.MakeOrdinal(binding.Ordinal.value - 1UL);
						PageReaderStructuredEntityRowset.StructuredEntityBinding structuredEntityBinding = binding as PageReaderStructuredEntityRowset.StructuredEntityBinding;
						array2[num] = new PageReaderStructuredEntityRowset.StructuredEntityBinding(binding2, dbordinal, (structuredEntityBinding != null) ? structuredEntityBinding.SubBindings : null);
					}
					this.firstBinding = new PageReaderStructuredEntityRowset.StructuredEntityBinding(this.subBindings[0], PageReaderStructuredEntityRowset.StructuredEntityBinding.MakeOrdinal(0UL), new Binding[] { this.subBindings[0] });
					this.remainingBindings = new PageReaderStructuredEntityRowset.StructuredEntityBinding(this, array);
				}
				firstBinding = this.firstBinding;
				remainingBindings = this.remainingBindings;
				return true;
			}

			// Token: 0x0600C5B5 RID: 50613 RVA: 0x002766A8 File Offset: 0x002748A8
			private static DBORDINAL MakeOrdinal(ulong index)
			{
				return new DBORDINAL
				{
					value = index
				};
			}

			// Token: 0x04006503 RID: 25859
			private readonly Binding[] subBindings;

			// Token: 0x04006504 RID: 25860
			private Binding firstBinding;

			// Token: 0x04006505 RID: 25861
			private Binding remainingBindings;
		}
	}
}
