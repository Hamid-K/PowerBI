using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000200 RID: 512
	internal sealed class CssStyleDeclaration : CssNode, ICssStyleDeclaration, ICssProperties, IEnumerable<ICssProperty>, IEnumerable, ICssNode, IStyleFormattable, IBindable
	{
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x0600116F RID: 4463 RVA: 0x000481B8 File Offset: 0x000463B8
		// (remove) Token: 0x06001170 RID: 4464 RVA: 0x000481F0 File Offset: 0x000463F0
		public event Action<string> Changed;

		// Token: 0x06001171 RID: 4465 RVA: 0x00048225 File Offset: 0x00046425
		private CssStyleDeclaration(CssRule parent, CssParser parser)
		{
			this._parent = parent;
			this._parser = parser;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0004823B File Offset: 0x0004643B
		internal CssStyleDeclaration(CssParser parser)
			: this(null, parser)
		{
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00048245 File Offset: 0x00046445
		internal CssStyleDeclaration()
			: this(null, null)
		{
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0004824F File Offset: 0x0004644F
		internal CssStyleDeclaration(CssRule parent)
			: this(parent, parent.Parser)
		{
		}

		// Token: 0x170003DE RID: 990
		public string this[int index]
		{
			get
			{
				return this.Declarations.GetItemByIndex(index).Name;
			}
		}

		// Token: 0x170003DF RID: 991
		public string this[string name]
		{
			get
			{
				return this.GetPropertyValue(name);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004827A File Offset: 0x0004647A
		public IEnumerable<CssProperty> Declarations
		{
			get
			{
				return base.Children.OfType<CssProperty>();
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00048288 File Offset: 0x00046488
		public bool IsStrictMode
		{
			get
			{
				return this.IsReadOnly || !this._parser.Options.IsIncludingUnknownDeclarations;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004810B File Offset: 0x0004630B
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x000482B5 File Offset: 0x000464B5
		public string CssText
		{
			get
			{
				return this.ToCss();
			}
			set
			{
				this.Update(value);
				this.RaiseChanged();
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x000482C4 File Offset: 0x000464C4
		public bool IsReadOnly
		{
			get
			{
				return this._parser == null;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x000482CF File Offset: 0x000464CF
		public int Length
		{
			get
			{
				return this.Declarations.Count<CssProperty>();
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x000482DC File Offset: 0x000464DC
		public ICssRule Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000482E4 File Offset: 0x000464E4
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x000482F1 File Offset: 0x000464F1
		string ICssStyleDeclaration.AlignContent
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AlignContent);
			}
			set
			{
				this.SetProperty(PropertyNames.AlignContent, value, null);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00048300 File Offset: 0x00046500
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x0004830D File Offset: 0x0004650D
		string ICssStyleDeclaration.AlignItems
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AlignItems);
			}
			set
			{
				this.SetProperty(PropertyNames.AlignItems, value, null);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0004831C File Offset: 0x0004651C
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00048329 File Offset: 0x00046529
		string ICssStyleDeclaration.AlignSelf
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AlignSelf);
			}
			set
			{
				this.SetProperty(PropertyNames.AlignSelf, value, null);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00048338 File Offset: 0x00046538
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x00048345 File Offset: 0x00046545
		string ICssStyleDeclaration.Accelerator
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Accelerator);
			}
			set
			{
				this.SetProperty(PropertyNames.Accelerator, value, null);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00048354 File Offset: 0x00046554
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x00048361 File Offset: 0x00046561
		string ICssStyleDeclaration.AlignmentBaseline
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AlignBaseline);
			}
			set
			{
				this.SetProperty(PropertyNames.AlignBaseline, value, null);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00048370 File Offset: 0x00046570
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x0004837D File Offset: 0x0004657D
		string ICssStyleDeclaration.Animation
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Animation);
			}
			set
			{
				this.SetProperty(PropertyNames.Animation, value, null);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0004838C File Offset: 0x0004658C
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x00048399 File Offset: 0x00046599
		string ICssStyleDeclaration.AnimationDelay
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationDelay);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationDelay, value, null);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000483A8 File Offset: 0x000465A8
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x000483B5 File Offset: 0x000465B5
		string ICssStyleDeclaration.AnimationDirection
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationDirection);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationDirection, value, null);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000483C4 File Offset: 0x000465C4
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x000483D1 File Offset: 0x000465D1
		string ICssStyleDeclaration.AnimationDuration
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationDuration);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationDuration, value, null);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000483E0 File Offset: 0x000465E0
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x000483ED File Offset: 0x000465ED
		string ICssStyleDeclaration.AnimationFillMode
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationFillMode);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationFillMode, value, null);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000483FC File Offset: 0x000465FC
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00048409 File Offset: 0x00046609
		string ICssStyleDeclaration.AnimationIterationCount
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationIterationCount);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationIterationCount, value, null);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00048418 File Offset: 0x00046618
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00048425 File Offset: 0x00046625
		string ICssStyleDeclaration.AnimationName
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationName);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationName, value, null);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00048434 File Offset: 0x00046634
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00048441 File Offset: 0x00046641
		string ICssStyleDeclaration.AnimationPlayState
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationPlayState);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationPlayState, value, null);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00048450 File Offset: 0x00046650
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x0004845D File Offset: 0x0004665D
		string ICssStyleDeclaration.AnimationTimingFunction
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.AnimationTimingFunction);
			}
			set
			{
				this.SetProperty(PropertyNames.AnimationTimingFunction, value, null);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004846C File Offset: 0x0004666C
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00048479 File Offset: 0x00046679
		string ICssStyleDeclaration.BackfaceVisibility
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackfaceVisibility);
			}
			set
			{
				this.SetProperty(PropertyNames.BackfaceVisibility, value, null);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00048488 File Offset: 0x00046688
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x00048495 File Offset: 0x00046695
		string ICssStyleDeclaration.Background
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Background);
			}
			set
			{
				this.SetProperty(PropertyNames.Background, value, null);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x000484A4 File Offset: 0x000466A4
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x000484B1 File Offset: 0x000466B1
		string ICssStyleDeclaration.BackgroundAttachment
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundAttachment);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundAttachment, value, null);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000484C0 File Offset: 0x000466C0
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x000484CD File Offset: 0x000466CD
		string ICssStyleDeclaration.BackgroundClip
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundClip);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundClip, value, null);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x000484DC File Offset: 0x000466DC
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x000484E9 File Offset: 0x000466E9
		string ICssStyleDeclaration.BackgroundColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundColor);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundColor, value, null);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x000484F8 File Offset: 0x000466F8
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00048505 File Offset: 0x00046705
		string ICssStyleDeclaration.BackgroundImage
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundImage);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundImage, value, null);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00048514 File Offset: 0x00046714
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00048521 File Offset: 0x00046721
		string ICssStyleDeclaration.BackgroundOrigin
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundOrigin);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundOrigin, value, null);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00048530 File Offset: 0x00046730
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x0004853D File Offset: 0x0004673D
		string ICssStyleDeclaration.BackgroundPosition
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundPosition);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundPosition, value, null);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004854C File Offset: 0x0004674C
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x00048559 File Offset: 0x00046759
		string ICssStyleDeclaration.BackgroundPositionX
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundPositionX);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundPositionX, value, null);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00048568 File Offset: 0x00046768
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00048575 File Offset: 0x00046775
		string ICssStyleDeclaration.BackgroundPositionY
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundPositionY);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundPositionY, value, null);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00048584 File Offset: 0x00046784
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00048591 File Offset: 0x00046791
		string ICssStyleDeclaration.BackgroundRepeat
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundRepeat);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundRepeat, value, null);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x000485A0 File Offset: 0x000467A0
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x000485AD File Offset: 0x000467AD
		string ICssStyleDeclaration.BackgroundSize
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BackgroundSize);
			}
			set
			{
				this.SetProperty(PropertyNames.BackgroundSize, value, null);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x000485BC File Offset: 0x000467BC
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x000485C9 File Offset: 0x000467C9
		string ICssStyleDeclaration.BaselineShift
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BaselineShift);
			}
			set
			{
				this.SetProperty(PropertyNames.BaselineShift, value, null);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x000485D8 File Offset: 0x000467D8
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x000485E5 File Offset: 0x000467E5
		string ICssStyleDeclaration.Behavior
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Behavior);
			}
			set
			{
				this.SetProperty(PropertyNames.Behavior, value, null);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000485F4 File Offset: 0x000467F4
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00048601 File Offset: 0x00046801
		string ICssStyleDeclaration.Bottom
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Bottom);
			}
			set
			{
				this.SetProperty(PropertyNames.Bottom, value, null);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00048610 File Offset: 0x00046810
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0004861D File Offset: 0x0004681D
		string ICssStyleDeclaration.Border
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Border);
			}
			set
			{
				this.SetProperty(PropertyNames.Border, value, null);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004862C File Offset: 0x0004682C
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x00048639 File Offset: 0x00046839
		string ICssStyleDeclaration.BorderBottom
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderBottom);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderBottom, value, null);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00048648 File Offset: 0x00046848
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x00048655 File Offset: 0x00046855
		string ICssStyleDeclaration.BorderBottomColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderBottomColor);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderBottomColor, value, null);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00048664 File Offset: 0x00046864
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x00048671 File Offset: 0x00046871
		string ICssStyleDeclaration.BorderBottomLeftRadius
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderBottomLeftRadius);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderBottomLeftRadius, value, null);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00048680 File Offset: 0x00046880
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x0004868D File Offset: 0x0004688D
		string ICssStyleDeclaration.BorderBottomRightRadius
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderBottomRightRadius);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderBottomRightRadius, value, null);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004869C File Offset: 0x0004689C
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x000486A9 File Offset: 0x000468A9
		string ICssStyleDeclaration.BorderBottomStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderBottomStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderBottomStyle, value, null);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000486B8 File Offset: 0x000468B8
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x000486C5 File Offset: 0x000468C5
		string ICssStyleDeclaration.BorderBottomWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderBottomWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderBottomWidth, value, null);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000486D4 File Offset: 0x000468D4
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x000486E1 File Offset: 0x000468E1
		string ICssStyleDeclaration.BorderCollapse
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderCollapse);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderCollapse, value, null);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000486F0 File Offset: 0x000468F0
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x000486FD File Offset: 0x000468FD
		string ICssStyleDeclaration.BorderColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderColor);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderColor, value, null);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004870C File Offset: 0x0004690C
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x00048719 File Offset: 0x00046919
		string ICssStyleDeclaration.BorderImage
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderImage);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderImage, value, null);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00048728 File Offset: 0x00046928
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00048735 File Offset: 0x00046935
		string ICssStyleDeclaration.BorderImageOutset
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderImageOutset);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderImageOutset, value, null);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00048744 File Offset: 0x00046944
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x00048751 File Offset: 0x00046951
		string ICssStyleDeclaration.BorderImageRepeat
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderImageRepeat);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderImageRepeat, value, null);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00048760 File Offset: 0x00046960
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x0004876D File Offset: 0x0004696D
		string ICssStyleDeclaration.BorderImageSlice
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderImageSlice);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderImageSlice, value, null);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004877C File Offset: 0x0004697C
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x00048789 File Offset: 0x00046989
		string ICssStyleDeclaration.BorderImageSource
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderImageSource);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderImageSource, value, null);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00048798 File Offset: 0x00046998
		// (set) Token: 0x060011D5 RID: 4565 RVA: 0x000487A5 File Offset: 0x000469A5
		string ICssStyleDeclaration.BorderImageWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderImageWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderImageWidth, value, null);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x000487B4 File Offset: 0x000469B4
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x000487C1 File Offset: 0x000469C1
		string ICssStyleDeclaration.BorderLeft
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderLeft);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderLeft, value, null);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x000487D0 File Offset: 0x000469D0
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x000487DD File Offset: 0x000469DD
		string ICssStyleDeclaration.BorderLeftColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderLeftColor);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderLeftColor, value, null);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000487EC File Offset: 0x000469EC
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x000487F9 File Offset: 0x000469F9
		string ICssStyleDeclaration.BorderLeftStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderLeftStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderLeftStyle, value, null);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00048808 File Offset: 0x00046A08
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x00048815 File Offset: 0x00046A15
		string ICssStyleDeclaration.BorderLeftWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderLeftWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderLeftWidth, value, null);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00048824 File Offset: 0x00046A24
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x00048831 File Offset: 0x00046A31
		string ICssStyleDeclaration.BorderRadius
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderRadius);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderRadius, value, null);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00048840 File Offset: 0x00046A40
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004884D File Offset: 0x00046A4D
		string ICssStyleDeclaration.BorderRight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderRight);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderRight, value, null);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004885C File Offset: 0x00046A5C
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00048869 File Offset: 0x00046A69
		string ICssStyleDeclaration.BorderRightColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderRightColor);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderRightColor, value, null);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00048878 File Offset: 0x00046A78
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x00048885 File Offset: 0x00046A85
		string ICssStyleDeclaration.BorderRightStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderRightStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderRightStyle, value, null);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00048894 File Offset: 0x00046A94
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x000488A1 File Offset: 0x00046AA1
		string ICssStyleDeclaration.BorderRightWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderRightWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderRightWidth, value, null);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x000488B0 File Offset: 0x00046AB0
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x000488BD File Offset: 0x00046ABD
		string ICssStyleDeclaration.BorderSpacing
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderSpacing);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderSpacing, value, null);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x000488CC File Offset: 0x00046ACC
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x000488D9 File Offset: 0x00046AD9
		string ICssStyleDeclaration.BorderStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderStyle, value, null);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x000488E8 File Offset: 0x00046AE8
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x000488F5 File Offset: 0x00046AF5
		string ICssStyleDeclaration.BorderTop
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderTop);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderTop, value, null);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00048904 File Offset: 0x00046B04
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x00048911 File Offset: 0x00046B11
		string ICssStyleDeclaration.BorderTopColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderTopColor);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderTopColor, value, null);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00048920 File Offset: 0x00046B20
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004892D File Offset: 0x00046B2D
		string ICssStyleDeclaration.BorderTopLeftRadius
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderTopLeftRadius);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderTopLeftRadius, value, null);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004893C File Offset: 0x00046B3C
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00048949 File Offset: 0x00046B49
		string ICssStyleDeclaration.BorderTopRightRadius
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderTopRightRadius);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderTopRightRadius, value, null);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00048958 File Offset: 0x00046B58
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x00048965 File Offset: 0x00046B65
		string ICssStyleDeclaration.BorderTopStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderTopStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderTopStyle, value, null);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00048974 File Offset: 0x00046B74
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x00048981 File Offset: 0x00046B81
		string ICssStyleDeclaration.BorderTopWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderTopWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderTopWidth, value, null);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00048990 File Offset: 0x00046B90
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0004899D File Offset: 0x00046B9D
		string ICssStyleDeclaration.BorderWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BorderWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.BorderWidth, value, null);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000489AC File Offset: 0x00046BAC
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x000489B9 File Offset: 0x00046BB9
		string ICssStyleDeclaration.BoxShadow
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BoxShadow);
			}
			set
			{
				this.SetProperty(PropertyNames.BoxShadow, value, null);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000489C8 File Offset: 0x00046BC8
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x000489D5 File Offset: 0x00046BD5
		string ICssStyleDeclaration.BoxSizing
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BoxSizing);
			}
			set
			{
				this.SetProperty(PropertyNames.BoxSizing, value, null);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000489E4 File Offset: 0x00046BE4
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x000489F1 File Offset: 0x00046BF1
		string ICssStyleDeclaration.BreakAfter
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BreakAfter);
			}
			set
			{
				this.SetProperty(PropertyNames.BreakAfter, value, null);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x00048A00 File Offset: 0x00046C00
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x00048A0D File Offset: 0x00046C0D
		string ICssStyleDeclaration.BreakBefore
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BreakBefore);
			}
			set
			{
				this.SetProperty(PropertyNames.BreakBefore, value, null);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00048A1C File Offset: 0x00046C1C
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x00048A29 File Offset: 0x00046C29
		string ICssStyleDeclaration.BreakInside
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.BreakInside);
			}
			set
			{
				this.SetProperty(PropertyNames.BreakInside, value, null);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00048A38 File Offset: 0x00046C38
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00048A45 File Offset: 0x00046C45
		string ICssStyleDeclaration.CaptionSide
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.CaptionSide);
			}
			set
			{
				this.SetProperty(PropertyNames.CaptionSide, value, null);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00048A54 File Offset: 0x00046C54
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x00048A61 File Offset: 0x00046C61
		string ICssStyleDeclaration.Clear
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Clear);
			}
			set
			{
				this.SetProperty(PropertyNames.Clear, value, null);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00048A70 File Offset: 0x00046C70
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x00048A7D File Offset: 0x00046C7D
		string ICssStyleDeclaration.Clip
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Clip);
			}
			set
			{
				this.SetProperty(PropertyNames.Clip, value, null);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00048A8C File Offset: 0x00046C8C
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x00048A99 File Offset: 0x00046C99
		string ICssStyleDeclaration.ClipBottom
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ClipBottom);
			}
			set
			{
				this.SetProperty(PropertyNames.ClipBottom, value, null);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00048AA8 File Offset: 0x00046CA8
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00048AB5 File Offset: 0x00046CB5
		string ICssStyleDeclaration.ClipLeft
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ClipLeft);
			}
			set
			{
				this.SetProperty(PropertyNames.ClipLeft, value, null);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00048AC4 File Offset: 0x00046CC4
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00048AD1 File Offset: 0x00046CD1
		string ICssStyleDeclaration.ClipPath
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ClipPath);
			}
			set
			{
				this.SetProperty(PropertyNames.ClipPath, value, null);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00048AE0 File Offset: 0x00046CE0
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x00048AED File Offset: 0x00046CED
		string ICssStyleDeclaration.ClipRight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ClipRight);
			}
			set
			{
				this.SetProperty(PropertyNames.ClipRight, value, null);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00048AFC File Offset: 0x00046CFC
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x00048B09 File Offset: 0x00046D09
		string ICssStyleDeclaration.ClipRule
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ClipRule);
			}
			set
			{
				this.SetProperty(PropertyNames.ClipRule, value, null);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00048B18 File Offset: 0x00046D18
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x00048B25 File Offset: 0x00046D25
		string ICssStyleDeclaration.ClipTop
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ClipTop);
			}
			set
			{
				this.SetProperty(PropertyNames.ClipTop, value, null);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00048B34 File Offset: 0x00046D34
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x00048B41 File Offset: 0x00046D41
		string ICssStyleDeclaration.Color
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Color);
			}
			set
			{
				this.SetProperty(PropertyNames.Color, value, null);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00048B50 File Offset: 0x00046D50
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x00048B5D File Offset: 0x00046D5D
		string ICssStyleDeclaration.ColorInterpolationFilters
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColorInterpolationFilters);
			}
			set
			{
				this.SetProperty(PropertyNames.ColorInterpolationFilters, value, null);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00048B6C File Offset: 0x00046D6C
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x00048B79 File Offset: 0x00046D79
		string ICssStyleDeclaration.ColumnCount
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnCount);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnCount, value, null);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00048B88 File Offset: 0x00046D88
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x00048B95 File Offset: 0x00046D95
		string ICssStyleDeclaration.ColumnFill
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnFill);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnFill, value, null);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00048BA4 File Offset: 0x00046DA4
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x00048BB1 File Offset: 0x00046DB1
		string ICssStyleDeclaration.ColumnGap
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnGap);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnGap, value, null);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00048BC0 File Offset: 0x00046DC0
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x00048BCD File Offset: 0x00046DCD
		string ICssStyleDeclaration.ColumnRule
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnRule);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnRule, value, null);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00048BDC File Offset: 0x00046DDC
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x00048BE9 File Offset: 0x00046DE9
		string ICssStyleDeclaration.ColumnRuleColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnRuleColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnRuleColor, value, null);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00048BF8 File Offset: 0x00046DF8
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x00048C05 File Offset: 0x00046E05
		string ICssStyleDeclaration.ColumnRuleStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnRuleStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnRuleStyle, value, null);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00048C14 File Offset: 0x00046E14
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x00048C21 File Offset: 0x00046E21
		string ICssStyleDeclaration.ColumnRuleWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnRuleWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnRuleWidth, value, null);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00048C30 File Offset: 0x00046E30
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00048C3D File Offset: 0x00046E3D
		string ICssStyleDeclaration.Columns
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Columns);
			}
			set
			{
				this.SetProperty(PropertyNames.Columns, value, null);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00048C4C File Offset: 0x00046E4C
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00048C59 File Offset: 0x00046E59
		string ICssStyleDeclaration.ColumnSpan
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnSpan);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnSpan, value, null);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00048C68 File Offset: 0x00046E68
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00048C75 File Offset: 0x00046E75
		string ICssStyleDeclaration.ColumnWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ColumnWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.ColumnWidth, value, null);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00048C84 File Offset: 0x00046E84
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x00048C91 File Offset: 0x00046E91
		string ICssStyleDeclaration.Content
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Content);
			}
			set
			{
				this.SetProperty(PropertyNames.Content, value, null);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00048CA0 File Offset: 0x00046EA0
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x00048CAD File Offset: 0x00046EAD
		string ICssStyleDeclaration.CounterIncrement
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.CounterIncrement);
			}
			set
			{
				this.SetProperty(PropertyNames.CounterIncrement, value, null);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00048CBC File Offset: 0x00046EBC
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00048CC9 File Offset: 0x00046EC9
		string ICssStyleDeclaration.CounterReset
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.CounterReset);
			}
			set
			{
				this.SetProperty(PropertyNames.CounterReset, value, null);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00048CD8 File Offset: 0x00046ED8
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00048CE5 File Offset: 0x00046EE5
		string ICssStyleDeclaration.Float
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Float);
			}
			set
			{
				this.SetProperty(PropertyNames.Float, value, null);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00048CF4 File Offset: 0x00046EF4
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x00048D01 File Offset: 0x00046F01
		string ICssStyleDeclaration.Cursor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Cursor);
			}
			set
			{
				this.SetProperty(PropertyNames.Cursor, value, null);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00048D10 File Offset: 0x00046F10
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x00048D1D File Offset: 0x00046F1D
		string ICssStyleDeclaration.Direction
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Direction);
			}
			set
			{
				this.SetProperty(PropertyNames.Direction, value, null);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00048D2C File Offset: 0x00046F2C
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x00048D39 File Offset: 0x00046F39
		string ICssStyleDeclaration.Display
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Display);
			}
			set
			{
				this.SetProperty(PropertyNames.Display, value, null);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00048D48 File Offset: 0x00046F48
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x00048D55 File Offset: 0x00046F55
		string ICssStyleDeclaration.DominantBaseline
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.DominantBaseline);
			}
			set
			{
				this.SetProperty(PropertyNames.DominantBaseline, value, null);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00048D64 File Offset: 0x00046F64
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x00048D71 File Offset: 0x00046F71
		string ICssStyleDeclaration.EmptyCells
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.EmptyCells);
			}
			set
			{
				this.SetProperty(PropertyNames.EmptyCells, value, null);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00048D80 File Offset: 0x00046F80
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00048D8D File Offset: 0x00046F8D
		string ICssStyleDeclaration.EnableBackground
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.EnableBackground);
			}
			set
			{
				this.SetProperty(PropertyNames.EnableBackground, value, null);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00048D9C File Offset: 0x00046F9C
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00048DA9 File Offset: 0x00046FA9
		string ICssStyleDeclaration.Fill
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Fill);
			}
			set
			{
				this.SetProperty(PropertyNames.Fill, value, null);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00048DB8 File Offset: 0x00046FB8
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00048DC5 File Offset: 0x00046FC5
		string ICssStyleDeclaration.FillOpacity
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FillOpacity);
			}
			set
			{
				this.SetProperty(PropertyNames.FillOpacity, value, null);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00048DD4 File Offset: 0x00046FD4
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00048DE1 File Offset: 0x00046FE1
		string ICssStyleDeclaration.FillRule
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FillRule);
			}
			set
			{
				this.SetProperty(PropertyNames.FillRule, value, null);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00048DF0 File Offset: 0x00046FF0
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00048DFD File Offset: 0x00046FFD
		string ICssStyleDeclaration.Filter
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Filter);
			}
			set
			{
				this.SetProperty(PropertyNames.Filter, value, null);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00048E0C File Offset: 0x0004700C
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00048E19 File Offset: 0x00047019
		string ICssStyleDeclaration.Flex
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Flex);
			}
			set
			{
				this.SetProperty(PropertyNames.Flex, value, null);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00048E28 File Offset: 0x00047028
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00048E35 File Offset: 0x00047035
		string ICssStyleDeclaration.FlexBasis
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FlexBasis);
			}
			set
			{
				this.SetProperty(PropertyNames.FlexBasis, value, null);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00048E44 File Offset: 0x00047044
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00048E51 File Offset: 0x00047051
		string ICssStyleDeclaration.FlexDirection
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FlexDirection);
			}
			set
			{
				this.SetProperty(PropertyNames.FlexDirection, value, null);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00048E60 File Offset: 0x00047060
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x00048E6D File Offset: 0x0004706D
		string ICssStyleDeclaration.FlexFlow
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FlexFlow);
			}
			set
			{
				this.SetProperty(PropertyNames.FlexFlow, value, null);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00048E7C File Offset: 0x0004707C
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x00048E89 File Offset: 0x00047089
		string ICssStyleDeclaration.FlexGrow
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FlexGrow);
			}
			set
			{
				this.SetProperty(PropertyNames.FlexGrow, value, null);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00048E98 File Offset: 0x00047098
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x00048EA5 File Offset: 0x000470A5
		string ICssStyleDeclaration.FlexShrink
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FlexShrink);
			}
			set
			{
				this.SetProperty(PropertyNames.FlexShrink, value, null);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00048EB4 File Offset: 0x000470B4
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x00048EC1 File Offset: 0x000470C1
		string ICssStyleDeclaration.FlexWrap
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FlexWrap);
			}
			set
			{
				this.SetProperty(PropertyNames.FlexWrap, value, null);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00048ED0 File Offset: 0x000470D0
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x00048EDD File Offset: 0x000470DD
		string ICssStyleDeclaration.Font
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Font);
			}
			set
			{
				this.SetProperty(PropertyNames.Font, value, null);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00048EEC File Offset: 0x000470EC
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x00048EF9 File Offset: 0x000470F9
		string ICssStyleDeclaration.FontFamily
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontFamily);
			}
			set
			{
				this.SetProperty(PropertyNames.FontFamily, value, null);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00048F08 File Offset: 0x00047108
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x00048F15 File Offset: 0x00047115
		string ICssStyleDeclaration.FontFeatureSettings
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontFeatureSettings);
			}
			set
			{
				this.SetProperty(PropertyNames.FontFeatureSettings, value, null);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00048F24 File Offset: 0x00047124
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x00048F31 File Offset: 0x00047131
		string ICssStyleDeclaration.FontSize
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontSize);
			}
			set
			{
				this.SetProperty(PropertyNames.FontSize, value, null);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00048F40 File Offset: 0x00047140
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x00048F4D File Offset: 0x0004714D
		string ICssStyleDeclaration.FontSizeAdjust
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontSizeAdjust);
			}
			set
			{
				this.SetProperty(PropertyNames.FontSizeAdjust, value, null);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00048F5C File Offset: 0x0004715C
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x00048F69 File Offset: 0x00047169
		string ICssStyleDeclaration.FontStretch
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontStretch);
			}
			set
			{
				this.SetProperty(PropertyNames.FontStretch, value, null);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00048F78 File Offset: 0x00047178
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x00048F85 File Offset: 0x00047185
		string ICssStyleDeclaration.FontStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.FontStyle, value, null);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00048F94 File Offset: 0x00047194
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00048FA1 File Offset: 0x000471A1
		string ICssStyleDeclaration.FontVariant
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontVariant);
			}
			set
			{
				this.SetProperty(PropertyNames.FontVariant, value, null);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00048FB0 File Offset: 0x000471B0
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x00048FBD File Offset: 0x000471BD
		string ICssStyleDeclaration.FontWeight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.FontWeight);
			}
			set
			{
				this.SetProperty(PropertyNames.FontWeight, value, null);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00048FCC File Offset: 0x000471CC
		// (set) Token: 0x0600126B RID: 4715 RVA: 0x00048FD9 File Offset: 0x000471D9
		string ICssStyleDeclaration.GlyphOrientationHorizontal
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.GlyphOrientationHorizontal);
			}
			set
			{
				this.SetProperty(PropertyNames.GlyphOrientationHorizontal, value, null);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x00048FE8 File Offset: 0x000471E8
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x00048FF5 File Offset: 0x000471F5
		string ICssStyleDeclaration.GlyphOrientationVertical
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.GlyphOrientationVertical);
			}
			set
			{
				this.SetProperty(PropertyNames.GlyphOrientationVertical, value, null);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00049004 File Offset: 0x00047204
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00049011 File Offset: 0x00047211
		string ICssStyleDeclaration.Height
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Height);
			}
			set
			{
				this.SetProperty(PropertyNames.Height, value, null);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00049020 File Offset: 0x00047220
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0004902D File Offset: 0x0004722D
		string ICssStyleDeclaration.ImeMode
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ImeMode);
			}
			set
			{
				this.SetProperty(PropertyNames.ImeMode, value, null);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0004903C File Offset: 0x0004723C
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00049049 File Offset: 0x00047249
		string ICssStyleDeclaration.JustifyContent
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.JustifyContent);
			}
			set
			{
				this.SetProperty(PropertyNames.JustifyContent, value, null);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00049058 File Offset: 0x00047258
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00049065 File Offset: 0x00047265
		string ICssStyleDeclaration.LayoutGrid
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LayoutGrid);
			}
			set
			{
				this.SetProperty(PropertyNames.LayoutGrid, value, null);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00049074 File Offset: 0x00047274
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00049081 File Offset: 0x00047281
		string ICssStyleDeclaration.LayoutGridChar
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LayoutGridChar);
			}
			set
			{
				this.SetProperty(PropertyNames.LayoutGridChar, value, null);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00049090 File Offset: 0x00047290
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0004909D File Offset: 0x0004729D
		string ICssStyleDeclaration.LayoutGridLine
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LayoutGridLine);
			}
			set
			{
				this.SetProperty(PropertyNames.LayoutGridLine, value, null);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x000490AC File Offset: 0x000472AC
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x000490B9 File Offset: 0x000472B9
		string ICssStyleDeclaration.LayoutGridMode
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LayoutGridMode);
			}
			set
			{
				this.SetProperty(PropertyNames.LayoutGridMode, value, null);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000490C8 File Offset: 0x000472C8
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x000490D5 File Offset: 0x000472D5
		string ICssStyleDeclaration.LayoutGridType
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LayoutGridType);
			}
			set
			{
				this.SetProperty(PropertyNames.LayoutGridType, value, null);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000490E4 File Offset: 0x000472E4
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x000490F1 File Offset: 0x000472F1
		string ICssStyleDeclaration.Left
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Left);
			}
			set
			{
				this.SetProperty(PropertyNames.Left, value, null);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00049100 File Offset: 0x00047300
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0004910D File Offset: 0x0004730D
		string ICssStyleDeclaration.LetterSpacing
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LetterSpacing);
			}
			set
			{
				this.SetProperty(PropertyNames.LetterSpacing, value, null);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0004911C File Offset: 0x0004731C
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00049129 File Offset: 0x00047329
		string ICssStyleDeclaration.LineHeight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.LineHeight);
			}
			set
			{
				this.SetProperty(PropertyNames.LineHeight, value, null);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00049138 File Offset: 0x00047338
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00049145 File Offset: 0x00047345
		string ICssStyleDeclaration.ListStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ListStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.ListStyle, value, null);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00049154 File Offset: 0x00047354
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00049161 File Offset: 0x00047361
		string ICssStyleDeclaration.ListStyleImage
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ListStyleImage);
			}
			set
			{
				this.SetProperty(PropertyNames.ListStyleImage, value, null);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00049170 File Offset: 0x00047370
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x0004917D File Offset: 0x0004737D
		string ICssStyleDeclaration.ListStylePosition
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ListStylePosition);
			}
			set
			{
				this.SetProperty(PropertyNames.ListStylePosition, value, null);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0004918C File Offset: 0x0004738C
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x00049199 File Offset: 0x00047399
		string ICssStyleDeclaration.ListStyleType
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ListStyleType);
			}
			set
			{
				this.SetProperty(PropertyNames.ListStyleType, value, null);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000491A8 File Offset: 0x000473A8
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x000491B5 File Offset: 0x000473B5
		string ICssStyleDeclaration.Margin
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Margin);
			}
			set
			{
				this.SetProperty(PropertyNames.Margin, value, null);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000491C4 File Offset: 0x000473C4
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x000491D1 File Offset: 0x000473D1
		string ICssStyleDeclaration.MarginBottom
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarginBottom);
			}
			set
			{
				this.SetProperty(PropertyNames.MarginBottom, value, null);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x000491E0 File Offset: 0x000473E0
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x000491ED File Offset: 0x000473ED
		string ICssStyleDeclaration.MarginLeft
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarginLeft);
			}
			set
			{
				this.SetProperty(PropertyNames.MarginLeft, value, null);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x000491FC File Offset: 0x000473FC
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00049209 File Offset: 0x00047409
		string ICssStyleDeclaration.MarginRight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarginRight);
			}
			set
			{
				this.SetProperty(PropertyNames.MarginRight, value, null);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00049218 File Offset: 0x00047418
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00049225 File Offset: 0x00047425
		string ICssStyleDeclaration.MarginTop
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarginTop);
			}
			set
			{
				this.SetProperty(PropertyNames.MarginTop, value, null);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00049234 File Offset: 0x00047434
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00049241 File Offset: 0x00047441
		string ICssStyleDeclaration.Marker
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Marker);
			}
			set
			{
				this.SetProperty(PropertyNames.Marker, value, null);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00049250 File Offset: 0x00047450
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x0004925D File Offset: 0x0004745D
		string ICssStyleDeclaration.MarkerEnd
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarkerEnd);
			}
			set
			{
				this.SetProperty(PropertyNames.MarkerEnd, value, null);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0004926C File Offset: 0x0004746C
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00049279 File Offset: 0x00047479
		string ICssStyleDeclaration.MarkerMid
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarkerMid);
			}
			set
			{
				this.SetProperty(PropertyNames.MarkerMid, value, null);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00049288 File Offset: 0x00047488
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x00049295 File Offset: 0x00047495
		string ICssStyleDeclaration.MarkerStart
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MarkerStart);
			}
			set
			{
				this.SetProperty(PropertyNames.MarkerStart, value, null);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x000492A4 File Offset: 0x000474A4
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x000492B1 File Offset: 0x000474B1
		string ICssStyleDeclaration.Mask
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Mask);
			}
			set
			{
				this.SetProperty(PropertyNames.Mask, value, null);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000492C0 File Offset: 0x000474C0
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x000492CD File Offset: 0x000474CD
		string ICssStyleDeclaration.MaxHeight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MaxHeight);
			}
			set
			{
				this.SetProperty(PropertyNames.MaxHeight, value, null);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000492DC File Offset: 0x000474DC
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x000492E9 File Offset: 0x000474E9
		string ICssStyleDeclaration.MaxWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MaxWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.MaxWidth, value, null);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000492F8 File Offset: 0x000474F8
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00049305 File Offset: 0x00047505
		string ICssStyleDeclaration.MinHeight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MinHeight);
			}
			set
			{
				this.SetProperty(PropertyNames.MinHeight, value, null);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00049314 File Offset: 0x00047514
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00049321 File Offset: 0x00047521
		string ICssStyleDeclaration.MinWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.MinWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.MinWidth, value, null);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00049330 File Offset: 0x00047530
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x0004933D File Offset: 0x0004753D
		string ICssStyleDeclaration.Opacity
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Opacity);
			}
			set
			{
				this.SetProperty(PropertyNames.Opacity, value, null);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004934C File Offset: 0x0004754C
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00049359 File Offset: 0x00047559
		string ICssStyleDeclaration.Order
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Order);
			}
			set
			{
				this.SetProperty(PropertyNames.Order, value, null);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x00049368 File Offset: 0x00047568
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00049375 File Offset: 0x00047575
		string ICssStyleDeclaration.Orphans
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Orphans);
			}
			set
			{
				this.SetProperty(PropertyNames.Orphans, value, null);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00049384 File Offset: 0x00047584
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00049391 File Offset: 0x00047591
		string ICssStyleDeclaration.Outline
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Outline);
			}
			set
			{
				this.SetProperty(PropertyNames.Outline, value, null);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x000493A0 File Offset: 0x000475A0
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x000493AD File Offset: 0x000475AD
		string ICssStyleDeclaration.OutlineColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.OutlineColor);
			}
			set
			{
				this.SetProperty(PropertyNames.OutlineColor, value, null);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000493BC File Offset: 0x000475BC
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x000493C9 File Offset: 0x000475C9
		string ICssStyleDeclaration.OutlineStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.OutlineStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.OutlineStyle, value, null);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000493D8 File Offset: 0x000475D8
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x000493E5 File Offset: 0x000475E5
		string ICssStyleDeclaration.OutlineWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.OutlineWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.OutlineWidth, value, null);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000493F4 File Offset: 0x000475F4
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x00049401 File Offset: 0x00047601
		string ICssStyleDeclaration.Overflow
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Overflow);
			}
			set
			{
				this.SetProperty(PropertyNames.Overflow, value, null);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00049410 File Offset: 0x00047610
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x0004941D File Offset: 0x0004761D
		string ICssStyleDeclaration.OverflowX
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.OverflowX);
			}
			set
			{
				this.SetProperty(PropertyNames.OverflowX, value, null);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0004942C File Offset: 0x0004762C
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x00049439 File Offset: 0x00047639
		string ICssStyleDeclaration.OverflowY
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.OverflowY);
			}
			set
			{
				this.SetProperty(PropertyNames.OverflowY, value, null);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00049448 File Offset: 0x00047648
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00049455 File Offset: 0x00047655
		string ICssStyleDeclaration.OverflowWrap
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.WordWrap);
			}
			set
			{
				this.SetProperty(PropertyNames.WordWrap, value, null);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00049464 File Offset: 0x00047664
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00049471 File Offset: 0x00047671
		string ICssStyleDeclaration.Padding
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Padding);
			}
			set
			{
				this.SetProperty(PropertyNames.Padding, value, null);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00049480 File Offset: 0x00047680
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x0004948D File Offset: 0x0004768D
		string ICssStyleDeclaration.PaddingBottom
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PaddingBottom);
			}
			set
			{
				this.SetProperty(PropertyNames.PaddingBottom, value, null);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0004949C File Offset: 0x0004769C
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x000494A9 File Offset: 0x000476A9
		string ICssStyleDeclaration.PaddingLeft
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PaddingLeft);
			}
			set
			{
				this.SetProperty(PropertyNames.PaddingLeft, value, null);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x000494B8 File Offset: 0x000476B8
		// (set) Token: 0x060012C5 RID: 4805 RVA: 0x000494C5 File Offset: 0x000476C5
		string ICssStyleDeclaration.PaddingRight
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PaddingRight);
			}
			set
			{
				this.SetProperty(PropertyNames.PaddingRight, value, null);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000494D4 File Offset: 0x000476D4
		// (set) Token: 0x060012C7 RID: 4807 RVA: 0x000494E1 File Offset: 0x000476E1
		string ICssStyleDeclaration.PaddingTop
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PaddingTop);
			}
			set
			{
				this.SetProperty(PropertyNames.PaddingTop, value, null);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000494F0 File Offset: 0x000476F0
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x000494FD File Offset: 0x000476FD
		string ICssStyleDeclaration.PageBreakAfter
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PageBreakAfter);
			}
			set
			{
				this.SetProperty(PropertyNames.PageBreakAfter, value, null);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0004950C File Offset: 0x0004770C
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x00049519 File Offset: 0x00047719
		string ICssStyleDeclaration.PageBreakBefore
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PageBreakBefore);
			}
			set
			{
				this.SetProperty(PropertyNames.PageBreakBefore, value, null);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00049528 File Offset: 0x00047728
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00049535 File Offset: 0x00047735
		string ICssStyleDeclaration.PageBreakInside
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PageBreakInside);
			}
			set
			{
				this.SetProperty(PropertyNames.PageBreakInside, value, null);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00049544 File Offset: 0x00047744
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00049551 File Offset: 0x00047751
		string ICssStyleDeclaration.Perspective
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Perspective);
			}
			set
			{
				this.SetProperty(PropertyNames.Perspective, value, null);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00049560 File Offset: 0x00047760
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0004956D File Offset: 0x0004776D
		string ICssStyleDeclaration.PerspectiveOrigin
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PerspectiveOrigin);
			}
			set
			{
				this.SetProperty(PropertyNames.PerspectiveOrigin, value, null);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0004957C File Offset: 0x0004777C
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00049589 File Offset: 0x00047789
		string ICssStyleDeclaration.PointerEvents
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.PointerEvents);
			}
			set
			{
				this.SetProperty(PropertyNames.PointerEvents, value, null);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00049598 File Offset: 0x00047798
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x000495A5 File Offset: 0x000477A5
		string ICssStyleDeclaration.Quotes
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Quotes);
			}
			set
			{
				this.SetProperty(PropertyNames.Quotes, value, null);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000495B4 File Offset: 0x000477B4
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x000495C1 File Offset: 0x000477C1
		string ICssStyleDeclaration.Position
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Position);
			}
			set
			{
				this.SetProperty(PropertyNames.Position, value, null);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x000495D0 File Offset: 0x000477D0
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x000495DD File Offset: 0x000477DD
		string ICssStyleDeclaration.Right
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Right);
			}
			set
			{
				this.SetProperty(PropertyNames.Right, value, null);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000495EC File Offset: 0x000477EC
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x000495F9 File Offset: 0x000477F9
		string ICssStyleDeclaration.RubyAlign
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.RubyAlign);
			}
			set
			{
				this.SetProperty(PropertyNames.RubyAlign, value, null);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00049608 File Offset: 0x00047808
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00049615 File Offset: 0x00047815
		string ICssStyleDeclaration.RubyOverhang
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.RubyOverhang);
			}
			set
			{
				this.SetProperty(PropertyNames.RubyOverhang, value, null);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00049624 File Offset: 0x00047824
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x00049631 File Offset: 0x00047831
		string ICssStyleDeclaration.RubyPosition
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.RubyPosition);
			}
			set
			{
				this.SetProperty(PropertyNames.RubyPosition, value, null);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00049640 File Offset: 0x00047840
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x0004964D File Offset: 0x0004784D
		string ICssStyleDeclaration.Scrollbar3dLightColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Scrollbar3dLightColor);
			}
			set
			{
				this.SetProperty(PropertyNames.Scrollbar3dLightColor, value, null);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0004965C File Offset: 0x0004785C
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00049669 File Offset: 0x00047869
		string ICssStyleDeclaration.ScrollbarArrowColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ScrollbarArrowColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ScrollbarArrowColor, value, null);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00049678 File Offset: 0x00047878
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00049685 File Offset: 0x00047885
		string ICssStyleDeclaration.ScrollbarDarkShadowColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ScrollbarDarkShadowColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ScrollbarDarkShadowColor, value, null);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00049694 File Offset: 0x00047894
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x000496A1 File Offset: 0x000478A1
		string ICssStyleDeclaration.ScrollbarFaceColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ScrollbarFaceColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ScrollbarFaceColor, value, null);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x000496B0 File Offset: 0x000478B0
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x000496BD File Offset: 0x000478BD
		string ICssStyleDeclaration.ScrollbarHighlightColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ScrollbarHighlightColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ScrollbarHighlightColor, value, null);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x000496CC File Offset: 0x000478CC
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x000496D9 File Offset: 0x000478D9
		string ICssStyleDeclaration.ScrollbarShadowColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ScrollbarShadowColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ScrollbarShadowColor, value, null);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x000496E8 File Offset: 0x000478E8
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x000496F5 File Offset: 0x000478F5
		string ICssStyleDeclaration.ScrollbarTrackColor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ScrollbarTrackColor);
			}
			set
			{
				this.SetProperty(PropertyNames.ScrollbarTrackColor, value, null);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00049704 File Offset: 0x00047904
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x00049711 File Offset: 0x00047911
		string ICssStyleDeclaration.Stroke
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Stroke);
			}
			set
			{
				this.SetProperty(PropertyNames.Stroke, value, null);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00049720 File Offset: 0x00047920
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x0004972D File Offset: 0x0004792D
		string ICssStyleDeclaration.StrokeDasharray
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeDasharray);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeDasharray, value, null);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0004973C File Offset: 0x0004793C
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00049749 File Offset: 0x00047949
		string ICssStyleDeclaration.StrokeDashoffset
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeDashoffset);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeDashoffset, value, null);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00049758 File Offset: 0x00047958
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00049765 File Offset: 0x00047965
		string ICssStyleDeclaration.StrokeLinecap
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeLinecap);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeLinecap, value, null);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00049774 File Offset: 0x00047974
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00049781 File Offset: 0x00047981
		string ICssStyleDeclaration.StrokeLinejoin
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeLinejoin);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeLinejoin, value, null);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00049790 File Offset: 0x00047990
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0004979D File Offset: 0x0004799D
		string ICssStyleDeclaration.StrokeMiterlimit
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeMiterlimit);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeMiterlimit, value, null);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x000497AC File Offset: 0x000479AC
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x000497B9 File Offset: 0x000479B9
		string ICssStyleDeclaration.StrokeOpacity
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeOpacity);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeOpacity, value, null);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x000497C8 File Offset: 0x000479C8
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x000497D5 File Offset: 0x000479D5
		string ICssStyleDeclaration.StrokeWidth
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.StrokeWidth);
			}
			set
			{
				this.SetProperty(PropertyNames.StrokeWidth, value, null);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x000497E4 File Offset: 0x000479E4
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x000497F1 File Offset: 0x000479F1
		string ICssStyleDeclaration.TableLayout
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TableLayout);
			}
			set
			{
				this.SetProperty(PropertyNames.TableLayout, value, null);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00049800 File Offset: 0x00047A00
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0004980D File Offset: 0x00047A0D
		string ICssStyleDeclaration.TextAlign
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextAlign);
			}
			set
			{
				this.SetProperty(PropertyNames.TextAlign, value, null);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0004981C File Offset: 0x00047A1C
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00049829 File Offset: 0x00047A29
		string ICssStyleDeclaration.TextAlignLast
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextAlignLast);
			}
			set
			{
				this.SetProperty(PropertyNames.TextAlignLast, value, null);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00049838 File Offset: 0x00047A38
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00049845 File Offset: 0x00047A45
		string ICssStyleDeclaration.TextAnchor
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextAnchor);
			}
			set
			{
				this.SetProperty(PropertyNames.TextAnchor, value, null);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00049854 File Offset: 0x00047A54
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x00049861 File Offset: 0x00047A61
		string ICssStyleDeclaration.TextAutospace
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextAutospace);
			}
			set
			{
				this.SetProperty(PropertyNames.TextAutospace, value, null);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00049870 File Offset: 0x00047A70
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x0004987D File Offset: 0x00047A7D
		string ICssStyleDeclaration.TextDecoration
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextDecoration);
			}
			set
			{
				this.SetProperty(PropertyNames.TextDecoration, value, null);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0004988C File Offset: 0x00047A8C
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00049899 File Offset: 0x00047A99
		string ICssStyleDeclaration.TextIndent
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextIndent);
			}
			set
			{
				this.SetProperty(PropertyNames.TextIndent, value, null);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x000498A8 File Offset: 0x00047AA8
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x000498B5 File Offset: 0x00047AB5
		string ICssStyleDeclaration.TextJustify
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextJustify);
			}
			set
			{
				this.SetProperty(PropertyNames.TextJustify, value, null);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000498C4 File Offset: 0x00047AC4
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x000498D1 File Offset: 0x00047AD1
		string ICssStyleDeclaration.TextOverflow
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextOverflow);
			}
			set
			{
				this.SetProperty(PropertyNames.TextOverflow, value, null);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000498E0 File Offset: 0x00047AE0
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x000498ED File Offset: 0x00047AED
		string ICssStyleDeclaration.TextShadow
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextShadow);
			}
			set
			{
				this.SetProperty(PropertyNames.TextShadow, value, null);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000498FC File Offset: 0x00047AFC
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00049909 File Offset: 0x00047B09
		string ICssStyleDeclaration.TextTransform
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextTransform);
			}
			set
			{
				this.SetProperty(PropertyNames.TextTransform, value, null);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00049918 File Offset: 0x00047B18
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00049925 File Offset: 0x00047B25
		string ICssStyleDeclaration.TextUnderlinePosition
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TextUnderlinePosition);
			}
			set
			{
				this.SetProperty(PropertyNames.TextUnderlinePosition, value, null);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00049934 File Offset: 0x00047B34
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00049941 File Offset: 0x00047B41
		string ICssStyleDeclaration.Top
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Top);
			}
			set
			{
				this.SetProperty(PropertyNames.Top, value, null);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00049950 File Offset: 0x00047B50
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x0004995D File Offset: 0x00047B5D
		string ICssStyleDeclaration.Transform
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Transform);
			}
			set
			{
				this.SetProperty(PropertyNames.Transform, value, null);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0004996C File Offset: 0x00047B6C
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00049979 File Offset: 0x00047B79
		string ICssStyleDeclaration.TransformOrigin
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TransformOrigin);
			}
			set
			{
				this.SetProperty(PropertyNames.TransformOrigin, value, null);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00049988 File Offset: 0x00047B88
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x00049995 File Offset: 0x00047B95
		string ICssStyleDeclaration.TransformStyle
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TransformStyle);
			}
			set
			{
				this.SetProperty(PropertyNames.TransformStyle, value, null);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x000499A4 File Offset: 0x00047BA4
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x000499B1 File Offset: 0x00047BB1
		string ICssStyleDeclaration.Transition
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Transition);
			}
			set
			{
				this.SetProperty(PropertyNames.Transition, value, null);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x000499C0 File Offset: 0x00047BC0
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x000499CD File Offset: 0x00047BCD
		string ICssStyleDeclaration.TransitionDelay
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TransitionDelay);
			}
			set
			{
				this.SetProperty(PropertyNames.TransitionDelay, value, null);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x000499DC File Offset: 0x00047BDC
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x000499E9 File Offset: 0x00047BE9
		string ICssStyleDeclaration.TransitionDuration
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TransitionDuration);
			}
			set
			{
				this.SetProperty(PropertyNames.TransitionDuration, value, null);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x000499F8 File Offset: 0x00047BF8
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x00049A05 File Offset: 0x00047C05
		string ICssStyleDeclaration.TransitionProperty
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TransitionProperty);
			}
			set
			{
				this.SetProperty(PropertyNames.TransitionProperty, value, null);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00049A14 File Offset: 0x00047C14
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x00049A21 File Offset: 0x00047C21
		string ICssStyleDeclaration.TransitionTimingFunction
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.TransitionTimingFunction);
			}
			set
			{
				this.SetProperty(PropertyNames.TransitionTimingFunction, value, null);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00049A30 File Offset: 0x00047C30
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x00049A3D File Offset: 0x00047C3D
		string ICssStyleDeclaration.UnicodeBidi
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.UnicodeBidi);
			}
			set
			{
				this.SetProperty(PropertyNames.UnicodeBidi, value, null);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00049A4C File Offset: 0x00047C4C
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x00049A59 File Offset: 0x00047C59
		string ICssStyleDeclaration.VerticalAlign
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.VerticalAlign);
			}
			set
			{
				this.SetProperty(PropertyNames.VerticalAlign, value, null);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00049A68 File Offset: 0x00047C68
		// (set) Token: 0x0600132D RID: 4909 RVA: 0x00049A75 File Offset: 0x00047C75
		string ICssStyleDeclaration.Visibility
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Visibility);
			}
			set
			{
				this.SetProperty(PropertyNames.Visibility, value, null);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00049A84 File Offset: 0x00047C84
		// (set) Token: 0x0600132F RID: 4911 RVA: 0x00049A91 File Offset: 0x00047C91
		string ICssStyleDeclaration.WhiteSpace
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.WhiteSpace);
			}
			set
			{
				this.SetProperty(PropertyNames.WhiteSpace, value, null);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00049AA0 File Offset: 0x00047CA0
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x00049AAD File Offset: 0x00047CAD
		string ICssStyleDeclaration.Widows
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Widows);
			}
			set
			{
				this.SetProperty(PropertyNames.Widows, value, null);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00049ABC File Offset: 0x00047CBC
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x00049AC9 File Offset: 0x00047CC9
		string ICssStyleDeclaration.Width
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Width);
			}
			set
			{
				this.SetProperty(PropertyNames.Width, value, null);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00049AD8 File Offset: 0x00047CD8
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x00049AE5 File Offset: 0x00047CE5
		string ICssStyleDeclaration.WordBreak
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.WordBreak);
			}
			set
			{
				this.SetProperty(PropertyNames.WordBreak, value, null);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00049AF4 File Offset: 0x00047CF4
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x00049B01 File Offset: 0x00047D01
		string ICssStyleDeclaration.WordSpacing
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.WordSpacing);
			}
			set
			{
				this.SetProperty(PropertyNames.WordSpacing, value, null);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00049B10 File Offset: 0x00047D10
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x00049B1D File Offset: 0x00047D1D
		string ICssStyleDeclaration.WritingMode
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.WritingMode);
			}
			set
			{
				this.SetProperty(PropertyNames.WritingMode, value, null);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00049B2C File Offset: 0x00047D2C
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x00049B39 File Offset: 0x00047D39
		string ICssStyleDeclaration.ZIndex
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.ZIndex);
			}
			set
			{
				this.SetProperty(PropertyNames.ZIndex, value, null);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00049B48 File Offset: 0x00047D48
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x00049B55 File Offset: 0x00047D55
		string ICssStyleDeclaration.Zoom
		{
			get
			{
				return this.GetPropertyValue(PropertyNames.Zoom);
			}
			set
			{
				this.SetProperty(PropertyNames.Zoom, value, null);
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00049B64 File Offset: 0x00047D64
		public void Update(string value)
		{
			if (this.IsReadOnly)
			{
				throw new DomException(DomError.NoModificationAllowed);
			}
			base.Clear();
			if (!string.IsNullOrEmpty(value))
			{
				this._parser.AppendDeclarations(this, value);
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00049B90 File Offset: 0x00047D90
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			List<string> list = new List<string>();
			List<string> serialized = new List<string>();
			Func<CssProperty, bool> <>9__0;
			foreach (CssProperty cssProperty in this.Declarations)
			{
				string name = cssProperty.Name;
				if (this.IsStrictMode)
				{
					if (serialized.Contains(name))
					{
						continue;
					}
					IEnumerable<string> shorthands = Factory.Properties.GetShorthands(name);
					if (shorthands.Any<string>())
					{
						IEnumerable<CssProperty> declarations = this.Declarations;
						Func<CssProperty, bool> func;
						if ((func = <>9__0) == null)
						{
							func = (<>9__0 = (CssProperty m) => !serialized.Contains(m.Name));
						}
						List<CssProperty> list2 = declarations.Where(func).ToList<CssProperty>();
						foreach (string text in shorthands.OrderByDescending((string m) => Factory.Properties.GetLonghands(m).Count<string>()))
						{
							CssShorthandProperty cssShorthandProperty = Factory.Properties.CreateShorthand(text);
							string[] properties = Factory.Properties.GetLonghands(text);
							CssProperty[] array = list2.Where((CssProperty m) => properties.Contains(m.Name, StringComparison.Ordinal)).ToArray<CssProperty>();
							if (array.Length != 0)
							{
								int num = array.Count((CssProperty m) => m.IsImportant);
								if ((num <= 0 || num == array.Length) && properties.Length == array.Length)
								{
									string text2 = cssShorthandProperty.Stringify(array);
									if (!string.IsNullOrEmpty(text2))
									{
										list.Add(CssStyleFormatter.Instance.Declaration(text, text2, num != 0));
										foreach (CssProperty cssProperty2 in array)
										{
											serialized.Add(cssProperty2.Name);
											list2.Remove(cssProperty2);
										}
									}
								}
							}
						}
					}
					if (serialized.Contains(name))
					{
						continue;
					}
					serialized.Add(name);
				}
				list.Add(cssProperty.ToCss(formatter));
			}
			writer.Write(formatter.Declarations(list));
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00049E0C File Offset: 0x0004800C
		public string RemoveProperty(string propertyName)
		{
			if (!this.IsReadOnly)
			{
				string propertyValue = this.GetPropertyValue(propertyName);
				this.RemovePropertyByName(propertyName);
				this.RaiseChanged();
				return propertyValue;
			}
			throw new DomException(DomError.NoModificationAllowed);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00049E34 File Offset: 0x00048034
		private void RemovePropertyByName(string propertyName)
		{
			foreach (CssProperty cssProperty in this.Declarations)
			{
				if (cssProperty.Name.Is(propertyName))
				{
					base.RemoveChild(cssProperty);
					break;
				}
			}
			if (this.IsStrictMode && Factory.Properties.IsShorthand(propertyName))
			{
				foreach (string text in Factory.Properties.GetLonghands(propertyName))
				{
					this.RemovePropertyByName(text);
				}
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00049ED0 File Offset: 0x000480D0
		public string GetPropertyPriority(string propertyName)
		{
			CssProperty property = this.GetProperty(propertyName);
			if (property != null && property.IsImportant)
			{
				return Keywords.Important;
			}
			if (this.IsStrictMode && Factory.Properties.IsShorthand(propertyName))
			{
				foreach (string text in Factory.Properties.GetLonghands(propertyName))
				{
					if (!this.GetPropertyPriority(text).Isi(Keywords.Important))
					{
						return string.Empty;
					}
				}
				return Keywords.Important;
			}
			return string.Empty;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00049F50 File Offset: 0x00048150
		public string GetPropertyValue(string propertyName)
		{
			CssProperty cssProperty = this.GetProperty(propertyName);
			if (cssProperty != null)
			{
				return cssProperty.Value;
			}
			if (this.IsStrictMode && Factory.Properties.IsShorthand(propertyName))
			{
				CssShorthandProperty cssShorthandProperty = Factory.Properties.CreateShorthand(propertyName);
				string[] longhands = Factory.Properties.GetLonghands(propertyName);
				List<CssProperty> list = new List<CssProperty>();
				foreach (string text in longhands)
				{
					cssProperty = this.GetProperty(text);
					if (cssProperty == null)
					{
						return string.Empty;
					}
					list.Add(cssProperty);
				}
				return cssShorthandProperty.Stringify(list.ToArray());
			}
			return string.Empty;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00049FE5 File Offset: 0x000481E5
		public void SetPropertyValue(string propertyName, string propertyValue)
		{
			this.SetProperty(propertyName, propertyValue, null);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00049FF0 File Offset: 0x000481F0
		public void SetPropertyPriority(string propertyName, string priority)
		{
			if (this.IsReadOnly)
			{
				throw new DomException(DomError.NoModificationAllowed);
			}
			if (string.IsNullOrEmpty(priority) || priority.Isi(Keywords.Important))
			{
				bool flag = !string.IsNullOrEmpty(priority);
				IEnumerable<string> enumerable;
				if (!this.IsStrictMode || !Factory.Properties.IsShorthand(propertyName))
				{
					enumerable = Enumerable.Repeat<string>(propertyName, 1);
				}
				else
				{
					IEnumerable<string> longhands = Factory.Properties.GetLonghands(propertyName);
					enumerable = longhands;
				}
				foreach (string text in enumerable)
				{
					CssProperty property = this.GetProperty(text);
					if (property != null)
					{
						property.IsImportant = flag;
					}
				}
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0004A0A0 File Offset: 0x000482A0
		public void SetProperty(string propertyName, string propertyValue, string priority = null)
		{
			if (this.IsReadOnly)
			{
				throw new DomException(DomError.NoModificationAllowed);
			}
			if (!string.IsNullOrEmpty(propertyValue))
			{
				if (priority == null || priority.Isi(Keywords.Important))
				{
					CssValue cssValue = this._parser.ParseValue(propertyValue);
					if (cssValue != null)
					{
						CssProperty cssProperty = this.CreateProperty(propertyName);
						if (cssProperty != null && cssProperty.TrySetValue(cssValue))
						{
							cssProperty.IsImportant = priority != null;
							this.SetProperty(cssProperty);
							this.RaiseChanged();
							return;
						}
					}
				}
			}
			else
			{
				this.RemoveProperty(propertyName);
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004A118 File Offset: 0x00048318
		internal CssProperty CreateProperty(string propertyName)
		{
			CssProperty cssProperty = this.GetProperty(propertyName);
			if (cssProperty != null)
			{
				return cssProperty;
			}
			cssProperty = Factory.Properties.Create(propertyName);
			if (cssProperty != null || this.IsStrictMode)
			{
				return cssProperty;
			}
			return new CssUnknownProperty(propertyName);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004A154 File Offset: 0x00048354
		internal CssProperty GetProperty(string name)
		{
			return this.Declarations.Where((CssProperty m) => m.Name.Isi(name)).FirstOrDefault<CssProperty>();
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004A18A File Offset: 0x0004838A
		internal void SetProperty(CssProperty property)
		{
			if (property is CssShorthandProperty)
			{
				this.SetShorthand((CssShorthandProperty)property);
				return;
			}
			this.SetLonghand(property);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004A1A8 File Offset: 0x000483A8
		internal void SetDeclarations(IEnumerable<CssProperty> decls)
		{
			this.ChangeDeclarations(decls, (CssProperty m) => false, (CssProperty o, CssProperty n) => !o.IsImportant || n.IsImportant);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004A1FC File Offset: 0x000483FC
		internal void UpdateDeclarations(IEnumerable<CssProperty> decls)
		{
			this.ChangeDeclarations(decls, (CssProperty m) => !m.CanBeInherited, (CssProperty o, CssProperty n) => o.IsInherited);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004A250 File Offset: 0x00048450
		private void ChangeDeclarations(IEnumerable<CssProperty> decls, Predicate<CssProperty> defaultSkip, Func<CssProperty, CssProperty, bool> removeExisting)
		{
			List<CssProperty> list = new List<CssProperty>();
			foreach (CssProperty cssProperty in decls)
			{
				bool flag = defaultSkip(cssProperty);
				foreach (CssProperty cssProperty2 in this.Declarations)
				{
					if (cssProperty2.Name.Is(cssProperty.Name))
					{
						if (removeExisting(cssProperty2, cssProperty))
						{
							base.RemoveChild(cssProperty2);
							break;
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(cssProperty);
				}
			}
			foreach (CssProperty cssProperty3 in list)
			{
				base.AppendChild(cssProperty3);
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004A354 File Offset: 0x00048554
		private void SetLonghand(CssProperty property)
		{
			foreach (CssProperty cssProperty in this.Declarations)
			{
				if (cssProperty.Name.Is(property.Name))
				{
					base.RemoveChild(cssProperty);
					break;
				}
			}
			base.AppendChild(property);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004A3C0 File Offset: 0x000485C0
		private void SetShorthand(CssShorthandProperty shorthand)
		{
			CssProperty[] array = Factory.Properties.CreateLonghandsFor(shorthand.Name);
			shorthand.Export(array);
			foreach (CssProperty cssProperty in array)
			{
				this.SetLonghand(cssProperty);
			}
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004A400 File Offset: 0x00048600
		private void RaiseChanged()
		{
			Action<string> changed = this.Changed;
			if (changed == null)
			{
				return;
			}
			changed(this.CssText);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0004A418 File Offset: 0x00048618
		public IEnumerator<ICssProperty> GetEnumerator()
		{
			return this.Declarations.GetEnumerator();
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0004A425 File Offset: 0x00048625
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000A96 RID: 2710
		private readonly CssRule _parent;

		// Token: 0x04000A97 RID: 2711
		private readonly CssParser _parser;
	}
}
