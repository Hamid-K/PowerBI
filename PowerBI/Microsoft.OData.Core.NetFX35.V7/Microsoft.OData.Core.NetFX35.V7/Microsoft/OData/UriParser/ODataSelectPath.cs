using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000147 RID: 327
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataSelectPathCollection just doesn't sound right")]
	public class ODataSelectPath : ODataPath
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x0002A19A File Offset: 0x0002839A
		public ODataSelectPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002A1A9 File Offset: 0x000283A9
		public ODataSelectPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002A1B8 File Offset: 0x000283B8
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
					if (odataPathSegment is DynamicPathSegment || odataPathSegment is PropertySegment || odataPathSegment is TypeSegment)
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
