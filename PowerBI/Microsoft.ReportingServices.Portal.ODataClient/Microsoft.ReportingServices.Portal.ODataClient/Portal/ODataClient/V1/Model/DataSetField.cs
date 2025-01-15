using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000114 RID: 276
	[OriginalName("DataSetField")]
	public class DataSetField : INotifyPropertyChanged
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00017358 File Offset: 0x00015558
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSetField CreateDataSetField(ReportParameterType dataType)
		{
			return new DataSetField
			{
				DataType = dataType
			};
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00017366 File Offset: 0x00015566
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0001736E File Offset: 0x0001556E
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00017382 File Offset: 0x00015582
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0001738A File Offset: 0x0001558A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataType")]
		public ReportParameterType DataType
		{
			get
			{
				return this._DataType;
			}
			set
			{
				this._DataType = value;
				this.OnPropertyChanged("DataType");
			}
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06000BF8 RID: 3064 RVA: 0x000173A0 File Offset: 0x000155A0
		// (remove) Token: 0x06000BF9 RID: 3065 RVA: 0x000173D8 File Offset: 0x000155D8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001740D File Offset: 0x0001560D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400056C RID: 1388
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400056D RID: 1389
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterType _DataType;
	}
}
