using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200132A RID: 4906
	internal class GetPageReaderTableValue : TableValue
	{
		// Token: 0x060081AB RID: 33195 RVA: 0x001B892B File Offset: 0x001B6B2B
		public GetPageReaderTableValue(ILifetimeService lifetimeService, Func<IPageReader> getPageReader)
		{
			this.lifetimeService = lifetimeService;
			this.getPageReader = getPageReader;
		}

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x060081AC RID: 33196 RVA: 0x001B8941 File Offset: 0x001B6B41
		public ITableSource TableSource
		{
			get
			{
				this.EnsureTableSourceAndType();
				return this.tableSource;
			}
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x060081AD RID: 33197 RVA: 0x001B894F File Offset: 0x001B6B4F
		public override TypeValue Type
		{
			get
			{
				this.EnsureTableSourceAndType();
				return this.type;
			}
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x001B8960 File Offset: 0x001B6B60
		public override IPageReader GetReader()
		{
			IPageReader pageReader;
			if (this.cachedPageReader != null)
			{
				pageReader = this.cachedPageReader;
				this.cachedPageReader = null;
			}
			else
			{
				pageReader = this.getPageReader();
			}
			this.SetTableSourceAndType(pageReader);
			return pageReader;
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x001B899C File Offset: 0x001B6B9C
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IPageReader reader = this.GetReader();
			return new GetPageReaderTableValue.Enumerator(this.Columns, reader);
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x001B89BC File Offset: 0x001B6BBC
		private void EnsureTableSourceAndType()
		{
			if (this.type == null && this.tableSource == null && this.cachedPageReader == null)
			{
				this.cachedPageReader = this.lifetimeService.RegisterForCleanup(this.GetReader());
			}
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x001B89F0 File Offset: 0x001B6BF0
		private void SetTableSourceAndType(IPageReader pageReader)
		{
			if (this.type == null || this.tableSource == null)
			{
				IPageReaderWithTableSource pageReaderWithTableSource = pageReader as IPageReaderWithTableSource;
				this.type = DataReaderSchemaTableTableTypeValue.New(pageReader.Schema);
				if (pageReaderWithTableSource != null)
				{
					this.tableSource = pageReaderWithTableSource.TableSource;
					this.type = GetPageReaderTableValue.SetKeys(this.type, this.tableSource);
				}
			}
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x001B8A4C File Offset: 0x001B6C4C
		private static TableTypeValue SetKeys(TableTypeValue type, ITableSource tableSource)
		{
			if (tableSource != null && tableSource.KeyColumnNames != null)
			{
				int[] columns = TableValue.GetColumns(type.ItemType.Fields.Keys, Keys.New(tableSource.KeyColumnNames.ToArray<string>()));
				if (columns.Length != 0)
				{
					TableKey tableKey = new TableKey(columns, true);
					type = type.ReplaceTableKeys(new TableKey[] { tableKey }).AsTableType;
				}
			}
			return type;
		}

		// Token: 0x04004696 RID: 18070
		private readonly ILifetimeService lifetimeService;

		// Token: 0x04004697 RID: 18071
		private readonly Func<IPageReader> getPageReader;

		// Token: 0x04004698 RID: 18072
		private TableTypeValue type;

		// Token: 0x04004699 RID: 18073
		private ITableSource tableSource;

		// Token: 0x0400469A RID: 18074
		private IPageReader cachedPageReader;

		// Token: 0x0200132B RID: 4907
		private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x060081B3 RID: 33203 RVA: 0x001B8AAE File Offset: 0x001B6CAE
			public Enumerator(Keys keys, IPageReader pageReader)
			{
				this.keys = keys;
				this.reader = new PageReaderDataReader(pageReader, new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties));
			}

			// Token: 0x17002316 RID: 8982
			// (get) Token: 0x060081B4 RID: 33204 RVA: 0x001B8AE1 File Offset: 0x001B6CE1
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17002317 RID: 8983
			// (get) Token: 0x060081B5 RID: 33205 RVA: 0x001B8AE9 File Offset: 0x001B6CE9
			public IValueReference Current
			{
				get
				{
					if (!this.hasRow)
					{
						throw new InvalidOperationException();
					}
					if (this.row == null)
					{
						this.row = this.CreateRow();
					}
					return this.row;
				}
			}

			// Token: 0x060081B6 RID: 33206 RVA: 0x001B8B13 File Offset: 0x001B6D13
			public void Dispose()
			{
				this.reader.Dispose();
			}

			// Token: 0x060081B7 RID: 33207 RVA: 0x000033E7 File Offset: 0x000015E7
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060081B8 RID: 33208 RVA: 0x001B8B20 File Offset: 0x001B6D20
			public bool MoveNext()
			{
				this.row = null;
				this.hasRow = this.reader.Read();
				return this.hasRow;
			}

			// Token: 0x060081B9 RID: 33209 RVA: 0x001B8B40 File Offset: 0x001B6D40
			private RecordValue CreateRow()
			{
				IValueReference[] array = new IValueReference[this.keys.Length];
				for (int i = 0; i < this.keys.Length; i++)
				{
					try
					{
						array[i] = ValueMarshaller.MarshalFromClr(this.reader.GetValue(i));
					}
					catch (ValueException ex)
					{
						array[i] = new ExceptionValueReference(ex);
					}
				}
				return RecordValue.New(this.keys, array);
			}

			// Token: 0x0400469B RID: 18075
			private readonly Keys keys;

			// Token: 0x0400469C RID: 18076
			private readonly IDataReader reader;

			// Token: 0x0400469D RID: 18077
			private bool hasRow;

			// Token: 0x0400469E RID: 18078
			private RecordValue row;
		}
	}
}
