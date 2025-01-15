using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000193 RID: 403
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataSelectPathCollection just doesn't sound right")]
	public class ODataSelectPath : ODataPath
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x00039FA8 File Offset: 0x000381A8
		public ODataSelectPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00039FB7 File Offset: 0x000381B7
		public ODataSelectPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00039FC8 File Offset: 0x000381C8
		private void ValidatePath()
		{
			int num = 0;
			if (base.Count == 1 && base.FirstSegment is TypeSegment)
			{
				throw new ODataException(Strings.ODataSelectPath_CannotOnlyHaveTypeSegment);
			}
			foreach (ODataPathSegment odataPathSegment in this)
			{
				if (odataPathSegment is NavigationPropertySegment)
				{
					if (num != base.Count - 1)
					{
						throw new ODataException(Strings.ODataSelectPath_NavPropSegmentCanOnlyBeLastSegment);
					}
				}
				else if (odataPathSegment is OperationSegment)
				{
					if (num != base.Count - 1)
					{
						throw new ODataException(Strings.ODataSelectPath_OperationSegmentCanOnlyBeLastSegment);
					}
				}
				else
				{
					if (odataPathSegment is DynamicPathSegment || odataPathSegment is PropertySegment || odataPathSegment is TypeSegment || odataPathSegment is AnnotationSegment)
					{
						num++;
						continue;
					}
					throw new ODataException(Strings.ODataSelectPath_InvalidSelectPathSegmentType(odataPathSegment.GetType().Name));
				}
				num++;
			}
		}
	}
}
