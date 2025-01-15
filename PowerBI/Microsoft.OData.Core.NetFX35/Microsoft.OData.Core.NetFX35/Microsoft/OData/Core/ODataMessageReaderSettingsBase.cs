using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core
{
	// Token: 0x02000181 RID: 385
	public abstract class ODataMessageReaderSettingsBase
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x00032DDE File Offset: 0x00030FDE
		[SuppressMessage("Microsoft.Usage", "CA2214:contains a call chain that results in a call to a virtual method defined by the class", Justification = "One derived type will only ever be created")]
		protected ODataMessageReaderSettingsBase()
		{
			this.checkCharacters = false;
			this.enableAtomMetadataReading = false;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00032DF4 File Offset: 0x00030FF4
		[SuppressMessage("Microsoft.Usage", "CA2214:contains a call chain that results in a call to a virtual method defined by the class", Justification = "One derived type will only ever be created")]
		protected ODataMessageReaderSettingsBase(ODataMessageReaderSettingsBase other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettingsBase>(other, "other");
			this.checkCharacters = other.checkCharacters;
			this.enableAtomMetadataReading = other.enableAtomMetadataReading;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
			this.shouldIncludeAnnotation = other.shouldIncludeAnnotation;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00032E47 File Offset: 0x00031047
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00032E4F File Offset: 0x0003104F
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00032E58 File Offset: 0x00031058
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00032E60 File Offset: 0x00031060
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00032E69 File Offset: 0x00031069
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00032E84 File Offset: 0x00031084
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

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00032E8D File Offset: 0x0003108D
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x00032E95 File Offset: 0x00031095
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

		// Token: 0x0400062A RID: 1578
		private ODataMessageQuotas messageQuotas;

		// Token: 0x0400062B RID: 1579
		private bool checkCharacters;

		// Token: 0x0400062C RID: 1580
		private bool enableAtomMetadataReading;

		// Token: 0x0400062D RID: 1581
		private Func<string, bool> shouldIncludeAnnotation;
	}
}
