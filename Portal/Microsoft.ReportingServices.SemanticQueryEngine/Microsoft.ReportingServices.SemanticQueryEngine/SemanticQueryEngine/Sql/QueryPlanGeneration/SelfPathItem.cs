using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000067 RID: 103
	internal sealed class SelfPathItem : IPathItem
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x000148AA File Offset: 0x00012AAA
		internal SelfPathItem(IQueryEntity entity)
		{
			if (entity == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("entity"));
			}
			this.m_entity = entity;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00004555 File Offset: 0x00002755
		Cardinality IPathItem.Cardinality
		{
			[DebuggerStepThrough]
			get
			{
				return Cardinality.One;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00004B5D File Offset: 0x00002D5D
		Optionality IPathItem.Optionality
		{
			[DebuggerStepThrough]
			get
			{
				return Optionality.Required;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004555 File Offset: 0x00002755
		Cardinality IPathItem.ReverseCardinality
		{
			[DebuggerStepThrough]
			get
			{
				return Cardinality.One;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00004B5D File Offset: 0x00002D5D
		Optionality IPathItem.ReverseOptionality
		{
			[DebuggerStepThrough]
			get
			{
				return Optionality.Required;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000148CC File Offset: 0x00012ACC
		IQueryEntity IPathItem.TargetEntity
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_entity;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000148CC File Offset: 0x00012ACC
		IQueryEntity IPathItem.SourceEntity
		{
			get
			{
				return this.m_entity;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000148D4 File Offset: 0x00012AD4
		public override string ToString()
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("[Self({0})]", new object[] { this.m_entity });
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000148EF File Offset: 0x00012AEF
		public override bool Equals(object obj)
		{
			return obj is SelfPathItem && ((SelfPathItem)obj).m_entity == this.m_entity;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001490E File Offset: 0x00012B0E
		public override int GetHashCode()
		{
			return this.m_entity.GetHashCode();
		}

		// Token: 0x040001F9 RID: 505
		private readonly IQueryEntity m_entity;
	}
}
