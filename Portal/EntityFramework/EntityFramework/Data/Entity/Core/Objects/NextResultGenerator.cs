using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000412 RID: 1042
	internal class NextResultGenerator
	{
		// Token: 0x06003151 RID: 12625 RVA: 0x0009D15F File Offset: 0x0009B35F
		internal NextResultGenerator(ObjectContext context, EntityCommand entityCommand, EdmType[] edmTypes, ReadOnlyCollection<EntitySet> entitySets, MergeOption mergeOption, bool streaming, int resultSetIndex)
		{
			this._context = context;
			this._entityCommand = entityCommand;
			this._entitySets = entitySets;
			this._edmTypes = edmTypes;
			this._resultSetIndex = resultSetIndex;
			this._streaming = streaming;
			this._mergeOption = mergeOption;
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x0009D19C File Offset: 0x0009B39C
		internal ObjectResult<TElement> GetNextResult<TElement>(DbDataReader storeReader)
		{
			bool flag = false;
			try
			{
				flag = storeReader.NextResult();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, ex);
				}
				throw;
			}
			if (flag)
			{
				MetadataHelper.CheckFunctionImportReturnType<TElement>(this._edmTypes[this._resultSetIndex], this._context.MetadataWorkspace);
				return this._context.MaterializedDataRecord<TElement>(this._entityCommand, storeReader, this._resultSetIndex, this._entitySets, this._edmTypes, null, this._mergeOption, this._streaming);
			}
			return null;
		}

		// Token: 0x04001041 RID: 4161
		private readonly EntityCommand _entityCommand;

		// Token: 0x04001042 RID: 4162
		private readonly ReadOnlyCollection<EntitySet> _entitySets;

		// Token: 0x04001043 RID: 4163
		private readonly ObjectContext _context;

		// Token: 0x04001044 RID: 4164
		private readonly EdmType[] _edmTypes;

		// Token: 0x04001045 RID: 4165
		private readonly int _resultSetIndex;

		// Token: 0x04001046 RID: 4166
		private readonly bool _streaming;

		// Token: 0x04001047 RID: 4167
		private readonly MergeOption _mergeOption;
	}
}
