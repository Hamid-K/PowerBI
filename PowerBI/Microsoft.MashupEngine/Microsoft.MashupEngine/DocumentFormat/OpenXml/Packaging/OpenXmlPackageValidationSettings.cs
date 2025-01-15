using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200211B RID: 8475
	[Obsolete("This functionality is obsolete and will be removed from future version release. Please see OpenXmlValidator class for supported validation functionality.", false)]
	internal class OpenXmlPackageValidationSettings
	{
		// Token: 0x0600D1B9 RID: 53689 RVA: 0x0029BB1A File Offset: 0x00299D1A
		internal EventHandler<OpenXmlPackageValidationEventArgs> GetEventHandler()
		{
			return this.valEventHandler;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600D1BA RID: 53690 RVA: 0x0029BB22 File Offset: 0x00299D22
		// (remove) Token: 0x0600D1BB RID: 53691 RVA: 0x0029BB3B File Offset: 0x00299D3B
		public event EventHandler<OpenXmlPackageValidationEventArgs> EventHandler
		{
			add
			{
				this.valEventHandler = (EventHandler<OpenXmlPackageValidationEventArgs>)Delegate.Combine(this.valEventHandler, value);
			}
			remove
			{
				this.valEventHandler = (EventHandler<OpenXmlPackageValidationEventArgs>)Delegate.Remove(this.valEventHandler, value);
			}
		}

		// Token: 0x170032B2 RID: 12978
		// (get) Token: 0x0600D1BC RID: 53692 RVA: 0x0029BB54 File Offset: 0x00299D54
		// (set) Token: 0x0600D1BD RID: 53693 RVA: 0x0029BB5C File Offset: 0x00299D5C
		internal FileFormatVersions FileFormat { get; set; }

		// Token: 0x04006955 RID: 26965
		private EventHandler<OpenXmlPackageValidationEventArgs> valEventHandler;
	}
}
