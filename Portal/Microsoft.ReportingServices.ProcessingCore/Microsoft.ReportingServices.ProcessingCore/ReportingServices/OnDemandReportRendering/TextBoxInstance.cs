using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200031D RID: 797
	public sealed class TextBoxInstance : ReportItemInstance
	{
		// Token: 0x06001D91 RID: 7569 RVA: 0x00074496 File Offset: 0x00072696
		internal TextBoxInstance(Microsoft.ReportingServices.OnDemandReportRendering.TextBox reportItemDef)
			: base(reportItemDef)
		{
			this.m_textBoxDef = reportItemDef;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000744A6 File Offset: 0x000726A6
		public void AddToCurrentPage()
		{
			this.m_reportElementDef.RenderingContext.AddToCurrentPage(this.m_textBoxDef.Name, this.OriginalValue);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000744C9 File Offset: 0x000726C9
		public void RegisterToggleSender()
		{
			if (!this.m_reportElementDef.IsOldSnapshot && this.IsToggleParent)
			{
				this.m_reportElementDef.RenderingContext.AddValidToggleSender(this.UniqueName);
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x000744F6 File Offset: 0x000726F6
		public ParagraphInstanceCollection ParagraphInstances
		{
			get
			{
				if (this.m_paragraphInstances == null)
				{
					this.m_paragraphInstances = new ParagraphInstanceCollection(this.m_textBoxDef);
				}
				return this.m_paragraphInstances;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00074518 File Offset: 0x00072718
		public string Value
		{
			get
			{
				if (!this.m_formattedValueEvaluated)
				{
					this.m_formattedValueEvaluated = true;
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_formattedValue = ((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).Value;
					}
					else if (this.m_textBoxDef.IsSimple)
					{
						this.m_formattedValue = this.m_textBoxDef.Paragraphs[0].TextRuns[0].Instance.Value;
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder();
						bool flag = false;
						bool flag2 = true;
						foreach (ParagraphInstance paragraphInstance in this.ParagraphInstances)
						{
							if (!flag2)
							{
								flag = true;
								stringBuilder.Append(Environment.NewLine);
							}
							else
							{
								flag2 = false;
							}
							foreach (TextRunInstance textRunInstance in paragraphInstance.TextRunInstances)
							{
								string value = textRunInstance.Value;
								if (value != null)
								{
									flag = true;
									stringBuilder.Append(value);
								}
							}
						}
						if (flag)
						{
							this.m_formattedValue = stringBuilder.ToString();
						}
					}
				}
				return this.m_formattedValue;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0007465C File Offset: 0x0007285C
		public object OriginalValue
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).OriginalValue;
				}
				this.EvaluateOriginalValue();
				return this.m_originalValue.Value;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x00074694 File Offset: 0x00072894
		public bool IsToggleParent
		{
			get
			{
				if (this.m_isToggleParent == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_isToggleParent = new bool?(((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).IsToggleParent);
					}
					else
					{
						this.m_isToggleParent = new bool?(this.m_textBoxDef.TexBoxDef.EvaluateIsToggle(this.ReportScopeInstance, base.RenderingContext.OdpContext));
					}
				}
				return this.m_isToggleParent.Value;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x00074714 File Offset: 0x00072914
		public bool ToggleState
		{
			get
			{
				if (!this.m_toggleStateEvaluated)
				{
					this.m_toggleStateEvaluated = true;
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_toggleState = ((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).ToggleState;
					}
					else if (this.IsToggleParent)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.TextBox texBoxDef = this.m_textBoxDef.TexBoxDef;
						this.m_toggleState = texBoxDef.EvaluateInitialToggleState(this.ReportScopeInstance, base.RenderingContext.OdpContext);
						if (base.RenderingContext.IsSenderToggled(this.UniqueName))
						{
							this.m_toggleState = !this.m_toggleState;
						}
					}
					else
					{
						this.m_toggleState = false;
					}
				}
				return this.m_toggleState;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x000747BF File Offset: 0x000729BF
		public SortOptions SortState
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).SortState;
				}
				return base.RenderingContext.GetSortState(this.UniqueName);
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x000747F8 File Offset: 0x000729F8
		public bool Duplicate
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).Duplicate;
				}
				if (this.m_duplicate == null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.TextBox texBoxDef = this.m_textBoxDef.TexBoxDef;
					if (texBoxDef.HideDuplicates == null)
					{
						return false;
					}
					this.EvaluateOriginalValue();
					this.m_duplicate = new bool?(texBoxDef.CalculateDuplicates(this.m_originalValue, base.RenderingContext.OdpContext));
				}
				return this.m_duplicate.Value;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00074880 File Offset: 0x00072A80
		public TypeCode TypeCode
		{
			get
			{
				if (this.m_typeCode == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						object originalValue = ((Microsoft.ReportingServices.ReportRendering.TextBox)this.m_reportElementDef.RenderReportItem).OriginalValue;
						if (originalValue != null)
						{
							Type type = originalValue.GetType();
							this.m_typeCode = new TypeCode?(Type.GetTypeCode(type));
						}
						else
						{
							this.m_typeCode = new TypeCode?(TypeCode.Empty);
						}
					}
					else
					{
						this.EvaluateOriginalValue();
					}
				}
				return this.m_typeCode.Value;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x000748FC File Offset: 0x00072AFC
		public bool ProcessedWithError
		{
			get
			{
				return (this.m_reportElementDef.IsOldSnapshot || this.m_textBoxDef.TexBoxDef.IsSimple) && this.m_textBoxDef.Paragraphs[0].TextRuns[0].Instance.ProcessedWithError;
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00074950 File Offset: 0x00072B50
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			if (this.m_reportElementDef.IsOldSnapshot)
			{
				this.m_typeCode = null;
			}
			else
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.TextBox texBoxDef = this.m_textBoxDef.TexBoxDef;
				if (texBoxDef.HasExpressionBasedValue)
				{
					texBoxDef.ResetTextBoxImpl(base.RenderingContext.OdpContext);
					this.m_originalValueEvaluated = false;
					if (texBoxDef.IsSimple)
					{
						this.m_typeCode = null;
					}
					else
					{
						this.m_typeCode = new TypeCode?(TypeCode.String);
					}
				}
			}
			this.m_formattedValueEvaluated = false;
			this.m_formattedValue = null;
			this.m_toggleStateEvaluated = false;
			this.m_duplicate = null;
			this.m_isToggleParent = null;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000749FC File Offset: 0x00072BFC
		private void EvaluateOriginalValue()
		{
			if (!this.m_originalValueEvaluated)
			{
				this.m_originalValueEvaluated = true;
				Microsoft.ReportingServices.ReportIntermediateFormat.TextBox texBoxDef = this.m_textBoxDef.TexBoxDef;
				if (texBoxDef.HasValue)
				{
					OnDemandProcessingContext odpContext = base.RenderingContext.OdpContext;
					this.m_originalValue = default(VariantResult);
					if (texBoxDef.IsSimple)
					{
						InternalTextRunInstance internalTextRunInstance = (InternalTextRunInstance)this.m_textBoxDef.Paragraphs[0].TextRuns[0].Instance;
						this.m_originalValue.Value = internalTextRunInstance.OriginalValue;
						this.m_originalValue.ErrorOccurred = internalTextRunInstance.ProcessedWithError;
						this.m_typeCode = new TypeCode?(internalTextRunInstance.TypeCode);
						this.m_originalValue.TypeCode = this.m_typeCode.Value;
						return;
					}
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = false;
					bool flag2 = true;
					foreach (ParagraphInstance paragraphInstance in this.ParagraphInstances)
					{
						if (!flag2)
						{
							flag = true;
							stringBuilder.Append(Environment.NewLine);
						}
						else
						{
							flag2 = false;
						}
						foreach (TextRunInstance textRunInstance in paragraphInstance.TextRunInstances)
						{
							object originalValue = textRunInstance.OriginalValue;
							if (originalValue != null)
							{
								flag = true;
								stringBuilder.Append(originalValue);
							}
						}
					}
					if (flag)
					{
						this.m_originalValue.Value = stringBuilder.ToString();
						this.m_originalValue.TypeCode = TypeCode.String;
						this.m_typeCode = new TypeCode?(TypeCode.String);
						return;
					}
				}
				else
				{
					this.m_typeCode = new TypeCode?(TypeCode.Empty);
				}
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00074BAC File Offset: 0x00072DAC
		internal List<string> GetFieldsUsedInValueExpression()
		{
			List<string> list = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.TextBox texBoxDef = this.m_textBoxDef.TexBoxDef;
			if (texBoxDef.HasExpressionBasedValue)
			{
				list = texBoxDef.GetFieldsUsedInValueExpression(this.ReportScopeInstance, base.RenderingContext.OdpContext);
			}
			return list;
		}

		// Token: 0x04000F54 RID: 3924
		private ParagraphInstanceCollection m_paragraphInstances;

		// Token: 0x04000F55 RID: 3925
		private bool m_formattedValueEvaluated;

		// Token: 0x04000F56 RID: 3926
		private string m_formattedValue;

		// Token: 0x04000F57 RID: 3927
		private bool m_originalValueEvaluated;

		// Token: 0x04000F58 RID: 3928
		private VariantResult m_originalValue;

		// Token: 0x04000F59 RID: 3929
		private bool m_toggleState;

		// Token: 0x04000F5A RID: 3930
		private bool m_toggleStateEvaluated;

		// Token: 0x04000F5B RID: 3931
		private bool? m_duplicate;

		// Token: 0x04000F5C RID: 3932
		private TypeCode? m_typeCode;

		// Token: 0x04000F5D RID: 3933
		private Microsoft.ReportingServices.OnDemandReportRendering.TextBox m_textBoxDef;

		// Token: 0x04000F5E RID: 3934
		private bool? m_isToggleParent;
	}
}
