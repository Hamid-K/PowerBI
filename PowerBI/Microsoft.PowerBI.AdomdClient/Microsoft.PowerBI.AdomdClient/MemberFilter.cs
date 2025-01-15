using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A8 RID: 168
	public class MemberFilter
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x00029694 File Offset: 0x00027894
		public MemberFilter(string propertyName, string propertyValue)
			: this(propertyName, MemberFilterType.Equals, propertyValue)
		{
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000296A0 File Offset: 0x000278A0
		public MemberFilter(string propertyName, MemberFilterType filterType, string propertyValue)
		{
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			if (propertyName.Length == 0)
			{
				throw new ArgumentException(null, "propertyName");
			}
			if (!MemberFilter.IsValidMemberFilterType(filterType))
			{
				throw new ArgumentException(null, "filterType");
			}
			if (propertyValue == null)
			{
				throw new ArgumentNullException("propertyValue");
			}
			this.propertyName = propertyName;
			this.propertyValue = propertyValue;
			this.filterType = filterType;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002970C File Offset: 0x0002790C
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00029714 File Offset: 0x00027914
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(null, "value");
				}
				this.propertyName = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0002973F File Offset: 0x0002793F
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00029747 File Offset: 0x00027947
		public string PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.propertyValue = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002975E File Offset: 0x0002795E
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x00029766 File Offset: 0x00027966
		public MemberFilterType FilterType
		{
			get
			{
				return this.filterType;
			}
			set
			{
				if (!MemberFilter.IsValidMemberFilterType(value))
				{
					throw new ArgumentException(null, "value");
				}
				this.filterType = value;
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00029783 File Offset: 0x00027983
		private static bool IsValidMemberFilterType(MemberFilterType type)
		{
			return type >= MemberFilterType.Equals && type <= MemberFilterType.Contains;
		}

		// Token: 0x0400065D RID: 1629
		private string propertyName;

		// Token: 0x0400065E RID: 1630
		private string propertyValue;

		// Token: 0x0400065F RID: 1631
		private MemberFilterType filterType;
	}
}
