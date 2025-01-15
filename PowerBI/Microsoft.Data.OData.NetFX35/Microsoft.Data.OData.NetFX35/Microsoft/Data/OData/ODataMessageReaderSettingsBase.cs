using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000134 RID: 308
	public abstract class ODataMessageReaderSettingsBase
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0001A150 File Offset: 0x00018350
		protected ODataMessageReaderSettingsBase()
		{
			this.checkCharacters = false;
			this.enableAtomMetadataReading = false;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001A168 File Offset: 0x00018368
		protected ODataMessageReaderSettingsBase(ODataMessageReaderSettingsBase other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettingsBase>(other, "other");
			this.checkCharacters = other.checkCharacters;
			this.enableAtomMetadataReading = other.enableAtomMetadataReading;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
			this.shouldIncludeAnnotation = other.shouldIncludeAnnotation;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001A1BB File Offset: 0x000183BB
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0001A1C3 File Offset: 0x000183C3
		public virtual bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.checkCharacters = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001A1CC File Offset: 0x000183CC
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0001A1D4 File Offset: 0x000183D4
		public virtual bool EnableAtomMetadataReading
		{
			get
			{
				return this.enableAtomMetadataReading;
			}
			set
			{
				this.enableAtomMetadataReading = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001A1DD File Offset: 0x000183DD
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0001A1F8 File Offset: 0x000183F8
		public virtual ODataMessageQuotas MessageQuotas
		{
			get
			{
				if (this.messageQuotas == null)
				{
					this.messageQuotas = new ODataMessageQuotas();
				}
				return this.messageQuotas;
			}
			set
			{
				this.messageQuotas = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001A201 File Offset: 0x00018401
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001A209 File Offset: 0x00018409
		public virtual Func<string, bool> ShouldIncludeAnnotation
		{
			get
			{
				return this.shouldIncludeAnnotation;
			}
			set
			{
				this.shouldIncludeAnnotation = value;
			}
		}

		// Token: 0x0400030E RID: 782
		private ODataMessageQuotas messageQuotas;

		// Token: 0x0400030F RID: 783
		private bool checkCharacters;

		// Token: 0x04000310 RID: 784
		private bool enableAtomMetadataReading;

		// Token: 0x04000311 RID: 785
		private Func<string, bool> shouldIncludeAnnotation;
	}
}
