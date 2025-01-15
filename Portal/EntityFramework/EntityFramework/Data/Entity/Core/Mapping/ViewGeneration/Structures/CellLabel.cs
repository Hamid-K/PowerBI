using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200059B RID: 1435
	internal class CellLabel
	{
		// Token: 0x06004569 RID: 17769 RVA: 0x000F4EE1 File Offset: 0x000F30E1
		internal CellLabel(CellLabel source)
		{
			this.m_startLineNumber = source.m_startLineNumber;
			this.m_startLinePosition = source.m_startLinePosition;
			this.m_sourceLocation = source.m_sourceLocation;
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x000F4F0D File Offset: 0x000F310D
		internal CellLabel(MappingFragment fragmentInfo)
			: this(fragmentInfo.StartLineNumber, fragmentInfo.StartLinePosition, fragmentInfo.SourceLocation)
		{
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x000F4F27 File Offset: 0x000F3127
		internal CellLabel(int startLineNumber, int startLinePosition, string sourceLocation)
		{
			this.m_startLineNumber = startLineNumber;
			this.m_startLinePosition = startLinePosition;
			this.m_sourceLocation = sourceLocation;
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000F4F44 File Offset: 0x000F3144
		internal int StartLineNumber
		{
			get
			{
				return this.m_startLineNumber;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x000F4F4C File Offset: 0x000F314C
		internal int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x000F4F54 File Offset: 0x000F3154
		internal string SourceLocation
		{
			get
			{
				return this.m_sourceLocation;
			}
		}

		// Token: 0x040018E7 RID: 6375
		private readonly int m_startLineNumber;

		// Token: 0x040018E8 RID: 6376
		private readonly int m_startLinePosition;

		// Token: 0x040018E9 RID: 6377
		private readonly string m_sourceLocation;
	}
}
