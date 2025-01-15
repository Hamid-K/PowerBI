using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001CB RID: 459
	internal class ContextManager
	{
		// Token: 0x06000BC1 RID: 3009 RVA: 0x00028B57 File Offset: 0x00026D57
		private static ContextManager.ContextRow LogicalGetContextRow()
		{
			return (CallContext.LogicalGetData(ContextManager.sm_contextSlot) as ContextManager.ContextRow) ?? ContextManager.ContextRow.Empty;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00028B71 File Offset: 0x00026D71
		private static void LogicalSetContextRow(ContextManager.ContextRow row)
		{
			CallContext.LogicalSetData(ContextManager.sm_contextSlot, row);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00028B80 File Offset: 0x00026D80
		internal static void Dump(TraceDump dumper, params int[] slotsToIgnore)
		{
			ContextManager.ContextRow contextRow = ContextManager.LogicalGetContextRow();
			if (!contextRow.IsEmpty())
			{
				contextRow.Dump(dumper, slotsToIgnore);
				return;
			}
			dumper.Add("<Current context is empty>");
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00028BAF File Offset: 0x00026DAF
		internal static IDisposable PushContext<T>(int key, T t)
		{
			ContextManager.ContextRow contextRow = new ContextManager.ContextRow(ContextManager.LogicalGetContextRow(), key, t);
			ContextManager.LogicalSetContextRow(contextRow);
			return contextRow;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00028BC8 File Offset: 0x00026DC8
		internal static T Pop<T>(int key)
		{
			return ContextManager.PopRow().GetContextMember<T>(key);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00028BD5 File Offset: 0x00026DD5
		internal static T GetContextMember<T>(int key)
		{
			return ContextManager.LogicalGetContextRow().GetContextMember<T>(key);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00028BE2 File Offset: 0x00026DE2
		internal static IEnumerable<T> GetContextMemberStack<T>(int key)
		{
			return ContextManager.LogicalGetContextRow().GetContextMemberStack<T>(key);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00028BEF File Offset: 0x00026DEF
		internal static void ClearStack()
		{
			ContextManager.LogicalSetContextRow(null);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00028BF7 File Offset: 0x00026DF7
		internal static IContextStack CaptureStack()
		{
			return ContextManager.LogicalGetContextRow();
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00028BFE File Offset: 0x00026DFE
		private static ContextManager.ContextRow Restore(ContextManager.ContextRow row)
		{
			ContextManager.ContextRow contextRow = ContextManager.LogicalGetContextRow();
			ContextManager.LogicalSetContextRow(row);
			return contextRow;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00028C0B File Offset: 0x00026E0B
		private static ContextManager.ContextRow PopRow()
		{
			ContextManager.ContextRow contextRow = ContextManager.LogicalGetContextRow();
			ContextManager.LogicalSetContextRow(contextRow.PreviousRow);
			return contextRow;
		}

		// Token: 0x0400048F RID: 1167
		private static string sm_contextSlot = "Microsoft.Cloud.Platform.Utils.Context";

		// Token: 0x02000684 RID: 1668
		private sealed class ContextRow : IDisposable, IContextStack, ITraceDumpable
		{
			// Token: 0x06002DBE RID: 11710 RVA: 0x000A11B4 File Offset: 0x0009F3B4
			private ContextRow()
			{
				this.PreviousRow = null;
				this.Height = 0;
				this.m_cells = new object[0];
			}

			// Token: 0x06002DBF RID: 11711 RVA: 0x000A11D8 File Offset: 0x0009F3D8
			internal ContextRow(ContextManager.ContextRow previous, int key, object contextMember)
			{
				this.PreviousRow = (previous.IsEmpty() ? null : previous);
				this.Height = previous.Height + 1;
				int num = previous.m_cells.Length;
				this.m_cells = new object[(key >= num) ? (key + 1) : num];
				previous.m_cells.CopyTo(this.m_cells, 0);
				this.m_cells[key] = contextMember;
			}

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000A1244 File Offset: 0x0009F444
			// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x000A124C File Offset: 0x0009F44C
			internal ContextManager.ContextRow PreviousRow { get; private set; }

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x000A1255 File Offset: 0x0009F455
			// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x000A125D File Offset: 0x0009F45D
			internal int Height { get; private set; }

			// Token: 0x06002DC4 RID: 11716 RVA: 0x000A1266 File Offset: 0x0009F466
			internal bool IsEmpty()
			{
				return this.m_cells.Length == 0;
			}

			// Token: 0x06002DC5 RID: 11717 RVA: 0x000A1274 File Offset: 0x0009F474
			internal IEnumerable<T> GetContextMemberStack<T>(int key)
			{
				Stack<T> stack = new Stack<T>();
				ContextManager.ContextRow contextRow = this;
				T t = default(T);
				while (contextRow != null && !contextRow.IsEmpty())
				{
					t = contextRow.GetContextMember<T>(key);
					if (t != null && !t.Equals(default(T)))
					{
						if (stack.Count == 0)
						{
							goto IL_006C;
						}
						if (stack.Count > 0)
						{
							T t2 = stack.Peek();
							if (!t2.Equals(t))
							{
								goto IL_006C;
							}
						}
						IL_0073:
						contextRow = contextRow.PreviousRow;
						continue;
						IL_006C:
						stack.Push(t);
						goto IL_0073;
					}
					break;
				}
				return stack;
			}

			// Token: 0x06002DC6 RID: 11718 RVA: 0x000A1308 File Offset: 0x0009F508
			private bool EqualCells(ContextManager.ContextRow other)
			{
				if (other == null)
				{
					return false;
				}
				if (this.m_cells.Length != other.m_cells.Length)
				{
					return false;
				}
				for (int i = 0; i < this.m_cells.Length; i++)
				{
					if (!object.Equals(this.m_cells[i], other.m_cells[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06002DC7 RID: 11719 RVA: 0x000A135B File Offset: 0x0009F55B
			public void Dispose()
			{
				ContextManager.PopRow();
			}

			// Token: 0x06002DC8 RID: 11720 RVA: 0x000A1364 File Offset: 0x0009F564
			public IDisposable Restore()
			{
				ContextManager.ContextRow contextRow = this;
				if (this.IsEmpty())
				{
					contextRow = null;
				}
				ContextManager.ContextRow row = ContextManager.Restore(contextRow);
				return new DeferredDispose(delegate
				{
					row.Restore();
				});
			}

			// Token: 0x06002DC9 RID: 11721 RVA: 0x000A13A0 File Offset: 0x0009F5A0
			public T GetContextMember<T>(int key)
			{
				if (key < this.m_cells.Length && this.m_cells[key] != null)
				{
					return (T)((object)this.m_cells[key]);
				}
				return default(T);
			}

			// Token: 0x06002DCA RID: 11722 RVA: 0x000A13D9 File Offset: 0x0009F5D9
			public void Dump(TraceDump dumper)
			{
				this.Dump(dumper, null);
			}

			// Token: 0x06002DCB RID: 11723 RVA: 0x000A13E4 File Offset: 0x0009F5E4
			public void Dump(TraceDump dumper, params int[] slotsToIgnore)
			{
				if (this.m_cells.Length == 0)
				{
					dumper.Add("<Context: empty>");
					return;
				}
				bool flag = false;
				int key;
				int num;
				for (key = 0; key < this.m_cells.Length; key = num)
				{
					if (slotsToIgnore == null || slotsToIgnore.FirstPosition((int slot) => slot == key) == -1)
					{
						if (!flag)
						{
							dumper.Add("Context:");
							flag = true;
						}
						string text = ((key < UtilsContext.KeyNames.Length) ? UtilsContext.KeyNames[key] : key.ToString(CultureInfo.InvariantCulture));
						object obj = ((this.m_cells[key] != null) ? this.m_cells[key] : string.Empty);
						dumper.AddNameValue(text, obj);
					}
					num = key + 1;
				}
				if (!flag)
				{
					dumper.Add("<Context: uninteresting slots elided, empty otherwise>");
				}
			}

			// Token: 0x04001267 RID: 4711
			private object[] m_cells;

			// Token: 0x04001268 RID: 4712
			public static ContextManager.ContextRow Empty = new ContextManager.ContextRow();
		}
	}
}
