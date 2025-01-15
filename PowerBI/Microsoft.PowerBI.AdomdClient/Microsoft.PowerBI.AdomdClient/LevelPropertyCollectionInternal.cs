using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A1 RID: 161
	internal sealed class LevelPropertyCollectionInternal : CacheBasedFilteredCollection
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x0002849C File Offset: 0x0002669C
		internal LevelPropertyCollectionInternal(AdomdConnection connection, Level parentLevel)
			: base(connection, InternalObjectType.InternalTypeLevelProperty, parentLevel.ParentHierarchy.ParentDimension.ParentCube.metadataCache)
		{
			this.parentLevel = parentLevel;
			base.Initialize((DataRow)((IAdomdBaseObject)parentLevel).MetadataData, null);
		}

		// Token: 0x170002EF RID: 751
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

		// Token: 0x170002F0 RID: 752
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

		// Token: 0x0600094B RID: 2379 RVA: 0x0002854C File Offset: 0x0002674C
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

		// Token: 0x0600094C RID: 2380 RVA: 0x000285BD File Offset: 0x000267BD
		public override IEnumerator GetEnumerator()
		{
			return new LevelPropertyCollection.Enumerator(this);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000285CC File Offset: 0x000267CC
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

		// Token: 0x040005FC RID: 1532
		internal const string schemaName = "MDSCHEMA_PROPERTIES";

		// Token: 0x040005FD RID: 1533
		internal const string propertyType = "PROPERTY_TYPE";

		// Token: 0x040005FE RID: 1534
		internal const int MDPROP_MEMBER = 1;

		// Token: 0x040005FF RID: 1535
		private Level parentLevel;
	}
}
