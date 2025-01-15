using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000085 RID: 133
	[OriginalName("DataSetField")]
	public class DataSetField : INotifyPropertyChanged
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x0000B87F File Offset: 0x00009A7F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DataSetField CreateDataSetField(ReportParameterType dataType)
		{
			return new DataSetField
			{
				DataType = dataType
			};
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000B88D File Offset: 0x00009A8D
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0000B895 File Offset: 0x00009A95
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000B8A9 File Offset: 0x00009AA9
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0000B8B1 File Offset: 0x00009AB1
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

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060005D6 RID: 1494 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		// (remove) Token: 0x060005D7 RID: 1495 RVA: 0x0000B900 File Offset: 0x00009B00
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000B935 File Offset: 0x00009B35
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400029E RID: 670
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400029F RID: 671
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ReportParameterType _DataType;
	}
}
