using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000108 RID: 264
	[Key("Id")]
	[OriginalName("HistorySnapshot")]
	public class HistorySnapshot : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x000165BF File Offset: 0x000147BF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static HistorySnapshot CreateHistorySnapshot(Guid ID, DateTimeOffset creationDate, int size)
		{
			return new HistorySnapshot
			{
				Id = ID,
				CreationDate = creationDate,
				Size = size
			};
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000165DB File Offset: 0x000147DB
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x000165E3 File Offset: 0x000147E3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000165F7 File Offset: 0x000147F7
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x000165FF File Offset: 0x000147FF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("HistoryId")]
		public string HistoryId
		{
			get
			{
				return this._HistoryId;
			}
			set
			{
				this._HistoryId = value;
				this.OnPropertyChanged("HistoryId");
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00016613 File Offset: 0x00014813
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0001661B File Offset: 0x0001481B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CreationDate")]
		public DateTimeOffset CreationDate
		{
			get
			{
				return this._CreationDate;
			}
			set
			{
				this._CreationDate = value;
				this.OnPropertyChanged("CreationDate");
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001662F File Offset: 0x0001482F
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00016637 File Offset: 0x00014837
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Size")]
		public int Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				this._Size = value;
				this.OnPropertyChanged("Size");
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06000B70 RID: 2928 RVA: 0x0001664C File Offset: 0x0001484C
		// (remove) Token: 0x06000B71 RID: 2929 RVA: 0x00016684 File Offset: 0x00014884
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B72 RID: 2930 RVA: 0x000166B9 File Offset: 0x000148B9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000530 RID: 1328
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000531 RID: 1329
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _HistoryId;

		// Token: 0x04000532 RID: 1330
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreationDate;

		// Token: 0x04000533 RID: 1331
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Size;
	}
}
