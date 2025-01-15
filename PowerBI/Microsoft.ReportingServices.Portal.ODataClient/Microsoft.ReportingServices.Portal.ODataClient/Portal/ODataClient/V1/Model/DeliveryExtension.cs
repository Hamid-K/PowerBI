using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000119 RID: 281
	[OriginalName("DeliveryExtension")]
	public class DeliveryExtension : Extension
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x00017750 File Offset: 0x00015950
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DeliveryExtension CreateDeliveryExtension(ExtensionType extensionType, bool visible, bool isImmutable, bool defaultDeliveryExtension)
		{
			return new DeliveryExtension
			{
				ExtensionType = extensionType,
				Visible = visible,
				IsImmutable = isImmutable,
				DefaultDeliveryExtension = defaultDeliveryExtension
			};
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00017773 File Offset: 0x00015973
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0001777B File Offset: 0x0001597B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IsImmutable")]
		public bool IsImmutable
		{
			get
			{
				return this._IsImmutable;
			}
			set
			{
				this._IsImmutable = value;
				this.OnPropertyChanged("IsImmutable");
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001778F File Offset: 0x0001598F
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x00017797 File Offset: 0x00015997
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DefaultDeliveryExtension")]
		public bool DefaultDeliveryExtension
		{
			get
			{
				return this._DefaultDeliveryExtension;
			}
			set
			{
				this._DefaultDeliveryExtension = value;
				this.OnPropertyChanged("DefaultDeliveryExtension");
			}
		}

		// Token: 0x0400057C RID: 1404
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsImmutable;

		// Token: 0x0400057D RID: 1405
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DefaultDeliveryExtension;
	}
}
