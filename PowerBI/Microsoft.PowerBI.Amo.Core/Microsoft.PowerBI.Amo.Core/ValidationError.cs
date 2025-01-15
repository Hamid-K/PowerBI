using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000CB RID: 203
	[Guid("6A4FEEEC-977B-4f3a-9694-E65969EAD568")]
	public sealed class ValidationError
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00029B56 File Offset: 0x00027D56
		public ValidationError(IModelComponent source, string error)
		{
			this.Source = source;
			this.ErrorText = error;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00029B6C File Offset: 0x00027D6C
		public ValidationError(IModelComponent source, string error, ErrorPriority priority)
		{
			this.Source = source;
			this.ErrorText = error;
			this.Priority = priority;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00029B89 File Offset: 0x00027D89
		public ValidationError(IModelComponent source, string error, int errorCode)
		{
			this.Source = source;
			this.ErrorText = error;
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00029BA6 File Offset: 0x00027DA6
		public ValidationError(IModelComponent source, string error, ErrorPriority priority, int errorCode)
		{
			this.Source = source;
			this.ErrorText = error;
			this.Priority = priority;
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00029BCB File Offset: 0x00027DCB
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00029BD3 File Offset: 0x00027DD3
		public IModelComponent Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00029BDC File Offset: 0x00027DDC
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00029BE4 File Offset: 0x00027DE4
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00029BED File Offset: 0x00027DED
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00029BF5 File Offset: 0x00027DF5
		public ErrorPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00029BFE File Offset: 0x00027DFE
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00029C06 File Offset: 0x00027E06
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00029C0F File Offset: 0x00027E0F
		public string FullErrorText
		{
			get
			{
				if (this.Source != null)
				{
					return ValidationSR.FullErrorText(this.Source.FriendlyPath, this.ErrorText);
				}
				return this.ErrorText;
			}
		}

		// Token: 0x04000703 RID: 1795
		private IModelComponent source;

		// Token: 0x04000704 RID: 1796
		private string errorText;

		// Token: 0x04000705 RID: 1797
		private ErrorPriority priority;

		// Token: 0x04000706 RID: 1798
		private int errorCode;
	}
}
