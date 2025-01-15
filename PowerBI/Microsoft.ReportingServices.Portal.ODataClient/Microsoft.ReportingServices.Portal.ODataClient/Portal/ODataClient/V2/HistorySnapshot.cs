using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200007C RID: 124
	[Key("Id")]
	[OriginalName("HistorySnapshot")]
	public class HistorySnapshot : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000AE4B File Offset: 0x0000904B
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000AE67 File Offset: 0x00009067
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0000AE6F File Offset: 0x0000906F
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000AE83 File Offset: 0x00009083
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000AE8B File Offset: 0x0000908B
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

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000AE9F File Offset: 0x0000909F
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0000AEA7 File Offset: 0x000090A7
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000AEBB File Offset: 0x000090BB
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0000AEC3 File Offset: 0x000090C3
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

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x0600056A RID: 1386 RVA: 0x0000AED8 File Offset: 0x000090D8
		// (remove) Token: 0x0600056B RID: 1387 RVA: 0x0000AF10 File Offset: 0x00009110
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600056C RID: 1388 RVA: 0x0000AF45 File Offset: 0x00009145
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400026F RID: 623
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000270 RID: 624
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _HistoryId;

		// Token: 0x04000271 RID: 625
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _CreationDate;

		// Token: 0x04000272 RID: 626
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Size;
	}
}
