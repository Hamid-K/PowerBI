using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C8 RID: 712
	public sealed class ReportEnumProperty<EnumType> : ReportProperty where EnumType : struct
	{
		// Token: 0x06001AE7 RID: 6887 RVA: 0x0006BBCA File Offset: 0x00069DCA
		internal ReportEnumProperty()
		{
			this.m_value = default(EnumType);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0006BBDE File Offset: 0x00069DDE
		internal ReportEnumProperty(EnumType value)
		{
			this.m_value = value;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0006BBF0 File Offset: 0x00069DF0
		internal ReportEnumProperty(bool isExpression, string expressionString, EnumType value)
			: this(isExpression, expressionString, value, default(EnumType))
		{
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0006BC0F File Offset: 0x00069E0F
		internal ReportEnumProperty(bool isExpression, string expressionString, EnumType value, EnumType defaultValue)
			: base(isExpression, expressionString)
		{
			if (!isExpression)
			{
				this.m_value = value;
				return;
			}
			this.m_value = defaultValue;
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0006BC2C File Offset: 0x00069E2C
		public EnumType Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000D60 RID: 3424
		private EnumType m_value;
	}
}
