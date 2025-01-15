using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200001E RID: 30
	internal class DataGroupings2005 : DataHierarchy
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002D7E File Offset: 0x00000F7E
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002D86 File Offset: 0x00000F86
		[XmlElement(typeof(RdlCollection<DataMember>))]
		[XmlArrayItem("DataGrouping", typeof(DataGrouping2005))]
		public IList<DataMember> DataGroupings
		{
			get
			{
				return base.DataMembers;
			}
			set
			{
				base.DataMembers = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002D8F File Offset: 0x00000F8F
		public DataGroupings2005()
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002D97 File Offset: 0x00000F97
		internal DataGroupings2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
