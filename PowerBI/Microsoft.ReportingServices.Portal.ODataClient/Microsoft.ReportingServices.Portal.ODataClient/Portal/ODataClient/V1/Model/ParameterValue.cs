using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000BF RID: 191
	[OriginalName("ParameterValue")]
	public class ParameterValue : INotifyPropertyChanged
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x000110D2 File Offset: 0x0000F2D2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ParameterValue CreateParameterValue(bool isValueFieldReference)
		{
			return new ParameterValue
			{
				IsValueFieldReference = isValueFieldReference
			};
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000110E0 File Offset: 0x0000F2E0
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x000110E8 File Offset: 0x0000F2E8
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

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000110FC File Offset: 0x0000F2FC
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00011104 File Offset: 0x0000F304
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00011118 File Offset: 0x0000F318
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00011120 File Offset: 0x0000F320
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsValueFieldReference")]
		public bool IsValueFieldReference
		{
			get
			{
				return this._IsValueFieldReference;
			}
			set
			{
				this._IsValueFieldReference = value;
				this.OnPropertyChanged("IsValueFieldReference");
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000864 RID: 2148 RVA: 0x00011134 File Offset: 0x0000F334
		// (remove) Token: 0x06000865 RID: 2149 RVA: 0x0001116C File Offset: 0x0000F36C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000866 RID: 2150 RVA: 0x000111A1 File Offset: 0x0000F3A1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000400 RID: 1024
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000401 RID: 1025
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;

		// Token: 0x04000402 RID: 1026
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsValueFieldReference;
	}
}
