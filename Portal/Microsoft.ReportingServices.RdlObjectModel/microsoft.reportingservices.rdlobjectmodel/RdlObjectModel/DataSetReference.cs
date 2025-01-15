using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001EB RID: 491
	public class DataSetReference : ReportObject
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0002664B File Offset: 0x0002484B
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x0002665E File Offset: 0x0002485E
		public string DataSetName
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

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0002666D File Offset: 0x0002486D
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x00026680 File Offset: 0x00024880
		public string ValueField
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

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0002668F File Offset: 0x0002488F
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x000266A2 File Offset: 0x000248A2
		[DefaultValue("")]
		public string LabelField
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000266B1 File Offset: 0x000248B1
		public DataSetReference()
		{
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x000266B9 File Offset: 0x000248B9
		internal DataSetReference(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000266C2 File Offset: 0x000248C2
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, this.DataSetName);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000266E0 File Offset: 0x000248E0
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

		// Token: 0x020003F6 RID: 1014
		internal class Definition : DefinitionStore<DataSetReference, DataSetReference.Definition.Properties>
		{
			// Token: 0x060018B8 RID: 6328 RVA: 0x0003BB7F File Offset: 0x00039D7F
			private Definition()
			{
			}

			// Token: 0x02000508 RID: 1288
			internal enum Properties
			{
				// Token: 0x040010CE RID: 4302
				DataSetName,
				// Token: 0x040010CF RID: 4303
				ValueField,
				// Token: 0x040010D0 RID: 4304
				LabelField
			}
		}
	}
}
