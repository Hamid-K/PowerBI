using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000254 RID: 596
	[CannotApplyEqualityOperator]
	[Serializable]
	public class ErrorCorrelationId : IEquatable<ErrorCorrelationId>
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00034AB7 File Offset: 0x00032CB7
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00034ABF File Offset: 0x00032CBF
		public long CorrelationId
		{
			get
			{
				return this.m_correlationId;
			}
			private set
			{
				this.m_correlationId = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00034AC8 File Offset: 0x00032CC8
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00034AD0 File Offset: 0x00032CD0
		public int SequenceNumber
		{
			get
			{
				return this.m_sequenceNumber;
			}
			private set
			{
				this.m_sequenceNumber = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00034AD9 File Offset: 0x00032CD9
		public bool IsError
		{
			get
			{
				return this.CorrelationId != 0L;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00034AE5 File Offset: 0x00032CE5
		public ErrorCorrelationId(long correlationId, int sequenceNumber)
		{
			this.CorrelationId = correlationId;
			this.SequenceNumber = sequenceNumber;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00034AFB File Offset: 0x00032CFB
		public static ErrorCorrelationId NoError()
		{
			return new ErrorCorrelationId(0L, 0);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00034B05 File Offset: 0x00032D05
		public static ErrorCorrelationId NewCorrelatedError([NotNull] ErrorCorrelationId parent)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ErrorCorrelationId>(parent, "parent");
			if (!parent.IsError)
			{
				return ErrorCorrelationId.NoError();
			}
			return new ErrorCorrelationId(parent.CorrelationId, parent.SequenceNumber + 1);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00034B33 File Offset: 0x00032D33
		public static ErrorCorrelationId NewRootError()
		{
			return new ErrorCorrelationId(Randomizer.GetI64(1L, long.MaxValue), 0);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00034B4B File Offset: 0x00032D4B
		public bool RootCauseOf(ErrorCorrelationId otherError)
		{
			return this.IsRootCauseError && this.CauseOf(otherError);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00034B5E File Offset: 0x00032D5E
		public bool CauseOf(ErrorCorrelationId otherError)
		{
			return this.IsError && otherError != null && otherError.CorrelationId == this.CorrelationId && otherError.SequenceNumber >= this.SequenceNumber;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00034B8C File Offset: 0x00032D8C
		public bool IsRootCauseError
		{
			get
			{
				return this.SequenceNumber == 0 && this.IsError;
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00034B9E File Offset: 0x00032D9E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ErrorCorrelationId);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00034BAC File Offset: 0x00032DAC
		public override int GetHashCode()
		{
			return this.SequenceNumber ^ this.CorrelationId.GetHashCode();
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00034BCE File Offset: 0x00032DCE
		public bool Equals(ErrorCorrelationId other)
		{
			return other != null && this.SequenceNumber == other.SequenceNumber && this.CorrelationId == other.CorrelationId;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00034BF1 File Offset: 0x00032DF1
		public override string ToString()
		{
			return this.CorrelationId + "." + this.SequenceNumber;
		}

		// Token: 0x040005DB RID: 1499
		private long m_correlationId;

		// Token: 0x040005DC RID: 1500
		private int m_sequenceNumber;
	}
}
