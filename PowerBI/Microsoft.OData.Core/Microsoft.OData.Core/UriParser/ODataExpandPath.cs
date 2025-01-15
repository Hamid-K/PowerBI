using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018F RID: 399
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataExpandPathCollection just doesn't sound right")]
	public class ODataExpandPath : ODataPath
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x000397E0 File Offset: 0x000379E0
		public ODataExpandPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000397EF File Offset: 0x000379EF
		public ODataExpandPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000397FE File Offset: 0x000379FE
		internal IEdmNavigationProperty GetNavigationProperty()
		{
			return ((NavigationPropertySegment)base.LastSegment).NavigationProperty;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00039810 File Offset: 0x00037A10
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
