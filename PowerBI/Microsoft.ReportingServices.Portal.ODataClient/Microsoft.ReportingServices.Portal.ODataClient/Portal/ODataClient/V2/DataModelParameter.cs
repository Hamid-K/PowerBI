using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000075 RID: 117
	[Key("Name")]
	[OriginalName("DataModelParameter")]
	public class DataModelParameter : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0000A978 File Offset: 0x00008B78
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataModelParameter CreateDataModelParameter(string name)
		{
			return new DataModelParameter
			{
				Name = name
			};
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000A986 File Offset: 0x00008B86
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000A98E File Offset: 0x00008B8E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000A9A2 File Offset: 0x00008BA2
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0000A9AA File Offset: 0x00008BAA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.OnPropertyChanged("Value");
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x0600052C RID: 1324 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		// (remove) Token: 0x0600052D RID: 1325 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600052E RID: 1326 RVA: 0x0000AA2D File Offset: 0x00008C2D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400025B RID: 603
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400025C RID: 604
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
