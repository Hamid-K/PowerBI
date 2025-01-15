using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200057A RID: 1402
	internal abstract class ConstraintBase : InternalBase
	{
		// Token: 0x060043E0 RID: 17376
		internal abstract ErrorLog.Record GetErrorRecord();
	}
}
