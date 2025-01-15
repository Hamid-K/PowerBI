using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000415 RID: 1045
	public sealed class BorderWidth : IVoluntarySerializable
	{
		// Token: 0x0600212A RID: 8490 RVA: 0x00080704 File Offset: 0x0007E904
		public BorderWidth()
		{
			this.Left.Empty();
			this.Right.Empty();
			this.Top.Empty();
			this.Bottom.Empty();
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00080793 File Offset: 0x0007E993
		public void Set(double defaultWidth, UnitType unit)
		{
			this.Default.BaseUnit = new Unit(defaultWidth, unit);
			this.Left.Empty();
			this.Right.Empty();
			this.Top.Empty();
			this.Bottom.Empty();
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000807D4 File Offset: 0x0007E9D4
		public void Set(double top, double bottom, double left, double right, UnitType unit)
		{
			this.Default.Empty();
			this.Left.BaseUnit = new Unit(left, unit);
			this.Right.BaseUnit = new Unit(right, unit);
			this.Top.BaseUnit = new Unit(top, unit);
			this.Bottom.BaseUnit = new Unit(bottom, unit);
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x00080839 File Offset: 0x0007EA39
		[XmlIgnore]
		public float TopWidth
		{
			get
			{
				return this.GetDrawingWidth(this.Top);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x00080847 File Offset: 0x0007EA47
		[XmlIgnore]
		public float BottomWidth
		{
			get
			{
				return this.GetDrawingWidth(this.Bottom);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x00080855 File Offset: 0x0007EA55
		[XmlIgnore]
		public float LeftWidth
		{
			get
			{
				return this.GetDrawingWidth(this.Left);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x00080863 File Offset: 0x0007EA63
		[XmlIgnore]
		public float RightWidth
		{
			get
			{
				return this.GetDrawingWidth(this.Right);
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00080874 File Offset: 0x0007EA74
		private float GetDrawingWidth(StyleUnit styleunit)
		{
			Unit unit = Style.Definition.BorderWidth.Default;
			if (styleunit.IsEmpty)
			{
				unit = this.Default.BaseUnit;
			}
			else
			{
				unit = styleunit.BaseUnit;
			}
			unit.ChangeType(UnitType.Point);
			return (float)unit.Value;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000808BC File Offset: 0x0007EABC
		public bool ShouldBeSerialized()
		{
			return ((IVoluntarySerializable)this.Default).ShouldBeSerialized() || ((IVoluntarySerializable)this.Left).ShouldBeSerialized() || ((IVoluntarySerializable)this.Right).ShouldBeSerialized() || ((IVoluntarySerializable)this.Top).ShouldBeSerialized() || ((IVoluntarySerializable)this.Bottom).ShouldBeSerialized();
		}

		// Token: 0x04000E95 RID: 3733
		public StyleUnit Default = new StyleUnit(Style.Definition.BorderWidth);

		// Token: 0x04000E96 RID: 3734
		public StyleUnit Left = new StyleUnit(Style.Definition.BorderWidth);

		// Token: 0x04000E97 RID: 3735
		public StyleUnit Right = new StyleUnit(Style.Definition.BorderWidth);

		// Token: 0x04000E98 RID: 3736
		public StyleUnit Top = new StyleUnit(Style.Definition.BorderWidth);

		// Token: 0x04000E99 RID: 3737
		public StyleUnit Bottom = new StyleUnit(Style.Definition.BorderWidth);
	}
}
