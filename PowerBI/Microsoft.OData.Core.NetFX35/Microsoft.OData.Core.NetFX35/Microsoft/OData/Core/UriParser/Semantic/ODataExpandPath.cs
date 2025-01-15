using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200024C RID: 588
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataExpandPathCollection just doesn't sound right")]
	public class ODataExpandPath : ODataPath
	{
		// Token: 0x060014E8 RID: 5352 RVA: 0x0004A295 File Offset: 0x00048495
		public ODataExpandPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004A2A4 File Offset: 0x000484A4
		public ODataExpandPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0004A2B3 File Offset: 0x000484B3
		internal IEdmNavigationProperty GetNavigationProperty()
		{
			return ((NavigationPropertySegment)base.LastSegment).NavigationProperty;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004A2C8 File Offset: 0x000484C8
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
