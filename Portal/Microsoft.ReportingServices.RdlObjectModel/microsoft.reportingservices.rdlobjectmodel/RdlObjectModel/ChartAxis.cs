using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200008E RID: 142
	public class ChartAxis : ReportObject, INamedObject
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00019012 File Offset: 0x00017212
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00019025 File Offset: 0x00017225
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00019034 File Offset: 0x00017234
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00019042 File Offset: 0x00017242
		[ReportExpressionDefaultValue(typeof(ChartVisibleTypes), ChartVisibleTypes.Auto)]
		public ReportExpression<ChartVisibleTypes> Visible
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartVisibleTypes>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00019056 File Offset: 0x00017256
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00019069 File Offset: 0x00017269
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00019078 File Offset: 0x00017278
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0001908B File Offset: 0x0001728B
		public ChartAxisTitle ChartAxisTitle
		{
			get
			{
				return (ChartAxisTitle)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001909A File Offset: 0x0001729A
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x000190A8 File Offset: 0x000172A8
		[ReportExpressionDefaultValue(typeof(ChartAxisMarginVisibleTypes), ChartAxisMarginVisibleTypes.Auto)]
		public ReportExpression<ChartAxisMarginVisibleTypes> Margin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartAxisMarginVisibleTypes>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000190BC File Offset: 0x000172BC
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x000190CA File Offset: 0x000172CA
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000190DE File Offset: 0x000172DE
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x000190EC File Offset: 0x000172EC
		[ReportExpressionDefaultValue(typeof(ChartIntervalTypes), ChartIntervalTypes.Auto)]
		public ReportExpression<ChartIntervalTypes> IntervalType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00019100 File Offset: 0x00017300
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0001910E File Offset: 0x0001730E
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00019122 File Offset: 0x00017322
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x00019130 File Offset: 0x00017330
		[ReportExpressionDefaultValue(typeof(ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Auto)]
		public ReportExpression<ChartIntervalOffsetTypes> IntervalOffsetType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00019144 File Offset: 0x00017344
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00019153 File Offset: 0x00017353
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> VariableAutoInterval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00019168 File Offset: 0x00017368
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00019177 File Offset: 0x00017377
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> LabelInterval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001918C File Offset: 0x0001738C
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001919B File Offset: 0x0001739B
		[ReportExpressionDefaultValue(typeof(ChartIntervalTypes), ChartIntervalTypes.Default)]
		public ReportExpression<ChartIntervalTypes> LabelIntervalType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalTypes>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x000191B0 File Offset: 0x000173B0
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x000191BF File Offset: 0x000173BF
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> LabelIntervalOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x000191D4 File Offset: 0x000173D4
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x000191E3 File Offset: 0x000173E3
		[ReportExpressionDefaultValue(typeof(ChartIntervalOffsetTypes), ChartIntervalOffsetTypes.Default)]
		public ReportExpression<ChartIntervalOffsetTypes> LabelIntervalOffsetType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartIntervalOffsetTypes>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x000191F8 File Offset: 0x000173F8
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001920C File Offset: 0x0001740C
		public ChartGridLines ChartMajorGridLines
		{
			get
			{
				return (ChartGridLines)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001921C File Offset: 0x0001741C
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00019230 File Offset: 0x00017430
		public ChartGridLines ChartMinorGridLines
		{
			get
			{
				return (ChartGridLines)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00019240 File Offset: 0x00017440
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00019254 File Offset: 0x00017454
		public ChartTickMarks ChartMajorTickMarks
		{
			get
			{
				return (ChartTickMarks)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00019264 File Offset: 0x00017464
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00019278 File Offset: 0x00017478
		public ChartTickMarks ChartMinorTickMarks
		{
			get
			{
				return (ChartTickMarks)base.PropertyStore.GetObject(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00019288 File Offset: 0x00017488
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00019297 File Offset: 0x00017497
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> MarksAlwaysAtPlotEdge
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000192AC File Offset: 0x000174AC
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x000192BB File Offset: 0x000174BB
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Reverse
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000192D0 File Offset: 0x000174D0
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x000192DF File Offset: 0x000174DF
		[ReportExpressionDefaultValue]
		public ReportExpression CrossAt
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000192F4 File Offset: 0x000174F4
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00019303 File Offset: 0x00017503
		[ReportExpressionDefaultValue(typeof(ChartAxisLocations), ChartAxisLocations.Default)]
		public ReportExpression<ChartAxisLocations> Location
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartAxisLocations>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00019318 File Offset: 0x00017518
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00019327 File Offset: 0x00017527
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Interlaced
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001933C File Offset: 0x0001753C
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0001934B File Offset: 0x0001754B
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> InterlacedColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00019360 File Offset: 0x00017560
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00019374 File Offset: 0x00017574
		[XmlElement(typeof(RdlCollection<ChartStripLine>))]
		public IList<ChartStripLine> ChartStripLines
		{
			get
			{
				return (IList<ChartStripLine>)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00019384 File Offset: 0x00017584
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00019393 File Offset: 0x00017593
		[ReportExpressionDefaultValue(typeof(ChartArrowsTypes), ChartArrowsTypes.None)]
		public ReportExpression<ChartArrowsTypes> Arrows
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartArrowsTypes>>(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000193A8 File Offset: 0x000175A8
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x000193B7 File Offset: 0x000175B7
		[DefaultValue(false)]
		public bool Scalar
		{
			get
			{
				return base.PropertyStore.GetBoolean(26);
			}
			set
			{
				base.PropertyStore.SetBoolean(26, value);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000193C7 File Offset: 0x000175C7
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x000193D6 File Offset: 0x000175D6
		[ReportExpressionDefaultValue]
		public ReportExpression Minimum
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000193EB File Offset: 0x000175EB
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x000193FA File Offset: 0x000175FA
		[ReportExpressionDefaultValue]
		public ReportExpression Maximum
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001940F File Offset: 0x0001760F
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001941E File Offset: 0x0001761E
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> LogScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(29);
			}
			set
			{
				base.PropertyStore.SetObject(29, value);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00019433 File Offset: 0x00017633
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00019442 File Offset: 0x00017642
		[ReportExpressionDefaultValue(typeof(double), 10.0)]
		public ReportExpression<double> LogBase
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(30);
			}
			set
			{
				base.PropertyStore.SetObject(30, value);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00019457 File Offset: 0x00017657
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00019466 File Offset: 0x00017666
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> HideLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(31);
			}
			set
			{
				base.PropertyStore.SetObject(31, value);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001947B File Offset: 0x0001767B
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001948A File Offset: 0x0001768A
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Angle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(32);
			}
			set
			{
				base.PropertyStore.SetObject(32, value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001949F File Offset: 0x0001769F
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x000194AE File Offset: 0x000176AE
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> PreventFontShrink
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(33);
			}
			set
			{
				base.PropertyStore.SetObject(33, value);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000194C3 File Offset: 0x000176C3
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x000194D2 File Offset: 0x000176D2
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> PreventFontGrow
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(34);
			}
			set
			{
				base.PropertyStore.SetObject(34, value);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000194E7 File Offset: 0x000176E7
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x000194F6 File Offset: 0x000176F6
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> PreventLabelOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(35);
			}
			set
			{
				base.PropertyStore.SetObject(35, value);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001950B File Offset: 0x0001770B
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001951A File Offset: 0x0001771A
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> PreventWordWrap
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001952F File Offset: 0x0001772F
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001953E File Offset: 0x0001773E
		[ReportExpressionDefaultValue(typeof(ChartLabelRotationTypes), ChartLabelRotationTypes.Rotate90)]
		public ReportExpression<ChartLabelRotationTypes> AllowLabelRotation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartLabelRotationTypes>>(37);
			}
			set
			{
				base.PropertyStore.SetObject(37, value);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00019553 File Offset: 0x00017753
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00019562 File Offset: 0x00017762
		[ReportExpressionDefaultValue(typeof(bool), true)]
		public ReportExpression<bool> IncludeZero
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(38);
			}
			set
			{
				base.PropertyStore.SetObject(38, value);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00019577 File Offset: 0x00017777
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00019586 File Offset: 0x00017786
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> LabelsAutoFitDisabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(39);
			}
			set
			{
				base.PropertyStore.SetObject(39, value);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001959B File Offset: 0x0001779B
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x000195AA File Offset: 0x000177AA
		[ReportExpressionDefaultValue(typeof(ReportSize), "6pt")]
		public ReportExpression<ReportSize> MinFontSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(40);
			}
			set
			{
				base.PropertyStore.SetObject(40, value);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000195BF File Offset: 0x000177BF
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x000195CE File Offset: 0x000177CE
		[ReportExpressionDefaultValue(typeof(ReportSize), "10pt")]
		public ReportExpression<ReportSize> MaxFontSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(41);
			}
			set
			{
				base.PropertyStore.SetObject(41, value);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000195E3 File Offset: 0x000177E3
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x000195F2 File Offset: 0x000177F2
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> OffsetLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(42);
			}
			set
			{
				base.PropertyStore.SetObject(42, value);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00019607 File Offset: 0x00017807
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00019616 File Offset: 0x00017816
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> HideEndLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(43);
			}
			set
			{
				base.PropertyStore.SetObject(43, value);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001962B File Offset: 0x0001782B
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0001963F File Offset: 0x0001783F
		public ChartAxisScaleBreak ChartAxisScaleBreak
		{
			get
			{
				return (ChartAxisScaleBreak)base.PropertyStore.GetObject(44);
			}
			set
			{
				base.PropertyStore.SetObject(44, value);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001964F File Offset: 0x0001784F
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00019663 File Offset: 0x00017863
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(45);
			}
			set
			{
				base.PropertyStore.SetObject(45, value);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019673 File Offset: 0x00017873
		public ChartAxis()
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001967B File Offset: 0x0001787B
		internal ChartAxis(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019684 File Offset: 0x00017884
		public override void Initialize()
		{
			base.Initialize();
			this.ChartStripLines = new RdlCollection<ChartStripLine>();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.IntervalType = ChartIntervalTypes.Auto;
			this.IntervalOffsetType = ChartIntervalOffsetTypes.Auto;
			this.LabelIntervalType = ChartIntervalTypes.Default;
			this.LabelIntervalOffsetType = ChartIntervalOffsetTypes.Default;
			this.LogBase = 10.0;
			this.IncludeZero = true;
		}

		// Token: 0x02000344 RID: 836
		internal class Definition : DefinitionStore<ChartAxis, ChartAxis.Definition.Properties>
		{
			// Token: 0x060017C7 RID: 6087 RVA: 0x0003AB5B File Offset: 0x00038D5B
			private Definition()
			{
			}

			// Token: 0x02000463 RID: 1123
			internal enum Properties
			{
				// Token: 0x040009BD RID: 2493
				Name,
				// Token: 0x040009BE RID: 2494
				Visible,
				// Token: 0x040009BF RID: 2495
				Style,
				// Token: 0x040009C0 RID: 2496
				ChartAxisTitle,
				// Token: 0x040009C1 RID: 2497
				Margin,
				// Token: 0x040009C2 RID: 2498
				Interval,
				// Token: 0x040009C3 RID: 2499
				IntervalType,
				// Token: 0x040009C4 RID: 2500
				IntervalOffset,
				// Token: 0x040009C5 RID: 2501
				IntervalOffsetType,
				// Token: 0x040009C6 RID: 2502
				VariableAutoInterval,
				// Token: 0x040009C7 RID: 2503
				LabelInterval,
				// Token: 0x040009C8 RID: 2504
				LabelIntervalType,
				// Token: 0x040009C9 RID: 2505
				LabelIntervalOffset,
				// Token: 0x040009CA RID: 2506
				LabelIntervalOffsetType,
				// Token: 0x040009CB RID: 2507
				ChartMajorGridLines,
				// Token: 0x040009CC RID: 2508
				ChartMinorGridLines,
				// Token: 0x040009CD RID: 2509
				ChartMajorTickMarks,
				// Token: 0x040009CE RID: 2510
				ChartMinorTickMarks,
				// Token: 0x040009CF RID: 2511
				MarksAlwaysAtPlotEdge,
				// Token: 0x040009D0 RID: 2512
				Reverse,
				// Token: 0x040009D1 RID: 2513
				CrossAt,
				// Token: 0x040009D2 RID: 2514
				Location,
				// Token: 0x040009D3 RID: 2515
				Interlaced,
				// Token: 0x040009D4 RID: 2516
				InterlacedColor,
				// Token: 0x040009D5 RID: 2517
				ChartStripLines,
				// Token: 0x040009D6 RID: 2518
				Arrows,
				// Token: 0x040009D7 RID: 2519
				Scalar,
				// Token: 0x040009D8 RID: 2520
				Minimum,
				// Token: 0x040009D9 RID: 2521
				Maximum,
				// Token: 0x040009DA RID: 2522
				LogScale,
				// Token: 0x040009DB RID: 2523
				LogBase,
				// Token: 0x040009DC RID: 2524
				HideLabels,
				// Token: 0x040009DD RID: 2525
				Angle,
				// Token: 0x040009DE RID: 2526
				PreventFontShrink,
				// Token: 0x040009DF RID: 2527
				PreventFontGrow,
				// Token: 0x040009E0 RID: 2528
				PreventLabelOffset,
				// Token: 0x040009E1 RID: 2529
				PreventWordWrap,
				// Token: 0x040009E2 RID: 2530
				AllowLabelRotation,
				// Token: 0x040009E3 RID: 2531
				IncludeZero,
				// Token: 0x040009E4 RID: 2532
				LabelsAutoFitDisabled,
				// Token: 0x040009E5 RID: 2533
				MinFontSize,
				// Token: 0x040009E6 RID: 2534
				MaxFontSize,
				// Token: 0x040009E7 RID: 2535
				OffsetLabels,
				// Token: 0x040009E8 RID: 2536
				HideEndLabels,
				// Token: 0x040009E9 RID: 2537
				ChartAxisScaleBreak,
				// Token: 0x040009EA RID: 2538
				CustomProperties,
				// Token: 0x040009EB RID: 2539
				PropertyCount
			}
		}
	}
}
