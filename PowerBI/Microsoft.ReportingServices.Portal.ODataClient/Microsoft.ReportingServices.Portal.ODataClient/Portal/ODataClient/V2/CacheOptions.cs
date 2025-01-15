using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000019 RID: 25
	[Key("Id")]
	[OriginalName("CacheOptions")]
	public class CacheOptions : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00003373 File Offset: 0x00001573
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CacheOptions CreateCacheOptions(Guid ID, ItemExecutionType executionType)
		{
			return new CacheOptions
			{
				Id = ID,
				ExecutionType = executionType
			};
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003388 File Offset: 0x00001588
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00003390 File Offset: 0x00001590
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000033A4 File Offset: 0x000015A4
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000033AC File Offset: 0x000015AC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ExecutionType")]
		public ItemExecutionType ExecutionType
		{
			get
			{
				return this._ExecutionType;
			}
			set
			{
				this._ExecutionType = value;
				this.OnPropertyChanged("ExecutionType");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000033C0 File Offset: 0x000015C0
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000033C8 File Offset: 0x000015C8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Expiration")]
		public ExpirationReference Expiration
		{
			get
			{
				return this._Expiration;
			}
			set
			{
				this._Expiration = value;
				this.OnPropertyChanged("Expiration");
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000E5 RID: 229 RVA: 0x000033DC File Offset: 0x000015DC
		// (remove) Token: 0x060000E6 RID: 230 RVA: 0x00003414 File Offset: 0x00001614
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000E7 RID: 231 RVA: 0x00003449 File Offset: 0x00001649
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400008A RID: 138
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400008B RID: 139
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ItemExecutionType _ExecutionType;

		// Token: 0x0400008C RID: 140
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ExpirationReference _Expiration;
	}
}
