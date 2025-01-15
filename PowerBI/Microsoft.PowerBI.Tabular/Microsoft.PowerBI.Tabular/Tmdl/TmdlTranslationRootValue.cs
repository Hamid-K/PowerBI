using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000151 RID: 337
	internal sealed class TmdlTranslationRootValue : TmdlValue
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x00090718 File Offset: 0x0008E918
		public TmdlTranslationRootValue(TmdlTranslationElement model)
			: this()
		{
			this.Root = model;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00090727 File Offset: 0x0008E927
		internal TmdlTranslationRootValue()
			: base(TmdlValueType.TranslationRoot, null, true, true)
		{
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00090733 File Offset: 0x0008E933
		public override object Value
		{
			get
			{
				return this.Root;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0009073B File Offset: 0x0008E93B
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x00090743 File Offset: 0x0008E943
		public TmdlTranslationElement Root { get; internal set; }

		// Token: 0x06001589 RID: 5513 RVA: 0x0009074C File Offset: 0x0008E94C
		private protected override void WriteBody(ITmdlWriter writer)
		{
			this.Root.WriteTo(writer);
		}
	}
}
