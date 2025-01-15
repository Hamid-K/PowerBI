using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200007E RID: 126
	[OriginalName("DefinitionItem")]
	public class DefinitionItem : INotifyPropertyChanged
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x0000B086 File Offset: 0x00009286
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DefinitionItem CreateDefinitionItem(Guid ID)
		{
			return new DefinitionItem
			{
				Id = ID
			};
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000B094 File Offset: 0x00009294
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000B09C File Offset: 0x0000929C
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000B0B0 File Offset: 0x000092B0
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0000B0B8 File Offset: 0x000092B8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Path")]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				this._Path = value;
				this.OnPropertyChanged("Path");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000B0CC File Offset: 0x000092CC
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0000B0D4 File Offset: 0x000092D4
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000B0E8 File Offset: 0x000092E8
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000B0F0 File Offset: 0x000092F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Hash")]
		public string Hash
		{
			get
			{
				return this._Hash;
			}
			set
			{
				this._Hash = value;
				this.OnPropertyChanged("Hash");
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000583 RID: 1411 RVA: 0x0000B104 File Offset: 0x00009304
		// (remove) Token: 0x06000584 RID: 1412 RVA: 0x0000B13C File Offset: 0x0000933C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000585 RID: 1413 RVA: 0x0000B171 File Offset: 0x00009371
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000279 RID: 633
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400027A RID: 634
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Path;

		// Token: 0x0400027B RID: 635
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400027C RID: 636
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Hash;
	}
}
