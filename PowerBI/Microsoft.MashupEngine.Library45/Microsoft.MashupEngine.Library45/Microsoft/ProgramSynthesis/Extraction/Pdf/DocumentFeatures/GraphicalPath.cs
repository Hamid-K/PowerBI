using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CB8 RID: 3256
	[NullableContext(1)]
	[Nullable(0)]
	public class GraphicalPath : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x0010830D File Offset: 0x0010650D
		public static Color BackgroundColor { get; } = Color.White;

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x00108314 File Offset: 0x00106514
		public Vector<PixelUnit> StartPoint { get; }

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x0010831C File Offset: 0x0010651C
		public Color? FillColor { get; }

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x00108324 File Offset: 0x00106524
		public Color? StrokingColor { get; }

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x0010832C File Offset: 0x0010652C
		public bool IsClosed { get; }

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060053D6 RID: 21462 RVA: 0x00108334 File Offset: 0x00106534
		public bool IsStroked
		{
			get
			{
				return this.StrokingColor != null;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x00108350 File Offset: 0x00106550
		public bool IsFilled
		{
			get
			{
				return this.FillColor != null;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060053D8 RID: 21464 RVA: 0x0010836B File Offset: 0x0010656B
		public int LineWidth { get; }

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x00108373 File Offset: 0x00106573
		public int RenderingOrder { get; }

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x0010837B File Offset: 0x0010657B
		public IReadOnlyList<GraphicalPath.AbstractSegment> Segments { get; }

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x00108383 File Offset: 0x00106583
		public bool HasOnlyLineSegments
		{
			get
			{
				return this.Segments.All((GraphicalPath.AbstractSegment segment) => segment is GraphicalPath.LineSegment);
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x001083B0 File Offset: 0x001065B0
		public GraphicalPath([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pixelBounds, Vector<PixelUnit> startPoint, IReadOnlyList<GraphicalPath.AbstractSegment> segments, bool isClosed, int lineWidth, Color? fillColor, Color? strokingColor, int renderingOrder)
		{
			this.PixelBounds = pixelBounds;
			this.StartPoint = startPoint;
			this.Segments = segments;
			this.IsClosed = isClosed;
			this.LineWidth = lineWidth;
			this.FillColor = fillColor;
			this.StrokingColor = strokingColor;
			this.RenderingOrder = renderingOrder;
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060053DD RID: 21469 RVA: 0x00108400 File Offset: 0x00106600
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060053DE RID: 21470 RVA: 0x00108408 File Offset: 0x00106608
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00108410 File Offset: 0x00106610
		[return: Nullable(new byte[] { 0, 1, 0, 1, 1, 1, 1 })]
		public Optional<IEnumerable<Record<Vector<PixelUnit>, Vector<PixelUnit>>>> EnumerateLines()
		{
			return this.HasOnlyLineSegments.Then(from line in this.<EnumerateLines>g__PointsFromLines|36_0().Windowed<Vector<PixelUnit>>()
				where line.Item1 != line.Item2
				select line);
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0010844C File Offset: 0x0010664C
		[return: Nullable(new byte[] { 0, 1, 0, 1 })]
		public Optional<IEnumerable<AxisAlignedLine<PixelUnit>>> EnumerateAxisAlignedLines()
		{
			return from lines in this.EnumerateLines().SelectMany(delegate(IEnumerable<Record<Vector<PixelUnit>, Vector<PixelUnit>>> lines)
				{
					Func<Record<Vector<PixelUnit>, Vector<PixelUnit>>, Optional<AxisAlignedLine<PixelUnit>>> func;
					if ((func = GraphicalPath.<>O.<0>__MaybeAsAxisAlignedLine) == null)
					{
						func = (GraphicalPath.<>O.<0>__MaybeAsAxisAlignedLine = new Func<Record<Vector<PixelUnit>, Vector<PixelUnit>>, Optional<AxisAlignedLine<PixelUnit>>>(AxisAlignedLineUtilities.MaybeAsAxisAlignedLine<PixelUnit>));
					}
					return (from g in lines.Select(func).SplitRuns((Optional<AxisAlignedLine<PixelUnit>> lineOpt) => lineOpt.Select((AxisAlignedLine<PixelUnit> l) => l.Axis), null)
						select from _ in g.Key
							select new AxisAlignedLine<PixelUnit>(g.First<Optional<AxisAlignedLine<PixelUnit>>>().Value.Axis, g.First<Optional<AxisAlignedLine<PixelUnit>>>().Value.Range.Join(g.Last<Optional<AxisAlignedLine<PixelUnit>>>().Value.Range), g.First<Optional<AxisAlignedLine<PixelUnit>>>().Value.Position)).WholeSequenceOfValues<AxisAlignedLine<PixelUnit>>();
				})
				select from line in lines
					where !line.IsTrivial
					select line;
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x001084A7 File Offset: 0x001066A7
		[return: Nullable(new byte[] { 0, 0, 1 })]
		public Optional<Bounds<PixelUnit>> MaybeAsBox()
		{
			if (!this.HasOnlyLineSegments)
			{
				return Optional<Bounds<PixelUnit>>.Nothing;
			}
			return this.EnumerateAxisAlignedLines().OrElseDefault<IEnumerable<AxisAlignedLine<PixelUnit>>>().MaybeAsBox();
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x001084C8 File Offset: 0x001066C8
		public override string ToString()
		{
			return string.Format("Path({0}{1}Start={2}, {3} Segments={4})", new object[]
			{
				this.IsStroked ? string.Format("StrokingColor={0}, ", this.StrokingColor) : "",
				this.IsFilled ? string.Format("FillColor={0}, ", this.FillColor) : "",
				this.StartPoint,
				this.IsClosed ? "closed" : "open",
				string.Join<GraphicalPath.AbstractSegment>(", ", this.Segments)
			});
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x00108572 File Offset: 0x00106772
		[CompilerGenerated]
		private IEnumerable<Vector<PixelUnit>> <EnumerateLines>g__PointsFromLines|36_0()
		{
			yield return this.StartPoint;
			foreach (GraphicalPath.AbstractSegment abstractSegment in this.Segments)
			{
				yield return abstractSegment.EndPoint;
			}
			IEnumerator<GraphicalPath.AbstractSegment> enumerator = null;
			if (this.IsClosed)
			{
				yield return this.StartPoint;
			}
			yield break;
			yield break;
		}

		// Token: 0x02000CB9 RID: 3257
		[Nullable(0)]
		public abstract class AbstractSegment
		{
			// Token: 0x17000F1B RID: 3867
			// (get) Token: 0x060053E5 RID: 21477 RVA: 0x00108582 File Offset: 0x00106782
			public Vector<PixelUnit> EndPoint { get; }

			// Token: 0x060053E6 RID: 21478 RVA: 0x0010858A File Offset: 0x0010678A
			protected AbstractSegment(Vector<PixelUnit> endPoint)
			{
				this.EndPoint = endPoint;
			}
		}

		// Token: 0x02000CBA RID: 3258
		[Nullable(0)]
		public class LineSegment : GraphicalPath.AbstractSegment
		{
			// Token: 0x060053E7 RID: 21479 RVA: 0x00108599 File Offset: 0x00106799
			public LineSegment(Vector<PixelUnit> endPoint)
				: base(endPoint)
			{
			}

			// Token: 0x060053E8 RID: 21480 RVA: 0x001085A2 File Offset: 0x001067A2
			public override string ToString()
			{
				return string.Format("LineSegment to {0}", base.EndPoint);
			}
		}

		// Token: 0x02000CBB RID: 3259
		[Nullable(0)]
		public class BezierSegment : GraphicalPath.AbstractSegment
		{
			// Token: 0x17000F1C RID: 3868
			// (get) Token: 0x060053E9 RID: 21481 RVA: 0x001085B4 File Offset: 0x001067B4
			public Vector<PixelUnit> ControlPoint1 { get; }

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x060053EA RID: 21482 RVA: 0x001085BC File Offset: 0x001067BC
			public Vector<PixelUnit> ControlPoint2 { get; }

			// Token: 0x060053EB RID: 21483 RVA: 0x001085C4 File Offset: 0x001067C4
			public BezierSegment(Vector<PixelUnit> endPoint, Vector<PixelUnit> controlPoint1, Vector<PixelUnit> controlPoint2)
				: base(endPoint)
			{
				this.ControlPoint1 = controlPoint1;
				this.ControlPoint2 = controlPoint2;
			}

			// Token: 0x060053EC RID: 21484 RVA: 0x001085DB File Offset: 0x001067DB
			public override string ToString()
			{
				return string.Format("BezierSegment to {0} via {1} and {2}", base.EndPoint, this.ControlPoint1, this.ControlPoint2);
			}
		}

		// Token: 0x02000CBD RID: 3261
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040025D5 RID: 9685
			[Nullable(0)]
			public static Func<Record<Vector<PixelUnit>, Vector<PixelUnit>>, Optional<AxisAlignedLine<PixelUnit>>> <0>__MaybeAsAxisAlignedLine;
		}
	}
}
