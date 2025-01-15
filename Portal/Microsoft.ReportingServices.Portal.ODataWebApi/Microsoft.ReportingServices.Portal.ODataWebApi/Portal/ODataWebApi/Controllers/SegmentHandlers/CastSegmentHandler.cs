using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Microsoft.OData.UriParser;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers
{
	// Token: 0x0200003D RID: 61
	internal class CastSegmentHandler : ISegmentHandler
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000C772 File Offset: 0x0000A972
		public CastSegmentHandler()
			: this(typeof(CatalogItem).Assembly)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C789 File Offset: 0x0000A989
		internal CastSegmentHandler(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this._assembly = assembly;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public object Handle(object source, ODataPathSegment segment)
		{
			TypeSegment typeSegment = (TypeSegment)segment;
			Type type = this._assembly.GetType(typeSegment.Identifier);
			if (type == null)
			{
				return null;
			}
			if (source is IEnumerable)
			{
				return typeof(Enumerable).GetMethod("OfType").MakeGenericMethod(new Type[] { type }).Invoke(null, new object[] { source });
			}
			if (type.IsInstanceOfType(source))
			{
				return source;
			}
			return null;
		}

		// Token: 0x040000C3 RID: 195
		private readonly Assembly _assembly;
	}
}
