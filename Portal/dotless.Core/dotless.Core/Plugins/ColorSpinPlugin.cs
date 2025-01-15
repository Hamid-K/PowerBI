using System;
using System.ComponentModel;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Plugins
{
	// Token: 0x02000015 RID: 21
	[Description("Automatically spins all colors in a less file")]
	[DisplayName("ColorSpin")]
	public class ColorSpinPlugin : VisitorPlugin
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003939 File Offset: 0x00001B39
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003941 File Offset: 0x00001B41
		public double Spin { get; set; }

		// Token: 0x0600008D RID: 141 RVA: 0x0000394A File Offset: 0x00001B4A
		public ColorSpinPlugin(double spin)
		{
			this.Spin = spin;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003959 File Offset: 0x00001B59
		public override VisitorPluginType AppliesTo
		{
			get
			{
				return VisitorPluginType.AfterEvaluation;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000395C File Offset: 0x00001B5C
		public override Node Execute(Node node, out bool visitDeeper)
		{
			visitDeeper = true;
			Color color = node as Color;
			if (color == null)
			{
				return node;
			}
			HslColor hslColor = HslColor.FromRgbColor(color);
			hslColor.Hue += this.Spin / 360.0;
			return hslColor.ToRgbColor().ReducedFrom<Color>(new Node[] { color });
		}
	}
}
