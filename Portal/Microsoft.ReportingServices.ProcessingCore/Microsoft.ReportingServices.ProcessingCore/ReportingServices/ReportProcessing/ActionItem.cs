using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E0 RID: 1760
	[Serializable]
	internal sealed class ActionItem
	{
		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x001832A8 File Offset: 0x001814A8
		// (set) Token: 0x06005FE3 RID: 24547 RVA: 0x001832B0 File Offset: 0x001814B0
		internal ExpressionInfo HyperLinkURL
		{
			get
			{
				return this.m_hyperLinkURL;
			}
			set
			{
				this.m_hyperLinkURL = value;
			}
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x06005FE4 RID: 24548 RVA: 0x001832B9 File Offset: 0x001814B9
		// (set) Token: 0x06005FE5 RID: 24549 RVA: 0x001832C1 File Offset: 0x001814C1
		internal ExpressionInfo DrillthroughReportName
		{
			get
			{
				return this.m_drillthroughReportName;
			}
			set
			{
				this.m_drillthroughReportName = value;
			}
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06005FE6 RID: 24550 RVA: 0x001832CA File Offset: 0x001814CA
		// (set) Token: 0x06005FE7 RID: 24551 RVA: 0x001832D2 File Offset: 0x001814D2
		internal ParameterValueList DrillthroughParameters
		{
			get
			{
				return this.m_drillthroughParameters;
			}
			set
			{
				this.m_drillthroughParameters = value;
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06005FE8 RID: 24552 RVA: 0x001832DB File Offset: 0x001814DB
		// (set) Token: 0x06005FE9 RID: 24553 RVA: 0x001832E3 File Offset: 0x001814E3
		internal ExpressionInfo DrillthroughBookmarkLink
		{
			get
			{
				return this.m_drillthroughBookmarkLink;
			}
			set
			{
				this.m_drillthroughBookmarkLink = value;
			}
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06005FEA RID: 24554 RVA: 0x001832EC File Offset: 0x001814EC
		// (set) Token: 0x06005FEB RID: 24555 RVA: 0x001832F4 File Offset: 0x001814F4
		internal ExpressionInfo BookmarkLink
		{
			get
			{
				return this.m_bookmarkLink;
			}
			set
			{
				this.m_bookmarkLink = value;
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06005FEC RID: 24556 RVA: 0x001832FD File Offset: 0x001814FD
		// (set) Token: 0x06005FED RID: 24557 RVA: 0x00183305 File Offset: 0x00181505
		internal ExpressionInfo Label
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

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06005FEE RID: 24558 RVA: 0x0018330E File Offset: 0x0018150E
		// (set) Token: 0x06005FEF RID: 24559 RVA: 0x00183316 File Offset: 0x00181516
		internal int ComputedIndex
		{
			get
			{
				return this.m_computedIndex;
			}
			set
			{
				this.m_computedIndex = value;
			}
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06005FF0 RID: 24560 RVA: 0x0018331F File Offset: 0x0018151F
		// (set) Token: 0x06005FF1 RID: 24561 RVA: 0x00183327 File Offset: 0x00181527
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x06005FF2 RID: 24562 RVA: 0x00183330 File Offset: 0x00181530
		internal ActionExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x00183338 File Offset: 0x00181538
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ActionStart();
			if (this.m_hyperLinkURL != null)
			{
				this.m_hyperLinkURL.Initialize("Hyperlink", context);
				context.ExprHostBuilder.ActionHyperlink(this.m_hyperLinkURL);
			}
			if (this.m_drillthroughReportName != null)
			{
				this.m_drillthroughReportName.Initialize("DrillthroughReportName", context);
				context.ExprHostBuilder.ActionDrillThroughReportName(this.m_drillthroughReportName);
			}
			if (this.m_drillthroughParameters != null)
			{
				for (int i = 0; i < this.m_drillthroughParameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_drillthroughParameters[i];
					context.ExprHostBuilder.ActionDrillThroughParameterStart();
					parameterValue.Initialize(context, false);
					parameterValue.ExprHostID = context.ExprHostBuilder.ActionDrillThroughParameterEnd();
				}
			}
			if (this.m_drillthroughBookmarkLink != null)
			{
				this.m_drillthroughBookmarkLink.Initialize("BookmarkLink", context);
				context.ExprHostBuilder.ActionDrillThroughBookmarkLink(this.m_drillthroughBookmarkLink);
			}
			if (this.m_bookmarkLink != null)
			{
				this.m_bookmarkLink.Initialize("BookmarkLink", context);
				context.ExprHostBuilder.ActionBookmarkLink(this.m_bookmarkLink);
			}
			if (this.m_label != null)
			{
				this.m_label.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_label);
			}
			this.m_exprHostID = context.ExprHostBuilder.ActionEnd();
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x0018348C File Offset: 0x0018168C
		internal void SetExprHost(IList<ActionExprHost> actionItemExprHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(actionItemExprHosts != null && reportObjectModel != null);
				this.m_exprHost = actionItemExprHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.DrillThroughParameterHostsRemotable != null)
				{
					Global.Tracer.Assert(this.m_drillthroughParameters != null);
					for (int i = this.m_drillthroughParameters.Count - 1; i >= 0; i--)
					{
						this.m_drillthroughParameters[i].SetExprHost(this.m_exprHost.DrillThroughParameterHostsRemotable, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x0018352C File Offset: 0x0018172C
		internal void SetExprHost(ActionExprHost actionExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(actionExprHost != null && reportObjectModel != null);
			this.m_exprHost = actionExprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_exprHost.DrillThroughParameterHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_drillthroughParameters != null);
				for (int i = this.m_drillthroughParameters.Count - 1; i >= 0; i--)
				{
					this.m_drillthroughParameters[i].SetExprHost(this.m_exprHost.DrillThroughParameterHostsRemotable, reportObjectModel);
				}
			}
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x001835B8 File Offset: 0x001817B8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.HyperLinkURL, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DrillthroughReportName, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DrillthroughParameters, ObjectType.ParameterValueList),
				new MemberInfo(MemberName.DrillthroughBookmarkLink, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BookmarkLink, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Index, Token.Int32)
			});
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x0018366C File Offset: 0x0018186C
		internal void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, int ownerUniqueName, int index)
		{
			if (this.m_drillthroughReportName == null)
			{
				return;
			}
			Global.Tracer.Assert(this.m_drillthroughReportName.Type == ExpressionInfo.Types.Constant);
			if (this.m_drillthroughReportName.Value == null)
			{
				return;
			}
			DrillthroughParameters drillthroughParameters = null;
			if (this.m_drillthroughParameters != null)
			{
				int i = 0;
				while (i < this.m_drillthroughParameters.Count)
				{
					ParameterValue parameterValue = this.m_drillthroughParameters[i];
					if (parameterValue.Omit == null)
					{
						goto IL_007E;
					}
					Global.Tracer.Assert(parameterValue.Omit.Type == ExpressionInfo.Types.Constant);
					if (!parameterValue.Omit.BoolValue)
					{
						goto IL_007E;
					}
					IL_00B6:
					i++;
					continue;
					IL_007E:
					Global.Tracer.Assert(parameterValue.Value.Type == ExpressionInfo.Types.Constant);
					if (drillthroughParameters == null)
					{
						drillthroughParameters = new DrillthroughParameters();
					}
					drillthroughParameters.Add(parameterValue.Name, parameterValue.Value.Value);
					goto IL_00B6;
				}
			}
			DrillthroughInformation drillthroughInformation = new DrillthroughInformation(this.m_drillthroughReportName.Value, drillthroughParameters, null);
			string text = ownerUniqueName.ToString(CultureInfo.InvariantCulture) + ":" + index.ToString(CultureInfo.InvariantCulture);
			processingContext.DrillthroughInfo.AddDrillthrough(text, drillthroughInformation);
		}

		// Token: 0x040030C1 RID: 12481
		private ExpressionInfo m_hyperLinkURL;

		// Token: 0x040030C2 RID: 12482
		private ExpressionInfo m_drillthroughReportName;

		// Token: 0x040030C3 RID: 12483
		private ParameterValueList m_drillthroughParameters;

		// Token: 0x040030C4 RID: 12484
		private ExpressionInfo m_drillthroughBookmarkLink;

		// Token: 0x040030C5 RID: 12485
		private ExpressionInfo m_bookmarkLink;

		// Token: 0x040030C6 RID: 12486
		private ExpressionInfo m_label;

		// Token: 0x040030C7 RID: 12487
		private int m_exprHostID = -1;

		// Token: 0x040030C8 RID: 12488
		private int m_computedIndex = -1;

		// Token: 0x040030C9 RID: 12489
		[NonSerialized]
		private ActionExprHost m_exprHost;
	}
}
