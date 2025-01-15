using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A8 RID: 168
	public class MemberFilter
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x000299C4 File Offset: 0x00027BC4
		public MemberFilter(string propertyName, string propertyValue)
			: this(propertyName, MemberFilterType.Equals, propertyValue)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000299D0 File Offset: 0x00027BD0
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00029A3C File Offset: 0x00027C3C
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00029A44 File Offset: 0x00027C44
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00029A6F File Offset: 0x00027C6F
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00029A77 File Offset: 0x00027C77
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00029A8E File Offset: 0x00027C8E
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00029A96 File Offset: 0x00027C96
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

		// Token: 0x060009CD RID: 2509 RVA: 0x00029AB3 File Offset: 0x00027CB3
		private static bool IsValidMemberFilterType(MemberFilterType type)
		{
			return type >= MemberFilterType.Equals && type <= MemberFilterType.Contains;
		}

		// Token: 0x0400066A RID: 1642
		private string propertyName;

		// Token: 0x0400066B RID: 1643
		private string propertyValue;

		// Token: 0x0400066C RID: 1644
		private MemberFilterType filterType;
	}
}
