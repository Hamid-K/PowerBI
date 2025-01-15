using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001AE RID: 430
	public class MapDataRegion : ReportObject, INamedObject
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00022F7E File Offset: 0x0002117E
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x00022F91 File Offset: 0x00021191
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00022FA0 File Offset: 0x000211A0
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x00022FB3 File Offset: 0x000211B3
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00022FC2 File Offset: 0x000211C2
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00022FD5 File Offset: 0x000211D5
		[XmlElement(typeof(RdlCollection<Filter>))]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00022FE4 File Offset: 0x000211E4
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00022FF7 File Offset: 0x000211F7
		public MapMember MapMember
		{
			get
			{
				return (MapMember)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00023006 File Offset: 0x00021206
		public MapDataRegion()
		{
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002300E File Offset: 0x0002120E
		internal MapDataRegion(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00023017 File Offset: 0x00021217
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002301F File Offset: 0x0002121F
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, this.DataSetName);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002303C File Offset: 0x0002123C
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Report ancestor = base.GetAncestor<Report>();
			if (ancestor != null)
			{
				DataSet dataSetByName = ancestor.GetDataSetByName(this.DataSetName);
				if (dataSetByName != null && !dependencies.Contains(dataSetByName))
				{
					dependencies.Add(dataSetByName);
				}
			}
		}

		// Token: 0x020003DA RID: 986
		internal class Definition : DefinitionStore<MapDataRegion, MapDataRegion.Definition.Properties>
		{
			// Token: 0x0600187E RID: 6270 RVA: 0x0003B771 File Offset: 0x00039971
			private Definition()
			{
			}

			// Token: 0x020004F2 RID: 1266
			internal enum Properties
			{
				// Token: 0x04001040 RID: 4160
				Name,
				// Token: 0x04001041 RID: 4161
				DataSetName,
				// Token: 0x04001042 RID: 4162
				Filters,
				// Token: 0x04001043 RID: 4163
				MapMember,
				// Token: 0x04001044 RID: 4164
				PropertyCount
			}
		}
	}
}
