using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004EB RID: 1259
	public class PageBreak : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x0010E567 File Offset: 0x0010C767
		// (set) Token: 0x06003FE4 RID: 16356 RVA: 0x0010E56F File Offset: 0x0010C76F
		internal PageBreakLocation BreakLocation
		{
			get
			{
				return this.m_pageBreakLocation;
			}
			set
			{
				this.m_pageBreakLocation = value;
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x0010E578 File Offset: 0x0010C778
		// (set) Token: 0x06003FE6 RID: 16358 RVA: 0x0010E580 File Offset: 0x0010C780
		internal ExpressionInfo ResetPageNumber
		{
			get
			{
				return this.m_resetPageNumber;
			}
			set
			{
				this.m_resetPageNumber = value;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x0010E589 File Offset: 0x0010C789
		// (set) Token: 0x06003FE8 RID: 16360 RVA: 0x0010E591 File Offset: 0x0010C791
		internal ExpressionInfo Disabled
		{
			get
			{
				return this.m_disabled;
			}
			set
			{
				this.m_disabled = value;
			}
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x0010E59A File Offset: 0x0010C79A
		// (set) Token: 0x06003FEA RID: 16362 RVA: 0x0010E5A2 File Offset: 0x0010C7A2
		internal PageBreakExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
			set
			{
				this.m_exprHost = value;
			}
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x0010E5AC File Offset: 0x0010C7AC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			PageBreak pageBreak = (PageBreak)base.MemberwiseClone();
			if (this.m_disabled != null)
			{
				pageBreak.m_disabled = (ExpressionInfo)this.m_disabled.PublishClone(context);
			}
			if (this.m_resetPageNumber != null)
			{
				pageBreak.m_resetPageNumber = (ExpressionInfo)this.m_resetPageNumber.PublishClone(context);
			}
			return pageBreak;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x0010E604 File Offset: 0x0010C804
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.PageBreakStart();
			if (this.m_disabled != null)
			{
				this.m_disabled.Initialize("Disabled", context);
				context.ExprHostBuilder.Disabled(this.m_disabled);
			}
			if (this.m_resetPageNumber != null)
			{
				this.m_resetPageNumber.Initialize("ResetPageNumber", context);
				context.ExprHostBuilder.ResetPageNumber(this.m_resetPageNumber);
			}
			context.ExprHostBuilder.PageBreakEnd();
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x0010E680 File Offset: 0x0010C880
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(PageBreak.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.PageBreakLocation)
				{
					if (memberName != MemberName.Disabled)
					{
						if (memberName != MemberName.ResetPageNumber)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_resetPageNumber);
						}
					}
					else
					{
						writer.Write(this.m_disabled);
					}
				}
				else
				{
					writer.WriteEnum((int)this.m_pageBreakLocation);
				}
			}
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x0010E704 File Offset: 0x0010C904
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(PageBreak.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.PageBreakLocation)
				{
					if (memberName != MemberName.Disabled)
					{
						if (memberName != MemberName.ResetPageNumber)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_resetPageNumber = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_disabled = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_pageBreakLocation = (PageBreakLocation)reader.ReadEnum();
				}
			}
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x0010E791 File Offset: 0x0010C991
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "No references to resolve");
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x0010E7A3 File Offset: 0x0010C9A3
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageBreak;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x0010E7AC File Offset: 0x0010C9AC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.PageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.Disabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ResetPageNumber, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x0010E806 File Offset: 0x0010CA06
		internal void SetExprHost(PageBreakExprHost pageBreakExpressionHost, ObjectModelImpl reportObjectModel)
		{
			if (pageBreakExpressionHost != null)
			{
				this.m_exprHost = pageBreakExpressionHost;
				Global.Tracer.Assert(this.m_exprHost != null && reportObjectModel != null);
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x0010E837 File Offset: 0x0010CA37
		internal bool EvaluateDisabled(IReportScopeInstance romInstance, OnDemandProcessingContext context, IPageBreakOwner pageBreakOwner)
		{
			context.SetupContext(pageBreakOwner.InstancePath, romInstance);
			return context.ReportRuntime.EvaluatePageBreakDisabledExpression(this, this.m_disabled, pageBreakOwner.ObjectType, pageBreakOwner.ObjectName);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x0010E864 File Offset: 0x0010CA64
		internal bool EvaluateResetPageNumber(IReportScopeInstance romInstance, OnDemandProcessingContext context, IPageBreakOwner pageBreakOwner)
		{
			context.SetupContext(pageBreakOwner.InstancePath, romInstance);
			return context.ReportRuntime.EvaluatePageBreakResetPageNumberExpression(this, this.m_resetPageNumber, pageBreakOwner.ObjectType, pageBreakOwner.ObjectName);
		}

		// Token: 0x04001D71 RID: 7537
		private PageBreakLocation m_pageBreakLocation;

		// Token: 0x04001D72 RID: 7538
		private ExpressionInfo m_disabled;

		// Token: 0x04001D73 RID: 7539
		private ExpressionInfo m_resetPageNumber;

		// Token: 0x04001D74 RID: 7540
		[NonSerialized]
		private PageBreakExprHost m_exprHost;

		// Token: 0x04001D75 RID: 7541
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = PageBreak.GetDeclaration();
	}
}
