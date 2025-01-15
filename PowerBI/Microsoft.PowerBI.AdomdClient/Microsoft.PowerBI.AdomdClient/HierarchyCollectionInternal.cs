using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008A RID: 138
	internal sealed class HierarchyCollectionInternal : CacheBasedFilteredCollection
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x000275EC File Offset: 0x000257EC
		internal HierarchyCollectionInternal(AdomdConnection connection, Set axis, string cubeName)
			: base(connection)
		{
			this.connection = connection;
			this.axis = axis;
			this.cubeName = cubeName;
			this.internalTableCollection = axis.AxisDataset;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00027618 File Offset: 0x00025818
		internal HierarchyCollectionInternal(AdomdConnection connection, Dimension parentDimension, bool isAttribute)
			: base(connection, InternalObjectType.InternalTypeHierarchy, parentDimension.ParentCube.metadataCache)
		{
			this.connection = connection;
			this.parentDimension = parentDimension;
			string text = null;
			if (isAttribute)
			{
				text = "( ((HIERARCHY_ORIGIN % 2) = 0) AND ( ( Convert(HIERARCHY_ORIGIN/ 2, 'System.Int32') % 2 ) = 1 ))";
			}
			base.Initialize((DataRow)((IAdomdBaseObject)parentDimension).MetadataData, text);
		}

		// Token: 0x17000287 RID: 647
		public Hierarchy this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.IsMetadata)
				{
					DataRow dataRow = this.internalCollection[index];
					return HierarchyCollectionInternal.GetHiearchyByRow(this.connection, dataRow, this.parentDimension, base.Catalog, base.SessionId);
				}
				DataTable dataTable = this.axis.AxisDataset[index];
				return new Hierarchy(this.connection, dataTable, this.cubeName, this.axis.ParentAxis, index);
			}
		}

		// Token: 0x17000288 RID: 648
		public Hierarchy this[string index]
		{
			get
			{
				Hierarchy hierarchy = this.Find(index);
				if (null == hierarchy)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return hierarchy;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002771C File Offset: 0x0002591C
		public Hierarchy Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			if (this.IsMetadata)
			{
				DataRow dataRow = base.FindObjectByName(index, (DataRow)((IAdomdBaseObject)this.parentDimension).MetadataData, Hierarchy.hierarchyNameColumn);
				if (dataRow == null && index == this.parentDimension.Name)
				{
					dataRow = base.FindObjectByName(null, (DataRow)((IAdomdBaseObject)this.parentDimension).MetadataData, Hierarchy.hierarchyNameColumn);
				}
				if (dataRow == null)
				{
					return null;
				}
				return HierarchyCollectionInternal.GetHiearchyByRow(this.connection, dataRow, this.parentDimension, base.Catalog, base.SessionId);
			}
			else
			{
				if (this.Count == 0)
				{
					return null;
				}
				if (this.parentDimension == null)
				{
					this.parentDimension = this[0].ParentDimension;
				}
				Hierarchy hierarchy = this.parentDimension.hierarchies.Find(index);
				if (hierarchy != null && this.axis.AxisDataset.Contains(hierarchy.UniqueName))
				{
					return hierarchy;
				}
				return null;
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00027813 File Offset: 0x00025A13
		public override IEnumerator GetEnumerator()
		{
			return new HierarchyCollection.Enumerator(this);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00027820 File Offset: 0x00025A20
		public override void CopyTo(Array array, int index)
		{
			if (this.IsMetadata)
			{
				base.CopyTo(array, index);
				return;
			}
			this.internalTableCollection.CopyTo(array, index);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00027840 File Offset: 0x00025A40
		public override int Count
		{
			get
			{
				if (this.IsMetadata)
				{
					return base.Count;
				}
				return this.internalTableCollection.Count;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0002785C File Offset: 0x00025A5C
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002785F File Offset: 0x00025A5F
		public override object SyncRoot
		{
			get
			{
				if (this.IsMetadata)
				{
					return base.SyncRoot;
				}
				return this.internalTableCollection.SyncRoot;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002787C File Offset: 0x00025A7C
		internal static Hierarchy GetHiearchyByRow(AdomdConnection connection, DataRow row, Dimension parentDimension, string catalog, string sessionId)
		{
			Hierarchy hierarchy;
			if (row[0] is DBNull)
			{
				hierarchy = new Hierarchy(connection, row, parentDimension, catalog, sessionId);
				row[0] = hierarchy;
			}
			else
			{
				hierarchy = (Hierarchy)row[0];
			}
			return hierarchy;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x000278BB File Offset: 0x00025ABB
		private bool IsMetadata
		{
			get
			{
				return this.axis == null;
			}
		}

		// Token: 0x040005AE RID: 1454
		internal static string schemaName = "MDSCHEMA_HIERARCHIES";

		// Token: 0x040005AF RID: 1455
		internal static string hierUNameRest = "HIERARCHY_UNIQUE_NAME";

		// Token: 0x040005B0 RID: 1456
		private const string attrbHierRest = "HIERARCHY_ORIGIN";

		// Token: 0x040005B1 RID: 1457
		private const string attributeHierarchyFilterExpression = "( ((HIERARCHY_ORIGIN % 2) = 0) AND ( ( Convert(HIERARCHY_ORIGIN/ 2, 'System.Int32') % 2 ) = 1 ))";

		// Token: 0x040005B2 RID: 1458
		internal AdomdConnection connection;

		// Token: 0x040005B3 RID: 1459
		internal IDSFDataSet internalTableCollection;

		// Token: 0x040005B4 RID: 1460
		private Set axis;

		// Token: 0x040005B5 RID: 1461
		private Dimension parentDimension;

		// Token: 0x040005B6 RID: 1462
		private string cubeName;
	}
}
