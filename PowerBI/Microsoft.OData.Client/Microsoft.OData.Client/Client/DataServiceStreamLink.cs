using System;
using System.ComponentModel;

namespace Microsoft.OData.Client
{
	// Token: 0x0200004D RID: 77
	public sealed class DataServiceStreamLink : INotifyPropertyChanged
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000957D File Offset: 0x0000777D
		internal DataServiceStreamLink(string name)
		{
			this.name = name;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000253 RID: 595 RVA: 0x0000958C File Offset: 0x0000778C
		// (remove) Token: 0x06000254 RID: 596 RVA: 0x000095C4 File Offset: 0x000077C4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000095F9 File Offset: 0x000077F9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009601 File Offset: 0x00007801
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00009609 File Offset: 0x00007809
		public Uri SelfLink
		{
			get
			{
				return this.selfLink;
			}
			internal set
			{
				this.selfLink = value;
				this.OnPropertyChanged("SelfLink");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000961D File Offset: 0x0000781D
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00009625 File Offset: 0x00007825
		public Uri EditLink
		{
			get
			{
				return this.editLink;
			}
			internal set
			{
				this.editLink = value;
				this.OnPropertyChanged("EditLink");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009639 File Offset: 0x00007839
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00009641 File Offset: 0x00007841
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			internal set
			{
				this.contentType = value;
				this.OnPropertyChanged("ContentType");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00009655 File Offset: 0x00007855
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000965D File Offset: 0x0000785D
		public string ETag
		{
			get
			{
				return this.etag;
			}
			internal set
			{
				this.etag = value;
				this.OnPropertyChanged("ETag");
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009671 File Offset: 0x00007871
		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly string name;

		// Token: 0x040000D0 RID: 208
		private Uri selfLink;

		// Token: 0x040000D1 RID: 209
		private Uri editLink;

		// Token: 0x040000D2 RID: 210
		private string contentType;

		// Token: 0x040000D3 RID: 211
		private string etag;
	}
}
