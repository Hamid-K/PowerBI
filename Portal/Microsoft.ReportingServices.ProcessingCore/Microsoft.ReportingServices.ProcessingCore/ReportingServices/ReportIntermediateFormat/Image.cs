using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004CC RID: 1228
	[Serializable]
	internal sealed class Image : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, IActionOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003E47 RID: 15943 RVA: 0x0010A4FF File Offset: 0x001086FF
		internal Image(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x0010A508 File Offset: 0x00108708
		internal Image(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x06003E49 RID: 15945 RVA: 0x0010A512 File Offset: 0x00108712
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Image;
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x0010A515 File Offset: 0x00108715
		// (set) Token: 0x06003E4B RID: 15947 RVA: 0x0010A51D File Offset: 0x0010871D
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
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

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x0010A526 File Offset: 0x00108726
		// (set) Token: 0x06003E4D RID: 15949 RVA: 0x0010A52E File Offset: 0x0010872E
		internal Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source
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

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x0010A537 File Offset: 0x00108737
		// (set) Token: 0x06003E4F RID: 15951 RVA: 0x0010A53F File Offset: 0x0010873F
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

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x0010A548 File Offset: 0x00108748
		// (set) Token: 0x06003E51 RID: 15953 RVA: 0x0010A550 File Offset: 0x00108750
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

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x0010A559 File Offset: 0x00108759
		// (set) Token: 0x06003E53 RID: 15955 RVA: 0x0010A561 File Offset: 0x00108761
		internal Microsoft.ReportingServices.OnDemandReportRendering.Image.Sizings Sizing
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

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x0010A56A File Offset: 0x0010876A
		// (set) Token: 0x06003E55 RID: 15957 RVA: 0x0010A572 File Offset: 0x00108772
		internal List<ExpressionInfo> Tags
		{
			get
			{
				return this.m_tags;
			}
			set
			{
				this.m_tags = value;
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x0010A57B File Offset: 0x0010877B
		// (set) Token: 0x06003E57 RID: 15959 RVA: 0x0010A583 File Offset: 0x00108783
		internal Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes EmbeddingMode
		{
			get
			{
				return this.m_embeddingMode;
			}
			set
			{
				this.m_embeddingMode = value;
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x0010A58C File Offset: 0x0010878C
		internal ImageExprHost ImageExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x06003E59 RID: 15961 RVA: 0x0010A594 File Offset: 0x00108794
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x0010A59C File Offset: 0x0010879C
		// (set) Token: 0x06003E5B RID: 15963 RVA: 0x0010A5A4 File Offset: 0x001087A4
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

		// Token: 0x06003E5C RID: 15964 RVA: 0x0010A5B0 File Offset: 0x001087B0
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.ImageStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.GenericValue(this.m_value);
				if (ExpressionInfo.Types.Constant == this.m_value.Type && this.m_source == Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.External && !context.ReportContext.IsSupportedProtocol(this.m_value.StringValue, true))
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsUnsupportedProtocol, Severity.Error, this.ObjectType, this.m_name, "Value", new string[]
					{
						this.m_value.StringValue,
						"http://, https://, ftp://, file:, mailto:, or news:"
					});
				}
			}
			if (this.m_MIMEType != null)
			{
				this.m_MIMEType.Initialize("MIMEType", context);
				context.ExprHostBuilder.ImageMIMEType(this.m_MIMEType);
			}
			if (this.m_tags != null)
			{
				for (int i = 0; i < this.m_tags.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_tags[i];
					expressionInfo.Initialize("Tag", context);
					ExpressionInfo.Types type = expressionInfo.Type;
				}
			}
			if (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Embedded == this.m_source && this.m_embeddingMode == Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Inline)
			{
				Global.Tracer.Assert(this.m_value != null, "(null != m_value)");
				Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateEmbeddedImageName(this.m_value, context.EmbeddedImages, this.ObjectType, this.m_name, "Value", context.ErrorContext);
			}
			base.ExprHostID = context.ExprHostBuilder.ImageEnd();
			return true;
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0010A788 File Offset: 0x00108988
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Image image = (Microsoft.ReportingServices.ReportIntermediateFormat.Image)base.PublishClone(context);
			if (this.m_action != null)
			{
				image.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_value != null)
			{
				image.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_MIMEType != null)
			{
				image.m_MIMEType = (ExpressionInfo)this.m_MIMEType.PublishClone(context);
			}
			if (this.m_tags != null)
			{
				image.m_tags = new List<ExpressionInfo>(this.m_tags.Count);
				foreach (ExpressionInfo expressionInfo in this.m_tags)
				{
					image.m_tags.Add((ExpressionInfo)expressionInfo.PublishClone(context));
				}
			}
			if (this.m_fieldsUsedInValueExpression != null)
			{
				image.m_fieldsUsedInValueExpression = new List<string>(this.m_fieldsUsedInValueExpression.Count);
				foreach (string text in this.m_fieldsUsedInValueExpression)
				{
					image.m_fieldsUsedInValueExpression.Add((string)text.Clone());
				}
			}
			return image;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x0010A8E8 File Offset: 0x00108AE8
		[SkipMemberStaticValidation(MemberName.Tag)]
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Image, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Source, Token.Enum),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MIMEType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Sizing, Token.Enum),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Tag, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.Spanning(100, 200)),
				new MemberInfo(MemberName.Tags, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.EmbeddingMode, Token.Enum, Lifetime.AddedIn(200))
			});
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x0010A9C8 File Offset: 0x00108BC8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Image.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.Value)
					{
						writer.Write(this.m_value);
						continue;
					}
					switch (memberName)
					{
					case MemberName.Source:
						writer.WriteEnum((int)this.m_source);
						continue;
					case MemberName.MIMEType:
						writer.Write(this.m_MIMEType);
						continue;
					case MemberName.Sizing:
						writer.WriteEnum((int)this.m_sizing);
						continue;
					default:
						if (memberName == MemberName.Action)
						{
							writer.Write(this.m_action);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Tag)
					{
						ExpressionInfo expressionInfo = null;
						if (this.m_tags != null && this.m_tags.Count > 0)
						{
							expressionInfo = this.m_tags[0];
						}
						writer.Write(expressionInfo);
						continue;
					}
					if (memberName == MemberName.EmbeddingMode)
					{
						writer.WriteEnum((int)this.m_embeddingMode);
						continue;
					}
					if (memberName == MemberName.Tags)
					{
						writer.Write<ExpressionInfo>(this.m_tags);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x0010AB04 File Offset: 0x00108D04
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Image.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.Value)
					{
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Source:
						this.m_source = (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType)reader.ReadEnum();
						continue;
					case MemberName.MIMEType:
						this.m_MIMEType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Sizing:
						this.m_sizing = (Microsoft.ReportingServices.OnDemandReportRendering.Image.Sizings)reader.ReadEnum();
						continue;
					default:
						if (memberName == MemberName.Action)
						{
							this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				else if (memberName != MemberName.Tag)
				{
					if (memberName == MemberName.EmbeddingMode)
					{
						this.m_embeddingMode = (Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.Tags)
					{
						this.m_tags = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
						continue;
					}
				}
				else
				{
					ExpressionInfo expressionInfo = (ExpressionInfo)reader.ReadRIFObject();
					if (expressionInfo != null)
					{
						this.m_tags = new List<ExpressionInfo>(1) { expressionInfo };
						continue;
					}
					continue;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x0010AC45 File Offset: 0x00108E45
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x0010AC4F File Offset: 0x00108E4F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Image;
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x0010AC58 File Offset: 0x00108E58
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.ImageHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
				{
					this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
				}
			}
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x0010ACD7 File Offset: 0x00108ED7
		internal bool ShouldTrackFieldsUsedInValue()
		{
			return this.Action != null && this.Action.TrackFieldsUsedInValueExpression;
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x0010ACEE File Offset: 0x00108EEE
		internal string EvaluateMimeTypeExpression(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateImageMIMETypeExpression(this);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x0010AD04 File Offset: 0x00108F04
		internal byte[] EvaluateBinaryValueExpression(IReportScopeInstance romInstance, OnDemandProcessingContext context, out bool errOccurred)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateImageBinaryValueExpression(this, out errOccurred);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x0010AD1B File Offset: 0x00108F1B
		internal string EvaluateStringValueExpression(IReportScopeInstance romInstance, OnDemandProcessingContext context, out bool errOccurred)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateImageStringValueExpression(this, out errOccurred);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x0010AD32 File Offset: 0x00108F32
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateTagExpression(ExpressionInfo tag, IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateImageTagExpression(this, tag);
		}

		// Token: 0x04001D03 RID: 7427
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001D04 RID: 7428
		private Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType m_source;

		// Token: 0x04001D05 RID: 7429
		private ExpressionInfo m_value;

		// Token: 0x04001D06 RID: 7430
		private ExpressionInfo m_MIMEType;

		// Token: 0x04001D07 RID: 7431
		private List<ExpressionInfo> m_tags;

		// Token: 0x04001D08 RID: 7432
		private Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes m_embeddingMode;

		// Token: 0x04001D09 RID: 7433
		private Microsoft.ReportingServices.OnDemandReportRendering.Image.Sizings m_sizing;

		// Token: 0x04001D0A RID: 7434
		[NonSerialized]
		private ImageExprHost m_exprHost;

		// Token: 0x04001D0B RID: 7435
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001D0C RID: 7436
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Image.GetDeclaration();

		// Token: 0x04001D0D RID: 7437
		[NonSerialized]
		internal static readonly byte[] TransparentImageBytes = new byte[]
		{
			71, 73, 70, 56, 57, 97, 1, 0, 1, 0,
			240, 0, 0, 219, 223, 239, 0, 0, 0, 33,
			249, 4, 1, 0, 0, 0, 0, 44, 0, 0,
			0, 0, 1, 0, 1, 0, 0, 2, 2, 68,
			1, 0, 59
		};
	}
}
