using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200078F RID: 1935
	internal sealed class CalculatedFieldWrapperImpl : CalculatedFieldWrapper
	{
		// Token: 0x06006C19 RID: 27673 RVA: 0x001B6B0D File Offset: 0x001B4D0D
		internal CalculatedFieldWrapperImpl(Field fieldDef, ReportRuntime reportRT)
		{
			this.m_fieldDef = fieldDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = reportRT;
		}

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x06006C1A RID: 27674 RVA: 0x001B6B2C File Offset: 0x001B4D2C
		public override object Value
		{
			get
			{
				if (!this.m_isValueReady)
				{
					if (this.m_isVisited)
					{
						this.m_iErrorContext.Register(ProcessingErrorCode.rsCyclicExpression, Severity.Warning, ObjectType.Field, this.m_fieldDef.Name, "Value", Array.Empty<string>());
						throw new ReportProcessingException_InvalidOperationException();
					}
					this.m_isVisited = true;
					this.m_value = this.m_reportRT.EvaluateFieldValueExpression(this.m_fieldDef);
					this.m_isVisited = false;
					this.m_isValueReady = true;
				}
				return this.m_value;
			}
		}

		// Token: 0x04003640 RID: 13888
		private Field m_fieldDef;

		// Token: 0x04003641 RID: 13889
		private object m_value;

		// Token: 0x04003642 RID: 13890
		private bool m_isValueReady;

		// Token: 0x04003643 RID: 13891
		private bool m_isVisited;

		// Token: 0x04003644 RID: 13892
		private ReportRuntime m_reportRT;

		// Token: 0x04003645 RID: 13893
		private IErrorContext m_iErrorContext;
	}
}
