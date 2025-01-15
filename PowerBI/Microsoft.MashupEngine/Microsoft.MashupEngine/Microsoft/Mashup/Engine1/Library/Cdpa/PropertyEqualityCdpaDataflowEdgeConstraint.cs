using System;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DA4 RID: 3492
	internal class PropertyEqualityCdpaDataflowEdgeConstraint : CdpaDataflowEdgeConstraint
	{
		// Token: 0x06005F16 RID: 24342 RVA: 0x00148063 File Offset: 0x00146263
		public PropertyEqualityCdpaDataflowEdgeConstraint(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x00148072 File Offset: 0x00146272
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x0014807A File Offset: 0x0014627A
		public override bool Equals(object other)
		{
			return this.Equals(other as PropertyEqualityCdpaDataflowEdgeConstraint);
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x0014807A File Offset: 0x0014627A
		public override bool Equals(CdpaDataflowEdgeConstraint other)
		{
			return this.Equals(other as PropertyEqualityCdpaDataflowEdgeConstraint);
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x00148088 File Offset: 0x00146288
		public bool Equals(PropertyEqualityCdpaDataflowEdgeConstraint other)
		{
			return other != null && this.propertyName == other.propertyName;
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x001480A0 File Offset: 0x001462A0
		public override int GetHashCode()
		{
			return this.propertyName.GetHashCode();
		}

		// Token: 0x0400342B RID: 13355
		private readonly string propertyName;
	}
}
