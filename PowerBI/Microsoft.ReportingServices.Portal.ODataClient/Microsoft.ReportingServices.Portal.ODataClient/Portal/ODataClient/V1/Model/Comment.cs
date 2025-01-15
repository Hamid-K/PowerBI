using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000CD RID: 205
	[Key("Id")]
	[EntitySet("Comments")]
	[OriginalName("Comment")]
	public class Comment : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x000129F3 File Offset: 0x00010BF3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Comment CreateComment(long ID, Guid itemId, string text, DateTimeOffset createdDate)
		{
			return new Comment
			{
				Id = ID,
				ItemId = itemId,
				Text = text,
				CreatedDate = createdDate
			};
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00012A16 File Offset: 0x00010C16
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00012A1E File Offset: 0x00010C1E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00012A32 File Offset: 0x00010C32
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x00012A3A File Offset: 0x00010C3A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ItemId")]
		public Guid ItemId
		{
			get
			{
				return this._ItemId;
			}
			set
			{
				this._ItemId = value;
				this.OnPropertyChanged("ItemId");
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00012A4E File Offset: 0x00010C4E
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00012A56 File Offset: 0x00010C56
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UserName")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
				this.OnPropertyChanged("UserName");
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00012A6A File Offset: 0x00010C6A
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00012A72 File Offset: 0x00010C72
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ThreadId")]
		public long? ThreadId
		{
			get
			{
				return this._ThreadId;
			}
			set
			{
				this._ThreadId = value;
				this.OnPropertyChanged("ThreadId");
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00012A86 File Offset: 0x00010C86
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00012A8E File Offset: 0x00010C8E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AttachmentPath")]
		public string AttachmentPath
		{
			get
			{
				return this._AttachmentPath;
			}
			set
			{
				this._AttachmentPath = value;
				this.OnPropertyChanged("AttachmentPath");
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00012AA2 File Offset: 0x00010CA2
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00012AAA File Offset: 0x00010CAA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Text")]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
				this.OnPropertyChanged("Text");
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00012ABE File Offset: 0x00010CBE
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00012AC6 File Offset: 0x00010CC6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CreatedDate")]
		public DateTimeOffset CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				this._CreatedDate = value;
				this.OnPropertyChanged("CreatedDate");
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00012ADA File Offset: 0x00010CDA
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x00012AE2 File Offset: 0x00010CE2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ModifiedDate")]
		public DateTimeOffset? ModifiedDate
		{
			get
			{
				return this._ModifiedDate;
			}
			set
			{
				this._ModifiedDate = value;
				this.OnPropertyChanged("ModifiedDate");
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06000924 RID: 2340 RVA: 0x00012AF8 File Offset: 0x00010CF8
		// (remove) Token: 0x06000925 RID: 2341 RVA: 0x00012B30 File Offset: 0x00010D30
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000926 RID: 2342 RVA: 0x00012B65 File Offset: 0x00010D65
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400044D RID: 1101
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Id;

		// Token: 0x0400044E RID: 1102
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _ItemId;

		// Token: 0x0400044F RID: 1103
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _UserName;

		// Token: 0x04000450 RID: 1104
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long? _ThreadId;

		// Token: 0x04000451 RID: 1105
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AttachmentPath;

		// Token: 0x04000452 RID: 1106
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Text;

		// Token: 0x04000453 RID: 1107
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreatedDate;

		// Token: 0x04000454 RID: 1108
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _ModifiedDate;
	}
}
