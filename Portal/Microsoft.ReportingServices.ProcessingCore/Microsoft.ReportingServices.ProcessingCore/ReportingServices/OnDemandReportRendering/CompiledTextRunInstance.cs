using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000325 RID: 805
	public sealed class CompiledTextRunInstance : TextRunInstance, ICompiledTextRunInstance
	{
		// Token: 0x06001E08 RID: 7688 RVA: 0x0007574C File Offset: 0x0007394C
		internal CompiledTextRunInstance(CompiledRichTextInstance compiledRichTextInstance)
			: base(compiledRichTextInstance.TextRunDefinition)
		{
			this.m_compiledRichTextInstance = compiledRichTextInstance;
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00075764 File Offset: 0x00073964
		public override string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					this.m_uniqueName = this.m_reportElementDef.InstanceUniqueName + "x" + this.m_compiledRichTextInstance.GenerateID().ToString();
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x000757AD File Offset: 0x000739AD
		public override StyleInstance Style
		{
			get
			{
				return this.m_style;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x000757B5 File Offset: 0x000739B5
		public override string Value
		{
			get
			{
				return this.m_value ?? "";
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x000757C6 File Offset: 0x000739C6
		public override object OriginalValue
		{
			get
			{
				return this.m_value ?? "";
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x000757D7 File Offset: 0x000739D7
		public override string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = base.Definition.Instance.ToolTip;
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x000757FD File Offset: 0x000739FD
		public override MarkupType MarkupType
		{
			get
			{
				return this.m_markupType;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00075808 File Offset: 0x00073A08
		public ActionInstance ActionInstance
		{
			get
			{
				if (this.m_actionInstance == null && base.Definition.ActionInfo != null)
				{
					ActionCollection actions = base.Definition.ActionInfo.Actions;
					if (actions != null && actions.Count > 0)
					{
						this.m_actionInstance = actions[0].Instance;
					}
				}
				return this.m_actionInstance;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0007585F File Offset: 0x00073A5F
		public override TypeCode TypeCode
		{
			get
			{
				return TypeCode.String;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00075863 File Offset: 0x00073A63
		public override bool IsCompiled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00075866 File Offset: 0x00073A66
		public override bool ProcessedWithError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00075869 File Offset: 0x00073A69
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x00075876 File Offset: 0x00073A76
		ICompiledStyleInstance ICompiledTextRunInstance.Style
		{
			get
			{
				return (ICompiledStyleInstance)this.m_style;
			}
			set
			{
				this.m_style = (CompiledRichTextStyleInstance)value;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00075884 File Offset: 0x00073A84
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x0007588C File Offset: 0x00073A8C
		string ICompiledTextRunInstance.Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				if (value == null)
				{
					this.m_label = string.Empty;
					return;
				}
				this.m_label = value;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000758A4 File Offset: 0x00073AA4
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x000758AC File Offset: 0x00073AAC
		string ICompiledTextRunInstance.Value
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

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x000758B5 File Offset: 0x00073AB5
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x000758BD File Offset: 0x00073ABD
		string ICompiledTextRunInstance.ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				if (value == null)
				{
					this.m_toolTip = string.Empty;
					return;
				}
				this.m_toolTip = value;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000758D5 File Offset: 0x00073AD5
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x000758DD File Offset: 0x00073ADD
		MarkupType ICompiledTextRunInstance.MarkupType
		{
			get
			{
				return this.m_markupType;
			}
			set
			{
				this.m_markupType = value;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x000758E6 File Offset: 0x00073AE6
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x000758EE File Offset: 0x00073AEE
		IActionInstance ICompiledTextRunInstance.ActionInstance
		{
			get
			{
				return this.m_actionInstance;
			}
			set
			{
				this.m_actionInstance = (ActionInstance)value;
			}
		}

		// Token: 0x04000F7A RID: 3962
		private CompiledRichTextInstance m_compiledRichTextInstance;

		// Token: 0x04000F7B RID: 3963
		private MarkupType m_markupType;

		// Token: 0x04000F7C RID: 3964
		private string m_toolTip;

		// Token: 0x04000F7D RID: 3965
		private string m_label;

		// Token: 0x04000F7E RID: 3966
		private string m_value;

		// Token: 0x04000F7F RID: 3967
		private ActionInstance m_actionInstance;
	}
}
