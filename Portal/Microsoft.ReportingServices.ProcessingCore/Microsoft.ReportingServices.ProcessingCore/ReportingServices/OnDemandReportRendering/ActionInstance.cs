using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000214 RID: 532
	public sealed class ActionInstance : BaseInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionInstance
	{
		// Token: 0x0600143C RID: 5180 RVA: 0x00052A3D File Offset: 0x00050C3D
		internal ActionInstance(IReportScope reportScope, Microsoft.ReportingServices.OnDemandReportRendering.Action actionDef)
			: base(reportScope)
		{
			this.m_isOldSnapshot = false;
			this.m_actionDef = actionDef;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00052A54 File Offset: 0x00050C54
		internal ActionInstance(Microsoft.ReportingServices.ReportRendering.Action renderAction)
			: base(null)
		{
			this.m_isOldSnapshot = true;
			this.m_renderAction = renderAction;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00052A6C File Offset: 0x00050C6C
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00052B5C File Offset: 0x00050D5C
		public string Label
		{
			get
			{
				if (this.m_label == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderAction != null)
						{
							this.m_label = this.m_renderAction.Label;
						}
					}
					else if (this.m_actionDef.Label != null)
					{
						if (!this.m_actionDef.Label.IsExpression)
						{
							this.m_label = this.m_actionDef.Label.Value;
						}
						else if (this.m_actionDef.Owner.ReportElementOwner == null || this.m_actionDef.Owner.ReportElementOwner.CriOwner == null)
						{
							ActionInfo owner = this.m_actionDef.Owner;
							this.m_label = this.m_actionDef.ActionItemDef.EvaluateLabel(this.ReportScopeInstance, owner.RenderingContext.OdpContext, owner.InstancePath, owner.ObjectType, owner.ObjectName);
						}
					}
				}
				return this.m_label;
			}
			set
			{
				ReportElement reportElementOwner = this.m_actionDef.Owner.ReportElementOwner;
				if (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && this.m_actionDef.Label != null && !this.m_actionDef.Label.IsExpression))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_label = value;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00052BBC File Offset: 0x00050DBC
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00052CAC File Offset: 0x00050EAC
		public string BookmarkLink
		{
			get
			{
				if (this.m_bookmark == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderAction != null)
						{
							this.m_bookmark = this.m_renderAction.BookmarkLink;
						}
					}
					else if (this.m_actionDef.BookmarkLink != null)
					{
						if (!this.m_actionDef.BookmarkLink.IsExpression)
						{
							this.m_bookmark = this.m_actionDef.BookmarkLink.Value;
						}
						else if (this.m_actionDef.Owner.ReportElementOwner == null || this.m_actionDef.Owner.ReportElementOwner.CriOwner == null)
						{
							ActionInfo owner = this.m_actionDef.Owner;
							this.m_bookmark = this.m_actionDef.ActionItemDef.EvaluateBookmarkLink(this.ReportScopeInstance, owner.RenderingContext.OdpContext, owner.InstancePath, owner.ObjectType, owner.ObjectName);
						}
					}
				}
				return this.m_bookmark;
			}
			set
			{
				ReportElement reportElementOwner = this.m_actionDef.Owner.ReportElementOwner;
				if (!this.m_actionDef.Owner.IsChartConstruction && (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition && this.m_actionDef.BookmarkLink == null) || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && (this.m_actionDef.BookmarkLink == null || !this.m_actionDef.BookmarkLink.IsExpression))))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_bookmark = value;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00052D34 File Offset: 0x00050F34
		public ReportUrl Hyperlink
		{
			get
			{
				if (this.m_hyperlink == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderAction != null && this.m_renderAction.HyperLinkURL != null)
						{
							this.m_hyperlink = new ReportUrl(this.m_renderAction.HyperLinkURL);
						}
					}
					else if (this.m_actionDef.Hyperlink != null)
					{
						if (!this.m_actionDef.Hyperlink.IsExpression)
						{
							this.m_hyperlink = this.m_actionDef.Hyperlink.Value;
						}
						else if (this.m_actionDef.Owner.ReportElementOwner == null || this.m_actionDef.Owner.ReportElementOwner.CriOwner == null)
						{
							ActionInfo owner = this.m_actionDef.Owner;
							string text = this.m_actionDef.ActionItemDef.EvaluateHyperLinkURL(this.ReportScopeInstance, owner.RenderingContext.OdpContext, owner.InstancePath, owner.ObjectType, owner.ObjectName);
							((IActionInstance)this).SetHyperlinkText(text);
						}
					}
				}
				return this.m_hyperlink;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00052E3A File Offset: 0x0005103A
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x00052E44 File Offset: 0x00051044
		public string HyperlinkText
		{
			get
			{
				return this.m_hyperlinkText;
			}
			set
			{
				ReportElement reportElementOwner = this.m_actionDef.Owner.ReportElementOwner;
				if (!this.m_actionDef.Owner.IsChartConstruction && (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition && this.m_actionDef.Hyperlink == null) || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && (this.m_actionDef.Hyperlink == null || !this.m_actionDef.Hyperlink.IsExpression))))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				((IActionInstance)this).SetHyperlinkText(value);
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00052ECC File Offset: 0x000510CC
		void IActionInstance.SetHyperlinkText(string hyperlinkText)
		{
			this.m_hyperlinkText = hyperlinkText;
			if (this.m_hyperlinkText != null)
			{
				ActionInfo owner = this.m_actionDef.Owner;
				this.m_hyperlink = ReportUrl.BuildHyperlinkUrl(owner.RenderingContext, owner.ObjectType, owner.ObjectName, "Hyperlink", owner.RenderingContext.OdpContext.ReportContext, this.m_hyperlinkText);
				if (this.m_hyperlink == null)
				{
					this.m_hyperlinkText = null;
					return;
				}
			}
			else
			{
				this.m_hyperlink = null;
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00052F43 File Offset: 0x00051143
		internal void Update(Microsoft.ReportingServices.ReportRendering.Action newAction)
		{
			this.m_renderAction = newAction;
			this.m_label = null;
			this.m_bookmark = null;
			this.m_hyperlink = null;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00052F61 File Offset: 0x00051161
		protected override void ResetInstanceCache()
		{
			this.m_label = null;
			this.m_bookmark = null;
			this.m_hyperlink = null;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00052F78 File Offset: 0x00051178
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ActionInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Label)
				{
					if (memberName != MemberName.HyperLinkURL)
					{
						switch (memberName)
						{
						case MemberName.DrillthroughReportName:
						{
							string text = null;
							if (this.m_actionDef.Drillthrough != null && this.m_actionDef.Drillthrough.ReportName.IsExpression)
							{
								text = this.m_actionDef.Drillthrough.Instance.ReportName;
							}
							writer.Write(text);
							break;
						}
						case MemberName.DrillthroughParameters:
						{
							ParameterInstance[] array = null;
							if (this.m_actionDef.Drillthrough != null && this.m_actionDef.Drillthrough.Parameters != null)
							{
								array = new ParameterInstance[this.m_actionDef.Drillthrough.Parameters.Count];
								for (int i = 0; i < array.Length; i++)
								{
									array[i] = this.m_actionDef.Drillthrough.Parameters[i].Instance;
								}
							}
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array2 = array;
							writer.Write(array2);
							break;
						}
						case MemberName.BookmarkLink:
						{
							string text2 = null;
							if (this.m_actionDef.BookmarkLink != null && this.m_actionDef.BookmarkLink.IsExpression)
							{
								text2 = this.m_bookmark;
							}
							writer.Write(text2);
							break;
						}
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						string text3 = null;
						if (this.m_actionDef.Hyperlink != null && this.m_actionDef.Hyperlink.IsExpression)
						{
							text3 = this.m_hyperlinkText;
						}
						writer.Write(text3);
					}
				}
				else
				{
					string text4 = null;
					if (this.m_actionDef.Label.IsExpression)
					{
						text4 = this.m_label;
					}
					writer.Write(text4);
				}
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00053148 File Offset: 0x00051348
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ActionInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Label)
				{
					if (memberName != MemberName.HyperLinkURL)
					{
						switch (memberName)
						{
						case MemberName.DrillthroughReportName:
						{
							string text = reader.ReadString();
							if (this.m_actionDef.Drillthrough != null && this.m_actionDef.Drillthrough.ReportName.IsExpression)
							{
								this.m_actionDef.Drillthrough.Instance.ReportName = text;
							}
							else
							{
								Global.Tracer.Assert(text == null, "(reportName == null)");
							}
							break;
						}
						case MemberName.DrillthroughParameters:
						{
							ParameterCollection parameterCollection = null;
							if (this.m_actionDef.Drillthrough != null)
							{
								parameterCollection = this.m_actionDef.Drillthrough.Parameters;
							}
							((ROMInstanceObjectCreator)reader.PersistenceHelper).StartParameterInstancesDeserialization(parameterCollection);
							reader.ReadArrayOfRIFObjects<ParameterInstance>();
							((ROMInstanceObjectCreator)reader.PersistenceHelper).CompleteParameterInstancesDeserialization();
							break;
						}
						case MemberName.BookmarkLink:
						{
							string text2 = reader.ReadString();
							if (this.m_actionDef.BookmarkLink != null && this.m_actionDef.BookmarkLink.IsExpression)
							{
								this.m_bookmark = text2;
							}
							else
							{
								Global.Tracer.Assert(text2 == null, "(bookmarkLink == null)");
							}
							break;
						}
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						string text3 = reader.ReadString();
						if (this.m_actionDef.Hyperlink != null && this.m_actionDef.Hyperlink.IsExpression)
						{
							this.m_hyperlinkText = text3;
						}
						else
						{
							Global.Tracer.Assert(text3 == null, "(hyperlink == null)");
						}
					}
				}
				else
				{
					string text4 = reader.ReadString();
					if (this.m_actionDef.Label.IsExpression)
					{
						this.m_label = text4;
					}
					else
					{
						Global.Tracer.Assert(text4 == null, "(label == null)");
					}
				}
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0005333A File Offset: 0x0005153A
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00053347 File Offset: 0x00051547
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInstance;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00053350 File Offset: 0x00051550
		[SkipMemberStaticValidation(MemberName.DrillthroughReportName)]
		[SkipMemberStaticValidation(MemberName.DrillthroughParameters)]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.BookmarkLink, Token.String),
				new MemberInfo(MemberName.HyperLinkURL, Token.String),
				new MemberInfo(MemberName.DrillthroughReportName, Token.String),
				new MemberInfo(MemberName.DrillthroughParameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInstance)
			});
		}

		// Token: 0x0400098F RID: 2447
		[NonSerialized]
		private bool m_isOldSnapshot;

		// Token: 0x04000990 RID: 2448
		[NonSerialized]
		private Microsoft.ReportingServices.ReportRendering.Action m_renderAction;

		// Token: 0x04000991 RID: 2449
		[NonSerialized]
		private Microsoft.ReportingServices.OnDemandReportRendering.Action m_actionDef;

		// Token: 0x04000992 RID: 2450
		[NonSerialized]
		private ReportUrl m_hyperlink;

		// Token: 0x04000993 RID: 2451
		private string m_label;

		// Token: 0x04000994 RID: 2452
		private string m_bookmark;

		// Token: 0x04000995 RID: 2453
		private string m_hyperlinkText;

		// Token: 0x04000996 RID: 2454
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ActionInstance.GetDeclaration();
	}
}
