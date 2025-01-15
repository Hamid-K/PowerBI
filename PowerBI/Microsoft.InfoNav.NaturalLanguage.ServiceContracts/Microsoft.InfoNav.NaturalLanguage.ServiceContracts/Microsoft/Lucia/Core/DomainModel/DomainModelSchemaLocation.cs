using System;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000186 RID: 390
	public struct DomainModelSchemaLocation
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0000E580 File Offset: 0x0000C780
		public static DomainModelSchemaLocation Empty
		{
			get
			{
				return default(DomainModelSchemaLocation);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000E596 File Offset: 0x0000C796
		public DomainModelSchemaLocation(int lineNumber, int linePosition)
		{
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		public readonly int LineNumber { get; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000E5AE File Offset: 0x0000C7AE
		public readonly int LinePosition { get; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0000E5B6 File Offset: 0x0000C7B6
		public bool IsEmpty
		{
			get
			{
				return this.LineNumber == 0 && this.LinePosition == 0;
			}
		}
	}
}
