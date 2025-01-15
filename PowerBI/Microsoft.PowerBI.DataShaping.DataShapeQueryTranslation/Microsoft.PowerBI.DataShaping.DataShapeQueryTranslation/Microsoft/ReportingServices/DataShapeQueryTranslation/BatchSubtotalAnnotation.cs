using System;
using System.Globalization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Utils;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000041 RID: 65
	internal sealed class BatchSubtotalAnnotation
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00007B14 File Offset: 0x00005D14
		internal BatchSubtotalAnnotation(IScope startScope, IScope stopScope, string subtotalIndicatorColumnName, SortDirection sortDirection, SubtotalUsage usage)
		{
			this.m_startScope = startScope;
			this.m_stopScope = stopScope;
			this.m_subtotalIndicatorColumnName = subtotalIndicatorColumnName;
			this.m_sortDirection = sortDirection;
			this.m_usage = usage;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007B44 File Offset: 0x00005D44
		internal void Validate(BatchSubtotalAnnotation other)
		{
			if (this.m_startScope != other.m_startScope || this.m_stopScope != other.m_stopScope || this.m_sortDirection != other.m_sortDirection || this.m_subtotalIndicatorColumnName != other.m_subtotalIndicatorColumnName || this.m_usage != other.m_usage)
			{
				this.m_errorString = string.Format(CultureInfo.InvariantCulture, "Incompatible subtotal requirements detected!\r\n{0}\r\n{1}", this.ToString(), other.ToString());
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007BBD File Offset: 0x00005DBD
		internal bool IsValid
		{
			get
			{
				return this.m_errorString == null;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00007BC8 File Offset: 0x00005DC8
		internal string ErrorMessage
		{
			get
			{
				return this.m_errorString;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007BD0 File Offset: 0x00005DD0
		internal IScope StartScope
		{
			get
			{
				return this.m_startScope;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00007BD8 File Offset: 0x00005DD8
		internal IScope StopScope
		{
			get
			{
				return this.m_stopScope;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00007BE0 File Offset: 0x00005DE0
		internal string SubtotalIndicatorColumnName
		{
			get
			{
				return this.m_subtotalIndicatorColumnName;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00007BE8 File Offset: 0x00005DE8
		internal SortDirection SortDirection
		{
			get
			{
				return this.m_sortDirection;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00007BF0 File Offset: 0x00005DF0
		internal SubtotalUsage Usage
		{
			get
			{
				return this.m_usage;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "From: {0}, To: {1}, Sort: {2}, SubtotalIndicatorName: {3}, Usage: {4}", new object[]
			{
				this.m_startScope.Id.Value,
				this.m_stopScope.Id.Value,
				this.m_sortDirection.ToString(),
				this.m_subtotalIndicatorColumnName.MarkAsModelInfo(),
				this.m_usage
			});
		}

		// Token: 0x040000B7 RID: 183
		private readonly IScope m_startScope;

		// Token: 0x040000B8 RID: 184
		private readonly IScope m_stopScope;

		// Token: 0x040000B9 RID: 185
		private readonly string m_subtotalIndicatorColumnName;

		// Token: 0x040000BA RID: 186
		private readonly SortDirection m_sortDirection;

		// Token: 0x040000BB RID: 187
		private readonly SubtotalUsage m_usage;

		// Token: 0x040000BC RID: 188
		private string m_errorString;
	}
}
