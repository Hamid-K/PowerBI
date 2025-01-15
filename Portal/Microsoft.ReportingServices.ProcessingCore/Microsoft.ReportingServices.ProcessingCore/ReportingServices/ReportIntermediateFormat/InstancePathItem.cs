using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003B6 RID: 950
	public sealed class InstancePathItem
	{
		// Token: 0x0600268E RID: 9870 RVA: 0x000B8DF9 File Offset: 0x000B6FF9
		internal InstancePathItem()
		{
			this.m_indexType = InstancePathItemType.None;
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000B8E16 File Offset: 0x000B7016
		internal InstancePathItem(InstancePathItemType type, int id)
		{
			this.m_indexType = type;
			this.m_indexInCollection = id;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000B8E3A File Offset: 0x000B703A
		internal InstancePathItem(InstancePathItem original)
		{
			this.m_indexType = original.m_indexType;
			this.m_instanceIndex = original.m_instanceIndex;
			this.m_indexInCollection = original.m_indexInCollection;
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000B8E74 File Offset: 0x000B7074
		internal bool IsDynamicMember
		{
			get
			{
				return this.m_indexType == InstancePathItemType.ColumnMemberInstanceIndex || this.m_indexType == InstancePathItemType.RowMemberInstanceIndex || this.m_indexType == InstancePathItemType.ColumnMemberInstanceIndexTopMost;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x000B8E93 File Offset: 0x000B7093
		internal bool IsScope
		{
			get
			{
				return this.IsDynamicMember || this.m_indexType == InstancePathItemType.DataRegion || this.m_indexType == InstancePathItemType.SubReport;
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x000B8EB3 File Offset: 0x000B70B3
		internal bool IsEmpty
		{
			get
			{
				return this.m_indexType == InstancePathItemType.None;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x000B8EBE File Offset: 0x000B70BE
		internal int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000B8EC6 File Offset: 0x000B70C6
		internal int InstanceIndex
		{
			get
			{
				return this.m_instanceIndex;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000B8ECE File Offset: 0x000B70CE
		internal InstancePathItemType Type
		{
			get
			{
				return this.m_indexType;
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000B8ED8 File Offset: 0x000B70D8
		public override int GetHashCode()
		{
			if (this.m_hash == 0)
			{
				this.m_hash = 41999;
				this.m_hash *= this.m_instanceIndex + this.m_indexInCollection + 3;
				this.m_hash = (this.m_hash << (int)(this.m_indexType + 3)) ^ (this.m_hash >> (int)((InstancePathItemType)32 - (this.m_indexType + 3)));
			}
			return this.m_hash;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000B8F48 File Offset: 0x000B7148
		internal void ResetContext()
		{
			this.SetContext(-1);
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000B8F51 File Offset: 0x000B7151
		internal void MoveNext()
		{
			this.SetContext(this.m_instanceIndex + 1);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000B8F61 File Offset: 0x000B7161
		internal void SetContext(int index)
		{
			this.m_instanceIndex = index;
			this.m_hash = 0;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000B8F74 File Offset: 0x000B7174
		internal static void DeepCopyPath(List<InstancePathItem> instancePath, ref List<InstancePathItem> copy)
		{
			if (instancePath == null)
			{
				return;
			}
			int i = 0;
			int count = instancePath.Count;
			if (copy == null)
			{
				copy = new List<InstancePathItem>(count);
				while (i < count)
				{
					copy.Add(new InstancePathItem(instancePath[i]));
					i++;
				}
				return;
			}
			int num = copy.Count;
			if (count == 0)
			{
				if (num > 0)
				{
					copy.Clear();
				}
				return;
			}
			if (num > count)
			{
				int num2 = num - count;
				copy.RemoveRange(count, num2);
				num -= num2;
			}
			while (i < num)
			{
				InstancePathItem instancePathItem = copy[i];
				InstancePathItem instancePathItem2 = instancePath[i];
				instancePathItem.m_hash = 0;
				instancePathItem.m_indexInCollection = instancePathItem2.m_indexInCollection;
				instancePathItem.m_indexType = instancePathItem2.m_indexType;
				instancePathItem.m_instanceIndex = instancePathItem2.m_instanceIndex;
				i++;
			}
			while (i < count)
			{
				copy.Add(new InstancePathItem(instancePath[i]));
				i++;
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000B904C File Offset: 0x000B724C
		internal static bool IsSameScopePath(IInstancePath originalRIFObject, IInstancePath lastRIFObject)
		{
			if (originalRIFObject == null != (lastRIFObject == null))
			{
				return false;
			}
			if (originalRIFObject == null && lastRIFObject == null)
			{
				return true;
			}
			if (originalRIFObject.Equals(lastRIFObject))
			{
				return true;
			}
			List<InstancePathItem> instancePath = originalRIFObject.InstancePath;
			List<InstancePathItem> instancePath2 = lastRIFObject.InstancePath;
			bool flag;
			int sharedPathIndex = InstancePathItem.GetSharedPathIndex(0, instancePath, instancePath2, false, out flag);
			if (flag)
			{
				return true;
			}
			if (sharedPathIndex < 0)
			{
				return false;
			}
			int count = instancePath.Count;
			int count2 = instancePath2.Count;
			int num = sharedPathIndex + 1;
			int num2 = sharedPathIndex + 1;
			while (num < count && !instancePath[num].IsScope)
			{
				num++;
			}
			if (num + 1 == count && instancePath[num].m_indexType == InstancePathItemType.SubReport)
			{
				num = count;
			}
			while (num2 < count2 && !instancePath2[num2].IsScope)
			{
				num2++;
			}
			return num == count && num2 == count2;
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000B911C File Offset: 0x000B731C
		internal static bool IsSamePath(List<InstancePathItem> path1, List<InstancePathItem> path2)
		{
			if (path1 == null != (path2 == null))
			{
				return false;
			}
			if (path1 == null && path2 == null)
			{
				return true;
			}
			if (path1.Count != path2.Count)
			{
				return false;
			}
			bool flag;
			InstancePathItem.GetSharedPathIndex(0, path1, path2, false, out flag);
			return flag;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000B915C File Offset: 0x000B735C
		internal static int GetSharedPathIndex(int startIndexForNewPath, List<InstancePathItem> oldPath, List<InstancePathItem> newPath)
		{
			bool flag;
			return InstancePathItem.GetSharedPathIndex(startIndexForNewPath, oldPath, newPath, false, out flag);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000B9174 File Offset: 0x000B7374
		internal static int GetSharedPathIndex(int startIndexForNewPath, List<InstancePathItem> oldPath, List<InstancePathItem> newPath, bool returnPreviousIndex, out bool identicalPaths)
		{
			identicalPaths = false;
			int num = -1;
			int num2 = -1;
			if (oldPath == null != (newPath == null))
			{
				return num2;
			}
			if (oldPath == null && newPath == null)
			{
				return num2;
			}
			int count = oldPath.Count;
			int count2 = newPath.Count;
			int i = startIndexForNewPath;
			int num3 = startIndexForNewPath;
			if (startIndexForNewPath < 0 || startIndexForNewPath >= count || startIndexForNewPath >= count2)
			{
				return num2;
			}
			while (i < count && num3 < count2)
			{
				while (i < count)
				{
					if (!oldPath[i].IsEmpty)
					{
						break;
					}
					i++;
				}
				while (num3 < count2 && newPath[num3].IsEmpty)
				{
					num3++;
				}
				if (returnPreviousIndex && i < count && num3 + 1 == count2)
				{
					return num;
				}
				if (i < count != num3 < count2)
				{
					return num2;
				}
				if (i == count && num3 == count2)
				{
					break;
				}
				InstancePathItem instancePathItem = oldPath[i];
				InstancePathItem instancePathItem2 = newPath[num3];
				if (instancePathItem.m_indexType != instancePathItem2.m_indexType || instancePathItem.m_indexInCollection != instancePathItem2.m_indexInCollection || instancePathItem.m_instanceIndex != instancePathItem2.m_instanceIndex)
				{
					return num2;
				}
				num = num2;
				num2 = num3;
				i++;
				num3++;
			}
			if (i == count && num3 == count2)
			{
				identicalPaths = true;
			}
			return num2;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000B9294 File Offset: 0x000B7494
		internal static bool IsEmptyPath(int startIndex, List<InstancePathItem> path)
		{
			if (path == null)
			{
				return true;
			}
			Global.Tracer.Assert(startIndex >= 0, "(startIndex >= 0)");
			int count = path.Count;
			if (startIndex != 0)
			{
				if (startIndex >= count)
				{
					return true;
				}
				if (path[startIndex].Type != InstancePathItemType.SubReport)
				{
					return false;
				}
				startIndex++;
			}
			while (path[startIndex].IsEmpty && startIndex < count)
			{
				startIndex++;
			}
			return startIndex == count;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000B9300 File Offset: 0x000B7500
		internal static bool IsValidContext(List<InstancePathItem> path)
		{
			for (int i = 0; i < path.Count; i++)
			{
				InstancePathItem instancePathItem = path[i];
				if (instancePathItem.IsDynamicMember && instancePathItem.InstanceIndex < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000B933C File Offset: 0x000B753C
		internal static List<InstancePathItem> CombineRowColPath(List<InstancePathItem> rowPath, List<InstancePathItem> columnPath)
		{
			int parentDataRegionIndex = InstancePathItem.GetParentDataRegionIndex(rowPath);
			int parentDataRegionIndex2 = InstancePathItem.GetParentDataRegionIndex(columnPath);
			Global.Tracer.Assert(rowPath[parentDataRegionIndex].m_indexInCollection == columnPath[parentDataRegionIndex2].m_indexInCollection && parentDataRegionIndex == parentDataRegionIndex2);
			int num = columnPath.Count - parentDataRegionIndex2 - 1;
			List<InstancePathItem> list = new List<InstancePathItem>(rowPath.Count + num);
			list.AddRange(rowPath);
			if (0 < num)
			{
				columnPath[parentDataRegionIndex2 + 1].m_indexType = InstancePathItemType.ColumnMemberInstanceIndexTopMost;
				for (int i = 0; i < num; i++)
				{
					list.Add(columnPath[parentDataRegionIndex2 + 1 + i]);
				}
			}
			return list;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000B93D8 File Offset: 0x000B75D8
		internal static int GetParentDataRegionIndex(List<InstancePathItem> instancePath)
		{
			if (instancePath == null || instancePath.Count == 0)
			{
				return -1;
			}
			for (int i = instancePath.Count - 1; i >= 0; i--)
			{
				if (InstancePathItemType.DataRegion == instancePath[i].m_indexType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000B9418 File Offset: 0x000B7618
		internal static int GetParentReportIndex(List<InstancePathItem> instancePath, bool isSubreport)
		{
			if (instancePath == null || instancePath.Count == 0)
			{
				return 0;
			}
			int num = instancePath.Count;
			if (isSubreport && instancePath[num - 1].m_indexType == InstancePathItemType.SubReport)
			{
				num--;
			}
			for (int i = num - 1; i >= 0; i--)
			{
				if (instancePath[i].m_indexType == InstancePathItemType.SubReport)
				{
					return i + 1;
				}
			}
			return 0;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000B9473 File Offset: 0x000B7673
		internal static string GenerateUniqueNameString(int id, List<InstancePathItem> instancePath)
		{
			return InstancePathItem.GenerateUniqueNameString(id.ToString(CultureInfo.InvariantCulture), instancePath);
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000B9487 File Offset: 0x000B7687
		internal static string GenerateUniqueNameString(string idString, List<InstancePathItem> instancePath)
		{
			if (instancePath == null || instancePath.Count == 0)
			{
				return idString;
			}
			return idString + "i" + InstancePathItem.GenerateInstancePathString(instancePath, -1);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000B94A8 File Offset: 0x000B76A8
		internal static string GenerateUniqueNameString(int id, List<InstancePathItem> instancePath, int parentInstanceIndex)
		{
			string text = id.ToString(CultureInfo.InvariantCulture);
			if (instancePath == null || instancePath.Count == 0)
			{
				return text;
			}
			return text + "i" + InstancePathItem.GenerateInstancePathString(instancePath, parentInstanceIndex);
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000B94E1 File Offset: 0x000B76E1
		internal static string GenerateInstancePathString(List<InstancePathItem> instancePath)
		{
			return InstancePathItem.GenerateInstancePathString(instancePath, -1);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000B94EC File Offset: 0x000B76EC
		private static string GenerateInstancePathString(List<InstancePathItem> instancePath, int parentInstanceIndex)
		{
			if (instancePath == null || instancePath.Count == 0)
			{
				return "";
			}
			int count = instancePath.Count;
			ReverseStringBuilder reverseStringBuilder = new ReverseStringBuilder(count * 2 + 4);
			bool flag = true;
			bool flag2 = true;
			bool flag3 = parentInstanceIndex >= 0;
			for (int i = count - 1; i >= 0; i--)
			{
				InstancePathItem instancePathItem = instancePath[i];
				switch (instancePathItem.m_indexType)
				{
				case InstancePathItemType.DataRegion:
					InstancePathItem.AppendInteger(ref reverseStringBuilder, instancePathItem.m_indexInCollection);
					reverseStringBuilder.Append('T');
					break;
				case InstancePathItemType.SubReport:
					InstancePathItem.AppendInteger(ref reverseStringBuilder, instancePathItem.m_indexInCollection);
					reverseStringBuilder.Append('S');
					break;
				case InstancePathItemType.ColumnMemberInstanceIndexTopMost:
				case InstancePathItemType.ColumnMemberInstanceIndex:
					if (flag3)
					{
						flag3 = false;
						InstancePathItem.AppendInteger(ref reverseStringBuilder, parentInstanceIndex);
					}
					else
					{
						InstancePathItem.AppendInteger(ref reverseStringBuilder, instancePathItem.m_instanceIndex);
					}
					if (flag)
					{
						flag = false;
						reverseStringBuilder.Append('x');
						InstancePathItem.AppendInteger(ref reverseStringBuilder, instancePathItem.m_indexInCollection);
					}
					reverseStringBuilder.Append('C');
					break;
				case InstancePathItemType.RowMemberInstanceIndex:
					if (flag3)
					{
						flag3 = false;
						InstancePathItem.AppendInteger(ref reverseStringBuilder, parentInstanceIndex);
					}
					else
					{
						InstancePathItem.AppendInteger(ref reverseStringBuilder, instancePathItem.m_instanceIndex);
					}
					if (flag2)
					{
						flag2 = false;
						reverseStringBuilder.Append('x');
						InstancePathItem.AppendInteger(ref reverseStringBuilder, instancePathItem.m_indexInCollection);
					}
					reverseStringBuilder.Append('R');
					break;
				}
			}
			return reverseStringBuilder.ToString();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000B962F File Offset: 0x000B782F
		private static void AppendInteger(ref ReverseStringBuilder builder, int value)
		{
			while (value > 9)
			{
				builder.Append(InstancePathItem.GetIntegerChar(value % 10));
				value /= 10;
			}
			builder.Append(InstancePathItem.GetIntegerChar(value));
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000B965C File Offset: 0x000B785C
		private static char GetIntegerChar(int digit)
		{
			switch (digit)
			{
			case 0:
				return '0';
			case 1:
				return '1';
			case 2:
				return '2';
			case 3:
				return '3';
			case 4:
				return '4';
			case 5:
				return '5';
			case 6:
				return '6';
			case 7:
				return '7';
			case 8:
				return '8';
			case 9:
				return '9';
			default:
				return '0';
			}
		}

		// Token: 0x04001645 RID: 5701
		private int m_instanceIndex = -1;

		// Token: 0x04001646 RID: 5702
		private int m_indexInCollection = -1;

		// Token: 0x04001647 RID: 5703
		private InstancePathItemType m_indexType;

		// Token: 0x04001648 RID: 5704
		private int m_hash;

		// Token: 0x04001649 RID: 5705
		internal const char DefinitionInstanceDelimiter = 'i';

		// Token: 0x0400164A RID: 5706
		internal const char InstancePathDelimiter = 'x';

		// Token: 0x0400164B RID: 5707
		private const char m_0 = '0';

		// Token: 0x0400164C RID: 5708
		private const char m_1 = '1';

		// Token: 0x0400164D RID: 5709
		private const char m_2 = '2';

		// Token: 0x0400164E RID: 5710
		private const char m_3 = '3';

		// Token: 0x0400164F RID: 5711
		private const char m_4 = '4';

		// Token: 0x04001650 RID: 5712
		private const char m_5 = '5';

		// Token: 0x04001651 RID: 5713
		private const char m_6 = '6';

		// Token: 0x04001652 RID: 5714
		private const char m_7 = '7';

		// Token: 0x04001653 RID: 5715
		private const char m_8 = '8';

		// Token: 0x04001654 RID: 5716
		private const char m_9 = '9';
	}
}
