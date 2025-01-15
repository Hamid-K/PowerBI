using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005CF RID: 1487
	internal class RecordConverter
	{
		// Token: 0x060047BD RID: 18365 RVA: 0x000FE48A File Offset: 0x000FC68A
		internal RecordConverter(UpdateTranslator updateTranslator)
		{
			this.m_updateTranslator = updateTranslator;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x000FE499 File Offset: 0x000FC699
		internal PropagatorResult ConvertOriginalValuesToPropagatorResult(IEntityStateEntry stateEntry, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			return this.ConvertStateEntryToPropagatorResult(stateEntry, false, modifiedPropertiesBehavior);
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x000FE4A4 File Offset: 0x000FC6A4
		internal PropagatorResult ConvertCurrentValuesToPropagatorResult(IEntityStateEntry stateEntry, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			return this.ConvertStateEntryToPropagatorResult(stateEntry, true, modifiedPropertiesBehavior);
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x000FE4B0 File Offset: 0x000FC6B0
		private PropagatorResult ConvertStateEntryToPropagatorResult(IEntityStateEntry stateEntry, bool useCurrentValues, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			PropagatorResult propagatorResult;
			try
			{
				IExtendedDataRecord extendedDataRecord;
				if (!useCurrentValues)
				{
					extendedDataRecord = (IExtendedDataRecord)stateEntry.OriginalValues;
				}
				else
				{
					IExtendedDataRecord currentValues = stateEntry.CurrentValues;
					extendedDataRecord = currentValues;
				}
				IExtendedDataRecord extendedDataRecord2 = extendedDataRecord;
				bool flag = false;
				propagatorResult = ExtractorMetadata.ExtractResultFromRecord(stateEntry, flag, extendedDataRecord2, useCurrentValues, this.m_updateTranslator, modifiedPropertiesBehavior);
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw EntityUtil.Update(Strings.Update_ErrorLoadingRecord, ex, new IEntityStateEntry[] { stateEntry });
				}
				throw;
			}
			return propagatorResult;
		}

		// Token: 0x04001984 RID: 6532
		private readonly UpdateTranslator m_updateTranslator;
	}
}
