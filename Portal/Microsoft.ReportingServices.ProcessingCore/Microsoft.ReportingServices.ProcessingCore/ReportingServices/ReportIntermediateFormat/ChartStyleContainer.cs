using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200049E RID: 1182
	internal abstract class ChartStyleContainer : IStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600394D RID: 14669 RVA: 0x000F9550 File Offset: 0x000F7750
		internal ChartStyleContainer()
		{
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000F9558 File Offset: 0x000F7758
		internal ChartStyleContainer(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000F9567 File Offset: 0x000F7767
		// (set) Token: 0x06003950 RID: 14672 RVA: 0x000F956F File Offset: 0x000F776F
		public Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000F9578 File Offset: 0x000F7778
		public virtual IInstancePath InstancePath
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06003952 RID: 14674 RVA: 0x000F9580 File Offset: 0x000F7780
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x000F9584 File Offset: 0x000F7784
		public string Name
		{
			get
			{
				return this.m_chart.Name;
			}
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000F9591 File Offset: 0x000F7791
		internal virtual void Initialize(InitializationContext context)
		{
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000F95A8 File Offset: 0x000F77A8
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			ChartStyleContainer chartStyleContainer = (ChartStyleContainer)base.MemberwiseClone();
			chartStyleContainer.m_chart = (Chart)context.CurrentDataRegionClone;
			if (this.m_styleClass != null)
			{
				chartStyleContainer.m_styleClass = (Style)this.m_styleClass.PublishClone(context);
			}
			return chartStyleContainer;
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000F95F3 File Offset: 0x000F77F3
		internal virtual void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(null != exprHost && null != reportObjectModel)");
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000F962C File Offset: 0x000F782C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style)
			});
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000F9678 File Offset: 0x000F7878
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartStyleContainer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_styleClass);
				}
			}
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000F96E4 File Offset: 0x000F78E4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartStyleContainer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Chart>(this);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_styleClass = (Style)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000F9754 File Offset: 0x000F7954
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartStyleContainer.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Chart)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Chart)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000F97F8 File Offset: 0x000F79F8
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer;
		}

		// Token: 0x04001B8B RID: 7051
		[Reference]
		protected Chart m_chart;

		// Token: 0x04001B8C RID: 7052
		protected Style m_styleClass;

		// Token: 0x04001B8D RID: 7053
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartStyleContainer.GetDeclaration();
	}
}
