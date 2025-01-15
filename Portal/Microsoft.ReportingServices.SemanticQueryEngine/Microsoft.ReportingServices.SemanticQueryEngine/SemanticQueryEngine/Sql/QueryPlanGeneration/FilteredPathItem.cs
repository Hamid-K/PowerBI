using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000057 RID: 87
	internal sealed class FilteredPathItem : IPathItem
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x00012286 File Offset: 0x00010486
		internal FilteredPathItem(IPathItem pathItem)
			: this(pathItem, null, false)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012291 File Offset: 0x00010491
		private FilteredPathItem(IPathItem pathItem, Expression filterCondition, bool evaluate)
		{
			if (pathItem == null || pathItem is FilteredPathItem)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.m_pathItem = pathItem;
			this.m_filterCondition = filterCondition;
			this.m_evaluate = evaluate;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000122BF File Offset: 0x000104BF
		public Cardinality Cardinality
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathItem.Cardinality;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000122CC File Offset: 0x000104CC
		public Optionality Optionality
		{
			[DebuggerStepThrough]
			get
			{
				if (this.m_filterCondition == null)
				{
					return this.m_pathItem.Optionality;
				}
				return Optionality.Optional;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000122E3 File Offset: 0x000104E3
		public Cardinality ReverseCardinality
		{
			[DebuggerStepThrough]
			get
			{
				if (this.m_pathItem.ReverseCardinality == Cardinality.Many && this.m_evaluate)
				{
					return Cardinality.One;
				}
				return this.m_pathItem.ReverseCardinality;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00012308 File Offset: 0x00010508
		public Optionality ReverseOptionality
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathItem.ReverseOptionality;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00012315 File Offset: 0x00010515
		public IQueryEntity TargetEntity
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathItem.TargetEntity;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00012322 File Offset: 0x00010522
		public IQueryEntity SourceEntity
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathItem.SourceEntity;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0001232F File Offset: 0x0001052F
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00012337 File Offset: 0x00010537
		internal Expression FilterCondition
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_filterCondition;
			}
			[DebuggerStepThrough]
			set
			{
				if (this.m_filterCondition != null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				this.m_filterCondition = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001234E File Offset: 0x0001054E
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00012356 File Offset: 0x00010556
		internal bool Evaluate
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_evaluate;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_evaluate = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001235F File Offset: 0x0001055F
		internal IPathItem ExpressionPathItem
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathItem;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012368 File Offset: 0x00010568
		public bool IsSameAs(FilteredPathItem pathItem)
		{
			return this.m_pathItem.Equals(pathItem.ExpressionPathItem) && this.m_evaluate == pathItem.Evaluate && (this.m_filterCondition == pathItem.FilterCondition || (this.m_filterCondition != null && pathItem.FilterCondition != null && this.m_filterCondition.IsSameAs(pathItem.FilterCondition)));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000123CE File Offset: 0x000105CE
		internal FilteredPathItem Clone()
		{
			return new FilteredPathItem(this.m_pathItem, this.m_filterCondition, this.m_evaluate);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000123E8 File Offset: 0x000105E8
		public override string ToString()
		{
			string text = this.m_pathItem.ToString();
			if (this.m_filterCondition != null)
			{
				text += "{F+}";
			}
			return text;
		}

		// Token: 0x040001CD RID: 461
		private readonly IPathItem m_pathItem;

		// Token: 0x040001CE RID: 462
		private Expression m_filterCondition;

		// Token: 0x040001CF RID: 463
		private bool m_evaluate;
	}
}
