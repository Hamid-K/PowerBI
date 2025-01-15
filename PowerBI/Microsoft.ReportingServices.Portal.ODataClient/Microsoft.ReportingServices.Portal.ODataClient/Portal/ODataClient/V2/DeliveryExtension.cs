using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000088 RID: 136
	[Key("Name")]
	[OriginalName("DeliveryExtension")]
	public class DeliveryExtension : Extension
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DeliveryExtension CreateDeliveryExtension(ExtensionType extensionType, string name, bool visible, bool isImmutable, bool defaultDeliveryExtension)
		{
			return new DeliveryExtension
			{
				ExtensionType = extensionType,
				Name = name,
				Visible = visible,
				IsImmutable = isImmutable,
				DefaultDeliveryExtension = defaultDeliveryExtension
			};
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000BAEE File Offset: 0x00009CEE
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000BAF6 File Offset: 0x00009CF6
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000BB0A File Offset: 0x00009D0A
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000BB12 File Offset: 0x00009D12
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

		// Token: 0x040002A8 RID: 680
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _IsImmutable;

		// Token: 0x040002A9 RID: 681
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DefaultDeliveryExtension;
	}
}
