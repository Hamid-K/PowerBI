using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x020002C1 RID: 705
	public abstract class AnnotationCodeGenerator
	{
		// Token: 0x0600221A RID: 8730 RVA: 0x0005FEE8 File Offset: 0x0005E0E8
		public virtual IEnumerable<string> GetExtraNamespaces(IEnumerable<string> annotationNames)
		{
			Check.NotNull<IEnumerable<string>>(annotationNames, "annotationNames");
			return Enumerable.Empty<string>();
		}

		// Token: 0x0600221B RID: 8731
		public abstract void Generate(string annotationName, object annotation, IndentedTextWriter writer);
	}
}
