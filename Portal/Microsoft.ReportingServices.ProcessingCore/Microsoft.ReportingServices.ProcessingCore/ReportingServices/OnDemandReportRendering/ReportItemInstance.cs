using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200030B RID: 779
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportItemInstance : ReportElementInstance
	{
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00071EF4 File Offset: 0x000700F4
		internal ReportItemInstance(Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00071F00 File Offset: 0x00070100
		public virtual string UniqueName
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot)
				{
					return this.m_reportElementDef.RenderReportItem.UniqueName;
				}
				if (this.m_uniqueName == null)
				{
					this.m_uniqueName = this.m_reportElementDef.ReportItemDef.UniqueName;
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06001CB3 RID: 7347 RVA: 0x00071F50 File Offset: 0x00070150
		// (set) Token: 0x06001CB4 RID: 7348 RVA: 0x00071FF8 File Offset: 0x000701F8
		public string ToolTip
		{
			get
			{
				if (!this.m_toolTipEvaluated)
				{
					this.m_toolTipEvaluated = true;
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_toolTip = this.m_reportElementDef.RenderReportItem.ToolTip;
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef = this.m_reportElementDef.ReportItemDef;
						if (reportItemDef.ToolTip != null)
						{
							if (!reportItemDef.ToolTip.IsExpression)
							{
								this.m_toolTip = reportItemDef.ToolTip.StringValue;
							}
							else if (this.m_reportElementDef.CriOwner == null)
							{
								this.m_toolTip = reportItemDef.EvaluateToolTip(this.ReportScopeInstance, this.RenderingContext.OdpContext);
							}
						}
					}
				}
				return this.m_toolTip;
			}
			set
			{
				if (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.m_reportElementDef).ToolTip.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_toolTipEvaluated = true;
				this.m_toolTip = value;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x00072050 File Offset: 0x00070250
		// (set) Token: 0x06001CB6 RID: 7350 RVA: 0x000720F8 File Offset: 0x000702F8
		public string Bookmark
		{
			get
			{
				if (!this.m_bookmarkEvaluated)
				{
					this.m_bookmarkEvaluated = true;
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_bookmark = this.m_reportElementDef.RenderReportItem.Bookmark;
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef = this.m_reportElementDef.ReportItemDef;
						if (reportItemDef.Bookmark != null)
						{
							if (!reportItemDef.Bookmark.IsExpression)
							{
								this.m_bookmark = reportItemDef.Bookmark.StringValue;
							}
							else if (this.m_reportElementDef.CriOwner == null)
							{
								this.m_bookmark = reportItemDef.EvaluateBookmark(this.ReportScopeInstance, this.RenderingContext.OdpContext);
							}
						}
					}
				}
				return this.m_bookmark;
			}
			set
			{
				if (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.m_reportElementDef).Bookmark.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_bookmarkEvaluated = true;
				this.m_bookmark = value;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00072150 File Offset: 0x00070350
		// (set) Token: 0x06001CB8 RID: 7352 RVA: 0x000721F8 File Offset: 0x000703F8
		public string DocumentMapLabel
		{
			get
			{
				if (!this.m_documentMapLabelEvaluated)
				{
					this.m_documentMapLabelEvaluated = true;
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_documentMapLabel = this.m_reportElementDef.RenderReportItem.Label;
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef = this.m_reportElementDef.ReportItemDef;
						if (reportItemDef.DocumentMapLabel != null)
						{
							if (!reportItemDef.DocumentMapLabel.IsExpression)
							{
								this.m_documentMapLabel = reportItemDef.DocumentMapLabel.StringValue;
							}
							else if (this.m_reportElementDef.CriOwner == null)
							{
								this.m_documentMapLabel = reportItemDef.EvaluateDocumentMapLabel(this.ReportScopeInstance, this.RenderingContext.OdpContext);
							}
						}
					}
				}
				return this.m_documentMapLabel;
			}
			set
			{
				if (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (this.m_reportElementDef.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.m_reportElementDef).DocumentMapLabel.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_documentMapLabelEvaluated = true;
				this.m_documentMapLabel = value;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00072250 File Offset: 0x00070450
		public virtual VisibilityInstance Visibility
		{
			get
			{
				if (this.m_visibility == null && ((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.m_reportElementDef).Visibility != null)
				{
					this.m_visibility = new ReportItemVisibilityInstance(this.m_reportElementDef as Microsoft.ReportingServices.OnDemandReportRendering.ReportItem);
				}
				return this.m_visibility;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00072288 File Offset: 0x00070488
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_reportElementDef.RenderingContext;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00072295 File Offset: 0x00070495
		protected string GetDefaultFontFamily()
		{
			if (this.RenderingContext.OdpContext == null)
			{
				return null;
			}
			return this.RenderingContext.OdpContext.ReportDefinition.DefaultFontFamily;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000722BC File Offset: 0x000704BC
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_uniqueName = null;
			this.m_toolTipEvaluated = false;
			this.m_toolTip = null;
			this.m_bookmarkEvaluated = false;
			this.m_bookmark = null;
			this.m_documentMapLabelEvaluated = false;
			this.m_documentMapLabel = null;
			if (this.m_visibility != null)
			{
				this.m_visibility.SetNewContext();
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00072314 File Offset: 0x00070514
		internal override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ReportItemInstance.m_Declaration);
			Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = (Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)base.ReportElementDef;
			List<string> list;
			List<object> list2;
			reportItem.CustomProperties.GetDynamicValues(out list, out list2);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName == MemberName.Label)
					{
						string text = null;
						if (reportItem.DocumentMapLabel.IsExpression)
						{
							text = this.m_documentMapLabel;
						}
						writer.Write(text);
						continue;
					}
					if (memberName == MemberName.ToolTip)
					{
						string text2 = null;
						if (reportItem.ToolTip.IsExpression)
						{
							text2 = this.m_toolTip;
						}
						writer.Write(text2);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Bookmark)
					{
						string text3 = null;
						if (reportItem.Bookmark.IsExpression)
						{
							text3 = this.m_bookmark;
						}
						writer.Write(text3);
						continue;
					}
					if (memberName == MemberName.CustomPropertyNames)
					{
						writer.WriteListOfPrimitives<string>(list);
						continue;
					}
					if (memberName == MemberName.CustomPropertyValues)
					{
						writer.WriteListOfPrimitives<object>(list2);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0007242C File Offset: 0x0007062C
		internal override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ReportItemInstance.m_Declaration);
			Microsoft.ReportingServices.OnDemandReportRendering.ReportItem reportItem = (Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)base.ReportElementDef;
			List<string> list = null;
			List<object> list2 = null;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName != MemberName.Label)
					{
						if (memberName == MemberName.ToolTip)
						{
							string text = reader.ReadString();
							if (reportItem.ToolTip.IsExpression)
							{
								this.m_toolTip = text;
								continue;
							}
							Global.Tracer.Assert(text == null, "(toolTip == null)");
							continue;
						}
					}
					else
					{
						string text2 = reader.ReadString();
						if (reportItem.DocumentMapLabel.IsExpression)
						{
							this.m_documentMapLabel = text2;
							continue;
						}
						Global.Tracer.Assert(text2 == null, "(documentMapLabel == null)");
						continue;
					}
				}
				else if (memberName != MemberName.Bookmark)
				{
					if (memberName == MemberName.CustomPropertyNames)
					{
						list = reader.ReadListOfPrimitives<string>();
						continue;
					}
					if (memberName == MemberName.CustomPropertyValues)
					{
						list2 = reader.ReadListOfPrimitives<object>();
						continue;
					}
				}
				else
				{
					string text3 = reader.ReadString();
					if (reportItem.Bookmark.IsExpression)
					{
						this.m_bookmark = text3;
						continue;
					}
					Global.Tracer.Assert(text3 == null, "(bookmark == null)");
					continue;
				}
				Global.Tracer.Assert(false);
			}
			reportItem.CustomProperties.SetDynamicValues(list, list2);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00072591 File Offset: 0x00070791
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemInstance;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00072598 File Offset: 0x00070798
		[SkipMemberStaticValidation(MemberName.CustomPropertyNames)]
		[SkipMemberStaticValidation(MemberName.CustomPropertyValues)]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportElementInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ToolTip, Token.String),
				new MemberInfo(MemberName.Bookmark, Token.String),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.CustomPropertyNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
				new MemberInfo(MemberName.CustomPropertyValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Object)
			});
		}

		// Token: 0x04000F0C RID: 3852
		[NonSerialized]
		protected string m_uniqueName;

		// Token: 0x04000F0D RID: 3853
		private string m_toolTip;

		// Token: 0x04000F0E RID: 3854
		private string m_bookmark;

		// Token: 0x04000F0F RID: 3855
		private string m_documentMapLabel;

		// Token: 0x04000F10 RID: 3856
		[NonSerialized]
		private bool m_toolTipEvaluated;

		// Token: 0x04000F11 RID: 3857
		[NonSerialized]
		private bool m_bookmarkEvaluated;

		// Token: 0x04000F12 RID: 3858
		[NonSerialized]
		private bool m_documentMapLabelEvaluated;

		// Token: 0x04000F13 RID: 3859
		[NonSerialized]
		protected VisibilityInstance m_visibility;

		// Token: 0x04000F14 RID: 3860
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportItemInstance.GetDeclaration();
	}
}
