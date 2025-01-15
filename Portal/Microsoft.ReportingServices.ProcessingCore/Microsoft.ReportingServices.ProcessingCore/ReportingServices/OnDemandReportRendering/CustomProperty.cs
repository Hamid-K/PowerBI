using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B4 RID: 692
	public sealed class CustomProperty
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x0006ACEE File Offset: 0x00068EEE
		internal CustomProperty(ReportElement reportElementOwner, RenderingContext renderingContext, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo nameExpr, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpr, string name, object value, TypeCode typeCode)
		{
			this.m_reportElementOwner = reportElementOwner;
			this.Init(nameExpr, valueExpr, name, value, typeCode);
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0006AD14 File Offset: 0x00068F14
		internal CustomProperty(RenderingContext renderingContext, Microsoft.ReportingServices.ReportProcessing.ExpressionInfo nameExpr, Microsoft.ReportingServices.ReportProcessing.ExpressionInfo valueExpr, string name, object value, TypeCode typeCode)
		{
			this.m_name = new ReportStringProperty(nameExpr);
			this.m_value = new ReportVariantProperty(valueExpr);
			if (nameExpr.IsExpression || valueExpr.IsExpression)
			{
				this.m_instance = new CustomPropertyInstance(this, name, value, typeCode);
			}
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0006AD68 File Offset: 0x00068F68
		public ReportStringProperty Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0006AD70 File Offset: 0x00068F70
		public ReportVariantProperty Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0006AD78 File Offset: 0x00068F78
		internal ReportElement ReportElementOwner
		{
			get
			{
				return this.m_reportElementOwner;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0006AD80 File Offset: 0x00068F80
		public CustomPropertyInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006AD98 File Offset: 0x00068F98
		private void Init(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo nameExpr, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpr, string name, object value, TypeCode typeCode)
		{
			this.m_name = new ReportStringProperty(nameExpr);
			this.m_value = new ReportVariantProperty(valueExpr);
			if (nameExpr.IsExpression || valueExpr.IsExpression)
			{
				this.m_instance = new CustomPropertyInstance(this, name, value, typeCode);
				return;
			}
			this.m_instance = null;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006ADE6 File Offset: 0x00068FE6
		internal void Update(string name, object value, TypeCode typeCode)
		{
			if (this.m_instance != null)
			{
				this.m_instance.Update(name, value, typeCode);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0006AE00 File Offset: 0x00069000
		internal void ConstructCustomPropertyDefinition(Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValueDef)
		{
			Global.Tracer.Assert(this.m_reportElementOwner != null && this.m_instance != null, "m_reportElementOwner != null && m_instance != null");
			if (this.m_instance.Name != null)
			{
				dataValueDef.Name = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(this.m_instance.Name);
			}
			else
			{
				dataValueDef.Name = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			if (this.m_instance.Value != null)
			{
				dataValueDef.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression((string)this.m_instance.Value);
			}
			else
			{
				dataValueDef.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.Init(dataValueDef.Name, dataValueDef.Value, this.m_instance.Name, this.m_instance.Value, this.m_instance.TypeCode);
		}

		// Token: 0x04000D2E RID: 3374
		private ReportStringProperty m_name;

		// Token: 0x04000D2F RID: 3375
		private ReportVariantProperty m_value;

		// Token: 0x04000D30 RID: 3376
		private CustomPropertyInstance m_instance;

		// Token: 0x04000D31 RID: 3377
		private RenderingContext m_renderingContext;

		// Token: 0x04000D32 RID: 3378
		private ReportElement m_reportElementOwner;
	}
}
