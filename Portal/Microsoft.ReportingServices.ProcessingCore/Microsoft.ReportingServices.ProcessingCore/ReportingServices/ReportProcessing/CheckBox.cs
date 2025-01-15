using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E5 RID: 1765
	[Serializable]
	internal sealed class CheckBox : ReportItem
	{
		// Token: 0x06006031 RID: 24625 RVA: 0x0018422E File Offset: 0x0018242E
		internal CheckBox(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x00184237 File Offset: 0x00182437
		internal CheckBox(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_height = "3.175mm";
			this.m_width = "3.175mm";
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x00184257 File Offset: 0x00182457
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Checkbox;
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06006034 RID: 24628 RVA: 0x0018425A File Offset: 0x0018245A
		// (set) Token: 0x06006035 RID: 24629 RVA: 0x00184262 File Offset: 0x00182462
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

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06006036 RID: 24630 RVA: 0x0018426B File Offset: 0x0018246B
		// (set) Token: 0x06006037 RID: 24631 RVA: 0x00184273 File Offset: 0x00182473
		internal string HideDuplicates
		{
			get
			{
				return this.m_hideDuplicates;
			}
			set
			{
				this.m_hideDuplicates = value;
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06006038 RID: 24632 RVA: 0x0018427C File Offset: 0x0018247C
		// (set) Token: 0x06006039 RID: 24633 RVA: 0x00184284 File Offset: 0x00182484
		internal bool OldValue
		{
			get
			{
				return this.m_oldValue;
			}
			set
			{
				this.m_oldValue = value;
				this.m_hasOldValue = true;
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x00184294 File Offset: 0x00182494
		// (set) Token: 0x0600603B RID: 24635 RVA: 0x0018429C File Offset: 0x0018249C
		internal bool HasOldValue
		{
			get
			{
				return this.m_hasOldValue;
			}
			set
			{
				this.m_hasOldValue = value;
			}
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x001842A8 File Offset: 0x001824A8
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, false);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
			}
			if (this.m_hideDuplicates != null)
			{
				context.ValidateHideDuplicateScope(this.m_hideDuplicates, this);
			}
			return true;
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x0018431D File Offset: 0x0018251D
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(false, string.Empty);
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x00184330 File Offset: 0x00182530
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HideDuplicates, Token.String)
			});
		}

		// Token: 0x040030DA RID: 12506
		private ExpressionInfo m_value;

		// Token: 0x040030DB RID: 12507
		private string m_hideDuplicates;

		// Token: 0x040030DC RID: 12508
		[NonSerialized]
		private bool m_oldValue;

		// Token: 0x040030DD RID: 12509
		[NonSerialized]
		private bool m_hasOldValue;
	}
}
