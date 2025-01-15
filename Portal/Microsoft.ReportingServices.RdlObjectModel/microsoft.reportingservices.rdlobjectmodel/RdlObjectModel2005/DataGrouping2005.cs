using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200001F RID: 31
	internal class DataGrouping2005 : DataMember, IUpgradeable
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002DA0 File Offset: 0x00000FA0
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002DAE File Offset: 0x00000FAE
		[DefaultValue(false)]
		public bool Static
		{
			get
			{
				return base.PropertyStore.GetBoolean(4);
			}
			set
			{
				base.PropertyStore.SetBoolean(4, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002DBD File Offset: 0x00000FBD
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002DCA File Offset: 0x00000FCA
		public Grouping2005 Grouping
		{
			get
			{
				return (Grouping2005)base.Group;
			}
			set
			{
				base.Group = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002DD3 File Offset: 0x00000FD3
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002DDB File Offset: 0x00000FDB
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> Sorting
		{
			get
			{
				return base.SortExpressions;
			}
			set
			{
				base.SortExpressions = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002DE4 File Offset: 0x00000FE4
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002DEC File Offset: 0x00000FEC
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

		// Token: 0x060000EC RID: 236 RVA: 0x00002DF5 File Offset: 0x00000FF5
		public DataGrouping2005()
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002DFD File Offset: 0x00000FFD
		public DataGrouping2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002E06 File Offset: 0x00001006
		public override void Initialize()
		{
			base.Initialize();
			base.CustomProperties = new RdlCollection<CustomProperty>();
			this.DataGroupings = new RdlCollection<DataMember>();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002E24 File Offset: 0x00001024
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeDataGrouping(this);
		}

		// Token: 0x020002FE RID: 766
		public new class Definition : DefinitionStore<DataGrouping2005, DataGrouping2005.Definition.Properties>
		{
			// Token: 0x060016FA RID: 5882 RVA: 0x0003646A File Offset: 0x0003466A
			private Definition()
			{
			}

			// Token: 0x02000432 RID: 1074
			public enum Properties
			{
				// Token: 0x04000858 RID: 2136
				Static = 4,
				// Token: 0x04000859 RID: 2137
				PropertyCount
			}
		}
	}
}
