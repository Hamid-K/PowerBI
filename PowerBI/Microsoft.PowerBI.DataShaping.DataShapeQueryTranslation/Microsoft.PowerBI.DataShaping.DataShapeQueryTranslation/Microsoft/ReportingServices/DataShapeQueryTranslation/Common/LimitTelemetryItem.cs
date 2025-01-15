using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Common
{
	// Token: 0x02000111 RID: 273
	internal sealed class LimitTelemetryItem : IStructuredToString
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x00028B84 File Offset: 0x00026D84
		internal LimitTelemetryItem(string name, ExpressionId value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00028B9A File Offset: 0x00026D9A
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00028BA2 File Offset: 0x00026DA2
		public ExpressionId Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00028BAC File Offset: 0x00026DAC
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00028BD5 File Offset: 0x00026DD5
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("LimitTelemetryItem");
			builder.WriteAttribute<string>("Name", this.m_name, false, false);
			builder.WriteProperty<ExpressionId>("Value", this.m_value, false);
			builder.EndObject();
		}

		// Token: 0x04000530 RID: 1328
		private readonly string m_name;

		// Token: 0x04000531 RID: 1329
		private readonly ExpressionId m_value;
	}
}
