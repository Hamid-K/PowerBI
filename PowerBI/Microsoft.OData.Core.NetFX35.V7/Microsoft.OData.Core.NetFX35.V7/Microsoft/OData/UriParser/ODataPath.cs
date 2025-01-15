using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000144 RID: 324
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataPathCollection just doesn't sound right")]
	public class ODataPath : IEnumerable<ODataPathSegment>, IEnumerable
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x00029B34 File Offset: 0x00027D34
		public ODataPath(IEnumerable<ODataPathSegment> segments)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataPathSegment>>(segments, "segments");
			this.segments = Enumerable.ToList<ODataPathSegment>(segments);
			if (Enumerable.Any<ODataPathSegment>(this.segments, (ODataPathSegment s) => s == null))
			{
				throw Error.ArgumentNull("segments");
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00029B96 File Offset: 0x00027D96
		public ODataPath(params ODataPathSegment[] segments)
			: this(segments)
		{
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00029B9F File Offset: 0x00027D9F
		public ODataPathSegment FirstSegment
		{
			get
			{
				if (this.segments.Count != 0)
				{
					return this.segments[0];
				}
				return null;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00029BBC File Offset: 0x00027DBC
		public ODataPathSegment LastSegment
		{
			get
			{
				if (this.segments.Count != 0)
				{
					return this.segments[this.segments.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00029BE5 File Offset: 0x00027DE5
		public int Count
		{
			get
			{
				return this.segments.Count;
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00029BF2 File Offset: 0x00027DF2
		public IEnumerator<ODataPathSegment> GetEnumerator()
		{
			return this.segments.GetEnumerator();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00029BFF File Offset: 0x00027DFF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00029C08 File Offset: 0x00027E08
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We would rather ship the PathSegmentTranslator so that its more extensible later")]
		public IEnumerable<T> WalkWith<T>(PathSegmentTranslator<T> translator)
		{
			return Enumerable.Select<ODataPathSegment, T>(this.segments, (ODataPathSegment segment) => segment.TranslateWith<T>(translator));
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00029C3C File Offset: 0x00027E3C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We would rather ship the PathSegmentHandler so that its more extensible later")]
		public void WalkWith(PathSegmentHandler handler)
		{
			foreach (ODataPathSegment odataPathSegment in this.segments)
			{
				odataPathSegment.HandleWith(handler);
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00029C8C File Offset: 0x00027E8C
		internal bool Equals(ODataPath other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(other, "other");
			return this.segments.Count == other.segments.Count && !Enumerable.Any<ODataPathSegment>(Enumerable.Where<ODataPathSegment>(this.segments, (ODataPathSegment t, int i) => !t.Equals(other.segments[i])));
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00029CF5 File Offset: 0x00027EF5
		internal void Add(ODataPathSegment newSegment)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(newSegment, "newSegment");
			this.segments.Add(newSegment);
		}

		// Token: 0x04000771 RID: 1905
		private readonly IList<ODataPathSegment> segments;
	}
}
