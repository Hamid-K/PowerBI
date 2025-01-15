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
	// Token: 0x02000427 RID: 1063
	[Serializable]
	internal sealed class MapTitle : MapDockableSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000D4E50 File Offset: 0x000D3050
		internal MapTitle()
		{
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000D4E58 File Offset: 0x000D3058
		internal MapTitle(Map map, int id)
			: base(map, id)
		{
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000D4E62 File Offset: 0x000D3062
		// (set) Token: 0x06002EEB RID: 12011 RVA: 0x000D4E6A File Offset: 0x000D306A
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000D4E73 File Offset: 0x000D3073
		// (set) Token: 0x06002EED RID: 12013 RVA: 0x000D4E7B File Offset: 0x000D307B
		internal ExpressionInfo Text
		{
			get
			{
				return this.m_text;
			}
			set
			{
				this.m_text = value;
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000D4E84 File Offset: 0x000D3084
		// (set) Token: 0x06002EEF RID: 12015 RVA: 0x000D4E8C File Offset: 0x000D308C
		internal ExpressionInfo Angle
		{
			get
			{
				return this.m_angle;
			}
			set
			{
				this.m_angle = value;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000D4E95 File Offset: 0x000D3095
		// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x000D4E9D File Offset: 0x000D309D
		internal ExpressionInfo TextShadowOffset
		{
			get
			{
				return this.m_textShadowOffset;
			}
			set
			{
				this.m_textShadowOffset = value;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000D4EA6 File Offset: 0x000D30A6
		internal new MapTitleExprHost ExprHost
		{
			get
			{
				return (MapTitleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000D4EB4 File Offset: 0x000D30B4
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapTitleStart(this.m_name);
			base.Initialize(context);
			if (this.m_text != null)
			{
				this.m_text.Initialize("Text", context);
				context.ExprHostBuilder.MapTitleText(this.m_text);
			}
			if (this.m_angle != null)
			{
				this.m_angle.Initialize("Angle", context);
				context.ExprHostBuilder.MapTitleAngle(this.m_angle);
			}
			if (this.m_textShadowOffset != null)
			{
				this.m_textShadowOffset.Initialize("TextShadowOffset", context);
				context.ExprHostBuilder.MapTitleTextShadowOffset(this.m_textShadowOffset);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapTitleEnd();
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000D4F70 File Offset: 0x000D3170
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapTitle mapTitle = (MapTitle)base.PublishClone(context);
			if (this.m_text != null)
			{
				mapTitle.m_text = (ExpressionInfo)this.m_text.PublishClone(context);
			}
			if (this.m_angle != null)
			{
				mapTitle.m_angle = (ExpressionInfo)this.m_angle.PublishClone(context);
			}
			if (this.m_textShadowOffset != null)
			{
				mapTitle.m_textShadowOffset = (ExpressionInfo)this.m_textShadowOffset.PublishClone(context);
			}
			return mapTitle;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000D4FE8 File Offset: 0x000D31E8
		internal void SetExprHost(MapTitleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000D500C File Offset: 0x000D320C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDockableSubItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Text, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Angle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextShadowOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000D5080 File Offset: 0x000D3280
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapTitle.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Text)
				{
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == MemberName.Text)
					{
						writer.Write(this.m_text);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Angle)
					{
						writer.Write(this.m_angle);
						continue;
					}
					if (memberName == MemberName.TextShadowOffset)
					{
						writer.Write(this.m_textShadowOffset);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000D512C File Offset: 0x000D332C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapTitle.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Text)
				{
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.Text)
					{
						this.m_text = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Angle)
					{
						this.m_angle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.TextShadowOffset)
					{
						this.m_textShadowOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000D51E9 File Offset: 0x000D33E9
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTitle;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000D51F0 File Offset: 0x000D33F0
		internal string EvaluateText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapTitleTextExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000D5230 File Offset: 0x000D3430
		internal double EvaluateAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapTitleAngleExpression(this, this.m_map.Name);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000D5256 File Offset: 0x000D3456
		internal string EvaluateTextShadowOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapTitleTextShadowOffsetExpression(this, this.m_map.Name);
		}

		// Token: 0x04001895 RID: 6293
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapTitle.GetDeclaration();

		// Token: 0x04001896 RID: 6294
		private ExpressionInfo m_text;

		// Token: 0x04001897 RID: 6295
		private ExpressionInfo m_angle;

		// Token: 0x04001898 RID: 6296
		private ExpressionInfo m_textShadowOffset;

		// Token: 0x04001899 RID: 6297
		private string m_name;
	}
}
