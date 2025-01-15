using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000021 RID: 33
	[Key("Id")]
	[EntitySet("Comments")]
	[OriginalName("Comment")]
	public class Comment : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00003D76 File Offset: 0x00001F76
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00003D99 File Offset: 0x00001F99
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00003DA1 File Offset: 0x00001FA1
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003DB5 File Offset: 0x00001FB5
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003DBD File Offset: 0x00001FBD
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003DD1 File Offset: 0x00001FD1
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00003DD9 File Offset: 0x00001FD9
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003DED File Offset: 0x00001FED
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00003DF5 File Offset: 0x00001FF5
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003E09 File Offset: 0x00002009
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00003E11 File Offset: 0x00002011
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003E25 File Offset: 0x00002025
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00003E2D File Offset: 0x0000202D
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00003E41 File Offset: 0x00002041
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00003E49 File Offset: 0x00002049
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003E5D File Offset: 0x0000205D
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00003E65 File Offset: 0x00002065
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

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000158 RID: 344 RVA: 0x00003E7C File Offset: 0x0000207C
		// (remove) Token: 0x06000159 RID: 345 RVA: 0x00003EB4 File Offset: 0x000020B4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600015A RID: 346 RVA: 0x00003EE9 File Offset: 0x000020E9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000B6 RID: 182
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Id;

		// Token: 0x040000B7 RID: 183
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _ItemId;

		// Token: 0x040000B8 RID: 184
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _UserName;

		// Token: 0x040000B9 RID: 185
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long? _ThreadId;

		// Token: 0x040000BA RID: 186
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AttachmentPath;

		// Token: 0x040000BB RID: 187
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Text;

		// Token: 0x040000BC RID: 188
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreatedDate;

		// Token: 0x040000BD RID: 189
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset? _ModifiedDate;
	}
}
