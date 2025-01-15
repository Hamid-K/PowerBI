using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B8 RID: 1976
	internal sealed class TextBoxImpl : ReportItemImpl
	{
		// Token: 0x0600702C RID: 28716 RVA: 0x001D3417 File Offset: 0x001D1617
		internal TextBoxImpl(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox itemDef, ReportRuntime reportRT, IErrorContext iErrorContext)
			: base(itemDef, reportRT, iErrorContext)
		{
			this.m_textBox = itemDef;
			this.m_paragraphs = new ParagraphsImpl(this.m_textBox, this.m_reportRT, this.m_iErrorContext, this.m_scope);
		}

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x0600702D RID: 28717 RVA: 0x001D344C File Offset: 0x001D164C
		public override object Value
		{
			get
			{
				this.GetResult(null, true);
				return this.m_result.Value;
			}
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x0600702E RID: 28718 RVA: 0x001D3462 File Offset: 0x001D1662
		internal Paragraphs Paragraphs
		{
			get
			{
				return this.m_paragraphs;
			}
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x001D346C File Offset: 0x001D166C
		private bool IsTextboxInScope()
		{
			OnDemandProcessingContext odpContext = this.m_reportRT.ReportObjectModel.OdpContext;
			IRIFReportScope irifreportScope;
			if (odpContext.IsTablixProcessingMode)
			{
				irifreportScope = odpContext.LastTablixProcessingReportScope;
				if (irifreportScope == null)
				{
					irifreportScope = odpContext.ReportDefinition;
				}
			}
			else if (odpContext.IsTopLevelSubReportProcessing)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport = odpContext.LastRIFObject as Microsoft.ReportingServices.ReportIntermediateFormat.SubReport;
				Global.Tracer.Assert(subReport != null, "Missing reference to subreport object");
				irifreportScope = subReport.GetContainingSection(odpContext);
			}
			else
			{
				IReportScope currentReportScope = odpContext.CurrentReportScope;
				if (currentReportScope != null)
				{
					irifreportScope = currentReportScope.RIFReportScope;
				}
				else
				{
					irifreportScope = odpContext.ReportDefinition;
				}
			}
			return irifreportScope != null && irifreportScope.TextboxInScope(this.m_textBox.SequenceID);
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x001D350C File Offset: 0x001D170C
		internal VariantResult GetResult(IReportScopeInstance romInstance, bool calledFromValue)
		{
			if (calledFromValue && !this.IsTextboxInScope())
			{
				this.m_result = default(VariantResult);
			}
			else if (!this.m_isValueReady)
			{
				if (this.m_isVisited)
				{
					this.m_iErrorContext.Register(ProcessingErrorCode.rsCyclicExpression, Severity.Warning, this.m_textBox.ObjectType, this.m_textBox.Name, "Value", Array.Empty<string>());
					throw new ReportProcessingException_InvalidOperationException();
				}
				this.m_isVisited = true;
				ObjectModelImpl reportObjectModel = this.m_reportRT.ReportObjectModel;
				OnDemandProcessingContext odpContext = this.m_reportRT.ReportObjectModel.OdpContext;
				bool contextUpdated = this.m_reportRT.ContextUpdated;
				IInstancePath instancePath = null;
				this.m_reportRT.ContextUpdated = false;
				if (odpContext.IsTablixProcessingMode || calledFromValue)
				{
					instancePath = odpContext.LastRIFObject;
				}
				bool flag = this.m_textBox.Action != null && this.m_textBox.Action.TrackFieldsUsedInValueExpression;
				Dictionary<string, bool> dictionary = null;
				if (flag)
				{
					dictionary = new Dictionary<string, bool>();
				}
				try
				{
					bool flag2 = false;
					if (this.m_paragraphs.Count == 1)
					{
						TextRunsImpl textRunsImpl = (TextRunsImpl)this.m_paragraphs[0].TextRuns;
						if (textRunsImpl.Count == 1)
						{
							flag2 = true;
							TextRunImpl textRunImpl = (TextRunImpl)textRunsImpl[0];
							this.m_result = textRunImpl.GetResult(romInstance);
							if (flag)
							{
								textRunImpl.MergeFieldsUsedInValueExpression(dictionary);
							}
						}
					}
					if (!flag2)
					{
						bool flag3 = false;
						this.m_result = default(VariantResult);
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < this.m_paragraphs.Count; i++)
						{
							if (i > 0)
							{
								flag3 = true;
								stringBuilder.Append(Environment.NewLine);
							}
							TextRunsImpl textRunsImpl2 = (TextRunsImpl)this.m_paragraphs[i].TextRuns;
							for (int j = 0; j < textRunsImpl2.Count; j++)
							{
								TextRunImpl textRunImpl2 = (TextRunImpl)textRunsImpl2[j];
								VariantResult result = textRunImpl2.GetResult(romInstance);
								if (result.Value != null)
								{
									if (result.TypeCode == TypeCode.Object && (result.Value is TimeSpan || result.Value is DateTimeOffset))
									{
										string text = textRunImpl2.TextRunDef.FormatTextRunValue(result, odpContext);
										if (text != null)
										{
											result.Value = text;
										}
										else
										{
											result.Value = ReportRuntime.ConvertToStringFallBack(result.Value);
										}
									}
									flag3 = true;
									stringBuilder.Append(result.Value);
								}
								if (flag)
								{
									textRunImpl2.MergeFieldsUsedInValueExpression(dictionary);
								}
							}
						}
						if (flag3)
						{
							this.m_result.Value = stringBuilder.ToString();
							this.m_result.TypeCode = TypeCode.String;
						}
					}
					if (flag)
					{
						this.m_fieldsUsedInValueExpression = new List<string>();
						foreach (string text2 in dictionary.Keys)
						{
							this.m_fieldsUsedInValueExpression.Add(text2);
						}
					}
				}
				finally
				{
					odpContext.RestoreContext(instancePath);
					this.m_reportRT.ContextUpdated = contextUpdated;
					this.m_isVisited = false;
					this.m_isValueReady = true;
				}
			}
			return this.m_result;
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x001D384C File Offset: 0x001D1A4C
		internal List<string> GetFieldsUsedInValueExpression(IReportScopeInstance romInstance)
		{
			if (!this.m_isValueReady)
			{
				this.GetResult(romInstance, true);
			}
			return this.m_fieldsUsedInValueExpression;
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x001D3865 File Offset: 0x001D1A65
		internal override void Reset()
		{
			if (this.m_textBox.HasExpressionBasedValue)
			{
				this.m_isValueReady = false;
				this.m_paragraphs.Reset();
			}
		}

		// Token: 0x06007033 RID: 28723 RVA: 0x001D3886 File Offset: 0x001D1A86
		internal override void Reset(VariantResult value)
		{
			this.SetResult(value);
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x001D388F File Offset: 0x001D1A8F
		internal void SetResult(VariantResult result)
		{
			this.m_result = result;
			this.m_isValueReady = true;
		}

		// Token: 0x040039F5 RID: 14837
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextBox m_textBox;

		// Token: 0x040039F6 RID: 14838
		private VariantResult m_result;

		// Token: 0x040039F7 RID: 14839
		private bool m_isValueReady;

		// Token: 0x040039F8 RID: 14840
		private bool m_isVisited;

		// Token: 0x040039F9 RID: 14841
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x040039FA RID: 14842
		private ParagraphsImpl m_paragraphs;
	}
}
