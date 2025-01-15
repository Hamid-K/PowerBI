using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200007F RID: 127
	public class UnresolvedPathSegment : ODataPathSegment
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0000F765 File Offset: 0x0000D965
		public UnresolvedPathSegment(string segmentValue)
		{
			if (segmentValue == null)
			{
				throw Error.ArgumentNull("segmentValue");
			}
			this.SegmentValue = segmentValue;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000F782 File Offset: 0x0000D982
		public virtual string SegmentKind
		{
			get
			{
				return "unresolved";
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000F789 File Offset: 0x0000D989
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000F791 File Offset: 0x0000D991
		public string SegmentValue { get; private set; }

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000F79A File Offset: 0x0000D99A
		public override string ToString()
		{
			return this.SegmentValue;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F7A4 File Offset: 0x0000D9A4
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			if (typeof(T) == typeof(string))
			{
				return (T)((object)this.SegmentValue);
			}
			return default(T);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void HandleWith(PathSegmentHandler handler)
		{
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}
	}
}
