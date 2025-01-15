using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004ED RID: 1261
	[Serializable]
	internal sealed class PageSection : ReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003FFB RID: 16379 RVA: 0x0010E90D File Offset: 0x0010CB0D
		internal PageSection(bool isHeader, int id, int idForReportItems, ReportSection reportSection)
			: base(id, reportSection)
		{
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
			this.m_isHeader = isHeader;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x0010E92C File Offset: 0x0010CB2C
		internal PageSection(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06003FFD RID: 16381 RVA: 0x0010E935 File Offset: 0x0010CB35
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				if (!this.m_isHeader)
				{
					return Microsoft.ReportingServices.ReportProcessing.ObjectType.PageFooter;
				}
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.PageHeader;
			}
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x0010E942 File Offset: 0x0010CB42
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x0010E94A File Offset: 0x0010CB4A
		internal bool IsHeader
		{
			get
			{
				return this.m_isHeader;
			}
			set
			{
				this.m_isHeader = value;
			}
		}

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x0010E953 File Offset: 0x0010CB53
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x0010E95B File Offset: 0x0010CB5B
		internal bool PrintOnFirstPage
		{
			get
			{
				return this.m_printOnFirstPage;
			}
			set
			{
				this.m_printOnFirstPage = value;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x0010E964 File Offset: 0x0010CB64
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x0010E96C File Offset: 0x0010CB6C
		internal bool PrintOnLastPage
		{
			get
			{
				return this.m_printOnLastPage;
			}
			set
			{
				this.m_printOnLastPage = value;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x0010E975 File Offset: 0x0010CB75
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x0010E97D File Offset: 0x0010CB7D
		internal bool PrintBetweenSections
		{
			get
			{
				return this.m_printBetweenSections;
			}
			set
			{
				this.m_printBetweenSections = value;
			}
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x0010E986 File Offset: 0x0010CB86
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x0010E98E File Offset: 0x0010CB8E
		internal ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x0010E997 File Offset: 0x0010CB97
		internal bool UpgradedSnapshotPostProcessEvaluate
		{
			get
			{
				return this.m_postProcessEvaluate;
			}
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x0010E9A0 File Offset: 0x0010CBA0
		internal override bool Initialize(InitializationContext context)
		{
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection;
			context.ObjectType = this.ObjectType;
			context.ObjectName = null;
			context.ExprHostBuilder.PageSectionStart();
			base.Initialize(context);
			this.m_reportItems.Initialize(context);
			base.ExprHostID = context.ExprHostBuilder.PageSectionEnd();
			return false;
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x0010EA07 File Offset: 0x0010CC07
		protected override void DataRendererInitialize(InitializationContext context)
		{
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x0010EA0C File Offset: 0x0010CC0C
		[SkipMemberStaticValidation(MemberName.PostProcessEvaluate)]
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageSection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.PrintOnFirstPage, Token.Boolean),
				new MemberInfo(MemberName.PrintOnLastPage, Token.Boolean),
				new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection),
				new ReadOnlyMemberInfo(MemberName.PostProcessEvaluate, Token.Boolean),
				new MemberInfo(MemberName.PrintBetweenSections, Token.Boolean)
			});
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x0010EA98 File Offset: 0x0010CC98
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(PageSection.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.PrintOnFirstPage)
				{
					if (memberName == MemberName.ReportItems)
					{
						writer.Write(this.m_reportItems);
						continue;
					}
					if (memberName == MemberName.PrintOnFirstPage)
					{
						writer.Write(this.m_printOnFirstPage);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.PrintOnLastPage)
					{
						writer.Write(this.m_printOnLastPage);
						continue;
					}
					if (memberName == MemberName.PrintBetweenSections)
					{
						writer.Write(this.m_printBetweenSections);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x0010EB4C File Offset: 0x0010CD4C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(PageSection.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ReportItems)
				{
					switch (memberName)
					{
					case MemberName.PrintOnFirstPage:
						this.m_printOnFirstPage = reader.ReadBoolean();
						break;
					case MemberName.PrintOnLastPage:
						this.m_printOnLastPage = reader.ReadBoolean();
						break;
					case MemberName.PostProcessEvaluate:
						this.m_postProcessEvaluate = reader.ReadBoolean();
						break;
					default:
						if (memberName != MemberName.PrintBetweenSections)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_printBetweenSections = reader.ReadBoolean();
						}
						break;
					}
				}
				else
				{
					this.m_reportItems = (ReportItemCollection)reader.ReadRIFObject();
				}
			}
			if (this.m_name == null)
			{
				if (this.IsHeader)
				{
					this.m_name = "PageHeader";
					return;
				}
				this.m_name = "PageFooter";
			}
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x0010EC36 File Offset: 0x0010CE36
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x0010EC43 File Offset: 0x0010CE43
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageSection;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0010EC4C File Offset: 0x0010CE4C
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.PageSectionHostsRemotable[base.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_styleClass != null)
				{
					this.m_styleClass.SetStyleExprHost(this.m_exprHost);
				}
			}
		}

		// Token: 0x04001D76 RID: 7542
		private bool m_printOnFirstPage;

		// Token: 0x04001D77 RID: 7543
		private bool m_printOnLastPage;

		// Token: 0x04001D78 RID: 7544
		private bool m_printBetweenSections;

		// Token: 0x04001D79 RID: 7545
		private ReportItemCollection m_reportItems;

		// Token: 0x04001D7A RID: 7546
		private bool m_postProcessEvaluate;

		// Token: 0x04001D7B RID: 7547
		[NonSerialized]
		private bool m_isHeader;

		// Token: 0x04001D7C RID: 7548
		[NonSerialized]
		private StyleExprHost m_exprHost;

		// Token: 0x04001D7D RID: 7549
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = PageSection.GetDeclaration();
	}
}
