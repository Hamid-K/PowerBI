using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200001E RID: 30
	[DebuggerDisplay("PropagateCompletion = {PropagateCompletion}, MaxMessages = {MaxMessages}, Append = {Append}")]
	public class DataflowLinkOptions
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000383C File Offset: 0x00001A3C
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003844 File Offset: 0x00001A44
		public bool PropagateCompletion
		{
			get
			{
				return this._propagateCompletion;
			}
			set
			{
				this._propagateCompletion = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000384D File Offset: 0x00001A4D
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003855 File Offset: 0x00001A55
		public int MaxMessages
		{
			get
			{
				return this._maxNumberOfMessages;
			}
			set
			{
				if (value < 1 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._maxNumberOfMessages = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003871 File Offset: 0x00001A71
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003879 File Offset: 0x00001A79
		public bool Append
		{
			get
			{
				return this._append;
			}
			set
			{
				this._append = value;
			}
		}

		// Token: 0x04000028 RID: 40
		internal const int Unbounded = -1;

		// Token: 0x04000029 RID: 41
		private bool _propagateCompletion;

		// Token: 0x0400002A RID: 42
		private int _maxNumberOfMessages = -1;

		// Token: 0x0400002B RID: 43
		private bool _append = true;

		// Token: 0x0400002C RID: 44
		internal static readonly DataflowLinkOptions Default = new DataflowLinkOptions();

		// Token: 0x0400002D RID: 45
		internal static readonly DataflowLinkOptions UnlinkAfterOneAndPropagateCompletion = new DataflowLinkOptions
		{
			MaxMessages = 1,
			PropagateCompletion = true
		};
	}
}
