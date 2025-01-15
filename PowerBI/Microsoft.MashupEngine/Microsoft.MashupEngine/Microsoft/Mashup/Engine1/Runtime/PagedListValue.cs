using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015BF RID: 5567
	internal sealed class PagedListValue : StreamedListValue
	{
		// Token: 0x06008B76 RID: 35702 RVA: 0x001D51BA File Offset: 0x001D33BA
		public PagedListValue(Func<PagedListValue.GetCurrentEnumerator, RowCount, RowCount, Value> pageCreator, IEnumerable<IValueReference> enumerable, RowCount pageSize)
		{
			this.pageCreator = pageCreator;
			this.enumerable = enumerable;
			this.pageSize = pageSize;
		}

		// Token: 0x06008B77 RID: 35703 RVA: 0x001D51D7 File Offset: 0x001D33D7
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new PagedListValue.SplitEnumerator(this.pageCreator, this.pageSize, this.enumerable.GetEnumerator());
		}

		// Token: 0x04004C69 RID: 19561
		private readonly Func<PagedListValue.GetCurrentEnumerator, RowCount, RowCount, Value> pageCreator;

		// Token: 0x04004C6A RID: 19562
		private readonly IEnumerable<IValueReference> enumerable;

		// Token: 0x04004C6B RID: 19563
		private readonly RowCount pageSize;

		// Token: 0x020015C0 RID: 5568
		// (Invoke) Token: 0x06008B79 RID: 35705
		public delegate IEnumerator<IValueReference> GetCurrentEnumerator();

		// Token: 0x020015C1 RID: 5569
		private sealed class SplitEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06008B7C RID: 35708 RVA: 0x001D51F5 File Offset: 0x001D33F5
			public SplitEnumerator(Func<PagedListValue.GetCurrentEnumerator, RowCount, RowCount, Value> pageCreator, RowCount pageSize, IEnumerator<IValueReference> enumerator)
			{
				this.pageCreator = pageCreator;
				this.pageSize = pageSize;
				this.enumerator = enumerator;
				this.currentOffset = RowCount.Zero;
			}

			// Token: 0x170024B5 RID: 9397
			// (get) Token: 0x06008B7D RID: 35709 RVA: 0x001D521D File Offset: 0x001D341D
			public IValueReference Current
			{
				get
				{
					if (this.currentPage == null)
					{
						throw new InvalidOperationException();
					}
					return this.pageCreator(new PagedListValue.GetCurrentEnumerator(this.currentPage.GetEnumerator), this.currentOffset, this.pageSize);
				}
			}

			// Token: 0x170024B6 RID: 9398
			// (get) Token: 0x06008B7E RID: 35710 RVA: 0x001D5255 File Offset: 0x001D3455
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008B7F RID: 35711 RVA: 0x001D525D File Offset: 0x001D345D
			public void Dispose()
			{
				if (this.enumerator != null)
				{
					if (this.currentPage == null || !this.currentPage.TakeOwnership())
					{
						this.enumerator.Dispose();
					}
					this.enumerator = null;
				}
			}

			// Token: 0x06008B80 RID: 35712 RVA: 0x001D5290 File Offset: 0x001D3490
			public bool MoveNext()
			{
				if (this.enumerator == null)
				{
					return false;
				}
				if (this.currentPage != null)
				{
					this.currentOffset += this.pageSize;
					this.currentPage.NextPage();
				}
				if (!this.enumerator.MoveNext())
				{
					return false;
				}
				this.currentPage = new PagedListValue.SinglePage(this.enumerator, this.pageSize);
				return true;
			}

			// Token: 0x06008B81 RID: 35713 RVA: 0x000091AE File Offset: 0x000073AE
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04004C6C RID: 19564
			private readonly Func<PagedListValue.GetCurrentEnumerator, RowCount, RowCount, Value> pageCreator;

			// Token: 0x04004C6D RID: 19565
			private readonly RowCount pageSize;

			// Token: 0x04004C6E RID: 19566
			private IEnumerator<IValueReference> enumerator;

			// Token: 0x04004C6F RID: 19567
			private PagedListValue.SinglePage currentPage;

			// Token: 0x04004C70 RID: 19568
			private RowCount currentOffset;
		}

		// Token: 0x020015C2 RID: 5570
		private sealed class SinglePage
		{
			// Token: 0x06008B82 RID: 35714 RVA: 0x001D52F8 File Offset: 0x001D34F8
			public SinglePage(IEnumerator<IValueReference> enumerator, RowCount pageSize)
			{
				this.page = new PagedListValue.SinglePageEnumerator(enumerator, pageSize);
				this.firstEnumeration = true;
			}

			// Token: 0x06008B83 RID: 35715 RVA: 0x001D5314 File Offset: 0x001D3514
			public IEnumerator<IValueReference> GetEnumerator()
			{
				if (this.firstEnumeration)
				{
					this.firstEnumeration = false;
					return this.page;
				}
				return null;
			}

			// Token: 0x06008B84 RID: 35716 RVA: 0x001D532D File Offset: 0x001D352D
			public void NextPage()
			{
				this.page.NextPage();
			}

			// Token: 0x06008B85 RID: 35717 RVA: 0x001D533A File Offset: 0x001D353A
			public bool TakeOwnership()
			{
				if (this.firstEnumeration)
				{
					this.firstEnumeration = false;
					return false;
				}
				return this.page.TakeOwnership();
			}

			// Token: 0x04004C71 RID: 19569
			private PagedListValue.SinglePageEnumerator page;

			// Token: 0x04004C72 RID: 19570
			private bool firstEnumeration;
		}

		// Token: 0x020015C3 RID: 5571
		private sealed class SinglePageEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06008B86 RID: 35718 RVA: 0x001D5358 File Offset: 0x001D3558
			public SinglePageEnumerator(IEnumerator<IValueReference> enumerator, RowCount pageSize)
			{
				this.enumerator = enumerator;
				this.remainingItems = pageSize;
				this.isFirstItem = true;
			}

			// Token: 0x170024B7 RID: 9399
			// (get) Token: 0x06008B87 RID: 35719 RVA: 0x001D5375 File Offset: 0x001D3575
			public IValueReference Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x170024B8 RID: 9400
			// (get) Token: 0x06008B88 RID: 35720 RVA: 0x001D5382 File Offset: 0x001D3582
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06008B89 RID: 35721 RVA: 0x001D538C File Offset: 0x001D358C
			public void Dispose()
			{
				bool? flag = this.hasOwnership;
				bool flag2 = true;
				if (((flag.GetValueOrDefault() == flag2) & (flag != null)) && this.enumerator != null)
				{
					this.enumerator.Dispose();
					this.enumerator = null;
				}
			}

			// Token: 0x06008B8A RID: 35722 RVA: 0x001D53D0 File Offset: 0x001D35D0
			public bool MoveNext()
			{
				if (this.remainingItems.IsZero)
				{
					return false;
				}
				this.remainingItems = RowCount.op_Decrement(this.remainingItems);
				if (this.isFirstItem)
				{
					this.isFirstItem = false;
					return true;
				}
				return this.enumerator != null && this.enumerator.MoveNext();
			}

			// Token: 0x06008B8B RID: 35723 RVA: 0x000091AE File Offset: 0x000073AE
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06008B8C RID: 35724 RVA: 0x001D5424 File Offset: 0x001D3624
			public void NextPage()
			{
				bool? flag = this.hasOwnership;
				bool flag2 = false;
				if (((flag.GetValueOrDefault() == flag2) & (flag != null)) || this.remainingItems.IsZero)
				{
					return;
				}
				RowCount rowCount = this.remainingItems;
				List<IValueReference> list = new List<IValueReference>((int)rowCount.Value);
				while (this.MoveNext())
				{
					IValueReference valueReference = this.Current;
					list.Add(valueReference);
				}
				this.isFirstItem = false;
				this.remainingItems = rowCount;
				this.enumerator = list.GetEnumerator();
				this.hasOwnership = new bool?(false);
			}

			// Token: 0x06008B8D RID: 35725 RVA: 0x001D54B2 File Offset: 0x001D36B2
			public bool TakeOwnership()
			{
				if (this.hasOwnership == null)
				{
					this.hasOwnership = new bool?(true);
				}
				return this.hasOwnership.Value;
			}

			// Token: 0x04004C73 RID: 19571
			private IEnumerator<IValueReference> enumerator;

			// Token: 0x04004C74 RID: 19572
			private RowCount remainingItems;

			// Token: 0x04004C75 RID: 19573
			private bool isFirstItem;

			// Token: 0x04004C76 RID: 19574
			private bool? hasOwnership;
		}
	}
}
