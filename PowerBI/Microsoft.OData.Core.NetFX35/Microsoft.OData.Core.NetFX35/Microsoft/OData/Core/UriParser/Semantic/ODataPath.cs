using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200024B RID: 587
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataPathCollection just doesn't sound right")]
	public class ODataPath : ODataAnnotatable, IEnumerable<ODataPathSegment>, IEnumerable
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x0004A080 File Offset: 0x00048280
		public ODataPath(IEnumerable<ODataPathSegment> segments)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataPathSegment>>(segments, "segments");
			this.segments = Enumerable.ToList<ODataPathSegment>(segments);
			if (Enumerable.Any<ODataPathSegment>(this.segments, (ODataPathSegment s) => s == null))
			{
				throw Error.ArgumentNull("segments");
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004A0DF File Offset: 0x000482DF
		public ODataPath(params ODataPathSegment[] segments)
			: this((IEnumerable<ODataPathSegment>)segments)
		{
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0004A0ED File Offset: 0x000482ED
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

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004A10A File Offset: 0x0004830A
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

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0004A133 File Offset: 0x00048333
		public int Count
		{
			get
			{
				return this.segments.Count;
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0004A140 File Offset: 0x00048340
		public IEnumerator<ODataPathSegment> GetEnumerator()
		{
			return this.segments.GetEnumerator();
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0004A14D File Offset: 0x0004834D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0004A16C File Offset: 0x0004836C
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We would rather ship the PathSegmentTranslator so that its more extensible later")]
		public IEnumerable<T> WalkWith<T>(PathSegmentTranslator<T> translator)
		{
			return Enumerable.Select<ODataPathSegment, T>(this.segments, (ODataPathSegment segment) => segment.TranslateWith<T>(translator));
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0004A1A0 File Offset: 0x000483A0
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "We would rather ship the PathSegmentHandler so that its more extensible later")]
		public void WalkWith(PathSegmentHandler handler)
		{
			foreach (ODataPathSegment odataPathSegment in this.segments)
			{
				odataPathSegment.HandleWith(handler);
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004A214 File Offset: 0x00048414
		internal bool Equals(ODataPath other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(other, "other");
			return this.segments.Count == other.segments.Count && !Enumerable.Any<ODataPathSegment>(Enumerable.Where<ODataPathSegment>(this.segments, (ODataPathSegment t, int i) => !t.Equals(other.segments[i])));
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004A27C File Offset: 0x0004847C
		internal void Add(ODataPathSegment newSegment)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(newSegment, "newSegment");
			this.segments.Add(newSegment);
		}

		// Token: 0x040008BE RID: 2238
		private readonly IList<ODataPathSegment> segments;
	}
}
