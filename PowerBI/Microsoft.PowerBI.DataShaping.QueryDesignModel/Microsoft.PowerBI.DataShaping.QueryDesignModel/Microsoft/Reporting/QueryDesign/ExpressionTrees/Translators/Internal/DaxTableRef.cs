using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000149 RID: 329
	internal readonly struct DaxTableRef : IEquatable<DaxTableRef>
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x000314C3 File Offset: 0x0002F6C3
		internal DaxTableRef(string tableName)
		{
			this.TableName = tableName;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x000314CC File Offset: 0x0002F6CC
		public string TableName { get; }

		// Token: 0x060011B3 RID: 4531 RVA: 0x000314D4 File Offset: 0x0002F6D4
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.TableName))
			{
				return string.Empty;
			}
			return "'" + this.TableName.Replace("'", "''") + "'";
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00031510 File Offset: 0x0002F710
		public override bool Equals(object obj)
		{
			DaxTableRef? daxTableRef = obj as DaxTableRef?;
			return daxTableRef != null && this.Equals(daxTableRef.Value);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00031541 File Offset: 0x0002F741
		public override int GetHashCode()
		{
			return DaxRef.NameComparer.GetHashCode(this.TableName);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00031553 File Offset: 0x0002F753
		public bool Equals(DaxTableRef other)
		{
			return DaxRef.NameComparer.Equals(this.TableName, other.TableName);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003156C File Offset: 0x0002F76C
		public static bool operator ==(DaxTableRef left, DaxTableRef right)
		{
			return left.Equals(right);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00031576 File Offset: 0x0002F776
		public static bool operator !=(DaxTableRef left, DaxTableRef right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000ADF RID: 2783
		public static readonly DaxTableRef Empty;
	}
}
