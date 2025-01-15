using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007C5 RID: 1989
	internal sealed class TextRunImpl : Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.TextRun
	{
		// Token: 0x06007083 RID: 28803 RVA: 0x001D4D82 File Offset: 0x001D2F82
		internal TextRunImpl(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBoxDef, Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRunDef, ReportRuntime reportRT, IErrorContext iErrorContext, IScope scope)
		{
			this.m_textBoxDef = textBoxDef;
			this.m_textRunDef = textRunDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = iErrorContext;
			this.m_scope = scope;
		}

		// Token: 0x1700265A RID: 9818
		// (get) Token: 0x06007084 RID: 28804 RVA: 0x001D4DAF File Offset: 0x001D2FAF
		public override object Value
		{
			get
			{
				this.GetResult(null);
				return this.m_result.Value;
			}
		}

		// Token: 0x1700265B RID: 9819
		// (get) Token: 0x06007085 RID: 28805 RVA: 0x001D4DC4 File Offset: 0x001D2FC4
		internal Microsoft.ReportingServices.ReportIntermediateFormat.TextRun TextRunDef
		{
			get
			{
				return this.m_textRunDef;
			}
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x001D4DCC File Offset: 0x001D2FCC
		internal VariantResult GetResult(IReportScopeInstance romInstance)
		{
			if (!this.m_isValueReady)
			{
				if (this.m_isVisited)
				{
					this.m_iErrorContext.Register(ProcessingErrorCode.rsCyclicExpression, Severity.Warning, this.m_textRunDef.ObjectType, this.m_textRunDef.Name, "Value", Array.Empty<string>());
					throw new ReportProcessingException_InvalidOperationException();
				}
				this.m_isVisited = true;
				ObjectType objectType = this.m_reportRT.ObjectType;
				string objectName = this.m_reportRT.ObjectName;
				string propertyName = this.m_reportRT.PropertyName;
				IScope currentScope = this.m_reportRT.CurrentScope;
				this.m_reportRT.CurrentScope = this.m_scope;
				OnDemandProcessingContext odpContext = this.m_reportRT.ReportObjectModel.OdpContext;
				ObjectModelImpl reportObjectModel = this.m_reportRT.ReportObjectModel;
				try
				{
					odpContext.SetupContext(this.m_textBoxDef, romInstance);
					bool flag = (this.m_textRunDef.Action != null && this.m_textRunDef.Action.TrackFieldsUsedInValueExpression) || (this.m_textBoxDef != null && this.m_textBoxDef.Action != null && this.m_textBoxDef.Action.TrackFieldsUsedInValueExpression);
					if (flag)
					{
						reportObjectModel.ResetFieldsUsedInExpression();
					}
					this.m_result = this.m_reportRT.EvaluateTextRunValueExpression(this.m_textRunDef);
					if (flag)
					{
						this.m_fieldsUsedInValueExpression = new List<string>();
						reportObjectModel.AddFieldsUsedInExpression(this.m_fieldsUsedInValueExpression);
					}
				}
				finally
				{
					this.m_reportRT.CurrentScope = currentScope;
					this.m_reportRT.ObjectType = objectType;
					this.m_reportRT.ObjectName = objectName;
					this.m_reportRT.PropertyName = propertyName;
					this.m_isVisited = false;
					this.m_isValueReady = true;
				}
			}
			return this.m_result;
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x001D4F74 File Offset: 0x001D3174
		internal List<string> GetFieldsUsedInValueExpression(IReportScopeInstance romInstance)
		{
			if (!this.m_isValueReady)
			{
				this.GetResult(romInstance);
			}
			return this.m_fieldsUsedInValueExpression;
		}

		// Token: 0x06007088 RID: 28808 RVA: 0x001D4F8C File Offset: 0x001D318C
		internal void MergeFieldsUsedInValueExpression(Dictionary<string, bool> usedFields)
		{
			if (this.m_fieldsUsedInValueExpression == null)
			{
				return;
			}
			for (int i = 0; i < this.m_fieldsUsedInValueExpression.Count; i++)
			{
				string text = this.m_fieldsUsedInValueExpression[i];
				if (text != null)
				{
					usedFields[text] = true;
				}
			}
		}

		// Token: 0x06007089 RID: 28809 RVA: 0x001D4FD0 File Offset: 0x001D31D0
		internal void Reset()
		{
			if (this.m_isValueReady && this.m_textRunDef.Value.IsExpression)
			{
				this.m_isValueReady = false;
			}
		}

		// Token: 0x04003A25 RID: 14885
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextBox m_textBoxDef;

		// Token: 0x04003A26 RID: 14886
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextRun m_textRunDef;

		// Token: 0x04003A27 RID: 14887
		private ReportRuntime m_reportRT;

		// Token: 0x04003A28 RID: 14888
		private IErrorContext m_iErrorContext;

		// Token: 0x04003A29 RID: 14889
		private VariantResult m_result;

		// Token: 0x04003A2A RID: 14890
		private bool m_isValueReady;

		// Token: 0x04003A2B RID: 14891
		private bool m_isVisited;

		// Token: 0x04003A2C RID: 14892
		private IScope m_scope;

		// Token: 0x04003A2D RID: 14893
		private List<string> m_fieldsUsedInValueExpression;
	}
}
