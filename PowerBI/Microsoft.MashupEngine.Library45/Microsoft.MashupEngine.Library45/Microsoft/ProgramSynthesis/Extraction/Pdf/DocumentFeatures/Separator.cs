using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D47 RID: 3399
	public class Separator : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x00112E2B File Offset: 0x0011102B
		[Nullable(new byte[] { 0, 1 })]
		public AxisAlignedLine<PixelUnit> Line
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060056E1 RID: 22241 RVA: 0x00112E33 File Offset: 0x00111033
		public Color? StrokingColor { get; }

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x00112E3B File Offset: 0x0011103B
		public Color? FillColor { get; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x00112E43 File Offset: 0x00111043
		public int LineWidth { get; }

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x060056E4 RID: 22244 RVA: 0x00112E4B File Offset: 0x0011104B
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x00112E54 File Offset: 0x00111054
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.Line.Bounds;
			}
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x00112E6F File Offset: 0x0011106F
		public Separator([Nullable(new byte[] { 0, 1 })] AxisAlignedLine<PixelUnit> line, Color? strokingColor, Color? fillColor, int lineWidth)
		{
			this.Line = line;
			this.StrokingColor = strokingColor;
			this.FillColor = fillColor;
			this.LineWidth = lineWidth;
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x00112E94 File Offset: 0x00111094
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("Separator({0}, StrokingColor={1}, FillColor={2}, LineWidth={3})", new object[] { this.Line, this.StrokingColor, this.FillColor, this.LineWidth });
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00112EEC File Offset: 0x001110EC
		[NullableContext(1)]
		internal bool IsVisible(SeparatorCollection separators, int tolerance)
		{
			QuadTree<ShadedBounds, PixelUnit> backgrounds = separators.Backgrounds;
			if (this.StrokingColor != null && this.FillColor == null)
			{
				ShadedBounds shadedBounds = backgrounds.ElementsThatContain(this.PixelBounds).ArgMax((ShadedBounds bg) => bg.RenderingOrders.Max);
				if (shadedBounds != null && shadedBounds.ShadingColor.ColorEquals(this.StrokingColor.Value))
				{
					return false;
				}
			}
			if (this.StrokingColor == null && this.FillColor != null)
			{
				Bounds<PixelUnit> bounds = this.PixelBounds.Extend(this.Line.Axis.Perpendicular(), tolerance);
				if (backgrounds.ElementsThatContain(bounds).Any((ShadedBounds bg) => bg.ShadingColor.ColorEquals(this.FillColor.Value)))
				{
					return false;
				}
				List<ShadedBounds> list = (from bg in backgrounds.OverlappingElements(bounds)
					where bg.ShadingColor.ColorEquals(this.FillColor.Value)
					select bg).ToList<ShadedBounds>();
				if (list.Count > 1)
				{
					Range<PixelUnit> separatorPos = Range<PixelUnit>.CreateAround(this.Line.Position, tolerance);
					Func<ShadedBounds, Range<PixelUnit>> <>9__6;
					Dictionary<int, Ranges<PixelUnit>> dictionary = list.GroupBy(delegate(ShadedBounds b)
					{
						Range<PixelUnit> range = b.PixelBounds[this.Line.Axis.Perpendicular()];
						bool flag = range.Min < separatorPos.Max;
						bool flag2 = range.Max > separatorPos.Min;
						if (flag && flag2)
						{
							return 0;
						}
						if (flag)
						{
							return -1;
						}
						if (flag2)
						{
							return 1;
						}
						return 0;
					}).ToDictionary((IGrouping<int, ShadedBounds> g) => g.Key, delegate(IGrouping<int, ShadedBounds> g)
					{
						Func<ShadedBounds, Range<PixelUnit>> func;
						if ((func = <>9__6) == null)
						{
							func = (<>9__6 = (ShadedBounds b) => b.PixelBounds[this.Line.Axis].Expand(tolerance));
						}
						return new Ranges<PixelUnit>(g.Select(func));
					});
					foreach (int num in new int[] { -1, 0, 1 })
					{
						if (!dictionary.ContainsKey(num))
						{
							dictionary[num] = Ranges<PixelUnit>.Empty;
						}
					}
					if (dictionary[-1].Intersect(dictionary[1]).Join(dictionary[0]).Contains(this.Line.Range))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
