using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200024E RID: 590
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "ODataSelectPathCollection just doesn't sound right")]
	public class ODataSelectPath : ODataPath
	{
		// Token: 0x060014F8 RID: 5368 RVA: 0x0004A586 File Offset: 0x00048786
		public ODataSelectPath(IEnumerable<ODataPathSegment> segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0004A595 File Offset: 0x00048795
		public ODataSelectPath(params ODataPathSegment[] segments)
			: base(segments)
		{
			this.ValidatePath();
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0004A5A4 File Offset: 0x000487A4
		private void ValidatePath()
		{
			int num = 0;
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
				else if (odataPathSegment is TypeSegment)
				{
					if (num == base.Count - 1)
					{
						throw new ODataException(Strings.ODataSelectPath_CannotEndInTypeSegment);
					}
				}
				else
				{
					if (!(odataPathSegment is OpenPropertySegment) && !(odataPathSegment is PropertySegment))
					{
						throw new ODataException(Strings.ODataSelectPath_InvalidSelectPathSegmentType(odataPathSegment.GetType().Name));
					}
					continue;
				}
				num++;
			}
		}
	}
}
