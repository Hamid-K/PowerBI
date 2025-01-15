using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D3 RID: 467
	[DataContract(Name = "ScriptInput", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ScriptInput : IEquatable<ScriptInput>
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00018471 File Offset: 0x00016671
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00018479 File Offset: 0x00016679
		[DataMember(IsRequired = true, Order = 10)]
		public string VariableName { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00018482 File Offset: 0x00016682
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0001848A File Offset: 0x0001668A
		[DataMember(IsRequired = true, Order = 20)]
		public IList<ScriptInputColumn> Columns { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00018493 File Offset: 0x00016693
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x0001849B File Offset: 0x0001669B
		[DataMember(IsRequired = true, Order = 30)]
		public IList<ScriptInputParameter> Parameters { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x000184A4 File Offset: 0x000166A4
		public List<Tuple<string, List<string>>> DataRoles
		{
			get
			{
				if (this.Columns == null)
				{
					throw new ArgumentNullException("Columns");
				}
				if (this.Columns.Count == 0)
				{
					throw new ArgumentException("Columns should not be empty", "Columns");
				}
				int num = this.Columns.Count((ScriptInputColumn scriptInputColumn) => string.IsNullOrWhiteSpace(scriptInputColumn.Role));
				if (num > 0 && num < this.Columns.Count)
				{
					throw new ArgumentException("In a ScriptInputColumn list, all items should have a non empty Role property or all items should have an empty Role property", "Columns");
				}
				IEnumerable<Tuple<string, List<string>>> enumerable = this.Columns.GroupBy(delegate(ScriptInputColumn scriptInputColumn)
				{
					if (!string.IsNullOrWhiteSpace(scriptInputColumn.Role))
					{
						return scriptInputColumn.Role;
					}
					return ScriptInput.DefaultDataRoleName;
				}, (string dataRole, IEnumerable<ScriptInputColumn> scriptInputColumns) => Tuple.Create<string, List<string>>(dataRole, scriptInputColumns.Select((ScriptInputColumn scriptInputColumn) => scriptInputColumn.Name).ToList<string>()), StringComparer.OrdinalIgnoreCase);
				if (enumerable.Any(delegate(Tuple<string, List<string>> dataRoleWithColumnNames)
				{
					if (dataRoleWithColumnNames.Item2 != null && dataRoleWithColumnNames.Item2.Count<string>() != 0)
					{
						return dataRoleWithColumnNames.Item2.Any((string columnName) => string.IsNullOrWhiteSpace(columnName));
					}
					return true;
				}))
				{
					throw new ArgumentException("Data role to column names association failure", "Columns");
				}
				if (enumerable.Count<Tuple<string, List<string>>>() > 1)
				{
					if (enumerable.Any((Tuple<string, List<string>> dataRoleWithColumnNames) => string.IsNullOrWhiteSpace(dataRoleWithColumnNames.Item1)))
					{
						throw new ArgumentException("When more than 1 data role is available, data role names cannot be empty", "Columns");
					}
				}
				if (enumerable.Count<Tuple<string, List<string>>>() == 0)
				{
					throw new ArgumentException("There must be at least one data role available", "Columns");
				}
				return enumerable.ToList<Tuple<string, List<string>>>();
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00018614 File Offset: 0x00016814
		public bool Equals(ScriptInput other)
		{
			bool? flag = Util.AreEqual<ScriptInput>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return string.Equals(this.VariableName, other.VariableName, StringComparison.Ordinal) && this.Columns.SequenceEqual(other.Columns) && this.Parameters.SequenceEqual(other.Parameters);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00018674 File Offset: 0x00016874
		public override bool Equals(object other)
		{
			return this.Equals(other as ScriptInput);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00018684 File Offset: 0x00016884
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this.Columns != null) ? Hashing.CombineHash<ScriptInputColumn>(this.Columns, null) : 0, (this.Parameters != null) ? Hashing.CombineHash<ScriptInputParameter>(this.Parameters, null) : 0, (this.VariableName != null) ? this.VariableName.GetHashCode() : 0);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000186DC File Offset: 0x000168DC
		public static bool operator ==(ScriptInput left, ScriptInput right)
		{
			bool? flag = Util.AreEqual<ScriptInput>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00018709 File Offset: 0x00016909
		public static bool operator !=(ScriptInput left, ScriptInput right)
		{
			return !(left == right);
		}

		// Token: 0x04000686 RID: 1670
		private static readonly string DefaultDataRoleName = string.Empty;
	}
}
