using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000323 RID: 803
	internal sealed class ShimTextRunInstance : TextRunInstance
	{
		// Token: 0x06001DDE RID: 7646 RVA: 0x00075489 File Offset: 0x00073689
		internal ShimTextRunInstance(TextRun textRunDef, TextBoxInstance textBoxInstance)
			: base(textRunDef)
		{
			this.m_textBoxInstance = textBoxInstance;
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0007549C File Offset: 0x0007369C
		public override string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					ReportItem renderReportItem = this.m_reportElementDef.RenderReportItem;
					this.m_uniqueName = renderReportItem.ID + "i1i" + renderReportItem.UniqueName;
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000754DF File Offset: 0x000736DF
		public override MarkupType MarkupType
		{
			get
			{
				return MarkupType.None;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x000754E2 File Offset: 0x000736E2
		public override string Value
		{
			get
			{
				return this.m_textBoxInstance.Value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000754EF File Offset: 0x000736EF
		public override object OriginalValue
		{
			get
			{
				return this.m_textBoxInstance.OriginalValue;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000754FC File Offset: 0x000736FC
		public override TypeCode TypeCode
		{
			get
			{
				return this.m_textBoxInstance.TypeCode;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x00075509 File Offset: 0x00073709
		public override bool IsCompiled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0007550C File Offset: 0x0007370C
		public override bool ProcessedWithError
		{
			get
			{
				return this.OriginalValue == null && !string.IsNullOrEmpty(this.Value);
			}
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00075526 File Offset: 0x00073726
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x04000F70 RID: 3952
		private TextBoxInstance m_textBoxInstance;
	}
}
