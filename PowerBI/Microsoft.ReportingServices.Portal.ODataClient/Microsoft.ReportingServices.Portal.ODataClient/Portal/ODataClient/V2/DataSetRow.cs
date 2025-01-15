using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000026 RID: 38
	[Key("Id")]
	[EntitySet("DataSetData")]
	[OriginalName("DataSetRow")]
	public class DataSetRow : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600018D RID: 397 RVA: 0x000046E5 File Offset: 0x000028E5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSetRow CreateDataSetRow(Guid ID)
		{
			return new DataSetRow
			{
				Id = ID
			};
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000046F3 File Offset: 0x000028F3
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000046FB File Offset: 0x000028FB
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

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000190 RID: 400 RVA: 0x00004710 File Offset: 0x00002910
		// (remove) Token: 0x06000191 RID: 401 RVA: 0x00004748 File Offset: 0x00002948
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000192 RID: 402 RVA: 0x0000477D File Offset: 0x0000297D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000D6 RID: 214
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;
	}
}
