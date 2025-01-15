using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DD RID: 221
	internal sealed class NamedSetCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x0002E65C File Offset: 0x0002C85C
		internal NamedSetCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeNamedSet, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x170004A9 RID: 1193
		public NamedSet this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return NamedSetCollectionInternal.GetNamedSetByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x170004AA RID: 1194
		public NamedSet this[string index]
		{
			get
			{
				NamedSet namedSet = this.Find(index);
				if (null == namedSet)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return namedSet;
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002E6F4 File Offset: 0x0002C8F4
		public NamedSet Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, "SET_NAME");
			if (dataRow == null)
			{
				return null;
			}
			return NamedSetCollectionInternal.GetNamedSetByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002E740 File Offset: 0x0002C940
		public override IEnumerator GetEnumerator()
		{
			return new NamedSetCollection.Enumerator(this);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002E750 File Offset: 0x0002C950
		internal static NamedSet GetNamedSetByRow(AdomdConnection connection, DataRow row, CubeDef parentCube, string catalog, string sessionId)
		{
			NamedSet namedSet;
			if (row[0] is DBNull)
			{
				namedSet = new NamedSet(connection, row, parentCube, catalog, sessionId);
				row[0] = namedSet;
			}
			else
			{
				namedSet = (NamedSet)row[0];
			}
			return namedSet;
		}

		// Token: 0x040007EC RID: 2028
		internal static string schemaName = "MDSCHEMA_SETS";

		// Token: 0x040007ED RID: 2029
		private CubeDef parentCube;
	}
}
