using System;
using System.Drawing;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000414 RID: 1044
	public sealed class BorderColor : IVoluntarySerializable
	{
		// Token: 0x06002121 RID: 8481 RVA: 0x000804F8 File Offset: 0x0007E6F8
		public BorderColor()
		{
			this.Left.Empty();
			this.Right.Empty();
			this.Top.Empty();
			this.Bottom.Empty();
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00080587 File Offset: 0x0007E787
		public void Set(Color color)
		{
			this.Default.ColorRgb = color;
			this.Left.Empty();
			this.Right.Empty();
			this.Top.Empty();
			this.Bottom.Empty();
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000805C1 File Offset: 0x0007E7C1
		public void Set(Color top, Color bottom, Color left, Color right)
		{
			this.Default.Empty();
			this.Left.ColorRgb = left;
			this.Right.ColorRgb = right;
			this.Top.ColorRgb = top;
			this.Bottom.ColorRgb = bottom;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x000805FF File Offset: 0x0007E7FF
		[XmlIgnore]
		public Color TopColor
		{
			get
			{
				return this.GetDrawingColor(this.Top);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x0008060D File Offset: 0x0007E80D
		[XmlIgnore]
		public Color BottomColor
		{
			get
			{
				return this.GetDrawingColor(this.Bottom);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x0008061B File Offset: 0x0007E81B
		[XmlIgnore]
		public Color LeftColor
		{
			get
			{
				return this.GetDrawingColor(this.Left);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00080629 File Offset: 0x0007E829
		[XmlIgnore]
		public Color RightColor
		{
			get
			{
				return this.GetDrawingColor(this.Right);
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00080638 File Offset: 0x0007E838
		private Color GetDrawingColor(ReportColor color)
		{
			if (color.IsEmpty)
			{
				if (this.Default.IsColorRgb)
				{
					return this.Default.ColorRgb;
				}
				return Style.Definition.BorderColor.Default;
			}
			else
			{
				if (color.IsColorRgb)
				{
					return color.ColorRgb;
				}
				return Style.Definition.BorderColor.Default;
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00080690 File Offset: 0x0007E890
		public bool ShouldBeSerialized()
		{
			return ((IVoluntarySerializable)this.Default).ShouldBeSerialized() || ((IVoluntarySerializable)this.Left).ShouldBeSerialized() || ((IVoluntarySerializable)this.Right).ShouldBeSerialized() || ((IVoluntarySerializable)this.Top).ShouldBeSerialized() || ((IVoluntarySerializable)this.Bottom).ShouldBeSerialized();
		}

		// Token: 0x04000E90 RID: 3728
		public ReportColor Default = new ReportColor(Style.Definition.BorderColor);

		// Token: 0x04000E91 RID: 3729
		public ReportColor Left = new ReportColor(Style.Definition.BorderColor);

		// Token: 0x04000E92 RID: 3730
		public ReportColor Right = new ReportColor(Style.Definition.BorderColor);

		// Token: 0x04000E93 RID: 3731
		public ReportColor Top = new ReportColor(Style.Definition.BorderColor);

		// Token: 0x04000E94 RID: 3732
		public ReportColor Bottom = new ReportColor(Style.Definition.BorderColor);
	}
}
