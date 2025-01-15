using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000124 RID: 292
	internal readonly struct DaxColumnRef : IEquatable<DaxColumnRef>
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0002C8E0 File Offset: 0x0002AAE0
		internal DaxColumnRef(string columnName, DaxTableRef tableRef)
		{
			this.ColumnName = columnName;
			this.TableRef = tableRef;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
		public string ColumnName { get; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0002C8F8 File Offset: 0x0002AAF8
		public string TableName
		{
			get
			{
				return this.TableRef.TableName;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0002C913 File Offset: 0x0002AB13
		public DaxTableRef TableRef { get; }

		// Token: 0x06001041 RID: 4161 RVA: 0x0002C91B File Offset: 0x0002AB1B
		public DaxColumnRef ToUnqualifiedReference()
		{
			return new DaxColumnRef(this.ColumnName, DaxTableRef.Empty);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002C930 File Offset: 0x0002AB30
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.ColumnName))
			{
				return string.Empty;
			}
			return DaxIdentifiers.CreateSquareBracketedIdentifierWithPrefix(this.TableRef.ToString(), this.ColumnName);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002C970 File Offset: 0x0002AB70
		public override bool Equals(object obj)
		{
			DaxColumnRef? daxColumnRef = obj as DaxColumnRef?;
			return daxColumnRef != null && this.Equals(daxColumnRef.Value);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
		public override int GetHashCode()
		{
			return DaxRef.NameComparer.GetHashCode(this.ColumnName) ^ this.TableRef.GetHashCode();
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002C9D8 File Offset: 0x0002ABD8
		public bool Equals(DaxColumnRef other)
		{
			return DaxRef.NameComparer.Equals(this.ColumnName, other.ColumnName) && this.TableRef.Equals(other.TableRef);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002CA15 File Offset: 0x0002AC15
		public static bool operator ==(DaxColumnRef left, DaxColumnRef right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002CA1F File Offset: 0x0002AC1F
		public static bool operator !=(DaxColumnRef left, DaxColumnRef right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000A76 RID: 2678
		public static readonly DaxColumnRef Empty;
	}
}
