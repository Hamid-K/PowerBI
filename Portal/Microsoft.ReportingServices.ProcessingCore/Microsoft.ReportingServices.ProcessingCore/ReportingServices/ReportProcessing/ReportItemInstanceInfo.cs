using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200071D RID: 1821
	[Serializable]
	internal abstract class ReportItemInstanceInfo : InstanceInfo, IShowHideReceiver
	{
		// Token: 0x0600659B RID: 26011 RVA: 0x0018FD54 File Offset: 0x0018DF54
		protected ReportItemInstanceInfo(ReportProcessing.ProcessingContext pc, ReportItem reportItemDef, ReportItemInstance owner, int index)
		{
			this.ConstructorHelper(pc, reportItemDef, owner);
			if (pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
			{
				pc.ChunkManager.AddInstance(this, reportItemDef, owner, index, pc.InPageSection);
			}
			reportItemDef.StartHidden = this.m_startHidden;
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0018FDA4 File Offset: 0x0018DFA4
		protected ReportItemInstanceInfo(ReportProcessing.ProcessingContext pc, ReportItem reportItemDef, ReportItemInstance owner, int index, bool customCreated)
		{
			if (!customCreated)
			{
				this.ConstructorHelper(pc, reportItemDef, owner);
			}
			else
			{
				this.m_reportItemDef = reportItemDef;
			}
			if (pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
			{
				pc.ChunkManager.AddInstance(this, reportItemDef, owner, index, pc.InPageSection);
			}
			reportItemDef.StartHidden = this.m_startHidden;
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0018FE00 File Offset: 0x0018E000
		protected ReportItemInstanceInfo(ReportProcessing.ProcessingContext pc, ReportItem reportItemDef, ReportItemInstance owner, bool addToChunk)
		{
			this.ConstructorHelper(pc, reportItemDef, owner);
			if (addToChunk && pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
			{
				pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
			}
			reportItemDef.StartHidden = this.m_startHidden;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x0018FE4F File Offset: 0x0018E04F
		protected ReportItemInstanceInfo(ReportItem reportItemDef)
		{
			this.m_reportItemDef = reportItemDef;
		}

		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x0600659F RID: 26015 RVA: 0x0018FE5E File Offset: 0x0018E05E
		// (set) Token: 0x060065A0 RID: 26016 RVA: 0x0018FE66 File Offset: 0x0018E066
		internal object[] StyleAttributeValues
		{
			get
			{
				return this.m_styleAttributeValues;
			}
			set
			{
				this.m_styleAttributeValues = value;
			}
		}

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x060065A1 RID: 26017 RVA: 0x0018FE6F File Offset: 0x0018E06F
		// (set) Token: 0x060065A2 RID: 26018 RVA: 0x0018FE77 File Offset: 0x0018E077
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x0018FE80 File Offset: 0x0018E080
		// (set) Token: 0x060065A4 RID: 26020 RVA: 0x0018FE88 File Offset: 0x0018E088
		internal ReportItem ReportItemDef
		{
			get
			{
				return this.m_reportItemDef;
			}
			set
			{
				this.m_reportItemDef = value;
			}
		}

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x0018FE91 File Offset: 0x0018E091
		// (set) Token: 0x060065A6 RID: 26022 RVA: 0x0018FE99 File Offset: 0x0018E099
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x060065A7 RID: 26023 RVA: 0x0018FEA2 File Offset: 0x0018E0A2
		// (set) Token: 0x060065A8 RID: 26024 RVA: 0x0018FEAA File Offset: 0x0018E0AA
		internal string Bookmark
		{
			get
			{
				return this.m_bookmark;
			}
			set
			{
				this.m_bookmark = value;
			}
		}

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x0018FEB3 File Offset: 0x0018E0B3
		// (set) Token: 0x060065AA RID: 26026 RVA: 0x0018FEBB File Offset: 0x0018E0BB
		internal string ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17002400 RID: 9216
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x0018FEC4 File Offset: 0x0018E0C4
		// (set) Token: 0x060065AC RID: 26028 RVA: 0x0018FECC File Offset: 0x0018E0CC
		internal DataValueInstanceList CustomPropertyInstances
		{
			get
			{
				return this.m_customPropertyInstances;
			}
			set
			{
				this.m_customPropertyInstances = value;
			}
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x0018FED8 File Offset: 0x0018E0D8
		private void ConstructorHelper(ReportProcessing.ProcessingContext pc, ReportItem reportItemDef, ReportItemInstance owner)
		{
			this.m_reportItemDef = reportItemDef;
			Style styleClass = reportItemDef.StyleClass;
			if (styleClass != null && styleClass.ExpressionList != null && 0 < styleClass.ExpressionList.Count)
			{
				this.m_styleAttributeValues = new object[styleClass.ExpressionList.Count];
			}
			ReportProcessing.RuntimeRICollection.EvalReportItemAttr(reportItemDef, owner, this, pc);
			if (reportItemDef.CustomProperties != null)
			{
				this.m_customPropertyInstances = reportItemDef.CustomProperties.EvaluateExpressions(reportItemDef.ObjectType, reportItemDef.Name, null, pc);
			}
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x0018FF52 File Offset: 0x0018E152
		internal object GetStyleAttributeValue(int index)
		{
			Global.Tracer.Assert(this.m_styleAttributeValues != null && 0 <= index && index < this.m_styleAttributeValues.Length);
			return this.m_styleAttributeValues[index];
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x0018FF80 File Offset: 0x0018E180
		void IShowHideReceiver.ProcessReceiver(ReportProcessing.ProcessingContext context, int uniqueName)
		{
			this.m_startHidden = context.ProcessReceiver(uniqueName, this.m_reportItemDef.Visibility, this.m_reportItemDef.ExprHost, this.m_reportItemDef.ObjectType, this.m_reportItemDef.Name);
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x0018FFBC File Offset: 0x0018E1BC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.StyleAttributeValues, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.StartHidden, Token.Boolean),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.Bookmark, Token.String),
				new MemberInfo(MemberName.ToolTip, Token.String),
				new MemberInfo(MemberName.CustomPropertyInstances, ObjectType.DataValueInstanceList)
			});
		}

		// Token: 0x040032C6 RID: 12998
		protected object[] m_styleAttributeValues;

		// Token: 0x040032C7 RID: 12999
		protected bool m_startHidden;

		// Token: 0x040032C8 RID: 13000
		protected string m_label;

		// Token: 0x040032C9 RID: 13001
		protected string m_bookmark;

		// Token: 0x040032CA RID: 13002
		protected string m_toolTip;

		// Token: 0x040032CB RID: 13003
		protected DataValueInstanceList m_customPropertyInstances;

		// Token: 0x040032CC RID: 13004
		[NonSerialized]
		protected ReportItem m_reportItemDef;
	}
}
