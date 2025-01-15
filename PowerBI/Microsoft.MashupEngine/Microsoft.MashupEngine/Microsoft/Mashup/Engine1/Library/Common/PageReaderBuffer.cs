using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010FC RID: 4348
	internal sealed class PageReaderBuffer : IDisposable
	{
		// Token: 0x060071C7 RID: 29127 RVA: 0x00187671 File Offset: 0x00185871
		public PageReaderBuffer(IPageReader pageReader)
		{
			this.pageReader = pageReader;
			this.pages = new List<IPage>();
			this.readerPageIndex = -1;
			this.buffering = true;
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x00187699 File Offset: 0x00185899
		public void StopBuffering()
		{
			this.buffering = false;
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x001876A2 File Offset: 0x001858A2
		public IPageReader GetPageReader()
		{
			return new PageReaderBuffer.PageReader(this);
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x001876AC File Offset: 0x001858AC
		public void Dispose()
		{
			if (this.pages != null)
			{
				foreach (IPage page in this.pages)
				{
					page.Dispose();
				}
				this.pages = null;
			}
			if (this.pageReader != null)
			{
				this.pageReader.Dispose();
				this.pageReader = null;
			}
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x00187728 File Offset: 0x00185928
		private PageReaderBuffer.BufferPage CreatePage()
		{
			return new PageReaderBuffer.BufferPage();
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x00187730 File Offset: 0x00185930
		private void ReadPage(int pageIndex, PageReaderBuffer.IBufferPage page)
		{
			if (pageIndex < this.pages.Count)
			{
				IPage page2 = this.pages[pageIndex];
				page.SetPage(page2, true);
				return;
			}
			if (pageIndex != this.readerPageIndex + 1)
			{
				throw new InvalidOperationException();
			}
			IPage page3 = this.pageReader.CreatePage();
			this.pageReader.Read(page3);
			this.readerPageIndex++;
			if (this.buffering)
			{
				this.pages.Add(page3);
			}
			page.SetPage(page3, this.buffering);
		}

		// Token: 0x04003EE6 RID: 16102
		private IPageReader pageReader;

		// Token: 0x04003EE7 RID: 16103
		private List<IPage> pages;

		// Token: 0x04003EE8 RID: 16104
		private int readerPageIndex;

		// Token: 0x04003EE9 RID: 16105
		private bool buffering;

		// Token: 0x020010FD RID: 4349
		private sealed class PageReader : IPageReader, IDisposable
		{
			// Token: 0x060071CD RID: 29133 RVA: 0x001877B9 File Offset: 0x001859B9
			public PageReader(PageReaderBuffer buffer)
			{
				this.buffer = buffer;
			}

			// Token: 0x17001FE5 RID: 8165
			// (get) Token: 0x060071CE RID: 29134 RVA: 0x001877C8 File Offset: 0x001859C8
			public TableSchema Schema
			{
				get
				{
					return this.buffer.pageReader.Schema;
				}
			}

			// Token: 0x17001FE6 RID: 8166
			// (get) Token: 0x060071CF RID: 29135 RVA: 0x001877DA File Offset: 0x001859DA
			public IProgress Progress
			{
				get
				{
					return this.buffer.pageReader.Progress;
				}
			}

			// Token: 0x17001FE7 RID: 8167
			// (get) Token: 0x060071D0 RID: 29136 RVA: 0x001877EC File Offset: 0x001859EC
			public int MaxPageRowCount
			{
				get
				{
					return this.buffer.pageReader.MaxPageRowCount;
				}
			}

			// Token: 0x060071D1 RID: 29137 RVA: 0x001877FE File Offset: 0x001859FE
			public IPage CreatePage()
			{
				return this.buffer.CreatePage();
			}

			// Token: 0x060071D2 RID: 29138 RVA: 0x0018780B File Offset: 0x00185A0B
			public void Read(IPage page)
			{
				this.buffer.ReadPage(this.pageIndex, (PageReaderBuffer.IBufferPage)page);
				this.pageIndex++;
			}

			// Token: 0x060071D3 RID: 29139 RVA: 0x000020FA File Offset: 0x000002FA
			public IPageReader NextResult()
			{
				return null;
			}

			// Token: 0x060071D4 RID: 29140 RVA: 0x00187832 File Offset: 0x00185A32
			public void Dispose()
			{
				this.buffer = null;
			}

			// Token: 0x04003EEA RID: 16106
			private PageReaderBuffer buffer;

			// Token: 0x04003EEB RID: 16107
			private int pageIndex;
		}

		// Token: 0x020010FE RID: 4350
		private interface IBufferPage : IPage, IDisposable
		{
			// Token: 0x060071D5 RID: 29141
			void SetPage(IPage page, bool buffered);
		}

		// Token: 0x020010FF RID: 4351
		private class BufferPage : PageReaderBuffer.IBufferPage, IPage, IDisposable, ILeaveEngineContext<IPage>, IEnterEngineContext<IPage>
		{
			// Token: 0x060071D6 RID: 29142 RVA: 0x0018783B File Offset: 0x00185A3B
			public BufferPage()
			{
				this.enterOnUsePage = new PageReaderBuffer.BufferPage.EnterOnUseBufferPage(this);
				this.leaveOnUsePage = new PageReaderBuffer.BufferPage.LeaveOnUseBufferPage(this);
			}

			// Token: 0x060071D7 RID: 29143 RVA: 0x0018785C File Offset: 0x00185A5C
			public void SetPage(IPage page, bool buffered)
			{
				if (this.page != null && !this.buffered)
				{
					this.page.Dispose();
				}
				this.page = page;
				this.buffered = buffered;
				this.pageException = null;
				this.enterOnUsePage.Clear();
				this.leaveOnUsePage.Clear();
			}

			// Token: 0x17001FE8 RID: 8168
			// (get) Token: 0x060071D8 RID: 29144 RVA: 0x001878AF File Offset: 0x00185AAF
			public int ColumnCount
			{
				get
				{
					return this.page.ColumnCount;
				}
			}

			// Token: 0x17001FE9 RID: 8169
			// (get) Token: 0x060071D9 RID: 29145 RVA: 0x001878BC File Offset: 0x00185ABC
			public int RowCount
			{
				get
				{
					if (this.page == null)
					{
						return 0;
					}
					return this.page.RowCount;
				}
			}

			// Token: 0x060071DA RID: 29146 RVA: 0x001878D3 File Offset: 0x00185AD3
			public IColumn GetColumn(int ordinal)
			{
				return this.page.GetColumn(ordinal);
			}

			// Token: 0x17001FEA RID: 8170
			// (get) Token: 0x060071DB RID: 29147 RVA: 0x001878E1 File Offset: 0x00185AE1
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.page.ExceptionRows;
				}
			}

			// Token: 0x17001FEB RID: 8171
			// (get) Token: 0x060071DC RID: 29148 RVA: 0x001878EE File Offset: 0x00185AEE
			public ISerializedException PageException
			{
				get
				{
					if (this.page != null)
					{
						this.pageException = this.page.PageException;
					}
					return this.pageException;
				}
			}

			// Token: 0x060071DD RID: 29149 RVA: 0x0018790F File Offset: 0x00185B0F
			public void Dispose()
			{
				if (this.page != null && !this.buffered)
				{
					this.page.Dispose();
				}
				this.page = null;
			}

			// Token: 0x060071DE RID: 29150 RVA: 0x00187933 File Offset: 0x00185B33
			public IPage Leave()
			{
				return this.enterOnUsePage;
			}

			// Token: 0x060071DF RID: 29151 RVA: 0x0018793B File Offset: 0x00185B3B
			public IPage Enter()
			{
				return this.leaveOnUsePage;
			}

			// Token: 0x04003EEC RID: 16108
			private readonly PageReaderBuffer.BufferPage.EnterOnUseBufferPage enterOnUsePage;

			// Token: 0x04003EED RID: 16109
			private readonly PageReaderBuffer.BufferPage.LeaveOnUseBufferPage leaveOnUsePage;

			// Token: 0x04003EEE RID: 16110
			private IPage page;

			// Token: 0x04003EEF RID: 16111
			private bool buffered;

			// Token: 0x04003EF0 RID: 16112
			private ISerializedException pageException;

			// Token: 0x02001100 RID: 4352
			private class EnterOnUseBufferPage : VirtualPage, PageReaderBuffer.IBufferPage, IPage, IDisposable, IEnterEngineContext<IPage>
			{
				// Token: 0x060071E0 RID: 29152 RVA: 0x00187943 File Offset: 0x00185B43
				public EnterOnUseBufferPage(PageReaderBuffer.BufferPage bufferPage)
				{
					this.bufferPage = bufferPage;
				}

				// Token: 0x17001FEC RID: 8172
				// (get) Token: 0x060071E1 RID: 29153 RVA: 0x00187952 File Offset: 0x00185B52
				protected override IPage Page
				{
					get
					{
						if (this.enterOnUsePage == null)
						{
							this.enterOnUsePage = this.bufferPage.page.LeaveEngineContext<IPage>();
						}
						return this.enterOnUsePage;
					}
				}

				// Token: 0x060071E2 RID: 29154 RVA: 0x00187978 File Offset: 0x00185B78
				public void SetPage(IPage page, bool buffered)
				{
					this.bufferPage.SetPage(page.EnterEngineContext<IPage>(), buffered);
				}

				// Token: 0x060071E3 RID: 29155 RVA: 0x0018798C File Offset: 0x00185B8C
				public override void Dispose()
				{
					using (EngineContext.Enter())
					{
						this.bufferPage.Dispose();
					}
				}

				// Token: 0x060071E4 RID: 29156 RVA: 0x001879CC File Offset: 0x00185BCC
				public void Clear()
				{
					this.enterOnUsePage = null;
				}

				// Token: 0x060071E5 RID: 29157 RVA: 0x001879D5 File Offset: 0x00185BD5
				public IPage Enter()
				{
					return this.bufferPage;
				}

				// Token: 0x04003EF1 RID: 16113
				private readonly PageReaderBuffer.BufferPage bufferPage;

				// Token: 0x04003EF2 RID: 16114
				private IPage enterOnUsePage;
			}

			// Token: 0x02001101 RID: 4353
			private class LeaveOnUseBufferPage : VirtualPage, PageReaderBuffer.IBufferPage, IPage, IDisposable, ILeaveEngineContext<IPage>
			{
				// Token: 0x060071E6 RID: 29158 RVA: 0x001879DD File Offset: 0x00185BDD
				public LeaveOnUseBufferPage(PageReaderBuffer.BufferPage bufferPage)
				{
					this.bufferPage = bufferPage;
				}

				// Token: 0x17001FED RID: 8173
				// (get) Token: 0x060071E7 RID: 29159 RVA: 0x001879EC File Offset: 0x00185BEC
				protected override IPage Page
				{
					get
					{
						if (this.leaveOnUsePage == null)
						{
							this.leaveOnUsePage = this.bufferPage.page.EnterEngineContext<IPage>();
						}
						return this.leaveOnUsePage;
					}
				}

				// Token: 0x060071E8 RID: 29160 RVA: 0x00187A12 File Offset: 0x00185C12
				public void SetPage(IPage page, bool buffered)
				{
					this.bufferPage.SetPage(page.LeaveEngineContext<IPage>(), buffered);
				}

				// Token: 0x060071E9 RID: 29161 RVA: 0x00187A28 File Offset: 0x00185C28
				public override void Dispose()
				{
					using (EngineContext.Leave())
					{
						this.bufferPage.Dispose();
					}
				}

				// Token: 0x060071EA RID: 29162 RVA: 0x00187A68 File Offset: 0x00185C68
				public void Clear()
				{
					this.leaveOnUsePage = null;
				}

				// Token: 0x060071EB RID: 29163 RVA: 0x00187A71 File Offset: 0x00185C71
				public IPage Leave()
				{
					return this.bufferPage;
				}

				// Token: 0x04003EF3 RID: 16115
				private readonly PageReaderBuffer.BufferPage bufferPage;

				// Token: 0x04003EF4 RID: 16116
				private IPage leaveOnUsePage;
			}
		}
	}
}
