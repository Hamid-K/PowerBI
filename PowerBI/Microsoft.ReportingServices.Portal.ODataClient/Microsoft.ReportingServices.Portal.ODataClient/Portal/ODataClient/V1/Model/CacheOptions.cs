using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000CB RID: 203
	[OriginalName("CacheOptions")]
	public class CacheOptions : INotifyPropertyChanged
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00012905 File Offset: 0x00010B05
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static CacheOptions CreateCacheOptions(ItemExecutionType executionType)
		{
			return new CacheOptions
			{
				ExecutionType = executionType
			};
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00012913 File Offset: 0x00010B13
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0001291B File Offset: 0x00010B1B
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

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001292F File Offset: 0x00010B2F
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x00012937 File Offset: 0x00010B37
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

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600090C RID: 2316 RVA: 0x0001294C File Offset: 0x00010B4C
		// (remove) Token: 0x0600090D RID: 2317 RVA: 0x00012984 File Offset: 0x00010B84
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600090E RID: 2318 RVA: 0x000129B9 File Offset: 0x00010BB9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400044A RID: 1098
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ItemExecutionType _ExecutionType;

		// Token: 0x0400044B RID: 1099
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ExpirationReference _Expiration;
	}
}
