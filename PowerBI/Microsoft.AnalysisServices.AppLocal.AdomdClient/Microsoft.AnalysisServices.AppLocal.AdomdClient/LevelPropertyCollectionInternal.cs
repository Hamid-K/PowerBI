using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A1 RID: 161
	internal sealed class LevelPropertyCollectionInternal : CacheBasedFilteredCollection
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x000287CC File Offset: 0x000269CC
		internal LevelPropertyCollectionInternal(AdomdConnection connection, Level parentLevel)
			: base(connection, InternalObjectType.InternalTypeLevelProperty, parentLevel.ParentHierarchy.ParentDimension.ParentCube.metadataCache)
		{
			this.parentLevel = parentLevel;
			base.Initialize((DataRow)((IAdomdBaseObject)parentLevel).MetadataData, null);
		}

		// Token: 0x170002F5 RID: 757
		public LevelProperty this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return LevelPropertyCollectionInternal.GetLevelPropertyByRow(base.Connection, dataRow, this.parentLevel, index);
			}
		}

		// Token: 0x170002F6 RID: 758
		public LevelProperty this[string index]
		{
			get
			{
				LevelProperty levelProperty = this.Find(index);
				if (null == levelProperty)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return levelProperty;
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002887C File Offset: 0x00026A7C
		public LevelProperty Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, (DataRow)((IAdomdBaseObject)this.parentLevel).MetadataData, LevelProperty.levelPropNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			int num = 0;
			DataRow[] internalCollection = this.internalCollection;
			int num2 = 0;
			while (num2 < internalCollection.Length && internalCollection[num2] != dataRow)
			{
				num++;
				num2++;
			}
			return LevelPropertyCollectionInternal.GetLevelPropertyByRow(base.Connection, dataRow, this.parentLevel, num);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000288ED File Offset: 0x00026AED
		public override IEnumerator GetEnumerator()
		{
			return new LevelPropertyCollection.Enumerator(this);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000288FC File Offset: 0x00026AFC
		internal static LevelProperty GetLevelPropertyByRow(AdomdConnection connection, DataRow row, Level parentLevel, int propOrdinal)
		{
			LevelProperty levelProperty;
			if (row[0] is DBNull)
			{
				levelProperty = new LevelProperty(connection, row, parentLevel, propOrdinal);
				row[0] = levelProperty;
			}
			else
			{
				levelProperty = (LevelProperty)row[0];
			}
			return levelProperty;
		}

		// Token: 0x04000609 RID: 1545
		internal const string schemaName = "MDSCHEMA_PROPERTIES";

		// Token: 0x0400060A RID: 1546
		internal const string propertyType = "PROPERTY_TYPE";

		// Token: 0x0400060B RID: 1547
		internal const int MDPROP_MEMBER = 1;

		// Token: 0x0400060C RID: 1548
		private Level parentLevel;
	}
}
