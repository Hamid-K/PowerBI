using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001FD RID: 509
	public class TablixMember : HierarchyMember, IHierarchyMember
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x000279D1 File Offset: 0x00025BD1
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x000279E4 File Offset: 0x00025BE4
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

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000279F3 File Offset: 0x00025BF3
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x00027A06 File Offset: 0x00025C06
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

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00027A15 File Offset: 0x00025C15
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00027A28 File Offset: 0x00025C28
		public TablixHeader TablixHeader
		{
			get
			{
				return (TablixHeader)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00027A37 File Offset: 0x00025C37
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00027A4A File Offset: 0x00025C4A
		[XmlElement(typeof(RdlCollection<TablixMember>))]
		public IList<TablixMember> TablixMembers
		{
			get
			{
				return (IList<TablixMember>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00027A59 File Offset: 0x00025C59
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00027A6C File Offset: 0x00025C6C
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00027A7B File Offset: 0x00025C7B
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00027A89 File Offset: 0x00025C89
		[DefaultValue(false)]
		public bool FixedData
		{
			get
			{
				return base.PropertyStore.GetBoolean(5);
			}
			set
			{
				base.PropertyStore.SetBoolean(5, value);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00027A98 File Offset: 0x00025C98
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00027AAB File Offset: 0x00025CAB
		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00027ABA File Offset: 0x00025CBA
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00027AC8 File Offset: 0x00025CC8
		[DefaultValue(false)]
		public bool HideIfNoRows
		{
			get
			{
				return base.PropertyStore.GetBoolean(7);
			}
			set
			{
				base.PropertyStore.SetBoolean(7, value);
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00027AD7 File Offset: 0x00025CD7
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00027AE5 File Offset: 0x00025CE5
		[DefaultValue(KeepWithGroupTypes.None)]
		public KeepWithGroupTypes KeepWithGroup
		{
			get
			{
				return (KeepWithGroupTypes)base.PropertyStore.GetInteger(8);
			}
			set
			{
				base.PropertyStore.SetInteger(8, (int)value);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00027AF4 File Offset: 0x00025CF4
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00027B03 File Offset: 0x00025D03
		[DefaultValue(false)]
		public bool RepeatOnNewPage
		{
			get
			{
				return base.PropertyStore.GetBoolean(9);
			}
			set
			{
				base.PropertyStore.SetBoolean(9, value);
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00027B13 File Offset: 0x00025D13
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x00027B27 File Offset: 0x00025D27
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00027B37 File Offset: 0x00025D37
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00027B46 File Offset: 0x00025D46
		[DefaultValue(DataElementOutputTypes.Auto)]
		[ValidEnumValues("TablixMemberDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(11);
			}
			set
			{
				((EnumProperty)DefinitionStore<TablixMember, TablixMember.Definition.Properties>.GetProperty(11)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(11, (int)value);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00027B69 File Offset: 0x00025D69
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x00027B78 File Offset: 0x00025D78
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return base.PropertyStore.GetBoolean(12);
			}
			set
			{
				base.PropertyStore.SetBoolean(12, value);
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00027B88 File Offset: 0x00025D88
		public TablixMember()
		{
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00027B90 File Offset: 0x00025D90
		internal TablixMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00027B99 File Offset: 0x00025D99
		public override void Initialize()
		{
			base.Initialize();
			this.SortExpressions = new RdlCollection<SortExpression>();
			this.TablixMembers = new RdlCollection<TablixMember>();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.DataElementOutput = DataElementOutputTypes.Auto;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00027BC9 File Offset: 0x00025DC9
		IEnumerable<IHierarchyMember> IHierarchyMember.Members
		{
			get
			{
				foreach (IHierarchyMember hierarchyMember in this.TablixMembers)
				{
					yield return hierarchyMember;
				}
				IEnumerator<TablixMember> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x02000402 RID: 1026
		internal class Definition : DefinitionStore<TablixMember, TablixMember.Definition.Properties>
		{
			// Token: 0x060018D3 RID: 6355 RVA: 0x0003BDDB File Offset: 0x00039FDB
			private Definition()
			{
			}

			// Token: 0x02000512 RID: 1298
			internal enum Properties
			{
				// Token: 0x0400110B RID: 4363
				Group,
				// Token: 0x0400110C RID: 4364
				SortExpressions,
				// Token: 0x0400110D RID: 4365
				TablixHeader,
				// Token: 0x0400110E RID: 4366
				TablixMembers,
				// Token: 0x0400110F RID: 4367
				CustomProperties,
				// Token: 0x04001110 RID: 4368
				FixedData,
				// Token: 0x04001111 RID: 4369
				Visibility,
				// Token: 0x04001112 RID: 4370
				HideIfNoRows,
				// Token: 0x04001113 RID: 4371
				KeepWithGroup,
				// Token: 0x04001114 RID: 4372
				RepeatOnNewPage,
				// Token: 0x04001115 RID: 4373
				DataElementName,
				// Token: 0x04001116 RID: 4374
				DataElementOutput,
				// Token: 0x04001117 RID: 4375
				KeepTogether
			}
		}
	}
}
