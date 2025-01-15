using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data.Entity.Hierarchy
{
	// Token: 0x020002C9 RID: 713
	[DataContract]
	[Serializable]
	public class HierarchyId : IComparable
	{
		// Token: 0x06002243 RID: 8771 RVA: 0x00060A95 File Offset: 0x0005EC95
		public HierarchyId()
		{
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00060AA0 File Offset: 0x0005ECA0
		public HierarchyId(string hierarchyId)
		{
			this._hierarchyId = hierarchyId;
			if (hierarchyId != null)
			{
				string[] array = hierarchyId.Split(new char[] { '/' });
				if (!string.IsNullOrEmpty(array[0]) || !string.IsNullOrEmpty(array[array.Length - 1]))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The input string '{0}' is not a valid string representation of a HierarchyId node.", new object[] { hierarchyId }), "hierarchyId");
				}
				int num = array.Length - 2;
				int[][] array2 = new int[num][];
				for (int i = 0; i < num; i++)
				{
					string[] array3 = array[i + 1].Split(new char[] { '.' });
					int[] array4 = new int[array3.Length];
					for (int j = 0; j < array3.Length; j++)
					{
						int num2;
						if (!int.TryParse(array3[j], out num2))
						{
							throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The input string '{0}' is not a valid string representation of a HierarchyId node.", new object[] { hierarchyId }), "hierarchyId");
						}
						array4[j] = num2;
					}
					array2[i] = array4;
				}
				this._nodes = array2;
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00060BA4 File Offset: 0x0005EDA4
		public HierarchyId GetAncestor(int n)
		{
			if (this._nodes == null || (int)this.GetLevel() < n)
			{
				return new HierarchyId(null);
			}
			if ((int)this.GetLevel() == n)
			{
				return new HierarchyId("/");
			}
			return new HierarchyId("/" + string.Join("/", this._nodes.Take((int)this.GetLevel() - n).Select(new Func<int[], string>(HierarchyId.IntArrayToStirng))) + "/");
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00060C20 File Offset: 0x0005EE20
		public HierarchyId GetDescendant(HierarchyId child1, HierarchyId child2)
		{
			if (this._nodes == null)
			{
				return new HierarchyId(null);
			}
			if (child1 != null && (child1.GetLevel() != this.GetLevel() + 1 || !child1.IsDescendantOf(this)))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "HierarchyId.GetDescendant failed because '{0}' must be a child of 'this'.  '{0}' was '{1}' and 'this' was '{2}'.", new object[]
				{
					"child1",
					child1,
					this.ToString()
				}), "child1");
			}
			if (child2 != null && (child2.GetLevel() != this.GetLevel() + 1 || !child2.IsDescendantOf(this)))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "HierarchyId.GetDescendant failed because '{0}' must be a child of 'this'.  '{0}' was '{1}' and 'this' was '{2}'.", new object[]
				{
					"child2",
					child1,
					this.ToString()
				}), "child2");
			}
			if (child1 == null && child2 == null)
			{
				return new HierarchyId(this.ToString() + 1.ToString() + "/");
			}
			if (child1 == null)
			{
				HierarchyId hierarchyId = new HierarchyId(child2.ToString());
				int[] array = hierarchyId._nodes.Last<int[]>();
				array[array.Length - 1]--;
				return new HierarchyId("/" + string.Join("/", hierarchyId._nodes.Select(new Func<int[], string>(HierarchyId.IntArrayToStirng))) + "/");
			}
			if (child2 == null)
			{
				HierarchyId hierarchyId2 = new HierarchyId(child1.ToString());
				int[] array2 = hierarchyId2._nodes.Last<int[]>();
				array2[array2.Length - 1]++;
				return new HierarchyId("/" + string.Join("/", hierarchyId2._nodes.Select(new Func<int[], string>(HierarchyId.IntArrayToStirng))) + "/");
			}
			int[] array3 = child1._nodes.Last<int[]>();
			int[] array4 = child2._nodes.Last<int[]>();
			if (HierarchyId.CompareIntArrays(array3, array4) >= 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "HierarchyId.GetDescendant failed because 'child1' must be less than 'child2'.  'child1' was '{0}' and 'child2' was '{1}'.", new object[] { child1, child2 }), "child1");
			}
			int num = 0;
			while (num < array3.Length && array3[num] >= array4[num])
			{
				num++;
			}
			array3 = array3.Take(num + 1).ToArray<int>();
			if (array3[num] + 1 < array4[num])
			{
				array3[num]++;
			}
			else
			{
				array3 = array3.Concat(new int[] { 1 }).ToArray<int>();
			}
			return new HierarchyId(string.Concat(new string[]
			{
				"/",
				string.Join("/", this._nodes.Select(new Func<int[], string>(HierarchyId.IntArrayToStirng))),
				"/",
				HierarchyId.IntArrayToStirng(array3),
				"/"
			}));
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00060EDD File Offset: 0x0005F0DD
		public short GetLevel()
		{
			if (this._nodes == null)
			{
				return 0;
			}
			return (short)this._nodes.Length;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00060EF2 File Offset: 0x0005F0F2
		public static HierarchyId GetRoot()
		{
			return DbHierarchyServices.GetRoot();
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00060EFC File Offset: 0x0005F0FC
		public bool IsDescendantOf(HierarchyId parent)
		{
			if (parent == null)
			{
				return true;
			}
			if (this._nodes == null || parent.GetLevel() > this.GetLevel())
			{
				return false;
			}
			for (int i = 0; i < (int)parent.GetLevel(); i++)
			{
				if (HierarchyId.CompareIntArrays(this._nodes[i], parent._nodes[i]) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00060F58 File Offset: 0x0005F158
		public HierarchyId GetReparentedValue(HierarchyId oldRoot, HierarchyId newRoot)
		{
			if (oldRoot == null || newRoot == null)
			{
				return new HierarchyId(null);
			}
			if (!this.IsDescendantOf(oldRoot))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "HierarchyId.GetReparentedValue failed because 'oldRoot' was not an ancestor node of 'this'.  'oldRoot' was '{0}', and 'this' was '{1}'.", new object[]
				{
					oldRoot,
					this.ToString()
				}), "oldRoot");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/");
			foreach (int[] array in newRoot._nodes)
			{
				stringBuilder.Append(HierarchyId.IntArrayToStirng(array));
				stringBuilder.Append("/");
			}
			foreach (int[] array2 in this._nodes.Skip((int)oldRoot.GetLevel()))
			{
				stringBuilder.Append(HierarchyId.IntArrayToStirng(array2));
				stringBuilder.Append("/");
			}
			return new HierarchyId(stringBuilder.ToString());
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00061068 File Offset: 0x0005F268
		public static HierarchyId Parse(string input)
		{
			return new HierarchyId(input);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00061070 File Offset: 0x0005F270
		private static string IntArrayToStirng(IEnumerable<int> array)
		{
			return string.Join<int>(".", array);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00061080 File Offset: 0x0005F280
		private static int CompareIntArrays(int[] array1, int[] array2)
		{
			int num = Math.Min(array1.Length, array2.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = array1[i];
				int num3 = array2[i];
				if (num2 < num3)
				{
					return -1;
				}
				if (num2 > num3)
				{
					return 1;
				}
			}
			if (array1.Length > num)
			{
				return 1;
			}
			if (array2.Length > num)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000610CC File Offset: 0x0005F2CC
		public static int Compare(HierarchyId hid1, HierarchyId hid2)
		{
			int[][] array = ((hid1 == null) ? null : hid1._nodes);
			int[][] array2 = ((hid2 == null) ? null : hid2._nodes);
			if (array == null && array2 == null)
			{
				return 0;
			}
			if (array == null)
			{
				return -1;
			}
			if (array2 == null)
			{
				return 1;
			}
			int num = Math.Min(array.Length, array2.Length);
			for (int i = 0; i < num; i++)
			{
				int[] array3 = array[i];
				int[] array4 = array2[i];
				int num2 = HierarchyId.CompareIntArrays(array3, array4);
				if (num2 != 0)
				{
					return num2;
				}
			}
			if (hid1._nodes.Length > num)
			{
				return 1;
			}
			if (hid2._nodes.Length > num)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00061150 File Offset: 0x0005F350
		public static bool operator <(HierarchyId hid1, HierarchyId hid2)
		{
			return HierarchyId.Compare(hid1, hid2) < 0;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0006115C File Offset: 0x0005F35C
		public static bool operator >(HierarchyId hid1, HierarchyId hid2)
		{
			return hid2 < hid1;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00061165 File Offset: 0x0005F365
		public static bool operator <=(HierarchyId hid1, HierarchyId hid2)
		{
			return HierarchyId.Compare(hid1, hid2) <= 0;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00061174 File Offset: 0x0005F374
		public static bool operator >=(HierarchyId hid1, HierarchyId hid2)
		{
			return hid2 <= hid1;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0006117D File Offset: 0x0005F37D
		public static bool operator ==(HierarchyId hid1, HierarchyId hid2)
		{
			return HierarchyId.Compare(hid1, hid2) == 0;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00061189 File Offset: 0x0005F389
		public static bool operator !=(HierarchyId hid1, HierarchyId hid2)
		{
			return !(hid1 == hid2);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00061195 File Offset: 0x0005F395
		protected bool Equals(HierarchyId other)
		{
			return this == other;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0006119E File Offset: 0x0005F39E
		public override int GetHashCode()
		{
			if (this._hierarchyId == null)
			{
				return 0;
			}
			return this._hierarchyId.GetHashCode();
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000611B5 File Offset: 0x0005F3B5
		public override bool Equals(object obj)
		{
			return this.Equals((HierarchyId)obj);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000611C3 File Offset: 0x0005F3C3
		public override string ToString()
		{
			return this._hierarchyId;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000611CC File Offset: 0x0005F3CC
		public int CompareTo(object obj)
		{
			HierarchyId hierarchyId = obj as HierarchyId;
			if (hierarchyId != null)
			{
				return HierarchyId.Compare(this, hierarchyId);
			}
			return -1;
		}

		// Token: 0x04000BE7 RID: 3047
		private readonly string _hierarchyId;

		// Token: 0x04000BE8 RID: 3048
		private readonly int[][] _nodes;

		// Token: 0x04000BE9 RID: 3049
		public const string PathSeparator = "/";

		// Token: 0x04000BEA RID: 3050
		private const string InvalidHierarchyIdExceptionMessage = "The input string '{0}' is not a valid string representation of a HierarchyId node.";

		// Token: 0x04000BEB RID: 3051
		private const string GetReparentedValueOldRootExceptionMessage = "HierarchyId.GetReparentedValue failed because 'oldRoot' was not an ancestor node of 'this'.  'oldRoot' was '{0}', and 'this' was '{1}'.";

		// Token: 0x04000BEC RID: 3052
		private const string GetDescendantMostBeChildExceptionMessage = "HierarchyId.GetDescendant failed because '{0}' must be a child of 'this'.  '{0}' was '{1}' and 'this' was '{2}'.";

		// Token: 0x04000BED RID: 3053
		private const string GetDescendantChild1MustLessThanChild2ExceptionMessage = "HierarchyId.GetDescendant failed because 'child1' must be less than 'child2'.  'child1' was '{0}' and 'child2' was '{1}'.";
	}
}
