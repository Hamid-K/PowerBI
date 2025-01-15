using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000085 RID: 133
	public class ChartMember : HierarchyMember, IHierarchyMember
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00018349 File Offset: 0x00016549
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0001835C File Offset: 0x0001655C
		public override Group Group
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0001836B File Offset: 0x0001656B
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0001837E File Offset: 0x0001657E
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> SortExpressions
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001838D File Offset: 0x0001658D
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000183A0 File Offset: 0x000165A0
		[XmlElement(typeof(RdlCollection<ChartMember>))]
		public IList<ChartMember> ChartMembers
		{
			get
			{
				return (IList<ChartMember>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000183AF File Offset: 0x000165AF
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x000183BD File Offset: 0x000165BD
		public ReportExpression Label
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000183D1 File Offset: 0x000165D1
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x000183E4 File Offset: 0x000165E4
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000183F3 File Offset: 0x000165F3
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00018406 File Offset: 0x00016606
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00018415 File Offset: 0x00016615
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00018423 File Offset: 0x00016623
		[DefaultValue(DataElementOutputTypes.Auto)]
		[ValidEnumValues("ChartMemberDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(7);
			}
			set
			{
				((EnumProperty)DefinitionStore<ChartMember, ChartMember.Definition.Properties>.GetProperty(7)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(7, (int)value);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00018444 File Offset: 0x00016644
		public ChartMember()
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001844C File Offset: 0x0001664C
		internal ChartMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00018455 File Offset: 0x00016655
		public override void Initialize()
		{
			base.Initialize();
			this.SortExpressions = new RdlCollection<SortExpression>();
			this.ChartMembers = new RdlCollection<ChartMember>();
			this.CustomProperties = new RdlCollection<CustomProperty>();
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001847E File Offset: 0x0001667E
		IEnumerable<IHierarchyMember> IHierarchyMember.Members
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

		// Token: 0x02000339 RID: 825
		internal class Definition : DefinitionStore<ChartMember, ChartMember.Definition.Properties>
		{
			// Token: 0x060017AC RID: 6060 RVA: 0x0003A823 File Offset: 0x00038A23
			private Definition()
			{
			}

			// Token: 0x0200045A RID: 1114
			internal enum Properties
			{
				// Token: 0x04000951 RID: 2385
				Group,
				// Token: 0x04000952 RID: 2386
				SortExpressions,
				// Token: 0x04000953 RID: 2387
				ChartMembers,
				// Token: 0x04000954 RID: 2388
				Label,
				// Token: 0x04000955 RID: 2389
				LabelLocID,
				// Token: 0x04000956 RID: 2390
				CustomProperties,
				// Token: 0x04000957 RID: 2391
				DataElementName,
				// Token: 0x04000958 RID: 2392
				DataElementOutput,
				// Token: 0x04000959 RID: 2393
				PropertyCount
			}
		}
	}
}
