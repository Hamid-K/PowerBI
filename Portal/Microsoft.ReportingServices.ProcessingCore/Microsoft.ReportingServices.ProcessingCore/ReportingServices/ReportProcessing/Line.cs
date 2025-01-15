using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E2 RID: 1762
	[Serializable]
	internal sealed class Line : ReportItem
	{
		// Token: 0x06005FFB RID: 24571 RVA: 0x001837A0 File Offset: 0x001819A0
		internal Line(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x001837A9 File Offset: 0x001819A9
		internal Line(int id, ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x001837B3 File Offset: 0x001819B3
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Line;
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x06005FFE RID: 24574 RVA: 0x001837B6 File Offset: 0x001819B6
		// (set) Token: 0x06005FFF RID: 24575 RVA: 0x001837BE File Offset: 0x001819BE
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

		// Token: 0x06006000 RID: 24576 RVA: 0x001837C8 File Offset: 0x001819C8
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
			if ((0.0 > heightValue && 0.0 <= widthValue) || (0.0 > widthValue && 0.0 <= heightValue))
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
				this.m_visibility.Initialize(context, false, false);
			}
			base.ExprHostID = context.ExprHostBuilder.LineEnd();
			return true;
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x0018394C File Offset: 0x00181B4C
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.LineHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x0018399C File Offset: 0x00181B9C
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

		// Token: 0x06006003 RID: 24579 RVA: 0x00183AA0 File Offset: 0x00181CA0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.Slanted, Token.Boolean)
			});
		}

		// Token: 0x040030CA RID: 12490
		private bool m_slanted;

		// Token: 0x040030CB RID: 12491
		private const string ZeroSize = "0mm";

		// Token: 0x040030CC RID: 12492
		[NonSerialized]
		private ReportItemExprHost m_exprHost;
	}
}
