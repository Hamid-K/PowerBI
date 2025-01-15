using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
	// Token: 0x020002D0 RID: 720
	public class DataSet : DataSetBase
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00033214 File Offset: 0x00031414
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x00033227 File Offset: 0x00031427
		public Query Query
		{
			get
			{
				return (Query)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00033236 File Offset: 0x00031436
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x00033249 File Offset: 0x00031449
		[XmlElement(typeof(RdlCollection<Field>))]
		[XmlArrayItem("Field", typeof(Field), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
		public IList<Field> Fields
		{
			get
			{
				return (IList<Field>)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00033258 File Offset: 0x00031458
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x0003326C File Offset: 0x0003146C
		[XmlElement(typeof(RdlCollection<Filter>))]
		[XmlArrayItem("Filter", typeof(Filter), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0003327C File Offset: 0x0003147C
		public DataSet()
		{
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00033284 File Offset: 0x00031484
		internal DataSet(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00033290 File Offset: 0x00031490
		public bool Equals(DataSet dataSet)
		{
			return dataSet != null && (this.Query != null || dataSet.Query == null) && (this.Query == null || dataSet.Query != null) && this.Query.Equals(dataSet.Query) && this.FieldsAreEqual(this.Fields, dataSet.Fields) && this.FiltersAreEqual(this.Filters, dataSet.Filters) && base.Equals(dataSet);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0003330F File Offset: 0x0003150F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataSet);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0003331D File Offset: 0x0003151D
		public override int GetHashCode()
		{
			if (this.Query != null)
			{
				return this.Query.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0003333C File Offset: 0x0003153C
		private bool FieldsAreEqual(IList<Field> FirstList, IList<Field> SecondList)
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

		// Token: 0x06001632 RID: 5682 RVA: 0x00033384 File Offset: 0x00031584
		private bool FiltersAreEqual(IList<Filter> FirstList, IList<Filter> SecondList)
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

		// Token: 0x06001633 RID: 5683 RVA: 0x000333CA File Offset: 0x000315CA
		public override QueryBase GetQuery()
		{
			return this.Query;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000333D2 File Offset: 0x000315D2
		public override void Initialize()
		{
			base.Initialize();
			this.Query = new Query();
			this.Fields = new RdlCollection<Field>();
			this.Filters = new RdlCollection<Filter>();
		}

		// Token: 0x02000412 RID: 1042
		internal new class Definition : DefinitionStore<DataSet, DataSet.Definition.Properties>
		{
			// Token: 0x0200051E RID: 1310
			internal enum Properties
			{
				// Token: 0x0400114C RID: 4428
				Name,
				// Token: 0x0400114D RID: 4429
				CaseSensitivity,
				// Token: 0x0400114E RID: 4430
				Collation,
				// Token: 0x0400114F RID: 4431
				AccentSensitivity,
				// Token: 0x04001150 RID: 4432
				KanatypeSensitivity,
				// Token: 0x04001151 RID: 4433
				WidthSensitivity,
				// Token: 0x04001152 RID: 4434
				InterpretSubtotalsAsDetails,
				// Token: 0x04001153 RID: 4435
				Query,
				// Token: 0x04001154 RID: 4436
				Fields,
				// Token: 0x04001155 RID: 4437
				Filters
			}
		}
	}
}
