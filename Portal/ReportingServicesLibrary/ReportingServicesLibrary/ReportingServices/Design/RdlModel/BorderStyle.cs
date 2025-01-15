using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000413 RID: 1043
	public sealed class BorderStyle : IVoluntarySerializable
	{
		// Token: 0x06002118 RID: 8472 RVA: 0x000025F4 File Offset: 0x000007F4
		public BorderStyle()
		{
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000803C9 File Offset: 0x0007E5C9
		public BorderStyle(string defaultStyle)
		{
			this.Default = defaultStyle;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000803D8 File Offset: 0x0007E5D8
		public BorderStyle(string top, string bottom, string left, string right)
		{
			this.Left = left;
			this.Right = right;
			this.Top = top;
			this.Bottom = bottom;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x000803FD File Offset: 0x0007E5FD
		[XmlIgnore]
		public string TopStyle
		{
			get
			{
				return this.GetDrawingStyle(this.Top);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0008040B File Offset: 0x0007E60B
		[XmlIgnore]
		public string BottomStyle
		{
			get
			{
				return this.GetDrawingStyle(this.Bottom);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x00080419 File Offset: 0x0007E619
		[XmlIgnore]
		public string LeftStyle
		{
			get
			{
				return this.GetDrawingStyle(this.Left);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x00080427 File Offset: 0x0007E627
		[XmlIgnore]
		public string RightStyle
		{
			get
			{
				return this.GetDrawingStyle(this.Right);
			}
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00080435 File Offset: 0x0007E635
		private string GetDrawingStyle(string style)
		{
			if (!string.IsNullOrEmpty(style))
			{
				return style;
			}
			if (string.IsNullOrEmpty(this.Default))
			{
				return "None";
			}
			return this.Default;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0008045C File Offset: 0x0007E65C
		public bool ShouldBeSerialized()
		{
			return (this.Default != null && this.Default != "None") || (this.Left != null && this.Left != "None") || (this.Right != null && this.Right != "None") || (this.Top != null && this.Top != "None") || (this.Bottom != null && this.Bottom != "None");
		}

		// Token: 0x04000E80 RID: 3712
		[DefaultValue("None")]
		public string Default;

		// Token: 0x04000E81 RID: 3713
		[DefaultValue("None")]
		public string Left;

		// Token: 0x04000E82 RID: 3714
		[DefaultValue("None")]
		public string Right;

		// Token: 0x04000E83 RID: 3715
		[DefaultValue("None")]
		public string Top;

		// Token: 0x04000E84 RID: 3716
		[DefaultValue("None")]
		public string Bottom;

		// Token: 0x04000E85 RID: 3717
		public const string DefaultStyle = "None";

		// Token: 0x04000E86 RID: 3718
		public const string None = "None";

		// Token: 0x04000E87 RID: 3719
		public const string Solid = "Solid";

		// Token: 0x04000E88 RID: 3720
		public const string Dotted = "Dotted";

		// Token: 0x04000E89 RID: 3721
		public const string Dashed = "Dashed";

		// Token: 0x04000E8A RID: 3722
		public const string Double = "Double";

		// Token: 0x04000E8B RID: 3723
		public const string Groove = "Groove";

		// Token: 0x04000E8C RID: 3724
		public const string Ridge = "Ridge";

		// Token: 0x04000E8D RID: 3725
		public const string Inset = "Inset";

		// Token: 0x04000E8E RID: 3726
		public const string WindowInset = "WindowInset";

		// Token: 0x04000E8F RID: 3727
		public const string Outset = "Outset";
	}
}
