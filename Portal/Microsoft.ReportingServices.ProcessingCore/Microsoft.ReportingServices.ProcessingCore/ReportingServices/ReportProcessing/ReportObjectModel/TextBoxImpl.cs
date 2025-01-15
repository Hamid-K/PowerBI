using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000796 RID: 1942
	internal sealed class TextBoxImpl : ReportItemImpl
	{
		// Token: 0x06006C46 RID: 27718 RVA: 0x001B71D7 File Offset: 0x001B53D7
		internal TextBoxImpl(TextBox itemDef, ReportRuntime reportRT, IErrorContext iErrorContext)
			: base(itemDef, reportRT, iErrorContext)
		{
			this.m_textBox = itemDef;
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x06006C47 RID: 27719 RVA: 0x001B71E9 File Offset: 0x001B53E9
		public override object Value
		{
			get
			{
				this.GetResult();
				return this.m_result.Value;
			}
		}

		// Token: 0x06006C48 RID: 27720 RVA: 0x001B71FD File Offset: 0x001B53FD
		internal void SetResult(VariantResult result)
		{
			this.m_result = result;
			this.m_isValueReady = true;
		}

		// Token: 0x06006C49 RID: 27721 RVA: 0x001B7210 File Offset: 0x001B5410
		internal VariantResult GetResult()
		{
			if (!this.m_isValueReady)
			{
				if (this.m_isVisited)
				{
					this.m_iErrorContext.Register(ProcessingErrorCode.rsCyclicExpression, Severity.Warning, this.m_textBox.ObjectType, this.m_textBox.Name, "Value", Array.Empty<string>());
					throw new ReportProcessingException_InvalidOperationException();
				}
				this.m_isVisited = true;
				ObjectType objectType = this.m_reportRT.ObjectType;
				string objectName = this.m_reportRT.ObjectName;
				string propertyName = this.m_reportRT.PropertyName;
				ReportProcessing.IScope currentScope = this.m_reportRT.CurrentScope;
				this.m_reportRT.CurrentScope = this.m_scope;
				this.m_result = this.m_reportRT.EvaluateTextBoxValueExpression(this.m_textBox);
				this.m_reportRT.CurrentScope = currentScope;
				this.m_reportRT.ObjectType = objectType;
				this.m_reportRT.ObjectName = objectName;
				this.m_reportRT.PropertyName = propertyName;
				this.m_isVisited = false;
				this.m_isValueReady = true;
			}
			return this.m_result;
		}

		// Token: 0x06006C4A RID: 27722 RVA: 0x001B730A File Offset: 0x001B550A
		internal void Reset()
		{
			this.m_isValueReady = false;
		}

		// Token: 0x04003668 RID: 13928
		private TextBox m_textBox;

		// Token: 0x04003669 RID: 13929
		private VariantResult m_result;

		// Token: 0x0400366A RID: 13930
		private bool m_isValueReady;

		// Token: 0x0400366B RID: 13931
		private bool m_isVisited;
	}
}
