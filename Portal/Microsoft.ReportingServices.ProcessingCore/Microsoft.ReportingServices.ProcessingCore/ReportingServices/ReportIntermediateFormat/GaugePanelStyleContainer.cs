using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003F6 RID: 1014
	internal abstract class GaugePanelStyleContainer : IStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002AB3 RID: 10931 RVA: 0x000C6730 File Offset: 0x000C4930
		internal GaugePanelStyleContainer()
		{
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000C6738 File Offset: 0x000C4938
		internal GaugePanelStyleContainer(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000C6747 File Offset: 0x000C4947
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x000C674F File Offset: 0x000C494F
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

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000C6758 File Offset: 0x000C4958
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000C6760 File Offset: 0x000C4960
		Microsoft.ReportingServices.ReportProcessing.ObjectType IStyleContainer.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000C6764 File Offset: 0x000C4964
		string IStyleContainer.Name
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000C6771 File Offset: 0x000C4971
		internal virtual void Initialize(InitializationContext context)
		{
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000C6788 File Offset: 0x000C4988
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			GaugePanelStyleContainer gaugePanelStyleContainer = (GaugePanelStyleContainer)base.MemberwiseClone();
			gaugePanelStyleContainer.m_gaugePanel = (GaugePanel)context.CurrentDataRegionClone;
			if (this.m_styleClass != null)
			{
				gaugePanelStyleContainer.m_styleClass = (Style)this.m_styleClass.PublishClone(context);
			}
			return gaugePanelStyleContainer;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000C67D3 File Offset: 0x000C49D3
		internal virtual void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000C6804 File Offset: 0x000C4A04
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Token.Reference),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style)
			});
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000C6850 File Offset: 0x000C4A50
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(GaugePanelStyleContainer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName == MemberName.GaugePanel)
					{
						writer.WriteReference(this.m_gaugePanel);
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

		// Token: 0x06002ABF RID: 10943 RVA: 0x000C68BC File Offset: 0x000C4ABC
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(GaugePanelStyleContainer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.StyleClass)
				{
					if (memberName == MemberName.GaugePanel)
					{
						this.m_gaugePanel = reader.ReadReference<GaugePanel>(this);
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

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000C692C File Offset: 0x000C4B2C
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(GaugePanelStyleContainer.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.GaugePanel)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_gaugePanel = (GaugePanel)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000C69D0 File Offset: 0x000C4BD0
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer;
		}

		// Token: 0x04001768 RID: 5992
		[Reference]
		protected GaugePanel m_gaugePanel;

		// Token: 0x04001769 RID: 5993
		protected Style m_styleClass;

		// Token: 0x0400176A RID: 5994
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugePanelStyleContainer.GetDeclaration();
	}
}
