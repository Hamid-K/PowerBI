using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000015 RID: 21
	[Key("Name")]
	[OriginalName("Property")]
	public class Property : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003193 File Offset: 0x00001393
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Property CreateProperty(string name)
		{
			return new Property
			{
				Name = name
			};
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000031A1 File Offset: 0x000013A1
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000031A9 File Offset: 0x000013A9
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000031BD File Offset: 0x000013BD
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000031C5 File Offset: 0x000013C5
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

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000CB RID: 203 RVA: 0x000031DC File Offset: 0x000013DC
		// (remove) Token: 0x060000CC RID: 204 RVA: 0x00003214 File Offset: 0x00001414
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000CD RID: 205 RVA: 0x00003249 File Offset: 0x00001449
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000084 RID: 132
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000085 RID: 133
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
