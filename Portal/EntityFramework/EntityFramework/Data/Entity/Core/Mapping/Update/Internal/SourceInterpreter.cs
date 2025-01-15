using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D0 RID: 1488
	internal class SourceInterpreter
	{
		// Token: 0x060047C1 RID: 18369 RVA: 0x000FE520 File Offset: 0x000FC720
		private SourceInterpreter(UpdateTranslator translator, EntitySet sourceTable)
		{
			this.m_stateEntries = new List<IEntityStateEntry>();
			this.m_translator = translator;
			this.m_sourceTable = sourceTable;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x000FE541 File Offset: 0x000FC741
		internal static ReadOnlyCollection<IEntityStateEntry> GetAllStateEntries(PropagatorResult source, UpdateTranslator translator, EntitySet sourceTable)
		{
			SourceInterpreter sourceInterpreter = new SourceInterpreter(translator, sourceTable);
			sourceInterpreter.RetrieveResultMarkup(source);
			return new ReadOnlyCollection<IEntityStateEntry>(sourceInterpreter.m_stateEntries);
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x000FE55C File Offset: 0x000FC75C
		private void RetrieveResultMarkup(PropagatorResult source)
		{
			if (source.Identifier != -1)
			{
				do
				{
					if (source.StateEntry != null)
					{
						this.m_stateEntries.Add(source.StateEntry);
						if (source.Identifier != -1)
						{
							PropagatorResult propagatorResult;
							if (this.m_translator.KeyManager.TryGetIdentifierOwner(source.Identifier, out propagatorResult) && propagatorResult.StateEntry != null && this.ExtentInScope(propagatorResult.StateEntry.EntitySet))
							{
								this.m_stateEntries.Add(propagatorResult.StateEntry);
							}
							foreach (IEntityStateEntry entityStateEntry in this.m_translator.KeyManager.GetDependentStateEntries(source.Identifier))
							{
								this.m_stateEntries.Add(entityStateEntry);
							}
						}
					}
					source = source.Next;
				}
				while (source != null);
				return;
			}
			if (!source.IsSimple && !source.IsNull)
			{
				foreach (PropagatorResult propagatorResult2 in source.GetMemberValues())
				{
					this.RetrieveResultMarkup(propagatorResult2);
				}
			}
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x000FE680 File Offset: 0x000FC880
		private bool ExtentInScope(EntitySetBase extent)
		{
			return extent != null && this.m_translator.ViewLoader.GetAffectedTables(extent, this.m_translator.MetadataWorkspace).Contains(this.m_sourceTable);
		}

		// Token: 0x04001985 RID: 6533
		private readonly List<IEntityStateEntry> m_stateEntries;

		// Token: 0x04001986 RID: 6534
		private readonly UpdateTranslator m_translator;

		// Token: 0x04001987 RID: 6535
		private readonly EntitySet m_sourceTable;
	}
}
