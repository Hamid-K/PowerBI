using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200008A RID: 138
	internal sealed class HierarchyCollectionInternal : CacheBasedFilteredCollection
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0002791C File Offset: 0x00025B1C
		internal HierarchyCollectionInternal(AdomdConnection connection, Set axis, string cubeName)
			: base(connection)
		{
			this.connection = connection;
			this.axis = axis;
			this.cubeName = cubeName;
			this.internalTableCollection = axis.AxisDataset;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00027948 File Offset: 0x00025B48
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

		// Token: 0x1700028D RID: 653
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

		// Token: 0x1700028E RID: 654
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

		// Token: 0x06000892 RID: 2194 RVA: 0x00027A4C File Offset: 0x00025C4C
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

		// Token: 0x06000893 RID: 2195 RVA: 0x00027B43 File Offset: 0x00025D43
		public override IEnumerator GetEnumerator()
		{
			return new HierarchyCollection.Enumerator(this);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00027B50 File Offset: 0x00025D50
		public override void CopyTo(Array array, int index)
		{
			if (this.IsMetadata)
			{
				base.CopyTo(array, index);
				return;
			}
			this.internalTableCollection.CopyTo(array, index);
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00027B70 File Offset: 0x00025D70
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00027B8C File Offset: 0x00025D8C
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00027B8F File Offset: 0x00025D8F
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

		// Token: 0x06000898 RID: 2200 RVA: 0x00027BAC File Offset: 0x00025DAC
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00027BEB File Offset: 0x00025DEB
		private bool IsMetadata
		{
			get
			{
				return this.axis == null;
			}
		}

		// Token: 0x040005BB RID: 1467
		internal static string schemaName = "MDSCHEMA_HIERARCHIES";

		// Token: 0x040005BC RID: 1468
		internal static string hierUNameRest = "HIERARCHY_UNIQUE_NAME";

		// Token: 0x040005BD RID: 1469
		private const string attrbHierRest = "HIERARCHY_ORIGIN";

		// Token: 0x040005BE RID: 1470
		private const string attributeHierarchyFilterExpression = "( ((HIERARCHY_ORIGIN % 2) = 0) AND ( ( Convert(HIERARCHY_ORIGIN/ 2, 'System.Int32') % 2 ) = 1 ))";

		// Token: 0x040005BF RID: 1471
		internal AdomdConnection connection;

		// Token: 0x040005C0 RID: 1472
		internal IDSFDataSet internalTableCollection;

		// Token: 0x040005C1 RID: 1473
		private Set axis;

		// Token: 0x040005C2 RID: 1474
		private Dimension parentDimension;

		// Token: 0x040005C3 RID: 1475
		private string cubeName;
	}
}
