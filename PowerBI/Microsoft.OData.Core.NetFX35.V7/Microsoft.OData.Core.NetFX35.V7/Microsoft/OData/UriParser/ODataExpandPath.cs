using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000143 RID: 323
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataExpandPathCollection just doesn't sound right")]
	public class ODataExpandPath : ODataPath
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x00029A3B File Offset: 0x00027C3B
		public ODataExpandPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00029A4A File Offset: 0x00027C4A
		public ODataExpandPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00029A59 File Offset: 0x00027C59
		internal IEdmNavigationProperty GetNavigationProperty()
		{
			return ((NavigationPropertySegment)base.LastSegment).NavigationProperty;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00029A6C File Offset: 0x00027C6C
		private void ValidatePath()
		{
			int num = 0;
			bool flag = false;
			foreach (ODataPathSegment odataPathSegment in this)
			{
				if (odataPathSegment is TypeSegment)
				{
					if (num == base.Count - 1)
					{
						throw new ODataException(Strings.ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty);
					}
				}
				else if (odataPathSegment is PropertySegment)
				{
					if (num == base.Count - 1)
					{
						throw new ODataException(Strings.ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty);
					}
				}
				else
				{
					if (!(odataPathSegment is NavigationPropertySegment))
					{
						throw new ODataException(Strings.ODataExpandPath_InvalidExpandPathSegment(odataPathSegment.GetType().Name));
					}
					if (num < base.Count - 1 || flag)
					{
						throw new ODataException(Strings.ODataExpandPath_OnlyLastSegmentMustBeNavigationProperty);
					}
					flag = true;
				}
				num++;
			}
		}
	}
}
