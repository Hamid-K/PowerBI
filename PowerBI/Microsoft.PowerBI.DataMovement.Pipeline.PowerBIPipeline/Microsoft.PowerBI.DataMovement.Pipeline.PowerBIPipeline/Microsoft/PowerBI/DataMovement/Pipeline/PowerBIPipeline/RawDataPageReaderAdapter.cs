using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000016 RID: 22
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RawDataPageReaderAdapter : IRawDataPageReader, IPageReader, IDisposable
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003F5F File Offset: 0x0000215F
		internal RawDataPageReaderAdapter(IPageReader pageReader)
		{
			if (pageReader == null)
			{
				throw new ArgumentNullException("pageReader");
			}
			this.m_pageReader = pageReader;
			this.m_schemaTables = new List<DataTable> { pageReader.SchemaTable };
			this.cancelIssued = false;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003F9C File Offset: 0x0000219C
		public static Func<IPageReader, IRawDataPageReader> PageReaderAdapter
		{
			get
			{
				return (IPageReader pageReader) => new RawDataPageReaderAdapter(pageReader);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003FBD File Offset: 0x000021BD
		public DataTable SchemaTable
		{
			get
			{
				return this.SchemaTables.First<DataTable>();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003FCA File Offset: 0x000021CA
		public IEnumerable<DataTable> SchemaTables
		{
			get
			{
				return this.m_schemaTables;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003FD2 File Offset: 0x000021D2
		public IProgress Progress
		{
			get
			{
				return this.m_pageReader.Progress;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003FDF File Offset: 0x000021DF
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003FE9 File Offset: 0x000021E9
		public bool CancelIssued
		{
			get
			{
				return this.cancelIssued;
			}
			set
			{
				this.cancelIssued = value;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003FF4 File Offset: 0x000021F4
		public IPage CreatePage()
		{
			return this.m_pageReader.CreatePage();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004001 File Offset: 0x00002201
		public void Read(IPage page)
		{
			this.m_pageReader.Read(page);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000400F File Offset: 0x0000220F
		public void Dispose()
		{
			this.m_pageReader.Dispose();
		}

		// Token: 0x04000058 RID: 88
		private readonly IPageReader m_pageReader;

		// Token: 0x04000059 RID: 89
		private readonly IEnumerable<DataTable> m_schemaTables;

		// Token: 0x0400005A RID: 90
		private volatile bool cancelIssued;
	}
}
