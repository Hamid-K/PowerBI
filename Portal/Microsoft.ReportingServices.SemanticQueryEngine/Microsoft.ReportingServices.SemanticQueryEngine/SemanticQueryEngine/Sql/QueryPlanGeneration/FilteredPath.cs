using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000056 RID: 86
	internal sealed class FilteredPath : IList<IPathItem>, ICollection<IPathItem>, IEnumerable<IPathItem>, IEnumerable
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00011EB7 File Offset: 0x000100B7
		internal FilteredPath()
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011ECA File Offset: 0x000100CA
		internal FilteredPath(ExpressionPath path)
		{
			this.AddRange(path);
		}

		// Token: 0x170000B1 RID: 177
		IPathItem IList<IPathItem>.this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				return this[index];
			}
			[DebuggerStepThrough]
			set
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011EED File Offset: 0x000100ED
		int IList<IPathItem>.IndexOf(IPathItem item)
		{
			if (item != null && !(item is FilteredPathItem))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			return this.m_list.IndexOf((FilteredPathItem)item);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B421 File Offset: 0x00009621
		void IList<IPathItem>.Insert(int index, IPathItem item)
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B421 File Offset: 0x00009621
		void IList<IPathItem>.RemoveAt(int index)
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00011F11 File Offset: 0x00010111
		int ICollection<IPathItem>.Count
		{
			[DebuggerStepThrough]
			get
			{
				return this.Length;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00004555 File Offset: 0x00002755
		bool ICollection<IPathItem>.IsReadOnly
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000B421 File Offset: 0x00009621
		void ICollection<IPathItem>.Add(IPathItem item)
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B421 File Offset: 0x00009621
		void ICollection<IPathItem>.Clear()
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011F19 File Offset: 0x00010119
		bool ICollection<IPathItem>.Contains(IPathItem item)
		{
			if (item != null && !(item is FilteredPathItem))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			return this.m_list.Contains((FilteredPathItem)item);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000B421 File Offset: 0x00009621
		void ICollection<IPathItem>.CopyTo(IPathItem[] array, int arrayIndex)
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000B421 File Offset: 0x00009621
		bool ICollection<IPathItem>.Remove(IPathItem item)
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00011F3D File Offset: 0x0001013D
		IEnumerator<IPathItem> IEnumerable<IPathItem>.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Length; i = num)
			{
				yield return this[i].ExpressionPathItem;
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011F4C File Offset: 0x0001014C
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Length; i = num)
			{
				yield return this[i].ExpressionPathItem;
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00011F5B File Offset: 0x0001015B
		internal int Length
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00011F68 File Offset: 0x00010168
		internal bool IsEmpty
		{
			[DebuggerStepThrough]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x170000B6 RID: 182
		internal FilteredPathItem this[int index]
		{
			[DebuggerStepThrough]
			get
			{
				if (index < 0 || index > this.Length)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("index"));
				}
				return this.m_list[index];
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00011F9E File Offset: 0x0001019E
		internal FilteredPathItem LastItem
		{
			[DebuggerStepThrough]
			get
			{
				if (this.Length <= 0)
				{
					return null;
				}
				return this[this.Length - 1];
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00011FB9 File Offset: 0x000101B9
		internal void Add(FilteredPathItem pathItem)
		{
			if (pathItem == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("pathItem"));
			}
			this.m_list.Add(pathItem);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011FDA File Offset: 0x000101DA
		internal void AddRange(ExpressionPath path)
		{
			this.InsertRange(this.m_list.Count, path);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011FF0 File Offset: 0x000101F0
		internal void InsertRange(int insertAt, ExpressionPath path)
		{
			if (insertAt < 0 || insertAt > this.Length)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("insertAt"));
			}
			if (!path.IsEmpty)
			{
				int i = 0;
				while (i < path.Length)
				{
					this.m_list.Insert(insertAt, new FilteredPathItem(path[i]));
					i++;
					insertAt++;
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012050 File Offset: 0x00010250
		internal void CanonicalizeEvals()
		{
			if (this.Length == 0)
			{
				return;
			}
			int i;
			for (i = 0; i < this.Length; i++)
			{
				if (this[i].Evaluate)
				{
					this[i].Evaluate = false;
				}
				if (this[i].ExpressionPathItem.Cardinality == Cardinality.Many)
				{
					break;
				}
			}
			FilteredPathItem filteredPathItem = null;
			bool flag = false;
			bool flag2 = false;
			while (i < this.Length)
			{
				if (this[i].ExpressionPathItem.Cardinality == Cardinality.Many && !flag2)
				{
					flag2 = true;
					filteredPathItem = null;
				}
				if (flag2 && this[i].Evaluate)
				{
					if (filteredPathItem != null)
					{
						filteredPathItem.Evaluate = false;
					}
					filteredPathItem = this[i];
				}
				if (this[i].ExpressionPathItem.ReverseCardinality == Cardinality.Many && flag2)
				{
					flag2 = false;
					flag = false;
				}
				if (!flag2)
				{
					if (this[i].Evaluate)
					{
						flag = true;
					}
					else if (flag && this[i].ExpressionPathItem.ReverseCardinality == Cardinality.Many)
					{
						this[i].Evaluate = true;
					}
				}
				i++;
			}
			if (flag2 && filteredPathItem != null)
			{
				filteredPathItem.Evaluate = false;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012160 File Offset: 0x00010360
		internal CardinalityContext GetCardinalityContext()
		{
			return PathAlgorithms.GetCardinalityContext<IPathItem>(this);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00012168 File Offset: 0x00010368
		internal Cardinality GetCardinality()
		{
			return PathAlgorithms.GetCardinality<IPathItem>(this);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00012170 File Offset: 0x00010370
		internal Optionality GetOptionality()
		{
			return PathAlgorithms.GetOptionality<IPathItem>(this);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012178 File Offset: 0x00010378
		internal int GetLeadingScalarLength()
		{
			return PathAlgorithms.GetLeadingScalarLength<IPathItem>(this);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00012180 File Offset: 0x00010380
		internal FilteredPath GetSegment(int startAt, int length)
		{
			if (startAt < 0 || startAt > this.Length)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("startAt"));
			}
			if (length < 0)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("length"));
			}
			if (startAt + length > this.Length)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			FilteredPath filteredPath = new FilteredPath();
			for (int i = startAt; i < startAt + length; i++)
			{
				filteredPath.Add(this.m_list[i].Clone());
			}
			return filteredPath;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000121FB File Offset: 0x000103FB
		internal FilteredPath Clone()
		{
			return this.GetSegment(0, this.Length);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001220C File Offset: 0x0001040C
		public override string ToString()
		{
			string[] array = new string[this.Length];
			string text = "";
			for (int i = 0; i < this.Length; i++)
			{
				array[i] = this[i].ToString();
				if (this[i].Evaluate)
				{
					array[i] = "(" + array[i];
					text += ")";
				}
			}
			return string.Join("/", array) + text;
		}

		// Token: 0x040001CC RID: 460
		private readonly List<FilteredPathItem> m_list = new List<FilteredPathItem>();
	}
}
