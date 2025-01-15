using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E4 RID: 1764
	[Serializable]
	internal sealed class Image : ReportItem, IActionOwner
	{
		// Token: 0x0600601C RID: 24604 RVA: 0x00183EDE File Offset: 0x001820DE
		internal Image(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x00183EE7 File Offset: 0x001820E7
		internal Image(int id, ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x0600601E RID: 24606 RVA: 0x00183EF1 File Offset: 0x001820F1
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Image;
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x00183EF4 File Offset: 0x001820F4
		// (set) Token: 0x06006020 RID: 24608 RVA: 0x00183EFC File Offset: 0x001820FC
		internal Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x00183F05 File Offset: 0x00182105
		// (set) Token: 0x06006022 RID: 24610 RVA: 0x00183F0D File Offset: 0x0018210D
		internal Image.SourceType Source
		{
			get
			{
				return this.m_source;
			}
			set
			{
				this.m_source = value;
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x00183F16 File Offset: 0x00182116
		// (set) Token: 0x06006024 RID: 24612 RVA: 0x00183F1E File Offset: 0x0018211E
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x00183F27 File Offset: 0x00182127
		// (set) Token: 0x06006026 RID: 24614 RVA: 0x00183F2F File Offset: 0x0018212F
		internal ExpressionInfo MIMEType
		{
			get
			{
				return this.m_MIMEType;
			}
			set
			{
				this.m_MIMEType = value;
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x06006027 RID: 24615 RVA: 0x00183F38 File Offset: 0x00182138
		// (set) Token: 0x06006028 RID: 24616 RVA: 0x00183F40 File Offset: 0x00182140
		internal Image.Sizings Sizing
		{
			get
			{
				return this.m_sizing;
			}
			set
			{
				this.m_sizing = value;
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x00183F49 File Offset: 0x00182149
		internal ImageExprHost ImageExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x00183F51 File Offset: 0x00182151
		Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x00183F59 File Offset: 0x00182159
		// (set) Token: 0x0600602C RID: 24620 RVA: 0x00183F61 File Offset: 0x00182161
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x00183F6C File Offset: 0x0018216C
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.ImageStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, false);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.GenericValue(this.m_value);
				if (ExpressionInfo.Types.Constant == this.m_value.Type && this.m_source == Image.SourceType.External && !context.ReportContext.IsSupportedProtocol(this.m_value.Value, true))
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsUnsupportedProtocol, Severity.Error, this.ObjectType, this.m_name, "Value", new string[]
					{
						this.m_value.Value,
						"http://, https://, ftp://, file:, mailto:, or news:"
					});
				}
			}
			if (this.m_MIMEType != null)
			{
				this.m_MIMEType.Initialize("MIMEType", context);
				context.ExprHostBuilder.ImageMIMEType(this.m_MIMEType);
			}
			if (Image.SourceType.Embedded == this.m_source)
			{
				Global.Tracer.Assert(this.m_value != null);
				PublishingValidator.ValidateEmbeddedImageName(this.m_value, context.EmbeddedImages, this.ObjectType, this.m_name, "Value", context.ErrorContext);
			}
			base.ExprHostID = context.ExprHostBuilder.ImageEnd();
			return true;
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x001840FC File Offset: 0x001822FC
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.ImageHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_action != null)
				{
					if (this.m_exprHost.ActionInfoHost != null)
					{
						this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
						return;
					}
					if (this.m_exprHost.ActionHost != null)
					{
						this.m_action.SetExprHost(this.m_exprHost.ActionHost, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x0018419E File Offset: 0x0018239E
		internal override void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, NonComputedUniqueNames nonCompNames)
		{
			if (this.m_action == null || nonCompNames == null)
			{
				return;
			}
			this.m_action.ProcessDrillthroughAction(processingContext, nonCompNames.UniqueName);
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x001841C0 File Offset: 0x001823C0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Source, Token.Enum),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MIMEType, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Sizing, Token.Enum)
			});
		}

		// Token: 0x040030D3 RID: 12499
		private Action m_action;

		// Token: 0x040030D4 RID: 12500
		private Image.SourceType m_source;

		// Token: 0x040030D5 RID: 12501
		private ExpressionInfo m_value;

		// Token: 0x040030D6 RID: 12502
		private ExpressionInfo m_MIMEType;

		// Token: 0x040030D7 RID: 12503
		private Image.Sizings m_sizing;

		// Token: 0x040030D8 RID: 12504
		[NonSerialized]
		private ImageExprHost m_exprHost;

		// Token: 0x040030D9 RID: 12505
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x02000CC2 RID: 3266
		internal enum SourceType
		{
			// Token: 0x04004E85 RID: 20101
			External,
			// Token: 0x04004E86 RID: 20102
			Embedded,
			// Token: 0x04004E87 RID: 20103
			Database
		}

		// Token: 0x02000CC3 RID: 3267
		public enum Sizings
		{
			// Token: 0x04004E89 RID: 20105
			AutoSize,
			// Token: 0x04004E8A RID: 20106
			Fit,
			// Token: 0x04004E8B RID: 20107
			FitProportional,
			// Token: 0x04004E8C RID: 20108
			Clip
		}
	}
}
