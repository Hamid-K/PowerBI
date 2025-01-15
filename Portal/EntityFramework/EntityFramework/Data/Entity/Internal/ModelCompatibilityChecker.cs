using System;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000125 RID: 293
	internal class ModelCompatibilityChecker
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x000356C8 File Offset: 0x000338C8
		public virtual bool CompatibleWithModel(InternalContext internalContext, ModelHashCalculator modelHashCalculator, bool throwIfNoMetadata, DatabaseExistenceState existenceState = DatabaseExistenceState.Unknown)
		{
			if (internalContext.CodeFirstModel == null)
			{
				if (throwIfNoMetadata)
				{
					throw Error.Database_NonCodeFirstCompatibilityCheck();
				}
				return true;
			}
			else
			{
				VersionedModel versionedModel = internalContext.QueryForModel(existenceState);
				if (versionedModel != null)
				{
					return internalContext.ModelMatches(versionedModel);
				}
				string text = internalContext.QueryForModelHash();
				if (text != null)
				{
					return string.Equals(text, modelHashCalculator.Calculate(internalContext.CodeFirstModel), StringComparison.Ordinal);
				}
				if (throwIfNoMetadata)
				{
					throw Error.Database_NoDatabaseMetadata();
				}
				return true;
			}
		}
	}
}
