using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000135 RID: 309
	public abstract class ODataMessageWriterSettingsBase
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x0001A212 File Offset: 0x00018412
		protected ODataMessageWriterSettingsBase()
		{
			this.checkCharacters = false;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001A221 File Offset: 0x00018421
		protected ODataMessageWriterSettingsBase(ODataMessageWriterSettingsBase other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettingsBase>(other, "other");
			this.checkCharacters = other.checkCharacters;
			this.indent = other.indent;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001A25D File Offset: 0x0001845D
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001A265 File Offset: 0x00018465
		public virtual bool Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001A26E File Offset: 0x0001846E
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0001A276 File Offset: 0x00018476
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001A27F File Offset: 0x0001847F
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0001A29A File Offset: 0x0001849A
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

		// Token: 0x04000312 RID: 786
		private ODataMessageQuotas messageQuotas;

		// Token: 0x04000313 RID: 787
		private bool checkCharacters;

		// Token: 0x04000314 RID: 788
		private bool indent;
	}
}
