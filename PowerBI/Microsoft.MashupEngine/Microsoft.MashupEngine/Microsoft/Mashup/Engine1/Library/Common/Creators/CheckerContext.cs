using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x02001178 RID: 4472
	internal class CheckerContext : IDisposable
	{
		// Token: 0x06007565 RID: 30053 RVA: 0x00192944 File Offset: 0x00190B44
		protected CheckerContext(ContextLabel milestone, CheckerContext parentContext)
			: this(milestone, parentContext.setCurrent)
		{
			this.parentContext = parentContext;
		}

		// Token: 0x06007566 RID: 30054 RVA: 0x0019295A File Offset: 0x00190B5A
		protected CheckerContext(ContextLabel milestone, Action<CheckerContext> setCurrent)
		{
			if (setCurrent == null)
			{
				throw new ArgumentNullException("setCurrent");
			}
			this.setCurrent = setCurrent;
			this.setCurrent(this);
			this.milestone = milestone;
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06007567 RID: 30055 RVA: 0x0019298A File Offset: 0x00190B8A
		// (set) Token: 0x06007568 RID: 30056 RVA: 0x00192992 File Offset: 0x00190B92
		internal IRecordExpression CurrentRecord { get; set; }

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06007569 RID: 30057 RVA: 0x0019299C File Offset: 0x00190B9C
		protected virtual bool IsValid
		{
			get
			{
				if (this.parentContext == null)
				{
					return this.milestone == ContextLabel.None;
				}
				switch (this.milestone)
				{
				case ContextLabel.FieldAccess:
					return this.Parent.Milestone != ContextLabel.FieldAccess;
				case ContextLabel.MultifieldRecordProjection:
					return this.Parent.Milestone == ContextLabel.Root || this.Parent.Milestone == ContextLabel.Select || this.Parent.Milestone == ContextLabel.Sort || this.Parent.Milestone == ContextLabel.Transform || this.Parent.Milestone == ContextLabel.TransformBody || this.Parent.Milestone == ContextLabel.Inline || this.Parent.Milestone == ContextLabel.MultifieldRecordProjection;
				case ContextLabel.Root:
					return this.parentContext.Milestone == ContextLabel.None;
				case ContextLabel.Query:
					return this.parentContext.Milestone == ContextLabel.Root || this.parentContext.Milestone == ContextLabel.Query;
				case ContextLabel.SelectBody:
					return this.parentContext.Milestone == ContextLabel.Select;
				case ContextLabel.TransformBody:
					return this.parentContext.Milestone == ContextLabel.Transform;
				case ContextLabel.SortBody:
					return this.parentContext.Milestone == ContextLabel.Sort;
				}
				return false;
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x0600756A RID: 30058 RVA: 0x00192AD3 File Offset: 0x00190CD3
		internal ContextLabel Milestone
		{
			get
			{
				return this.milestone;
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x0600756B RID: 30059 RVA: 0x00192ADB File Offset: 0x00190CDB
		internal CheckerContext Parent
		{
			get
			{
				return this.parentContext;
			}
		}

		// Token: 0x0600756C RID: 30060 RVA: 0x00192AE3 File Offset: 0x00190CE3
		public void Dispose()
		{
			this.setCurrent(this.parentContext);
		}

		// Token: 0x0600756D RID: 30061 RVA: 0x00192AF6 File Offset: 0x00190CF6
		public CheckerContext Enter(ContextLabel newMilestone, FoldingTracingService foldingTracingService)
		{
			CheckerContext checkerContext = this.EnterHelper(newMilestone);
			if (checkerContext.IsValid)
			{
				return checkerContext;
			}
			if (foldingTracingService != null)
			{
				throw foldingTracingService.NewFoldingFailureException<FoldingWarnings.FoldingWarning<ContextLabel, ContextLabel>>(DbEnvironmentFoldingWarnings.InvalidContext(newMilestone, this.parentContext.Milestone));
			}
			throw new FoldingFailureException("Folding failed. More details are available in the trace.");
		}

		// Token: 0x0600756E RID: 30062 RVA: 0x00192B2D File Offset: 0x00190D2D
		protected virtual CheckerContext EnterHelper(ContextLabel milestone)
		{
			return new CheckerContext(milestone, this);
		}

		// Token: 0x0600756F RID: 30063 RVA: 0x00192B36 File Offset: 0x00190D36
		internal static CheckerContext New(ContextLabel milestone, Action<CheckerContext> setCurrent)
		{
			return new CheckerContext(milestone, setCurrent);
		}

		// Token: 0x04004065 RID: 16485
		private readonly ContextLabel milestone;

		// Token: 0x04004066 RID: 16486
		private readonly CheckerContext parentContext;

		// Token: 0x04004067 RID: 16487
		protected readonly Action<CheckerContext> setCurrent;
	}
}
