using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	internal abstract class TSqlFragment
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00005F66 File Offset: 0x00004166
		internal void UpdateTokenInfo(TSqlFragment fragment)
		{
			if (fragment == null)
			{
				return;
			}
			this.UpdateTokenInfo(fragment.FirstTokenIndex, fragment.LastTokenIndex);
			if (fragment.ScriptTokenStream != null)
			{
				this.ScriptTokenStream = fragment.ScriptTokenStream;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005F94 File Offset: 0x00004194
		internal void UpdateTokenInfo(int firstIndex, int lastIndex)
		{
			if (firstIndex < 0 || lastIndex < 0)
			{
				return;
			}
			if (firstIndex > lastIndex)
			{
				int num = firstIndex;
				firstIndex = lastIndex;
				lastIndex = num;
			}
			if (firstIndex < this._firstTokenIndex || this._firstTokenIndex == -1)
			{
				this._firstTokenIndex = firstIndex;
			}
			if (lastIndex > this._lastTokenIndex || this._lastTokenIndex == -1)
			{
				this._lastTokenIndex = lastIndex;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005FE8 File Offset: 0x000041E8
		public virtual void Accept(TSqlFragmentVisitor visitor)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005FEA File Offset: 0x000041EA
		public virtual void AcceptChildren(TSqlFragmentVisitor visitor)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005FEC File Offset: 0x000041EC
		public int StartOffset
		{
			get
			{
				if (this._firstTokenIndex == -1 || this._scriptTokenStream == null)
				{
					return -1;
				}
				return this._scriptTokenStream[this._firstTokenIndex].Offset;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006018 File Offset: 0x00004218
		public int FragmentLength
		{
			get
			{
				if (this._firstTokenIndex == -1 || this._lastTokenIndex == -1 || this._scriptTokenStream == null)
				{
					return -1;
				}
				TSqlParserToken tsqlParserToken = this._scriptTokenStream[this._lastTokenIndex];
				int num = ((tsqlParserToken.Text == null) ? 0 : tsqlParserToken.Text.Length);
				return tsqlParserToken.Offset - this._scriptTokenStream[this._firstTokenIndex].Offset + num;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006089 File Offset: 0x00004289
		public int StartLine
		{
			get
			{
				if (this._firstTokenIndex == -1 || this._scriptTokenStream == null)
				{
					return -1;
				}
				return this._scriptTokenStream[this._firstTokenIndex].Line;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000060B4 File Offset: 0x000042B4
		public int StartColumn
		{
			get
			{
				if (this._firstTokenIndex == -1 || this._scriptTokenStream == null)
				{
					return -1;
				}
				return this._scriptTokenStream[this._firstTokenIndex].Column;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000060DF File Offset: 0x000042DF
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000060E7 File Offset: 0x000042E7
		public int FirstTokenIndex
		{
			get
			{
				return this._firstTokenIndex;
			}
			set
			{
				this._firstTokenIndex = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000060F0 File Offset: 0x000042F0
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000060F8 File Offset: 0x000042F8
		public int LastTokenIndex
		{
			get
			{
				return this._lastTokenIndex;
			}
			set
			{
				this._lastTokenIndex = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006101 File Offset: 0x00004301
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006109 File Offset: 0x00004309
		public IList<TSqlParserToken> ScriptTokenStream
		{
			get
			{
				return this._scriptTokenStream;
			}
			set
			{
				this._scriptTokenStream = value;
			}
		}

		// Token: 0x0400014A RID: 330
		public const int Uninitialized = -1;

		// Token: 0x0400014B RID: 331
		private int _firstTokenIndex = -1;

		// Token: 0x0400014C RID: 332
		private int _lastTokenIndex = -1;

		// Token: 0x0400014D RID: 333
		private IList<TSqlParserToken> _scriptTokenStream;
	}
}
