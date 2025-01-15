using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C09 RID: 7177
	public static class IPageReaderExtensions
	{
		// Token: 0x0600B31D RID: 45853 RVA: 0x00247124 File Offset: 0x00245324
		public static IPageReader OnDispose(this IPageReader pageReader, Action action)
		{
			return new IPageReaderExtensions.NotifyingPageReader(pageReader, null, action);
		}

		// Token: 0x0600B31E RID: 45854 RVA: 0x00247130 File Offset: 0x00245330
		public static IPageReader AfterDispose(this IPageReader pageReader, Action action)
		{
			return pageReader.OnDispose(delegate
			{
				try
				{
					pageReader.Dispose();
				}
				finally
				{
					action();
				}
			});
		}

		// Token: 0x0600B31F RID: 45855 RVA: 0x00247168 File Offset: 0x00245368
		public static IPageReader TraceToAndDispose(this IPageReader pageReader, IHostTrace trace)
		{
			IPageReader pageReader2 = new TracingPageReader(pageReader, trace, 1).AfterDispose(new Action(trace.Dispose));
			trace.Suspend();
			return pageReader2;
		}

		// Token: 0x0600B320 RID: 45856 RVA: 0x0024718A File Offset: 0x0024538A
		public static IPageReader OnReadToEndOfThisResult(this IPageReader pageReader, Action action)
		{
			return new IPageReaderExtensions.NotifyingPageReader(pageReader, action, null);
		}

		// Token: 0x0600B321 RID: 45857 RVA: 0x00247194 File Offset: 0x00245394
		public static IPageReader OnReadToEndOfAllResults(this IPageReader pageReader, Action action)
		{
			return new IPageReaderExtensions.NotifyingEndOfAllResultsPageReader(pageReader, action);
		}

		// Token: 0x02001C0A RID: 7178
		private sealed class NotifyingPageReader : DelegatingPageReader
		{
			// Token: 0x0600B322 RID: 45858 RVA: 0x0024719D File Offset: 0x0024539D
			public NotifyingPageReader(IPageReader pageReader, Action onReadToEnd, Action onDispose)
				: base(pageReader)
			{
				this.onReadToEnd = onReadToEnd;
				this.onDispose = onDispose;
			}

			// Token: 0x0600B323 RID: 45859 RVA: 0x002471B4 File Offset: 0x002453B4
			public override void Read(IPage page)
			{
				base.Read(page);
				if (this.onReadToEnd != null && page.RowCount == 0)
				{
					this.onReadToEnd();
				}
			}

			// Token: 0x0600B324 RID: 45860 RVA: 0x002471D8 File Offset: 0x002453D8
			public override void Dispose()
			{
				if (this.onDispose != null)
				{
					Action action = this.onDispose;
					this.onDispose = null;
					action();
				}
				base.Dispose();
			}

			// Token: 0x04005B65 RID: 23397
			private readonly Action onReadToEnd;

			// Token: 0x04005B66 RID: 23398
			private Action onDispose;
		}

		// Token: 0x02001C0B RID: 7179
		private sealed class NotifyingEndOfAllResultsPageReader : DelegatingPageReader
		{
			// Token: 0x0600B325 RID: 45861 RVA: 0x002471FA File Offset: 0x002453FA
			public NotifyingEndOfAllResultsPageReader(IPageReader pageReader, Action onReadToEnd)
				: base(pageReader)
			{
				this.onReadToEnd = onReadToEnd;
			}

			// Token: 0x0600B326 RID: 45862 RVA: 0x0024720A File Offset: 0x0024540A
			public override void Read(IPage page)
			{
				base.Read(page);
				if (page.RowCount == 0)
				{
					this.GetNextReader();
				}
			}

			// Token: 0x0600B327 RID: 45863 RVA: 0x00247221 File Offset: 0x00245421
			public override IPageReader NextResult()
			{
				this.GetNextReader();
				IPageReader pageReader = this.nextReader;
				this.nextReader = null;
				this.hasNextReader = false;
				return pageReader;
			}

			// Token: 0x0600B328 RID: 45864 RVA: 0x0024723D File Offset: 0x0024543D
			public override void Dispose()
			{
				base.Dispose();
				IPageReader pageReader = this.nextReader;
				if (pageReader == null)
				{
					return;
				}
				pageReader.Dispose();
			}

			// Token: 0x0600B329 RID: 45865 RVA: 0x00247258 File Offset: 0x00245458
			private void GetNextReader()
			{
				if (!this.hasNextReader)
				{
					this.nextReader = base.NextResult();
					if (this.nextReader != null)
					{
						this.nextReader = new IPageReaderExtensions.NotifyingEndOfAllResultsPageReader(this.nextReader, this.onReadToEnd);
					}
					else
					{
						this.onReadToEnd();
					}
					this.hasNextReader = true;
				}
			}

			// Token: 0x04005B67 RID: 23399
			private readonly Action onReadToEnd;

			// Token: 0x04005B68 RID: 23400
			private IPageReader nextReader;

			// Token: 0x04005B69 RID: 23401
			private bool hasNextReader;
		}
	}
}
