using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
	// Token: 0x020002D1 RID: 721
	public class Query : QueryBase
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x000333FB File Offset: 0x000315FB
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x0003340E File Offset: 0x0003160E
		public string DataSourceReference
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0003341D File Offset: 0x0003161D
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x00033430 File Offset: 0x00031630
		[XmlElement(typeof(RdlCollection<DataSetParameter>))]
		[XmlArrayItem("DataSetParameter", typeof(DataSetParameter), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
		public IList<DataSetParameter> DataSetParameters
		{
			get
			{
				return (IList<DataSetParameter>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0003343F File Offset: 0x0003163F
		public Query()
		{
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00033447 File Offset: 0x00031647
		internal Query(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00033450 File Offset: 0x00031650
		public override void Initialize()
		{
			base.Initialize();
			this.DataSourceReference = "";
			this.DataSetParameters = new RdlCollection<DataSetParameter>();
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0003346E File Offset: 0x0003166E
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			string.IsNullOrEmpty(this.DataSourceReference);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00033483 File Offset: 0x00031683
		public bool Equals(Query query)
		{
			return query != null && (this.DataSetParametersAreEqual(this.DataSetParameters, query.DataSetParameters) && this.DataSourceReference == query.DataSourceReference) && base.Equals(query);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000334BC File Offset: 0x000316BC
		private bool DataSetParametersAreEqual(IList<DataSetParameter> FirstList, IList<DataSetParameter> SecondList)
		{
			if (FirstList.Count != SecondList.Count)
			{
				return false;
			}
			for (int i = 0; i < FirstList.Count; i++)
			{
				if (!FirstList[i].Equals(SecondList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00033502 File Offset: 0x00031702
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Query);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00033510 File Offset: 0x00031710
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x02000413 RID: 1043
		internal new class Definition : DefinitionStore<Query, Query.Definition.Properties>
		{
			// Token: 0x0200051F RID: 1311
			internal enum Properties
			{
				// Token: 0x04001157 RID: 4439
				CommandType,
				// Token: 0x04001158 RID: 4440
				CommandText,
				// Token: 0x04001159 RID: 4441
				Timeout,
				// Token: 0x0400115A RID: 4442
				DataSourceReference,
				// Token: 0x0400115B RID: 4443
				DataSetParameters
			}
		}
	}
}
