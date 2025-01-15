using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200018E RID: 398
	public class MapSpatialDataSet : MapSpatialData
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x00021701 File Offset: 0x0001F901
		public MapSpatialDataSet()
		{
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00021709 File Offset: 0x0001F909
		internal MapSpatialDataSet(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00021712 File Offset: 0x0001F912
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00021720 File Offset: 0x0001F920
		public ReportExpression DataSetName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00021734 File Offset: 0x0001F934
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00021742 File Offset: 0x0001F942
		public ReportExpression SpatialField
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00021756 File Offset: 0x0001F956
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00021769 File Offset: 0x0001F969
		[XmlElement(typeof(RdlCollection<ReportExpression>))]
		[XmlArrayItem("MapFieldName", typeof(ReportExpression))]
		public IList<ReportExpression> MapFieldNames
		{
			get
			{
				return (IList<ReportExpression>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00021778 File Offset: 0x0001F978
		public override void Initialize()
		{
			base.Initialize();
			this.MapFieldNames = new RdlCollection<ReportExpression>();
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002178C File Offset: 0x0001F98C
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, this.DataSetName.Expression);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000217C0 File Offset: 0x0001F9C0
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Report ancestor = base.GetAncestor<Report>();
			if (ancestor != null)
			{
				DataSet dataSetByName = ancestor.GetDataSetByName(this.DataSetName.Expression);
				if (dataSetByName != null && !dependencies.Contains(dataSetByName))
				{
					dependencies.Add(dataSetByName);
				}
			}
		}

		// Token: 0x020003BB RID: 955
		internal class Definition : DefinitionStore<MapSpatialDataSet, MapSpatialDataSet.Definition.Properties>
		{
			// Token: 0x0600185F RID: 6239 RVA: 0x0003B679 File Offset: 0x00039879
			private Definition()
			{
			}

			// Token: 0x020004D3 RID: 1235
			internal enum Properties
			{
				// Token: 0x04000F29 RID: 3881
				DataSetName,
				// Token: 0x04000F2A RID: 3882
				SpatialField,
				// Token: 0x04000F2B RID: 3883
				MapFieldNames,
				// Token: 0x04000F2C RID: 3884
				PropertyCount
			}
		}
	}
}
