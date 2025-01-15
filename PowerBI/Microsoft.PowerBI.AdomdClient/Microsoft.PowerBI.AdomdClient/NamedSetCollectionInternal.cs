using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DD RID: 221
	internal sealed class NamedSetCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0002E32C File Offset: 0x0002C52C
		internal NamedSetCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeNamedSet, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x170004A3 RID: 1187
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

		// Token: 0x170004A4 RID: 1188
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

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
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

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002E410 File Offset: 0x0002C610
		public override IEnumerator GetEnumerator()
		{
			return new NamedSetCollection.Enumerator(this);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002E420 File Offset: 0x0002C620
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

		// Token: 0x040007DF RID: 2015
		internal static string schemaName = "MDSCHEMA_SETS";

		// Token: 0x040007E0 RID: 2016
		private CubeDef parentCube;
	}
}
