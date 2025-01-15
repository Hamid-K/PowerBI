using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000184 RID: 388
	public abstract class ODataMessageWriterSettingsBase
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00033D4B File Offset: 0x00031F4B
		[SuppressMessage("Microsoft.Usage", "CA2214:contains a call chain that results in a call to a virtual method defined by the class", Justification = "One derived type will only ever be created")]
		protected ODataMessageWriterSettingsBase()
		{
			this.checkCharacters = false;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00033D5A File Offset: 0x00031F5A
		[SuppressMessage("Microsoft.Usage", "CA2214:contains a call chain that results in a call to a virtual method defined by the class", Justification = "One derived type will only ever be created")]
		protected ODataMessageWriterSettingsBase(ODataMessageWriterSettingsBase other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettingsBase>(other, "other");
			this.checkCharacters = other.checkCharacters;
			this.indent = other.indent;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00033D96 File Offset: 0x00031F96
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00033D9E File Offset: 0x00031F9E
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

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00033DA7 File Offset: 0x00031FA7
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00033DAF File Offset: 0x00031FAF
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00033DB8 File Offset: 0x00031FB8
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00033DD3 File Offset: 0x00031FD3
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

		// Token: 0x04000649 RID: 1609
		private ODataMessageQuotas messageQuotas;

		// Token: 0x0400064A RID: 1610
		private bool checkCharacters;

		// Token: 0x0400064B RID: 1611
		private bool indent;
	}
}
