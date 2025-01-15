using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200020A RID: 522
	internal class CompiledParagraphInfo
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x00050E8F File Offset: 0x0004F08F
		internal CompiledParagraphInfo()
		{
			this.m_flatStore = new CompiledParagraphInfo.FlattenedPropertyStore();
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00050EA2 File Offset: 0x0004F0A2
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00050EAA File Offset: 0x0004F0AA
		internal HtmlElement.HtmlElementType ElementType
		{
			get
			{
				return this.m_elementType;
			}
			set
			{
				this.m_elementType = value;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00050EB3 File Offset: 0x0004F0B3
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00050EC0 File Offset: 0x0004F0C0
		internal int ListLevel
		{
			get
			{
				return this.m_flatStore.ListLevel;
			}
			set
			{
				this.m_flatStore.ListLevel = value;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00050ECE File Offset: 0x0004F0CE
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00050EDB File Offset: 0x0004F0DB
		internal ListStyle ListStyle
		{
			get
			{
				return this.m_flatStore.ListStyle;
			}
			set
			{
				this.m_flatStore.ListStyle = value;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00050EE9 File Offset: 0x0004F0E9
		internal ReportSize LeftIndent
		{
			get
			{
				if (this.m_leftIndentSet)
				{
					return this.m_leftIndent;
				}
				if (this.m_parentParagraph != null)
				{
					return this.m_parentParagraph.LeftIndent;
				}
				return null;
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00050F0F File Offset: 0x0004F10F
		internal void AddLeftIndent(ReportSize size)
		{
			this.m_leftIndent = ReportSize.SumSizes(this.LeftIndent, size);
			this.m_leftIndentSet = true;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00050F2A File Offset: 0x0004F12A
		internal ReportSize RightIndent
		{
			get
			{
				if (this.m_rightIndentSet)
				{
					return this.m_rightIndent;
				}
				if (this.m_parentParagraph != null)
				{
					return this.m_parentParagraph.RightIndent;
				}
				return null;
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00050F50 File Offset: 0x0004F150
		internal void AddRightIndent(ReportSize size)
		{
			this.m_rightIndent = ReportSize.SumSizes(this.RightIndent, size);
			this.m_rightIndentSet = true;
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00050F6B File Offset: 0x0004F16B
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00050F91 File Offset: 0x0004F191
		internal ReportSize HangingIndent
		{
			get
			{
				if (this.m_hangingIndentSet)
				{
					return this.m_hangingIndent;
				}
				if (this.m_parentParagraph != null)
				{
					return this.m_parentParagraph.HangingIndent;
				}
				return null;
			}
			set
			{
				this.m_hangingIndent = value;
				this.m_hangingIndentSet = true;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00050FA1 File Offset: 0x0004F1A1
		internal ReportSize MarginTop
		{
			get
			{
				return this.m_flatStore.MarginTop;
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00050FAE File Offset: 0x0004F1AE
		internal void UpdateMarginTop(ReportSize value)
		{
			this.m_flatStore.UpdateMarginTop(value);
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00050FBC File Offset: 0x0004F1BC
		internal ReportSize MarginBottom
		{
			get
			{
				if (this.m_marginBottomSet)
				{
					return this.m_marginBottom;
				}
				return null;
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00050FCE File Offset: 0x0004F1CE
		internal void AddMarginBottom(ReportSize size)
		{
			this.m_marginBottom = size;
			this.m_marginBottomSet = true;
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00050FDE File Offset: 0x0004F1DE
		internal ReportSize SpaceBefore
		{
			get
			{
				return this.m_flatStore.SpaceBefore;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00050FEC File Offset: 0x0004F1EC
		internal void AddSpaceBefore(ReportSize size)
		{
			ReportSize spaceBefore = this.m_flatStore.SpaceBefore;
			ReportSize reportSize = ReportSize.SumSizes(spaceBefore, size);
			this.m_hasSpaceBefore = reportSize != null && reportSize.ToMillimeters() > 0.0 && (spaceBefore == null || reportSize.ToMillimeters() != spaceBefore.ToMillimeters());
			this.m_flatStore.SpaceBefore = reportSize;
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0005104D File Offset: 0x0004F24D
		internal ReportSize SpaceAfter
		{
			get
			{
				if (this.m_spaceAfterSet)
				{
					return this.m_spaceAfter;
				}
				return null;
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0005105F File Offset: 0x0004F25F
		internal void AddSpaceAfter(ReportSize size)
		{
			if (this.m_spaceAfterSet)
			{
				this.m_spaceAfter = ReportSize.SumSizes(this.m_spaceAfter, size);
				return;
			}
			this.m_spaceAfter = size;
			this.m_spaceAfterSet = true;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0005108C File Offset: 0x0004F28C
		internal CompiledParagraphInfo CreateChildParagraph(HtmlElement.HtmlElementType elementType)
		{
			CompiledParagraphInfo compiledParagraphInfo = new CompiledParagraphInfo();
			compiledParagraphInfo.ElementType = elementType;
			compiledParagraphInfo.m_parentParagraph = this;
			compiledParagraphInfo.m_flatStore = this.m_flatStore;
			this.m_childParagraph = compiledParagraphInfo;
			return compiledParagraphInfo;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000510C4 File Offset: 0x0004F2C4
		internal CompiledParagraphInfo RemoveAll()
		{
			CompiledParagraphInfo compiledParagraphInfo = this;
			while (compiledParagraphInfo.m_parentParagraph != null)
			{
				compiledParagraphInfo = compiledParagraphInfo.RemoveParagraph(compiledParagraphInfo.ElementType);
			}
			this.ApplyPendingMargins();
			compiledParagraphInfo.ResetParagraph();
			return compiledParagraphInfo;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000510F8 File Offset: 0x0004F2F8
		internal CompiledParagraphInfo RemoveParagraph(HtmlElement.HtmlElementType elementType)
		{
			if (this.m_elementType == elementType)
			{
				this.ApplySpaceAfter();
				if (this.m_parentParagraph != null)
				{
					this.m_parentParagraph.m_childParagraph = null;
					return this.m_parentParagraph;
				}
				this.ResetParagraph();
			}
			else if (this.m_parentParagraph != null)
			{
				this.m_parentParagraph.InternalRemoveParagraph(elementType);
			}
			return this;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0005114C File Offset: 0x0004F34C
		internal void InternalRemoveParagraph(HtmlElement.HtmlElementType elementType)
		{
			if (this.m_elementType == elementType)
			{
				this.ApplySpaceAfter();
				if (this.m_parentParagraph != null)
				{
					this.m_parentParagraph.m_childParagraph = this.m_childParagraph;
					this.m_childParagraph.m_parentParagraph = this.m_parentParagraph;
					return;
				}
				if (this.m_parentParagraph == null)
				{
					this.m_childParagraph.m_parentParagraph = null;
					return;
				}
			}
			else if (this.m_parentParagraph != null)
			{
				this.m_parentParagraph.InternalRemoveParagraph(elementType);
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000511BC File Offset: 0x0004F3BC
		private void ApplySpaceAfter()
		{
			ReportSize spaceAfter = this.SpaceAfter;
			if (this.m_lastParagraph != null)
			{
				this.m_flatStore.UpdatePendingMarginBottom(this.MarginBottom);
				this.AddToParagraphSpaceAfter(this.m_lastParagraph, spaceAfter);
				return;
			}
			if (this.IsNonEmptySize(spaceAfter) || this.m_hasSpaceBefore)
			{
				this.ApplyPendingMargins();
				this.AddSpaceBefore(this.MarginTop);
				this.m_flatStore.ClearMarginTop();
				this.AddSpaceBefore(spaceAfter);
				this.m_flatStore.UpdatePendingMarginBottom(this.MarginBottom);
				return;
			}
			this.m_flatStore.UpdateMarginTop(this.MarginBottom);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00051250 File Offset: 0x0004F450
		private void ApplyPendingMargins()
		{
			ICompiledParagraphInstance lastPopulatedParagraph = this.m_flatStore.LastPopulatedParagraph;
			ReportSize pendingMarginBottom = this.m_flatStore.PendingMarginBottom;
			if (this.IsNonEmptySize(pendingMarginBottom))
			{
				if (lastPopulatedParagraph != null)
				{
					ReportSize marginTop = this.m_flatStore.MarginTop;
					if (marginTop == null)
					{
						this.AddToParagraphSpaceAfter(lastPopulatedParagraph, pendingMarginBottom);
					}
					else if (pendingMarginBottom.ToMillimeters() >= marginTop.ToMillimeters())
					{
						this.AddToParagraphSpaceAfter(lastPopulatedParagraph, pendingMarginBottom);
						this.m_flatStore.ClearMarginTop();
					}
				}
				else
				{
					this.m_flatStore.UpdateMarginTop(pendingMarginBottom);
				}
				this.m_flatStore.ClearPendingMarginBottom();
			}
			this.m_flatStore.LastPopulatedParagraph = null;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000512E0 File Offset: 0x0004F4E0
		private void AddToParagraphSpaceAfter(ICompiledParagraphInstance paragraphInstance, ReportSize additionalSpace)
		{
			paragraphInstance.SpaceAfter = ReportSize.SumSizes(paragraphInstance.SpaceAfter, additionalSpace);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000512F4 File Offset: 0x0004F4F4
		private bool IsNonEmptySize(ReportSize size)
		{
			return size != null && size.ToMillimeters() > 0.0;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0005130C File Offset: 0x0004F50C
		private void ResetParagraph()
		{
			this.m_leftIndentSet = false;
			this.m_rightIndentSet = false;
			this.m_hangingIndentSet = false;
			this.m_spaceAfterSet = false;
			this.m_marginBottomSet = false;
			this.m_hasSpaceBefore = false;
			this.m_lastParagraph = null;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00051340 File Offset: 0x0004F540
		internal void PopulateParagraph(ICompiledParagraphInstance paragraphInstance)
		{
			this.ApplyPendingMargins();
			this.m_flatStore.LastPopulatedParagraph = paragraphInstance;
			paragraphInstance.ListStyle = this.m_flatStore.ListStyle;
			this.m_flatStore.ListStyle = ListStyle.None;
			paragraphInstance.ListLevel = this.m_flatStore.ListLevel;
			ReportSize leftIndent = this.LeftIndent;
			if (leftIndent != null)
			{
				paragraphInstance.LeftIndent = leftIndent;
			}
			ReportSize rightIndent = this.RightIndent;
			if (rightIndent != null)
			{
				paragraphInstance.RightIndent = rightIndent;
			}
			ReportSize hangingIndent = this.HangingIndent;
			if (hangingIndent != null)
			{
				paragraphInstance.HangingIndent = hangingIndent;
			}
			ReportSize marginTop = this.MarginTop;
			ReportSize spaceBefore = this.SpaceBefore;
			if (spaceBefore != null || marginTop != null)
			{
				paragraphInstance.SpaceBefore = ReportSize.SumSizes(marginTop, spaceBefore);
				this.m_flatStore.SpaceBefore = null;
				this.m_flatStore.ClearMarginTop();
			}
			this.StoreLastParagraph(paragraphInstance);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00051402 File Offset: 0x0004F602
		private void StoreLastParagraph(ICompiledParagraphInstance paragraphInstance)
		{
			this.m_lastParagraph = paragraphInstance;
			if (this.m_parentParagraph != null)
			{
				this.m_parentParagraph.StoreLastParagraph(paragraphInstance);
			}
		}

		// Token: 0x0400094C RID: 2380
		private HtmlElement.HtmlElementType m_elementType;

		// Token: 0x0400094D RID: 2381
		private CompiledParagraphInfo m_parentParagraph;

		// Token: 0x0400094E RID: 2382
		private CompiledParagraphInfo m_childParagraph;

		// Token: 0x0400094F RID: 2383
		private ReportSize m_leftIndent;

		// Token: 0x04000950 RID: 2384
		private ReportSize m_rightIndent;

		// Token: 0x04000951 RID: 2385
		private ReportSize m_hangingIndent;

		// Token: 0x04000952 RID: 2386
		private ReportSize m_spaceAfter;

		// Token: 0x04000953 RID: 2387
		private ReportSize m_marginBottom;

		// Token: 0x04000954 RID: 2388
		private bool m_hasSpaceBefore;

		// Token: 0x04000955 RID: 2389
		private bool m_marginBottomSet;

		// Token: 0x04000956 RID: 2390
		private bool m_leftIndentSet;

		// Token: 0x04000957 RID: 2391
		private bool m_rightIndentSet;

		// Token: 0x04000958 RID: 2392
		private bool m_hangingIndentSet;

		// Token: 0x04000959 RID: 2393
		private bool m_spaceAfterSet;

		// Token: 0x0400095A RID: 2394
		private CompiledParagraphInfo.FlattenedPropertyStore m_flatStore;

		// Token: 0x0400095B RID: 2395
		private ICompiledParagraphInstance m_lastParagraph;

		// Token: 0x0200093D RID: 2365
		internal class FlattenedPropertyStore
		{
			// Token: 0x17002967 RID: 10599
			// (get) Token: 0x06007F8C RID: 32652 RVA: 0x0020DD78 File Offset: 0x0020BF78
			// (set) Token: 0x06007F8D RID: 32653 RVA: 0x0020DD80 File Offset: 0x0020BF80
			internal int ListLevel
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

			// Token: 0x17002968 RID: 10600
			// (get) Token: 0x06007F8E RID: 32654 RVA: 0x0020DD89 File Offset: 0x0020BF89
			// (set) Token: 0x06007F8F RID: 32655 RVA: 0x0020DD91 File Offset: 0x0020BF91
			internal ListStyle ListStyle
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

			// Token: 0x17002969 RID: 10601
			// (get) Token: 0x06007F90 RID: 32656 RVA: 0x0020DD9A File Offset: 0x0020BF9A
			// (set) Token: 0x06007F91 RID: 32657 RVA: 0x0020DDA2 File Offset: 0x0020BFA2
			internal ReportSize SpaceBefore
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

			// Token: 0x1700296A RID: 10602
			// (get) Token: 0x06007F92 RID: 32658 RVA: 0x0020DDAB File Offset: 0x0020BFAB
			internal ReportSize MarginTop
			{
				get
				{
					return this.m_marginTop;
				}
			}

			// Token: 0x06007F93 RID: 32659 RVA: 0x0020DDB3 File Offset: 0x0020BFB3
			internal void ClearMarginTop()
			{
				this.m_marginTop = null;
			}

			// Token: 0x06007F94 RID: 32660 RVA: 0x0020DDBC File Offset: 0x0020BFBC
			internal void UpdateMarginTop(ReportSize marginTop)
			{
				this.m_marginTop = this.GetLargest(this.m_marginTop, marginTop);
			}

			// Token: 0x06007F95 RID: 32661 RVA: 0x0020DDD1 File Offset: 0x0020BFD1
			internal void AddMarginTop(ReportSize margin)
			{
				this.m_marginTop = ReportSize.SumSizes(this.m_marginTop, margin);
			}

			// Token: 0x1700296B RID: 10603
			// (get) Token: 0x06007F96 RID: 32662 RVA: 0x0020DDE5 File Offset: 0x0020BFE5
			internal ReportSize PendingMarginBottom
			{
				get
				{
					return this.m_pendingMarginBottom;
				}
			}

			// Token: 0x06007F97 RID: 32663 RVA: 0x0020DDED File Offset: 0x0020BFED
			internal void ClearPendingMarginBottom()
			{
				this.m_pendingMarginBottom = null;
			}

			// Token: 0x06007F98 RID: 32664 RVA: 0x0020DDF6 File Offset: 0x0020BFF6
			internal void UpdatePendingMarginBottom(ReportSize marginBottom)
			{
				this.m_pendingMarginBottom = this.GetLargest(this.m_pendingMarginBottom, marginBottom);
			}

			// Token: 0x1700296C RID: 10604
			// (get) Token: 0x06007F99 RID: 32665 RVA: 0x0020DE0B File Offset: 0x0020C00B
			// (set) Token: 0x06007F9A RID: 32666 RVA: 0x0020DE13 File Offset: 0x0020C013
			internal ICompiledParagraphInstance LastPopulatedParagraph
			{
				get
				{
					return this.m_lastPopulatedParagraph;
				}
				set
				{
					this.m_lastPopulatedParagraph = value;
				}
			}

			// Token: 0x06007F9B RID: 32667 RVA: 0x0020DE1C File Offset: 0x0020C01C
			private ReportSize GetLargest(ReportSize size1, ReportSize size2)
			{
				if (size1 == null)
				{
					return size2;
				}
				if (size2 == null)
				{
					return size1;
				}
				if (size1.ToMillimeters() > size2.ToMillimeters())
				{
					return size1;
				}
				return size2;
			}

			// Token: 0x04004015 RID: 16405
			private ListStyle m_listStyle;

			// Token: 0x04004016 RID: 16406
			private int m_listLevel;

			// Token: 0x04004017 RID: 16407
			private ReportSize m_spaceBefore;

			// Token: 0x04004018 RID: 16408
			private ReportSize m_marginTop;

			// Token: 0x04004019 RID: 16409
			private ReportSize m_pendingMarginBottom;

			// Token: 0x0400401A RID: 16410
			private ICompiledParagraphInstance m_lastPopulatedParagraph;
		}
	}
}
