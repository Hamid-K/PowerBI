using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000409 RID: 1033
	public class ExecutionOptions
	{
		// Token: 0x060030F3 RID: 12531 RVA: 0x0009C300 File Offset: 0x0009A500
		public ExecutionOptions(MergeOption mergeOption)
		{
			this.MergeOption = mergeOption;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0009C30F File Offset: 0x0009A50F
		public ExecutionOptions(MergeOption mergeOption, bool streaming)
		{
			this.MergeOption = mergeOption;
			this.UserSpecifiedStreaming = new bool?(streaming);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x0009C32A File Offset: 0x0009A52A
		internal ExecutionOptions(MergeOption mergeOption, bool? streaming)
		{
			this.MergeOption = mergeOption;
			this.UserSpecifiedStreaming = streaming;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x0009C340 File Offset: 0x0009A540
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x0009C348 File Offset: 0x0009A548
		public MergeOption MergeOption { get; private set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x0009C354 File Offset: 0x0009A554
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. This property no longer returns an accurate value.")]
		public bool Streaming
		{
			get
			{
				return this.UserSpecifiedStreaming ?? true;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x0009C37A File Offset: 0x0009A57A
		// (set) Token: 0x060030FA RID: 12538 RVA: 0x0009C382 File Offset: 0x0009A582
		internal bool? UserSpecifiedStreaming { get; private set; }

		// Token: 0x060030FB RID: 12539 RVA: 0x0009C38B File Offset: 0x0009A58B
		public static bool operator ==(ExecutionOptions left, ExecutionOptions right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0009C39F File Offset: 0x0009A59F
		public static bool operator !=(ExecutionOptions left, ExecutionOptions right)
		{
			return !(left == right);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x0009C3AC File Offset: 0x0009A5AC
		public override bool Equals(object obj)
		{
			ExecutionOptions executionOptions = obj as ExecutionOptions;
			if (executionOptions == null)
			{
				return false;
			}
			if (this.MergeOption == executionOptions.MergeOption)
			{
				bool? userSpecifiedStreaming = this.UserSpecifiedStreaming;
				bool? userSpecifiedStreaming2 = executionOptions.UserSpecifiedStreaming;
				return (userSpecifiedStreaming.GetValueOrDefault() == userSpecifiedStreaming2.GetValueOrDefault()) & (userSpecifiedStreaming != null == (userSpecifiedStreaming2 != null));
			}
			return false;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0009C404 File Offset: 0x0009A604
		public override int GetHashCode()
		{
			return this.MergeOption.GetHashCode() ^ this.UserSpecifiedStreaming.GetHashCode();
		}

		// Token: 0x04001028 RID: 4136
		internal static readonly ExecutionOptions Default = new ExecutionOptions(MergeOption.AppendOnly);
	}
}
