using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B43 RID: 6979
	public class TempPageService : ITempPageService, IDisposable
	{
		// Token: 0x0600AEA9 RID: 44713 RVA: 0x0023C478 File Offset: 0x0023A678
		public TempPageService()
		{
			this.pages = new HashSet<TempPageService.Page>();
		}

		// Token: 0x0600AEAA RID: 44714 RVA: 0x0023C48C File Offset: 0x0023A68C
		public Stream CreatePage(uint length)
		{
			TempPageService.Page page = new TempPageService.Page(this, (uint)MemoryMappedFile.PageAlign((long)((ulong)length)));
			HashSet<TempPageService.Page> hashSet = this.pages;
			lock (hashSet)
			{
				this.pages.Add(page);
			}
			return page.Stream.OnDispose(new Action(page.Dispose)).Take((long)((ulong)length));
		}

		// Token: 0x0600AEAB RID: 44715 RVA: 0x0023C500 File Offset: 0x0023A700
		public void Dispose()
		{
			HashSet<TempPageService.Page> hashSet = this.pages;
			lock (hashSet)
			{
				foreach (TempPageService.Page page in this.pages.ToList<TempPageService.Page>())
				{
					page.Dispose();
				}
			}
		}

		// Token: 0x0600AEAC RID: 44716 RVA: 0x0023C57C File Offset: 0x0023A77C
		private void RemovePage(TempPageService.Page page)
		{
			HashSet<TempPageService.Page> hashSet = this.pages;
			lock (hashSet)
			{
				this.pages.Remove(page);
			}
		}

		// Token: 0x04005A0E RID: 23054
		private readonly HashSet<TempPageService.Page> pages;

		// Token: 0x02001B44 RID: 6980
		private class Page
		{
			// Token: 0x0600AEAD RID: 44717 RVA: 0x0023C5C4 File Offset: 0x0023A7C4
			public Page(TempPageService service, uint length)
			{
				this.service = service;
				this.file = MemoryMappedFile.Create((ulong)length);
				this.view = this.file.MapView(0UL, length);
				this.stream = new MemoryMappedViewStream(this.view);
			}

			// Token: 0x17002BD0 RID: 11216
			// (get) Token: 0x0600AEAE RID: 44718 RVA: 0x0023C610 File Offset: 0x0023A810
			public Stream Stream
			{
				get
				{
					return this.stream;
				}
			}

			// Token: 0x0600AEAF RID: 44719 RVA: 0x0023C618 File Offset: 0x0023A818
			public void Dispose()
			{
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				if (this.view != null)
				{
					this.view.Dispose();
					this.view = null;
				}
				if (this.file != null)
				{
					this.file.Dispose();
					this.file = null;
				}
				this.service.RemovePage(this);
			}

			// Token: 0x04005A0F RID: 23055
			private readonly TempPageService service;

			// Token: 0x04005A10 RID: 23056
			private MemoryMappedFile file;

			// Token: 0x04005A11 RID: 23057
			private MemoryMappedView view;

			// Token: 0x04005A12 RID: 23058
			private MemoryMappedViewStream stream;
		}
	}
}
