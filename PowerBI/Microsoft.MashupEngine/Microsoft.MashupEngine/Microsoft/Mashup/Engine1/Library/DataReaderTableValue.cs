using System;
using System.Data;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200023D RID: 573
	public static class DataReaderTableValue
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x00031148 File Offset: 0x0002F348
		public static TableValue New(ILifetimeService lifetimeService, Func<IDataReaderSource> getSource)
		{
			GetPageReaderTableValue getPageReaderTableValue = new GetPageReaderTableValue(lifetimeService, () => new DataReaderSourcePageReader(getSource()));
			if (getPageReaderTableValue.TableSource != null)
			{
				return RelatedTablesTableValue.New(getPageReaderTableValue, EmptyArray<RelatedTable>.Instance, Engine.CreateColumnIdentities(ArrayHelpers.NewArray<IColumnIdentity>(getPageReaderTableValue.TableSource.ColumnCount, new Func<int, IColumnIdentity>(getPageReaderTableValue.TableSource.ColumnIdentity))), Engine.CreateRelationships(getPageReaderTableValue.TableSource.Relationships ?? EmptyArray<IRelationship>.Instance));
			}
			return getPageReaderTableValue;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000311CC File Offset: 0x0002F3CC
		public static TableValue New(ILifetimeService lifetimeService, Func<IDataReader> getReader)
		{
			return new GetPageReaderTableValue(lifetimeService, () => new DataReaderPageReader(getReader()));
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000311F8 File Offset: 0x0002F3F8
		public static TableValue New(DataTable table)
		{
			return DataReaderTableValue.New(new DataReaderTableValue.DummyLifetimeService(), () => table.CreateDataReader());
		}

		// Token: 0x0200023E RID: 574
		private sealed class DummyLifetimeService : ILifetimeService, ITrackingService<IDisposable>
		{
			// Token: 0x060018FE RID: 6398 RVA: 0x0000336E File Offset: 0x0000156E
			public void Register(IDisposable disposable)
			{
			}

			// Token: 0x060018FF RID: 6399 RVA: 0x0000336E File Offset: 0x0000156E
			public void Unregister(IDisposable disposable)
			{
			}
		}
	}
}
