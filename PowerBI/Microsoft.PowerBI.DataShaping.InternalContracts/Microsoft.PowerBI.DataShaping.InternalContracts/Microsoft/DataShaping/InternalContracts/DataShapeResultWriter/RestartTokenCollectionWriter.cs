using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000058 RID: 88
	internal sealed class RestartTokenCollectionWriter : StreamingDsrWriterWrapperBase
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00005493 File Offset: 0x00003693
		internal override void Begin()
		{
			base.Writer.BeginArray();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000054A0 File Offset: 0x000036A0
		internal void WriteValue(object value)
		{
			base.Writer.WriteTypeEncodedValue(value);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000054AE File Offset: 0x000036AE
		internal void BeginCollection()
		{
			base.Writer.BeginArray();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000054BB File Offset: 0x000036BB
		internal void EndCollection()
		{
			base.Writer.EndArray();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000054C8 File Offset: 0x000036C8
		internal override void End()
		{
			base.Writer.EndArray();
		}
	}
}
