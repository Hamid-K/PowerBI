using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000188 RID: 392
	public abstract class MapVectorLayer : MapLayer
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x0002135D File Offset: 0x0001F55D
		public MapVectorLayer()
		{
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00021365 File Offset: 0x0001F565
		internal MapVectorLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002136E File Offset: 0x0001F56E
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0002137C File Offset: 0x0001F57C
		public string MapDataRegionName
		{
			get
			{
				return base.PropertyStore.GetObject<string>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002138B File Offset: 0x0001F58B
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0002139E File Offset: 0x0001F59E
		[XmlElement(typeof(RdlCollection<MapBindingFieldPair>))]
		public IList<MapBindingFieldPair> MapBindingFieldPairs
		{
			get
			{
				return (IList<MapBindingFieldPair>)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000213AD File Offset: 0x0001F5AD
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x000213C0 File Offset: 0x0001F5C0
		[XmlElement(typeof(RdlCollection<MapFieldDefinition>))]
		public IList<MapFieldDefinition> MapFieldDefinitions
		{
			get
			{
				return (IList<MapFieldDefinition>)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000213CF File Offset: 0x0001F5CF
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x000213E2 File Offset: 0x0001F5E2
		public MapSpatialData MapSpatialData
		{
			get
			{
				return (MapSpatialData)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000213F1 File Offset: 0x0001F5F1
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00021405 File Offset: 0x0001F605
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00021415 File Offset: 0x0001F615
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00021424 File Offset: 0x0001F624
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues("MapDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(10);
			}
			set
			{
				((EnumProperty)DefinitionStore<MapVectorLayer, MapVectorLayer.Definition.Properties>.GetProperty(10)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(10, (int)value);
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00021447 File Offset: 0x0001F647
		public override void Initialize()
		{
			base.Initialize();
			this.MapBindingFieldPairs = new RdlCollection<MapBindingFieldPair>();
			this.MapFieldDefinitions = new RdlCollection<MapFieldDefinition>();
			this.DataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002146C File Offset: 0x0001F66C
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.MapDataRegionName = nameChanges.GetNewName(NameChanges.EntryType.ReportItem, this.MapDataRegionName);
		}

		// Token: 0x020003B6 RID: 950
		internal new class Definition : DefinitionStore<MapVectorLayer, MapVectorLayer.Definition.Properties>
		{
			// Token: 0x0600185A RID: 6234 RVA: 0x0003B651 File Offset: 0x00039851
			private Definition()
			{
			}

			// Token: 0x020004CE RID: 1230
			internal enum Properties
			{
				// Token: 0x04000EE7 RID: 3815
				Name,
				// Token: 0x04000EE8 RID: 3816
				VisibilityMode,
				// Token: 0x04000EE9 RID: 3817
				MinimumZoom,
				// Token: 0x04000EEA RID: 3818
				MaximumZoom,
				// Token: 0x04000EEB RID: 3819
				Transparency,
				// Token: 0x04000EEC RID: 3820
				MapDataRegionName,
				// Token: 0x04000EED RID: 3821
				MapBindingFieldPairs,
				// Token: 0x04000EEE RID: 3822
				MapFieldDefinitions,
				// Token: 0x04000EEF RID: 3823
				MapSpatialData,
				// Token: 0x04000EF0 RID: 3824
				DataElementName,
				// Token: 0x04000EF1 RID: 3825
				DataElementOutput
			}
		}
	}
}
