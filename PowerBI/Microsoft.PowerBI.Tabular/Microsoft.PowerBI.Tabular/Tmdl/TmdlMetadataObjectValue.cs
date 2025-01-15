using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200013E RID: 318
	internal sealed class TmdlMetadataObjectValue : TmdlValue
	{
		// Token: 0x060014E5 RID: 5349 RVA: 0x0008CD07 File Offset: 0x0008AF07
		public TmdlMetadataObjectValue(ObjectType objectType)
			: base(TmdlValueType.MetadataObject, null, true, true)
		{
			this.Object = new TmdlObject(objectType);
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0008CD1F File Offset: 0x0008AF1F
		public override object Value
		{
			get
			{
				return this.Object;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x0008CD27 File Offset: 0x0008AF27
		public TmdlObject Object { get; }

		// Token: 0x060014E8 RID: 5352 RVA: 0x0008CD2F File Offset: 0x0008AF2F
		private protected override void WriteBody(ITmdlWriter writer)
		{
		}
	}
}
