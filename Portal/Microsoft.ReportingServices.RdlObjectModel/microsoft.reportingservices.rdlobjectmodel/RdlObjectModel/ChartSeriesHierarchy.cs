using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000084 RID: 132
	public class ChartSeriesHierarchy : ReportObject, IHierarchy
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x000182F3 File Offset: 0x000164F3
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00018306 File Offset: 0x00016506
		[XmlElement(typeof(RdlCollection<ChartMember>))]
		public IList<ChartMember> ChartMembers
		{
			get
			{
				return (IList<ChartMember>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00018315 File Offset: 0x00016515
		public ChartSeriesHierarchy()
		{
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001831D File Offset: 0x0001651D
		internal ChartSeriesHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00018326 File Offset: 0x00016526
		public override void Initialize()
		{
			base.Initialize();
			this.ChartMembers = new RdlCollection<ChartMember>();
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00018339 File Offset: 0x00016539
		IEnumerable<IHierarchyMember> IHierarchy.Members
		{
			get
			{
				foreach (IHierarchyMember hierarchyMember in this.ChartMembers)
				{
					yield return hierarchyMember;
				}
				IEnumerator<ChartMember> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x02000337 RID: 823
		internal class Definition : DefinitionStore<ChartSeriesHierarchy, ChartSeriesHierarchy.Definition.Properties>
		{
			// Token: 0x060017A2 RID: 6050 RVA: 0x0003A6A4 File Offset: 0x000388A4
			private Definition()
			{
			}

			// Token: 0x02000459 RID: 1113
			internal enum Properties
			{
				// Token: 0x0400094E RID: 2382
				ChartMembers,
				// Token: 0x0400094F RID: 2383
				PropertyCount
			}
		}
	}
}
