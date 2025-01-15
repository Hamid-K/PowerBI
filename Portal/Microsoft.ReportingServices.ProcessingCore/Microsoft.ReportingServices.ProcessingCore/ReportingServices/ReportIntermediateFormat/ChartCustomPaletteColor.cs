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
	// Token: 0x0200047F RID: 1151
	[Serializable]
	internal sealed class ChartCustomPaletteColor : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x000EA5F3 File Offset: 0x000E87F3
		internal ChartCustomPaletteColor()
		{
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000EA5FB File Offset: 0x000E87FB
		internal ChartCustomPaletteColor(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000EA60A File Offset: 0x000E880A
		internal ChartCustomPaletteColorExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000EA612 File Offset: 0x000E8812
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000EA61A File Offset: 0x000E881A
		// (set) Token: 0x0600356F RID: 13679 RVA: 0x000EA622 File Offset: 0x000E8822
		internal ExpressionInfo Color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
			}
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000EA62B File Offset: 0x000E882B
		internal void SetExprHost(ChartCustomPaletteColorExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000EA65C File Offset: 0x000E885C
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.ChartCustomPaletteColorStart(index);
			if (this.m_color != null)
			{
				this.m_color.Initialize("Color", context);
				context.ExprHostBuilder.ChartCustomPaletteColor(this.m_color);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartCustomPaletteColorEnd();
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000EA6B3 File Offset: 0x000E88B3
		internal string EvaluateColor(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartCustomPaletteColorExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000EA6DC File Offset: 0x000E88DC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartCustomPaletteColor chartCustomPaletteColor = (ChartCustomPaletteColor)base.MemberwiseClone();
			chartCustomPaletteColor.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_color != null)
			{
				chartCustomPaletteColor.m_color = (ExpressionInfo)this.m_color.PublishClone(context);
			}
			return chartCustomPaletteColor;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000EA728 File Offset: 0x000E8928
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartCustomPaletteColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Color, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference)
			});
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000EA788 File Offset: 0x000E8988
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartCustomPaletteColor.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Color)
				{
					if (memberName != MemberName.ExprHostID)
					{
						if (memberName != MemberName.Chart)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.WriteReference(this.m_chart);
						}
					}
					else
					{
						writer.Write(this.m_exprHostID);
					}
				}
				else
				{
					writer.Write(this.m_color);
				}
			}
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000EA80C File Offset: 0x000E8A0C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartCustomPaletteColor.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Color)
				{
					if (memberName != MemberName.ExprHostID)
					{
						if (memberName != MemberName.Chart)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						}
					}
					else
					{
						this.m_exprHostID = reader.ReadInt32();
					}
				}
				else
				{
					this.m_color = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000EA898 File Offset: 0x000E8A98
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartCustomPaletteColor.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Chart)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000EA93C File Offset: 0x000E8B3C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartCustomPaletteColor;
		}

		// Token: 0x04001A4A RID: 6730
		private int m_exprHostID;

		// Token: 0x04001A4B RID: 6731
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001A4C RID: 6732
		private ExpressionInfo m_color;

		// Token: 0x04001A4D RID: 6733
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartCustomPaletteColor.GetDeclaration();

		// Token: 0x04001A4E RID: 6734
		[NonSerialized]
		private ChartCustomPaletteColorExprHost m_exprHost;
	}
}
