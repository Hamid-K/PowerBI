using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004F5 RID: 1269
	public struct RectangularArray<T> : IEquatable<RectangularArray<T>>, IStructuralEquatable
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000543F3 File Offset: 0x000525F3
		public readonly int Width { get; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000543FB File Offset: 0x000525FB
		public readonly int Height { get; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00054403 File Offset: 0x00052603
		public Bounds<TableUnit> Span
		{
			get
			{
				return new Bounds<TableUnit>(0, this.Width - 1, 0, this.Height - 1);
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0005441C File Offset: 0x0005261C
		public RectangularArray(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			this._array = new T[this.Width][];
			for (int i = 0; i < this.Width; i++)
			{
				this._array[i] = new T[this.Height];
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0005446C File Offset: 0x0005266C
		public RectangularArray(Vector<TableUnit> size)
		{
			this = new RectangularArray<T>(size.X, size.Y);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00054480 File Offset: 0x00052680
		public RectangularArray(T[,] array)
		{
			this.Width = array.GetLength(0);
			this.Height = array.GetLength(1);
			this._array = new T[this.Width][];
			for (int i = 0; i < this.Width; i++)
			{
				this._array[i] = new T[this.Height];
				for (int j = 0; j < this.Height; j++)
				{
					this._array[i][j] = array[i, j];
				}
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00054503 File Offset: 0x00052703
		private RectangularArray(T[][] array, int width, int height)
		{
			this._array = array;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0005451C File Offset: 0x0005271C
		public RectangularArray(RectangularArray<T> array)
		{
			this.Width = array.GetLength(Axis.Horizontal);
			this.Height = array.GetLength(Axis.Vertical);
			this._array = new T[this.Width][];
			for (int i = 0; i < this.Width; i++)
			{
				this._array[i] = new T[this.Height];
				Array.Copy(array._array[i], this._array[i], this.Height);
			}
		}

		// Token: 0x170004D7 RID: 1239
		public T this[int x, int y]
		{
			get
			{
				return this._array[x][y];
			}
			set
			{
				this._array[x][y] = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		public T this[Vector<TableUnit> idx]
		{
			get
			{
				return this._array[idx.X][idx.Y];
			}
			set
			{
				this._array[idx.X][idx.Y] = value;
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000545EC File Offset: 0x000527EC
		public T[,] ToMultidimensionalArray()
		{
			T[,] array = new T[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				T[] array2 = this._array[i];
				for (int j = 0; j < this.Height; j++)
				{
					array[i, j] = array2[j];
				}
			}
			return array;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00054646 File Offset: 0x00052846
		public IEnumerable<T> ToEnumerable()
		{
			int num;
			for (int x = 0; x < this.Width; x = num + 1)
			{
				for (int y = 0; y < this.Height; y = num + 1)
				{
					T t = this[x, y];
					if (t != null)
					{
						yield return t;
					}
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0005465B File Offset: 0x0005285B
		public IEnumerable<T> ToEnumerableWithNulls()
		{
			int num;
			for (int x = 0; x < this.Width; x = num + 1)
			{
				for (int y = 0; y < this.Height; y = num + 1)
				{
					T t = this[x, y];
					yield return t;
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00054670 File Offset: 0x00052870
		public IEnumerable<T> ToEnumerableRowMajor()
		{
			int num;
			for (int y = 0; y < this.Height; y = num + 1)
			{
				for (int x = 0; x < this.Width; x = num + 1)
				{
					T t = this[x, y];
					if (t != null)
					{
						yield return t;
					}
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00054688 File Offset: 0x00052888
		public RectangularArray<T> Collapse(Func<T, bool> isNonEmpty = null)
		{
			bool[] array = new bool[this.Width];
			bool[] array2 = new bool[this.Height];
			if (isNonEmpty == null)
			{
				isNonEmpty = new Func<T, bool>(RectangularArray<T>.<Collapse>g__IsNonNull|24_0);
			}
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (isNonEmpty(this[i, j]))
					{
						array[i] = true;
						array2[j] = true;
					}
				}
			}
			RectangularArray<T> rectangularArray = new RectangularArray<T>(array.Count((bool filled) => filled), array2.Count((bool filled) => filled));
			int num = 0;
			for (int k = 0; k < this.Width; k++)
			{
				if (array[k])
				{
					int num2 = 0;
					for (int l = 0; l < this.Height; l++)
					{
						if (array2[l])
						{
							rectangularArray[num, num2] = this[k, l];
							num2++;
						}
					}
					num++;
				}
			}
			return rectangularArray;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000547B0 File Offset: 0x000529B0
		public RectangularArray<T> Section(int minX, int minY, int maxX, int maxY)
		{
			if (minX < 0 || maxX > this.Width || minY < 0 || maxY > this.Height)
			{
				throw new ArgumentException("Section out of bounds.");
			}
			if (minX > maxX || minY > maxY)
			{
				throw new ArgumentException("Minimum bound greater than maximum bound.");
			}
			int num = maxX - minX;
			int num2 = maxY - minY;
			RectangularArray<T> rectangularArray = new RectangularArray<T>(num, num2);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					rectangularArray[i, j] = this[i + minX, j + minY];
				}
			}
			return rectangularArray;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0005483D File Offset: 0x00052A3D
		public RectangularArray<T> Section(Bounds<TableUnit> bounds)
		{
			return this.Section(bounds.Left, bounds.Top, bounds.Right + 1, bounds.Bottom + 1);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00054865 File Offset: 0x00052A65
		public int GetLength(Axis axis)
		{
			if (axis == Axis.Horizontal)
			{
				return this.Width;
			}
			if (axis != Axis.Vertical)
			{
				throw new ArgumentException();
			}
			return this.Height;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00054884 File Offset: 0x00052A84
		public IEnumerable<T> Slice(Axis axis, int index)
		{
			RectangularArray<T>.<>c__DisplayClass28_0 CS$<>8__locals1 = new RectangularArray<T>.<>c__DisplayClass28_0();
			CS$<>8__locals1.axis = axis;
			CS$<>8__locals1.index = index;
			if (CS$<>8__locals1.index >= this.GetLength(CS$<>8__locals1.axis.Perpendicular()) || CS$<>8__locals1.index < 0)
			{
				throw new ArgumentException("Index out of bounds.");
			}
			CS$<>8__locals1.self = this;
			return CS$<>8__locals1.<Slice>g__Generator|0();
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000548E4 File Offset: 0x00052AE4
		public IEnumerable<IEnumerable<T>> Slices(Axis axis)
		{
			return from i in Enumerable.Range(0, this.GetLength(axis.Perpendicular()))
				select this.Slice(axis, i);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00054932 File Offset: 0x00052B32
		public IEnumerable<IEnumerable<T>> Rows()
		{
			return this.Slices(Axis.Horizontal);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x0005493B File Offset: 0x00052B3B
		public IEnumerable<IEnumerable<T>> Columns()
		{
			return this.Slices(Axis.Vertical);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00054944 File Offset: 0x00052B44
		public IEnumerable<T> Row(int i)
		{
			return this.Slice(Axis.Horizontal, i);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0005494E File Offset: 0x00052B4E
		public IEnumerable<T> Column(int i)
		{
			return this.Slice(Axis.Vertical, i);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00054958 File Offset: 0x00052B58
		public IEnumerable<T> Row(int i, Bounds<TableUnit> intersection)
		{
			if (!intersection.Vertical.Contains(i))
			{
				return null;
			}
			return this.Slice(Axis.Horizontal, i).Slice(intersection.Horizontal);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00054990 File Offset: 0x00052B90
		public IEnumerable<T> Column(int i, Bounds<TableUnit> intersection)
		{
			if (!intersection.Horizontal.Contains(i))
			{
				return null;
			}
			return this.Slice(Axis.Vertical, i).Slice(intersection.Vertical);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000549C5 File Offset: 0x00052BC5
		public IEnumerable<T> Row(int i, Range<TableUnit> horizontalIntersection)
		{
			return this.Slice(Axis.Horizontal, i).Slice(horizontalIntersection);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000549D5 File Offset: 0x00052BD5
		public IEnumerable<T> Column(int i, Range<TableUnit> verticalIntersection)
		{
			return this.Slice(Axis.Vertical, i).Slice(verticalIntersection);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000549E8 File Offset: 0x00052BE8
		public RectangularArray<T> Transpose()
		{
			RectangularArray<T> rectangularArray = new RectangularArray<T>(this.Height, this.Width);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					rectangularArray[i, j] = this[j, i];
				}
			}
			return rectangularArray;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00054A3C File Offset: 0x00052C3C
		public RectangularArray<TR> Select<TR>(Func<T, TR> fn)
		{
			RectangularArray<TR> rectangularArray = new RectangularArray<TR>(this.Width, this.Height);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					rectangularArray[i, j] = fn(this[i, j]);
				}
			}
			return rectangularArray;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00054A98 File Offset: 0x00052C98
		public RectangularArray<TR> Select<TR>(Func<T, int, int, TR> fn)
		{
			RectangularArray<TR> rectangularArray = new RectangularArray<TR>(this.Width, this.Height);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					rectangularArray[i, j] = fn(this[i, j], i, j);
				}
			}
			return rectangularArray;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00054AF4 File Offset: 0x00052CF4
		public bool StructuralEqual(RectangularArray<T> other, IEqualityComparer<T> comparer = null)
		{
			if (this.Height != other.Height || this.Width != other.Width)
			{
				return false;
			}
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!comparer.Equals(this[i, j], other[i, j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00054B6C File Offset: 0x00052D6C
		public Vector<TableUnit> Corner(Ordinal ordinal)
		{
			int num = ((ordinal.Horizontal().Derivative() == Derivative.Increasing) ? (this.Width - 1) : 0);
			int num2 = ((ordinal.Vertical().Derivative() == Derivative.Increasing) ? (this.Height - 1) : 0);
			return new Vector<TableUnit>(num, num2);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00054BB4 File Offset: 0x00052DB4
		public RectangularArray<T> JoinBelow(RectangularArray<T> other)
		{
			if (this.Width != other.Width)
			{
				throw new ArgumentException("Arrays must be the same width to join below or above");
			}
			int num = this.Height + other.Height;
			T[][] array = new T[num][];
			for (int i = 0; i < this.Width; i++)
			{
				array[i] = this._array[i].Concat(other._array[i]).ToArray<T>();
			}
			return new RectangularArray<T>(array, this.Width, num);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00054C2C File Offset: 0x00052E2C
		public RectangularArray<T> JoinAbove(RectangularArray<T> other)
		{
			return other.JoinBelow(this);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00054C3C File Offset: 0x00052E3C
		public RectangularArray<T> JoinRight(RectangularArray<T> other)
		{
			if (this.Height != other.Height)
			{
				throw new ArgumentException("Arrays must be the same height to join right or left");
			}
			return new RectangularArray<T>(this._array.Concat(other._array).ToArray<T[]>(), this.Width, this.Height + other.Height);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00054C92 File Offset: 0x00052E92
		public RectangularArray<T> JoinLeft(RectangularArray<T> other)
		{
			return other.JoinRight(this);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00054CA4 File Offset: 0x00052EA4
		public RectangularArray<T> Join(RectangularArray<T> other, Direction dir)
		{
			switch (dir)
			{
			case Direction.Up:
				return this.JoinAbove(other);
			case Direction.Right:
				return this.JoinRight(other);
			case Direction.Down:
				return this.JoinBelow(other);
			case Direction.Left:
				return this.JoinLeft(other);
			default:
				throw new ArgumentException(string.Format("Unknown direction: {0}", dir));
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00054D00 File Offset: 0x00052F00
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			RectangularArray<string> textTable = this.Select<string>(delegate(T cell)
			{
				ref T ptr = ref cell;
				T t = default(T);
				if (t == null)
				{
					t = cell;
					ptr = ref t;
					if (t == null)
					{
						return null;
					}
				}
				return ptr.ToString().Replace("\n", " ");
			});
			string text;
			Dictionary<int, int> dictionary = Enumerable.Range(0, textTable.GetLength(Axis.Horizontal)).ToDictionary((int key) => key, (int key) => textTable.Slice(Axis.Vertical, key).Max(delegate(string text)
			{
				if (text == null)
				{
					return 0;
				}
				return text.Length;
			}));
			for (int i = 0; i < this.Height; i++)
			{
				bool flag = true;
				for (int j = 0; j < this.Width; j++)
				{
					int num = dictionary[j];
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					text = textTable[j, i] ?? "";
					stringBuilder.Append(text.PadRight(num));
					flag = false;
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00054E03 File Offset: 0x00053003
		public bool Equals(RectangularArray<T> other)
		{
			return this.StructuralEqual(other, null);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00054E10 File Offset: 0x00053010
		public bool Equals(object other, IEqualityComparer comparer)
		{
			if (!(other is RectangularArray<T>))
			{
				return false;
			}
			RectangularArray<T> rectangularArray = (RectangularArray<T>)other;
			if (this.Height != rectangularArray.Height || this.Width != rectangularArray.Width)
			{
				return false;
			}
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!comparer.Equals(this[i, j], rectangularArray[i, j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00054E98 File Offset: 0x00053098
		public int GetHashCode(IEqualityComparer comparer)
		{
			return this.ToEnumerableWithNulls().OrderDependentHashCode(comparer);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00054EA6 File Offset: 0x000530A6
		[CompilerGenerated]
		internal static bool <Collapse>g__IsNonNull|24_0(T x)
		{
			return x != null;
		}

		// Token: 0x04000DBA RID: 3514
		private readonly T[][] _array;
	}
}
