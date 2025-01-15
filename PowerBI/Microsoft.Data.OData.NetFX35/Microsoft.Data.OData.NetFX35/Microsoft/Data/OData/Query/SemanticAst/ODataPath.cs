using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200004A RID: 74
	public class ODataPath : ODataAnnotatable, IEnumerable<ODataPathSegment>, IEnumerable
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000079FC File Offset: 0x00005BFC
		public ODataPath(IEnumerable<ODataPathSegment> segments)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<ODataPathSegment>>(segments, "segments");
			this.segments = Enumerable.ToList<ODataPathSegment>(segments);
			if (Enumerable.Any<ODataPathSegment>(this.segments, (ODataPathSegment s) => s == null))
			{
				throw Error.ArgumentNull("segments");
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007A5B File Offset: 0x00005C5B
		public ODataPath(params ODataPathSegment[] segments)
			: this((IEnumerable<ODataPathSegment>)segments)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007A69 File Offset: 0x00005C69
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00007A86 File Offset: 0x00005C86
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007AAF File Offset: 0x00005CAF
		public int Count
		{
			get
			{
				return this.segments.Count;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007ABC File Offset: 0x00005CBC
		public IEnumerator<ODataPathSegment> GetEnumerator()
		{
			return this.segments.GetEnumerator();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007AC9 File Offset: 0x00005CC9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public IEnumerable<T> WalkWith<T>(PathSegmentTranslator<T> translator)
		{
			return Enumerable.Select<ODataPathSegment, T>(this.segments, (ODataPathSegment segment) => segment.Translate<T>(translator));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007B1C File Offset: 0x00005D1C
		public void WalkWith(PathSegmentHandler handler)
		{
			foreach (ODataPathSegment odataPathSegment in this.segments)
			{
				odataPathSegment.Handle(handler);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007B90 File Offset: 0x00005D90
		internal bool Equals(ODataPath other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPath>(other, "other");
			return this.segments.Count == other.segments.Count && !Enumerable.Any<ODataPathSegment>(Enumerable.Where<ODataPathSegment>(this.segments, (ODataPathSegment t, int i) => !t.Equals(other.segments[i])));
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007BF8 File Offset: 0x00005DF8
		internal void Add(ODataPathSegment newSegment)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(newSegment, "newSegment");
			this.segments.Add(newSegment);
		}

		// Token: 0x04000081 RID: 129
		private readonly IList<ODataPathSegment> segments;
	}
}
