using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200050D RID: 1293
	[Serializable]
	internal sealed class Line : ReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060044B1 RID: 17585 RVA: 0x0011F040 File Offset: 0x0011D240
		internal Line(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x0011F049 File Offset: 0x0011D249
		internal Line(int id, ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x0011F053 File Offset: 0x0011D253
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Line;
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x0011F056 File Offset: 0x0011D256
		// (set) Token: 0x060044B5 RID: 17589 RVA: 0x0011F05E File Offset: 0x0011D25E
		internal bool LineSlant
		{
			get
			{
				return this.m_slanted;
			}
			set
			{
				this.m_slanted = value;
			}
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x0011F068 File Offset: 0x0011D268
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.LineStart(this.m_name);
			base.Initialize(context);
			double heightValue = this.m_heightValue;
			double widthValue = this.m_widthValue;
			double topValue = this.m_topValue;
			double leftValue = this.m_leftValue;
			if ((0.0 > ReportItem.RoundSize(heightValue) && 0.0 <= ReportItem.RoundSize(widthValue)) || (0.0 > ReportItem.RoundSize(widthValue) && 0.0 <= ReportItem.RoundSize(heightValue)))
			{
				this.m_slanted = true;
			}
			this.m_heightValue = Math.Abs(heightValue);
			this.m_widthValue = Math.Abs(widthValue);
			if (0.0 <= heightValue)
			{
				this.m_topValue = topValue;
			}
			else
			{
				this.m_topValue = topValue + heightValue;
				if (0.0 > this.m_topValue)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsNegativeTopHeight, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				}
			}
			if (0.0 <= widthValue)
			{
				this.m_leftValue = leftValue;
			}
			else
			{
				this.m_leftValue = leftValue + widthValue;
				if (0.0 > this.m_leftValue)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsNegativeLeftWidth, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				}
			}
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			base.ExprHostID = context.ExprHostBuilder.LineEnd();
			return true;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x0011F1FC File Offset: 0x0011D3FC
		internal override void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			if (overwrite)
			{
				this.m_top = "0mm";
				this.m_topValue = 0.0;
				this.m_left = "0mm";
				this.m_leftValue = 0.0;
			}
			if (this.m_width == null || (overwrite && this.m_widthValue > 0.0 && this.m_widthValue != width))
			{
				this.m_width = width.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
			}
			if (this.m_height == null || (overwrite && this.m_heightValue > 0.0 && this.m_heightValue != height))
			{
				this.m_height = height.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
			}
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x0011F2FF File Offset: 0x0011D4FF
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			return base.PublishClone(context);
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x0011F308 File Offset: 0x0011D508
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Line, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Slanted, Token.Boolean)
			});
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x0011F340 File Offset: 0x0011D540
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Line.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Slanted)
				{
					writer.Write(this.m_slanted);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x0011F398 File Offset: 0x0011D598
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Line.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Slanted)
				{
					this.m_slanted = reader.ReadBoolean();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x0011F3F0 File Offset: 0x0011D5F0
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x0011F3FA File Offset: 0x0011D5FA
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Line;
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x0011F404 File Offset: 0x0011D604
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.LineHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x04001F17 RID: 7959
		private bool m_slanted;

		// Token: 0x04001F18 RID: 7960
		private const string ZeroSize = "0mm";

		// Token: 0x04001F19 RID: 7961
		[NonSerialized]
		private ReportItemExprHost m_exprHost;

		// Token: 0x04001F1A RID: 7962
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Line.GetDeclaration();
	}
}
