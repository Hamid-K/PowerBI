using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000055 RID: 85
	internal sealed class MessageWriter : DsrObjectWriterBase
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x000053C5 File Offset: 0x000035C5
		internal void WriteCode(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Writer.WriteProperty(base.DsrNames.CodeUpper, value);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000053E6 File Offset: 0x000035E6
		internal void WriteMessage(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Writer.WriteProperty(base.DsrNames.MessageUpper, value);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005407 File Offset: 0x00003607
		internal void WriteObjectName(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Writer.WriteProperty(base.DsrNames.ObjectName, value);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005428 File Offset: 0x00003628
		internal void WriteObjectType(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Writer.WriteProperty(base.DsrNames.ObjectType, value);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005449 File Offset: 0x00003649
		internal void WritePropertyName(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Writer.WriteProperty(base.DsrNames.PropertyName, value);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000546A File Offset: 0x0000366A
		internal void WriteSeverity(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Writer.WriteProperty(base.DsrNames.Severity, value);
			}
		}
	}
}
