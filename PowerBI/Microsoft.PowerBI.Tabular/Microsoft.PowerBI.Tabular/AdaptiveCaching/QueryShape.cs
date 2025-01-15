using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular.AdaptiveCaching
{
	// Token: 0x02000128 RID: 296
	[DataContract]
	[Serializable]
	internal struct QueryShape : IEquatable<QueryShape>
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x0008B0EC File Offset: 0x000892EC
		public override bool Equals(object obj)
		{
			if (obj is QueryShape)
			{
				QueryShape queryShape = (QueryShape)obj;
				return this.Equals(queryShape);
			}
			return false;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0008B114 File Offset: 0x00089314
		public bool Equals(QueryShape other)
		{
			if (string.Compare(this.Root, other.Root, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (this.Columns == null)
			{
				if (other.Columns != null)
				{
					return false;
				}
			}
			else
			{
				if (other.Columns == null)
				{
					return false;
				}
				if (this.Columns.Count != other.Columns.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Columns.Count; i++)
				{
					if (!this.Columns[i].Equals(other.Columns[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0008B1A8 File Offset: 0x000893A8
		public override int GetHashCode()
		{
			int num = 2133263913;
			num = num * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Root);
			if (this.Columns != null)
			{
				for (int i = 0; i < this.Columns.Count; i++)
				{
					num = num * -1521134295 + this.Columns[i].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0400032B RID: 811
		[DataMember(Name = "root")]
		public string Root;

		// Token: 0x0400032C RID: 812
		[DataMember(Name = "columns")]
		public List<QueryColumn> Columns;
	}
}
