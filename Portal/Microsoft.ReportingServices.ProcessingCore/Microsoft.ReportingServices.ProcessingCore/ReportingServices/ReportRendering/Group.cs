using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000030 RID: 48
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Group
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		internal Group(CustomReportItem owner, Grouping groupingDef)
		{
			this.m_ownerItem = owner;
			this.m_groupingDef = groupingDef;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000E2EE File Offset: 0x0000C4EE
		internal Group(DataRegion owner, Grouping groupingDef, Visibility visibilityDef)
		{
			this.m_ownerItem = owner;
			this.m_groupingDef = groupingDef;
			this.m_visibilityDef = visibilityDef;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000E30B File Offset: 0x0000C50B
		public string Name
		{
			get
			{
				if (this.m_groupingDef == null)
				{
					return null;
				}
				return this.m_groupingDef.Name;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060004A7 RID: 1191
		public abstract string ID { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000E322 File Offset: 0x0000C522
		public string UniqueName
		{
			get
			{
				if (this.m_uniqueName == 0)
				{
					return null;
				}
				return this.m_uniqueName.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060004A9 RID: 1193
		public abstract string Label { get; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000E33E File Offset: 0x0000C53E
		public virtual bool PageBreakAtEnd
		{
			get
			{
				return this.m_groupingDef != null && this.m_groupingDef.PageBreakAtEnd;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000E355 File Offset: 0x0000C555
		public virtual bool PageBreakAtStart
		{
			get
			{
				return this.m_groupingDef != null && this.m_groupingDef.PageBreakAtStart;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000E36C File Offset: 0x0000C56C
		public string Custom
		{
			get
			{
				if (this.m_groupingDef != null)
				{
					string text = this.m_groupingDef.Custom;
					if (text == null && this.CustomProperties != null)
					{
						CustomProperty customProperty = this.CustomProperties["Custom"];
						if (customProperty != null && customProperty.Value != null)
						{
							text = DataTypeUtility.ConvertToInvariantString(customProperty.Value);
						}
					}
					return text;
				}
				return null;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060004AD RID: 1197
		public abstract CustomPropertyCollection CustomProperties { get; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060004AE RID: 1198
		public abstract bool Hidden { get; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000E3C3 File Offset: 0x0000C5C3
		public virtual bool HasToggle
		{
			get
			{
				return Visibility.HasToggle(this.m_visibilityDef);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		public virtual string ToggleItem
		{
			get
			{
				if (this.m_visibilityDef == null)
				{
					return null;
				}
				return this.m_visibilityDef.Toggle;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000E3E7 File Offset: 0x0000C5E7
		internal virtual TextBox ToggleParent
		{
			get
			{
				if (!this.HasToggle)
				{
					return null;
				}
				Global.Tracer.Assert(this.OwnerDataRegion != null);
				return this.OwnerDataRegion.RenderingContext.GetToggleParent(this.m_uniqueName);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000E41C File Offset: 0x0000C61C
		public virtual SharedHiddenState SharedHidden
		{
			get
			{
				return Visibility.GetSharedHidden(this.m_visibilityDef);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000E429 File Offset: 0x0000C629
		public virtual bool IsToggleChild
		{
			get
			{
				Global.Tracer.Assert(this.OwnerDataRegion != null);
				return this.OwnerDataRegion.RenderingContext.IsToggleChild(this.m_uniqueName);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000E454 File Offset: 0x0000C654
		public virtual string DataElementName
		{
			get
			{
				if (this.m_groupingDef == null)
				{
					return null;
				}
				return this.m_groupingDef.DataElementName;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000E46B File Offset: 0x0000C66B
		public virtual string DataCollectionName
		{
			get
			{
				if (this.m_groupingDef == null)
				{
					return null;
				}
				return this.m_groupingDef.DataCollectionName;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000E482 File Offset: 0x0000C682
		public virtual DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_groupingDef == null)
				{
					return DataElementOutputTypes.Output;
				}
				return this.m_groupingDef.DataElementOutput;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000E499 File Offset: 0x0000C699
		internal DataRegion OwnerDataRegion
		{
			get
			{
				return this.m_ownerItem as DataRegion;
			}
		}

		// Token: 0x040000E9 RID: 233
		protected ReportItem m_ownerItem;

		// Token: 0x040000EA RID: 234
		internal Grouping m_groupingDef;

		// Token: 0x040000EB RID: 235
		internal Visibility m_visibilityDef;

		// Token: 0x040000EC RID: 236
		protected int m_uniqueName;

		// Token: 0x040000ED RID: 237
		protected CustomPropertyCollection m_customProperties;
	}
}
