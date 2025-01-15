using System;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x020002C4 RID: 708
	public interface IMergeableAnnotation
	{
		// Token: 0x06002229 RID: 8745
		CompatibilityResult IsCompatibleWith(object other);

		// Token: 0x0600222A RID: 8746
		object MergeWith(object other);
	}
}
