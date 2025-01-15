using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000030 RID: 48
	[OriginalName("ParameterValue")]
	public class ParameterValue : INotifyPropertyChanged
	{
		// Token: 0x06000204 RID: 516 RVA: 0x000053A0 File Offset: 0x000035A0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ParameterValue CreateParameterValue(bool isValueFieldReference)
		{
			return new ParameterValue
			{
				IsValueFieldReference = isValueFieldReference
			};
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000053AE File Offset: 0x000035AE
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000053B6 File Offset: 0x000035B6
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000053CA File Offset: 0x000035CA
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000053D2 File Offset: 0x000035D2
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000053E6 File Offset: 0x000035E6
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000053EE File Offset: 0x000035EE
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

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600020B RID: 523 RVA: 0x00005404 File Offset: 0x00003604
		// (remove) Token: 0x0600020C RID: 524 RVA: 0x0000543C File Offset: 0x0000363C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600020D RID: 525 RVA: 0x00005471 File Offset: 0x00003671
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400010A RID: 266
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400010B RID: 267
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;

		// Token: 0x0400010C RID: 268
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsValueFieldReference;
	}
}
