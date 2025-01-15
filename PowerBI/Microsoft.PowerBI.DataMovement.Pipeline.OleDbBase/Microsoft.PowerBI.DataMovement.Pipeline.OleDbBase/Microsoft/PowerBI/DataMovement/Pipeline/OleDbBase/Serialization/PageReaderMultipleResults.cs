using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000CC RID: 204
	public sealed class PageReaderMultipleResults : IMultipleResults
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000A9F3 File Offset: 0x00008BF3
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public PageReaderMultipleResults(RowsetStreamBase stream, bool readColumnOrdinals)
		{
			this.m_multipleResultsGatewayResposeStream = stream;
			this.m_readColumnOrdinals = readColumnOrdinals;
			this.m_isFirstRowset = true;
			this.m_rowsetIndex = 0;
			this.m_pageReaderRowset = null;
			this.m_hasRowsetResult = this.GetHasRowset();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000AA2C File Offset: 0x00008C2C
		private int GetHasRowset()
		{
			bool flag;
			if (!this.m_multipleResultsGatewayResposeStream.TryGetHasRowset(out flag))
			{
				TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceError("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults: error reading first hasMoreRowsets flag.");
				return -2147217887;
			}
			TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceInformation("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults: hasRowset = {0}.", new object[] { flag });
			if (!flag)
			{
				return 265929;
			}
			return 0;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000AA88 File Offset: 0x00008C88
		public unsafe int GetResult(IntPtr unkOuter, DBRESULTFLAG resultFlag, ref Guid riid, DBROWCOUNT* rowsAffected, out IntPtr rowset)
		{
			rowset = IntPtr.Zero;
			if (riid != IID.IUnknown && riid != IID.IRowset)
			{
				TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceError("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults unsupported interface {0}.", new object[] { riid });
				return -2147217887;
			}
			if (this.m_pageReaderRowset != null && !this.m_pageReaderRowset.IsEndOfRowset)
			{
				if (!this.m_pageReaderRowset.IsLastRow)
				{
					TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceError("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults, tries to move to next rowset without consuming all rows. IsLastRow:{0}. RowCount:{1}", new object[]
					{
						this.m_pageReaderRowset.IsLastRow,
						this.m_pageReaderRowset.CurrentPageRowCount
					});
					return -2147217887;
				}
				this.m_pageReaderRowset.ReadPage();
				if (this.m_pageReaderRowset.CurrentPageRowCount != 0)
				{
					TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceError("DataMovement.Pipeline.OleDbBase.PageReaderMultipleResults, tries to move to next rowset without consuming all rows. Next page should be empty, but it has rowcount:{0}.", new object[] { this.m_pageReaderRowset.CurrentPageRowCount });
					return -2147217887;
				}
			}
			if (this.m_isFirstRowset)
			{
				this.m_isFirstRowset = false;
			}
			else
			{
				this.m_rowsetIndex++;
				TraceSourceBase<OleDbBaseTraceSource>.Tracer.TraceInformation("Start to read rowset at {0}", new object[] { this.m_rowsetIndex });
				this.m_hasRowsetResult = this.GetHasRowset();
			}
			if (this.m_hasRowsetResult != 0)
			{
				return this.m_hasRowsetResult;
			}
			OleDbPageReader oleDbPageReader = new OleDbPageReader(this.m_multipleResultsGatewayResposeStream, this.m_readColumnOrdinals);
			this.m_pageReaderRowset = new PageReaderRowset(oleDbPageReader);
			return Aggregator.AggregateRowset(unkOuter, this.m_pageReaderRowset, ref riid, out rowset);
		}

		// Token: 0x0400038A RID: 906
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly RowsetStreamBase m_multipleResultsGatewayResposeStream;

		// Token: 0x0400038B RID: 907
		private readonly bool m_readColumnOrdinals;

		// Token: 0x0400038C RID: 908
		private int m_hasRowsetResult;

		// Token: 0x0400038D RID: 909
		private int m_rowsetIndex;

		// Token: 0x0400038E RID: 910
		private bool m_isFirstRowset;

		// Token: 0x0400038F RID: 911
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private PageReaderRowset m_pageReaderRowset;
	}
}
