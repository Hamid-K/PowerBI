using System;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000336 RID: 822
	internal sealed class TagInstance
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x00076CAA File Offset: 0x00074EAA
		internal TagInstance(Tag tagDef)
		{
			this.m_tagDef = tagDef;
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x00076CB9 File Offset: 0x00074EB9
		public TypeCode DataType
		{
			get
			{
				this.EvaluateTagValue();
				return this.m_tag.Value.TypeCode;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x00076CD1 File Offset: 0x00074ED1
		public object Value
		{
			get
			{
				this.EvaluateTagValue();
				return this.m_tag.Value.Value;
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00076CEC File Offset: 0x00074EEC
		private void EvaluateTagValue()
		{
			if (this.m_tag == null)
			{
				ExpressionInfo expression = this.m_tagDef.Expression;
				if (expression != null)
				{
					if (expression.IsExpression)
					{
						Image image = this.m_tagDef.Image;
						this.m_tag = new VariantResult?(image.ImageDef.EvaluateTagExpression(expression, image.Instance.ReportScopeInstance, image.RenderingContext.OdpContext));
						return;
					}
					VariantResult variantResult = new VariantResult(false, expression.Value);
					ReportRuntime.SetVariantType(ref variantResult);
					this.m_tag = new VariantResult?(variantResult);
					return;
				}
				else
				{
					this.m_tag = new VariantResult?(new VariantResult(false, null));
				}
			}
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00076D8F File Offset: 0x00074F8F
		internal void ResetInstanceCache()
		{
			this.m_tag = null;
		}

		// Token: 0x04000FAD RID: 4013
		private readonly Tag m_tagDef;

		// Token: 0x04000FAE RID: 4014
		private VariantResult? m_tag;
	}
}
