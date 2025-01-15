using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000324 RID: 804
	public sealed class CompiledParagraphInstance : ParagraphInstance, ICompiledParagraphInstance
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0007552E File Offset: 0x0007372E
		internal CompiledParagraphInstance(CompiledRichTextInstance compiledRichTextInstance)
			: base(compiledRichTextInstance.ParagraphDefinition)
		{
			this.m_compiledRichTextInstance = compiledRichTextInstance;
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00075543 File Offset: 0x00073743
		private InternalParagraphInstance NativeParagraphInstance
		{
			get
			{
				return (InternalParagraphInstance)base.Definition.Instance;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00075558 File Offset: 0x00073758
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

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000755A1 File Offset: 0x000737A1
		public override StyleInstance Style
		{
			get
			{
				return this.m_style;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x000755A9 File Offset: 0x000737A9
		public override ReportSize LeftIndent
		{
			get
			{
				if (this.m_leftIndent == null)
				{
					this.m_leftIndent = this.NativeParagraphInstance.GetLeftIndent(false);
				}
				return this.m_leftIndent;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x000755CB File Offset: 0x000737CB
		public override ReportSize RightIndent
		{
			get
			{
				if (this.m_rightIndent == null)
				{
					this.m_rightIndent = this.NativeParagraphInstance.GetRightIndent(false);
				}
				return this.m_rightIndent;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x000755ED File Offset: 0x000737ED
		public override ReportSize HangingIndent
		{
			get
			{
				if (this.m_hangingIndent == null)
				{
					this.m_hangingIndent = this.NativeParagraphInstance.GetHangingIndent(false);
				}
				return this.m_hangingIndent;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0007560F File Offset: 0x0007380F
		public override ListStyle ListStyle
		{
			get
			{
				if (this.m_listStyle == ListStyle.None)
				{
					return this.NativeParagraphInstance.ListStyle;
				}
				return this.m_listStyle;
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0007562B File Offset: 0x0007382B
		public override int ListLevel
		{
			get
			{
				return this.m_listLevel + this.NativeParagraphInstance.ListLevel;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0007563F File Offset: 0x0007383F
		public override ReportSize SpaceBefore
		{
			get
			{
				if (this.m_spaceBefore == null)
				{
					this.m_spaceBefore = this.NativeParagraphInstance.GetSpaceBefore(false);
				}
				return this.m_spaceBefore;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00075661 File Offset: 0x00073861
		public override ReportSize SpaceAfter
		{
			get
			{
				if (this.m_spaceAfter == null)
				{
					this.m_spaceAfter = this.NativeParagraphInstance.GetSpaceAfter(false);
				}
				return this.m_spaceAfter;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00075683 File Offset: 0x00073883
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x0007568B File Offset: 0x0007388B
		public CompiledTextRunInstanceCollection CompiledTextRunInstances
		{
			get
			{
				return this.m_compiledTextRunInstances;
			}
			internal set
			{
				this.m_compiledTextRunInstances = value;
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00075694 File Offset: 0x00073894
		internal TextRun TextRunDefinition
		{
			get
			{
				return this.m_compiledRichTextInstance.TextRunDefinition;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x000756A1 File Offset: 0x000738A1
		public override bool IsCompiled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x000756A4 File Offset: 0x000738A4
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x000756AC File Offset: 0x000738AC
		IList<ICompiledTextRunInstance> ICompiledParagraphInstance.CompiledTextRunInstances
		{
			get
			{
				return this.m_compiledTextRunInstances;
			}
			set
			{
				this.m_compiledTextRunInstances = (CompiledTextRunInstanceCollection)value;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x000756BA File Offset: 0x000738BA
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x000756C7 File Offset: 0x000738C7
		ICompiledStyleInstance ICompiledParagraphInstance.Style
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

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000756D5 File Offset: 0x000738D5
		// (set) Token: 0x06001DFB RID: 7675 RVA: 0x000756DD File Offset: 0x000738DD
		ReportSize ICompiledParagraphInstance.LeftIndent
		{
			get
			{
				return this.m_leftIndent;
			}
			set
			{
				this.m_leftIndent = value;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000756E6 File Offset: 0x000738E6
		// (set) Token: 0x06001DFD RID: 7677 RVA: 0x000756EE File Offset: 0x000738EE
		ReportSize ICompiledParagraphInstance.RightIndent
		{
			get
			{
				return this.m_rightIndent;
			}
			set
			{
				this.m_rightIndent = value;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000756F7 File Offset: 0x000738F7
		// (set) Token: 0x06001DFF RID: 7679 RVA: 0x000756FF File Offset: 0x000738FF
		ReportSize ICompiledParagraphInstance.HangingIndent
		{
			get
			{
				return this.m_hangingIndent;
			}
			set
			{
				this.m_hangingIndent = value;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x00075708 File Offset: 0x00073908
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x00075710 File Offset: 0x00073910
		ListStyle ICompiledParagraphInstance.ListStyle
		{
			get
			{
				return this.m_listStyle;
			}
			set
			{
				this.m_listStyle = value;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x00075719 File Offset: 0x00073919
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x00075721 File Offset: 0x00073921
		int ICompiledParagraphInstance.ListLevel
		{
			get
			{
				return this.m_listLevel;
			}
			set
			{
				this.m_listLevel = value;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0007572A File Offset: 0x0007392A
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x00075732 File Offset: 0x00073932
		ReportSize ICompiledParagraphInstance.SpaceBefore
		{
			get
			{
				return this.m_spaceBefore;
			}
			set
			{
				this.m_spaceBefore = value;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0007573B File Offset: 0x0007393B
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x00075743 File Offset: 0x00073943
		ReportSize ICompiledParagraphInstance.SpaceAfter
		{
			get
			{
				return this.m_spaceAfter;
			}
			set
			{
				this.m_spaceAfter = value;
			}
		}

		// Token: 0x04000F71 RID: 3953
		private CompiledRichTextInstance m_compiledRichTextInstance;

		// Token: 0x04000F72 RID: 3954
		private CompiledTextRunInstanceCollection m_compiledTextRunInstances;

		// Token: 0x04000F73 RID: 3955
		private ReportSize m_leftIndent;

		// Token: 0x04000F74 RID: 3956
		private ReportSize m_rightIndent;

		// Token: 0x04000F75 RID: 3957
		private ReportSize m_hangingIndent;

		// Token: 0x04000F76 RID: 3958
		private ListStyle m_listStyle;

		// Token: 0x04000F77 RID: 3959
		private int m_listLevel;

		// Token: 0x04000F78 RID: 3960
		private ReportSize m_spaceBefore;

		// Token: 0x04000F79 RID: 3961
		private ReportSize m_spaceAfter;
	}
}
