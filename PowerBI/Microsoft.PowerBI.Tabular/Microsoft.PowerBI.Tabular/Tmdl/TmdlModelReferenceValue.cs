using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x0200013F RID: 319
	internal sealed class TmdlModelReferenceValue : TmdlValue
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x0008CD31 File Offset: 0x0008AF31
		public TmdlModelReferenceValue(ObjectName objectName)
			: base(TmdlValueType.ModelReference, objectName.ToString(), false, true)
		{
			this.ObjectName = objectName;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0008CD50 File Offset: 0x0008AF50
		public override object Value
		{
			get
			{
				return this.ObjectName;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0008CD5D File Offset: 0x0008AF5D
		public ObjectName ObjectName { get; }

		// Token: 0x060014EC RID: 5356 RVA: 0x0008CD68 File Offset: 0x0008AF68
		private protected override void WriteValue(ITmdlWriter writer)
		{
			writer.Write(this.ObjectName.FullyQualifiedName, Array.Empty<object>());
		}
	}
}
