using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Specifications.Serialization;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Learning
{
	// Token: 0x02000C18 RID: 3096
	[NullableContext(1)]
	[Nullable(0)]
	internal class DisjunctiveBoundsExampleSpec : Spec
	{
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x000FBADB File Offset: 0x000F9CDB
		public IReadOnlyDictionary<State, DisjunctiveBoundsExampleSpec.PossibleBounds> DisjunctiveExamples { get; }

		// Token: 0x06004FF2 RID: 20466 RVA: 0x000FBAE3 File Offset: 0x000F9CE3
		public DisjunctiveBoundsExampleSpec(IReadOnlyDictionary<State, DisjunctiveBoundsExampleSpec.PossibleBounds> disjunctiveExamples)
			: base(disjunctiveExamples.Keys, true)
		{
			this.DisjunctiveExamples = disjunctiveExamples;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x000FBAFC File Offset: 0x000F9CFC
		protected override bool CorrectOnProvided(State state, object output)
		{
			BoundsOnPdfPage boundsOnPdfPage = output as BoundsOnPdfPage;
			DisjunctiveBoundsExampleSpec.PossibleBounds possibleBounds;
			return boundsOnPdfPage != null && this.DisjunctiveExamples.TryGetValue(state, out possibleBounds) && possibleBounds.Contains(boundsOnPdfPage);
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x000FBB2C File Offset: 0x000F9D2C
		protected override bool EqualsOnInput(State state, Spec otherSpec)
		{
			DisjunctiveBoundsExampleSpec disjunctiveBoundsExampleSpec = otherSpec as DisjunctiveBoundsExampleSpec;
			DisjunctiveBoundsExampleSpec.PossibleBounds possibleBounds;
			DisjunctiveBoundsExampleSpec.PossibleBounds possibleBounds2;
			return disjunctiveBoundsExampleSpec != null && disjunctiveBoundsExampleSpec.DisjunctiveExamples.TryGetValue(state, out possibleBounds) && this.DisjunctiveExamples.TryGetValue(state, out possibleBounds2) && possibleBounds2.Equals(possibleBounds);
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x000FBB6C File Offset: 0x000F9D6C
		protected override int GetHashCodeOnInput(State state)
		{
			return this.DisjunctiveExamples[state].GetHashCode();
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override XElement SerializeImpl(Dictionary<object, int> identityCache, SpecSerializationContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override XElement InputToXML(State input, Dictionary<object, int> identityCache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x000FBB80 File Offset: 0x000F9D80
		protected internal override Spec TransformInputs(Func<State, State> transformer)
		{
			return new DisjunctiveBoundsExampleSpec(this.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => transformer(kv.Key), (KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Value));
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x000FBBD8 File Offset: 0x000F9DD8
		public DisjunctiveBoundsExampleSpec Select(Func<DisjunctiveBoundsExampleSpec.PossibleBounds, DisjunctiveBoundsExampleSpec.PossibleBounds> mutateOutput)
		{
			return new DisjunctiveBoundsExampleSpec(this.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, (KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => mutateOutput(kv.Value)));
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x000FBC2D File Offset: 0x000F9E2D
		public DisjunctiveBoundsExampleSpec Select([Nullable(new byte[] { 1, 0, 1, 1, 1 })] Func<KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds>, DisjunctiveBoundsExampleSpec.PossibleBounds> mutateKv)
		{
			return new DisjunctiveBoundsExampleSpec(this.DisjunctiveExamples.ToDictionary((KeyValuePair<State, DisjunctiveBoundsExampleSpec.PossibleBounds> kv) => kv.Key, mutateKv));
		}

		// Token: 0x02000C19 RID: 3097
		[Nullable(0)]
		internal abstract class PossibleBounds : IEquatable<DisjunctiveBoundsExampleSpec.PossibleBounds>
		{
			// Token: 0x06004FFB RID: 20475
			public abstract bool Equals(DisjunctiveBoundsExampleSpec.PossibleBounds other);

			// Token: 0x17000E4D RID: 3661
			// (get) Token: 0x06004FFC RID: 20476
			[Nullable(new byte[] { 0, 1 })]
			internal abstract Bounds<PixelUnit> CanonicalBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x06004FFD RID: 20477 RVA: 0x000FBC5F File Offset: 0x000F9E5F
			public bool Contains(BoundsOnPdfPage boundsOnPdfPage)
			{
				return this.OnSamePageAs(boundsOnPdfPage) && this.Contains(boundsOnPdfPage.Bounds);
			}

			// Token: 0x06004FFE RID: 20478
			public abstract bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds);

			// Token: 0x06004FFF RID: 20479
			public abstract bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis axis);

			// Token: 0x06005000 RID: 20480
			internal abstract bool OnSamePageAs(SinglePagePdfRegion page);

			// Token: 0x06005001 RID: 20481
			public abstract override int GetHashCode();
		}

		// Token: 0x02000C1A RID: 3098
		[NullableContext(0)]
		internal abstract class ConcretePossibleBounds : DisjunctiveBoundsExampleSpec.PossibleBounds
		{
			// Token: 0x06005003 RID: 20483
			internal abstract bool ContainsEdge([Nullable(new byte[] { 0, 1 })] AxisAlignedLine<PixelUnit> edge, Direction dir, Axis? checkedAxis = null);
		}

		// Token: 0x02000C1B RID: 3099
		[Nullable(0)]
		internal class SnapToGlyphsBounds : DisjunctiveBoundsExampleSpec.ConcretePossibleBounds
		{
			// Token: 0x17000E4E RID: 3662
			// (get) Token: 0x06005005 RID: 20485 RVA: 0x000FBC80 File Offset: 0x000F9E80
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000E4F RID: 3663
			// (get) Token: 0x06005006 RID: 20486 RVA: 0x000FBC88 File Offset: 0x000F9E88
			public HashSet<Glyph> CoveredGlyphs { get; }

			// Token: 0x06005007 RID: 20487 RVA: 0x000FBC90 File Offset: 0x000F9E90
			internal SnapToGlyphsBounds(BoundsOnPdfPage boundsOnPdfPage)
			{
				this._boundsOnPdfPage = boundsOnPdfPage;
				this.Bounds = boundsOnPdfPage.Bounds;
				this.CoveredGlyphs = (from g in boundsOnPdfPage.PageData.GetGlyphs().SelectMany((IReadOnlyList<Glyph> g) => g)
					where !string.IsNullOrWhiteSpace(g.Text) && this.Bounds.Contains(g.ApparentPixelBounds)
					select g).ConvertToHashSet<Glyph>();
				this._hashCode = this.CoveredGlyphs.OrderIndependentHashCode((Glyph g) => g.GetHashCode());
			}

			// Token: 0x06005008 RID: 20488 RVA: 0x000FBD34 File Offset: 0x000F9F34
			public override bool Equals(DisjunctiveBoundsExampleSpec.PossibleBounds other)
			{
				DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds snapToGlyphsBounds = other as DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds;
				return snapToGlyphsBounds != null && this.CoveredGlyphs.SetEquals(snapToGlyphsBounds.CoveredGlyphs);
			}

			// Token: 0x17000E50 RID: 3664
			// (get) Token: 0x06005009 RID: 20489 RVA: 0x000FBD5E File Offset: 0x000F9F5E
			[Nullable(new byte[] { 0, 1 })]
			internal override Bounds<PixelUnit> CanonicalBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.Bounds;
				}
			}

			// Token: 0x0600500A RID: 20490 RVA: 0x000FBD66 File Offset: 0x000F9F66
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
			{
				return bounds.Contains(this.Bounds) && new DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds(this._boundsOnPdfPage.GetBoundsOnSamePage(bounds)).Equals(this);
			}

			// Token: 0x0600500B RID: 20491 RVA: 0x000FBD90 File Offset: 0x000F9F90
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis axis)
			{
				return bounds.Contains(this.Bounds, axis) && new DisjunctiveBoundsExampleSpec.SnapToGlyphsBounds(this._boundsOnPdfPage.GetBoundsOnSamePage(bounds.With(axis.Perpendicular(), this.Bounds[axis]))).Equals(this);
			}

			// Token: 0x0600500C RID: 20492 RVA: 0x000FBDE1 File Offset: 0x000F9FE1
			internal override bool OnSamePageAs(SinglePagePdfRegion page)
			{
				return page.SamePageAs(this._boundsOnPdfPage);
			}

			// Token: 0x0600500D RID: 20493 RVA: 0x000FBDF0 File Offset: 0x000F9FF0
			internal override bool ContainsEdge([Nullable(new byte[] { 0, 1 })] AxisAlignedLine<PixelUnit> edge, Direction dir, Axis? checkedAxis = null)
			{
				bool flag = checkedAxis == null || checkedAxis.Value == edge.Axis;
				if ((checkedAxis == null || checkedAxis.Value != edge.Axis) && Math.Sign(edge.Position.CompareTo(this.Bounds[dir])) != dir.Derivative().Value())
				{
					return false;
				}
				if (flag && !edge.Range.Contains(this.Bounds[dir.AlignedAxis().Perpendicular()]))
				{
					return false;
				}
				if (checkedAxis != null)
				{
					return true;
				}
				Bounds<PixelUnit> between = new Bounds<PixelUnit>(delegate(Axis axis)
				{
					if (axis != dir.AlignedAxis())
					{
						return edge.Range;
					}
					return Range<PixelUnit>.CreateUnordered(this.Bounds[dir.Opposite()], edge.Position);
				});
				return !(from g in this._boundsOnPdfPage.PageData.GetGlyphs().SelectMany((IReadOnlyList<Glyph> g) => g)
					where between.Contains(g.ApparentPixelBounds) && !this.CoveredGlyphs.Contains(g) && !string.IsNullOrWhiteSpace(g.Text)
					select g).Any<Glyph>();
			}

			// Token: 0x0600500E RID: 20494 RVA: 0x000FBF46 File Offset: 0x000FA146
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x0600500F RID: 20495 RVA: 0x000FBF4E File Offset: 0x000FA14E
			public override string ToString()
			{
				return string.Format("SnapToGlyphBounds({0})", this.Bounds);
			}

			// Token: 0x04002348 RID: 9032
			private readonly BoundsOnPdfPage _boundsOnPdfPage;

			// Token: 0x04002349 RID: 9033
			private readonly int _hashCode;
		}

		// Token: 0x02000C1E RID: 3102
		[Nullable(0)]
		internal class EdgeOfPossibleBounds : DisjunctiveBoundsExampleSpec.PossibleBounds
		{
			// Token: 0x17000E51 RID: 3665
			// (get) Token: 0x06005019 RID: 20505 RVA: 0x000FC031 File Offset: 0x000FA231
			public DisjunctiveBoundsExampleSpec.ConcretePossibleBounds BaseBounds { get; }

			// Token: 0x17000E52 RID: 3666
			// (get) Token: 0x0600501A RID: 20506 RVA: 0x000FC039 File Offset: 0x000FA239
			public Direction Edge { get; }

			// Token: 0x0600501B RID: 20507 RVA: 0x000FC041 File Offset: 0x000FA241
			internal EdgeOfPossibleBounds(DisjunctiveBoundsExampleSpec.ConcretePossibleBounds baseBounds, Direction edge)
			{
				this.BaseBounds = baseBounds;
				this.Edge = edge;
			}

			// Token: 0x17000E53 RID: 3667
			// (get) Token: 0x0600501C RID: 20508 RVA: 0x000FC058 File Offset: 0x000FA258
			[Nullable(new byte[] { 0, 1 })]
			internal override Bounds<PixelUnit> CanonicalBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.BaseBounds.CanonicalBounds.Edges[this.Edge].Bounds;
				}
			}

			// Token: 0x0600501D RID: 20509 RVA: 0x000FC08C File Offset: 0x000FA28C
			public override bool Equals(DisjunctiveBoundsExampleSpec.PossibleBounds other)
			{
				DisjunctiveBoundsExampleSpec.EdgeOfPossibleBounds edgeOfPossibleBounds = other as DisjunctiveBoundsExampleSpec.EdgeOfPossibleBounds;
				return edgeOfPossibleBounds != null && edgeOfPossibleBounds.Edge == this.Edge && edgeOfPossibleBounds.BaseBounds.Equals(this.BaseBounds);
			}

			// Token: 0x0600501E RID: 20510 RVA: 0x000FC0C4 File Offset: 0x000FA2C4
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
			{
				return this.BaseBounds.ContainsEdge(bounds.Edges[this.Edge.Opposite()], this.Edge, null);
			}

			// Token: 0x0600501F RID: 20511 RVA: 0x000FC102 File Offset: 0x000FA302
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis axis)
			{
				return this.BaseBounds.ContainsEdge(bounds.Edges[this.Edge.Opposite()], this.Edge, new Axis?(axis));
			}

			// Token: 0x06005020 RID: 20512 RVA: 0x000FC132 File Offset: 0x000FA332
			internal override bool OnSamePageAs(SinglePagePdfRegion page)
			{
				return this.BaseBounds.OnSamePageAs(page);
			}

			// Token: 0x06005021 RID: 20513 RVA: 0x000FC140 File Offset: 0x000FA340
			public override int GetHashCode()
			{
				return (39 * this.BaseBounds.GetHashCode()) ^ this.Edge.GetHashCode();
			}

			// Token: 0x06005022 RID: 20514 RVA: 0x000FC170 File Offset: 0x000FA370
			public override string ToString()
			{
				return string.Format("EdgeOfPossibleBounds({0} of {1})", this.Edge, this.BaseBounds);
			}
		}

		// Token: 0x02000C1F RID: 3103
		[Nullable(0)]
		internal class DirectionFromPossibleBounds : DisjunctiveBoundsExampleSpec.PossibleBounds
		{
			// Token: 0x17000E54 RID: 3668
			// (get) Token: 0x06005023 RID: 20515 RVA: 0x000FC18D File Offset: 0x000FA38D
			public DisjunctiveBoundsExampleSpec.PossibleBounds BaseBounds { get; }

			// Token: 0x17000E55 RID: 3669
			// (get) Token: 0x06005024 RID: 20516 RVA: 0x000FC195 File Offset: 0x000FA395
			public Direction Direction { get; }

			// Token: 0x06005025 RID: 20517 RVA: 0x000FC1A0 File Offset: 0x000FA3A0
			internal DirectionFromPossibleBounds(DisjunctiveBoundsExampleSpec.PossibleBounds baseBounds, Direction direction)
			{
				this.BaseBounds = baseBounds;
				this.Direction = direction;
				for (;;)
				{
					DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds directionFromPossibleBounds = this.BaseBounds as DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds;
					if (directionFromPossibleBounds == null || directionFromPossibleBounds.Direction != direction)
					{
						break;
					}
					this.BaseBounds = directionFromPossibleBounds.BaseBounds;
				}
				if (this.BaseBounds is DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds)
				{
					throw new NotImplementedException("DirectionFrom of DirectionFrom is unsupported.");
				}
			}

			// Token: 0x17000E56 RID: 3670
			// (get) Token: 0x06005026 RID: 20518 RVA: 0x000FC200 File Offset: 0x000FA400
			[Nullable(new byte[] { 0, 1 })]
			internal override Bounds<PixelUnit> CanonicalBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					AxisAlignedLine<PixelUnit> axisAlignedLine = this.BaseBounds.CanonicalBounds.Edges[this.Direction];
					AxisAlignedLine<PixelUnit> axisAlignedLine2 = new AxisAlignedLine<PixelUnit>(axisAlignedLine.Axis, axisAlignedLine.Range, axisAlignedLine.Position + this.Direction.Derivative().Value());
					return axisAlignedLine2.Bounds;
				}
			}

			// Token: 0x06005027 RID: 20519 RVA: 0x000FC260 File Offset: 0x000FA460
			public override bool Equals(DisjunctiveBoundsExampleSpec.PossibleBounds other)
			{
				DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds directionFromPossibleBounds = other as DisjunctiveBoundsExampleSpec.DirectionFromPossibleBounds;
				return directionFromPossibleBounds != null && directionFromPossibleBounds.Direction == this.Direction && directionFromPossibleBounds.BaseBounds.Equals(this.BaseBounds);
			}

			// Token: 0x06005028 RID: 20520 RVA: 0x000FC298 File Offset: 0x000FA498
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
			{
				return !this.BaseBounds.CanonicalBounds.Contains(bounds) && this.BaseBounds.CanonicalBounds.Overlaps(bounds, this.Direction.AlignedAxis().Perpendicular()) && this.BaseBounds.CanonicalBounds.IsAfter(bounds, this.Direction.AlignedAxis(), false) == (this.Direction.Derivative() == Derivative.Decreasing);
			}

			// Token: 0x06005029 RID: 20521 RVA: 0x000FC314 File Offset: 0x000FA514
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis axis)
			{
				if (axis == this.Direction.AlignedAxis())
				{
					if (this.BaseBounds.CanonicalBounds.Contains(bounds, axis))
					{
						return false;
					}
					if (this.BaseBounds.CanonicalBounds.IsAfter(bounds, this.Direction.AlignedAxis(), false) != (this.Direction.Derivative() == Derivative.Decreasing))
					{
						return false;
					}
				}
				else if (!this.BaseBounds.CanonicalBounds.Overlaps(bounds, this.Direction.AlignedAxis().Perpendicular()))
				{
					return false;
				}
				return true;
			}

			// Token: 0x0600502A RID: 20522 RVA: 0x000FC3A3 File Offset: 0x000FA5A3
			internal override bool OnSamePageAs(SinglePagePdfRegion page)
			{
				return this.BaseBounds.OnSamePageAs(page);
			}

			// Token: 0x0600502B RID: 20523 RVA: 0x000FC3B4 File Offset: 0x000FA5B4
			public override int GetHashCode()
			{
				return (37 * this.BaseBounds.GetHashCode()) ^ this.Direction.GetHashCode();
			}

			// Token: 0x0600502C RID: 20524 RVA: 0x000FC3E4 File Offset: 0x000FA5E4
			public override string ToString()
			{
				return string.Format("DirectionFromPossibleBounds({0} of {1})", this.Direction, this.BaseBounds);
			}
		}

		// Token: 0x02000C20 RID: 3104
		[Nullable(0)]
		internal class ReplaceAxisPossibleBounds : DisjunctiveBoundsExampleSpec.PossibleBounds
		{
			// Token: 0x17000E57 RID: 3671
			// (get) Token: 0x0600502D RID: 20525 RVA: 0x000FC401 File Offset: 0x000FA601
			public DisjunctiveBoundsExampleSpec.PossibleBounds BaseBounds { get; }

			// Token: 0x17000E58 RID: 3672
			// (get) Token: 0x0600502E RID: 20526 RVA: 0x000FC409 File Offset: 0x000FA609
			public Axis Axis { get; }

			// Token: 0x17000E59 RID: 3673
			// (get) Token: 0x0600502F RID: 20527 RVA: 0x000FC411 File Offset: 0x000FA611
			[Nullable(new byte[] { 0, 1 })]
			public Range<PixelUnit> Range
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x06005030 RID: 20528 RVA: 0x000FC419 File Offset: 0x000FA619
			public ReplaceAxisPossibleBounds(DisjunctiveBoundsExampleSpec.PossibleBounds baseBounds, Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> range)
			{
				this.BaseBounds = baseBounds;
				this.Axis = axis;
				this.Range = range;
			}

			// Token: 0x06005031 RID: 20529 RVA: 0x000FC438 File Offset: 0x000FA638
			public override bool Equals(DisjunctiveBoundsExampleSpec.PossibleBounds other)
			{
				DisjunctiveBoundsExampleSpec.ReplaceAxisPossibleBounds replaceAxisPossibleBounds = other as DisjunctiveBoundsExampleSpec.ReplaceAxisPossibleBounds;
				return replaceAxisPossibleBounds != null && replaceAxisPossibleBounds.Axis == this.Axis && replaceAxisPossibleBounds.Range.Equals(this.Range) && replaceAxisPossibleBounds.BaseBounds.Equals(this.BaseBounds);
			}

			// Token: 0x17000E5A RID: 3674
			// (get) Token: 0x06005032 RID: 20530 RVA: 0x000FC488 File Offset: 0x000FA688
			[Nullable(new byte[] { 0, 1 })]
			internal override Bounds<PixelUnit> CanonicalBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.BaseBounds.CanonicalBounds.With(this.Axis, this.Range);
				}
			}

			// Token: 0x06005033 RID: 20531 RVA: 0x000FC4B4 File Offset: 0x000FA6B4
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
			{
				return this.BaseBounds.Contains(bounds.With(this.Axis, this.Range));
			}

			// Token: 0x06005034 RID: 20532 RVA: 0x000FC4D4 File Offset: 0x000FA6D4
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis axis)
			{
				return this.BaseBounds.Contains(bounds.With(this.Axis, this.Range), axis);
			}

			// Token: 0x06005035 RID: 20533 RVA: 0x000FC4F5 File Offset: 0x000FA6F5
			internal override bool OnSamePageAs(SinglePagePdfRegion page)
			{
				return this.BaseBounds.OnSamePageAs(page);
			}

			// Token: 0x06005036 RID: 20534 RVA: 0x000FC504 File Offset: 0x000FA704
			public override int GetHashCode()
			{
				return (11 * this.BaseBounds.GetHashCode()) ^ (19 * this.Axis.GetHashCode()) ^ this.Range.GetHashCode();
			}

			// Token: 0x06005037 RID: 20535 RVA: 0x000FC54C File Offset: 0x000FA74C
			public override string ToString()
			{
				return string.Format("ReplaceAxisPossibleBounds(set {0} to {1} in {2})", this.Axis, this.Range, this.BaseBounds);
			}
		}

		// Token: 0x02000C21 RID: 3105
		[Nullable(0)]
		internal class IgnoreAxisPossibleBounds : DisjunctiveBoundsExampleSpec.PossibleBounds
		{
			// Token: 0x17000E5B RID: 3675
			// (get) Token: 0x06005038 RID: 20536 RVA: 0x000FC574 File Offset: 0x000FA774
			public DisjunctiveBoundsExampleSpec.PossibleBounds BaseBounds { get; }

			// Token: 0x17000E5C RID: 3676
			// (get) Token: 0x06005039 RID: 20537 RVA: 0x000FC57C File Offset: 0x000FA77C
			public Axis Axis { get; }

			// Token: 0x0600503A RID: 20538 RVA: 0x000FC584 File Offset: 0x000FA784
			public IgnoreAxisPossibleBounds(DisjunctiveBoundsExampleSpec.PossibleBounds baseBounds, Axis axis)
			{
				this.BaseBounds = baseBounds;
				this.Axis = axis;
			}

			// Token: 0x0600503B RID: 20539 RVA: 0x000FC59C File Offset: 0x000FA79C
			public override bool Equals(DisjunctiveBoundsExampleSpec.PossibleBounds other)
			{
				DisjunctiveBoundsExampleSpec.IgnoreAxisPossibleBounds ignoreAxisPossibleBounds = other as DisjunctiveBoundsExampleSpec.IgnoreAxisPossibleBounds;
				return ignoreAxisPossibleBounds != null && ignoreAxisPossibleBounds.Axis == this.Axis && ignoreAxisPossibleBounds.BaseBounds.Equals(this.BaseBounds);
			}

			// Token: 0x17000E5D RID: 3677
			// (get) Token: 0x0600503C RID: 20540 RVA: 0x000FC5D4 File Offset: 0x000FA7D4
			[Nullable(new byte[] { 0, 1 })]
			internal override Bounds<PixelUnit> CanonicalBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.BaseBounds.CanonicalBounds;
				}
			}

			// Token: 0x0600503D RID: 20541 RVA: 0x000FC5E1 File Offset: 0x000FA7E1
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
			{
				return this.BaseBounds.Contains(bounds, this.Axis.Perpendicular());
			}

			// Token: 0x0600503E RID: 20542 RVA: 0x000FC5FA File Offset: 0x000FA7FA
			public override bool Contains([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, Axis axis)
			{
				return axis != this.Axis || this.Contains(bounds);
			}

			// Token: 0x0600503F RID: 20543 RVA: 0x000FC60E File Offset: 0x000FA80E
			internal override bool OnSamePageAs(SinglePagePdfRegion page)
			{
				return this.BaseBounds.OnSamePageAs(page);
			}

			// Token: 0x06005040 RID: 20544 RVA: 0x000FC61C File Offset: 0x000FA81C
			public override int GetHashCode()
			{
				return (11 * this.BaseBounds.GetHashCode()) ^ this.Axis.GetHashCode();
			}

			// Token: 0x06005041 RID: 20545 RVA: 0x000FC64C File Offset: 0x000FA84C
			public override string ToString()
			{
				return string.Format("IgnoreAxisPossibleBounds(ignore {0} in {1})", this.Axis, this.BaseBounds);
			}
		}
	}
}
