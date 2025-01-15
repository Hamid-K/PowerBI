using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000EC RID: 236
	internal sealed class ModelRuleProcessor
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x000276AC File Offset: 0x000258AC
		public ModelRuleProcessor(IModelGenEvents events, IEnumerable<ProcessingRule> rules, SemanticModel model, IDsvItemFilter filter, ExistingBindingContext bindingContext, bool traceVerbose)
		{
			if (events == null || rules == null || model == null || filter == null || bindingContext == null)
			{
				throw new InternalModelingException("One of the ModelRuleProcessor ctor parameters is null");
			}
			this.m_events = events;
			this.m_rules = new List<ProcessingRule>(rules);
			this.m_model = model;
			this.m_filter = filter;
			this.m_bindingContext = bindingContext;
			this.m_traceVerbose = traceVerbose;
			if (!this.m_rules.TrueForAll((ProcessingRule r) => r.Enabled))
			{
				throw new InternalModelingException("Only enabled rules should be passed in to ModelRuleProcessor");
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00027744 File Offset: 0x00025944
		public ModelGenResult ProcessRules()
		{
			this.m_finalPass = 1;
			this.m_totalColumns = this.GetTotalColumns();
			this.m_modelGenResult = new ModelGenResult();
			foreach (ProcessingRule processingRule in this.m_rules)
			{
				processingRule.SetContext(this.m_model, this.m_bindingContext);
			}
			this.m_currentPass = 1;
			while (this.m_currentPass <= this.m_finalPass && !this.m_events.CancelRequested)
			{
				this.m_events.RaiseLog(new ModelGenLogEventArgs(LogEntryType.Info, null, null, SR.ModelGen_PassHeader(this.m_currentPass)));
				this.ProcessRulesOnAllDsvItems();
				this.m_currentPass++;
			}
			if (!this.m_events.CancelRequested)
			{
				return this.m_modelGenResult;
			}
			return null;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002782C File Offset: 0x00025A2C
		private int GetTotalColumns()
		{
			int num = 0;
			foreach (DsvTable dsvTable in this.m_model.DataSourceView.Tables)
			{
				num += dsvTable.Columns.Count;
			}
			return num;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00027894 File Offset: 0x00025A94
		private void ProcessRulesOnAllDsvItems()
		{
			string text = SR.ModelGen_TableProgressLine1(this.m_currentPass);
			int num = 0;
			foreach (DsvTable dsvTable in this.m_model.DataSourceView.Tables)
			{
				if (this.m_filter.Evaluate(dsvTable))
				{
					this.ProcessRulesOnDsvItem(dsvTable);
				}
				foreach (DsvColumn dsvColumn in dsvTable.Columns)
				{
					string text2 = SR.ModelGen_TableProgressLine2(dsvTable.Name, dsvColumn.Name);
					this.m_events.RaiseProgress(new ProgressEventArgs(text, text2, ++num, this.m_totalColumns));
					if (this.m_filter.Evaluate(dsvColumn))
					{
						this.ProcessRulesOnDsvItem(dsvColumn);
					}
					if (this.m_events.CancelRequested)
					{
						return;
					}
				}
			}
			text = SR.ModelGen_RelationProgressLine1(this.m_currentPass);
			int num2 = 0;
			while (num2 < this.m_model.DataSourceView.Relations.Count && !this.m_events.CancelRequested)
			{
				DsvRelation dsvRelation = this.m_model.DataSourceView.Relations[num2];
				this.m_events.RaiseProgress(new ProgressEventArgs(text, dsvRelation.Name, num2 + 1, this.m_model.DataSourceView.Relations.Count));
				if (this.m_filter.Evaluate(dsvRelation))
				{
					this.ProcessRulesOnDsvItem(dsvRelation);
				}
				num2++;
			}
			foreach (ProcessingRule processingRule in this.m_rules)
			{
				IProcessingRuleCallback processingRuleCallback = processingRule as IProcessingRuleCallback;
				if (processingRuleCallback != null && processingRule.ProcessOnPass == this.m_currentPass)
				{
					processingRuleCallback.CompleteProcess();
				}
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00027AB0 File Offset: 0x00025CB0
		private void ProcessRulesOnDsvItem(DsvItem dsvItem)
		{
			foreach (ProcessingRule processingRule in this.m_rules)
			{
				if (processingRule.ProcessOnPass == this.m_currentPass)
				{
					if (processingRule.AppliesTo(dsvItem))
					{
						this.ProcessRuleOnDsvItem(processingRule, dsvItem);
					}
				}
				else
				{
					this.m_finalPass = Math.Max(this.m_finalPass, processingRule.ProcessOnPass);
				}
				if (this.m_events.CancelRequested)
				{
					break;
				}
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00027B44 File Offset: 0x00025D44
		private void ProcessRuleOnDsvItem(ProcessingRule r, DsvItem dsvItem)
		{
			LogEntryType logEntryType;
			IList<string> list;
			try
			{
				RuleProcessResult ruleProcessResult = r.Process(dsvItem);
				if (ruleProcessResult.Success && !ruleProcessResult.Skipped)
				{
					this.m_modelGenResult.AddResult(r.Name, ruleProcessResult);
				}
				if (ruleProcessResult.Success)
				{
					logEntryType = (ruleProcessResult.Skipped ? LogEntryType.Verbose : LogEntryType.Info);
				}
				else
				{
					logEntryType = LogEntryType.Warning;
				}
				list = ruleProcessResult.Messages;
			}
			catch (Exception ex)
			{
				logEntryType = LogEntryType.Error;
				list = new string[] { this.m_traceVerbose ? ex.ToString() : ex.Message };
			}
			foreach (string text in list)
			{
				this.m_events.RaiseLog(new ModelGenLogEventArgs(logEntryType, r.Name, dsvItem.Name, text));
			}
		}

		// Token: 0x040004FC RID: 1276
		private readonly IModelGenEvents m_events;

		// Token: 0x040004FD RID: 1277
		private readonly List<ProcessingRule> m_rules;

		// Token: 0x040004FE RID: 1278
		private readonly SemanticModel m_model;

		// Token: 0x040004FF RID: 1279
		private readonly IDsvItemFilter m_filter;

		// Token: 0x04000500 RID: 1280
		private readonly ExistingBindingContext m_bindingContext;

		// Token: 0x04000501 RID: 1281
		private readonly bool m_traceVerbose;

		// Token: 0x04000502 RID: 1282
		private int m_currentPass;

		// Token: 0x04000503 RID: 1283
		private int m_finalPass;

		// Token: 0x04000504 RID: 1284
		private int m_totalColumns;

		// Token: 0x04000505 RID: 1285
		private ModelGenResult m_modelGenResult;
	}
}
