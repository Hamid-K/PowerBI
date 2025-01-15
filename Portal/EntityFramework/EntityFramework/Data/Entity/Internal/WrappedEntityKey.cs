using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200012E RID: 302
	internal class WrappedEntityKey
	{
		// Token: 0x060014A8 RID: 5288 RVA: 0x00035F68 File Offset: 0x00034168
		public WrappedEntityKey(EntitySet entitySet, string entitySetName, object[] keyValues, string keyValuesParamName)
		{
			if (keyValues == null)
			{
				keyValues = new object[1];
			}
			List<string> list = entitySet.ElementType.KeyMembers.Select((EdmMember m) => m.Name).ToList<string>();
			if (list.Count != keyValues.Length)
			{
				throw new ArgumentException(Strings.DbSet_WrongNumberOfKeyValuesPassed, keyValuesParamName);
			}
			this._keyValuePairs = list.Zip(keyValues, (string name, object value) => new KeyValuePair<string, object>(name, value));
			if (keyValues.All((object v) => v != null))
			{
				this._key = new EntityKey(entitySetName, this.KeyValuePairs);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00036038 File Offset: 0x00034238
		public bool HasNullValues
		{
			get
			{
				return this._key == null;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00036046 File Offset: 0x00034246
		public EntityKey EntityKey
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0003604E File Offset: 0x0003424E
		public IEnumerable<KeyValuePair<string, object>> KeyValuePairs
		{
			get
			{
				return this._keyValuePairs;
			}
		}

		// Token: 0x040009B6 RID: 2486
		private readonly IEnumerable<KeyValuePair<string, object>> _keyValuePairs;

		// Token: 0x040009B7 RID: 2487
		private readonly EntityKey _key;
	}
}
