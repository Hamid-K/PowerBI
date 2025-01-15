using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000029 RID: 41
	public abstract class ColumnBindingsBase : ISchema
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00007714 File Offset: 0x00005914
		protected ColumnBindingsBase(ISchema input)
		{
			Contracts.CheckValue<ISchema>(input, "input");
			this.Input = input;
			this._names = new string[0];
			this._nameToInfoIndex = new Dictionary<string, int>();
			ColumnBindingsBase.ComputeColumnMapping(this.Input, this._names, out this._colMap, out this._mapIinfoToCol);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007770 File Offset: 0x00005970
		protected ColumnBindingsBase(ISchema input, bool user, params string[] names)
		{
			Contracts.CheckValue<ISchema>(input, "input");
			Contracts.CheckNonEmpty<string>(names, "names");
			this.Input = input;
			this._names = names;
			this._nameToInfoIndex = new Dictionary<string, int>(names.Length);
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				if (string.IsNullOrWhiteSpace(text))
				{
					throw user ? Contracts.ExceptUserArg("column", "New column needs a name") : Contracts.ExceptDecode("New column needs a name");
				}
				if (this._nameToInfoIndex.ContainsKey(text))
				{
					throw user ? Contracts.ExceptUserArg("column", "New column '{0}' specified multiple times", new object[] { text }) : Contracts.ExceptDecode("New column '{0}' specified multiple times", new object[] { text });
				}
				this._nameToInfoIndex.Add(text, i);
			}
			ColumnBindingsBase.ComputeColumnMapping(this.Input, names, out this._colMap, out this._mapIinfoToCol);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007860 File Offset: 0x00005A60
		private static void ComputeColumnMapping(ISchema input, string[] names, out int[] colMap, out int[] mapIinfoToCol)
		{
			colMap = new int[input.ColumnCount + names.Length];
			mapIinfoToCol = new int[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				int num;
				if (input.TryGetColumnIndex(text, ref num))
				{
					Contracts.Check(0 <= num && num < input.ColumnCount);
					string columnName = input.GetColumnName(num);
					Contracts.Check(columnName == text);
					Contracts.Check(colMap[num] == 0);
					mapIinfoToCol[i] = ~num;
					colMap[num] = ~i;
				}
			}
			int num2 = colMap.Length;
			int num3 = names.Length;
			while (--num3 >= 0)
			{
				if (mapIinfoToCol[num3] == 0)
				{
					colMap[--num2] = ~num3;
					mapIinfoToCol[num3] = num2;
				}
			}
			int num4 = input.ColumnCount;
			while (--num4 >= 0)
			{
				if (colMap[num4] < 0)
				{
					int num5 = ~colMap[num4];
					colMap[--num2] = ~num5;
					mapIinfoToCol[num5] = num2;
				}
				colMap[--num2] = num4;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000795C File Offset: 0x00005B5C
		public int ColumnCount
		{
			get
			{
				return this._colMap.Length;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007966 File Offset: 0x00005B66
		public int InfoCount
		{
			get
			{
				return this._mapIinfoToCol.Length;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007970 File Offset: 0x00005B70
		public int MapColumnIndex(out bool isSrcColumn, int col)
		{
			int num = this._colMap[col];
			if (num < 0)
			{
				num = ~num;
				isSrcColumn = false;
			}
			else
			{
				isSrcColumn = true;
			}
			return num;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007996 File Offset: 0x00005B96
		public int MapIinfoToCol(int iinfo)
		{
			return this._mapIinfoToCol[iinfo];
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000079A0 File Offset: 0x00005BA0
		public bool TryGetColumnIndex(string name, out int col)
		{
			if (name == null)
			{
				col = 0;
				return false;
			}
			int num;
			if (this.TryGetColumnIndexCore(name, out num))
			{
				col = this.MapIinfoToCol(num);
				return true;
			}
			int num2;
			if (this.Input.TryGetColumnIndex(name, ref num2))
			{
				int num3 = num2;
				while (this._colMap[num3] != num2)
				{
					num3++;
				}
				col = num3;
				return true;
			}
			col = 0;
			return false;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000079F8 File Offset: 0x00005BF8
		public string GetColumnName(int col)
		{
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			bool flag;
			int num = this.MapColumnIndex(out flag, col);
			if (flag)
			{
				return this.Input.GetColumnName(num);
			}
			return this.GetColumnNameCore(num);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007A40 File Offset: 0x00005C40
		public ColumnType GetColumnType(int col)
		{
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			bool flag;
			int num = this.MapColumnIndex(out flag, col);
			if (flag)
			{
				return this.Input.GetColumnType(num);
			}
			return this.GetColumnTypeCore(num);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007A88 File Offset: 0x00005C88
		public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
		{
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			bool flag;
			int num = this.MapColumnIndex(out flag, col);
			if (flag)
			{
				return this.Input.GetMetadataTypes(num);
			}
			return this.GetMetadataTypesCore(num);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public ColumnType GetMetadataTypeOrNull(string kind, int col)
		{
			Contracts.CheckNonEmpty(kind, "kind");
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			bool flag;
			int num = this.MapColumnIndex(out flag, col);
			if (flag)
			{
				return this.Input.GetMetadataTypeOrNull(kind, num);
			}
			return this.GetMetadataTypeCore(kind, num);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007B28 File Offset: 0x00005D28
		public void GetMetadata<TValue>(string kind, int col, ref TValue value)
		{
			Contracts.CheckNonEmpty(kind, "kind");
			Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
			bool flag;
			int num = this.MapColumnIndex(out flag, col);
			if (flag)
			{
				this.Input.GetMetadata<TValue>(kind, num, ref value);
				return;
			}
			this.GetMetadataCore<TValue>(kind, num, ref value);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007B80 File Offset: 0x00005D80
		protected bool TryGetColumnIndexCore(string name, out int iinfo)
		{
			return this._nameToInfoIndex.TryGetValue(name, out iinfo);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007B8F File Offset: 0x00005D8F
		protected string GetColumnNameCore(int iinfo)
		{
			return this._names[iinfo];
		}

		// Token: 0x060000F3 RID: 243
		protected abstract ColumnType GetColumnTypeCore(int iinfo);

		// Token: 0x060000F4 RID: 244 RVA: 0x00007B99 File Offset: 0x00005D99
		protected virtual IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
		{
			return Enumerable.Empty<KeyValuePair<string, ColumnType>>();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007BA0 File Offset: 0x00005DA0
		protected virtual ColumnType GetMetadataTypeCore(string kind, int iinfo)
		{
			return null;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007BA3 File Offset: 0x00005DA3
		protected virtual void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
		{
			throw MetadataUtils.ExceptGetMetadata();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007BAA File Offset: 0x00005DAA
		public bool[] GetActive(Func<int, bool> predicate)
		{
			return Utils.BuildArray<bool>(this.ColumnCount, predicate);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public bool[] GetActiveInput(Func<int, bool> predicate)
		{
			bool[] array = new bool[this.Input.ColumnCount];
			for (int i = 0; i < this._colMap.Length; i++)
			{
				int num = this._colMap[i];
				if (num >= 0 && predicate(i))
				{
					array[num] = true;
				}
			}
			return array;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007C04 File Offset: 0x00005E04
		public bool AnyNewColumnsActive(Func<int, bool> predicate)
		{
			foreach (int num in this._mapIinfoToCol)
			{
				if (predicate(num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400006A RID: 106
		public readonly ISchema Input;

		// Token: 0x0400006B RID: 107
		private readonly Dictionary<string, int> _nameToInfoIndex;

		// Token: 0x0400006C RID: 108
		private readonly string[] _names;

		// Token: 0x0400006D RID: 109
		private readonly int[] _mapIinfoToCol;

		// Token: 0x0400006E RID: 110
		private readonly int[] _colMap;
	}
}
