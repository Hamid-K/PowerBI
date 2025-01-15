using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000086 RID: 134
	public class ChartCategoryHierarchy : ReportObject, IHierarchy
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001848E File Offset: 0x0001668E
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000184A1 File Offset: 0x000166A1
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

		// Token: 0x060004CB RID: 1227 RVA: 0x000184B0 File Offset: 0x000166B0
		public ChartCategoryHierarchy()
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000184B8 File Offset: 0x000166B8
		internal ChartCategoryHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000184C1 File Offset: 0x000166C1
		public override void Initialize()
		{
			base.Initialize();
			this.ChartMembers = new RdlCollection<ChartMember>();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000184D4 File Offset: 0x000166D4
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

		// Token: 0x0200033B RID: 827
		internal class Definition : DefinitionStore<ChartCategoryHierarchy, ChartCategoryHierarchy.Definition.Properties>
		{
			// Token: 0x060017B6 RID: 6070 RVA: 0x0003A9A3 File Offset: 0x00038BA3
			private Definition()
			{
			}

			// Token: 0x0200045B RID: 1115
			internal enum Properties
			{
				// Token: 0x0400095B RID: 2395
				ChartMembers,
				// Token: 0x0400095C RID: 2396
				PropertyCount
			}
		}
	}
}
