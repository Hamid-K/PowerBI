using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput
{
	// Token: 0x0200013C RID: 316
	[JsonObject(MemberSerialization.OptIn)]
	public class Table<T> : ITable<T>, IEnumerable<IEnumerable<T>>, IEnumerable, IEquatable<Table<T>>
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x00016608 File Offset: 0x00014808
		[JsonConstructor]
		public Table(IEnumerable<string> columnNames, IEnumerable<IEnumerable<T>> rows, IReadOnlyList<ITableMetadata> metadata = null)
		{
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (columnNames == null)
			{
				IEnumerable<T> enumerable = rows.FirstOrDefault<IEnumerable<T>>();
				if (enumerable == null)
				{
					throw new ArgumentOutOfRangeException("rows", "Table is empty when no columns are provided");
				}
				IEnumerable<T> enumerable2 = enumerable;
				this.ColumnNames = (from idx in Enumerable.Range(1, enumerable2.Count<T>())
					select FormattableString.Invariant(FormattableStringFactory.Create("column{0}", new object[] { idx }))).ToList<string>();
			}
			else
			{
				this.ColumnNames = columnNames.Select((string colName) => colName.ToString()).ToList<string>();
			}
			this.Rows = rows.Select((IEnumerable<T> row) => row.ToList<T>()).ToList<List<T>>();
			this._columnHashes = new Dictionary<string, int>();
			this._normalizedColumnHashes = new Dictionary<string, int>();
			this._metadata = metadata ?? new List<ITableMetadata>();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00016715 File Offset: 0x00014915
		public List<string> NormalizedColumn(string columnName)
		{
			return this.Column(columnName).Select(delegate(T a)
			{
				double num;
				if (a == null || !double.TryParse(a.ToString(), out num))
				{
					ref T ptr = ref a;
					T t = default(T);
					if (t == null)
					{
						t = a;
						ptr = ref t;
						if (t == null)
						{
							return null;
						}
					}
					return ptr.ToString();
				}
				return num.ToString();
			}).ToList<string>();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00016748 File Offset: 0x00014948
		public List<T> Column(string columnName)
		{
			int? num = this.ColumnNames.IndexOf(columnName);
			if (num != null)
			{
				int colIdx = num.GetValueOrDefault();
				Func<IEnumerable<T>, T> <>9__1;
				return this._columns.GetOrAdd(columnName, delegate(string _)
				{
					IEnumerable<IEnumerable<T>> rows = this.Rows;
					Func<IEnumerable<T>, T> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (IEnumerable<T> row) => row.ElementAtOrDefault(colIdx));
					}
					return rows.Select(func).ToList<T>();
				});
			}
			return null;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000167A1 File Offset: 0x000149A1
		public bool Equals(Table<T> other)
		{
			return other != null && (this == other || (object.Equals(this.ColumnNames, other.ColumnNames) && object.Equals(this.Rows, other.Rows)));
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000167D4 File Offset: 0x000149D4
		[JsonProperty]
		public IEnumerable<IEnumerable<T>> Rows { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000167DC File Offset: 0x000149DC
		[JsonProperty]
		public IEnumerable<string> ColumnNames { get; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x000167E4 File Offset: 0x000149E4
		public IReadOnlyList<ITableMetadata> Metadata
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000167EC File Offset: 0x000149EC
		public IEnumerator<IEnumerable<T>> GetEnumerator()
		{
			return this.Rows.GetEnumerator();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000167F9 File Offset: 0x000149F9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00016801 File Offset: 0x00014A01
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Table<T>)obj)));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00016830 File Offset: 0x00014A30
		public override int GetHashCode()
		{
			return this.ColumnNames.AppendItem(this.ColumnNames.Select(new Func<string, int>(this.HashedColumn)).OrderDependentHashCode<int>().ToString()).Concat(from m in this.Metadata
				select m.ToString() into s
				orderby s
				select s).OrderDependentHashCode<string>();
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000168C4 File Offset: 0x00014AC4
		public int HashedColumn(string columnName)
		{
			return this._columnHashes.GetOrAdd(columnName, (string _) => this.Column(columnName).Select(delegate(T a)
			{
				ref T ptr = ref a;
				T t = default(T);
				string text;
				if (t == null)
				{
					t = a;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_0031;
					}
				}
				text = ptr.ToString();
				IL_0031:
				ref T ptr2 = ref a;
				t = default(T);
				object obj;
				if (t == null)
				{
					t = a;
					ptr2 = ref t;
					if (t == null)
					{
						obj = null;
						goto IL_0062;
					}
				}
				obj = ptr2.GetType();
				IL_0062:
				object obj2 = obj;
				return text + ((obj2 != null) ? obj2.ToString() : null);
			}).OrderDependentHashCode<string>());
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00016904 File Offset: 0x00014B04
		public int HashedNormalizedColumn(string columnName)
		{
			return this._normalizedColumnHashes.GetOrAdd(columnName, (string _) => this.NormalizedColumn(columnName).Select(delegate(string a)
			{
				string text = ((a != null) ? a.ToString() : null);
				Type type = ((a != null) ? a.GetType() : null);
				return text + ((type != null) ? type.ToString() : null);
			}).OrderDependentHashCode<string>());
		}

		// Token: 0x0400031A RID: 794
		private readonly Dictionary<string, List<T>> _columns = new Dictionary<string, List<T>>();

		// Token: 0x0400031B RID: 795
		private readonly Dictionary<string, int> _columnHashes;

		// Token: 0x0400031C RID: 796
		private readonly Dictionary<string, int> _normalizedColumnHashes;

		// Token: 0x0400031D RID: 797
		private readonly IReadOnlyList<ITableMetadata> _metadata;
	}
}
