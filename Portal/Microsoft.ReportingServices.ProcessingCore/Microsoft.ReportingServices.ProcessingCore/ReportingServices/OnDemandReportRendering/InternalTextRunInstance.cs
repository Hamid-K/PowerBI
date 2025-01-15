using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000322 RID: 802
	internal sealed class InternalTextRunInstance : TextRunInstance
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x00075106 File Offset: 0x00073306
		internal InternalTextRunInstance(InternalTextRun textRunDef)
			: base(textRunDef)
		{
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0007510F File Offset: 0x0007330F
		public override string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					this.m_uniqueName = InstancePathItem.GenerateUniqueNameString(this.TextRunDef.IDString, this.TextRunDef.InstancePath);
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x00075140 File Offset: 0x00073340
		public override string Value
		{
			get
			{
				if (!this.m_formattedValueEvaluated)
				{
					this.m_formattedValueEvaluated = true;
					this.EvaluateOriginalValue();
					if (this.m_originalValue.TypeCode == TypeCode.String)
					{
						this.m_formattedValue = this.m_originalValue.Value as string;
					}
					else
					{
						this.m_formattedValue = this.TextRunDef.FormatTextRunValue(this.m_originalValue, base.ReportElementDef.RenderingContext.OdpContext);
					}
				}
				return this.m_formattedValue;
			}
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000751B6 File Offset: 0x000733B6
		internal VariantResult GetOriginalValue()
		{
			this.EvaluateOriginalValue();
			return this.m_originalValue;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000751C4 File Offset: 0x000733C4
		private void EvaluateOriginalValue()
		{
			if (!this.m_originalValueEvaluated)
			{
				this.m_originalValueEvaluated = true;
				TextRun textRunDef = this.TextRunDef;
				ExpressionInfo value = textRunDef.Value;
				if (value != null)
				{
					if (value.IsExpression)
					{
						this.m_originalValue = textRunDef.EvaluateValue(this.ReportScopeInstance, base.ReportElementDef.RenderingContext.OdpContext);
						this.m_originalValueNeedsReset = true;
						return;
					}
					this.m_originalValue = default(VariantResult);
					this.m_originalValue.Value = value.Value;
					ReportRuntime.SetVariantType(ref this.m_originalValue);
				}
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0007524C File Offset: 0x0007344C
		public override object OriginalValue
		{
			get
			{
				this.EvaluateOriginalValue();
				if (!this.IsDateTimeOffsetOrTimeSpan())
				{
					return this.m_originalValue.Value;
				}
				if (this.Value != null)
				{
					return this.Value;
				}
				return ReportRuntime.ConvertToStringFallBack(this.m_originalValue.Value);
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x00075288 File Offset: 0x00073488
		public override string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					ExpressionInfo toolTip = this.TextRunDef.ToolTip;
					if (toolTip != null)
					{
						if (toolTip.IsExpression)
						{
							this.m_toolTip = this.TextRunDef.EvaluateToolTip(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
						}
						else
						{
							this.m_toolTip = toolTip.StringValue;
						}
					}
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000752F0 File Offset: 0x000734F0
		public override MarkupType MarkupType
		{
			get
			{
				if (this.m_markupType == null)
				{
					ExpressionInfo markupType = this.TextRunDef.MarkupType;
					if (markupType != null)
					{
						if (markupType.IsExpression)
						{
							this.m_markupType = new MarkupType?(RichTextHelpers.TranslateMarkupType(this.TextRunDef.EvaluateMarkupType(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext)));
						}
						else
						{
							this.m_markupType = new MarkupType?(RichTextHelpers.TranslateMarkupType(markupType.StringValue));
						}
					}
					else
					{
						this.m_markupType = new MarkupType?(MarkupType.None);
					}
				}
				return this.m_markupType.Value;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x00075383 File Offset: 0x00073583
		public override TypeCode TypeCode
		{
			get
			{
				this.EvaluateOriginalValue();
				if (this.IsDateTimeOffsetOrTimeSpan())
				{
					return TypeCode.String;
				}
				return this.m_originalValue.TypeCode;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000753A1 File Offset: 0x000735A1
		internal TextRun TextRunDef
		{
			get
			{
				return ((InternalTextRun)this.m_reportElementDef).TextRunDef;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000753B3 File Offset: 0x000735B3
		public override bool IsCompiled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x000753B6 File Offset: 0x000735B6
		public override bool ProcessedWithError
		{
			get
			{
				this.EvaluateOriginalValue();
				return this.m_originalValue.ErrorOccurred;
			}
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000753C9 File Offset: 0x000735C9
		private bool IsDateTimeOffsetOrTimeSpan()
		{
			return this.m_originalValue.TypeCode == TypeCode.Object && (this.m_originalValue.Value is DateTimeOffset || this.m_originalValue.Value is TimeSpan);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00075400 File Offset: 0x00073600
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_formattedValueEvaluated = false;
			this.m_formattedValue = null;
			this.m_toolTip = null;
			this.m_markupType = null;
			if (this.m_originalValueNeedsReset)
			{
				this.m_originalValueNeedsReset = false;
				this.m_originalValueEvaluated = false;
			}
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00075440 File Offset: 0x00073640
		internal List<string> GetFieldsUsedInValueExpression()
		{
			List<string> list = null;
			ExpressionInfo value = this.TextRunDef.Value;
			if (value != null && value.IsExpression)
			{
				list = this.TextRunDef.GetFieldsUsedInValueExpression(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
			}
			return list;
		}

		// Token: 0x04000F69 RID: 3945
		private string m_toolTip;

		// Token: 0x04000F6A RID: 3946
		private MarkupType? m_markupType;

		// Token: 0x04000F6B RID: 3947
		private bool m_formattedValueEvaluated;

		// Token: 0x04000F6C RID: 3948
		private string m_formattedValue;

		// Token: 0x04000F6D RID: 3949
		private VariantResult m_originalValue;

		// Token: 0x04000F6E RID: 3950
		private bool m_originalValueEvaluated;

		// Token: 0x04000F6F RID: 3951
		private bool m_originalValueNeedsReset;
	}
}
