using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x0200008E RID: 142
	public class ODataPathTemplate
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public ODataPathTemplate(params ODataPathSegmentTemplate[] segments)
			: this(segments)
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000102AD File Offset: 0x0000E4AD
		public ODataPathTemplate(IEnumerable<ODataPathSegmentTemplate> segments)
			: this(segments.AsList<ODataPathSegmentTemplate>())
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000102BB File Offset: 0x0000E4BB
		public ODataPathTemplate(IList<ODataPathSegmentTemplate> segments)
		{
			if (segments == null)
			{
				throw Error.ArgumentNull("segments");
			}
			this._segments = new ReadOnlyCollection<ODataPathSegmentTemplate>(segments);
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000102DD File Offset: 0x0000E4DD
		public ReadOnlyCollection<ODataPathSegmentTemplate> Segments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000102E8 File Offset: 0x0000E4E8
		public bool TryMatch(ODataPath path, IDictionary<string, object> values)
		{
			if (path.Segments.Count != this.Segments.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Segments.Count; i++)
			{
				if (!this.Segments[i].TryMatch(path.Segments[i], values))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000126 RID: 294
		private ReadOnlyCollection<ODataPathSegmentTemplate> _segments;
	}
}
