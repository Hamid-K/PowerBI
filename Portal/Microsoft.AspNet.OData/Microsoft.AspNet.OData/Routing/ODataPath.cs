using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200007D RID: 125
	[ODataPathParameterBinding]
	public class ODataPath
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000F64A File Offset: 0x0000D84A
		public ODataPath(params ODataPathSegment[] segments)
			: this(segments)
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000F654 File Offset: 0x0000D854
		public ODataPath(IEnumerable<ODataPathSegment> segments)
		{
			if (segments == null)
			{
				throw Error.ArgumentNull("segments");
			}
			IList<ODataPathSegment> list = (segments as IList<ODataPathSegment>) ?? segments.ToList<ODataPathSegment>();
			this._edmType = (list.Any<ODataPathSegment>() ? list.Last<ODataPathSegment>().EdmType : null);
			this._segments = new ReadOnlyCollection<ODataPathSegment>(list);
			ODataPathSegmentHandler odataPathSegmentHandler = new ODataPathSegmentHandler();
			foreach (ODataPathSegment odataPathSegment in list)
			{
				UnresolvedPathSegment unresolvedPathSegment = odataPathSegment as UnresolvedPathSegment;
				if (unresolvedPathSegment != null)
				{
					odataPathSegmentHandler.Handle(unresolvedPathSegment);
				}
				else
				{
					odataPathSegment.HandleWith(odataPathSegmentHandler);
				}
			}
			this._navigationSource = odataPathSegmentHandler.NavigationSource;
			this._pathTemplate = odataPathSegmentHandler.PathTemplate;
			this._pathLiteral = odataPathSegmentHandler.PathLiteral;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000F72C File Offset: 0x0000D92C
		public IEdmType EdmType
		{
			get
			{
				return this._edmType;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000F734 File Offset: 0x0000D934
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this._navigationSource;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000F73C File Offset: 0x0000D93C
		public ReadOnlyCollection<ODataPathSegment> Segments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000F744 File Offset: 0x0000D944
		public virtual string PathTemplate
		{
			get
			{
				return this._pathTemplate;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000F74C File Offset: 0x0000D94C
		public override string ToString()
		{
			return this._pathLiteral;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000F754 File Offset: 0x0000D954
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000F75C File Offset: 0x0000D95C
		public ODataPath Path { get; internal set; }

		// Token: 0x040000F9 RID: 249
		private readonly ReadOnlyCollection<ODataPathSegment> _segments;

		// Token: 0x040000FA RID: 250
		private readonly IEdmType _edmType;

		// Token: 0x040000FB RID: 251
		private readonly IEdmNavigationSource _navigationSource;

		// Token: 0x040000FC RID: 252
		private readonly string _pathTemplate;

		// Token: 0x040000FD RID: 253
		private readonly string _pathLiteral;
	}
}
