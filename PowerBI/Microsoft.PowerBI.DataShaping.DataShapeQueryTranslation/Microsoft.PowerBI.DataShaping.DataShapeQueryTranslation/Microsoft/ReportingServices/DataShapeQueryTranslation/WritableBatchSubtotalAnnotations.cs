using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000040 RID: 64
	internal sealed class WritableBatchSubtotalAnnotations : BatchSubtotalAnnotations
	{
		// Token: 0x0600029C RID: 668 RVA: 0x000079F8 File Offset: 0x00005BF8
		internal WritableBatchSubtotalAnnotations()
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007A00 File Offset: 0x00005C00
		internal WritableBatchSubtotalAnnotations(BatchSubtotalAnnotations otherAnnotations)
			: base(otherAnnotations)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00007A09 File Offset: 0x00005C09
		internal void AddSubtotalAnnotation(IScope key, BatchSubtotalAnnotation subtotalAnnotation)
		{
			this.m_subtotalAnnotations.Add(key, subtotalAnnotation);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007A18 File Offset: 0x00005C18
		internal void AddSubtotalSourceAnnotation(IIdentifiable source, BatchSubtotalAnnotation subtotalAnnotation)
		{
			this.m_subtotalSourceAnnotations.Add(source, subtotalAnnotation);
			if (this.m_subtotalAnnotationSources.ContainsKey(subtotalAnnotation))
			{
				this.m_subtotalAnnotationSources[subtotalAnnotation].Add(source);
				return;
			}
			this.m_subtotalAnnotationSources.Add(subtotalAnnotation, new List<IIdentifiable> { source });
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00007A6B File Offset: 0x00005C6B
		internal void RemoveSubtotalSourceAnnotation(DataMember member)
		{
			this.m_subtotalSourceAnnotations.Remove(member);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007A7C File Offset: 0x00005C7C
		internal void RemoveSubtotalAnnotation(BatchSubtotalAnnotation subtotalAnnotation)
		{
			IEnumerable<KeyValuePair<IScope, BatchSubtotalAnnotation>> subtotalAnnotations = this.m_subtotalAnnotations;
			Func<KeyValuePair<IScope, BatchSubtotalAnnotation>, bool> <>9__0;
			Func<KeyValuePair<IScope, BatchSubtotalAnnotation>, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (KeyValuePair<IScope, BatchSubtotalAnnotation> kvp) => kvp.Value == subtotalAnnotation);
			}
			foreach (KeyValuePair<IScope, BatchSubtotalAnnotation> keyValuePair in subtotalAnnotations.Where(func).ToList<KeyValuePair<IScope, BatchSubtotalAnnotation>>())
			{
				this.m_subtotalAnnotations.Remove(keyValuePair.Key);
			}
		}
	}
}
