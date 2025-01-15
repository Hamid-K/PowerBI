using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000518 RID: 1304
	[Serializable]
	public sealed class Visibility : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x00122B36 File Offset: 0x00120D36
		// (set) Token: 0x0600455E RID: 17758 RVA: 0x00122B3E File Offset: 0x00120D3E
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x00122B47 File Offset: 0x00120D47
		// (set) Token: 0x06004560 RID: 17760 RVA: 0x00122B4F File Offset: 0x00120D4F
		internal string Toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x00122B58 File Offset: 0x00120D58
		// (set) Token: 0x06004562 RID: 17762 RVA: 0x00122B60 File Offset: 0x00120D60
		internal Microsoft.ReportingServices.ReportIntermediateFormat.TextBox ToggleSender
		{
			get
			{
				return this.m_toggleSender;
			}
			set
			{
				this.m_toggleSender = value;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x00122B69 File Offset: 0x00120D69
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x00122B71 File Offset: 0x00120D71
		internal bool RecursiveReceiver
		{
			get
			{
				return this.m_recursiveReceiver;
			}
			set
			{
				this.m_recursiveReceiver = value;
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x00122B7A File Offset: 0x00120D7A
		// (set) Token: 0x06004566 RID: 17766 RVA: 0x00122B82 File Offset: 0x00120D82
		internal TablixMember RecursiveMember
		{
			get
			{
				return this.m_recursiveMember;
			}
			set
			{
				this.m_recursiveMember = value;
			}
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x00122B8B File Offset: 0x00120D8B
		internal bool IsToggleReceiver
		{
			get
			{
				return this.m_toggle != null && this.m_toggle.Length > 0;
			}
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x00122BA5 File Offset: 0x00120DA5
		internal bool IsConditional
		{
			get
			{
				return this.m_hidden != null;
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x00122BB0 File Offset: 0x00120DB0
		internal bool IsClone
		{
			get
			{
				return this.m_isClone;
			}
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x00122BB8 File Offset: 0x00120DB8
		internal void Initialize(InitializationContext context)
		{
			this.Initialize(context, true);
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x00122BC2 File Offset: 0x00120DC2
		internal void Initialize(InitializationContext context, bool registerVisibilityToggle)
		{
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.GenericVisibilityHidden(this.m_hidden);
			}
			if (registerVisibilityToggle)
			{
				this.RegisterVisibilityToggle(context);
			}
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00122BFA File Offset: 0x00120DFA
		internal VisibilityToggleInfo RegisterVisibilityToggle(InitializationContext context)
		{
			return context.RegisterVisibilityToggle(this);
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x00122C04 File Offset: 0x00120E04
		internal static SharedHiddenState GetSharedHidden(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			if (visibility == null)
			{
				return SharedHiddenState.Never;
			}
			if (visibility.Toggle == null)
			{
				if (visibility.Hidden == null)
				{
					return SharedHiddenState.Never;
				}
				if (ExpressionInfo.Types.Constant == visibility.Hidden.Type)
				{
					if (visibility.Hidden.BoolValue)
					{
						return SharedHiddenState.Always;
					}
					return SharedHiddenState.Never;
				}
			}
			return SharedHiddenState.Sometimes;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00122C3D File Offset: 0x00120E3D
		internal static bool HasToggle(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility)
		{
			return visibility != null && visibility.IsToggleReceiver;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00122C4C File Offset: 0x00120E4C
		internal object PublishClone(AutomaticSubtotalContext context, bool isSubtotalMember)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility;
			if (isSubtotalMember)
			{
				visibility = new Microsoft.ReportingServices.ReportIntermediateFormat.Visibility();
				visibility.m_hidden = ExpressionInfo.CreateConstExpression(true);
			}
			else
			{
				visibility = (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility)base.MemberwiseClone();
				if (this.m_hidden != null)
				{
					visibility.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
				}
				if (this.m_toggle != null)
				{
					context.AddVisibilityWithToggleToUpdate(visibility);
					visibility.m_toggle = (string)this.m_toggle.Clone();
				}
			}
			visibility.m_isClone = true;
			return visibility;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x00122CCB File Offset: 0x00120ECB
		internal void UpdateToggleItemReference(AutomaticSubtotalContext context)
		{
			if (this.m_toggle != null)
			{
				this.m_toggle = context.GetNewReportItemName(this.m_toggle);
			}
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x00122CE8 File Offset: 0x00120EE8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Visibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Toggle, Token.String),
				new MemberInfo(MemberName.RecursiveReceiver, Token.Boolean),
				new MemberInfo(MemberName.ToggleSender, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox, Token.Reference),
				new MemberInfo(MemberName.RecursiveMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember, Token.Reference)
			});
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x00122D74 File Offset: 0x00120F74
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Toggle)
				{
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
					if (memberName == MemberName.Toggle)
					{
						writer.Write(this.m_toggle);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RecursiveReceiver)
					{
						writer.Write(this.m_recursiveReceiver);
						continue;
					}
					if (memberName == MemberName.ToggleSender)
					{
						writer.WriteReference(this.m_toggleSender);
						continue;
					}
					if (memberName == MemberName.RecursiveMember)
					{
						writer.WriteReference(this.m_recursiveMember);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00122E38 File Offset: 0x00121038
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Toggle)
				{
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Toggle)
					{
						this.m_toggle = reader.ReadString();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RecursiveReceiver)
					{
						this.m_recursiveReceiver = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.ToggleSender)
					{
						this.m_toggleSender = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
						continue;
					}
					if (memberName == MemberName.RecursiveMember)
					{
						this.m_recursiveMember = reader.ReadReference<TablixMember>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00122F00 File Offset: 0x00121100
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable2;
					if (memberName != MemberName.ToggleSender)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						if (memberName != MemberName.RecursiveMember)
						{
							Global.Tracer.Assert(false);
						}
						else if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable))
						{
							this.m_recursiveMember = referenceable as TablixMember;
						}
					}
					else if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable2))
					{
						this.m_toggleSender = referenceable2 as Microsoft.ReportingServices.ReportIntermediateFormat.TextBox;
					}
				}
			}
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x00122FC0 File Offset: 0x001211C0
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Visibility;
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x00122FC8 File Offset: 0x001211C8
		internal static bool ComputeHidden(IVisibilityOwner visibilityOwner, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ToggleCascadeDirection direction, out bool valueIsDeep)
		{
			valueIsDeep = false;
			bool flag = false;
			Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility = visibilityOwner.Visibility;
			if (visibility != null)
			{
				switch (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.GetSharedHidden(visibility))
				{
				case SharedHiddenState.Always:
					flag = true;
					break;
				case SharedHiddenState.Never:
					flag = false;
					break;
				case SharedHiddenState.Sometimes:
					flag = visibilityOwner.ComputeStartHidden(renderingContext);
					if (visibility.IsToggleReceiver)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.TextBox toggleSender = visibility.ToggleSender;
						Global.Tracer.Assert(toggleSender != null, "Missing Persisted Toggle Receiver -> Sender Link");
						string senderUniqueName = visibilityOwner.SenderUniqueName;
						if (senderUniqueName != null && renderingContext.IsSenderToggled(senderUniqueName))
						{
							flag = !flag;
						}
						if (!flag)
						{
							flag = Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.ComputeDeepHidden(flag, visibilityOwner, direction, renderingContext);
						}
						valueIsDeep = true;
					}
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
			return flag;
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00123070 File Offset: 0x00121270
		internal static bool ComputeDeepHidden(bool hidden, IVisibilityOwner visibilityOwner, ToggleCascadeDirection direction, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility = visibilityOwner.Visibility;
			if (hidden && (visibility == null || !visibility.IsToggleReceiver))
			{
				hidden = false;
			}
			if (!hidden && visibility != null && visibility.IsToggleReceiver)
			{
				if (visibility.RecursiveReceiver && visibilityOwner is TablixMember)
				{
					hidden |= ((TablixMember)visibilityOwner).ComputeToggleSenderDeepHidden(renderingContext);
				}
				else
				{
					hidden |= visibility.ToggleSender.ComputeDeepHidden(renderingContext, direction);
				}
			}
			if (!hidden && (visibility == null || !visibility.RecursiveReceiver) && visibilityOwner.ContainingDynamicVisibility != null)
			{
				hidden |= visibilityOwner.ContainingDynamicVisibility.ComputeDeepHidden(renderingContext, direction);
			}
			if (!hidden && direction != ToggleCascadeDirection.Column && visibilityOwner.ContainingDynamicRowVisibility != null)
			{
				hidden |= visibilityOwner.ContainingDynamicRowVisibility.ComputeDeepHidden(renderingContext, direction);
			}
			if (!hidden && direction != ToggleCascadeDirection.Row && visibilityOwner.ContainingDynamicColumnVisibility != null)
			{
				hidden |= visibilityOwner.ContainingDynamicColumnVisibility.ComputeDeepHidden(renderingContext, direction);
			}
			return hidden;
		}

		// Token: 0x04001F42 RID: 8002
		private ExpressionInfo m_hidden;

		// Token: 0x04001F43 RID: 8003
		private string m_toggle;

		// Token: 0x04001F44 RID: 8004
		private bool m_recursiveReceiver;

		// Token: 0x04001F45 RID: 8005
		private TablixMember m_recursiveMember;

		// Token: 0x04001F46 RID: 8006
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextBox m_toggleSender;

		// Token: 0x04001F47 RID: 8007
		[NonSerialized]
		private bool m_isClone;

		// Token: 0x04001F48 RID: 8008
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.GetDeclaration();
	}
}
