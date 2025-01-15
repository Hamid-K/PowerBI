using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200003F RID: 63
	internal class BatchSubtotalAnnotations
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00007861 File Offset: 0x00005A61
		internal BatchSubtotalAnnotations()
		{
			this.m_subtotalAnnotations = new Dictionary<IScope, BatchSubtotalAnnotation>();
			this.m_subtotalSourceAnnotations = new Dictionary<IIdentifiable, BatchSubtotalAnnotation>();
			this.m_subtotalAnnotationSources = new Dictionary<BatchSubtotalAnnotation, IList<IIdentifiable>>();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000788A File Offset: 0x00005A8A
		internal BatchSubtotalAnnotations(BatchSubtotalAnnotations otherAnnotations)
		{
			this.m_subtotalAnnotations = new Dictionary<IScope, BatchSubtotalAnnotation>(otherAnnotations.m_subtotalAnnotations);
			this.m_subtotalSourceAnnotations = new Dictionary<IIdentifiable, BatchSubtotalAnnotation>(otherAnnotations.m_subtotalSourceAnnotations);
			this.m_subtotalAnnotationSources = new Dictionary<BatchSubtotalAnnotation, IList<IIdentifiable>>(otherAnnotations.m_subtotalAnnotationSources);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000078C5 File Offset: 0x00005AC5
		internal int SubtotalAnnotationCount
		{
			get
			{
				return this.m_subtotalAnnotations.Count;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000078D2 File Offset: 0x00005AD2
		internal bool TryGetSubtotalAnnotation(IScope key, out BatchSubtotalAnnotation subtotalAnnotation)
		{
			return this.m_subtotalAnnotations.TryGetValue(key, out subtotalAnnotation);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000078E1 File Offset: 0x00005AE1
		internal bool ContainsSubtotalSourceAnnotation(IIdentifiable source)
		{
			return this.m_subtotalSourceAnnotations.ContainsKey(source);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000078EF File Offset: 0x00005AEF
		internal bool TryGetSubtotalSourceAnnotation(DataMember member, out BatchSubtotalAnnotation subtotalAnnotation)
		{
			return this.m_subtotalSourceAnnotations.TryGetValue(member, out subtotalAnnotation);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00007900 File Offset: 0x00005B00
		public bool Validate(TranslationErrorContext errorContext)
		{
			bool flag = true;
			foreach (KeyValuePair<IScope, BatchSubtotalAnnotation> keyValuePair in this.m_subtotalAnnotations)
			{
				if (!keyValuePair.Value.IsValid)
				{
					errorContext.Register(TranslationMessages.InvalidBatchSubtotalAnnotation(EngineMessageSeverity.Error, keyValuePair.Key.ObjectType, keyValuePair.Key.Id, null, keyValuePair.Value.ErrorMessage));
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00007990 File Offset: 0x00005B90
		public static string GetSubtotalIndicatorColumnName(IScope rollupParent, bool primary, string dataShapeIdentifier, int dataShapeDepthCount)
		{
			string text;
			string text2;
			if (rollupParent.ObjectType == ObjectType.DataShape)
			{
				text = ((dataShapeDepthCount > 1) ? dataShapeIdentifier : string.Empty);
				text2 = (primary ? "GrandTotalRow" : "GrandTotalColumn");
			}
			else
			{
				text = string.Empty;
				text2 = rollupParent.Id.Value;
			}
			return "Is" + text + text2 + "Total";
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000079E9 File Offset: 0x00005BE9
		public bool TryGetSubtotalAnnotationSources(BatchSubtotalAnnotation annotation, out IList<IIdentifiable> sources)
		{
			return this.m_subtotalAnnotationSources.TryGetValue(annotation, out sources);
		}

		// Token: 0x040000B4 RID: 180
		protected readonly Dictionary<IScope, BatchSubtotalAnnotation> m_subtotalAnnotations;

		// Token: 0x040000B5 RID: 181
		protected readonly Dictionary<IIdentifiable, BatchSubtotalAnnotation> m_subtotalSourceAnnotations;

		// Token: 0x040000B6 RID: 182
		protected readonly Dictionary<BatchSubtotalAnnotation, IList<IIdentifiable>> m_subtotalAnnotationSources;
	}
}
