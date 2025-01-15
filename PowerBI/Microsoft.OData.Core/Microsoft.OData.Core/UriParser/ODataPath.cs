using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000190 RID: 400
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataPathCollection just doesn't sound right")]
	public class ODataPath : IEnumerable<ODataPathSegment>, IEnumerable
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x000398D8 File Offset: 0x00037AD8
		public ODataPath(IEnumerable<ODataPathSegment> segments)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataPathSegment>>(segments, "segments");
			this.segments = segments.ToList<ODataPathSegment>();
			if (this.segments.Any((ODataPathSegment s) => s == null))
			{
				throw Error.ArgumentNull("segments");
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0003993A File Offset: 0x00037B3A
		public ODataPath(params ODataPathSegment[] segments)
			: this(segments)
		{
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00039943 File Offset: 0x00037B43
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00039960 File Offset: 0x00037B60
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00039989 File Offset: 0x00037B89
		public int Count
		{
			get
			{
				return this.segments.Count;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00039996 File Offset: 0x00037B96
		public IEnumerator<ODataPathSegment> GetEnumerator()
		{
			return this.segments.GetEnumerator();
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000399A3 File Offset: 0x00037BA3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000399AC File Offset: 0x00037BAC
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We would rather ship the PathSegmentTranslator so that its more extensible later")]
		public IEnumerable<T> WalkWith<T>(PathSegmentTranslator<T> translator)
		{
			return this.segments.Select((ODataPathSegment segment) => segment.TranslateWith<T>(translator));
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000399E0 File Offset: 0x00037BE0
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We would rather ship the PathSegmentHandler so that its more extensible later")]
		public void WalkWith(PathSegmentHandler handler)
		{
			foreach (ODataPathSegment odataPathSegment in this.segments)
			{
				odataPathSegment.HandleWith(handler);
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00039A30 File Offset: 0x00037C30
		internal bool Equals(ODataPath other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(other, "other");
			return this.segments.Count == other.segments.Count && !this.segments.Where((ODataPathSegment t, int i) => !t.Equals(other.segments[i])).Any<ODataPathSegment>();
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00039A99 File Offset: 0x00037C99
		internal void Add(ODataPathSegment newSegment)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(newSegment, "newSegment");
			this.segments.Add(newSegment);
		}

		// Token: 0x040008AC RID: 2220
		private readonly IList<ODataPathSegment> segments;
	}
}
