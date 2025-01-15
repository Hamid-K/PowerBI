using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006DD RID: 1757
	[Serializable]
	public abstract class ReportItem : IDOwner, ISearchByUniqueName, IComparable
	{
		// Token: 0x06005F4D RID: 24397 RVA: 0x00181E2B File Offset: 0x0018002B
		protected ReportItem(int id, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id)
		{
			this.m_parent = parent;
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00181E65 File Offset: 0x00180065
		protected ReportItem(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
		{
			this.m_parent = parent;
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06005F4F RID: 24399
		internal abstract ObjectType ObjectType { get; }

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06005F50 RID: 24400 RVA: 0x00181E9E File Offset: 0x0018009E
		// (set) Token: 0x06005F51 RID: 24401 RVA: 0x00181EA6 File Offset: 0x001800A6
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

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06005F52 RID: 24402 RVA: 0x00181EAF File Offset: 0x001800AF
		// (set) Token: 0x06005F53 RID: 24403 RVA: 0x00181EB7 File Offset: 0x001800B7
		internal Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x00181EC0 File Offset: 0x001800C0
		// (set) Token: 0x06005F55 RID: 24405 RVA: 0x00181EC8 File Offset: 0x001800C8
		internal string Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x00181ED1 File Offset: 0x001800D1
		// (set) Token: 0x06005F57 RID: 24407 RVA: 0x00181ED9 File Offset: 0x001800D9
		internal double TopValue
		{
			get
			{
				return this.m_topValue;
			}
			set
			{
				this.m_topValue = value;
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x00181EE2 File Offset: 0x001800E2
		// (set) Token: 0x06005F59 RID: 24409 RVA: 0x00181EEA File Offset: 0x001800EA
		internal string Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				this.m_left = value;
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x00181EF3 File Offset: 0x001800F3
		// (set) Token: 0x06005F5B RID: 24411 RVA: 0x00181EFB File Offset: 0x001800FB
		internal double LeftValue
		{
			get
			{
				return this.m_leftValue;
			}
			set
			{
				this.m_leftValue = value;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x00181F04 File Offset: 0x00180104
		// (set) Token: 0x06005F5D RID: 24413 RVA: 0x00181F0C File Offset: 0x0018010C
		internal string Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x06005F5E RID: 24414 RVA: 0x00181F15 File Offset: 0x00180115
		// (set) Token: 0x06005F5F RID: 24415 RVA: 0x00181F1D File Offset: 0x0018011D
		internal double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x00181F26 File Offset: 0x00180126
		// (set) Token: 0x06005F61 RID: 24417 RVA: 0x00181F2E File Offset: 0x0018012E
		internal string Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x00181F37 File Offset: 0x00180137
		// (set) Token: 0x06005F63 RID: 24419 RVA: 0x00181F3F File Offset: 0x0018013F
		internal double WidthValue
		{
			get
			{
				return this.m_widthValue;
			}
			set
			{
				this.m_widthValue = value;
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x00181F48 File Offset: 0x00180148
		internal double AbsoluteTopValue
		{
			get
			{
				if (this.m_heightValue < 0.0)
				{
					return Math.Round(this.m_topValue + this.m_heightValue, 1);
				}
				return Math.Round(this.m_topValue, 1);
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x06005F65 RID: 24421 RVA: 0x00181F7B File Offset: 0x0018017B
		internal double AbsoluteLeftValue
		{
			get
			{
				if (this.m_widthValue < 0.0)
				{
					return Math.Round(this.m_leftValue + this.m_widthValue, 1);
				}
				return Math.Round(this.m_leftValue, 1);
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x00181FAE File Offset: 0x001801AE
		internal double AbsoluteBottomValue
		{
			get
			{
				if (this.m_heightValue < 0.0)
				{
					return Math.Round(this.m_topValue, 1);
				}
				return Math.Round(this.m_topValue + this.m_heightValue, 1);
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x06005F67 RID: 24423 RVA: 0x00181FE1 File Offset: 0x001801E1
		internal double AbsoluteRightValue
		{
			get
			{
				if (this.m_widthValue < 0.0)
				{
					return Math.Round(this.m_leftValue, 1);
				}
				return Math.Round(this.m_leftValue + this.m_widthValue, 1);
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x06005F68 RID: 24424 RVA: 0x00182014 File Offset: 0x00180214
		// (set) Token: 0x06005F69 RID: 24425 RVA: 0x0018201C File Offset: 0x0018021C
		internal int ZIndex
		{
			get
			{
				return this.m_zIndex;
			}
			set
			{
				this.m_zIndex = value;
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x06005F6A RID: 24426 RVA: 0x00182025 File Offset: 0x00180225
		// (set) Token: 0x06005F6B RID: 24427 RVA: 0x0018202D File Offset: 0x0018022D
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x06005F6C RID: 24428 RVA: 0x00182036 File Offset: 0x00180236
		// (set) Token: 0x06005F6D RID: 24429 RVA: 0x0018203E File Offset: 0x0018023E
		internal Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x06005F6E RID: 24430 RVA: 0x00182047 File Offset: 0x00180247
		// (set) Token: 0x06005F6F RID: 24431 RVA: 0x0018204F File Offset: 0x0018024F
		internal ExpressionInfo Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x06005F70 RID: 24432 RVA: 0x00182058 File Offset: 0x00180258
		// (set) Token: 0x06005F71 RID: 24433 RVA: 0x00182060 File Offset: 0x00180260
		internal ExpressionInfo Bookmark
		{
			get
			{
				return this.m_bookmark;
			}
			set
			{
				this.m_bookmark = value;
			}
		}

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x06005F72 RID: 24434 RVA: 0x00182069 File Offset: 0x00180269
		// (set) Token: 0x06005F73 RID: 24435 RVA: 0x00182071 File Offset: 0x00180271
		internal string Custom
		{
			get
			{
				return this.m_custom;
			}
			set
			{
				this.m_custom = value;
			}
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x06005F74 RID: 24436 RVA: 0x0018207A File Offset: 0x0018027A
		// (set) Token: 0x06005F75 RID: 24437 RVA: 0x00182082 File Offset: 0x00180282
		internal bool RepeatedSibling
		{
			get
			{
				return this.m_repeatedSibling;
			}
			set
			{
				this.m_repeatedSibling = value;
			}
		}

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06005F76 RID: 24438 RVA: 0x0018208B File Offset: 0x0018028B
		// (set) Token: 0x06005F77 RID: 24439 RVA: 0x00182093 File Offset: 0x00180293
		internal bool IsFullSize
		{
			get
			{
				return this.m_isFullSize;
			}
			set
			{
				this.m_isFullSize = value;
			}
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06005F78 RID: 24440 RVA: 0x0018209C File Offset: 0x0018029C
		// (set) Token: 0x06005F79 RID: 24441 RVA: 0x001820A4 File Offset: 0x001802A4
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06005F7A RID: 24442 RVA: 0x001820AD File Offset: 0x001802AD
		// (set) Token: 0x06005F7B RID: 24443 RVA: 0x001820B5 File Offset: 0x001802B5
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06005F7C RID: 24444 RVA: 0x001820BE File Offset: 0x001802BE
		internal virtual string DataElementNameDefault
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x001820C6 File Offset: 0x001802C6
		// (set) Token: 0x06005F7E RID: 24446 RVA: 0x001820CE File Offset: 0x001802CE
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x001820D7 File Offset: 0x001802D7
		// (set) Token: 0x06005F80 RID: 24448 RVA: 0x001820DF File Offset: 0x001802DF
		internal virtual int DistanceFromReportTop
		{
			get
			{
				return this.m_distanceFromReportTop;
			}
			set
			{
				this.m_distanceFromReportTop = value;
			}
		}

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x001820E8 File Offset: 0x001802E8
		// (set) Token: 0x06005F82 RID: 24450 RVA: 0x001820F0 File Offset: 0x001802F0
		internal int DistanceBeforeTop
		{
			get
			{
				return this.m_distanceBeforeTop;
			}
			set
			{
				this.m_distanceBeforeTop = value;
			}
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06005F83 RID: 24451 RVA: 0x001820F9 File Offset: 0x001802F9
		// (set) Token: 0x06005F84 RID: 24452 RVA: 0x00182101 File Offset: 0x00180301
		internal IntList SiblingAboveMe
		{
			get
			{
				return this.m_siblingAboveMe;
			}
			set
			{
				this.m_siblingAboveMe = value;
			}
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x0018210A File Offset: 0x0018030A
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x00182112 File Offset: 0x00180312
		// (set) Token: 0x06005F87 RID: 24455 RVA: 0x0018211A File Offset: 0x0018031A
		internal bool Computed
		{
			get
			{
				return this.m_computed;
			}
			set
			{
				this.m_computed = value;
			}
		}

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x00182123 File Offset: 0x00180323
		// (set) Token: 0x06005F89 RID: 24457 RVA: 0x0018212B File Offset: 0x0018032B
		internal string RepeatWith
		{
			get
			{
				return this.m_repeatWith;
			}
			set
			{
				this.m_repeatWith = value;
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06005F8A RID: 24458 RVA: 0x00182134 File Offset: 0x00180334
		// (set) Token: 0x06005F8B RID: 24459 RVA: 0x0018213C File Offset: 0x0018033C
		internal Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL DataElementOutputRDL
		{
			get
			{
				return this.m_dataElementOutputRDL;
			}
			set
			{
				this.m_dataElementOutputRDL = value;
			}
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06005F8C RID: 24460 RVA: 0x00182145 File Offset: 0x00180345
		internal ReportItemExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06005F8D RID: 24461 RVA: 0x0018214D File Offset: 0x0018034D
		// (set) Token: 0x06005F8E RID: 24462 RVA: 0x00182155 File Offset: 0x00180355
		internal virtual int StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06005F8F RID: 24463 RVA: 0x0018215E File Offset: 0x0018035E
		// (set) Token: 0x06005F90 RID: 24464 RVA: 0x00182166 File Offset: 0x00180366
		internal virtual int EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06005F91 RID: 24465 RVA: 0x0018216F File Offset: 0x0018036F
		// (set) Token: 0x06005F92 RID: 24466 RVA: 0x00182177 File Offset: 0x00180377
		internal virtual bool SoftPageBreak
		{
			get
			{
				return this.m_softPageBreak;
			}
			set
			{
				this.m_softPageBreak = value;
			}
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06005F93 RID: 24467 RVA: 0x00182180 File Offset: 0x00180380
		// (set) Token: 0x06005F94 RID: 24468 RVA: 0x00182188 File Offset: 0x00180388
		internal virtual bool ShareMyLastPage
		{
			get
			{
				return this.m_shareMyLastPage;
			}
			set
			{
				this.m_shareMyLastPage = value;
			}
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06005F95 RID: 24469 RVA: 0x00182191 File Offset: 0x00180391
		// (set) Token: 0x06005F96 RID: 24470 RVA: 0x00182199 File Offset: 0x00180399
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06005F97 RID: 24471 RVA: 0x001821A2 File Offset: 0x001803A2
		// (set) Token: 0x06005F98 RID: 24472 RVA: 0x001821AA File Offset: 0x001803AA
		internal string RenderingModelID
		{
			get
			{
				return this.m_renderingModelID;
			}
			set
			{
				this.m_renderingModelID = value;
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06005F99 RID: 24473 RVA: 0x001821B3 File Offset: 0x001803B3
		// (set) Token: 0x06005F9A RID: 24474 RVA: 0x001821BB File Offset: 0x001803BB
		internal StyleProperties SharedStyleProperties
		{
			get
			{
				return this.m_sharedStyleProperties;
			}
			set
			{
				this.m_sharedStyleProperties = value;
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x06005F9B RID: 24475 RVA: 0x001821C4 File Offset: 0x001803C4
		// (set) Token: 0x06005F9C RID: 24476 RVA: 0x001821CC File Offset: 0x001803CC
		internal bool NoNonSharedStyleProps
		{
			get
			{
				return this.m_noNonSharedStyleProps;
			}
			set
			{
				this.m_noNonSharedStyleProps = value;
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06005F9D RID: 24477 RVA: 0x001821D5 File Offset: 0x001803D5
		// (set) Token: 0x06005F9E RID: 24478 RVA: 0x001821DD File Offset: 0x001803DD
		internal ReportSize HeightForRendering
		{
			get
			{
				return this.m_heightForRendering;
			}
			set
			{
				this.m_heightForRendering = value;
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x001821E6 File Offset: 0x001803E6
		// (set) Token: 0x06005FA0 RID: 24480 RVA: 0x001821EE File Offset: 0x001803EE
		internal ReportSize WidthForRendering
		{
			get
			{
				return this.m_widthForRendering;
			}
			set
			{
				this.m_widthForRendering = value;
			}
		}

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x06005FA1 RID: 24481 RVA: 0x001821F7 File Offset: 0x001803F7
		// (set) Token: 0x06005FA2 RID: 24482 RVA: 0x001821FF File Offset: 0x001803FF
		internal ReportSize TopForRendering
		{
			get
			{
				return this.m_topForRendering;
			}
			set
			{
				this.m_topForRendering = value;
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06005FA3 RID: 24483 RVA: 0x00182208 File Offset: 0x00180408
		// (set) Token: 0x06005FA4 RID: 24484 RVA: 0x00182210 File Offset: 0x00180410
		internal ReportSize LeftForRendering
		{
			get
			{
				return this.m_leftForRendering;
			}
			set
			{
				this.m_leftForRendering = value;
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x00182219 File Offset: 0x00180419
		internal virtual DataElementOutputTypes DataElementOutputDefault
		{
			get
			{
				return DataElementOutputTypes.Output;
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x06005FA6 RID: 24486 RVA: 0x0018221C File Offset: 0x0018041C
		// (set) Token: 0x06005FA7 RID: 24487 RVA: 0x00182224 File Offset: 0x00180424
		internal double TopInStartPage
		{
			get
			{
				return this.m_topInPage;
			}
			set
			{
				this.m_topInPage = value;
			}
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06005FA8 RID: 24488 RVA: 0x0018222D File Offset: 0x0018042D
		// (set) Token: 0x06005FA9 RID: 24489 RVA: 0x00182235 File Offset: 0x00180435
		internal double BottomInEndPage
		{
			get
			{
				return this.m_bottomInPage;
			}
			set
			{
				this.m_bottomInPage = value;
			}
		}

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x06005FAA RID: 24490 RVA: 0x0018223E File Offset: 0x0018043E
		// (set) Token: 0x06005FAB RID: 24491 RVA: 0x00182246 File Offset: 0x00180446
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x06005FAC RID: 24492 RVA: 0x0018224F File Offset: 0x0018044F
		// (set) Token: 0x06005FAD RID: 24493 RVA: 0x00182257 File Offset: 0x00180457
		internal ReportProcessing.PageTextboxes RepeatedSiblingTextboxes
		{
			get
			{
				return this.m_repeatedSiblingTextboxes;
			}
			set
			{
				this.m_repeatedSiblingTextboxes = value;
			}
		}

		// Token: 0x06005FAE RID: 24494 RVA: 0x00182260 File Offset: 0x00180460
		internal virtual bool Initialize(InitializationContext context)
		{
			if (this.m_top == null)
			{
				this.m_top = "0mm";
				this.m_topValue = 0.0;
			}
			else
			{
				this.m_topValue = context.ValidateSize(ref this.m_top, "Top");
			}
			if (this.m_left == null)
			{
				this.m_left = "0mm";
				this.m_leftValue = 0.0;
			}
			else
			{
				this.m_leftValue = context.ValidateSize(ref this.m_left, "Left");
			}
			if (this.m_parent != null)
			{
				bool flag = true;
				if (this.m_width == null)
				{
					if ((context.Location & LocationFlags.InMatrixOrTable) == (LocationFlags)0)
					{
						if (ObjectType.Table == context.ObjectType || ObjectType.Matrix == context.ObjectType)
						{
							this.m_width = "0mm";
							this.m_widthValue = 0.0;
							flag = false;
						}
						else if (ObjectType.PageHeader == context.ObjectType || ObjectType.PageFooter == context.ObjectType)
						{
							Report report = this.m_parent as Report;
							this.m_widthValue = report.PageSectionWidth;
							this.m_width = Converter.ConvertSize(this.m_widthValue);
						}
						else
						{
							this.m_widthValue = Math.Round(this.m_parent.m_widthValue - this.m_leftValue, Validator.DecimalPrecision);
							this.m_width = Converter.ConvertSize(this.m_widthValue);
						}
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.m_widthValue = context.ValidateSize(this.m_width, "Width");
				}
				flag = true;
				if (this.m_height == null)
				{
					if ((context.Location & LocationFlags.InMatrixOrTable) == (LocationFlags)0)
					{
						if (ObjectType.Table == context.ObjectType || ObjectType.Matrix == context.ObjectType)
						{
							this.m_height = "0mm";
							this.m_heightValue = 0.0;
							flag = false;
						}
						else
						{
							this.m_heightValue = Math.Round(this.m_parent.m_heightValue - this.m_topValue, Validator.DecimalPrecision);
							this.m_height = Converter.ConvertSize(this.m_heightValue);
						}
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.m_heightValue = context.ValidateSize(this.m_height, "Height");
				}
			}
			else
			{
				this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
				this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
			}
			if ((context.Location & LocationFlags.InMatrixOrTable) == (LocationFlags)0)
			{
				this.ValidateParentBoundaries(context, context.ObjectType, context.ObjectName);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			if (this.m_label != null)
			{
				this.m_label.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_label);
			}
			if (this.m_bookmark != null)
			{
				this.m_bookmark.Initialize("Bookmark", context);
				context.ExprHostBuilder.ReportItemBookmark(this.m_bookmark);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ReportItemToolTip(this.m_toolTip);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, true, context);
			}
			this.DataRendererInitialize(context);
			return false;
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x0018257C File Offset: 0x0018077C
		private void ValidateParentBoundaries(InitializationContext context, ObjectType objectType, string objectName)
		{
			if (this.m_parent != null && !(this.m_parent is Report))
			{
				if (objectType == ObjectType.Line)
				{
					if (this.AbsoluteTopValue < 0.0)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Top".ToLowerInvariant(), Array.Empty<string>());
					}
					if (this.AbsoluteLeftValue < 0.0)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Left".ToLowerInvariant(), Array.Empty<string>());
					}
				}
				if (this.AbsoluteBottomValue > Math.Round(this.m_parent.HeightValue, 1))
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Bottom".ToLowerInvariant(), Array.Empty<string>());
				}
				if (this.AbsoluteRightValue > Math.Round(this.m_parent.WidthValue, 1))
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Right".ToLowerInvariant(), Array.Empty<string>());
				}
			}
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x0018268C File Offset: 0x0018088C
		protected virtual void DataRendererInitialize(InitializationContext context)
		{
			CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, this.DataElementNameDefault, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
			switch (this.m_dataElementOutputRDL)
			{
			case Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.Output:
				this.m_dataElementOutput = DataElementOutputTypes.Output;
				return;
			case Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.NoOutput:
				this.m_dataElementOutput = DataElementOutputTypes.NoOutput;
				return;
			case Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.ContentsOnly:
				this.m_dataElementOutput = DataElementOutputTypes.ContentsOnly;
				return;
			case Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.Auto:
				if (context.TableColumnVisible && (this.m_visibility == null || this.m_visibility.Hidden == null || this.m_visibility.Toggle != null || (ExpressionInfo.Types.Constant == this.m_visibility.Hidden.Type && !this.m_visibility.Hidden.BoolValue)))
				{
					this.m_dataElementOutput = this.DataElementOutputDefault;
					return;
				}
				this.m_dataElementOutput = DataElementOutputTypes.NoOutput;
				return;
			default:
				return;
			}
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x00182760 File Offset: 0x00180960
		internal virtual void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			if (overwrite)
			{
				this.m_top = "0mm";
				this.m_topValue = 0.0;
				this.m_left = "0mm";
				this.m_leftValue = 0.0;
			}
			if (this.m_width == null || (overwrite && this.m_widthValue != width))
			{
				this.m_width = width.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
			}
			if (this.m_height == null || (overwrite && this.m_heightValue != height))
			{
				this.m_height = height.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
			}
			this.ValidateParentBoundaries(context, this.ObjectType, this.Name);
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x00182854 File Offset: 0x00180A54
		internal void CalculateSizes(InitializationContext context, bool overwrite)
		{
			double num = this.m_widthValue;
			double num2 = this.m_heightValue;
			if (this.m_width == null)
			{
				num = Math.Round(this.m_parent.m_widthValue - this.m_leftValue, Validator.DecimalPrecision);
			}
			if (this.m_height == null)
			{
				num2 = Math.Round(this.m_parent.m_heightValue - this.m_topValue, Validator.DecimalPrecision);
			}
			this.CalculateSizes(num, num2, context, overwrite);
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x001828C3 File Offset: 0x00180AC3
		internal virtual void RegisterReceiver(InitializationContext context)
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, false);
			}
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x001828DC File Offset: 0x00180ADC
		int IComparable.CompareTo(object obj)
		{
			if (!(obj is Microsoft.ReportingServices.ReportProcessing.ReportItem))
			{
				throw new ArgumentException("Argument was not a ReportItem.  Can only compare two ReportItems");
			}
			Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem = (Microsoft.ReportingServices.ReportProcessing.ReportItem)obj;
			if (this.m_topValue < reportItem.m_topValue)
			{
				return -1;
			}
			if (this.m_topValue > reportItem.m_topValue)
			{
				return 1;
			}
			if (this.m_leftValue < reportItem.m_leftValue)
			{
				return -1;
			}
			if (this.m_leftValue > reportItem.m_leftValue)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06005FB5 RID: 24501
		internal abstract void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel);

		// Token: 0x06005FB6 RID: 24502 RVA: 0x00182944 File Offset: 0x00180B44
		protected void ReportItemSetExprHost(ReportItemExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_customProperties != null);
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x001829C4 File Offset: 0x00180BC4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IDOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.Top, Token.String),
				new MemberInfo(MemberName.TopValue, Token.Double),
				new MemberInfo(MemberName.Left, Token.String),
				new MemberInfo(MemberName.LeftValue, Token.Double),
				new MemberInfo(MemberName.Height, Token.String),
				new MemberInfo(MemberName.HeightValue, Token.Double),
				new MemberInfo(MemberName.Width, Token.String),
				new MemberInfo(MemberName.WidthValue, Token.Double),
				new MemberInfo(MemberName.ZIndex, Token.Int32),
				new MemberInfo(MemberName.Visibility, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Visibility),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Bookmark, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Custom, Token.String),
				new MemberInfo(MemberName.RepeatedSibling, Token.Boolean),
				new MemberInfo(MemberName.IsFullSize, Token.Boolean),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.DistanceFromReportTop, Token.Int32),
				new MemberInfo(MemberName.DistanceBeforeTop, Token.Int32),
				new MemberInfo(MemberName.SiblingAboveMe, Token.Array, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataValueList)
			});
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x00182BBC File Offset: 0x00180DBC
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (nonCompNames == null)
			{
				return null;
			}
			if (targetUniqueName == nonCompNames.UniqueName)
			{
				return this;
			}
			Rectangle rectangle = this as Rectangle;
			if (rectangle != null)
			{
				return rectangle.SearchChildren(targetUniqueName, ref nonCompNames, chunkManager);
			}
			return null;
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x00182BF0 File Offset: 0x00180DF0
		internal virtual void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, NonComputedUniqueNames nonCompNames)
		{
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x00182BF4 File Offset: 0x00180DF4
		internal void ProcessNavigationAction(ReportProcessing.NavigationInfo navigationInfo, NonComputedUniqueNames nonCompNames, int startPage)
		{
			if (nonCompNames == null)
			{
				return;
			}
			if (this.m_bookmark != null && this.m_bookmark.Value != null)
			{
				navigationInfo.ProcessBookmark(this.m_bookmark.Value, startPage, nonCompNames.UniqueName);
			}
			Rectangle rectangle = this as Rectangle;
			if (this.m_label != null && this.m_label.Value != null)
			{
				int num = -1;
				if (rectangle != null)
				{
					navigationInfo.EnterDocumentMapChildren();
					num = rectangle.ProcessNavigationChildren(navigationInfo, nonCompNames, startPage);
				}
				if (num < 0)
				{
					num = nonCompNames.UniqueName;
				}
				navigationInfo.AddToDocumentMap(num, rectangle != null, startPage, this.m_label.Value);
				return;
			}
			if (rectangle != null)
			{
				rectangle.ProcessNavigationChildren(navigationInfo, nonCompNames, startPage);
			}
		}

		// Token: 0x04003088 RID: 12424
		private const string ZeroSize = "0mm";

		// Token: 0x04003089 RID: 12425
		internal const int OverlapDetectionRounding = 1;

		// Token: 0x0400308A RID: 12426
		protected string m_name;

		// Token: 0x0400308B RID: 12427
		protected Style m_styleClass;

		// Token: 0x0400308C RID: 12428
		protected string m_top;

		// Token: 0x0400308D RID: 12429
		protected double m_topValue;

		// Token: 0x0400308E RID: 12430
		protected string m_left;

		// Token: 0x0400308F RID: 12431
		protected double m_leftValue;

		// Token: 0x04003090 RID: 12432
		protected string m_height;

		// Token: 0x04003091 RID: 12433
		protected double m_heightValue;

		// Token: 0x04003092 RID: 12434
		protected string m_width;

		// Token: 0x04003093 RID: 12435
		protected double m_widthValue;

		// Token: 0x04003094 RID: 12436
		protected int m_zIndex;

		// Token: 0x04003095 RID: 12437
		protected ExpressionInfo m_toolTip;

		// Token: 0x04003096 RID: 12438
		protected Visibility m_visibility;

		// Token: 0x04003097 RID: 12439
		protected ExpressionInfo m_label;

		// Token: 0x04003098 RID: 12440
		protected ExpressionInfo m_bookmark;

		// Token: 0x04003099 RID: 12441
		protected string m_custom;

		// Token: 0x0400309A RID: 12442
		protected bool m_repeatedSibling;

		// Token: 0x0400309B RID: 12443
		protected bool m_isFullSize;

		// Token: 0x0400309C RID: 12444
		private int m_exprHostID = -1;

		// Token: 0x0400309D RID: 12445
		protected string m_dataElementName;

		// Token: 0x0400309E RID: 12446
		protected DataElementOutputTypes m_dataElementOutput;

		// Token: 0x0400309F RID: 12447
		protected int m_distanceFromReportTop = -1;

		// Token: 0x040030A0 RID: 12448
		protected int m_distanceBeforeTop;

		// Token: 0x040030A1 RID: 12449
		protected IntList m_siblingAboveMe;

		// Token: 0x040030A2 RID: 12450
		protected DataValueList m_customProperties;

		// Token: 0x040030A3 RID: 12451
		[NonSerialized]
		protected Microsoft.ReportingServices.ReportProcessing.ReportItem m_parent;

		// Token: 0x040030A4 RID: 12452
		[NonSerialized]
		protected bool m_computed;

		// Token: 0x040030A5 RID: 12453
		[NonSerialized]
		protected string m_repeatWith;

		// Token: 0x040030A6 RID: 12454
		[NonSerialized]
		protected Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL m_dataElementOutputRDL = Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.Auto;

		// Token: 0x040030A7 RID: 12455
		[NonSerialized]
		private ReportItemExprHost m_exprHost;

		// Token: 0x040030A8 RID: 12456
		[NonSerialized]
		protected int m_startPage = -1;

		// Token: 0x040030A9 RID: 12457
		[NonSerialized]
		protected int m_endPage = -1;

		// Token: 0x040030AA RID: 12458
		[NonSerialized]
		protected bool m_softPageBreak;

		// Token: 0x040030AB RID: 12459
		[NonSerialized]
		protected bool m_shareMyLastPage = true;

		// Token: 0x040030AC RID: 12460
		[NonSerialized]
		protected bool m_startHidden;

		// Token: 0x040030AD RID: 12461
		[NonSerialized]
		protected double m_topInPage;

		// Token: 0x040030AE RID: 12462
		[NonSerialized]
		protected double m_bottomInPage;

		// Token: 0x040030AF RID: 12463
		[NonSerialized]
		private ReportProcessing.PageTextboxes m_repeatedSiblingTextboxes;

		// Token: 0x040030B0 RID: 12464
		[NonSerialized]
		protected string m_renderingModelID;

		// Token: 0x040030B1 RID: 12465
		[NonSerialized]
		protected StyleProperties m_sharedStyleProperties;

		// Token: 0x040030B2 RID: 12466
		[NonSerialized]
		protected bool m_noNonSharedStyleProps;

		// Token: 0x040030B3 RID: 12467
		[NonSerialized]
		protected ReportSize m_heightForRendering;

		// Token: 0x040030B4 RID: 12468
		[NonSerialized]
		protected ReportSize m_widthForRendering;

		// Token: 0x040030B5 RID: 12469
		[NonSerialized]
		protected ReportSize m_topForRendering;

		// Token: 0x040030B6 RID: 12470
		[NonSerialized]
		protected ReportSize m_leftForRendering;

		// Token: 0x02000CC0 RID: 3264
		public enum DataElementOutputTypesRDL
		{
			// Token: 0x04004E7C RID: 20092
			Output,
			// Token: 0x04004E7D RID: 20093
			NoOutput,
			// Token: 0x04004E7E RID: 20094
			ContentsOnly,
			// Token: 0x04004E7F RID: 20095
			Auto
		}

		// Token: 0x02000CC1 RID: 3265
		internal enum DataElementStylesRDL
		{
			// Token: 0x04004E81 RID: 20097
			AttributeNormal,
			// Token: 0x04004E82 RID: 20098
			ElementNormal,
			// Token: 0x04004E83 RID: 20099
			Auto
		}
	}
}
