using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006DE RID: 1758
	[Serializable]
	public sealed class Visibility
	{
		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x06005FBB RID: 24507 RVA: 0x00182C93 File Offset: 0x00180E93
		// (set) Token: 0x06005FBC RID: 24508 RVA: 0x00182C9B File Offset: 0x00180E9B
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

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x06005FBD RID: 24509 RVA: 0x00182CA4 File Offset: 0x00180EA4
		// (set) Token: 0x06005FBE RID: 24510 RVA: 0x00182CAC File Offset: 0x00180EAC
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

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06005FBF RID: 24511 RVA: 0x00182CB5 File Offset: 0x00180EB5
		// (set) Token: 0x06005FC0 RID: 24512 RVA: 0x00182CBD File Offset: 0x00180EBD
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

		// Token: 0x06005FC1 RID: 24513 RVA: 0x00182CC8 File Offset: 0x00180EC8
		internal void Initialize(InitializationContext context, bool isContainer, bool tableRowCol)
		{
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				if (tableRowCol)
				{
					context.ExprHostBuilder.TableRowColVisibilityHiddenExpressionsExpr(this.m_hidden);
				}
				else
				{
					context.ExprHostBuilder.GenericVisibilityHidden(this.m_hidden);
				}
			}
			this.m_toggleItemInfo = this.RegisterReceiver(context, isContainer);
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x00182D25 File Offset: 0x00180F25
		internal ToggleItemInfo RegisterReceiver(InitializationContext context, bool isContainer)
		{
			if (context.RegisterHiddenReceiver)
			{
				return context.RegisterReceiver(this.m_toggle, this, isContainer);
			}
			return null;
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x00182D41 File Offset: 0x00180F41
		internal void UnRegisterReceiver(InitializationContext context)
		{
			if (this.m_toggleItemInfo != null)
			{
				context.UnRegisterReceiver(this.m_toggle, this.m_toggleItemInfo);
			}
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x00182D5E File Offset: 0x00180F5E
		internal static SharedHiddenState GetSharedHidden(Visibility visibility)
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

		// Token: 0x06005FC5 RID: 24517 RVA: 0x00182D97 File Offset: 0x00180F97
		internal static bool HasToggle(Visibility visibility)
		{
			return visibility != null && visibility.Toggle != null;
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x00182DAC File Offset: 0x00180FAC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Hidden, Token.Boolean),
				new MemberInfo(MemberName.Toggle, Token.String),
				new MemberInfo(MemberName.RecursiveReceiver, Token.Boolean)
			});
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x00182E08 File Offset: 0x00181008
		internal static bool IsOnePassHierarchyVisible(ReportItem reportItem)
		{
			return Visibility.IsOnePassVisible(reportItem) && (reportItem.Parent == null || Visibility.IsOnePassHierarchyVisible(reportItem.Parent));
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x00182E2C File Offset: 0x0018102C
		private static bool IsOnePassVisible(ReportItem reportItem)
		{
			if (reportItem == null)
			{
				return false;
			}
			if (reportItem.Visibility == null)
			{
				return true;
			}
			if (reportItem.Visibility.Toggle != null)
			{
				return false;
			}
			if (reportItem.Visibility.Hidden == null)
			{
				return true;
			}
			if (ExpressionInfo.Types.Constant == reportItem.Visibility.Hidden.Type)
			{
				return !reportItem.Visibility.Hidden.BoolValue;
			}
			return !reportItem.StartHidden;
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x00182E96 File Offset: 0x00181096
		internal static bool IsVisible(ReportItem reportItem)
		{
			return Visibility.IsVisible(reportItem, null, null);
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x00182EA0 File Offset: 0x001810A0
		internal static bool IsVisible(ReportItem reportItem, ReportItemInstance reportItemInstance, ReportItemInstanceInfo reportItemInstanceInfo)
		{
			if (reportItem == null)
			{
				return false;
			}
			bool flag = reportItemInstance != null && reportItemInstanceInfo != null && reportItemInstanceInfo.StartHidden;
			return Visibility.IsVisible(reportItem.Visibility, flag);
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x00182ECE File Offset: 0x001810CE
		internal static bool IsVisible(Visibility visibility, bool startHidden)
		{
			return visibility == null || visibility.Toggle != null || visibility.Hidden == null || ExpressionInfo.Types.Constant == visibility.Hidden.Type || !startHidden;
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x00182EFD File Offset: 0x001810FD
		internal static bool IsVisible(SharedHiddenState state, bool hidden, bool hasToggle)
		{
			return state == SharedHiddenState.Always || SharedHiddenState.Never == state || hasToggle || !hidden;
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x00182F14 File Offset: 0x00181114
		internal static bool IsTableCellVisible(bool[] tableColumnsVisible, int startIndex, int colSpan)
		{
			Global.Tracer.Assert(startIndex >= 0 && colSpan > 0 && tableColumnsVisible != null && startIndex + colSpan <= tableColumnsVisible.Length);
			bool flag = false;
			int num = 0;
			while (num < colSpan && !flag)
			{
				flag |= tableColumnsVisible[startIndex + num];
				num++;
			}
			return flag;
		}

		// Token: 0x040030B7 RID: 12471
		private ExpressionInfo m_hidden;

		// Token: 0x040030B8 RID: 12472
		private string m_toggle;

		// Token: 0x040030B9 RID: 12473
		private bool m_recursiveReceiver;

		// Token: 0x040030BA RID: 12474
		[NonSerialized]
		private ToggleItemInfo m_toggleItemInfo;
	}
}
