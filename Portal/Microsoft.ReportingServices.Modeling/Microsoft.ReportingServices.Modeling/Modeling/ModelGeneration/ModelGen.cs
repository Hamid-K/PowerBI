using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E9 RID: 233
	public sealed class ModelGen : Component, IModelGenEvents, ICancelEvent
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00026FE1 File Offset: 0x000251E1
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00026FE9 File Offset: 0x000251E9
		public RuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
			set
			{
				this.CheckNotRunning();
				this.m_ruleSet = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00026FF8 File Offset: 0x000251F8
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00027000 File Offset: 0x00025200
		public SemanticModel Model
		{
			get
			{
				return this.m_model;
			}
			set
			{
				this.CheckNotRunning();
				this.m_model = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002700F File Offset: 0x0002520F
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00027017 File Offset: 0x00025217
		public IDsvStatisticsProvider DsvStatisticsProvider
		{
			get
			{
				return this.m_dsvStatisticsProvider;
			}
			set
			{
				this.CheckNotRunning();
				this.m_dsvStatisticsProvider = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00027026 File Offset: 0x00025226
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0002702E File Offset: 0x0002522E
		public bool OverwriteDsvStatistics
		{
			get
			{
				return this.m_overwriteDsvStatistics;
			}
			set
			{
				this.CheckNotRunning();
				this.m_overwriteDsvStatistics = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0002703D File Offset: 0x0002523D
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00027045 File Offset: 0x00025245
		public bool TraceVerbose
		{
			get
			{
				return this.m_traceVerbose;
			}
			set
			{
				this.CheckNotRunning();
				this.m_traceVerbose = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00027054 File Offset: 0x00025254
		public bool IsRunning
		{
			get
			{
				return this.m_running;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000BFE RID: 3070 RVA: 0x00027060 File Offset: 0x00025260
		// (remove) Token: 0x06000BFF RID: 3071 RVA: 0x00027098 File Offset: 0x00025298
		public event EventHandler<ProgressEventArgs> Progress;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000C00 RID: 3072 RVA: 0x000270D0 File Offset: 0x000252D0
		// (remove) Token: 0x06000C01 RID: 3073 RVA: 0x00027108 File Offset: 0x00025308
		public event EventHandler<ModelGenLogEventArgs> Log;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000C02 RID: 3074 RVA: 0x00027140 File Offset: 0x00025340
		// (remove) Token: 0x06000C03 RID: 3075 RVA: 0x00027178 File Offset: 0x00025378
		public event EventHandler<ModelGenCompleteEventArgs> Complete;

		// Token: 0x06000C04 RID: 3076 RVA: 0x000271AD File Offset: 0x000253AD
		public void SetScope(IEnumerable<ModelEntity> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException();
			}
			this.CheckNotRunning();
			this.m_scopeItems = new Bag<ModelEntity>(entities);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000271CA File Offset: 0x000253CA
		public void SetScope(IEnumerable<ModelAttribute> attributes)
		{
			if (attributes == null)
			{
				throw new ArgumentNullException();
			}
			this.CheckNotRunning();
			this.m_scopeItems = new Bag<ModelAttribute>(attributes);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000271E8 File Offset: 0x000253E8
		public ModelGenResult Run()
		{
			if (this.m_ruleSet == null || this.m_model == null || this.m_model.DataSourceView == null)
			{
				throw new InvalidOperationException();
			}
			this.CheckNotRunning();
			ModelGenResult modelGenResult = null;
			try
			{
				this.m_running = true;
				ICollection<ModelEntity> collection = this.m_scopeItems as ICollection<ModelEntity>;
				ICollection<ModelAttribute> collection2 = this.m_scopeItems as ICollection<ModelAttribute>;
				ExistingBindingContext existingBindingContext = new ExistingBindingContext();
				DsvItemMapper.FillExistingBindingInfo(this.m_model, collection, collection2, existingBindingContext);
				IEnumerable<EvaluateDsvItemRule> enabledDsvItemRules = this.m_ruleSet.GetEnabledDsvItemRules();
				IDsvItemFilter dsvItemFilter;
				if (collection != null)
				{
					dsvItemFilter = new ModelGen.DsvItemFilterWithEntityScope(enabledDsvItemRules, collection, existingBindingContext);
				}
				else if (collection2 != null)
				{
					dsvItemFilter = new ModelGen.DsvItemFilterWithAttributeScope(enabledDsvItemRules, collection2, existingBindingContext);
				}
				else
				{
					if (this.m_scopeItems != null)
					{
						string text = "Unknown m_scopeItems: ";
						object scopeItems = this.m_scopeItems;
						throw new InternalModelingException(text + ((scopeItems != null) ? scopeItems.ToString() : null));
					}
					dsvItemFilter = new ModelGen.DsvItemFilter(enabledDsvItemRules);
				}
				if (this.m_dsvStatisticsProvider != null)
				{
					this.m_dsvStatisticsProvider.Progress += this.inner_Progress;
					this.m_dsvStatisticsProvider.Log += this.inner_Log;
					try
					{
						this.m_dsvStatisticsProvider.Fill(this.m_model.DataSourceView, dsvItemFilter, this.m_overwriteDsvStatistics, this);
					}
					finally
					{
						this.m_dsvStatisticsProvider.Progress -= this.inner_Progress;
						this.m_dsvStatisticsProvider.Log -= this.inner_Log;
					}
				}
				new ModelRuleProcessor(this, this.m_ruleSet.GetEnabledProcessingRules(), this.m_model, dsvItemFilter, existingBindingContext, this.m_traceVerbose).ProcessRules();
				foreach (ValidationMessage validationMessage in this.m_model.Validate(false))
				{
					this.OnLog(new ModelGenLogEventArgs((validationMessage.Severity == Severity.Error) ? LogEntryType.Error : LogEntryType.Warning, validationMessage.ObjectType, validationMessage.ObjectID, validationMessage.Message));
				}
			}
			finally
			{
				this.m_running = false;
			}
			return modelGenResult;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002741C File Offset: 0x0002561C
		public void RunAsync()
		{
			object lockRoot = this.m_lockRoot;
			lock (lockRoot)
			{
				this.CheckNotRunning();
				this.m_cancelRequestEvent = new ManualResetEvent(false);
				this.m_asyncThread = new Thread(new ThreadStart(this.RunAsyncInternal));
				this.m_asyncThread.Name = "ModelGen";
				this.m_asyncThread.Start();
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000274A0 File Offset: 0x000256A0
		public void Cancel()
		{
			object lockRoot = this.m_lockRoot;
			lock (lockRoot)
			{
				if (this.m_asyncThread != null)
				{
					this.m_cancelRequestEvent.Set();
					this.m_asyncThread.Join(5000);
					if (this.m_asyncThread != null && this.m_asyncThread.IsAlive)
					{
						this.m_asyncThread.Abort();
					}
				}
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002752C File Offset: 0x0002572C
		protected override void Dispose(bool disposing)
		{
			if (this.m_asyncThread != null && this.m_running)
			{
				this.Cancel();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002754F File Offset: 0x0002574F
		private void CheckNotRunning()
		{
			if (this.m_running)
			{
				throw new InvalidOperationException(SR.ModelGen_AlreadyExecuting);
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00027568 File Offset: 0x00025768
		private void RunAsyncInternal()
		{
			Exception ex = null;
			ModelGenResult modelGenResult = null;
			try
			{
				modelGenResult = this.Run();
			}
			catch (Exception ex)
			{
			}
			finally
			{
				bool flag = this.m_cancelRequestEvent.WaitOne(0, false);
				this.m_asyncThread = null;
				this.m_cancelRequestEvent = null;
				if (ex != null)
				{
					this.OnComplete(new ModelGenCompleteEventArgs(modelGenResult, ex, flag));
				}
				else
				{
					this.OnComplete(new ModelGenCompleteEventArgs(modelGenResult, null, flag));
				}
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000275E4 File Offset: 0x000257E4
		private void OnProgress(ProgressEventArgs e)
		{
			if (this.Progress != null)
			{
				this.Progress(this, e);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000275FB File Offset: 0x000257FB
		private void OnLog(ModelGenLogEventArgs e)
		{
			if (this.Log != null)
			{
				this.Log(this, e);
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00027612 File Offset: 0x00025812
		private void OnComplete(ModelGenCompleteEventArgs e)
		{
			if (this.Complete != null)
			{
				this.Complete(this, e);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00027629 File Offset: 0x00025829
		private void inner_Progress(object sender, ProgressEventArgs e)
		{
			this.OnProgress(e);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00027632 File Offset: 0x00025832
		private void inner_Log(object sender, LogEventArgs e)
		{
			this.OnLog(new ModelGenLogEventArgs(e));
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00027640 File Offset: 0x00025840
		void IModelGenEvents.RaiseProgress(ProgressEventArgs e)
		{
			this.OnProgress(e);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00027649 File Offset: 0x00025849
		void IModelGenEvents.RaiseLog(ModelGenLogEventArgs e)
		{
			this.OnLog(e);
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00027652 File Offset: 0x00025852
		bool ICancelEvent.CancelRequested
		{
			get
			{
				return this.m_cancelRequestEvent != null && this.m_cancelRequestEvent.WaitOne(0, false);
			}
		}

		// Token: 0x040004EC RID: 1260
		private const int CancelRequestTimeout = 5000;

		// Token: 0x040004ED RID: 1261
		private RuleSet m_ruleSet;

		// Token: 0x040004EE RID: 1262
		private SemanticModel m_model;

		// Token: 0x040004EF RID: 1263
		private IDsvStatisticsProvider m_dsvStatisticsProvider;

		// Token: 0x040004F0 RID: 1264
		private bool m_overwriteDsvStatistics;

		// Token: 0x040004F1 RID: 1265
		private bool m_traceVerbose;

		// Token: 0x040004F2 RID: 1266
		private object m_scopeItems;

		// Token: 0x040004F3 RID: 1267
		private volatile bool m_running;

		// Token: 0x040004F4 RID: 1268
		private volatile Thread m_asyncThread;

		// Token: 0x040004F5 RID: 1269
		private ManualResetEvent m_cancelRequestEvent;

		// Token: 0x040004F6 RID: 1270
		private object m_lockRoot = new object();

		// Token: 0x020001C6 RID: 454
		private class DsvItemFilter : IDsvItemFilter
		{
			// Token: 0x0600115C RID: 4444 RVA: 0x00036563 File Offset: 0x00034763
			internal DsvItemFilter(IEnumerable<EvaluateDsvItemRule> rules)
			{
				this.m_rules = new List<EvaluateDsvItemRule>(rules);
			}

			// Token: 0x0600115D RID: 4445 RVA: 0x00036578 File Offset: 0x00034778
			public virtual bool Evaluate(DsvItem dsvItem)
			{
				foreach (EvaluateDsvItemRule evaluateDsvItemRule in this.m_rules)
				{
					if (evaluateDsvItemRule.AppliesTo(dsvItem))
					{
						return !evaluateDsvItemRule.Exclude;
					}
				}
				return false;
			}

			// Token: 0x040007D9 RID: 2009
			private readonly List<EvaluateDsvItemRule> m_rules;
		}

		// Token: 0x020001C7 RID: 455
		private class DsvItemFilterWithEntityScope : ModelGen.DsvItemFilter
		{
			// Token: 0x0600115E RID: 4446 RVA: 0x000365DC File Offset: 0x000347DC
			internal DsvItemFilterWithEntityScope(IEnumerable<EvaluateDsvItemRule> rules, ICollection<ModelEntity> scopeEntities, ExistingBindingContext bindingContext)
				: base(rules)
			{
				this.m_scopeEntities = scopeEntities;
				this.m_bindingContext = bindingContext;
			}

			// Token: 0x0600115F RID: 4447 RVA: 0x000365F4 File Offset: 0x000347F4
			public override bool Evaluate(DsvItem dsvItem)
			{
				if (!base.Evaluate(dsvItem))
				{
					return false;
				}
				DsvTable dsvTable = dsvItem as DsvTable;
				DsvColumn dsvColumn = dsvItem as DsvColumn;
				if (dsvTable != null)
				{
					ExistingTableBindingInfo bindingInfo = this.m_bindingContext.GetBindingInfo(dsvTable);
					if (bindingInfo.Entity == null || !this.m_scopeEntities.Contains(bindingInfo.Entity))
					{
						return false;
					}
				}
				if (dsvColumn != null)
				{
					ExistingColumnBindingInfo bindingInfo2 = this.m_bindingContext.GetBindingInfo(dsvColumn);
					if (bindingInfo2.Entity != null && !this.m_scopeEntities.Contains(bindingInfo2.Entity))
					{
						return false;
					}
					if ((bindingInfo2.Attribute == null && !this.Evaluate(dsvColumn.Table)) || (bindingInfo2.Attribute != null && !this.m_scopeEntities.Contains(bindingInfo2.Attribute.Entity)))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040007DA RID: 2010
			private readonly ICollection<ModelEntity> m_scopeEntities;

			// Token: 0x040007DB RID: 2011
			private readonly ExistingBindingContext m_bindingContext;
		}

		// Token: 0x020001C8 RID: 456
		private class DsvItemFilterWithAttributeScope : ModelGen.DsvItemFilter
		{
			// Token: 0x06001160 RID: 4448 RVA: 0x000366AD File Offset: 0x000348AD
			internal DsvItemFilterWithAttributeScope(IEnumerable<EvaluateDsvItemRule> rules, ICollection<ModelAttribute> scopeAttrs, ExistingBindingContext bindingContext)
				: base(rules)
			{
				this.m_scopeAttrs = scopeAttrs;
				this.m_bindingContext = bindingContext;
			}

			// Token: 0x06001161 RID: 4449 RVA: 0x000366C4 File Offset: 0x000348C4
			public override bool Evaluate(DsvItem dsvItem)
			{
				if (!base.Evaluate(dsvItem))
				{
					return false;
				}
				DsvColumn dsvColumn = dsvItem as DsvColumn;
				if (dsvColumn != null)
				{
					ExistingColumnBindingInfo bindingInfo = this.m_bindingContext.GetBindingInfo(dsvColumn);
					if (bindingInfo.Attribute != null)
					{
						return this.m_scopeAttrs.Contains(bindingInfo.Attribute);
					}
				}
				return false;
			}

			// Token: 0x040007DC RID: 2012
			private readonly ICollection<ModelAttribute> m_scopeAttrs;

			// Token: 0x040007DD RID: 2013
			private readonly ExistingBindingContext m_bindingContext;
		}
	}
}
